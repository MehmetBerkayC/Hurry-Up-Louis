using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRadius = 3;

    private Collider2D[] _currentColliders;

    private void Update() // Check interactable collisions when pressed button
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentColliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);

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
}