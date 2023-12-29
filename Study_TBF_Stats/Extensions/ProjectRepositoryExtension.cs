using Microsoft.IdentityModel.Tokens;
using Study_TBF_Stats.Extensions.Utility;
using Study_TBF_Stats.Models;
using System.Linq.Dynamic.Core;

using System.Runtime.CompilerServices;

namespace Study_TBF_Stats.Extensions
{
    public static class ProjectRepositoryExtension
    {
        //Product Filtering
        public static IQueryable<TbProject> IsCompletedProject(this IQueryable<TbProject> projects, bool? completed)
        {
            if (completed == true) return projects.Where(x => x.CompletedDate != null);

            if (completed == false) return projects.Where(x => x.CompletedDate == null);

            return projects;


        }
        public static IQueryable<TbProject> FilterProject(this IQueryable<TbProject> projects, int minArea, int maxArea)
        {
            if (minArea == 0 && maxArea == int.MaxValue) return projects;

            return projects.Where(x => x.FloorAreas >= minArea && x.FloorAreas <= maxArea);
        }
        //Product Searching
        public static IQueryable<TbProject> SearchProject(this IQueryable<TbProject> projects, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return projects;
            var lowCaseSearchTerm = searchTerm.Trim().ToLower();
            return projects.Where(x => x.ProjectName.ToLower().Contains(lowCaseSearchTerm));

        }
        //Product Sorting (OrderBy)
        public static IQueryable<TbProject> SortProject(this IQueryable<TbProject> projects, string orderByQueryString)
        {

            if (string.IsNullOrEmpty(orderByQueryString))
                return projects.OrderBy(x => x.ProjectId);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<TbProject>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return projects.OrderBy(x => x.ProjectId);

            return projects.OrderBy(orderQuery);
        }
    }
}
