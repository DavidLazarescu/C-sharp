using AutoMapper;
using backend_learning.DTOs;
using backend_learning.Entities;

namespace backend_learning.Helpers.AutoMapper
{
    public class JobAutomapperProfile : Profile
    {
        public JobAutomapperProfile()
        {
            CreateMap<Job, JobOutputDto>();
            CreateMap<JobInputDto, Job>();
        }
    }
}