using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject bathroomTrigger;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        OpenDoor();    
    }

    public void OpenDoor()
    {
        animator.SetBool("IsOpen", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("IsOpen", false);
    }

    public void DestroyTrigger()
    {
        Destroy(bathroomTrigger);
    }
}
