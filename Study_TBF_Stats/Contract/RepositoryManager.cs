using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Repository;
using Study_TBF_Stats.Repository.IRepository;

namespace Study_TBF_Stats.Contract
{
    public sealed class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly StudyTbfSupContext _context;
        private readonly Lazy<IProjectRepository> _projectRepository;
        private readonly Lazy<IUsersRepository> _usersRepository;
        private readonly Lazy<ITimeSheetRepository> _timeSheetRepository;
        public RepositoryManager(StudyTbfSupContext context)
        {
            _context = context;
            _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(context));
            _usersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(context));
            _timeSheetRepository = new Lazy<ITimeSheetRepository>(() => new TimeSheetRepository(context));
        }
        //public IProjectRepository ProductRepository => _projectRepository.Value;

        public IProjectRepository ProjectRepository => _projectRepository.Value;

        public IUsersRepository UsersRepository => _usersRepository.Value;

        public ITimeSheetRepository TimeSheetRepository => _timeSheetRepository.Value;

        //public IProjectRepository ProjectRepository => throw new NotImplementedException();

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
