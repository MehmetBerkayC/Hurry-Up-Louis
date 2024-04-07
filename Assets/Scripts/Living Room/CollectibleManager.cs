using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static event Action OnMinigameStart;
    public static event Action OnLastCollectible;
    
    public static CollectibleManager Instance;

    [SerializeField]
    private List<Collectible> allCollectibles = new List<Collectible>();
    private List<Collectible> collectedCollectibles = new List<Collectible>();

    [SerializeField] 
    private List<Dialogue> gameDialogues = new List<Dialogue>();

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
        if (allCollectibles.Count == collectedCollectibles.Count)
        {
            // Dialog -> Gidebilirim Artýk (Ana Kapý Triggerý Açýlýr)
            DialogueManager.Instance.StartDialogue(gameDialogues[1]);
            // Game Should End
            Debug.Log("Game Ends"); // -> Trigger Halledecek
        }

        if (allCollectibles.Count - 1 == collectedCollectibles.Count) // Only 1 item remains
        {
            //Play Briefcase dialogue
            DialogueManager.Instance.StartDialogue(gameDialogues[0]);

            OnLastCollectible?.Invoke(); // Notify item to become active/interactable
        }
    }
}
