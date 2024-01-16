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

namespace CinemaSim
{
    internal class CinemaUI
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
            foreach(var movie in _cinema.Movies)
                Console.WriteLine(movie);
        }

        private void DisplaySchedule()
        {
            foreach (var movie in _schedule.FilmsInSchedule)
                Console.WriteLine($"{movie.Key,-22}|\t{movie.Value}");
        }

        private void SignUp(string name)
        {
            decimal balanceForTest = 0;
            _user = new User(name, balanceForTest);
            _user.SerializeToXml();
        }

        private void SignIn(string login)
        {
            if (UserExtensions.GetUserFromXml(login) == null)
                throw new NullReferenceException();
            _user = UserExtensions.GetUserFromXml(login)!;
        }
        private void TakeTicket(User user)
        {
            Console.WriteLine("Enter the time in the format 00:00");
            if (DateTime.TryParse(Console.ReadLine(), out var time))
            {
                Console.WriteLine("Enter your place");
                if (int.TryParse(Console.ReadLine(), out var place))
                {
                    try
                    {
                        user.Tickets.Add(_schedule.ReserveSeat(user, time, place));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex is InvalidTicketReservationException ||
                                          ex is SeatAlreadyTakenException ||
                                          ex is InsufficientFundsException
                                ? ex.Message : "Unexpected error occurred:");
                    }
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
                    string name = "";
                    if (choice == 1 || choice == 2)
                    {
                        Console.WriteLine("Enter your name:");
                        name = Console.ReadLine()!;
                    }
                    switch (choice)
                    {
                        case (1):
                            SignUp(name);
                            isAuthorized = false;
                            break;
                        case (2):
                            try
                            {
                                SignIn(name);
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine($"File not found at path:");
                                return;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error during deserialization: {ex.Message}");
                                return;
                            }
                            isAuthorized = false;
                            break;
                        case (3):
                            return;
                        default:
                            Console.WriteLine("Wrong input");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong Input");
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
                if(int.TryParse(Console.ReadLine(), out var result)) { 
                    switch (result)
                    {
                        case(1):
                            Console.WriteLine("Enter balance replenishment amount:");
                            if (int.TryParse(Console.ReadLine(), out var amount))
                            {
                                _user.ReplenishBalance(amount);
                                Console.WriteLine($"Balance: {_user.Balance}");
                            }
                            else
                                Console.WriteLine("Wrong Input");
                            break;
                        case(2):
                            DisplaySchedule();
                            Console.WriteLine("How many tickets do you want to order?");
                            if (int.TryParse(Console.ReadLine(), out var count))
                            {
                                for (int i = 0; i < count; ++i)
                                    TakeTicket(_user);
                            }
                            else
                                Console.WriteLine("Wrong Input");
                            break;
                        case(3):
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
                else
                {
                    Console.WriteLine("Wrong Input");
                }
            }
            
        }
    }
}
