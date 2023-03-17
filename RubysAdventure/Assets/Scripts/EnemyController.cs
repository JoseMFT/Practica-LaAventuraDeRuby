using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour {

    float speed = 2f, changeTime = 2f, timer, collisionTimer, collisionChangeTime = 2f;
    bool vertical = false, broken = true;
    Vector2 enemyPosition;
    Rigidbody2D rbEnemy;
    public RubyController player;
    public Animator robotAnimator;
    // Start is called before the first frame update
    void Start () {
        timer = changeTime;
        robotAnimator = GetComponent<Animator> ();
        collisionTimer = collisionChangeTime;
        rbEnemy = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate () {

        timer -= Time.deltaTime;

        if (timer <= 0f) {
            timer = changeTime;
            vertical = !vertical;
            if (vertical) {
                speed = -speed;
            }
        }

        enemyPosition = rbEnemy.position;

        if (vertical) {
            robotAnimator.SetFloat ("MoveX", 0f);
            robotAnimator.SetFloat ("MoveY", speed);
            enemyPosition.y += Time.deltaTime * speed;
        } else {
            robotAnimator.SetFloat ("MoveX", speed);
            robotAnimator.SetFloat ("MoveY", 0f);
            enemyPosition.x += Time.deltaTime * speed;
        }

        rbEnemy.MovePosition (enemyPosition);

        if (!broken) {
            return;
        }

    }

    private void OnCollisionEnter2D (Collision2D collision) {
        player = collision.gameObject.GetComponent<RubyController> ();

        if (player) {
            player.ChangeHealth (-1);
        }
    }

    private void OnCollisionStay2D (Collision2D collision) {
        player = collision.gameObject.GetComponent<RubyController> ();
        if (player) {
            collisionTimer -= Time.deltaTime;
            if (collisionTimer < 0f) {
                collisionTimer = collisionChangeTime;
                player.ChangeHealth (-1);
            }
        }
    }

    public void Fix () {
        broken = false;
        rbEnemy.simulated = false;
        robotAnimator.SetTrigger ("Fixed");
    }
}
