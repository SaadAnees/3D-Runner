using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // PowerManager powerState;

    public CharacterController controller;

    public AudioClip ethiriumClip, lifeClip, deathClip, powerupClip, shieldClip, hoverClip, wooshClip;

    public GameObject life1, life2, life3;
    public int lifeCounter = 3;
    public Text score_txt, header_txt, timer_txt;
    public int scoreCounter;
    public bool death = false;
    float time;

    public GameObject JetPack, Armour, Shield, Hover;

    public GameObject DeathMenu;
    public Animator anim;
    /// <summary>
    /// PlayerMotor
    /// 
    /// </summary>
    float LANE_DISTANCE = 1.1f;
    const float TURN_SPEED = 1.05f;
    float jumpForce = 7.0f;
    float jetpackForce = 4.0f;
    float gravity = 12.0f;
    float verticalvelocity;
    [SerializeField]
    float speed = 12.0f;
    int desiredLane = 1;
    /// <summary>
    /// PlayerMotor
    /// 
    /// </summary>
    /// 
    int difficultyLevel = 1;
    int maxDifficulttyLevel = 15;
    int scoreToNextLevel = 15;
    public GameObject Instructions;
    public GameObject shieldParticle;
    float minutes;
    float seconds;
    SoundManager soundManager;

    private void OnEnable()
    {
        soundManager = FindObjectOfType<SoundManager>();
        if (instance == null)
        {
            instance = this;
        }
    }

    void OnDestroy()
    {
        print("OnDestroy");
        //soundManager.PlayMusic(soundManager.Music);
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == Databank.SCENE_SCIFI)
            LANE_DISTANCE = 2f;
        lifeCounter = 3;

        if (instance == null)
        {
            instance = this;
        }
       // if (SceneManager.GetActiveScene().name == Databank.SCENE_SCIFI)
            //Obstacles.instance.now = true;

            Time.timeScale = 1;

        //if (SceneManager.GetActiveScene().name == Databank.SCENE_SCIFI)
        //    FindObjectOfType<SoundManager>().PlaySharjah();
        //else
        //    FindObjectOfType<SoundManager>().PlayMeyrin1();

#if UNITY_EDITOR
        time = 900f;

#endif
#if ANDROID
       

