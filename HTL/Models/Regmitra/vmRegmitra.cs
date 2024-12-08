using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DusColl
{

    [Serializable]
    public class vmRegmitra
    {
        public cFilterContract FilterTransaksi { get; set; }
        public DataTable DTHeaderTx { get; set; }
        public DataTable DTHeaderRefTx { get; set; }
        public DataTable DTDetailTx { get; set; }
        public DataTable DTAllTx { get; set; }
        public cRegmitra HeaderInfo { get; set; }
        public cRegmitra HeaderInfoOld { get; set; }
        public DataTable DTAllLog { get; set; }
        public DataTable DTLogTx { get; set; }
        public DataTable DTDokumen { get; set; }
        public DataTable DTResultUpload { get; set; }
        public cAccountMetrik Permission { get; set; }
        public string CheckWithKey { get; set; }
    }



    [Serializable]
    public class blRegmitraddl
    {

        public async Task<string> dbgetvalidate(cRegmitra model, cRegmitra modelold, HttpPostedFileBase[] files, string[] documen, DataTable dokumenlistrequired, string FlagOPR, string TglAkhirKontrak, string module, string UserID, string GroupName)
        {
            string valid = "";

            if (int.Parse(HasKeyProtect.Decryption(model.keylookupdataDTX).ToString()) < 0)
            {
                valid = "Cek Refrensi Dokumen Tidak diijinkan";
                return valid;
            }

            //saat save draft paling tidak isikan Nama Mitra dan no ktp
            if (model.StatusFollow == "5")
            {
                if ((model.NamaMitra ?? "") == "")
                {
                    valid = "Isikan nama mitra";
                    return valid;
                }

                if ((model.NoKTP ?? "") == "")
                {
                    valid = "Isikan No KTP nama mitra";
                    return valid;
                }
            }


            if ((model.NoKTP ?? "") != "" && (model.NoKTP ?? "").Length != 16)
            {
                valid = "No KTP harus 16 digit";
                return valid;
            }
            if ((model.NoNPWP ?? "") != "" && (model.NoNPWP ?? "").Length != 15)
            {
                valid = "No NPWP harus 15 digit";
                return valid;
            }

            if ((model.NoSPPI ?? "") != "")
            {
                string[] splt = model.NoSPPI.Split('-');

                if ((model.NoSPPI ?? "").Length != 19)
                {
                    valid = "No SPPI harus 19 digit";
                    return valid;
                }

                if (splt.Length != 4)
                {
                    valid = "Format No SPPI xxxx-xxxxx-xxx-xxxx";
                    return valid;
                }
                else
                {
                    if (splt[0].Length != 4)
                    {
                        valid = "Format No SPPI xxxx-xxxxx-xxx-xxxx";
                        return valid;
                    }
                    if (splt[1].Length != 5)
                    {
                        valid = "Format No SPPI xxxx-xxxxx-xxx-xxxx";
                        return valid;
                    }
                    if (splt[2].Length != 3)
                    {
                        valid = "Format No SPPI xxxx-xxxxx-xxx-xxxx";
                        return valid;
                    }
                    if (splt[3].Length != 4)
                    {
                        valid = "Format No SPPI xxxx-xxxxx-xxx-xxxx";
                        return valid;
                    }
                }

            }

            if ((model.NoHP ?? "") != "" && (model.NoHP ?? "").Length != 13)
            {
                if ((model.NoHP ?? "").Length < 10 || (model.NoHP ?? "").Length > 13)
                {
                    valid = "No Handphone1 minimal 10 digit dan maximal 13 digit";
                    return valid;
                }
            }

            if ((model.NoHP1 ?? "") != "" && (model.NoHP1 ?? "").Length != 13)
            {
                if ((model.NoHP1 ?? "").Length < 10 || (model.NoHP1 ?? "").Length > 13)
                {
                    valid = "No Handphone2 harus  10 digit dan maximal 13 digit";
                    return valid;
                }
            }

            if ((model.NoWA ?? "") != "" && (model.NoWA ?? "").Length != 13)
            {
                if ((model.NoWA ?? "").Length < 10 || (model.NoWA ?? "").Length > 13)
                {
                    valid = "No WA harus  10 digit dan maximal 13 digit";
                    return valid;
                }
            }

            if ((model.Norekening ?? "") != "" && (model.Norekening ?? "").Length != 15)
            {
                if ((model.Norekening ?? "").Length < 10 || (model.Norekening ?? "").Length > 15)
                {
                    valid = "No Rekening minimal 10 digit dan maximal 15 digit";
                    return valid;
                }
            }

            if ((model.CabangBank ?? "") != "" && (model.CabangBank.Any(x => char.IsDigit(x))))
            {
                valid = "Terdapat angka dalam cabang bank";
                return valid;
            }


            //cek jika bukan save draf
            if ((FlagOPR == "CRETHDR" || FlagOPR == "REVHDR" || FlagOPR == "REVHDRDRAFT") && model.StatusFollow != "5")
            {
                if ((model.tglmasuk ?? "").Length <= 10 || (model.tglakhir ?? "").Length <= 10)
                {
                    valid = "Silahkan Cek Format tanggal masuk dan tanggal akhir (Format tanggal harus : 2 digit tgl nama bulan dan 4 digit tahun)";
                    return valid;
                }
                else
                {
                    if (DateTime.Parse(model.tglakhir) < DateTime.Parse(model.tglmasuk))
                    {
                        valid = "Silahkan tanggal akhir harus lebih besar dari tanggal masuk";
                        return valid;
                    }
                }


                if ((int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.ROTASI))
                {
                    if ((model.StatusDate ?? "") != "")
                    {
                        if (DateTime.Parse(model.StatusDate) > DateTime.Now.Date)
                        {
                            valid = "Tanggal rotasi tidak boleh lebih besar dari tanggal hari ini";
                            return valid;
                        }
                    }
                }

                if ((int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.UBAHSTATUS))
                {
                    if ((model.StatusDate ?? "") != "")
                    {
                        if (DateTime.Parse(model.StatusDate) > DateTime.Now.Date)
                        {
                            valid = "Tanggal perubahan status mitra tidak boleh lebih besar dari tanggal hari ini";
                            return valid;
                        }
                    }
                }

                /*
                //cek jika bukan save draft
                if ((int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.BARU
                || int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.PANJANG) && model.StatusFollow != "5")
                {

                    if (FlagOPR != "APRHDR")
                    {
                        string date1 = (int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.PANJANG) ? TglAkhirKontrak : model.tglmasuk;

                        if (int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.PANJANG)
                        {
                            if (model.DivisiName.ToLower().Contains("penjualan") || model.DivisiName.ToLower().Contains("sales"))
                            {
                                date1 = DateTime.Parse(date1).AddMonths(1).ToString("dd-MMMM-yyyy");
                            }

                            //else
                            //{
                            //    date1 = DateTime.Parse(date1).AddDays(1).ToString("dd-MMMM-yyyy");
                            //}
                        }

                        vmCommonddl Commonddl = new vmCommonddl();
                        DataTable dt = await Commonddl.dbdbGetDdlDevisiPeriodeListByEncrypt("1", model.Divisi, module, UserID, GroupName);
                        string periode = dt.Rows[0][2].ToString();
                        CultureInfo provider = new CultureInfo("en-GB");
                        DateTime dt1 = DateTime.Parse(date1, provider, DateTimeStyles.NoCurrentDateDefault);
                        //salaes sampai akhir tahun

                        string date2 = "";
                        int minday = 0;
                        string msgcap = "";
                        //salaes sampai akhir tahun
                        DateTime dt2 = dt1.AddMonths(int.Parse(periode));
                        if (periode == "0")
                        {
                            dt2 = dt2.AddMonths(12 - dt1.Month);
                            date2 = "31-" + dt2.ToString("MMMM-yyyy");
                            msgcap = "Setahun";
                        }
                        else
                        {
                            minday = -1;
                            date2 = dt2.AddDays(minday).ToString("dd-MMMM-yyyy");
                            msgcap = periode + " bulan";

                        }

                        if (model.tglakhir != date2)
                        {
                            return valid = "Tgl Akhir kontrak mitra harus " + msgcap + ", cek tanggal masuk dan tanggal keluar mitra";
                        }

                        if (DateTime.Parse(model.tglakhir) < DateTime.Parse(DateTime.Now.ToString("dd-MMMM-yyyy")))
                        {
                            return valid = "Tgl Akhir untuk masa kontrak mitra harus lebih besar dari tanggal hari ini ";
                        }
                        //cek tgl akhir harus lebih besar dari tgl hari ini 
                    }
                }
                */

                //cek jika bukan save draf
                if (((FlagOPR == "CRETHDR" || FlagOPR == "REVHDRDRAFT") && int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.UBAHSTATUS) && model.StatusFollow != "5")
                {
                    if (model.RegmitraStatus == modelold.RegmitraStatus)
                    {
                        return valid = "Tidak ada perubahan status mitra, silahkan cek kembali";
                    }
                    if (model.RegmitraStatus == null || modelold.RegmitraStatus == null)
                    {
                        return valid = "Status mitra tidak terdaftar";
                    }
                }

                //cek jika bukan save draf
                if (((FlagOPR == "CRETHDR" || FlagOPR == "REVHDR" || FlagOPR == "REVHDRDRAFT") && (int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.UBAH
                    || int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.PANJANG)
                    || int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.BARU) && model.StatusFollow != "5")
                {

                    model.AlamatKorespodensi = (model.AlamatKorespodensi ?? "");
                    model.NoSPPI = model.NoSPPI ?? "";
                    model.NoHP = model.NoHP ?? "";
                    model.NoHP1 = model.NoHP1 ?? "";
                    model.NoWA = model.NoWA ?? "";
                    model.Norekening = model.Norekening ?? "";
                    model.CabangBank = model.CabangBank ?? "";
                    model.NamaBank = model.NamaBank ?? "";
                    model.Pemilikkening = model.Pemilikkening ?? "";

                    modelold.AlamatKorespodensi = modelold.AlamatKorespodensi ?? "";
                    modelold.NoSPPI = modelold.NoSPPI ?? "";
                    modelold.NoHP = modelold.NoHP ?? "";
                    modelold.NoHP1 = modelold.NoHP1 ?? "";
                    modelold.NoWA = modelold.NoWA ?? "";
                    modelold.Norekening = modelold.Norekening ?? "";
                    modelold.CabangBank = modelold.CabangBank ?? "";
                    modelold.NamaBank = modelold.NamaBank ?? "";
                    modelold.Pemilikkening = modelold.Pemilikkening ?? "";

                    if (model.AlamatKorespodensi == modelold.AlamatKorespodensi &&
                        model.NoSPPI == modelold.NoSPPI && model.NoHP == modelold.NoHP && model.NoHP1 == modelold.NoHP1 &&
                        model.NoWA == modelold.NoWA && model.Norekening == modelold.Norekening && model.CabangBank == modelold.CabangBank &&
                        model.NamaBank == modelold.NamaBank && model.Pemilikkening == modelold.Pemilikkening &&
                        model.NamaFB == modelold.NamaFB &&
                        int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.UBAH && FlagOPR == "CRETHDR")
                    {
                        return valid = "Tidak ada perubahan data yang anda ajukan, silahkan cek kembali";
                    }

                    //upload jadi wajib jika ada perubahan SPSII
                    if (model.NoSPPI != modelold.NoSPPI)
                    {
                        if (dokumenlistrequired.Rows.Count == 0)
                        {
                            return valid = "Pilihan Dokumen belum diinputkan di master data 'Dokumen', silahkan hubungi Admin HO";
                        }
                        else
                        {
                            int docexits = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "kartu sppi").Count();
                            if (docexits == 0)
                            {
                                return valid = "Pilihan 'Dokumen SPPI' tidak ditemukan di master data 'Dokumen', silahkan hubungi Admin HO";
                            }
                            else
                            {
                                var rowsToUpdate = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "kartu sppi");
                                foreach (var row in rowsToUpdate)
                                {
                                    row.SetField("IsMandatory", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        var rowsToUpdate = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "kartu sppi");
                        foreach (var row in rowsToUpdate)
                        {
                            row.SetField("IsMandatory", false);
                        }
                    }

                    if (model.Alamat != modelold.Alamat || model.Tgllahir != modelold.Tgllahir
                        || model.Tempatlahir != model.Tempatlahir || model.JenisKelamin != modelold.JenisKelamin)
                    {
                        if (dokumenlistrequired.Rows.Count == 0)
                        {
                            return valid = "Pilihan Dokumen belum diinputkan di master data 'Dokumen', silahkan hubungi Admin HO";
                        }
                        else
                        {
                            int docexits = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "ktp").Count();
                            if (docexits == 0)
                            {
                                return valid = "Pilihan 'Dokumen KTP' tidak ditemukan di master data 'Dokumen',silahkan hubungi Admin HO";
                            }
                            else
                            {
                                var rowsToUpdate = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "ktp");
                                foreach (var row in rowsToUpdate)
                                {
                                    row.SetField("IsMandatory", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        var rowsToUpdate = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "ktp");
                        foreach (var row in rowsToUpdate)
                        {
                            row.SetField("IsMandatory", false);
                        }
                    }

                    if (model.Norekening != modelold.Norekening || model.CabangBank != modelold.CabangBank ||
                        model.NamaBank != modelold.NamaBank || model.Pemilikkening != modelold.Pemilikkening)
                    {
                        if (dokumenlistrequired.Rows.Count == 0)
                        {
                            return valid = "Pilihan Dokumen belum diinputkan di master data 'Dokumen', silahkan hubungi Admin HO";
                        }
                        else
                        {
                            int docexits = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "buku rekening").Count();
                            if (docexits == 0)
                            {
                                return valid = "Pilihan Dokumen 'Buku Rekening' tidak ditemukan di master data 'Dokumen', silahkan hubungi Admin HO";
                            }
                            else
                            {
                                var rowsToUpdate = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "buku rekening");
                                foreach (var row in rowsToUpdate)
                                {
                                    row.SetField("IsMandatory", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        var rowsToUpdate = dokumenlistrequired.AsEnumerable().Where(x => x.Field<string>("DOCUMENT_TYPE").ToLower().Trim() == "buku rekening");
                        foreach (var row in rowsToUpdate)
                        {
                            row.SetField("IsMandatory", false);
                        }
                    }
                    // upload jadi wajin jika ada perubahan info rekenign
                }
                //}
            }

            //optional bisa diisi atau tidak
            if (FlagOPR == "APRHDR" && int.Parse(model.StatusFollow.ToString()) == (int)StatusDocTrans.APRV
                && (model.NIKBaru ?? "") == ""
                && (int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.BARU))
            {
                valid = "Silahkan Isikan NIK Mitra";
                return valid;
            }

            if (FlagOPR != "APRHDR")
            {
                if (files != null)
                {

                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file.FileName.Length > 50)
                        {
                            valid = "Nama File " + file.FileName + " tidak boleh lebih dari 50 karakter";
                            return valid;
                        }

                        if (!file.ContentType.ToLower().Contains("jpg") && !file.ContentType.ToLower().Contains("jpeg") && !file.ContentType.ToLower().Contains("pdf"))
                        {
                            valid = "File " + file.FileName + " harus Extention jpg,jpeg,pdf";
                            return valid;
                        }
                        decimal sizep = file.ContentLength / 1024 / 102;
                        if (sizep > 350)
                        {
                            valid = "File " + file.FileName + " Ukuran File harus lebih kecil dari 350 KB";
                            return valid;
                        }
                    }
                }

                if (model.StatusFollow != "5")
                {
                    int cnt = dokumenlistrequired.AsEnumerable().Where(x => x.Field<Int32>("Uploaded") == 0 && x.Field<Boolean>("IsMandatory") == true).Count();
                    if (cnt > 0)
                    {
                        DataTable dtt = dokumenlistrequired.AsEnumerable().Where(x => x.Field<Int32>("Uploaded") == 0 && x.Field<Boolean>("IsMandatory") == true).CopyToDataTable();
                        foreach (DataRow dr in dtt.Rows)
                        {
                            string findoc = HasKeyProtect.Encryption(dr["DOCUMENT_TYPE"].ToString());
                            if (documen != null)
                            {
                                int find = documen.ToList().Where(x => x == findoc).Count();
                                if (find == 0)
                                {
                                    if (int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.UBAH ||
                                        int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.PANJANG ||
                                        int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.BARU)
                                    {
                                        valid = "Silahkan lengkapi dokumen '" + dr["DOCUMENT_TYPE"].ToString() + "'";
                                    }
                                    else
                                    {
                                        valid = "Silahkan lengkapi dokumen yang bertanda bintang";
                                    }
                                    return valid;
                                }
                            }
                            else
                            {
                                if (int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.UBAH ||
                                       int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.PANJANG ||
                                       int.Parse(model.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.BARU)
                                {
                                    valid = "Silahkan lengkapi dokumen '" + dr["DOCUMENT_TYPE"].ToString() + "'";
                                    //valid = "Silahkan lengkapi dokumen sesuai dengan perubahan data yang anda lakukan";
                                }
                                else
                                {
                                    valid = "Silahkan lengkapi dokumen yang bertanda bintang";
                                }
                                return valid;
                            }
                            //IsMandatory
                        }
                    }
                }
            }

            //if (FlagOPR == "CRETHDR" || FlagOPR == "REVHDR")
            //{

            //}

            return valid;
        }
    }

    [Serializable]
    public class vmRegmitraddl
    {

        #region Application

        public async Task<List<string>> dbGetDetailTxListCount(string ItemCode, string RegmitraNo, int PageNumber, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@ItemCode", ItemCode),
                    new SqlParameter("@RegmitraNo", RegmitraNo),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trxdt_forcast_list_cnt", sqlParam);

            List<String> dta = new List<string>();
            if (dt.Rows.Count > 0)
            {
                dta.Add(dt.Rows[0][0].ToString());
                dta.Add(dt.Rows[0][1].ToString());
                dta.Add(dt.Rows[0][2].ToString());
            }
            else
            {
                dta.Add("0");
                dta.Add("0");
                dta.Add("0");
            }


            return dta;
        }
        public async Task<List<DataTable>> dbGetDetailTxList(DataTable DTFromDB, string ItemCode, string RegmitraNo, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {

                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@ItemCode", ItemCode),
                    new SqlParameter("@RegmitraNo", RegmitraNo),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trxdt_forcast_list", sqlParam);


            }
            else
            {
                dt = DTFromDB;
            }


            dtlist.Add(dt);

            if (dt.Rows.Count > 0)
            {
                int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());
                dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();
            }

            dtlist.Add(dt);

            return dtlist;
        }

        public async Task<List<string>> dbGetHeaderTxListCount(string RequestNo, string Divisi, string Cabang, string NamaMitra, string fromdate, string todate, int PageNumber, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@Requestno", RequestNo),
                    new SqlParameter("@Divisi", Divisi),
                    new SqlParameter("@Cabang", Cabang),
                    new SqlParameter("@NamaMitra", NamaMitra),
                    new SqlParameter("@tglfrom", fromdate),
                    new SqlParameter("@tglto", todate),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_list_cnt", sqlParam);

            List<String> dta = new List<string>();
            if (dt.Rows.Count > 0)
            {
                dta.Add(dt.Rows[0][0].ToString());
                dta.Add(dt.Rows[0][1].ToString());
                dta.Add(dt.Rows[0][2].ToString());
            }
            else
            {
                dta.Add("0");
                dta.Add("0");
                dta.Add("0");
            }


            return dta;
        }
        public async Task<List<DataTable>> dbGetHeaderTxList(DataTable DTFromDB, string RequestNo, string Divisi, string Cabang, string NamaMitra, string fromdate, string todate, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {

                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@Requestno", RequestNo),
                    new SqlParameter("@Divisi", Divisi),
                    new SqlParameter("@Cabang", Cabang),
                    new SqlParameter("@NamaMitra", NamaMitra),
                    new SqlParameter("@tglfrom", fromdate),
                    new SqlParameter("@tglto", todate),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_list", sqlParam);


            }
            else
            {
                dt = DTFromDB;
            }


            dtlist.Add(dt);

            if (dt.Rows.Count > 0)
            {
                int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());
                dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();
            }

            dtlist.Add(dt);

            return dtlist;
        }

        public async Task<DataTable> dbSaveRegMitra(cRegmitra models, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                if ((models.FlagOperation ?? "") == "")
                {
                    SqlParameter[] sqlParam = {
                          new SqlParameter("@id", models.IDHeaderTx),
                            new SqlParameter("@RegNo", models.RegmitraNo??""),
                            new SqlParameter("@RegType", models.RegmitraType??""),
                            new SqlParameter("@RegDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@RegCreateby", models.RegmitraCreatedBy),
                            new SqlParameter("@Divisi", models.Divisi??""),
                            new SqlParameter("@Area", models.Area??""),
                            new SqlParameter("@Cabang", models.Cabang??""),
                            new SqlParameter("@handleJob", models.handleJob??""),
                            new SqlParameter("@tglmasuk", models.tglmasuk??""),
                            new SqlParameter("@tglakhir", models.tglakhir?? ""),
                            new SqlParameter("@NamaMitra", models.NamaMitra ?? ""),
                            new SqlParameter("@NoKTP", models.NoKTP?? ""),
                            new SqlParameter("@NoNPWP", models.NoNPWP?? ""),
                            new SqlParameter("@Alamat", models.Alamat?? ""),
                            new SqlParameter("@AlamatKorespodensi", models.AlamatKorespodensi?? ""),
                            new SqlParameter("@Tempatlahir", models.Tempatlahir ?? ""),
                            new SqlParameter("@Tgllahir", models.Tgllahir ?? ""),
                            new SqlParameter("@JenisKelamin", models.JenisKelamin?? ""),
                            new SqlParameter("@Pendidikan", models.Pendidikan ?? ""),
                            new SqlParameter("@StatusKawin", models.StatusKawin ?? ""),
                            new SqlParameter("@NamaBank", models.NamaBank?? ""),
                            new SqlParameter("@CabangBank", models.CabangBank?? ""),
                            new SqlParameter("@Norekening", models.Norekening?? ""),
                            new SqlParameter("@Pemilikkening", models.Pemilikkening?? ""),
                            new SqlParameter("@NoHP", models.NoHP ?? ""),
                            new SqlParameter("@NoHP1", models.NoHP1 ?? ""),
                            new SqlParameter("@NoSPPI", models.NoSPPI ?? ""),
                            new SqlParameter("@NoFAX", models.NoFAX ?? ""),
                            new SqlParameter("@NoWA", models.NoWA?? ""),
                            new SqlParameter("@Email", models.Email?? ""),
                            new SqlParameter("@NIKBaru", models.NIKBaru?? ""),
                            new SqlParameter("@NIKLama", models.NIKLama?? ""),
                            new SqlParameter("@NamaFB", models.NamaFB?? ""),
                            new SqlParameter("@ContractNo", models.ContractNo?? ""),
                            new SqlParameter("@Comment", models.FollowNotes??""),
                            new SqlParameter("@FlagOperation", models.FlagOperation??""),
                            new SqlParameter("@StatusFollow", models.StatusFollow ?? "0"),
                            new SqlParameter("@StatusMitra", models.RegmitraStatus??"1"),
                            new SqlParameter("@StatusDate", models.StatusDate ?? ""),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_sve", sqlParam);
                }
                else
                {

                    SqlParameter[] sqlParam = {
                            new SqlParameter("@id", models.IDHeaderTx),
                            new SqlParameter("@RefID", models.keylookupdataDTX),
                            new SqlParameter("@RegNo", models.RegmitraNo??""),
                            new SqlParameter("@RegType", models.RegmitraType??""),
                            new SqlParameter("@RegDate", models.RegmitraDate ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@RegCreateby", models.RegmitraCreatedBy),
                            new SqlParameter("@Divisi", models.Divisi??""),
                            new SqlParameter("@Area", models.Area??""),
                            new SqlParameter("@Cabang", models.Cabang??""),
                            new SqlParameter("@handleJob", models.handleJob??""),
                            new SqlParameter("@tglmasuk", models.tglmasuk??""),
                            new SqlParameter("@tglakhir", models.tglakhir?? ""),
                            new SqlParameter("@NamaMitra", models.NamaMitra ?? ""),
                            new SqlParameter("@NoKTP", models.NoKTP?? ""),
                            new SqlParameter("@NoNPWP", models.NoNPWP?? ""),
                            new SqlParameter("@Alamat", models.Alamat?? ""),
                            new SqlParameter("@AlamatKorespodensi", models.AlamatKorespodensi?? ""),
                            new SqlParameter("@Tempatlahir", models.Tempatlahir ?? ""),
                            new SqlParameter("@Tgllahir", models.Tgllahir ?? ""),
                            new SqlParameter("@JenisKelamin", models.JenisKelamin?? ""),
                            new SqlParameter("@Pendidikan", models.Pendidikan ?? ""),
                            new SqlParameter("@StatusKawin", models.StatusKawin ?? ""),
                            new SqlParameter("@NamaBank", models.NamaBank?? ""),
                            new SqlParameter("@CabangBank", models.CabangBank?? ""),
                            new SqlParameter("@Norekening", models.Norekening?? ""),
                            new SqlParameter("@Pemilikkening", models.Pemilikkening?? ""),
                            new SqlParameter("@NoSPPI", models.NoSPPI?? ""),
                            new SqlParameter("@NoHP", models.NoHP ?? ""),
                            new SqlParameter("@NoHP1", models.NoHP1 ?? ""),
                            new SqlParameter("@NoFAX", models.NoFAX ?? ""),
                            new SqlParameter("@NoWA", models.NoWA?? ""),
                            new SqlParameter("@Email", models.Email?? ""),
                            new SqlParameter("@NIKLama", models.NIKLama?? ""),
                            new SqlParameter("@NIKBaru", models.NIKBaru?? ""),
                            new SqlParameter("@NamaFB", models.NamaFB?? ""),
                            new SqlParameter("@ContractNo", models.ContractNo?? ""),
                            new SqlParameter("@Comment", models.FollowNotes??""),
                            new SqlParameter("@FlagOperation", models.FlagOperation??""),
                            new SqlParameter("@StatusMitra", models.RegmitraStatus??"1"),
                            new SqlParameter("@StatusFollow", models.StatusFollow ?? "0"),
                            new SqlParameter("@StatusDate", models.StatusDate ?? ""),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };


                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_sve", sqlParam);
                }


            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbPublishPKS(string ID, string tipe, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@type", tipe),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };


                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_sharepk", sqlParam);

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbSaveRegMitradoc(string id, string FlagOperation, string NIK, string NOKTP, string RegNo, string RegType, string documenttype, string filename, string contenttype, string contentlength, string filebyte, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                if ((FlagOperation ?? "") == "")
                {
                    SqlParameter[] sqlParam = {
                           new SqlParameter("@id", id),
                            new SqlParameter("@RegNo", RegNo),
                            new SqlParameter("@RegType", RegType),
                            new SqlParameter("@NoKTP", NOKTP),
                            new SqlParameter("@NIK", NIK),
                            new SqlParameter("@DocumentType", documenttype),
                            new SqlParameter("@FileName", filename),
                            new SqlParameter("@ContentType", contenttype),
                            new SqlParameter("@ContentLength", contentlength),
                            new SqlParameter("@FileByte",filebyte),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_docsve", sqlParam);
                }
                else
                {

                    SqlParameter[] sqlParam = {
                           new SqlParameter("@id", id),
                            new SqlParameter("@RegNo", RegNo),
                            new SqlParameter("@RegType", RegType),
                            new SqlParameter("@NoKTP", NOKTP),
                            new SqlParameter("@NIK", NIK),
                            new SqlParameter("@DocumentType", documenttype),
                            new SqlParameter("@FileName", filename),
                            new SqlParameter("@ContentType", contenttype),
                            new SqlParameter("@ContentLength", contentlength),
                            new SqlParameter("@FileByte",filebyte),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_docsve", sqlParam);
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }


        public async Task<DataTable> dbSaveDocTemp(string id, string FlagOperation, string documenttype, string filename, string contenttype, string contentlength, string filebyte, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                           new SqlParameter("@id", id),
                            new SqlParameter("@DocumentType", documenttype),
                            new SqlParameter("@RegType", FlagOperation),
                            new SqlParameter("@FileName", filename),
                            new SqlParameter("@ContentType", contenttype),
                            new SqlParameter("@ContentLength", contentlength),
                            new SqlParameter("@FileByte",filebyte),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_document_type_template_sve", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbSaveUpdtStatMitra(string id, string StatusMitra, string tglubah, string catatan, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                           new SqlParameter("@ID", id),
                            new SqlParameter("@StatusMitra", StatusMitra),
                             new SqlParameter("@Tglubahstatus", tglubah),
                              new SqlParameter("@catatan", catatan),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_don_uptstatus", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }
        public async Task<List<string>> dbGetHeaderTxListdonCount(string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@keysearch", KeySearch),
                    new SqlParameter("@Divisi", Divisi),
                    new SqlParameter("@Cabang", Cabang),
                    new SqlParameter("@Area", Area),
                    new SqlParameter("@tglfrom", fromdate),
                    new SqlParameter("@tglto", todate),
                    new SqlParameter("@status", status),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_don_list_cnt", sqlParam);

            List<String> dta = new List<string>();
            if (dt.Rows.Count > 0)
            {
                dta.Add(dt.Rows[0][0].ToString());
                dta.Add(dt.Rows[0][1].ToString());
                dta.Add(dt.Rows[0][2].ToString());
            }
            else
            {
                dta.Add("0");
                dta.Add("0");
                dta.Add("0");
            }


            return dta;
        }
        public async Task<List<DataTable>> dbGetHeaderTxdonList(DataTable DTFromDB, string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {

                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@keysearch", KeySearch),
                    new SqlParameter("@Divisi", Divisi),
                    new SqlParameter("@Cabang", Cabang),
                    new SqlParameter("@Area", Area),
                    new SqlParameter("@tglfrom", fromdate),
                    new SqlParameter("@tglto", todate),
                    new SqlParameter("@status", status),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_don_list", sqlParam);


            }
            else
            {
                dt = DTFromDB;
            }


            dtlist.Add(dt);

            if (dt.Rows.Count > 0)
            {
                int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());
                dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();
            }

            dtlist.Add(dt);

            return dtlist;
        }


        public async Task<DataTable> dbGetTxdata4andal(string type, string tglpro, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@type", type),
                    new SqlParameter("@tglpro", tglpro),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
            };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_wfh_export_data4andal", sqlParam);

            return dt;
        }

        public async Task<DataTable> dbGetTxdata4andalRute(DataTable @tble, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@tble", @tble),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
            };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_wfh_route_aprv4andal", sqlParam);

            return dt;
        }

        public async Task<DataTable> dbGetRptTxdonList(string tipe,  string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, string idcaption, string userid, string groupname)
        {

            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@Tipe", tipe),
                    new SqlParameter("@keysearch", KeySearch),
                    new SqlParameter("@tglfrom", fromdate),
                    new SqlParameter("@tglto", todate),
                    new SqlParameter("@status", status),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
              };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_rpt_list", sqlParam);

            return dt;
        }


        //public async Task<int> dbDelDetailTx(DataTable dtemp, string ModuleID, string UserID, string GroupName)
        //{
        //    int result = 0;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());


        //        SqlParameter[] sqlParam = {
        //                    new SqlParameter("@tableuplod", dtemp),
        //                    new SqlParameter("@moduleid", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //         };


        //        dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trxdt_Regmitra_del", sqlParam);

        //        result = int.Parse(dt.Rows[0][0].ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
        //    }

        //    return result;
        //}



        //public async Task<DataTable> CreateTableAkta()
        //{
        //    await Task.Delay(1);

        //    DataTable TableOrderAkta = new DataTable();
        //    TableOrderAkta = new DataTable("TableOrderAkta");

        //    DataColumn CONT_TYPE = new DataColumn("CONT_TYPE");
        //    CONT_TYPE.DataType = System.Type.GetType("System.Int32");
        //    TableOrderAkta.Columns.Add(CONT_TYPE);

        //    DataColumn CLIENT_FDC_ID = new DataColumn("CLIENT_FDC_ID");
        //    CLIENT_FDC_ID.DataType = System.Type.GetType("System.Int64");
        //    TableOrderAkta.Columns.Add(CLIENT_FDC_ID);

        //    DataColumn CONT_NO = new DataColumn("CONT_NO");
        //    CONT_NO.DataType = System.Type.GetType("System.String");
        //    TableOrderAkta.Columns.Add(CONT_NO);

        //    DataColumn DEED_DATE = new DataColumn("DEED_DATE");
        //    DEED_DATE.DataType = System.Type.GetType("System.DateTime");
        //    TableOrderAkta.Columns.Add(DEED_DATE);

        //    DataColumn DEED_NO = new DataColumn("DEED_NO");
        //    DEED_NO.DataType = System.Type.GetType("System.String");
        //    TableOrderAkta.Columns.Add(DEED_NO);

        //    DataColumn DEED_CODE = new DataColumn("DEED_CODE");
        //    DEED_CODE.DataType = System.Type.GetType("System.String");
        //    TableOrderAkta.Columns.Add(DEED_CODE);

        //    DataColumn DEED_TIME = new DataColumn("DEED_TIME");
        //    DEED_TIME.DataType = System.Type.GetType("System.TimeSpan");
        //    TableOrderAkta.Columns.Add(DEED_TIME);

        //    DataColumn CLNT_ID = new DataColumn("CLNT_ID");
        //    CLNT_ID.DataType = System.Type.GetType("System.String");
        //    TableOrderAkta.Columns.Add(CLNT_ID);

        //    DataColumn NTRY_ID = new DataColumn("NTRY_ID");
        //    NTRY_ID.DataType = System.Type.GetType("System.String");
        //    TableOrderAkta.Columns.Add(NTRY_ID);

        //    DataColumn SEND_CLIENT_DATE = new DataColumn("SEND_CLIENT_DATE");
        //    SEND_CLIENT_DATE.DataType = System.Type.GetType("System.DateTime");
        //    TableOrderAkta.Columns.Add(SEND_CLIENT_DATE);

        //    return TableOrderAkta;
        //}
        //public async Task<int> dbSaveAkta(DataTable TableOrderAkta, string UserID, string GroupName)
        //{

        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            string TableOrderAktaStr = JsonConvert.SerializeObject(TableOrderAkta, Formatting.Indented);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"TableOrderAktaStr", TableOrderAktaStr },
        //                   {"UserID", UserID},
        //                   {"GroupName", GroupName},
        //                };


        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextAkta.cmdSaveAkta.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}
        //public async Task<string> dbSaveAktaValid(DataTable TableOrderAkta, bool IsFleet)
        //{

        //    string resultInt = "";
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            string TableOrderAktaStr = JsonConvert.SerializeObject(TableOrderAkta, Formatting.Indented);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"TableOrderAktaStr", TableOrderAktaStr },
        //                   {"IsFleet", IsFleet.ToString()},
        //                };


        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextAkta.cmdSaveAktavalid.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultInt = responsed.Content.ReadAsAsync<string>().Result;
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        #endregion Application

    }
}