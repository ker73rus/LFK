using System.Collections;
using UnityEngine;

public class DiceLevels : MonoBehaviour
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

    public void Start()
    {
        toLevelsPanel();
    }
    public void Win()
    {
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
        Level1.GetComponent<DiceGame>().Start();
        Level2.SetActive(false);
        Level3.SetActive(false);
        LevelsPanel.SetActive(false);
    }
    public void loadLevel2()
    {
        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level2.SetActive(true);
        Level2.GetComponent<DiceGame>().Start();
        Level1.SetActive(false);
        Level3.SetActive(false);
        LevelsPanel.SetActive(false);
    }
    public void loadLevel3()
    {
        WinPanel.SetActive(false);
        GamePanel.SetActive(true);
        Level3.SetActive(true);
        Level3.GetComponent<DiceGame>().Start();
        Level1.SetActive(false);
        Level2.SetActive(false);
        LevelsPanel.SetActive(false);
    }
    public void toLevelsPanel()
    {
        WinPanel.SetActive(false);
        GamePanel.SetActive(false);
        Level3.SetActive(false);
        Level1.SetActive(false);
        Level2.SetActive(false);
        LevelsPanel.SetActive(true);
    }
}
