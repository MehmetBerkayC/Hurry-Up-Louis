using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    [SerializeField] Ingredient _currentIngredient;
    [SerializeField] private List<Ingredient> ProgressList = new List<Ingredient>();

    int[] _checkpoint = { 0, 1, 6 };

    int k = 0;
    int temp = 0;

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

    private void Update()
    {
        //CompareList();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        UnityEngine.Debug.Log("Envantere Eklendi " + ingredient.name);
        _currentIngredient = ingredient;
        // Yeni bir malzeme eklendiğinde progres kontrolü yapılmalı
        CompareList();
    }


    public bool CompareList() //TODO son progresi eklerken bir sorun olusuyor onun disinda gorunurde sorun yok calisiyor
    {
        if(_currentIngredient == ProgressList[k])
        {
            if (k == ProgressList.Count - 1)
            {
                UnityEngine.Debug.Log("Mini game ends");
                return true;
            }

            UnityEngine.Debug.Log("Dogru malzeme devam edin.");
            UnityEngine.Debug.Log("Bir sonraki malzeme: " + ProgressList[k + 1]);

            foreach (int j in _checkpoint)
            {
                if (j == k)
                {
                    temp = j;
                }
            }
            UnityEngine.Debug.Log("k degeri: " + k + "Count degeri: " + ProgressList.Count);

            k++;

            return true;
        }
        else
        {
            UnityEngine.Debug.Log("Yanlis malzeme secimi yaptiniz." + "Bir sonraki malzeme: " + ProgressList[temp]);

            UnityEngine.Debug.Log("son checkpoint: " + temp);
            k = temp;
            return false;
        }   
    }




    //public bool CompareList()
    //{
    //    for (int i = k; i < ProgressList.Count -1; i++)
    //    {
    //        // listeyi sirayla kontrol et
    //        if (ProgressList[i].name == _currentIngredient.name)
    //        {
    //            //buraya girerse sorun yok devam et 
    //            UnityEngine.Debug.Log("Bir sonraki malzeme: " + ProgressList[i + 1]);

    //            //yeni checkpointe gelindiyse onu ayarla
    //            foreach (int j in _checkpoint)
    //            {
    //                if (j == i)
    //                {
    //                    temp = j;
    //                }
    //            }
    //            UnityEngine.Debug.Log("i nin suanki degeri: " + i);

    //            return true;
    //        }
    //        else
    //        {
    //            UnityEngine.Debug.Log("tarife uyulmadi");
    //        }
    //    }
    //    // eger buraya gelirse yanlis islem yapildi checkpoint donusu
    //    k = temp;
    //    return false;
    //}
}
