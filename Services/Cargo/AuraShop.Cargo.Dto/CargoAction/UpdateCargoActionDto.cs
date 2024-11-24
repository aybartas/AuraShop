namespace AuraShop.Cargo.Dto.CargoAction;

public class UpdateCargoActionDto
{
    public int CargoId { get; set; }
    public DateTime ActionDate { get; set; }
    public string Message { get; set; }
}