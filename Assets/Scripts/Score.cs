using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int nbPlatformTouched=0;
    int nbTrapTouched=0;
    bool touchedBonus=false;
    int scoreFinal; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform") {
            nbPlatformTouched++;
        
        }

        if (collision.gameObject.tag == "Trap")
        {
            nbTrapTouched++;

        }

        if (collision.gameObject.tag == "Platform Bonus")
        {
            touchedBonus = true;
        }

        if (collision.gameObject.tag == "Finish") {

            scoreFinal = calculateScore(nbPlatformTouched,nbTrapTouched,touchedBonus);
            PlayerPrefs.SetInt("Score",scoreFinal);
   
        }
    }

    public int calculateScore(int nbPlatformTouched, int nbTrapTouched, bool touchedBonus)
    {
        int score = 0;

        score += nbPlatformTouched * 2;
        score -= nbTrapTouched;

        if (touchedBonus)
        {
            score *= 3;
        }
        if (score<0) {

            score = 0;
        }
        return score;
    
    }

}
