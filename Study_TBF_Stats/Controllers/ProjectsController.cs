using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Exceptions;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;
using Study_TBF_Stats.RequestFeatures;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Study_TBF_Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly StudyTbfSupContext _context;
        private ResponseDto _response;
        private IMapper _mapper;

        // Constructor của ProjectsController đặt các tham số vào biến thanh viên tương ứng, thông qua constructor để chuyển các dependency vào.
        public ProjectsController(StudyTbfSupContext context, IMapper mapper, IServiceManager service)
        {
            _context = context;
            _serviceManager = service;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]

        public async Task<IActionResult> GetProjects([FromQuery] ProjectParameters projectParameters)
        {
            try
            {
                if (!(projectParameters.MinFloorArea < projectParameters.MaxFloorArea))
                    throw new MaxAreaRangeBadRequestException();

                var pageResult = await _serviceManager.ProjectService.GetAllProjectAsync(projectParameters, trackChanges: false);
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pageResult.metaData));

                var usedhour = _context.TbTimeSheets
                    .GroupBy(ts => ts.ProjectId)
                    .Select(group => new
                    {
                        ProjectId = group.Key,
                        UsedHours = group.Sum(ts => ts.Tshours)
                    });


                foreach (var projectDto in pageResult.tbProjects)

                {
                    var timeSheetResult = usedhour.FirstOrDefault(ts => ts.ProjectId == projectDto.ProjectId);
                    projectDto.UsedHours = timeSheetResult != null ? timeSheetResult.UsedHours : 0;
                    // Chuỗi chứa danh sách userId cần lọc
                    var userIdString = projectDto.Listmember;
                    // Chuyển đổi chuỗi thành danh sách các userId
                    List<int> userIdsToFilter = userIdString
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(id => int.Parse(id.Trim('(', ')')))
                        .ToList();
                    // Lọc người dùng dựa trên userID
                    List<TbUser> filterUsers = _context.TbUsers.Where(user => userIdsToFilter.Contains(user.UserId)).ToList();

                    projectDto.FilterMembers = filterUsers;
                }
                if (projectParameters.Discipline == null)
                {
                    var responseDto = new ResponseDto
                    {
                        Result = pageResult.tbProjects,
                        IsSuccess = true,
                        Message = "Project data retrieved successfully"

                    };

                    return Ok(responseDto);
                }
                else
                {
                    var discipline = projectParameters.Discipline;
                    var filterByDiscipline = pageResult.tbProjects.Where(project => HasMemberWithDiscipline(project, discipline)).ToList();
                    static bool HasMemberWithDiscipline(TbProjectDto project, string? discipline)
                    {
                        if(project.FilterMembers != null && project.FilterMembers.Any())
                        {
                            return project.FilterMembers.Any(member=>member.Discipline==discipline);
                        }
                        return false;
                       
                    }


                    var responseDto = new ResponseDto
                    {
                        Result = filterByDiscipline,
                        IsSuccess = true,
                        Message = "Project data retrieved successfully"

                    };

                    return Ok(responseDto);
                }



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

   
        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> GetProject(int projectId)
        {
            var project = await _serviceManager.ProjectService.GetProjectAsync(projectId, trackChanges: false);
            var usedhour = _context.TbTimeSheets
                   .Where(ts => ts.ProjectId == projectId)
                   .GroupBy(ts => ts.ProjectId)
                   .Select(group => new
                   {
                       ProjectId = group.Key,
                       UsedHours = group.Sum(ts => ts.Tshours)
                   });




            project.UsedHours = usedhour == null ? 0 : usedhour.First().UsedHours;
            // Chuỗi chứa danh sách userId cần lọc
            var userIdString = project.Listmember;
                // Chuyển đổi chuỗi thành danh sách các userId
                List<int> userIdsToFilter = userIdString
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.Parse(id.Trim('(', ')')))
                    .ToList();
                // Lọc người dùng dựa trên userID
                List<TbUser> filterUsers = _context.TbUsers.Where(user => userIdsToFilter.Contains(user.UserId)).ToList();
                project.FilterMembers = filterUsers;
            


            var responseDto = new ResponseDto
            {
                Result = project,
                IsSuccess = true,
                Message = "Project data retrieved successfully"

            };

            return Ok(responseDto);


            

        }
        [HttpPost]
        public ResponseDto Post([FromForm] TbProjectDto tbProjectDto)
        {
            try
            {
                TbProject tbProject = _mapper.Map<TbProject>(tbProjectDto);
                _context.TbProjects.Add(tbProject);
                _context.SaveChanges();
                _response.Result = _mapper.Map<TbProjectDto>(tbProject);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut]
        public ResponseDto Put([FromForm] TbProjectDto tbProjectDto)
        {
            try
            {
                TbProject tbProject = _mapper.Map<TbProject>(tbProjectDto);
                _context.TbProjects.Update(tbProject);
                _context.SaveChanges();
                _response.Result = _mapper.Map<TbProjectDto>(tbProject);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public ResponseDto Delete(int id)
        {
            try
            {
                TbProject obj = _context.TbProjects.First(u => u.ProjectId == id);
                _context.TbProjects.Remove(obj);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
