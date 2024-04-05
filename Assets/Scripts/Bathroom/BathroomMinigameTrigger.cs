using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomMinigameTrigger : MonoBehaviour
{
    [SerializeField] DoorBehaviour bathroomDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bathroomDoor.OpenDoor();
        bathroomDoor.DestroyTrigger();

        BathroomMinigameManager.Instance.StartMinigame();

        Destroy(this.gameObject);
    }
}
