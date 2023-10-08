using MovieServices.Models;

namespace MovieServices.DAOs
{
    public class MovieDAO
    {
        public static List<Movie> GetMovieList()
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    var movieList = context.Movies.ToList();
                    foreach (var movie in movieList)
                    {
                        if (movie.IsActive)
                        {
                            movies.Add(movie);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movies;
        }

        public static Movie GetMovieById(int id)
        {
            Movie movie = new Movie();
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    movie = context.Movies.SingleOrDefault(mv => (mv.MovieId == id) && mv.IsActive);
                    movie.Description = movie.Description.Substring(2, movie.Description.Length - 3);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movie;
        }

        public static Movie CreateMovie(Movie movie, List<int> cates)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    movie.IsActive = true;

                    context.Movies.Add(movie);
                    context.SaveChanges();

                    MovieCategoryDAO.CreateMovieCategory(cates, movie.MovieId);
                    return movie;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Movie UpdateMovie(Movie movie, List<int> cates)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    var _movie = context.Movies.SingleOrDefault(m => m.MovieId == movie.MovieId && m.IsActive);
                    if (_movie != null)
                    {
                        movie.IsActive = _movie.IsActive;

                        // Sử dụng SetValues để cập nhật giá trị từ movie vào _movie
                        context.Entry(_movie).CurrentValues.SetValues(movie);
                        context.SaveChanges();

                        MovieCategoryDAO.UpdateMovieCategory(cates, movie.MovieId);
                        return _movie; // Trả về _movie sau khi cập nhật
                    }
                    else
                    {
                        throw new Exception("Movie does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Movie DeleteMovie(int id)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    var _movie = context.Movies.SingleOrDefault(m => m.MovieId == id && m.IsActive);
                    if (_movie != null)
                    {
                        _movie.IsActive = false;

                        // Sử dụng SetValues để cập nhật giá trị từ movie vào _movie
                        context.Entry(_movie).CurrentValues.SetValues(_movie);
                        context.SaveChanges();

                        return _movie; // Trả về _movie sau khi cập nhật
                    }
                    else
                    {
                        throw new Exception("Movie does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<Movie> SearchMovies(string searchTerm)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    return context.Movies
                        .Where(movie =>
                            movie.IsActive &&
                            (movie.MovieName.Contains(searchTerm)
                            // || movie.Categories.Contains(searchTerm) 
                            ))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
