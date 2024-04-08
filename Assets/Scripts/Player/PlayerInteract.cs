using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRadius = 3;
    [SerializeField] private KeyCode keyToInterract = KeyCode.Space;

    private Collider2D[] _currentColliders;

    public bool IsInteractable;

    private void Awake()
    {
        IsInteractable = true;
    }

    private void Update() // Check interactable collisions when pressed button
    {
        if (IsInteractable && Input.GetKeyDown(keyToInterract))
        {
            _currentColliders = Physics2D.OverlapCircleAll(transform.position, interactRadius); // Later do the check before interaction (will not be performant) to implement interact visuals when near interactable

            if (_currentColliders.Length > 0)
            {
                foreach (Collider2D collider in _currentColliders)
                {
                    if (collider.TryGetComponent(out IInteractable interactable))
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }

    public IInteractable CheckInteractables()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable) && interactable.IsInteractable)
            {
                return interactable;
            }
        }
        return null;
    }

    public KeyCode GetInteractKey(){
        return keyToInterract;
    }
}