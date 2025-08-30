# CV Baja Diva Manufaktur

Proyek ini adalah bagian dari tugas mata kuliah **Pemrograman Framework Enterprise** semester 5, yang dikembangkan untuk **CV Baja Diva Manufaktur**. Perusahaan ini berfokus pada produksi mesin Tepat Guna dengan merek **PRESCO** untuk mendukung UMKM di sektor pengolahan makanan, pertanian, dan peternakan.

---

## 🎯 Fitur Utama Aplikasi

-   **Manajemen Pengguna**: Sistem login dan manajemen hak akses (admin, staff).
-   **Manajemen Produk**: Mengelola data produk, termasuk nama, harga, stok, kategori, dan deskripsi.
-   **Manajemen Bahan Baku**: Mengelola data bahan baku dan ketersediaan stok.
-   **Manajemen Supplier**: Mengelola data pemasok bahan baku.
-   **Manajemen Transaksi**: Mencatat penjualan mesin kepada pelanggan.
-   **Manajemen Pesan Bahan Baku**: Mencatat pesanan bahan baku dari supplier.

---

## 🛠️ Spesifikasi Teknis

-   **Framework**: .NET 7
-   **Bahasa Pemrograman**: C#
-   **Teknologi Web**: ASP.NET Core MVC
-   **Manajemen Database**: Entity Framework Core
-   **Database**: SQL Server (LocalDB)

---

## 🚀 Cara Menjalankan Proyek

### 1. Klon Repositori

```bash
git clone https://github.com/zulkarnainizul/cv-bajadiva-manufaktur
```

### 2. Atur Database

Proyek ini menggunakan **Entity Framework Migrations**. Pastikan **SQL Server Express LocalDB** sudah terinstal dan berjalan.

1.  Hapus folder `Migrations` yang ada (jika ada) dari proyek lama.
2.  Buka **Package Manager Console** di Visual Studio.
3.  Jalankan perintah berikut untuk membuat file migrasi baru:
    ```powershell
    Add-Migration FinalCreate
    ```
4.  Setelah migrasi dibuat, jalankan perintah ini untuk membuat database:
    ```powershell
    Update-Database
    ```

---

### 3. Tambahkan Data Pengguna

Database yang baru dibuat masih kosong. Anda perlu menambahkan akun **admin** secara manual di tabel **Pengguna** agar bisa login. Hak Akses terdiri atas Admin, Karyawan, dan Supplier.

---

### 4. Jalankan Aplikasi

Setelah semua langkah di atas selesai, jalankan proyek dari Visual Studio dengan menekan tombol **Start** atau **F5**.

---

### 🤝 Kontributor

* [Zulkarnain] - [Pengembang]
