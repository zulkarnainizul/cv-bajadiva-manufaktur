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

        public DbSet<KelCVBajaDivaManufaktur.Models.PesanBB> PesanBB { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mengatasi masalah "multiple cascade paths"
            // Dengan mengatur relasi PesanBB ke Supplier agar tidak melakukan penghapusan otomatis.
            modelBuilder.Entity<PesanBB>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            // Memanggil implementasi dasar (penting)
            base.OnModelCreating(modelBuilder);
        }

    }

}
