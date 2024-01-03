using Microsoft.EntityFrameworkCore;
using Study_TBF_Stats.Contract;
using Study_TBF_Stats.Extensions;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Repository.IRepository;
using Study_TBF_Stats.RequestFeatures;
using System.Linq.Expressions;

namespace Study_TBF_Stats.Repository
{
    public class UsersRepository : RepositoryBase<TbUser>, IUsersRepository
    {
        public UsersRepository(StudyTbfSupContext studyTbfSupContext) : base(studyTbfSupContext) { }

        public void CreateUser(TbUser user)
        {
            Create(user);
        }

        public void DeleteUser(TbUser user)
        {
            Delete(user);
        }


        public async Task<PagedList<TbUser>> GetAllUsersAsync(UsersParameter usersParameter, bool trackChanges)
        {
            var users = await FindByCondtion(u => true, trackChanges)
                .FilterUser(usersParameter.Discipline)
                .SearchUser(usersParameter.SearchTerm)
                .SortUser(usersParameter.orderBy)
                .ToListAsync();
            return PagedList<TbUser>.ToPagedList(users, usersParameter.pageNumber, usersParameter.pageSize);
        }

        public async Task<TbUser> GetUserAsync(int userId, bool trackChanges)
        {
            return await FindByCondtion(u => u.UserId.Equals(userId), trackChanges).SingleOrDefaultAsync();
        }

       
    }
}
