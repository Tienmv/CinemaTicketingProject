using System;

namespace Persistence
{
    public class Cinema
    {
        public int? CineId { get; set; }
        public string CineAddress { get; set; }
        public string CineName { get; set; }
        public string CinePhone { get; set; }

        public Cinema()
        {

        }
        public Cinema(int? CineId, string CineAddress, string CineName, string CinePhone)
        {
            this.CineId = CineId;
            this.CineAddress = CineAddress;
            this.CineName = CineName;
            this.CinePhone = CinePhone;
        }
    }
}