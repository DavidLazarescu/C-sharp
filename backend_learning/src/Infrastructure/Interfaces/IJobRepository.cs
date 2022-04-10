using backend_learning.Infrastructure.DTOs;
using backend_learning.Domain.Entities;

namespace backend_learning.Infrastructure.Interfaces
{
    public interface IJobRepository
    {
        public Task<Job> FindJobWithName(string name);
        public Task<JobOutputDto> InsertIfDoesNotExist(JobInputDto inputDto);
        public Task<IEnumerable<JobOutputDto>> GetAllJobDtos();
    }
}