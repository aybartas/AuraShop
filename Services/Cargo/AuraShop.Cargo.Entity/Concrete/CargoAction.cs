
namespace AuraShop.Cargo.Entity.Concrete
{
    public class CargoAction
    {
        public int Id { get; set; }
        public int CargoId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Message { get; set; }
    }
}
