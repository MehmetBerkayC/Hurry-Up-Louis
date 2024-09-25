using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BathroomMinigameManager : MonoBehaviour
{
    public static BathroomMinigameManager Instance;

    [Header("Database")]
    [SerializeField] private SelfCareItemDatabase itemDatabase;

    [Header("References")]
    [SerializeField] private GameObject miniGameUI;
    [SerializeField] private Collider2D gameTrigger;

    [SerializeField] private Transform[] goodSlots;
    [SerializeField] private Transform[] badSlots;
    [SerializeField] private TextMeshProUGUI currentValue;
    [SerializeField] private TextMeshProUGUI targetValue;
    
    [Header("UI Items")]
    [SerializeField] private Transform uiItemsParent;

    [SerializeField] private GameObject uiItemPrefab;

    private PlayerController _playerController;

    private bool _isMiniGameOn;
    private bool _targetAlreadyCalculated;

    private List<SelfCareItem> selectedItems = new List<SelfCareItem>();

    private int _targetScore;
    private int _currentScore;

    private float _defaultPlayerSpeed;
    [SerializeField] private Dialogue endMiniGameDialogue;

    [Header("Timer")]
    [SerializeField] private float triggerResetTime = 1.2f;
    private float timer = 0;

    public static event Action OnMinigameComplete;
    
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
        
        // Reset Text
        CalculateTargetValue();

        currentValue.text = 0.ToString();
    }

    private void StopPlayerMovement()
    {
        _defaultPlayerSpeed = _playerController.GetPlayerSpeed();
        _playerController.SetPlayerSpeed(0);
    }

    private void ResumePlayerMovement()
    {
        _playerController.SetPlayerSpeed(_defaultPlayerSpeed);
    }

    public void StartMinigame()
    {
        _isMiniGameOn = true;

        StopPlayerMovement();

        // Set up the game
        ToggleUI();

        gameTrigger.enabled = false;

        PopulateItems();
    }

    private void ToggleUI()
    {
        miniGameUI.SetActive(_isMiniGameOn);
    }

    private void PopulateItems()
    {
        foreach (SelfCareItem item in itemDatabase.Items)
        {
            // Selectable Items Display
            Instantiate(uiItemPrefab, uiItemsParent.transform).TryGetComponent(out UISelfCareItemHolder clickableHolder);
            clickableHolder.careItem = item;
            clickableHolder.Initialize();
        }
    }

    public void ItemSelectedFromDisplay(SelfCareItem selfCareItem) // For Buttons
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

        AudioManager.Instance.Play("Select");

        UpdateCurrentValue();
    }

    private void FindEmptySlot(Array slots, SelfCareItem careItem)
    {
        if (selectedItems.Contains(careItem)) // List includes item already
        {
            return;
        }

        foreach (Transform slot in slots) // Go through slots
        {
            slot.TryGetComponent(out GoodAndBadSlot slotInfo); // Get slot info

            if (slotInfo.GetItem() == null) // Check if already occupied
            {
                slotInfo.Initialize(careItem);
                selectedItems.Add(careItem);
                return;
            }
        }
    }

    private void UpdateCurrentValue()
    {
        int sum = 0;

        foreach (var item in selectedItems)
        {
            sum += item.ItemValue;
        }

        _currentScore = sum;
        currentValue.text = sum.ToString();
        
        if (_currentScore == _targetScore && selectedItems.Count == 5)
        {
            GameCompleted();
        }
    }

    private void GameCompleted()
    {
        _isMiniGameOn = false;

        AudioManager.Instance.Play("Mission Success");
        ToggleUI();

        ResumePlayerMovement();
        DialogueManager.Instance.StartDialogue(endMiniGameDialogue);

        ReminderTrigger.Instance.ActivateTrigger();

        OnMinigameComplete?.Invoke();
    }

    private void CalculateTargetValue()
    {
        if (!_targetAlreadyCalculated)
        {
            int positiveTargets = 0, negativeTargets = 0;

            List<SelfCareItem> targets = new List<SelfCareItem>();

            // A randomised target list using item database
            System.Random randomGen = new System.Random();

            bool targetNeeded = true;

            while (targetNeeded)
            {
                int index = randomGen.Next(0, itemDatabase.Items.Length);

                SelfCareItem item = itemDatabase.Items[index];

                if (!targets.Contains(item))
                {
                    if (positiveTargets < 3 && item.ItemValue > 0) // positive value needed
                    {
                        positiveTargets++;
                        targets.Add(itemDatabase.Items[index]);
                    }
                    else if (negativeTargets < 2 && item.ItemValue < 0) // negative value needed
                    {
                        negativeTargets++;
                        targets.Add(itemDatabase.Items[index]);
                    }
                }

                if (targets.Count == 5)
                {
                    targetNeeded = false;
                }
            }

            // Calculate target value
            _targetScore = 0;

            foreach (var item in targets)
            {
                _targetScore += item.ItemValue;
            }

            targetValue.text = _targetScore.ToString();

            _targetAlreadyCalculated = true;
        }
    }

    public bool RemoveItemFromList(SelfCareItem selfCareItem)
    {
        bool returnValue = selectedItems.Remove(selfCareItem);
        AudioManager.Instance.Play("Select");
        UpdateCurrentValue();
        return returnValue;
    }

    public void ExitMinigame()
    {
        _isMiniGameOn = false;

        ResumePlayerMovement();

        // Close the game
        ToggleUI();

        StartCoroutine(TriggerResetCountdown());
    }

    IEnumerator TriggerResetCountdown()
    {
        timer = 0;

        Debug.Log("Cooldown start!");
        while(timer < triggerResetTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"Cooldown ended at: {timer}!");

        gameTrigger.enabled = true;
    }
}