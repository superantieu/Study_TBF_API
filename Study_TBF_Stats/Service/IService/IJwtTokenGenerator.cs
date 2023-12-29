using Study_TBF_Stats.Models;

namespace Study_TBF_Stats.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(TbUser tbUser);
    }
}
