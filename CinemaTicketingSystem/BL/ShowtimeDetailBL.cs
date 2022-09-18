using System;
using Persistence;
using DAL;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BL
{
    public class ShowtimeDetailBL
    {
        ShowtimeDetailDAL sddal = new ShowtimeDetailDAL();
        public ShowtimeDetail GetShowtimeDetailByShowId(int? showdId){
            if (showdId == null)
            {
                return null;
            }
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(showdId.ToString());
            if (matchCollection.Count < showdId.ToString().Length)
            {
                return null;
            }
            return sddal.GetShowtimeDetailByShowtimedId(showdId);    
        }
        public List<ShowtimeDetail> GetShowtimeDetailsByShowId(int? showId)
        {
            if (showId == null)
            {
                return null;
            }
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(showId.ToString());
            if (matchCollection.Count < showId.ToString().Length)
            {
                return null;
            }
            return sddal.GetShowtimeDetailsByShowtimeId(showId);
        }
        public List<ShowtimeDetail> GetShowtimeDetailsByShowIdAndTimeNow(int? showId){
            if (showId == null)
            {
                return null;
            }
            return sddal.GetShowtimeDetailsByShowtimeIdAndDateNow(showId);
        }
    }
}