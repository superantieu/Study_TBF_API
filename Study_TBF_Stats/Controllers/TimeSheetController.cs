using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Models.Dto;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly IServiceManager _services;
        
        private readonly IMapper _mapper;
        public TimeSheetController(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _services = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeSheets([FromQuery] TimeSheetsParameter timeSheetsParameter)
        {
            try
            {
                var pageResult = await _services.TimeSheetService.GetAllTimeSheetAsync(timeSheetsParameter, trackChanges: false);
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pageResult.metaData));
                var _responseDto = new ResponseDto()
                {
                    Result = pageResult.timesheets,
                    IsSuccess = true,
                    Message = "TimeSheets data retrieved successfully"
                };

                return Ok(_responseDto);
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

    }
}
