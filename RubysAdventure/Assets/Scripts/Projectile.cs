using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour {
    Rigidbody2D rbProjectile;
    // Start is called before the first frame update
    void Awake () {
        rbProjectile = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {

    }

    public void Launch (Vector2 direction, float force) {
        rbProjectile.AddForce (direction * force);
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        Debug.Log ("Collisioned with: " + collision.gameObject);
        Destroy (gameObject);

    }
}
