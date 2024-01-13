using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class Schedule
    {
        private readonly DateTime _closing = Convert.ToDateTime("21:00");
        public List<MovieScreening> FilmsInSchedule { get; set; }

        public Schedule() => FilmsInSchedule = new List<MovieScreening>();

        public void AddMoviesToSchedule(List<Movie> movies)
        {
            DateTime currentTime = Convert.ToDateTime("09:00");

            foreach (var movie in movies)
            {
                if (movie != null && movie.TimeOfTheFilm <= _closing)
                {
                    FilmsInSchedule.Add(new MovieScreening(currentTime, movie));
                    currentTime = currentTime.Add(movie.TimeOfTheFilm.TimeOfDay);
                }
            }
        }
        public void RemoveMoviesFromSchedule() => FilmsInSchedule.Clear();
        public Ticket ReserveSeat(DateTime time)
        {

            foreach (var movie in FilmsInSchedule)
            {
                if (time == movie.StartTime)
                {
                    var ticket = new Ticket(movie.Screening, time);
                    return ticket;
                }
            }
            throw new InvalidTicketReservationException("Incorrect time entered");
        }
    }
}
