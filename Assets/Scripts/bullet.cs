using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float bulletSpeed = 1.5f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name != "player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                virus Virus = other.gameObject.GetComponent<virus>();
                Virus.health -= 10;
            }

            Destroy(gameObject);
        }
    }
}
