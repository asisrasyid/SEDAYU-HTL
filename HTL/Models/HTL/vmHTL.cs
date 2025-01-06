using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DusColl
{
    [Serializable]
    public class vmHTL
    {
        public cFilterContract FilterTransaksi { get; set; }
        public DataTable DTHeaderTx { get; set; }
        public DataTable DTHeaderRefTx { get; set; }
        public DataTable DTDetailTx { get; set; }
        public DataTable DTAllTx { get; set; }
        public cHTL HeaderInfo { get; set; }
        public cHTL HeaderInfoOld { get; set; }
        public DataTable DTAllLog { get; set; }
        public DataTable DTLogTx { get; set; }
        public DataTable DTDokumen { get; set; }
        public DataTable DTResultUpload { get; set; }
        public DataTable DTHistory { get; set; }
        public DataTable DTGIsue { get; set; }

        public DataTable DTSRT { get; set; }
        public DataTable DTSRTPSG { get; set; }
        public DataTable DTDBTPSG { get; set; }

        public cAccountMetrik Permission { get; set; }
        public string CheckWithKey { get; set; }
        public string DokumenType { get; set; }
    }

    public class OCRData
    {
        public string Provinsi { get; set; }
        public string Kota { get; set; }
        public string Nik { get; set; }
        public string Nama { get; set; }
        public string TempatLahir { get; set; }
        public string KelaminTemp { get; set; }
        public string Alamat { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public string Kel { get; set; }
        public string Kec { get; set; }
        public string Agama { get; set; }
        public string StatusPerkawinan { get; set; }
        public string Kewarganegaraan { get; set; }
        public string JenisKelaminDesc { get; set; }
        public string TanggalLahir { get; set; }
    }

    [Serializable]
    public class blHTLddl
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
    public class vmHTLddl
    {
        #region Application

        public async Task<List<string>> dbGetHeaderTxListCount(string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, string idcaption, string userid, string groupname)
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

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_list_cnt", sqlParam);

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

        public async Task<List<string>> dbGetHeaderTxListCountRoyaPros(string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, string idcaption, string userid, string groupname)
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
                    new SqlParameter ("@moduleId","ROYALIST"),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_roya_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetHeaderTxList(DataTable DTFromDB, string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_list", sqlParam);
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
        public async Task<List<DataTable>> dbGetHeaderTxListRoyaPros(DataTable DTFromDB, string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                    new SqlParameter ("@moduleId","ROYALIST"),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_roya_list", sqlParam);
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


        public async Task<List<string>> dbGetHeaderTxListHTCount(string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, string idcaption, string userid, string groupname)
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

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlht_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetHeaderTxHTList(DataTable DTFromDB, string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlht_list", sqlParam);
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

        public async Task<DataTable> dbGetHeaderTxHTCheckedList(int status, bool chec, string userid, string Search)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@status", status),
                    new SqlParameter("@Checked", chec),
                    new SqlParameter("@Serach", Search??""),
                    new SqlParameter("@UserIDLog", userid??""),
             };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlht_checked", sqlParam);

            return dt;
        }
        public async Task<DataTable> dbGetHeaderTxRoyaCheckedList(int status, bool chec, string userid, string Search)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@status", status),
                    new SqlParameter("@Checked", chec),
                    new SqlParameter("@Serach", Search??""),
                    new SqlParameter("@UserIDLog", userid??""),
             };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_roya_checked", sqlParam);

            return dt;
        }

        public async Task<DataTable> dbGetMultiData4NIK(string noAppl, string NIK, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@NoAppl", noAppl),
                    new SqlParameter("@NIK", NIK),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlmulti_srchni", sqlParam);

            return dt;
        }

        public async Task<DataTable> dbGetMultiData(string noAppl, string JenisData, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@NoAppl", noAppl),
                    new SqlParameter("@JenisData", JenisData),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlmulti_list", sqlParam);

            return dt;
        }

        public async Task<DataTable> dbShowHisHTLINV(string NoAPPL, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@NoReg",NoAPPL),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_invoice_ppat_check", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbShowHisHTLEXP(string NoAPPL, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@NoReg",NoAPPL),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_order_htl_expired_check", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbShowHisHTLRJT(string NoAPPL, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@NoReg",NoAPPL),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_order_htl_reject_check", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbShowHisHTL(string tipe, string NoAPPL, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@tipe",tipe),
                        new SqlParameter("@NoAppL",NoAPPL),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_hist_list", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbShowHisGroupIsue(string tipe)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@tipe",""),
                 };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_prm_grouping_isue", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<int> dbRequestDoc(string tipe, string NoAPPL, string moduleID, string userid, string groupname)
        {
            int dt = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@JenisReq",tipe),
                        new SqlParameter("@NoAppL",NoAPPL),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dtx = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_requestdoc_sve", sqlParam);
                dt = int.Parse(dtx.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<int> dbupdateHTL(int ID, cHTL model, string confirm, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@Id",ID),
                        new SqlParameter("@TglOrder",model.TglOrder??"1984-01-01"),
                        new SqlParameter("@NoAppL",model.NoAppl),
                        new SqlParameter("@NoSertifikat",model.NoSertifikat??""),
                        new SqlParameter("@NoBelanko",model.NoBlanko??""),
                        new SqlParameter("@JenisSertifikat",model.JenisSertifikat??""),
                        new SqlParameter("@NomorNIB",model.NomorNIB??""),
                        new SqlParameter("@NoSuratUkur",model.NoSuratUkur??""),
                        new SqlParameter("@TglSuratUkur",model.TglSuratUkur??""),
                        new SqlParameter("@LuasTanah",model.LuasTanah??"0"),
                        new SqlParameter("@LokasiTanahDiProvinsi",model.LokasiTanahDiProvinsi??""),
                        new SqlParameter("@LokasiTanahDiKota",model.LokasiTanahDiKota??""),
                        new SqlParameter("@LokasiTanahDiKecamatan",model.LokasiTanahDiKecamatan??""),
                        new SqlParameter("@LokasiTanahDiDesaKelurahan",model.LokasiTanahDiDesaKelurahan??""),

                        new SqlParameter("@NIKDebitur",model.NIKDebitur??""),
                        new SqlParameter("@PekerjaanDebitur",model.PekerjaanDebitur??""),
                        new SqlParameter("@Debitur",model.NamaDebitur??""),
                        new SqlParameter("@WargaDebitur",model.WargaDebitur??""),
                        new SqlParameter("@JKelaminDebitur",model.JKelaminDebitur??""),
                        new SqlParameter("@TptLahirDebitur",model.TptLahirDebitur??""),
                        new SqlParameter("@TgllahirDebitur",model.TgllahirDebitur??""),

                        new SqlParameter("@AlamatDebitur",model.AlamatDebitur??""),
                        new SqlParameter("@ProvinsiDebitur",model.ProvinsiDebitur??""),
                        new SqlParameter("@KotaDebitur",model.KotaDebitur??""),
                        new SqlParameter("@KecamatanDebitur",model.KecamatanDebitur??""),
                        new SqlParameter("@DesaKelurahanDebitur",model.DesaKelurahanDebitur??""),

                        new SqlParameter("@NIKPemilikSertifikat",model.NIKPemilikSertifikat??""),
                        new SqlParameter("@PekerjaanPemilikSertifikat",model.PekerjaanPemilikSertifikat??""),
                        new SqlParameter("@NamaPemilikSertifikat",model.NamaPemilikSertifikat??""),

                        new SqlParameter("@JKelaminPemilikSertifikat",model.JKelaminPemilikSertifikat??""),
                        new SqlParameter("@TptlahirPemilikSertifikat",model.TptlahirPemilikSertifikat??""),
                        new SqlParameter("@TgllahirPemilikSertifikat",model.TgllahirPemilikSertifikat ??""),
                        new SqlParameter("@WargaPemilikSertifikat",model.WargaPemilikSertifikat??""),

                        new SqlParameter("@AlamatPemilikSertifikat",model.AlamatPemilikSertifikat??""),
                        new SqlParameter("@ProvinsiPemilikSertifikat",model.ProvinsiPemilikSertifikat??""),
                        new SqlParameter("@KotaPemilikSertifikat",model.KotaPemilikSertifikat??""),
                        new SqlParameter("@KecamatanPemilikSertifikat",model.KecamatanPemilikSertifikat??""),
                        new SqlParameter("@DesaKelurahanPemilikSertifikat",model.DesaKelurahanPemilikSertifikat??""),
                        new SqlParameter("@JenisPengajuan",model.JenisPengajuan??"0"),

                        new SqlParameter("@OrderKeNotaris",model.OrderKeNotaris??""),
                        new SqlParameter("@NilaiHT",model.NilaiHT??"0"),
                        new SqlParameter("@NilaiTerimaNasabah",model.NilaiPinjamanDiterima ??"0"),
                        new SqlParameter("@KodeAkta",model.KodeAkta??""),
                        new SqlParameter("@NoHT",model.NoHT??""),
                        new SqlParameter("@TglSertifikatCEK",model.TglSertifikatCEK??""),

                        new SqlParameter("@JasaPengecekan",bool.Parse(model.JasaPengecekan.ToString())),
                        new SqlParameter("@JasaValidasi",bool.Parse(model.JasaValidasi.ToString())),
                        new SqlParameter("@SKMHT",bool.Parse(model.SKMHT.ToString())),
                        new SqlParameter("@APHT_SHT",bool.Parse(model.APHT_SHT.ToString())),
                        new SqlParameter("@ROYA",bool.Parse(model.ROYA.ToString())),
                        new SqlParameter("@PENCORETAN_PTSL",bool.Parse(model.PENCORETAN_PTSL.ToString())),
                        new SqlParameter("@KUASA_MENGAMBIL",bool.Parse(model.KUASA_MENGAMBIL.ToString())),
                        new SqlParameter("@PNBP",bool.Parse(model.PNBP.ToString())),
                        new SqlParameter("@ADM_HT",bool.Parse(model.ADM_HT.ToString())),

                        new SqlParameter("@ttdfformstr",""),
                        new SqlParameter("@ttdskstr",""),
                        new SqlParameter("@ttdabsahstr",""),
                        new SqlParameter("@ttnsbh",""),

                        new SqlParameter("@NoPerjanjian",model.NoPerjanjian??""),
                        new SqlParameter("@TglPerjanjian",model.TglPerjanjian??""),

                        new SqlParameter("@NamaCabang",model.NamaCabang??""),
                        new SqlParameter("@Keterangan",model.Keterangan??""),
                        new SqlParameter("@Status",model.Status??"0"),
                        new SqlParameter("@confirm",confirm),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<DataTable> dbupdateHTLNW(int ID, cHTL model, string confirm, string moduleID, string userid, string groupname)
        {
            DataTable result = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@Id",ID),
                        new SqlParameter("@TglOrder",model.TglOrder??"1984-01-01"),
                        new SqlParameter("@NoAppL",model.NoAppl),
                        new SqlParameter("@NoSertifikat",model.NoSertifikat??""),
                        new SqlParameter("@NoBelanko",model.NoBlanko??""),
                        new SqlParameter("@JenisSertifikat",model.JenisSertifikat??""),
                        new SqlParameter("@NomorNIB",model.NomorNIB??""),
                        new SqlParameter("@NoSuratUkur",model.NoSuratUkur??""),
                        new SqlParameter("@TglSuratUkur",model.TglSuratUkur??""),
                        new SqlParameter("@LuasTanah",model.LuasTanah??"0"),
                        new SqlParameter("@LokasiTanahDiProvinsi",model.LokasiTanahDiProvinsi??""),
                        new SqlParameter("@LokasiTanahDiKota",model.LokasiTanahDiKota??""),
                        new SqlParameter("@LokasiTanahDiKecamatan",model.LokasiTanahDiKecamatan??""),
                        new SqlParameter("@LokasiTanahDiDesaKelurahan",model.LokasiTanahDiDesaKelurahan??""),

                        new SqlParameter("@NIKDebitur",model.NIKDebitur??""),
                        new SqlParameter("@PekerjaanDebitur",model.PekerjaanDebitur??""),
                        new SqlParameter("@Debitur",model.NamaDebitur??""),
                        new SqlParameter("@WargaDebitur",model.WargaDebitur??""),
                        new SqlParameter("@JKelaminDebitur",model.JKelaminDebitur??""),
                        new SqlParameter("@TptLahirDebitur",model.TptLahirDebitur??""),
                        new SqlParameter("@TgllahirDebitur",model.TgllahirDebitur??""),

                        new SqlParameter("@AlamatDebitur",model.AlamatDebitur??""),
                        new SqlParameter("@RTDebitur",model.RTDebitur??""),
                        new SqlParameter("@RWDebitur",model.RWDebitur??""),
                        new SqlParameter("@StatusDebitur",model.StatusDebitur??""),

                        new SqlParameter("@ProvinsiDebitur",model.ProvinsiDebitur??""),
                        new SqlParameter("@KotaDebitur",model.KotaDebitur??""),
                        new SqlParameter("@KecamatanDebitur",model.KecamatanDebitur??""),
                        new SqlParameter("@DesaKelurahanDebitur",model.DesaKelurahanDebitur??""),

                        new SqlParameter("@NIKPemilikSertifikat",model.NIKPemilikSertifikat??""),
                        new SqlParameter("@PekerjaanPemilikSertifikat",model.PekerjaanPemilikSertifikat??""),
                        new SqlParameter("@NamaPemilikSertifikat",model.NamaPemilikSertifikat??""),

                        new SqlParameter("@JKelaminPemilikSertifikat",model.JKelaminPemilikSertifikat??""),
                        new SqlParameter("@TptlahirPemilikSertifikat",model.TptlahirPemilikSertifikat??""),
                        new SqlParameter("@TgllahirPemilikSertifikat",model.TgllahirPemilikSertifikat ??""),
                        new SqlParameter("@WargaPemilikSertifikat",model.WargaPemilikSertifikat??""),

                        new SqlParameter("@AlamatPemilikSertifikat",model.AlamatPemilikSertifikat??""),
                        new SqlParameter("@ProvinsiPemilikSertifikat",model.ProvinsiPemilikSertifikat??""),
                        new SqlParameter("@KotaPemilikSertifikat",model.KotaPemilikSertifikat??""),
                        new SqlParameter("@KecamatanPemilikSertifikat",model.KecamatanPemilikSertifikat??""),
                        new SqlParameter("@DesaKelurahanPemilikSertifikat",model.DesaKelurahanPemilikSertifikat??""),
                        new SqlParameter("@JenisPengajuan",model.JenisPengajuan??"0"),

                        new SqlParameter("@OrderKeNotaris",model.OrderKeNotaris??""),
                        new SqlParameter("@NilaiHT",model.NilaiHT??"0"),
                        new SqlParameter("@NilaiTerimaNasabah",model.NilaiPinjamanDiterima ??"0"),
                        new SqlParameter("@KodeAkta",model.KodeAkta??""),
                        new SqlParameter("@NoHT",model.NoHT??""),
                        new SqlParameter("@TglSertifikatCEK",model.TglSertifikatCEK??""),

                        new SqlParameter("@JasaPengecekan",bool.Parse(model.JasaPengecekan.ToString())),
                        new SqlParameter("@JasaValidasi",bool.Parse(model.JasaValidasi.ToString())),
                        new SqlParameter("@SKMHT",bool.Parse(model.SKMHT.ToString())),
                        new SqlParameter("@APHT_SHT",bool.Parse(model.APHT_SHT.ToString())),
                        new SqlParameter("@ROYA",bool.Parse(model.ROYA.ToString())),
                        new SqlParameter("@PENCORETAN_PTSL",bool.Parse(model.PENCORETAN_PTSL.ToString())),
                        new SqlParameter("@KUASA_MENGAMBIL",bool.Parse(model.KUASA_MENGAMBIL.ToString())),
                        new SqlParameter("@PNBP",bool.Parse(model.PNBP.ToString())),
                        new SqlParameter("@ADM_HT",bool.Parse(model.ADM_HT.ToString())),

                        new SqlParameter("@ttdfformstr",""),
                        new SqlParameter("@ttdskstr",""),
                        new SqlParameter("@ttdabsahstr",""),
                        new SqlParameter("@ttnsbh",""),

                        new SqlParameter("@NoPerjanjian",model.NoPerjanjian??""),
                        new SqlParameter("@TglPerjanjian",model.TglPerjanjian??""),

                        new SqlParameter("@NamaCabang",model.NamaCabang??""),
                        new SqlParameter("@Keterangan",model.Keterangan??""),
                        new SqlParameter("@Status",model.Status??"0"),
                        new SqlParameter("@confirm",confirm),
                        new SqlParameter("@Perihal",model.Case), /* isue sertifikat*/
                        new SqlParameter("@PerihalCab",model.CaseCab), /* alasan Reject*/
                        new SqlParameter("@AlasanPending",model.CaseCabPending), /* alasan Pending*/
                        new SqlParameter("@AlasanPendingAkd",model.CaseCabPendingAkd), /* alasan Pending akad*/

                        new SqlParameter("@NoSHT",model.nosht??""),
                        new SqlParameter("@KodeSHT",model.kodesht??""),
                        new SqlParameter("@NoBerkasSHT",model.noberkasht??""),
                        new SqlParameter("@NoBerkascek",model.noberkasceking??""),

                        new SqlParameter("@TglHasilSertifikat",model.TglHasilSertifikat??""),
                        new SqlParameter("@JmlTerbitSPA",model.JmlTerbitSPA),

                        new SqlParameter("@TglDueDateISUSHM",model.DeadlineSLA??""),

                        new SqlParameter("@PosHandle",model.PosisiPenangan??""),

                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                result = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_sve", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbupdateHTLIPT(int ID, cHTLIPTData model, string confirm, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@Id",ID),
                        new SqlParameter("@NoAppL",model.NoApplIpt),
                        new SqlParameter("@JenisData",model.JenisData??""),
                        new SqlParameter("@Mode",confirm??""),
                        new SqlParameter("@JenisHak",model.JenisSertifikat??""),
                        new SqlParameter("@NoSertifikat",model.NoSertifikat??""),
                        new SqlParameter("@NoNIB",model.NomorNIB??""),
                        new SqlParameter("@NoBlanko",model.NoBlanko??""),
                        new SqlParameter("@NoSuratUkur",model.NoSuratUkur??""),
                        new SqlParameter("@TglSuratUkur",model.TglSuratUkur??""),
                        new SqlParameter("@LuasTanah",model.LuasTanah??""),
                        new SqlParameter("@ProvinsiTanah",model.LokasiTanahDiProvinsi??""),
                        new SqlParameter("@KotaTanah",model.LokasiTanahDiKota??""),
                        new SqlParameter("@KecamatanTanah",model.LokasiTanahDiKecamatan??""),
                        new SqlParameter("@DesaKelurahanTanah",model.LokasiTanahDiDesaKelurahan??""),

                        new SqlParameter("@RefNIK",model.REFNIK??""),
                        new SqlParameter("@NIK",model.NIK??""),
                        new SqlParameter("@Pekerjaan",model.Pekerjaan??""),
                        new SqlParameter("@Nama",model.Nama??""),

                        new SqlParameter("@JKelamin",model.JKelamin??""),
                        new SqlParameter("@Tptlahir",model.Tptlahir??""),
                        new SqlParameter("@Tgllahir",model.Tgllahir ??""),
                        new SqlParameter("@Warga",model.Warga??""),
                        new SqlParameter("@StatusNikah",model.StatusPernikahan??""),

                        new SqlParameter("@Alamat",model.Alamat??""),
                        new SqlParameter("@RT",model.RT??""),
                        new SqlParameter("@RW",model.RW??""),
                        new SqlParameter("@Provinsi",model.Provinsi??""),
                        new SqlParameter("@Kota",model.Kota??""),
                        new SqlParameter("@Kecamatan",model.Kecamatan??""),
                        new SqlParameter("@DesaKelurahan",model.DesaKelurahan??""),

                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlmulti_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<DataTable> dbgetInvPPATvalid(string jenis, string OrderPPAT, string moduleID, string userid, string groupname)
        {
            DataTable result = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@jeninv",jenis),
                        new SqlParameter("@OderPPAT", OrderPPAT),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };
                await Task.Delay(0);
                result = await dbaccess.ExecuteDataTable(strconnection, "udp_app_htl_order_invppat_val", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return result;
        }

        public async Task<DataTable> dbgetOrderOS(string jenis, string OrderPPAT, string moduleID, string userid, string groupname)
        {
            DataTable result = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@jeninv",jenis),
                        new SqlParameter("@OderPPAT", OrderPPAT),
                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };
                await Task.Delay(0);
                result = await dbaccess.ExecuteDataTable(strconnection, "udp_app_htl_order_validation_od", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return result;
        }

        public async Task<DataTable> dbupdateBAST(string JenisINV, DataTable dtx, byte[] FileByteINV, string InvNo, string namefile, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                  {
                        new SqlParameter ("@jeninv",JenisINV),
                        new SqlParameter ("@tempkey",dtx),
                        new SqlParameter ("@FileNameINV",FileByteINV),
                        new SqlParameter ("@Noinv",InvNo),
                        new SqlParameter ("@Filenme",namefile),
                        new SqlParameter ("@moduleId",moduleID),
                        new SqlParameter ("@UserIDLog",userid),
                        new SqlParameter ("@UserGroupLog",groupname),
                 };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_htl_order_bast", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return dt;
        }

        public async Task<int> dbupdateHTLIPTSPAHT(int ID, cHTLIPTData model, string confirm, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                        new SqlParameter("@Id",ID),
                        new SqlParameter("@NoAppL",model.NoApplIpt),
                        new SqlParameter("@JenisData",model.JenisData??""),
                        new SqlParameter("@Mode",confirm??""),

                        new SqlParameter("@KodeAkta",model.KodeAkta??""),
                        new SqlParameter("@NoHT",model.NoHT??""),
                        new SqlParameter("@TglHasilCek",model.TglSertiCekCN??""),
                        new SqlParameter("@TglSPA",model.TglSPA??""),

                        new SqlParameter("@TglSKMHT",model.TglSKMHT??""),
                        new SqlParameter("@NoSKMHT",model.NoSKMHT??""),

                        new SqlParameter("@KodeSHT",model.KodeSHT??""),
                        new SqlParameter("@NoSHT",model.NoSHT??""),
                        new SqlParameter("@NoBerkasSHT",model.NoBerkasSHT??""),
                        new SqlParameter("@LinkBerkasSHT",model.LinkBerkasSHT ??""),

                        new SqlParameter("@moduleId", moduleID),
                        new SqlParameter("@UserIDLog", userid),
                        new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlmulti_spa_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<DataTable> dbSaveRegMitradoc(string id, string FlagOperation, string NIK, string NOKTP, string RegNo, string RegType, string documenttype, string filename, string contenttype, string contentlength, string filebyte, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

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
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_docsve", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }
        public async Task<DataTable> dbSaveRoyaData(string id, string FlagOperation, string NIK, string NOKTP, string RegNo, string RegType, string documenttype, string filename, string contenttype, string contentlength, string filebyte, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

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
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_roya_upsv", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }
        public async Task<DataTable> dbUpdateRoyaSave(string id, string NoApps, string SttsNum,string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", id),
                            new SqlParameter("@NoApps", NoApps),
                            new SqlParameter("@SttsNum", SttsNum),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName),
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_roya_upsv", sqlParam);
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

        public async Task<DataTable> dbdbGetDdlOrderGetCek(string tipe, string Key, string tglproses, string moduleid, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@tipe" ,tipe),
                    new SqlParameter ("@tglpross" ,tglproses),
                    new SqlParameter ("@Key" ,Key),
                    new SqlParameter ("@moduleId",moduleid),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_order_htl_cek", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<int> dbupdateUploadRegisHTFlag(string jenisinv, DataTable dtx, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                SqlParameter[] sqlParam =
                  {
                        new SqlParameter ("@Jenis",jenisinv),
                        new SqlParameter ("@tempkey",dtx),
                        new SqlParameter ("@moduleId",moduleID),
                        new SqlParameter ("@UserIDLog",userid),
                        new SqlParameter ("@UserGroupLog",groupname),
                 };
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_htl_sht_genflag", sqlParam);
                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return result;
        }

        public async Task<int> dbupdateUploadSCHBACTlag(string jenisinv, string nobast, string tglbast, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                SqlParameter[] sqlParam =
                  {
                        new SqlParameter ("@Jenis",jenisinv),
                        new SqlParameter ("@NoBAST",nobast),
                        new SqlParameter ("@tgl",tglbast),
                        new SqlParameter ("@moduleId",moduleID),
                        new SqlParameter ("@UserIDLog",userid),
                        new SqlParameter ("@UserGroupLog",groupname),
                 };
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_htl_bast_genflag", sqlParam);
                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return result;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlNotarisListByEncrypt(string Moduleid, string NoAppl, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@noappl" ,NoAppl),
                    new SqlParameter ("@moduleId" ,Moduleid),
                    new SqlParameter ("@UserIDLog" ,UserID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_notaris", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = HashNetFramework.HasKeyProtect.Encryption(c.Field<string>("IDNotaris")),
                           Text = c.Field<string>("NamaNotaris")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbGetRptTxdonList(string tipe, string KeySearch, string Notaris, string cabang, string fromdate, string todate, string status, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@Tipe", tipe),
                    new SqlParameter("@keysearch", KeySearch),
                    new SqlParameter("@Notaris", Notaris),
                    new SqlParameter("@cabang", cabang),
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

        public async Task<DataTable> dbSaveIsueGrp(string NoAPP, string PosBerkas, string GroupPos, string ketPos, int nominal, string user)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                           new SqlParameter("@NoAplikasi", NoAPP),
                            new SqlParameter("@PosisiBerkas", PosBerkas),
                            new SqlParameter("@GroupPosisi", GroupPos),
                            new SqlParameter("@Keterangan", ketPos),
                            new SqlParameter("@nominal", nominal),
                            new SqlParameter("@User", user),
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udt_app_htl_groupisue_sve", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }
        

        //public async Task<List<string>> dbGetDetailTxListCount(string ItemCode, string RegmitraNo, int PageNumber, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();

        //    dbAccessHelper dbaccess = new dbAccessHelper();
        //    string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //    SqlParameter[] sqlParam =
        //    {
        //            new SqlParameter("@ItemCode", ItemCode),
        //            new SqlParameter("@RegmitraNo", RegmitraNo),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //            new SqlParameter ("@PageNumber",PageNumber),
        //        };

        //    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trxdt_forcast_list_cnt", sqlParam);

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }

        //    return dta;
        //}
        //public async Task<List<DataTable>> dbGetDetailTxList(DataTable DTFromDB, string ItemCode, string RegmitraNo, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //        SqlParameter[] sqlParam =
        //         {
        //            new SqlParameter("@ItemCode", ItemCode),
        //            new SqlParameter("@RegmitraNo", RegmitraNo),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //            new SqlParameter ("@PageNumber",PageNumber),
        //        };

        //        dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trxdt_forcast_list", sqlParam);

        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);

        //    if (dt.Rows.Count > 0)
        //    {
        //        int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();
        //    }

        //    dtlist.Add(dt);

        //    return dtlist;
        //}

        //public async Task<List<string>> dbGetHeaderTxListCount(string RequestNo, string Divisi, string Cabang, string NamaMitra, string fromdate, string todate, int PageNumber, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();

        //    dbAccessHelper dbaccess = new dbAccessHelper();
        //    string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //    SqlParameter[] sqlParam =
        //    {
        //            new SqlParameter("@Requestno", RequestNo),
        //            new SqlParameter("@Divisi", Divisi),
        //            new SqlParameter("@Cabang", Cabang),
        //            new SqlParameter("@NamaMitra", NamaMitra),
        //            new SqlParameter("@tglfrom", fromdate),
        //            new SqlParameter("@tglto", todate),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //            new SqlParameter ("@PageNumber",PageNumber),
        //        };

        //    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_list_cnt", sqlParam);

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }

        //    return dta;
        //}
        //public async Task<List<DataTable>> dbGetHeaderTxList(DataTable DTFromDB, string RequestNo, string Divisi, string Cabang, string NamaMitra, string fromdate, string todate, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //        SqlParameter[] sqlParam =
        //         {
        //            new SqlParameter("@Requestno", RequestNo),
        //            new SqlParameter("@Divisi", Divisi),
        //            new SqlParameter("@Cabang", Cabang),
        //            new SqlParameter("@NamaMitra", NamaMitra),
        //            new SqlParameter("@tglfrom", fromdate),
        //            new SqlParameter("@tglto", todate),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //            new SqlParameter ("@PageNumber",PageNumber),
        //        };

        //        dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_list", sqlParam);

        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);

        //    if (dt.Rows.Count > 0)
        //    {
        //        int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();
        //    }

        //    dtlist.Add(dt);

        //    return dtlist;
        //}

        //public async Task<DataTable> dbSaveRegMitra(cRegmitra models, string ModuleID, string UserID, string GroupName)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
        //        if ((models.FlagOperation ?? "") == "")
        //        {
        //            SqlParameter[] sqlParam = {
        //                  new SqlParameter("@id", models.IDHeaderTx),
        //                    new SqlParameter("@RegNo", models.RegmitraNo??""),
        //                    new SqlParameter("@RegType", models.RegmitraType??""),
        //                    new SqlParameter("@RegDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
        //                    new SqlParameter("@RegCreateby", models.RegmitraCreatedBy),
        //                    new SqlParameter("@Divisi", models.Divisi??""),
        //                    new SqlParameter("@Area", models.Area??""),
        //                    new SqlParameter("@Cabang", models.Cabang??""),
        //                    new SqlParameter("@handleJob", models.handleJob??""),
        //                    new SqlParameter("@tglmasuk", models.tglmasuk??""),
        //                    new SqlParameter("@tglakhir", models.tglakhir?? ""),
        //                    new SqlParameter("@NamaMitra", models.NamaMitra ?? ""),
        //                    new SqlParameter("@NoKTP", models.NoKTP?? ""),
        //                    new SqlParameter("@NoNPWP", models.NoNPWP?? ""),
        //                    new SqlParameter("@Alamat", models.Alamat?? ""),
        //                    new SqlParameter("@AlamatKorespodensi", models.AlamatKorespodensi?? ""),
        //                    new SqlParameter("@Tempatlahir", models.Tempatlahir ?? ""),
        //                    new SqlParameter("@Tgllahir", models.Tgllahir ?? ""),
        //                    new SqlParameter("@JenisKelamin", models.JenisKelamin?? ""),
        //                    new SqlParameter("@Pendidikan", models.Pendidikan ?? ""),
        //                    new SqlParameter("@StatusKawin", models.StatusKawin ?? ""),
        //                    new SqlParameter("@NamaBank", models.NamaBank?? ""),
        //                    new SqlParameter("@CabangBank", models.CabangBank?? ""),
        //                    new SqlParameter("@Norekening", models.Norekening?? ""),
        //                    new SqlParameter("@Pemilikkening", models.Pemilikkening?? ""),
        //                    new SqlParameter("@NoHP", models.NoHP ?? ""),
        //                    new SqlParameter("@NoHP1", models.NoHP1 ?? ""),
        //                    new SqlParameter("@NoSPPI", models.NoSPPI ?? ""),
        //                    new SqlParameter("@NoFAX", models.NoFAX ?? ""),
        //                    new SqlParameter("@NoWA", models.NoWA?? ""),
        //                    new SqlParameter("@Email", models.Email?? ""),
        //                    new SqlParameter("@NIKBaru", models.NIKBaru?? ""),
        //                    new SqlParameter("@NIKLama", models.NIKLama?? ""),
        //                    new SqlParameter("@NamaFB", models.NamaFB?? ""),
        //                    new SqlParameter("@ContractNo", models.ContractNo?? ""),
        //                    new SqlParameter("@Comment", models.FollowNotes??""),
        //                    new SqlParameter("@FlagOperation", models.FlagOperation??""),
        //                    new SqlParameter("@StatusFollow", models.StatusFollow ?? "0"),
        //                    new SqlParameter("@StatusMitra", models.RegmitraStatus??"1"),
        //                    new SqlParameter("@StatusDate", models.StatusDate ?? ""),
        //                    new SqlParameter("@moduleId", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //            };

        //            await Task.Delay(0);
        //            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_sve", sqlParam);
        //        }
        //        else
        //        {
        //            SqlParameter[] sqlParam = {
        //                    new SqlParameter("@id", models.IDHeaderTx),
        //                    new SqlParameter("@RefID", models.keylookupdataDTX),
        //                    new SqlParameter("@RegNo", models.RegmitraNo??""),
        //                    new SqlParameter("@RegType", models.RegmitraType??""),
        //                    new SqlParameter("@RegDate", models.RegmitraDate ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
        //                    new SqlParameter("@RegCreateby", models.RegmitraCreatedBy),
        //                    new SqlParameter("@Divisi", models.Divisi??""),
        //                    new SqlParameter("@Area", models.Area??""),
        //                    new SqlParameter("@Cabang", models.Cabang??""),
        //                    new SqlParameter("@handleJob", models.handleJob??""),
        //                    new SqlParameter("@tglmasuk", models.tglmasuk??""),
        //                    new SqlParameter("@tglakhir", models.tglakhir?? ""),
        //                    new SqlParameter("@NamaMitra", models.NamaMitra ?? ""),
        //                    new SqlParameter("@NoKTP", models.NoKTP?? ""),
        //                    new SqlParameter("@NoNPWP", models.NoNPWP?? ""),
        //                    new SqlParameter("@Alamat", models.Alamat?? ""),
        //                    new SqlParameter("@AlamatKorespodensi", models.AlamatKorespodensi?? ""),
        //                    new SqlParameter("@Tempatlahir", models.Tempatlahir ?? ""),
        //                    new SqlParameter("@Tgllahir", models.Tgllahir ?? ""),
        //                    new SqlParameter("@JenisKelamin", models.JenisKelamin?? ""),
        //                    new SqlParameter("@Pendidikan", models.Pendidikan ?? ""),
        //                    new SqlParameter("@StatusKawin", models.StatusKawin ?? ""),
        //                    new SqlParameter("@NamaBank", models.NamaBank?? ""),
        //                    new SqlParameter("@CabangBank", models.CabangBank?? ""),
        //                    new SqlParameter("@Norekening", models.Norekening?? ""),
        //                    new SqlParameter("@Pemilikkening", models.Pemilikkening?? ""),
        //                    new SqlParameter("@NoSPPI", models.NoSPPI?? ""),
        //                    new SqlParameter("@NoHP", models.NoHP ?? ""),
        //                    new SqlParameter("@NoHP1", models.NoHP1 ?? ""),
        //                    new SqlParameter("@NoFAX", models.NoFAX ?? ""),
        //                    new SqlParameter("@NoWA", models.NoWA?? ""),
        //                    new SqlParameter("@Email", models.Email?? ""),
        //                    new SqlParameter("@NIKLama", models.NIKLama?? ""),
        //                    new SqlParameter("@NIKBaru", models.NIKBaru?? ""),
        //                    new SqlParameter("@NamaFB", models.NamaFB?? ""),
        //                    new SqlParameter("@ContractNo", models.ContractNo?? ""),
        //                    new SqlParameter("@Comment", models.FollowNotes??""),
        //                    new SqlParameter("@FlagOperation", models.FlagOperation??""),
        //                    new SqlParameter("@StatusMitra", models.RegmitraStatus??"1"),
        //                    new SqlParameter("@StatusFollow", models.StatusFollow ?? "0"),
        //                    new SqlParameter("@StatusDate", models.StatusDate ?? ""),
        //                    new SqlParameter("@moduleId", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //            };

        //            await Task.Delay(0);
        //            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_sve", sqlParam);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
        //    }

        //    return dt;
        //}

        //public async Task<DataTable> dbPublishPKS(string ID, string tipe, string ModuleID, string UserID, string GroupName)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //        SqlParameter[] sqlParam = {
        //                    new SqlParameter("@id", ID),
        //                    new SqlParameter("@type", tipe),
        //                    new SqlParameter("@moduleId", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //            };

        //        await Task.Delay(0);
        //        dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_sharepk", sqlParam);

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
        //    }

        //    return dt;
        //}

        //public async Task<DataTable> dbSaveRegMitradoc(string id, string FlagOperation, string NIK, string NOKTP, string RegNo, string RegType, string documenttype, string filename, string contenttype, string contentlength, string filebyte, string ModuleID, string UserID, string GroupName)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
        //        if ((FlagOperation ?? "") == "")
        //        {
        //            SqlParameter[] sqlParam = {
        //                   new SqlParameter("@id", id),
        //                    new SqlParameter("@RegNo", RegNo),
        //                    new SqlParameter("@RegType", RegType),
        //                    new SqlParameter("@NoKTP", NOKTP),
        //                    new SqlParameter("@NIK", NIK),
        //                    new SqlParameter("@DocumentType", documenttype),
        //                    new SqlParameter("@FileName", filename),
        //                    new SqlParameter("@ContentType", contenttype),
        //                    new SqlParameter("@ContentLength", contentlength),
        //                    new SqlParameter("@FileByte",filebyte),
        //                    new SqlParameter("@moduleId", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //            };

        //            await Task.Delay(0);
        //            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_docsve", sqlParam);
        //        }
        //        else
        //        {
        //            SqlParameter[] sqlParam = {
        //                   new SqlParameter("@id", id),
        //                    new SqlParameter("@RegNo", RegNo),
        //                    new SqlParameter("@RegType", RegType),
        //                    new SqlParameter("@NoKTP", NOKTP),
        //                    new SqlParameter("@NIK", NIK),
        //                    new SqlParameter("@DocumentType", documenttype),
        //                    new SqlParameter("@FileName", filename),
        //                    new SqlParameter("@ContentType", contenttype),
        //                    new SqlParameter("@ContentLength", contentlength),
        //                    new SqlParameter("@FileByte",filebyte),
        //                    new SqlParameter("@moduleId", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //            };

        //            await Task.Delay(0);
        //            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_docsve", sqlParam);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
        //    }

        //    return dt;
        //}

        //public async Task<DataTable> dbSaveDocTemp(string id, string FlagOperation, string documenttype, string filename, string contenttype, string contentlength, string filebyte, string ModuleID, string UserID, string GroupName)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //        SqlParameter[] sqlParam = {
        //                   new SqlParameter("@id", id),
        //                    new SqlParameter("@DocumentType", documenttype),
        //                    new SqlParameter("@RegType", FlagOperation),
        //                    new SqlParameter("@FileName", filename),
        //                    new SqlParameter("@ContentType", contenttype),
        //                    new SqlParameter("@ContentLength", contentlength),
        //                    new SqlParameter("@FileByte",filebyte),
        //                    new SqlParameter("@moduleId", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //            };

        //        await Task.Delay(0);
        //        dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_document_type_template_sve", sqlParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
        //    }

        //    return dt;
        //}

        //public async Task<DataTable> dbSaveUpdtStatMitra(string id, string StatusMitra, string tglubah, string catatan, string ModuleID, string UserID, string GroupName)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //        SqlParameter[] sqlParam = {
        //                   new SqlParameter("@ID", id),
        //                    new SqlParameter("@StatusMitra", StatusMitra),
        //                     new SqlParameter("@Tglubahstatus", tglubah),
        //                      new SqlParameter("@catatan", catatan),
        //                    new SqlParameter("@moduleId", ModuleID),
        //                    new SqlParameter("@UserIDLog", UserID),
        //                    new SqlParameter("@UserGroupLog", GroupName)
        //            };

        //        await Task.Delay(0);
        //        dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_don_uptstatus", sqlParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
        //    }

        //    return dt;
        //}

        //public async Task<List<string>> dbGetHeaderTxListdonCount(string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();

        //    dbAccessHelper dbaccess = new dbAccessHelper();
        //    string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //    SqlParameter[] sqlParam =
        //    {
        //            new SqlParameter("@keysearch", KeySearch),
        //            new SqlParameter("@Divisi", Divisi),
        //            new SqlParameter("@Cabang", Cabang),
        //            new SqlParameter("@Area", Area),
        //            new SqlParameter("@tglfrom", fromdate),
        //            new SqlParameter("@tglto", todate),
        //            new SqlParameter("@status", status),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //            new SqlParameter ("@PageNumber",PageNumber),
        //        };

        //    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_don_list_cnt", sqlParam);

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }

        //    return dta;
        //}
        //public async Task<List<DataTable>> dbGetHeaderTxdonList(DataTable DTFromDB, string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        dbAccessHelper dbaccess = new dbAccessHelper();
        //        string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //        SqlParameter[] sqlParam =
        //         {
        //            new SqlParameter("@keysearch", KeySearch),
        //            new SqlParameter("@Divisi", Divisi),
        //            new SqlParameter("@Cabang", Cabang),
        //            new SqlParameter("@Area", Area),
        //            new SqlParameter("@tglfrom", fromdate),
        //            new SqlParameter("@tglto", todate),
        //            new SqlParameter("@status", status),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //            new SqlParameter ("@PageNumber",PageNumber),
        //        };

        //        dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_don_list", sqlParam);

        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);

        //    if (dt.Rows.Count > 0)
        //    {
        //        int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();
        //    }

        //    dtlist.Add(dt);

        //    return dtlist;
        //}

        //public async Task<DataTable> dbGetTxdata4andal(string type, string tglpro, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();

        //    dbAccessHelper dbaccess = new dbAccessHelper();
        //    string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //    SqlParameter[] sqlParam =
        //     {
        //            new SqlParameter("@type", type),
        //            new SqlParameter("@tglpro", tglpro),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //    };

        //    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_wfh_export_data4andal", sqlParam);

        //    return dt;
        //}

        //public async Task<DataTable> dbGetTxdata4andalRute(DataTable @tble, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();

        //    dbAccessHelper dbaccess = new dbAccessHelper();
        //    string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //    SqlParameter[] sqlParam =
        //     {
        //            new SqlParameter("@tble", @tble),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //    };

        //    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_wfh_route_aprv4andal", sqlParam);

        //    return dt;
        //}

        //public async Task<DataTable> dbGetRptTxdonList(string KeySearch, string Divisi, string Cabang, string Area, string fromdate, string todate, int status, int PageNumber, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();

        //    dbAccessHelper dbaccess = new dbAccessHelper();
        //    string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

        //    SqlParameter[] sqlParam =
        //     {
        //            new SqlParameter("@keysearch", KeySearch),
        //            new SqlParameter("@Divisi", Divisi),
        //            new SqlParameter("@Cabang", Cabang),
        //            new SqlParameter("@Area", Area),
        //            new SqlParameter("@tglfrom", fromdate),
        //            new SqlParameter("@tglto", todate),
        //            new SqlParameter("@status", status),
        //            new SqlParameter ("@moduleId",idcaption),
        //            new SqlParameter ("@UserIDLog",userid),
        //            new SqlParameter ("@UserGroupLog",groupname),
        //            new SqlParameter ("@PageNumber",PageNumber),
        //        };

        //    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_don_list", sqlParam);

        //    return dt;
        //}

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