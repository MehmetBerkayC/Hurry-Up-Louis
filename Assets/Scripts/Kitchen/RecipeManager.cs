using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    [SerializeField] private Ingredient _currentIngredient;
    [SerializeField] private List<Ingredient> ProgressList = new List<Ingredient>();

    private int[] _checkpoint = { 0, 2, 6 };

    private int k = 0;
    private int temp = 0;

    [SerializeField] private GameObject _bowl;
    [SerializeField] private GameObject _pan;
    [SerializeField] private GameObject _stove;
    [SerializeField] private GameObject _table;
    [SerializeField] private List<GameObject> _kitchenObject;

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

    public void AddIngredient(Ingredient ingredient)
    {
        _currentIngredient = ingredient;
        CompareList();
    }

    public bool CompareList()
    {
        if (_currentIngredient == ProgressList[k])
        {
            if (k == ProgressList.Count - 1)
            {
                UnityEngine.Debug.Log("Mini game ends");
                return true;
            }
            UnityEngine.Debug.Log("Bir sonraki malzeme: " + ProgressList[k + 1]);

            SetObject(k);
            k++;

            foreach (int j in _checkpoint)
            {
                if (j == k)
                {
                    temp = j;
                }
            }

            return true;
        }
        else
        {
            SetObject(k);
            UnityEngine.Debug.Log("Yanlis malzeme secimi yaptiniz." + "Bir sonraki malzeme: " + ProgressList[temp]);
            k = temp;
            return false;
        }
            
    }

    private void SetObject(int j)
    {
        if (j == 0 && _currentIngredient == ProgressList[k]) // pan kapat
        {
            _pan.SetActive(false);
        }
        else if (j == 1 && _currentIngredient == ProgressList[k]) // baslangicta acik olan stove u kapa pani hareket ettir
        {
            _pan.SetActive(true);
            _pan.transform.position = _stove.transform.GetChild(0).position;

            _stove.SetActive(false);
        }
        else if (j == 12&& _currentIngredient == ProgressList[k]) // pan kapat
        {
            _bowl.SetActive(false);
        }
        else if (j == 14 && _currentIngredient == ProgressList[k]) // pan kapat
        {
            _pan.SetActive(false);
            _table.SetActive(true);
        }
        else if(j == 15 && _currentIngredient == ProgressList[k]) // baslangicta sorun olmamasi icin kapali olan masayi ac
        {
            _pan.SetActive(true);
            _pan.transform.position = _table.transform.GetChild(0).position;
        }


        if (_currentIngredient == ProgressList[k])
        {
            foreach (GameObject t in _kitchenObject)
            {
                IngredientTrigger ingredientTrigger = t.GetComponent<IngredientTrigger>();
                if (t.GetComponent<IngredientTrigger>()._name == _currentIngredient.name)
                {
                    if(t==_pan)
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
            for(int i = k; i<ProgressList.Count; i++)
            {
                foreach(GameObject t in _kitchenObject)
                {
                    if (t.GetComponent<IngredientTrigger>()._name == ProgressList[i].name)
                    {
                        t.SetActive(true);
                    }
                }
            }
        }
    }
}