using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelCVBajaDivaManufaktur.Models
{
    public class Transaksi
    {

        [Key]
        public int Id { get; set; }

        public string? NamaPelanggan { get; set; }

        [ForeignKey("Produk")]
        public int ProdukId { get; set; }
        public Produk? Produk { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime TanggalBeli { get; set; }
        public int Jumlah { get; set; }
        public decimal TotalBayar { get; set; }
        public string? CaraBayar { get; set; }
    }
}
