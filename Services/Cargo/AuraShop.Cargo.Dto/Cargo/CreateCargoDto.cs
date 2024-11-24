using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraShop.Cargo.Entity.Concrete;

namespace AuraShop.Cargo.Dto.Cargo
{
    public class CreateCargoDto
    {
        public string OrderNumber { get; set; }
        public int TrackingNumber { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoStatus Status { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}
