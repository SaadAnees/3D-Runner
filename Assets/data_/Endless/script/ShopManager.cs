using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject boy, girl;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("CHARACTER") == "boy")
        {
            boy.SetActive(true);
            girl.SetActive(false);
        }
        if (PlayerPrefs.GetString("CHARACTER") == "girl")
        {
            boy.SetActive(false);
            girl.SetActive(true);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("hall_of_fame");
    }
}
