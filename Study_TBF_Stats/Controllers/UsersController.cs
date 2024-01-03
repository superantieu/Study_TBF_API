using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study_TBF_Stats.Contract;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _services;
        private readonly StudyTbfSupContext _context;
        private IMapper _mapper;

        public UsersController(IServiceManager services, StudyTbfSupContext context, IMapper mapper)
        {
            _services = services;
            _context = context;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UsersParameter usersParameter)
        {
            try
            {
                var pageResult = await _services.UsersService.GetAllUserAsync(usersParameter, trackChanges: false);
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pageResult.metaData));
                var responseDto = new ResponseDto()
                {
                    Result = pageResult.user,
                    IsSuccess = true,
                    Message = "Users data retrieved successfully"
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto
                {

                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var project = await _services.UsersService.GetUserAsync(userId, trackChanges: false);
 
            var responseDto = new ResponseDto
            {
                Result = project,
                IsSuccess = true,
                Message = "Project data retrieved successfully"

            };

            return Ok(responseDto);




        }
    }
}
