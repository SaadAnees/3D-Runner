using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{

    private static VFXController _instance;

    public static VFXController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    //VFX
    public GameObject loseLife;
    public GameObject winLife;
    public ParticleSystem pickGlider;
    public ParticleSystem pickDiamonds;
    public ParticleSystem shield;

    public Vector3 particlepos;


    public void LoseLifeParticlePlay()
    {
        
        Instantiate(loseLife,transform.position, transform.rotation,transform);
    }

    public void WinLifeParticlePlay()
    {
        particlepos = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);

        Instantiate(winLife,particlepos, transform.rotation,transform);
    }


    public void PickGliderParticlePlay()
    {
        Instantiate(pickGlider,transform.position, transform.rotation,transform);
    }


    public void PickUpDiamondsPlay()
    {
        Instantiate(pickDiamonds,transform.position, transform.rotation,transform);
    }

    public void PickUpShieldPlay()
    {
        Instantiate(shield,transform.position, transform.rotation,transform);
    }

}
