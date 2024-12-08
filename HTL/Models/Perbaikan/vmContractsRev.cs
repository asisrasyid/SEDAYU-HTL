//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Aspose;
//using Aspose.Cells;
//using System.ComponentModel;
//using Aspose.Words;
//using Aspose.Words.Replacing;
//using System.Web.Helpers;
//using System.Drawing;

//namespace DusColl
//{
//    [Serializable]
//    public class vmContractsRev
//    {
//        public string DaftarCabang { get; set; }
//        public string JenisPenghapusan { get; set; }
//        public string PemberiFidusia { get; set; }

//        public cDouemnts RoyaDocumentOne { get; set; }

//        public cContracts ContractDetailOne { get; set; }

//        public IEnumerable<cListSelected> ddlDocumentType { get; set; }
//        public IEnumerable<cListSelected> ddlClient { get; set; }
//        public IEnumerable<cListSelected> ddlBranch { get; set; }
//        public IEnumerable<cListSelected> ddlContractStatus { get; set; }
//        public IEnumerable<cListSelected> ddlDocStatus { get; set; }
//        public IEnumerable<cListSelected> ddlJenisPelanggan { get; set; }
//        public IEnumerable<cListSelected> ddlkontrakFDC { get; set; }


//        public IEnumerable<cListSelected> DDLNotarisID { get; set; }
//        public IEnumerable<cListSelected> DDLGIVFDC { get; set; }
//        public IEnumerable<cListSelected> DDLRCVFDC { get; set; }

//        public List<cContractsRev> ContractDetail { get; set; }
//        public List<cContractsOrderRegisRevList> DetailOrderRegisList { get; set; }

//        public cAccountDetail AccountDetail { get; set; }
//        public cAccountGroupUser AccountGroupUser { get; set; }
//        public cAccountMetrik AccountMetrik { get; set; }



//        public List<cRoya> RoyaList { get; set; }
//        public cFilterContractRev DetailFilter { get; set; }

//        public cRoya RoyaOneRow { get; set; }
//        public string securemoduleID { get; set; }

//        public string varJenisDocument { get; set; }
//        public string IdCaption { get; set; }


//        dbAccessHelper dbaccess = new dbAccessHelper();
//        string strconnection = CustomSecureData.DecryptionPass(ConfigurationManager.AppSettings["AppDB"].ToString());

//        public async Task<IEnumerable<cListSelected>> dbGetClientListByEncrypt()
//        {
//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_prm_client_list", null);

//            ddlClient = (from c in dt.AsEnumerable()
//                         select new cListSelected()
//                         {
//                             Value = CustomSecureData.Encryption(c.Field<String>("IDClient").ToString()),
//                             Text = c.Field<String>("Nama Client").ToString()
//                         }).ToList();

//            return ddlClient;
//        }

//        public async Task<IEnumerable<cListSelected>> dbGetDdlBranchList(string Kodecabang = "", string clientid = "")
//        {

//            clientid = CustomSecureData.Decryption(clientid);

//            SqlParameter[] sqlParam =
//           {
//                new SqlParameter ("@KodeCabang" ,Kodecabang),
//                new SqlParameter ("@ClientID",clientid)
//            };

//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_prm_branch_list", sqlParam);

//            ddlBranch = (from c in dt.AsEnumerable()
//                         select new cListSelected()
//                         {
//                             Text = c.Field<String>("NAMACABANG").ToString(),
//                             Value = c.Field<String>("KODECABANG").ToString()
//                         }).ToList();

//            return ddlBranch;
//        }

//        public async Task<IEnumerable<cListSelected>> dbGetDdlBranchListByEncrypt(string Kodecabang = "", string clientid = "")
//        {

//            clientid = CustomSecureData.Decryption(clientid);

//            SqlParameter[] sqlParam =
//           {
//                new SqlParameter ("@KodeCabang" ,Kodecabang),
//                new SqlParameter ("@ClientID",clientid)
//            };

//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_prm_branch_list", sqlParam);

