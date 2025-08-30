using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelCVBajaDivaManufaktur.Models
{
    public class BahanBaku
    {
        [Key]
        public int Id { get; set; }
        public string? NamaBahan { get; set; }
        public int Stok { get; set; }

        public string? Satuan { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
