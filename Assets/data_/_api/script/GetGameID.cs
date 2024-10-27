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

public class GetGameID : MonoBehaviour
{
    public static GetGameID instance;

    string Url;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Url = Databank.Uri + "/api/game";

    }


    public void Get_GameID()
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

    string endlessID, sortingID, oddoneoutID, tilematchingID, blockgameID;

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
        print("response 2 = " + jsonNode[1][0][0].ToString());
        print("response 3 = " + jsonNode[1][1][0].ToString());
        print("response 4 = " + jsonNode[1][2][0].ToString());
        print("response 5 = " + jsonNode[1][3][0].ToString());
        print("response 6 = " + jsonNode[1][4][0].ToString());

        endlessID = jsonNode[1][0][0].ToString().Replace('"',' ').Trim();
        oddoneoutID = jsonNode[1][1][0].ToString().Replace('"', ' ').Trim();
        sortingID = jsonNode[1][2][0].ToString().Replace('"', ' ').Trim();
        tilematchingID = jsonNode[1][3][0].ToString().Replace('"', ' ').Trim();
        blockgameID = jsonNode[1][4][0].ToString().Replace('"', ' ').Trim();


        PlayerPrefs.SetString("ENDLESSID",endlessID);
        PlayerPrefs.SetString("ODDONEOUTID", oddoneoutID);
        PlayerPrefs.SetString("SORTINGID", sortingID);
        PlayerPrefs.SetString("TILEMATCHID", tilematchingID);
        PlayerPrefs.SetString("BLOCKGAMEID", blockgameID);



        if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
        {
            Invoke("ChangeScene", 1.0f);
        }

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("GetGame");
    }
}
