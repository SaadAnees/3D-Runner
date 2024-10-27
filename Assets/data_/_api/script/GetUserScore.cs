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

public class GetUserScore : MonoBehaviour
{
    public static GetUserScore instance;

    string Url;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Url = Databank.Uri + "/temporary-api/user/score";

    }


    public void Get_UserScore()
    {
        GetData();
    }

    void GetData()
    {

        string myJSON = JsonUtility.ToJson("");
        print("MYJSON = " + myJSON);

        Hashtable headers = new Hashtable();

        print("LoginManager.instance.access_token = " + PlayerPrefs.GetString("ACCESS_TOKEN"));

        headers.Add("Authorization", "Bearer " + PlayerPrefs.GetString("ACCESS_TOKEN"));

        byte[] body = Encoding.UTF8.GetBytes(myJSON);

        WWW www = new WWW(Url, null, headers);

        StartCoroutine("GetdataEnumerator", www);

    }

    string response;

    IEnumerator GetdataEnumerator(WWW www)
    {
        yield return www;
        if (www.error != null)
        {
            Debug.Log("WWW if = " + www.error);
            print("WWW if = " + www.text.ToString());
        }
        else
        {
            Debug.Log("WWW else = " + www.error);
            print("WWW else = " + www.text.ToString());
        }

        response = www.text;

        JSONNode jsonNode = SimpleJSON.JSON.Parse(response);

        print("response 1 = " + jsonNode[0].ToString());
        //print("response 2 = " + jsonNode[1][1].ToString());

        if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
        {
            Invoke("ChangeScene", 1.0f);
        }

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("logout");
    }
}
