using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class virus : MonoBehaviour
{
    private Animator animator;
    string current_animation;
    private Rigidbody2D rigid;
    private CircleCollider2D circleCollider;

    private bool death = false;
    public float health;
    public float movementSpeed = 1f;
    public float minRange;

    public GameObject player;

    private void ChangeAnimationState(string new_animation, float speed)
    {
        animator.speed = speed;

        if (current_animation == new_animation) return;

        animator.Play(new_animation);
        current_animation = new_animation;
    }

    public void Death()
    {
        death = true;
        ChangeAnimationState("virus_explode", 1);
        circleCollider.enabled = false;
    }

    private void ChasePlayer()
    {
        if (player.GetComponent<player>().death) return;

        if (Vector3.Distance(transform.position, player.transform.position) >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;

        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !death)
        {
            Death();
            gameObject.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        }

        if (current_animation == "virus_explode")
        {
            Destroy(gameObject, 0.8f);
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= minRange && !death) ChasePlayer();
    }
}
