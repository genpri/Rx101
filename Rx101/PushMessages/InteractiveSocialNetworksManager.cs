using System;
using System.Collections.Generic;

namespace PushMessages
{
    internal class InteractiveSocialNetworksManager
    {
        private readonly ISocialNetworkClient _facebook = new FakeFacebookClient();
        private readonly ISocialNetworkClient _linkedin = new FakeLinkedinClient();
        private readonly ISocialNetworkClient _twitter = new FakeTwitterClient();

        public event EventHandler<Message> MessageAvailable;

        public void LoadMessages(string hashtag)
        {
            var statuses = _facebook.Search(hashtag: hashtag);
            NotifyMessages(messages: statuses);
            var tweets = _twitter.Search(hashtag: hashtag);
            NotifyMessages(messages: tweets);
            var linkedinMsgs = _linkedin.Search(hashtag: hashtag);
            NotifyMessages(messages: linkedinMsgs);
        }

        private void NotifyMessages(IEnumerable<Message> messages)
        {
            foreach (var message in messages) MessageAvailable?.Invoke(this, e: message);
        }
    }
}