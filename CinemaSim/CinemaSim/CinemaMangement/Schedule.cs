using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSim.Movies;
using CinemaSim.Users;
using CinemaSim.CustomExceptions;
using System.Runtime.InteropServices;

namespace CinemaSim.CinemaMangement
{
    public class Schedule
    {
        private Dictionary<DateTime, CinemaHall> _hallsInSchedule;

        private Dictionary<Movie, DateTime> _moviesInSchedule;

        public IEnumerable<KeyValuePair<Movie, DateTime>> GetMoviesFromSchedule() 
            => _moviesInSchedule;

        public Schedule() => (_moviesInSchedule, _hallsInSchedule) =
            (new Dictionary<Movie, DateTime>(), new Dictionary<DateTime, CinemaHall>());

        public void AddMoviesToSchedule(List<Movie> movies)
        {
            DateTime currentTime = Convert.ToDateTime("09:00");

            foreach (var movie in movies)
            {
                if (movie != null && movie.Duration <= Constants.Closing)
                {
                    _moviesInSchedule.Add(movie, currentTime);
                    var cinemaHall = new CinemaHall();
                    _hallsInSchedule.Add(currentTime, cinemaHall);

                    currentTime = currentTime.Add(movie.Duration.TimeOfDay);
                }
            }
        }
        public void RemoveMoviesFromSchedule() => _moviesInSchedule.Clear();

        public Ticket ReserveSeat(User user, string name, int place)
        {
            var searchMovie = _moviesInSchedule.FirstOrDefault(m => m.Key.Name == name);

            if (searchMovie.Key != null)
            {
                if (_hallsInSchedule[searchMovie.Value].IsPlaceFree(place))
                {
                    foreach (var movie in _moviesInSchedule)
                    {
                        if (name == movie.Key.Name)
                        {
                            var ticket = new Ticket(movie.Key, movie.Value.Date, place);
                            user.WithdrawBalance(ticket.Price);
                            _hallsInSchedule[movie.Value].Places[place - 1] = false;
                            return ticket;
                        }
                    }
                    throw new InvalidTicketReservationException();
                }
                else
                {
                    throw new SeatAlreadyTakenException();
                }
            }
            else
            {
                throw new ArgumentException("Movie not found", nameof(name));
            }
        }
    }
}
