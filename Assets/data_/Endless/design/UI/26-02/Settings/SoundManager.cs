using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;


    public AudioClip Music, Selectbtn, settingPlus, powerupClip;

    public AudioClip Scifi, Meyrin;

    // Singleton instance.
    public static SoundManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //if (PlayerPrefs.HasKey("SFXVolume") == false)
        //{
        //    PlayerPrefs.SetFloat("SFXVolume", 0.6f);
        //}

        //if (PlayerPrefs.HasKey("MusicVolume") == false)
        //{
        //    PlayerPrefs.SetFloat("MusicVolume", 0.6f);
        //}

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
        PlayMusic(Music);
        //MusicVolume(0.5f);
        MusicVolume(0.6f);
        SFXVolume(0.6f);
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    public void PlayPowerUpClip()
    {
        EffectsSource.PlayOneShot(powerupClip);
    }

    // Play a single clip through the sound effects source.
    public void Play()
    {
        //EffectsSource.clip = Selectbtn;
        EffectsSource.PlayOneShot(Selectbtn);
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
    public void PlayMusicdefault()
    {
        MusicSource.clip = Music;
        MusicSource.Play();
    }

    public void PlayMeyrin1()
    {
        MusicSource.clip = Meyrin;
        MusicSource.Play();
    }
    public void PlaySharjah()
    {
        MusicSource.clip = Scifi;
        MusicSource.Play();
    }


    public void PlayMusicdefault1(int i)
    {
        if(i == 1)
        {
            MusicSource.clip = Scifi;
        }
        else
        {
            MusicSource.clip = Meyrin;
        }
        MusicSource.Play();
    }
    public void MusicVolume(float i)
    {
        MusicSource.volume = i;
        //if (SceneManager.GetActiveScene().name == Databank.SCENE_SEQUENCE)
        //    FindObjectOfType<Storyboard>().SetVideoVolume();
    }
    public void SFXVolume(float i)
    {
        EffectsSource.volume = i;
        //PlayerPrefs.SetFloat("SFXVolume", i);
    }

    public void SettingControl()
    {
        //EffectsSource.clip = Selectbtn;
        EffectsSource.PlayOneShot(settingPlus);
    }


}