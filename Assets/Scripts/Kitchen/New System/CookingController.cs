using Cooking.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Control
{
    public class CookingController : MonoBehaviour
    {
        // Need some kind of recipe
        // When the game starts, make every item interactable or visible
        // When an item is acquired, check if it is collected in order
        // if not, return the item back to its position -> solve by disable/enable items
        // - Display a message indicating item being picked in wrong order
        // 

        [SerializeField] CookingRecipe cookingRecipe;
        [SerializeField] UICookingItem cookingUI;
        [SerializeField] CookingItemData currentHeldItem;

        private CookingItemData nextItemToAdd;
        private int recipeIndex = 0;

        public static CookingController Instance;

        private void Awake()
        {
            Instance = this;    
        }

        public void HoldItem(CookingItemData itemToHold)
        {
            cookingUI.SetHeldItem(itemToHold);
            currentHeldItem = itemToHold;
            CalculateNextStep();
        }

        private void CalculateNextStep()
        {
            if (recipeIndex >= cookingRecipe.Recipe.Count) return;      // Valid Index
            if (currentHeldItem != null) return;                        // Valid Item 
            if (currentHeldItem != nextItemToAdd) ResetCookingStep();   // ^




        }

        private void ResetCookingStep()
        {
            cookingUI.ResetHeldItem();
            // Re-enable item, Update UI -Remove-
            // check index
        }
        // Think of cooking items as objects/checkpoints with trigger
        // Interact with certain collisions to progress and
        // if there is an item to hold, equip and display on UI
    }
}
