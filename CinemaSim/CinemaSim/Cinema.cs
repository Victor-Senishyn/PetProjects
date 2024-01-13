using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class Cinema
    {
        public List<Movie> Movies { get; set; }

        public Cinema() => Movies = new List<Movie>();

        public Cinema(params Movie[] movies) => Movies = new List<Movie>(movies);

        public void AddMovie(Movie movie) => Movies.Add(movie);
    }
}
