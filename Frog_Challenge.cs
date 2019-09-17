using System;
namespace Frog_Challenge
{
    public class Frog
    {
        Random random = new Random();

        public int Jump(int numberOfPlatforms)
        {
            int currentPlatform = 0;
            int jumps = 0;

            while (currentPlatform != numberOfPlatforms)
            {
                int target = random.Next(currentPlatform + 1, numberOfPlatforms + 1);
                currentPlatform = target;
                jumps++;
            }

            return jumps;
        }

        /* usage example
         *    Frog frog = new Frog();

            for (int k = 1; k < 1000; k++)
            {
                List<int> data = new List<int>();

                for (int i = 0; i < 100000; i++)
                {
                    data.Add(frog.Jump(k));
                }

                Console.WriteLine(data.Average());
            }
         */
    }
}
