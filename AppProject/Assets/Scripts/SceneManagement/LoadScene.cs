using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sName;
    private PlayerControl pc;

    private int lastLevel;

    void Start()
    {
        pc = PlayerControl.instance;
        /*Setting fixed android resolution for windows player, 1280 x 2048 is best, 
        but not all screens will have that resolution so has been scaled down, uses
        10 x 16 aspect ratio*/
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(640, 1024, true);
        }
    }

    private void OnDisable()
    {
        //Basic saving of last scene for returning from shop
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
        //Checking to see if first time in app
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
