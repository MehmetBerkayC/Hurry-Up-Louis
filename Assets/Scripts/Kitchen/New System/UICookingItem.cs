using Cooking.Data;
using Cooking.World;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class UICookingItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemNameText;

    [SerializeField] AbstractCookingItem heldItem;
    [SerializeField] CookingItemData heldItemData;

    public CookingItemData GetHeldItem() => heldItemData;

    private void Awake()
    {
        itemNameText = GetComponentInChildren<TextMeshProUGUI>();
        // Get the right image component
        Component[] componentsInChildren = GetComponentsInChildren<Image>();
        itemImage = (Image)componentsInChildren[1];

        ResetHeldItem();
    }

    public void SetHeldItem(AbstractCookingItem itemToHold)
    {
        if (itemToHold.GetItemData() == null) return;
        // Item Data
        heldItemData = itemToHold.GetItemData();
        itemNameText.text = heldItemData.Name;

        if (heldItemData.Sprite == null) itemImage.enabled = false;
        else
        {
            itemImage.enabled = true;
            itemImage.sprite = heldItemData.Sprite;
        }
        
        gameObject.SetActive(true);

        // World Item
        heldItem = itemToHold;
        if (!heldItem.AlwaysActive)
        {
            heldItem.gameObject.SetActive(false);
        }
    }

    public void ResetHeldItem()
    {
        heldItem = null;
        heldItemData = null;
        itemNameText.text = "";
        itemImage.sprite = null;
    }

    public void RevertHeldItem()
    {
        // Have to re-enable gameobject that holds the item
        heldItem.gameObject.SetActive(true);

        ResetHeldItem();
    }
}
