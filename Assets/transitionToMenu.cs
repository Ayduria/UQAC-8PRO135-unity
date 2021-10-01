using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionToMenu : MonoBehaviour
{

    public void toSettings()
    {
        SceneManager.LoadScene(2);    
    }

}
