using System;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             Write a program and ask the user to enter a number. 
            Compute the factorial of the number and print it on the console.
            For example, if the user enters 5, 
            the program should calculate 5 x 4 x 3 x 2 x 1 and display it as 5! = 120.
             */
            string input = "";
            int num = 0;
            int numFac = 1;
            input = Console.ReadLine();
            int.TryParse(input, out num);

            for(int i = num; i >= 1; i--)
            {
                numFac *= i;
            }
            Console.WriteLine(numFac);
        }
    }
}
