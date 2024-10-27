using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreeMode : MonoBehaviour
{
    public GameObject Loader;
    public GameObject[] stages;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].GetComponent<Button>().interactable = false;
        }

        if (PlayerPrefs.GetString("LEVEL_level") == "1" && int.Parse(PlayerPrefs.GetString("LEVEL_checkpoint")) >= 1)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].GetComponent<Button>().interactable = false;
            }

            stages[1].GetComponent<Button>().interactable = true;

        }

        if (PlayerPrefs.GetString("LEVEL_level") == "1" && int.Parse(PlayerPrefs.GetString("LEVEL_stage")) == 3)
        {
            stages[4].GetComponent<Button>().interactable = true;

        }

        if (PlayerPrefs.GetString("LEVEL_level") == "1" && int.Parse(PlayerPrefs.GetString("LEVEL_stage")) == 4)
        {
            stages[4].GetComponent<Button>().interactable = true;
            stages[5].GetComponent<Button>().interactable = true;
        }

        if (PlayerPrefs.GetString("LEVEL_level") == "1" && int.Parse(PlayerPrefs.GetString("LEVEL_stage")) == 5)
        {
            stages[4].GetComponent<Button>().interactable = true;
            stages[5].GetComponent<Button>().interactable = true;
            stages[6].GetComponent<Button>().interactable = true;
        }

        if (PlayerPrefs.GetString("LEVEL_level") == "2" && int.Parse(PlayerPrefs.GetString("LEVEL_checkpoint")) >= 1)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].GetComponent<Button>().interactable = false;
            }

            stages[1].GetComponent<Button>().interactable = true;
            //stages[2].GetComponent<Button>().interactable = true;
            stages[4].GetComponent<Button>().interactable = true;
            stages[5].GetComponent<Button>().interactable = true;
            stages[6].GetComponent<Button>().interactable = true;
            stages[7].GetComponent<Button>().interactable = true;
        }

        if (PlayerPrefs.GetString("LEVEL_level") == "3" && int.Parse(PlayerPrefs.GetString("LEVEL_checkpoint")) >= 1)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].GetComponent<Button>().interactable = false;
            }

            stages[1].GetComponent<Button>().interactable = true;
            //stages[3].GetComponent<Button>().interactable = true;
            //stages[2].GetComponent<Button>().interactable = true;
            stages[4].GetComponent<Button>().interactable = true;
            stages[5].GetComponent<Button>().interactable = true;
            stages[6].GetComponent<Button>().interactable = true;
            stages[7].GetComponent<Button>().interactable = true;
        }

        stages[0].GetComponent<Button>().interactable = true;

        //testing
        stages[1].GetComponent<Button>().interactable = true;
        //stages[3].GetComponent<Button>().interactable = true;
        //stages[2].GetComponent<Button>().interactable = true;
        stages[4].GetComponent<Button>().interactable = true;
        stages[5].GetComponent<Button>().interactable = true;
        stages[6].GetComponent<Button>().interactable = true;
        stages[7].GetComponent<Button>().interactable = true;

    }

    public void Submit()
    {
        SceneManager.LoadScene("curve_scifi");
    }

    public void ToLevel(string name)
    {
        Loader.SetActive(true);
        StartCoroutine(LoadLevel(name));
    }


    IEnumerator LoadLevel(string s)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(s);
    }


    // Update is called once per frame
    void Update()
    {
        //print(Time.timeScale + " TimeScale");
        if(Time.timeScale == 0)
        Time.timeScale = 1f;
        if (GameObject.Find("GameData"))
            Destroy(GameObject.Find("*GameData*"));
    }

}
