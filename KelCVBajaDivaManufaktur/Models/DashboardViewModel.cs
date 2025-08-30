using System.Transactions;

namespace KelCVBajaDivaManufaktur.Models
{
    public class DashboardViewModel
    {
        
        public int JumlahTransaksi { get; set; }
        public int JumlahProduk { get; set; }
        public int JumlahBahanBaku { get; set; }
        public int JumlahSupplier { get; set; }
        public List<Transaksi> TransaksiList { get; set; }
        public List<PesanBB> PesanBBList { get; set; }
        public List<decimal> TotalBayarSumByMonth { get; set; }
        public ProductSalesViewModel ProductSales { get; set; }
    }
}
