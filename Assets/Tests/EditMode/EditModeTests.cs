using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditModeTests
{
    [Test]
    public void QuitButtonQuitsGame()
    {
        var menu = new GameObject().AddComponent<MainMenu>().GetComponent<MainMenu>();
        var button = GameObject.Find("/CanvasMenu/MainMenu/BoutonQuit").GetComponent<Button>();
        button.onClick.AddListener(menu.QuitGame);
        button.onClick.Invoke();
        Assert.IsTrue(menu.QuitInvoked);
    }
}
