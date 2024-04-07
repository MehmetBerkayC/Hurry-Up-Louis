using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Start Kitchen Minigame
        Destroy(this.gameObject);
    }
}
