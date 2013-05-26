using WorldMessages.DomainModelFramework.Query;

namespace WorldMessages.DomainModel.Message
{
    public class MessageSearchCriteria : SearchCriteria<MessageModel>
    {
        private long id;

        public long Id
        {
            get { return id; }
            set
            {
                id = value;
                Filters["Id"] = x => x.Id.Equals(id);
            }
        }

    }
}
