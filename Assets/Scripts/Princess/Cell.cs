using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Text;
    [SerializeField]
    int num;
    [SerializeField]
    Image Image;


    public void SetNum(int n)
    {
        num = n;
        Text.text = num.ToString();
        if(num == 0 || num == -1)
        {
            Image.color = Color.cyan;
        }
        else
        {
            Image.color= Color.white;
        }
    }
}
