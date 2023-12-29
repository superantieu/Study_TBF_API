using Study_TBF_Stats.Models;

namespace Study_TBF_Stats.Extensions
{
    public static class TimeSheetRepositoryExtensions
    {
        public static IQueryable<TbTimeSheet> FilterTimeSheet(this IQueryable<TbTimeSheet> timesheets, int? projectId)
        {
            if (projectId == null) return timesheets;
            return timesheets.Where(t => t.ProjectId == projectId);
        }

    }
}
