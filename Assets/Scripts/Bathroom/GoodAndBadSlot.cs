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

    public void Initialize(SelfCareItem item)
    {
        selfCareItem = item;
        icon.sprite = item.Icon;
        valueText.text = item.ItemValue.ToString();
    }

    public void Reset()
    {
        selfCareItem = null;
        icon.sprite = null; // Maybe find a default sprite
        valueText.text = "0";
    }

    public SelfCareItem GetItem()
    {
        return selfCareItem;
    }
}
