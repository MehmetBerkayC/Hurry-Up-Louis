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
        [Header("References")]
        [SerializeField] CookingRecipe cookingRecipe;
        [SerializeField] UICookingItem cookingUI;

        [Header("Step Data")]
        [SerializeField] AbstractCookingItem currentHeldItem;
        [SerializeField] AbstractCookingItem previousItem;
        [SerializeField] CookingItemData nextItemToAdd;

        [Header("Note & Dialogues")]
        [SerializeField] NoteTrigger kitchenRecipeNote;
        [SerializeField] Dialogue wrongIngredientDialogue;
        [SerializeField] Dialogue missionSuccessfulDialogue;

        private int recipeIndex = -1;

        public static CookingController Instance;
        // Instead of doing something like this, please remember/have time to have a minigame bool in game manager
        public static bool IsMinigameOn = false;

        public AbstractCookingItem GetPreviousItem() => previousItem;
        public AbstractCookingItem GetCurrentItem() => currentHeldItem;

        private bool _firstItem = true;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void StartCooking()
        {
            IsMinigameOn = true;
            _firstItem = true;
            // UI
            cookingUI.gameObject.SetActive(false);

            CalculateNextStep();
        }

        private void EndCooking()
        {
            IsMinigameOn = false;
            // Dialogue
            AudioManager.Instance.PlaySFX("Mission Success");
            
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
            // UI
            cookingUI.ResetHeldItem();
            cookingUI.gameObject.SetActive(false);
        }

        private void WrongIngredientNotice() // Dialogue and Sound
        {
            AudioManager.Instance.PlaySFX("Mission Fail");

            if (wrongIngredientDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(wrongIngredientDialogue);
            }
        }

        public void HoldItem(AbstractCookingItem cookingItem)
        {
            if (!IsMinigameOn) return;

            CheckRecipeStep(cookingItem);
        }

        private void CheckRecipeStep(AbstractCookingItem cookingItem)
        {
            if (cookingItem == null && !_firstItem) return;    // Invalid Item 
            if (cookingItem.GetItemData() != nextItemToAdd)   // Wrong Item
            {
                RevertCookingStep();   
                return;
            }
            CalculateNextStep(cookingItem); // Valid
        }

        private void CalculateNextStep(AbstractCookingItem cookingItem = null)
        {
            if(cookingItem == null && _firstItem) // Firt item of the recipe
            {
                recipeIndex++;
                nextItemToAdd = cookingRecipe.Recipe[recipeIndex]; // Advance to next item
                _firstItem = false;
                Debug.Log("Next: " + (recipeIndex < cookingRecipe.Recipe.Count ? nextItemToAdd.Name : "None"));
                return;
            }

            // ----- Other Items -----
            recipeIndex++;
            if (recipeIndex < cookingRecipe.Recipe.Count) // there are items to go through
            {
                PickUpItem(cookingItem);
                nextItemToAdd = cookingRecipe.Recipe[recipeIndex]; // Advance to next item
            }
            else
            {
                PickUpItem(cookingItem);
                RecipeCompleted();
            }

            Debug.Log("Next: " + (recipeIndex < cookingRecipe.Recipe.Count ? nextItemToAdd.Name : "None"));
        }

        private void PickUpItem(AbstractCookingItem cookingItem)
        {
            previousItem = currentHeldItem; // Remember current item
            currentHeldItem = cookingItem;
            // Set up UI
            cookingUI.SetHeldItem(cookingItem);
            // Pick up Sound
            AudioManager.Instance.PlaySFX("Pick Up");
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
            if (recipeIndex >= 0) recipeIndex -= 1; // Return 1 step

            if(previousItem != null) { 
                // Re-enable item
                currentHeldItem.gameObject.SetActive(true);
            }

            // Reset current item and UI
            cookingUI.ResetHeldItem();
            ResetCurrentItem();

            // Re-calculate
            CalculateNextStep();

            WrongIngredientNotice(); // Warn Player
        }

        private void ResetCurrentItem() 
        {
            if (previousItem != null)
            {
                cookingUI.SetHeldItem(currentHeldItem);
            }
        }
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

// Think of cooking items as objects/checkpoints with trigger
// Interact with certain collisions to progress and
// if there is an item to hold, equip and display on UI
// ------------------------------------------------------------------
// Need some kind of recipe
// When the game starts, make every item interactable or visible
// When an item is acquired, check if it is collected in order
// if not, return the item back to its position -> solve by disable/enable items
// - Display a message indicating item being picked in wrong order