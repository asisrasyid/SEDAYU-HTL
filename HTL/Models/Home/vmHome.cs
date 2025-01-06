using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DusColl
{
    [Serializable]
    public class vmHome : HashNetFramework.cAccount
    {
        public cFilterContract DetailFilter { get; set; }
        public DataTable OrderAll { get; set; }
        public int OrderAll1 { get; set; }
        public int OrderAll2 { get; set; }
        public int OrderAll3 { get; set; }
        public int OrderAll4 { get; set; }
        public int OrderAll5 { get; set; }
        public int OrderAll6 { get; set; }
        public int OrderAll7 { get; set; }
        public int OrderAll8 { get; set; }
        public int OrderAll9 { get; set; }
        public int OrderAll10 { get; set; }
        public int OrderAll11 { get; set; }
        public int OrderAll12 { get; set; }

        public DataTable TodoAll { get; set; }

        public DataTable TodoModule { get; set; }
        public DataTable TodoUser { get; set; }
        public DataTable activiryUser { get; set; }
        public DataTable infouser { get; set; }

        public DataTable TodoNOT { get; set; }
        public int TodoNOTCekSer { get; set; }
        public int TodoNOTSendBPN { get; set; }
        public int TodoNOTSiapAkad { get; set; }
        public int TotalInvCEK { get; set; }
        public int TotalInvSKMHTAPHT { get; set; }
        public int TotalInvCANCEL { get; set; }
        public int TotalInv { get; set; }

        public int TotalYetFIF { get; set; }

        public DataTable TodoODER { get; set; }

        public DataTable TodoNOTOD { get; set; }
        public int TodoNOTOD1 { get; set; }
        public int TodoNOTOD2 { get; set; }
        public int TodoNOTOD3 { get; set; }

        public DataTable TodoCAB { get; set; }
        public int TodoCABPerbaikan { get; set; }
        public int TodoCABPending { get; set; }
        public int TodoCABSiapAkad { get; set; }

        public DataTable TodoCABOD { get; set; }
        public int TodoCABOD1 { get; set; }
        public int TodoCABOD2 { get; set; }

        public DataTable TodoADM { get; set; }

        public DataTable TodoCVERFY { get; set; }
        public int TodoVERFY1 { get; set; }
        public int TodoVERFY2 { get; set; }

        public DataTable TodoCVERFYOD { get; set; }
        public int TodoCVERFYOD1 { get; set; }
        public int TodoCVERFYOD2 { get; set; }

        public DataTable TodoReadyINV { get; set; }

        public cAccountMetrik Permission { get; set; }
        public List<vmIsue> vmGrp_isue { get; set; }
    }

    public class vmIsue
    {
        public string tipe { get; set; }
        public string posisi_berkas { get; set; }
        public string posisi_penanganan { get; set; }
        public string group_posisi { get; set; }
    }

    [Serializable]
    public class vmHomeddl
    {
        public async Task<List<String>> dbGetHomeListCount(string KeySearch, string Divisi, string Area, string Cabang, string Status, string fromdate, string todate, string ModeTodo, int PageNumber, string idcaption, string userid, string groupname)
        {
            DataTable dt;
            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
             {
                new SqlParameter("@RequestNo", KeySearch),
                new SqlParameter("@Divisi", Divisi),
                new SqlParameter("@Area", Area),
                new SqlParameter("@Cabang", Cabang),
                new SqlParameter("@Status", Status),
                new SqlParameter("@startdate", fromdate),
                new SqlParameter("@enddate", todate),
                new SqlParameter("@ModeDash", ModeTodo),
                new SqlParameter ("@moduleId",idcaption),
                new SqlParameter ("@UserIDLog",userid),
                new SqlParameter ("@UserGroupLog",groupname),
                new SqlParameter ("@PageNumber",PageNumber),
            };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_wfh_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetHomeList(DataTable DTFromDB, string KeySearch, string Divisi, string Area, string Cabang, string Status, string fromdate, string todate, string ModeTodo, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@RequestNo", KeySearch),
                    new SqlParameter("@Divisi", Divisi),
                    new SqlParameter("@Area", Area),
                    new SqlParameter("@Cabang", Cabang),
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@startdate", fromdate),
                    new SqlParameter("@enddate", todate),
                    new SqlParameter("@ModeDash", ModeTodo),
                    new SqlParameter ("@moduleId",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_wfh_list", sqlParam);
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

        public async Task<DataTable> dbLogActivityUserWF(string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_apprworkflow_activity", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbLoginformasiUserWF(string ModuleID, string UserID, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@moduleId", ModuleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog", GroupName)
                    };

                await Task.Delay(0);
                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_trx_apprsimastra_informasi", sqlParam);
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