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
//    public class vmRoya
//    {
//        public string DaftarCabang { get; set; }
//        public string JenisPenghapusan { get; set; }
//        public string PemberiFidusia { get; set; }

//        public cDouemnts RoyaDocumentOne { get; set; }
//        public string CheckWithKey { get; set; }

//        public IEnumerable<cListSelected> DDLRoyaCause { get; set; }
//        public IEnumerable<cListSelected> DDLRoyaType { get; set; }
//        public IEnumerable<cListSelected> DDLRoyaStatus { get; set; }
//        public IEnumerable<cListSelected> DDLRoyaClient { get; set; }
//        public IEnumerable<cListSelected> ddlBranch { get; set; }
//        public IEnumerable<cListSelected> ddlClient { get; set; }

//        public List<cRoyaList> DetailRoyaList { get; set; }

//        public DataTable DetailRoyaListDT { get; set; }

//        public DataTable DetailRoyaListNotExistsDT { get; set; }
//        public DataTable DetailRoyaListExistsDT { get; set; }
//        public DataTable DetailRoyaListDuplicateDT { get; set; }

//        public IEnumerable<cListSelected> ddlDocumentType { get; set; }

//        public int UserTypeApps { get; set; }

//        public string SelectClient { get; set; }

//        public IEnumerable<cListSelected> DDLNotarisID { get; set; }
//        public IEnumerable<cListSelected> DDLGIVFDC { get; set; }
//        public IEnumerable<cListSelected> DDLRCVFDC { get; set; }

//        public List<cContracts> ContractDetail { get; set; }

//        public cAccountDetail AccountDetail { get; set; }
//        public cAccountGroupUser AccountGroupUser { get; set; }
//        public cAccountMetrik AccountMetrik { get; set; }

//        public List<cRoya> RoyaList { get; set; }
//        public cFilter DetailFilter { get; set; }

//        public DataTable DTOrdersFromDB { get; set; }

//        public cRoya RoyaOneRow { get; set; }
//        public string securemoduleID { get; set; }

//        public string varJenisDocument { get; set; }
//        public string IdCaption { get; set; }


//        dbAccessHelper dbaccess = new dbAccessHelper();
//        string strconnection = CustomSecureData.DecryptionPass(ConfigurationManager.AppSettings["AppDB"].ToString());

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

//        public cFilter IntiFilter(string secidcaption, string secClientLogin, string secCabangLogin, string secNotarisLogin, int UserType)
//        {

//            cFilter modFilter = new cFilter();
//            modFilter.UserType = UserType;
//            modFilter.idcaption = secidcaption;
//            modFilter.ClientLogin = secClientLogin;
//            modFilter.CabangLogin = secCabangLogin;
//            modFilter.NotaryLogin = secNotarisLogin;
//            modFilter.SelectClient = CustomSecureData.Encryption("");
//            modFilter.SelectNotaris = CustomSecureData.Encryption("");
//            modFilter.SelectBranch = CustomSecureData.Encryption("");
//            modFilter.SelectContractStatus = "";
//            modFilter.fromdate = "";
//            modFilter.todate = "";
//            modFilter.PageNumber = 1;
//            modFilter.isdownload = false;
//            modFilter.UserTypeApps = defineusertypeAPPs.GetDefineUserTypeAPPvsDB(secClientLogin, secCabangLogin, secNotarisLogin, UserType);

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

//        public async Task<IEnumerable<cListSelected>> dbGetClientListByEncrypt()
//        {
//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_prm_client_list", null);

//            DDLRoyaClient = (from c in dt.AsEnumerable()
//                             select new cListSelected()
//                             {
//                                 Value = CustomSecureData.Encryption(c.Field<String>("IDClient").ToString()),
//                                 Text = c.Field<String>("Nama Client").ToString()
//                             }).ToList();

//            return DDLRoyaClient;
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

//        public async Task<List<string>> dbGetRoyaListCount(cFilter model, string userid, string groupuser)
//        {

