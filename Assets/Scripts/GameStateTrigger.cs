using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateTrigger : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] DoorBehaviour door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomFunctionality();
    }
    private void RoomFunctionality()
    {
        GameManager.Instance.UpdateGameState(state);
        // Can close door (this trigger can be attached to a door/ control a door)
        door.CloseDoor();
    }
}
