using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSim.Movies;
using CinemaSim.Users;
using CinemaSim.CustomExceptions;

namespace CinemaSim.CinemaMangement
{
    public class Schedule
    {
        private readonly DateTime _closing = Convert.ToDateTime("21:00");

        private Dictionary<DateTime, CinemaHall> _hallsInSchedule;

        public Dictionary<DateTime, Movie> FilmsInSchedule { get; set; }

        public Schedule() => (FilmsInSchedule, _hallsInSchedule) =
            (new Dictionary<DateTime, Movie>(), new Dictionary<DateTime, CinemaHall>());

        public void AddMoviesToSchedule(List<Movie> movies)
        {
            DateTime currentTime = Convert.ToDateTime("09:00");

            foreach (var movie in movies)
            {
                if (movie != null && movie.Duration <= _closing)
                {
                    FilmsInSchedule.Add(currentTime, movie);
                    var cinemaHall = new CinemaHall();
                    _hallsInSchedule.Add(currentTime, cinemaHall);

                    currentTime = currentTime.Add(movie.Duration.TimeOfDay);
                }
            }
        }
        public void RemoveMoviesFromSchedule() => FilmsInSchedule.Clear();

        public Ticket ReserveSeat(User user, DateTime time, int place)
        {
            if (_hallsInSchedule[time].IsPlaceFree(place))
            {
                foreach (var movie in FilmsInSchedule)
                {
                    if (time == movie.Key)
                    {
                        var ticket = new Ticket(movie.Value, time, place);
                        user.WithdrawBalance(ticket.Price);
                        _hallsInSchedule[time].Places[place - 1] = false;
                        return ticket;
                    }
                }
                throw new InvalidTicketReservationException();
            }
            throw new SeatAlreadyTakenException();
        }
    }
}
