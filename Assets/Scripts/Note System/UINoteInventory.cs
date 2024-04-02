using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoteInventory : MonoBehaviour
{
    public static UINoteInventory Instance;

    [SerializeField] private GameObject notePrefabUI;
    [SerializeField] private Transform parentNotePanel;

    private List<Note> notes = new List<Note>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddNote(Note note)
    {
        if (!notes.Contains(note)) // New note
        {
            notes.Add(note);

            // Make new note gameObject and attach the note
            NoteHolder noteHolder = Instantiate(notePrefabUI, parentNotePanel).GetComponent<NoteHolder>();
            noteHolder.HeldNote = note;
        }
    }

    public bool IsNoteInList(Note note)
    {
        if (notes.Contains(note))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveNote(Note note)
    {
        // Loop through children
        foreach (NoteHolder child in parentNotePanel)
        {
            if (child.HeldNote == note)
            {
                Destroy(child.gameObject);
                notes.Remove(note);
                return true; // Remove successful
            }
        }

        return false; // Didn't remove anything
    }
}