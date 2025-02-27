using UnityEngine;

public class HanoiLevels : MonoBehaviour
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

    public void loadLevel1()
    {
        GamePanel.SetActive(true);
        Level1.SetActive(true);
        Level1.GetComponent<HanoiGame>().Start();
        Level2.SetActive(false);
        Level3.SetActive(false);
        LevelsPanel.SetActive(false);
    }
    public void loadLevel2()
    {
        GamePanel.SetActive(true);
        Level2.SetActive(true);
        Level2.GetComponent<HanoiGame>().Start();
        Level1.SetActive(false);
        Level3.SetActive(false);
        LevelsPanel.SetActive(false);
    }
    public void loadLevel3()
    {
        GamePanel.SetActive(true);
        Level3.SetActive(true);
        Level3.GetComponent<HanoiGame>().Start();
        Level1.SetActive(false);
        Level2.SetActive(false);
        LevelsPanel.SetActive(false);
    }
    public void toLevelsPanel()
    {
        GamePanel.SetActive(false);
        Level3.SetActive(false);
        Level1.SetActive(false);
        Level2.SetActive(false);
        LevelsPanel.SetActive(true);
    }
}
