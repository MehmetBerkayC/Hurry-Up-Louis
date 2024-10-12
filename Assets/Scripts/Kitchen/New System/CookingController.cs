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
        [SerializeField] private AbstractCookingItem _currentHeldItem;
        [SerializeField] private CookingItemData _nextItemToAdd;

        [Header("Note & Dialogues")]
        [SerializeField] NoteTrigger kitchenRecipeNote;
        [SerializeField] Dialogue wrongIngredientDialogue;
        [SerializeField] Dialogue missionSuccessfulDialogue;

        private int _recipeIndex = -1; // Starts from 0 as first item check

        public static CookingController Instance;

        // Instead of doing something like this, please remember/have time to have a minigame bool in game manager
        public static bool IsMinigameOn = false;

        public AbstractCookingItem GetCurrentItem() => _currentHeldItem;

        public static Action OnStateChanged;
        public static Action OnItemPickedUp;

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
            _recipeIndex = -1;
            IsMinigameOn = true;
            
            // Decide the first item
            CalculateFirstItem();

            // Invoke Events
            OnStateChanged?.Invoke();
        }

        private void CalculateFirstItem() // DONE
        {
            _recipeIndex++;
            _nextItemToAdd = cookingRecipe.Recipe[_recipeIndex]; // Advance to next item
            Debug.Log("Next: " + (_recipeIndex < cookingRecipe.Recipe.Count ? _nextItemToAdd.Name : "None"));
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

            if (cookingItem.GetItemData() != _nextItemToAdd) // Wrong Item
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

            _recipeIndex++;
            
            if (_recipeIndex < cookingRecipe.Recipe.Count) 
            {
                _nextItemToAdd = cookingRecipe.Recipe[_recipeIndex]; // Advance to next item
            }
            else
            {
                RecipeCompleted();
            }

            Debug.Log("Next: " + (_recipeIndex < cookingRecipe.Recipe.Count ? _nextItemToAdd.Name : "None"));
        }

        private void PickUpItem(AbstractCookingItem cookingItem) // DONE
        {
            _currentHeldItem = cookingItem;

            // Set up UI
            cookingUI.SetHeldItem(cookingItem);

            // Audio
            AudioManager.Instance.PlaySFX("Pick Up");

            // Events
            OnItemPickedUp?.Invoke();
        }

        private void RecipeCompleted() // DONE
        {
            Debug.Log("Cooking Complete!");

            ResetVariables();

            EndCooking();
        }

        private void ResetVariables() // DONE
        {
            _recipeIndex = -1;
            _currentHeldItem = null;
            _nextItemToAdd = null;
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
    }
}