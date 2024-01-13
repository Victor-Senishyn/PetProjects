using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class Schedule : IEnumerable<MovieScreening>
    {
        private List<MovieScreening> _schedule;
        private readonly DateTime _closing = Convert.ToDateTime("21:00");

        public Schedule() => _schedule = new List<MovieScreening>();

        public void AddMoviesToSchedule(List<Movie> movies)
        {
            DateTime workTime = Convert.ToDateTime("09:00");

            foreach (var movie in movies)
            {
                if (movie != null && movie.TimeOfTheFilm <= _closing)
                {
                    _schedule.Add(new MovieScreening(workTime, movie));
                    workTime = workTime.Add(movie.TimeOfTheFilm.TimeOfDay);
                }
            }
        }
        public void RemoveMoviesFromSchedule() => _schedule.Clear();
        public Ticket ReserveSeat(DateTime time)
        {

            foreach (var movie in _schedule)
            {
                if (time == movie.StartTime)
                {
                    var ticket = new Ticket(movie.Screening, time);
                    return ticket;
                }
            }
            throw new InvalidTicketReservationException("Incorrect time entered");
        }

        public IEnumerator<MovieScreening> GetEnumerator() => _schedule.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_schedule).GetEnumerator();
    }
}
