using System;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             Write a program that picks a random number between 1 and 10.
            Give the user 4 chances to guess the number. 
            If the user guesses the number, display “You won"; otherwise, display “You lost". 
            (To make sure the program is behaving correctly, you can display the secret number on the console first.) 
             */

            Random random = new Random();
            int rng = random.Next(1, 11);
            Console.WriteLine(rng);
            bool success = false;

            int chances = 4;

            while (chances > 0)
            {
                string input = "";
                int num;

                input = Console.ReadLine();
               if (int.TryParse(input, out num))
                {
                    if (num == rng)
                    {
                        success = true;
                        break;
                    }
                    chances--;
                }
            }

            if (success)
            {
                Console.Write("You Won");
            } else
            {
                Console.Write("You Lost");

            }

        }
    }
}
