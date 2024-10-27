using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using SimpleJSON;
using GameData;
using UnityEngine.UI;
using System;

public class Databank : MonoBehaviour
{
    public static Databank instance;
    private static string uri = "";
    public static string Uri { get => uri; /*set => uri = value;*/ }

    private static string image_uri = "";
    public static string ImageUri { get => uri; /*set => uri = value;*/  }

    public const string LOCAL_ACCESS_TOKEN = "LOCAL_ACCESS_TOKEN";
    public const string SCENE_LOGIN = "Login";
    public const string SCENE_SIGNUP = "SignUp";
    public const string SCENE_MAINMENU = "MainMenu";
    public const string SCENE_SEQUENCE = "Sequence";
    public const string SCENE_MEYRIN1 = "Meyrin1";
    public const string SCENE_MEYRIN2 = "Meyrin2";
    public const string SCENE_MEYRIN3 = "Meyrin3";
    public const string SCENE_SCIFI = "curve_scifi";
    public const string SCENE_CHARACTERSELECTION = "CharacterSelection";
    public const string SCENE_MAP = "Map";
    public const string SCENE_FREEMODE = "Freemode";
    public const string SCENE_MODESELECTION = "mode_selection";
    public const string SCENE_Awards = "awards";

    public const string SCENE_MINIGAME_TILEMATCHING = "TileMatching";
    public const string SCENE_MINIGAME_LOADING = "Loading";
    public const string SCENE_MINIGAME_EASY = "EASY";
    public const string SCENE_MINIGAME_HARD = "MEDIUM";

    public const string SCENE_MINIGAME_BLOCKPROGRAMMING = "blockprogramming";
    public const string SCENE_MINIGAME_ODDONEOUT = "MobileScene";
    public const string SCENE_MINIGAME_SORTING = "Sorting";

    public static string URL_SignIn = Uri + "/api/user/signin";
    public static string URL_SignUp = Uri + "/api/user/signup";
    public static string URL_SignOut = Uri + "/api/user/signout";
    public static string URL_Quote = Uri + "/api/quote";
    public static string URL_Score = Uri + "/api/user/score";
    public static string URL_PasswordReset = Uri + "/api/user/passwordreset";
    public static string URL_Character = Uri + "/api/user/character";
    public static string URL_Storyboard = Uri + "/api/storyboard?chsid="; // param on storyboard.cs.... + Level + "&stgid="+ "2";
    public static string URL_BlockProgrammingLevelDetails = Uri + "/api/game/blockprogramming/level";
    public static string URL_OddOneOutLevelDetails = Uri + "/api/game/oddoneout?lid=";
    public static string URL_TileMatchingLevelAndTimerDetails = Uri + "/api/game/tileswapping/level";
    public static string URL_OddOneOutLevelAndTimerDetails = Uri + "/api/game/oddoneout/level";
    public static string URL_SortingLevelDetails = Uri + "/api/game/sorting/level";
    public static string URL_Sorting = Uri + "/api/game/sorting";
    public static string URL_TileMatching = Uri + "/api/game/tileswapping/";
    public static string URL_Profile = Uri + "/api/user/profile";
    public static string URL_Shop = Uri + "/api/powerups";
    public static string URL_PurchaseItem = Uri + "/api/user/purchase";
    public static string URL_ItemList = Uri + "/api/user/powerups";
    public static string URL_Language = Uri + "/api/user/language";
    public static string URL_Awards = Uri + "/api/user/award";
    public static string URL_EventScore = Uri + "/api/user/eventscore";
    public static string URL_Star = Uri + "/api/user/star";
    public static string URL_PowerUps = Uri + "/api/user/powerups";
    public static string URL_ComsumePowerUps = Uri + "/api/user/powerups"; 


    #region Addtional In-Game variables
    public int tileMatchingScore = 0;
    public string mode_type = "freemode";
    public string debugText = "Debug text";
   


    public int localLevelPlaying;
    public bool Replayed = false;

    #endregion

    #region GameData Variables

    public static string Access_Token;

    public string Name;
    public string Email;
    public string Gender;
    public int Age;
    public string Language;

    public string Message;

    public int Character;

    public int Level;
    public int Stage;
    public int CheckPoint;
    public int ChallengeId;

    public int Required_Points;
    public int Distance;
    public int Time_Limit;

    public int Total_Points;
    public int Life;
    public int Speed;

    public string Level_Instruction;
    public string Quotes;

    public string ID;
    public int Powerup;
    public string Quantity;

    public int StarsCount;
    #endregion

    public int _Lifes = 3;
    public bool reset = false;
    public int PowerupCount;

