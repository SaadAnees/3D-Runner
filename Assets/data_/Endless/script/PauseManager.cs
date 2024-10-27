using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePopup, settingsPopup;

    public void ShowPause()
    {
        Time.timeScale = 0;
        pausePopup.SetActive(true);
    }

    public void ClosePause()
    {
        Time.timeScale = 1;
        pausePopup.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePopup.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        pausePopup.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Setting()
    {
        pausePopup.SetActive(false);
        settingsPopup.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

}
