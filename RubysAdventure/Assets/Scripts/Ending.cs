using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject mainCanvas, menuCanvas;
    bool canFinish = false;
    float endTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        LeanTween.scale(gameObject, Vector3.zero, 0f).setOnComplete(() =>
      {
          LeanTween.scale(gameObject, Vector3.one, 1.25f).setEaseOutBounce().setOnComplete(() =>
        {
            canFinish = true;
        });
      });
    }

    // Update is called once per frame
    void Update()
    {
        if (canFinish)
        {
            if (endTime >= 0f)
            {
                endTime -= Time.deltaTime;
            } else
            {
                Application.Quit();
            }
        }
    }
}
