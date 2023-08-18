using Microsoft.AspNetCore.Mvc;
using TMDBLibrary.Services;
using TMDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;  // For .ToList()
using System.Threading.Tasks;

namespace TMDB_App.Controllers
{
    public class MoviesController : Controller
    {
        private readonly TMDBService _tmdbService;

        public MoviesController(TMDBService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        // Test method to see if the endpoint is hit
        public IActionResult Test()
        {
            return Content("Hello World! MoviesController is accessible.");
        }

        // Immediate testing method
        public IActionResult Index()
        {
            // This will override your API fetching just for this test
            var movies = new List<Movie>
            {
                new Movie { Title = "Test Movie", Description = "Test Description", ReleaseDate = DateTime.Now }
            };

            return View(movies);
        }

        // Your original method renamed for reference, you can remove this once everything is working
        public async Task<IActionResult> OriginalIndexMethod()
        {
            List<Movie> movies;
            try
            {
                movies = (await _tmdbService.GetPopularMovies()).ToList();

                if (movies == null || movies.Count == 0)
                {
                    movies = new List<Movie>();
                    ViewBag.Message = "No movies found!";
                }
            }
            catch (Exception ex)
            {
                movies = new List<Movie>();
                ViewBag.Message = "There was an error fetching the movies. Please try again later.";
            }

            return View("Index", movies);  // Specify the "Index" view explicitly
        }
    }
}
