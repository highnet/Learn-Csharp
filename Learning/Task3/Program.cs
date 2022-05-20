using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Write a program and ask the user to enter the width and height of an image.
            Then tell if the image is landscape or portrait.
            */
            int width;
            int height;

            Console.WriteLine("Width?");
            int.TryParse(Console.ReadLine(), out width);

            Console.WriteLine("Height?");
            int.TryParse(Console.ReadLine(), out height);

            Console.WriteLine(width == height ? "Square" : width > height ? "Landscape" : "Portrait");

        }
    }
}
