using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaterLevel : MonoBehaviour, Levels
{
    [SerializeField]
    GameObject LevelsPanel;
    [SerializeField]
    GameObject GamePanel;
    [SerializeField]
    GameObject Level1;
    [SerializeField]
    GameObject Level2;
    [SerializeField]
    GameObject Level3;
    [SerializeField]
    GameObject WinPanel;
    [SerializeField]
    GameObject ToMiniGamesButton;
    [SerializeField]
    GameObject ToLevelsButton;
    [SerializeField]
    GameObject TutorialPanel;
    [SerializeField]
    GameObject TutorialAsk;
    [SerializeField]
    GameObject TutorialNext;
    [SerializeField]
    TextMeshProUGUI TutorialText;
    public bool win { get; set; } = false;
    public bool story { get; set; } = false;
    public bool lose { get; set; } = false;
    public bool tutor { get; set; } = false;


    public void Begin()
    {
        TutorialPanel.SetActive(true);
        TutorialText.text = "Вы уже умеете играть?";
        TutorialAsk.SetActive(true);
        TutorialNext.SetActive(false);
        LevelsPanel.SetActive(false);
        GamePanel.SetActive(false);
    }
    public void UnderstandTutorial()
    {
        TutorialPanel.SetActive(false);
        toLevelsPanel();
        ToMiniGamesButton.SetActive(!story);
        ToLevelsButton.SetActive(!story);
    }
    public void IKnow()
    {
        UnderstandTutorial();
    }
    public void IDontKnow()
    {
        TutorialText.text = "В этой мини-игре тебе нужно отмерить определённое количество воды, переливая её между ёмкостями различного объёма. Ты можешь наполнять и опорожнять ёмкости, а также переливать воду из одной ёмкости в другую. Цель игры — получить точно заданное количество воды в одной из ёмкостей.\r\nТебе придётся планировать последовательность действий для достижения нужного количества воды. Например, если у тебя есть ёмкости на 5 и 3 литра, а нужно отмерить 4 литра, ты можешь наполнить большую ёмкость, перелить воду в меньшую, а затем снова наполнить большую. Однако будь осторожен: неправильная последовательность действий может привести к потере воды или невозможности достичь нужного количества. Чтобы успешно играть, тебе понадобятся логическое мышление для планирования последовательности действий, умение решать задачи методом проб и ошибок, а также внимательность к деталям, чтобы не пропустить нужную комбинацию.\r\n";
        TutorialNext.SetActive(true);
        TutorialAsk.SetActive(false);
    }
    public void Win()
    {
        win = true;
        if (!story)
            StartCoroutine(WinC());
    }
    IEnumerator WinC()
    {
        yield return new WaitForSeconds(1);
        WinPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        toLevelsPanel();
    }

    public void loadLevel1()
    {
        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level1.SetActive(true);
        Level1.GetComponent<WaterGame>().Start();
        Level2.SetActive(false);
        Level3.SetActive(false);
        LevelsPanel.SetActive(false);
        win = false;
        ToMiniGamesButton.SetActive(!story);
        ToLevelsButton.SetActive(!story);
    }
    public void loadLevel2()
    {
        win = false;
        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level2.SetActive(true);
        Level2.GetComponent<WaterGame>().Start();
        Level1.SetActive(false);
        Level3.SetActive(false);
        LevelsPanel.SetActive(false);
        ToMiniGamesButton.SetActive(!story);
        ToLevelsButton.SetActive(!story);
    }
    public void loadLevel3()
    {
        win = false;
        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level3.SetActive(true);
        Level3.GetComponent<WaterGame>().Start();
        Level1.SetActive(false);
        Level2.SetActive(false);
        LevelsPanel.SetActive(false);
        ToMiniGamesButton.SetActive(!story);
        ToLevelsButton.SetActive(!story);
    }
    public void toLevelsPanel()
    {
        win = false;
        WinPanel.SetActive(false);
        GamePanel.SetActive(false);
        Level3.SetActive(false);
        Level1.SetActive(false);
        Level2.SetActive(false);
        LevelsPanel.SetActive(true);
        ToMiniGamesButton.SetActive(!story);
        ToLevelsButton.SetActive(!story);
    }
}
