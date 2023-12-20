namespace Study_TBF_Stats.Models.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(TbUser tbUser);
    }
}
