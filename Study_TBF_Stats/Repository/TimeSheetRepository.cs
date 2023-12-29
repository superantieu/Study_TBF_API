using Microsoft.EntityFrameworkCore;
using Study_TBF_Stats.Contract;
using Study_TBF_Stats.Extensions;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Repository.IRepository;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Repository
{
    public class TimeSheetRepository : RepositoryBase<TbTimeSheet>, ITimeSheetRepository
    {
        public TimeSheetRepository(StudyTbfSupContext studyTbfSupContext) : base(studyTbfSupContext)
        {
        }

        public void CreateTimeSheet(TbTimeSheet timeSheet)
        {
            Create(timeSheet);
        }

        public void DeleteTimeSheet(TbTimeSheet timeSheet)
        {
            Delete(timeSheet);
        }

        public async Task<PagedList<TbTimeSheet>> GetAllTimeSheetsAsync(TimeSheetsParameter timeSheetsParameter, bool trackChanges)
        {
            var timesheets = await FindByCondtion(t => true, trackChanges)
                .FilterTimeSheet(timeSheetsParameter.ProjectId)
                .ToListAsync();
            return PagedList<TbTimeSheet>.ToPagedList(timesheets, timeSheetsParameter.pageNumber, timeSheetsParameter.pageSize);
        }

        public async Task<TbTimeSheet> GetTimeSheetsAsync(Guid Id, bool trackChanges)
        {
            return await FindByCondtion(t => t.Id.Equals(Id), trackChanges).SingleOrDefaultAsync();
        }

       
       
    }
}
