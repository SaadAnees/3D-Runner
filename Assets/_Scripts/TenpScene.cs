using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TenpScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        print("TenpSceneTenpSceneTenpSceneTenpSceneTenpScene");
        SceneManager.LoadSceneAsync(MyConstantVariables.ToLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
