using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public static MessageBox instance;
   
    public GameObject MessageCanvas;
     Button _button;
     Text _message;
    public bool isShowing = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    

    public void Show(string message)
    {
        if (isShowing)
            return;

        isShowing = true;
        Debug.Log("Show Message: " + message);
       
        GameObject go = Instantiate(MessageCanvas);
        _message = go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        _message.text = message;
        _button = go.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>();
        _button.onClick.AddListener(Hide);
    }

    public void Hide()
    {
        Debug.Log("Hide Message");
        _message.text = "";
        _button.onClick.RemoveListener(Hide);
        Destroy(GameObject.FindGameObjectWithTag("MessageCanvas"));
        isShowing = false;
    }
}
