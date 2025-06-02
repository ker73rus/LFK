using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;

public class PrincessLevels : MonoBehaviour, Levels
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
        TutorialText.text = "¬ы уже умеете играть?";
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
        TutorialText.text = "¬ этой мини-игре ты управл€ешь принцессой, которой нужно выбратьс€ из замка, избега€ встречи с чудовищем. «амок представлен сеткой комнат, кажда€ из которых имеет свою \"стоимость\" Ч это может быть врем€ прохождени€ или риск встречи с чудовищем. “ы можешь двигатьс€ вверх, вниз, влево или вправо, выбира€ направление в каждой комнате. „тобы выиграть, тебе нужно найти кратчайший и безопасный путь до выхода, минимизиру€ риск встречи с чудовищем и затраченное врем€.\r\n“ебе придЄтс€ планировать маршрут, учитыва€ стоимость каждой комнаты и риск встречи с чудовищем. ќднако будь осторожен: выбор комнат с высоким риском встречи с чудовищем иц неэффективное планирование маршрута могут привести к поражению. „тобы успешно пройти игру, тебе понадоб€тс€ логическое мышление дл€ планировани€ маршрута, умение оценивать риски и принимать решени€ в услови€х неопределенности, а также пам€ть дл€ запоминани€ пройденных комнат и их характеристик.\r\n";
        TutorialNext.SetActive(true);
        TutorialAsk.SetActive(false);
    }

    public void Win()
    {
        if(!lose) 
            win = true;
        if(!story)
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
        Level1.GetComponent<PrincessGame>().Start();
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
        Level2.GetComponent<PrincessGame>().Start();
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
        Level3.GetComponent<PrincessGame>().Start();
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
    }
}