//            ddlBranch = (from c in dt.AsEnumerable()
//                         select new cListSelected()
//                         {
//                             Text = c.Field<String>("NAMACABANG").ToString(),
//                             Value = CustomSecureData.Encryption(c.Field<String>("KODECABANG").ToString())
//                         }).ToList();

//            return ddlBranch;
//        }


//        public cFilterContractRev IntiFilter(string secidcaption, string secClientLogin, string secBranchLogin, string secNotaryLogin, int UserType)
//        {
//            cFilterContractRev modFilter = new cFilterContractRev();
//            modFilter.idcaption = secidcaption;
//            modFilter.NoPerjanjian = CustomSecureData.Encryption("");
//            modFilter.UserType = UserType;
//            modFilter.ClientLogin = secClientLogin;
//            modFilter.SelectClient = CustomSecureData.Encryption("");
//            modFilter.CabangLogin= secBranchLogin;
//            modFilter.SelectBranch = CustomSecureData.Encryption("");
//            modFilter.SelectContractStatus = "";
//            modFilter.NotaryLogin = secNotaryLogin;
//            modFilter.SelectNotaris = CustomSecureData.Encryption("");
//            modFilter.fromdate = "";
//            modFilter.todate = "";
//            modFilter.PageNumber = 1;
//            modFilter.isdownload = false;
//            modFilter.UserTypeApps = defineusertypeAPPs.GetDefineUserTypeAPPvsDB(secClientLogin, secBranchLogin, secNotaryLogin, UserType);

//            return modFilter;
//        }

//        public async Task<IEnumerable<cListSelected>> dbddlgetparamenumsList(string enumname)
//        {

//            SqlParameter[] sqlParam =
//         {
//                new SqlParameter ("@enumname",enumname),

//            };

//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_prm_enums_list", sqlParam);


//            IEnumerable<cListSelected> DDL = (from c in dt.AsEnumerable()
//                                              select new cListSelected()
//                                              {
//                                                  Text = c.Field<string>("EnumsDesc").ToString(),
//                                                  Value = c.Field<string>("EnumValue").ToString()
//                                              }).ToList();

//            return DDL;

//        }


//        public async Task<IEnumerable<cListSelected>> dbddlgetNotarisList(string IDNotaris = "", string Nama = "")
//        {

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@IDNotaris",IDNotaris),
//                new SqlParameter ("@Nama",Nama),

//            };
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_prm_notaris_list", sqlParam);


//            DDLNotarisID = (from c in dt.AsEnumerable()
//                            select new cListSelected()
//                            {
//                                Text = c.Field<string>("Nama Notaris").ToString(),
//                                Value = c.Field<string>("IDNotaris").ToString()
//                            }).ToList();

//            return DDLNotarisID;


//        }

//        public async Task<cRoya> dbGetKedudukanList(string IDNotaris)
//        {

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@IDNotaris",IDNotaris)
//            };
//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_prm_notaris_list", sqlParam);
//            try
//            {
//                RoyaOneRow = (from c in dt.AsEnumerable()
//                              select new cRoya()
//                              {
//                                  KedudukanNotaris = c.Field<string>("Wilayah Kerja")
//                              }).SingleOrDefault();
//            }
//            catch (Exception ex)
//            {
//                string mesg = ex.Message;
//            }
//            return RoyaOneRow;
//        }


//        public async Task<int> dbOrderRegisRevFidusiaSave(DataTable Dt, string clientID, string cabangid, string UserID, string GroupName)
//        {
//            string ClientIDS = CustomSecureData.Decryption(clientID);
//            cabangid = CustomSecureData.Decryption(cabangid);
//            SqlParameter[] sqlParam = {
//                        new SqlParameter("@tableorder",Dt),
//                        new SqlParameter ("@clientID",ClientIDS),
//                        new SqlParameter ("@cabangID",cabangid),
//                        new SqlParameter ("@UserIDLog",UserID),
//                        new SqlParameter ("@UserGroupLog",GroupName)
//                    };

//            int result = 0;

//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_rev_cabang_save", sqlParam);
//            result = int.Parse(dt.Rows[0][0].ToString());


//            return result;
//        }


