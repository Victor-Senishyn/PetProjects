using System.Runtime.CompilerServices;
using CinemaSim;

var movies = new List<Movie>()
{
    new Movie("Interstellar", 2014, "2:10"),
    new AnimatedMovie("The Incredibles", 2018, "1:40", "3D"),
    new Movie("Forrest Gump", 1994, "2:20"),
    new ForeignMovie("Parasite", 2019, "2:00", "Korean"),
    new AnimatedMovie("Sponge Bob", 2004, "1:20", "2D"),
    new Movie("La La Land", 2016, "2:30"),
};

var cinema = new Cinema(movies.ToArray());
foreach (var movie in cinema)
    Console.WriteLine(movie);

Console.WriteLine("Press any button to continue:");
Console.ReadKey();
Console.Clear();

var schedule = new Schedule();
schedule.AddMoviesToSchedule(cinema.Movies);
foreach (var movieScreening in schedule)
    Console.WriteLine($"{movieScreening.StartTime} - {movieScreening.Screening}");

if(DateTime.TryParse(Console.ReadLine(), out var time))
{
    try
    {
        var ticket = schedule.ReserveSeat(time);
        Console.WriteLine(ticket);
    }
    catch (InvalidTicketReservationException ex)
    {
        Console.WriteLine($"Exception details: {ex}");
    }
}
else 
    Console.WriteLine($"Time entered is in the wrong format");

Console.ReadKey();
