using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    [SerializeField] private List<Ingredient> ingredientList = new List<Ingredient>();
    [SerializeField]private List<Ingredient> RecipeList = new List<Ingredient>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredientList.Add(ingredient);   
    }

    public bool CompareList()
    {


        return false;
    }
}
