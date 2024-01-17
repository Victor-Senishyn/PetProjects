using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;
using CinemaSim.CinemaMangement;
using CinemaSim.CustomExceptions;
using CinemaSim.Movies;
using CinemaSim.Users;
using CinemaSim.Authorization;
using System.Xml.Linq;

namespace CinemaSim
{
    public class CinemaUI
    {
        private Cinema _cinema;
        private Schedule _schedule;
        private User _user;

        public CinemaUI(IEnumerable<Movie> movies)
        {
            _cinema = new Cinema(movies.ToArray());
            _schedule = new Schedule();
            _schedule.AddMoviesToSchedule(_cinema.Movies);
        }

        private void DisplayMovies()
        {
            foreach (var movie in _cinema.Movies)
                Console.WriteLine(movie);
        }

        private void DisplaySchedule()
        {
            foreach (var movie in _schedule.GetMoviesFromSchedule())
                Console.WriteLine($"{movie.Key,-22}|\t{movie.Value}");
        }

        private void TakeTicket()
        {
            Console.WriteLine("Enter the movie`s title");
            string name = Console.ReadLine()!;
            Console.WriteLine("Enter your place");
            if (int.TryParse(Console.ReadLine(), out var place))
            {
                try
                {
                    _user.Tickets.Add(_schedule.ReserveSeat(_user, name, place));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex is InvalidTicketReservationException ||
                                        ex is SeatAlreadyTakenException ||
                                        ex is InsufficientFundsException
                            ? ex.Message : "Unexpected error occurred:");
                }
            }
            else
                Console.WriteLine($"Time entered is in the wrong format");
        }

        public void Run()
        {
            Console.WriteLine("Enter number of your choice:");
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Sign In");
            Console.WriteLine("3. Quit");

            bool isAuthorized = true;

            while (isAuthorized)
            {
                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    var commandHandler = AuthorizationFactory.BuildAuthorization(choice);
                    if (commandHandler == null)
                        return;

                    Console.WriteLine("Enter your name:");
                    string name = Console.ReadLine()!;
                    try
                    {
                        _user = commandHandler.Execute(name);
                        isAuthorized = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex is FileNotFoundException
                                ? "File not found at path:" : $"Error during deserialization: {ex.Message}");
                    }
                }

                DisplayMovies();
                Console.WriteLine("Press any button to continue:");
                Console.ReadKey();
                Console.Clear();

                while (true)
                {
                    Console.WriteLine("Enter number of your choice:");
                    Console.WriteLine("1. Replenish Balance");
                    Console.WriteLine("2. Reserve Seat");
                    Console.WriteLine("3. Show Info");
                    Console.WriteLine("4. Quit");
                    if (int.TryParse(Console.ReadLine(), out var result))
                    {
                        switch (result)
                        {
                            case (1):
                                Console.WriteLine("Enter balance replenishment amount:");
                                if (int.TryParse(Console.ReadLine(), out var amount))
                                {
                                    _user.ReplenishBalance(amount);
                                    Console.WriteLine($"Balance: {_user.Balance}");
                                }
                                else
                                    Console.WriteLine("Wrong Input");
                                break;
                            case (2):
                                DisplaySchedule();
                                Console.WriteLine("How many tickets do you want to order?");
                                if (int.TryParse(Console.ReadLine(), out var count))
                                {
                                    for (int i = 0; i < count; ++i)
                                        TakeTicket();
                                }
                                else
                                    Console.WriteLine("Wrong Input");
                                break;
                            case (3):
                                Console.WriteLine(_user);
                                break;
                            case (4):
                                _user.UpdateUserInXml();
                                return;
                            default:
                                Console.WriteLine("Wrong input");
                                break;
                        }
                    }
                }
            }
        }
    }
}
