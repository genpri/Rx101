using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace HistoricalScheduler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var numbers = new Subject<int>())
            {
                var historicalScheduler =
                    new System.Reactive.Concurrency.HistoricalScheduler(new DateTime(2015, 11, 21, 17, 10, 0));
                historicalScheduler.Schedule(new DateTime(2015, 11, 21, 17, 10, 0), () => numbers.OnNext(1));
                historicalScheduler.Schedule(new DateTime(2015, 11, 21, 17, 11, 0), () => numbers.OnNext(2));
                historicalScheduler.Schedule(new DateTime(2015, 11, 21, 17, 32, 0), () => numbers.OnNext(3));
                historicalScheduler.Schedule(new DateTime(2015, 11, 21, 17, 39, 0), () => numbers.OnNext(4));
                historicalScheduler.Schedule(new DateTime(2015, 11, 21, 17, 51, 0), () => historicalScheduler.Stop());

                numbers.AsObservable()
                    .Buffer(TimeSpan.FromMinutes(20), scheduler: historicalScheduler)
                    .Subscribe(buffer =>
                    {
                        Console.WriteLine(format: "time is: {0}", arg0: historicalScheduler.Now);
                        Console.WriteLine(value: "Buffer:");
                        foreach (var x in buffer) Console.WriteLine(format: "\t{0}", arg0: x);
                    });

                historicalScheduler.Start();

                Console.WriteLine(value: "Press <enter> to continue");
                Console.ReadLine();
            }
        }
    }
}