using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PrincessGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    (int x, int y) player = (0, 0);
    [SerializeField]
    RectTransform Player;
    [SerializeField]
    int size = 5;
    [SerializeField]
    int level = 1;
    [SerializeField]
    int max = 15;
    List<List<int>> map =  new () {
    new List<int> { 1, 3, 2, 5, 8 },
    new List<int> { 2, 1, 3, 4, 7 },
    new List<int> { 4, 2, 1, 3, 6 },
    new List<int> { 7, 5, 2, 2, 4 },
    new List<int> { 9, 6, 3, 1, 0 }
};
    [SerializeField]
    PrincessLevels Level;
    [SerializeField]
    TextMeshProUGUI Time;
    [SerializeField]
    GameObject Map;
    [SerializeField]
    GameObject Cell;
    [SerializeField]
    TextMeshProUGUI Result;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Map.GetComponentsInChildren<Cell>().Where(x => x.CompareTag("Respawn")).Select(x => x).ToList().ForEach(x => Destroy(x.gameObject));
        player = (0, 0);
        Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
        if (level == 2)
        {
            map = new()
            {
                new List<int> { 1, 2, 3, 5, 6, 7, 9 },
                new List<int> { 2, 1, 4, 6, 8, 9, 10 },
                new List<int> { 3, 2, 2, 4, 5, 7, 8 },
                new List<int> { 5, 4, 3, 2, 3, 4, 6 },
                new List<int> { 7, 6, 4, 3, 2, 2, 3 },
                new List<int> { 8, 7, 6, 5, 4, 1, 2 },
                new List<int> { 9, 8, 7, 6, 5, 3, 0 }
            };
            max = 22;
        }
        else if (level == 3)
        {
            map =  new List<List<int>> {
                new List<int> { 10, 9, 8, 7, 6, 5, 4 },
                new List<int> { 10, 10, 7, 6, 5, 4, 3 },
                new List<int> { 10, 10, 6, 5, 4, 3, 2 },
                new List<int> { 10, 10, 5, 4, 3, 2, 1 },
                new List<int> { 10, 10, 4, 3, 2, 1, 1 },
                new List<int> { 10, 10, 3, 2, 1, 1, 1 },
                new List<int> { 0, 1, 2, 3, 4, 5, 6 }
            };
            max = 45;
        }
        else
        {
            max = 15;
        }
        Time.text = "Осталось времени:" + max;
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++)
            {
                GameObject t = Instantiate(Cell, Map.transform);
                t.GetComponent<RectTransform>().localPosition = new Vector3(-220 + (110 *j), 220 + (-110 * i));
                t.GetComponent<Cell>().SetNum(map[i][j]);
            }
        
        }


    }
    public void Up()
    {
        if (player.y - 1 >= 0)
        {
            if (map[player.y - 1][player.x] == 0)
            {
                Result.text = "Ты победил! Чудовище осталось ни с чем";
                Level.Win();
                player.y -= 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
            else {
                player.y -= 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
                max-= map[player.y][player.x];
            }
            Time.text = "Осталось времени:" + max;
            if (max < 0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.Win();
            }
        }
    }
    public void Down()
    {
        if (player.y + 1 < size)
        {
            if (map[player.y + 1][player.x] == 0)
            {

                Result.text = "Ты победил! Чудовище осталось ни с чем";
                Level.Win();
                player.y += 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
            else
            {
                player.y += 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x),220 + (-110 * player.y));
                max -= map[player.y][player.x];
            }
            Time.text = "Осталось времени:" + max;
            if (max < 0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.Win();
            }
        }
    }
    public void Left()
    {
        if (player.x - 1 >= 0)
        {
            if (map[player.y][player.x - 1] == 0)
            {

                Result.text = "Ты победил! Чудовище осталось ни с чем";
                Level.Win();
                player.x -= 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
            else
            {
                player.x -= 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
                max -= map[player.y][player.x];
            }
            Time.text = "Осталось времени:" + max;
            if (max < 0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.Win();
            }
        }
    }
    public void Right()
    {
        if (player.x + 1 < size)
        {
            if (map[player.y][player.x + 1] == 0)
            {

                Result.text = "Ты победил! Чудовище осталось ни с чем";
                Level.Win();
                player.x += 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
            else
            {
                player.x += 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
                max -= map[player.y][player.x];
            }
            Time.text = "Осталось времени:" + max;
            if(max < 0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.Win();
            }    
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
