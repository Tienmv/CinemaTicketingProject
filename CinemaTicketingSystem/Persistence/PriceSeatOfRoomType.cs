using System;

namespace Persistence
{
    public class PriceSeatOfRoomType
    {
        public string RTName { get; set; }
        public string STType { get; set; }
        public double Price { get; set; }

        public PriceSeatOfRoomType(){

        }
        public PriceSeatOfRoomType(string RTName, string STType, double Price){
            this.RTName = RTName;
            this.STType = STType;
            this.Price = Price;
        }
        public override bool Equals(object? obj)
        {
            PriceSeatOfRoomType priceSeatOfRoomType = (PriceSeatOfRoomType)obj;
            return RTName == priceSeatOfRoomType.RTName && STType == priceSeatOfRoomType.STType;
        }
        public override int GetHashCode()
        {
            return (RTName + STType + Price).GetHashCode();
        }
    }
}