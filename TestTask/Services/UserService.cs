using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _ctx;

    public UserService(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<User> GetUser()
    {
        return await _ctx.Users
            .Where(_ => _.Orders.Any())
            .OrderByDescending(_ => _.Orders.Count())
            .FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetUsers()
    {
        return await _ctx.Users
            .Where(_ => _.Status == UserStatus.Inactive)
            .ToListAsync();
    }
}