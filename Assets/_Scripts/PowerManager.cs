using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager instance;
    public AudioClip hoverClip, shieldClip;
    public AudioSource audisources;
    public enum PowerState { JetPack, Hover, Shield, Run}

    //public bool isRun, isJetpack, isHover, isShiled, isPower, isGlide;

    public GameObject JetpackBtn, HoverBtn, ShieldBtn;
    public PowerState powerState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        powerState = PowerState.Run;

        //isRun = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F1))
            OnJetpack();


    }

    public void StopAll()
    {
        audisources.Stop();
        StopHover();
        StopJetPack();
        StopShield();
        OnRun();  
    }
   
    public void Glide()
    {

        StopArmour();
        StopJetPack();

        StopHover();
        StopShield();
        JetpackBtn.SetActive(true);

        
        PlayerController.instance.JetPack.SetActive(true);
        PlayerController.instance.anim.SetBool("fly", true);
       

        Invoke("StopGlide", 6);
    }

    public void OnJetpack()
    {
        Debug.Log("Yes");
        StopAll();
        audisources.PlayOneShot(hoverClip);
        powerState = PowerState.JetPack;
        print("PowerState " + PowerState.JetPack);
      
        if (!JetpackBtn.activeSelf)
            JetpackBtn.SetActive(true);
        else
            JetpackBtn.gameObject.transform.GetChild(1).GetComponent<Poweruploader>().OnEnable();
        //isPower = true;
        //isJetpack = true;
        //isRun = false;
        //isHover = false;
        //isGlide = false;
        //isShiled = false;
        PlayerController.instance.JetPack.SetActive(true);
        PlayerController.instance.anim.SetBool("fly", true);
        VFXController.Instance.PickGliderParticlePlay();


        //Invoke("StopFly", 6);
    }

    public void OnHover()
    {
        StopAll();
        audisources.PlayOneShot(hoverClip);
        powerState = PowerState.Hover;
        print("PowerState " + PowerState.Hover);
      
        HoverBtn.SetActive(true);
        //isPower = true;
        //isJetpack = false;
        //isRun = false;
        //isGlide = false;
        //isHover = true;
        //isShiled = false;
        PlayerController.instance.Hover.SetActive(true);
        PlayerController.instance.anim.SetBool("hover", true);
        VFXController.Instance.PickGliderParticlePlay();

       // Invoke("StopHover", 6);
    }

    public void OnRun()
    {
        powerState = PowerState.Run;
        //print("PowerState " + PowerState.Run);
       
       
        PlayerController.instance.anim.SetBool("run", true);
    }

    public void OnShield()
    {
        StopAll();
        audisources.PlayOneShot(shieldClip);
        powerState = PowerState.Shield;
        print("PowerState " + PowerState.Shield);
        ShieldBtn.SetActive(true);
        PlayerController.instance.Shield.SetActive(true);
        VFXController.Instance.PickUpShieldPlay();
        PlayerController.instance.shieldParticle.SetActive(true);
        //Invoke("StopShield", 6);
    }

    public void Armour()
    {

        StopAll();
        //isGlide = false;
        //isPower = true;
        //isShiled = true;
        //isRun = true;
        PlayerController.instance.Armour.SetActive(true);
        Invoke("StopArmour", 6);
    }

    void StopJetPack()
    {
        //print("StopJetPack");
        if (powerState == PowerState.JetPack)
           StartCoroutine(PlayerController.instance.AfterJetpackBlink(1.5f, 0.1f));
        PlayerController.instance.Shield.SetActive(false);

        PlayerController.instance.Hover.SetActive(false);

        JetpackBtn.SetActive(false);
        //isPower = false;
        //isRun = true;
        //isGlide = false;
        //isJetpack = false;
        //isShiled = false;
        //isGlide = false;
        PlayerController.instance.JetPack.SetActive(false);
        PlayerController.instance.anim.SetBool("fly", false);
    }
    void StopHover()
    {
        PlayerController.instance.Shield.SetActive(false);
        PlayerController.instance.JetPack.SetActive(false);

        HoverBtn.SetActive(false);
        //isPower = false;
        //isRun = true;
        //isGlide = false;
        //isJetpack = false;
        //isShiled = false;
        //isGlide = false;
        PlayerController.instance.Hover.SetActive(false);
        PlayerController.instance.anim.SetBool("hover", false);
    }
    void StopShield()
    {

        PlayerController.instance.JetPack.SetActive(false);
        PlayerController.instance.Hover.SetActive(false);
        ShieldBtn.SetActive(false);
        //isPower = false;
        //isRun = true;
        //isGlide = false;
        //isJetpack = false;
        //isShiled = false;
        //isGlide = false;
        PlayerController.instance.Shield.SetActive(false);
        PlayerController.instance.shieldParticle.SetActive(false);

    }
    void StopArmour()
    {
        //isPower = false;
        //isRun = true;
        PlayerController.instance.Armour.SetActive(false);
    }

    void StopGlide()
    {
        PlayerController.instance.Shield.SetActive(false);
        PlayerController.instance.JetPack.SetActive(false);
        PlayerController.instance.Hover.SetActive(false);
        JetpackBtn.SetActive(false);
        ShieldBtn.SetActive(false);
        //isPower = false;
        //isRun = true;
        //isGlide = false;
        //isJetpack = false;
        //isShiled = false;
        //isGlide = false;
        PlayerController.instance.anim.SetBool("fly", false);
    }

   
}
