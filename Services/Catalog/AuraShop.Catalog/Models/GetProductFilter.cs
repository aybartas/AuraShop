using AuraShop.Catalog.Entities;

namespace AuraShop.Catalog.Models
{
    public class GetProductFilter
    {
        private int? _page = 0;
        private int? _size = 6;
        public int? Page
        {
            get => _page;
            set => _page = value ?? 0;
        }
        public int? Size
        {
            get => _size;
            set => _size = (value > 50 ? 50 : value) ?? 6;
        }
        public bool? Ascending { get; set; } = true;
        public string? SortBy { get; set; }
        public string? SearchText { get; set; }
        public string? ProductName { get; set; }
        public string? Brands { get; set; }
        public string? Categories { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

}