//            string ClientIDS = CustomSecureData.Decryption(model.ClientLogin);
//            ClientIDS = (ClientIDS == "" && (model.SelectClient ?? "") != "") ? CustomSecureData.Decryption(model.SelectClient) : ClientIDS;

//            string BranchIDS = CustomSecureData.Decryption(model.CabangLogin);
//            BranchIDS = (BranchIDS == "" && (model.SelectBranch ?? "") != "") ? CustomSecureData.Decryption(model.SelectBranch) : BranchIDS;

//            string NotaryIDS = CustomSecureData.Decryption(model.NotaryLogin);
//            NotaryIDS = (NotaryIDS == "" && (model.SelectNotaris ?? "") != "") ? CustomSecureData.Decryption(model.SelectNotaris) : NotaryIDS;

//            string NoPerjanjian = (model.NoPerjanjian ?? "") == "" ? "" : model.isModeFilter == true ? model.NoPerjanjian : CustomSecureData.Decryption(model.NoPerjanjian);
//            string idcaption = (model.idcaption ?? "") == "" ? "" : CustomSecureData.Decryption(model.idcaption);


//            string fromdate = model.fromdate ?? "";
//            string NoSertifikat = model.NoSertifikat ?? "";
//            string todate = model.todate ?? "";
//            string SelectRoyaType = model.SelectRoyaType ?? "";
//            string SelectRoyaStatus = model.SelectRoyaStatus ?? "";
//            int PageNumber = model.PageNumber;
//            bool isdownload = model.isdownload;

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@noPerjanjian",NoPerjanjian),
//                new SqlParameter ("@NoSertifikat",NoSertifikat),
//                new SqlParameter ("@JenisData",SelectRoyaType),
//                new SqlParameter ("@Status",SelectRoyaStatus),
//                new SqlParameter ("@TglRequestFrom",fromdate),
//                new SqlParameter ("@TglRequestTo",todate),
//                new SqlParameter ("@ClientID",ClientIDS),
//                new SqlParameter ("@cabang",BranchIDS),
//                new SqlParameter ("@RoyaID",null),
//                new SqlParameter ("@UserIDLog",userid),
//                new SqlParameter ("@UserGroupLog",groupuser),
//                new SqlParameter("@PageNumber",model.PageNumber),
//                new SqlParameter("@isDownload",model.isdownload)
//            };

//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_listCount", sqlParam);

//            List<String> dta = new List<string>();
//            if (dt.Rows.Count > 0)
//            {
//                dta.Add(dt.Rows[0][0].ToString());
//                dta.Add(dt.Rows[0][1].ToString());
//                dta.Add(dt.Rows[0][2].ToString());
//            }
//            else
//            {
//                dta.Add("0");
//                dta.Add("0");
//                dta.Add("0");
//            }
//            return dta;
//        }


//        public async Task<List<cRoya>> dbGetRoyaList(DataTable DTFromDB, cFilter model, string userid, string groupuser)
//        {

//            string ClientIDS = CustomSecureData.Decryption(model.ClientLogin);
//            ClientIDS = (ClientIDS == "" && (model.SelectClient ?? "") != "") ? CustomSecureData.Decryption(model.SelectClient) : ClientIDS;

//            string BranchIDS = CustomSecureData.Decryption(model.CabangLogin);
//            BranchIDS = (BranchIDS == "" && (model.SelectBranch ?? "") != "") ? CustomSecureData.Decryption(model.SelectBranch) : BranchIDS;

//            string NotaryIDS = CustomSecureData.Decryption(model.NotaryLogin);
//            NotaryIDS = (NotaryIDS == "" && (model.SelectNotaris ?? "") != "") ? CustomSecureData.Decryption(model.SelectNotaris) : NotaryIDS;

//            string NoPerjanjian = (model.NoPerjanjian ?? "") == "" ? "" : model.isModeFilter == true ? model.NoPerjanjian : CustomSecureData.Decryption(model.NoPerjanjian);
//            string idcaption = (model.idcaption ?? "") == "" ? "" : CustomSecureData.Decryption(model.idcaption);


