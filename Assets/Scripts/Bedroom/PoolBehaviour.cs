using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBehaviour : MonoBehaviour
{
    public static PoolBehaviour Instance; // Will have only 1 soo....

    [SerializeField] private Collider2D obstacleCollision;
    [SerializeField] private Collider2D dialogueCollision;
    
    [SerializeField] private float playerSpeedWhileSwimming = 2f;

    public Note RelatedNote;

    public static bool IsPassable;
    private PlayerController _player;

    private float _defaultPlayerSpeed;

    private void Awake()
    {
        if (Instance == null) // If there will be more than 1 delete instance code and make it flexible
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
        if(obstacleCollision == null || dialogueCollision == null)
        {
            Collider2D[] collisions = GetComponentsInChildren<Collider2D>();
            obstacleCollision = collisions[0];
            dialogueCollision = collisions[1];
        }
    }

    public void BecomePassable()
    {
        Debug.Log("Passable");

        IsPassable = true;

        obstacleCollision.isTrigger = true; // Swimmable
        dialogueCollision.enabled = false;
    }

    public void BecomeImpassable()
    {
        Debug.Log("Impassable");

        IsPassable = false;

        obstacleCollision.isTrigger = false; // Not Swimmable
        dialogueCollision.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out _player))
        {
            if (_player) // Player Present
            {
                _defaultPlayerSpeed = _player.GetPlayerSpeed();
                _player.SetPlayerSpeed(playerSpeedWhileSwimming);
                AudioManager.Instance.PlaySFX("Swim");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_player) // Probably don't even need to do this
        {
            _player.SetPlayerSpeed(_defaultPlayerSpeed);
            _player = null;
            AudioManager.Instance.StopSFX("Swim");
        }
    }
}