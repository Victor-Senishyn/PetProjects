using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class Cinema : IEnumerable<Movie>, IEnumerable<KeyValuePair<DateTime, Movie>>
    {
        private List<Movie> _movies;

        private Dictionary<DateTime, Movie> _schedule;

        public Cinema() => _movies = new List<Movie>();

        public Cinema(params Movie[] movies) => _movies = new List<Movie>(movies);

        public IEnumerable<KeyValuePair<DateTime, Movie>> GetSchedule()
        {
            foreach (var movie in _schedule)
                yield return movie;
        }

        public void AddMovie(Movie movie) => _movies.Add(movie);

        public IEnumerable<Movie> GetFilms()
        {
            foreach (var movie in _movies)
                yield return movie;
        }
        public void AddMoviesToSchedule(params string[] movieNames)
        {
            _schedule = new Dictionary<DateTime, Movie>();
            DateTime workTime = Convert.ToDateTime("09:00");
            DateTime closing = Convert.ToDateTime("21:00");

            foreach (var movieName in movieNames)
            {
                var movie = _movies.FirstOrDefault(x => x.Name == movieName);
                if (movie != null && movie.TimeOfTheFilm <= closing)
                {
                    _schedule.Add(workTime, movie);
                    workTime = workTime.Add(movie.TimeOfTheFilm.TimeOfDay);
                }
            }
        }

        public Ticket ReserveSeat(DateTime time)
        {

            foreach (var movie in _schedule)
            {
                if (time == movie.Key)
                {
                    var ticket = new Ticket(movie.Value, time);
                    return ticket;
                }
            }
            return null;
        }

        public IEnumerator<Movie> GetEnumerator() => _movies.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_movies).GetEnumerator();

        IEnumerator<KeyValuePair<DateTime, Movie>> IEnumerable<KeyValuePair<DateTime, Movie>>.GetEnumerator() 
            => ((IEnumerable<KeyValuePair<DateTime, Movie>>)_schedule).GetEnumerator();
    }
}
