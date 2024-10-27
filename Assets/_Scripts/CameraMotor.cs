using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    Vector3 offset;

    public GameObject girl, boy;

    // Start is called before the first frame update
    void Awake()
    {

        lookAt = boy.transform;



    }

    void Start()
    {
        offset = transform.position - lookAt.position;
        //transform.position = lookAt.position + offset;
    }
   

    // Update is called once per frame
    void LateUpdate()
    {
        if(!PlayerController.instance.death)
        {
             Vector3 desiredPos = lookAt.position + offset;
             desiredPos.x = 0;
            //transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime);
            transform.position = desiredPos;
        }
      
        //transform.position = lookAt.position + offset;
    }
}
