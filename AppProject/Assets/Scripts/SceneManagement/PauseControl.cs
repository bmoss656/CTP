using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to pause app
public class PauseControl : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject pauseCanvas;

	public void PauseApp()
    {
        Time.timeScale = 0;
        mainCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void UnpauseApp()
    {
        Time.timeScale = 1;
        mainCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    //Time needs to be paused at certain points without canva's being activated
    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void UnpauseTime()
    {
        Time.timeScale = 1;
    }

}
