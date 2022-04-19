using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using backend_learning.Infrastructure.DTOs.User;
using backend_learning.Domain.Entities;
using backend_learning.Infrastructure.Interfaces;
using Infrastructure.RequestObjects;


namespace backend_learning.Infrastructure.Repositories;

// The repository pattern implements a layer of abstraction between the classes which want to access the database and
// EF core. You are able to write cleaner and more understandable code, by not writing the LINQ queries yourself, but
// calling methods which do what you want 
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> UserAlreadyExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<UserOutputDto>> GetAllUserDtosAsync(bool trackChanges)
    {
        return await (trackChanges ?
            _context.Users
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .ToListAsync()
            :
            _context.Users
                    .AsNoTracking()
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .ToListAsync());
    }

    public async Task<User> GetUserByEmailAsync(string email, bool trackChanges)
    {
        return await (trackChanges ?
            _context.Users
                    .Include(p => p.Jobs)
                    .Where(x => x.Email == email)
                    .SingleOrDefaultAsync()
            :
            _context.Users
                    .AsNoTracking()
                    .Include(p => p.Jobs)
                    .Where(x => x.Email == email)
                    .SingleOrDefaultAsync());
    }

    public async Task<User> GetUserByIdAsync(int id, bool trackChanges)
    {
        return await (trackChanges ?
            _context.Users
                    .Include(p => p.Jobs)
                    .Where(x => x.UserId == id)
                    .SingleOrDefaultAsync()
            :
            _context.Users
                    .AsNoTracking()
                    .Include(p => p.Jobs)
                    .Where(x => x.UserId == id)
                    .SingleOrDefaultAsync());
    }

    public async Task<UserOutputDto> GetUserDtoByEmailAsync(string email, bool trackChanges)
    {
        return await (trackChanges ?
            _context.Users
                    .Where(x => x.Email == email)
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync()
            :
            _context.Users
                    .AsNoTracking()
                    .Where(x => x.Email == email)
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync());
    }

    public async Task<UserOutputDto> GetUserDtoByIdAsync(int id, bool trackChanges)
    {
        return await (trackChanges ?
            _context.Users
                    .Where(x => x.UserId == id)
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync()
            :
            _context.Users
                    .AsNoTracking()
                    .Where(x => x.UserId == id)
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync());
    }

    public async Task<IEnumerable<Job>> GetJobsByEmail(string email, JobRequestObject jobRequestObject)
    {
        var user = await GetUserByEmailAsync(email, true);
        await _context.Entry(user).Collection(p => p.Jobs).LoadAsync();

        var jobs = user.Jobs
                  .Skip((jobRequestObject.PageNumber - 1) * jobRequestObject.PageSize)
                  .Take(jobRequestObject.PageSize);

        return jobs;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.AddAsync(user);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }
}