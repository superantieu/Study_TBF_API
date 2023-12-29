using Study_TBF_Stats.Models;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Service.IService
{
    public interface ITimeSheetService
    {
        Task<(IEnumerable<TbTimeSheet> timesheets, MetaData metaData)> GetAllTimeSheetAsync(TimeSheetsParameter timeSheetsParameter, bool trackChanges);
    }
}
