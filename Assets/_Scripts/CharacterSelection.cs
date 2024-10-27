using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using System;
using System.Text;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;
    
    int index;
    GameObject[] characterList;
    public GameObject[] headerList;
    public Text characterNameText;
    public GameObject girl_idle, boy_idle;
    public Button[] buttons;

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
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        characterNameText.text = Databank.instance.Name;

        if (Databank.instance.Character > 0)
            index = Databank.instance.Character - 1;
        else
            index = Databank.instance.Character;
        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;

        }

        foreach (GameObject item in characterList)
        {
            item.SetActive(false);
        }
        Debug.Log("index " + index);
        if (characterList[index])
            characterList[index].SetActive(true);

        foreach (GameObject item in headerList)
        {
            item.SetActive(false);
        }

        if (headerList[index])
            headerList[index].SetActive(true);
        print("CurrentIndex " + index);
    }

    public void Toggle(bool t)
    {
       
        if (t)
        {
            headerList[index].SetActive(false);
            characterList[index].SetActive(false);
            index--;
            if (index < 0)
                index = characterList.Length - 1;

            headerList[index].SetActive(true);
            characterList[index].SetActive(true);

        }
        else if (!t)
        {
            headerList[index].SetActive(false);
            characterList[index].SetActive(false);
            index++;
            if (index == characterList.Length)
                index = 0;
            headerList[index].SetActive(true);
            characterList[index].SetActive(true);

        }

        print("Index " + index);
    }

    public void Play()
    {
        StartCoroutine(InitiateCharacterSelection(index + 1));
        print("SelectedIndex " + (index + 1) );
    }


    IEnumerator InitiateCharacterSelection(int NewCharacterID)
    {

        var uwr = new UnityWebRequest(Databank.URL_Character, UnityWebRequest.kHttpVerbPOST);

        SelectedCharacter userdata = new SelectedCharacter();
        userdata.characterId = NewCharacterID;
        string json = JsonUtility.ToJson(userdata);
        Debug.Log(json);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("InitiateCharacterSelection-Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("InitiateCharacterSelection-Received: " + uwr.downloadHandler.text);

            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);

            if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
            {
                Databank.instance.Character = NewCharacterID;
                Databank.instance.AddScore(0, 1);
                SceneManager.LoadScene(Databank.SCENE_MAINMENU);
            }
        }


    }


}

public class SelectedCharacter
{
    public int characterId;
}


