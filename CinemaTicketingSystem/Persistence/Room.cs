using System;

namespace Persistence
{
    public class Room
    {
        public int? RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomSeats { get; set; }
        public string RTName { get; set; }

        public Room()
        {

        }
        public Room(int? RoomId, string? RoomName, string? RoomSeats, string? RTName)
        {
            this.RoomId = RoomId;
            this.RoomName = RoomName;
            this.RoomSeats = RoomSeats;
            this.RTName = RTName;
        }
        public override bool Equals(object? obj)
        {
            Room room = (Room)obj;
            return RoomId == room.RoomId;
        }
        public override int GetHashCode()
        {
            return (RoomId + RoomName + RoomSeats + RTName).GetHashCode();
        }
    }
}