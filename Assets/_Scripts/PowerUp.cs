using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   
   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            //print(this.gameObject.tag);
            //FindObjectOfType<SoundManager>().PlayPowerUpClip();
            if (this.gameObject.tag == "hover")
                PowerManager.instance.OnHover();

            //if (this.gameObject.tag == "glider")
            //    PowerManager.instance.Glide();

            if (this.gameObject.tag == "jetpack")
                PowerManager.instance.OnJetpack();

            if (this.gameObject.tag == "shield")
                PowerManager.instance.OnShield();
        }
    }

    
}
