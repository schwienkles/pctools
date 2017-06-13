using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AanklooiProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = CreateTask(4, 1000, "Task A");
            var b = CreateTask(4, 1000, "Task B");
            a.Start();
            b.Start();
            Console.ReadKey();
        }

        static Task CreateTask(int steps, int delay, string name)
        {
            return new Task(async () =>
            {
                Console.WriteLine($"Starting task {name}");
                for (var i = 0; i < steps; i++)
                {
                    await Task.Delay(delay);
                    Console.WriteLine($"{name} Progress: {100 / steps * (i + 1)}%");
                }
                Console.WriteLine($"Finished task {name}");
            });
        }

        static void BusyWait(double ms)
        {
            DateTime end = DateTime.Now.AddMilliseconds(ms);
            while (DateTime.Now < end) ;

            Thread.Sleep(600);
        }
    }
}
