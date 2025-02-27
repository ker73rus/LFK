using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Level
{
    public Level(string name, int id, int left, int right, int need, Func<int, int> lFMove, string lfText, Func<int, int> lSMove, string lsText, Func<int, int> rFMove, string rfText, Func<int, int> rSMove, string rsText)
    {
        Name = name;
        this.id = id;
        this.left = left;
        this.right = right;
        this.need = need;
        this.lsText = lsText;
        this.lfText = lfText;
        this.rsText = rsText;
        this.rfText = rfText;
        LFMove = lFMove;
        LSMove = lSMove;
        RFMove = rFMove;
        RSMove = rSMove;
    }
    public Level(string name, int id, int left, int right, int need, Func<(int, int), (int, int)> fMove, string fText, Func<(int, int), (int, int)> sMove, string sText, Func<(int, int), (int, int)> tMove, string tText)
    {
        Name = name;
        this.id = id;
        this.left = left;
        this.right = right;
        this.need = need;
        this.sText = lsText;
        this.fText = lfText;
        FMove = fMove;
        SMove = sMove;
        TMove = tMove;
        this.tText = tText;
        this.fText = fText;
        this.sText = sText;
    }
    public Level(string name, int id, int left, int need, Func<int, int> lFMove, string lfText, Func<int, int> lSMove, string lsText)
    {
        Name = name;
        this.id = id;
        this.left = left;
        this.need = need;
        LFMove= lFMove;
        LSMove= lSMove;
        this.lsText = lsText;
        this.lfText = lfText;
    }
    public string Name { get; }
    public int id { get; }
    public int left { get; }
    public int right { get;}
    public int need { get; }
    public string lfText { get; }
    public string rfText {  get; }
    public string lsText { get; }
    public string rsText { get; }
    public string fText { get; }
    public string sText { get; }
    public string tText { get; }
    public Func<int, int> LFMove {  get; }
    public Func<int, int> LSMove {  get; }
    public Func<int, int> RFMove {  get; }
    public Func<int, int> RSMove {  get; }
    public Func<(int, int), (int, int)> FMove { get; }
    public Func<(int,int), (int, int)> SMove { get; }
    public Func<(int,int), (int, int)> TMove { get; }

}
class GameState
{
    public int Left { get; }
    public int Right { get; }
    public Level Level { get; }
    public bool IsPlayerTurn { get; }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if(!(obj is GameState))
            return false;
        GameState other = obj as GameState;
        if(other.Left == Left && other.Right == Right && other.IsPlayerTurn == IsPlayerTurn)
            return true;
        return base.Equals(obj);
    }


    public GameState(int left, int right, Level level, bool isPlayerTurn)
    {
        Left = left;
        Right = right;
        Level = level;
        IsPlayerTurn = isPlayerTurn;
    }

    public bool IsWinningMove()
    {
        if(Level.id == 1 && Left>= Level.need)
            return true;
        else if(Level.id >= 2 && (Left+Right >= Level.need))
            return true;
        else return false;
    }

    public bool CanOpponentWinNextMove()
    {
        if (!IsPlayerTurn)
        {
            if (Level.id == 1)
                return Level.LFMove(Left) >= Level.need || Level.LSMove(Left) >= Level.need;
            else if (Level.id == 2)
                return (Level.LFMove(Left) + Right >= Level.need || Level.LSMove(Left) + Right >= Level.need ||
                             Left + Level.RFMove(Right) >= Level.need || Left + Level.RSMove(Right) >= Level.need);
            else if (Level.id == 3)
                return (Level.FMove((Left, Right)).Item1 + Level.FMove((Left, Right)).Item2 >= Level.need) ||
                    (Level.SMove((Left, Right)).Item1 + Level.SMove((Left, Right)).Item2 >= Level.need) ||
                    (Level.FMove((Right, Left)).Item1 + Level.FMove((Right, Left)).Item2 >= Level.need) ||
                    (Level.SMove((Right, Left)).Item1 + Level.SMove((Right, Left)).Item2 >= Level.need);
            else return false;

        }
        else return false;
    }

    public override string ToString()
    {
        return Level.id == 1 ? $"({Left})" : $"({Left};{Right})";
    }

    public List<GameState> GenerateMoves()
    {
        List<GameState> moves = new List<GameState>();

        if (Level.id == 1)
        {
            int newLeft1 = Level.LFMove(Left);
            int newLeft2 = Level.LSMove(Left);
            if (newLeft1 != Left) moves.Add(new GameState(newLeft1, Right, Level, !IsPlayerTurn));
            if (newLeft2 != Left) moves.Add(new GameState(newLeft2, Right, Level, !IsPlayerTurn));
        }
        else if (Level.id == 2)
        {
            int newLeft1 = Level.LFMove(Left);
            int newLeft2 = Level.LSMove(Left);
            int newRight1 = Level.RFMove(Right);
            int newRight2 = Level.RSMove(Right);
            if (newLeft1 != Left) moves.Add(new GameState(newLeft1, Right, Level, !IsPlayerTurn));
            if (newLeft2 != Left) moves.Add(new GameState(newLeft2, Right, Level, !IsPlayerTurn));
            if (newRight1 != Right) moves.Add(new GameState(Left, newRight1, Level, !IsPlayerTurn));
            if (newRight2 != Right) moves.Add(new GameState(Left, newRight2, Level, !IsPlayerTurn));
        }
        else if (Level.id == 3)
        {
            (int newLeft1, int newRight1) = Level.FMove((Left, Right));
            (int newLeft2, int newRight2) = Level.SMove((Left, Right));
            (int newRight3, int newLeft3) = Level.FMove((Right, Left));
            (int newRight4, int newLeft4) = Level.SMove((Right, Left));
            moves.Add(new GameState(newLeft1, newRight1, Level, !IsPlayerTurn));
            moves.Add(new GameState(newLeft2, newRight2, Level, !IsPlayerTurn));
            moves.Add(new GameState(newLeft3, newRight3, Level, !IsPlayerTurn));
            moves.Add(new GameState(newLeft4, newRight4, Level, !IsPlayerTurn));
        }
        return moves;
    }
}

