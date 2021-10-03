using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Decompte : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public float time = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
        time += 1;
    }

    IEnumerator timer()
    {
        while(time > 0)
        {
            time--;
            yield return new WaitForSeconds(1f);
            GetComponent<Text>().text = time.ToString();
        }

        if (time == 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
