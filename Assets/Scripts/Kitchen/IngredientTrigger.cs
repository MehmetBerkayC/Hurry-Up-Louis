using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTrigger : MonoBehaviour, IInteractable
{
    public Ingredient ingredient;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        TryGetComponent(out spriteRenderer);
        //spriteRenderer.sprite = ingredient.sprite;
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
        AudioManager.Instance.Play("Pick Up");
        RecipeManager.Instance.AddIngredient(ingredient);
    }
}