using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public GameObject startScreen, helpScreen;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void HelpScreen()
    {
        startScreen.SetActive(false);
        helpScreen.SetActive(true);
    }

    public void BackToStartScreen()
    {
        startScreen.SetActive(true);
        helpScreen.SetActive(false);
    }
}
