using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayModeTests
{
    [UnityTest]
    public IEnumerator PlayButtonLaunchesGame()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.1f);
        int sceneTransitionedFrom = SceneManager.GetActiveScene().buildIndex;

        var menu = new GameObject().AddComponent<MainMenu>().GetComponent<MainMenu>();
        var button = GameObject.Find("/CanvasMenu/MainMenu/BoutonPlay").GetComponent<Button>();

        button.onClick.AddListener(menu.PlayGame);
        button.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        int sceneTransitionedTo = SceneManager.GetActiveScene().buildIndex;

        Assert.AreEqual(sceneTransitionedTo, sceneTransitionedFrom+2);
    }

    [UnityTest]
    public IEnumerator SettingsButtonLeadsToSettings()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.1f);
        int sceneTransitionedFrom = SceneManager.GetActiveScene().buildIndex;

        var menu = new GameObject().AddComponent<MainMenu>().GetComponent<MainMenu>();
        var button = GameObject.Find("/CanvasMenu/MainMenu/BoutonOptions").GetComponent<Button>();

        button.onClick.AddListener(menu.SettingsMenu);
        button.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        int sceneTransitionedTo = SceneManager.GetActiveScene().buildIndex;

        Assert.AreEqual(sceneTransitionedTo, sceneTransitionedFrom + 1);
    }

    [UnityTest]
    public IEnumerator QuitButtonQuitsGame()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.1f);

        var menu = new GameObject().AddComponent<MainMenu>().GetComponent<MainMenu>();
        var button = GameObject.Find("/CanvasMenu/MainMenu/BoutonQuit").GetComponent<Button>();

        button.onClick.AddListener(menu.QuitGame);
        button.onClick.Invoke();
        yield return new WaitForSeconds(0.1f); 

        Assert.IsTrue(menu.QuitInvoked);
    }

    [UnityTest]
    public IEnumerator EndSceneSwitchesBackToMainMenu()
    {
        var endScreen = new GameObject().AddComponent<RestartGameLoop>();
        endScreen.ReturnToMenu();
        yield return new WaitForSeconds(0.1f);
        int sceneTransitionedTo = SceneManager.GetActiveScene().buildIndex;

        Assert.AreEqual(sceneTransitionedTo, 1);
    }
}
