using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] Note note;

    public bool IsInteractable => true;

    public void Interact()
    {
        OpenNote();
    }

    private void OpenNote()
    {
        NoteManager.Instance.OpenNote(note);
    }
}
