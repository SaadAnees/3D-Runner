using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject boy, girl;
    public Button[] buttons;
    public Text userName;

    private void Awake()
    {
        buttons = FindObjectsOfType<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        SceneManager.sceneLoaded -= OnSceneLoaded;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
        userName.text = Databank.instance.Name;
    }
    //public Text thought;

    // Start is called before the first frame update
    void Start()
    {
       

        if (Databank.instance.Character == 1)
        {
            boy.SetActive(false);
            girl.SetActive(true);
        }
        if (Databank.instance.Character == 2)
        {
            boy.SetActive(true);
            girl.SetActive(false);
        }
    }

    public void Shop()
    {
        SceneManager.LoadScene("shop");
    }

    public void CharacterSelection()
    {
        SceneManager.LoadScene(Databank.SCENE_CHARACTERSELECTION);
    }

    public void MapSelection()
    {
        SceneManager.LoadScene("mission_selection");
    }

    public void Back()
    {
        SceneManager.LoadScene("mode_selection");
    }

    public void Awards()
    {

        SceneManager.LoadScene(Databank.SCENE_Awards);
    }

    public void Map()
    {

        SceneManager.LoadScene(Databank.SCENE_MAP);
    }

    public void FreeMode()
    {
        SceneManager.LoadScene(Databank.SCENE_FREEMODE);
    }

    public void Story()
    {
        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
    }

    public void ModeSelection()
    {
        SceneManager.LoadScene(Databank.SCENE_MODESELECTION);
    }

}
