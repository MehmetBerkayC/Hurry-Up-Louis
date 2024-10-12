using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionBubble : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;

    [SerializeField] private GameObject interactionPopUp;
    [SerializeField] private TextMeshProUGUI interactionPopUpText;


    private void Start()
    {
        if(playerInteract == null)
        {
            playerInteract = FindObjectOfType<PlayerInteract>();
        }
    }

    private void Update()
    {
        if (playerInteract.CheckInteractables() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        interactionPopUp.gameObject.SetActive(true);
        interactionPopUpText.text = playerInteract.GetInteractKey().ToString();
    }

    private void Hide()
    {
        interactionPopUp.gameObject.SetActive(false);
        interactionPopUpText.text = playerInteract.GetInteractKey().ToString();
    }
}