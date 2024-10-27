using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject path1, path2;

    public int movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
    }

    private void FixedUpdate()
    {
        //transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
    }

    private void OnTriggerExit(Collider other)
    {

        print("1");

        //if(other.gameObject.tag == "path1")
        //{
        //    print("2");
        //    path2.transform.position = new Vector3(path2.transform.position.x,path2.transform.position.y,path2.transform.position.z+400);
        //}

        //if (other.gameObject.tag == "path2")
        //{
        //    print("3");
        //    path1.transform.position = new Vector3(path1.transform.position.x, path1.transform.position.y, path1.transform.position.z + 400);
        //}

    }

}
