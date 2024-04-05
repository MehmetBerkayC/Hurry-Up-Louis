using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient ingredient;
    private SpriteRenderer spriteRenderer;
    public string _name;

    private void Start()
    {
        _name = ingredient.name;
        //spriteRenderer.sprite = ingredient.sprite;
    }

    public void Interact()
    {
        PickUpIngredient();
    }

    private void PickUpIngredient()
    {
        RecipeManager.Instance.AddIngredient(ingredient);
    }
}