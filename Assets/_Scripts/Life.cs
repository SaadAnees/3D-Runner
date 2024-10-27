using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            VFXController.Instance.WinLifeParticlePlay();
            if (PlayerController.instance.lifeCounter < 3)
            {
                PlayerController.instance.LifeUpdate();
               
            }
            
        }
    }
}
