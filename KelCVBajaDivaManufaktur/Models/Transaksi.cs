using System.ComponentModel.DataAnnotations;

namespace KelCVBajaDivaManufaktur.Models
{
    public class Transaksi
    {
        public int Id { get; set; }

        public string? NamaPelanggan { get; set; }
        public string? NamaMenu { get; set; }
        [DataType(DataType.Date)]
        public DateTime TanggalBeli { get; set; }
        public int Jumlah { get; set; }
        public decimal TotalBayar { get; set; }
        public string? CaraBayar { get; set; }
    }
}
