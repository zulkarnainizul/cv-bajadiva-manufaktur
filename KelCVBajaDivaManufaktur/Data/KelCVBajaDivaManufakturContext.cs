using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KelCVBajaDivaManufaktur.Models;

namespace KelCVBajaDivaManufaktur.Data
{
    public class KelCVBajaDivaManufakturContext : DbContext
    {
        public KelCVBajaDivaManufakturContext (DbContextOptions<KelCVBajaDivaManufakturContext> options)
            : base(options)
        {
        }

        public DbSet<KelCVBajaDivaManufaktur.Models.BahanBaku> BahanBaku { get; set; } = default!;

        public DbSet<KelCVBajaDivaManufaktur.Models.Pengguna> Pengguna { get; set; } = default!;

        public DbSet<KelCVBajaDivaManufaktur.Models.Produk> Produk { get; set; } = default!;

        public DbSet<KelCVBajaDivaManufaktur.Models.Supplier> Supplier { get; set; } = default!;

        public DbSet<KelCVBajaDivaManufaktur.Models.Transaksi> Transaksi { get; set; } = default!;
    }
}
