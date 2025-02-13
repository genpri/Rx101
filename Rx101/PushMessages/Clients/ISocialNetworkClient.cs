using System;
using System.Collections.Generic;

namespace PushMessages
{
    internal interface ISocialNetworkClient
    {
        IEnumerable<Message> Search(string hashtag);
        IObservable<Message> ObserveSearchedMessages(string hashtag);
    }
}