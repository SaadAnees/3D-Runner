using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("mode_selection");
    }
}
