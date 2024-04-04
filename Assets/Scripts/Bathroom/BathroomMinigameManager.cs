using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BathroomMinigameManager : MonoBehaviour
{
    [SerializeField] private SelfCareItemDatabase itemDatabase;

    public static BathroomMinigameManager Instance;

    [Header("References")]
    [SerializeField] private GameObject miniGameUI;

    [SerializeField] private Transform[] goodSlots;
    [SerializeField] private Transform[] badSlots;
    [SerializeField] private Transform currentValue;
    [SerializeField] private Transform targetValue;

    [Header("Info Items")]
    [SerializeField] private Transform itemDisplayParent;

    [SerializeField] private GameObject itemHolderPrefab;

    [Header("Clickable Items")]
    [SerializeField] private Transform clickableItemsParent;

    [SerializeField] private GameObject clickableItemPrefab;

    private PlayerController _playerController;

    private bool _isMiniGameOn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this.gameObject); }
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out _playerController);
    }

    private void Update()
    {
        if (_isMiniGameOn)
        {
            _playerController.StopPlayerMovement(); // can make movement speed 0 instead of updating every frame but is similar anyways?
        }
    }

    public void StartMinigame()
    {
        _isMiniGameOn = true;

        // Set up the game
        SetUpUI();

        PopulateItems();
    }

    private void SetUpUI()
    {
        miniGameUI.SetActive(_isMiniGameOn);
    }

    private void PopulateItems()
    {
        foreach (SelfCareItem item in itemDatabase.Items)
        {
            // Item Information Display
            Instantiate(itemHolderPrefab, itemDisplayParent).TryGetComponent(out UISelfCareItemHolder holder);
            holder.careItem = item;
            holder.Initialize();

            // Selectable Items Display
            Instantiate(clickableItemPrefab, clickableItemsParent.transform).TryGetComponent(out UISelfCareItemHolder clickableHolder);
            clickableHolder.careItem = item;
            clickableHolder.Initialize();
        }
    }

    public void OnItemSelectedFromDisplay(SelfCareItem selfCareItem) // For Buttons
    {
        // May change
        if (selfCareItem.ItemValue == 0) return; // Should be a default value, so no item should have 0

        if (selfCareItem.ItemValue > 0) // Positive Good Negative Bad
        {
            FindEmptySlot(goodSlots, selfCareItem);
        }
        else
        {
            FindEmptySlot(badSlots, selfCareItem);
        }
    }

    private void FindEmptySlot(Array slots, SelfCareItem careItem)
    {
        foreach (Transform slot in slots) // Go through slots
        {
            slot.TryGetComponent(out GoodAndBadSlot slotInfo); // Get slot info

            if (slotInfo.GetItem() == null) // Check if already occupied
            {
                slotInfo.Initialize(careItem);
            }
        }
    }

    public void ResetGameBoardSlot(GoodAndBadSlot slot)
    {
        slot.Reset();
    }
}