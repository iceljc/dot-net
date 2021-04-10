using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public AdminController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }

        [HttpPost("movie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.CreateMovie(movieCreateRequest);
            return CreatedAtRoute("GetMovie", new {id = createdMovie.Id}, createdMovie);
        }

        [HttpPut("movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.UpdateMovie(movieCreateRequest);
            return Ok(createdMovie);
        }

        [HttpGet("purchases")]
        public async Task<IActionResult> GetAllPurchases([FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var movies = await _movieService.GetAllMoviePurchasesByPagination(pageSize, page);
            return Ok(movies);
        }
    }
}