//            string fromdate = model.fromdate ?? "";
//            string NoSertifikat = model.NoSertifikat ?? "";
//            string todate = model.todate ?? "";
//            string SelectRoyaType = model.SelectRoyaType ?? "";
//            string SelectRoyaStatus = model.SelectRoyaStatus ?? "";
//            int PageNumber = model.PageNumber;
//            bool isdownload = model.isdownload;

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@noPerjanjian",NoPerjanjian),
//                new SqlParameter ("@NoSertifikat",NoSertifikat),
//                new SqlParameter ("@JenisData",SelectRoyaType),
//                new SqlParameter ("@Status",SelectRoyaStatus),
//                new SqlParameter ("@TglRequestFrom",fromdate),
//                new SqlParameter ("@TglRequestTo",todate),
//                new SqlParameter ("@ClientID",ClientIDS),
//                new SqlParameter ("@cabang",BranchIDS),
//                new SqlParameter ("@RoyaID",null),
//                new SqlParameter ("@UserIDLog",userid),
//                new SqlParameter ("@UserGroupLog",groupuser),
//                new SqlParameter("@PageNumber",model.PageNumber),
//                new SqlParameter("@isDownload",model.isdownload)
//            };

//            //await Task.Delay(500);

//            DataTable dt = new DataTable();
//            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
//            {
//                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_list", sqlParam);
//                DTOrdersFromDB = dt;

//            }
//            else
//            {
//                dt = DTFromDB;

//            }

//            int starrow = (int.Parse(model.pagenumberclient.ToString()) - 1) * int.Parse(model.pagingsizeclient.ToString());

//            RoyaList = new List<cRoya>();
//            if (dt.Rows.Count > 0)
//            {
//                dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(model.pagingsizeclient.ToString())).CopyToDataTable();
//                RoyaList = (from c in dt.AsEnumerable()
//                            select new cRoya()
//                            {
//                                KeyLookupdata = HashNetFramework.HasKeyProtect.GenerateOTP(),
//                                secID_FDC = (c.Field<int>("CLIENT_FDC_ID").ToString()), //CustomSecureData.Encryption
//                                SecureRoyaID = (c.Field<int>("RoyaId").ToString()), //CustomSecureData.Encryption
//                                secureSertifikate = (c.Field<string>("NoSertifikat").ToString()), //CustomSecureData.Encryption
//                                secureContractNo = (c.Field<string>("NoPerjanjian").ToString()),//CustomSecureData.Encryption
//                                RoyaType = c.Field<string>("JenisRoya").ToString(),
//                                TanggalRequestRoya = c.Field<DateTime>("CreatedDate"),
//                                NoPerjanjian = c.Field<string>("NoPerjanjian"),
//                                NamaNotaris = c.Field<string>("NamaNotaris"),
//                                NoSertifikat = c.Field<string>("NoSertifikat").ToString(),
//                                TanggalSertifikat = c.Field<DateTime>("TanggalSertifikat"),
//                                NoRoyaSertifikat = c.Field<string>("NoRoyaSertifikat").ToString(),
//                                TanggalRoyaSertifikat = c.Field<DateTime?>("TanggalRoyaSertifikat"),
//                                TanggalPenghapusan = c.Field<DateTime?>("TanggalPenghapusan"),
//                                CreatedDate = c.Field<DateTime>("CreatedDate"),
//                                Status = c.Field<int>("StatusRequest"),
//                                StatusDesc = c.Field<string>("StatusRequestDesc"),
//                            }).ToList();
//            }
//            return RoyaList;
//        }



//        public async Task<DataTable> dbRoyaFidusiaCheck(DataTable Dt, string clientID, string clientIDselect, string cabangid, string emailID, string UserID, string GroupName)
//        {
//            string ClientIDS = CustomSecureData.Decryption(clientID);
//            ClientIDS = (ClientIDS ?? "")=="" ? CustomSecureData.Decryption(clientIDselect) : ClientIDS;

