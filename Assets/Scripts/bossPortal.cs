using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossPortal : MonoBehaviour
{
    public string nextScene;
    public int nextLevel;
    private bool teleporting = false;

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        teleporting = true;
    }

    void Update() {
        if (teleporting)
        {
            if (PlayerPrefs.GetInt("level") < nextLevel) PlayerPrefs.SetInt("level", nextLevel);
            SceneManager.LoadScene(nextScene);
            teleporting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {   
            if (PlayerPrefs.GetString("key", "") == "boss")
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Debug.Log("Teleporting to next level...");
                StartCoroutine(Wait(0.5f));
            }
            else
            {
                Debug.Log("Need boss key");
            }
        }
    }
}
