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

        [Header("References")]
        [SerializeField] CookingRecipe cookingRecipe;
        [SerializeField] UICookingItem cookingUI;
        [SerializeField] CookingItem currentHeldItem;
        [SerializeField] CookingItemData currentHeldItemData;

        [Header("Note & Dialogues")]
        [SerializeField] private NoteTrigger kitchenRecipeNote;
        [SerializeField] Dialogue wrongIngredientDialogue;
        [SerializeField] Dialogue missionSuccessfulDialogue;

        private CookingItemData nextItemToAdd;
        private int recipeIndex = -1;

        public static CookingController Instance;
        // Instead of doing something like this, please remember/have time to have a minigame bool in game manager
        public static bool IsMinigameOn = false; 

        private void Awake()
        {
            Instance = this;    
        }

        private void Start()
        {
            //StartCooking();    
        }

        public void StartCooking()
        {
            IsMinigameOn = true;
            // Do anything with UI or pausing gameplay
            CalculateNextStep(); // index should start with -1
        }

        private void EndCooking()
        {
            IsMinigameOn = false;
            // Dialogue
            AudioManager.Instance.Play("Mission Success");
            
            // Triggers
            ReminderTrigger.Instance.SetKitchenAsDone();
            ReminderTrigger.Instance.ActivateTrigger();

            // Mission Successful Dialogue
            if(missionSuccessfulDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(missionSuccessfulDialogue);
            }

            // Get rid of recipe note
            kitchenRecipeNote.DisposeOf();
        }

        private void WrongIngredient() // Dialogue and Sound
        {
            AudioManager.Instance.Play("Mission Fail");

            if (wrongIngredientDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(wrongIngredientDialogue);
            }
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

            EndCooking();
        }

        private void RevertCookingStep()
        {
            // check index bounds
            if(recipeIndex >= 0) recipeIndex -= 1; // Return 1 step

            // Re-enable item
            currentHeldItem.gameObject.SetActive(true);

            cookingUI.ResetHeldItem(); // UI reset

            CalculateNextStep();

            WrongIngredient(); // Warn Player
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