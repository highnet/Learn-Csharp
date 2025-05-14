using System;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Write a program and continuously ask the user to enter a number or "ok" to exit. 
             * Calculate the sum of all the previously entered numbers and display it on the console.
             */

            string input = "";
            int sum = 0;
            while (input != "ok")
            {
                input = Console.ReadLine();
                
                    int num;
                    int.TryParse(input, out num);
                    sum += num;
                
            } Console.WriteLine(sum);
        }
    }
}
