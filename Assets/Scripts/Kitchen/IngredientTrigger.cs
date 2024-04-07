using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTrigger : MonoBehaviour, IInteractable
{
    public Ingredient ingredient;
    private SpriteRenderer spriteRenderer;

    public bool IsInteractable { get; private set; } = false;

    private void Start()
    {
        RecipeManager.OnMinigameStart += Activate;
        RecipeManager.OnMinigameEnd += Deactivate;

        TryGetComponent(out spriteRenderer);
        //spriteRenderer.sprite = ingredient.sprite;
    }

    public void Activate()
    {
        IsInteractable = true;
        spriteRenderer.enabled = true;
    }
    
    public void Deactivate()
    {
        IsInteractable = false;
        spriteRenderer.enabled = false;
    }

    public void Interact()
    {
        if (IsInteractable)
        {
            PickUpIngredient();
        }
    }

    private void PickUpIngredient()
    {
        RecipeManager.Instance.AddIngredient(this);
    }

    private void OnDestroy()
    {
        RecipeManager.OnMinigameStart -= Activate;
        RecipeManager.OnMinigameEnd -= Deactivate;
    }
}