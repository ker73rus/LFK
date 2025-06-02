using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;

public class DiceLevels : MonoBehaviour, Levels
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
        TutorialText.text = "В этой мини-игре ты участвуешь в серии раундов и бросаешь виртуальные кубики, а затем пытаешься угадать, кто выиграл — ты или компьютер. Компьютер может использовать различные стратегии, включая блеф. После броска ты делаешь ставку на результат, пытаясь определить, блефует ли компьютер. Цель игры — правильно угадать, кто выиграл раунд, и сделать успешную ставку.\r\nТебе придётся анализировать результаты бросков и поведение компьютера, чтобы понять, блефует ли он. Например, если компьютер показывает высокий результат, но ты подозреваешь, что он блефует, можешь поставить на свою победу. Однако будь осторожен: неправильная оценка ситуации или слишком рискованные ставки могут привести к поражению. Чтобы успешно играть, тебе понадобятся логическое мышление для анализа результатов бросков, умение распознавать блеф и стратегии компьютера, а также навыки риск-менеджмента для принятия решений о ставках.\r\n";
        TutorialNext.SetActive(true);
        TutorialAsk.SetActive(false);
    }
    public void Win()
    {
        if(!lose)
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
        win = false;
        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level1.SetActive(true);
        Level1.GetComponent<DiceGame>().Start();
        Level2.SetActive(false);
        Level3.SetActive(false);
        LevelsPanel.SetActive(false);
        ToMiniGamesButton.SetActive(!story);
        ToLevelsButton.SetActive(!story);
    }
    public void loadLevel2()
    {
        win = false;
        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level2.SetActive(true);
        Level2.GetComponent<DiceGame>().Start();
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
        Level3.GetComponent<DiceGame>().Start();
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
