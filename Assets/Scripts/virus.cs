using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class virus : MonoBehaviour
{
    private Animator animator;
    string current_animation;
    private Rigidbody2D rigid;
    private CircleCollider2D circleCollider;

    private bool death = false;
    public float health;
    public float movementSpeed;
    public float minRange;

    public bool paused = false;

    public GameObject player;
    public Tilemap walkable;
    private AStar.AStarSearch astar;
    List<Vector3Int> path;
    private IEnumerator movingCoroutine = null;
    private bool moving = false;

    public AudioSource kena;

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
        kena.Play();
    }

    private IEnumerator moveObject(List<Vector3Int> path)
    {
        if (path != null && !player.GetComponent<player>().death)
        {
            for (int i = 0; i < path.Count; i++)
            {
                float t = 0;
                Vector3 current = transform.position;
                Vector3 dest = walkable.CellToWorld(path[i]);

                while (t < 1f)
                {
                    if (death)
                    {
                        moving = false;
                        path = null;
                        StopCoroutine(movingCoroutine);
                        movingCoroutine = null;
                    }

                    if (paused)
                    {
                        moving = false;
                        path = null;
                        StopCoroutine(movingCoroutine);
                        movingCoroutine = null;
                    }

                    transform.position = Vector3.Lerp(current, dest, (t += movementSpeed * Time.deltaTime));
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        moving = false;
        path = null;
        StopCoroutine(movingCoroutine);
        movingCoroutine = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;

        circleCollider = gameObject.GetComponent<CircleCollider2D>();

        astar = new AStar.AStarSearch();
        path = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !death)
        {
            Death();
            Vector3 scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(2.5f * scale.x + scale.x, 2.5f * scale.y + scale.y, 2.5f * scale.z + scale.z);
        }

        if (current_animation == "virus_explode")
        {
            Destroy(gameObject, 0.8f);
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= minRange && !death && !moving)
        {
            moving = true;
            path = astar.Search(walkable, transform.position, player.transform.position);
            movingCoroutine = moveObject(path);
            StartCoroutine(movingCoroutine);
        }
    }
}
