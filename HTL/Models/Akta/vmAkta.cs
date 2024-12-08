using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HashNetFramework;
using Newtonsoft.Json;

namespace DusColl
{
    [Serializable]
    public class vmAkta
    {

        public cFilterContract DetailFilter { get; set; }

        public DataTable DTAktaFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }

        public DataTable DTAktaCreateFromDB { get; set; }
        public DataTable DTDetailCreateForGrid { get; set; }

        public string[] AktaSelect { get; set; }


    }

    [Serializable]
    public class vmAktaddl
    {
      
        #region Application

        public async Task<cAkta> dbGetOrderOutstandingCount(string notaryid)
        {


            cAkta varDetailAkta = new cAkta();
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
                           {"NotarisID", notaryid},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdGetOrderOutstandingCount.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        varDetailAkta = responsed.Content.ReadAsAsync<cAkta>().Result;

                    }
                }
            }

            return varDetailAkta;

        }

        public async Task<List<string>> dbGetAktaListCreateCount(string SelectNotaris, string TglAkta, string NoAkta, string pukulakta, string tglOrder, int OutstandingOrder, int JedaMenitAkta, string NoPerjanjian, bool IsFleet, int PageNumber, string moduleID, string userid, string groupname)
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
                            {"NotarisID", SelectNotaris},
                            {"TglAkta", TglAkta},
                            {"Pukul_Akta", pukulakta},
                            {"Tgl_order", tglOrder},
                            {"OutstandingOrder", OutstandingOrder.ToString()},
                            {"JedaMenitAkta", JedaMenitAkta.ToString()},
                            {"NoPerjanjian", NoPerjanjian},
                            {"No_Akta", NoAkta},
                            {"IsFleet", IsFleet.ToString()},
                            {"PageNumber", PageNumber.ToString()},
                            {"idcaption", moduleID},
                            {"UserID", userid},
                            {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdGetAktaListCreate.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetAktaListCreate(DataTable DTFromDB, string SelectNotaris, string TglAkta, string NoAkta, string pukulakta, string tglOrder, int OutstandingOrder, int JedaMenitAkta, string NoPerjanjian, bool IsFleet, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {

            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
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
                            {"NotarisID", SelectNotaris},
                            {"TglAkta", TglAkta},
                            {"Pukul_Akta", pukulakta},
                            {"Tgl_order", tglOrder},
                            {"OutstandingOrder", OutstandingOrder.ToString()},
                            {"JedaMenitAkta", JedaMenitAkta.ToString()},
                            {"NoPerjanjian", NoPerjanjian},
                            {"No_Akta", NoAkta},
                            {"IsFleet", IsFleet.ToString()},
                            {"PageNumber", PageNumber.ToString()},
                            {"idcaption", moduleID},
                            {"UserID", userid},
                            {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextAkta.cmdGetAktaListCreate.GetDescriptionEnums().ToString();
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

        public async Task<List<string>> dbGetAktaListCount(string ClientIDS, string BranchIDS, string NotaryIDS, string fromdate, string todate, string NoAkta, string NoPerjanjian, int PageNumber, string idcaption, string userid, string groupname)
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
                            {"fromdate", fromdate},
                            {"todate", todate},
                            {"NoAkta", NoAkta},
                            {"SelectClient", ClientIDS},
                            {"SelectBranch", BranchIDS},
                            {"SelectNotaris", NotaryIDS},
                            {"PageNumber", PageNumber.ToString()},
                            {"idcaption", idcaption},
                            {"UserID", userid},
                            {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdGetAktaList.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetAktaList(DataTable DTFromDB, string ClientIDS, string BranchIDS, string NotaryIDS, string fromdate, string todate, string NoAkta, string NoPerjanjian, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        {



            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
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
                            {"fromdate", fromdate},
                            {"todate", todate},
                            {"NoAkta", NoAkta},
                            {"SelectClient", ClientIDS},
                            {"SelectBranch", BranchIDS},
                            {"SelectNotaris", NotaryIDS},
                            {"PageNumber", PageNumber.ToString()},
                            {"idcaption", idcaption},
                            {"UserID", userid},
                            {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextAkta.cmdGetAktaList.GetDescriptionEnums().ToString();
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


        public async Task<DataTable> CreateTableAkta()
        {
            await Task.Delay(1);

            DataTable TableOrderAkta = new DataTable();
            TableOrderAkta = new DataTable("TableOrderAkta");

            DataColumn CONT_TYPE = new DataColumn("CONT_TYPE");
            CONT_TYPE.DataType = System.Type.GetType("System.Int32");
            TableOrderAkta.Columns.Add(CONT_TYPE);

            DataColumn CLIENT_FDC_ID = new DataColumn("CLIENT_FDC_ID");
            CLIENT_FDC_ID.DataType = System.Type.GetType("System.Int64");
            TableOrderAkta.Columns.Add(CLIENT_FDC_ID);

            DataColumn CONT_NO = new DataColumn("CONT_NO");
            CONT_NO.DataType = System.Type.GetType("System.String");
            TableOrderAkta.Columns.Add(CONT_NO);

            DataColumn DEED_DATE = new DataColumn("DEED_DATE");
            DEED_DATE.DataType = System.Type.GetType("System.DateTime");
            TableOrderAkta.Columns.Add(DEED_DATE);

            DataColumn DEED_NO = new DataColumn("DEED_NO");
            DEED_NO.DataType = System.Type.GetType("System.String");
            TableOrderAkta.Columns.Add(DEED_NO);

            DataColumn DEED_CODE = new DataColumn("DEED_CODE");
            DEED_CODE.DataType = System.Type.GetType("System.String");
            TableOrderAkta.Columns.Add(DEED_CODE);

            DataColumn DEED_TIME = new DataColumn("DEED_TIME");
            DEED_TIME.DataType = System.Type.GetType("System.TimeSpan");
            TableOrderAkta.Columns.Add(DEED_TIME);

            DataColumn CLNT_ID = new DataColumn("CLNT_ID");
            CLNT_ID.DataType = System.Type.GetType("System.String");
            TableOrderAkta.Columns.Add(CLNT_ID);

            DataColumn NTRY_ID = new DataColumn("NTRY_ID");
            NTRY_ID.DataType = System.Type.GetType("System.String");
            TableOrderAkta.Columns.Add(NTRY_ID);

            DataColumn SEND_CLIENT_DATE = new DataColumn("SEND_CLIENT_DATE");
            SEND_CLIENT_DATE.DataType = System.Type.GetType("System.DateTime");
            TableOrderAkta.Columns.Add(SEND_CLIENT_DATE);

            return TableOrderAkta;
        }
        public async Task<int> dbSaveAkta(DataTable TableOrderAkta, string UserID, string GroupName)
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

                    string TableOrderAktaStr = JsonConvert.SerializeObject(TableOrderAkta, Formatting.Indented);

                    var model = new Dictionary<string, string>
                        {
                           {"TableOrderAktaStr", TableOrderAktaStr },
                           {"UserID", UserID},
                           {"GroupName", GroupName},
                        };


                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdSaveAkta.GetDescriptionEnums().ToString();
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
        public async Task<string> dbSaveAktaValid(DataTable TableOrderAkta, bool IsFleet)
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

                    string TableOrderAktaStr = JsonConvert.SerializeObject(TableOrderAkta, Formatting.Indented);

                    var model = new Dictionary<string, string>
                        {
                           {"TableOrderAktaStr", TableOrderAktaStr },
                           {"IsFleet", IsFleet.ToString()},
                        };


                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdSaveAktavalid.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultInt = responsed.Content.ReadAsAsync<string>().Result;
                    }
                }
            }
            return resultInt;

        }

        #endregion Application

        #region Report
        public async Task<DataTable> dbGetRptAktaUsedSummary(string ClientIDS, string NotaryIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
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
                               {"SelectClient", ClientIDS },
                               {"NotarisID", NotaryIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdGetRptAktaUsedSummary.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetRptAktaUsedDetail(string ClientIDS, string NotaryIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
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
                               {"SelectClient", ClientIDS },
                               {"NotarisID", NotaryIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdGetRptAktaDetailBLN.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetRptAktaBLN(string ClientIDS, string NotaryIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
        {

            fromdate = "01-" + fromdate;

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
                               {"NotarisID", NotaryIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdGetRptAktaBLN.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetRptAktaTaxNtry(string ClientIDS, string NotaryIDS, string fromdate, string todate, string idcaption, string userid, string groupname)
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
                               {"NotarisID", NotaryIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextAkta.cmdGetRptAktaTaxNtry.GetDescriptionEnums().ToString();
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

