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
        [SerializeField] CookingItemData nextItemToAdd;

        [Header("Note & Dialogues")]
        [SerializeField] NoteTrigger kitchenRecipeNote;
        [SerializeField] Dialogue wrongIngredientDialogue;
        [SerializeField] Dialogue missionSuccessfulDialogue;

        private int recipeIndex = -1; // Starts from 0 as first item check

        public static CookingController Instance;
        // Instead of doing something like this, please remember/have time to have a minigame bool in game manager
        public static bool IsMinigameOn = false;

        public AbstractCookingItem GetCurrentItem() => currentHeldItem;

        public static Action OnStateChanged;

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

        public void StartCooking() // DONE
        {
            // Set Up the Game
            recipeIndex = -1;
            IsMinigameOn = true;
            
            // Decide the first item
            CalculateFirstItem();

            // Invoke Events
            OnStateChanged?.Invoke();
        }

        private void CalculateFirstItem() // DONE
        {
            recipeIndex++;
            nextItemToAdd = cookingRecipe.Recipe[recipeIndex]; // Advance to next item
            Debug.Log("Next: " + (recipeIndex < cookingRecipe.Recipe.Count ? nextItemToAdd.Name : "None"));
        }

        private void EndCooking() // DONE
        {
            // Minigame OFF
            IsMinigameOn = false;
            
            AudioManager.Instance.PlaySFX("Mission Success");

            // Triggers
            ReminderTrigger.Instance.SetKitchenAsDone();
            ReminderTrigger.Instance.ActivateTrigger();
            
            // Dialogue
            if(missionSuccessfulDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(missionSuccessfulDialogue);
            }

            // Get rid of recipe note
            kitchenRecipeNote.DisposeOf();
            
            // Reset UI
            cookingUI.ResetHeldItem();
            cookingUI.gameObject.SetActive(false);

            // Invoke Events
            OnStateChanged?.Invoke();
        }

        public void HoldItem(AbstractCookingItem cookingItem) // DONE
        {
            if (!IsMinigameOn) return;

            CheckItemValidation(cookingItem);
        }

        private void CheckItemValidation(AbstractCookingItem cookingItem) // DONE
        {
            // Invalid Items
            if (cookingItem == null) return; // Null

            if (cookingItem.GetItemData() != nextItemToAdd) // Wrong Item
            {
                WrongCookingItem();
                return;
            }

            // Valid Item
            CalculateNextStep(cookingItem); 
        }

        private void WrongCookingItem() // DONE ?
        {
            // Audio and Dialogue
            AudioManager.Instance.PlaySFX("Mission Fail");

            if (wrongIngredientDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(wrongIngredientDialogue);
            }
        }

        private void CalculateNextStep(AbstractCookingItem cookingItem) // DONE
        {
            PickUpItem(cookingItem);

            recipeIndex++;
            
            if (recipeIndex < cookingRecipe.Recipe.Count) 
            {
                nextItemToAdd = cookingRecipe.Recipe[recipeIndex]; // Advance to next item
            }
            else
            {
                RecipeCompleted();
            }

            Debug.Log("Next: " + (recipeIndex < cookingRecipe.Recipe.Count ? nextItemToAdd.Name : "None"));
        }

        private void PickUpItem(AbstractCookingItem cookingItem) // DONE
        {
            currentHeldItem = cookingItem;

            // Set up UI
            cookingUI.SetHeldItem(cookingItem);

            // Audio
            AudioManager.Instance.PlaySFX("Pick Up");
        }

        private void RecipeCompleted() // TIDY UP
        {
            Debug.Log("Complete!");
            
            // nextItemToAdd = null; // RESET VARIABLES

            EndCooking();
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