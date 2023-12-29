using System.Threading.Tasks;
using Study_TBF_Stats.Repository.IRepository;

namespace Study_TBF_Stats.Contract.IContract
{
    public interface IRepositoryManager
    {
        IProjectRepository ProjectRepository { get; }
        IUsersRepository UsersRepository { get; }

        ITimeSheetRepository TimeSheetRepository { get; }

        Task SaveAsync();
    }
}
