using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController: MonoBehaviour {
    Vector2 position, inputMovement;
    public bool hasHorizontalInput, hasVerticalInput, isWalking, usingScreenJoystick;
    public float maxSpeed = 6f, speed = 3f, ogSpeed;
    public GameObject joyStick;
    void Start () {
        ogSpeed = speed;
        maxSpeed = speed * 1.5f;
    }

    // Update is called once per frame
    void Update () {

        //DetectController ();

        transform.position += new Vector3 (position.x, position.y, 0f) * Time.deltaTime * maxSpeed;
        DetectInput ();


        if (isWalking) {
            if (speed < maxSpeed) {
                speed += Time.deltaTime;
            } else if (speed > maxSpeed) {
                speed = maxSpeed;
            }
        } else {
            speed = ogSpeed;
        }
    }

    public void OnMovement (InputAction.CallbackContext value) {
        inputMovement = value.ReadValue<Vector2> ();
        position.Set (inputMovement.x, inputMovement.y);
        position.Normalize ();
    }

    public void DetectController () {
        /*if (Input.GetJoystickNames ().Length > 0) {
            if (!joyStick.activeSelf) {
                joyStick.SetActive (true);
            }
        } else {
            if (joyStick.activeSelf) {
                joyStick.SetActive (false);
            }
        }*/

        /*if (!isWalking) {
            if (Input.GetAxis ("Horizontal") || Input.GetAxis ("Vertical")) {
                joyStick.SetActive (false);
                inputMovement.x = Input.GetAxis ("Horizontal");
                inputMovement.y = Input.GetAxis ("Vertical");
                position.Set (inputMovement.x, inputMovement.y);
                position.Normalize ();
            }
        }*/
    }

    public void DetectInput () {
        hasHorizontalInput = !Mathf.Approximately (inputMovement.x, 0f);
        hasVerticalInput = !Mathf.Approximately (inputMovement.y, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
    }

}
