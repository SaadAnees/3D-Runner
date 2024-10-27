using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Text itemName;
    public int itemId;
    public Image itemImage;
    public Text itemQuantity;
    public Text itemPointsRequired;

    public Button purchaseButton;


    private void Start()
    {
       // myselfButton = GetComponent<Button>();
        purchaseButton.onClick.AddListener(() => ButtonPurchase());

    }

    void ButtonPurchase()
    {
        PurchaseItem.instance.RegisterItem(itemId,purchaseButton);
        //PurchaseItem.instance.PurchaseShopItem(itemId);
    }

    
}
