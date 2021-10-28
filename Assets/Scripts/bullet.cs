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
                string skin = PlayerPrefs.GetString("skin", "standard");
                float damage = 0;
                if (skin == "standard") damage = 10;
                else if (skin == "bowUp") damage = 20;
                else if (skin == "plateArmor") damage = 30;

                virus Virus = other.gameObject.GetComponent<virus>();
                Virus.health -= damage;
            }

            Destroy(gameObject);
        }
    }
}
