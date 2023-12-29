using Study_TBF_Stats.Service.IService;

namespace Study_TBF_Stats.Contract.IContract
{
    public interface IServiceManager
    {
        IProjectService ProjectService { get; }
        IUsersService UsersService { get; }
        ITimeSheetService TimeSheetService { get; }


    }
}
