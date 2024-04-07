using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameTrigger : MonoBehaviour
{
    [SerializeField] NoteTrigger kitchenRecipe;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        kitchenRecipe.BecomeInteractable(true);
        RecipeManager.Instance.StartMinigame();
        Destroy(this.gameObject);
    }
}
