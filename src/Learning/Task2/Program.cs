using System;

namespace Task2
{
    class Program
    {

        // Write a program which takes two numbers from the console and displays the maximum of the two.
        static void Main(string[] args)
        {
            int num1;
            int.TryParse(Console.ReadLine(), out num1);
            int num2;
            int.TryParse(Console.ReadLine(), out num2);
            Console.WriteLine(Math.Max(num1, num2));
        }
    }
}
