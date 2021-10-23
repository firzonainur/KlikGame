using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    void load()
    {
        SceneManager.LoadScene("main_menu");
    }
}
