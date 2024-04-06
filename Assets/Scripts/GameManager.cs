using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Bedroom, // add triggers to the rooms to trigger objectives -> triggers work only once
    Bathroom,
    Decide,
}


public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] Dialogue[] dialogues;
    public static GameManager Instance;

    public static GameState State; 

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
        Debug.Log("Current State is:" + State.ToString());
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
            case GameState.None:
                break;
            case GameState.Bedroom:
                StartBedroomSequence();
                break;
            case GameState.Bathroom:
                StartBathroomSequence();
                break;
            case GameState.Decide:
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    // Fuction
    /// Character wakes up, pulls up a dialogue, after then player mobility is unlocked(dialoge system)
    public void StartBedroomSequence()
    {
        DialogueManager.Instance.StartDialogue(dialogues[0]); // Wake up dialogue
        
        // Set up objectives if needed

        // Set Total Game Time Async/Coroutine (Game officially starts) // Coroutine uses the main thread
    }

    public void StartBathroomSequence()
    {
        // TODO: guide player
    }
}
