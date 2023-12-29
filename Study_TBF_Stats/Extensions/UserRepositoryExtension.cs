using Study_TBF_Stats.Extensions.Utility;
using Study_TBF_Stats.Models;
using System.Linq.Dynamic.Core;

namespace Study_TBF_Stats.Extensions
{
    public static class UserRepositoryExtension
    {


        public static IQueryable<TbUser> FilterUser(this IQueryable<TbUser> users, string? discipline)
        {
            if (discipline == null) return users;

            return users.Where(x => x.Discipline == discipline);
        }
        //Product Searching
        public static IQueryable<TbUser> SearchUser(this IQueryable<TbUser> users, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return users;
            var lowCaseSearchTerm = searchTerm.Trim().ToLower();
            return users.Where(x => x.FullName.ToLower().Contains(lowCaseSearchTerm));

        }
        //Product Sorting (OrderBy)
        public static IQueryable<TbUser> SortUser(this IQueryable<TbUser> users, string orderByQueryString)
        {

            if (string.IsNullOrEmpty(orderByQueryString))
                return users.OrderBy(x => x.UserId);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<TbUser>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return users.OrderBy(x => x.UserId);

            return users.OrderBy(orderQuery);
        }
    }
}
