using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDecisionTrigger : MonoBehaviour
{
    [SerializeField] GameStateTrigger[] triggers;
    [SerializeField] Collider2D decisionTrigger;
    [SerializeField] SpriteRenderer spriteRenderer;

    public static ActivateDecisionTrigger Instance;

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

    private void Start()
    {
        TryGetComponent(out decisionTrigger);
        TryGetComponent(out spriteRenderer);

        DeactivateTrigger();
    }

    public void ActivateTrigger()
    {
        decisionTrigger.enabled = true;
        // Debug
        spriteRenderer.enabled = true;
    }

    public void DeactivateTrigger()
    {
        decisionTrigger.enabled = false;
        // Debug
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateGameTriggers();
        DeactivateTrigger();
    }

    private void ActivateGameTriggers()
    {
        foreach (var trigger in triggers)
        {
            trigger.ActivateTrigger();
        }
    }
}
