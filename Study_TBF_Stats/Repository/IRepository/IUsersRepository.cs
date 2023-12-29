using Study_TBF_Stats.Models;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Repository.IRepository
{
    public interface IUsersRepository
    {
        Task<PagedList<TbUser>> GetAllUsersAsync(UsersParameter usersParameter, bool trackChanges);
        Task<TbUser> GetUserAsync(Guid userId, bool trackChanges);
        void CreateUser(TbUser user);
        void DeleteUser(TbUser user);

    }
}