//            cabangid = CustomSecureData.Decryption(cabangid);
//            SqlParameter[] sqlParam = {
//                        new SqlParameter("@tableorder",Dt),
//                        new SqlParameter ("@clientID",ClientIDS),
//                        new SqlParameter ("@cabangID",cabangid),
//                        new SqlParameter ("@emailID",emailID),
//                        new SqlParameter ("@UserIDLog",UserID),
//                        new SqlParameter ("@UserGroupLog",GroupName)
//                    };

//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_cabang_CheckDuplicate", sqlParam);

//            return dt;
//        }

//        public async Task<int> dbRoyaFidusiaSave(DataTable Dt, string clientID,string clientIDselect, string cabangid, string EmailID, string templatename, string ModuleID, string UserID, string GroupName)
//        {
//            string ClientIDS = CustomSecureData.Decryption(clientID);
//            ClientIDS = (ClientIDS ?? "") == "" ? CustomSecureData.Decryption(clientIDselect) : ClientIDS;

//            cabangid = CustomSecureData.Decryption(cabangid);
//            SqlParameter[] sqlParam = {
//                        new SqlParameter("@tableorder",Dt),
//                        new SqlParameter ("@clientID",ClientIDS),
//                        new SqlParameter ("@cabangID",cabangid),
//                        new SqlParameter ("@EmailID",EmailID),
//                        new SqlParameter ("@templatename",templatename),
//                        new SqlParameter ("@ModuleID",ModuleID),
//                        new SqlParameter ("@UserIDLog",UserID),
//                        new SqlParameter ("@UserGroupLog",GroupName)
//                    };

//            int result = 0;

//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_cabang_save", sqlParam);
//            result = int.Parse(dt.Rows[0][0].ToString());


//            return result;
//        }



//        public async Task<List<cRoyaList>> dbGetListRoya(DataTable Dt)
//        {

//            await Task.Delay(0);
//            List<cRoyaList> ListGrid = (from c in Dt.AsEnumerable()
//                                        select new cRoyaList
//                                        {

//                                            //NO_AGREEMENT = c.Field<string>("NO_AGREEMENT"),
//                                            //JENIS_ROYA = c.Field<string>("JENIS_ROYA"),

//                                            //TANGGAL_PELUNASAN = c.Field<string>("TANGGAL_PELUNASAN"),
//                                            //TANGGAL_SERTIFIKAT = c.Field<string>("TANGGAL_SERTIFIKAT"),
//                                            //NO_SERTIFIKAT = c.Field<string>("NO_SERTIFIKAT"),

//                                            //NO_AKTA = c.Field<string>("NO_AKTA"),
//                                            //TANGGAL_AKTA = c.Field<string>("TANGGAL_AKTA"),

//                                            Nama_PemberiFidusia = c.Field<string>("NAMA_CUSTOMER"),
//                                            Identitas_PemberiFidusia = c.Field<string>("NO_KTP_NPWP"),
//                                            ContactNo_PemberiFidusia = c.Field<string>("NO_KONTAK"),
//                                            Alamat_PemberiFidusia = c.Field<string>("ALAMAT"),
//                                            KodePos_PemberiFidusia = c.Field<string>("KODEPOS"),
//                                            Kota_PemberiFidusia = c.Field<string>("KOTA"),
//                                            Kecamatan_PemberiFidusia = c.Field<string>("KECAMATAN"),
//                                            Kelurahan_PemberiFidusia = c.Field<string>("KELURAHAN"),
//                                            RT_PemberiFidusia = c.Field<string>("RT"),
//                                            RW_PemberiFidusia = c.Field<string>("RW"),

//                                            Amount_PokokHutang = c.Field<string>("NILAI_POKOKHUTANG"),
//                                            Amount_Penjamin = c.Field<string>("NILAI_PENJAMINAN"),
//                                            Amount_OTR = c.Field<string>("NILAI_OTR"),

//                                            Nama_Notaris = c.Field<string>("NAMA_NOTARIS") ?? "",
//                                            Fidusia_Online_ID = c.Field<string>("FIDUSIA_ONLINE_ID") ?? "",

