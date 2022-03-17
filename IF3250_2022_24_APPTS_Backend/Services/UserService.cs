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
    Task<List<User>> GetAll();
    Task<User> GetOne(int id);
    Task<User> Register(RegisterRequest model);
    Task<User> Update(int id, UpdateRequest model);
    Task<User> Delete(int id);
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
        var user = await _context.user.SingleOrDefaultAsync(x => (x.email == model.email && x.type == model.type));

        // validate
        if (user == null || !BCrypt.Verify(model.password, user.password))
            throw new AppException("Email or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtUtils.GenerateToken(user);
        //System.Diagnostics.Debug.WriteLine(response.Token);
        return response;
    }

    public async Task<List<User>> GetAll()
    {
        return await _context.user.ToListAsync();
    }

    public async Task<User> GetOne(int id)
    {
        var user = await getUser(id);
        return user;
    }

    public async Task<User> Register(RegisterRequest model)
    {
        // validate
        if (await _context.user.AnyAsync(x => x.email == model.email))
            throw new AppException("Email '" + model.email + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<User>(model);
        Console.WriteLine(user);

        // hash password
        user.password = BCrypt.HashPassword(model.password);

        // save user
        await _context.user.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> Update(int id, UpdateRequest model)
    {
        var user = await getUser(id);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.user.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> Delete(int id)
    {
        var user = await getUser(id);
        _context.user.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

    // helper methods


    private async Task<User> getUser(int id)
    {
        var user = await _context.user.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

}
