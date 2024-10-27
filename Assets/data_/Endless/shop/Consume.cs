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
using System.Linq;
using GameData;

public class Consume : MonoBehaviour
{
    public static Consume instance;
    public List<int> ItemId = new List<int>();
    public List<string> Itemname = new List<string>();
    public List<int> ItemQuantity = new List<int>();
    public List<GameObject> ItemDisplay = new List<GameObject>();


    public GameObject powerUpDialog;
    public Button yesBtn;
    public Button noBtn;


    public int markedItemIndex = -1;
    public string markedItemName = "";

    public Text powerUpselectedText;

    public GameObject myItemPrefab;
    public GameObject myItemParent;

    public GameObject ArmorButton;

    string Url;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        //Url = Databank.Uri + "/api/user/purchase";
        GetUserItems();

    }

    public void GetUserItems()
    {
        //ConsumeItems(id);
        StartCoroutine(GetUserItemsCoroutine());
    }

    IEnumerator GetUserItemsCoroutine()
    {
        var uwr = new UnityWebRequest(Databank.URL_ItemList, UnityWebRequest.kHttpVerbGET);
        byte[] bytes = Encoding.UTF8.GetBytes(Databank.Access_Token);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);
        uh.contentType = "application/json";
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);
        print("Comes here");
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Shop Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Shop Received: " + uwr.downloadHandler.text);
            
            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);

            //print(uwr.downloadHandler.text);

            var N = JSON.Parse(uwr.downloadHandler.text);

            int count = N["details"].Count;

            //for each entry, get the item and populate
            for (int i = 0; i < count; ++i)
            {
                if (int.Parse(N["details"][i]["id"]) != 4)
                {
                    GameObject myItem = Instantiate(myItemPrefab, myItemParent.transform);

                    try
                    {

                        Itemname.Add(N["details"][i]["powerup"]);
                        ItemQuantity.Add(N["details"][i]["quantity"]);
                        ItemId.Add(int.Parse(N["details"][i]["id"]));

                        myItem.GetComponent<MyPowerUps>().itemName.text = N["details"][i]["powerup"];
                        myItem.GetComponent<MyPowerUps>().itemQuantity.text = N["details"][i]["quantity"];
                        myItem.GetComponent<MyPowerUps>().itemId = int.Parse(N["details"][i]["id"]);


                        if (string.Equals(myItem.GetComponent<MyPowerUps>().itemName.text, "Shield"))
                        {
                            myItem.GetComponent<MyPowerUps>().itemImage.sprite = Shop.instance.itemImages[2];

                        }
                        else if (string.Equals(myItem.GetComponent<MyPowerUps>().itemName.text, "Hoverboard"))
                        {
                            myItem.GetComponent<MyPowerUps>().itemImage.sprite = Shop.instance.itemImages[0];

                        }
                        else if (string.Equals(myItem.GetComponent<MyPowerUps>().itemName.text, "Jetpack"))
                        {
                            myItem.GetComponent<MyPowerUps>().itemImage.sprite = Shop.instance.itemImages[1];

                        }
                        //else if (string.Equals(myItem.GetComponent<MyPowerUps>().itemName.text, "Armor"))
                        //{
                        //    myItem.GetComponent<MyPowerUps>().itemImage.sprite = Shop.instance.itemImages[4];

                        //}

                        ItemDisplay.Add(myItem);


                    }
                    catch
                    {

                    }
                }
                else
                {
                    ArmorButton.SetActive(true);
                }

            }

        }
    }

    //void PostData()
    //{
    //    Consumeitm userdata = new Consumeitm();
    //    userdata.powerUpId = "1";

    //    string myJSON = JsonUtility.ToJson(userdata);
    //    print("MYJSON = " + myJSON);

    //    Hashtable headers = new Hashtable();

    //    print("LoginManager.instance.access_token = " + PlayerPrefs.GetString("ACCESS_TOKEN"));

    //    //headers.Add("Authorization", "Bearer " + PlayerPrefs.GetString("ACCESS_TOKEN"));

    //    //headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IlJqUkZRVFZGUkRJNE1ESTBPVUZCUkRSQ01UVkNPRVkyTUVaQ1JqazNNakEwUXpNNFFVSXlOZyJ9.eyJpc3MiOiJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NWM3M2U1MGYzN2FjYTYzMmQyM2FiNTg4IiwiYXVkIjpbImh0dHBzOi8vZGVtby9hcGkiLCJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vdXNlcmluZm8iXSwiaWF0IjoxNTUxNjk2MzI4LCJleHAiOjE1NTQyODgzMjgsImF6cCI6IlJHellvQlpRMWIxNUpib1VTYmpRMzFKOGNyUk9RWWd1Iiwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCBhZGRyZXNzIHBob25lIiwiZ3R5IjoicGFzc3dvcmQifQ.NoHxQZ_C8zHm1nMRtBKK3lxlwgM0Uk27iNviSCvM3452xY01WDlUipzS2oqAwAUIwWbydqkRSt9hRDJuJHnKaJuLc8AcoCxohj-rHNrfNVB48925z4MW5oEhM_5f0qjWmuOLySGPr5JfaxZS35gQeD9Wd4K_IgNn24fqX6u3f0Pxo4Um1YdBKHjPJfEyCLq8nFhslbEq0B8-GM9kHZjD2XaoL8gnkTXnuewmaA_zOtqBQBs9KatOay-P_FI_0TQVtLTflbXbKZgqfXMpnStkvTZ3OnAuMyH5o3PppCUFsHDS1KL9rnOM6wnYmjRwM7-rrCUAFZK8Hr5bTfu7-LIFMw");
    //    headers.Add("Authorization", "Bearer " + PlayerPrefs.GetString("ACCESS_TOKEN"));
    //    byte[] body = Encoding.UTF8.GetBytes(myJSON);
    //    UnityWebRequest www = UnityWebRequest.Put(Url, body);
    //    //WWW www = new WWW(Url, body, headers);

    //    StartCoroutine("PostdataEnumerator", www);

    //}
    //public string ConsumeItems(int id)
    //{

    //    Consumeitm userdata = new Consumeitm();
    //    userdata.powerUpId = id.ToString();

    //    string myJSON = JsonUtility.ToJson(userdata);
    //    Debug.Log(myJSON);
    //    try
    //    {
    //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
    //        httpWebRequest.ContentType = "application/json";
    //        httpWebRequest.Method = "PUT";
    //        //httpWebRequest.Headers.Add("Authorization:Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IlJqUkZRVFZGUkRJNE1ESTBPVUZCUkRSQ01UVkNPRVkyTUVaQ1JqazNNakEwUXpNNFFVSXlOZyJ9.eyJpc3MiOiJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NWM3ZTExMGNjMTg5OGIxOWQ2N2I5N2Y0IiwiYXVkIjpbImh0dHBzOi8vZGVtby9hcGkiLCJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vdXNlcmluZm8iXSwiaWF0IjoxNTUxNzY1NzgxLCJleHAiOjE1NTQzNTc3ODEsImF6cCI6IlJHellvQlpRMWIxNUpib1VTYmpRMzFKOGNyUk9RWWd1Iiwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCBhZGRyZXNzIHBob25lIiwiZ3R5IjoicGFzc3dvcmQifQ.mMh6YLvS5M4QwczosdIDhCa33f9QvLxuXlKJBzgvtJaJBWOob1Whb66oudNZ5xGgEcj_bRrn9pj-nqByjPCe_KDfm1H2339BZd1_vECjdbct2ecEFt6HBIDY27-f1mBq2N8V_W1W_dsYoOcv6y7wnpH-hGeg5xqbTRdM54NTAGxIJHwK4sQ0StWWUzBADcMgtIxpeUrT4NhmQV-MCfZ3cv9z4Lqnne7tuQ6tV9HkjWno3SRcbjR_kPbtE2L5HUjsbebsvfzMOTiLg_rcnLDcSM5AroO7kj4sQ6vytoLKAqjmRm2z8eOoUdbNA_vCs8EFWH6VbkZ3NkcG8QPtgYURPw");
    //        httpWebRequest.Headers.Add("Authorization", "Bearer " + PlayerPrefs.GetString("ACCESS_TOKEN"));
    //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    //        {
    //            streamWriter.Write(myJSON);
    //            streamWriter.Flush();
    //            streamWriter.Close();
    //        }

    //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //        {
    //            var response = streamReader.ReadToEnd();
    //            JSONNode jsonNode = SimpleJSON.JSON.Parse(response);
    //            Debug.Log(response);

    //            Debug.LogError(jsonNode[0].ToString().Replace('"', ' '));
    //            Debug.LogError(jsonNode["power_up"].Count);

    //            //if (jsonNode[0].ToString() == "success")
    //            //{

    //            for (int i = 0; i < jsonNode["power_up"].Count; i++)
    //            {
    //                print("response 1 = " + jsonNode["power_up"][i]["id"].ToString());
    //                print("response 1 = " + jsonNode["power_up"][i]["powerup"].ToString());
    //                print("response 1 = " + jsonNode["power_up"][i]["quantity"].ToString());
    //                PlayerPrefs.SetString(("item_" + i), jsonNode["power_up"][i]["quantity"]);
    //            }
    //            return response;
    //        }

    //    }
    //    catch (System.Exception e)
    //    {
    //        // Your catch here
    //        print(e.ToString());
    //        return "";
    //    }

    //}

    //string response;

    //IEnumerator PostdataEnumerator(WWW www)
    //{
    //    yield return www;
    //    if (www.error != null)
    //    {
    //        Debug.Log("WWW if = " + www.error);
    //        print("WWW if = " + www.text.ToString());
    //    }
    //    else
    //    {
    //        Debug.Log("WWW else = " + www.error);
    //        print("WWW else = " + www.text.ToString());
    //    }

    //    response = www.text;

    //    JSONNode jsonNode = SimpleJSON.JSON.Parse(response);
    //    Debug.LogError(jsonNode[0].ToString().Replace('"', ' '));
    //    Debug.LogError(jsonNode["power_up"].Count);

    //    //if (jsonNode[0].ToString() == "success")
    //    //{

    //    for (int i = 0; i < jsonNode["power_up"].Count; i++)
    //    {
    //        print("response 1 = " + jsonNode["power_up"][i]["id"].ToString());
    //        print("response 1 = " + jsonNode["power_up"][i]["powerup"].ToString());
    //        print("response 1 = " + jsonNode["power_up"][i]["quantity"].ToString());
    //    }

    //    PlayerPrefs.SetString("POWERUP", "");
    //    PlayerPrefs.SetString("POWER_DES", "");
    //    PlayerPrefs.SetString("POWER_COST", "");
    //    //}

    //}

    //void ChangeScene()
    //{
    //    SceneManager.LoadScene("login");
    //}


    public void RegisterMarkItem(int itemIndex, string itemName)
    {
        markedItemIndex = itemIndex;
        markedItemName = itemName;
        powerUpDialog.SetActive(true);
    }

    public void UnRegisterMarkItem()
    {
        markedItemIndex = -1;
        markedItemName = "";
        powerUpDialog.SetActive(false);
        powerUpselectedText.text = "";
    }

    public void MarkItem()
    {
        yesBtn.interactable = false;
        noBtn.interactable = false;
        if (markedItemIndex != -1 && markedItemIndex != 4)
        {
            int index = ItemId.FindIndex(a => a == markedItemIndex);
            if (ItemQuantity[index] > 0)
            {
                StartCoroutine(MarkItemCoroutine(markedItemIndex, markedItemName));
            }
            else
            {
                foreach (var item in ItemQuantity)
                {
                    print(item);
                }
                print(index);
               print("You dont have enough to consume");
            }
        }
        else if (markedItemIndex == 4)
        {
            StartCoroutine(MarkItemCoroutine(4, "Armor"));

        }
        else
        {
            yesBtn.interactable = true;
            noBtn.interactable = true;

        }

    }


    IEnumerator MarkItemCoroutine(int _itemId, string _itemName)
    {
        print(_itemName + "" + _itemId);
        var uwr = new UnityWebRequest(Databank.URL_ItemList, UnityWebRequest.kHttpVerbPOST);

        string json = "{\"powerUpId\":\"" + _itemId + "\"}";
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Mark-Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Mark-Received: " + uwr.downloadHandler.text);
            //GetUserItems();
            //print(uwr.downloadHandler.text);
        }

        //powerUpDialog.GetComponentInChildren<Text>().text = _itemName.ToUpper() + " IS SELECTED";
        powerUpselectedText.text = _itemName.ToUpper() + " IS SELECTED";

        powerUpDialog.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        powerUpDialog.SetActive(false);
        //powerUpDialog.GetComponentInChildren<Text>().text = "";

        UnRegisterMarkItem();

        yesBtn.interactable = true;
        noBtn.interactable = true;


        Databank.instance.Powerup = _itemId;
        Databank.instance.PowerupCount = 1; // because marking is only possible when you have purchased
           
        print("Databank " + Databank.instance.Powerup);
    }

    public void ClearItems(int _itemId)
    {
        GameObject item = ItemDisplay.Find(i => i.GetComponent<MyPowerUps>().itemId == _itemId);
        int temp = int.Parse(item.GetComponent<MyPowerUps>().itemQuantity.text) + 1;
        item.GetComponent<MyPowerUps>().itemQuantity.text = temp.ToString();
    }


}
//public class Consumeitm
//{
//    public string powerUpId;
//}
