using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RubyController: MonoBehaviour {
    public TextMeshProUGUI currentHPText;
    public int maxHealth = 5;
    public bool animLowHP = false;
    float timeInvincible = 0f, invincibleTimer = 2.5f;
    bool isInvincible = false;
    public Animator heartAnimator;
    public int health { get { return currentHealth; } }
    int currentHealth, prevHealth;
    Vector2 position, inputMovement;
    public bool hasHorizontalInput, hasVerticalInput, isWalking, keyboardActive;
    public float maxSpeed = 7.5f, speed = 5f, ogSpeed = 5f;
    public GameObject joyStick;
    Rigidbody2D rigidbody2d;

    private void Awake () {
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }
    void Start () {
        currentHealth = maxHealth;
        prevHealth = maxHealth;
        currentHPText.text = currentHealth + "/" + maxHealth;
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

        if (prevHealth > currentHealth) {
            heartAnimator.SetTrigger ("LostHealth");
        } else if (prevHealth < currentHealth) {
            heartAnimator.SetTrigger ("GainedHealth");
        }
        prevHealth = currentHealth;

        if (currentHealth <= 1) {
            if (animLowHP != true) {
                if (heartAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1.0f) {
                    heartAnimator.SetBool ("LowHP", true);
                    animLowHP = true;
                }
            }

        } else {
            if (animLowHP != false) {
                heartAnimator.SetBool ("LowHP", false);
                animLowHP = false;
            }
        }

        if (isInvincible) {
            if (timeInvincible >= invincibleTimer) {
                timeInvincible += Time.deltaTime;
            } else {
                isInvincible = false;
                timeInvincible = 0f;
            }
        }
    }

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

    public void ChangeHealth (int amount) {
        if (amount < 0) {
            if (isInvincible) {
                return;
            }
            isInvincible = true;
            timeInvincible = 0f;
        }
        if (isInvincible = false || amount > 0) {
            currentHealth = Mathf.Clamp (currentHealth + amount, 0, maxHealth);
            Debug.Log (currentHealth + " / " + maxHealth);
            currentHPText.text = currentHealth + "/" + maxHealth;
        }
    }

}
