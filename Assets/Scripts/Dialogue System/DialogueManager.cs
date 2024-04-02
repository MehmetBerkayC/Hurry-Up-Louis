using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] KeyCode keyToSeeNextLine = KeyCode.Tab;
    
    [SerializeField] GameObject dialoquePanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialoqueBox;
    
    [SerializeField] Animator dialogueAnimator;
    
    public static DialogueManager Instance;

    Queue<string> dialoqueMessages;

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

        dialoqueMessages = new Queue<string>();
    }

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_isDialogueOpen && Input.GetKeyDown(keyToSeeNextLine))
        {
            if (dialoqueBox.text == sentence)
            {
                DisplayNextSentence();
            }
            else
            {
                StopAllCoroutines();
                dialoqueBox.text = sentence;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _isDialogueOpen = true;

        // Find a better way to pause game when in dialogue
        playerController.StopPlayerMovement();
        playerController.enabled = false;

        if (!dialoquePanel.activeInHierarchy) { // Safety
            dialoquePanel.SetActive(true);
        }

        dialogueAnimator.SetBool("IsOpen", true);

        // Debug.Log("Start conversation with" + dialogue.CharacterName);
        nameText.text = dialogue.CharacterName;

        dialoqueMessages.Clear();

        foreach (string sentence in dialogue.Sentences)
        {
            dialoqueMessages.Enqueue(sentence);
        }

        DisplayNextSentence(); // Display first message
    }

    public void DisplayNextSentence() // use from a button or key
    {
        if (dialoqueMessages.Count == 0)
        {
            EndDialoque();
            return;
        }

        sentence = dialoqueMessages.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
        //chatboxText.text = sentence;
        // Debug.Log($"{sentence}"); 
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialoqueBox.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialoqueBox.text += letter;
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
    }
}
