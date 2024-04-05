using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bathroom Minigame/Self Care Item Database", fileName = "New Self Care Item Database")]
public class SelfCareItemDatabase : ScriptableObject
{
    public SelfCareItem[] Items;
}
