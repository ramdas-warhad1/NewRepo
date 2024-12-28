namespace Data.DTOs
{
    public class ProductDisplayModel
    {
        public IEnumerable<Product> products { get; set; }
        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;
    }
}
