using System;

namespace MyTimer
{
    class Program
    {
        static int counter = 0;
        static void increment()
        {
            ++counter;
            Console.WriteLine(counter);
        }
        static void Main(string[] args)
        {
            MyTimer t = new MyTimer(increment, 1000);
            //t.interval = 1000;
            //string op = "";
            do
            {
                Console.WriteLine("\nPress any key to start.");
                Console.ReadKey();
                t.run();
                Console.WriteLine("\nPress any key to pause.");
                Console.ReadKey();
                t.pause();
                Console.WriteLine("\nPress 1 to restart or another key to end.");
                //op = Console.ReadLine();
            } while (t.repeat(Console.ReadKey().Key));
        }
    }
}
