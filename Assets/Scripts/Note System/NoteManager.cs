using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    [SerializeField] private Animator noteAnimator;
    [SerializeField] private KeyCode keyToReturn;
    [SerializeField] private Button returnButton;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI context;
    [SerializeField] private GameObject notePanel;

    [SerializeField] private float timeToForget = 15;

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
        if (!_currentNotes.Contains(note))
        {
            _currentNotes.Add(note);

            // Add note to UI
            UINoteInventory.Instance.AddNote(note);

            //Check Interactable Objects
            CheckNoteDependencies(note, false);

            if (note.Temporary)
            {
                StartCoroutine(ForgetTimer(note));
            }
        }

        _isNoteOpen = true; // Open input listen

        // Play animation
        noteAnimator.SetBool("IsOpen", true);
        AudioManager.Instance.PlaySFX("Note Paper");

        // Setup Buttons and Note Text
        var buttonText = returnButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = $"Press {keyToReturn} or click here to return!";

        label.text = note.Label;
        context.text = note.Notes;

        StartCoroutine(CloseNote(note.ReadableTime));
    }

    public void CloseNote()
    {
        _isNoteOpen = false; // Close input listen

        // Play animation
        noteAnimator.SetBool("IsOpen", false);
        AudioManager.Instance.PlaySFX("Note Paper");

        // Reset Text
        label.text = "";
        context.text = "";
    }

    public IEnumerator CloseNote(int readableTime)
    {
        yield return new WaitForSeconds(readableTime);
        _isNoteOpen = false; // Close input listen

        // Play animation
        noteAnimator.SetBool("IsOpen", false);
        AudioManager.Instance.PlaySFX("Note Paper");

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
        CheckNoteDependencies(note, true); // true because note is being forgotten

        bool removeFromUI = UINoteInventory.Instance.RemoveNote(note); // probably will use this instead
        bool removeFromManager = _currentNotes.Remove(note);

        if (removeFromUI && removeFromManager)
        {
            return true; // Successfully removed
        }
        else if (!removeFromManager)
        {
            Debug.LogError("Couldn't remove note from manager!");
            return false;
        }
        else if (!removeFromUI)
        {
            Debug.LogError("Couldn't remove note from UI inventory");
            return false;
        }
        else
        {
            return false;
        }
    }

    private void CheckNoteDependencies(Note note, bool forget)
    {
        // This will suck ass as a system but should work for every gameobject dependency situation
        if (note == PoolBehaviour.Instance.RelatedNote)
        {
            if (forget) //  Just for the outcome of the memory check false -> new addition to memory, true -> removing from memory
            {
                PoolBehaviour.Instance.BecomeImpassable();
            }
            else
            {
                PoolBehaviour.Instance.BecomePassable();
                MinigameSplitTrigger.Instance.ActivateTrigger();
            }
        }
        else if (note == CollectibleGameTrigger.Instance.RelatedNote)
        {
            CollectibleManager.Instance.StartMinigame();
        } // Add more when needed
    }

    public IEnumerator ForgetTimer(Note note)
    {
        yield return new WaitForSeconds(timeToForget);

        RemoveNote(note);
    }
}