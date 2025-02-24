using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level
{
    public Level(string name, int id,int left,int right,int need, Func<int, int> lFMove, Func<int, int> lSMove, Func<int, int> rFMove, Func<int, int> rSMove)
    {
        Name = name;
        this.id = id;
        this.left = left;
        this.right = Random.Range(1, right+1);
        this.need = need;
        LFMove = lFMove;
        LSMove = lSMove;
        RFMove = rFMove;
        RSMove = rSMove;
    }

    public string Name { get; }
    public int id { get; }
    public int left {  get; }
    public int right { get; }
    public int need { get; }
    public Func<int,int> LFMove {  get; }
    public Func<int, int> LSMove {  get; }
    public Func<int, int> RFMove {  get; }
    public Func<int, int> RSMove {  get; }

}

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

    List<Level> levels = new() { 
        new Level("Легкий", 1,7,69,77, (left) => { return left+=1; }, (left) => { return left *= 2; }, (right) => { return right++; }, (right) => { return right *= 2; }),
        new Level("Легкий+", 2,4,77,82, (left) => { return left+=1; }, (left) => { return left *= 4; }, (right) => { return right++; }, (right) => { return right *= 4; }),
        new Level("Средний", 3,3,57,61, (left) => { return left+=1; }, (left) => { return left *= 4; }, (right) => { return right++; }, (right) => { return right *= 4; }),
        new Level("Средний+", 4,8,32,41, (left) => { return left+=1; }, (left) => { return left *= 4; }, (right) => { return right++; }, (right) => { return right *= 4; }),
    
    
    
    };

    [SerializeField]
    int right = 3;
    [SerializeField]
    int left = 5;

    public bool player = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Visual();
        print(left = levels.First().LFMove(left));
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
            int t = UnityEngine.Random.Range(0, 4);
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
        Visual();
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
    void Visual()
    {
        summ.text = "Сумма камней: \n" + (left + right);
        leftText.text = left.ToString();
        rightText.text = right.ToString();
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
