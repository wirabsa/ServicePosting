using Dapper;
using MySqlConnector;
using segelServices.Models;
using segelServices.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace segelServices.Services
{
    public class Posting
    {
        public async Task Segel()
        {
            using var conn = new MySqlConnection(AppSettings.ConnString);
            await conn.OpenAsync();
            await using var transaction = await conn.BeginTransactionAsync();
            try
            {
                await conn.ExecuteAsync($"TRUNCATE TABLE statusposting", transaction: transaction);
                
                var jumTunggakan = await conn.QueryFirstOrDefaultAsync<Pengaturan>("SELECT batas_blok_kelainan from pengaturan ", transaction: transaction);

                var jadwalPosting = await conn.QueryFirstOrDefaultAsync<ServiceSetting>("SELECT * FROM service_posting WHERE Idkey='SEGEL' ", transaction: transaction);
                if(jadwalPosting!=null)
                {

                    DateTime tanggalProses = jadwalPosting.DateOn.Value.Date;
                    DateTime hariIni = DateTime.Now.Date;

                    if (hariIni >= tanggalProses )
                    {
                        var data = await conn.QueryAsync<StatusPosting>($@"SELECT kode,periode,nosamb,nama,alamat,kodegol,koderayon,kodediameter,kodekolektif,stanlalu,stanskrg,stanangkat,pakai,
                    biayapemakaian,administrasi,pemeliharaan,retribusi,pelayanan,administrasilain,pemeliharaanlain,retribusilain,meterai,dendatunggakan,rekair,ppn,total,tglbayar,flaglunas,loketbayar,kasir 
                    FROM piutang WHERE nosamb IN(SELECT nosamb FROM piutang WHERE CURRENT_DATE>= tglmulaidenda AND flag<>'4' AND flagangsur='0' and nosamb='0100107'  GROUP BY nosamb HAVING COUNT(nosamb)={jumTunggakan.Batas_blok_kelainan} )
                    AND CURRENT_DATE>= tglmulaidenda AND flag<>'4' AND flagangsur='0' ", transaction: transaction);


                        if (data != null)
                        {
                            await conn.ExecuteAsync($@" INSERT INTO statusposting (kode,periode,nosamb,nama,alamat,kodeGol,kodeRayon,kodeDiameter,kodeKolektif,stanLalu,stanSkrg,
                                                stanAngkat,pakai,biayaPemakaian,administrasi,pemeliharaan,retribusi,pelayanan,administrasiLain,pemeliharaanLain,retribusiLain,
                                                meterai,dendaTunggakan,rekair,ppn,total,tglBayar,flagLunas,loketBayar,kasir) values (@kode,@periode,@nosamb,@nama,@alamat,@kodeGol
                                                ,@kodeRayon,@kodeDiameter,@kodeKolektif,@stanLalu,@stanSkrg,@stanAngkat,@pakai,@biayaPemakaian,@administrasi,@pemeliharaan,@retribusi,
                                                @pelayanan,@administrasiLain,@pemeliharaanLain,@retribusiLain,@meterai,@dendaTunggakan,@rekair,@ppn,@total,@tglBayar,@flagLunas,
                                                @loketBayar,@kasir)", data, transaction: transaction);
                        }
                        await conn.ExecuteAsync($"UPDATE service_posting SET dateon = @value WHERE Idkey='SEGEL'", new { value = DateTime.Now.AddMonths(1) }, transaction: transaction);
                    }
                }
                await transaction.CommitAsync();

            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                await transaction.RollbackAsync();
            }
            finally
            {
                await conn.CloseAsync();
                await MySqlConnection.ClearPoolAsync(conn);
            }
        }
    }
}
