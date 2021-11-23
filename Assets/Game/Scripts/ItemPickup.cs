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
    private AudioSource pickupSound;
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
        pickupSound = GetComponent<AudioSource>();

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
                totalLawnmowerCount--;
                totalEnemyCount--;
            } else {
                Debug.Log("You died lol");
                Destroy(gameObject);
            }
        }
    }
}
