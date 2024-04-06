using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    private bool _IsInteractable;
    [SerializeField] bool isLastCollectible; // true if last item

    public bool IsInteractable => _IsInteractable;

    private void Start()
    {
        if (isLastCollectible) // Shouldn't have done this
        {
            CollectibleManager.OnLastCollectible += BecomeInteractable; ; // Sub
        }
        else
        {
            CollectibleManager.OnMinigameStart += BecomeInteractable; // Sub
        }
    }

    private void BecomeInteractable()
    {
        _IsInteractable = true;
    }

    public void Interact()
    {
        if (_IsInteractable)
        {
            Debug.Log("Collected");
            Collect();
        }
    }

    private void Collect()
    {
        CollectibleManager.Instance.Collect(this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (isLastCollectible)
        {
            CollectibleManager.OnMinigameStart -= BecomeInteractable; // Unsub
        }
        else
        {
            CollectibleManager.OnLastCollectible -= BecomeInteractable; // Unsub
        }
    }
}