//        public async Task<string> dbOrderRegisRevFidusiaCheck(DataTable Dt, string clientID, string cabangid, string UserID, string GroupName)
//        {
//            string ClientIDS = CustomSecureData.Decryption(clientID);
//            cabangid = CustomSecureData.Decryption(cabangid);
//            SqlParameter[] sqlParam = {
//                        new SqlParameter("@tableorder",Dt),
//                        new SqlParameter ("@clientID",ClientIDS),
//                        new SqlParameter ("@cabangID",cabangid),
//                        new SqlParameter ("@UserIDLog",UserID),
//                        new SqlParameter ("@UserGroupLog",GroupName)
//                    };

//            string result = "";

//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_rev_cabang_CheckAdendum", sqlParam);
//            result = (dt.Rows[0][0].ToString());


//            return result;
//        }

//        public async Task<List<String>> dbGetContractRevListCount(cFilterContractRev model, string userid, string groupname)
//        {

//            string ClientIDS = CustomSecureData.Decryption(model.ClientLogin);
//            ClientIDS = (ClientIDS == "" && (model.SelectClient ?? "") != "") ? CustomSecureData.Decryption(model.SelectClient) : ClientIDS;

//            string BranchIDS = CustomSecureData.Decryption(model.CabangLogin);
//            BranchIDS = (BranchIDS == "" && (model.SelectBranch ?? "") != "") ? CustomSecureData.Decryption(model.SelectBranch) : BranchIDS;

//            string NotaryIDS = CustomSecureData.Decryption(model.NotaryLogin);
//            NotaryIDS = (NotaryIDS == "" && (model.SelectNotaris ?? "") != "") ? CustomSecureData.Decryption(model.SelectNotaris) : NotaryIDS;

//            string NoPerjanjian = (model.NoPerjanjian ?? "") == "" ? "" : model.isModeFilter == true ? model.NoPerjanjian : CustomSecureData.Decryption(model.NoPerjanjian);
//            string idcaption = (model.idcaption ?? "") == "" ? "" : CustomSecureData.Decryption(model.idcaption);

//            string idpdc = (model.secIDFDC ?? "") == "" ? "0" : CustomSecureData.Decryption(model.secIDFDC);

//            string fromdate = model.fromdate ?? "";
//            string todate = model.todate ?? "";
//            string StatusContract = model.SelectContractStatus ?? "";
//            int PageNumber = model.PageNumber;
//            bool isdownload = model.isdownload;

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@idPDC",int.Parse(idpdc)),
//                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
//                new SqlParameter ("@status_Perjanjian",StatusContract),
//                new SqlParameter ("@kodecabang",BranchIDS),
//                new SqlParameter ("@ClientID",ClientIDS),
//                new SqlParameter ("@tgl_Perjanjianawal",fromdate),
//                new SqlParameter ("@tgl_Perjanjianakhir",todate),
//                new SqlParameter ("@moduleId",idcaption),
//                new SqlParameter ("@UserIDLog",userid),
//                new SqlParameter ("@UserGroupLog",groupname),
//                new SqlParameter ("@PageNumber",model.PageNumber),
//                new SqlParameter ("@isDownload",model.isdownload)
//            };
//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_Rev_listcount", sqlParam);

//            List<String> dta = new List<string>();
//            if (dt.Rows.Count > 0)
//            {
//                dta.Add(dt.Rows[0][0].ToString());
//                dta.Add(dt.Rows[0][1].ToString());
//            }
//            else
//            {
//                dta.Add("0");
//                dta.Add("0");
//            }

//            return dta;
//        }

//        public async Task<List<cContractsRev>> dbGetContractRevList(cFilterContractRev model, string userid, string groupname)
//        {


//            string ClientIDS = CustomSecureData.Decryption(model.ClientLogin);
//            ClientIDS = (ClientIDS == "" && (model.SelectClient ?? "") != "") ? CustomSecureData.Decryption(model.SelectClient) : ClientIDS;

//            string BranchIDS = CustomSecureData.Decryption(model.CabangLogin);
//            BranchIDS = (BranchIDS == "" && (model.SelectBranch ?? "") != "") ? CustomSecureData.Decryption(model.SelectBranch) : BranchIDS;

