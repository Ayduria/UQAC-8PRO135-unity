using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeLogo : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image.canvasRenderer.SetAlpha(0.0f);
        StartCoroutine(Fade());
    }

    void fadeIn()
    {
        image.CrossFadeAlpha(1, 2, false);
    }

    void fadeOut()
    {
        image.CrossFadeAlpha(0, 2, false);
    }

    IEnumerator Fade()
    {
        fadeIn();
        yield return new WaitForSeconds(2);
        fadeOut();
    }
}
