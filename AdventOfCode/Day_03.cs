namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string _input;

    public Day_03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private int CalculatePower()
    {
        int gammaRate = 0;
        int epsilonRate = 0;
        int power = 0;

        int[] bitCount = new int[12];

        foreach(string s in _input.Split("\n"))
        {
            for(int i=0;i<s.Length;i++)
            {
                if(s[i] == '1') { bitCount[i]++; }
            }
        }

        string gamma="";
        foreach(int i in bitCount)
        {
            if (i > 500) { gamma+="1"; }
            else { gamma+="0"; }
        }

        gammaRate = Convert.ToInt32(gamma, 2);
        epsilonRate = 4095 - gammaRate;
        power = gammaRate * epsilonRate;

        return power;
    }

    private int CalculateLifeSupport()
    {
        int oxygenRating = 0;
        int CO2Rating = 0;
        int lifeSupport = 0;

        for(int rating = 0; rating < 2; rating++)
        {
            List<string> numList = _input.Split("\n").ToList();
            int n = 0;
            string searchCriteria = "";
            while (numList.Count > 1)
            {
                int bitCount = 0;
                foreach (string s in numList)
                {
                    if (s[n] == '1') { bitCount++; }
                }

                if (numList.Count - (2*bitCount) <=0 && bitCount < numList.Count)
                {
                    searchCriteria += $"{1 - rating}";
                }
                else if (bitCount > 0 && bitCount < numList.Count)
                {
                    searchCriteria += $"{0 + rating}";
                }
                else if (bitCount == 0) { searchCriteria += "0"; }
                else { searchCriteria += "1"; }

                numList = numList.Where(s => s.StartsWith(searchCriteria)).ToList();

                n++;
            }

            numList[0] = numList[0].Trim();
            if (rating == 0) { oxygenRating = Convert.ToInt32(numList[0], 2); }
            else { CO2Rating= Convert.ToInt32(numList[0], 2); }
        }
        
        lifeSupport = oxygenRating * CO2Rating;
        return lifeSupport;
    }
    public override ValueTask<string> Solve_1() => new($"{CalculatePower()} ");

    public override ValueTask<string> Solve_2() => new($"{CalculateLifeSupport()}");
}