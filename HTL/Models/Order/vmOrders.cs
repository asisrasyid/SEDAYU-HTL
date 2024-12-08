using HashNetFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace DusColl
{
    [Serializable]
    public class vmOrders
    {
        [Required(ErrorMessage = "Pilih Nama Klien")]
        public string ClientId { get; set; }
        public string Region { get; set; }
        public string Cabang { get; set; }
        public string JenisKontrak { get; set; }
        public string TanggalOrder { get; set; }
        public string NotarisId { get; set; }
        [Required(ErrorMessage = "Jumlah harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public decimal JumlahOrder { get; set; }

        //public List<cCreateOrder> Orders { get; set; }

        public DataTable TableOrder { get; set; }
        public string[] Contracts { get; set; }

        public cFilterContract DetailFilter { get; set; }
        public cFilterContract DetailFilter1 { get; set; }

        public DataTable DTOrdersFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }

        public DataTable DTOrders1FromDB { get; set; }
        public DataTable DTDetail1ForGrid { get; set; }

        public DataTable DTSumaryOrderForGrid { get; set; }

        public DataTable DTOrdersCreateFromDB { get; set; }
        public DataTable DTDetailCreateForGrid { get; set; }

    }

    [Serializable]
    public class vmOrdersddl
    {



        public async Task<List<string>> dbGetOrderListCount(string ClientIDS, string NotaryIDS, string fromdate, string todate, string jeniskontrak, int PageNumber, string moduleID, string userid, string groupname)
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
                           {"SelectNotaris", NotaryIDS},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextOrder.cmdGetOrderList.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetOrderList(DataTable DTFromDB, string ClientIDS, string NotaryIDS, string fromdate, string todate, string jeniskontrak, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
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
                           {"SelectClient", ClientIDS},
                           {"SelectNotaris", NotaryIDS},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextOrder.cmdGetOrderList.GetDescriptionEnums().ToString();
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
            int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

            if (dt.Rows.Count > 0)
            {
                dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

            }
            dtlist.Add(dt);

            return dtlist;

        }
        public async Task<List<string>> dbGetDetailOrderCount(string clientId, string NotarisID, string TglOrder, int PageNumber, string moduleid, string userid, string groupname)
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
                           {"SelectClient", clientId},
                           {"SelectNotaris",NotarisID},
                           {"TglOrder", TglOrder},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextOrder.cmdGetDetailOrderList.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetDetailOrderList(DataTable DTFromDB, string clientId, string NotarisID, string TglOrder, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleid, string userid, string groupname)
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
                           {"SelectClient", clientId},
                           {"SelectNotaris",NotarisID},
                           {"TglOrder", TglOrder},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextOrder.cmdGetOrderList.GetDescriptionEnums().ToString();
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


        public async Task<DataTable> dbSearchOrderInfoCount(string clientid, int PageNumber, string moduleid, string userid, string groupname)
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
                           {"SelectClient", clientid},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextOrder.cmdSearchOrderInfoCount.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbShareOrderNotaris(string NotarisID, string fromdate,string todate, string moduleid, string userid, string groupname)
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
                           {"SelectNotaris", NotarisID},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextOrder.cmdShareOrderNotaris.GetDescriptionEnums().ToString();
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


        public async Task<List<string>> dbSearchOrderListCreateCount(string clientid, string RegionId, string cabangid, string jeniskontrak, int PageNumber, string moduleid, string userid, string groupname)
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
                           {"SelectClient", clientid},
                           {"SelectRegion", RegionId},
                           {"SelectBranch", cabangid},
                           {"SelectJenisKontrak", jeniskontrak},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextOrder.cmdSearchOrderListCreate.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbSearchOrderListCreate(DataTable DTFromDB, string clientid, string RegionId, string cabangid, string jeniskontrak, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleid, string userid, string groupname)
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
                           {"CrunchCiber", "true"},
                           {"SelectClient", clientid},
                           {"SelectRegion", RegionId},
                           {"SelectBranch", cabangid},
                           {"SelectJenisKontrak", jeniskontrak},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextOrder.cmdSearchOrderListCreate.GetDescriptionEnums().ToString();
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
        public async Task<int> dbSaveOrder(DataTable TableOrder, string ClientId, string region, string SelectJenisKontrak, string UserID, string GroupName)
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

                    string TableOrderAktaStr = JsonConvert.SerializeObject(TableOrder, Formatting.Indented);

                    var model = new Dictionary<string, string>
                        {
                           {"TableVariable", TableOrderAktaStr },
                           {"SelectClient", ClientId},
                           {"SelectRegion", region},
                           {"SelectJenisKontrak", SelectJenisKontrak},
                           {"UserID", UserID},
                           {"GroupName", GroupName},
                        };


                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextOrder.cmdSaveOrder.GetDescriptionEnums().ToString();
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


        public DataTable CreateTableOrder()
        {


            DataTable TableOrder = new DataTable("TableOrder");

            DataColumn CONT_TYPE = new DataColumn("CONT_TYPE");
            CONT_TYPE.DataType = System.Type.GetType("System.Int32");
            TableOrder.Columns.Add(CONT_TYPE);

            DataColumn CLIENT_FDC_ID = new DataColumn("CLIENT_FDC_ID");
            CLIENT_FDC_ID.DataType = System.Type.GetType("System.Int64");
            TableOrder.Columns.Add(CLIENT_FDC_ID);


            DataColumn NoPerjanjian = new DataColumn("NoPerjanjian");
            NoPerjanjian.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(NoPerjanjian);

            DataColumn TglPerjanjian = new DataColumn("TglPerjanjian");
            TglPerjanjian.DataType = System.Type.GetType("System.DateTime");
            TableOrder.Columns.Add(TglPerjanjian);

            DataColumn KodeCabang = new DataColumn("KodeCabang");
            KodeCabang.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(KodeCabang);

            DataColumn NamaCabang = new DataColumn("NamaCabang");
            NamaCabang.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(NamaCabang);

            DataColumn JenisNasabah = new DataColumn("JenisNasabah");
            JenisNasabah.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(JenisNasabah);

            DataColumn NamaNasabah = new DataColumn("NamaNasabah");
            NamaNasabah.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(NamaNasabah);

            DataColumn ClientId = new DataColumn("ClientId");
            ClientId.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(ClientId);

            DataColumn NotarisId = new DataColumn("NotarisId");
            NotarisId.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(NotarisId);

            DataColumn User = new DataColumn("User");
            User.DataType = System.Type.GetType("System.String");
            TableOrder.Columns.Add(User);

            DataColumn SEND_CLIENT_DATE = new DataColumn("SEND_CLIENT_DATE");
            SEND_CLIENT_DATE.DataType = System.Type.GetType("System.DateTime");
            TableOrder.Columns.Add(SEND_CLIENT_DATE);

            return TableOrder;
        }



        ////public async Task<string> dbCancelOrder(string noOrder, string userId)
        ////{
        ////    await Task.Delay(500);

        ////    SqlParameter[] param = { new SqlParameter("@NoOrder", noOrder), new SqlParameter("@UserId", userId), new SqlParameter("@msg", SqlDbType.VarChar, 30) { Direction = ParameterDirection.Output } };
        ////    string msg = string.Empty;
        ////    try
        ////    {

        ////        SqlCommand commond = await dbaccess.ExecuteNonQueryWithOutput(strconnection, "udp_app_contracts_order_cancel", param);
        ////        msg = commond.Parameters[2].Value.ToString();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        msg = ex.Message;
        ////    }

        ////    return msg;
        ////}



    }


}

