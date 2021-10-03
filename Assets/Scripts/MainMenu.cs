using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public SceneLoader sceneLoader;

    public void PlayGame()
    {
        sceneLoader.LoadNextScene();
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene(6);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
