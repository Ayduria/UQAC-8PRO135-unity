using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidCount : MonoBehaviour
{
    public Text AsteroidCountUI;

    // Update is called once per frame
    void Start()
    {
        int asteroidCount = GameObject.FindGameObjectsWithTag("Asteroid").Length;
        AsteroidCountUI.text = "Asteroid count : " + asteroidCount;
    }

    void Update()
    {
        //int asteroidCount = GameObject.FindGameObjectsWithTag("Asteroid").Length;

    }
}