class GameSolver
{
    public List<GameState> FindShortestWinningStrategy(GameState startState)
    {
        Queue<List<GameState>> queue = new Queue<List<GameState>>();
        HashSet<(int, int, bool)> visited = new HashSet<(int, int, bool)>();

        queue.Enqueue(new List<GameState> { startState });
        visited.Add((startState.Left, startState.Right, startState.IsPlayerTurn));

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            GameState lastState = path.Last();

            foreach (var move in lastState.GenerateMoves())
            {
                if (move.IsWinningMove() && move.IsPlayerTurn)
                {
                    path.Add(move);
                    return path;
                }
            }

            // ���� ����� 1 ����� ����� �������� � �������� ���� ����
            foreach (var move in lastState.GenerateMoves())
            {
                if (move.IsWinningMove() && lastState.IsPlayerTurn)
                {
                    path.Add(move);
                    return path;
                }
            }

            List<GameState> safeMoves = new List<GameState>();

            foreach (var move in lastState.GenerateMoves())
            {
                if (!move.IsPlayerTurn && move.CanOpponentWinNextMove())
                    continue;
                safeMoves.Add(move);
            }

            if (safeMoves.Count == 0) continue;

            foreach (var move in safeMoves)
            {
                if (!visited.Contains((move.Left, move.Right, move.IsPlayerTurn)))
                {
                    visited.Add((move.Left, move.Right, move.IsPlayerTurn));
                    var newPath = new List<GameState>(path) { move };
                    queue.Enqueue(newPath);
                }
            }
        }

        return new List<GameState>();
    }

    public string GetTextFromPath(List<GameState> path)
    {
        if (path.Count == 0) return "�� �������� � ����������� ���������: ��� �� �� �� ������, �������� ��� ��������.";

        return string.Join(" -> ", path.Select((state, index) =>
            index == path.Count - 1 ? $"{{{state.ToString().Trim('(', ')')}}}" :
            index % 2 == 0 ? state.ToString() : $"[{state.ToString().Trim('(', ')')}]"));
    }
    
}





public class MainGame : MonoBehaviour
{
    [SerializeField]
    GameObject TutorialPanel;
    [SerializeField]
    GameObject GamePanel;
    [SerializeField]
    GameObject AnalysisPanel;
    [SerializeField]
    GameObject TutorialText;
    [SerializeField]
    GameObject LevelsPanel;
    [SerializeField]
    TextMeshProUGUI resultText;

    [SerializeField]
    TextMeshProUGUI target;
    [SerializeField]
    TextMeshProUGUI summ;
    [SerializeField]
    TextMeshProUGUI rightText;
    [SerializeField]
    TextMeshProUGUI leftText;
    [SerializeField]
    GameObject LevelButton;

