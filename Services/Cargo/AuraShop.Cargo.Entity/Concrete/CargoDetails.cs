
namespace AuraShop.Cargo.Entity.Concrete
{
    public class Cargo
    {
        public int Id { get; set; }
        public int BarcodeNumber { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
        public List<CargoAction> CargoActions { get; set; }
        public CargoStatus Status { get; set; }
    }
}