//                                        }).ToList();

//            return ListGrid;
//        }



//        public async Task<List<cRoya>> dbGetRoyaListbyID(string royaid, string ClientID, string ClientIDSelect)
//        {

//            int IdRoya = int.Parse(CustomSecureData.Decryption(royaid));
//            string ClientIDs = CustomSecureData.Decryption(ClientID);
//            ClientIDs = ClientIDs == "" ? CustomSecureData.Decryption(ClientIDSelect) : ClientIDs;

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@RoyaID",IdRoya),
//                new SqlParameter ("@ClientID",ClientIDs)
//            };

//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_GetIdlist", sqlParam);

//            RoyaList = (from c in dt.AsEnumerable()
//                        select new cRoya()
//                        {
//                            SecureRoyaID = CustomSecureData.Encryption(c.Field<int>("RoyaId").ToString()),
//                            RoyaNumber = c.Field<string>("RoyaNumber"),
//                            RoyaCause = c.Field<string>("RoyaCause"),
//                            RoyaType = c.Field<string>("RoyaType"),
//                            NoPerjanjian = c.Field<string>("NoPerjanjian"),
//                            NamaNotaris = c.Field<string>("NamaNotaris"),
//                            NoSertifikat = c.Field<string>("NoSertifikat").ToString(),
//                            TanggalSertifikat = c.Field<DateTime>("TglTanggalSertifikat"),
//                            TanggalPenghapusan = c.Field<DateTime>("TanggalPenghapusan"),
//                            Status = c.Field<int>("StatusRequest"),
//                            JenisPemberi = c.Field<string>("JenisPemberi"),
//                            NamaPemberi = c.Field<string>("NamaPemberi"),
//                            NPWP_SK_NIKPemberi = c.Field<string>("NPWP_SK_NIKPemberi"),
//                            ContactNumberPemberi = c.Field<string>("ContactNumberPemberi"),
//                            AlamatPemberi = c.Field<string>("AlamatPemberi"),
//                            KodePosPemberi = c.Field<string>("KodePosPemberi"),
//                            KabupatenPemberi = c.Field<string>("KabupatenPemberi"),
//                            KecamatanPemberi = c.Field<string>("KecamatanPemberi"),
//                            KelurahanPemberi = c.Field<string>("KelurahanPemberi"),
//                            RTPemberi = c.Field<string>("RTPemberi"),
//                            RWPemberi = c.Field<string>("RWPemberi"),
//                            JenisPenerima = c.Field<string>("JenisPenerima"),
//                            NamaPenerima = c.Field<string>("NamaPenerima"),
//                            NPWP_SK_NIKPenerima = c.Field<string>("NPWP_SK_NIKPenerima"),
//                            ContactNumberPenerima = c.Field<string>("ContactNumberPenerima"),
//                            AlamatPenerima = c.Field<string>("AlamatPenerima"),
//                            KodePosPenerima = c.Field<string>("KodePosPenerima"),
//                            KabupatenPenerima = c.Field<string>("KabupatenPenerima"),
//                            KecamatanPenerima = c.Field<string>("KecamatanPenerima"),
//                            KelurahanPenerima = c.Field<string>("KelurahanPenerima"),
//                            RTPenerima = c.Field<string>("RTPenerima"),
//                            RWPenerima = c.Field<string>("RWPenerima"),
//                            NilaiHutang = c.Field<decimal>("NilaiHutang"),
//                            NilaiPenjaminan = c.Field<decimal>("NilaiPenjaminan"),
//                            NoAkta = c.Field<string>("NoAkta"),
//                            TanggalAkta = c.Field<DateTime>("TanggalAkta"),
//                            NamaNotarisakta = c.Field<string>("NotarisPenjamin"),
//                            KedudukanNotarisakta = c.Field<string>("KedudukanPenjamin")

//                        }).ToList();

//            return RoyaList;
//        }

//        public async Task<int> dbSaveRoya(vmRoya model)
//        {

