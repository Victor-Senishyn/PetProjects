using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class Cinema
    {
        private List<Movie> _movies;

        private Dictionary<int, Movie> _schedule;

        public Cinema() => _movies = new List<Movie>();

        public Cinema(params Movie[] movies) => _movies = new List<Movie>(movies);

        private void ShowSchedule()
        {
            int i = 1;
            Console.WriteLine("Schedule:");
            foreach (var movie in _schedule)
                Console.WriteLine($"{i++}) {TimeSpan.FromMinutes(movie.Key).ToString(@"hh\:mm")} - {movie.Value}");
        }

        public void AddMovie(Movie movie) => _movies.Add(movie);

        public void ShowFilms() => _movies.ForEach(movie => Console.WriteLine(movie));

        public void AddMoviesToSchedule(params string[] movieNames)
        {
            _schedule = new Dictionary<int, Movie>(); 
            int workTime = 540;
            int closingInMinutes = 1260;

            foreach (var movieName in movieNames)
            {
                var movie = _movies.FirstOrDefault(x => x.Name == movieName);
                if (movie != null && movie.TimeOfTheFilm <= closingInMinutes - workTime)
                {
                    _schedule.Add(workTime, movie);
                    workTime += movie.TimeOfTheFilm;
                }
            }
        }

        public Ticket ReserveSeat()
        {
            ShowSchedule();
            Console.WriteLine($"\nEnter the movie time:");
            string time = Console.ReadLine();

            foreach (var movie in _schedule)
            {
                if (time == TimeSpan.FromMinutes(movie.Key).ToString(@"hh\:mm"))
                {
                    var ticket = new Ticket(movie.Value, time);
                    Console.WriteLine(ticket);
                    return ticket;
                }
            }

            Console.WriteLine("You choise wrong time. If you want quit Enter 'q'");
            if (Console.ReadLine() == "q")
                return null;
            return ReserveSeat();
        }
    }
}
