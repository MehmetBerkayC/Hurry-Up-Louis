using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private Animator noteAnimator;
    [SerializeField] private KeyCode keyToReturn;
    [SerializeField] private Button returnButton;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI context;
    [SerializeField] private GameObject notePanel;

    public static NoteManager Instance;

    private List<Note> _currentNotes = new List<Note>();
    private bool _isNoteOpen;

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

    private void Update()
    {
        if (_isNoteOpen && Input.GetKeyDown(keyToReturn))
        {
            CloseNote();
        }
    }

    // Should work like the dialogue system
    public void OpenNote(Note note)
    {
        // Add note to the list if it is a new one
        if (!_currentNotes.Contains(note)) {
            _currentNotes.Add(note);
        }

        _isNoteOpen = true; // Open input listen

        // Play animation
        noteAnimator.SetBool("IsOpen", true);

        // Setup Buttons and Note Text
        var buttonText = returnButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = $"Press {keyToReturn} or click here to return!";

        label.text = note.Label;
        context.text = note.Notes;
    }

    public void CloseNote()
    {
        _isNoteOpen = false; // Close input listen

        // Play animation
        noteAnimator.SetBool("IsOpen", false);

        // Reset Text
        label.text = "";
        context.text = "";
    }

    public List<Note> GetNoteList()
    {
        return _currentNotes;
    }

    public bool RemoveNote(Note note) // For memory time implementation
    {
        return _currentNotes.Remove(note);
    }
}