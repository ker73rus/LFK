using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject MenuPanel;
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
    public void MainGame()
    {
        MainGamePanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void PrincessGame()
    {
        MiniGamesPanel.SetActive(false);
        PrincessGamePanel.SetActive(true);
    }
    
    public void UltimatumGame()
    {
        UltimatumGamePanel.SetActive(true );
        MiniGamesPanel.SetActive(false );
    }

    public void DiceGame()
    {
        DiceGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false );
    }

    public void LabyrinthGame()
    {
        LabyrinthGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false );   
    }

    public void WaterGame()
    {
        WaterGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false);
    }

    public void CodeGame()
    {
        CodeGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false);
    }
    public void HanoiGame()
    {
        HanoiGamePanel.SetActive(true);
        MiniGamesPanel.SetActive(false);
    }
    public void MiniGames()
    {
        MenuPanel.SetActive(false);
        MiniGamesPanel.SetActive(true);
    }
    public void Menu() {
        MiniGamesPanel.SetActive(false);
        MainGamePanel.SetActive(false);
        MenuPanel.SetActive(true);   
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
        MiniGamesPanel.SetActive(true);
    }
}
