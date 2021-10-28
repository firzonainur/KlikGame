using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
    }
 
}
