using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundOnCollision : MonoBehaviour
{
    public SceneLoader sceneLoader;
    private AudioSource collisionSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
        collisionSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
           collisionSound.Play();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
