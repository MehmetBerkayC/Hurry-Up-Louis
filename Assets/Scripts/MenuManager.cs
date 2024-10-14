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
        LoadMainMenu();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Rush Louis", LoadSceneMode.Single);
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlayMusic("Main Theme");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlayMusic("Start Menu");
    }
    
    public void LoadBadEndScreen()
    {
        SceneManager.LoadScene("Bad End Screen", LoadSceneMode.Single);
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlayMusic("Bad Ending");
    }
    
    public void LoadGoodEndScreen()
    {
        SceneManager.LoadScene("Good End Screen", LoadSceneMode.Single);
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlayMusic("Good Ending");
    }
    
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
