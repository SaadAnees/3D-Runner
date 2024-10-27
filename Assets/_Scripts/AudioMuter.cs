using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioMuter : MonoBehaviour
{
   void Start()
   {
       try{
            GameObject.Find("AudioMaster").GetComponent<AudioSource>().volume = 0.5f;
        } catch(System.Exception)
        {

        }
   }
}
