using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalAudioSetting : MonoBehaviour
{

    public List<Image> Soundlevel = new List<Image>();
    public List<Image> Musiclevel = new List<Image>();
    public int sfxvol,musicvol;
    float currentsfxvolume,currentmusicvolume;
    // Start is called before the first frame update
    void Start()
    {

        //if(PlayerPrefs.HasKey("SFXVolume") == false)
        //{
        //    PlayerPrefs.SetFloat("SFXVolume", 0.6f);
        //}

        //if (PlayerPrefs.HasKey("MusicVolume") == false)
        //{
        //    PlayerPrefs.SetFloat("MusicVolume", 0.6f);
        //}
        //currentsfxvolume = PlayerPrefs.GetFloat("SFXVolume");
        //currentmusicvolume = PlayerPrefs.GetFloat("MusicVolume");

        currentsfxvolume = 0.6f;
        currentmusicvolume = 0.6f;

        if (currentsfxvolume == 1 )
        {
            sfxvol = 5;
        }
        else if (currentsfxvolume == 0.8f)
        {
            sfxvol = 4;
        }
        else if (currentsfxvolume == 0.6f)
        {
            sfxvol = 3;
        }
        else if (currentsfxvolume == 0.4f)
        {
            sfxvol = 2;
        }
        else if (currentsfxvolume == 0.2f)
        {
            sfxvol = 1;
        }
        else
        {
            sfxvol = 0;
        }


        if (currentmusicvolume == 1)
        {
            musicvol = 5;
        }
        else if (currentmusicvolume == 0.8f)
        {
            musicvol = 4;
        }
        else if (currentmusicvolume == 0.6f)
        {
            musicvol = 3;
        }
        else if (currentmusicvolume == 0.4f)
        {
            musicvol = 2;
        }
        else if (currentmusicvolume == 0.2f)
        {
            musicvol = 1;
        }
        else
        {
            musicvol = 0;
        }


        for (int i = 0; i < Soundlevel.Count; i++)
        {
            Soundlevel[i].enabled = false;
        }
        for (int i = 0; i < sfxvol; i++)
        {
            Soundlevel[i].enabled = true;
        }


        for (int i = 0; i < Musiclevel.Count; i++)
        {
            Musiclevel[i].enabled = false;
        }
        for (int i = 0; i < musicvol; i++)
        {
            Musiclevel[i].enabled = true;
        }
    }
   

    public void ChangeSFX(bool up)
    {
       
        if (up) 
        {
            FindObjectOfType<SoundManager>().SettingControl(); 
            if (currentsfxvolume >=0   && sfxvol > 0)
            {
                currentsfxvolume -= 0.2f;
                sfxvol = sfxvol - 1;
                //PlayerPrefs.SetFloat("SFXVolume", currentsfxvolume);
                Soundlevel[sfxvol].enabled = false;
                FindObjectOfType<SoundManager>().SFXVolume(currentsfxvolume);
            }
        }
        if (!up)
        {
            FindObjectOfType<SoundManager>().SettingControl();
            if (currentsfxvolume < 1f && sfxvol < 6)
            {
                currentsfxvolume += 0.2f;
                //PlayerPrefs.SetFloat("SFXVolume", currentsfxvolume);
                Soundlevel[sfxvol].enabled = true;
                sfxvol = sfxvol + 1;
                FindObjectOfType<SoundManager>().SFXVolume(currentsfxvolume);
            }
        }
    }

    public void ChangeMusic(bool down)
    {

        if (down)
        {
            FindObjectOfType<SoundManager>().SettingControl();
            Debug.Log(currentmusicvolume);
            Debug.Log(musicvol);

            if (currentmusicvolume >= 0 && musicvol > 0)
            {
                Debug.Log(musicvol);
                currentmusicvolume -= 0.2f;
                musicvol = musicvol - 1;
                //PlayerPrefs.SetFloat("MusicVolume", currentmusicvolume);
                Musiclevel[musicvol].enabled = false;
                FindObjectOfType<SoundManager>().MusicVolume(currentmusicvolume);
            }
        }
        if (!down)
        {
            FindObjectOfType<SoundManager>().SettingControl();
            if (currentmusicvolume < 1f && musicvol < 6)
            {
                currentmusicvolume += 0.2f;
                //PlayerPrefs.SetFloat("MusicVolume", currentmusicvolume);
                Musiclevel[musicvol].enabled = true;
                musicvol = musicvol + 1;
                FindObjectOfType<SoundManager>().MusicVolume(currentmusicvolume);
            }
        }
    }



    public void QuitGame()
    {
        FindObjectOfType<SoundManager>().Play();
        FindObjectOfType<SoundManager>().PlayMusicdefault();
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Databank.SCENE_MAINMENU);
        //Application.Quit();
    }

    public void Logout()
    {
        FindObjectOfType<SoundManager>().PlayMusicdefault();
        FindObjectOfType<SoundManager>().Play();
        SignUpManager.isSignup = false;
        Databank.instance.SignOut();
    }

}
