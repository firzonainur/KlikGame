using System.Collections;
using UnityEngine;

public class chest : MonoBehaviour
{
    private bool interact = false;
    public Sprite openedChestSprite;
    public Sprite emptyChestSprite;
    BoxCollider2D b2d;
    SpriteRenderer spriteRenderer;
    private bool fadeOut = false;
    public float fadeSpeed = 0f;
    public string itemType;
    public string item;

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        spriteRenderer.sprite = emptyChestSprite;
        PlayerPrefs.SetString(itemType, item);
        Debug.Log("You get " + item);
        fadeOut = true;
    }

    private void OpenChest()
    {
        interact = false;
        b2d.enabled = false;
        spriteRenderer.sprite = openedChestSprite;
        StartCoroutine(Wait(1f));
    }
    
    void Start()
    {
        b2d = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Chest telah dibuka
        if (PlayerPrefs.GetString(itemType) == item)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space) && interact)
        {
            OpenChest();
        }

        if (fadeOut)
        {
            Color objectColor = spriteRenderer.material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            spriteRenderer.material.color = objectColor;

            if (objectColor.a <= 0)
            {
                fadeOut = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            interact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            interact = false;
        }
    }
}
