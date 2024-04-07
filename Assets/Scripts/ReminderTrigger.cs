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
    public bool ActivatedOnceAlready { get; private set; } = false;

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
        DeactivateTrigger();
    }

    public void ActivateTrigger()
    {
        CheckBothGamesDone();
        
        gameObject.SetActive(true);
        trigger.enabled = true;
        spriteRenderer.enabled = true;
    }
    
    public void DeactivateTrigger()
    {
        gameObject.SetActive(false);
        trigger.enabled = false;
        spriteRenderer.enabled = false;
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

        DeactivateTrigger();
    }

    public void SetKitchenAsDone()
    {
        IsKitchenDone = true;
    }

    public void CheckBothGamesDone()
    {
        if (ActivatedOnceAlready)
        {
            // Activate Livingroom Trigger
            GameManager.Instance.ActivateCorrectTrigger(TriggerConnections.LivingRoom);
            Destroy(gameObject);
        }
        else
        {
            ActivatedOnceAlready = true;
        }
    }
}