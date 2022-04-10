using backend_learning.Infrastructure.DTOs;
using backend_learning.Domain.Entities;

namespace backend_learning.Infrastructure.Interfaces
{
    public interface IJobRepository
    {
        public Task<Job> GetJobWithName(string name, bool trackChanges);
        public Task<JobOutputDto> InsertIfDoesNotExist(JobInputDto inputDto);
        public Task<IEnumerable<JobOutputDto>> GetAllJobDtos(bool trackChanges);
    }
}