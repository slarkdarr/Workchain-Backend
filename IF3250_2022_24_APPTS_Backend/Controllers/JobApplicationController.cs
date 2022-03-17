namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.JobApplication;
using IF3250_2022_24_APPTS_Backend.Services;
using IF3250_2022_24_APPTS_Backend.Entities;

[Authorize]
[ApiController]
[Route("[controller]")]
public class JobApplicationController : ControllerBase
{
    private IJobApplicationService _jobApplicationService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public JobApplicationController(
        IJobApplicationService jobApplicationService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _jobApplicationService = jobApplicationService;
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
    public async Task<IActionResult> Add(AddJobApplicationRequest model)
    {
        var company = (User)HttpContext.Items["User"];
        await _jobApplicationService.Add(company.user_id, model);
        return Ok(new { message = "Job Application added successfully" });
    }

    /// <summary>Fetching All Job Openings</summary>
    /// <returns>A list of all Job Openings</returns>
    [HttpGet]
    public IQueryable<Object> GetAll()
    {
        var job_application = _jobApplicationService.GetAll();
        return job_application;
    }

    /// <summary>Fetching a Job Opening by job_id</summary>
    /// <returns>1 Job Opening</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var job_application = await _jobApplicationService.GetByJobApplicationId(id);
        return Ok(job_application);
    }

    /// <summary>Deleting a Job Opening by job_id</summary>
    /// <returns>Message</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _jobApplicationService.Delete(id);
        return Ok(new { message = "Job Application deleted successfully" });
    }
}