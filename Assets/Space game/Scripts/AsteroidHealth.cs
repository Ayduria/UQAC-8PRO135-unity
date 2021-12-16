using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidHealth : MonoBehaviour
{
    public GameObject HealthBarUI;
    private Image healthBar;
    private float maxHealth = 100;
    private float currentHealth;
    private bool healthBarVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = HealthBarUI.transform.GetChild(0).GetChild(1).GetComponent<Image>();

        currentHealth = maxHealth;
    }

    void OnMouseDown()
    {
        TakeDamage();
        if (!healthBarVisible)
        {
            GameObject healthBar = Instantiate(HealthBarUI, this.transform);
            healthBarVisible = true;
        }
    }

    void TakeDamage()
    {
        currentHealth -= 50;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
