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
    public class Cinema : IEnumerable<Movie>
    {
        public List<Movie> Movies;

        public Cinema() => Movies = new List<Movie>();

        public Cinema(params Movie[] movies) => Movies = new List<Movie>(movies);

        public void AddMovie(Movie movie) => Movies.Add(movie);

        public IEnumerator<Movie> GetEnumerator() => Movies.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Movies).GetEnumerator();
    }
}
