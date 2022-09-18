using System;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class PriceSeatOfRoomTypeBL
    {
        PriceSeatOfRoomTypeDAL psortdal = new PriceSeatOfRoomTypeDAL();
        public List<PriceSeatOfRoomType> GetPriceSeatOfRoomTypeByRTName(string rtName){
            if (rtName == null)
            {
                return null;
            }
            return psortdal.GetPriceSeatOfRoomTypesByRTName(rtName);
        }
    }
}