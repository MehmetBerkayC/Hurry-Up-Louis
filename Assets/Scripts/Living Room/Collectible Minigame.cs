using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMinigameManager : MonoBehaviour
{
    public static event Action OnMinigameStart;
    
    public static CollectibleMinigameManager Instance;

    [SerializeField] List<Collectible> allCollectibles = new List<Collectible>();

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartMinigame()
    {
        OnMinigameStart?.Invoke(); // Notify all items to become active/interactable

    }
}
