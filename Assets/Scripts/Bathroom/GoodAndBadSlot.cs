using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodAndBadSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] Image icon;

    private SelfCareItem selfCareItem;

    private void Start()
    {
        Reset();
    }

    public void Initialize(SelfCareItem item)
    {
        selfCareItem = item;
        icon.sprite = item.Icon;
        valueText.text = item.ItemValue.ToString();
    }

    public void Reset()
    {
        icon.sprite = null; // Maybe find a default sprite
        valueText.text = "0";

        // Try to remove item
        if(!BathroomMinigameManager.Instance.RemoveItemFromList(selfCareItem))
        {
            Debug.Log("Couldn't remove item from list!");
        }

        selfCareItem = null;
    }

    public SelfCareItem GetItem()
    {
        return selfCareItem;
    }
}
