using Cooking.Control;
using Cooking.Data;
using Cooking.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingItemOutcome : AbstractCookingItem, IInteractable
{
    [SerializeField] CookingItemData previousStepItem;
    [SerializeField] CookingItemData currentStepItem;   // Next one is this one
    [SerializeField] Vector3 spawnPosition = Vector3.zero;
    
    private Collider2D coll;
    private SpriteRenderer spriteRenderer;
    public bool IsInteractable { get; private set; } = false;

    private new void Start()
    {
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        CheckItemConditions();
    }

    public void Interact()
    {
        if (IsInteractable)
        {
            CookingController.Instance.HoldItem(this);
        }
    }

    private void CheckItemConditions()
    {
        // is required item valid?
        AbstractCookingItem prevItem = CookingController.Instance.GetPreviousItem();
        AbstractCookingItem currentItem = CookingController.Instance.GetCurrentItem();

        if (previousStepItem != null && prevItem != null && currentStepItem != null && currentItem != null)
        {
            if (spawnPosition != Vector3.zero)
            {
                transform.position = spawnPosition;
            }

            if (previousStepItem == prevItem.GetItemData() && currentStepItem == currentItem.GetItemData())
            {
                IsInteractable = true;
                coll.enabled = true;
                InitializeItem();
            }
        }
        else
        {
            return;   
        }
    }
}
