using AutoMapper;
using Microsoft.Extensions.Logging;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Service;
using Study_TBF_Stats.Service.IService;

namespace Study_TBF_Stats.Contract
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<IUsersService> _usersService;
        private readonly Lazy<ITimeSheetService> _timeSheetService;


        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, mapper)) ;
            _usersService = new Lazy<IUsersService>(() => new UserService(repositoryManager, mapper ));
            _timeSheetService = new Lazy<ITimeSheetService>(() => new TimeSheetService(repositoryManager, mapper));
        }
        public IProjectService ProjectService => _projectService.Value;

        public IUsersService UsersService => _usersService.Value;

        public ITimeSheetService TimeSheetService => _timeSheetService.Value;
    }
}
