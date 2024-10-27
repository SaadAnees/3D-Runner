using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    public int maxCoin = 9;
    public float chanceToSpawn = 0.5f;
    public bool forceSpawnAll = false;

    GameObject[] coins;

    void Awake()
    {
        coins = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject;
        }

        OnDisable();
    }

    void OnEnable()
    {
        if (Random.Range(0.0f, 1.0f) > chanceToSpawn)
            return;

        if(forceSpawnAll)
        {
            //print("if CoinSpawner");
            for (int i = 0; i < maxCoin; i++)
            {
                //print("CoinSpawnerCoinSpawnerCoinSpawnerCoinSpawnerCoinSpawner " + coins[i].gameObject.name);
                coins[i].SetActive(true);
            }
        }
        else
        {
            //print("ELSE ESLE CoinSpawner");
            int r = Random.Range(0, maxCoin);
            for (int i = 0; i < r; i++)
            {
                coins[i].SetActive(true);
            }
        }
            
    }

    void OnDisable()
    {
        foreach (GameObject go in coins)
        {
            go.SetActive(false);
        }
    }

}
