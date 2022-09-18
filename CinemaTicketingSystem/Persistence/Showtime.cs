using System;
using System.Collections.Generic;

namespace Persistence
{
    public class Showtime
    {
        public int? ShowtimeId { get; set; }
        public int ShowtimeStatus { get; set; }
        public string ShowtimeWeekdays { get; set; }
        public string ShowTimeline { get; set; }
        public int RoomId { get; set; }
        public int MovieId { get; set; }

        public List<ShowtimeDetail> ShowtimeDetails { get; set; }

        public Showtime()
        {

        }
        public Showtime(int? ShowtimeId, int ShowtimeStatus, string ShowtimeWeekdays, string ShowTimeline, int RoomId, int MovieId, List<ShowtimeDetail> showtimedDetails)
        {
            this.ShowtimeId = ShowtimeId;
            this.ShowtimeStatus = ShowtimeStatus;
            this.ShowtimeWeekdays = ShowtimeWeekdays;
            this.ShowTimeline = ShowTimeline;
            this.RoomId = RoomId;
            this.MovieId = MovieId;
            this.ShowtimeDetails = showtimedDetails;
        }
    }
}