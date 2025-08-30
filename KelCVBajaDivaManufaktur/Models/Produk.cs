using System.ComponentModel.DataAnnotations;

namespace KelCVBajaDivaManufaktur.Models
{
    public class Produk
    {
        [Key]
        public int Id { get; set; }
        public string? NamaProduk { get; set; }
        public string? GambarProduk { get; set; }
        public int Stok { get; set; }
        public decimal Harga { get; set; }
        public string? Kategori { get; set; }
        public string? Deskripsi { get; set; }
    }
}
