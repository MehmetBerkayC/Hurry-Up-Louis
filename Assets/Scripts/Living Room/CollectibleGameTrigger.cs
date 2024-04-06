using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGameTrigger : MonoBehaviour
{
    public static CollectibleGameTrigger Instance;

    public Note RelatedNote;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
