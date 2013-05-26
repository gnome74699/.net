
using WorldMessages.CommonFramework.InversionOfControl;

namespace WorldMessages.DomainModelFramework.Query
{
    //TODO Revoir L'inversion de control + Autoregister
    [AutoRegister(LifetimeManager =  typeof(PerCallContextLifeTimeManager))]
    public interface IRepository<T>
    {
        // <summary>
        /// Get items corresponding to the given <see cref="SearchCriteria{T}"/>.
        /// </summary>
        /// <param name="searchCriteria">Criteria (including sort and paging)</param>
        /// <returns>Paged result</returns>
        QueryResult<T> GetItems(SearchCriteria<T> searchCriteria = null);

        /// <summary>
        /// Get single item corresponding to the given <see cref="SearchCriteria{T}"/>.
        /// </summary>
        /// <param name="searchCriteria">Criteria that should target only one Domain Object</param>
        /// <returns>Single item</returns>
        T GetItem(SearchCriteria<T> searchCriteria);

        /// <summary>
        /// Get single item based on its unique key (code).
        /// </summary>
        /// <param name="entityCode">Unique key</param>
        /// <returns>Single object with <see cref="entityCode"/></returns>
        T GetItem(string entityCode);
    }
}
