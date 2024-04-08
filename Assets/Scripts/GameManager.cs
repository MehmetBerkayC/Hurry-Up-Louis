using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// add triggers to the rooms to trigger objectives -> triggers work only once
public enum GameState
{
    None,
    Bedroom, 
    Bathroom,
    Kitchen,
    LivingRoom,
    GoodEnding, 
    BadEnding, 
}


public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChanged;

    [Header("Bedroom")]
    [SerializeField] Dialogue[] bedroomDialogues;
    
    [Header("Bathroom")]
    [SerializeField] Dialogue[] bathroomDialogues;
    
    [Header("Kitchen")]
    [SerializeField] Dialogue[] kitchenDialogues;

    [Header("Living Room")]
    [SerializeField] NoteTrigger livingRoomNote;
    [SerializeField] Dialogue[] livingRoomDialogues;

    [Header("End Game Dialogues")]
    [SerializeField] Dialogue[] endGameDialogues;

    [SerializeField] List<GameStateTrigger> triggersInLevel = new List<GameStateTrigger>();
    
    public static GameManager Instance;

    public static GameState State;

    private bool _IsEndingGood;

    public static bool BothMinigamesDone;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    bool once = true;

    private void Update()
    {
        if (once)
        {
            UpdateGameState(GameState.Bedroom);
            once = false;
        }
        /*UpdateGameState(GameState.Bedroom);*/ // Make this apply after game start 
        // maybe through start game button
         // First Room
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            default: // Goes to the first condition below it
            case GameState.None: // Do nothing
                break;
            case GameState.Bedroom:
                StartBedroomSequence();
                break;
            case GameState.Bathroom:
                StartBathroomSequence();
                break;
            case GameState.Kitchen:
                StartKitchenSequence();
                break;
            case GameState.LivingRoom:
                StartLivingRoomSequence();
                break;
            case GameState.GoodEnding:
                StartGoodEndingSequence();
                break;
            case GameState.BadEnding:
                StartBadEndingSequence();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void ActivateCorrectTrigger(TriggerConnections connection)
    {
        foreach (var trigger in triggersInLevel)
        {
            if (trigger.Connection == connection)
            {
                trigger.ActivateTrigger();
            }
        }
    }

    // Set Total Game Time Async/Coroutine (Game officially starts) // Coroutine uses the main thread
    // End the game depending on game time (probably will use Update())
    // -> dont use coroutines use time.time + gametime and update upon that
    private void CheckGameTime() 
    {
        // start timer function (once)
        // track game time
    }

    /// Character wakes up, pulls up a dialogue, after then player mobility is unlocked(dialoge system)
    public void StartBedroomSequence()
    {
        // Fade IN
        AudioManager.Instance.Play("Alarm");
        DialogueManager.Instance.StartDialogue(bedroomDialogues[0]); // Wake up dialogue
        
        // Set up objectives if needed
    }

    public void StartBathroomSequence()
    {
        // TODO: guide player
        DialogueManager.Instance.StartDialogue(bathroomDialogues[0]); // Walks through door
    } 
    
    public void StartKitchenSequence()
    {
        // TODO: guide player
        DialogueManager.Instance.StartDialogue(kitchenDialogues[0]); // Walks through door
    } 
    
    public void StartLivingRoomSequence()
    {
        // Make Note Interactable
        livingRoomNote.BecomeInteractable(true);

        // TODO: guide player
        DialogueManager.Instance.StartDialogue(livingRoomDialogues[0]); // Finished previous obj
    }

    public void StartGoodEndingSequence()
    {
        // TODO: guide player
        DialogueManager.Instance.StartDialogue(endGameDialogues[0]); // Good End
        // Fade OUT
        // Good End Screen
        SceneManager.LoadScene("End Screen");
    }
    
    public void StartBadEndingSequence()
    {
        // TODO: guide player
        DialogueManager.Instance.StartDialogue(endGameDialogues[1]); // Bad End
        // Got Late
        // Fade OUT
        // Bad End Screen
        SceneManager.LoadScene("Game Over");
    }
}
