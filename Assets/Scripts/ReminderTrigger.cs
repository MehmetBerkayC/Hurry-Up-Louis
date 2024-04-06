using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue[] reminderDialogues; // 0 bathroom 1 kitchen

    [SerializeField] private Collider2D trigger;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public static ReminderTrigger Instance;

    public bool IsKitchenDone { get; private set; } = false;

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
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out trigger);
        ActivateTrigger(false);
    }

    public void ActivateTrigger(bool value)
    {
        trigger.enabled = value;
        spriteRenderer.enabled = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsKitchenDone)
        {
            // Play Bathroom Reminder
            DialogueManager.Instance.StartDialogue(reminderDialogues[0]);
        }
        else
        {
            // Play Kitchen Reminder
            DialogueManager.Instance.StartDialogue(reminderDialogues[1]);
        }

        ActivateTrigger(false);
    }

    public void SetIsKitchenDone(bool value)
    {
        IsKitchenDone = value;
    }
}