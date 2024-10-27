using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinMagnet : MonoBehaviour
{
    float speed = 30f;
    float maxDistance = 3.8f;
    Vector3 currPos;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == Databank.SCENE_SCIFI)
            currPos = new Vector3(transform.position.x, 1.1f, transform.position.z);
        else
            currPos = new Vector3(transform.position.x, 0.7f, transform.position.z);
        //print(currPos);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        //print(Vector3.Distance(transform.position, player.position));
        if (PowerManager.instance.powerState == PowerManager.PowerState.JetPack)
        {
            if (Vector3.Distance(transform.position, player.position) < maxDistance)
            {
                
                if (Vector3.Distance(transform.position, player.position) < 1f)
                    transform.GetComponent<MeshRenderer>().enabled = false;
                else
                    transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                //print(transform.position + " " + player.position);
            }
            else
                transform.position = currPos;

        }
        else
        {
            transform.position = currPos;
            
        }
           

    }
}