//            string NoSertifikat = model.RoyaOneRow.NoSertifikat ?? model.RoyaOneRow.NoSertifikatLama;
//            string NoPerjanjian = model.RoyaOneRow.NoPerjanjian ?? "";
//            DateTime TanggalSertifikat = model.RoyaOneRow.TanggalSertifikat.Equals(DateTime.MinValue) ? model.RoyaOneRow.TanggalSertifikatLama : model.RoyaOneRow.TanggalSertifikat;
//            string NamaNotaris = model.RoyaOneRow.NamaNotaris ?? model.RoyaOneRow.NamaNotarisLama ?? "";
//            string KedudukanNotaris = model.RoyaOneRow.KedudukanNotaris ?? model.RoyaOneRow.KedudukanNotarisLama;
//            string WaktuSertifikat = model.RoyaOneRow.WaktuSertifikat ?? model.RoyaOneRow.WaktuSertifikatLama;
//            string TanggalSertifikatStr = TanggalSertifikat.ToString("dd-MMMM-yyyy");
//            string TanggalAktaStr = model.RoyaOneRow.TanggalAkta.ToString("dd-MMMM-yyyy");
//            string TanggalHapusStr = String.Format("{0:dd-MMMM-yyyy}", model.RoyaOneRow.TanggalPenghapusan);

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@JenisPenghapusan",model.RoyaOneRow.RoyaCause),
//                new SqlParameter ("@JenisData",model.RoyaOneRow.RoyaType),
//                new SqlParameter ("@NoPerjanjian",NoPerjanjian),
//                new SqlParameter ("@TanggalPenghapusan",TanggalHapusStr),
//                new SqlParameter ("@ClientID",CustomSecureData.Decryption(model.AccountDetail.ClientID)),
//                new SqlParameter ("@UserIDLog",model.AccountDetail.UserID),
//                new SqlParameter ("@UserGroupLog",model.AccountGroupUser.GroupName)
//            };

//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_save", sqlParam);
//            int result = int.Parse(dt.Rows[0][0].ToString());

//            return result;
//        }

//        public async Task<int> dbRoyaDoneUpload(string ClientID, string ClientIDSelect, string secureContractNo, string secureSertifikate, string UserID, string GroupName, string idcaption)
//        {

//            string decsecureContractNo = CustomSecureData.Decryption(secureContractNo);
//            string decsecureSertifikate = CustomSecureData.Decryption(secureSertifikate);

//            string ClientIDs = CustomSecureData.Decryption(ClientID);
//            ClientIDs = ClientIDs == "" ? CustomSecureData.Decryption(ClientIDSelect) : ClientIDs;


//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@moduleid",idcaption ?? ""),
//                new SqlParameter ("@noperjanjian",decsecureContractNo),
//                new SqlParameter ("@sertifikate ", decsecureSertifikate),
//                new SqlParameter ("@ClientID",ClientIDs),
//                new SqlParameter ("@UserIDLog",UserID),
//                new SqlParameter ("@UserGroupLog",GroupName),

//            };

//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_upload_upt", sqlParam);
//            int result = int.Parse(dt.Rows[0][0].ToString());

//            return result;

//        }

//        public async Task<int> dbRoyaCancel(string RoyaID, string userid, string GroupUser, string ClientID, string ClientIDSelect)
//        {
//            int iRoyaID = int.Parse(CustomSecureData.Decryption(RoyaID));
//            string ClientIDs = CustomSecureData.Decryption(ClientID);
//            ClientIDs = ClientIDs == "" ? CustomSecureData.Decryption(ClientIDSelect) : ClientIDs;

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@ID",iRoyaID),
//                new SqlParameter ("@ClientID",ClientIDs),
//                new SqlParameter ("@UserIDLog",userid),
//                new SqlParameter ("@UserGroupLog",GroupUser),
//                new SqlParameter ("@success",SqlDbType.Int)
//            };
//            sqlParam[4].Direction = ParameterDirection.Output;

//            //await Task.Delay(500);
//            SqlCommand commond = await dbaccess.ExecuteNonQueryWithOutput(strconnection, "udp_app_contracts_roya_cancel", sqlParam);

