using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarTransition : MonoBehaviour
{
        public GameObject Loader_value;

    float load;

    // Start is called before the first frame update
    void Start()
    {
        Loader_value.GetComponent<Image>().fillAmount = 0;

        InvokeRepeating("FillLoader",0.1f,0.1f);
          
    }

    void FillLoader()
    {

        load = load + 0.1f;

        Loader_value.GetComponent<Image>().fillAmount = load;

        if(Loader_value.GetComponent<Image>().fillAmount == 1)
        {
            print("DONE");
            load = 0;
            Loader_value.GetComponent<Image>().fillAmount = 1;
        }
    }
}
