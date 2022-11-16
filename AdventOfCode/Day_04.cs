using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode;

public class Day_04 : BaseDay
{
    private readonly string _input;
    private List<int> bingoNumbers = new List<int>();
    private List<BingoCard> BingoCards = new List<BingoCard>();

    public Day_04()
    {
        _input = File.ReadAllText(InputFilePath);
        Initialize();
    }
    private void Initialize()
    {
        int splitIndex = _input.IndexOf("\n");

        string bNumString = _input.Substring(0,splitIndex);
        string bCardString = _input.Substring(splitIndex);

        foreach(string s in bNumString.Split(',', '\n'))
        {
            if (s != "")
            {
                bingoNumbers.Add(int.Parse(s));
            }
        }

        int[] cardNums = new int[25];
        int i = 0;
        foreach(string s in bCardString.Split(' ','\n','\r'))
        {
            if (s != "")
            {
                cardNums[i++] = int.Parse(s);
            }
            if(i == 25)
            {
                BingoCard b = new BingoCard(cardNums);
                BingoCards.Add(b);  
                i = 0;
            }
        }
    }
    
    private int WinningBoardScore()
    {
        int score = 0;

        foreach(int n in bingoNumbers)
        {
            foreach(BingoCard bc in BingoCards)
            {
                bc.CheckMatch(n);
                if (bc._win) 
                { 
                    score = bc.Sum();
                    break;
                }
            }
            if(score > 0)
            {
                score = score * n;
                break;
            }
        }

        return score;
    }

    private int LosingBoardScore()
    {
        int score = 0;

        int winners = 1;
        foreach(int n in bingoNumbers)
        {
            for(int i = 0; i < 100; i++)
            {
                if (!BingoCards[i]._win)
                {
                    BingoCards[i].CheckMatch(n);
                    if (BingoCards[i]._win)
                    {
                        winners++;
                        if (winners == 100)
                        {
                            score = BingoCards[i].Sum() * n;
                            return score;
                        }
                    }
                }
            }
        }
        return score;
    }

    public override ValueTask<string> Solve_1() => new($"{WinningBoardScore()} ");

    public override ValueTask<string> Solve_2() => new($"{LosingBoardScore()}");
}

public class BingoCard
{
    private int[] _cardValues = new int[25];
    private int[] _column;
    private int[] _row;
    private bool[] _marked;
    public bool _win { get; private set; }

    public BingoCard(int[] cardValues)
    {
        for(int i=0;i<25;i++)
        {
            _cardValues[i] = cardValues[i]; 
        }
        _column = new int[5];
        _row = new int[5];
        _marked= new bool[25];
        _win = false;
    }

    private void TallyMatch(int matchLocation)
    {
        if (!_marked[matchLocation])
        {
            _column[matchLocation % 5]++;
            _row[matchLocation / 5]++;
            _marked[matchLocation] = true;
        }
        
        if(_column.Contains(5) || _row.Contains(5)) { _win = true; }
    }

    public void CheckMatch(int bingoNumber)
    {
        for(int i = 0; i < 25; i++)
        {
            if(_cardValues[i] == bingoNumber)
            {
                TallyMatch(i);
                break;
            }
        }
    }

    public int Sum()
    {
        int sum = 0;
        int i = 0;
        foreach(int n in _cardValues)
        {
            if (!_marked[i++]) { 
                sum += n;
            }
        }
        return sum;
    }
}