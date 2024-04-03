using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] Ingredient ingredient;
    SpriteRenderer spriteRenderer;
    string _name;

    void Start()
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