//            int result = int.Parse(commond.Parameters[4].Value.ToString());

//            return result;

//        }

//        public async Task<cDouemnts> dbGetDocumentByID(string ClientID, string ClientIDSelect, string codeidimg, string UserLog, string UserGroup, string securemoduleID)
//        {

//            int attcmentid = int.Parse(CustomSecureData.Decryption(codeidimg));
//            string moduleID = CustomSecureData.Decryption(securemoduleID);

//            string ClientIDs = CustomSecureData.Decryption(ClientID);
//            ClientIDs = ClientIDs == "" ? CustomSecureData.Decryption(ClientIDSelect) : ClientIDs;

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@ID",attcmentid),
//                new SqlParameter("@moduleid", moduleID),
//                new SqlParameter ("@ClientID",ClientIDs),
//                new SqlParameter("@UserIDLog", UserLog),
//                new SqlParameter("@UserGroupLog", UserGroup)
//            };

//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_upload_get", sqlParam);

//            RoyaDocumentOne = new cDouemnts();
//            RoyaDocumentOne = (from c in dt.AsEnumerable()
//                               select new cDouemnts()
//                               {
//                                   Result = c.Field<int>("Result"),
//                                   JENIS_DOCUMENT = c.Field<string>("JENIS_DOCUMENT").ToString(),
//                                   NO_PERJANJIAN = c.Field<string>("NO_PERJANJIAN").ToString(),
//                                   FILE_NAME = c.Field<string>("FILE_NAME").ToString(),
//                                   FILE_BYTE = c.Field<Byte[]>("FILE_BYTE"),
//                                   CONTENT_TYPE = c.Field<string>("CONTENT_TYPE").ToString(),
//                                   CONTENT_LENGTH = c.Field<decimal>("CONTENT_LENGTH"),
//                                   USERID = c.Field<string>("USERID").ToString(),
//                               }).SingleOrDefault();

//            return RoyaDocumentOne;
//        }

//        public async Task<List<cContracts>> dbGetContractCertificateInfo(string NoPerjanjian, string clientID, string cabang, string UserLog, string UserGroup, string securemoduleID)
//        {

//            string moduleID = CustomSecureData.Decryption(securemoduleID);
//            cabang = CustomSecureData.Decryption(cabang);
//            string ClientIDs = CustomSecureData.Decryption(clientID);

//            SqlParameter[] sqlParam =
//            {
//                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
//                 new SqlParameter("@moduleid", moduleID),
//                new SqlParameter ("@clientID",ClientIDs),
//                new SqlParameter ("@cabang",cabang),
//                new SqlParameter("@UserIDLog", UserLog),
//                new SqlParameter("@UserGroupLog", UserGroup)
//            };

