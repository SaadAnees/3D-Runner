using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using System.Text;
using SimpleJSON;
using TMPro;


public class Quote : MonoBehaviour
{
    public static Quote instance;
    public GameObject RTLText;
    string Url;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Url = Databank.Uri + "/api/quote";

        GetQuote();

    }

    public void GetQuote()
    {
        PostData();
    }

    void PostData()
    {

        StartCoroutine("PostdataEnumerator");

    }

    string response;

    public Text quote;

    IEnumerator PostdataEnumerator()
    {
        UnityWebRequest uwr = UnityWebRequest.Get(Url);
    ;
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Quotes: " + uwr.downloadHandler.text);

            response = uwr.downloadHandler.text;

            JSONNode jsonNode = SimpleJSON.JSON.Parse(response);

            print("Current Language " + Databank.instance.Language);
            if(Databank.instance.Language == "ar")
            {
                print("This " + jsonNode[1]);
                RTLText.GetComponent<TextMeshProUGUI>().text = jsonNode[1].ToString().Replace('"', ' ').Trim();
                quote.text = "";
            }
            else
            {
                RTLText.GetComponent<TextMeshProUGUI>().text = "";
                quote.text = jsonNode[1].ToString().Replace('"', ' ').Trim();
                print("This " + jsonNode[1]);
            }
                


        }


       

        //quote.text = Databank.instance.Quotes;
    }

}
