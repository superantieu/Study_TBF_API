using Microsoft.AspNetCore.Identity;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;
using Study_TBF_Stats.Service.IService;
using System;

namespace Study_TBF_Stats.Service
{
    public class AuthSevice : IAuthService
    {
        private readonly StudyTbfSupContext _studyTbfSupContext;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthSevice(StudyTbfSupContext studyTbfSupContext, IJwtTokenGenerator jwtTokenGenerator
            )
        {
            _studyTbfSupContext = studyTbfSupContext;
            _jwtTokenGenerator = jwtTokenGenerator;

        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _studyTbfSupContext.TbUsers.FirstOrDefault(u => u.FullName.ToLower() == loginRequestDto.UserName.ToLower());
            if (user == null)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            TbUserDto tbUserDTO = new()
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Discipline = user.Discipline,
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = tbUserDTO,
                Token = token

            };
            return loginResponseDto;
        }
    }
}
