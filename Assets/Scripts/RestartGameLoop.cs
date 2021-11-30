using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class RestartGameLoop : MonoBehaviour
{
    string sceneName;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "victory")
        {
            time = 5.0f;
        } else if (sceneName == "defeat")
        {
            time = 8.0f;
        }
        
        Invoke("ReturnToMenu", time);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ReturnToMenu();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
