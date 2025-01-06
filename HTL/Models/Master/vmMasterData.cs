using HashNetFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DusColl
{
    [Serializable]
    public class vmMasterData
    {
        public cFilterMasterData MasterFilter { get; set; }
        public DataTable DTFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }
    }

    [Serializable]
    public class vmMasterDataddl
    {
        //public async Task<List<string>> dbGeProvinListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"keyword", keyword},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetProvinList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }
        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetProvinList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"keyword", keyword},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetProvinList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public async Task<int> dbupdateProvin(int ID, string Provinsi, string Kota, string kecamatan, string kelurahan, string zipcode, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"Provinsi", Provinsi},
        //                   {"Kota", Kota},
        //                   {"Kecamatan", kecamatan},
        //                   {"Kelurahan", kelurahan},
        //                   {"ZipCode", zipcode},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSaveProvin.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;
        //}

        //public async Task<List<string>> dbGeProvinAhuListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"keyword", keyword},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetProvinAhuList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetProvinAhuList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"keyword", keyword},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetProvinAhuList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public async Task<int> dbupdateProvinAhu(int ID, string Provinsi, string Kota, string AliasKota, string kecamatan, string Aliaskecamatan, string PengadilanNegeri, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"Provinsi", Provinsi},
        //                   {"Kota", Kota},
        //                   {"Kecamatan", kecamatan},
        //                   {"AliasKota", AliasKota},
        //                   {"AliasKecamatan", Aliaskecamatan},
        //                   {"PengadilanNegeri", PengadilanNegeri},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSaveProvinAhu.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        //public async Task<List<string>> dbGeGeSharePICListCount(string SelectUserID, string regionid, string conttype, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"Isdownload","false"},
        //                   {"SelectUserID", SelectUserID??""},
        //                   {"SelectRegion", regionid??""},
        //                   {"SelectJenisKontrak", conttype??""},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetSharePICList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetGeSharePICList(DataTable DTFromDB, string SelectUserID, string regionid, string conttype, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"Isdownload","false"},
        //                   {"SelectUserID", SelectUserID},
        //                   {"SelectRegion", regionid},
        //                   {"SelectJenisKontrak", conttype},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetSharePICList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }
        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public DataTable dbGetSharePICListExport(string SelectUserID, string regionid, string conttype, int PageNumber, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"Isdownload","true"},
        //                   {"SelectUserID", SelectUserID},
        //                   {"SelectRegion", regionid},
        //                   {"SelectJenisKontrak", conttype},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", idcaption},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetSharePICList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    return dt;
        //}
        //public async Task<int> dbupdateSharePIC(int ID, string SelectUserID, string regionid, string branch, string conttype, string persentase, bool active, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                    {"ID", ID.ToString()},
        //                    {"SelectUserID", SelectUserID},
        //                    {"RegionID", regionid},
        //                    {"Cabang", branch},
        //                    {"JenisKontrak", conttype},
        //                    {"persentase", persentase},
        //                    {"actived", active.ToString()},
        //                    {"moduleID", moduleID},
        //                    {"UserID", userid},
        //                    {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSaveSharePIC.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}
        //public async Task<int> dbdeleteSharePIC(int ID, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdDelSharePIC.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        //public async Task<List<string>> dbGeGeShareNotarisListCount(string ntryid, string regionid, string conttype, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"Isdownload","false"},
        //                   {"SelectNotaris", ntryid??""},
        //                   {"SelectRegion", regionid??""},
        //                   {"SelectJenisKontrak", conttype??""},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetShareNotarisList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetGeShareNotarisList(DataTable DTFromDB, string ntryid, string regionid, string conttype, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"Isdownload","false"},
        //                   {"SelectNotaris", ntryid},
        //                   {"SelectRegion", regionid},
        //                   {"SelectJenisKontrak", conttype},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetShareNotarisList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }
        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public DataTable dbGetShareNotarisListExport(string ntryid, string regionid, string conttype, int PageNumber, string idcaption, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"Isdownload","true"},
        //                   {"SelectNotaris", ntryid},
        //                   {"SelectRegion", regionid},
        //                   {"SelectJenisKontrak", conttype},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", idcaption},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetShareNotarisList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    return dt;
        //}
        //public async Task<int> dbupdateShareNotaris(int ID, string ntryid, string ntryidbckp, string regionid, string branch, string conttype, string persentase, bool active, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                    {"ID", ID.ToString()},
        //                    {"NotarisID", ntryid},
        //                    {"NotarisIDBCKP", ntryidbckp},
        //                    {"RegionID", regionid},
        //                    {"Cabang", branch},
        //                    {"JenisKontrak", conttype},
        //                    {"persentase", persentase},
        //                    {"actived", active.ToString()},
        //                    {"moduleID", moduleID},
        //                    {"UserID", userid},
        //                    {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSaveShareNotaris.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}
        //public async Task<int> dbdeleteShareNotaris(int ID, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdDelShareNotaris.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        //public async Task<List<string>> dbGetNotarisListCount(string ntryid, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetNotarisList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetNotarisList(DataTable DTFromDB, string ntryid, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetNotarisList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public async Task<int> dbupdateNotaris(int ID, string NTRY_ID, string NTRY_NAME, string NTRY_REGION, string NTRY_SK_NO, string NTRY_SK_DATE,
        //                                        string NTRY_ADDRESS, string NTRY_EMAIL, string NTRY_DOMICILE, string NTRY_NAME_REG_AHU, string NTRY_ID_AHU, string NTRY_ID_WILAYAH_AHU,
        //                                        string NTRY_WILAYAH_AHU, bool Actived, string feentry, string NTRY_MAX_DEED, string moduleID, string userid, string groupname
        //                                        )
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"NotarisID", NTRY_ID},
        //                   {"NotarisNama", NTRY_NAME},
        //                   {"RegionID", NTRY_REGION},
        //                   {"NTRY_SK_NO", NTRY_SK_NO},
        //                   {"NTRY_SK_DATE", NTRY_SK_DATE},
        //                   {"NTRY_ADDRESS", NTRY_ADDRESS},
        //                   {"NTRY_EMAIL", NTRY_EMAIL},
        //                   {"NTRY_DOMICILE", NTRY_DOMICILE},
        //                   {"NTRY_NAME_REG_AHU", NTRY_NAME_REG_AHU},
        //                   {"NTRY_ID_AHU", NTRY_ID_AHU},
        //                   {"NTRY_ID_WILAYAH_AHU", NTRY_ID_WILAYAH_AHU},
        //                   {"NTRY_WILAYAH_AHU", NTRY_WILAYAH_AHU},
        //                   {"NTRY_MAX_DEED",NTRY_MAX_DEED },
        //                   {"NTRY_FEE", feentry},
        //                   {"actived", Actived.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSaveNotaris.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        //public async Task<int> dbdeleteNotaris(int ID, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdDelNotaris.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        //public async Task<List<string>> dbGetNotarisStaffListCount(string ntryid, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetNotarisStaffList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetNotarisStaffList(DataTable DTFromDB, string ntryid, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetNotarisStaffList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public async Task<int> dbupdateNotarisStaff(int ID, string idnotaris, string StaffName, string BirthPlace, string BirthDate, string City,
        //                                        string Address, string NeighborHood_No, string Hamlet_No, string UrbanVillage, string SubDistrict,
        //                                        string Identity_No, string StartDate, string EndDate, string moduleID, string userid, string groupname
        //                                        )
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"NotarisID", idnotaris},
        //                   {"StaffName", StaffName},
        //                   {"BirthPlace", BirthPlace},
        //                   {"BirthDate", BirthDate},
        //                   {"City", City},
        //                   {"Address", Address},
        //                   {"NeighborHood_No", NeighborHood_No},
        //                   {"Hamlet_No", Hamlet_No},
        //                   {"UrbanVillage", UrbanVillage},
        //                   {"SubDistrict", SubDistrict},
        //                   {"Identity_No", Identity_No},
        //                   {"StartDate", StartDate},
        //                   {"EndDate", EndDate},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSaveNotarisStaff.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        //public async Task<int> dbdeleteNotarisStaff(int ID, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdDelNotarisStaff.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;
        //}

        //public async Task<List<string>> dbGetPenghadapPTListCount(string ntryid, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetPenghadapPTList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetPenghadapPTList(DataTable DTFromDB, string ntryid, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetPenghadapPTList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public async Task<int> dbupdatePenghadapPT(int ID, string idpenghadap, string Name, string BirthPlace, string BirthDate, string City, string jobtitle,
        //                                        string Address, string NeighborHood_No, string Hamlet_No, string UrbanVillage, string SubDistrict, string citizen,
        //                                        string Identity_Type, string Identity_No, bool Is_Active, string cabang, string StartDate, string EndDate,
        //                                        string provinsi, string tglsk, string nosk, string jeniskelamin, string moduleID, string userid, string groupname
        //                                        )
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"SIGNATURE_CODE", idpenghadap},
        //                   {"FULL_NAME", Name},
        //                   {"JOB_TITLE", jobtitle},
        //                   {"PLACE_BIRTH", BirthPlace},
        //                   {"DATE_BIRTH", BirthDate},
        //                   {"IDENTITY_TYPE", Identity_Type},
        //                   {"IDENTITY_NO", Identity_No},
        //                   {"CITIZENSHIP", citizen},
        //                   {"Address", Address},
        //                   {"RT", NeighborHood_No},
        //                   {"RW", Hamlet_No},
        //                   {"KELURAHAN", UrbanVillage},
        //                   {"KECAMATAN", SubDistrict},
        //                   {"City", City},
        //                   {"Is_Active", Is_Active.ToString()},
        //                   {"CabangSelect", cabang},
        //                   {"StartDate", StartDate},
        //                   {"EndDate", EndDate},
        //                   {"Province", provinsi??""},
        //                   {"TglSK", tglsk??""},
        //                   {"NoSK", nosk??""},
        //                   {"JenisKelamin",jeniskelamin??""},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSavePenghadapPT.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}
        //public async Task<int> dbdeletePenghadapPT(int ID, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdDelPenghadapPT.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        //public async Task<List<string>> dbGetKlienListCount(string ntryid, int PageNumber, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "true"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdGetklienList.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //            }
        //        }
        //    }

        //    List<String> dta = new List<string>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        dta.Add(dt.Rows[0][0].ToString());
        //        dta.Add(dt.Rows[0][1].ToString());
        //        dta.Add(dt.Rows[0][2].ToString());
        //    }
        //    else
        //    {
        //        dta.Add("0");
        //        dta.Add("0");
        //        dta.Add("0");
        //    }
        //    return dta;

        //}
        //public async Task<List<DataTable>> dbGetKlienList(DataTable DTFromDB, string ntryid, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        //{
        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null || DTFromDB.Rows.Count == 0)
        //    {
        //        string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri(uril);
        //            var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //            var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultJSON = response.Content.ReadAsStringAsync().Result;
        //                var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //                var model = new Dictionary<string, string>
        //                {
        //                   {"CrunchCiber", "false"},
        //                   {"keyword", ntryid},
        //                   {"PageNumber", PageNumber.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //                var stringPayload = JsonConvert.SerializeObject(model);
        //                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //                string cmdtextapi = cCommandTextMaster.cmdGetklienList.GetDescriptionEnums().ToString();
        //                var responsed = client.PostAsync(cmdtextapi, content).Result;
        //                if (responsed.IsSuccessStatusCode)
        //                {
        //                    resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                    dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dt = DTFromDB;
        //    }

        //    dtlist.Add(dt);
        //    int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();

        //    }
        //    dtlist.Add(dt);

        //    return dtlist;

        //}
        //public async Task<int> dbupdateKlien(int ID, string CLNT_ID, string CLNT_NAME, string CLNT_ADDRESS, string CLNT_TYPE, string CLNT_CONTACT_NO,
        //                                        string CLNT_NPWP_NIK_SK, string CLNT_POSTCODE, string CLNT_PROVINCE, string CLNT_CITY, string CLNT_SUBDISTRICT,
        //                                        string CLNT_URBAN_VILLAGE, string CLNT_HAMLET_NO, string CLNT_NEIGHBOORHOOD_NO,
        //                                        string CLNT_PROVINCE_ID_AHU, string CLNT_CITY_ID_AHU, bool actived, string moduleID, string userid, string groupname
        //                                        )
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"CLNT_ID", CLNT_ID},
        //                   {"CLNT_NAME", CLNT_NAME},
        //                   {"CLNT_ADDRESS", CLNT_ADDRESS},
        //                   {"CLNT_TYPE", CLNT_TYPE},
        //                   {"CLNT_CONTACT_NO", CLNT_CONTACT_NO},
        //                   {"CLNT_NPWP_NIK_SK", CLNT_NPWP_NIK_SK},
        //                   {"CLNT_POSTCODE", CLNT_POSTCODE},
        //                   {"CLNT_PROVINCE", CLNT_PROVINCE},
        //                   {"CLNT_CITY", CLNT_CITY},
        //                   {"CLNT_SUBDISTRICT", CLNT_SUBDISTRICT},
        //                   {"CLNT_URBAN_VILLAGE", CLNT_URBAN_VILLAGE},
        //                   {"CLNT_HAMLET_NO", CLNT_HAMLET_NO},
        //                   {"CLNT_NEIGHBOORHOOD_NO", CLNT_NEIGHBOORHOOD_NO},
        //                   {"CLNT_PROVINCE_ID_AHU", CLNT_PROVINCE_ID_AHU},
        //                   {"CLNT_CITY_ID_AHU", CLNT_CITY_ID_AHU},
        //                   {"actived", actived.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdSaveklien.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;
        //}

        //public async Task<int> dbdeleteKlien(int ID, string moduleID, string userid, string groupname)
        //{
        //    int resultInt = 0;
        //    DataTable dt = new DataTable();
        //    string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(uril);
        //        var login = new Dictionary<string, string>
        //                {
        //                   {"grant_type", "password"},
        //                   {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
        //                };
        //        var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var resultJSON = response.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

        //            var model = new Dictionary<string, string>
        //                {
        //                   {"ID", ID.ToString()},
        //                   {"moduleID", moduleID},
        //                   {"UserID", userid},
        //                   {"GroupName", groupname},
        //                };

        //            var stringPayload = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //            string cmdtextapi = cCommandTextMaster.cmdDelklien.GetDescriptionEnums().ToString();
        //            var responsed = client.PostAsync(cmdtextapi, content).Result;
        //            if (responsed.IsSuccessStatusCode)
        //            {
        //                resultJSON = responsed.Content.ReadAsStringAsync().Result;
        //                resultInt = int.Parse(resultJSON);
        //            }
        //        }
        //    }
        //    return resultInt;

        //}

        public async Task<List<string>> dbGetCabangListCount(string keyword, bool IsActive, bool IsPusat, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@IsActive",IsActive),
                    new SqlParameter ("@IsPusat",IsPusat),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_branch_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetCabangList(DataTable DTFromDB, string keyword, bool IsActive, bool IsPusat, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter("@IsActive", IsActive),
                    new SqlParameter("@IsPusat", IsPusat),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_branch_list", sqlParam);
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

        public async Task<int> dbupdateCabang(int ID, string REGION, string BRCH_ID, string BRCH_CODE, string BRCH_NAME, string PIC, bool IsPusat, bool IsActive, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@REGION", REGION),
                            new SqlParameter("@BRCH_NAME", BRCH_NAME),
                            new SqlParameter("@BRCH_CODE", BRCH_CODE),
                            new SqlParameter("@PIC", PIC),
                            new SqlParameter("@ISPUSAT", IsPusat),
                            new SqlParameter("@IsActive", IsActive),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_branch_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteCabang(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_branch_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetBankNotListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_banknot_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetBankNotList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_banknot_list", sqlParam);
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

        public async Task<int> dbupdateBankNOT(int ID, string notarisID, string PemilikRekening, string NoRekening, string NamaBank,
            string CabangBank, string KodeInv, string PRC_CHECK, string PRC_SKMHT, string PRC_APHT, string PRC_CNLBAKD,
            string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@ppatID", notarisID),
                            new SqlParameter("@PemilikRekening", PemilikRekening),
                            new SqlParameter("@NoRekening", NoRekening),
                            new SqlParameter("@NamaBank", NamaBank),
                            new SqlParameter("@CabangBank", CabangBank),
                            new SqlParameter("@initinv", KodeInv),
                            new SqlParameter("@PRC_CHECK", PRC_CHECK),
                            new SqlParameter("@PRC_SKMHT", PRC_SKMHT),
                            new SqlParameter("@PRC_APHT", PRC_APHT),
                            new SqlParameter("@PRC_CNLBAKD", PRC_CNLBAKD),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_banknot_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteBankNot(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_banknot_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetSchInvNotListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_SchInvNot_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetSchInvNotList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_SchInvNot_list", sqlParam);
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

        public async Task<int> dbupdateSchInvNot(int ID, string notarisID, string TglInv,
            string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@ppatID", notarisID),
                            new SqlParameter("@TglInv", TglInv),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_SchInvNot_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteSchInvNot(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_SchInvNot_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetNotRgnListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_notrgn_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetNotRgnList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_notrgn_list", sqlParam);
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

        public async Task<int> dbupdateNOTRgn(int ID, string notarisID, string wilayah, bool allowd,
            string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@ppatID", notarisID),
                            new SqlParameter("@wilayah", wilayah),
                            new SqlParameter("@allow", allowd),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_notrgn_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteNotRgn(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_notrgn_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetLogCommentListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_LogComment_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetLogCommentList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_LogComment_list", sqlParam);
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

        public async Task<int> dbupdateLogComment(int ID, string statusNew,
            string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@Status", statusNew),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_LogComment_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteLogComment(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_LogComment_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetAccCabangListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acccabang_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetAccCabangList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acccabang_list", sqlParam);
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

        public async Task<int> dbupdateAccCabang(int ID, string UserIDCab, string TglInv, bool allowed,
            string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@UserID", UserIDCab),
                            new SqlParameter("@wilayah", TglInv),
                            new SqlParameter("@allow",allowed ),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acccabang_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteAccCabang(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acccabang_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetAcctGrpListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acctgrp_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetAcctGrpList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@keycode", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acctgrp_list", sqlParam);
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

        public async Task<int> dbupdateAcctGrp(int ID, string usrid, string grpnme,
            string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@UserID", usrid),
                            new SqlParameter("@groupname", grpnme),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acctgrp_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteAcctGrp(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_acctgrp_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetBankListCount(string keyword, bool IsActive, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter("@IsActive", IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_bank_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetBankList(DataTable DTFromDB, string keyword, bool IsActive, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                     new SqlParameter("@IsActive", IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_bank_list", sqlParam);
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

        public async Task<int> dbupdateBank(int ID, string BANK_ID, string BANK_NAME, string BI_CODE, bool Isactive, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@BANK_ID", BANK_ID),
                            new SqlParameter("@BANK_NAME", BANK_NAME),
                            new SqlParameter("@BI_CODE", BI_CODE??""),
                            new SqlParameter("@IsActive", Isactive),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_bank_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteBank(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_bank_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGeteducListCount(string keyword, bool IsActive, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter("@IsActive", IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_pendidikan_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGeteducList(DataTable DTFromDB, string keyword, bool IsActive, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                     new SqlParameter("@IsActive", IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_pendidikan_list", sqlParam);
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

        public async Task<int> dbupdateeduc(int ID, string educ_NAME, bool Isactive, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@pendidikan", educ_NAME),
                            new SqlParameter("@IsActive", Isactive),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_pendidikan_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteeduc(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_pendidikan_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetRegionListCount(string keyword, bool IsActive, bool IsPusat, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@IsActive",IsActive),
                    new SqlParameter ("@IspUsat",IsPusat),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_region_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetRegionList(DataTable DTFromDB, string keyword, bool IsActive, bool IsPusat, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@IsActive",IsActive),
                    new SqlParameter ("@IspUsat",IsPusat),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_region_list", sqlParam);
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

        public async Task<int> dbupdateRegion(int ID, string REGION, string REGION_NAME, bool ISPUSAT, bool IsActive, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@REGION_NAME", REGION_NAME),
                            new SqlParameter("@ISPUSAT", ISPUSAT),
                             new SqlParameter("@IsActive", IsActive),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_region_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteRegion(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_region_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetInfoTextListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_infotext_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetInfoTextList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_infotext_list", sqlParam);
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

        public async Task<int> dbupdateInfoText(int ID, string InfoText, string EndDate, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@InfoText", InfoText),
                            new SqlParameter("@EndDate", EndDate),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_infotext_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteInfoText(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_infotext_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetDivisiGrpListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_teamho_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetDivisiGrpList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_teamho_list", sqlParam);
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

        public async Task<int> dbupdateDivisiGrp(int ID, string GROUP_NAME, string PIC, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@GROUP_NAME", GROUP_NAME),
                            new SqlParameter("@PIC", PIC),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_teamho_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteDivisiGrp(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_teamho_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetDivisiListCount(string keyword, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_divisi_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetDivisiList(DataTable DTFromDB, string keyword, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_divisi_list", sqlParam);
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

        public async Task<int> dbupdateDivisi(int ID, string DIVISI, string DIVISI_NAME, string CONT_PERIODE, string CONT_ATASNAMA, string CONT_JABATANATASNAMA, string Group, string PIC, bool IsActive, string DivCode, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@DIVISI_NAME", DIVISI_NAME),
                            new SqlParameter("@CONT_PERIODE", CONT_PERIODE),
                            new SqlParameter("@CONT_UP", CONT_ATASNAMA),
                            new SqlParameter("@CONT_UPJBT", CONT_JABATANATASNAMA),
                            new SqlParameter("@GROUP", Group??""),
                            new SqlParameter("@PIC", PIC),
                            new SqlParameter("@Isactive", IsActive),
                            new SqlParameter("@DIVISI_CODE", DivCode),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_divisi_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteDivisi(int ID, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_divisi_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGethandlejobsListCount(string keyword, string Divisi, bool IsActive, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@Divisi",Divisi),
                    new SqlParameter ("@IsActive",IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_handlejobs_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGethandlejobsList(DataTable DTFromDB, string keyword, string Divisi, bool IsActive, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter ("@Divisi",Divisi),
                    new SqlParameter ("@IsActive",IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_handlejobs_list", sqlParam);
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

        public async Task<int> dbupdatehandlejobs(int ID, int IDDET, string JOBID, string JOBDESC, string Divisi, string DivisiSelectOne, bool IsActive, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@iddet", IDDET),
                            new SqlParameter("@JOBDESC", JOBDESC),
                            new SqlParameter("@Divisi", Divisi),
                            new SqlParameter("@DivisiSel", DivisiSelectOne),
                            new SqlParameter("@IsActive", IsActive),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_handlejobs_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeletehandlejobs(int ID, int IDDET, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@iddet", IDDET),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_handlejobs_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetDocTypeListCount(string keyword, string Divisi, string RegType, bool IsActive, int PageNumber, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();

            dbAccessHelper dbaccess = new dbAccessHelper();
            string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter("@Divisi", Divisi),
                    new SqlParameter("@RegType", RegType),
                    new SqlParameter("@IsActive", IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

            dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_document_type_list_cnt", sqlParam);

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

        public async Task<List<DataTable>> dbGetDocTypeList(DataTable DTFromDB, string keyword, string Divisi, string RegType, bool IsActive, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
        {
            DataTable dt = new DataTable();
            List<DataTable> dtlist = new List<DataTable>();
            if (DTFromDB == null || DTFromDB.Rows.Count == 0)
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam =
                 {
                    new SqlParameter("@KeyWord", keyword),
                    new SqlParameter("@Divisi", Divisi),
                    new SqlParameter("@RegType", RegType),
                    new SqlParameter("@IsActive", IsActive),
                    new SqlParameter ("@moduleId",moduleID),
                    new SqlParameter ("@UserIDLog",userid),
                    new SqlParameter ("@UserGroupLog",groupname),
                    new SqlParameter ("@PageNumber",PageNumber),
                };

                dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_document_type_list", sqlParam);
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

        public async Task<int> dbupdateDocType(int ID, int IDDET, string doctype, string docalias, string regtype, bool IsMandatory, bool IsActive, string Divisi, string DivisiSelOne, bool IsNeedTemplate, string moduleID, string userid, string groupname)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@iddet", IDDET),
                            new SqlParameter("@DocType", doctype),
                            new SqlParameter("@DocALias", docalias),
                            new SqlParameter("@RegType", regtype),
                            new SqlParameter("@IsNeedTemplate", IsNeedTemplate),
                            new SqlParameter("@IsMandatory", IsMandatory),
                            new SqlParameter("@IsActive", IsActive),
                            new SqlParameter("@Divisi", Divisi),
                            new SqlParameter("@DivisiSel", DivisiSelOne),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", userid),
                            new SqlParameter("@UserGroupLog", groupname)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_document_type_sve", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<int> dbdeleteDocType(int ID, int IDDET, string moduleID, string UserID, string GroupName)
        {
            int result = 0;
            try
            {
                dbAccessHelper dbaccess = new dbAccessHelper();
                string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDB());

                SqlParameter[] sqlParam = {
                            new SqlParameter("@id", ID),
                            new SqlParameter("@iddet", IDDET),
                            new SqlParameter("@moduleId", moduleID),
                            new SqlParameter("@UserIDLog", UserID),
                            new SqlParameter("@UserGroupLog",GroupName)
                 };

                await Task.Delay(0);
                DataTable dt = await dbaccess.ExecuteDataTable(strconnection, "udp_mdt_app_document_type_del", sqlParam);

                result = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorAPI.txt");
            }

            return result;
        }

        public async Task<List<string>> dbGetCoaListCount(string ntryid, int PageNumber, string moduleID, string userid, string groupname)
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
                           {"keyword", ntryid},
                           {"PageNumber", PageNumber.ToString()},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextMaster.cmdGetjurnalcoaList.GetDescriptionEnums().ToString();
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

        public async Task<List<DataTable>> dbGetCoaList(DataTable DTFromDB, string ntryid, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
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
                           {"keyword", ntryid},
                           {"PageNumber", PageNumber.ToString()},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextMaster.cmdGetjurnalcoaList.GetDescriptionEnums().ToString();
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

        public async Task<int> dbupdateCoa(int ID, string CoaCode, string Description, bool TipeCoa, bool OprMinus, string NatureAcct,
                                                string GrupOrderRptbalance, string GrupAcct, bool IsNotViewInLR, bool IsNotViewInNR, string InitialCode,
                                                bool IsBlock, string moduleID, string userid, string groupname
                                                )
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
                           {"ID", ID.ToString()},
                           {"CoaCode", CoaCode},
                           {"Description", Description},
                           {"TipeCoa", TipeCoa.ToString()},
                           {"OprMinus", OprMinus.ToString()},
                           {"NatureAcct", NatureAcct??""},
                           {"GrupOrderRptbalance", GrupOrderRptbalance},
                           {"GrupAcct", GrupAcct},
                           {"IsNotViewInLR", IsNotViewInLR.ToString()},
                           {"IsNotViewInNR", IsNotViewInNR.ToString()},
                           {"InitialCode", InitialCode??""},
                           {"IsBlock", IsBlock.ToString()},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextMaster.cmdSavejurnalcoa.GetDescriptionEnums().ToString();
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

        public async Task<int> dbdeleteCoa(int ID, string moduleID, string userid, string groupname)
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
                           {"ID", ID.ToString()},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextMaster.cmdDeljurnalcoa.GetDescriptionEnums().ToString();
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

        public async Task<List<string>> dbGetCoaMAPListCount(string ntryid, int PageNumber, string moduleID, string userid, string groupname)
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
                           {"keyword", ntryid},
                           {"PageNumber", PageNumber.ToString()},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextMaster.cmdGetjurnalmapList.GetDescriptionEnums().ToString();
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

        public async Task<List<DataTable>> dbGetCoaMAPList(DataTable DTFromDB, string ntryid, int PageNumber, double pagenumberclient, double pagingsizeclient, string moduleID, string userid, string groupname)
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
                           {"keyword", ntryid},
                           {"PageNumber", PageNumber.ToString()},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextMaster.cmdGetjurnalmapList.GetDescriptionEnums().ToString();
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

        public async Task<int> dbupdateCoaMAP(int ID, string KodeGroup, string Transaksi, string LawanTransaksi,
                                                string Debit, string Kredit, string StartDate, string EndDate, string moduleID, string userid, string groupname
                                                )
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
                           {"ID", ID.ToString()},
                           {"KodeGroup", KodeGroup},
                           {"Transaksi", Transaksi},
                           {"LawanTransaksi", LawanTransaksi},
                           {"D", Debit},
                           {"K", Kredit},
                           {"StartDate", StartDate},
                           {"EndDate", EndDate},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextMaster.cmdSavejurnalmap.GetDescriptionEnums().ToString();
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

        public async Task<int> dbdeleteCoaMAP(int ID, string moduleID, string userid, string groupname)
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
                           {"ID", ID.ToString()},
                           {"moduleID", moduleID},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextMaster.cmdDeljurnalmap.GetDescriptionEnums().ToString();
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