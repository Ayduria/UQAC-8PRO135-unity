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
    private GameObject[] ennemies;
    public GameObject postProcessing;

    private void Start()
    {
        ennemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemyCount = ennemies.Length;
    }

    private void Update()
    {
        lawnmowerCount.text = "Tondeuses: " + totalLawnmowerCount;
        enemyCount.text = "Ennemis restants: " + totalEnemyCount;

        if (totalEnemyCount == 0)
        {
            SceneManager.LoadScene("Victory");
        }

        GameObject closestEnemy = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;
        foreach(GameObject enemy in ennemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }

        if (distance < 50)
        {
            postProcessing.SetActive(true);
        } else
        {
            postProcessing.SetActive(false);
        }

    }

    void OnCollisionEnter(Collision hit)
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
            }
        }
    }
}
