using Study_TBF_Stats.Models;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Service.IService
{
    public interface IUsersService
    {
        Task<(IEnumerable<TbUser> user, MetaData metaData)> GetAllUserAsync(UsersParameter userParameters, bool trackChanges);
    }
}
