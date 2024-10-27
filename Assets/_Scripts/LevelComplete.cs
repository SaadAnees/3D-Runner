using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelComplete : MonoBehaviour
{
    public static LevelComplete Instance;

    public GameObject GameOverPopup;
    public GameObject Buttons;
    public Button playBtn;
    public Text titleText;

    public Text timerText;
    public Text EtherText;
    public GameObject[] starsArr;

    void OnEnable()
    {
        //print("Replayed: " + Databank.instance.Replayed);
        if(!Databank.instance.Replayed)
            Buttons.SetActive(false);
        Databank.instance.Replayed = false;
        print("Curr_scoreCounter " + PlayerController.instance.scoreCounter);
        timerText.text = PlayerController.instance.timer_txt.text;
        EtherText.text = PlayerController.instance.scoreCounter.ToString();
        print("Required_Points " + Databank.instance.Required_Points);

        if (PlayerController.instance.scoreCounter > Databank.instance.Required_Points && PlayerController.instance.scoreCounter < 550)
        {
            //print("1");
            starsArr[0].SetActive(true);
         

        }
        else if (PlayerController.instance.scoreCounter > 550 && PlayerController.instance.scoreCounter < 750)
        {
            //print("2");
            starsArr[0].SetActive(true);
            starsArr[1].SetActive(true);
        }

        else if (PlayerController.instance.scoreCounter >= 750)
        {
            //print("3");
            starsArr[0].SetActive(true);
            starsArr[1].SetActive(true);
            starsArr[2].SetActive(true);
        }

        else if (PlayerController.instance.scoreCounter < Databank.instance.Required_Points)
        {
            titleText.text = "GAME OVER!";
            playBtn.gameObject.SetActive(false);
        }

        if (Databank.instance.Replayed || Databank.instance.localLevelPlaying < Databank.instance.ChallengeId)
        {
            //print("BUTTONS");
            FindObjectOfType<LevelComplete>().EnableButtons();
        }

        if (Databank.instance.mode_type == "freemode")
        {
            print("mode_type: " + Databank.instance.mode_type);
            EnableButtons();
            
        }

    }

  
    public void EnableButtons()
    {
        //print("EnableButtons");
        Buttons.SetActive(true);
    }


    public void SelectLevel(int num)
    {
        switch (num)
        {
            case 1:
                SceneManager.LoadScene(Databank.SCENE_MEYRIN1);
                break;
            case 2:
                SceneManager.LoadScene(Databank.SCENE_MEYRIN2);
                break;
            case 3:
                SceneManager.LoadScene(Databank.SCENE_MEYRIN3);
                break;
            default:
                break;
        }

    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        //Databank.instance.Replayed = true;
        GameOverPopup.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Map()
    {
        Time.timeScale = 1;
        GameOverPopup.SetActive(false);
        SceneManager.LoadScene(Databank.SCENE_MAP);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        GameOverPopup.SetActive(false);
        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
    }

    public void Back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
    }

    public void Close()
    {
        Time.timeScale = 1;
        GameOverPopup.SetActive(false);
    }

    public void Storyboard()
    {
        if (Databank.instance.localLevelPlaying < Databank.instance.ChallengeId)
        {
            Databank.instance.localLevelPlaying++;
        }
        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);

    }

    void OnDisable()
    {
        
        starsArr[0].SetActive(false);
        starsArr[1].SetActive(false);
        starsArr[2].SetActive(false);
    }

    public void GoToMap()
    {
        if (Databank.instance.mode_type == "freemode")
        {
            SceneManager.LoadScene("freemode");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");

        }
    }

    public void Proceed()
    {
        if(Databank.instance.localLevelPlaying < Databank.instance.ChallengeId)
        {
            Databank.instance.localLevelPlaying++;
        }

        if (Databank.instance.mode_type == "freemode")
        {
            SceneManager.LoadScene("freemode");
        }

        else
        {
            SceneManager.LoadScene("Sequence");

        }
    }

}
