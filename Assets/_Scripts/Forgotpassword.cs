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

public class Forgotpassword : MonoBehaviour
{

    public static Forgotpassword instance;

    public InputField email;
    public GameObject LoginCanvas;
    public InputField pwd;
    public InputField pwd1;
    public Text Errormsg;
    public bool isloadingapi;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void Back()
    {
        CloseThis();
    }

    private void Update()
    {
#if UNITY_ANDROID

        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("login");
        //}

#endif

       

        if (pwd.text.Length < 4)
        {
            Errormsg.enabled = true;
            Errormsg.text = "SET MINIMUM 4 CHARACTERS";
        }
        else
        {
            if(pwd.text.Length > 0 || pwd1.text.Length > 0)
            {
                print("if");
                if (pwd.text != pwd1.text)
                {
                    Errormsg.enabled = true;
                    Errormsg.text = "PASSWORD DO NOT MATCH";

                }
                else
                {
                    print("else");
                    if (email.text.Length > 0)
                    {
                        Errormsg.enabled = true;
                        Errormsg.text = "SET USERNAME";
                    }
                    else
                        Errormsg.enabled = false;
                }
                   
            }
           
            else if(pwd.text.ToString() == pwd1.text.ToString() && pwd1.text.ToString() != "")
            {
                 
                Errormsg.text = "";
            }
            else
            {
                if (isloadingapi)
                {
                    Errormsg.text = "";
                }
                else
                {
                   
                }
                //pwdinfo_txt.SetActive(false);
                //pwdinfo_txt.GetComponent<Text>().text = "";
            }
        }

    }

    public void Resetpass()
    {
        ForgotpasswordDATA ldata = new ForgotpasswordDATA();

        ldata.username = email.text.ToString();
        ldata.password = pwd.text.ToString();

        string myJSON = JsonUtility.ToJson(ldata);
      
        StartCoroutine(InitiatePasswordReset(myJSON));

    }


    public IEnumerator InitiatePasswordReset(string json)
    {
        print(json);
        print(pwd.text.ToString());
        print(pwd1.text.ToString());

        if (pwd.text.ToString() == pwd1.text.ToString() && pwd1.text.ToString() != "")
        {
            Errormsg.text = "PASSWORD DO NOT MATCH";
            isloadingapi = true;
            var uwr = new UnityWebRequest(Databank.URL_PasswordReset, UnityWebRequest.kHttpVerbPOST);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("InitiatePasswordReset Error While Sending: " + uwr.error);
            }
            else
            {
                isloadingapi = false;
                Debug.Log("InitiatePasswordReset Received: " + uwr.downloadHandler.text);

                JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);


                Errormsg.text = jsonNode[2].ToString().ToUpper();
                if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
                {
                    ClearTextBox();
                    //Message
                }
                else if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("failed"))
                {
                   
                    //Message

                }

                Invoke("CloseThis", 1f);
            }
        }

      

    }

    void CloseThis()
    {
        ClearTextBox();
        LoginCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void ClearTextBox()
    {
        email.text = "";
        pwd.text = "";
        pwd1.text = "";
    }
}

public class ForgotpasswordDATA
{
    public string username;
    public string password;
}
