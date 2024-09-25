using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomMinigameTrigger : MonoBehaviour
{
    private void Start()
    {
        BathroomMinigameManager.OnMinigameComplete += DestroyTrigger;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BathroomMinigameManager.Instance.StartMinigame();
    }

    public void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
