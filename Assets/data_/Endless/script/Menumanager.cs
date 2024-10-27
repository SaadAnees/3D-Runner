using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menumanager : MonoBehaviour
{

    public GameObject free_active, free_injactive, mission_active,mission_inactive;

    string scene_name;

    private void Start()
    {
        scene_name = "freemode";
        Databank.instance.mode_type = "freemode";

        //PlayerPrefs.SetString("MODE", "free");
    }

    public void GotoPlay1()
    {
        SceneManager.LoadScene(scene_name); 
    }

    public void FreePlay()
    {
       // PlayerPrefs.SetString("MODE","free");

        scene_name = "freemode";
        Databank.instance.mode_type = "freemode";
        free_injactive.SetActive(false);
        free_active.SetActive(true);
        mission_active.SetActive(false);
        mission_inactive.SetActive(true);

        print("scene = "+scene_name);

    }

    public void MissionPlay()
    {
        //PlayerPrefs.SetString("MODE", "mission");
        Databank.instance.mode_type = "mission";
        Databank.instance.localLevelPlaying = Databank.instance.ChallengeId;
        scene_name = Databank.SCENE_SEQUENCE;
        free_injactive.SetActive(true);
        free_active.SetActive(false);
        mission_active.SetActive(true);
        mission_inactive.SetActive(false);

        print("scene = " + scene_name);
    }

    public void Back()
    {
        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
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

}
