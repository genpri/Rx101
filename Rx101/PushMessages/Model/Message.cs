namespace PushMessages
{
    internal class Message
    {
        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}