using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using SimpleJSON;
using UnityEngine.SceneManagement;
using GameData;
using TMPro;

public class Storyboard : MonoBehaviour
{
    public static Storyboard instance;

    public Image loadingBar;
    public GameObject idleScreen;
    public Image BackgroundImage;
    public Image CharacterImage;
    public Text BodyTxt;
    public Text HeaderTxt;
    public Button NextBtn;
    public Button PreviousBtn;
    public Button SkipBtn;
    public GameObject RTLText;
    public GameObject StoryPanel;
    public VideoPlayer videoPlayer;
    //Dictionary<string, List<Story.Detail>> lisoftStories = new Dictionary<string, List<Story.Detail>>();
    List<Story.Detail> lisoftStories = new List<Story.Detail>();
    GameData.Story.Storyboard storyBoard;
    int index;
    void Awake()
    {
        Storyboarding();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (index < 1)
        {
            PreviousBtn.interactable = false;
            
        }
        //StartCoroutine(PlayVideo());
        videoPlayer.loopPointReached += EndReached;
    }
   
    void Storyboarding()
    {
        StartCoroutine(InitiateStoryboard());
        Databank.instance.debugText = "Loading Storyboard..";
    }

    IEnumerator InitiateStoryboard()
    {
        yield return new WaitForSeconds(1.5f);
        print("Storyboarding " + Databank.instance.localLevelPlaying + " == " + Databank.instance.ChallengeId);
     
        var uwr = new UnityWebRequest(Databank.URL_Storyboard + Databank.instance.localLevelPlaying, UnityWebRequest.kHttpVerbGET);
        print("InitiateStoryboard " + uwr.url);
        Databank.instance.debugText = uwr.url;
        byte[] bytes = Encoding.UTF8.GetBytes(Databank.Access_Token);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);
        uh.contentType = "application/json";
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);
        //print(Mathf.Abs( uwr.downloadProgress));

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("InitiateStoryboard Error While Sending: " + uwr.error);
            Databank.instance.debugText = uwr.error;
        }
        else
        {
            Debug.Log("InitiateStoryboard Received: " + uwr.downloadHandler.text);
            Databank.instance.debugText = uwr.downloadHandler.text;
            loadingBar.fillAmount = 1f;
           
            idleScreen.SetActive(false);
            loadingBar.fillAmount += Time.deltaTime * uwr.downloadProgress;

            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);

            if (jsonNode["status"])
            {
                //Debug.Log(jsonNode["status"]);
                GetStoryInfo(uwr.downloadHandler.text);
            }

        }

    }

    void Update()
    {
        loadingBar.fillAmount += Time.deltaTime * Random.Range(0.0f, 0.6f);
    }

    void GetStoryInfo(string data)
    {
        //storyBoard = JsonConvert.DeserializeObject<GameData.Story.Storyboard>(data);

        // For first index
        ToggleStoryContent();

    }

    public void Toggle(bool dir)
    {
        print("Toggle index " + index);
        // Toggling between index
        if (dir)
        {
            if (index < storyBoard.details.Count - 1)
            {
                index++;
                PreviousBtn.interactable = true;
            }
            else
            {
                NextBtn.interactable = false;
                if (Databank.instance.Character == 0)
                {
                    //LocalGameState localGameState = new LocalGameState();

                    //localGameState.firstStoryDone = true;
                    //string localGS = JsonUtility.ToJson(localGameState);
                    //Databank.instance.firstStoryDone = true;

                    //PlayerPrefs.SetInt("firstStoryDone", 1); // 1 means true and 0 means false. this is to make sure Begining story plays
                    //Databank.instance.SaveLocalState();
                    SceneManager.LoadScene(Databank.SCENE_CHARACTERSELECTION);

                }
                else if (Databank.instance.ChallengeId == 2)
                    SceneManager.LoadScene(Databank.SCENE_SCIFI);
                else
                    Play();
            }
        }
        else
        {
            if (index > 0)
            {

                index--;
                if (index < 1)
                {
                    PreviousBtn.interactable = false;
                    
                }

            }
            else if (index < 1)
            {
                PreviousBtn.interactable = false;
                return;
            }
        }

        ToggleStoryContent();

        // if (storyBoard.details[index].character_image == "Alice")
        //     CharacterImage.sprite = Resources.Load<Sprite>("Storyboard/Characters/" + storyBoard.details[index].character_image);

        // else if (storyBoard.details[index].character_image == "Atlas")
        //     CharacterImage.sprite = Resources.Load<Sprite>("Storyboard/Characters/" + storyBoard.details[index].character_image);

        //else if (storyBoard.details[index].character_image != "Atlas" || storyBoard.details[index].character_image != "Alice")
        //     CharacterImage.sprite = Resources.Load<Sprite>("Storyboard/Characters/" + RandomString());



    }

    void ToggleStoryContent()
    {
        // IF video available
        if (storyBoard.details[index].video != string.Empty)
        {
            //
            print("Video available");
            StartCoroutine(PlayVideo(storyBoard.details[index].video));
            StoryPanel.SetActive(false);
        }
        else
        {
            //
            videoPlayer.Stop();
            if(!FindObjectOfType<SoundManager>().MusicSource.isPlaying)
                FindObjectOfType<SoundManager>().PlayMusic(FindObjectOfType<SoundManager>().Music);
            StoryPanel.SetActive(true);
            print("Story available");
        }

        //print(RandomCharacter() + " " + Databank.instance.Character);
        if (storyBoard.details[index].header == Databank.instance.Name)
        {
            print("Character Image: " + storyBoard.details[index].character_image + " = " + "Background: " + storyBoard.details[index].background_image);
            CharacterImage.sprite = Resources.Load<Sprite>("Storyboard/Characters/" + CurrentCharacter());
        }
        else if (storyBoard.details[index].character_image == "Atlas" ||
   storyBoard.details[index].character_image == "Alice" ||
   storyBoard.details[index].character_image == "Bio-Medical Scientist Aegis" ||
   storyBoard.details[index].character_image == "Cognition Specialist Shine" ||
   storyBoard.details[index].character_image == "Sustainability Engineer Totem" ||
   storyBoard.details[index].character_image == "Stem Scientist Moe")
            CharacterImage.sprite = Resources.Load<Sprite>("Storyboard/Characters/" + storyBoard.details[index].character_image);

        else //if (storyBoard.details[index].character_image != string.Empty)
        {

            string cname = RandomCharacters();
            print("ELSE IF " + cname);
            CharacterImage.sprite = Resources.Load<Sprite>("Storyboard/Characters/" + cname);
        }


        if (storyBoard.details[index].background_image == "Bio-Medical lab" ||
            storyBoard.details[index].background_image == "Cognitive Lab" ||
            storyBoard.details[index].background_image == "Meyrin" ||
            storyBoard.details[index].background_image == "Spacestation" ||
            storyBoard.details[index].background_image == "Stem Lab" ||
            storyBoard.details[index].background_image == "Sustainability Engineer lab" ||
            storyBoard.details[index].background_image == "Training Lab" ||
            storyBoard.details[index].background_image == "Sharjah")
            BackgroundImage.sprite = Resources.Load<Sprite>("Storyboard/Backgrounds/" + storyBoard.details[index].background_image);
        else
            BackgroundImage.sprite = Resources.Load<Sprite>("Storyboard/Backgrounds/" + RandomBackground());


        if(Databank.instance.Language == "ar")
            RTLText.GetComponent<TextMeshProUGUI>().text = storyBoard.details[index].body;
        else
        {
            BodyTxt.text = storyBoard.details[index].body;
            HeaderTxt.text = storyBoard.details[index].header;
        }

        //BodyTxt.gameObject.GetComponent<SetArabicTextExample>().FitText();

        //if (LocalizationManager.CurrentLanguage == "Arabic")
        //    BodyTxt.alignment = TextAnchor.MiddleRight;
    }

    IEnumerator PlayVideo(string videoName)
    {
        print("video: " + videoName);
        if (videoPlayer.isPlaying)
            videoPlayer.Stop();
        if (!videoPlayer.gameObject.activeSelf)
            videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = Resources.Load<VideoClip>("Storyboard/Videos/" + videoName);
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        
        //videoPlayer.GetComponent<RawImage>().texture = Resources.Load<Texture>("Storyboard/VideoTexture");
        // if (videoPlayer.GetComponent<RawImage>().texture != null)

        //videoPlayer.GetComponent<RawImage>().texture = videoPlayer.texture;
        FindObjectOfType<SoundManager>().StopMusic();
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0,FindObjectOfType<SoundManager>().GetComponent<AudioSource>());
        videoPlayer.SetDirectAudioVolume(0, FindObjectOfType<SoundManager>().GetComponent<AudioSource>().volume);
        videoPlayer.Play();
        //print(videoPlayer.GetDirectAudioVolume(0));

    }
    
    public void SetVideoVolume()
    {
        videoPlayer.SetDirectAudioVolume(0, FindObjectOfType<SoundManager>().GetComponent<AudioSource>().volume);
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        print("finished video");
        if (videoPlayer.gameObject.activeSelf)
            videoPlayer.gameObject.SetActive(false);
        Toggle(true);
        //vp.Stop();
        //vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        //StoryPanel.SetActive(false);
    }

    public void SelectLevel(int num)
    {
        switch (num)
        {
            case 1:
                SceneManager.LoadScene(Databank.SCENE_MEYRIN1);
                break;
            case 2:
                SceneManager.LoadScene(Databank.SCENE_MEYRIN2);
                break;
            case 3:
                SceneManager.LoadScene(Databank.SCENE_MEYRIN3);
                break;
            default:
                break;
        }

    }

    string RandomCharacters()
    {
        string[] arr = new string[8];
        arr[0] = "Alice";
        arr[1] = "Atlas";
        arr[2] = "Alice";
        arr[3] = "Atlas";
        arr[4] = "Alice";
        arr[5] = "Atlas";
        arr[6] = "Alice";
        arr[7] = "Atlas";

        return arr[Random.Range(0, 7)];

    }

    string CurrentCharacter()
    {
        string[] arr = new string[8];
        arr[0] = "Alice";
        arr[1] = "Atlas";
        arr[2] = "Alice";
        arr[3] = "Atlas";
        arr[4] = "Alice";
        arr[5] = "Atlas";
        arr[6] = "Alice";
        arr[7] = "Atlas";
        string s;
        if (Databank.instance.Character == 1)
            s = "Alice";
        else if (Databank.instance.Character == 2)
            s = "Atlas";
        else
            s = arr[Random.Range(0, 7)];


        return s;

    }

    string RandomBackground()
    {
        string[] arr = new string[8];
        arr[0] = "Bio-Medical lab";
        arr[1] = "Cognitive Lab";
        arr[2] = "Meyrin";
        arr[3] = "Space Statation";
        arr[4] = "Stem Lab";
        arr[5] = "Sustainability Engineer lab";
        arr[6] = "Training Lab";
        arr[7] = "Bio-Medical lab";

        return arr[Random.Range(0, 7)];

    }

    public void Skip()
    {
        if (Databank.instance.Character == 0)
            SceneManager.LoadScene(Databank.SCENE_CHARACTERSELECTION);
        //else if (Databank.instance.localLevelPlaying == 2)
        //    SceneManager.LoadScene(Databank.SCENE_SCIFI);
        else
            Play();
    }

    public void Play()
    {
        if (Databank.instance.localLevelPlaying == 5)
        {
            SceneManager.LoadScene(Databank.SCENE_MINIGAME_BLOCKPROGRAMMING);
        }
        else if (Databank.instance.localLevelPlaying == 8)
        {
            SceneManager.LoadScene(Databank.SCENE_MINIGAME_ODDONEOUT);
        }
        else if (Databank.instance.localLevelPlaying == 11)
        {
            SceneManager.LoadScene(Databank.SCENE_MINIGAME_SORTING);
        }

        else if (Databank.instance.localLevelPlaying == 14)
        {
            SceneManager.LoadScene(Databank.SCENE_MINIGAME_TILEMATCHING);
        }
        else if (Databank.instance.localLevelPlaying == 2)
        {
            SceneManager.LoadScene(Databank.SCENE_SCIFI);
        }
        else if (Databank.instance.localLevelPlaying != 5 ||
            Databank.instance.localLevelPlaying != 8 ||
            Databank.instance.localLevelPlaying != 11 ||
            Databank.instance.localLevelPlaying != 14 ||
            Databank.instance.localLevelPlaying != 2)
        {

            //SelectLevel(int.Parse(Databank.instance.Level));
            SceneManager.LoadScene(Databank.SCENE_MEYRIN1);
        }
        else
        {
            //Fail safe
            MainMenu();
        }

    }

    public void Map()
    {
        SceneManager.LoadScene(Databank.SCENE_MAP);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
    }

    public void BackButton()
    {
        if (Databank.instance.Character == 0)
        {

            SceneManager.LoadScene(Databank.SCENE_CHARACTERSELECTION);

        }
        else
            MainMenu();
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(Databank.SCENE_LOGIN);
    }

}
