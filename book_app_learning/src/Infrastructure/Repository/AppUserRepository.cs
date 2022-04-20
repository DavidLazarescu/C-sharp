using System.Linq.Expressions;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

public class AppUserRepository : IAppUserRepository
{
    private readonly DataContext _context;


    public AppUserRepository(DataContext context)
    {
        _context = context;
    }


    public async Task AddAppUser(AppUser user)
    {
        await _context.AddAsync(user);
    }

    public async Task<AppUser> GetAppUserByConditionAsync(Expression<Func<AppUser, bool>> predicate, bool trackChanges)
    {
        return await _context.AppUsers.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<AppUser>> GetAppUsersByConditionAsync(Expression<Func<AppUser, bool>> predicate, bool trackChanges)
    {
        return await _context.AppUsers.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _context.AppUsers.ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}