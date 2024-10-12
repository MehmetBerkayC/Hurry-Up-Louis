using Cooking.Control;
using Cooking.Data;
using Cooking.World;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Cooking.Data
{
    public class CookingItemOutcome : AbstractCookingItem, IInteractable
    {
        [Header("Required Items to Function -InOrder-")]
        [SerializeField] List<AbstractCookingItem> prerequisiteItems = new();
        private List<AbstractCookingItem> _previousItems = new();

        [Header("Item Configuration")]
        [SerializeField] Vector3 spawnPosition = Vector3.zero;
        [field: SerializeField] public bool IsInteractable { get; private set; } = false;

        [Header("After Item Function")]
        [SerializeField] CookingItem[] itemsToDeactivate;
        [SerializeField] CookingItem[] itemsToActivate;
    
        private Collider2D coll;
        private SpriteRenderer spriteRenderer;


        private new void Start()
        {
            AssignAndDeactivate_VisualsAndCollider();
        }

        private void AssignAndDeactivate_VisualsAndCollider()
        {
            coll = GetComponent<Collider2D>();
            coll.enabled = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }

        private void OnEnable()
        {
            CookingController.OnStateChanged += CheckInteractability;
            CookingController.OnItemPickedUp += UpdatePrerequisiteItems;
        }

        private void OnDisable()
        {
            CookingController.OnStateChanged -= CheckInteractability;
            CookingController.OnItemPickedUp -= UpdatePrerequisiteItems;
        }


        public void Interact()
        {
            if (IsInteractable)
            {
                CookingController.Instance.HoldItem(this);
            }
        }

        private void CheckInteractability()
        {
            if (!CookingController.IsMinigameOn)
            {
                IsInteractable = false;
            }
            else
            {
                IsInteractable = true;
            }
        }

        private void UpdatePrerequisiteItems() // DONE
        {
            // Get new item
            AbstractCookingItem item = CookingController.Instance.CurrentItem;
            Debug.Log(item);
            if (item != null)
            {
                _previousItems.Add(item);
            }
            else { return; }

            // Enough items
            if (_previousItems.Count == prerequisiteItems.Count)
                CheckPrerequisiteItems();

            // More items than needed
            if (_previousItems.Count > prerequisiteItems.Count) { 
                _previousItems.RemoveAt(0); // remove first item
            }
        }

        private void CheckPrerequisiteItems() // DONE
        {
            int validConditionSteps = 0;

            if (_previousItems.Count == prerequisiteItems.Count)
            {
                for (int i = 0; i < prerequisiteItems.Count; i++)
                {
                    CookingItemData previousItem = _previousItems[i].GetItemData();
                    CookingItemData requiredItem = prerequisiteItems[i].GetItemData();

                    if (previousItem != requiredItem) { 
                        Debug.Log("Item order not met, aborting operation!");
                        break;
                    }
                    else
                    {
                        validConditionSteps ++;
                    }
                }

                // Conditions Satisfied
                if (validConditionSteps == prerequisiteItems.Count) 
                    ItemConditionsSatisfied();
            }
        }

        private void ItemConditionsSatisfied() // DONE
        {
            // Activate Item
            SetUpItem();

            // Affect Items
            CheckAffectedItems();
        }

        private void SetUpItem() // DONE
        {
            // Set Spawn Position
            if (spawnPosition != Vector3.zero)
            {
                transform.position = spawnPosition;
            }

            IsInteractable = true;
            coll.enabled = true;
            InitializeItem();
        }

        private void CheckAffectedItems() // DONE
        {
            if (itemsToActivate.Length > 0)
            {
                foreach (var item in itemsToActivate)
                {
                    if (item != null) item.gameObject.SetActive(true);
                }
            }

            if (itemsToDeactivate.Length > 0)
            {
                foreach (var item in itemsToDeactivate)
                {
                    if (item != null) item.gameObject.SetActive(false);
                }
            }
        }
    }
}
