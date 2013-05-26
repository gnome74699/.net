using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WorldMessages.DomainModelFramework.Query
{
    public class SearchCriteria<T>
    {
        private Dictionary<string, Expression<Func<T, object>>> includes;

        protected Dictionary<string, Expression<Func<T, bool>>> Filters { get; private set; }

        private enum OrderDirectionEnum
        {
            Ascending = 0,
            Descending = 1,
        }

        public SearchCriteria()
        {
            Page = 1;
            Size = Int32.MaxValue;
            Filters = new Dictionary<string, Expression<Func<T, bool>>>();
        }

        /// <summary>
        /// Get and set requested page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Get and set page size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Get and set order by clause
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Apply filters, paging and ordering to given <see cref="source"/>
        /// </summary>
        /// <param name="source">Data source</param>
        /// <returns>Items corresponding to criteria</returns>
        public IQueryable<T> ToQueryable(IQueryable<T> source)
        {
            var result = Filters.Aggregate(source, (current, filter) => current.Where(filter.Value));

            var expressions = OrderByExpression();
            if (expressions.Count == 0)
            {
                return DefaultOrderBy(result);
            }

            var orderedResult = expressions.First().Value.Equals(OrderDirectionEnum.Ascending)
                                    ? result.OrderBy(expressions.First().Key)
                                    : result.OrderByDescending(expressions.First().Key);

            return expressions.Skip(1).Aggregate(orderedResult,
                                                 (current, expression) =>
                                                 expression.Value.Equals(OrderDirectionEnum.Ascending)
                                                     ? current.ThenBy(expression.Key)
                                                     : current.ThenByDescending(expression.Key));
        }

        public SearchCriteria<T> Include(Expression<Func<T, object>> property)
        {
            if (includes == null)
            {
                includes = new Dictionary<string, Expression<Func<T, object>>>();
            }

            includes.Add(property.ToString(), property);
            return this;
        }

        internal QueryResult<T> Query(IQueryable<T> source, Dictionary<string, Func<IQueryable<T>, Expression<Func<T, object>>, SearchCriteria<T>, IQueryable<T>>> fetchings)
        {
            var filteredSource = ToQueryable(source);
            var pagedResults = this.Size != int.MaxValue
                                   ? filteredSource.Skip((this.Page - 1) * this.Size).Take(this.Size)
                                   : filteredSource;
            pagedResults = ApplyIncludes(pagedResults, fetchings);
            return new QueryResult<T>(
                pagedResults,
                filteredSource.Count());
        }

        private IQueryable<T> ApplyIncludes(IQueryable<T> pagedResults, Dictionary<string, Func<IQueryable<T>, Expression<Func<T, object>>, SearchCriteria<T>, IQueryable<T>>> fetchings)
        {
            if (includes == null)
            {
                return pagedResults;
            }
            else if (fetchings == null)
            {
                throw new NotImplementedException("Search Criteria was used with Include but no fetchings are given in parameters (see in repository typically).");
            }

            return includes.Aggregate(pagedResults, (current, include) => fetchings[include.Key](current, include.Value, this));
        }

        protected virtual IQueryable<T> DefaultOrderBy(IQueryable<T> source)
        {
            return source;
        }

        private Dictionary<Expression<Func<T, object>>, OrderDirectionEnum> OrderByExpression()
        {
            var fieldNames = new Dictionary<string, OrderDirectionEnum>();
            if (string.IsNullOrEmpty(OrderBy)) return new Dictionary<Expression<Func<T, object>>, OrderDirectionEnum>();
            var fields = OrderBy.Split('~');
            foreach (string field in fields)
            {
                var members = field.Split('-');
                if (members.Count() == 2)
                {
                    fieldNames.Add(members.First(),
                                   members.Last().Equals("asc")
                                       ? OrderDirectionEnum.Ascending
                                       : OrderDirectionEnum.Descending);
                }
            }

            // x => (object) x.[keyValuePair.Key] : use convert to object to manage value types
            var parameter = Expression.Parameter(typeof(T));
            return
                fieldNames.ToDictionary(
                    keyValuePair =>
                    Expression.Lambda<Func<T, object>>(
                        Expression.Convert(
                            Expression.Property(parameter, keyValuePair.Key), typeof(object)), parameter),
                    keyValuePair => keyValuePair.Value);
        }
    }

    public static class SearchCriteriaExtensions
    {
        public static QueryResult<T> Query<T>(this SearchCriteria<T> searchCriteria, IQueryable<T> source, Dictionary<string, Func<IQueryable<T>, Expression<Func<T, object>>, SearchCriteria<T>, IQueryable<T>>> fetchings)
        {
            var nullSafeSearchCriteria = searchCriteria ?? new AnySearchCriteria<T>();
            return nullSafeSearchCriteria.Query(source, fetchings);
        }
    }
}
