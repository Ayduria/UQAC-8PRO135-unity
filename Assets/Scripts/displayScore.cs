using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayScore : MonoBehaviour
{
    public Text scoreUI;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        scoreUI.text = "Score : " + score;
    }

    
}
