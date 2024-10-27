using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniComplete : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("hall_of_fame");
    }

    public void Awards()
    {
        SceneManager.LoadScene("awards");
    }

    public void Play()
    {
        SceneManager.LoadScene("mode_selection");
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("hall_of_fame");
        }

#endif
    }

}
