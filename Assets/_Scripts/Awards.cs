using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using GameData;

public class Awards : MonoBehaviour
{
    /// <summary>
    /// 
    /// Stem Mission Level 1 - 5

   
   // Medic Mission Level 1 - 8


//Sustainability Mission Level 1 - 11



//Cognitive Mission Level 1 - 14 - 


//Meyrin Phase 1 Conqueror - 14



//3 Star Champion - 

//If you make 3 stars in the 3 checkpoints consecutively

//Meyrin World Conqueror

    /// </summary>
    public Text userName;
    public GameObject Prefab;
    public Transform Content;
    public GameObject boy, girl;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(InitiateAwards());
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
        userName.text = Databank.instance.Name;

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID

        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("hall_of_fame");
        //}

#endif
    }

    IEnumerator InitiateAwards()
    {
        //yield return new WaitForSeconds(1.5f);
        //var uwr = new UnityWebRequest(URL_Star, UnityWebRequest.kHttpVerbGET);
        UnityWebRequest uwr = UnityWebRequest.Get(Databank.URL_Awards);
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("InitiateAwards Error: " + uwr.error);
           
        }
        else
        {
            Debug.Log("InitiateAwards Received: " + uwr.downloadHandler.text);
            GetPlayerInfo(uwr.downloadHandler.text);
        }
    }

    void GetPlayerInfo(string data)
    {
        //AwardsAPI.Awards award = JsonConvert.DeserializeObject<AwardsAPI.Awards>(data);

        //foreach (var item in award.details)
        //{
        //    print("playerState " + item.awardName);
        //    GameObject go = Instantiate(Prefab, Content);
        //    go.transform.GetChild(0).name = item.awardId + " " + item.awardName;
        //    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("AwardsIcons/"+item.awardName);
        //}
       


    }

    public void Back()
    {
        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
    }

}
