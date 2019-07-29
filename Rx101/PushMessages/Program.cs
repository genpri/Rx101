using System;
using System.Diagnostics;

namespace PushMessages
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mgr = new SocialNetworksManager();

            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine(value: "Loading messages");
            var messages = mgr.LoadMessages(hashtag: "Rx");
            foreach (var msg in messages) Console.WriteLine($"Iterated:{msg} \t after {stopwatch.Elapsed}");

            Console.WriteLine(value: "--------------------");
            Console.WriteLine(value: "Interactive messages");
            stopwatch.Restart();
            var interactiveMgr = new InteractiveSocialNetworksManager();
            interactiveMgr.MessageAvailable +=
                (sender, msg) => Console.WriteLine($"Observed:{msg} \t after {stopwatch.Elapsed}");
            interactiveMgr.LoadMessages(hashtag: "Rx");


            Console.WriteLine(value: "--------------------");
            Console.WriteLine(value: "Observing messages");
            stopwatch.Restart();

            mgr.ObserveLoadedMessages(hashtag: "Rx")
                .Subscribe(msg => Console.WriteLine($"Observed:{msg} \t after {stopwatch.Elapsed}"),
                    ex =>
                    {
                        /*OnError*/
                    },
                    () =>
                    {
                        /*OnCompleted*/
                    });

            Console.ReadLine();
        }
    }
}