using System;
using Persistence;
using DAL;

namespace BL
{
    public class TicketBL
    {
        private TicketDAL tdal = new TicketDAL();

        public bool SellTicket(ShowtimeDetail showtimeDetail){
            return tdal.SellTicket(showtimeDetail);
        }
    }
}