namespace KelCVBajaDivaManufaktur
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? NamaProduk { get; set; }
        public IFormFile photo { get; set; }
        public int Stok { get; set; }
        public decimal Harga { get; set; }
        public string? Kategori { get; set; }
        public string? Deskripsi { get; set; }
    }
}
