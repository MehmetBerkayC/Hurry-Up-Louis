using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    [SerializeField] private Ingredient currentIngredient;
    [SerializeField] private List<Ingredient> progressList = new List<Ingredient>();

    private int[] _checkpoint = { 0, 2, 6 };

    private int k = 0;
    private int temp = 0;

    [SerializeField] private GameObject bowl;
    [SerializeField] private GameObject pan;
    [SerializeField] private GameObject stove;
    [SerializeField] private GameObject table;

    [SerializeField] private List<GameObject> kitchenObject;

    [SerializeField] private NoteTrigger kitchenNote;

    public bool IsMinigameOn = false;

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
    }

    public void StartMinigame()
    {
        IsMinigameOn = true;
    }

    private void GameCompleted()
    {
        IsMinigameOn = false;

        AudioManager.Instance.Play("Mission Success");
        ReminderTrigger.Instance.SetKitchenAsDone();
        ReminderTrigger.Instance.ActivateTrigger();

        Destroy(kitchenNote.gameObject);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (IsMinigameOn)
        {
            currentIngredient = ingredient;
            CompareList();
        }
    }

    public bool CompareList()
    {
        if (currentIngredient == progressList[k])
        {
            SetObject(k);
            k++;

            if (k == progressList.Count - 1)
            {
                GameCompleted();
                return true;
            }

            foreach (int j in _checkpoint)
            {
                if (j == k)
                {
                    temp = j;
                }
            }

            AudioManager.Instance.Play("Pick Up");
            return true;
        }
        else // Wrong Item
        {
            SetObject(k);
            k = temp;
            AudioManager.Instance.Play("Mission Fail");
            return false;
        }
    }

    private void SetObject(int j)
    {
        if (j == 0 && currentIngredient == progressList[k]) 
        {
            pan.SetActive(false);
        }
        else if (j == 1 && currentIngredient == progressList[k]) 
        {
            pan.SetActive(true);
            pan.transform.position = stove.transform.GetChild(0).position;

            stove.SetActive(false);
        }
        else if (j == 12&& currentIngredient == progressList[k]) 
        {
            bowl.SetActive(false);
        }
        else if (j == 14 && currentIngredient == progressList[k]) 
        {
            pan.SetActive(false);
            table.SetActive(true);
        }
        else if(j == 15 && currentIngredient == progressList[k]) 
        {
            pan.SetActive(true);
            pan.transform.position = table.transform.GetChild(0).position;
        }


        if (currentIngredient == progressList[k])
        {
            foreach (GameObject t in kitchenObject)
            {
                IngredientTrigger ingredientTrigger = t.GetComponent<IngredientTrigger>();
                if (t.GetComponent<IngredientTrigger>().ingredient.name == currentIngredient.name)
                {
                    if(t==pan)
                    {
                        break;
                    }
                    else
                    {
                        t.SetActive(false);
                    }
                }
            }
        }
        else
        {
            for(int i = k; i<progressList.Count; i++)
            {
                foreach(GameObject t in kitchenObject)
                {
                    if (t.GetComponent<IngredientTrigger>().ingredient.name == progressList[i].name)
                    {
                        t.SetActive(true);
                    }
                }
            }
        }
    }
}