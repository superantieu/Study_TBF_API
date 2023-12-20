using Study_TBF_Stats.Models.Dto;

namespace Study_TBF_Stats.Models.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
