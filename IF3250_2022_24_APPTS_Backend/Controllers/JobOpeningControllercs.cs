namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.JobOpening;
using IF3250_2022_24_APPTS_Backend.Services;
using IF3250_2022_24_APPTS_Backend.Entities;

[Authorize]
[ApiController]
[Route("[controller]")]
public class JobOpeningController : ControllerBase
{
    private IJobOpeningService _jobOpeningService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public JobOpeningController(
        IJobOpeningService jobOpeningService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _jobOpeningService = jobOpeningService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    /// <summary>Adding a Job Opening</summary>
    /// <returns>Message</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///        "job_id": 0,
    ///        "job_name": "Software Developer",
    ///        "start_recruitment_date": "16/03/2022",
    ///        "end_recruitment_date": "23/03/2022",
    ///        "job_type": "programming",
    ///        "salary": 10000000,
    ///        "description": "Adding Job Opening"
    ///     }
    ///
    /// </remarks>
    [HttpPost("add")]
    public async Task<IActionResult> Add(AddJobOpeningRequest model)
    {
        var company = (User)HttpContext.Items["User"];
        await _jobOpeningService.Add(company.user_id, model);
        return Ok(new { message = "Job opening added successfully" });
    }

    /// <summary>Fetching All Job Openings</summary>
    /// <returns>A list of all Job Openings</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var job_opening = await _jobOpeningService.GetAll();
        return Ok(job_opening);
    }

    /// <summary>Fetching a Job Opening by job_id</summary>
    /// <returns>1 Job Opening</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var job_opening = await _jobOpeningService.GetByJobId(id);
        return Ok(job_opening);
    }

    /// <summary>Deleting a Job Opening by job_id</summary>
    /// <returns>Message</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _jobOpeningService.Delete(id);
        return Ok(new { message = "Job opening deleted successfully" });
    }
}