    [SerializeField]
    GameObject OnePanel;
    [SerializeField]
    GameObject TwoPanel;
    [SerializeField]
    GameObject ThreePanel;
    [SerializeField]
    Button OneFMoveButton;
    [SerializeField]
    Button OneSMoveButton;
    [SerializeField]
    Button TwoFLMoveButton;
    [SerializeField]
    Button TwoFRMoveButton;
    [SerializeField]
    Button TwoSLMoveButton;
    [SerializeField]
    Button TwoSRMoveButton;
    [SerializeField]
    Button ThreeFLMoveButton;
    [SerializeField]
    Button ThreeFRMoveButton;
    [SerializeField]
    Button ThreeSLMoveButton;
    [SerializeField]
    Button ThreeSRMoveButton;
    [SerializeField]
    Button ThreeTRMoveButton;
    [SerializeField]
    Button ThreeTLMoveButton;

    [SerializeField]
    List<List<GameState>> turnStories = new();


    [SerializeField]
    int needScore = 77;

    [SerializeField]
    Level level;

    List<Level> levels = new() {
        new Level("��������", 1, 30,31, (left) => { return left+=1; },"+1", (left) => {return left+=10; },"+10") ,
        new Level("������", 1, 30,31, (left) => { return left+=1; },"+1", (left) => {return left+=10; }, "+10") ,
        new Level("�������", 2,7,69,77, (left) => { return left+=1; },"+1", (left) => { return left *= 2; },"*2", (right) => { return right+=1; }, "+1",(right) => { return right *= 2; }, "*2"),
        new Level("�������+", 2,3,57,61, (left) => { return left+=1; },"+1", (left) => { return left *= 4; }, "*4",(right) => { return right+=1; },"+1", (right) => { return right *= 4; }, "*4"),
        new Level("�������", 3, 8, 32, need: 41,(lr) => {return (lr.Item1+1,lr.Item2+2); },"+1 ; +2", 
            (lr) => {return (lr.Item1*2,lr.Item2); }, "*2",(_) => { return (_); }, "null" ),
        new Level("�������+", 2,8,32,41, (left) => { return left+=1; }, "+1",(left) => { return left *= 4; }, "*4", (right) => { return right+=1; },"+1", (right) => { return right *= 4; }, "*4"),

    };

    [SerializeField]
    int right = 3;
    [SerializeField]
    int left = 5;

    (int prevLeft, int prevRight) p = (0,0);

    bool Tutorial = true;

