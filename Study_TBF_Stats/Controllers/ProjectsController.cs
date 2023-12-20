using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;
using System.Collections.Generic;

namespace Study_TBF_Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly StudyTbfSupContext _context;
        private ResponseDto _response;
        private IMapper _mapper;
        public ProjectsController(StudyTbfSupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {

                var result = _context.TbTimeSheets
                    .GroupBy(ts => ts.ProjectId)
                    .Select(group => new
                    {
                        ProjectId = group.Key,
                        UsedHours = group.Sum(ts => ts.Tshours)
                    });

                var combinedResults = _mapper.Map<IEnumerable<TbProjectDto>>(_context.TbProjects);

                foreach (var projectDto in combinedResults)

                {
                    var timeSheetResult = result.FirstOrDefault(ts => ts.ProjectId == projectDto.ProjectId);
                    projectDto.UsedHours = timeSheetResult != null ? timeSheetResult.UsedHours : 0;
                }

                _response.Result = combinedResults.ToList();



            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                TbProject obj = _context.TbProjects.First(u => u.ProjectId == id);
                _response.Result = _mapper.Map<TbProjectDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
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
