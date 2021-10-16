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
    public void scoreGivesCorrectValue() {

        var scoreScript = new GameObject().AddComponent<Score>().GetComponent<Score>();

        var platformTouched = 5;
        var trapTouched = 2;
        var bonusTouched = true;

        var expectedScore = (5 * 2 - 2)*3;
        var score = scoreScript.calculateScore(platformTouched,trapTouched,bonusTouched);
        Assert.AreEqual(expectedScore, score);

    }

    [Test]
    public void scoreCantBeNegative()
    {

        var scoreScript = new GameObject().AddComponent<Score>().GetComponent<Score>();

        var platformTouched = 3;
        var trapTouched = 10;
        var bonusTouched = false;

        var score = scoreScript.calculateScore(platformTouched, trapTouched, bonusTouched);
        Assert.AreEqual(0, score);
    }

   }
