using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue[] reminderDialogues; // 0 bathroom 1 kitchen

    [SerializeField]
    private Collider2D trigger;

    public static ReminderTrigger Instance;

    public bool IsInteractable { get; private set; }

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
    }
}