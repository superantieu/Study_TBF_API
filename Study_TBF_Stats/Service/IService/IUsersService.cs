using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Service.IService
{
    public interface IUsersService
    {
        Task<(IEnumerable<TbUserDto> user, MetaData metaData)> GetAllUserAsync(UsersParameter userParameters, bool trackChanges);
        Task<TbUserDto> GetUserAsync(int userId, bool trackChanges);
    }
}
