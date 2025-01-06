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
    public class vmFinance
    {
        public string CheckWithKey { get; set; }
        public string securemoduleID { get; set; }
        public cFilterContract DetailFilter { get; set; }
        public cAccountMetrik Permission { get; set; }
        public DataTable DTOrdersFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }
        public DataTable DTREKAP { get; set; }
    }

    public class vmFinanceddl
    {
        public async Task<cFinance> dbGetPayListTxt(string ClientIDS, string ID, string idcaption, string userid, string groupname)
        {
            cFinance models = new cFinance();
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                {
                    new SqlParameter ("@moduleid",idcaption),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@ID",ID),
               };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_com_app_docupload_request", sqlParam);
                models.bytefile = (byte[])dt.Rows[0]["FILE_BYTE"];
                models.content_type = dt.Rows[0]["CONTENT_TYPE"].ToString();
                models.filename = dt.Rows[0]["FILE_NAME"].ToString();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return models;
        }

        public async Task<int> dbupdatepaymentBNI(string filename, string Req_type, int recdata, string Datevalue, string reqID, int status, byte[] bytefile, string content_type, string moduleID, string userid, string groupname)
        {
            int dt = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                  {
                         new SqlParameter ("@reqID",reqID),
                         new SqlParameter ("@File_name",filename),
                         new SqlParameter ("@Req_type",Req_type),
                         new SqlParameter ("@Rec_Data",recdata),
                         new SqlParameter ("@Datevalue",Datevalue),
                         new SqlParameter ("@status",status),
                         new SqlParameter ("@File_byte",bytefile),
                         new SqlParameter ("@content_type",content_type),
                         new SqlParameter ("@moduleId",moduleID),
                         new SqlParameter ("@UserIDLog",userid),
                         new SqlParameter ("@UserGroupLog",groupname),
                 };

                DataTable dtx = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_bni_send_req", sqlParam);
                dt = int.Parse(dtx.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbupdatepaymentINV(string filename, string Req_no, string Req_type, int recdata, string Datevalue, string reqID, int status, byte[] bytefile, string content_type, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                  {
                         new SqlParameter ("@reqID",reqID),
                         new SqlParameter ("@Reguestno",Req_no),
                         new SqlParameter ("@File_name",filename),
                         new SqlParameter ("@Req_type",Req_type??"0"),
                         new SqlParameter ("@Rec_Data",recdata),
                         new SqlParameter ("@Datevalue",Datevalue),
                         new SqlParameter ("@status",status),
                         new SqlParameter ("@File_byte",bytefile),
                         new SqlParameter ("@content_type",content_type),
                         new SqlParameter ("@moduleId",moduleID),
                         new SqlParameter ("@UserIDLog",userid),
                         new SqlParameter ("@UserGroupLog",groupname),
                 };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_invoice_send_req", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dt;
        }

        public async Task<DataTable> dbupdatepaymentINVGENPPAT(string JenisINV, DataTable dtx, byte[] FileByteINV, string InvNo, string namefile, string moduleID, string userid, string groupname)
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

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_invoice_genppat", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return dt;
        }

        public async Task<DataTable> dbupdatepaymentINVGEN(string JenisINV, DataTable dtx, byte[] FileByteINV, string InvNo,
            string namefile, string NamaPPAT, string NoRequest, string moduleID, string userid, string groupname)
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
                        new SqlParameter ("@PPAT",NamaPPAT??""),
                        new SqlParameter ("@NoRequest",NoRequest??""),
                        new SqlParameter ("@Filenme",namefile),
                        new SqlParameter ("@moduleId",moduleID),
                        new SqlParameter ("@UserIDLog",userid),
                        new SqlParameter ("@UserGroupLog",groupname),
                 };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_invoice_gen", sqlParam);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return dt;
        }

        public async Task<int> dbupdatepaymentINVGENFlag(string jenisinv, DataTable dtx, string moduleID, string userid, string groupname)
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
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_invoice_genflag", sqlParam);
                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }
            return result;
        }

        public async Task<List<String>> dbGetPayListCountINV(string ClientIDS, string fromdate, string requestype, string status, int PageNumber, string idcaption, string userid, string groupname)
        {
            List<String> dta = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                SqlParameter[] sqlParam =
                  {
                        new SqlParameter ("@ClientID",ClientIDS),
                        new SqlParameter ("@tgl_request",fromdate),
                        new SqlParameter ("@requestype",requestype),
                        new SqlParameter ("@status",status),
                        new SqlParameter ("@moduleId",idcaption),
                        new SqlParameter ("@UserIDLog",userid),
                        new SqlParameter ("@UserGroupLog",groupname),
                        new SqlParameter ("@PageNumber",PageNumber),
                 };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_invoice_send_list_cnt", sqlParam);

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
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dta;
        }

        public async Task<List<DataTable>> dbGetPayListINV(DataTable DTFromDB, string ClientIDS, string fromdate, string requestype, string status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            try
            {
                if (DTFromDB == null)
                {
                    dbAccessHelper dbaccess = new dbAccessHelper();
                    string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());
                    SqlParameter[] sqlParam =
                      {
                        new SqlParameter ("@ClientID",ClientIDS),
                        new SqlParameter ("@tgl_request",fromdate),
                        new SqlParameter ("@requestype",requestype),
                        new SqlParameter ("@status",status),
                        new SqlParameter ("@moduleId",idcaption),
                        new SqlParameter ("@UserIDLog",userid),
                        new SqlParameter ("@UserGroupLog",groupname),
                        new SqlParameter ("@PageNumber",PageNumber),
                 };

                    dt = await dbaccess.ExecuteDataTable(strconnection, "udp_app_finance_invoice_send_list", sqlParam);
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
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return dtlist;
        }

        /*

        public async Task<DataTable> dbupdatepayment(string filename, string Req_type, int recdata, string Datevalue, string reqID, int status, byte[] bytefile, string content_type, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.reqID = reqID;
                    model.filename = filename;
                    model.Req_type = Req_type;
                    model.recdata = recdata;
                    model.Datevalue = Datevalue;
                    model.status = status;
                    model.bytefile = bytefile;
                    model.content_type = content_type;
                    model.UserID = userid;
                    model.idcaption = moduleID;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetBillPaymentupd.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }

        public async Task<List<String>> dbGetPayListCount(string ClientIDS, string fromdate, string requestype, string status, int PageNumber, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.CrunchCiber = true;
                    model.SelectClient = ClientIDS;
                    model.fromdate = fromdate;
                    model.Req_type = requestype;
                    model.status = int.Parse(status);
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetPiutangPayList.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

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
        public async Task<List<DataTable>> dbGetPayList(DataTable DTFromDB, string ClientIDS, string fromdate, string requestype, string status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null)
            {
                string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(uril);
                    var login = new Dictionary<string, string>
                        {
                           {"grant_type", "password"},
                           {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                        };
                    var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resultJSON = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);
                        cFinance model = new cFinance();
                        model.CrunchCiber = false;
                        model.SelectClient = ClientIDS;
                        model.fromdate = fromdate;
                        model.Req_type = requestype;
                        model.status = int.Parse(status);
                        model.UserID = userid;
                        model.idcaption = idcaption;
                        model.GroupName = groupname;

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextFinance.cmdGetPiutangPayList.GetDescriptionEnums().ToString();
                        var responsed = client.PostAsync(cmdtextapi, content).Result;
                        if (responsed.IsSuccessStatusCode)
                        {
                            resultJSON = responsed.Content.ReadAsStringAsync().Result;
                            dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                        }
                    }
                }
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

        public async Task<DataTable> dbGetPayListTxtINV(string ClientIDS, string ID, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.ID = ID;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetBillPaymentupdtxt.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }
            return dt;
        }

        public async Task<List<String>> dbGetPayListCountBNI(string ClientIDS, string fromdate, string requestype, string status, int PageNumber, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.CrunchCiber = true;
                    model.SelectClient = ClientIDS;
                    model.fromdate = fromdate;
                    model.Req_type = requestype;
                    model.status = int.Parse(status);
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetPayListBNI.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

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
        public async Task<List<DataTable>> dbGetPayListBNI(DataTable DTFromDB, string ClientIDS, string fromdate, string requestype, string status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null)
            {
                string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(uril);
                    var login = new Dictionary<string, string>
                        {
                           {"grant_type", "password"},
                           {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                        };
                    var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resultJSON = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);
                        cFinance model = new cFinance();
                        model.CrunchCiber = false;
                        model.SelectClient = ClientIDS;
                        model.fromdate = fromdate;
                        model.Req_type = requestype;
                        model.status = int.Parse(status);
                        model.UserID = userid;
                        model.idcaption = idcaption;
                        model.GroupName = groupname;

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextFinance.cmdGetPayListBNI.GetDescriptionEnums().ToString();
                        var responsed = client.PostAsync(cmdtextapi, content).Result;
                        if (responsed.IsSuccessStatusCode)
                        {
                            resultJSON = responsed.Content.ReadAsStringAsync().Result;
                            dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                        }
                    }
                }
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

        public async Task<List<String>> dbGetListCountFakturRegis(string ClientIDS, string fromdate, string requestype, string nomor, string status, int PageNumber, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.CrunchCiber = true;
                    model.SelectClient = ClientIDS;
                    model.fromdate = fromdate;
                    model.Req_type = requestype;
                    model.NoPerjanjian = nomor;
                    model.status = int.Parse(status);
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetFakturRegisList.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

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
        public async Task<List<DataTable>> dbGetListFakturRegis(DataTable DTFromDB, string ClientIDS, string fromdate, string requestype, string nomor, string status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null)
            {
                string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(uril);
                    var login = new Dictionary<string, string>
                        {
                           {"grant_type", "password"},
                           {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                        };
                    var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resultJSON = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);
                        cFinance model = new cFinance();
                        model.CrunchCiber = false;
                        model.SelectClient = ClientIDS;
                        model.fromdate = fromdate;
                        model.Req_type = requestype;
                        model.NoPerjanjian = nomor;
                        model.status = int.Parse(status);
                        model.UserID = userid;
                        model.idcaption = idcaption;
                        model.GroupName = groupname;

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextFinance.cmdGetFakturRegisList.GetDescriptionEnums().ToString();
                        var responsed = client.PostAsync(cmdtextapi, content).Result;
                        if (responsed.IsSuccessStatusCode)
                        {
                            resultJSON = responsed.Content.ReadAsStringAsync().Result;
                            dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                        }
                    }
                }
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
        public async Task<DataTable> dbGetFakturRegisupd(string filename, string Req_type, int recdata, string Datevalue, string reqID, int status, byte[] bytefile, string content_type, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.reqID = reqID;
                    model.filename = filename;
                    model.Req_type = Req_type;
                    model.recdata = recdata;
                    model.Datevalue = Datevalue;
                    model.status = status;
                    model.bytefile = bytefile;
                    model.content_type = content_type;
                    model.UserID = userid;
                    model.idcaption = moduleID;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmddbGetFakturRegisupd.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }

        public async Task<DataTable> dbupdateJurnal(string filename, string Req_type, int recdata, string Datevalue, string reqID, int status, byte[] bytefile, string content_type, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.reqID = reqID;
                    model.filename = filename;
                    model.Req_type = Req_type;
                    model.recdata = recdata;
                    model.Datevalue = Datevalue;
                    model.status = status;
                    model.bytefile = bytefile;
                    model.content_type = content_type;
                    model.UserID = userid;
                    model.idcaption = moduleID;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetAccountingUpl.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }
        public async Task<List<String>> dbGetJurnalListCount(string ClientIDS, string fromdate, string requestype, string status, int PageNumber, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.CrunchCiber = true;
                    model.SelectClient = ClientIDS;
                    model.fromdate = fromdate;
                    model.Req_type = requestype;
                    model.status = int.Parse(status);
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetAccountingList.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

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
        public async Task<List<DataTable>> dbGetJurnalList(DataTable DTFromDB, string ClientIDS, string fromdate, string requestype, string status, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null)
            {
                string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(uril);
                    var login = new Dictionary<string, string>
                        {
                           {"grant_type", "password"},
                           {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                        };
                    var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resultJSON = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);
                        cFinance model = new cFinance();
                        model.CrunchCiber = false;
                        model.SelectClient = ClientIDS;
                        model.fromdate = fromdate;
                        model.Req_type = requestype;
                        model.status = int.Parse(status);
                        model.UserID = userid;
                        model.idcaption = idcaption;
                        model.GroupName = groupname;

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextFinance.cmdGetAccountingList.GetDescriptionEnums().ToString();
                        var responsed = client.PostAsync(cmdtextapi, content).Result;
                        if (responsed.IsSuccessStatusCode)
                        {
                            resultJSON = responsed.Content.ReadAsStringAsync().Result;
                            dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                        }
                    }
                }
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
        public async Task<string> dbsveJurnal(DataTable modeled, string periode, string Source, string userid, string groupname, string moduleID)
        {
            string resultInt = "";
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                        {
                           {"grant_type", "password"},
                           {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                        };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    string TableOrderAktaStr = JsonConvert.SerializeObject(modeled, Formatting.Indented);

                    cFinance model = new cFinance();
                    model.tablejurnal = TableOrderAktaStr;
                    model.fromdate = periode;
                    model.Source = Source;
                    model.UserID = userid;
                    model.idcaption = moduleID;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetAccountingJurnal.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultInt = responsed.Content.ReadAsAsync<string>().Result;
                    }
                }
            }
            return resultInt;
        }

        public async Task<int> dbgetcombinetxtbni(string datauplodid, bool CrunchCiber, string UserGroup, string securemoduleID, string userid)
        {
            int resultInt = 0;
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                        {
                           {"grant_type", "password"},
                           {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                        };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    var model = new Dictionary<string, string>
                        {
                           {"reqID",datauplodid},
                           {"idcaption", securemoduleID},
                           {"UserID", userid},
                           {"GroupName", UserGroup},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdgetcombinetxtbni.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        resultInt = int.Parse(resultJSON);
                    }
                }
            }
            return resultInt;
        }

        #region Report

        public async Task<DataTable> dbGetRptPiutangReg(string ClientIDS, string BranchIDS, string fromdate, string todate, string StatuspaidContract, string SelectJenisKontrak, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.SelectBranch = BranchIDS;
                    model.SelectClient = ClientIDS;
                    model.fromdate = fromdate;
                    model.todate = todate;
                    model.status = int.Parse(StatuspaidContract);
                    model.SelectJenisKontrak = SelectJenisKontrak;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetRptPiutangReg.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }
        public async Task<DataTable> dbGetRptClaimBaseReg(string ClientIDS, string BranchIDS, string fromdate, string todate, string SelectClaimBaseStatus, string SelectjenisKontract, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.SelectBranch = BranchIDS;
                    model.SelectClient = ClientIDS;
                    model.fromdate = fromdate;
                    model.todate = todate;
                    model.SelectJenisKontrak = SelectjenisKontract;
                    model.SelectClaimBaseStatus = SelectClaimBaseStatus;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetRptClaimBaseReg.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }
        public async Task<DataTable> dbGetRptBillingCreateReg(string ClientIDS, string BranchIDS, string fromdate, string todate, string NoPerjanjian, string DueDate, string RequestNo, string jeniskontrak, string idcaption, string userid, string groupname)
        {
            string cmdtextapi = cCommandTextFinance.cmdGetRptBillingCreateReg.GetDescriptionEnums().ToString();
            bool CrunchCiber = false;
            if (BranchIDS == "00F")
            {
                BranchIDS = "000";
                CrunchCiber = true;
            }

            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.CrunchCiber = CrunchCiber;
                    model.NoPerjanjian = NoPerjanjian;
                    model.SelectBranch = BranchIDS;
                    model.SelectClient = ClientIDS;
                    model.SelectJenisKontrak = jeniskontrak;
                    model.fromdate = fromdate;
                    model.todate = todate;
                    model.duedate = DueDate;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }
        public async Task<DataTable> dbGetRptBillingCreateDetailReg(string ClientIDS, string BranchIDS, string fromdate, string todate, string NoPerjanjian, string RequestNo, string jeniskontrak, string idcaption, string userid, string groupname, bool createinvoice = false)
        {
            string cmdtextapi = cCommandTextFinance.cmdGetRptBillingCreateRegDetail.GetDescriptionEnums().ToString();
            bool CrunchCiber = false;
            if (BranchIDS == "00F")
            {
                cmdtextapi = cCommandTextFinance.cmdGetRptBillingCreateRegDetail.GetDescriptionEnums().ToString();
                BranchIDS = "000";
                CrunchCiber = true;
            }

            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.CrunchCiber = CrunchCiber;
                    model.NoPerjanjian = NoPerjanjian;
                    model.SelectBranch = BranchIDS;
                    model.SelectClient = ClientIDS;
                    model.SelectJenisKontrak = jeniskontrak;
                    model.fromdate = fromdate;
                    model.todate = todate;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }
        public async Task<DataTable> dbGetRptBillIDRegAHU(string ClientIDS, string BranchIDS, string fromdate, string todate, string NoPerjanjian, string jeniskontrak, string idcaption, string userid, string groupname, bool createinvoice = false)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.NoPerjanjian = NoPerjanjian;
                    model.SelectBranch = BranchIDS;
                    model.SelectClient = ClientIDS;
                    model.SelectJenisKontrak = jeniskontrak;
                    model.fromdate = fromdate;
                    model.todate = todate;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetRptBillIDRegAHU.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }
        public async Task<string> dbGetRptBillIDCRET(string ClientIDS, string BranchIDS, string requestno, string jeniskontrak, string idcaption, string userid, string groupname, bool createinvoice = false)
        {
            string cmdtext = ""; // cCommandTextFinance.cmdGetRptBillCRET.GetDescriptionEnums().ToString();

            string result = "Tidak Ada Proses";
            //SqlParameter[] sqlParam =
            // {
            //            new SqlParameter ("@ClientId",ClientIDS),
            //            new SqlParameter ("@BranchId",BranchIDS),
            //            new SqlParameter ("@RequestNO",requestno),
            //            new SqlParameter ("@Cont_Type",jeniskontrak),
            //            new SqlParameter ("@moduleId",idcaption),
            //            new SqlParameter ("@UserIDLog",userid),
            //            new SqlParameter ("@UserGroupLog",groupname),
            //            new SqlParameter ("@result",SqlDbType.VarChar,130),
            //        };

            //sqlParam[7].Direction = ParameterDirection.Output;
            //SqlCommand commond = await dbaccess.ExecuteNonQueryWithOutput(strconnection, cmdtext, sqlParam);
            //result = commond.Parameters[7].Value.ToString();

            return result;
        }
        public async Task<DataTable> dbGetRptRugiLaba(string todate, bool isposting, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.CrunchCiber = isposting;
                    model.todate = todate;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetRptRlaba.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }
        public async Task<DataTable> dbGetRptNeraca(string todate, bool isposting, string idcaption, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

                    cFinance model = new cFinance();
                    model.todate = todate;
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextFinance.cmdGetRptNeraca.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                        dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                    }
                }
            }

            return dt;
        }

        #endregion Report

        */
    }
}