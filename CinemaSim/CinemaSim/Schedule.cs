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
        public Dictionary<DateTime,Movie> FilmsInSchedule { get; set; }

        public Schedule() => FilmsInSchedule = new Dictionary<DateTime, Movie>();

        public void AddMoviesToSchedule(List<Movie> movies)
        {
            DateTime currentTime = Convert.ToDateTime("09:00");

            foreach (var movie in movies)
            {
                if (movie != null && movie.Duration <= _closing)
                {
                    FilmsInSchedule.Add(currentTime, movie);
                    currentTime = currentTime.Add(movie.Duration.TimeOfDay);
                }
            }
        }
        public void RemoveMoviesFromSchedule() => FilmsInSchedule.Clear();
        public Ticket ReserveSeat(DateTime time)
        {

            foreach (var movie in FilmsInSchedule)
            {
                if (time == movie.Key)
                {
                    var ticket = new Ticket(movie.Value, time);
                    return ticket;
                }
            }
            throw new InvalidTicketReservationException("Incorrect time entered");
        }
    }
}
