namespace StorageApi.DTOs
{
    public class ProductDto
    {
        public int Id { get; }
        public string Name { get; } = string.Empty;
        public int Price { get; }
        public int Count { get; }
        public int InventoryValue => Price * Count;

    }
}
