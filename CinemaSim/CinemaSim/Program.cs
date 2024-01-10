using CinemaSim;

var movies = new List<Movie>()
{
    new Movie("Interstellar", 2014, 169),
    new AnimatedMovie("The Incredibles", 2018, 118, "3D"),
    new Movie("Forrest Gump", 1994, 142),
    new ForeignMovie("Parasite", 2019, 132, "Korean"),
    new AnimatedMovie("Sponge Bob", 2004, 102, "2D"),
    new Movie("La La Land", 2016, 128),
};

var cinema = new Cinema(movies.ToArray());
cinema.ShowFilms();
Console.WriteLine("Press any button to continue:");
Console.ReadKey();
Console.Clear();

cinema.AddMoviesToSchedule("Interstellar", "Sponge Bob", "Parasite", "Forrest Gump", "La La Land", "Unknown Film");
cinema.ReserveSeat();

