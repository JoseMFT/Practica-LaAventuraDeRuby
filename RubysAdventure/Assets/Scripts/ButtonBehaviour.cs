using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    Vector3 ogSize;
    // Start is called before the first frame update
    void Start()
    {
        ogSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleUp ()
    {
        LeanTween.scale(gameObject, ogSize * 1.1f, .1f).setEaseOutCubic();
    }

    public void ScaleDown ()
    {
        LeanTween.scale(gameObject, ogSize, .1f).setEaseOutCubic();
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
