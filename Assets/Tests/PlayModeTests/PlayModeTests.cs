using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayModeTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestForJumpMovement()
    {
        GameObject sphere = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Sphere"));
        float y = sphere.transform.position.y;
        sphere.SendMessage("Jump");
        float updatedY = sphere.transform.position.y;
        Assert.AreNotEqual(y, updatedY);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EndSceneSwitchesBackToMainMenu()
    {
        var gameObject = new GameObject();
        var endScreen = gameObject.AddComponent<RestartGameLoop>();

        endScreen.ReturnToMenu();

        yield return new WaitForSeconds(0.1f);

        string sceneTransitionedTo = SceneManager.GetActiveScene().name;

        Assert.AreEqual(sceneTransitionedTo, "menu");
    }

    [UnityTest]
    public IEnumerator SettingsButtonLeadsToSettings()
    {
        var gameObject = new GameObject();
        var menu = gameObject.AddComponent<MainMenu>();

        menu.SettingsMenu();

        yield return new WaitForSeconds(0.1f);

        string sceneTransitionedTo = SceneManager.GetActiveScene().name;

        Assert.AreEqual(sceneTransitionedTo, "settings");
    }
}
