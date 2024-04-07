using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    public static event Action OnMinigameStart;

    public static event Action OnMinigameEnd;

    [SerializeField] private IngredientTrigger previousIngredientTrigger;
    [SerializeField] private IngredientTrigger currentIngredientTrigger;

    private int _listIndex = -1;

    [SerializeField] private IngredientTrigger bowl;
    [SerializeField] private IngredientTrigger pan;
    [SerializeField] private IngredientTrigger stove;
    [SerializeField] private IngredientTrigger table;
    [SerializeField] private IngredientTrigger chair;

    [SerializeField] private List<IngredientTrigger> recipeOrder = new List<IngredientTrigger>();

    [SerializeField] private NoteTrigger kitchenRecipe;

    [SerializeField] private int[] bowlIndexes; 
    [SerializeField] private int[] panIndexes; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartMinigame()
    {
        Debug.Log("Kitchen Game Start");
        OnMinigameStart?.Invoke();

        table.Deactivate();
        chair.Deactivate();
        stove.Deactivate();
    }

    private void GameCompleted()
    {
        Debug.Log("Kitchen Game End");

        Destroy(kitchenRecipe.gameObject);
        OnMinigameEnd?.Invoke();
    }

    public void AddIngredient(IngredientTrigger ingredientTrigger)
    {
        if (previousIngredientTrigger)
        {
            previousIngredientTrigger = currentIngredientTrigger; // remember last item
        }

        currentIngredientTrigger = ingredientTrigger; // get new item
        _listIndex++;

        // Deactivate if not a bowl or pan at specified indexes
        if (!(currentIngredientTrigger == bowl && bowlIndexes.Contains(_listIndex)) || 
            !(currentIngredientTrigger == pan && panIndexes.Contains(_listIndex)))
        {
            currentIngredientTrigger.Deactivate();
        }

        CheckProgressionOrder();
        Debug.Log(_listIndex);
    }

    public void CheckProgressionOrder()
    {
        if (currentIngredientTrigger == recipeOrder[_listIndex]) // Object is next on list
        {
            if (currentIngredientTrigger == recipeOrder[recipeOrder.Count - 1]) // Last Item
            {
                GameCompleted();
            }
            
            CheckScriptedEvents();
        }
        else // Wrong item in sequence
        {
            // Reset Last Item
            if (previousIngredientTrigger)
            {
                previousIngredientTrigger.Activate();
                previousIngredientTrigger = null; // reset item slot
            }

            // Reset Current Item
            currentIngredientTrigger.Activate(); 
            currentIngredientTrigger = null; // reset item slot

            _listIndex--;
        }
    }

    private void CheckScriptedEvents() // Sloppy but will work Scripted Events
    {
        if (currentIngredientTrigger == pan)
        {
            if (_listIndex == panIndexes[0]) // Deactivate Pan, Activate Stove
            {
                pan.Deactivate();

                stove.Activate();
            }
            else if (_listIndex == panIndexes[1]) // Deactivate Stove, Activate Pan and Move it to Stove
            {
                pan.Activate();
                
                pan.transform.position = stove.transform.GetChild(0).position;
                
                stove.Deactivate();
            }
            else if (_listIndex == panIndexes[2]) // Deactivate Pan and Activate Table
            {
                pan.Deactivate();

                table.Activate();
            }
        }
        else if (currentIngredientTrigger == table) // Move Pan, Deactivate Table, Activate Chair
        {
            if (_listIndex == panIndexes[3])
            {
                pan.Deactivate();
                pan.transform.position = table.transform.GetChild(0).position;

                table.Deactivate();

                chair.Activate();
            }
        }
        else if (currentIngredientTrigger == bowl) // Bowl only deactivates once
        {
            if (_listIndex == bowlIndexes[0])
            {
                bowl.Deactivate();
            }
        }
    }
}