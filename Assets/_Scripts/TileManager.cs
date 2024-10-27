using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;
	Transform playerTransform;
	public float spawnZ = 16.0f;
	public float tileLength = 140f;
    public int numOfTiles = 4;
	List<GameObject> listOfTiles;
	// Use this for initialization
	void Start () {
		listOfTiles = new List<GameObject> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
        //print(numOfTiles);
		for (int i = 0; i < numOfTiles; i++) {
            if (i < 1)
                SpawnTile(0);
            else
                SpawnTile();
            //print(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTransform != null) {
            if(SceneManager.GetActiveScene().name == Databank.SCENE_MEYRIN1)
            {
                if (playerTransform.position.z - 30f > (spawnZ - numOfTiles * tileLength))
                {
                    SpawnTile();
                    DeleteTile();
                }
            }
            else
            {
                if (playerTransform.position.z - 120f > (spawnZ - numOfTiles * tileLength))
                {
                    SpawnTile();
                    DeleteTile();
                }
            }
			
		}
	}

	void SpawnTile(int prefabIndex = -1)
	{
		GameObject go;
        if(prefabIndex == -1)
		    go = Instantiate (tilePrefabs [Random.Range(0, tilePrefabs.Length)]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.position = new Vector3 (0f, 0f, spawnZ);
        go.transform.SetParent(transform);
        //go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
		listOfTiles.Add (go);
	}

	void DeleteTile()
	{
		Destroy (listOfTiles [0]);
		listOfTiles.RemoveAt (0);
	}
}
