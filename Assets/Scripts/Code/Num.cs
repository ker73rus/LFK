using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Num : MonoBehaviour
{
    [SerializeField]
    CodeGame game;
    [SerializeField]
    Image image;
    public TextMeshProUGUI Text;
    public int curNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Up()
    {
        if(curNum < 9)
            curNum++;
        else curNum = 0;
        Text.text = curNum.ToString();
        SetState(0);
    }
    public void Down()
    {
        if(curNum >0)
            curNum--;
        else curNum = 9;
        Text.text = curNum.ToString();
        SetState(0);

    }

    public void SetState(int state)
    {
        switch (state)
        {
            case 0:
                Color color = Color.white;
                ColorUtility.TryParseHtmlString("#DEEAF4", out color);
                image.color = color; break;
            case 1:
                image.color = Color.red; break;
            case 2:
                image.color = Color.yellow; break;
            case 3:
                image.color = Color.green; break;
            default:
                image.color = Color.white;
                break;
        }

    }
}
