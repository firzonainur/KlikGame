using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject panelPause;

    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            panelPause.SetActive(false);
        }
    }

    public void Restart()
    {
        Application.LoadLevel(3);
    }

    public void MenuUtama()
    {
        Application.LoadLevel(1);
    }


}
