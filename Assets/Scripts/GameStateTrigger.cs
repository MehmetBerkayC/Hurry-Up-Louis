using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerConnections
{
    Bathroom,
    Kitchen,
    LivingRoom,
    End
}

public class GameStateTrigger : MonoBehaviour
{
    [SerializeField] bool triggerActiveByDefault;
    [SerializeField] private GameState state;

    [SerializeField] Collider2D trigger;
    [SerializeField] DoorBehaviour door;

    public TriggerConnections Connection;
    
    private void Start()
    {
        TryGetComponent(out trigger);   
        
        if (!triggerActiveByDefault)
        {
            trigger.enabled = false;
        }
    }

    public void ActivateTrigger()
    {
        trigger.enabled = true;
    }
    
    public void DeactivateTrigger()
    {
        trigger.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomFunctionality();
    }

    private void RoomFunctionality()
    {
        // Can close door (this trigger can be attached to a door/ control a door)
        if (door != null)
        {
            door.CloseDoor();
        }

        DeactivateTrigger();

        GameManager.Instance.UpdateGameState(state);
    }
}
