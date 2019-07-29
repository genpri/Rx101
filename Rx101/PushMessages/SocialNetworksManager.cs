using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace PushMessages
{
    internal class SocialNetworksManager
    {
        private readonly ISocialNetworkClient _facebook = new FakeFacebookClient();
        private readonly ISocialNetworkClient _linkedin = new FakeLinkedinClient();
        private readonly ISocialNetworkClient _twitter = new FakeTwitterClient();

        public IEnumerable<Message> LoadMessages(string hashtag)
        {
            var statuses = _facebook.Search(hashtag: hashtag);
            var tweets = _twitter.Search(hashtag: hashtag);
            var linkedinMsgs = _linkedin.Search(hashtag: hashtag);
            return statuses.Concat(second: tweets).Concat(second: linkedinMsgs);
        }

        public IObservable<Message> ObserveLoadedMessages(string hashtag)
        {
            return Observable.Merge(
                _facebook.ObserveSearchedMessages(hashtag: hashtag),
                _twitter.ObserveSearchedMessages(hashtag: hashtag),
                _linkedin.ObserveSearchedMessages(hashtag: hashtag));

            //
            //The above can also be written like this:
            //
            //_facebook.ObserveSearchedMessages(hashtag)
            //    .Merge(_twitter.ObserveSearchedMessages(hashtag))
            //    .Merge(_twitter.ObserveSearchedMessages(_linkedin));
        }
    }
}