//            string NotaryIDS = CustomSecureData.Decryption(model.NotaryLogin);
//            NotaryIDS = (NotaryIDS == "" && (model.SelectNotaris ?? "") != "") ? CustomSecureData.Decryption(model.SelectNotaris) : NotaryIDS;

//            string NoPerjanjian = (model.NoPerjanjian ?? "") == "" ? "" : model.isModeFilter == true ? model.NoPerjanjian : CustomSecureData.Decryption(model.NoPerjanjian);
//            string idcaption = (model.idcaption ?? "") == "" ? "" : CustomSecureData.Decryption(model.idcaption);

//            string idpdc = (model.secIDFDC ?? "") == "" ? "0" : CustomSecureData.Decryption(model.secIDFDC);

//            string fromdate = model.fromdate ?? "";
//            string todate = model.todate ?? "";
//            string StatusContract = model.SelectContractStatus ?? "";
//            int PageNumber = model.PageNumber;
//            bool isdownload = model.isdownload;

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@idPDC",int.Parse(idpdc)),
//                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
//                new SqlParameter ("@status_Perjanjian",StatusContract),
//                new SqlParameter ("@kodecabang",BranchIDS),
//                new SqlParameter ("@ClientID",ClientIDS),
//                new SqlParameter ("@tgl_Perjanjianawal",fromdate),
//                new SqlParameter ("@tgl_Perjanjianakhir",todate),
//                new SqlParameter ("@moduleId",idcaption),
//                new SqlParameter ("@UserIDLog",userid),
//                new SqlParameter ("@UserGroupLog",groupname),
//                new SqlParameter ("@PageNumber",model.PageNumber),
//                new SqlParameter ("@isDownload",model.isdownload)
//            };
//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_Rev_list", sqlParam);
//            ContractDetail = new List<cContractsRev>();
//            if (dt.Rows.Count > 0)
//            {
//                ContractDetail = (from c in dt.AsEnumerable()
//                                  select new cContractsRev
//                                  {
//                                      secNO_PERJANJIAN = CustomSecureData.Encryption(c.Field<string>("NO_PERJANJIAN")),
//                                      secREFNO_PERJANJIAN = CustomSecureData.Encryption(c.Field<string>("Noref_Perjanjian")),
//                                      secSERTIFIKATE_NO = CustomSecureData.Encryption(c.Field<string>("SERTIFIKAT_NO")),
//                                      secID_FDC = CustomSecureData.Encryption(c.Field<int>("ID_FDC").ToString()),
//                                      TGL_PENGAJUAN = c.Field<DateTime>("TGL_Pengajuan"),
//                                      ID_FDC = c.Field<int>("ID_FDC"),
//                                      BRAN_BR_ID = c.Field<string>("BRAN_BR_ID"),
//                                      BRAN_BR_NAME = c.Field<string>("BRAN_BR_NAME"),
//                                      NASABAH_JENIS = c.Field<string>("NASABAH_JENIS"),
//                                      NAMA_CUST = c.Field<string>("NAMA_CUST"),
//                                      JENIS_KELAMIN = c.Field<string>("JENIS_KELAMIN"),
//                                      STATUS_PERNIKAHAN = c.Field<string>("STATUS_PERNIKAHAN"),
//                                      TEMPAT_LAHIR = c.Field<string>("TEMPAT_LAHIR"),
//                                      TGL_LAHIR = c.Field<DateTime?>("TGL_LAHIR"),
//                                      PEKERJAAN = c.Field<string>("PEKERJAAN"),
//                                      KEWARGANEGARAAN = c.Field<string>("KEWARGANEGARAAN"),
//                                      JENIS_IDENTITAS = c.Field<string>("JENIS_IDENTITAS"),
//                                      NO_IDENTITAS = c.Field<string>("NO_IDENTITAS"),
//                                      ALAMAT = c.Field<string>("ALAMAT"),
//                                      RT_RW = c.Field<string>("RT_RW"),
//                                      KELURAHAN = c.Field<string>("KELURAHAN"),
//                                      KECAMATAN = c.Field<string>("KECAMATAN"),
//                                      KOTA = c.Field<string>("KOTA"),
//                                      PROVINSI = c.Field<string>("PROVINSI"),
//                                      KODE_POS = c.Field<string>("KODE_POS"),
//                                      NAMA_DEBITUR = c.Field<string>("NAMA_DEBITUR"),
//                                      JENIS_PERJANJIAN = c.Field<string>("JENIS_PERJANJIAN"),
//                                      NAMA_PERJANJIAN = c.Field<string>("NAMA_PERJANJIAN"),
//                                      NO_PERJANJIAN = c.Field<string>("NO_PERJANJIAN"),
//                                      NOREF_PERJANJIAN = c.Field<string>("Noref_Perjanjian"),
//                                      TGL_PERJANJIAN = c.Field<DateTime?>("TGL_PERJANJIAN"),
//                                      NILAI_POKOK_HUTANG = c.Field<decimal>("NILAI_POKOK_HUTANG"),
//                                      NILAI_PENJAMINAN = c.Field<decimal>("NILAI_PENJAMINAN"),
//                                      TGL_AWAL_CICILAN = c.Field<DateTime?>("TGL_AWAL_CICILAN"),
//                                      PERIODE_TENOR = c.Field<int>("PERIODE_TENOR"),
//                                      KONDISI_KENDARAN = c.Field<string>("KONDISI_KENDARAN"),
//                                      MERK_KENDARAN = c.Field<string>("MERK_KENDARAN"),
//                                      TYPE_KENDARAN = c.Field<string>("TYPE_KENDARAN"),
//                                      WARNA_KENDARAN = c.Field<string>("WARNA_KENDARAN"),
//                                      TAHUN_PEMBUATAN = c.Field<string>("TAHUN_PEMBUATAN"),
//                                      TAHUN_PERAKITAN = c.Field<string>("TAHUN_PERAKITAN"),
//                                      NOMOR_RANGKA = c.Field<string>("NOMOR_RANGKA"),
//                                      NOMOR_MESIN = c.Field<string>("NOMOR_MESIN"),
//                                      NOMOR_BPKB = c.Field<string>("NOMOR_BPKB"),
//                                      NILAI_OBJECT = c.Field<decimal>("NILAI_OBJECT"),
//                                      NILAI_JASA = c.Field<decimal>("NILAI_JASA"),
//                                      NILAI_PNBP = c.Field<decimal>("NILAI_PNBP"),
//                                      NAMA_PENJAMIN = c.Field<string>("NAMA_PENJAMIN"),
//                                      JENIS_KELAMIN_PENJAMIN = c.Field<string>("JENIS_KELAMIN_PENJAMIN"),
//                                      STATUS_PENJAMIN = c.Field<string>("STATUS_PENJAMIN"),
//                                      ULANG_TAHUN_PENJAMIN = c.Field<DateTime?>("ULANG_TAHUN_PENJAMIN"),
//                                      ALAMAT_ULANG_TAHUN_PENJAMIN = c.Field<string>("ALAMAT_ULANG_TAHUN_PENJAMIN"),
//                                      JOB_PENJAMIN = c.Field<string>("JOB_PENJAMIN"),
//                                      KEWARGANEGARAAN_PENJAMIN = c.Field<string>("KEWARGANEGARAAN_PENJAMIN"),
//                                      JENIS_ID_PENJAMIN = c.Field<string>("JENIS_ID_PENJAMIN"),
//                                      JABATAN_PENJAMIN = c.Field<string>("JABATAN_PENJAMIN"),
//                                      ALAMAT_PENJAMIN = c.Field<string>("ALAMAT_PENJAMIN"),
//                                      RT_RW_PENJAMIN = c.Field<string>("RT_RW_PENJAMIN"),
//                                      KELUARAHAN_PENJAMIN = c.Field<string>("KELUARAHAN_PENJAMIN"),
//                                      KECAMATAN_PENJAMIN = c.Field<string>("KECAMATAN_PENJAMIN"),
//                                      KOTA_PENJAMIN = c.Field<string>("KOTA_PENJAMIN"),
//                                      PROVINSI_PENJAMIN = c.Field<string>("PROVINSI_PENJAMIN"),
//                                      AKTA_NO = c.Field<string>("AKTA_NO"),
//                                      TGL_AKTA = c.Field<DateTime?>("TGL_AKTA"),
//                                      SERTIFIKAT_NO = c.Field<string>("SERTIFIKAT_NO"),
//                                      TGL_SERTIFIKAT = c.Field<DateTime?>("TGL_SERTIFIKAT"),
//                                      NO_VOUCHER = c.Field<string>("NO_VOUCHER"),
//                                      NPWP_BADAN = c.Field<string>("NPWP_BADAN"),
//                                      NO_ID_PENJAMIN = c.Field<string>("NO_ID_PENJAMIN"),
//                                      STATUS_PERJANJIAN = c.Field<int>("STATUS_PERJANJIAN"),
//                                      STATUS_PERJANJIAN_DESC = c.Field<String>("STATUS_PERJANJIAN_DESC"),
//                                      STATUS_PERBAIKAN = c.Field<String>("STATUS_PERBAIKAN"),
//                                      STATUS_PERBAIKAN_DESC = c.Field<String>("STATUS_PERBAIKAN_DESC"),
//                                      NAMA_NOTARIS = c.Field<String>("NAMA_NOTARIS"),
//                                      STATUSDOCUMENT = c.Field<String>("STATUS_DOCUMENT"),
//                                      CONT_HISTORICAL_NOTES = c.Field<String>("CONT_HISTORICAL_NOTES"),
//                                      CONT_KRONOLOG_NOTES = c.Field<String>("CONT_KRONOLOG_NOTES")
//                                  }).ToList();

