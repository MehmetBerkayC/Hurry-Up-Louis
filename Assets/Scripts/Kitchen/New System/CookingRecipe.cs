using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Data
{
    [CreateAssetMenu(menuName ="Cooking Minigame/Recipe", fileName ="Cooking Recipe")]
    public class CookingRecipe : ScriptableObject
    {
        public List<CookingItemData> Recipe;
    }
}
