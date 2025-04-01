using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaterGame : MonoBehaviour, Game
{
    [SerializeField]
    TextMeshProUGUI lText;
    [SerializeField]
    TextMeshProUGUI rText;
    [SerializeField]
    int need;
    [SerializeField]
    int nowLeft = 0;
    [SerializeField]
    int nowRight = 0;
    [SerializeField]
    int maxLeft = 0;
    [SerializeField]
    int maxRight = 0;
    [SerializeField]
    WaterLevel level;
    [SerializeField]
    Image LeftImage;
    [SerializeField]
    Image RightImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        nowLeft = 0;
        nowRight = 0;
        Visual();
    }

    public void Visual()
    {
        lText.text = nowLeft.ToString();
        rText.text = nowRight.ToString();
        LeftImage.fillAmount = 0.67f * nowLeft/maxLeft ;
        RightImage.fillAmount = 0.67f * nowRight/maxRight ;
    }


    public void PourLeft()
    {
        nowLeft = maxLeft;
        Visual();
    }
    public void PourRight()
    {
        nowRight = maxRight;
        Visual();
    }
    public void PourOutLeft()
    {
        nowLeft = 0;
        Visual();
    }
    public void PourOutRight()
    {
        nowRight = 0;
        Visual();
    }
    public void PourOverLeft()
    {
        if(nowLeft >= maxRight - nowRight)
        {
            int t = maxRight - nowRight;
            nowLeft -= t;
            nowRight += t;
        }
        else
        {
            nowRight += nowLeft;
            nowLeft = 0;
        }
        Visual();
        Check();
    }
    public void PourOverRight()
    {
        if (nowRight >= maxLeft - nowLeft)
        {
            int t = maxLeft - nowLeft;
            nowRight -= t;
            nowLeft += t;
        }
        else
        {
            nowLeft += nowRight;
            nowRight = 0;
        }
        Visual();
        Check();
    }
    public void Check()
    {
        if(nowLeft == need ||  nowRight == need)
        {
            level.Win();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
