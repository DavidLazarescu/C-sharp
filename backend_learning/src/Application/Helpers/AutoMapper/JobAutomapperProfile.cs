using AutoMapper;
using backend_learning.Infrastructure.DTOs;
using backend_learning.Domain.Entities;

namespace backend_learning.Application.Helpers.AutoMapper
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