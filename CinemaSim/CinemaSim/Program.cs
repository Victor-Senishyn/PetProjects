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
foreach (var movie in cinema.GetFilms())
    Console.WriteLine(movie);

Console.WriteLine("Press any button to continue:");
Console.ReadKey();
Console.Clear();

cinema.AddMoviesToSchedule("Interstellar", "Sponge Bob", "Parasite", "Forrest Gump", "La La Land", "Unknown Film");
foreach (var movie in cinema.GetSchedule())
    Console.WriteLine(movie);
DateTime time = Convert.ToDateTime(Console.ReadLine());

var ticket = cinema.ReserveSeat(time);
Console.WriteLine(ticket == null ? "Wrong input" : ticket);

Console.ReadKey();
