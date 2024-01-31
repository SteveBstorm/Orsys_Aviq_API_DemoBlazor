using API_DemoBlazor.Models;

namespace API_DemoBlazor.Services
{
    public class MovieService
    {
        public List<Movie> List { get; set; }

        public MovieService() { 
            List = new List<Movie>();
            List.Add(new Movie
            {
                Id = 1,
                Title = "Kaamelott",
                ReleaseYear = 2022,
                Synopsis = "Des chevaliers qui cherchent un vase ou une coupe",
                Realisator = new Person { LastName = "Astier", FirstName = "Alexandre" }
            });

            List.Add(new Movie
            {
                Id = 2,
                Title = "Star Wars",
                ReleaseYear = 1977,
                Synopsis = "Un pirate et un wookie qui court après la princesse",
                Realisator = new Person { LastName = "Lucas", FirstName = "Georges" }
            });
        }

        public void Add(Movie movie)
        {
            movie.Id = List.Max(x => x.Id) + 1;
            List.Add(movie);
        }

        public List<Movie> GetAll()
        {
            return List;
        }
    }
}
