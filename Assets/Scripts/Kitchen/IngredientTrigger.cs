using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient ingredient;

    public bool IsInteractable { get; set; }

    public void Interact()
    {
        PickUpIngredient();
    }

    private void PickUpIngredient()
    {
        RecipeManager.Instance.AddIngredient(ingredient);
    }
}