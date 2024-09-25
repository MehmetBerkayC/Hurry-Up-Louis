using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelfCareItemHolder : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI valueText;

    public SelfCareItem careItem;

    public void Initialize() 
    {
        if(image == null) image = GetComponentInChildren<Image>(); // safety, but won't do it for texts
        
        image.sprite = careItem.Icon;
        nameText.text = careItem.ItemName;
        valueText.text = careItem.ItemValue.ToString();
    }

    public void SelectItem() // Send item info on selection
    {
        BathroomMinigameManager.Instance.ItemSelectedFromDisplay(careItem);
    }
}
