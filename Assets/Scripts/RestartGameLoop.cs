using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ReturnToMenu", 3.0f);
    }

     public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
