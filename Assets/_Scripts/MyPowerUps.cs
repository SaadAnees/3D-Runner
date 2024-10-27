using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPowerUps : MonoBehaviour
{

    public Text itemName;
    public Image itemImage;
    public Text itemQuantity;
    public int itemId;


    public Button markButton;

    private void Start()
    {
        markButton.onClick.AddListener(() => ButtonMark());

    }

    void ButtonMark()
    {
        Consume.instance.RegisterMarkItem(itemId, itemName.text);
    }
}
