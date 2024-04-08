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
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] DoorBehaviour door;

    public TriggerConnections Connection;
    
    private void Start()
    {
        TryGetComponent(out trigger);   
        TryGetComponent(out sprite);
        sprite.enabled = false;

        if (!triggerActiveByDefault)
        {
            DeactivateTrigger();
        }
    }

    public void ActivateTrigger()
    {
        trigger.enabled = true;
        // Debug
        //sprite.enabled = true;
    }

    public void DeactivateTrigger()
    {
        trigger.enabled = false;
        // Debug
        //sprite.enabled = false;
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

        GameManager.Instance.UpdateGameState(state);
        DeactivateTrigger();
    }
}
