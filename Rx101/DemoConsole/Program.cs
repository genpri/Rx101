using System;
using System.Reactive.Linq;

namespace DemoConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Observable.Range(1, 10)
                .Subscribe(x => Console.WriteLine(value: x));

            var subscription =
                Observable.Interval(TimeSpan.FromSeconds(1))
                    .Subscribe(x => Console.WriteLine(value: x));


            Console.WriteLine(value: "Press any key to continue...");
            Console.ReadLine();
        }
    }
}