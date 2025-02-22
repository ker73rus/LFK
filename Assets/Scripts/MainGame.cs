using System.Collections;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    GameObject TutorialPanel;
    [SerializeField]
    GameObject GamePanel;
    [SerializeField]
    GameObject AnalysisPanel;
    [SerializeField]
    TextMeshProUGUI resultText;

    [SerializeField]
    TextMeshProUGUI summ;
    [SerializeField]
    TextMeshProUGUI rightText;
    [SerializeField]
    TextMeshProUGUI leftText;

    [SerializeField]
    int needScore = 77;

    [SerializeField]
    int right = 3;
    [SerializeField]
    int left = 5;

    public bool player = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckSum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AIChoice()
    {
        player = false;
        yield return new WaitForSeconds(1f);
        if(left + 1 + right >= needScore)
        {
            left++;
            CheckSum();
        }
        else if(right + 1 + left >= needScore) { right++; CheckSum(); }
        else if(left*2 + right >= needScore) { left*=2; CheckSum(); }
        else if(right*2 + left >= needScore) {right*=2; CheckSum(); }
        else
        {
            int t = Random.Range(0, 4);
            switch (t)
            {
                case 0:
                    left += 1;
                    break;
                case 1:
                    right += 1;break;
                case 2:
                    left *= 2;break;
                case 3:
                    right *= 2;break;
            }
            CheckSum();
        }

    }
    public void UnderstandTutorial()
    {
        TutorialPanel.SetActive(false);
        GamePanel.SetActive(true);
    }
    void CheckSum()
    {
        summ.text = "Сумма камней: \n" + (left + right);
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        if (left + right >= needScore)
        {
            GamePanel.SetActive(false);
            AnalysisPanel.SetActive(true);
            if (player)
            {
                resultText.text = "Ты победил! Сделал всё правильно!";
            }
            else
            {
                resultText.text = "Неудачный выбор ходов. Попробуй ещё раз! \n Аналитика будет доступна в последующих версиях";
            }
        }
        else {
            if (player)
            {
                StartCoroutine(AIChoice());
            }
            else
            {
                player = true;
            }
        }
    }

    public void LeftFirstMove()
    {
        if (player)
        {
            left += 1;
            leftText.text = left.ToString();
            CheckSum();
        }
    }
    public void RightFirstMove()
    {
        if (player)
        {
            right += 1;
            rightText.text = right.ToString();
            CheckSum();
        }
    }
    public void LeftSecondMove()
    {
        if (player)
        {
            left *= 2;
            leftText.text = left.ToString();
            CheckSum();
        }
    }
    public void RightSecondMove()
    {
        if (player)
        {
            right *= 2;
            rightText.text = right.ToString();
            CheckSum();
        }
    }
}
