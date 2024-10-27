using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
   
    void Awake()
    {
        StartCoroutine(NetworkdCampetibility("https://www.google.com"));
       
    }
    
    void Start()
    {
        InvokeRepeating("InternetStatus", 10f, 15f);
    }

    void InternetStatus()
    {
        StartCoroutine(NetworkdCampetibility("https://www.google.com"));
    }
    IEnumerator NetworkdCampetibility(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {

                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Network Available");
                Databank.instance.LoadLocalState();
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }

            if (webRequest.responseCode == 0)
                MessageBox.instance.Show(webRequest.error);
            else if (webRequest.responseCode == 400)
                MessageBox.instance.Show("400 Bad Request: " + webRequest.error);
            else if (webRequest.responseCode == 401)
                MessageBox.instance.Show("401 Bad Request: " + webRequest.error);
            else if (webRequest.responseCode == 404)
                MessageBox.instance.Show("404 Not Found: " + webRequest.error);
            else if (webRequest.responseCode == 500)
                MessageBox.instance.Show("500 Internal Server Error: " + webRequest.error);

            if (webRequest.responseCode == 200)
                if (MessageBox.instance.isShowing)
                    MessageBox.instance.Hide();

            Debug.Log("HTTP RESPONSE CODE: " + webRequest.responseCode);
        }
    }





}