namespace StorageApi.DTOs
{
    public class ProductStatsDto
    {
        public int TotalProductsCount { get; set; }
        public int TotalInventoryValue { get; set; }
        public int AveragePrice { get; set; }
    }
}
