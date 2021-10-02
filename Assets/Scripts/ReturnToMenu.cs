using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndReturnToMenu());
    }

    IEnumerator WaitAndReturnToMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
