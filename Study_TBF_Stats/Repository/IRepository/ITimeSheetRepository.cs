using Study_TBF_Stats.Models;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Repository.IRepository
{
    public interface ITimeSheetRepository
    {
        Task<PagedList<TbTimeSheet>> GetAllTimeSheetsAsync(TimeSheetsParameter timeSheetsParameter, bool trackChanges);
        Task<TbTimeSheet> GetTimeSheetsAsync(Guid id, bool trackChanges);
        void CreateTimeSheet(TbTimeSheet timeSheet);
        void DeleteTimeSheet(TbTimeSheet timeSheet);
    }
}
