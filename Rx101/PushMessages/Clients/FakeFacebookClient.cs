using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace PushMessages
{
    internal class FakeFacebookClient : ISocialNetworkClient
    {
        public IEnumerable<Message> Search(string hashtag)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return Enumerable.Range(1, 2).Select(i => "Facebook Status" + i).Select(m => new Message {Content = m});
        }

        public IObservable<Message> ObserveSearchedMessages(string hashtag)
        {
            return Observable.Defer(() => Search(hashtag: hashtag).ToObservable(scheduler: Scheduler.Default));
        }
    }
}