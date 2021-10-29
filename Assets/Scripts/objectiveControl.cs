using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class objectiveControl : MonoBehaviour
{
    public GameObject objectiveBox;
    public GameObject hintReference;
    public bool aktif = false;
    public string[] hints;

    void Start()
    {
        objectiveBox.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300 + 40 * (hints.Count() - 1));

        for (int i = 0; i < hints.Count(); i++)
        {
            GameObject text = Instantiate(hintReference, new Vector3(0, 0, 0), Quaternion.identity);
            text.transform.SetParent(objectiveBox.transform);

            text.GetComponent<Text>().text = hints[i];

            RectTransform rect = text.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(0, -41 * i, 0);
            rect.localScale = new Vector3(1, 1, 1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            aktif = !aktif;
            objectiveBox.SetActive(aktif);
        }
    }
}
