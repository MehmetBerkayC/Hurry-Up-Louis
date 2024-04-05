using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static event Action OnMinigameStart;
    public static event Action OnLastCollectible;
    
    public static CollectibleManager Instance;

    [SerializeField]
    private List<Collectible> allCollectibles = new List<Collectible>();
    
    private List<Collectible> collectedCollectibles = new List<Collectible>();

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


    // This will start the game after the item list note is read
    public void StartMinigame()
    {
        
        OnMinigameStart?.Invoke(); // Notify items to become active/interactable
    }

    public void Collect(Collectible collectible)
    {
        collectedCollectibles.Add(collectible);
        CheckProgress();
    }

    private void CheckProgress()
    {
        if (allCollectibles.Count - 1 == collectedCollectibles.Count) // Only Briefcase Remains
        {
            //Play Briefcase dialogue
            OnLastCollectible?.Invoke(); // Notify item to become active/interactable
        }
    }
}
