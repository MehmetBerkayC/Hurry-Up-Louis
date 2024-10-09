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

    [SerializeField] private GameObject endTrigger;

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
        AudioManager.Instance.PlaySFX("Pick Up 2");
        CheckProgress();
    }

    private void CheckProgress()
    {
        if (allCollectibles.Count == collectedCollectibles.Count)
        {
            // Dialog -> Gidebilirim Art�k (Ana Kap� Trigger� A��l�r)
            DialogueManager.Instance.StartDialogue(gameDialogues[1]);
            // Game Should End
            Debug.Log("Minigame Ends"); // -> Trigger Halledecek
            EndMinigame();
        }

        if (allCollectibles.Count - 1 == collectedCollectibles.Count) // Only 1 item remains
        {
            //Play Briefcase dialogue
            DialogueManager.Instance.StartDialogue(gameDialogues[0]);

            OnLastCollectible?.Invoke(); // Notify item to become active/interactable
        }
    }

    private void EndMinigame()
    {
        AudioManager.Instance.PlaySFX("Mission Success");

        endTrigger.SetActive(true);
        GameManager.Instance.ActivateCorrectTrigger(TriggerConnections.End);
    }
}
