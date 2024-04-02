using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue System/Dialogue", fileName ="New Dialogue")]
public class Dialogue : ScriptableObject
{
    public string CharacterName;

    [TextArea(3,10)]
    public string[] Sentences;
}
