using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LabyrinthGame : MonoBehaviour
{
    [SerializeField]
    (int x, int y) player = (0, 0);
    [SerializeField]
    RectTransform Player;
    [SerializeField]
    int size = 5;
    [SerializeField]
    int level = 1;
    List<List<int>> map = new()
    {
        new List<int>(){ 2,1,0,1,0 },
        new List<int>(){ 0,1,0,0,0 },
        new List<int>(){ 0,1,0,1,0 },
        new List<int>(){ 0,1,0,1,0 },
        new List<int>(){ 0,0,0,1,3 }
    };
    [SerializeField]
    GameObject mark;
    [SerializeField]
    List<GameObject> marks = new();
    [SerializeField]
    GameObject Map;
    [SerializeField]
    LabyrinthLevel Level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        foreach (var item in marks)
        {
            Destroy(item);
        }
        marks.Clear();
        player = (0, 0);
        Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
        if (level == 2)
        {
            map = new()
            {
                new List<int>(){ 2,0,0,1,0,0,0 },
                new List<int>(){ 0,1,0,1,0,1,0 },
                new List<int>(){ 1,1,0,1,0,1,0 },
                new List<int>(){ 1,0,0,0,0,1,1 },
                new List<int>(){ 0,0,1,1,0,0,0 },
                new List<int>(){ 1,0,1,0,0,1,0 },
                new List<int>(){ 1,0,1,0,0,1,3 },
            };
        }
        else if (level == 3)
        {
            map = new()
            {
                new List<int>(){ 2,0,0,0,1,0,0, 1 },
                new List<int>(){ 0,1,1,1,1,1,0, 1 },
                new List<int>(){ 0,0,0,0,0,1,0, 1 },
                new List<int>(){ 1,1,0,1,0,0,0, 0 },
                new List<int>(){ 0,0,0,1,0,1,0, 1 },
                new List<int>(){ 0,1,1,1,0,1,0, 1 },
                new List<int>(){ 0,1,0,0,0,1,0, 1 },
                new List<int>(){ 3,1,0,1,0,1,0, 0 },
            };
        }
    }
    public void Up()
    {
        if (player.y - 1 >= 0)
        {
            if (map[player.y - 1][player.x] == 3)
                Level.Win();
            else if (map[player.y - 1][player.x] != 1)
            {
                player.y -= 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
        }
    }
    public void Down()
    {
        if (player.y + 1 < size)
        {
            if (map[player.y + 1][player.x] == 3)
                Level.Win();
            else if (map[player.y + 1][player.x] != 1)
            {
                player.y += 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
        }
    }
    public void Left()
    {
        if (player.x - 1 >= 0)
        {
            if (map[player.y][player.x - 1] == 3)
                Level.Win();
            else if (map[player.y][player.x - 1] != 1)
            {
                player.x -= 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
        }
    }
    public void Right()
    {
        if (player.x + 1 < size)
        {
            if (map[player.y][player.x + 1] == 3)
                Level.Win();
            else if (map[player.y][player.x + 1] != 1)
            {
                player.x += 1;
                Player.localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
            }
        }
    }
    public void Mark()
    {
        if (map[player.y][player.x] == 0)
        {
            marks.Add(Instantiate(mark, parent: Map.transform));
            marks.Last().GetComponent<RectTransform>().localPosition = new Vector3(-220 + (110 * player.x), 220 + (-110 * player.y));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Down();
        }
        else if( Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Mark();
        }
    }
}
