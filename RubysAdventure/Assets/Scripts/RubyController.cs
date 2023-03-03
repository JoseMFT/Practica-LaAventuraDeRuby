using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController: MonoBehaviour {
    Vector2 position, inputMovement, newposition;
    public bool hasHorizontalInput, hasVerticalInput, isWalking, keyboardActive;
    public float maxSpeed = 6f, speed = 3f, ogSpeed;
    public GameObject joyStick;
    Rigidbody2D rigidbody2d;

    private void Awake () {
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }
    void Start () {
        ogSpeed = speed;
        maxSpeed = speed * 1.5f;
    }

    // Update is called once per frame
    void Update () {
        keyboardActive = !joyStick.activeSelf;

        if (keyboardActive) {
            inputMovement.x = Input.GetAxis ("Horizontal");
            inputMovement.y = Input.GetAxis ("Vertical");
            position.Set (inputMovement.x, inputMovement.y);
            position.Normalize ();
        }

        position = rigidbody2d.position;
        position += inputMovement * Time.deltaTime * maxSpeed;
        rigidbody2d.MovePosition (position);


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

    /*public void DetectController () {
        if (Input.GetJoystickNames ().Length > 0) {
            if (!joyStick.activeSelf) {
                joyStick.SetActive (true);
            }
        } else {
            if (joyStick.activeSelf) {
                joyStick.SetActive (false);
            }
        }

        if (!isWalking) {
            if (Input.GetAxis ("Horizontal") || Input.GetAxis ("Vertical")) {
                joyStick.SetActive (false);
                inputMovement.x = Input.GetAxis ("Horizontal");
                inputMovement.y = Input.GetAxis ("Vertical");
                position.Set (inputMovement.x, inputMovement.y);
                position.Normalize ();
            }
        }    
    }*/

    public void DetectInput () {
        hasHorizontalInput = !Mathf.Approximately (inputMovement.x, 0f);
        hasVerticalInput = !Mathf.Approximately (inputMovement.y, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision != null) {
            Debug.Log ("ME CHOCO ME CHOCO ME CHOCO ME CHOCO AAAAAAAAAAAAAAAAAAA");
        }
    }

}