//            //await Task.Delay(500);
//            DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_contracts_roya_certificateinfo_get", sqlParam);
//            ContractDetail = new List<cContracts>();
//            if (dt.Rows.Count > 0)
//            {
//                ContractDetail = (from c in dt.AsEnumerable()
//                                  select new cContracts
//                                  {
//                                      NASABAH_JENIS = c.Field<string>("NASABAH_JENIS"),
//                                      NAMA_CUST = c.Field<string>("NAMA_CUST"),
//                                      JENIS_IDENTITAS = c.Field<string>("JENIS_IDENTITAS"),
//                                      NO_IDENTITAS = c.Field<string>("NO_IDENTITAS"),
//                                      ALAMAT = c.Field<string>("ALAMAT"),
//                                      RT_RW = c.Field<string>("RT_RW"),
//                                      RT = c.Field<string>("RT"),
//                                      RW = c.Field<string>("RW"),
//                                      KELURAHAN = c.Field<string>("KELURAHAN"),
//                                      KECAMATAN = c.Field<string>("KECAMATAN"),
//                                      KABUPATEN = c.Field<string>("KABUPATEN"),
//                                      KOTA = c.Field<string>("KOTA"),
//                                      KABUPATEN_KOTA = c.Field<string>("KABUPATEN_KOTA"),
//                                      PROVINSI = c.Field<string>("PROVINSI"),
//                                      KODE_POS = c.Field<string>("KODE_POS"),
//                                      NO_CONTACT = c.Field<string>("NO_CONTACT"),
//                                      NAMA_DEBITUR = c.Field<string>("NAMA_DEBITUR"),
//                                      NO_PERJANJIAN = c.Field<string>("NO_PERJANJIAN"),
//                                      TGL_PERJANJIAN = c.Field<DateTime?>("TGL_PERJANJIAN"),
//                                      NILAI_POKOK_HUTANG = c.Field<decimal>("NILAI_POKOK_HUTANG"),
//                                      NILAI_PENJAMINAN = c.Field<decimal>("NILAI_PENJAMINAN"),
//                                      AKTA_NO = c.Field<string>("AKTA_NO"),
//                                      TGL_AKTA = c.Field<DateTime?>("TGL_AKTA"),
//                                      PUKUL_AKTA = c.Field<TimeSpan>("PUKUL_AKTA"),
//                                      SERTIFIKAT_NO = c.Field<string>("SERTIFIKAT_NO"),
//                                      TGL_SERTIFIKAT = c.Field<DateTime?>("TGL_SERTIFIKAT"),
//                                      NAMA_NOTARIS = c.Field<String>("NAMA_NOTARIS"),
//                                      WILAYAH_KERJA = c.Field<String>("WILAYAH_KERJA"),
//                                  }).ToList();

//            }
//            return ContractDetail;
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
//            worksheet.Name = "Roya Fidusia";

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

//            worksheet.Cells[2, 2].Value = modelFilter.SelectClientDesc;
//            worksheet.Cells[3, 2].Value = modelFilter.SelectBranchDesc;
//            worksheet.Cells[4, 2].Value = modelFilter.NoPerjanjian;
//            worksheet.Cells[5, 2].Value = modelFilter.NoSertifikat;
//            worksheet.Cells[6, 2].Value = String.Format("{0:dd-MMMM-yyyy}", modelFilter.fromdate) + " s/d " + String.Format("{0:dd-MMMM-yyyy}", modelFilter.todate);
//            worksheet.Cells[7, 2].Value = modelFilter.SelectRoyaStatusDesc;


//            int startrow = 9;
//            for (var i = 0; i < RoyaList.Count; i++)
//            {
//                worksheet.Cells[i + startrow, 0].Value = i + 1;
//                worksheet.Cells[i + startrow, 0].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 1].Value = RoyaList[i].NoPerjanjian;
//                worksheet.Cells[i + startrow, 1].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 2].Value = RoyaList[i].CreatedDate.ToString("dd-MMMM-yyyy").ToString();
//                worksheet.Cells[i + startrow, 2].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 3].Value = RoyaList[i].NoSertifikat;
//                worksheet.Cells[i + startrow, 3].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 4].Value = RoyaList[i].NamaNotaris;
//                worksheet.Cells[i + startrow, 4].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 5].Value = RoyaList[i].NoRoyaSertifikat;
//                worksheet.Cells[i + startrow, 5].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 6].Value = RoyaList[i].RoyaType;
//                worksheet.Cells[i + startrow, 6].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 7].Value = String.Format("{0:dd-MMMM-yyyy}", RoyaList[i].TanggalPenghapusan).ToString();
//                worksheet.Cells[i + startrow, 7].SetStyle(CellStyles);
//                worksheet.Cells[i + startrow, 8].Value = RoyaList[i].StatusDesc;
//                worksheet.Cells[i + startrow, 8].SetStyle(CellStyles);
//            }

//            //worksheet.Replace("[tglCetak]", "Print Date : " + DateTime.Now.ToString("dd-MMMM-yyyy"));
//            //worksheet.Replace("[Periode]", "Order Date : " + TanggalOrder);

//            workbook.Save(stream, konfigurasi);

//            return Convert.ToBase64String(stream.ToArray());
//        }

//    }
//}