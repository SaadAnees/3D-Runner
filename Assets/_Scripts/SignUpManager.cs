using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using System.Text;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;

public class SignUpManager : MonoBehaviour
{

    public static SignUpManager instance;

    public GameObject info_txt, pwdinfo_txt;
    public bool isloadingapi;

    public Button SignUpBtn;
    public Text gendertxt, genderlabel;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }

        device_UUID = SystemInfo.deviceUniqueIdentifier;

        //print("device_UUID = " + device_UUID);

        StartCoroutine(FetchLocation());

       // Url = Databank.Uri + "/api/user/signup";

    }

    #region SIGNUP

    string device_UUID;

    float latitude, longitude;

    public Text bdate, pwd, gender;

    public InputField pwd1, pwd2, email, firstnm;

    int genderID;

    string pwd_pattern;

    bool checkPWD = true;

    Match match;

    public void GenderText()
    {
        print("GENDER: " + genderlabel.text);
        gendertxt.text = genderlabel.text;
    }

    private void Update()
    {
        if(firstnm.text.Length <= 0 ||
            email.text.Length <= 0 ||
            pwd.text.Length <= 0 ||
            gender.text.Length <= 0 ||
            pwd1.text.Length <= 0){

            SignUpBtn.interactable = false;
        }


        if (pwd1.text.Length < 4)
        {
            pwdinfo_txt.SetActive(true);
            pwdinfo_txt.GetComponent<Text>().text = "SET MINIMUM 4 CHARACTERS";
            SignUpBtn.interactable = false;

        }
        else
        {
            if (pwd1.text.ToString() != pwd2.text.ToString() && pwd2.text.ToString() != "")
            {
                pwdinfo_txt.SetActive(true);
                pwdinfo_txt.GetComponent<Text>().text = "PASSWORD DO NOT MATCH";
                isloadingapi = true;
                SignUpBtn.interactable = false;
            }
            else
            {
                if (isloadingapi)
                {
                    pwdinfo_txt.GetComponent<Text>().text = "";
                    SignUpBtn.interactable = true;
                }
                else
                {

                }
                //pwdinfo_txt.SetActive(false);
                //pwdinfo_txt.GetComponent<Text>().text = "";
            }
        }

#if UNITY_ANDROID

        //if(Input.GetKey(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("login");
        //}

#endif

    }

    IEnumerator FetchLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        //print("latitude" + latitude.ToString());
        //print("longitude" + longitude.ToString());

    }

    public void SignUp()
    { 

        if (gender.text.ToString() == "MALE" || gender.text.ToString() == "ﺮﻛﺬﻟا")
        {
            genderID = 1;
        }
        if (gender.text.ToString() == "FEMALE" || gender.text.ToString() == "ﺎﺛﺎﻧإ")
        {
            genderID = 2;
        }
        print("GENDER: " + gender.text.ToString());
        //print("GENDER ID = " + genderID);

        //FOR PASSWORD

        pwd_pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

        //checkPWD = isPwd();
        checkPWD = isPwd_4length();


        //print("checkPWD = " + checkPWD);

        //if(checkPWD)
        //{
        //    pwdinfo_txt.SetActive(false);
        //    PostData();
        //}
        //else
        //{
        //    pwdinfo_txt.SetActive(true);
        //    pwdinfo_txt.GetComponent<Text>().text = "Set MINIMUM 4 CHARACTERS";//.SetActive(true);
        //}

        PostData();

    }

    bool isPwd()
    {
        return Regex.IsMatch(pwd1.text.ToString(), pwd_pattern);
    }
    bool isPwd_4length()
    {
        if (pwd1.text.Length >= 4)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    void PostData()
    {
        UserData user = new UserData();
        user.uuid = device_UUID;
        user.name = firstnm.text.ToString();
        //user.lastname = lastnm.text.ToString();
        user.email = email.text.ToString();
        user.genderId = genderID;
        user.birthday = bdate.text.ToString();
        user.password = pwd1.text.ToString();
        user.location = latitude.ToString() + "," + longitude.ToString();

        string myJSON = JsonUtility.ToJson(user);
        print("MYJSON = " + myJSON);

      
        //var response = Signup(myJSON);
        //StartCoroutine("SignUpEnumerator", response);

        StartCoroutine(Signup(myJSON));
    }
    public IEnumerator Signup(string json)
    {
      
        var uwr = new UnityWebRequest(Databank.URL_SignUp, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            info_txt.GetComponent<Text>().text = uwr.error;
            StartCoroutine(ResetFields());
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            //info_txt.GetComponent<Text>().text = uwr.downloadHandler.text;
            //StartCoroutine("GetPlayerInfo", uwr.downloadHandler.text);
            GetSignUpDetails(uwr.downloadHandler.text);

        }


    }

    void GetSignUpDetails(string data)
    {

        print(data);
        JSONNode jsonNode = SimpleJSON.JSON.Parse(data);

        print("response 1 = " + jsonNode[0].ToString());
        print("response 2 = " + jsonNode[2].ToString());

        jsonNode[0].ToString().Replace('"', ' ');
        info_txt.SetActive(true);
        if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
        {
            isSignup = true;
            
            info_txt.GetComponent<Text>().text = jsonNode[2].ToString().Replace('"', ' ');
            Invoke("ChangeScene", 1f);
            //ChangeScene();
            isloadingapi = true;
        }
        else if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("failed"))
        {
            //info_txt.SetActive(true);
            isloadingapi = false;
           // pwdinfo_txt.GetComponent<Text>().text = jsonNode[2].ToString().Replace('"', ' ');
            info_txt.GetComponent<Text>().text = jsonNode[2].ToString().Replace('"', ' ');
            StartCoroutine(ResetFields());
        }
        else
        {
            isloadingapi = false;
           
            info_txt.GetComponent<Text>().text = jsonNode[2].ToString().Replace('"', ' ');
            ResetFields();
        }
    }

    IEnumerator ResetFields()
    {
        firstnm.text = "";
        email.text = "";
        pwd.text = "";
        genderlabel.text = "GENDER";
        gendertxt.text = "GENDER";
        pwd1.text = "";
        pwd2.text = "";
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(Databank.SCENE_SIGNUP);
        info_txt.GetComponent<Text>().text = "";
    }

    public static bool isSignup;

    void ChangeScene()
    {
        SceneManager.LoadScene(Databank.SCENE_LOGIN);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(Databank.SCENE_LOGIN);
    }

  
}

public class UserData
{
    public string uuid;
    public string name;
    //public string lastname;
    public string email;
    public int genderId;
    public string birthday;
    public string password;
    public string location;
}

#endregion

