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
    public class vmPendingTask
    {
        public string CheckWithKey { get; set; }
        public cFilterContract DetailFilter { get; set; }
        public DataTable DTOrdersFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }
        public cPendingTask SelectRowForUpd { get; set; }
        public cAccountMetrik Permission { get; set; }
    }

    [Serializable]
    public class vmPendingTaskddl
    {
        #region PendingTask Reguler
        public async Task<List<String>> dbGetPendingTaskListCount(string ClientIDS, string BranchIDS, string fromdate, string todate, string NoPerjanjian, string SelectJenisPelanggan, string SelectJenisKontrak, string SelectStatusCheck, int PageNumber, string idcaption, string userid, string groupname)
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
                           {"SelectJenisPelanggan", SelectJenisPelanggan},
                           {"SelectBranch", BranchIDS},
                           {"SelectClient", ClientIDS},
                           {"SelectStatusCheck",SelectStatusCheck},
                           {"SelectJenisKontrak", SelectJenisKontrak},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", idcaption},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendingTask.cmdGetPendingTaskList.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetPendingTaskList(DataTable DTFromDB, string ClientIDS, string BranchIDS, string fromdate, string todate, string NoPerjanjian, string SelectJenisPelanggan, string SelectJenisKontrak, string SelectStatusCheck, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                           {"SelectJenisPelanggan", SelectJenisPelanggan},
                           {"SelectBranch", BranchIDS},
                           {"SelectClient", ClientIDS},
                           {"SelectStatusCheck",SelectStatusCheck},
                           {"SelectJenisKontrak", SelectJenisKontrak},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"PageNumber", PageNumber.ToString()},
                           {"idcaption", idcaption},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextPendingTask.cmdGetPendingTaskList.GetDescriptionEnums().ToString();
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
        public async Task<int> dbupdatePendTaskDoc(cPendingTask docvalue, string statusdata, string statusdatacheck, string moduleID, string userid, string groupname)
        {

            string idFDC = docvalue.IDFDC;
            string ClientIDS = docvalue.clientid;
            string cabang = docvalue.CabangId;
            string NoPerjanjian = docvalue.NoPerjanjian;

            string jenisdoc = docvalue.JenisNasabah;

            string NamaBPKB = docvalue.NamaBPKB ?? "";
            string noidentitasBPKB = docvalue.NoidentitasBPKB ?? "";
            string nocontactBPKB = docvalue.NoContactBPKB ?? "";
            string alamatBPKB = docvalue.AlamatBPKB ?? "";
            string JenisKelaminBPKB = docvalue.JenisKelaminBPKB ?? "";
            string KelurahanBPKB = docvalue.KelurahanBPKB ?? "";
            string KecamatanBPKB = docvalue.KecamatanBPKB ?? "";
            string kotakabupatenBPKB = docvalue.KabupatenKotaBPKB ?? "";
            string kotakabupatenBPKBAHU = docvalue.KabupatenKotaBPKBAHU ?? "";
            string provinsiBPKB = docvalue.ProvinsiBPKB ?? "";
            string kodeposBPKB = docvalue.PoskodeBPKB ?? "";
            string RTBPKB = docvalue.RTBPKB ?? "";
            string RWBPKB = docvalue.RWBPKB ?? "";


            string NamaNasabah = docvalue.NamaNasabah ?? "";
            string noidentitasGRTE = docvalue.NoidentitasNasabah ?? "";
            string nocontactGRTE = docvalue.NoContactnasabah ?? "";
            string alamatGRTE = docvalue.AlamatNasabah ?? "";
            string JenisKelaminGRTE = docvalue.JenisKelaminNasabah ?? "";
            string KelurahanGRTE = docvalue.KelurahanNasabah ?? "";
            string KecamatanGRTE = docvalue.KecamatanNasabah ?? "";
            string kotakabupatenGRTE = docvalue.KabupatenKotaNasabah ?? "";
            string kotakabupatenGRTEAHU = docvalue.KabupatenKotaNasabahAHU ?? "";
            string provinsiGRTE = docvalue.ProvinsiNasabah ?? "";
            string kodeposGRTE = docvalue.PoskodeNasabah ?? "";
            string RTGRTE = docvalue.RTNasabah ?? "";
            string RWGRTE = docvalue.RWNasabah ?? "";

            string DataGive = docvalue.DataGIVE ?? "";
            string cust_typeAHU = docvalue.cust_type_ahu ?? "";


            string Notes = docvalue.Notes ?? "";

            int cont_type = docvalue.ContType;
            int roda = docvalue.Roda;

            string Mesin = docvalue.MesinNumber ?? "";
            string Rangka = docvalue.RangkaNumber ?? "";

            string TglPerjanjian = docvalue.TglPerjanjian.ToString("yyyy-MM-dd");
            string NilaiHutang = (docvalue.NilaiHutang ?? "0").Replace(",", "");
            string TglAwalAngsuran = DateTime.Parse((docvalue.TglAwalAngsuran ?? "1984-01-01").ToString()).ToString("yyyy-MM-dd");
            string TglAkhirAngsuran = DateTime.Parse((docvalue.TglAkhirAngsuran ?? "1984-01-01").ToString()).ToString("yyyy-MM-dd");
            string TahunObject = (docvalue.TahunObject ?? "").ToString();
            string Kondisiobject = (docvalue.Kondisiobject ?? "").ToString();
            string KategoriObject = (docvalue.KategoriObject ?? "").ToString();
            string MerkObject = (docvalue.MerkObject ?? "").ToString();
            string TipeObject = (docvalue.TipeObject ?? "").ToString();
            string WarnaObject = (docvalue.WarnaObject ?? "").ToString();
            string NilaiObject = (docvalue.NilaiObject ?? "0").Replace(",", "");
            string NilaiJaminan = (docvalue.NilaiJaminan ?? "0").Replace(",", "");

            bool filldebitur = docvalue.FillDebitur;
            bool contnotvalid = docvalue.ContNotValid;

            bool OverKontrak = docvalue.OverKontrak;
            bool TakeOut = docvalue.TakeOut;
            bool Regular = docvalue.Regular;
            bool multiguna = docvalue.multiguna;

            bool Verified = docvalue.Verified;

            if (statusdata == docvalue.radioA)
            {

                OverKontrak = true;
            }
            else if (statusdata == docvalue.radioB)
            {
                TakeOut = true;

            }
            else if (statusdata == docvalue.radioC)
            {
                multiguna = true;

            }
            else if (statusdata == docvalue.radioD)
            {
                Regular = true;
            }



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

                        {"IDFDC",idFDC.ToString()},
                        {"NoPerjanjian",NoPerjanjian},
                        {"Cabang",cabang},
                        {"clientid",ClientIDS},
                        {"JenisDocument",jenisdoc},

                        {"NamaBPKB",NamaBPKB },
                        {"NoidentitasBPKB",noidentitasBPKB},
                        {"NoContactBPKB",nocontactBPKB },
                        {"AlamatBPKB",alamatBPKB },
                        {"JenisKelaminBPKB",JenisKelaminBPKB },
                        {"KelurahanBPKB",KelurahanBPKB },
                        {"KecamatanBPKB",KecamatanBPKB },
                        {"KabupatenKotaBPKB",kotakabupatenBPKB },
                        {"KabupatenKotaBPKBAHU",kotakabupatenBPKBAHU },
                        {"ProvinsiBPKB",provinsiBPKB},
                        {"PoskodeBPKB",kodeposBPKB},
                        {"RTBPKB",RTBPKB},
                        {"RWBPKB",RWBPKB},

                        {"NamaNasabah",NamaNasabah },
                        {"NoidentitasNasabah",noidentitasGRTE},
                        {"NoContactnasabah",nocontactGRTE },
                        {"AlamatNasabah",alamatGRTE },
                        {"JenisKelaminNasabah",JenisKelaminGRTE },
                        {"KelurahanNasabah",KelurahanGRTE},
                        {"KecamatanNasabah",KecamatanGRTE },
                        {"KabupatenKotaNasabah",kotakabupatenGRTE },
                        {"KabupatenKotaNasabahAHU",kotakabupatenGRTEAHU },
                        {"ProvinsiNasabah",provinsiGRTE },
                        {"PoskodeNasabah",kodeposGRTE },
                        {"RTNasabah",RTGRTE },
                        {"RWNasabah",RWGRTE},

                        {"DataGIVE",DataGive },
                        {"cust_type_ahu",cust_typeAHU },

                        {"ContType",cont_type.ToString()},
                        {"Roda",roda.ToString()},

                        {"MesinNumber",Mesin.ToString()},
                        {"RangkaNumber",Rangka.ToString()},

                        {"TglPerjanjian",TglPerjanjian},
                        {"NilaiHutang",NilaiHutang },
                        {"TglAwalAngsuran",TglAwalAngsuran},
                        {"TglAkhirAngsuran",TglAkhirAngsuran},
                        {"TahunObject",TahunObject},
                        {"Kondisiobject",Kondisiobject},
                        {"KategoriObject",KategoriObject},
                        {"MerkObject",MerkObject},
                        {"TipeObject",TipeObject},
                        {"WarnaObject",WarnaObject },
                        {"NilaiObject",NilaiObject},
                        {"NilaiJaminan",NilaiJaminan},

                        {"StatusChecked",statusdatacheck },
                        {"FillDebitur",filldebitur.ToString() },
                        {"ContNotValid",contnotvalid.ToString() },
                        {"Verified",Verified.ToString() },
                        {"TakeOut",TakeOut.ToString() },
                        {"OverKontrak",OverKontrak.ToString() },
                        {"multiguna",multiguna.ToString() },
                        {"Regular",Regular.ToString() },
                        {"Notes",Notes },
                        {"idcaption",moduleID },
                        {"UserID", userid},
                        {"GroupName", groupname},
                   };


                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendingTask.cmdupdatePendTaskDoc.GetDescriptionEnums().ToString();
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
        public async Task<DataTable> dbGetPendingTaskGet(string idFDC, string ClientIDS, string NoPerjanjian, int conttype, string SelectCheck, string moduleid, string userid, string groupname)
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
                           {"ContType", conttype.ToString()},
                           {"IDFDC", idFDC},
                           {"NoPerjanjian", NoPerjanjian},
                           {"SelectStatusCheck",SelectCheck },
                           {"clientid", ClientIDS},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendingTask.cmdGetPendingTaskGet.GetDescriptionEnums().ToString();
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

        public async Task<int> dbGetPendingTaskFollowCheck(string idFDC, string ClientIDS, string NoPerjanjian, int conttype, string SelectCheck, string moduleid, string userid, string groupname)
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
                           {"ContType", conttype.ToString()},
                           {"IDFDC", idFDC},
                           {"NoPerjanjian", NoPerjanjian},
                           {"SelectStatusCheck",SelectCheck },
                           {"clientid", ClientIDS},
                           {"idcaption", moduleid},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendingTask.cmdGetPendingTaskFollowChecked.GetDescriptionEnums().ToString();
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

        public async Task<int> dbGetPendingTaskFollowInvoice(DataTable tabledata, bool verified, bool validdata, string moduleID, string userid, string groupname)
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

                    string cmdtextapi = cCommandTextPendingTask.cmdGetPendingTaskFollowInvoice.GetDescriptionEnums().ToString();
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

        public DataTable dbGetPendingTaskListExport(string ClientIDS, string BranchIDS, string NotaryIDS, string NoPerjanjian, string fromdate, string todate, string SelectContractStatus, string SelectJenisPelanggan, string SelectDocStatusNotValid, int PageNumber, string idcaption, string userid, string groupname)
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
                           {"NoPerjanjian",NoPerjanjian},
                           {"SelectContractStatus", SelectContractStatus},
                           {"SelectJenisPelanggan", SelectJenisPelanggan},
                           {"SelectNotaris", NotaryIDS},
                           {"Cabang", BranchIDS},
                           {"clientid", ClientIDS},
                           {"fromdate", fromdate},
                           {"todate", todate},
                           {"SelectDocStatusNotValid", SelectDocStatusNotValid},
                           {"idcaption", idcaption},
                           {"UserID", userid},
                           {"GroupName", groupname},
                        };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPendingTask.cmdGetPendingTaskListExport.GetDescriptionEnums().ToString();
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
        #endregion PendingTask Reguler

        //#region PendingTask Fleet
        //public async Task<List<String>> dbGetPendingTaskListCountFleet(string ClientIDS, string BranchIDS, string fromdate, string todate, string NoPerjanjian, string SelectJenisPelanggan, int PageNumber, string idcaption, string userid, string groupname)
        //{

        //    string cmdtext = cCommandTextPendingTask.cmdGetPendingTaskListCountFleet.GetDescriptionEnums().ToString();

        //    SqlParameter[] sqlParam =
        //    {
        //                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
        //                new SqlParameter ("@custtype",SelectJenisPelanggan),
        //                new SqlParameter ("@kodecabang",BranchIDS),
        //                new SqlParameter ("@ClientID",ClientIDS),
        //                new SqlParameter ("@tgl_Perjanjianawal",fromdate),
        //                new SqlParameter ("@tgl_Perjanjianakhir",todate),
        //                new SqlParameter ("@moduleId",idcaption),
        //                new SqlParameter ("@UserIDLog",userid),
        //                new SqlParameter ("@UserGroupLog",groupname),
        //                new SqlParameter ("@PageNumber",PageNumber),
        //            };
        //    //await Task.Delay(500);
        //    DataTable dt = await dbaccess.ExecuteDataTable(strconnection, cmdtext, sqlParam);

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
        //public async Task<List<DataTable>> dbGetPendingTaskListFleet(DataTable DTFromDB, string ClientIDS, string BranchIDS, string fromdate, string todate, string NoPerjanjian, string SelectJenisPelanggan, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
        //{


        //    string cmdtext = cCommandTextPendingTask.cmdGetPendingTaskListFleet.GetDescriptionEnums().ToString();

        //    SqlParameter[] sqlParam =
        //    {
        //                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
        //                new SqlParameter ("@custtype",SelectJenisPelanggan),
        //                new SqlParameter ("@kodecabang",BranchIDS),
        //                new SqlParameter ("@ClientID",ClientIDS),
        //                new SqlParameter ("@tgl_Perjanjianawal",fromdate),
        //                new SqlParameter ("@tgl_Perjanjianakhir",todate),
        //                new SqlParameter ("@moduleId",idcaption),
        //                new SqlParameter ("@UserIDLog",userid),
        //                new SqlParameter ("@UserGroupLog",groupname),
        //                new SqlParameter ("@PageNumber",PageNumber),
        //   };
        //    //await Task.Delay(500);

        //    DataTable dt = new DataTable();
        //    List<DataTable> dtlist = new List<DataTable>();
        //    if (DTFromDB == null)
        //    {
        //        dt = await dbaccess.ExecuteDataTable(strconnection, cmdtext, sqlParam);
        //    }
        //    else
        //    {
        //        dt = DTFromDB;

        //    }

        //    dtlist.Add(dt);

        //    if (dt.Rows.Count > 0)
        //    {
        //        int starrow = (int.Parse(pagenumberclient.ToString()) - 1) * int.Parse(pagingsizeclient.ToString());
        //        dt = dt.Rows.Cast<System.Data.DataRow>().Skip(starrow).Take(int.Parse(pagingsizeclient.ToString())).CopyToDataTable();
        //    }
        //    dtlist.Add(dt);

        //    return dtlist;
        //}
        //public async Task<int> dbupdatePendTaskDocFleet(cPendingTask docvalue, string statusdata, string moduleID, string userid, string groupname)
        //{

        //    string idFDC = docvalue.IDFDC;
        //    string ClientIDS = docvalue.clientid;
        //    string cabang = docvalue.CabangId;
        //    string NoPerjanjian = docvalue.NoPerjanjian;

        //    string jenisdoc = docvalue.JenisNasabah;

        //    string noidentitasBPKB = docvalue.NoidentitasBPKB ?? "";
        //    string nocontactBPKB = docvalue.NoContactBPKB ?? "";
        //    string alamatBPKB = docvalue.AlamatBPKB ?? "";
        //    string JenisKelaminBPKB = docvalue.JenisKelaminBPKB ?? "";
        //    string kotakabupatenBPKB = docvalue.KabupatenKotaBPKB ?? "";
        //    string kotakabupatenBPKBAHU = docvalue.KabupatenKotaBPKBAHU ?? "";
        //    string provinsiBPKB = docvalue.ProvinsiBPKB ?? "";
        //    string kodeposBPKB = docvalue.PoskodeBPKB ?? "";

        //    string noidentitasGRTE = docvalue.NoidentitasNasabah ?? "";
        //    string nocontactGRTE = docvalue.NoContactnasabah ?? "";
        //    string alamatGRTE = docvalue.AlamatNasabah ?? "";
        //    string JenisKelaminGRTE = docvalue.JenisKelaminNasabah ?? "";
        //    string kotakabupatenGRTE = docvalue.KabupatenKotaNasabah ?? "";
        //    string kotakabupatenGRTEAHU = docvalue.KabupatenKotaNasabahAHU ?? "";
        //    string provinsiGRTE = docvalue.ProvinsiNasabah ?? "";
        //    string kodeposGRTE = docvalue.PoskodeNasabah ?? "";

        //    string cust_typeAHU = docvalue.cust_type_ahu ?? "";

        //    int cont_type = docvalue.ContType;
        //    int roda = docvalue.Roda;

        //    string Notes = docvalue.Notes ?? "";

        //    bool filldebitur = docvalue.FillDebitur;
        //    bool contnotvalid = docvalue.ContNotValid;

        //    bool OverKontrak = docvalue.OverKontrak;
        //    bool TakeOut = docvalue.TakeOut;

        //    bool multiguna = docvalue.multiguna;

        //    bool Verified = docvalue.Verified;

        //    if (statusdata == docvalue.radioA)
        //    {

        //        OverKontrak = true;
        //    }
        //    else if (statusdata == docvalue.radioB)
        //    {
        //        TakeOut = true;

        //    }
        //    else if (statusdata == docvalue.radioC)
        //    {
        //        multiguna = true;

        //    }


        //    int result = 0;

        //    string cmdtext = cCommandTextPendingTask.cmdupdatePendTaskDocFleet.GetDescriptionEnums().ToString();

        //    SqlParameter[] sqlParam =
        //    {
        //                new SqlParameter ("@idFDC",idFDC),
        //                new SqlParameter ("@noperjanjian",NoPerjanjian),
        //                new SqlParameter ("@kodecabang",cabang),
        //                new SqlParameter ("@moduleID",moduleID),
        //                new SqlParameter ("@ClientID",ClientIDS),
        //                new SqlParameter ("@jenisdoc",jenisdoc),

        //                new SqlParameter ("@NoIdentitiasBPKB",noidentitasBPKB),
        //                new SqlParameter ("@NoContactBPKB",nocontactBPKB),
        //                new SqlParameter ("@AlamatBPKB",alamatBPKB),
        //                new SqlParameter ("@JenisKelaminBPKB",JenisKelaminBPKB),
        //                new SqlParameter ("@KabupatenKotaBPKB",kotakabupatenBPKB),
        //                 new SqlParameter ("@KabupatenKotaBPKBAHU",kotakabupatenBPKBAHU),
        //                new SqlParameter ("@ProvinsiBPKB",provinsiBPKB),
        //                new SqlParameter ("@PoskodeBPKB",kodeposBPKB),

        //                new SqlParameter ("@NoIdentitiasGRTE",noidentitasGRTE),
        //                new SqlParameter ("@NoContactGRTE",nocontactGRTE),
        //                new SqlParameter ("@AlamatGRTE",alamatGRTE),
        //                new SqlParameter ("@JenisKelaminGRTE",JenisKelaminGRTE),
        //                new SqlParameter ("@KabupatenKotaGRTE",kotakabupatenGRTE),
        //                 new SqlParameter ("@KabupatenKotaGRTEAHU",kotakabupatenGRTEAHU),
        //                new SqlParameter ("@ProvinsiGRTE",provinsiGRTE),
        //                new SqlParameter ("@PoskodeGRTE",kodeposGRTE),

        //                new SqlParameter ("@Type_AHU",cust_typeAHU),
        //                new SqlParameter ("@cont_type",cont_type),

        //                new SqlParameter ("@roda",roda),


        //                new SqlParameter ("@Verified",Verified),

        //                new SqlParameter ("@FillDebitur",filldebitur),
        //                new SqlParameter ("@ContNotValid",contnotvalid),

        //                new SqlParameter ("@TakeOut",TakeOut),
        //                new SqlParameter ("@overKontrak",OverKontrak),
        //                new SqlParameter ("@Multiguna",multiguna),

        //                new SqlParameter ("@notes",Notes),

        //                new SqlParameter ("@UserIDLog",userid),
        //                new SqlParameter ("@UserGroupLog",groupname),

        //            };


        //    await Task.Delay(0);
        //    DataTable dt = await dbaccess.ExecuteDataTable(strconnection, cmdtext, sqlParam);

        //    result = int.Parse(dt.Rows[0][0].ToString());

        //    return result;

        //}
        //public async Task<DataTable> dbGetPendingTaskGetFleet(string idFDC, string ClientIDS, string NoPerjanjian, int conttype, string moduleid, string userid, string groupname)
        //{

        //    string cmdtext = cCommandTextPendingTask.cmdGetPendingTaskGetFleet.GetDescriptionEnums().ToString();

        //    SqlParameter[] sqlParam =
        //    {
        //                new SqlParameter ("@cont_type",conttype),
        //                new SqlParameter ("@idFDC",idFDC),
        //                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
        //                new SqlParameter ("@ClientID",ClientIDS),
        //                new SqlParameter ("@moduleId",moduleid),
        //                new SqlParameter ("@UserIDLog",userid),
        //                new SqlParameter ("@UserGroupLog",groupname)
        //            };
        //    //await Task.Delay(500);

        //    DataTable dt = new DataTable();
        //    dt = await dbaccess.ExecuteDataTable(strconnection, cmdtext, sqlParam);
        //    return dt;
        //}
        //public DataTable dbGetPendingTaskListExportFleet(string ClientIDS, string BranchIDS, string NotaryIDS, string NoPerjanjian, string fromdate, string todate, string SelectContractStatus, string SelectJenisPelanggan, string SelectDocStatusNotValid, int PageNumber, string idcaption, string userid, string groupname)
        //{

        //    string cmdtext = cCommandTextPendingTask.cmdGetPendingTaskListExportFleet.GetDescriptionEnums().ToString();

        //    SqlParameter[] sqlParam =
        //    {
        //                new SqlParameter ("@No_Perjanjian",NoPerjanjian),
        //                new SqlParameter ("@statusdocupload",SelectContractStatus),
        //                new SqlParameter ("@custtype",SelectJenisPelanggan),
        //                new SqlParameter ("@NotaryID",NotaryIDS),
        //                new SqlParameter ("@kodecabang",BranchIDS),
        //                new SqlParameter ("@ClientID",ClientIDS),
        //                new SqlParameter ("@tgl_Perjanjianawal",fromdate),
        //                new SqlParameter ("@tgl_Perjanjianakhir",todate),
        //                new SqlParameter ("@notvaliddoc",SelectDocStatusNotValid),
        //                new SqlParameter ("@moduleId",idcaption),
        //                new SqlParameter ("@UserIDLog",userid),
        //                new SqlParameter ("@UserGroupLog",groupname),
        //                new SqlParameter ("@PageNumber",PageNumber),
        //            };
        //    //await Task.Delay(500);
        //    DataTable dt = new DataTable();

        //    dt = dbaccess.ExecuteDataTableNonAsync(strconnection, cmdtext, sqlParam);

        //    return dt;
        //}
        //#endregion PendingTask Fleet

    }
}