namespace IF3250_2022_24_APPTS_Backend.Services;

using AutoMapper;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Data;
using IF3250_2022_24_APPTS_Backend.Entities;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.JobApplication;
using Microsoft.EntityFrameworkCore;

public interface IJobApplicationService
{
    IQueryable<Object> GetAll();
    Task<JobApplication> GetByJobApplicationId(int job_id);
    Task<JobApplication> Add(int company_id, AddJobApplicationRequest model);
    Task<JobApplication> Delete(int job_id);
}

public class JobApplicationService : IJobApplicationService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public JobApplicationService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public IQueryable<Object> GetAll()
    {
        return from a in _context.job_application 
                   join b in _context.job_opening on a.job_id equals b.job_id
                   join c in _context.user on b.company_id equals c.user_id
                   select new JobApplicationResponse 
                   { 
                       application_id = a.application_id,
                       job_id = a.job_id,
                       applicant_id = a.applicant_id,
                       apply_date = a.apply_date,
                       requirement_link = a.requirement_link,
                       status = a.status,
                       applicant_name = a.applicant_name,
                       applicant_email = a.applicant_email,
                       applicant_telp = a.applicant_telp,
                       job_name = b.job_name,
                       company_name = c.full_name,
                       company_picture = c.profile_picture,
                       country = c.country,
                       city = c.city
                   };
    }

    public async Task<JobApplication> GetByJobApplicationId(int job_application_id)
    {
        var job_application = await getJobApplication(job_application_id);
        return job_application;
    }

    public async Task<JobApplication> Add(int applicant_id, AddJobApplicationRequest model)
    {
        var user = await _context.user.SingleOrDefaultAsync(x => (x.user_id == applicant_id && x.type == "applicant"));
        // validate
        if (user == null)
            throw new AppException("Company cannot apply to a job!");

        // map model to new Job Application object
        var job_application = _mapper.Map<JobApplication>(model);

        job_application.applicant_id = applicant_id;

        // save job application
        await _context.job_application.AddAsync(job_application);
        await _context.SaveChangesAsync();

        return job_application;
    }

    public async Task<JobApplication> Delete(int application_id)
    {
        var job_application = await getJobApplication(application_id);
        _context.job_application.Remove(job_application);
        await _context.SaveChangesAsync();

        return job_application;
    }

    // helper methods
    private async Task<JobApplication> getJobApplication(int job_application_id)
    {
        var job_application = await _context.job_application.FindAsync(job_application_id);
        if (job_application == null) throw new KeyNotFoundException("Job application not found");
        return job_application;
    }
}
