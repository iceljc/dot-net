using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> BuyMovie(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchase)
        {
            await _userService.PurchaseMovie(purchase);
            return Ok();
        }
    }
}