    void OnGUI()
    {
        //Rect initialBox = new Rect(100, 800, 600, 200);
        //GUI.Label(initialBox, debugText);
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (reset)
            ResetState();


        LoadLocalState();
        //if (SceneManager.GetActiveScene().name != SCENE_LOGIN)
        //    StartCoroutine(LoadScene(SCENE_LOGIN));
    }

    public void SignOut()
    {
        StartCoroutine(InitiateSignOut());
    }

    IEnumerator InitiateSignOut()
    {


        var uwr = new UnityWebRequest(URL_SignOut, UnityWebRequest.kHttpVerbPOST);
        byte[] bytes = Encoding.UTF8.GetBytes(Databank.Access_Token);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);
        uh.contentType = "application/json";
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            PlayerPrefs.DeleteKey(Databank.LOCAL_ACCESS_TOKEN);
            print(PlayerPrefs.GetString(Databank.LOCAL_ACCESS_TOKEN));
            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);

            if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
            {
                SceneManager.LoadScene(SCENE_LOGIN);
            }
        }

    }

    public void PowerUps()
    {
        StartCoroutine(InitiatePowerUps());
    }

    IEnumerator InitiatePowerUps()
    {


        var uwr = new UnityWebRequest(URL_ItemList, UnityWebRequest.kHttpVerbGET);
        byte[] bytes = Encoding.UTF8.GetBytes(Databank.Access_Token);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);
        uh.contentType = "application/json";
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("InitiatePowerUps Error: " + uwr.error);
        }
        else
        {
            Debug.Log("InitiatePowerUps Received: " + uwr.downloadHandler.text);

           
            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);

            if (jsonNode[2].Equals("success"))
            {
                print("YESY");
                //PowerUpAPI.Power powerItem = JsonConvert.DeserializeObject<PowerUpAPI.Power>(uwr.downloadHandler.text);
                //foreach (var item in powerItem.details)
                //{
                //    if (item.id == Powerup)
                //    {
                //        print(item.id + " == " + Powerup);
                //        PowerupCount = item.quantity;
                //        print(PowerupCount + " = " + item.quantity);
                //    }
                        

                //}
            }
        }

    }

    public IEnumerator SetLanguage(string language)
    {

        Language lang = new Language();
        lang.language = language;
        string json = JsonUtility.ToJson(lang);
        var uwr = new UnityWebRequest(URL_Language, UnityWebRequest.kHttpVerbPOST);
        Debug.Log("Requested Language: " + json);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);
        Debug.Log("Authorization " + "Bearer " + Databank.Access_Token);
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);

            print("///////////////");
            print(jsonNode[0]);
            print(jsonNode[1]);
            print(jsonNode[2]);
            print("///////////////");
            if (jsonNode[0].ToString().Replace('"', ' ').Trim().Equals("success"))
            {
                print("Changed to: " + jsonNode[1]);
                //Language = LocalizationManager.CurrentLanguage;
                Language = jsonNode[1];
                if (SceneManager.GetActiveScene().name == Databank.SCENE_MAINMENU)
                    FindObjectOfType<Quote>().GetQuote();
            }
        }

    }

    public void AddScore(int _score, int _gameID)
    {
        if (mode_type == "freemode" && SceneManager.GetActiveScene().name != Databank.SCENE_CHARACTERSELECTION)
        {
            Debug.Log("Freemode: cannot send score");
            return;
        }


        print(localLevelPlaying + " == " + ChallengeId);
        if (localLevelPlaying == ChallengeId ||
            localLevelPlaying == 0 ||
            localLevelPlaying == 1 &&
            !Replayed)
            StartCoroutine(InitiateAddScore(_score, _gameID));
    }

    IEnumerator InitiateAddScore(int _score, int _gameID)
    {
        //if (_score > Convert.ToInt32(Required_Points))
        //{
        var uwr = new UnityWebRequest(URL_Score, UnityWebRequest.kHttpVerbPOST);

        ScoreData data = new ScoreData();
        //uwr.timeout = 1;
        //data.score = PlayerController.instance.scoreCounter.ToString();
        data.score = _score;

        data.description = "endless";
        data.challengeId = ChallengeId; // because adding 1 when login.

        // Runner game - gameId = 1
        // for OOO - gameId = 2
        // for Sorting - gameId = 3
        // for Tile Matching - gameId = 4
        // for Block Programming - gameId = 5
        data.gameId = _gameID;
       
        string json = JsonUtility.ToJson(data);
        Debug.Log("Requested: " + json);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);
        //Debug.Log("Authorization " + "Bearer " + Databank.Access_Token);
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        try
        {
            Debug.Log("InitiateAddScore-Try");

            if (uwr.isHttpError)
            {
                Debug.Log("InitiateAddScore-isHttpError : " + uwr.error);
            }

            if (uwr.isNetworkError)
            {
                Debug.Log("InitiateAddScore-Error : " + uwr.error);
            }
            else
            {
                Debug.Log("InitiateAddScore-Received: " + uwr.downloadHandler.text);

                GetAddScoreInfo(uwr.downloadHandler.text);
            }
        }
        catch (Exception e)
        {
            Debug.Log("InitiateAddScore-catch");
            MessageBox.instance.Show("Cannot Add Score: " + e.Message);
        }

       
        //}

    }

    void GetAddScoreInfo(string data)
    {
        //Player.PlayerState playerState = JsonConvert.DeserializeObject<Player.PlayerState>(data);



        //if (playerState.status == "success")
        //{
        //    Level = playerState.levels.level;
        //    Stage = playerState.levels.stage;
        //    CheckPoint = playerState.levels.check_point;
        //    ChallengeId = Convert.ToInt32(playerState.levels.challengeId);

        //    Required_Points = playerState.mission.required_points;
        //    Distance = playerState.mission.distance;
        //    Time_Limit = playerState.mission.time_limit;

        //    Total_Points = playerState.game_play.total_points;
        //    Life = playerState.game_play.life;
        //    Speed = playerState.game_play.speed;

        //    Level_Instruction = playerState.game_message.level_instruction;
        //    Quotes = playerState.game_message.quotes;

        //    localLevelPlaying = ChallengeId;
        //    StarsCount = playerState.stars;

        //    if (SceneManager.GetActiveScene().name == Databank.SCENE_SCIFI || SceneManager.GetActiveScene().name == Databank.SCENE_MEYRIN1)
        //        FindObjectOfType<LevelComplete>().EnableButtons();
        //}

        //else
        //{
        //    if (SceneManager.GetActiveScene().name == Databank.SCENE_SCIFI || SceneManager.GetActiveScene().name == Databank.SCENE_MEYRIN1)
        //        FindObjectOfType<LevelComplete>().EnableButtons();
        //}


    }

    public void Stars()
    {
        StartCoroutine(InitiateGetStars());
    }
    public IEnumerator InitiateGetStars()
    {

        print(Databank.Access_Token);
        //var uwr = new UnityWebRequest(URL_Star, UnityWebRequest.kHttpVerbGET);
        UnityWebRequest uwr = UnityWebRequest.Get(URL_Star);
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("GetStars Received: " + uwr.downloadHandler.text);


            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);
            print(uwr.downloadHandler.text);

        }

    }

    void OnDestroy()
    {
        //print("OnDestroy");
        //LocalGameState localGameState = new LocalGameState();
        //localGameState.localAccesToken = Access_Token;

        //string localGS = JsonUtility.ToJson(localGameState);
        //SaveLocalState(localGS);
    }

    public virtual void SaveLocalState(string _localGS)
    {
        //PlayerPrefs.SetString("LocalGameState", _localGS);
        //print("PlayerPrefs.GetString; " + PlayerPrefs.GetString("LocalGameState"));
    }

    public virtual void SaveLocalState()
    {
        //LocalGameState localGameState = new LocalGameState();
        //localGameState.localAccesToken = Access_Token;
        //localGameState.firstStoryDone = firstStoryDone;
        //string localGS = JsonUtility.ToJson(localGameState);
        //print("SaveLocalState " + localGS);

        //SaveLocalState(localGS);



    }

    public void LoadLocalState()
    {
        if (MessageBox.instance.isShowing)
            return;

        if (PlayerPrefs.HasKey(Databank.LOCAL_ACCESS_TOKEN) ||
            PlayerPrefs.GetString(Databank.LOCAL_ACCESS_TOKEN) != String.Empty)
        {
            Debug.Log("LoadLocalState");
            print(PlayerPrefs.GetString(Databank.LOCAL_ACCESS_TOKEN));
            debugText = PlayerPrefs.GetString(Databank.LOCAL_ACCESS_TOKEN);
            Databank.Access_Token = PlayerPrefs.GetString(Databank.LOCAL_ACCESS_TOKEN);
            StartCoroutine(InitiateProfile());
           
        }
        else
        {
            Debug.Log("Login loading");
            debugText = "Login loading";
            //ShowMessageCanvas("Login loading");
            StartCoroutine(LoadScene(SCENE_LOGIN));
        }

        //StartCoroutine(SetLanguage());
    }

    IEnumerator InitiateProfile()
    {
        Debug.Log("InitiateProfile");
        //var uwr = new UnityWebRequest(URL_Star, UnityWebRequest.kHttpVerbGET);
        UnityWebRequest uwr = UnityWebRequest.Get(URL_Profile);
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        try
        {
            Debug.Log("InitiateProfile Try ");

            if (uwr.isHttpError)
            {
                Debug.Log("InitiateProfile isHttpError: " + uwr.error);
                debugText = uwr.error;
            }

            if (uwr.isNetworkError)
            {
                Debug.Log("InitiateProfile Error: " + uwr.error);
                debugText = uwr.error;
                MessageBox.instance.Show(uwr.error + ". Try again!");
            }
            else
            {
                Debug.Log("InitiateProfile Received: " + uwr.downloadHandler.text);
                debugText = uwr.downloadHandler.text;

                //JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);
                //print(jsonNode[0]);

                //Player.PlayerState playerState = JsonConvert.DeserializeObject<Player.PlayerState>(uwr.downloadHandler.text);

                ////print("playerState " + playerState.status);

                //if (playerState.status == "success")
                //{

                //    Level = playerState.levels.level;
                //    Stage = playerState.levels.stage;
                //    instance.CheckPoint = playerState.levels.check_point;

                //    ChallengeId = Convert.ToInt32(playerState.levels.challengeId);
                //    if (ChallengeId == 15)
                //    {
                //        // This If Conditioon is for temporary purpose to restrict user to phase 1 only
                //        //print("If =");
                //        ChallengeId = ChallengeId - 1;
                //    }


                //    Required_Points = playerState.mission.required_points;
                //    Distance = playerState.mission.distance;
                //    Time_Limit = playerState.mission.time_limit;

                //    Total_Points = playerState.game_play.total_points;
                //    Life = playerState.game_play.life;
                //    Speed = playerState.game_play.speed;
                //    Powerup = playerState.game_play.powerup;

                //    Level_Instruction = playerState.game_message.level_instruction;
                //    Quotes = playerState.game_message.quotes;

                //    Name = playerState.user_details.name;
                //    Email = playerState.user_details.email;
                //    Gender = playerState.user_details.gender;
                //    Age = playerState.user_details.age;
                //    Language = playerState.user_details.language;

                //    Character = Convert.ToInt32(playerState.character);

                //    localLevelPlaying = ChallengeId;
                //    //yield return new WaitForSeconds(0.5f);
                //    if (Character == 0)
                //    {
                //        SceneManager.LoadScene(Databank.SCENE_SEQUENCE);
                //    }
                //    else
                //    {
                //        SceneManager.LoadScene(Databank.SCENE_MAINMENU);
                //    }

                //    PowerUps();
                //}
                //else
                //    StartCoroutine(LoadScene(SCENE_LOGIN));

            }
        }
        catch (Exception)
        {
            Debug.Log("InitiateProfile Catch ");
            ResetState();
            StartCoroutine(LoadScene(SCENE_LOGIN));
        }
    }

    IEnumerator LoadScene(string scene)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }

    public void ConsumePowerUps(int _powerUpsID)
    {
        StartCoroutine(InitiateConsumePowerUps(_powerUpsID));
    }

    IEnumerator InitiateConsumePowerUps(int _powerUpsID)
    {
        //if (_score > Convert.ToInt32(Required_Points))
        //{
        var uwr = new UnityWebRequest(URL_ComsumePowerUps, UnityWebRequest.kHttpVerbPUT);


        string json = "{\"powerUpId\":\"" + _powerUpsID + "\"}";
        Debug.Log("Requested: " + json);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", "Bearer " + Databank.Access_Token);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("Consume-Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Consume-Received: " + uwr.downloadHandler.text);
            JSONNode jsonNode = SimpleJSON.JSON.Parse(uwr.downloadHandler.text);
           
            if(jsonNode[2] == "success")
            {
                Databank.instance.PowerupCount = jsonNode[0][1];
                if (Databank.instance.PowerupCount > 0)
                    Databank.instance.PowerupCount--;
            }
            
           
        }

    }

    void ResetState()
    {

        //PlayerPrefs.DeleteKey("firstStoryDone");
        PlayerPrefs.DeleteKey(Databank.LOCAL_ACCESS_TOKEN);
        PlayerPrefs.DeleteAll();
        //print(PlayerPrefs.GetInt("firstStoryDone"));
    }

}

public class Language
{
    public string language;
}

public class ScoreData
{
    public int score;
    public string description;
    public int challengeId;
    public int gameId;
}

public class LocalGameState
{
    public string localAccesToken;
    //public bool firstStoryDone;
    public string localName;
    public string localEmail;
    public string localGender;
    public int localAge;
    public int localCharacter;
    public int localChallengeID;
}


