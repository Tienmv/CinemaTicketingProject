using System;

namespace Persistence
{
    public class ShowtimeDetail
    {
        public int? ShowtimedId{get; set;}
        public DateTime? ShowTimeStart{get; set;}
        public DateTime? ShowTimeEnd{get; set;}
        public string? ShowtimeRoomSeat{get; set;}
        public int? ShowtimeId{get; set;}
        public ShowtimeDetail(){

        }

        public ShowtimeDetail(int? ShowtimedId, int? ShowtimeId, DateTime? ShowTimeStart, DateTime? ShowTimeEnd, string ShowtimeRoomSeat){
            this.ShowtimedId = ShowtimedId;
            this.ShowtimeId = ShowtimeId;
            this.ShowTimeStart = ShowTimeStart;
            this.ShowTimeEnd = ShowTimeEnd;
            this.ShowtimeRoomSeat = ShowtimeRoomSeat;
        }
    }
}