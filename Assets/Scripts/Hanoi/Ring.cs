using UnityEngine;

public class Ring : MonoBehaviour
{
    public int id = 0;
    [SerializeField]
    HanoiGame HanoiGame;
    public int curTower;

    public void OnCLick()
    {
        HanoiGame.SelectRing(gameObject);
    }
}
