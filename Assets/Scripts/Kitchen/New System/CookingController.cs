using Cooking.Data;
using Cooking.World;
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
        [SerializeField] CookingItem currentHeldItem;
        [SerializeField] CookingItemData currentHeldItemData;

        private CookingItemData nextItemToAdd;
        private int recipeIndex = -1;

        public static CookingController Instance;

        private void Awake()
        {
            Instance = this;    
        }

        private void Start()
        {
            StartCooking();    
        }

        private void StartCooking()
        {
            CalculateNextStep(); // index should start with -1
        }

        public void HoldItem(CookingItem cookingItem)
        {
            // World Item
            currentHeldItem = cookingItem;
            
            // Set up UI
            cookingUI.SetHeldItem(cookingItem);

            // Recipe Check
            currentHeldItemData = cookingItem.GetItemData();
            CheckRecipeStep();
        }

        private void CheckRecipeStep()
        {
            if (currentHeldItemData == null) return;    // Invalid Item 
            if (currentHeldItemData != nextItemToAdd)   // Wrong Item
            {
                RevertCookingStep();   
                return;
            }
            CalculateNextStep(); // Valid
        }

        private void CalculateNextStep()
        {
            recipeIndex++;
            if (recipeIndex < cookingRecipe.Recipe.Count) // there are items to go through
                nextItemToAdd = cookingRecipe.Recipe[recipeIndex];
            else
                RecipeCompleted();

            Debug.Log("Next: " + (recipeIndex < cookingRecipe.Recipe.Count ? nextItemToAdd.Name : "None"));
        }

        private void RecipeCompleted()
        {
            Debug.Log("Complete!");
            nextItemToAdd = null;
            // Dialogue, mission passed sound
        }

        private void RevertCookingStep()
        {
            // check index bounds
            if(recipeIndex > 0) recipeIndex -= 1; // Return 1 step

            // Re-enable item
            currentHeldItem.gameObject.SetActive(true);

            cookingUI.ResetHeldItem(); // UI reset

            CalculateNextStep();
        }
        // Think of cooking items as objects/checkpoints with trigger
        // Interact with certain collisions to progress and
        // if there is an item to hold, equip and display on UI
    }
}

// Things to Check
// Calculate next item by using recipeIndex:
//  - Check that there is an item
//  - Check that item is the one we need
//  - Go to next step:
//      - Increment recipe index
//      - Decide the next item to add
//  - If invalid item -> don't do anything
//  - If wrong item:
//      - Unequip the item and return back
//      - Can display a warning, sound or something else
//  ---- Only progress if holding the right item!! ----