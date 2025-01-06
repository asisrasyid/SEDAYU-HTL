using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DusColl
{
    [Serializable]
    public class vmAccount
    {
        public string AccountMenu { get; set; }
        public string AccountCaption { get; set; }
        public string SelectGrupAkses { get; set; }
        public cAccount AccountLogin { get; set; }
        public DataTable AccountTodo { get; set; }
        public DataTable AccountTodoNOT { get; set; }
        public DataTable AccountTodoCAB { get; set; }
        public DataTable AccountTodoADM { get; set; }

        public List<cAccountGroupUser> AccountGroupUserList { get; set; }
        public List<cAccountMetrik> AccountMetrikList { get; set; }
        public bool ForcePass { get; set; } = false;
        public int dashboardtipe { get; set; }

        public cFilterContract FilterTransaksi { get; set; }
        public DataTable DTHeaderTx { get; set; }
        public DataTable DTDetailTx { get; set; }
        public DataTable DTAllTx { get; set; }
        public cAccountRegis HeaderInfo { get; set; }
        public DataTable DTAllLog { get; set; }
        public DataTable DTLogTx { get; set; }
        public cAccountMetrik Permission { get; set; }
    }

    [Serializable]
    public class vmAccountddl
    {
        //private string DbconnectionString = @"Data Source=192.168.201.247;User Id=Azis;Password=P@ss1234;Database=HTL";
        public async Task<cAccount> dbAuthenticateUser(cAccount models)
        {
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                //string strconnection = DbconnectionString;

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@UserID",models.UserID),
                    new SqlParameter ("@UserPassword",models.codepass),
                    new SqlParameter ("@useremail",models.email),
                    new SqlParameter ("@templatename",models.template),
                    new SqlParameter ("@ModuleID",models.template),
                    new SqlParameter ("@validveridikasicoce",models.validverikasicode),

                    new SqlParameter ("@AuthenProperty",SqlDbType.Int),

                    new SqlParameter ("@mailed",SqlDbType.VarChar,150),
                    new SqlParameter ("@ClientID",SqlDbType.VarChar,30),
                    new SqlParameter ("@IDCabang",SqlDbType.VarChar,30),
                    new SqlParameter ("@IDNotaris",SqlDbType.VarChar,30),
                    new SqlParameter ("@ClientName",SqlDbType.VarChar,130),
                    new SqlParameter ("@CabangName",SqlDbType.VarChar,130),
                    new SqlParameter ("@NotarisName",SqlDbType.VarChar,-1),
                    new SqlParameter ("@UserName",SqlDbType.VarChar,130),
                    new SqlParameter ("@GenMoon",SqlDbType.VarChar,130),
                    new SqlParameter ("@UserType",SqlDbType.Int),
                    new SqlParameter ("@DivisiName",SqlDbType.VarChar,130),
                    new SqlParameter ("@IDDivisi",SqlDbType.VarChar,130),
                    new SqlParameter ("@IDRegion",SqlDbType.VarChar,130),
                    new SqlParameter ("@RegionName",SqlDbType.VarChar,130),

                    new SqlParameter ("@Address",SqlDbType.VarChar,130),
                    new SqlParameter ("@TglLAHIR",SqlDbType.VarChar,130),
                    new SqlParameter ("@TptLAHIR",SqlDbType.VarChar,130),
                    new SqlParameter ("@NIK",SqlDbType.VarChar,130),
                    new SqlParameter ("@Kota",SqlDbType.VarChar,130),
                    new SqlParameter ("@Domisili",SqlDbType.VarChar,130),
                    new SqlParameter ("@NamaNotaris",SqlDbType.VarChar,130),
                    new SqlParameter ("@Wilayah",SqlDbType.VarChar,130),
                    new SqlParameter ("@IDBPN",SqlDbType.VarChar,130),

                    new SqlParameter ("@Jabatan",SqlDbType.VarChar,130),
                    new SqlParameter ("@Pekerjaan",SqlDbType.VarChar,130),
                    new SqlParameter ("@HP",SqlDbType.VarChar,130),

                    new SqlParameter ("@NoSK",SqlDbType.VarChar,130),
                    new SqlParameter ("@TglSK",SqlDbType.VarChar,130),

                    new SqlParameter ("@Photo",SqlDbType.VarChar,-1),

                    new SqlParameter ("@tdform",SqlDbType.VarChar,-1),
                    new SqlParameter ("@tdsk",SqlDbType.VarChar,-1),
                    new SqlParameter ("@tdabs",SqlDbType.VarChar,-1),

                    new SqlParameter ("@JKelamin",SqlDbType.VarChar,20),

                    /*
                    new SqlParameter ("@topform",SqlDbType.VarChar,5),
                    new SqlParameter ("@leftform",SqlDbType.VarChar,5),

                    new SqlParameter ("@topsk",SqlDbType.VarChar,5),
                    new SqlParameter ("@leftsk",SqlDbType.VarChar,5),

                    new SqlParameter ("@topab",SqlDbType.VarChar,5),
                    new SqlParameter ("@leftab",SqlDbType.VarChar,5),

                    new SqlParameter ("@docform",SqlDbType.VarChar,-1),
                    new SqlParameter ("@docsk",SqlDbType.VarChar,-1),
                    new SqlParameter ("@docab",SqlDbType.VarChar,-1),

                    new SqlParameter ("@nosk",SqlDbType.VarChar,50),
                    new SqlParameter ("@tglsk",SqlDbType.VarChar,50),

                    */
                };

                sqlParam[6].Direction = ParameterDirection.Output;
                sqlParam[7].Direction = ParameterDirection.Output;
                sqlParam[8].Direction = ParameterDirection.Output;
                sqlParam[9].Direction = ParameterDirection.Output;
                sqlParam[10].Direction = ParameterDirection.Output;
                sqlParam[11].Direction = ParameterDirection.Output;
                sqlParam[12].Direction = ParameterDirection.Output;
                sqlParam[13].Direction = ParameterDirection.Output;
                sqlParam[14].Direction = ParameterDirection.Output;
                sqlParam[15].Direction = ParameterDirection.Output;
                sqlParam[16].Direction = ParameterDirection.Output;

                sqlParam[17].Direction = ParameterDirection.Output;
                sqlParam[18].Direction = ParameterDirection.Output;
                sqlParam[19].Direction = ParameterDirection.Output;
                sqlParam[20].Direction = ParameterDirection.Output;

                sqlParam[21].Direction = ParameterDirection.Output;
                sqlParam[22].Direction = ParameterDirection.Output;
                sqlParam[23].Direction = ParameterDirection.Output;
                sqlParam[24].Direction = ParameterDirection.Output;
                sqlParam[25].Direction = ParameterDirection.Output;
                sqlParam[26].Direction = ParameterDirection.Output;
                sqlParam[27].Direction = ParameterDirection.Output;
                sqlParam[28].Direction = ParameterDirection.Output;
                sqlParam[29].Direction = ParameterDirection.Output;

                sqlParam[30].Direction = ParameterDirection.Output;
                sqlParam[31].Direction = ParameterDirection.Output;
                sqlParam[32].Direction = ParameterDirection.Output;

                sqlParam[33].Direction = ParameterDirection.Output;
                sqlParam[34].Direction = ParameterDirection.Output;

                sqlParam[35].Direction = ParameterDirection.Output;

                sqlParam[36].Direction = ParameterDirection.Output;
                sqlParam[37].Direction = ParameterDirection.Output;
                sqlParam[38].Direction = ParameterDirection.Output;

                sqlParam[39].Direction = ParameterDirection.Output;

                /*
                sqlParam[37].Direction = ParameterDirection.Output;
                sqlParam[38].Direction = ParameterDirection.Output;

                sqlParam[39].Direction = ParameterDirection.Output;
                sqlParam[40].Direction = ParameterDirection.Output;

                sqlParam[41].Direction = ParameterDirection.Output;
                sqlParam[42].Direction = ParameterDirection.Output;

                sqlParam[43].Direction = ParameterDirection.Output;
                sqlParam[44].Direction = ParameterDirection.Output;
                sqlParam[45].Direction = ParameterDirection.Output;

                sqlParam[46].Direction = ParameterDirection.Output;
                sqlParam[47].Direction = ParameterDirection.Output;
                sqlParam[48].Direction = ParameterDirection.Output;
                */

                SqlCommand commond = await dbaccess.ExecuteNonQueryWithOutput(strconnection, "udp_app_account_auth", sqlParam);
                int result = int.Parse(commond.Parameters[6].Value.ToString());
                string result1 = (commond.Parameters[7].Value.ToString());
                string result2 = (commond.Parameters[8].Value.ToString());
                string result3 = (commond.Parameters[9].Value.ToString());
                string result4 = (commond.Parameters[10].Value.ToString());
                string result5 = (commond.Parameters[11].Value.ToString());
                string result6 = (commond.Parameters[12].Value.ToString());
                string result7 = (commond.Parameters[13].Value.ToString());
                string result8 = (commond.Parameters[14].Value.ToString());
                string result9 = (commond.Parameters[15].Value.ToString());
                int result10 = int.Parse(commond.Parameters[16].Value.ToString());

                string result11 = (commond.Parameters[17].Value.ToString());
                string result12 = (commond.Parameters[18].Value.ToString());
                string result13 = (commond.Parameters[19].Value.ToString());
                string result14 = (commond.Parameters[20].Value.ToString());

                string result15 = (commond.Parameters[21].Value.ToString());
                string result16 = (commond.Parameters[22].Value.ToString());
                string result17 = (commond.Parameters[23].Value.ToString());
                string result18 = (commond.Parameters[24].Value.ToString());
                string result19 = (commond.Parameters[25].Value.ToString());
                string result20 = (commond.Parameters[26].Value.ToString());
                string result21 = (commond.Parameters[27].Value.ToString());
                string result22 = (commond.Parameters[28].Value.ToString());
                string result23 = (commond.Parameters[29].Value.ToString());

                string result24 = (commond.Parameters[30].Value.ToString());
                string result25 = (commond.Parameters[31].Value.ToString());
                string result26 = (commond.Parameters[32].Value.ToString());

                string result27 = (commond.Parameters[33].Value.ToString());
                string result28 = (commond.Parameters[34].Value.ToString());

                string result29 = (commond.Parameters[35].Value.ToString());

                string result30 = (commond.Parameters[36].Value.ToString());
                string result31 = (commond.Parameters[37].Value.ToString());
                string result32 = (commond.Parameters[38].Value.ToString());

                string result33 = (commond.Parameters[39].Value.ToString());

                /*
                string result31 = (commond.Parameters[37].Value.ToString());
                string result32 = (commond.Parameters[38].Value.ToString());

                string result33 = (commond.Parameters[39].Value.ToString());
                string result34 = (commond.Parameters[40].Value.ToString());

                string result35 = (commond.Parameters[41].Value.ToString());
                string result36 = (commond.Parameters[42].Value.ToString());

                string result37 = (commond.Parameters[43].Value.ToString());
                string result38 = (commond.Parameters[44].Value.ToString());
                string result39 = (commond.Parameters[45].Value.ToString());

                string result40 = (commond.Parameters[46].Value.ToString());
                string result41 = (commond.Parameters[47].Value.ToString());
                string result42 = (commond.Parameters[48].Value.ToString());
                */

                models.AuthenProperty = result;
                models.Mailed = result1;
                models.ClientID = result2;
                models.IDCabang = result3;
                models.IDNotaris = result4;
                models.ClientName = result5;
                models.CabangName = result6;
                models.NotarisName = result7;
                models.UserName = result8;
                models.GenMoon = result9;
                models.UserType = result10.ToString();
                models.Region = result13.ToString();
                models.Area = result13.ToString();
                models.AreaName = result14.ToString();
                models.Divisi = result12.ToString();
                models.DivisiName = result11.ToString();

                models.Alamat = result15.ToString();
                models.tgllahir = result16.ToString();
                models.tptlahir = result17.ToString();
                models.NIK = result18.ToString();
                models.Kota = result19.ToString();
                models.Domisili = result20.ToString();
                models.Wilayah = result22.ToString();
                models.IDBPN = result23.ToString();

                models.Jabatan = result24.ToString();
                models.Pekerjaan = result25.ToString();
                models.Phone = result26.ToString();

                models.NoSK = result27.ToString();
                models.TglSK = result28.ToString();

                models.Photo = result29.ToString();

                models.ttdfform = result30.ToString();
                models.ttdsk = result31.ToString();
                models.ttdabsah = result32.ToString();

                models.JenisKelamin = result33.ToString();

                /*
                models.topForm = result31.ToString();
                models.leftForm = result32.ToString();

                models.topSK = result33.ToString();
                models.leftSK = result34.ToString();

                models.topAB= result35.ToString();
                models.leftAB = result36.ToString();

                models.docForm = result37.ToString();
                models.docSK = result38.ToString();
                models.docAB= result39.ToString();

                models.NoSK= result40.ToString();
                models.TglSK = result41.ToString();
                */
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            //string modelretunr= JsonConvert.SerializeObject(models);
            return models;
        }

        public async Task<List<cAccountGroupUser>> dbGetAuthGroupUser(bool Exceptiongroup, string GroupName = "", string UserID = "")
        {
            DataTable dt = new DataTable();
            List<cAccountGroupUser> DDL = new List<cAccountGroupUser>();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                new SqlParameter ("@GroupName",GroupName),
                new SqlParameter ("@UserID",UserID),
                new SqlParameter ("@Exceptiongroup",Exceptiongroup),
                 };
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_auth_group_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cAccountGroupUser()
                       {
                           secIDUser = HasKeyProtect.Encryption(c.Field<string>("UserID")),
                           SecGroupName = HasKeyProtect.Encryption(c.Field<string>("GroupName")),
                           GroupName = c.Field<string>("GroupName"),
                           UserGrup = c.Field<string>("UserID"),
                           MainGrup = c.Field<bool>("MainGrup")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<List<cAccountMetrik>> dbaccountmatriklist(bool Exceptiongroup, string GroupName = "", string IdMenu = "")
        {
            DataTable dt = new DataTable();
            List<cAccountMetrik> DDL = new List<cAccountMetrik>();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                SqlParameter[] sqlParam =
                          {
                                new SqlParameter ("@GroupName",GroupName),
                                new SqlParameter ("@ModuleID",IdMenu),
                                new SqlParameter ("@Exceptiongroup",Exceptiongroup)
                         };
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_auth_metrik_get", sqlParam);
                DDL = (from c in dt.AsEnumerable()
                       select new cAccountMetrik()
                       {
                           SecModuleID = HasKeyProtect.Encryption(c.Field<string>("ModuleID")),
                           SecGroupName = HasKeyProtect.Encryption(c.Field<string>("GroupName")),
                           GroupName = c.Field<string>("GroupName"),
                           MenuItem = new cMenu
                           {
                               ModuleID = c.Field<string>("ModuleID"),
                               ModuleName = c.Field<string>("ModuleName"),
                               ParentModuleID = c.Field<string>("ParentModuleID"),
                               ParentModuleName = c.Field<string>("ParentModuleName"),
                               SubModuleName = c.Field<string>("SubModuleName"),
                               Controller = c.Field<string>("Controller"),
                               Action = c.Field<string>("Action"),
                               SortOrderChild = c.Field<int>("SortOrderChild"),
                               SortOrder = c.Field<int>("SortOrder"),
                               ParentIcon = c.Field<string>("ParentIcon")
                           },
                           AllowView = c.Field<bool>("AllowView"),
                           AllowAdd = c.Field<bool>("AllowAdd"),
                           AllowEdit = c.Field<bool>("AllowEdit"),
                           AllowDelete = c.Field<bool>("AllowDelete"),
                           AllowBrowse = c.Field<bool>("AllowBrowse"),
                           AllowPrint = c.Field<bool>("AllowPrint"),
                           AllowDownload = c.Field<bool>("AllowDownload"),
                           AllowUpload = c.Field<bool>("AllowUpload"),
                           AllowApprove = c.Field<bool>("AllowApprove"),
                           AllowSubmit = c.Field<bool>("AllowSubmit"),
                           AllowCancel = c.Field<bool>("AllowCancel"),
                           AllowGenerate = c.Field<bool>("AllowGenerate"),
                           IsNeedApproval = c.Field<bool>("IsNeedApproval"),
                           AllowShowInMenu = c.Field<bool>("AllowShowInMenu")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<List<string>> dbGetHeaderTxListCount(string RequestNo, string email, string divisi, string cabang, string area, string status, int PageNumber, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                     new SqlParameter("@RegAccountNo", RequestNo),
                    new SqlParameter("@divisi", divisi),
                    new SqlParameter("@cabang", cabang),
                    new SqlParameter("@area", area),
                    new SqlParameter("@Status", status),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
            };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_reg_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetHeaderTxList(DataTable DTFromDB, string RequestNo, string email, string divisi, string cabang, string area, string status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@RegAccountNo", RequestNo),
                    new SqlParameter("@divisi", divisi),
                    new SqlParameter("@cabang", cabang),
                    new SqlParameter("@area", area),
                    new SqlParameter("@Status", status),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_reg_list", sqlParam);
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

        public async Task<DataTable> dbGetteamvery(string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    //new SqlParameter("@RegAccountNo", RequestNo),
                    //new SqlParameter("@divisi", divisi),
                    //new SqlParameter("@cabang", cabang),
                    //new SqlParameter("@area", area),
                    //new SqlParameter("@Status", status),
                    //new SqlParameter ("@moduleId",idcaption),
                    //new SqlParameter ("@UserIDLog",userid),
                    //new SqlParameter ("@UserGroupLog",groupname),
                    //new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udt_app_account_veryteam", sqlParam);

            return dt;
        }

        public async Task<List<SelectListItem>> HandleMap(string mapp, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            List<SelectListItem> items = new List<SelectListItem>();

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@jeni", mapp),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_handle", sqlParam);

            items = (from c in dt.AsEnumerable()
                     select new SelectListItem
                     {
                         Value = HashNetFramework.HasKeyProtect.Encryption(c.Field<string>("UserID")),
                         Text = c.Field<string>("Name")
                     }).ToList();

            return items;
        }

        public async Task<DataTable> dbSaveRegRTAccount(cAccountRegisNw models, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                if ((models.FlagOperation ?? "") == "")
                {
                    SqlParameter[] sqlParam = {
                            new SqlParameter("@id","0"),
                            new SqlParameter("@KodeRegis", models.KodeRegisCabang??""),
                            new SqlParameter("@email", models.email),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_regreset_sve", sqlParam);
                }
                else
                {
                    SqlParameter[] sqlParam = {
                            new SqlParameter("@id", "0"),
                            new SqlParameter("@KodeRegis", models.KodeRegisCabang??""),
                            new SqlParameter("@email", models.email??""),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_regreset_sve", sqlParam);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<int> dbprofileUserSve(cAccount models)
        {
            int dt = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                new SqlParameter ("@EmailID",models.Mailed),
                new SqlParameter ("@phone",models.Phone),
                new SqlParameter ("@useremail",models.email??""),
                new SqlParameter ("@UserName",models.UserName),
                new SqlParameter ("@jobtitile",models.Jabatan ??""),
                new SqlParameter ("@jobdesc",models.Pekerjaan ??""),
                new SqlParameter ("@tptlahir",models.tptlahir ?? ""),
                new SqlParameter ("@tgllahir",models.tgllahir??"0"),
                new SqlParameter ("@Alamat",models.Alamat ??""),
                new SqlParameter ("@NIK",models.NIK ??""),
                new SqlParameter ("@Kota",models.Kota ??""),
                new SqlParameter ("@Domisili",models.Domisili ??""),
                new SqlParameter ("@NamaNotaris",models.NotarisName??""),
                new SqlParameter ("@Wilayah",models.Wilayah??""),
                new SqlParameter ("@IDBPN",models.IDBPN??""),
                new SqlParameter ("@NoSK",models.NoSK ??""),
                new SqlParameter ("@TglSK",models.TglSK ??"0"),
                new SqlParameter ("@phote",models.Photo??""),
                new SqlParameter ("@ttdform",models.ttdfform??""),
                new SqlParameter ("@ttdsk",models.ttdsk??""),
                new SqlParameter ("@ttdabs",models.ttdabsah??""),

                new SqlParameter ("@JKelamin",models.JenisKelamin??""),
                //new SqlParameter ("@docform",models.docForm??""),
                //new SqlParameter ("@docsk",models.docSK??""),
                //new SqlParameter ("@docab",models.docAB??""),
                //new SqlParameter ("@nosk",models.NoSK??""),
                //new SqlParameter ("@tglsk",models.TglSK??""),

                 new SqlParameter ("@HandlePPAT",models.NotarisName),

                new SqlParameter ("@ModuleID",""),
                new SqlParameter ("@UserIDLog",models.UserID),
                new SqlParameter ("@UserGroupLog",models.GroupName),
                 };
                DataTable dtt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_auth_profile_set", sqlParam);
                dt = int.Parse(dtt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbSaveRegAccountNw(cAccountRegisNw models, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                if ((models.FlagOperation ?? "") == "")
                {
                    SqlParameter[] sqlParam = {
                            new SqlParameter("@id", "0"),
                            new SqlParameter("@KodeRegis", models.KodeRegisCabang??""),
                            new SqlParameter("@RegAccountNo", ""),
                            new SqlParameter("@RegAccountDate",  DateTime.Now.ToLongDateString()),
                            new SqlParameter("@RegAccountCreateBy", models.email ),
                            new SqlParameter("@Divisi", ""),
                            new SqlParameter("@FullName", models.FullName),
                            new SqlParameter("@Area", ""),
                            new SqlParameter("@Cabang", ""),
                            new SqlParameter("@email", models.email),
                            new SqlParameter("@PassCode", models.PasswordChange),
                            new SqlParameter("@Comment", ""),
                            new SqlParameter("@FlagOperation", models.FlagOperation??""),
                            new SqlParameter("@StatusFollow","0"),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_reg_sve", sqlParam);
                }
                else
                {
                    SqlParameter[] sqlParam = {
                            new SqlParameter("@id", "0"),
                            new SqlParameter("@KodeRegis", models.KodeRegisCabang??""),
                            new SqlParameter("@RegAccountNo", ""),
                            new SqlParameter("@RegAccountDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@RegAccountCreateBy", models.email ?? ""),
                            new SqlParameter("@FullName", models.FullName ?? ""),
                            new SqlParameter("@Divisi", ""),
                            new SqlParameter("@Area", ""),
                            new SqlParameter("@Cabang", ""),
                            new SqlParameter("@email", models.email ?? ""),
                            new SqlParameter("@PassCode", models.PasswordChange ?? ""),
                            new SqlParameter("@Comment", ""),
                            new SqlParameter("@FlagOperation", models.FlagOperation ?? ""),
                            new SqlParameter("@StatusFollow", "0"),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_reg_sve", sqlParam);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbSaveRegAccount(cAccountRegis models, string ModuleID, string UserID, string GroupName)
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
                            new SqlParameter("@KodeRegis", models.KodeRegisCabang??""),
                            new SqlParameter("@RegAccountNo", models.RegAccountNo??""),
                            new SqlParameter("@RegAccountDate", models.RegAccountDate ?? DateTime.Now.ToLongDateString()),
                            new SqlParameter("@RegAccountCreateBy", models.email ),
                            new SqlParameter("@Divisi", models.Devisi??""),
                            new SqlParameter("@FullName", models.FullName),
                            new SqlParameter("@Area", models.Area??""),
                            new SqlParameter("@Cabang", models.Cabang),
                            new SqlParameter("@email", models.email),
                            new SqlParameter("@PassCode", models.PasswordChange),
                            new SqlParameter("@Comment", ""),
                            new SqlParameter("@FlagOperation", models.FlagOperation??""),
                            new SqlParameter("@StatusFollow", models.StatusFollow ?? "0"),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_reg_sve", sqlParam);
                }
                else
                {
                    models.RegAccountDate = models.RegAccountDate ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    SqlParameter[] sqlParam = {
                            new SqlParameter("@id", models.IDHeaderTx),
                            new SqlParameter("@KodeRegis", models.KodeRegisCabang??""),
                            new SqlParameter("@RegAccountNo", models.RegAccountNo??""),
                            new SqlParameter("@RegAccountDate", models.RegAccountDate),
                            new SqlParameter("@RegAccountCreateBy", models.email??"" ),
                            new SqlParameter("@FullName", models.FullName??""),
                            new SqlParameter("@Divisi", models.Devisi??""),
                            new SqlParameter("@Area", models.Area??""),
                            new SqlParameter("@Cabang", models.Cabang??""),
                            new SqlParameter("@email", models.email??""),
                            new SqlParameter("@PassCode", models.PasswordChange??""),
                            new SqlParameter("@Comment", models.NotesFollow??""),
                            new SqlParameter("@FlagOperation", models.FlagOperation??""),
                            new SqlParameter("@StatusFollow", models.StatusFollow ?? "0"),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                    await Task.Delay(0);
                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_reg_sve", sqlParam);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbSaveRegAccountAct(cAccountRegis models, string statusbf, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@RegAccount", models.RegAccountNo),
                            new SqlParameter("@OnlyViewModule", models.IsViewModule),
                            new SqlParameter("@DivisiSel", models.Devisi),
                            new SqlParameter("@EmailUser", models.email),
                            new SqlParameter("@Notes", (models.NotesFollow ?? "").Replace("'", "''")),
                            new SqlParameter("@jenisProsesbf", statusbf ?? "0"),
                            new SqlParameter("@jenisProses", models.jenisprosesfollowup ?? "0"),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_reg_actv", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbSaveChangePass(string oldPass, string newpass, string mailed, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@UserID", UserID),
                            new SqlParameter("@PassOld", oldPass),
                            new SqlParameter("@PassNw", newpass),
                            new SqlParameter("@EmailUser", mailed),
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_chg_pss", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }
    }
}