//            }
//            return ContractDetail;
//        }


//        public async Task<List<cContractsOrderRegisRevList>> dbGetListOrderRegisRev(DataTable Dt)
//        {

//            await Task.Delay(0);
//            List<cContractsOrderRegisRevList> ListGrid = (from c in Dt.AsEnumerable()
//                                                          select new cContractsOrderRegisRevList
//                                                          {

//                                                              Tanggal_Perjanjian = c.Field<string>("Tanggal_Perjanjian"),
//                                                              NoPerjanjian = c.Field<string>("NoPerjanjian"),

//                                                              ref_NoPerjanjian = c.Field<string>("ref_NoPerjanjian"),

//                                                              Jenis_Pelanggan = c.Field<string>("Jenis_Pelanggan"),
//                                                              Jenis_Pembiayaan = c.Field<string>("Jenis_Pembiayaan"),
//                                                              Jenis_Penggunaan = c.Field<string>("Jenis_Penggunaan") ?? "",


//                                                              Nama_Debitur = c.Field<string>("Nama_Debitur"),
//                                                              Jenis_Kelamin_Debitur = c.Field<string>("Jenis_Kelamin_Debitur") ?? "",
//                                                              No_Telp_HP_Debitur = c.Field<string>("No_Telp_HP_Debitur") ?? "",
//                                                              Jenis_Identitas_Debitur = c.Field<string>("Jenis_Identitas_Debitur"),
//                                                              No_KTP_NPWP_Debitur = c.Field<string>("No_KTP_NPWP_Debitur"),
//                                                              Tempat_Lahir_Debitur = c.Field<string>("Tempat_Lahir_Debitur") ?? "",
//                                                              Tanggal_Lahir_Debitur = c.Field<string>("Tanggal_Lahir_Debitur") ?? "",
//                                                              Pekerjaan_Debitur = c.Field<string>("Pekerjaan_Debitur") ?? "",
//                                                              Alamat_Debitur = c.Field<string>("Alamat_Debitur"),
//                                                              RT_Debitur = c.Field<string>("RT_Debitur") ?? "",
//                                                              RW_Debitur = c.Field<string>("RW_Debitur") ?? "",
//                                                              Kelurahan_Debitur = c.Field<string>("Kelurahan_Debitur"),
//                                                              Kecamatan_Debitur = c.Field<string>("Kecamatan_Debitur"),
//                                                              Kota_Debitur = c.Field<string>("Kota_Debitur"),
//                                                              KodePos_Debitur = c.Field<string>("KodePos_Debitur"),
//                                                              Provinsi_Debitur = c.Field<string>("Provinsi_Debitur"),



