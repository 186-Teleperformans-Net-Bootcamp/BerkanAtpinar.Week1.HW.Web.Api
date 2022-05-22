
using BerkanAtpinar.Week1.HW.Web.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BerkanAtpinar.Week1.HW.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static List<Movie> MovieList = new List<Movie>()
        {
            new Movie{
                Id = 1,
                Title="The Shawshank Redemption",
                Director = "Frank Darabont",
                LeadRole = "Morgan Freeman",
                PublishYear = new DateTime(1994,01,01)
            },
            new Movie{
                Id = 2,
                Title="The Godfather",
                Director = "Francis Ford Coppola",
                LeadRole = "Marlon Brando",
                PublishYear = new DateTime(1972,01,01)
            },
            new Movie{
                Id = 3,
                Title="The Dark Knight",
                Director = "Christopher Nolan",
                LeadRole = "Christian Bale",
                PublishYear = new DateTime(2008,01,01)
            },
            new Movie{
                Id = 4,
                Title="The Godfather 2",
                Director = "Francis Ford Coppola",
                LeadRole = "Al Pacino",
                PublishYear = new DateTime(1994,01,01)
            }
        };

        [HttpGet]
        public IActionResult GetMovies()
        {
            if (MovieList.Count == 0)
                return NotFound("Listede herhangi bir kayıt yok");
            
            return Ok(MovieList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var movie = MovieList.Where(movie => movie.Id == id).SingleOrDefault();
            if(movie == null)
                return NotFound("Bu movie oluşturulmamış");
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] Movie newMovie)
        {
            if (newMovie is null)
                return BadRequest("Herhangi bir veri girilmedi");


            var movie = MovieList.SingleOrDefault(x => x.Id == newMovie.Id);

            if (movie is not null)
                return BadRequest("Bu movie listenizde var");
            
            MovieList.Add(newMovie);
            return Created("~api/movies",newMovie);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Movie newMovie)
        {
            if (newMovie is null)
                return BadRequest();

            var movie = MovieList.SingleOrDefault(x => x.Id == newMovie.Id);
            if(movie is not null)
            {
                movie.Title = newMovie.Title != default ? newMovie.Title : movie.Title;
                movie.Director = newMovie.Director != default ? newMovie.Director : movie.Director;
                movie.LeadRole = newMovie.LeadRole != default ? newMovie.LeadRole : movie.LeadRole;
                movie.PublishYear = newMovie.PublishYear != default ? newMovie.PublishYear : movie.PublishYear;
            }else
            {
                return NotFound();
            }
            return Ok(MovieList);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var movie = MovieList.SingleOrDefault(b => b.Id == id); 
            if (movie is null)
                return BadRequest("There is no record to delete!");

            MovieList.Remove(movie);
            return NoContent();
        }

        [HttpGet("{list}")]
        public IActionResult GetMoviesDirector([FromQuery] string director )
        {
            var movie = MovieList
                            .Where(x => x.Director == "Francis Ford Coppola").ToList();

            if(movie.Count == 0)
                return NotFound();
            return Ok(MovieList);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateAvailability(int id, string leadRole)
        {
            var movie = MovieList.SingleOrDefault(u => u.Id == id);
            if (movie != null)
            {

                MovieList.SingleOrDefault(g => g.Id == id).LeadRole = leadRole;
            }
            else
            {
                return NotFound("There is no record to update");
            }
            return NoContent(); 


        }
    }
}
