using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;

public class UltimatumLevels : MonoBehaviour, Levels
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
        TutorialText.text = "¬ этой мини-игре ты участвуешь в серии раундов, где делишь ресурсы (золото) с виртуальным соперником. ¬ каждом раунде ты предлагаешь, как разделить ресурс, а затем наблюдаешь за реакцией соперника Ч он может прин€ть или отклонить твоЄ предложение в зависимости от своей стратегии. ÷ель игры Ч максимизировать количество золота, которое ты получишь за несколько раундов, учитыва€ поведение соперника.\r\n“ебе придЄтс€ анализировать поведение соперника и адаптировать свою стратегию. Ќапример, если соперник отклон€ет жадные предложени€, попробуй предложить более справедливый раздел. ќднако будь осторожен: слишком жадные предложени€ могут привести к тому, что соперник будет отклон€ть их, а неумение адаптироватьс€ к его стратегии может снизить твой выигрыш. „тобы успешно играть, тебе понадоб€тс€ стратегическое мышление дл€ анализа поведени€ соперника, умение вести переговоры и находить компромиссы, а также эмоциональный интеллект дл€ понимани€ мотивов соперника.\r\n";
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
        WinPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        toLevelsPanel();
    }

    public void loadLevel1()
    {
        win = false;

        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level1.SetActive(true);
        Level1.GetComponent<UltimatumGame>().Start();
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
        Level2.GetComponent<UltimatumGame>().Start();
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
        Level3.GetComponent<UltimatumGame>().Start();
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
