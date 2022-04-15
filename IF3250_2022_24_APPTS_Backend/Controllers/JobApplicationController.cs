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

    public JobApplicationController(
        IJobApplicationService jobApplicationService)
    {
        _jobApplicationService = jobApplicationService;
    }


    /// <summary>Adding a Job Application</summary>
    /// <returns>Message</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///        "job_id": 0,
    ///        "apply_date": "16/03/2022",
    ///        "status": "In Review",
    ///        "applicant_name": "Jordan Daniel Joshua",
    ///        "applicant_email": "jordan@gmail.com",
    ///        "applicant_telp" : 12345678,
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

    /// <summary>Fetching All Job Applications</summary>
    /// <returns>A list of all Job Applications</returns>
    [HttpGet]
    public IQueryable<Object> GetAll()
    {
        var job_application = _jobApplicationService.GetAll();
        return job_application;
    }

    /// <summary>Fetching a Job Opening by application_id</summary>
    /// <returns>1 Job Application</returns>
    [HttpGet("{id}")]
    public IQueryable<Object> GetById(int id)
    {
        var job_application = _jobApplicationService.GetByJobApplicationId(id);
        return job_application;
    }

    /// <summary>Fetching All Job Applications By applicant_id</summary>
    /// <returns>A list of Job Applications</returns>
    [HttpGet("applicant")]
    public IQueryable<Object> GetByApplicantId()
    {
        var applicant = (User)HttpContext.Items["User"];
        var job_application = _jobApplicationService.GetByApplicantId(applicant.user_id);
        return job_application;
    }

    /// <summary>Fetching All Job Applications By company_id</summary>
    /// <returns>A list of Job Applications</returns>
    [HttpGet("company")]
    public IQueryable<Object> GetByCompanyId()
    {
        var company = (User)HttpContext.Items["User"];
        var job_application = _jobApplicationService.GetByCompanyId(company.user_id);
        return job_application;
    }

    /// <summary>Update Job Application</summary>
    /// <returns>Message</returns>
    /// <remarks>
    /// Requires Bearer Token in Header
    /// 
    /// Sample request:
    ///
    ///     {
    ///       "application_id": 1,
    ///       "status" : "Interview",
    ///       "interview_date" : "16/04/2022",
    ///       "interview_time" : "16:00",
    ///       "interview_link" : "meet.google.com/aws-aws-aws"
    ///     }
    ///      
    /// </remarks>
    [HttpPut]
    public async Task<IActionResult> Update(UpdateJobApplicationRequest model)
    {
        await _jobApplicationService.Update(model);
        return Ok(new { message = "Job Application updated successfully" });

    }

    /// <summary>Deleting a Job Application by application_id</summary>
    /// <returns>Message</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _jobApplicationService.Delete(id);
        return Ok(new { message = "Job Application deleted successfully" });
    }
}