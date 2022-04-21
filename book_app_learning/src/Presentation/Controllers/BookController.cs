using Application.Common.Dtos;
using Application.Common.Interfaces.Services;
using Application.Common.RequestParameters;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookOutDto>>> GetBooks([FromQuery] BookRequestParameter requestParameter)
        {
            if(requestParameter == null)
            {
                _logger.LogInformation("Getting books failed due to the request parameter being null");
                return BadRequest("Invalid request parameters");
            }

            return Ok(await _bookService.GetBooks(requestParameter));
        }
    }
}