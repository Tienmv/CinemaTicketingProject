using System;
using Persistence;
using DAL;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BL
{
    public class RoomBL
    {
        private RoomDAL rdal = new RoomDAL();
        public Room GetRoomByRoomId(int? roomId)
        {
            if (roomId == null)
            {
                return null;
            }
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(roomId.ToString());
            if (matchCollection.Count < roomId.ToString().Length)
            {
                return null;
            }
            return rdal.GetRoomByRoomId(roomId);
        }
        public List<Room> GetRoomsByCineId(int? cineId)
        {
            if (cineId == null)
            {
                return null;
            }
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(cineId.ToString());
            if (matchCollection.Count < cineId.ToString().Length)
            {
                return null;
            }
            return rdal.GetRoomsByCineId(cineId);
        }
    }
}