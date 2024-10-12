using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    // Add support for back-n-forth or multiple dialogues
    [SerializeField] Dialogue dialogue;

    [field: SerializeField]
    public bool IsInteractable { get; set; }

    public void Interact()
    {
        AudioManager.Instance.PlaySFX("Pick Up");
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
