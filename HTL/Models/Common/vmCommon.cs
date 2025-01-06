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
    public class vmCommon
    {
        public IEnumerable<cListSelected> ddlItems { get; set; }
        public IEnumerable<cListSelected> ddlCusts { get; set; }
        public IEnumerable<cListSelected> ddlCase { get; set; }
        public IEnumerable<cListSelected> ddlCaseCab { get; set; }
        public IEnumerable<cListSelected> ddlCasepen { get; set; }
        public IEnumerable<cListSelected> ddlCasepenAkd { get; set; }
        public IEnumerable<cListSelected> ddlVendor { get; set; }
        public IEnumerable<cListSelected> ddlFollow { get; set; }

        public IEnumerable<cListSelected> ddlNIK { get; set; }
        public IEnumerable<cListSelected> ddlNIB { get; set; }
        public IEnumerable<cListSelected> ddlnotaris { get; set; }
        public IEnumerable<cListSelected> ddlnotarisRgn { get; set; }

        public IEnumerable<cListSelected> ddlUserID { get; set; }

        public IEnumerable<cListSelected> ddlGrupUser { get; set; }

        public IEnumerable<cListSelected> ddlnotarisInv { get; set; }
        public IEnumerable<cListSelected> ddlstatus { get; set; }
        public DataTable ddlstatusmap { get; set; }
        public IEnumerable<cListSelected> ddlsrc { get; set; }
        public IEnumerable<cListSelected> ddlhak { get; set; }

        public IEnumerable<cListSelected> ddlLokkota { get; set; }
        public IEnumerable<cListSelected> ddlLokprov { get; set; }
        public IEnumerable<cListSelected> ddlLokkec { get; set; }
        public IEnumerable<cListSelected> ddlLokdesa { get; set; }

        public IEnumerable<cListSelected> ddlTeamHO { get; set; }
        public IEnumerable<cListSelected> ddlPeriode { get; set; }
        public IEnumerable<cListSelected> ddlPic { get; set; }
        public IEnumerable<cListSelected> ddlDevisi { get; set; }
        public IEnumerable<cListSelected> ddlBranch { get; set; }
        public IEnumerable<cListSelected> ddlRegion { get; set; }
        public IEnumerable<cListSelected> ddlBank { get; set; }
        public IEnumerable<cListSelected> ddlBankBranch { get; set; }
        public IEnumerable<cListSelected> ddlJenKel { get; set; }
        public IEnumerable<cListSelected> ddlStatKW { get; set; }
        public IEnumerable<cListSelected> ddlStatNKH { get; set; }
        public IEnumerable<cListSelected> ddlPosistionPenanganan { get; set; }

        public IEnumerable<cListSelected> ddlJenPen { get; set; }
        public IEnumerable<cListSelected> ddlJobs { get; set; }
        public IEnumerable<cListSelected> ddlRegmitraType { get; set; }
        public IEnumerable<cListSelected> ddlMitra { get; set; }
        public IEnumerable<cListSelected> ddlDocument { get; set; }
        public IEnumerable<cListSelected> ddlStatusMitra { get; set; }
        public IEnumerable<cListSelected> ddlStatusRegUsr { get; set; }
        public IEnumerable<cListSelected> ddlJenisDokumen { get; set; }

        public IEnumerable<cListSelected> ddlGrupAkses { get; set; }
    }

    [Serializable]
    public class vmCommonddl
    {
        public async Task<string> dbValidFileupload(HttpPostedFileBase file, string TypeCekFileFormat, string noappl, string module, string UserID, string Groupname, double MaxSizeR = 0)
        {
            string valid = "";
            double maxSize = MaxSizeR == 0 ? 1000 : MaxSizeR;
            double maxlengfile = 100;

            string flleext = file.FileName.Substring(file.FileName.Length - 5, 5).ToLower();
            if (!file.ContentType.ToLower().Contains("jpg") && !file.ContentType.ToLower().Contains("jpeg") && !file.ContentType.ToLower().Contains("pdf"))
            {
                valid = "Tipe File harus .jpg/.jpeg/.pdf";
            }
            else if (!flleext.Contains(".jpg") && !flleext.Contains(".pdf") && !flleext.Contains(".jpeg"))
            {
                valid = "Tipe File harus .jpg/.jpeg/.pdf";
            }
            else if (Math.Round(double.Parse(file.ContentLength.ToString()) / double.Parse("1000"), 2) > maxSize)
            {
                valid = "Ukuran file lebih besar dari " + maxSize.ToString() + " KB";
            }
            else if (file.FileName.Length > maxlengfile)
            {
                valid = "Nama file lebih besar dari " + maxlengfile.ToString() + " Karakter";
            }

            if (valid == "" && TypeCekFileFormat == "SERTICEK")
            {
                var splitfilename = file.FileName.Split('_');
                if (splitfilename.Length > 2)
                {
                    valid = "Format Nama File 'NoAplikasi_AtasNamaSertifikat' ";
                }
                else
                {
                    if (splitfilename[0].Length != 14)
                    {
                        valid = "No Aplikasi harus 14 digit";
                    }
                }

                if (valid == "")
                {
                    vmHTLddl dttl = new vmHTLddl();
                    DataTable dt = await dttl.dbdbGetDdlOrderGetCek("1", splitfilename[0], "", module, UserID, Groupname);
                    if (dt.Rows.Count == 0)
                    {
                        valid = "No Aplikasi tidak terdaftar";
                    }
                }
            }

            if (valid == "" && TypeCekFileFormat == "UPLOADDOC")
            {
                var splitfilename = file.FileName.Split('_');
                if (noappl.Length != 14)
                {
                    valid = "No Aplikasi harus 14 digit";
                }
                if (valid == "")
                {
                    vmHTLddl dttl = new vmHTLddl();
                    DataTable dt = await dttl.dbdbGetDdlOrderGetCek("4", noappl, "", module, UserID, Groupname);
                    if (dt.Rows.Count == 0)
                    {
                        //insert
                        dt = await dttl.dbdbGetDdlOrderGetCek("6", noappl, "", module, UserID, Groupname);
                    }
                    else
                    {
                        dt = await dttl.dbdbGetDdlOrderGetCek("7", noappl, "", module, UserID, Groupname);
                    }

                    string jdoc = splitfilename[0].ToLower().Replace(".jpg", "").Replace(".jpeg", "").Replace(".pdf", "");
                    dt = await dttl.dbdbGetDdlOrderGetCek("2", jdoc, "", module, UserID, Groupname);
                    if (dt.Rows.Count == 0)
                    {
                        valid = "Jenis dokumen " + jdoc.ToUpper() + " tidak terdaftar";
                    }
                }
            }

            return valid;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlBranchListByEncrypt(string RegType, string SelectBranch, string SelectRegion, string moduleid, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@RegType" ,RegType),
                    new SqlParameter ("@KodeCabang" ,SelectBranch),
                    new SqlParameter ("@Region",SelectRegion),
                    new SqlParameter ("@moduleId",moduleid),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_branch_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<Int32>("BrchID").ToString()),
                           Text = c.Field<string>("BranchName")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlDevisiListByEncrypt(string tipe, string SelectDevisi, string module, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@tipe",tipe),
                    new SqlParameter ("@Divisi",SelectDevisi),
                    new SqlParameter ("@moduleId",module),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_devisi_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<Int32>("DevisiID")).ToString(),
                           Text = c.Field<string>("DevisiName")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlDevisiGrpListByEncrypt(string tipe, string SelectGroup, string module, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@tipe",tipe),
                    new SqlParameter ("@GROUP",SelectGroup),
                    new SqlParameter ("@moduleId",module),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_teamho_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<Int32>("GroupID")).ToString(),
                           Text = c.Field<string>("GroupName")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbdbGetDdlDevisiPeriodeListByEncrypt(string tipe, string SelectDevisi, string module, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@tipe",tipe),
                    new SqlParameter ("@Divisi",SelectDevisi),
                    new SqlParameter ("@moduleId",module),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_devisi_get", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlRegionListByEncrypt(string tipe, string SelectRegion, string module, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@tipe",tipe),
                    new SqlParameter ("@Region",SelectRegion),
                    new SqlParameter ("@moduleId",module),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_region_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<Int32>("RegionID").ToString()),
                           Text = c.Field<string>("RegionName")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlhandleJobListByEncrypt(string regtype, string SelectCode, string selectdiv, string module, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@RegType" ,regtype),
                    new SqlParameter ("@JobID" ,SelectCode),
                    new SqlParameter ("@Divisi" ,selectdiv),
                    new SqlParameter ("@moduleId",module),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_handlejobs_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<Int32>("JobID").ToString()),
                           Text = c.Field<string>("jobDesc")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlBranchBranchListByEncrypt(string SelectBranch, string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@KodeCabang" ,SelectBranch),
                    new SqlParameter ("@moduleId",ModuleID),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",GroupName)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_bankbranch_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<Int32>("BrchID").ToString()),
                           Text = c.Field<string>("BranchName")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public IEnumerable<cListSelected> dbGetDdlgrupListByEncrypt(List<cAccountGroupUser> dt)
        {
            IEnumerable<cListSelected> DDL = (from c in dt.AsEnumerable()
                                              select new cListSelected()
                                              {
                                                  Text = c.GroupName,
                                                  Value = c.SecGroupName
                                              }).ToList();

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlEnumListByEncrypt(string formCode, string enumname, string Moduleid, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@formCode",formCode),
                    new SqlParameter ("@enumname",enumname),
                    new SqlParameter ("@moduleId" ,Moduleid),
                    new SqlParameter ("@UserIDLog" ,UserID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_enums_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<string>("EnumValue")),
                           Text = c.Field<string>("EnumsDesc")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlPICListByEncrypt(string SelectUserID, string UserType, string Moduleid, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@PUserID",SelectUserID),
                    new SqlParameter ("@UserType",UserType),
                    new SqlParameter ("@moduleId" ,Moduleid),
                    new SqlParameter ("@UserIDLog" ,UserID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_pic_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<string>("PICID")),
                           Text = c.Field<string>("Name")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbdbGetDokumenList(string id, string RegNo, string Cont_no, string typeview, string divisi, string regtype, string moduleId, string UserIDLog, string UserGroupLog)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@id" ,id),
                    new SqlParameter ("@RegNo" ,RegNo),
                    new SqlParameter ("@Cont_no" ,Cont_no),
                    new SqlParameter ("@typeview" ,typeview),
                    new SqlParameter ("@Divisi" ,divisi),
                    new SqlParameter ("@regType" ,regtype),
                    new SqlParameter ("@moduleId" ,moduleId),
                    new SqlParameter ("@UserIDLog" ,UserIDLog),
                    new SqlParameter ("@UserGroupLog" ,UserGroupLog),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_regismitra_docview", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDokumenListCek(DataTable dt)
        {
            IEnumerable<cListSelected> DDL = null;
            try
            {
                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = HasKeyProtect.Encryption((c.Field<int>("ID").ToString())),
                           Text = c.Field<string>("DOCUMENT_TYPE")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetJenisDokumenList(string id, string Regno, string tipe, string moduleId, string UserIDLog, string UserGroupLog)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@id" ,id),
                    new SqlParameter ("@RegNo" ,Regno),
                    new SqlParameter ("@Cont_no" ,""),
                    new SqlParameter ("@typeview" ,tipe),
                    new SqlParameter ("@Divisi" ,""),
                    new SqlParameter ("@regType" ,""),
                    new SqlParameter ("@moduleId" ,moduleId),
                    new SqlParameter ("@UserIDLog" ,UserIDLog),
                    new SqlParameter ("@UserGroupLog" ,UserGroupLog),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_docview", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<int>("ID").ToString()),
                           Text = c.Field<string>("Docname")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbdbGetJenisDokumen(string id, string Regno, string tipe, string moduleId, string UserIDLog, string UserGroupLog)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
               {
                    new SqlParameter ("@id" ,id),
                    new SqlParameter ("@RegNo" ,Regno??""),
                    new SqlParameter ("@Cont_no" ,""),
                    new SqlParameter ("@typeview" ,tipe),
                    new SqlParameter ("@Divisi" ,""),
                    new SqlParameter ("@regType" ,""),
                    new SqlParameter ("@moduleId" ,moduleId),
                    new SqlParameter ("@UserIDLog" ,UserIDLog),
                    new SqlParameter ("@UserGroupLog" ,UserGroupLog),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_docview", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetJenisDokumenDll(string id, string Regno, string tipe, string moduleId, string UserIDLog, string UserGroupLog)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
               {
                    new SqlParameter ("@id" ,id),
                    new SqlParameter ("@RegNo" ,Regno??""),
                    new SqlParameter ("@Cont_no" ,""),
                    new SqlParameter ("@typeview" ,tipe),
                    new SqlParameter ("@Divisi" ,""),
                    new SqlParameter ("@regType" ,""),
                    new SqlParameter ("@moduleId" ,moduleId),
                    new SqlParameter ("@UserIDLog" ,UserIDLog),
                    new SqlParameter ("@UserGroupLog" ,UserGroupLog),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_docview", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = HashNetFramework.HasKeyProtect.Encryption(c.Field<int>("IDDOC").ToString()),
                           Text = c.Field<string>("DOCUMENT_TYPE")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlbankNameListByEncrypt(string SelectCode, string UserID)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@bankid" ,SelectCode),
                    new SqlParameter ("@userId",UserID)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_bank_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<string>("BANKID")),
                           Text = c.Field<string>("BANKNAME")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbdbGetDdlMitraListByEncryptcek(string Key, string cabang, string regtype, string moduleid, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@Key" ,Key),
                    new SqlParameter ("@RegType" ,regtype),
                    new SqlParameter ("@cabang" ,cabang),
                    new SqlParameter ("@moduleId",moduleid),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_mitra_get_cek", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlpendidikanListByEncrypt(string SelectCode, string UserID)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@educid" ,SelectCode),
                    new SqlParameter ("@userId",UserID)
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_pendidikan_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = c.Field<int>("ID").ToString(),
                           Text = c.Field<string>("EducDesc")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlEnumsListByEncryptNw(string Kode, string moduleid, string UserID, string groupname, string fromKode = "")
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                     new SqlParameter ("@formCode",fromKode),
                    new SqlParameter ("@enumname",Kode),
                    new SqlParameter ("@moduleId",moduleid),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_enums_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = HasKeyProtect.Encryption(c.Field<string>("EnumValue")),
                           Text = c.Field<string>("EnumsDesc")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbdbGetDdlEnumsListByEncryptNwdt(string Kode, string moduleid, string UserID, string groupname, string fromKode = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                     new SqlParameter ("@formCode",fromKode),
                    new SqlParameter ("@enumname",Kode),
                    new SqlParameter ("@moduleId",moduleid),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_enums_get", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return dt;
        }

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlNotarisListByEncrypt(string Moduleid, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
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

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlNotarisListByEncryptINV(string Moduleid, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter ("@moduleId" ,Moduleid),
                    new SqlParameter ("@UserIDLog" ,UserID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_notaris4inv", sqlParam);

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

        public async Task<IEnumerable<cListSelected>> dbdbGetDdlEnumsListByEncrypt(string Kode, string moduleid, string UserID, string groupname, string fromKode = "")
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                     new SqlParameter ("@formCode",fromKode),
                    new SqlParameter ("@enumname",Kode),
                    new SqlParameter ("@moduleId",moduleid),
                    new SqlParameter ("@UserIDLog",UserID),
                    new SqlParameter ("@UserGroupLog",groupname),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_enums_get", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Value = (c.Field<string>("EnumValue")),
                           Text = c.Field<string>("EnumsDesc")
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<int> dbupdateflagsht(string jenisinv, DataTable dtx, string moduleID, string userid, string groupname)
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
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htlht_genflag", sqlParam);
                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return result;
        }

        public async Task<List<cDocumentsGet>> Getdocview(int IDKey, string regno, string tipe, string moduleid, string UserID, string groupname)
        {
            DataTable dt = new DataTable();
            List<cDocumentsGet> DDL = new List<cDocumentsGet>();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                new SqlParameter ("@id",IDKey),
                new SqlParameter ("@RegNo",regno),
                new SqlParameter ("@Cont_no",""),
                new SqlParameter ("@typeview",tipe),
                new SqlParameter ("@Divisi",""),
                new SqlParameter ("@regType",""),
                new SqlParameter ("@moduleId",moduleid),
                new SqlParameter ("@UserIDLog",UserID),
                new SqlParameter ("@UserGroupLog",groupname),
              };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_docview", sqlParam);
                DDL = (from c in dt.AsEnumerable()
                       select new cDocumentsGet()
                       {
                           FILE_NAME = c.Field<string>("FILE_NAME"),
                           password = c.Field<string>("NIK").ToString() == "" ? "Simasthel2022" : c.Field<string>("NIK").ToString(),
                           ExternalName = c.Field<string>("NIK").ToString() == "" ? c.Field<string>("NOKTP").ToString() + "_" + c.Field<string>("FILE_NAME") : c.Field<string>("NIK").ToString() + "_" + c.Field<string>("FILE_NAME"),
                           CONTENT_TYPE = c.Field<string>("CONTENT_TYPE"),
                           CONTENT_LENGTH = c.Field<string>("CONTENT_LENGTH"),
                           FILE_BYTE = c.Field<string>("FILE_BYTE"),
                           CREATED_DATE = c.Field<string>("CREATED_DATE"),
                           CREATED_BY = c.Field<string>("CREATED_BY"),
                           ID = c.Field<string>("ID"),
                           cont_no = c.Field<string>("cont_no"),
                           cont_type = c.Field<string>("cont_type"),
                           ID_UPLOAD = c.Field<string>("ID_UPLOAD"),
                           Document_Type = c.Field<string>("DocumentType"),
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<List<string>> dbGetApprovalLogListCount(string ForeCastNo, int PageNumber, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@ForeCastNo", ForeCastNo),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_approvallog_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetApprovalLogList(DataTable DTFromDB, string ForeCastNo, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@ForeCastNo", ForeCastNo),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_approvallog_list", sqlParam);
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

        public async Task<DataTable> dbGetApprovalTodo(string Tipe, string idcaption, string keycode, string userid, string groupname)
        {
            DataTable dt;
            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@Tipe", Tipe),
                    new SqlParameter("@keycode", keycode),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_htl_todo_cnt", sqlParam);
            return dt;
        }

        public async Task<Boolean> dbGetApprovalCheck(string TransNo, string idcaption, string userid, string groupname)
        {
            DataTable dt;
            Boolean bl;
            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                    new SqlParameter("@TransNo", TransNo),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname)
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_approvallog_chk", sqlParam);
            bl = Boolean.Parse(dt.Rows[0][0].ToString());
            return bl;
        }

        public async Task<IEnumerable<cListSelected>> dbgetDdlparamenumsList(string Code)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@enumname",Code),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_enums_get", sqlParam);
                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Text = c.Field<string>("EnumsDesc").ToString(),
                           Value = c.Field<string>("EnumValue").ToString()
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbGetDdlItemsForeCastList(string ItemCode, string ForeCastNo, string userID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@ItemCode" ,ItemCode),
                    new SqlParameter ("@ForeCastNo" ,ForeCastNo),
                    new SqlParameter ("@UserIDLog" ,userID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_itemforecast_get", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbGetDdlItemsPurchaseList(string ItemCode, string PurchaseNo, string userID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@ItemCode" ,ItemCode),
                    new SqlParameter ("@PurchaseNo" ,PurchaseNo),
                    new SqlParameter ("@UserIDLog" ,userID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_itempurchase_get", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<IEnumerable<cListSelected>> dbGetDdlItemsList(string keyword, string userID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@Keyword" ,keyword),
                    new SqlParameter ("@UserIDLog" ,userID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_items_get", sqlParam);
                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Text = c.Field<String>("ItemDescription").ToString(),
                           Value = c.Field<String>("ItemCode").ToString()
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbGetDdlCustList(string keyword, string userID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@Keyword" ,keyword),
                    new SqlParameter ("@UserIDLog" ,userID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_customer_get", sqlParam);
                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Text = c.Field<String>("CustomerName").ToString(),
                           Value = c.Field<String>("CustomerID").ToString()
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<IEnumerable<cListSelected>> dbGetDdlVendorList(string keyword, string userID, string GroupName)
        {
            DataTable dt = new DataTable();
            IEnumerable<cListSelected> DDL = null;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@Keyword" ,keyword),
                    new SqlParameter ("@UserIDLog" ,userID),
                    new SqlParameter ("@UserGroupLog" ,GroupName),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_prm_vendor_get", sqlParam);
                DDL = (from c in dt.AsEnumerable()
                       select new cListSelected()
                       {
                           Text = c.Field<String>("VendorName").ToString(),
                           Value = c.Field<String>("VendorID").ToString()
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<List<cConfig>> dbGetConfig(string kode)
        {
            DataTable dt = new DataTable();
            List<cConfig> DDL = new List<cConfig>();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDBCFG());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@key" ,kode),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_account_authconfigsempi", sqlParam);

                DDL = (from c in dt.AsEnumerable()
                       select new cConfig()
                       {
                           Code = c.Field<string>("Code").ToString(),
                           Name = c.Field<string>("Name").ToString(),
                       }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<DataTable> dbSetHostHistory(string UserID, string HostID, string ipaddress, string ipaddress1, string action,
           string TimeOut, string browser, string type = "")
        {
            DataTable DDL = new DataTable();
            //string DDL = "";
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@userid",UserID),
                    new SqlParameter ("@hostid",HostID),
                    new SqlParameter ("@ipaddress",ipaddress),
                    new SqlParameter ("@ipaddress1",ipaddress1),
                    new SqlParameter ("@action",action),
                    new SqlParameter ("@browser",browser),
                    new SqlParameter ("@timeout",TimeOut),
                    new SqlParameter ("@type",type),
                };

                DDL = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_host_history_sve", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public DataTable dbSetHostHistoryCK(string UserID, string HostID, string ipaddress, string ipaddress1, string action, string TimeOut, string browser = "", string type = "")
        {
            DataTable DDL = new DataTable();
            //string DDL = "";
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@userid",UserID),
                    new SqlParameter ("@hostid",HostID),
                    new SqlParameter ("@ipaddress",ipaddress),
                    new SqlParameter ("@ipaddress1",ipaddress1),
                    new SqlParameter ("@action",action),
                    new SqlParameter ("@browser",browser),
                    new SqlParameter ("@TimeOut",TimeOut),
                    new SqlParameter ("@type",type),
                };

                DDL = dbaccess.ExecuteDataTableNonAsync(strconnection, "udp_com_app_host_history_sve", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return DDL;
        }

        public async Task<int> dbSetOTPCode(string secIDUser, string templatename, string ModuleID, string useremail, string hostid, string ipaddress, string veryfiedcode)
        {
            DataTable dt = new DataTable();
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@templatename",templatename),
                    new SqlParameter ("@ModuleID",ModuleID),
                    new SqlParameter ("@UserID",secIDUser),
                    new SqlParameter ("@useremail",useremail),
                    new SqlParameter ("@hostid",hostid),
                    new SqlParameter ("@ipaddress",ipaddress),
                    new SqlParameter ("@veryfiedcode",veryfiedcode),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_otp_code_set", sqlParam);
                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<cOTP> dbCheckOTPCode(string secIDUser, string templatename, string ModuleID, string useremail, string hostid, string ipaddress, string OtpInputByuser, bool checkOTP = false, string col1 = "", string col2 = "")
        {
            cOTP otprec = new cOTP();

            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@templatename",templatename),
                    new SqlParameter ("@ModuleID",ModuleID),
                    new SqlParameter ("@UserID",secIDUser),
                    new SqlParameter ("@useremail",useremail),
                    new SqlParameter ("@hostid",hostid),
                    new SqlParameter ("@ipaddress",ipaddress),
                    new SqlParameter ("@OtpInputByUser",OtpInputByuser),
                    new SqlParameter ("@Col1",col1),
                    new SqlParameter ("@Col2",col2)
                };

                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_otp_code_chk", sqlParam);

                int resultED = int.Parse(dt.Rows[0][0].ToString());
                string Otpcode = dt.Rows[0][1].ToString();

                otprec.Result = resultED;
                otprec.Otpcode = Otpcode;
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return otprec;
        }

        public async Task<cOTP> VerifiedOTP(string UserID, string Mailed, string templatename, string HostPCName, string ipAddress, string OTPInputByUser, string CommentLog, string externalmessage, string extencol1 = "", string extencol2 = "")
        {
            string MessageNotValid = "";
            bool ErroSaveOTP = false;
            string OTPcode = OTPInputByUser;

            cOTP ResultOutput = new cOTP();

            ResultOutput = await dbCheckOTPCode(UserID, templatename, templatename, Mailed, HostPCName, ipAddress, OTPInputByUser, false, extencol1, extencol2);
            string Otpfromdb = ResultOutput.Otpcode;
            int resulted = ResultOutput.Result;
            if (resulted != 1)
            {
                MessageNotValid = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
                //jika result generate baru otp -60 atau otp exired  -59//
                if ((resulted == (int)ProccessOutput.FilterNotValidKodeExpiredFirst) || (resulted == (int)ProccessOutput.FilterNotValidKodeExpired))
                {
                    //jikat belum ada otpnya didb
                    if ((Otpfromdb ?? "") == "")
                    {
                        OTPInputByUser = HashNetFramework.HasKeyProtect.GenerateOTP();
                        int resulted1 = await dbSetOTPCode(UserID, templatename, templatename, Mailed, HostPCName, ipAddress, OTPInputByUser);
                        //otp berhasil di genreate//
                        if (resulted1 == 1)
                        {
                            //await dbSetHostHistory(UserID, HostPCName, ipAddress, CommentLog).ConfigureAwait(false);

                            int resultsendemail = 0;
                            if (templatename == "AccountForcechange")
                            {
                                resultsendemail = await MessageEmail.sendEmail((int)EmailType.userchangeforceglogin, Mailed, OTPInputByUser);
                            }
                            else if (templatename == "Accountchange")
                            {
                                resultsendemail = await MessageEmail.sendEmail((int)EmailType.ResetPassword, Mailed, OTPInputByUser);
                            }
                        }
                        else //gagal generate
                        {
                            ErroSaveOTP = true;
                            resulted = (int)ProccessOutput.Error;
                            MessageNotValid = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
                        }
                    }
                    else
                    {
                        // await dbSetHostHistory(UserID, HostPCName, ipAddress, "Code : " + CommentLog).ConfigureAwait(false);
                    }
                }
            }

            // set user must input code or not //
            if (((OTPcode == "") ||
                (resulted == (int)ProccessOutput.FilterNotValidKodeExpiredFirst) ||
                (resulted == (int)ProccessOutput.FilterNotValidKodeExpired) ||
                (resulted == (int)ProccessOutput.FilterValidoptgeneratecheck)) && ErroSaveOTP == false)
            {
                ResultOutput.isMustInputCode = true;
            }

            //for the first for wrong login//
            if ((resulted == (int)ProccessOutput.FilterNotValidKodeExpiredFirst))
            {
                MessageNotValid = (externalmessage ?? "") == "" ? MessageNotValid : externalmessage;
            }

            // jika berhasil
            if (resulted == (int)ProccessOutput.Success)
            {
                if (templatename == "AccountWrongLogin")
                {
                    MessageNotValid = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidunlockAccount));
                }
                else
                if (templatename == "Accountchange")
                {
                    MessageNotValid = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKataSandichange));
                }
            }

            ResultOutput.Message = MessageNotValid;
            ResultOutput.Result = resulted;
            ResultOutput.ErrorOTP = ErroSaveOTP;

            return ResultOutput;
        }
    }

    public static class initcapp
    {
        #region initicaptital

        public static string InitCap(string InputString)
        {
            string result = "";
            int index;
            string chared;
            string PrevChar;

            result = InputString.ToLower();
            index = 0;

            List<string> strlis = new List<string>();
            strlis.Add(" ");
            strlis.Add(";");
            strlis.Add(":");
            strlis.Add("! ");
            strlis.Add("?");
            strlis.Add(",");
            strlis.Add(".");
            strlis.Add("_");
            strlis.Add("-");
            strlis.Add("/");
            strlis.Add("&");
            strlis.Add("'");
            strlis.Add("(");

            string expet;
            try
            {
                while (index <= InputString.Length)
                {
                    chared = InputString.Substring(index, 1);
                    PrevChar = (index == 0) ? " " : InputString.Substring(index - 1, 1);
                    int find = strlis.Where(x => x == PrevChar).Count();
                    if (find > 0)
                    {
                        if (PrevChar != "'" || chared.ToUpper() != "S")
                        {
                            result = Stuff(result, index, 1, chared.ToUpper());
                        }
                    }
                    index = index + 1;
                }
            }
            catch (Exception ex)
            {
                expet = ex.Message;
            }
            return result;
        }

        public static string Stuff(this string str, int start, int length, string replaceWith_expression)
        {
            return str.Remove(start, length).Insert(start, replaceWith_expression);
        }

        #endregion initicaptital
    }
}