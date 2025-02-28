using System.Collections;
using TMPro;
using UnityEngine;

public class DiceGame : MonoBehaviour
{
    [SerializeField]
    int type = 1;
    [SerializeField]
    int player = 0;
    [SerializeField]
    int ai = 0;
    [SerializeField]
    TextMeshProUGUI AiAnswer;
    [SerializeField]
    TextMeshProUGUI Player;
    [SerializeField]
    TextMeshProUGUI Result;
    [SerializeField]
    DiceLevels level;
    [SerializeField]
    GameObject rates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        player = 0;
        ai = 0;
        rates.SetActive(false);
        AiAnswer.text = string.Empty;
        Player.text = string.Empty;
    }
    public int Dice()
    {
        int f = Random.Range(1, 7);
        int s = Random.Range(1, 7);
        return f + s;
    }
    public void PlayerDice()
    {
        player = Dice();
        Player.text = "Твои очки: \n" + player; 
        StartCoroutine(AIDice());
    }
    IEnumerator AIDice()
    {
        ai = Dice();
        yield return new WaitForSeconds(1);
        float t = Random.Range(0, 100);
        yield return new WaitForSeconds(1);
        switch (type)
        {
            case 1:
                if (t <= 30 && player > ai)
                {
                    Bluff();
                }
                else
                {
                    SayTrue();
                }
                break;
            case 2:
                if(t <= 50 && player > ai)
                {
                    Bluff();
                }
                else
                {
                    SayTrue();
                }
                break;
            case 3:

                if (t <= 70 && player > ai)
                {
                    Bluff();
                    
                }
                else SayTrue();
                break;
        }
        yield return new WaitForSeconds(1);
        rates.SetActive(true);
    }
    public void Bluff()
    {
        AiAnswer.text = "Компьютер говорит, что у него выпало " + (player == 12 ? 12 : Random.Range(player +1, 13)) + " очков";
    }
    public void SayTrue()
    {
        AiAnswer.text = "Компьютер говорит, что у него выпало " + ai + " очков";
    }
    public void PlayerWin()
    {
        if (player > ai)
        {
            Result.text = "Ты угадал!";
        }
        else if (player < ai)
            Result.text = "Ты не угадал! У компьютера было больше очков чем у тебя";
        else
            Result.text = "Ты не угадал! Ничья! У вас одинакове количество очков!";
        level.Win();
    }
    public void AiWin()
    {

        if (player < ai)
        {
            Result.text = "Ты угадал!";
        }
        else if (player > ai)
            Result.text = "Ты не угадал! У компьютера было меньше очков чем у тебя!";
        else Result.text = "Ты не угадал! Ничья! У вас одинаковое количество очков!";
        level.Win();
    }

    public void Draw()
    {

        if (player == ai)
        {
            Result.text = "Ты угадал!";
        }
        else if (player > ai)
            Result.text = "Ты не угадал! У компьютера было меньше очков чем у тебя!";
        else Result.text = "Ты не угадал! У компьютера было больше очков чем у тебя!";
        level.Win();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
