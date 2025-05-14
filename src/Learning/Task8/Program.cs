using System;
using System.Text.RegularExpressions;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex("^[0-9]*(,\\s?[0-9]*)*$");
            while (true)
            {
                string input = Console.ReadLine();

                if (regex.IsMatch(input))
                {
                    int max = -1;
                    foreach (var s in input.Split(","))
                    {
                        int num;
                        if (int.TryParse(s, out num))
                        {
                            if (num >= max)
                            {
                                max = num;
                            }
                        }
                    }
                    if (max != -1)
                    {
                        Console.WriteLine(max);
                    }
                }

            }

        }
    }
}
