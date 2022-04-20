using System.Linq.Expressions;
using Domain.Entities;

namespace Infrastructure.Interfaces.Repository;

public interface IAppUserRepository
{
    public Task<AppUser> GetAppUserByConditionAsync(Expression<Func<AppUser, bool>> predicate, bool trackChanges);
    public Task<IEnumerable<AppUser>> GetAppUsersByConditionAsync(Expression<Func<AppUser, bool>> predicate, bool trackChanges);
    public Task<IEnumerable<AppUser>> GetAllUsers();
    public Task<bool> SaveChangesAsync();
    public Task AddAppUser(AppUser user);
}