using TMPro;
using UnityEngine;

public class AboutPaging : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    public void Next()
    {
        text.pageToDisplay = text.pageToDisplay >= 4 ? 1 : text.pageToDisplay+1;
    }
    public void Previous() { 
        text.pageToDisplay = text.pageToDisplay <= 1 ? 4 : text.pageToDisplay-1;

    }
    public void Start()
    {
        text.pageToDisplay = 1;
    }
}
