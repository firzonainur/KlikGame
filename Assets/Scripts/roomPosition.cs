using UnityEngine;

public class roomPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject room1, room2, room3, room4, room5;

    public string roomKey;

    void Start()
    {
        int room = PlayerPrefs.GetInt(roomKey);
        Vector3 newPos = new Vector3(0, 0, 0);
        switch(room)
        {
            case 0: newPos = gameObject.transform.position; break;
            case 1: newPos = room1.transform.position; break;
            case 2: newPos = room2.transform.position; break;
            case 3: newPos = room3.transform.position; break;
            case 4: newPos = room4.transform.position; break;
            case 5: newPos = room5.transform.position; break;
        }

        gameObject.transform.position = newPos;
        PlayerPrefs.SetInt(roomKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
