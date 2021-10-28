using UnityEngine;

public class slipperyArea : MonoBehaviour
{
    public bool touchingIce = false;
    private Rigidbody2D r2d;
    public GameObject player;
    public float slipperySpeed;

    void Start()
    {
        r2d = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchingIce)
        {
            r2d.AddForce(new Vector2(slipperySpeed, r2d.velocity.y));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") touchingIce = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") touchingIce = false;
    }
}
