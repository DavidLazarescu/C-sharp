using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend_learning.Infrastructure.DTOs;
using backend_learning.Domain.Entities;
using backend_learning.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_learning.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public JobRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<Job> GetJobWithName(string name, bool trackChanges) 
        {
            if(trackChanges)
                return await _context.Jobs.SingleOrDefaultAsync(p => p.Name == name);
            else
                return await _context.Jobs.AsNoTracking()
                                          .SingleOrDefaultAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<JobOutputDto>> GetAllJobDtos(bool trackChanges) 
        {
            if(trackChanges)
                return await _context.Jobs.ProjectTo<JobOutputDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync();
            else
                return await _context.Jobs.AsNoTracking()
                                          .ProjectTo<JobOutputDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync();
        }

        public async Task<JobOutputDto> InsertIfDoesNotExist(JobInputDto jobInputDto)
        {
            if (await _context.Jobs.AnyAsync(p => p.Name == jobInputDto.Name))
                return null;

            var job = _mapper.Map<Job>(jobInputDto);
            await _context.AddAsync(job);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<JobOutputDto>(job);
        }
    }
}