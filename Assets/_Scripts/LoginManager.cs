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
using GameData;
using System;

public class LoginManager : MonoBehaviour
{

    public static LoginManager instance;

    public GameObject login1, login2, signup, ForgetPass, LoginCanvas;

    public InputField email;

    public InputField pwd;
    public Text Errormsg;
    bool isLoggedIn = false;
    //string Url;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        if (instance == null)
        {
            instance = this;
        }

        if (isLoggedIn)
            Databank.instance.SignOut();

        if (SignUpManager.isSignup)
        {
            login1.SetActive(false);
            login2.SetActive(true);
            signup.SetActive(false);
        }
        else
        {
            login1.SetActive(true);
            login2.SetActive(false);
            signup.SetActive(true);
        }

        //Url = Databank.Uri + "/api/user/signin";

        //StartCoroutine(FetchLocation());

    }

    float latitude, longitude;

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

        print("latitude" + latitude.ToString());
        print("longitude" + longitude.ToString());

    }



    public void Login()
    {
       
        if (!CanLogin())
        {
            MessageBox.instance.Show("Please Enter Your Info!");
            return;
        }
            

        PostLoginData();
    }

    void PostLoginData()
    {
        LoginData ldata = new LoginData();
#if UNITY_EDITOR
        //if (email.text == "")
        //{
        //    email.text = "baba1";
        //    pwd.text = "1234";
        //}

#endif
        print(email.text);
        ldata.username = email.text.ToString();
        ldata.password = pwd.text.ToString();
        ldata.location = latitude.ToString() + "," + longitude.ToString();

        string myJSON = JsonUtility.ToJson(ldata);

        StartCoroutine(InitiateLogin(myJSON));

    }

    IEnumerator InitiateLogin(string json)
    {
        var uwr = new UnityWebRequest(Databank.URL_SignIn, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        try
        {
            Debug.Log("InitiateLogin Try ");
            if (uwr.isHttpError)
            {
                Debug.Log("InitiateLogin isHttpError: " + uwr.error);
                Errormsg.text = uwr.error;
            }

            if (uwr.isNetworkError)
            {
                Debug.Log("InitiateLogin isNetworkError: " + uwr.error);
                Errormsg.text = uwr.error;
            }
            else
            {
                Debug.Log("InitiateLoginReceived: " + uwr.downloadHandler.text);
                //StartCoroutine("GetPlayerInfo", uwr.downloadHandler.text);
                GetPlayerInfo(uwr.downloadHandler.text);

            }
        }
        catch (Exception)
        {
            Debug.Log("InitiateLogin catch ");
        }

    }

    void GetPlayerInfo(string data)
    {
        //Player.PlayerState playerState = JsonConvert.DeserializeObject<Player.PlayerState>(data);

        //print("playerState " + playerState.message);

        //if (playerState.status == "success")
        //{
        //    Databank.Access_Token = playerState.details.access_token;

        //    PlayerPrefs.SetString(Databank.LOCAL_ACCESS_TOKEN, playerState.details.access_token);

        //    Databank.instance.Level = playerState.levels.level;
        //    Databank.instance.Stage = playerState.levels.stage;
        //    Databank.instance.CheckPoint = playerState.levels.check_point;

        //    Databank.instance.ChallengeId = Convert.ToInt32(playerState.levels.challengeId);
        //    if (Databank.instance.ChallengeId == 15)
        //    {
        //        // This If Conditioon is for temporary purpose to restrict user to phase 1 only
        //        print("If =");
        //        Databank.instance.ChallengeId = Databank.instance.ChallengeId - 1;  
        //    }
            
        //    print("login ChallengeId: " + Databank.instance.ChallengeId);
        //    Databank.instance.Required_Points = playerState.mission.required_points;
        //    Databank.instance.Distance = playerState.mission.distance;
        //    Databank.instance.Time_Limit = playerState.mission.time_limit;

        //    Databank.instance.Total_Points = playerState.game_play.total_points;
        //    Databank.instance.Life = playerState.game_play.life;
        //    Databank.instance.Speed = playerState.game_play.speed;
        //    Databank.instance.Powerup = playerState.game_play.powerup;

        //    Databank.instance.Level_Instruction = playerState.game_message.level_instruction;
        //    Databank.instance.Quotes = playerState.game_message.quotes;

        //    Databank.instance.Name = playerState.user_details.name;
        //    Databank.instance.Email = playerState.user_details.email;
        //    Databank.instance.Gender = playerState.user_details.gender;
        //    Databank.instance.Age = playerState.user_details.age;

        //    Databank.instance.Character = Convert.ToInt32(playerState.character);

        //    Databank.instance.localLevelPlaying = Databank.instance.ChallengeId;
        //    Time.timeScale = 1;
        //    isLoggedIn = true;
        //    Databank.instance.PowerUps();
        //    #region Local Game State
        //    //LocalGameState localGameState = new LocalGameState();
        //    //localGameState.localAccesToken = Databank.Access_Token;
        //    //localGameState.localName = playerState.user_details.name;
        //    //localGameState.localEmail = playerState.user_details.email;
        //    //localGameState.localGender = playerState.user_details.gender;
        //    //localGameState.localAge = playerState.user_details.age;
        //    //localGameState.localCharacter = int.Parse(playerState.character);
        //    //localGameState.localChallengeID = int.Parse(playerState.levels.challengeId);
        //    //string localGS = JsonUtility.ToJson(localGameState);
        //    //Databank.instance.SaveLocalState(localGS);
        //    #endregion

        //    ChangeScene();
        //}
        //else if(playerState.status == "failed")
        //{
           
        //    StartCoroutine(ResetFields());
        //}
        //else
        //{
        //    //something went wrong
        //    StartCoroutine(ResetFields()); // temporary
        //}

        //Errormsg.text = playerState.message;


    }

    IEnumerator ResetFields()
    {
        yield return new WaitForSeconds(2.5f);
        email.text = "";
        pwd.text = "";

        Errormsg.text = "";
    }
    void ChangeScene()
    {
        //SceneManager.LoadScene(Databank.SCENE_CHARACTERSELECTION);
        //return;
        //Databank.instance.AddScore(0);
        if (Databank.instance.Character == 0)
        {
            SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
        }
        else
        {
            SceneManager.LoadScene(Databank.SCENE_MAINMENU);
        }
        //SceneManager.LoadScene("Sequence");
    }

    public void OpenRegister()
    {
        SceneManager.LoadScene(Databank.SCENE_SIGNUP);
    }

    public void ToForgot()
    {
        LoginCanvas.SetActive(false);
        ForgetPass.SetActive(true);
    }

    bool CanLogin()
    {
        bool b = false;
        if (email.text.Length > 0|| pwd.text.Length > 0)
        {
            b = true;
        }
        return b;
    }
}

public class LoginData
{
    public string username;
    public string password;
    public string location;
}