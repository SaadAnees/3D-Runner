using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // No death condition
            if(PowerManager.instance.powerState == PowerManager.PowerState.Shield || 
                PowerManager.instance.powerState == PowerManager.PowerState.Shield || 
                PlayerController.instance.isBlinking)
            {
               // Do something
            }
            else
            {
                print("DEATH: " + PowerManager.instance.powerState + " ===  " + PlayerController.instance.isBlinking);
                VFXController.Instance.LoseLifeParticlePlay();
                gameObject.SetActive(false);
                PlayerController.instance.DeadUpdate();
            }
           
        }
    }
}
