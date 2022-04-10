using AutoMapper;
using backend_learning.Infrastructure.DTOs;
using backend_learning.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend_learning.Controllers
{
    [Route("api/jobs")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobController(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOutputDto>>> GetAllJobs()
        {
            return Ok(await _jobRepository.GetAllJobDtos(trackChanges: false));
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<JobOutputDto>>> GetJob(string name)
        {
            return Ok(await _jobRepository.GetJobWithName(name, trackChanges: false));
        }

        [HttpPost]
        public async Task<ActionResult<JobOutputDto>> CreateNewJob([FromBody] JobInputDto jobInputDto)
        {
            var job = await _jobRepository.InsertIfDoesNotExist(jobInputDto);
            if(job == null)
                return Ok("A job with this name already exists");

            return job;
        }
    }
}