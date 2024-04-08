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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioManager.Instance.Play("Main Theme");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Rush Louis");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void LoadBadEndScreen()
    {
        SceneManager.LoadScene("Bad End Screen");
    }
    
    public void LoadGoodEndScreen()
    {
        SceneManager.LoadScene("Good End Screen");
    }
    
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
