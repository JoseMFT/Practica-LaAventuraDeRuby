using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript: MonoBehaviour {
    CanvasGroup canvasGroup;
    public float animationTime = 1f, alphaGoal = -1f;
    GraphicRaycaster graphicRaycaster;
    bool animating = false;
    public GameObject joyStick;

    void Start () {
        canvasGroup = GetComponent<CanvasGroup> ();
        graphicRaycaster = GetComponent<GraphicRaycaster> ();
    }

    // Update is called once per frame
    void Update () {
        if (animating == false) {
            if (Input.GetKey ("escape")) {
                graphicRaycaster.enabled = false;
                animating = true;
                alphaGoal = -alphaGoal;
                canvasGroup.alpha += alphaGoal * (Time.deltaTime / animationTime);
            }
        } else {
            if (canvasGroup.alpha > 0f && canvasGroup.alpha! < 1f) {
                canvasGroup.alpha += alphaGoal * (Time.deltaTime / animationTime);
            } else if (canvasGroup.alpha == 0f) {
                animating = false;
            } else if (canvasGroup.alpha == 1f) {
                animating = false;
                graphicRaycaster.enabled = true;
            }
        }
    }

    public void CanvasAnimation () {

    }
}
