﻿
namespace AuraShop.Cargo.Entity.Concrete
{
    public class Cargo
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int TrackingNumber { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
        public List<CargoAction> CargoActions { get; set; }
        public CargoStatus Status { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; } 
    }
  
}
