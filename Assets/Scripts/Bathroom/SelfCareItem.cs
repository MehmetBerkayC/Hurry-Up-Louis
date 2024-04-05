using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Bathroom Minigame/Self Care Item", fileName ="New Self Care Item")]
public class SelfCareItem : ScriptableObject
{
    public Sprite Icon;
    public string ItemName;
    public int ItemValue;
}