//                                                              Nama_BPKB = c.Field<string>("Nama_BPKB"),
//                                                              Jenis_Kelamin_BPKB = c.Field<string>("Jenis_Kelamin_BPKB") ?? "",
//                                                              No_Telp_HP_BPKB = c.Field<string>("No_Telp_HP_BPKB") ?? "",
//                                                              Jenis_Identitas_BPKB = c.Field<string>("Jenis_Identitas_BPKB"),
//                                                              No_KTP_NPWP_BPKB = c.Field<string>("No_KTP_NPWP_BPKB"),
//                                                              Tempat_Lahir_BPKB = c.Field<string>("Tempat_Lahir_BPKB") ?? "",
//                                                              Tanggal_Lahir_BPKB = c.Field<string>("Tanggal_Lahir_BPKB") ?? "",
//                                                              Pekerjaan_BPKB = c.Field<string>("Pekerjaan_BPKB") ?? "",
//                                                              Alamat_BPKB = c.Field<string>("Alamat_BPKB"),
//                                                              RT_BPKB = c.Field<string>("RT_BPKB") ?? "",
//                                                              RW_BPKB = c.Field<string>("RW_BPKB") ?? "",
//                                                              Kelurahan_BPKB = c.Field<string>("Kelurahan_BPKB"),
//                                                              Kecamatan_BPKB = c.Field<string>("Kecamatan_BPKB"),
//                                                              Kota_BPKB = c.Field<string>("Kota_BPKB"),
//                                                              KodePos_BPKB = c.Field<string>("KodePos_BPKB"),
//                                                              Provinsi_BPKB = c.Field<string>("Provinsi_BPKB"),



