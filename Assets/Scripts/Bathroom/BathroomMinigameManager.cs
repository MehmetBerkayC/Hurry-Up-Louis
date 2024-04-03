using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BathroomMinigameManager : MonoBehaviour
{
    [SerializeField] private SelfCareItemDatabase itemDatabase;

    public static BathroomMinigameManager Instance;

    [SerializeField] private GameObject itemHolderPrefab;
    [SerializeField] private Transform itemDisplayParent;
    [SerializeField] private GameObject miniGameUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this.gameObject); }
    }

    public void StartMinigame()
    {
        // Set up the game

        PopulateItems();
    }

    private void PopulateItems()
    {
        foreach (SelfCareItem item in itemDatabase.Items)
        {
            Instantiate(itemHolderPrefab, itemDisplayParent).TryGetComponent(out UISelfCareItemHolder holder);
            holder.careItem = item;
            holder.Initialize();
        }
    }
}