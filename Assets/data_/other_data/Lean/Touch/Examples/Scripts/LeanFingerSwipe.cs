using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace Lean.Touch
{
    /// <summary>This script fires events if a finger has been held for a certain amount of time without moving.</summary>
    [HelpURL(LeanTouch.HelpUrlPrefix + "LeanFingerSwipe")]
    public class LeanFingerSwipe : MonoBehaviour
    {

        public static LeanFingerSwipe instance;

        public enum ClampType
        {
            None,
            Normalize,
            Direction4,
            ScaledDelta
        }

        // Event signature
        [System.Serializable]
        public class FingerEvent : UnityEvent<LeanFinger>
        {
        }

        [System.Serializable]
        public class Vector2Event : UnityEvent<Vector2>
        {
        }

        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreStartedOverGui = true;

        [Tooltip("Ignore fingers with IsOverGui?")]
        public bool IgnoreIsOverGui;

        [Tooltip("Do nothing if this LeanSelectable isn't selected?")]
        public LeanSelectable RequiredSelectable;

        [Tooltip("Must the swipe be in a specific direction?")]
        public bool CheckAngle;

        [Tooltip("The required angle of the swipe in degrees, where 0 is up, and 90 is right")]
        public float Angle;

        [Tooltip("The left/right tolerance of the swipe angle in degrees")]
        public float AngleThreshold = 90.0f;

        [Tooltip("Should the swipe delta be modified before use?")]
        public ClampType Clamp;

        [Tooltip("The swipe delta multiplier, useful if you're using a Clamp mode")]
        public float Multiplier = 1.0f;

        // Called on the first frame the conditions are met
        public FingerEvent OnSwipe;

        public Vector2Event OnSwipeDelta;

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            Start();
        }
#endif

        protected bool CheckSwipe(LeanFinger finger, Vector2 swipeDelta)
        {
            // Invalid angle?
            if (CheckAngle == true)
            {
                var angle = Mathf.Atan2(swipeDelta.x, swipeDelta.y) * Mathf.Rad2Deg;
                var delta = Mathf.DeltaAngle(angle, Angle);

                if (delta < AngleThreshold * -0.5f || delta >= AngleThreshold * 0.5f)
                {
                    return false;
                }
            }

            // Clamp delta?
            switch (Clamp)
            {
                case ClampType.Normalize:
                    {
                        swipeDelta = swipeDelta.normalized;
                    }
                    break;

                case ClampType.Direction4:
                    {
                        if (swipeDelta.x < -Mathf.Abs(swipeDelta.y)) swipeDelta = -Vector2.right;
                        if (swipeDelta.x > Mathf.Abs(swipeDelta.y)) swipeDelta = Vector2.right;
                        if (swipeDelta.y < -Mathf.Abs(swipeDelta.x)) swipeDelta = -Vector2.up;
                        if (swipeDelta.y > Mathf.Abs(swipeDelta.x)) swipeDelta = Vector2.up;
                    }
                    break;

                case ClampType.ScaledDelta:
                    {
                        swipeDelta *= LeanTouch.ScalingFactor;
                    }
                    break;
            }

            // Call event
            if (OnSwipe != null)
            {
                OnSwipe.Invoke(finger);
            }

            if (OnSwipeDelta != null)
            {
                OnSwipeDelta.Invoke(swipeDelta * Multiplier);
            }

            return true;
        }

        protected virtual void OnEnable()
        {
            // Hook events
            LeanTouch.OnFingerSwipe += FingerSwipe;
        }



        void Start()
        {
            if (RequiredSelectable == null)
            {
                RequiredSelectable = GetComponent<LeanSelectable>();
            }

            if (instance == null)
            {
                instance = this;
            }

        }
        void Update()
        {

            //if (isleft && Player.transform.position.x >= 0.15f)
            //{
            //    Player.transform.position = Vector3.MoveTowards(Player.transform.position,
            //        new Vector3(Player.transform.position.x - 0.15f, Player.transform.position.y,
            //            Player.transform.position.z), Time.deltaTime * 0.6f);


            //    // print("isleft");
            //}

            //if (isRight && Player.transform.position.x <= 0.45f)
            //{
            //    Player.transform.position = Vector3.MoveTowards(Player.transform.position,
            //        new Vector3(Player.transform.position.x + 0.15f, Player.transform.position.y,
            //            Player.transform.position.z), Time.deltaTime * 0.6f);


            //    // print("isleft");
            //}


            // if (PowerManager.instance.isJetpack)
            // {
            //     //Player.transform.position = new Vector3(Player.transform.position.x, 0.1f, Player.transform.position.z);

            // }

            // else
            // {
            //     // Player.transform.position = new Vector3(Player.transform.position.x, 0.002f, Player.transform.position.z);

            // }

        }


        protected virtual void OnDisable()
        {
            // Unhook events
            LeanTouch.OnFingerSwipe -= FingerSwipe;
        }

        private void FingerSwipe(LeanFinger finger)
        {
            // Ignore?
            if (IgnoreStartedOverGui == true && finger.StartedOverGui == true)
            {
                return;
            }

            if (IgnoreIsOverGui == true && finger.IsOverGui == true)
            {
                return;
            }

            if (RequiredSelectable != null && RequiredSelectable.IsSelectedBy(finger) == false)
            {
                return;
            }

            // Perform final swipe check and fire event
            CheckSwipe(finger, finger.SwipeScreenDelta);
        }

        public GameObject Player;
        public Animator player_anim;


        private bool isleft = false;
        //public void Myleft()
        //{


        //    if (Player.transform.position.x > 0.21f)
        //    {
        //        CancelInvoke();
        //        isleft = true;
        //        if (PowerManager.instance.isRun)
        //        {

        //            // Player.transform.position = new Vector3(Player.transform.position.x - 0.15f, Player.transform.position.y, Player.transform.position.z);

        //            player_anim.SetBool("left", true);
        //            Invoke("Run1", 0.2f);
        //        }
        //        if (PowerManager.instance.isJetpack || PowerManager.instance.isGlide)
        //        {
        //            //                    Player.transform.position = new Vector3(Player.transform.position.x - 0.15f, Player.transform.position.y, Player.transform.position.z);
        //            player_anim.SetBool("fly_left", true);
        //            Invoke("Run1", 0.2f);
        //        }
        //        if (PowerManager.instance.isHover)
        //        {
        //            //                    Player.transform.position = new Vector3(Player.transform.position.x - 0.15f, Player.transform.position.y, Player.transform.position.z);
        //            player_anim.SetBool("hover_left", true);
        //            Invoke("Run1", 0.2f);
        //        }

        //    }

        //    // if(PowerManager.instance.isJetpack)
        //    // {
        //    //     Player.transform.position = new Vector3(Player.transform.position.x , 0.1f, Player.transform.position.z);
        //    // }
        //    // else
        //    // {
        //    //     Player.transform.position = new Vector3(Player.transform.position.x , 0.002f, Player.transform.position.z);
        //    // }
        //}

        //private bool isRight = false;
        //public void Myright()
        //{
        //    // print("Myright");
        //    if (Player.transform.position.x < 0.4f)
        //    {
        //        CancelInvoke();
        //        isRight = true;
        //        if (PowerManager.instance.isRun)
        //        {

        //            // Player.transform.position = new Vector3(Player.transform.position.x + 0.15f, Player.transform.position.y, Player.transform.position.z);
        //            player_anim.SetBool("right", true);
        //            Invoke("Run1", 0.2f);
        //        }
        //        if (PowerManager.instance.isJetpack || PowerManager.instance.isGlide)
        //        {
        //            //                    Player.transform.position = new Vector3(Player.transform.position.x + 0.15f, Player.transform.position.y, Player.transform.position.z);
        //            player_anim.SetBool("fly_right", true);
        //            Invoke("Run1", 0.2f);
        //        }
        //        if (PowerManager.instance.isHover)
        //        {
        //            //                    Player.transform.position = new Vector3(Player.transform.position.x + 0.15f, Player.transform.position.y, Player.transform.position.z);
        //            player_anim.SetBool("hover_right", true);
        //            Invoke("Run1", 0.2f);
        //        }

        //    }
        //}

        //public void Jump()
        //{
        //    if (PowerManager.instance.isRun)
        //    {
        //        player_anim.SetBool("jump", true);
        //        Player.GetComponent<CapsuleCollider>().center = new Vector3(0, 2.5f, 0);
        //        if (SceneManager.GetActiveScene().name != "curve_scifi")
        //            Player.transform.position = new Vector3(Player.transform.position.x, 0.05f, Player.transform.position.z);
        //        Invoke("Run", 0.5f);
        //    }

        //    if (PowerManager.instance.isHover)
        //    {
        //        print("Hover jump");
        //        player_anim.SetTrigger("hover_jump");
        //        Player.GetComponent<CapsuleCollider>().center = new Vector3(0, 2.5f, 0);
        //        Invoke("Run", 0.5f);
        //    }

        //}


        //void Run()
        //{
        //    player_anim.SetBool("jump", false);

        //    Player.GetComponent<CapsuleCollider>().center = new Vector3(0, 1.4f, 0);
        //    if (SceneManager.GetActiveScene().name != "curve_scifi")
        //        Player.transform.position = new Vector3(Player.transform.position.x, 0.002f, Player.transform.position.z);
        //}

        //void Run1()
        //{
        //    isleft = false;
        //    isRight = false;
        //    player_anim.SetBool("right", false);
        //    player_anim.SetBool("left", false);
        //    player_anim.SetBool("fly_right", false);
        //    player_anim.SetBool("fly_left", false);
        //    player_anim.SetBool("hover_right", false);
        //    player_anim.SetBool("hover_left", false);
        //}

    }
}