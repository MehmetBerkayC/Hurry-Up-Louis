using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MenuButtonType
{
    MainMenuSceneLoader,
    GameSceneLoader,
    GoodEndingSceneLoader,
    BadEndingSceneLoader,
    QuitGame
}

public class MainMenuButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MenuButtonType buttonType;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (buttonType)
        {
            default:
            case MenuButtonType.MainMenuSceneLoader:
                MenuManager.Instance.LoadMainMenu();
                break;
            case MenuButtonType.GameSceneLoader:
                MenuManager.Instance.LoadGameScene();
                break;
            case MenuButtonType.GoodEndingSceneLoader:
                MenuManager.Instance.LoadGoodEndScreen();
                break;
            case MenuButtonType.BadEndingSceneLoader:
                MenuManager.Instance.LoadBadEndScreen();
                break;
            case MenuButtonType.QuitGame:
                MenuManager.Instance.QuitGame();
                break;
        }
    }
}
