namespace AuraShop.Catalog.Dtos.BrandDtos
{
    public class BrandDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateBrandDto
    {
        public string Name { get; set; }
    }

    public class UpdateBrandDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
