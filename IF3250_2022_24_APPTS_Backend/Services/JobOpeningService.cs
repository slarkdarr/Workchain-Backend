namespace IF3250_2022_24_APPTS_Backend.Services;

using AutoMapper;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Data;
using IF3250_2022_24_APPTS_Backend.Entities;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.JobOpening;
using Microsoft.EntityFrameworkCore;

public interface IJobOpeningService
{
    Task<List<JobOpening>> GetAll();
    Task<JobOpening> GetByJobId(int job_id);
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

    public async Task<List<JobOpening>> GetAll()
    {
        return await _context.job_opening.ToListAsync();
    }

    public async Task<JobOpening> GetByJobId(int job_id)
    {
        var job_opening = await getJobOpening(job_id);
        return job_opening;
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