//                                                              Name_PIC_PT = c.Field<string>("Name_PIC_PT") ?? "",
//                                                              Jenis_Kelamin_PIC_PT = "",
//                                                              NoHp_PIC_PT = c.Field<string>("NoHp_PIC_PT") ?? "",
//                                                              KTP_PIC_PT = c.Field<string>("KTP_PIC_PT") ?? "",
//                                                              Jabatan_PIC_PT = c.Field<string>("Jabatan_PIC_PT") ?? "",
//                                                              Alamat_PIC_PT = c.Field<string>("Alamat_PIC_PT") ?? "",
//                                                              RT_PIC_PT = c.Field<string>("RT_PIC_PT") ?? "",
//                                                              RW_PIC_PT = c.Field<string>("RW_PIC_PT") ?? "",
//                                                              Kelurahan_PIC_PT = c.Field<string>("Kelurahan_PIC_PT") ?? "",
//                                                              Kecamatan_PIC_PT = c.Field<string>("Kecamatan_PIC_PT") ?? "",
//                                                              Kota_PIC_PT = c.Field<string>("Kota_PIC_PT") ?? "",
//                                                              KodePos_PIC_PT = c.Field<string>("KodePos_PIC_PT") ?? "",
//                                                              Provinsi_PIC_PT = c.Field<string>("Provinsi_PIC_PT") ?? "",

//                                                              Tanggal_akhir_angsuran = c.Field<string>("Tanggal_akhir_angsuran"),
//                                                              Tanggal_awal_angsuran = c.Field<string>("Tanggal_awal_angsuran"),

//                                                              Jenis_Object = c.Field<string>("Jenis_Object"),
//                                                              No_BPKB_Object_Bekas = c.Field<string>("No_BPKB_Object_Bekas") ?? "",
//                                                              Kondisi_Object = c.Field<string>("Kondisi_Object"),
//                                                              Jumlah_Roda = c.Field<double?>("Jumlah_Roda"),


//                                                              Nilai_PokokHutang = c.Field<double?>("Nilai_PokokHutang"),
//                                                              Nilai_Penjaminan = c.Field<double?>("Nilai_Penjaminan"),
//                                                              Nilai_Objek_Penjaminan = c.Field<double?>("Nilai_Objek_Penjaminan"),

