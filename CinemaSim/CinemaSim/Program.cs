using System.Runtime.CompilerServices;
using CinemaSim;
using CinemaSim.Movies;

var movies = new List<Movie>()
{
    new Movie("Interstellar", 2014, "2:10"),
    new AnimatedMovie("The Incredibles", 2018, "1:40", "3D"),
    new Movie("Forrest Gump", 1994, "2:20"),
    new ForeignMovie("Parasite", 2019, "2:00", "Korean"),
    new AnimatedMovie("Sponge Bob", 2004, "1:20", "2D"),
    new Movie("La La Land", 2016, "2:30"),
};
var cinemaUI = new CinemaUI(movies);
cinemaUI.Run();

Console.ReadKey();
