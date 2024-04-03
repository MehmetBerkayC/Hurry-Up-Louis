using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteHolder : MonoBehaviour
{
    public Note HeldNote;

    [SerializeField] TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = HeldNote.Label;
    }

    public void OnClick()
    {
        NoteManager.Instance.OpenNote(HeldNote);
    }
}
