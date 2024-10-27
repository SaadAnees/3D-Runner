using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject settingPopup;

    //public GameObject[] sfx_bar, music_bar;
    int sfxCounter, musicCounter;

    private void Update()
    {
        if(sfxCounter == -1)
        {
            sfxCounter = 0;
        }
        if (sfxCounter > 5)
        {
            sfxCounter = 5;
        }
    }

    public void Show()
    {
        settingPopup.SetActive(true);
        FindObjectOfType<SoundManager>().Play();
    }

    public void Close()
    {
        settingPopup.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void Logout()
    {
        SignUpManager.isSignup = false;
        Databank.instance.SignOut();
    }

    public void SFXBarPlus()
    {
        sfxCounter++;

        print("PLUS = "+sfxCounter);

    }

    public void SFXBarMinus()
    {
        sfxCounter--;

        print("MINUS = "+sfxCounter);

    }

    public void MusicBarPlus()
    {

    }

    public void MusicBarMinus()
    {

    }


    public void GoToScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

}
