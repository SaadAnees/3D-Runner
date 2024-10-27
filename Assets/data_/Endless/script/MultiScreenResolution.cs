using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MultiScreenResolution : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        float ratio = ((float)Screen.height / (float)Screen.width);
        float ratio16_9 = (16.0f / 9.0f);
        float ratio4_3 = (4.0f / 3.0f);

        float ratioX = (2436.0f / 1125.0f);
        //float ratioX_Max = (2688.0f / 1242.0f);

        if (ratio - 0.001f > ratio16_9)
        {
            float a = (ratio - ratio16_9) / (ratioX - ratio16_9);
            camera.orthographicSize = Mathf.Lerp(7.0f, 9f, a);

            this.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.12f;
        }

        if (ratio + 0.001f < ratio16_9)
        {
            float a = (ratio16_9 - ratio) / (ratio16_9 - ratio4_3);
            camera.orthographicSize = Mathf.Lerp(7.0f, 5.8f, a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
