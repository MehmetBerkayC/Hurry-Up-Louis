using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient System/Ingredient", fileName = "New Ingredient")]
public class Ingredient : ScriptableObject
{
    public string IngredientName;
    public Sprite sprite;
}