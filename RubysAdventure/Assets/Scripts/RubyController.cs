using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RubyController: MonoBehaviour {
    AudioSource audioSource;
    public AudioClip damageClip;
    public int maxHealth = 5;
    public bool animLowHP = false, isInvincible = false, onSettings = false;
    float timeInvincible = 0f, invincibleTimer = 2.5f, shotPace = .5f, shotCD = 0f;
    public Animator rubyAnimator;
    public int health { get { return currentHealth; } }
    public int currentHealth, prevHealth, robotCount = 0;
    Vector2 position, inputMovement, lookDirection = new Vector2 (1f, 0f);
    bool hasHorizontalInput, hasVerticalInput, isWalking;
    public float maxSpeed = 7.5f, speed = 5f, ogSpeed = 5f;
    Rigidbody2D rigidbody2d;
    public GameObject prefabCog, endingCanvas;
    Projectile projectile;
    public Image reloadAtt;

    private void Awake () {
        rubyAnimator = GetComponent<Animator> ();
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }
    void Start () {
        currentHealth = maxHealth;
        ogSpeed = speed;
        maxSpeed = speed * 1.5f;
        audioSource = GetComponent<AudioSource>();
    }


    void FixedUpdate () {

        inputMovement.x = Input.GetAxis ("Horizontal");
        inputMovement.y = Input.GetAxis ("Vertical");

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

        if (!Mathf.Approximately (inputMovement.x, 0.0f) || !Mathf.Approximately (inputMovement.y, 0.0f)) {
            lookDirection.Set (inputMovement.x, inputMovement.y);
            lookDirection.Normalize ();
        }

        rubyAnimator.SetFloat ("Look X", lookDirection.x);
        rubyAnimator.SetFloat ("Look Y", lookDirection.y);
        rubyAnimator.SetFloat ("Speed", inputMovement.magnitude);


        if (isInvincible) {
            if (timeInvincible < invincibleTimer) {
                timeInvincible += Time.deltaTime;
            } else {
                isInvincible = false;
                timeInvincible = 0f;
            }
        }
        if (shotCD <= shotPace) {
            shotCD += Time.deltaTime;
        }
        
        if (!onSettings) {
            if (Input.GetKey("c") && shotCD > shotPace)
            {
                Launch();
                shotCD = 0f;
            }
        }

        reloadAtt.fillAmount = shotCD / shotPace;
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
        Debug.Log ("Auch (?)");
        if ((isInvincible == false && amount < 0) || amount > 0) {
            currentHealth = Mathf.Clamp (currentHealth + amount, 0, maxHealth);
            Debug.Log (currentHealth + " / " + maxHealth);
            UIHealthBar.instance.SetValue(currentHealth, maxHealth);
            if (amount < 0) {
                rubyAnimator.SetTrigger ("Hit");
                PlaySound(damageClip);
                isInvincible = true;
                timeInvincible = 0f;
            }
        }
    }

    public void Launch () {
        GameObject projectileObj = Instantiate (prefabCog, rigidbody2d.position + Vector2.up * .5f, Quaternion.identity);
        projectile = projectileObj.GetComponent<Projectile> ();
        projectile.Launch (lookDirection, 300f);
        rubyAnimator.SetTrigger ("Launch");

    }

    public void PlaySound (AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void RobotChange (int changeNumber)
    {
        robotCount -= changeNumber;

        if (robotCount <= 0)
        {
            endingCanvas.SetActive(true);
        }
    }

}
