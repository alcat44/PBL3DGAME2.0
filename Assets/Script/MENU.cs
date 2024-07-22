using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU : MonoBehaviour
{
    public GameObject loadingscreen;
    public string sceneName;

    public void playGame()
    {
        loadingscreen.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
    public void quitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
