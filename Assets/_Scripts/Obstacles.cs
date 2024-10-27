using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public static Obstacles instance;

    public GameObject[] obst;

    public GameObject[] powers;

    public GameObject[] life;

    int lifes = 3;
    int i, j, k, i1;

    public bool now;

    private void OnEnable()
    {
        //        print("OBSTCALE");

        if (instance == null)
        {
            instance = this;
        }
        now = true;

        for (int j2 = 0; j2 < powers.Length; j2++)
        {
            powers[j2].SetActive(false);
        }
        //        print("OBSTCALE NOW");
        i = Random.Range(0, obst.Length);
        i1 = Random.Range(0, obst.Length);

        for (int t = 0; t < obst.Length; t++)
        {
            if (i != i1)
            {
                if (t == i)
                {
                    obst[t].SetActive(true);
                }
                if (t == i1)
                {
                    obst[t].SetActive(true);
                }
            }
            else
            {
                i1 = Random.Range(0, obst.Length);
            }


        }

        j = Random.Range(0, powers.Length);

        for (int i = 0; i < powers.Length; i++)
        {
            powers[Random.Range(0, j)].SetActive(true);
        }
        //if (j == 0 )
        //{
        //    powers[0].SetActive(true);
        //    powers[1].SetActive(false);
        //    powers[2].SetActive(false);
        //    powers[3].SetActive(false);
        //}
        //if (j == 1 )
        //{
        //    powers[0].SetActive(false);
        //    powers[1].SetActive(true);
        //    powers[2].SetActive(false);
        //    powers[3].SetActive(false);
        //}
        //if (j == 2)
        //{
        //    powers[0].SetActive(false);
        //    powers[1].SetActive(false);
        //    powers[2].SetActive(true);
        //    powers[3].SetActive(false);
        //}
        //if (j == 3)
        //{
        //    powers[0].SetActive(false);
        //    powers[1].SetActive(false);
        //    powers[2].SetActive(false);
        //    powers[3].SetActive(true);
        //}
        //if (j == 4 )
        //{
        //    powers[0].SetActive(false);
        //    powers[1].SetActive(true);
        //    powers[2].SetActive(false);
        //    powers[3].SetActive(false);
        //}
        //if (j == 5)
        //{
        //    powers[0].SetActive(false);
        //    powers[1].SetActive(true);
        //    powers[2].SetActive(false);
        //    powers[3].SetActive(true);
        //}
        //if (j == 6)
        //{
        //    powers[0].SetActive(false);
        //    powers[1].SetActive(true);
        //    powers[2].SetActive(true);
        //    powers[3].SetActive(false);
        //}
        //if (j == 7)
        //{
        //    powers[0].SetActive(true);
        //    powers[1].SetActive(true);
        //    powers[2].SetActive(false);
        //    powers[3].SetActive(false);
        //}



        if (now == true)
        {
            if (lifes <= 3)
            {
                k = Random.Range(0, 6);

                if (k == 0)
                {
                    life[0].SetActive(false);
                    life[1].SetActive(false);
                    life[2].SetActive(false);
                }
                if (k == 1)
                {
                    life[0].SetActive(true);
                    life[1].SetActive(false);
                    life[2].SetActive(false);
                }
                if (k == 2)
                {
                    life[0].SetActive(false);
                    life[1].SetActive(true);
                    life[2].SetActive(false);
                }
                if (k == 3)
                {
                    life[0].SetActive(false);
                    life[1].SetActive(false);
                    life[2].SetActive(true);
                }
                if (k == 4)
                {
                    life[0].SetActive(false);
                    life[1].SetActive(false);
                    life[2].SetActive(false);
                }
                if (k == 5)
                {
                    life[0].SetActive(false);
                    life[1].SetActive(false);
                    life[2].SetActive(false);
                }
            }
        }

    }


}
