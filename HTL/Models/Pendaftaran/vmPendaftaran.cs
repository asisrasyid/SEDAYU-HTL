using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using HashNetFramework;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace DusColl
{
    [Serializable]
    public class vmPendaftaran
    {
        public cFilterContract DetailFilter { get; set; }
        public DataTable DTFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }
        public DataTable DTDetailForGridRowSelected { get; set; }
        public cpendaftaranOder Pendaftaranorderinput { get; set; }
        public cAccountMetrik Permission { get; set; }
    }


    [Serializable]
    public class vmPendaftaranddl
    {

        #region Application
        public async Task<List<String>> dbGetPendaftaranListCount(string ClientIDS, string BranchIDS, string fromdate, string todate, string StatusContract, string NoPerjanjian, int PageNumber, string idcaption, string userid, string groupname)
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

                    var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "true"},
                               {"NoPerjanjian", NoPerjanjian},
                               {"CONT_STATUS", StatusContract},
                               {"SelectBranch", BranchIDS},
                               {"SelectClient", ClientIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"PageNumber", PageNumber.ToString()},
                               {"idcaption",idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetPendaftaranList.GetDescriptionEnums().ToString();
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

        public async Task<List<DataTable>> dbGetPendaftaranList(DataTable DTFromDB, string ClientIDS, string BranchIDS, string fromdate, string todate, string StatusContract, string NoPerjanjian, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                        var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "false"},
                               {"NoPerjanjian", NoPerjanjian},
                               {"CONT_STATUS", StatusContract},
                               {"SelectBranch", BranchIDS},
                               {"SelectClient", ClientIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"PageNumber", PageNumber.ToString()},
                               {"idcaption",idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };
                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextPendaftaran.cmdGetPendaftaranList.GetDescriptionEnums().ToString();
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

        //public async Task<int> dbsvePendaftaranOrder(DataTable model, string clientID, string userid, string groupname, string moduleID)
        //{


        //    int result = 0;


        //    string cmdtext = cCommandTextPendaftaran.cmdGetPendaftaranOrdersve.GetDescriptionEnums().ToString();
        //    SqlParameter[] sqlParam =
        //    {

        //                new SqlParameter ("@tglOrder", DateTime.Now.ToString("yyyy-MM-dd")),
        //                new SqlParameter("@tableRegis", model),
        //                new SqlParameter ("@moduleID",moduleID),
        //                new SqlParameter ("@UserIDLog",userid),
        //                new SqlParameter ("@UserGroupLog",groupname),

        //    };

        //    //await Task.Delay(500);
        //    DataTable dt = await dbaccess.ExecuteDataTable(strconnection, cmdtext, sqlParam);

        //    result = int.Parse(dt.Rows[0][0].ToString());

        //    return result;

        //}


        //public async Task<DataTable> dbGetReOrderAHU(DataTable datauplod, string Jenisdoc, string ClientLogin, string Cabang, String UserLog, string UserGroup, string securemoduleID, string KeyEncrypt = "", string mail = "", string usergen = "")
        //{

        //    string moduleID = (securemoduleID); //HasKeyProtect.Decryption
        //    string ClientIDS = ClientLogin;

        //    string cmdtext = cCommandTextPendaftaran.cmdGetPendaftaranReOrderAHU.GetDescriptionEnums().ToString();

        //    SqlParameter[] sqlParam =
        //    {
        //        new SqlParameter ("@tableuplod",datauplod),
        //        new SqlParameter ("@clientID",ClientIDS),
        //        new SqlParameter ("@mail",mail),
        //        new SqlParameter ("@usergen",usergen),
        //        new SqlParameter ("@JenisDoc",Jenisdoc),
        //        new SqlParameter ("@moduleid",moduleID),
        //        new SqlParameter ("@UserIDLog",UserLog),
        //        new SqlParameter ("@UserGroupLog",UserGroup)

        //    };

        //    DataTable dt = await dbaccess.ExecuteDataTable(strconnection, cmdtext, sqlParam);

        //    return dt;
        //}


        public async Task<int> dbSettFlagToresenserty(DataTable datauplod, bool CrunchCiber, string UserGroup, string securemoduleID, string userid)
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

                    string TableOrderAktaStr = JsonConvert.SerializeObject(datauplod, Formatting.Indented);

                    var model = new Dictionary<string, string>
                        {
                           {"CrunchCiber",CrunchCiber.ToString()},
                           {"TableVariable", TableOrderAktaStr },
                           {"idcaption", securemoduleID},
                           {"UserID", userid},
                           {"GroupName", UserGroup},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetResendPendaftaran.GetDescriptionEnums().ToString();
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

        public async Task<int> dbSetToTemp4Manual(string cont_no, string cont_type, string client_fdc_id, string pnbp, string voucherno, string billid, string securemoduleID, string UserGroup, string userid)
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
                           {"CONT_NO",cont_no},
                           {"CONT_TYPE", cont_type},
                           {"CLIENT_FDC_ID", client_fdc_id},
                           {"BILL_ID_AHU", billid},
                           {"VOUCHER_AHU", voucherno},
                           {"AmtPNBP", pnbp},
                           {"idcaption", securemoduleID},
                           {"UserID", userid},
                           {"GroupName", UserGroup},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdSetPendaftaranMnl4Temp.GetDescriptionEnums().ToString();
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

        public async Task<int> dbSetToTemp4Paid(string cont_no, string cont_type, string client_fdc_id, string NotesBlock, string voucherno, string billid, string securemoduleID, string UserGroup, string userid)
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
                           {"CONT_NO",cont_no},
                           {"CONT_TYPE", cont_type},
                           {"CLIENT_FDC_ID", client_fdc_id},
                           {"BILL_ID_AHU", billid},
                           {"VOUCHER_AHU", voucherno},
                           {"SelectContractPaidStatusDesc", NotesBlock},
                           {"idcaption", securemoduleID},
                           {"UserID", userid},
                           {"GroupName", UserGroup},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdSetPendaftaranMnl4Temp.GetDescriptionEnums().ToString();
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


        public async Task<List<DataTable>> dbGetPendaftaranInfoRegList(DataTable DTFromDB, string ClientIDS, string NoPerjanjian, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                        var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "false"},
                               {"NoPerjanjian", NoPerjanjian},
                               {"SelectClient", ClientIDS},
                               {"PageNumber", PageNumber.ToString()},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };
                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextTrackingOrder.cmdGetTrackingOrderRegList.GetDescriptionEnums().ToString();
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


        #endregion Application

        #region Report 
        public async Task<DataTable> dbGetRptPendaftaran(string ClientIDS, string BranchIDS, string fromdate, string todate, string StatusContract, string NoPerjanjian, string idcaption, string userid, string groupname)
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

                    var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "true"},
                               {"NoPerjanjian", NoPerjanjian},
                               {"CONT_STATUS", StatusContract},
                               {"SelectBranch", BranchIDS},
                               {"SelectClient", ClientIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption",idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetRptPendaftaran.GetDescriptionEnums().ToString();
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

        //public async Task<DataTable> dbGetRptPemberkasan(string ClientIDS, string BranchIDS, string fromdate, string todate, string StatusContract, string NoPerjanjian, string idcaption, string userid, string groupname)
        //{

        //    string cmdtext = cCommandTextPendaftaran.cmdGetRptPendaftaran.GetDescriptionEnums().ToString();

        //    SqlParameter[] sqlParam =
        //    {
        //                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
        //                new SqlParameter ("@status_Perjanjian",StatusContract),
        //                new SqlParameter ("@kodecabang",BranchIDS),
        //                new SqlParameter ("@ClientID",ClientIDS),
        //                new SqlParameter ("@tgl_Perjanjianawal",fromdate),
        //                new SqlParameter ("@tgl_Perjanjianakhir",todate),
        //                new SqlParameter ("@moduleId",idcaption),
        //                new SqlParameter ("@UserIDLog",userid),
        //                new SqlParameter ("@UserGroupLog",groupname)
        //            };
        //    //await Task.Delay(500);
        //    DataTable dt = await dbaccess.ExecuteDataTable(strconnection, cmdtext, sqlParam);
        //    return dt;
        //}

        public async Task<DataTable> dbGetRptPendaftaranSLA(string ClientIDS, string jenispendaftaran, string isoverkontrak, string NoPerjanjian, string idcaption, string userid, string groupname)
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

                    var model = new Dictionary<string, string>
                            {
                               {"NoPerjanjian", NoPerjanjian},
                               {"SelectJenisKontrak",jenispendaftaran},
                               {"IS_OVER_CONTRAK",isoverkontrak},
                               {"SelectClient", ClientIDS},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetRptPendaftaranSLA.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetRptPendaftaranPNBPOut(string ClientIDS, string BranchIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
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

                    var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "false"},
                               {"SelectClient", ClientIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetRptPendaftaranPNBPOut.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetRptPendaftaranPNBPOutSummary(string ClientIDS, string BranchIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
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

                    var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "true"},
                               {"SelectClient", ClientIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetRptPendaftaranPNBPOut.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetRptPengirimanBekasPendaftaranYearTodate(string ClientIDS, string BranchIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
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

                    var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "true"},
                               {"SelectBranch", BranchIDS},
                               {"SelectClient", ClientIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetRptPengirimanBerkasPendaftaranMonthTodate.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetRptPengirimanBerkasPendaftaranMonthTodate(string ClientIDS, string BranchIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
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

                    var model = new Dictionary<string, string>
                            {
                               {"CrunchCiber", "false"},
                               {"SelectBranch", BranchIDS},
                               {"SelectClient", ClientIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendaftaran.cmdGetRptPengirimanBerkasPendaftaranMonthTodate.GetDescriptionEnums().ToString();
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

    }
}
