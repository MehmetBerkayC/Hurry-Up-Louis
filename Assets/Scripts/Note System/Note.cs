using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Note System/Note", fileName = "New Note")]
public class Note : ScriptableObject
{
    public string Label;
 
    // Can change it later to look like a book (like dialogues)
    [TextArea(3, 16)] public string Notes; 
}