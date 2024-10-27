using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MapSelection : MonoBehaviour
{
    public GameObject scrollView;

    public Font myfont;

    public Button[] btns, levels;

    public Button[] missions, minigames;

    public AudioClip mapbtn;

    public void GotoCheckpoint(int i)
    {
        SceneManager.LoadScene("curve_meyrin");
    }

    private void Start()
    {


        PlayerPrefs.SetString("MODE", "mission");

        if (PlayerPrefs.GetString("LEVEL_level") == "1")
        {
            scrollView.GetComponent<RectTransform>().localPosition = new Vector3(scrollView.GetComponent<RectTransform>().localPosition.x, 2458.787f, 0);
        }
        if (PlayerPrefs.GetString("LEVEL_level") == "2")
        {
            scrollView.GetComponent<RectTransform>().localPosition = new Vector3(scrollView.GetComponent<RectTransform>().localPosition.x, 398.8903f, 0);
        }
        if (PlayerPrefs.GetString("LEVEL_level") == "3")
        {
            scrollView.GetComponent<RectTransform>().localPosition = new Vector3(scrollView.GetComponent<RectTransform>().localPosition.x, -1815.787f, 0);
        }

        for (int i = 0; i < missions.Length; i++)
        {
            missions[i].transform.GetChild(0).GetComponent<Text>().font = myfont;
            missions[i].transform.GetChild(0).GetComponent<Text>().fontSize = 85;
            missions[i].transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            missions[i].interactable = false;
        }

        for(int i = 0; i < minigames.Length; i++)
        {
            minigames[i].transform.GetChild(0).GetComponent<Text>().text = " ";
            minigames[i].interactable = false;
        }

        SetCheckPoints(PlayerPrefs.GetString("LEVEL_level"), PlayerPrefs.GetString("LEVEL_stage"), PlayerPrefs.GetString("LEVEL_checkpoint"));

    }

    int checkInt;

    void SetCheckPoints(string level, string stage, string checkpoints)
    {
        if(level == "1")
        {
            if(stage == "2")
            {
                checkInt = int.Parse(checkpoints);
            }
            if (stage == "3")
            {
                checkInt = int.Parse(checkpoints) + 4;
            }
            if (stage == "4")
            {
                checkInt = int.Parse(checkpoints) + 8;
            }
            if (stage == "5")
            {
                checkInt = int.Parse(checkpoints) + 12;
            }
            for (int j = 0; j < checkInt; j++)
            {
                levels[j].interactable = true;
            }
        }
        if (level == "2")
        {
            if (stage == "2")
            {
                checkInt = int.Parse(checkpoints) + 16;
            }
            if (stage == "3")
            {
                checkInt = int.Parse(checkpoints) + 20;
            }
            if (stage == "4")
            {
                checkInt = int.Parse(checkpoints) + 24;
            }
            if (stage == "5")
            {
                checkInt = int.Parse(checkpoints) + 28;
            }
            for (int j = 0; j < checkInt; j++)
            {
                levels[j].interactable = true;
            }
        }
        if (level == "3")
        {
            if (stage == "2")
            {
                checkInt = int.Parse(checkpoints) + 32;
            }
            if (stage == "3")
            {
                checkInt = int.Parse(checkpoints) + 36;
            }
            if (stage == "4")
            {
                checkInt = int.Parse(checkpoints) + 40;
            }
            if (stage == "5")
            {
                checkInt = int.Parse(checkpoints) + 44;
            }
            for (int j = 0; j < checkInt; j++)
            {
                levels[j].interactable = true;
            }
        }
    }

    public void Meyrin1()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        SceneManager.LoadScene("curve_meyrin");
    }

    public void Meyrin2()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        SceneManager.LoadScene("curve_meyrin2");
    }

    public void Meyrin3()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        SceneManager.LoadScene("curve_meyrin3");
    }

    public void BlockProgramming()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        SceneManager.LoadScene("blockprogramming");
    }

    public void ooo()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        SceneManager.LoadScene("MobileScene");
    }

    public void SortingGame(int value)
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        //Difficulty_selection.instance.Getgamedata();

        if(value == 1)
        {
            PlayerPrefs.SetString("Sort", PlayerPrefs.GetString("Easy_sort"));
            PlayerPrefs.SetString("Time", PlayerPrefs.GetString("Easy_time"));
            PlayerPrefs.SetString("Speed", PlayerPrefs.GetString("Easy_speed"));
        }
        if(value == 2)
        {
            PlayerPrefs.SetString("Sort", PlayerPrefs.GetString("Medium_sort"));
            PlayerPrefs.SetString("Time", PlayerPrefs.GetString("Medium_time"));
            PlayerPrefs.SetString("Speed", PlayerPrefs.GetString("Medium_speed"));
        }
        if(value == 3)
        {
            PlayerPrefs.SetString("Sort", PlayerPrefs.GetString("Hard_sort"));
            PlayerPrefs.SetString("Time", PlayerPrefs.GetString("Hard_time"));
            PlayerPrefs.SetString("Speed", PlayerPrefs.GetString("Hard_speed"));
        }

        Invoke("ToSorting",1);


    }

    void ToSorting()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        SceneManager.LoadScene("gameplay");
    }

    public void ToMatch()
    {
        SceneManager.LoadScene("tilematching");
    }

    public void IntroMission()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mapbtn);
        SceneManager.LoadScene("curve_scifi");
    }

    public void Back()
    {
        SceneManager.LoadScene("hall_of_fame");
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
