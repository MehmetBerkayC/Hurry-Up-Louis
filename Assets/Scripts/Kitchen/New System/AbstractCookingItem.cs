using Cooking.Control;
using Cooking.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.World
{
    public abstract class AbstractCookingItem : MonoBehaviour
    {
        [SerializeField] protected CookingItemData itemData;
        [field: SerializeField] public bool AlwaysActive;

        public CookingItemData GetItemData() => itemData;

        protected void Start()
        {
            InitializeItem();
        }

        protected void InitializeItem()
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

        public void SetItemData(CookingItemData itemInfo)
        {
                itemData = itemInfo;
                InitializeItem();
        }
    }
}
