using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public GameObject[] collecetibles, path1, path2, path3, path4, path5, path6, path7, path8, path9, path10, path11;

    int i;

    // Start is called before the first frame update
    void OnEnable()
    {
        //print("collecetibles.Length " + collecetibles.Length);
        if (collecetibles.Length > 3)
        {
            for (int c = 0; c < collecetibles.Length - Random.Range(5, 13); c++)
            {
                collecetibles[c].SetActive(true);
                // print("hi");
                if (c == 0)
                {
                    for (int i = 0; i < path1.Length - Random.Range(0, 9); i++)
                    {
                        path1[i].SetActive(true);
                    }
                }

                if (c == 1)
                {
                    for (int i = 0; i < path2.Length - Random.Range(0, 9); i++)
                    {
                        path2[i].SetActive(true);
                    }
                }

                if (c == 2)
                {
                    for (int i = 0; i < path3.Length - Random.Range(0, path3.Length); i++)
                    {
                        path3[i].SetActive(true);
                    }
                }

                if (c == 3)
                {
                    for (int i = 0; i < path4.Length - Random.Range(0, 9); i++)
                    {
                        path4[i].SetActive(true);
                    }
                }

                if (c == 4)
                {
                    for (int i = 0; i < path5.Length - Random.Range(0, path5.Length); i++)
                    {
                        path5[i].SetActive(true);
                    }
                }

                if (c == 5)
                {
                    for (int i = 0; i < path6.Length - Random.Range(0, 9); i++)
                    {
                        path6[i].SetActive(true);
                    }
                }

                if (c == 6)
                {
                    for (int i = 0; i < path7.Length - Random.Range(0, 9); i++)
                    {
                        path7[i].SetActive(true);
                    }
                }

                if (c == 7)
                {
                    for (int i = 0; i < path8.Length - Random.Range(0, path8.Length); i++)
                    {
                        path8[i].SetActive(true);
                    }
                }

                if (c == 8)
                {
                    for (int i = 0; i < path9.Length - Random.Range(0, 9); i++)
                    {
                        path9[i].SetActive(true);
                    }
                }

                if (c == 9)
                {
                    for (int i = 0; i < path10.Length - Random.Range(0, path10.Length); i++)
                    {
                        path10[i].SetActive(true);
                    }
                }

                if (c == 10)
                {
                    for (int i = 0; i < path11.Length - Random.Range(0, 9); i++)
                    {
                        path11[i].SetActive(true);
                    }
                }

                if (c == 11)
                {
                    for (int i = 0; i < path11.Length - Random.Range(0, 9); i++)
                    {
                        path11[i].SetActive(true);
                    }
                }

            }


        }

        else if (collecetibles.Length == 3)

        {




            i = Random.Range(0, 4);

            if (i == 1)
            {
                collecetibles[0].SetActive(true);
                collecetibles[1].SetActive(false);
                collecetibles[2].SetActive(false);
                //collecetibles[3].SetActive(false);

                for (int i = 0; i < path1.Length - Random.Range(0, 5); i++)
                {
                    //print("1 ON");
                    path1[i].SetActive(true);
                }
            }
            if (i == 2)
            {
                collecetibles[0].SetActive(false);
                collecetibles[1].SetActive(true);
                collecetibles[2].SetActive(false);
                //collecetibles[3].SetActive(false);

                for (int i = 0; i < path1.Length - Random.Range(0, 5); i++)
                {
                    //print("2 ON");
                    path2[i].SetActive(true);
                }
            }
            if (i == 3)
            {
                collecetibles[0].SetActive(false);
                collecetibles[1].SetActive(false);
                collecetibles[2].SetActive(true);
                //collecetibles[3].SetActive(false);

                for (int i = 0; i < path1.Length - Random.Range(0, 5); i++)
                {
                    //print("3 ON");
                    path3[i].SetActive(true);
                }
            }
            if (i == 0)
            {
                collecetibles[0].SetActive(false);
                collecetibles[1].SetActive(false);
                collecetibles[2].SetActive(false);
                //collecetibles[3].SetActive(true);
            }
        }
    }

}
