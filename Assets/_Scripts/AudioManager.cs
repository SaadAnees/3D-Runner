using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    AudioSource audioSource;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "login" ||
        SceneManager.GetActiveScene().name == "SignUp" ||
         SceneManager.GetActiveScene().name == "freemode" ||
          SceneManager.GetActiveScene().name == "hall_of_fame" || SceneManager.GetActiveScene().name == "character_selection")
            audioSource.volume = 1f;
        else
            audioSource.volume = 0f;


    }
}
