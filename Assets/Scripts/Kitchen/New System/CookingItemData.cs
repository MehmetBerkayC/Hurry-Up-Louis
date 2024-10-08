using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Data
{
    [CreateAssetMenu(menuName = "Cooking Minigame/Item Data", fileName ="Cooking Item")]
    public class CookingItemData : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
    }
}
