using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTrigger : MonoBehaviour, IInteractable
{
    public Ingredient ingredient;
    private SpriteRenderer spriteRenderer;

    bool _isInteractable = true;

    public bool IsInteractable => _isInteractable;


    private void Start()
    {
        TryGetComponent(out spriteRenderer);
        //spriteRenderer.sprite = ingredient.sprite;
    }
    public void SetInteractable(bool value)
    {
        _isInteractable = value;
    }

    public void Interact()
    {
        if (RecipeManager.Instance.IsMinigameOn)
        {
            PickUpIngredient();
        }
    }

    private void PickUpIngredient()
    {
        RecipeManager.Instance.AddIngredient(ingredient);
    }
}