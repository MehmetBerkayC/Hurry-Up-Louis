using Cooking.Control;
using Cooking.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.World
{
    public class CookingItem : MonoBehaviour, IInteractable
    {
        [SerializeField] CookingItemData itemData;

        private void Start()
        {
            InitializeItem();
        }

        private void InitializeItem()
        {
            if (itemData != null)
            {
                TryGetComponent(out SpriteRenderer spriteRenderer);
                if (spriteRenderer != null || itemData.Sprite != null)
                {
                    spriteRenderer.enabled = true;
                    spriteRenderer.sprite = itemData.Sprite;
                }
            }
        }

        public CookingItemData GetItemData() => itemData;

        public void SetItemData(CookingItemData itemInfo)
        {
            itemData = itemInfo;
            InitializeItem();
        }

        public void Interact()
        {
            CookingController.Instance.HoldItem(this);
        }
    }
}
