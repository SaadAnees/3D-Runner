using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    void Start()
    {
        // try{
        //     GameObject.Find("AudioMaster").GetComponent<AudioSource>().mute = true;
        // } catch(System.Exception)
        // {

        // }
    }
    /// <summary>
    /// Restarts the current level.
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1;


        // Execute the function after a delay
        Invoke("ExecuteRestartLevel", 1);
    }

    /// <summary>
    /// Executes the Load Level function
    /// </summary>
    void ExecuteRestartLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ToLevel(string name)
    {
        StartCoroutine(ToLevelCoroutine(name));
    }

    IEnumerator ToLevelCoroutine(string name)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(name);

    }
    public void Logout()
    {
        SignUpManager.isSignup = false;
        Databank.instance.SignOut();
    }
    void OnDestroy()
    {
           // GameObject.Find("AudioMaster").GetComponent<AudioSource>().mute = false;

    }
}
