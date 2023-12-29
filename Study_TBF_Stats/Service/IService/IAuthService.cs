using Study_TBF_Stats.Models.Dto;

namespace Study_TBF_Stats.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
