using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace segelServices.Models
{
    public class StatusPosting
    {
        public string Kode { get; set; }
        public string Periode { get; set; }
        public string Nosamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeGol { get; set; }
        public string KodeRayon { get; set; }
        public string KodeDiameter { get; set; }
        public string KodeKolektif { get; set; }
        public decimal? StanLalu { get; set; }
        public decimal? StanSkrg { get; set; }
        public decimal? StanAngkat { get; set; }
        public decimal? Pakai { get; set; }
        public decimal? BiayaPemakaian { get; set; }
        public decimal? Administrasi { get; set; }
        public decimal? Pemeliharaan { get; set; }
        public decimal? Retribusi { get; set; }
        public decimal? Pelayanan { get; set; }
        public decimal? AdministrasiLain { get; set; }
        public decimal? PemeliharaanLain { get; set; }
        public decimal? RetribusiLain { get; set; }
        public decimal? Meterai { get; set; }
        public decimal? DendaTunggakan { get; set; }
        public decimal? Rekair { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Total { get; set; }
        public DateTime? TglBayar { get; set; }
        public string FlagLunas { get; set; }
        public string LoketBayar { get; set; }
        public string Kasir { get; set; }
    }
}
