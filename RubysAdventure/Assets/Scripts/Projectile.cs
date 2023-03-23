using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour {
    public GameObject hitFX;
    Rigidbody2D rbProjectile;
    public AudioClip hitRobotClip;
    // Start is called before the first frame update
    void Awake () {
        rbProjectile = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.magnitude > 500f) {
            Destroy (gameObject);
        }
    }

    public void Launch (Vector2 direction, float force) {
        rbProjectile.AddForce (direction * force);
    }

    private void OnCollisionEnter2D (Collision2D collision) {

        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController> ();
        Instantiate(hitFX, transform.position, Quaternion.identity);

        if (enemyController != null) {            
            enemyController.Fix ();
            enemyController.PlaySound(hitRobotClip);
        }
        Destroy (gameObject);

    }
}
