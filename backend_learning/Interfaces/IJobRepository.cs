using backend_learning.DTOs;
using backend_learning.Entities;

namespace backend_learning.Interfaces
{
    public interface IJobRepository
    {
        public Task<Job> FindJobWithName(string name);
        public Task<JobOutputDto> InsertIfDoesNotExist(JobInputDto inputDto);
        public Task<IEnumerable<JobOutputDto>> GetAllJobDtos();
    }
}