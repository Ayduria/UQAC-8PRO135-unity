using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidHealth : MonoBehaviour
{
    public ParticleSystem explosion;
    public GameObject HealthBarUI;
    private Image healthBar;
    private float maxHealth = 100;
    private float currentHealth;
    private bool healthBarVisible = false;

    // Start is called before the first frame update
    private void Start()
    {
        healthBar = HealthBarUI.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        currentHealth = maxHealth;
    }

    private void OnMouseDown()
    {
        TakeDamage();
        if (!healthBarVisible)
        {
            GameObject healthBar = Instantiate(HealthBarUI, this.transform);
            healthBarVisible = true;
        }
    }

    private void TakeDamage()
    {
        currentHealth -= 50;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth == 0)
        {
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Instantiate(explosion, this.transform.position, Quaternion.identity, this.transform);
            explosion.Play();
            AudioSource explosionSound = GameObject.Find("/ExplosionSound").GetComponent<AudioSource>();
            explosionSound.Play();
            Destroy(this.gameObject, 0.8f);
        }
    }
}
