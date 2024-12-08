using Dropbox.Api;
using Dropbox.Api.Files;
using HashNetFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DusColl
{
    [Serializable]
    public class vmPemberkasan
    {
        public string CheckWithKey { get; set; }
        public string securemoduleID { get; set; }
        public String ClientLogin { get; set; }

        public cFilterContract DetailFilter { get; set; }
        public List<cUploadFileSuccess> uploadSuccessFile { get; set; }

        //untuk handle ddl jenisdokumenn yang selalu null saat post file//
        public IEnumerable<cListSelected> ddlJenisDokumen { get; set; }

        public bool uploadberkas { get; set; } = false;
        public List<cDouemntsGroupType> DocumentGroupType { get; set; }
        public DataTable DTOrdersFromDB { get; set; }
        public DataTable DTDetailForGrid { get; set; }
        public cAccountMetrik Permission { get; set; }

    }

    [Serializable]
    public class vmPemberkasanddl
    {

        public async Task<List<String>> dbGetPemberkasanListCount(string ClientIDS, string region, string BranchIDS, string NotaryIDS, string NoPerjanjian, string jeniskontrak,
            string SelectDocStatus, string SelectJenisPelanggan, string fromdate, string todate, string notvalid,
            string fill_debitur, string namabpkbdebitur, string readyakta, int PageNumber, string idcaption, string userid, string groupname)
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
                               {"SelectRegion", region},
                               {"SelectBranch", BranchIDS},
                               {"SelectClient", ClientIDS},
                               {"SelectNotaris", NotaryIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"SelectJenisKontrak", jeniskontrak},
                               {"SelectDocStatus", SelectDocStatus},
                               {"Fill_Debitur_str", fill_debitur.ToString()},
                               {"StatusAkta", readyakta.ToString()},
                               {"NamaBPKBDebitur", namabpkbdebitur},
                               {"SelectJenisPelanggan", SelectJenisPelanggan},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                               {"PageNumber", PageNumber.ToString()},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasanlist.GetDescriptionEnums().ToString();
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

        public async Task<List<DataTable>> dbGetPemberkasanList(DataTable DTFromDB, string ClientIDS, string region, string BranchIDS, string NotaryIDS, string NoPerjanjian, string jeniskontrak,
            string SelectDocStatus, string SelectJenisPelanggan, string fromdate, string todate, string notvalid, string fill_debitur, string namabpkbdebitur, string readyakta,
            int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                               {"SelectRegion", region},
                               {"SelectBranch", BranchIDS},
                               {"SelectClient", ClientIDS},
                               {"SelectNotaris", NotaryIDS},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"SelectJenisKontrak", jeniskontrak},
                               {"SelectDocStatus", SelectDocStatus},
                               {"Fill_Debitur_str", fill_debitur.ToString()},
                               {"StatusAkta", readyakta.ToString()},
                               {"NamaBPKBDebitur", namabpkbdebitur},
                               {"SelectJenisPelanggan", SelectJenisPelanggan},
                               {"idcaption", idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                               {"PageNumber", PageNumber.ToString()},
                            };
                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasanlist.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetDocumentPemberkasanJenis(string FDCID, string NoPerjanjian, string ClientID, int Cont_Type, string moduleid, string userid, string usergroup)
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
                               {"SelectJenisKontrak", Cont_Type.ToString()},
                               {"IDFDC", FDCID},
                               {"NoPerjanjian", NoPerjanjian},
                               {"SelectClient", ClientID},
                               {"idcaption", moduleid},
                               {"UserID", userid},
                               {"GroupName", usergroup},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasanget.GetDescriptionEnums().ToString();
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

        public async Task<DataTable> dbGetDocumentPemberkasan4map(string cont_type, string NoPerjanjian, string ClientID, string OverKontrak, string moduleid, string userid, string usergroup)
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
                               {"SelectJenisKontrak", cont_type},
                               {"NoPerjanjian", NoPerjanjian},
                               {"SelectClient", ClientID},
                               {"OverKontrak", OverKontrak},
                               {"idcaption", moduleid},
                               {"UserID", userid},
                               {"GroupName", usergroup},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasanget4map.GetDescriptionEnums().ToString();
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

        public async Task<int> dbupdatePemberkasanStatusDoc(List<cDouemntsGroupType> docvalue, List<cDouemntsGroupType> docvalueOLD, string NoPerjanjian, string idFDC, string ClientIDS, string JenisCust, string conttype, string userid, string groupname, string moduleID, int statusbefore)
        {

            int statuscek = int.Parse(docvalue[0].SelectDocStatus);
            string Keterangan = docvalue[0].Keterangan ?? "";

            int i = 0;
            string paramvalue = "";
            bool checkall = false;

            foreach (var x in docvalue)
            {
                paramvalue = paramvalue + "" + x.COLUMN_NAME + " = '" + x.CheckValue.ToString() + "',";
                if (x.CheckValue.ToString() == "True")
                {
                    checkall = true;
                }
            }

            paramvalue = paramvalue.Remove(paramvalue.LastIndexOf(','));

            if ((Keterangan == "") && (statuscek == (int)docUploadStatus.LengkapCatatan))
            {
                return (int)ProccessOutput.neednote;
            }


            if ((checkall == false) && (statuscek == (int)docUploadStatus.TidakLengkap))
            {
                return (int)ProccessOutput.needkelengkapandata;
            }


            if ((statusbefore == (int)docUploadStatus.adawarka) && ((statuscek != (int)docUploadStatus.Lengkap) && (statuscek != (int)docUploadStatus.LengkapCatatan)) && (statuscek == (int)docUploadStatus.TidakLengkap))
            {
                return (int)ProccessOutput.needstatuslengkap;
            }

            JenisCust = docvalue[0].TIPEGIVE ?? JenisCust;
            string NamaPIC = docvalue[0].NamaPIC ?? "";
            string AlamatPIC = docvalue[0].AlamatPIC ?? "";
            string JenisKelaminPIC = docvalue[0].JenisKelaminPIC ?? "";
            string TempatLahirPIC = docvalue[0].TempatLahirPIC ?? "";
            string TglLahirPIC = docvalue[0].TglLahirPIC ?? "";
            string PekerjaanPIC = docvalue[0].PekerjaanPIC ?? "";
            string JabatanPIC = docvalue[0].JabatanPIC ?? "";
            string KewarganegaraanPIC = docvalue[0].KewarganegaraanPIC ?? "";
            string JenisidentitasPIC = docvalue[0].JenisidentitasPIC ?? "";
            string NoidentitasPIC = docvalue[0].NoidentitasPIC ?? "";
            string NoContactPIC = docvalue[0].NoContactPIC ?? "";
            string RTPIC = docvalue[0].RTPIC ?? "";
            string RWPIC = docvalue[0].RWPIC ?? "";
            string KelurahanPIC = docvalue[0].KelurahanPIC ?? "";
            string KecamatanPIC = docvalue[0].KecamatanPIC ?? "";
            string KabupatenKotaPIC = docvalue[0].KabupatenKotaPIC ?? "";
            string ProvinsiPIC = docvalue[0].ProvinsiPIC ?? "";
            string PoskodePIC = docvalue[0].PoskodePIC ?? "";
            string PengurusORG = docvalue[0].PengurusORG ?? "";

            string TIPEGIVE = docvalue[0].TIPEGIVE ?? "";
            string NamaDebiturGIVE = docvalue[0].NamaDebiturGIVE ?? "";
            string NamaGIVE = docvalue[0].NamaGIVE ?? "";
            string AlamatGIVE = docvalue[0].AlamatGIVE ?? "";
            string JenisKelaminGIVE = docvalue[0].JenisKelaminGIVE ?? "";
            string TempatLahirGIVE = docvalue[0].TempatLahirGIVE ?? "";
            string TglLahirGIVE = docvalue[0].TglLahirGIVE ?? "";
            string PekerjaanGIVE = docvalue[0].PekerjaanGIVE ?? "";
            string JabatanGIVE = docvalue[0].JabatanGIVE ?? "";
            string KewarganegaraanGIVE = docvalue[0].KewarganegaraanGIVE ?? "";
            string JenisidentitasGIVE = docvalue[0].JenisidentitasGIVE ?? "";
            string NoidentitasGIVE = docvalue[0].NoidentitasGIVE ?? "";
            string NoContactGIVE = docvalue[0].NoContactGIVE ?? "";
            string RTGIVE = docvalue[0].RTGIVE ?? "";
            string RWGIVE = docvalue[0].RWGIVE ?? "";
            string KelurahanGIVE = docvalue[0].KelurahanGIVE ?? "";
            string KecamatanGIVE = docvalue[0].KecamatanGIVE ?? "";
            string KabupatenKotaGIVE = docvalue[0].KabupatenKotaGIVE ?? "";
            string ProvinsiGIVE = docvalue[0].ProvinsiGIVE ?? "";
            string PoskodeGIVE = docvalue[0].PoskodeGIVE ?? "";
            string KotaPengadilanGIVE = docvalue[0].PengadilanKota ?? "";


            string NamaPICOld = docvalueOLD[0].NamaPIC ?? "";
            string AlamatPICOld = docvalueOLD[0].AlamatPIC ?? "";
            string JenisKelaminPICOld = docvalueOLD[0].JenisKelaminPIC ?? "";
            string TempatLahirPICOld = docvalueOLD[0].TempatLahirPIC ?? "";
            string TglLahirPICOld = docvalueOLD[0].TglLahirPIC ?? "";
            string PekerjaanPICOld = docvalueOLD[0].PekerjaanPIC ?? "";
            string JabatanPICOld = docvalueOLD[0].JabatanPIC ?? "";
            string KewarganegaraanPICOld = docvalueOLD[0].KewarganegaraanPIC ?? "";
            string JenisidentitasPICOld = docvalueOLD[0].JenisidentitasPIC ?? "";
            string NoidentitasPICOld = docvalueOLD[0].NoidentitasPIC ?? "";
            string NoContactPICOld = docvalueOLD[0].NoContactPIC ?? "";
            string RTPICOld = docvalueOLD[0].RTPIC ?? "";
            string RWPICOld = docvalueOLD[0].RWPIC ?? "";
            string KelurahanPICOld = docvalueOLD[0].KelurahanPIC ?? "";
            string KecamatanPICOld = docvalueOLD[0].KecamatanPIC ?? "";
            string KabupatenKotaPICOld = docvalueOLD[0].KabupatenKotaPIC ?? "";
            string ProvinsiPICOld = docvalueOLD[0].ProvinsiPIC ?? "";
            string PoskodePICOld = docvalueOLD[0].PoskodePIC ?? "";
            string PengurusORGOld = docvalueOLD[0].PengurusORG ?? "";

            string TIPEGIVEOld = docvalueOLD[0].TIPEGIVE ?? "";
            string NamaDebiturGIVEOld = docvalueOLD[0].NamaDebiturGIVE ?? "";
            string NamaGIVEOld = docvalueOLD[0].NamaGIVE ?? "";
            string AlamatGIVEOld = docvalueOLD[0].AlamatGIVE ?? "";
            string JenisKelaminGIVEOld = docvalueOLD[0].JenisKelaminGIVE ?? "";
            string TempatLahirGIVEOld = docvalueOLD[0].TempatLahirGIVE ?? "";
            string TglLahirGIVEOld = docvalueOLD[0].TglLahirGIVE ?? "";
            string PekerjaanGIVEOld = docvalueOLD[0].PekerjaanGIVE ?? "";
            string JabatanGIVEOld = docvalueOLD[0].JabatanGIVE ?? "";
            string KewarganegaraanGIVEOld = docvalueOLD[0].KewarganegaraanGIVE ?? "";
            string JenisidentitasGIVEOld = docvalueOLD[0].JenisidentitasGIVE ?? "";
            string NoidentitasGIVEOld = docvalueOLD[0].NoidentitasGIVE ?? "";
            string NoContactGIVEOld = docvalueOLD[0].NoContactGIVE ?? "";
            string RTGIVEOld = docvalueOLD[0].RTGIVE ?? "";
            string RWGIVEOld = docvalueOLD[0].RWGIVE ?? "";
            string KelurahanGIVEOld = docvalueOLD[0].KelurahanGIVE ?? "";
            string KecamatanGIVEOld = docvalueOLD[0].KecamatanGIVE ?? "";
            string KabupatenKotaGIVEOld = docvalueOLD[0].KabupatenKotaGIVE ?? "";
            string ProvinsiGIVEOld = docvalueOLD[0].ProvinsiGIVE ?? "";
            string PoskodeGIVEOld = docvalueOLD[0].PoskodeGIVE ?? "";
            string KotaPengadilanGIVEOld = docvalueOLD[0].PengadilanKota ?? "";
            bool isRegenerateakta = docvalue[0].IsGenerateAkta;
            bool Cetak_Akta = docvalue[0].Cetak_Akta;

            if (Cetak_Akta == true)
            {
                isRegenerateakta = false;
            }
            else
            {
                isRegenerateakta = true;
            }

            string OLDvalue = "NamaPICOld=" + NamaPICOld
            + "|" + "AlamatPICOld=" + AlamatPICOld
            + "|" + "JenisKelaminPICOld=" + JenisKelaminPICOld
            + "|" + "TempatLahirPICOld=" + TempatLahirPICOld
            + "|" + "TglLahirPICOld=" + TglLahirPICOld
            + "|" + "JabatanPICOld=" + JabatanPICOld
            + "|" + "KewarganegaraanPICOld=" + KewarganegaraanPICOld
            + "|" + "JenisidentitasPICOld=" + JenisidentitasPICOld
            + "|" + "NoidentitasPICOld=" + NoidentitasPICOld
            + "|" + "RTPICOld=" + RTPICOld
            + "|" + "RWPICOld=" + RWPICOld
            + "|" + "KelurahanPICOld=" + KelurahanPICOld
            + "|" + "KecamatanPICOld=" + KecamatanPICOld
            + "|" + "KabupatenKotaPICOld=" + KabupatenKotaPICOld
            + "|" + "ProvinsiPICOld=" + ProvinsiPICOld
            + "|" + "PoskodePICOld=" + PoskodePICOld
            + "|" + "PengurusORGOld=" + PengurusORGOld
            + "|" + "TIPEGIVEOld=" + TIPEGIVEOld
            + "|" + "NamaDebiturGIVEOld=" + NamaDebiturGIVEOld
            + "|" + "NamaGIVEOld=" + NamaGIVEOld
            + "|" + "AlamatGIVEOld=" + AlamatGIVEOld
            + "|" + "JenisKelaminGIVEOld=" + JenisKelaminGIVEOld
            + "|" + "TempatLahirGIVEOld=" + TempatLahirGIVEOld
            + "|" + "TglLahirGIVEOld=" + TglLahirGIVEOld
            + "|" + "KewarganegaraanGIVEOld=" + KewarganegaraanGIVEOld
            + "|" + "JenisidentitasGIVEOld=" + JenisidentitasGIVEOld
            + "|" + "NoidentitasGIVEOld=" + NoidentitasGIVEOld
            + "|" + "RTGIVEOld=" + RTGIVEOld
            + "|" + "RWGIVEOld=" + RWGIVEOld
            + "|" + "KelurahanGIVEOld=" + KelurahanGIVEOld
            + "|" + "KecamatanGIVEOld=" + KecamatanGIVEOld
            + "|" + "KabupatenKotaGIVEOld=" + KabupatenKotaGIVEOld
            + "|" + "ProvinsiGIVEOld=" + ProvinsiGIVEOld
            + "|" + "KotaPengadilanGIVEOld=" + KotaPengadilanGIVEOld
            + "|" + "PoskodeGIVEOld=" + PoskodeGIVEOld;

            string Newvalue = "NamaPIC=" + NamaPIC
           + "|" + "AlamatPIC=" + AlamatPIC
           + "|" + "JenisKelaminPIC=" + JenisKelaminPIC
           + "|" + "TempatLahirPIC=" + TempatLahirPIC
           + "|" + "TglLahirPIC=" + TglLahirPIC
           + "|" + "JabatanPIC=" + JabatanPIC
           + "|" + "KewarganegaraanPIC=" + KewarganegaraanPIC
           + "|" + "JenisidentitasPIC=" + JenisidentitasPIC
           + "|" + "NoidentitasPIC=" + NoidentitasPIC
           + "|" + "RTPIC=" + RTPIC
           + "|" + "RWPIC=" + RWPIC
           + "|" + "KelurahanPIC=" + KelurahanPIC
           + "|" + "KecamatanPIC=" + KecamatanPIC
           + "|" + "KabupatenKotaPIC=" + KabupatenKotaPIC
           + "|" + "ProvinsiPIC=" + ProvinsiPIC
           + "|" + "PoskodePIC=" + PoskodePIC
           + "|" + "PengurusORG=" + PengurusORG
           + "|" + "TIPEGIVE=" + TIPEGIVE
           + "|" + "NamaDebiturGIVE=" + NamaDebiturGIVE
           + "|" + "NamaGIVE=" + NamaGIVE
           + "|" + "AlamatGIVE=" + AlamatGIVE
           + "|" + "JenisKelaminGIVE=" + JenisKelaminGIVE
           + "|" + "TempatLahirGIVE=" + TempatLahirGIVE
           + "|" + "TglLahirGIVE=" + TglLahirGIVE
           + "|" + "KewarganegaraanGIVE=" + KewarganegaraanGIVE
           + "|" + "JenisidentitasGIVE=" + JenisidentitasGIVE
           + "|" + "NoidentitasGIVE=" + NoidentitasGIVE
           + "|" + "RTGIVE=" + RTGIVE
           + "|" + "RWGIVE=" + RWGIVE
           + "|" + "KelurahanGIVE=" + KelurahanGIVE
           + "|" + "KecamatanGIVE=" + KecamatanGIVE
           + "|" + "KabupatenKotaGIVE=" + KabupatenKotaGIVE
           + "|" + "ProvinsiGIVE=" + ProvinsiGIVE
           + "|" + "KotaPengadilanGIVE=" + KotaPengadilanGIVE;

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
                             {"IDFDC",idFDC},
                             {"NoPerjanjian",NoPerjanjian},
                             {"SelectClient",ClientIDS},
                             {"JenisDocument",JenisCust},
                             {"SelectJenisKontrak",conttype},
                             {"SelectDocStatus",statuscek.ToString()},
                             {"Keterangan",Keterangan},
                             {"ParamValue",paramvalue},

                             {"NamaPIC",NamaPIC},
                             {"AlamatPIC",AlamatPIC},
                             {"JenisKelaminPIC",JenisKelaminPIC},
                             {"TempatLahirPIC",TempatLahirPIC},
                             {"TglLahirPIC",TglLahirPIC},
                             {"PekerjaanPIC",PekerjaanPIC},
                             {"JabatanPIC",JabatanPIC},
                             {"KewarganegaraanPIC",KewarganegaraanPIC},
                             {"JenisidentitasPIC",JenisidentitasPIC},
                             {"NoidentitasPIC",NoidentitasPIC},
                             {"NoContactPIC",NoContactPIC},
                             {"RTPIC",RTPIC},
                             {"RWPIC",RWPIC},
                             {"KelurahanPIC",KelurahanPIC},
                             {"KecamatanPIC",KecamatanPIC},
                             {"KabupatenKotaPIC",KabupatenKotaPIC},
                             {"ProvinsiPIC",ProvinsiPIC},
                             {"PoskodePIC",PoskodePIC},
                             {"PengurusORG",PengurusORG},

                             {"TIPEGIVE",TIPEGIVE},
                             {"NamaDebiturGIVE",NamaDebiturGIVE},

                             {"NamaGIVE",NamaGIVE},
                             {"AlamatGIVE",AlamatGIVE},
                             {"JenisKelaminGIVE",JenisKelaminGIVE},
                             {"TempatLahirGIVE",TempatLahirGIVE},
                             {"TglLahirGIVE",TglLahirGIVE},
                             {"PekerjaanGIVE",PekerjaanGIVE},
                             {"JabatanGIVE",JabatanGIVE},
                             {"KewarganegaraanGIVE",KewarganegaraanGIVE},
                             {"JenisidentitasGIVE",JenisidentitasGIVE},
                             {"NoidentitasGIVE",NoidentitasGIVE},
                             {"NoContactGIVE",NoContactGIVE},
                             {"RTGIVE",RTGIVE},
                             {"RWGIVE",RWGIVE},
                             {"KelurahanGIVE",KelurahanGIVE},
                             {"KecamatanGIVE",KecamatanGIVE},
                             {"KabupatenKotaGIVE",KabupatenKotaGIVE},
                             {"ProvinsiGIVE",ProvinsiGIVE},
                             {"PoskodeGIVE",PoskodeGIVE},
                             {"PengadilanKota",KotaPengadilanGIVE },

                             {"IsGenerateAkta",isRegenerateakta.ToString()},
                             {"OLDvalue",OLDvalue},
                             {"Newvalue",Newvalue},

                             {"idcaption",moduleID},
                             {"UserID",userid},
                             {"GroupName",groupname},
                        };


                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasanstatusupd.GetDescriptionEnums().ToString();
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
        public async Task<List<String>> dbGetPemberkasanAktaListCount(string SelectClient, string fromdate, string SelectRequest, string SelectRequestStatus, int PageNumber, string idcaption, string userid, string groupname)
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
                    model.SelectClient = "";
                    model.fromdate = fromdate;
                    model.Req_type = "";
                    model.status = int.Parse("0");
                    model.UserID = userid;
                    model.idcaption = idcaption;
                    model.GroupName = groupname;


                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetAktaRegisList.GetDescriptionEnums().ToString();
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
        public async Task<List<DataTable>> dbGetPemberkasanAktaList(DataTable DTFromDB, string SelectClient, string fromdate, string SelectRequest, string SelectRequestStatus, int PageNumber, double pagenumberclient, double pagingsizeclient, string idcaption, string userid, string groupname)
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
                        model.CrunchCiber = true;
                        model.SelectClient = "";
                        model.fromdate = fromdate;
                        model.Req_type = "";
                        model.status = int.Parse("0");
                        model.UserID = userid;
                        model.idcaption = idcaption;
                        model.GroupName = groupname;
                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextPemberkasan.cmdGetAktaRegisList.GetDescriptionEnums().ToString();
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


        public async Task<List<String>> dbGetPemberkasanMappingListCount(cFilterContract model)
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

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasanmap.GetDescriptionEnums().ToString();
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

        public async Task<List<DataTable>> dbGetPemberkasanMappingList(DataTable DTFromDB, cFilterContract model, double pagenumberclient, double pagingsizeclient)
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

                        var stringPayload = JsonConvert.SerializeObject(model);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasanmap.GetDescriptionEnums().ToString();
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


        public async Task<byte[]> dbGetPemberkasanDwnwrkhnotaris(string NoPerjanjian, string Client_Fdc_ID, string JenisKontrak, string JenisDocument, string idcaption, string userid, string groupname)
        {

            byte[] dt = new byte[] { 0x20 };
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
                               {"ID", Client_Fdc_ID},
                               {"NoPerjanjian", NoPerjanjian},
                               {"SelectJenisKontrak", JenisKontrak},
                               {"JenisDocument",JenisDocument },
                               {"idcaption",idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetPemberkasandoc4ZipNotaris.GetDescriptionEnums().ToString();
                    var responsed = client.PostAsync(cmdtextapi, content).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        var bytear = responsed.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
                        dt = Convert.FromBase64String(bytear);
                    }
                }
            }
            return dt;

        }


        public async Task<DataTable> dbGetRptPemberkasan(string ClientIDS, string BranchIDS, string region, string JenisNasabah, string JenisKontrak, string fromdate, string todate, string SelectWarkahStatus, string NoPerjanjian, string idcaption, string userid, string groupname)
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
                               {"SelectJenisKontrak", JenisKontrak},
                               {"NoPerjanjian", NoPerjanjian},
                               {"SelectClient", ClientIDS},
                               {"SelectRegion", region},
                               {"SelectBranch", BranchIDS},
                               {"SelectJenisPelanggan", JenisNasabah},
                               {"SelectWarkahStatus", SelectWarkahStatus},
                               {"fromdate", fromdate},
                               {"todate", todate},
                               {"idcaption",idcaption},
                               {"UserID", userid},
                               {"GroupName", groupname},
                            };

                    var stringPayload = JsonConvert.SerializeObject(model);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    string cmdtextapi = cCommandTextPemberkasan.cmdGetRptPemberkasan.GetDescriptionEnums().ToString();
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

    }



}