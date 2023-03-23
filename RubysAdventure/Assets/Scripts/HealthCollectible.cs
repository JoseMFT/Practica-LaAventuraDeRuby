using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible: MonoBehaviour {
    public AudioClip collectedClip;
    RubyController rubyController;
    public GameObject prefabContact;
    void Start () {

    }


    private void OnTriggerEnter2D (Collider2D collision) {
        rubyController = collision.GetComponent<RubyController> ();

        if (rubyController != null) {
            if (rubyController.health < rubyController.maxHealth) {
                rubyController.ChangeHealth (1);
                Instantiate (prefabContact, transform.position, transform.rotation);
                Destroy (gameObject);
                rubyController.PlaySound(collectedClip);
            }
        }
    }
}