    public bool player = true;
    [SerializeField]
    int miss = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LevelsPanel.activeSelf)
        {
            LoadLevels();
        }
        else if (Tutorial) { 
            TutorialPanel.SetActive(true);
            TutorialText.SetActive(true);
        }
        else if (!Tutorial)
        {
            TutorialPanel.SetActive(false);
            GamePanel.SetActive(false);
            AnalysisPanel.SetActive(false);
            LevelsPanel.SetActive(true);
            LoadLevels();
        }
    }

    void LoadLevels()
    {
        float i = -3.5f;
        float j = 1;   
        foreach (var level in levels)
        {
            GameObject obj = Instantiate(LevelButton,new Vector3(i,j), Quaternion.identity,LevelsPanel.transform);
            if (i < 3.5f)
                i+=3.5f;
            else
            {
                j-=2;
                i = -3.5f;
            }

            obj.GetComponent<Button>().onClick.AddListener(() => { LoadLevel(levels.IndexOf(level)); });
            obj.GetComponentInChildren<TextMeshProUGUI>().text = level.Name;

        }
    }
    void LoadLevel(int index)
    {
        level = levels[index];
        player = true;
        LevelsPanel.GetComponentsInChildren<Button>().Where(x => x.CompareTag("Respawn")).Select(x => x).ToList().ForEach( x => Destroy(x.gameObject));
        switch (level.id) {
            case 1:
                needScore = level.need;
                left = Random.Range(1, level.left);
                right = 0;
                LevelsPanel.SetActive(false);
                GamePanel.SetActive(true);
                OnePanel.SetActive(true);
                TwoPanel.SetActive(false);
                ThreePanel.SetActive(false);
                OneFMoveButton.onClick.RemoveAllListeners();
                OneFMoveButton.onClick.AddListener(() => { LeftFirstMove(level.LFMove); Visual(); });
                OneFMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.lfText;
                OneSMoveButton.onClick.RemoveAllListeners();
                OneSMoveButton.onClick.AddListener(() => { LeftSecondMove(level.LSMove); Visual(); });
                OneSMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.lsText;
                leftText = OnePanel.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.CompareTag("One")).First();
                Visual();
                break;
            case 2:
                needScore = level.need;
                left = level.left;
                right = Random.Range(1, level.right);
                LevelsPanel.SetActive(false);
                GamePanel.SetActive(true);
                OnePanel.SetActive(false);
                TwoPanel.SetActive(true);
                ThreePanel.SetActive(false);
                TwoFLMoveButton.onClick.RemoveAllListeners();
                TwoFLMoveButton.onClick.AddListener(() => { LeftFirstMove(level.LFMove); Visual(); });
                TwoFLMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.lfText;

                TwoSLMoveButton.onClick.RemoveAllListeners();
                TwoSLMoveButton.onClick.AddListener(() => { LeftSecondMove(level.LSMove); Visual(); });
                TwoSLMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.lsText;

                TwoFRMoveButton.onClick.RemoveAllListeners();
                TwoFRMoveButton.onClick.AddListener(() => { RightFirstMove(level.RFMove); Visual(); });
                TwoFRMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.rfText;
                
                TwoSRMoveButton.onClick.RemoveAllListeners();
                TwoSRMoveButton.onClick.AddListener(() => { RightSecondMove(level.RSMove); Visual(); });
                TwoSRMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.rsText;
                leftText = TwoPanel.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.CompareTag("TwoLeft")).First();
                rightText = TwoPanel.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.CompareTag("TwoRight")).First();
                Visual();
                break;
            case 3:
                needScore = level.need;
                left = level.left;
                right = Random.Range(1, level.right);
                LevelsPanel.SetActive(false);
                GamePanel.SetActive(true);
                OnePanel.SetActive(false);
                TwoPanel.SetActive(false);
                ThreePanel.SetActive(true);
                ThreeFLMoveButton.onClick.RemoveAllListeners();
                ThreeFLMoveButton.onClick.AddListener(() => { LeftFirstMove(level.FMove); Visual(); });
                ThreeFLMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.fText;

                ThreeFRMoveButton.onClick.RemoveAllListeners();
                ThreeFRMoveButton.onClick.AddListener(() => { RightFirstMove(level.FMove); Visual(); });
                ThreeFRMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.fText;

                ThreeSLMoveButton.onClick.RemoveAllListeners();
                ThreeSLMoveButton.onClick.AddListener(() => { LeftSecondMove(level.SMove); Visual(); });
                ThreeSLMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.sText;

                ThreeSRMoveButton.onClick.RemoveAllListeners();
                ThreeSRMoveButton.onClick.AddListener(() => { RightSecondMove(level.SMove); Visual(); });
                ThreeSRMoveButton.GetComponentInChildren<TextMeshProUGUI>().text = level.sText;
                leftText = ThreePanel.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.CompareTag("ThreeLeft")).First();
                rightText = ThreePanel.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.CompareTag("ThreeRight")).First();
                Visual();
                break;
        
        }
        GameState state = new GameState(left, right, level, true); // ��������� ���������
        turnStories.Clear();
        GameSolver solver = new GameSolver();
        List<GameState> curTurnStrategy = solver.FindShortestWinningStrategy(state);
        turnStories.Add(curTurnStrategy);
        miss = 0;
        p.prevLeft = left; p.prevRight = right;
        string strategy =  solver.GetTextFromPath(curTurnStrategy);
        print(strategy);
    }

    /*bool FindPath(int l, int r, int c)
    { 
        if (l+r >= needScore)
        {

            if(c % 2 == 0)
                return true;
            else 
                return false;
        }
        else
        {
            switch(level.id)
            {
                case 1:
                    if (FindPath(level.LFMove(l), r, c + 1))
                    {
                        
                        if (c % 2 != 0)
                        {
                            if (level.LSMove(l) >= needScore)
                            {
                                res = "";
                                return false;
                            }
                            else {
                                res += "[" + level.LFMove(l) + "]";
                                return true;
                            }
                        }
                        else
                        {
                            if (level.LSMove(l) >= needScore)
                            {
                                res = "";
                                return false;
                            }
                            else
                            {
                                res += "(" + level.LFMove(l) + ")";
                                return true;
                            }
                        }

                    }
                    else if (FindPath(level.LSMove(l), r, c + 1))
                    {
                        if (c % 2 != 0)
                        {
                            if (level.LFMove(l) >= needScore)
                            {
                                res = "";
                                return false;
                            }
                            else
                            {
                                res += "[" + level.LSMove(l) + "]";
                                return true;
                            }
                        }
                        else {
                            res += "(" + level.LSMove(l) + ")";
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (FindPath(level.LFMove(l), r, c + 1))
                    {
                        res += "(" + level.LFMove(l) + " ; " + r + ")";
                        if (c % 2 == 0) return false;
                        else return true;
                    }
                    else if (FindPath(level.LSMove(l), r, c + 1))
                    {
                        res += "(" + level.LFMove(l) + " ; " + r + ")";
                        if (c % 2 == 0) return false;
                        else return true;
                    }
                    else if (FindPath(l, level.RFMove(r), c + 1))
                    {
                        res += "(" + l + " ; " + level.RFMove(r) + ")";
                        if (c % 2 == 0) return false;
                        else return true;
                    }
                    else if (FindPath(l, level.RFMove(r), c + 1))
                    {
                        res += "(" + l + " ; " + level.RSMove(r) + ")";
                        if (c % 2 == 0) return false;
                        else return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
                    
            }

        }
    }*/


    public void LeftFirstMove(Func<(int, int), (int, int)> lfm)
    {
        if (player)
        {
            (int left, int right) t = lfm((left, right));
            left = t.left; right = t.right;
            CheckSum();
        }

    }
    public void RightFirstMove(Func<(int, int), (int, int)> lfm)
    {
        if (player)
        {
            (int right, int left) t = lfm((right, left));
            left = t.left; right = t.right;
            CheckSum();
        }

    }
    public void LeftSecondMove(Func<(int, int), (int, int)> lfm)
    {
        if (player)
        {
            (int left, int right) t = lfm((left, right));
            left = t.left; right = t.right;
            CheckSum();
        }

    }
    public void RightSecondMove(Func<(int, int), (int, int)> lfm)
    {
        if (player)
        {
            (int right, int left) t = lfm((right, left));
            left = t.left; right = t.right;
            CheckSum();
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AIChoice()
    {
        player = false;
        yield return new WaitForSeconds(1f);
        switch (level.id)
        {
            case 1:
                if (level.LFMove(left) >= needScore)
                {
                    left = level.LFMove(left);
                }
                else if (level.LSMove(left) >= needScore)
                {
                    left = level.LSMove(left);
                }
                else
                {
                    int t = Random.Range(0, 2);
                    switch (t)
                    {
                        case 0:
                            left = level.LSMove(left);
                            break;
                        case 1:
                            left = level.LFMove(left);
                            break;
                    }
                }
                CheckSum();
                break;
            case 2:
                if (level.LFMove(left) + right >= needScore)
                    left = level.LFMove(left);
                else if (level.LSMove(left) + right >= needScore)
                    left = level.LSMove(left);
                else if (level.RFMove(right) + left >= needScore)
                    right = level.RFMove(right);
                else if (level.RSMove(right) + left >= needScore)
                    right = level.RSMove(right);
                else
                {
                    int t = Random.Range(0, 4);
                    switch (t)
                    {
                        case 0:
                            left = level.LFMove(left);
                            break;
                        case 1:
                            left = level.LSMove(left);
                            break;
                        case 2:
                            right = level.RFMove(right);
                            break;
                        case 3:
                            right = level.RSMove(right);
                            break;

                    }
                    
                }
                CheckSum();
                break;
            case 3:
                (int left, int right) result = new();
                var t1 = level.FMove((left, right));
                var tr1 = level.FMove((right, left));
                var t2 = level.SMove((left, right));
                var tr2 = level.SMove((right, left));
                if (t1.Item1 + t1.Item2 >= needScore)
                    result = t1;
                else if (tr1.Item1 + tr1.Item2 >= needScore)
                { result.right = tr1.Item1; result.left = tr1.Item2; }
                else if (t2.Item1 + t2.Item2 >= needScore)
                    result = t2;
                else if (tr2.Item1 + tr2.Item2 >= needScore)
                { result.right = tr2.Item1; result.left = tr2.Item2; }
                else
                {
                    int t = Random.Range(0, 4);
                    switch (t)
                    {
                        case 0:
                            result.left = t1.Item1;
                            result.right = t1.Item2;
                            break;
                        case 1:
                            result.left = tr1.Item2;
                            result.right = tr1.Item1;
                            break;
                        case 2:
                            result.left = t2.Item1;
                            result.right = t2.Item2;
                            break;
                        case 3:
                            result.right = tr2.Item1;
                            result.left = tr2.Item2;
                            break;
                    }
                }
                left = result.left; right = result.right;
                CheckSum();
                break;
        }
    }
    public void UnderstandTutorial()
    {
        TutorialPanel.SetActive(false);
        Tutorial = false;
        LevelsPanel.SetActive(true);
        LoadLevels();
    }
    public void UnderstandAnalysis()
    {
        AnalysisPanel.SetActive(false);
        LevelsPanel.SetActive(true);
        LoadLevels();

    }
    public void ToLevels()
    {
        GamePanel.SetActive(false);
        LevelsPanel.SetActive(true);
        LoadLevels();
    }
    void CheckSum()
    {
        Visual();
        if (left + right >= needScore)
        {
            GameSolver solver = new GameSolver();
            GamePanel.SetActive(false);
            AnalysisPanel.SetActive(true);
            if (player)
            {
                resultText.text = "�� �������! \n  ����������� ���������: ";
                
            }
            else
            {
                resultText.text = "�� ��������! �������� ���������, ����� ������, �� �� ��������� �� ������: \n";
                if (!(turnStories.Count() == 1 && solver.GetTextFromPath(turnStories.First()) == "�� �������� � ����������� ���������: ��� �� �� �� ������, �������� ��� ��������."))
                    miss++;

            }

            int sum = 0;
            foreach (var item in turnStories) {
                sum += item.Count();
                if (item.Equals(turnStories.First()))
                    resultText.text += solver.GetTextFromPath(item) + "\n";
                else if(item.Equals(turnStories.Last()))
                    resultText.text += "\n �������� ���������: " + solver.GetTextFromPath(item);
                else
                    resultText.text += "\n �� ���������� �� ��������� ���������: " + solver.GetTextFromPath(item);
            }
            if(player)
                resultText.text += "\n ������: " + miss + " - " + (Mathf.RoundToInt(miss/sum * 100) == 0  && miss != 0 ? 1: Mathf.RoundToInt(miss / sum * 100)) + "%";
            else
                resultText.text += "\n ������: " + miss + " -  100%";
        }
        else {
            if (player)
            {
                StartCoroutine(AIChoice());
            }
            else
            {
                player = true;
                GameState state = new GameState(left, right, level, true); // ������� ���������
                GameSolver solver = new GameSolver();
                List<GameState> curTurnStrategy = solver.FindShortestWinningStrategy(state);
                List<GameState> t = turnStories.Last();
                bool check = true;
                if ((t.Count() - curTurnStrategy.Count()) % 2 == 0) { 
                    t.Reverse();
                    curTurnStrategy.Reverse();
                    for (int i = 0; i < curTurnStrategy.Count(); i++)
                    {
                        if (t[i].Equals(curTurnStrategy[i]))
                        {
                            check = false; break;
                        }
                    }
                    curTurnStrategy.Reverse();
                    t.Reverse();
                }
                
                if (check)
                {
                    if (!(p.prevLeft == t[1].Left && p.prevRight == t[1].Right))
                        miss++;
                    turnStories.Add(curTurnStrategy);

                }
                string strategy = solver.GetTextFromPath(curTurnStrategy);
                print(strategy);
            }
        }
        p.prevLeft = left; p.prevRight = right;
    }
    void Visual()
    {
        summ.text = "����� ������: \n" + (left + right);
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        target.text = "����� ������� \n" + needScore; 
    }

    public void LeftFirstMove(Func<int,int> lfm)
    {
        if (player)
        {
            left = lfm(left);
            CheckSum();
        }

    }

    public void LeftSecondMove(Func<int,int> lsm)
    {
        if (player)
        {
            left = lsm(left);
            CheckSum();
        }

    }

    public void RightSecondMove(Func<int, int> rsm)
    {
        if (player)
        {
            right = rsm(right);
            CheckSum();
        }

    }
    public void RightFirstMove(Func<int, int> rfm)
    {
        if (player)
        {
            right = rfm(right);
            CheckSum();
        }

    }
}
