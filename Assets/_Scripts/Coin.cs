using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //VFXController.Instance.PickUpDiamondsPlay();
            //if (PowerManager.instance.powerState == PowerManager.PowerState.JetPack)
            //    print("Coin Picks ");
            gameObject.SetActive(false);
            PlayerController.instance.ScoreUpdate();
        }
    }
}
