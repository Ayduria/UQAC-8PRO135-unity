using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayModeTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayModeTestsSimplePasses()
    {
        // Use the Assert class to test conditions
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

        Scene sceneTransitionedTo = SceneManager.GetActiveScene();

        Assert.AreEqual(sceneTransitionedTo.name, "menu");
    }
}
