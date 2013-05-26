using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorldMessages.DomainModel.Message;
using WorldMessages.DomainModelFramework.Query;

namespace WorldMessages.DataAccess.Message
{
    public class MessagesRepository : IRepository<MessageModel>
    {
        private readonly WorldMessageDb dbContext;

        private
            Dictionary<string, Func<IQueryable<MessageModel>, Expression<Func<MessageModel, object>>, SearchCriteria<MessageModel>, IQueryable<MessageModel>>> fetchings;

        public MessagesRepository(WorldMessageDb dbContext)
        {
            this.dbContext = dbContext;
        }

        private void Add(
           Expression<Func<MessageModel, object>> property,
           Func<IQueryable<MessageModel>, Expression<Func<MessageModel, object>>, SearchCriteria<MessageModel>, IQueryable<MessageModel>> fetching)
        {
            if (fetchings == null)
            {
                fetchings =
                    new Dictionary
                        <string,
                            Func<IQueryable<MessageModel>, Expression<Func<MessageModel, object>>, SearchCriteria<MessageModel>, IQueryable<MessageModel>>>();
            }

            fetchings.Add(property.ToString(), fetching);
        }

        public QueryResult<MessageModel> GetItems(SearchCriteria<MessageModel> searchCriteria = null)
        {
            throw new NotImplementedException();
        }

        public MessageModel GetItem(SearchCriteria<MessageModel> searchCriteria)
        {
            throw new NotImplementedException();
        }

        public MessageModel GetItem(string entityCode)
        {
            throw new NotImplementedException();
        }
    }
}
