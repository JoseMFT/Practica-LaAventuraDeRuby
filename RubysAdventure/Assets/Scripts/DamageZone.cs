using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone: MonoBehaviour {
    const float damageTime = 2f;
    public float currentTime = 0;
    public GameObject prefabHit;
    public RubyController rubyController;

    // Start is called before the first frame update
    private void OnTriggerStay2D (Collider2D collision) {
        if (rubyController != null) {
            if (currentTime >= damageTime) {
                rubyController.ChangeHealth (-1);
                Instantiate (prefabHit, collision.transform.position, collision.transform.rotation);
                currentTime = 0f;
            } else {
                currentTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        rubyController = other.GetComponent<RubyController> ();
        if (rubyController != null) {
            currentTime = 0f;
            rubyController.ChangeHealth (-1);
            Instantiate (prefabHit, other.transform.position, other.transform.rotation);
        }
    }
}
