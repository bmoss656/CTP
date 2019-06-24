using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sName;
    public PlayerControl pc;

    private int lastLevel;


    private void OnDisable()
    {
        lastLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LastLevel", lastLevel);
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Time.timeScale = 1;

    }

    public void LoadLastScene()
    {
        lastLevel = PlayerPrefs.GetInt("LastLevel");
        SceneManager.LoadScene(lastLevel, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void SceneFromMenu()
    {
        if (pc.type == PlayerControl.PlayerType.EMPTY)
        {
            SceneManager.LoadScene("Intro", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
        }
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
