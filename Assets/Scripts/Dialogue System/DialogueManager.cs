using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] KeyCode keyToSeeNextLine = KeyCode.Tab;
    
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueBox;
    
    [SerializeField] Animator dialogueAnimator;
    
    public static DialogueManager Instance;

    Queue<string> dialogueMessages;

    private bool _isDialogueOpen;
    
    private string sentence;

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

        dialogueMessages = new Queue<string>();
    }

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_isDialogueOpen && Input.GetKeyDown(keyToSeeNextLine))
        {
            if (dialogueBox.text == sentence)
            {
                DisplayNextSentence();
            }
            else
            {
                StopAllCoroutines();
                dialogueBox.text = sentence;
            }
        }
    }

    public bool IsDialogOpen()
    {
        return _isDialogueOpen;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _isDialogueOpen = true;

        // Find a better way to pause game when in dialogue
        playerController.StopPlayerMovement();
        playerController.enabled = false;
        playerController.gameObject.TryGetComponent(out PlayerInteract playerInteract);
        playerInteract.IsInteractable = false;

        if (!dialoguePanel.activeInHierarchy) { // Safety
            dialoguePanel.SetActive(true);
        }

        dialogueAnimator.SetBool("IsOpen", true);

        // Debug.Log("Start conversation with" + dialogue.CharacterName);
        nameText.text = dialogue.CharacterName;

        dialogueMessages.Clear();

        // Is it a random dialogue pool?
        if (dialogue.IsRandomSentencePool)
        {
            string line = GetRandomLineFromDialogue(dialogue);
            dialogueMessages.Enqueue(line);
        }
        else
        {
            foreach (string sentence in dialogue.Sentences)
            {
                dialogueMessages.Enqueue(sentence);
            }
        }

        DisplayNextSentence(); // Display first message
    }

    public void DisplayNextSentence() // use from a button or key
    {
        if (dialogueMessages.Count == 0)
        {
            EndDialoque();
            return;
        }

        sentence = dialogueMessages.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
        //chatboxText.text = sentence;
        // Debug.Log($"{sentence}"); 
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueBox.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueBox.text += letter;
            yield return null;
        }
    }

    private void EndDialoque()
    {
        _isDialogueOpen = false;

        // Debug.Log("Conversation ended!");
        dialogueAnimator.SetBool("IsOpen", false);
        //dialoquePanel.SetActive(false); // change this if panel closes before animation
        playerController.enabled = true;
        playerController.gameObject.TryGetComponent(out PlayerInteract playerInteract);
        playerInteract.IsInteractable = true;

        if (GameManager.Instance.FreshStart)
        {
            GameManager.Instance.StartGameTimer();
            GameManager.Instance.FreshStart = false;
        }
    }


    public string GetRandomLineFromDialogue(Dialogue dialogue)
    {
        // Random Sentence
        return dialogue.Sentences[UnityEngine.Random.Range(0, dialogue.Sentences.Length)];
    }
}
