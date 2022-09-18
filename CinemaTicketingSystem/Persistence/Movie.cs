using System;

namespace Persistence
{
    public class Movie
    {
        public int? MovieId { get; set; }
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public string MovieAuthor { get; set; }
        public string MovieActor { get; set; }
        public string MovieCategory { get; set; }
        public int? MovieTime { get; set; }
        public DateTime MovieDateStart { get; set; }
        public DateTime MovieDateEnd { get; set; }

        public Movie()
        {

        }

        public Movie(int? movieId, string movieName, string movieDescription, string movieAuthor,
                    string movieActor, string movieCategory, int? movieTime, DateTime movieDateStart, DateTime movieDateEnd)
        {
            this.MovieId = movieId;
            this.MovieName = movieName;
            this.MovieDescription = movieDescription;
            this.MovieAuthor = movieAuthor;
            this.MovieActor = movieActor;
            this.MovieCategory = movieCategory;
            this.MovieTime = movieTime;
            this.MovieDateStart = movieDateStart;
            this.MovieDateEnd = movieDateEnd;
        }
    }
}