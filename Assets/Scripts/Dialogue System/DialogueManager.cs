using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialoquePanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialoqueBox;
    [SerializeField] Animator animator;
    public static DialogueManager Instance;

    Queue<string> dialoqueMessages;

    [SerializeField] PlayerController playerController;

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

    public void StartDialogue(Dialogue dialogue)
    {
        // Find a better way to pause game when in dialogue
        playerController.StopPlayerMovement();
        playerController.enabled = false;

        //dialoquePanel.SetActive(true);

        animator.SetBool("IsOpen", true);

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

        string sentence = dialoqueMessages.Dequeue();
        
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
        // Debug.Log("Conversation ended!");
        animator.SetBool("IsOpen", false);
        //dialoquePanel.SetActive(false); // change this if panel closes before animation
        playerController.enabled = true;
    }
}
