using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] Dialogue[] bedroomDialogues;
    [SerializeField] Dialogue[] bathroomDialogues;
    [SerializeField] Dialogue[] kitchenDialogues;
    [SerializeField] Dialogue[] livingRoomDialogues;
    [SerializeField] Dialogue[] endGameDialogues;

    [SerializeField] List<GameStateTrigger> triggersInLevel = new List<GameStateTrigger>();
    public static GameManager Instance;

    public GameState State;

    private bool _IsEndingGood;

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

    private void Start()
    {
        // UpdateGameState(GameState.Bedroom); // Make this apply after game start 
        // maybe through start game button
    }

    bool once = true;
    private void Update() // To simulate the existing of a start button
    {
        if (once)
        {
            UpdateGameState(GameState.Bedroom);
            once = false;
        }
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
                break;
            case GameState.BadEnding:
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
        // TODO: guide player
        DialogueManager.Instance.StartDialogue(livingRoomDialogues[0]); // Finished previous obj
    }

    public void StartGoodEndingSequence()
    {
        // TODO: guide player
        DialogueManager.Instance.StartDialogue(endGameDialogues[0]); // Good End
        // Rush to the door? -> trigger

        // Good End Screen
    }
    
    public void StartBadEndingSequence()
    {
        // TODO: guide player
        DialogueManager.Instance.StartDialogue(endGameDialogues[1]); // Bad End
        // Got Late

        // Bad End Screen
    }
}
