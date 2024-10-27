using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCoin : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] SkyEthereums;
    Transform player;
    float maxDistance = 40.0f;
    List<GameObject> listOfTiles;

    public float spawnZ = 16.0f;
    public float tileLength = 140f;
    public int numOfTiles = 4;

    void Start()
    {
        listOfTiles = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //SpawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        if (PowerManager.instance.powerState == PowerManager.PowerState.JetPack)
        {
            if (player.position.z - 30f > (spawnZ - numOfTiles * tileLength))
            {
                
                SpawnTile();
                //DeleteTile();
            }
        }
        else
        {
            if (transform.childCount > 0)
                DeleteTile();
        }
           

        //if (PowerManager.instance.powerState == PowerManager.PowerState.JetPack || Input.GetKey(KeyCode.A))
        //{
        //    print("HAHAHAHAHA " + Vector3.Distance(transform.position, player.position) +  " < " + maxDistance) ;
        //    if (Vector3.Distance(transform.position, player.position) < maxDistance)
        //    {
        //        print("YYOOYYOO");
        //        if(transform.childCount == 0)
        //        {
        //            GameObject go = Instantiate(SkyEthereums[0], transform, false) as GameObject;
        //            go.transform.localPosition = new Vector3(go.transform.localPosition.x - 2, go.transform.localPosition.y, go.transform.localPosition.z);
        //        }
                   
                
               
        //        //print(go.transform.localPosition);
        //        //go.transform.SetParent(transform);
        //    }
        //}
    }

    void SpawnTile()
    {
        
        if (transform.childCount == 0)
        {
            //print("SpawnTile");
            GameObject go = Instantiate(SkyEthereums[0], transform, false) as GameObject;

            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, player.position.z + 30f);

            spawnZ += tileLength;
            listOfTiles.Add(go);
        }
            
    }

    void DeleteTile()
    {
        Destroy(listOfTiles[0]);
        listOfTiles.RemoveAt(0);
    }
}
