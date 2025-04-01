using Assets.Scripts;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject MenuPanel;
    [SerializeField]
    GameObject StoryPanel;
    [SerializeField]
    GameObject AboutPanel;
    [SerializeField]
    GameObject MiniGamesPanel;
    [SerializeField]
    GameObject MainGamePanel;
    [SerializeField]
    GameObject PrincessGamePanel;
    [SerializeField]
    GameObject UltimatumGamePanel;
    [SerializeField]
    GameObject DiceGamePanel;
    [SerializeField]
    GameObject LabyrinthGamePanel;
    [SerializeField]
    GameObject WaterGamePanel;
    [SerializeField]
    GameObject CodeGamePanel;
    [SerializeField]
    GameObject HanoiGamePanel;
    [SerializeField]
    GameObject LoseStoryPanel;
    [SerializeField]
    GameObject toMenu;
    public void MainGame()
    {
        MainGamePanel.SetActive(true);
        MainGamePanel.GetComponent<MainGame>().story = false;
        MainGamePanel.GetComponent<MainGame>().Begin();
        MenuPanel.SetActive(false);
        MiniGamesPanel.SetActive(false);
        AboutPanel.SetActive(false);
        PrincessGamePanel.SetActive(false);
        UltimatumGamePanel.SetActive(false);
        DiceGamePanel.SetActive(false);
        LabyrinthGamePanel.SetActive(false);
        WaterGamePanel.SetActive(false);
        CodeGamePanel.SetActive(false);
        HanoiGamePanel.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StoryGame()
    {
        MenuPanel.SetActive(false);
        StoryPanel.SetActive(true);
        StoryPanel.GetComponent<StoryGame>().Begin();
        toMenu.SetActive(true);
    }
    public void PrincessGame()
    {
        MiniGamesPanel.SetActive(false);
        PrincessGamePanel.SetActive(true);
        PrincessGamePanel.GetComponent<Levels>().story = false;
        PrincessGamePanel.GetComponent<Levels>().Begin();
    }

    public void UltimatumGame()
    {
        UltimatumGamePanel.SetActive(true );
        MiniGamesPanel.SetActive(false );
        UltimatumGamePanel.GetComponent<Levels>().story = false;
        UltimatumGamePanel.GetComponent<Levels>().Begin();

    }

    public void DiceGame()
    {
        DiceGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false );
        DiceGamePanel.GetComponent<Levels>().story = false;
        DiceGamePanel.GetComponent<Levels>().Begin();

    }

    public void LabyrinthGame()
    {
        LabyrinthGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false );
        LabyrinthGamePanel.GetComponent<Levels>().story = false;
        LabyrinthGamePanel.GetComponent<Levels>().Begin();

    }

    public void WaterGame()
    {
        WaterGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false);
        WaterGamePanel.GetComponent<Levels>().story = false;
        WaterGamePanel.GetComponent<Levels>().Begin();

    }

    public void CodeGame()
    {
        CodeGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false);
        CodeGamePanel.GetComponent<Levels>().story = false;
        CodeGamePanel.GetComponent<Levels>().Begin();

    }
    public void HanoiGame()
    {
        HanoiGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false);
        HanoiGamePanel.GetComponent<Levels>().story = false;
        HanoiGamePanel.GetComponent<Levels>().Begin();

    }
    public void MiniGames()
    {
        MenuPanel.SetActive(false);
        MiniGamesPanel.SetActive(true);
    }
    public void Menu() {
        AboutPanel.SetActive(false);
        MiniGamesPanel.SetActive(false);
        MainGamePanel.SetActive(false);
        MenuPanel.SetActive(true);
        toMenu.SetActive(false);
        StoryPanel.SetActive(false);
        MiniGamesPanel.SetActive(false);
        AboutPanel.SetActive(false);
        PrincessGamePanel.SetActive(false);
        UltimatumGamePanel.SetActive(false);
        DiceGamePanel.SetActive(false);
        LabyrinthGamePanel.SetActive(false);
        WaterGamePanel.SetActive(false);
        CodeGamePanel.SetActive(false);
        HanoiGamePanel.SetActive(false);
        LoseStoryPanel.SetActive(false);
    }
    public void BackToMiniGames()
    {
        PrincessGamePanel.SetActive(false);
        UltimatumGamePanel.SetActive(false);
        DiceGamePanel.SetActive(false);
        LabyrinthGamePanel.SetActive(false);
        WaterGamePanel.SetActive(false);
        CodeGamePanel.SetActive(false);
        HanoiGamePanel.SetActive(false);
        MainGamePanel.SetActive(false) ;
        MiniGamesPanel.SetActive(true);
    }
    public void About()
    {
        AboutPanel.SetActive(true);
        AboutPanel.GetComponent<AboutPaging>().Start();
        MenuPanel.SetActive(false );
    }
}
