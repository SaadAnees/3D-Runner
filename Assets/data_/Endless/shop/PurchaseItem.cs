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


public class PurchaseItem : MonoBehaviour
{
    public static PurchaseItem instance;
    public List<Text> quantity = new List<Text>();
    public int currentItemId = -1;

    public GameObject purchaseDialog;
    public Text purchaseSuccess;
    public Button yesBtn;
    public Button noBtn;


    public Button addBtn;


    string Url;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Url = Databank.Uri + "/api/user/purchase";
        //PostData();
        //for (int i = 0; i < quantity.Count; i++)
        //{
        //    if(PlayerPrefs.HasKey(("item_" + i)) == false)
        //    {
        //        PlayerPrefs.SetString(("item_" + i), "0");
        //    }
        //    else
        //    {
        //        quantity[i].text = PlayerPrefs.GetString(("item_" + i));
        //    }
        //}
    }


    public void RegisterItem(int itemID, Button _addBtn)
    {
        purchaseDialog.SetActive(true);
        currentItemId = itemID;
        addBtn = _addBtn;
    }

    public void UnRegisterItem()
    {
        purchaseDialog.SetActive(false);
        currentItemId = -1;
        addBtn = null;
    }

    public void PurchaseShopItem()
    {
        yesBtn.interactable = false;
        noBtn.interactable = false;

        if (currentItemId != -1)
        {
            StartCoroutine(PurchaseItemFromShop(currentItemId));
        } else
        {
            yesBtn.interactable = true;
            noBtn.interactable = true;

        }

    }

    //void PostData(int itemid)
    //{
    //    Purchaseitems userdata = new Purchaseitems();
    //    userdata.powerUpId = itemid.ToString();

    //    string myJSON = JsonUtility.ToJson(userdata);
    //    print("MYJSON = " + myJSON);

    //    Hashtable headers = new Hashtable();

    //    //headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IlJqUkZRVFZGUkRJNE1ESTBPVUZCUkRSQ01UVkNPRVkyTUVaQ1JqazNNakEwUXpNNFFVSXlOZyJ9.eyJpc3MiOiJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NWM3ZTExMGNjMTg5OGIxOWQ2N2I5N2Y0IiwiYXVkIjpbImh0dHBzOi8vZGVtby9hcGkiLCJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vdXNlcmluZm8iXSwiaWF0IjoxNTUxNzY1NzgxLCJleHAiOjE1NTQzNTc3ODEsImF6cCI6IlJHellvQlpRMWIxNUpib1VTYmpRMzFKOGNyUk9RWWd1Iiwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCBhZGRyZXNzIHBob25lIiwiZ3R5IjoicGFzc3dvcmQifQ.mMh6YLvS5M4QwczosdIDhCa33f9QvLxuXlKJBzgvtJaJBWOob1Whb66oudNZ5xGgEcj_bRrn9pj-nqByjPCe_KDfm1H2339BZd1_vECjdbct2ecEFt6HBIDY27-f1mBq2N8V_W1W_dsYoOcv6y7wnpH-hGeg5xqbTRdM54NTAGxIJHwK4sQ0StWWUzBADcMgtIxpeUrT4NhmQV-MCfZ3cv9z4Lqnne7tuQ6tV9HkjWno3SRcbjR_kPbtE2L5HUjsbebsvfzMOTiLg_rcnLDcSM5AroO7kj4sQ6vytoLKAqjmRm2z8eOoUdbNA_vCs8EFWH6VbkZ3NkcG8QPtgYURPw");
    //    headers.Add("Authorization", "Bearer " + PlayerPrefs.GetString("ACCESS_TOKEN"));
    //    byte[] body = Encoding.UTF8.GetBytes(myJSON);

    //    WWW www = new WWW(Url, body, headers);

    //    StartCoroutine("PostdataEnumerator", www);

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

    //    for (int i = 0;i< jsonNode["power_up"].Count; i++)
    //    {
    //        print("response 1 = " + jsonNode["power_up"][i]["id"].ToString());
    //        print("response 1 = " + jsonNode["power_up"][i]["powerup"].ToString());
    //        print("response 1 = " + jsonNode["power_up"][i]["quantity"].ToString());
    //         Debug.LogError(jsonNode["power_up"][i]["powerup"].ToString().Replace('"', ' '));
    //         PlayerPrefs.SetString(("item_" + i),jsonNode["power_up"][i]["quantity"]);
    //    }
    //    for (int i = 0; i < jsonNode["power_up"].Count; i++)
    //    {
    //        quantity[i].text = jsonNode["power_up"][i]["quantity"].ToString().Replace('"', ' ');
    //    }


    IEnumerator PurchaseItemFromShop(int _itemId)
    {

        var uwr = new UnityWebRequest(Databank.URL_PurchaseItem, UnityWebRequest.kHttpVerbPOST);

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
            Debug.Log("Purchase-Error While Sending: " + uwr.error);
            purchaseSuccess.text = "FAILED";
        }
        else
        {
            Debug.Log("Purchase-Received: " + uwr.downloadHandler.text);



            //print(uwr.downloadHandler.text);
            var N = JSON.Parse(uwr.downloadHandler.text);

            if (string.Compare(N["status"], "failed") != 0)
            {

                purchaseSuccess.text = "SUCCESS";


                print(N["game_play"]["total_points"].ToString());
                Shop.instance.totalEtherium.text = N["game_play"]["total_points"].ToString();
                Databank.instance.Total_Points = N["game_play"]["total_points"];

                if(_itemId != 4)
                    Consume.instance.ClearItems(_itemId);
                Shop.instance.ClearItems(_itemId);

                //disable armor button after purchase
                if(_itemId == 4 && addBtn != null)
                {
                    addBtn.interactable = false;
                }
            } else
            {
                purchaseSuccess.text = "NOT ENOUGH ETHERIUM";

            }
        }

        yield return new WaitForSeconds(2.0f);

        purchaseSuccess.text = "";
        purchaseDialog.SetActive(false);

        UnRegisterItem();

        yesBtn.interactable = true;
        noBtn.interactable = true;
    }



}
