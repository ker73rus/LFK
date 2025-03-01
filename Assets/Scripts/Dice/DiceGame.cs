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
    [SerializeField]
    GameObject WinPanel;
    [SerializeField]
    int Turn = 1;
    [SerializeField]
    int AiWins = 0;
    [SerializeField]
    int PlayerWins = 0;
    [SerializeField]
    TextMeshProUGUI TurnText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Turn = 1;
        TurnText.text = "Раунд: " + Turn;
        AiWins = 0;
        PlayerWins = 0;
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
        if (player == 0)
        {
            player = Dice();
            Player.text = "Твои очки: \n" + player;
            StartCoroutine(AIDice());
        }
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
    IEnumerator NextTurn()
    {   
        yield return new WaitForSeconds(1);
        if (Turn < 3)
        {
            if(Turn == 1) Result.text = (PlayerWins > AiWins ? "Раунд за тобой" : "Раунд за компьютером");
            else
            {
                if (Result.text == "Раунд за тобой") Result.text = (PlayerWins > AiWins ? "Ты победил" : "Раунд за компьютером");
                else Result.text = (PlayerWins >= AiWins ? "Раунд за тобой" : "Ты проиграл!");
            }
            if (Turn == 2 && (PlayerWins == 2 || AiWins == 2)) { level.Win(); }
            else
            {
                WinPanel.SetActive(true);
                yield return new WaitForSeconds(2);
                Turn++;
                TurnText.text = "Раунд: " + Turn;   
                player = 0;
                ai = 0;
                rates.SetActive(false);
                AiAnswer.text = string.Empty;
                Player.text = string.Empty;
                WinPanel.SetActive(false);
            }
            

        }
        else if (PlayerWins >= 2)
        {
            Result.text = "Ты победил!";
            level.Win();
        }
        else {
            Result.text = "Ты проиграл!";
            level.Win();
        }
    }
 
    public void PlayerWin()
    {
        if (player > ai)
        {
            PlayerWins++;
        }
        else AiWins++;
        StartCoroutine(NextTurn());

    }
    public void AiWin()
    {

        if (player < ai)
        {
            PlayerWins++;
        }
        else AiWins++;
        StartCoroutine(NextTurn());

    }

    public void Draw()
    {

        if (player == ai)
        {
            PlayerWins++;
        }
        else 
            AiWins++;
        StartCoroutine(NextTurn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
