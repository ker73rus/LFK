using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PrincessGame : MonoBehaviour
{
    public static List<List<int>> GenerateMap(int width, int height, int minCost, int maxCost)
    {
        List<List<int>> map = new List<List<int>>();

        for (int i = 0; i < height; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < width; j++)
            {
                row.Add(Random.Range(minCost, maxCost + 1));
            }
            map.Add(row);
        }

        map[height - 1][width - 1] = 0;
        return map;
    }

    public static int FindOptimalPath(List<List<int>> map)
    {
        int height = map.Count;
        int width = map[0].Count;

        int[,] cost = new int[height, width];
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                cost[i, j] = int.MaxValue;

        List<(int x, int y, int cost)> queue = new List<(int, int, int)>();

        queue.Add((0, 0, map[0][0]));
        cost[0, 0] = map[0][0];

        int[] dx = { 1, 0 };
        int[] dy = { 0, 1 };

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => a.cost.CompareTo(b.cost));

            var (x, y, currentCost) = queue[0];
            queue.RemoveAt(0);

            if (x == height - 1 && y == width - 1)
                return currentCost;

            for (int i = 0; i < 2; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                if (newX >= 0 && newX < height && newY >= 0 && newY < width)
                {
                    int newCost = currentCost + map[newX][newY];

                    if (newCost < cost[newX, newY])
                    {
                        cost[newX, newY] = newCost;
                        queue.Add((newX, newY, newCost));
                    }
                }
            }
        }

        return -1;
    }
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
            map = GenerateMap(size, size, 1, 10);
            max = FindOptimalPath(map);
        }
        else if (level == 3)
        {
            map = GenerateMap(size,size,4,10);
            max = FindOptimalPath(map);
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
            if (max <=0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.lose = true;
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
            if (max <=0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.lose = true;
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
            if (max <=0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.lose = true;

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
            if(max <=0)
            {
                Result.text = "Ты проиграл! Чудовище схватило тебя";
                Level.lose = true;

                Level.Win();
            }    
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(max > 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Up();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Down();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Left();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Right();
            }
        }
       
    }
}
