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

public class StarAPI : MonoBehaviour
{
    public List<int> Levelid = new List<int>();
    public List<int> Stageid = new List<int>();
    public List<int> Checkpointid = new List<int>();
    public List<int> Starid = new List<int>();

    public static StarAPI instance;

    string Url;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Url = Databank.Uri + "/api/user/star";

        GetStar();

    }
   

    public void GetStar()
    {
        PostData();
    }

    void PostData()
    {
        Hashtable headers = new Hashtable();

        //headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IlJqUkZRVFZGUkRJNE1ESTBPVUZCUkRSQ01UVkNPRVkyTUVaQ1JqazNNakEwUXpNNFFVSXlOZyJ9.eyJpc3MiOiJodHRwczovL2Rldi1uYXplci5hdXRoMC5jb20vIiwic3ViIjoiUkd6WW9CWlExYjE1SmJvVVNialEzMUo4Y3JST1FZZ3VAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vZGVtby9hcGkiLCJpYXQiOjE1NTE4NjUxNzAsImV4cCI6MTU1NDQ1NzE3MCwiYXpwIjoiUkd6WW9CWlExYjE1SmJvVVNialEzMUo4Y3JST1FZZ3UiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.muT5LSBIooBosZyeG4GVejAy3Dzgcb4mVDIryHKmYALZWFoqEtDLdWFhjhJcoYcoAVeRxHj7zOjLtpDAWsNxc_Cq6FDsBD5EwMEHtvzPa8nbUeq68RqKUgWp_zeFvxWixqVB3YiHWFyeniT8VLMYAzsX_ISn48neJKKdYbhI3Yf9rG3naeDef00wYx4ic6vPZbPCR2zmRvR52yfMIQ4eAU5VlW21ouDKN_c5nmUIE3DjHMrdEFtAmsfZwYM433yaA9xrGGvZwh8sXd364vCPQr317REuE1Hv6pm8ROQa9I_yQgOb6TvD56J9av2A9h_XljmA2EQhd6fYZiGhxouqEg");

        headers.Add("Authorization", "Bearer " + PlayerPrefs.GetString("ACCESS_TOKEN"));

        WWW www = new WWW(Url, null, headers);

        StartCoroutine("PostdataEnumerator", www);

    }

    string response;

    public string access_token;

    IEnumerator PostdataEnumerator(WWW www)
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

        if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
        {
            for (int i = 0; i < jsonNode["details"].Count; i++)
            {
                print("response 1 = " + jsonNode["details"][i]["level_id"].ToString());
                print("response 1 = " + jsonNode["details"][i]["stage_id"].ToString());
                print("response 1 = " + jsonNode["details"][i]["checkpoint_id"].ToString());
                print("response 1 = " + jsonNode["details"][i]["star"].ToString());

                Levelid.Add(int.Parse(jsonNode["details"][i]["level_id"].ToString().Replace('"', ' ')));
                Stageid.Add(int.Parse(jsonNode["details"][i]["stage_id"].ToString().Replace('"', ' ')));
                Checkpointid.Add(int.Parse(jsonNode["details"][i]["checkpoint_id"].ToString().Replace('"', ' ')));
                Starid.Add(int.Parse(jsonNode["details"][i]["star"].ToString().Replace('"', ' ')));
                GetStarnumers(int.Parse(PlayerPrefs.GetString("LEVEL_level")),int.Parse(PlayerPrefs.GetString("LEVEL_stage")),int.Parse(PlayerPrefs.GetString("LEVEL_checkpoint")));
            }
        }
        else
        {

        }
    }

    public GameObject[] stars;

    public void GetStarnumers(int lvlid,int stageid,int checkpoint)
    {
        for(int i= 0; i < Levelid.Count; i++)
        {
            if (Levelid[i] == lvlid)
            {
                PlayerPrefs.SetString("stars",Starid[i].ToString());
            }
        }

        Debug.LogError(PlayerPrefs.GetString("stars"));

        if(PlayerPrefs.GetString("stars") == "1")
        {
            stars[0].SetActive(false);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
        if (PlayerPrefs.GetString("stars") == "2")
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(true);
        }
        if (PlayerPrefs.GetString("stars") == "3")
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }

    }


}
