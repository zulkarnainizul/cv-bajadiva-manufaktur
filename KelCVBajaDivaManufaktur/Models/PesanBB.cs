using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelCVBajaDivaManufaktur.Models
{
    public class PesanBB
    {
        [Key]
        public int Id { get; set; }
        public int StokBB { get; set; }

        public string? SatuanBB { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        [ForeignKey("BahanBaku")]
        public int BahanBakuId { get; set; }
        public BahanBaku? BahanBaku { get; set; }
    }
}
