using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poweruploader : MonoBehaviour
{
    public List<Image> poweruploader = new List<Image>();
    // Start is called before the first frame update
    int i;

    public void OnEnable()
    {
        CancelInvoke("UpdateTime");
        i = 0;
        InvokeRepeating("UpdateTime", 0.8f, 0.8f);
    }

    void Start()
    {
        //i = 0;
        //InvokeRepeating("UpdateTime",0,1);
    }
    void UpdateTime()
    {

        if (i < poweruploader.Count)
        {
            poweruploader[i++].enabled = false;
        }
        else
        {
            CancelInvoke("UpdateTime");
            PowerManager.instance.StopAll();
            for (int j = 0; j < poweruploader.Count; j++)
            {
                poweruploader[j].enabled = true;
            }
            
        }
    }
    void OnDisable()
    {
        CancelInvoke("UpdateTime");
        for (int j = 0; j < poweruploader.Count; j++)
        {
            poweruploader[j].enabled = true;
        }
    }
}
