using Cooking.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICookingItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemName;

    [SerializeField] CookingItemData heldItem;

    public CookingItemData GetHeldItem() => heldItem;

    public void SetHeldItem(CookingItemData itemToHold)
    {
        if (itemToHold == null) return;
        
        heldItem = itemToHold;
        itemName.text = itemToHold.Name;
        itemImage.sprite = itemToHold.Sprite;
        gameObject.SetActive(true);
    }

    public void ResetHeldItem()
    {
        heldItem = null;
        itemName.text = "";
        itemImage.sprite = null;
        gameObject.SetActive(false);
    }
}
