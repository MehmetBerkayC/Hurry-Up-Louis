using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cooking;
using Cooking.Control;

public class KitchenGameTrigger : MonoBehaviour
{
    [SerializeField] NoteTrigger recipeNote;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        recipeNote.BecomeInteractable(true);
        CookingController.Instance.StartCooking();
        //RecipeManager.Instance.StartMinigame();
        Destroy(this.gameObject);
    }
}
