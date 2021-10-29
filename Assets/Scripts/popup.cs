using UnityEngine;

public class popup : MonoBehaviour
{
    public GameObject popUp;
    public bool aktif;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        popUp.SetActive(aktif);
    }

    public void StopViruses()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var virus in viruses)
        {
            virus.GetComponent<virus>().paused = true;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<player>().enabled = false;
        Time.timeScale = 0;
    }

    public void EnableViruses()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var virus in viruses)
        {
            virus.GetComponent<virus>().paused = false;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<player>().enabled = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }
 
}