//                                                              Merk = c.Field<string>("Merk"),
//                                                              Tipe_Kendaraan = c.Field<string>("Tipe_Kendaraan"),
//                                                              Warna = c.Field<string>("Warna"),
//                                                              NoRangka = c.Field<string>("NoRangka"),
//                                                              NoMesin = c.Field<string>("NoMesin"),
//                                                              TahunPembuatan = c.Field<double?>("TahunPembuatan"),

//                                                              Detail_Perbaikan_Perubahan = c.Field<string>("Detail_Perbaikan_Perubahan"),
//                                                              Histori_Perbaikan_Perubahan_Sertifikat = c.Field<string>("Histori_Perbaikan_Perubahan_Sertifikat"),



//                                                          }).ToList();

//            return ListGrid;
//        }


//        public async Task<string> dbDownloadRoya(cFilter modelFilter, String RequestAppPath = "")
//        {
//            await Task.Delay(1);

//            var stream = new MemoryStream();
//            string designerFile = RequestAppPath + "External\\TemplateExcel\\ReportRoya - New.xlsx";
//            var workbook = new Workbook(designerFile);
//            Aspose.Cells.License license = new Aspose.Cells.License();
//            license.SetLicense(RequestAppPath + "External\\Aspose.Cells.86109.lic");
//            XlsSaveOptions konfigurasi = new XlsSaveOptions(Aspose.Cells.SaveFormat.Xlsx);

//            Worksheet worksheet = workbook.Worksheets["Sheet1"];
//            worksheet.Name = "Report Roya";

//            Aspose.Cells.Style CellStyles = workbook.Styles[workbook.Styles.Add()];
//            CellStyles.Pattern = BackgroundType.Solid;

//            CellStyles.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
//            CellStyles.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
//            CellStyles.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
//            CellStyles.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
//            CellStyles.Font.IsBold = false;
//            CellStyles.Font.Color = Color.Black;
//            CellStyles.HorizontalAlignment = TextAlignmentType.Center;
//            CellStyles.Font.Size = 9;

//            worksheet.Cells[8, 2].Value = modelFilter.NoPerjanjian;
//            worksheet.Cells[9, 2].Value = modelFilter.NoSertifikat;
//            worksheet.Cells[10, 2].Value = modelFilter.SelectRoyaStatusDesc;
//            worksheet.Cells[10, 5].Value = String.Format("{0:dd-MMMM-yyyy}", modelFilter.fromdate) + " s/d " + String.Format("{0:dd-MMMM-yyyy}", modelFilter.todate);

//            worksheet.Cells[8, 5].Value = modelFilter.SelectClientDesc;
//            worksheet.Cells[9, 5].Value = modelFilter.SelectBranchDesc;

//            int startrow = 12;
//            for (var i = 0; i < RoyaList.Count; i++)
//            {
//                worksheet.Cells[i + startrow, 0].Value = i + 1;
//                worksheet.Cells[i + startrow, 0].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 1].Value = RoyaList[i].RoyaNumber;
//                worksheet.Cells[i + startrow, 1].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 2].Value = RoyaList[i].CreatedDate.ToString("dd-MMMM-yyyy").ToString();
//                worksheet.Cells[i + startrow, 2].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 3].Value = RoyaList[i].NoPerjanjian;
//                worksheet.Cells[i + startrow, 3].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 4].Value = RoyaList[i].NoSertifikat;
//                worksheet.Cells[i + startrow, 4].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 5].Value = RoyaList[i].NamaNotaris;
//                worksheet.Cells[i + startrow, 5].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 6].Value = String.Format("{0:dd-MMMM-yyyy}", RoyaList[i].TanggalPenghapusan).ToString();
//                worksheet.Cells[i + startrow, 6].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 7].Value = RoyaList[i].StatusDesc;
//                worksheet.Cells[i + startrow, 7].SetStyle(CellStyles);
//            }

//            //worksheet.Replace("[tglCetak]", "Print Date : " + DateTime.Now.ToString("dd-MMMM-yyyy"));
//            //worksheet.Replace("[Periode]", "Order Date : " + TanggalOrder);

//            workbook.Save(stream, konfigurasi);

//            return Convert.ToBase64String(stream.ToArray());
//        }

//    }
//}