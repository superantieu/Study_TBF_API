using AutoMapper;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Exceptions;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;
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

        public async Task<(IEnumerable<TbUserDto> user, MetaData metaData)> GetAllUserAsync(UsersParameter userParameters, bool trackChanges)
        {
            var userMetaData = await _repositoryManager.UsersRepository.GetAllUsersAsync(userParameters, trackChanges);
            var userDto = _mapper.Map<IEnumerable<TbUserDto>>(userMetaData);
            return (user: userDto, metaData: userMetaData.MetaData);
        }
        public async Task<TbUserDto> GetUserAsync(int userId, bool trackChanges)
        {

            var user = await ValidUserExist(userId, trackChanges);
            var userDto = _mapper.Map<TbUserDto>(user);
            return userDto;
        }
        private async Task<TbUser> ValidUserExist(int userId, bool trackChanges)
        {
            var user = await _repositoryManager.UsersRepository.GetUserAsync(userId, trackChanges);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            return user;
        }
    }
}
