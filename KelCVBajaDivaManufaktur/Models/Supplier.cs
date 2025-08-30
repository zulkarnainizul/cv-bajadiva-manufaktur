using System.ComponentModel.DataAnnotations;

namespace KelCVBajaDivaManufaktur.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string? NamaSupplier { get; set; }
        public string? Alamat { get; set; }
        public string? NoHP { get; set; }
    }
}
