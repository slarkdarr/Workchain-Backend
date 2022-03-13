namespace IF3250_2022_24_APPTS_Backend.Services;

using AutoMapper;
using BCrypt.Net;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Data;
using IF3250_2022_24_APPTS_Backend.Entities;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.User;
using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<List<Applicant>> GetAll();
    Task<Applicant> GetOne(int id);
    Task<Applicant> Register(RegisterRequest model);
    Task<Applicant> Update(int id, UpdateRequest model);
    Task<Applicant> Delete(int id);
}

public class UserService : IUserService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public UserService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var user = await _context.applicant.SingleOrDefaultAsync(x => x.email == model.email);
        System.Diagnostics.Debug.WriteLine(model.password);

        // validate
        if (user == null || !BCrypt.Verify(model.password, user.password))
            throw new AppException("Email or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        System.Diagnostics.Debug.WriteLine("Kontol");
        response.Token = _jwtUtils.GenerateToken(user);
        //System.Diagnostics.Debug.WriteLine(response.Token);
        return response;
    }

    public async Task<List<Applicant>> GetAll()
    {
        return await _context.applicant.ToListAsync();
    }

    public async Task<Applicant> GetOne(int id)
    {
        var user = await getUser(id);
        return user;
    }

    public async Task<Applicant> Register(RegisterRequest model)
    {
        // validate
        if (await _context.applicant.AnyAsync(x => x.email == model.email))
            throw new AppException("Email '" + model.email + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<Applicant>(model);
        Console.WriteLine(user);

        // hash password
        user.password = BCrypt.HashPassword(model.password);

        // set all properties to null (TODO)
        user.profile_picture = null;
        user.birthdate = null;
        user.gender = null;
        user.country = null;
        user.city = null;
        user.headline = null;
        user.description = null;
        user.status = null;

        // save user
        await _context.applicant.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<Applicant> Update(int id, UpdateRequest model)
    {
        var user = await getUser(id);

        // validate
        if (model.email != user.email && await _context.applicant.AnyAsync(x => x.email == model.email))
            throw new AppException("Email '" + model.email + "' is already taken");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.password))
            user.password = BCrypt.HashPassword(model.password);

        // copy model to user and save
        _mapper.Map(model, user);
        Console.WriteLine(user);
        _context.applicant.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<Applicant> Delete(int id)
    {
        var user = await getUser(id);
        _context.applicant.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

    // helper methods


    private async Task<Applicant> getUser(int id)
    {
        var user = await _context.applicant.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

}
