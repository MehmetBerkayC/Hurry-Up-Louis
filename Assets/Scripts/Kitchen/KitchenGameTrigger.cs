using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameTrigger : MonoBehaviour
{
    [SerializeField] NoteTrigger recipeNote;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        recipeNote.BecomeInteractable(true);
        RecipeManager.Instance.StartMinigame();
        Destroy(this.gameObject);
    }
}
