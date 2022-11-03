namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly string _input;
    int[] depthReadings;


    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
        depthReadings = Array.ConvertAll(_input.Split('\n'), int.Parse);
    }

    private int DepthComparer()
    {
        int count = 0;
        int depth = depthReadings[0];
        for(int i = 1; i < depthReadings.Length; i++)
        {
            if (depthReadings[i] > depth) { count++; }
            depth = depthReadings[i];
        }
        return count;
    }

    private int RollingDepthComparer()
    {
        int count = 0;
        int depth = depthReadings[0] + depthReadings[1] + depthReadings[2];
        int newDepth = 0;
        for (int i = 3; i < depthReadings.Length; i++)
        {
            newDepth = depth + depthReadings[i] - depthReadings[i - 3];
            if (newDepth > depth) { count++; }
            depth = newDepth;
        }
        return count;
    }

    public override ValueTask<string> Solve_1() => new($"{DepthComparer()}");

    public override ValueTask<string> Solve_2() => new($"{RollingDepthComparer()}");
}
