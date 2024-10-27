using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour
{
    public Button[] TotalCheckpoints;

    public GameObject GamePlaySteps;

    // Start is called before the first frame update
    void Start()
    {
        Databank.instance.mode_type = "mission";
        
        print("Map: " + (Databank.instance.ChallengeId - 1));
        

        // This will enable the maps checkpoints according to ChallengeID
        for (int i = 0; i < Databank.instance.ChallengeId - 1; i++)
        {
            TotalCheckpoints[i].interactable = true;
        }
    }

    public void SelectLevel(int num)
    {
       
        print(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex());
        Databank.instance.localLevelPlaying = GamePlaySteps.transform.GetChild(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex()).GetSiblingIndex() + 2;
        print("localLevelPlaying " + Databank.instance.localLevelPlaying);
        //return;
        switch (num)
        {
            case 1:
                SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
                break;
            case 2:
                SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
                break;
            case 3:
                SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
                break;
            default:
                break;
        }
        
    }

    public void SelectMission(string missionName)
    {
        
        Databank.instance.localLevelPlaying = GamePlaySteps.transform.GetChild(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex()).GetSiblingIndex() + 2;
        print("localLevelPlaying " + Databank.instance.localLevelPlaying);
        switch (missionName)
        {
            case "Sorting":
                Sorting();
                break;
            case "BlockProgramming":
                BlockProgramming();
                break;
            case "OddOneOut":
                OddOneOut();
                break;
            case "TileMatching":
                TileMatching();
                break;
            default:
                Debug.Log("missionName " + missionName);
                break;
        }
    }

    void Sorting()
    {
        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
    }

    void BlockProgramming()
    {
        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
    }

    void OddOneOut()
    {
        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
    }

    void TileMatching()
    {
        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
    }
    
    public void MainManu()
    {
        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
    }

    public void MapScene()
    {
        SceneManager.LoadScene(Databank.SCENE_MAP);
    }

    public void SciFi()
    {
        Databank.instance.localLevelPlaying = GamePlaySteps.transform.GetChild(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex()).GetSiblingIndex() + 2;
        print(Databank.instance.localLevelPlaying);
        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
    }
}