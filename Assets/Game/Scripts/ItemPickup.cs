using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemPickup : MonoBehaviour
{
    private int totalLawnmowerCount = 0;
    private int totalEnemyCount;
    public Text lawnmowerCount;
    public Text enemyCount;
    public AudioSource pickupSound;
    public AudioSource enemyKilled;
    private GameObject[] cptEnnemis;

    private void Start()
    {
        cptEnnemis = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemyCount = cptEnnemis.Length;
    }

    private void Update()
    {
        lawnmowerCount.text = "Tondeuses: " + totalLawnmowerCount;
        enemyCount.text = "Ennemis restants: " + totalEnemyCount;

        if (totalEnemyCount == 0)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Lawnmower")
        {
            Destroy(hit.gameObject);
            pickupSound.Play();
            totalLawnmowerCount++;
        }

        if (hit.gameObject.tag == "Enemy")
        {
            if (totalLawnmowerCount > 0)
            {
                Destroy(hit.gameObject);
                enemyKilled.Play();
                totalLawnmowerCount--;
                totalEnemyCount--;
            } else {
                Debug.Log("You died lol");
                Destroy(gameObject);
            }
        }
    }
}
