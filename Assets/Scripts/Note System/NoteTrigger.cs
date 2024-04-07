using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] Note note;

    [SerializeField] bool _isInteractableByDefault;

    public bool IsInteractable { get; private set; } = false;
    
    private void Awake()
    {
        if (_isInteractableByDefault)
        {
            BecomeInteractable(true);
        }
    }

    public void BecomeInteractable(bool value)
    {
        IsInteractable = value;
    }

    public void Interact()
    {
        if (IsInteractable)
        {
            OpenNote();
        }
    }

    private void OpenNote()
    {
        NoteManager.Instance.OpenNote(note);
    }
}
