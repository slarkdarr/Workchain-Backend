namespace IF3250_2022_24_APPTS_Backend.Services;

using AutoMapper;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Data;
using IF3250_2022_24_APPTS_Backend.Entities;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.JobOpening;
using Microsoft.EntityFrameworkCore;
//using NHibernate.Linq;

public interface IJobOpeningService
{
    IQueryable<Object> GetAll();
    IQueryable<Object> GetJobOpeningByKeyword(string jobKeyword);
    IQueryable<Object> GetByJobId(int job_id);
    Task<List<JobOpening>> GetByCompanyId(int company_id);
    Task<JobOpening> Add(int company_id, AddJobOpeningRequest model);
    Task<JobOpening> Delete(int job_id);
}

public class JobOpeningService : IJobOpeningService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public JobOpeningService(
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
        return from a in _context.job_opening
               join b in _context.user on a.company_id equals b.user_id
               select new JobOpeningResponse
               {
                   job_id = a.job_id,
                   job_name = a.job_name,
                   start_recruitment_date = a.start_recruitment_date,
                   end_recruitment_date = a.end_recruitment_date,
                   job_type = a.job_type,
                   salary = a.salary,
                   description = a.description,
                   company_id = a.company_id,
                   company_name = b.full_name,
                   company_city = b.city,
                   company_profile_picture = b.profile_picture,
               };
    }

    public IQueryable<Object> GetJobOpeningByKeyword(string jobKeyword)
    {
        return from a in _context.job_opening
               join b in _context.user on a.company_id equals b.user_id
               where a.job_name.ToLower().Contains(jobKeyword.ToLower())
               select new JobOpeningResponse
               {
                   job_id = a.job_id,
                   job_name = a.job_name,
                   start_recruitment_date = a.start_recruitment_date,
                   end_recruitment_date = a.end_recruitment_date,
                   job_type = a.job_type,
                   salary = a.salary,
                   description = a.description,
                   company_id = a.company_id,
                   company_name = b.full_name,
                   company_city = b.city,
                   company_profile_picture = b.profile_picture,
               };
    }

    public IQueryable<Object> GetByJobId(int job_id)
    {
        return from a in _context.job_opening
               join b in _context.user on a.company_id equals b.user_id
               where a.job_id == job_id
               select new JobOpeningResponse
               {
                   job_id = a.job_id,
                   job_name = a.job_name,
                   start_recruitment_date = a.start_recruitment_date,
                   end_recruitment_date = a.end_recruitment_date,
                   job_type = a.job_type,
                   salary = a.salary,
                   description = a.description,
                   company_id = a.company_id,
                   company_name = b.full_name,
                   company_city = b.city,
                   company_profile_picture = b.profile_picture,
               };
    }

    public async Task<List<JobOpening>> GetByCompanyId(int company_id)
    {
        return await _context.job_opening.Where(x => (x.company_id == company_id)).ToListAsync();
    }

    public async Task<JobOpening> Add(int company_id, AddJobOpeningRequest model)
    {
        var user = await _context.user.SingleOrDefaultAsync(x => (x.user_id == company_id && x.type == "company"));
        // validate
        if (user == null)
            throw new AppException("User cannot make a job opening!");

        // map model to new user object
        var job_opening = _mapper.Map<JobOpening>(model);
        job_opening.company_id = company_id;

        // save user
        await _context.job_opening.AddAsync(job_opening);
        await _context.SaveChangesAsync();

        return job_opening;
    }

    public async Task<JobOpening> Delete(int job_id)
    {
        var job_opening = await getJobOpening(job_id);
        _context.job_opening.Remove(job_opening);
        await _context.SaveChangesAsync();

        return job_opening;
    }

    // helper methods
    private async Task<JobOpening> getJobOpening(int job_id)
    {
        var job_opening = await _context.job_opening.FindAsync(job_id);
        if (job_opening == null) throw new KeyNotFoundException("Job opening not found");
        return job_opening;
    }

}
