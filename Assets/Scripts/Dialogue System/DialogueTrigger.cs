using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    // Add support for back-n-forth or multiple dialogues
    [SerializeField] Dialogue dialogue;

    public bool IsInteractable => true; // default

    public void Interact()
    {
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
