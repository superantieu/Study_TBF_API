using AutoMapper;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.RequestFeatures;
using Study_TBF_Stats.Service.IService;

namespace Study_TBF_Stats.Service
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

      

        public TimeSheetService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
          
        }

        public async Task<(IEnumerable<TbTimeSheet> timesheets, MetaData metaData)> GetAllTimeSheetAsync(TimeSheetsParameter timeSheetsParameter, bool trackChanges)
        {
            var timesheetresult = await _repositoryManager.TimeSheetRepository.GetAllTimeSheetsAsync(timeSheetsParameter, trackChanges);
            var timesheetDto = _mapper.Map<IEnumerable<TbTimeSheet>>(timesheetresult);
            return (timesheets: timesheetDto, metaData: timesheetresult.MetaData);
        }


    }
}
