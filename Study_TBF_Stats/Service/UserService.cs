using AutoMapper;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.RequestFeatures;
using Study_TBF_Stats.Service.IService;

namespace Study_TBF_Stats.Service
{
    public class UserService : IUsersService
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

      

      

        public UserService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            
        }

        public async Task<(IEnumerable<TbUser> user, MetaData metaData)> GetAllUserAsync(UsersParameter userParameters, bool trackChanges)
        {
            var userMetaData = await _repositoryManager.UsersRepository.GetAllUsersAsync(userParameters, trackChanges);
            var userDto = _mapper.Map<IEnumerable<TbUser>>(userMetaData);
            return (user: userDto, metaData: userMetaData.MetaData);
        }
    }
}
