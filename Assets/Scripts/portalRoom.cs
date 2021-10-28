using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalRoom : MonoBehaviour
{
    private bool teleporting = false;
    public string roomKey;
    public int roomNumber;
    public string nextScene;

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        teleporting = true;
    }

    void Update() {
        if (teleporting)
        {
            PlayerPrefs.SetInt(roomKey, roomNumber);
            SceneManager.LoadScene(nextScene);
            teleporting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Teleporting to next level...");
            StartCoroutine(Wait(0.5f));
        }
    }
}
