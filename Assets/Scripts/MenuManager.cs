using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //private void Start()
    //{
    //    AudioManager.Instance.PlayMusic("Main Theme");
    //}

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Rush Louis", LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
    
    public void LoadBadEndScreen()
    {
        SceneManager.LoadScene("Bad End Screen", LoadSceneMode.Single);
    }
    
    public void LoadGoodEndScreen()
    {
        SceneManager.LoadScene("Good End Screen", LoadSceneMode.Single);
    }
    
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
