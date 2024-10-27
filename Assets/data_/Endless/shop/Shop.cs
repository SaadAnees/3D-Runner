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

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public List<int> ItemId = new List<int>();
    public List<string> Itemname = new List<string>();
    public List<int> ItemPrice = new List<int>();
    public List<GameObject> shopItems = new List<GameObject>();


    public List<Text> Prices = new List<Text>();
    public List<Text> Names = new List<Text>();

    public GameObject shopItemPrefab;
    public GameObject shopParent;

    public List<Sprite> itemImages = new List<Sprite>();
    public Text totalEtherium;
    //public List<Text> quantity = new List<Text>();

    public GameObject boy, girl;



    string Url;

    // Start is called before the first frame update
    void Start()
    {

        if (Databank.instance.Character == 1)
        {
            boy.SetActive(false);
            girl.SetActive(true);
        }
        if (Databank.instance.Character == 2)
        {
            boy.SetActive(true);
            girl.SetActive(false);
        }


        totalEtherium.text = Databank.instance.Total_Points.ToString();
        //PlayerPrefs.DeleteAll();
        if (instance == null)
        {
            instance = this;
        }

        Url = Databank.Uri + "/api/powerups";
        PostData();
    }

    public void GetShopItem()
    {
        PostData();
    }

    void PostData()
    {

        //string myJSON = JsonUtility.ToJson("");

        //Hashtable headers = new Hashtable();
        ////headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IlJqUkZRVFZGUkRJNE1ESTBPVUZCUkRSQ01UVkNPRVkyTUVaQ1JqazNNakEwUXpNNFFVSXlOZyJ9.eyJpc3MiOiJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NWM3ZTExMGNjMTg5OGIxOWQ2N2I5N2Y0IiwiYXVkIjpbImh0dHBzOi8vZGVtby9hcGkiLCJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vdXNlcmluZm8iXSwiaWF0IjoxNTUxNzY1NzgxLCJleHAiOjE1NTQzNTc3ODEsImF6cCI6IlJHellvQlpRMWIxNUpib1VTYmpRMzFKOGNyUk9RWWd1Iiwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCBhZGRyZXNzIHBob25lIiwiZ3R5IjoicGFzc3dvcmQifQ.mMh6YLvS5M4QwczosdIDhCa33f9QvLxuXlKJBzgvtJaJBWOob1Whb66oudNZ5xGgEcj_bRrn9pj-nqByjPCe_KDfm1H2339BZd1_vECjdbct2ecEFt6HBIDY27-f1mBq2N8V_W1W_dsYoOcv6y7wnpH-hGeg5xqbTRdM54NTAGxIJHwK4sQ0StWWUzBADcMgtIxpeUrT4NhmQV-MCfZ3cv9z4Lqnne7tuQ6tV9HkjWno3SRcbjR_kPbtE2L5HUjsbebsvfzMOTiLg_rcnLDcSM5AroO7kj4sQ6vytoLKAqjmRm2z8eOoUdbNA_vCs8EFWH6VbkZ3NkcG8QPtgYURPw");
        //headers.Add("Authorization", "Bearer " + Databank.Access_Token);
        //byte[] body = Encoding.UTF8.GetBytes(myJSON);

        //WWW www = new WWW(Url, null, headers);
        //StartCoroutine("PostdataEnumerator", www);

        StartCoroutine(GetShopItems());

    }

    string response;

    //IEnumerator PostdataEnumerator(WWW www)
    //{
    //    yield return www;
    //    if (www.error != null)
    //    {
    //        Debug.Log("WWW if = " + www.error);
    //    }
    //    else
    //    {
    //        Debug.Log("WWW else = " + www.error);
    //    }

    //    response = www.text;

    //    JSONNode jsonNode = SimpleJSON.JSON.Parse(response);
    //    Debug.LogError(jsonNode[0].ToString().Replace('"', ' '));

    //    //if (jsonNode[0].ToString().Replace('"', ' ') == "success")
    //    //{

    //        print("response 1 = " + response);
    //        for (int i = 0; i < jsonNode[1].Count; i++)
    //        {
    //            Debug.Log(int.Parse(jsonNode[1][i]["id"]));
    //            ItemId.Add(int.Parse(jsonNode[1][i]["id"]));
    //            Itemname.Add(jsonNode[1][i]["powerup"]);
    //            ItemPrice.Add(int.Parse(jsonNode[1][i]["requiredPoints"]));
    //        }
    //    for (int i = 0; i < jsonNode[1].Count; i++)
    //    {
    //        Prices[i].text = jsonNode[1][i]["requiredPoints"];
    //        Names[i].text = jsonNode[1][i]["powerup"];
    //    }
    //    //}

    //}


    IEnumerator GetShopItems()
    {
        var uwr = new UnityWebRequest(Databank.URL_ItemList, UnityWebRequest.kHttpVerbGET);
        byte[] bytes = Encoding.UTF8.GetBytes(Databank.Access_Token);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);
        uh.contentType = "application/json";
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);
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

            print(uwr.downloadHandler.text);

            var N = JSON.Parse(uwr.downloadHandler.text);

            int count = N["details"].Count;

            //for each entry, get the item and populate
            for (int i = 0; i < count; ++i)
            {
                GameObject shopItem = Instantiate(shopItemPrefab, shopParent.transform);
                try
                {
                    shopItem.GetComponent<ShopItem>().itemName.text = N["details"][i]["powerup"];
                    shopItem.GetComponent<ShopItem>().itemPointsRequired.text = N["details"][i]["cost"];
                    shopItem.GetComponent<ShopItem>().itemId = int.Parse(N["details"][i]["id"]);
                    shopItem.GetComponent<ShopItem>().itemQuantity.text = N["details"][i]["quantity"];


                    if (string.Equals(shopItem.GetComponent<ShopItem>().itemName.text, "Shield"))
                    {
                        shopItem.GetComponent<ShopItem>().itemImage.sprite = itemImages[2];

                    }
                    else if (string.Equals(shopItem.GetComponent<ShopItem>().itemName.text, "Hoverboard"))
                    {
                        shopItem.GetComponent<ShopItem>().itemImage.sprite = itemImages[0];

                    }
                    else if (string.Equals(shopItem.GetComponent<ShopItem>().itemName.text, "Jetpack"))
                    {
                        shopItem.GetComponent<ShopItem>().itemImage.sprite = itemImages[1];

                    }
                    else if (string.Equals(shopItem.GetComponent<ShopItem>().itemName.text, "Armor"))
                    {
                        shopItem.GetComponent<ShopItem>().itemImage.sprite = itemImages[4];
                        if(int.Parse(N["details"][i]["quantity"]) > 0)
                        {
                            shopItem.GetComponent<ShopItem>().purchaseButton.interactable = false;
                        }
                    }
                    

                    shopItems.Add(shopItem);
                }
                catch
                {

                }

            }

        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void ClearItems(int _itemId)
    {
        GameObject item = shopItems.Find(i => i.GetComponent<ShopItem>().itemId == _itemId);
        int temp = int.Parse(item.GetComponent<ShopItem>().itemQuantity.text) + 1;
        item.GetComponent<ShopItem>().itemQuantity.text = temp.ToString();
    }
}
