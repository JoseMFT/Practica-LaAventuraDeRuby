using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController: MonoBehaviour {
    public int maxHealth = 5;
    int currentHealth;
    Vector2 position, inputMovement, newPosition, prevPosition;
    public bool hasHorizontalInput, hasVerticalInput, isWalking, keyboardActive;
    public float maxSpeed = 7.5f, speed = 5f, ogSpeed = 5f;
    public GameObject joyStick;
    Rigidbody2D rigidbody2d;
    //string[] meanSpeedString;
    //float meanSpeedFloatAdd = 0f, meanSpeed = 0f;

    private void Awake () {
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }
    void Start () {
        ogSpeed = speed;
        maxSpeed = speed * 1.5f;
    }


    void FixedUpdate () {
        keyboardActive = !joyStick.activeSelf;

        //if (keyboardActive) {
        inputMovement.x = Input.GetAxis ("Horizontal");
        inputMovement.y = Input.GetAxis ("Vertical");
        //position.Set (inputMovement.x, inputMovement.y);
        //position.Normalize ();
        //}

        position = rigidbody2d.position;
        position += inputMovement * speed * Time.deltaTime;
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

        /*newPosition = position;
        Debug.Log ((newPosition - prevPosition).magnitude.ToString ());
        if (meanSpeedString != null) {
            if (meanSpeedString.Length < 255) {
                meanSpeedString[meanSpeedString.Length - 1] = (CurrentSpeed (newPosition, prevPosition).ToString ());
                meanSpeedFloatAdd += float.Parse (meanSpeedString[meanSpeedString.Length - 1]);
                meanSpeed = meanSpeedFloatAdd / meanSpeedString.Length;
                Debug.Log (meanSpeed.ToString ());
            } else {
                meanSpeedString = null;
                meanSpeedFloatAdd = 0;
                meanSpeed = 0;
            }
        } else {
            meanSpeedString[0] = (CurrentSpeed (newPosition, prevPosition).ToString ());
        }
        prevPosition = newPosition;        
        */
    }

    /*public void OnMovement (InputAction.CallbackContext value) {
        inputMovement = value.ReadValue<Vector2> ();
        position.Set (inputMovement.x, inputMovement.y);
        position.Normalize ();
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

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.tag == "HealthCollectibleFruit") {
            Destroy (collision.gameObject);
            ChangeHealth (1);
        }
    }

    public void ChangeHealth (int amount) {
        currentHealth = Mathf.Clamp (currentHealth + amount, 0, maxHealth);
        Debug.Log (currentHealth + " / " + maxHealth);
    }

    /*public float CurrentSpeed (Vector2 x, Vector2 y) {
        float z;
        z = (x-y).magnitude / Time.deltaTime;
        return z;
        
    }*/

}
