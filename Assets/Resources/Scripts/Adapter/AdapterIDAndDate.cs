using UnityEngine;
using TMPro;

public class AdapterIDAndDate : MonoBehaviour
{
    public TextMeshProUGUI Id;

    public TextMeshProUGUI Date;

    public void Parent(Transform Parent)
    {
        transform.SetParent(Parent);
    }
    public void Location(float top)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, top, 0);
    }

}
