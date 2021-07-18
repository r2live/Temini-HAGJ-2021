using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    
    public void pause()
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(true);
    }

    public void unpause()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void quitPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
