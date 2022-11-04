namespace AdventOfCode;

public class Day_02 : BaseDay
{
    private readonly string _input;

    public Day_02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private int DestinationCalculator()
    {
        int depth = 0;
        int distance = 0;
        int multipliedTotal = 0;

        foreach(string s in _input.Split("\n"))
        {
            string[] instruction = s.Split(" ");
            string direction = instruction[0];
            int magnitude = int.Parse(instruction[1]);

            if (direction == "forward") 
            { 
                distance+=magnitude;
                continue;
            }
            if(direction == "up") { magnitude *= -1; }
            depth += magnitude;
        }
        multipliedTotal = depth * distance;
        return multipliedTotal;
    }

    private int DestinationCalculatorV2()
    {
        int depth = 0;
        int distance = 0;
        int aim = 0;
        int multipliedTotal = 0;

        foreach (string s in _input.Split("\n"))
        {
            string[] instruction = s.Split(" ");
            string direction = instruction[0];
            int magnitude = int.Parse(instruction[1]);

            if (direction == "forward")
            {
                distance += magnitude;
                depth += magnitude * aim;
                continue;
            }
            if (direction == "up") { magnitude *= -1; }
            aim += magnitude;
        }
        multipliedTotal = depth * distance;
        return multipliedTotal;
    }

    public override ValueTask<string> Solve_1() => new($"{DestinationCalculator()}");

    public override ValueTask<string> Solve_2() => new($"{DestinationCalculatorV2()}");
}
