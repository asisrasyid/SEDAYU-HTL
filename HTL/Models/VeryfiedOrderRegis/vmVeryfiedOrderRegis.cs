using HashNetFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DusColl
{


    [Serializable]
    public class vmVeryfiedOrderRegis
    {
        public string CheckWithKey { get; set; }
        public string securemoduleID { get; set; }
        public cFilterContract DetailFilter { get; set; }
        public DataTable DTOrdersFromDBSMRY { get; set; }
        public DataTable DTOrdersFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }
        public DataTable DTDetailForGridRowSelected { get; set; }

    }


    [Serializable]
    public class vmVeryfiedOrderRegisddl
    {

        public async Task<List<String>> dbGetTrackingOrderRegveryfiedListCount(string ClientIDS, string NoPerjanjian, string fromdate, string todate, string SelectJenCek, int PageNumber, string idcaption, string userid, string groupname)
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
                           {"SelectClient", ClientIDS},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"SelectJenCek", SelectJenCek},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", idcaption},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextVeryfiedOrderRegis.cmdGetVeryfiedOrderRegList.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetTrackingOrderRegveryfiedList(DataTable DTFromDB, string ClientIDS, string NoPerjanjian, string fromdate, string todate, string SelectJenCek, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                           {"NoPerjanjian", NoPerjanjian},
                           {"SelectClient", ClientIDS},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"SelectJenCek", SelectJenCek},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", idcaption},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };
                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextVeryfiedOrderRegis.cmdGetVeryfiedOrderRegList.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetTrackingOrderRegveryfiedListDash(DataTable DTFromDB, string ClientIDS, string NoPerjanjian, string fromdate, string todate, string SelectJenCek, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                           {"SelectClient", ClientIDS},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", idcaption},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };
                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextVeryfiedOrderRegis.cmdGetVeryfiedOrderRegListdsh.GetDescriptionEnums().ToString();
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
        public DataTable dbGetVerifikasiExport(string ClientIDS, string NoPerjanjian, string fromdate, string todate, string SelectJenCek, int PageNumber, string idcaption, string userid, string groupname)
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
                           {"SelectClient", ClientIDS},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"SelectJenCek", SelectJenCek},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", idcaption},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextVeryfiedOrderRegis.cmdGetVeryfiedExport.GetDescriptionEnums().ToString();
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


        public async Task<int> dbupdateVerifikasi(DataTable tabledata, bool verified,bool validdata, string moduleID, string userid, string groupname)
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

                    string TableOrderAktaStr = JsonConvert.SerializeObject(tabledata, Formatting.Indented);

                    var model = new Dictionary<string, string>
                        {

                        {"TableVariable",TableOrderAktaStr},
                        {"isverified",verified.ToString()},
                        {"iscontnotvalided",validdata.ToString()},
                        {"idcaption",moduleID },
                        {"UserID", userid},
                        {"GroupName", groupname},
                   };


                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextVeryfiedOrderRegis.cmdGetVeryfiedupd.GetDescriptionEnums().ToString();
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
    }

}