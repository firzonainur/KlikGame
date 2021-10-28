using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class player : MonoBehaviour
{
    private Animator animator;
    string current_animation;
    private Rigidbody2D rigid;
    private bool rightPressed, leftPressed, upPressed, downPressed, attackPressed;
    private bool spacePressed;
    private bool attacking = false;
    public bool death = false;
    private float health = 10;

    private string skin = "standard";

    private SpriteRenderer sprite;

    [Range(0.1f, 2.0f)]
    public float animationSpeed;

    [Range(0.1f, 10.0f)]
    public float movementSpeed;

    [Range(0.1f, 10.0f)]
    public float attackSpeed;

    public GameObject bullet;
    public Transform arrowLeft, arrowRight, arrowUp, arrowDown;

    public Skins[] skins;

    public AudioSource tembak;

    [System.Serializable]
    public struct Skins {
        public Sprite[] sprites;
        public Sprite[] walkSprites;
        public Sprite[] deadSprites;
    }

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

        if (attackPressed && !attacking && !EventSystem.current.IsPointerOverGameObject())
        {
            attacking = true;
            rigid.velocity = new Vector2(0, 0);
            tembak.Play();
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

    private void Reskin()
    {
        skin = PlayerPrefs.GetString("skin", "standard");
        string spriteName = sprite.sprite.name;
        int spriteIndex = int.Parse(spriteName.Split('_')[1]);
        int skinIndex = 0;

        if (skin == "standard") skinIndex = 0;
        else if (skin == "bowUp") skinIndex = 1;
        else if (skin == "plateArmor") skinIndex = 2;

        if (spriteName.Contains("walk")) sprite.sprite = skins[skinIndex].walkSprites[spriteIndex];
        else if (spriteName.Contains("dead")) sprite.sprite = skins[skinIndex].deadSprites[spriteIndex];
        else sprite.sprite = skins[skinIndex].sprites[spriteIndex];
    }

    IEnumerator WaitHit(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        current_animation = "player_right";
        StopAnimation();
        
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;

        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (death) return;

        if (health <= 0) Death();

        CekInput();
    }

    private void LateUpdate() {
        Reskin();
    }

    private void FixedUpdate() {
        if (death) return;

        AturGerakan();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            if (PlayerPrefs.GetString("skin") == "plateArmor")
            {
                this.health -= 5;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(WaitHit(1f));
                
            }
            else
            {
                this.health -= 10;
            }
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
        }
    }
}