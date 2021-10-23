using System.Linq;
using UnityEngine;

public class player : MonoBehaviour
{
    private Animator animator;
    string current_animation;
    private Rigidbody2D rigid;
    private bool rightPressed, leftPressed, upPressed, downPressed, attackPressed;
    private bool attacking = false;
    public bool death = false;

    [Range(0.1f, 2.0f)]
    public float animationSpeed;

    [Range(0.1f, 10.0f)]
    public float movementSpeed;

    [Range(0.1f, 10.0f)]
    public float attackSpeed;

    public GameObject bullet;
    public Transform arrowLeft, arrowRight, arrowUp, arrowDown;

    private void ChangeAnimationState(string new_animation, float speed)
    {
        animator.speed = speed;

        if (current_animation == new_animation) return;

        animator.Play(new_animation);
        current_animation = new_animation;
    }

    private void StopAnimation()
    {
        if (current_animation.Contains("attack"))
        {
            current_animation = current_animation.Substring(0, current_animation.Length - 7);
        }
        
        animator.Play(current_animation, 0, 0);
    }

    private void CekInput()
    {
        rightPressed = Input.GetKey(KeyCode.D);
        leftPressed = Input.GetKey(KeyCode.A);
        upPressed = Input.GetKey(KeyCode.W);
        downPressed = Input.GetKey(KeyCode.S);
        attackPressed = Input.GetMouseButtonDown(0);
    }

    private void SpawnArrow()
    {
        if (current_animation.Contains("right")) Instantiate(bullet, arrowRight.position, arrowRight.rotation);
        else if (current_animation.Contains("left")) Instantiate(bullet, arrowLeft.position, arrowLeft.rotation);
        else if (current_animation.Contains("up")) Instantiate(bullet, arrowUp.position, arrowUp.rotation);
        else if (current_animation.Contains("down")) Instantiate(bullet, arrowDown.position, arrowDown.rotation);
    }

    private void AturGerakan()
    {
        if (attacking)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                StopAnimation();
                SpawnArrow();
                attacking = false;
            }
        }

        if (attackPressed && !attacking)
        {
            attacking = true;
            rigid.velocity = new Vector2(0, 0);
            if (!current_animation.Contains("attack"))
            {
                ChangeAnimationState(current_animation + "_attack", attackSpeed);
            }
        }

        if (!attacking)
        {
            if (rightPressed)
            {
                rigid.velocity = new Vector2(movementSpeed, 0);
                ChangeAnimationState("player_right", animationSpeed);
            }
            else if (leftPressed)
            {
                rigid.velocity = new Vector2(-movementSpeed, 0);
                ChangeAnimationState("player_left", animationSpeed);
            }
            else if (upPressed)
            {
                rigid.velocity = new Vector2(0, movementSpeed);
                ChangeAnimationState("player_up", animationSpeed);
            }
            else if (downPressed)
            {
                rigid.velocity = new Vector2(0, -movementSpeed);
                ChangeAnimationState("player_down", animationSpeed);
            }
            else
            {
                StopAnimation();
                rigid.velocity = new Vector2(0, 0);
            }
        }
    }

    public void Death()
    {
        death = true;
        rigid.velocity = new Vector2(0, 0);
        ChangeAnimationState("player_dead", 0.5f);
        CircleCollider2D[] colliders = gameObject.GetComponents<CircleCollider2D>();
        foreach(CircleCollider2D cc in colliders) cc.enabled = false;
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        current_animation = "player_right";
        StopAnimation();
        
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
    }

    private void Update()
    {
        if (death) return;

        CekInput();
    }

    private void FixedUpdate() {
        if (death) return;

        AturGerakan();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Death();
        }
    }
}
