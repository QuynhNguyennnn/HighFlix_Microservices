using MovieServices.Models;

namespace MovieServices.DAOs
{
    public class MovieCategoryDAO
    {
        /*public static void CreateMovieCategory(List<int> cates, int movieId)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    if (cates != null)
                    {
                        foreach (int cateid in cates)
                        {
                            var movieCategory = new MovieCategory();
                            movieCategory.CategoryId = cateid;
                            movieCategory.MovieId = movieId;
                            context.Add(movieCategory);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/

        public static List<MovieCategory> GetCategoryByMovieId(int movieId)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    var listCate = context.MovieCategories.Where(mc => mc.MovieId == movieId).ToList();
                    return listCate;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CreateMovieCategory(List<int> cates, int movieId)
        {
            try
            {
                using (var context = new HighFlixContext())
                {
                    if (cates != null)
                    {
                        foreach (int cateid in cates)
                        {
                            var movieCategory = new MovieCategory();
                            movieCategory.CategoryId = cateid;
                            movieCategory.MovieId = movieId;
                            context.Add(movieCategory);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateMovieCategory(List<int> cates, int movieId)
        {
            var movieCategories = new List<MovieCategory>();

            try
            {
                using (var context = new HighFlixContext())
                {
                    movieCategories = context.MovieCategories.Where(mc => mc.MovieId == movieId).ToList();
                    foreach (MovieCategory cate in movieCategories)
                    {
                        context.MovieCategories.Remove(cate);
                        context.SaveChanges();
                    }
                    if (cates != null)
                    {
                        foreach (int cateId in cates)
                        {
                            var movieCategory = new MovieCategory();
                            movieCategory.CategoryId = cateId;
                            movieCategory.MovieId = movieId;
                            context.Add(movieCategory);
                            context.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}