#endif
        //time = Databank.instance.Time_Limit;
        // float.TryParse(PlayerPrefs.GetString("MISSION_timelimit"), out time);

        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);

        score_txt.text = "0";

    }
    

    private void Update()
    {
        if (Instructions.activeSelf)
            return;
        ///////////////////////////////////////////////
        ///
        
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLane(false);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveLane(true);

        if (MobileInput.Instance.SwipeLeft)
        {
            //anim.SetTrigger("turn_left");
            MoveLane(false);
        }

        if (MobileInput.Instance.SwipeRight)
        {
            // if (!anim.GetCurrentAnimatorStateInfo(0).IsName("right")) // to make sure its not playing  animation again
            // anim.SetTrigger("turn_right");
            MoveLane(true);
        }
        //print("timescale " + Time.timeScale);
        //print(anim.GetCurrentAnimatorStateInfo(0).IsName("run"));

        //Vector3 v = new Vector3(1, 1, 1);
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;


        bool isGrounded = IsGrounded();

        if (isGrounded)
        {
            anim.SetBool("grounded", isGrounded);
            //print("isGrounded");
            verticalvelocity = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalvelocity = jumpForce;
                Jump();

            }

            if (MobileInput.Instance.SwipeUp)
            {
                //print("SwipeUp");
                verticalvelocity = jumpForce;
                Jump();
            }
        }
        else
        {
            // condition to avoid coming down abruptly after jetpack finishes
            if (PowerManager.instance.powerState != PowerManager.PowerState.JetPack)
            {
                //print("PowerManagerPowerManager");
                verticalvelocity -= (gravity * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    verticalvelocity = -jumpForce;
                }

                if (MobileInput.Instance.SwipeDown)
                {
                    verticalvelocity = -jumpForce;
                }
            }


        }

        // Jetpack properties
        if (PowerManager.instance.powerState == PowerManager.PowerState.JetPack)
        {
            //print(transform.position.y);
            if (transform.position.y <= 4f)
                moveVector.y = jetpackForce;

            //WorldTileManager.instance.maxSpeed = 5f;
            speed = 20f;
            // anim.SetBool("grounded", isGrounded);

        }
        else if (PowerManager.instance.powerState == PowerManager.PowerState.Hover && !anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            if (MobileInput.Instance.SwipeUp)
            {
                //print("SwipeUp");
                verticalvelocity = jumpForce - 2;
                Jump();
            }
            //print(anim.GetCurrentAnimatorStateInfo(0).IsName("jump"));
            if (SceneManager.GetActiveScene().name == Databank.SCENE_MEYRIN1)
            {
                if (transform.position.y <= 0.5f)
                    moveVector.y = 0.1f;
            }
            else
            {
                if (transform.position.y <= 1f)
                    moveVector.y = 0.1f;
            }

            // anim.SetBool("grounded", isGrounded);

        }
        else
        {
            
            speed = 12f;
            verticalvelocity -= (gravity * Time.deltaTime);
            moveVector.y = verticalvelocity;
        }



        // To move player forward. In our case environment is moving towards player.

        if (death)
            moveVector.z = 0;
        else
            moveVector.z = speed;
        controller.Move(moveVector * Time.deltaTime);

        //Rotate animation forward
        Vector3 dir = controller.velocity * Time.deltaTime;
        if (dir != Vector3.zero)
        {
            //print("controller.velocity " + dir);
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }


        ///////////////////////////////////////////////////
        ///

        if (time <= 1)
        {
            Time.timeScale = 0;
            GameOver(minutes, seconds);

            return;
        }
        time -= Time.deltaTime;
        minutes = Mathf.Floor(time / 60);
        seconds = Mathf.FloorToInt(time % 60);

        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer_txt.text = niceTime; //minutes.ToString("00") + ":" + seconds.ToString("00");

    }



    void Jump()
    {
        print("jump");
        //this.gameObject.GetComponent<AudioSource>().PlayOneShot(wooshClip);
        if (PowerManager.instance.powerState == PowerManager.PowerState.Hover)
            anim.SetTrigger("hover_jump");
        else
            anim.SetTrigger("jump");
    }

    void MoveLane(bool goingRight)
    {
        //this.gameObject.GetComponent<AudioSource>().PlayOneShot(wooshClip);
        //print("MoveLane: " + goingRight);
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

        //print("desiredLane: " + desiredLane);
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(
            new Vector3(
            controller.bounds.center.x,
            (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
            controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);
        return Physics.Raycast(groundRay, 0.2f + 0.1f);
    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        //Debug.Log("Collider: " + other.gameObject.name + " + " + other.gameObject.tag);
        //if (other.gameObject.tag == "collectibles")
        //{
        //    this.gameObject.GetComponent<AudioSource>().PlayOneShot(ethirium);
        //    other.gameObject.SetActive(false);
        //    if (PowerManager.instance.isHover)
        //        scoreCounter += 2;
        //    else
        //        scoreCounter++;

        //    //if (scoreCounter > scoreToNextLevel && !PowerManager.instance.isHover)
        //    //    LevelUp();
        //    score_txt.text = scoreCounter.ToString();




        //}

        //if (other.gameObject.tag == "obstacle")
        //{
        //    if (PowerManager.instance.isShiled || PowerManager.instance.isJetpack)
        //        return;

        //    StartCoroutine(DoBlinks(3f, 0.2f));

        //    other.gameObject.transform.parent.gameObject.SetActive(false);

        //    lifeCounter--;

        //    if (lifeCounter == 2)
        //    {
        //        life3.SetActive(false);
        //    }
        //    if (lifeCounter == 1)
        //    {
        //        life3.SetActive(false);
        //        life2.SetActive(false);
        //    }
        //    if (lifeCounter == 0)
        //    {
        //        life3.SetActive(false);
        //        life2.SetActive(false);
        //        life1.SetActive(false);
        //    }
        //    if (lifeCounter <= 0)
        //    {
        //        Time.timeScale = 0;
        //        GameOver();
        //        //SceneManager.LoadScene("map");
        //    }

        //    // print("LIFE COUNTER = " + lifeCounter);

        //    anim.SetBool("hit", true);
        //    WorldTileManager.instance.maxSpeed = 0;

        //    Invoke("Run", 1.0f);

        //    //Run();

        //}

        //if (other.gameObject.tag == "shield")
        //{

        //    this.gameObject.GetComponent<AudioSource>().PlayOneShot(ethirium);
        //    other.gameObject.SetActive(false);
        //    PowerManager.instance.Shiled();
        //}
        //if (other.gameObject.tag == "glider")
        //{
        //    print("glider");
        //    this.gameObject.GetComponent<AudioSource>().PlayOneShot(ethirium);
        //    other.gameObject.SetActive(false);
        //    PowerManager.instance.Glide();
        //}
        //if (other.gameObject.tag == "hover")
        //{
        //    this.gameObject.GetComponent<AudioSource>().PlayOneShot(ethirium);
        //    other.gameObject.SetActive(false);
        //    PowerManager.instance.Hover();
        //}
        //if (other.gameObject.tag == "jetpack")
        //{
        //    this.gameObject.GetComponent<AudioSource>().PlayOneShot(ethirium);
        //    other.gameObject.SetActive(false);
        //    PowerManager.instance.Jetpack();
        //}


    }

    public void ScoreUpdate()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(ethiriumClip);

        if (PowerManager.instance.powerState == PowerManager.PowerState.Hover)
            scoreCounter += 2;
        else
            scoreCounter++;

        //if (scoreCounter > scoreToNextLevel && !PowerManager.instance.isHover)
        //    LevelUp();
        score_txt.text = scoreCounter.ToString();
    }

    public void DeadUpdate()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(deathClip);
        death = true;
        StartCoroutine(DoBlinks(3f, 0.2f));

        lifeCounter--;

        if (lifeCounter == 2)
        {
            life3.SetActive(false);
        }
        if (lifeCounter == 1)
        {
            life3.SetActive(false);
            life2.SetActive(false);
        }
        if (lifeCounter == 0)
        {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(false);
        }
        if (lifeCounter <= 0)
        {
            Time.timeScale = 0;
            GameOver(minutes, seconds);
            //SceneManager.LoadScene("map");
        }

        // print("LIFE COUNTER = " + lifeCounter);

        anim.SetBool("hit", true);

        Invoke("Run", 1.0f);

    }

    public void LifeUpdate()
    {
        //print("Life Counter");
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(lifeClip);

        lifeCounter++;

        if (lifeCounter == 1)
        {
            life1.SetActive(true);
            life3.SetActive(false);
            life2.SetActive(false);
        }
        if (lifeCounter == 2)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }
        if (lifeCounter == 3)
        {
            life3.SetActive(true);
            life2.SetActive(true);
            life1.SetActive(true);
        }

    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficulttyLevel)
            return;

        if (PowerManager.instance.powerState == PowerManager.PowerState.Run)
            return;
        scoreToNextLevel *= 2;
        difficultyLevel++;

        print("Speed Up: " + difficultyLevel);
        print("scoreToNextLevel : " + scoreToNextLevel);
        WorldTileManager.instance.maxSpeed = (float)difficultyLevel;
    }

    void Run()
    {
        death = false;
        anim.SetBool("hit", false);
        //WorldTileManager.instance.maxSpeed = 2;
        difficultyLevel = 2;
        scoreToNextLevel = 15;
        //print("difficultyLevel " + difficultyLevel);
    }

    public Renderer[] renderer;
    public bool isBlinking = false;

    IEnumerator DoBlinks(float duration, float blinkTime)
    {
        while (duration > 0f)
        {
            duration -= 0.25f;

            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].enabled = !renderer[i].enabled;
            }

            //wait for a bit
            yield return new WaitForSeconds(blinkTime);

        }

        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].enabled = true;
        }

    }

    public IEnumerator AfterJetpackBlink(float duration, float blinkTime)
    {
        while (duration > 0f)
        {
            isBlinking = true;
            duration -= 0.25f;

            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].enabled = !renderer[i].enabled;
            }

            //wait for a bit
            yield return new WaitForSeconds(blinkTime);

        }

        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].enabled = true;
        }

        isBlinking = false;


    }

    void GameOver(float mins, float sec)
    {
        Debug.Log("Show Dead Menu");
        //timer_txt.text = mins.ToString() + ":" + sec.ToString();
        if (!DeathMenu.activeSelf)
        {
           
            DeathMenu.SetActive(true);
            //Databank.instance.Stars();
        }


    }

}
