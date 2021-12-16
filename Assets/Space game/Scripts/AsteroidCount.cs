using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidCount : MonoBehaviour
{
    public Text AsteroidCountUI;
    private int asteroidCount;

    void Update()
    {
        asteroidCount = GameObject.FindGameObjectsWithTag("Asteroid").Length;
        AsteroidCountUI.text = "Asteroid count : " + asteroidCount;
    }
}
