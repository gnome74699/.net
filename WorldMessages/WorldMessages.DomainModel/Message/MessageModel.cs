using WorldMessages.DomainModelFramework.Command;

namespace WorldMessages.DomainModel.Message
{
    public class MessageModel : DomainObject
    {
        public long Id { get; set; }

        public string Date { get; set; }

        public string Conversation { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string Text { get; set; }

    }
}
