using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using HashNetFramework;
using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using Spire.Pdf;
using Spire.Pdf.AutomaticFields;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml;
using Xceed.Words.NET;
namespace DusColl.Controllers
{
    public class HTLController : Controller
    {


        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmHTL HTL = new vmHTL();
        vmHTLddl HTLddl = new vmHTLddl();
        blHTLddl HTLbl = new blHTLddl();

        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();

        string tempTransksi = "HTLdtlist";
        string tempTransksifilter = "HTLlistfilter";
        string tempcommon = "common";
        string MainControllerNameHeaderTx = "HTL";
        string MainActionNameHeaderTx = "clnHeaderTx";


        //[HttpPost]
        //public ActionResult Save(string data4, string difname)
        //{
        //    string base64 = Request.Form["imgCropped"];
        //    byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
        //    base64 = Convert.ToBase64String(bytes);

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://api.verihubs.com/ktp/id/extract-async");
        //    client.DefaultRequestHeaders.Add("api-key", "4e50972b-8809-42e4-ac35");
        //    client.DefaultRequestHeaders.Add("app-id", "4e50972b-8809-42e4-ac35");
        //    MultipartFormDataContent form = new MultipartFormDataContent();
        //    HttpContent content = new StringContent("uploadFiles");
        //    form.Add(content, "uploadFiles");
        //    var stream = new MemoryStream(bytes);
        //    content = new StreamContent(stream);
        //    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //    {
        //        //Name = "uploadFiles",
        //        //FileName = "fle.png",
        //        //validate_quality="true",
        //        //reference_id="Sedayu",
        //        //callback_url= "https://webhook.site/42715001-f37e-4bfe-8b51-0d91413f8c63"

        //    };
        //    form.Add(content);
        //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //    var response = client.PostAsync("/ocr_api/ocr/...", form).Result;
        //    var k = response.Content.ReadAsStringAsync().Result;
        //    k = "{\"Flag\":\"Y\",\"Message\":\"Success\",\"Data\":{\"Provinsi\":\"SUMATERA SELATAN\",\"Kota\":\"MUSI BANYUASIN\",\"Nik\":\"1606085203720001\",\"Nama\":\"KISWATI\",\"TempatLahir\":\"SIMPANG SARI\",\"KelaminTemp\":\"PEREMPUAN\",\"Alamat\":\"JL. KOMPRES\",\"Rt\":\"011\",\"Rw\":\"003\",\"Kel\":\"KELUANG\",\"Kec\":\"KELUANG\",\"Agama\":\"ISLAM\",\"StatusPerkawinan\":\"KAWIN\",\"Kewarganegaraan\":\"WNI\",\"BerlakuHingga\":\"19 07-2018\",\"JenisKelaminId\":\"2\",\"JenisKelaminDesc\":\"Perempuan\",\"TanggalLahir\":\"1972-03-12\"}}";

        //    //JavaScriptSerializer j = new JavaScriptSerializer();
        //    //OCRData output = JsonConvert.DeserializeObject<OCRData>(k);

        //    dynamic jsondata = JObject.Parse(k);
        //    string vrProvinsi = "";
        //    string vrKota = "";
        //    string vrNik = "";
        //    string vrNama = "";
        //    string vrTempatLahir = "";
        //    string vrKelaminTemp = "";
        //    string vrAlamat = "";
        //    string vrRt = "";
        //    string vrRw = "";
        //    string vrKel = "";
        //    string vrKec = "";
        //    string vrAgama = "";
        //    string vrStatusPerkawinan = "";
        //    string vrKewarganegaraan = "";
        //    string vrJenisKelaminDesc = "";
        //    string vrTanggalLahir = "";
        //    if (jsondata.Flag.ToString() == "Y")
        //    {
        //        vrProvinsi = jsondata.Data.Provinsi;
        //        vrKota = jsondata.Data.Kota;
        //        vrNik = jsondata.Data.Nik;
        //        vrNama = jsondata.Data.Nama;
        //        vrTempatLahir = jsondata.Data.TempatLahir;
        //        vrKelaminTemp = jsondata.Data.KelaminTemp;
        //        vrAlamat = jsondata.Data.Alamat;
        //        vrRt = jsondata.Data.Rt;
        //        vrRw = jsondata.Data.Rw;
        //        vrKel = jsondata.Data.Kel;
        //        vrKec = jsondata.Data.Kec;
        //        vrAgama = jsondata.Data.Agama;
        //        vrStatusPerkawinan = jsondata.Data.StatusPerkawinan;
        //        vrKewarganegaraan = jsondata.Data.Kewarganegaraan;
        //        vrJenisKelaminDesc = jsondata.Data.JenisKelaminDesc;
        //        vrTanggalLahir = jsondata.Data.TanggalLahir;
        //    }

        //    return Json(new
        //    {
        //        Provinsi = vrProvinsi,
        //        Kota = vrKota,
        //        Nik = vrNik,
        //        Nama = vrNama,
        //        TempatLahir = vrTempatLahir,
        //        KelaminTemp = vrKelaminTemp,
        //        Alamat = vrAlamat,
        //        Rt = vrRt,
        //        Rw = vrRw,
        //        Kel = vrKel,
        //        Kec = vrKec,
        //        Agama = vrAgama,
        //        StatusPerkawinan = vrStatusPerkawinan,
        //        Kewarganegaraan = vrKewarganegaraan,
        //        JenisKelaminDesc = vrJenisKelaminDesc,
        //        TanggalLahir = vrTanggalLahir,
        //        Status = "",
        //        ocr4 = data4 ?? "",
        //        sm = difname ?? "",
        //        moderror = false
        //    });
        //}
        [HttpPost]
        public ActionResult Save(string data4, string difname)
        {

            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string base64 = Request.Form["imgCropped"];
                byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);

                base64 = Convert.ToBase64String(bytes);

                /* remark untuk icloud 
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://43.231.129.73/");
                client.DefaultRequestHeaders.Add("api-key", "4e50972b-8809-42e4-ac35-d9d873b55886");
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpContent content = new StringContent("uploadFiles");
                form.Add(content);
                var stream = new MemoryStream(bytes);
                content = new StreamContent(stream);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "uploadFiles",
                    FileName = "fle.png"
                };
                form.Add(content);
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var response = client.PostAsync("/ocr_api/ocr/demoktp", form).Result;
                var k = response.Content.ReadAsStringAsync().Result;
                */

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://api.verihubs.com/");
                client.DefaultRequestHeaders.Add("API-Key", "PmjA+HuCdaQCa+QW413rTWhJSWX7uTUn");
                client.DefaultRequestHeaders.Add("App-ID", "68d5875e-1b0b-468c-8052-cb470084f598");

                var param = new Dictionary<string, string>
                        {
                           {"validate_quality", "true"},
                           {"image", base64},
                        };
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var response = client.PostAsync("/ktp/id/extract-async", new FormUrlEncodedContent(param)).Result;
                var k = response.Content.ReadAsStringAsync().Result;

                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //var response = client.PostAsync("/ocr_api/ocr/...", form).Result;
                //var k = response.Content.ReadAsStringAsync().Result;
                //k = "{\"Flag\":\"Y\",\"Message\":\"Success\",\"Data\":{\"Provinsi\":\"SUMATERA SELATAN\",\"Kota\":\"MUSI BANYUASIN\",\"Nik\":\"1606085203720001\",\"Nama\":\"KISWATI\",\"TempatLahir\":\"SIMPANG SARI\",\"KelaminTemp\":\"PEREMPUAN\",\"Alamat\":\"JL. KOMPRES\",\"Rt\":\"011\",\"Rw\":\"003\",\"Kel\":\"KELUANG\",\"Kec\":\"KELUANG\",\"Agama\":\"ISLAM\",\"StatusPerkawinan\":\"KAWIN\",\"Kewarganegaraan\":\"WNI\",\"BerlakuHingga\":\"19 07-2018\",\"JenisKelaminId\":\"2\",\"JenisKelaminDesc\":\"Perempuan\",\"TanggalLahir\":\"1972-03-12\"}}";

                JavaScriptSerializer j = new JavaScriptSerializer();
                OCRData output = JsonConvert.DeserializeObject<OCRData>(k);

                dynamic jsondata = JObject.Parse(k);
                string vrProvinsi = "";
                string vrKota = "";
                string vrNik = "";
                string vrNama = "";
                string vrTempatLahir = "";
                string vrKelaminTemp = "";
                string vrAlamat = "";
                string vrRt = "";
                string vrRw = "";
                string vrKel = "";
                string vrKec = "";
                string vrAgama = "";
                string vrPekerjaan = "";
                string vrStatusPerkawinan = "";
                string vrKewarganegaraan = "";
                string vrJenisKelaminDesc = "";
                string vrTanggalLahir = "";

                if (jsondata.Data.KelaminTemp.ToString().ToLower() == "laki-laki")
                {
                    jsondata.Data.KelaminTemp = "laki-laki";

                }
                else if (jsondata.Data.KelaminTemp.ToString().ToLower() == "perempuan")
                {
                    jsondata.Data.KelaminTemp = "Perempuan";
                }

                if (jsondata.Data.StatusPerkawinan.ToString().ToLower() == "belum kawin")
                {
                    jsondata.Data.StatusPerkawinan = "Belum Menikah";
                }
                else if (jsondata.Data.StatusPerkawinan.ToString().ToLower() == "kawin")
                {
                    jsondata.Data.StatusPerkawinan = "Menikah";
                }


                if ((jsondata.Data.TanggalLahir ?? "") != "")
                {
                    jsondata.Data.TanggalLahir = DateTime.Parse(jsondata.Data.TanggalLahir.ToString()).ToString("dd-MMMM-yyyy");
                }


                if (jsondata.Flag.ToString() == "Y")
                {
                    vrProvinsi = jsondata.Data.Provinsi;
                    vrKota = jsondata.Data.Kota;
                    vrNik = jsondata.Data.Nik;
                    vrNama = jsondata.Data.Nama;
                    vrTempatLahir = jsondata.Data.TempatLahir;
                    vrKelaminTemp = jsondata.Data.KelaminTemp;
                    vrAlamat = jsondata.Data.Alamat;
                    vrRt = jsondata.Data.RT;
                    vrRw = jsondata.Data.RW;
                    vrKel = jsondata.Data.Kel;
                    vrKec = jsondata.Data.Kec;
                    vrAgama = jsondata.Data.Agama;
                    vrPekerjaan = jsondata.Data.Pekerjaan;
                    vrStatusPerkawinan = jsondata.Data.StatusPerkawinan;
                    vrKewarganegaraan = jsondata.Data.Kewarganegaraan;
                    vrJenisKelaminDesc = jsondata.Data.KelaminTemp;
                    vrTanggalLahir = jsondata.Data.TanggalLahir;
                }

                return Json(new
                {
                    Provinsi = vrProvinsi,
                    Kota = vrKota,
                    Nik = vrNik,
                    Nama = vrNama,
                    TempatLahir = vrTempatLahir,
                    KelaminTemp = vrKelaminTemp,
                    Alamat = vrAlamat,
                    Rt = vrRt,
                    Rw = vrRw,
                    Kel = vrKel,
                    Kec = vrKec,
                    Agama = vrAgama,
                    Pekerjaan = vrPekerjaan,
                    StatusPerkawinan = vrStatusPerkawinan,
                    Kewarganegaraan = vrKewarganegaraan,
                    JenisKelaminDesc = vrJenisKelaminDesc,
                    TanggalLahir = vrTanggalLahir,
                    Status = "",
                    ocr4 = data4 ?? "",
                    sm = difname ?? "",
                    moderror = false
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public async Task<ActionResult> clnHeaderTxHT(String menu, String caption, string kd, string tipemodule)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }


            try
            {

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string ClientID = Account.AccountLogin.ClientID;
                string IDCabang = Account.AccountLogin.IDCabang;
                string IDNotaris = Account.AccountLogin.IDNotaris;
                string Region = Account.AccountLogin.Region;
                string GroupName = Account.AccountLogin.GroupName;
                string ClientName = Account.AccountLogin.ClientName;
                string CabangName = Account.AccountLogin.CabangName;
                string Mailed = Account.AccountLogin.Mailed;
                string GenMoon = Account.AccountLogin.GenMoon;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string idcaption = HasKeyProtect.Encryption(caption);

                var chkstau = caption.Substring(caption.Length - 2, 2);
                if (chkstau == "on")
                {
                    caption = caption.Replace(chkstau, "");
                }

                // extend //
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //


                // some field must be overide first for default filter//
                string Divisi = "";
                string Cabang = "";
                string Area = "";
                string RequestNo = "";
                string FromDate = "";
                string Todate = "";
                int Status = (menu == "renew") ? 10 : -1; //default aktif


                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;
                int statuspro = 0;

                menuitemdescription = "DAFTAR LIST BPN ";

                if (caption == "HTLLISTHT1")
                {
                    statuspro = 49; //ditanguhkan
                    menuitemdescription = menuitemdescription + "(PENGAJUAN DITANGGUHKAN)";
                }
                else
                if (caption == "HTLLISTHT2")
                {
                    statuspro = 50;  //ditutup
                    menuitemdescription = menuitemdescription + "(PENGAJUAN DITUTUP)";
                }
                else if (caption == "HTLLISTHT3")
                {
                    statuspro = 48; //menunggu hasil
                    menuitemdescription = menuitemdescription + "(MENUNGGU HASIL BPN)";
                }
                else if (caption == "HTLLISTHT4")
                {
                    statuspro = 54; //terbit HT
                    menuitemdescription = menuitemdescription + "(TERBIT HT)";
                }
                else if (caption == "HTLLISTHT5")
                {
                    statuspro = 99; //terbit HT
                    menuitemdescription = menuitemdescription + "(PENGAJUAN BARU)";
                }
                else if (caption == "HTLLISTHT")
                {
                    statuspro = -1; //all
                    kd = tipemodule ?? "";

                }
                else if (caption == "HTLLISTHT6")
                {
                    statuspro = 999; //terbit HT
                    if (int.Parse(UserTypes) == (int)UserType.FDCM)
                    {
                        menuitemdescription = "KONFIRMASI BAST";
                    }
                    else if (int.Parse(UserTypes) == (int)UserType.Notaris)
                    {
                        menuitemdescription = "PEMBUATAN BAST";
                    }
                    else if (int.Parse(UserTypes) == (int)UserType.Branch)
                    {
                        menuitemdescription = "PENERIMAAN BAST";
                    }
                }

                // try show filter data//
                //if (caption == "HTLLISTHT")
                //{
                //    List<String> recordPage = await HTLddl.dbGetHeaderTxListHTCount(RequestNo, Divisi, Cabang, Area, FromDate, Todate, Status, PageNumber, caption, UserID, GroupName);
                //    TotalRecord = Convert.ToDouble(recordPage[0]);
                //    TotalPage = Convert.ToDouble(recordPage[1]);
                //    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                //    pagenumberclient = PageNumber;
                //    List<DataTable> dtlist = await HTLddl.dbGetHeaderTxHTList(null, RequestNo, Divisi, Cabang, Area, FromDate, Todate, Status, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                //    totalRecordclient = dtlist[0].Rows.Count;
                //    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());
                //    HTL.DTAllTx = dtlist[0];
                //    HTL.DTHeaderTx = dtlist[1];
                //}
                bool chec = (chkstau == "on") ? true : false;
                kd = kd ?? "";
                HTL.DTAllTx = await HTLddl.dbGetHeaderTxHTCheckedList(statuspro, chec, UserID, kd);
                TotalRecord = HTL.DTAllTx.Rows.Count;



                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                modFilter.ModuleName = caption;
                modFilter.ModuleID = idcaption;
                modFilter.idcaption = idcaption;

                modFilter.UserTypes = UserTypes;

                //set to object pendataran//

                HTL.FilterTransaksi = modFilter;
                HTL.Permission = PermisionModule;


                //if (Common.ddlhak == null)
                //{
                //    Common.ddlhak = await Commonddl.dbdbGetDdlEnumsListByEncrypt("HAK", caption, UserID, GroupName);
                //}

                //if (Common.ddlnotaris == null)
                // {
                //   Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, UserID, GroupName);

                // if (int.Parse(UserTypes) == (int)UserType.Notaris)
                //{
                //  string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                //Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                //}

                //}


                //Common.ddlstatus = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATHDL", caption, UserID, GroupName, "99");


                ViewBag.ShowNotaris = "";
                if (int.Parse(UserTypes) == (int)UserType.FDCM)
                {
                    ViewBag.ShowNotaris = "allow";
                }
                ViewBag.UserTypess = UserTypes;

                //ViewData["SelectHak"] = OwinLibrary.Get_SelectListItem(Common.ddlhak);
                //ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);
                //ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlstatus);



                //set session filterisasi //
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "HTL";
                ViewBag.action = "clnHeaderTxHT";

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                if (chkstau == "on")
                {
                    ViewBag.chk = "on";
                }
                else
                {
                    ViewBag.chk = "off";
                }
                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLLstDHT.cshtml", HTL),
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorPUB.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public async Task<ActionResult> clnHeaderTx(String menu, String caption)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }


            try
            {

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string ClientID = Account.AccountLogin.ClientID;
                string IDCabang = Account.AccountLogin.IDCabang;
                string IDNotaris = Account.AccountLogin.IDNotaris;
                string Region = Account.AccountLogin.Region;
                string GroupName = Account.AccountLogin.GroupName;
                string ClientName = Account.AccountLogin.ClientName;
                string CabangName = Account.AccountLogin.CabangName;
                string Mailed = Account.AccountLogin.Mailed;
                string GenMoon = Account.AccountLogin.GenMoon;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string idcaption = HasKeyProtect.Encryption(caption);

                // extend //
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //


                // some field must be overide first for default filter//
                string Divisi = "";
                string Cabang = "";
                string Area = "";
                string RequestNo = "";
                string FromDate = "";
                string Todate = "";
                int Status = (menu == "renew") ? 10 : -1; //default aktif


                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;


                // try show filter data//
                List<String> recordPage = await HTLddl.dbGetHeaderTxListCount(RequestNo, Divisi, Cabang, Area, FromDate, Todate, Status, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await HTLddl.dbGetHeaderTxList(null, RequestNo, Divisi, Cabang, Area, FromDate, Todate, Status, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                modFilter.ModuleName = caption;
                modFilter.ModuleID = idcaption;
                modFilter.idcaption = idcaption;

                modFilter.UserTypes = UserTypes;

                //set to object pendataran//
                HTL.DTAllTx = dtlist[0];
                HTL.DTHeaderTx = dtlist[1];
                HTL.FilterTransaksi = modFilter;
                HTL.Permission = PermisionModule;


                if (Common.ddlhak == null)
                {
                    Common.ddlhak = await Commonddl.dbdbGetDdlEnumsListByEncrypt("HAK", caption, UserID, GroupName);
                }

                //if (Common.ddlnotaris == null)
                // {
                //   Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, UserID, GroupName);

                // if (int.Parse(UserTypes) == (int)UserType.Notaris)
                //{
                //  string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                //Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                //}

                //}


                //Common.ddlstatus = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATHDL", caption, UserID, GroupName, "99");


                ViewBag.ShowNotaris = "";
                if (int.Parse(UserTypes) == (int)UserType.FDCM)
                {
                    ViewBag.ShowNotaris = "allow";
                }
                ViewBag.UserTypess = UserTypes;

                //ViewData["SelectHak"] = OwinLibrary.Get_SelectListItem(Common.ddlhak);
                //ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);
                //ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlstatus);



                //set session filterisasi //
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "HTL";
                ViewBag.action = "clnHeaderTx";

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLLst.cshtml", HTL),
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorPUB.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //#region Master DIVISI
        //[HttpPost]
        //public async Task<ActionResult> clnMTDDVISI(String menu, String caption)
        //{

        //    Account = (vmAccount)Session["Account"];
        //    bool IsErrorTimeout = false;
        //    if (Account != null)
        //    {
        //        Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
        //        if (Account.AccountLogin.RouteName != "")
        //        {
        //            IsErrorTimeout = true;
        //        }
        //    }
        //    else
        //    {
        //        IsErrorTimeout = true;
        //    }

        //    if (IsErrorTimeout == true)
        //    {
        //        string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
        //        return Json(new
        //        {
        //            url = urlpath,
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    try
        //    {
        //        string UserID = Account.AccountLogin.UserID;
        //        string UserName = Account.AccountLogin.UserName;
        //        string ClientID = Account.AccountLogin.ClientID;
        //        string IDCabang = Account.AccountLogin.IDCabang;
        //        string IDNotaris = Account.AccountLogin.IDNotaris;
        //        string Region = Account.AccountLogin.Region;
        //        string GroupName = Account.AccountLogin.GroupName;
        //        string ClientName = Account.AccountLogin.ClientName;
        //        string CabangName = Account.AccountLogin.CabangName;
        //        string Mailed = Account.AccountLogin.Mailed;
        //        string GenMoon = Account.AccountLogin.GenMoon;
        //        string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
        //        string idcaption = HasKeyProtect.Encryption(caption);

        //        // extend //
        //        bool CrunchCiber = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.AllowDownload).SingleOrDefault();
        //        string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
        //        // extend //

        //        // some field must be overide first for default filter//
        //        string Keyword = "";

        //        // set default for paging//
        //        int PageNumber = 1;
        //        double TotalRecord = 0;
        //        double TotalPage = 0;
        //        double pagingsizeclient = 0;
        //        double pagenumberclient = 0;
        //        double totalRecordclient = 0;
        //        double totalPageclient = 0;

        //        modFilter.UserID = UserID;
        //        modFilter.UserName = UserName;
        //        modFilter.GroupName = GroupName;
        //        modFilter.MailerDaemoon = Mailed;
        //        modFilter.GenDeamoon = GenMoon;
        //        modFilter.UserType = int.Parse(UserTypes);
        //        modFilter.UserTypeApps = int.Parse(UserTypes);
        //        modFilter.idcaption = idcaption;
        //        modFilter.Menu = menuitemdescription;
        //        modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
        //        modFilter.keyword = Keyword;
        //        modFilter.PageNumber = PageNumber;


        //        // try show filter data//
        //        List<String> recordPage = await MasterDataddl.dbGetDivisiListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataddl.dbGetDivisiList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        totalRecordclient = dtlist[0].Rows.Count;
        //        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //        //set in filter for paging//
        //        modFilter.TotalRecord = TotalRecord;
        //        modFilter.TotalPage = TotalPage;
        //        modFilter.pagingsizeclient = pagingsizeclient;
        //        modFilter.totalRecordclient = totalRecordclient;
        //        modFilter.totalPageclient = totalPageclient;
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set to object pendataran//
        //        master.DTFromDB = dtlist[0];
        //        master.DTDetailForGrid = dtlist[1];
        //        master.MasterFilter = modFilter;



        //        if (Common.ddlPic == null)
        //        {
        //            Common.ddlPic = await Commonddl.dbdbGetDdlPICListByEncrypt("1", "", caption, UserID, GroupName);
        //        }

        //        if (Common.ddlPeriode == null)
        //        {
        //            Common.ddlPeriode = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CONTPERIOD", caption, UserID, GroupName);
        //        }

        //        if (Common.ddlTeamHO == null)
        //        {
        //            Common.ddlTeamHO = await Commonddl.dbdbGetDdlEnumsListByEncrypt("GRUPTEAM", caption, UserID, GroupName);
        //        }

        //        ViewData["SelectTeamHO"] = OwinLibrary.Get_SelectListItem(Common.ddlTeamHO);
        //        ViewData["SelectPIC"] = OwinLibrary.Get_SelectListItem(Common.ddlPic);
        //        ViewData["SelectPeriode"] = OwinLibrary.Get_SelectListItem(Common.ddlPeriode);



        //        //set session filterisasi //
        //        TempData["DVISIMList"] = master;
        //        TempData["DVISIMListFilter"] = modFilter;
        //        TempData["common"] = Common;


        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDDVISI";


        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString();


        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/uiMasterData.cshtml", master),
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
        //        if (IsErrorTimeout == false)
        //        {
        //            Response.StatusCode = 406;
        //            Response.TrySkipIisCustomErrors = true;
        //            urlpath = Url.Action("Index", "Error", new { area = "" });
        //        }
        //        return Json(new
        //        {
        //            url = urlpath,
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //}
        //public async Task<ActionResult> clnRgridListMTDDVISI(int paged)
        //{
        //    Account = (vmAccount)Session["Account"];
        //    bool IsErrorTimeout = false;
        //    if (Account != null)
        //    {
        //        Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
        //        if (Account.AccountLogin.RouteName != "")
        //        {
        //            IsErrorTimeout = true;
        //        }
        //    }
        //    else
        //    {
        //        IsErrorTimeout = true;
        //    }
        //    if (IsErrorTimeout == true)
        //    {
        //        string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
        //        return Json(new
        //        {
        //            url = urlpath,
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    try
        //    {
        //        // get from session //
        //        modFilter = TempData["DVISIMListFilter"] as cFilterMasterData;
        //        master = TempData["DVISIMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        // get value filter have been filter//
        //        string keyword = modFilter.keyword;

        //        // set & get for next paging //
        //        int pagenumberclient = paged;
        //        int PageNumber = modFilter.PageNumber;
        //        double pagingsizeclient = modFilter.pagingsizeclient;
        //        double TotalRecord = modFilter.TotalRecord;
        //        double totalRecordclient = modFilter.totalRecordclient;

        //        //descript some value for db//
        //        caption = HasKeyProtect.Decryption(caption);


        //        // try show filter data//
        //        List<DataTable> dtlist = await MasterDataddl.dbGetDivisiList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];
        //        bool isModeFilter = modFilter.isModeFilter;

        //        string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;


        //        //set session filterisasi //
        //        TempData["DVISIMList"] = master;
        //        TempData["DVISIMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/_uiGridMasterDataList.cshtml", master),
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
        //        if (IsErrorTimeout == false)
        //        {
        //            Response.StatusCode = 406;
        //            Response.TrySkipIisCustomErrors = true;
        //            urlpath = Url.Action("Index", "Error", new { area = "" });
        //        }
        //        return Json(new
        //        {
        //            url = urlpath,
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //}


        public async Task<ActionResult> clnOpenAddHTLIPTDBT()
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HashNetFramework.HasKeyProtect.Decryption(modFilter.idcaption);
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);


                string viewbro = "/Views/HTL/_IManData.cshtml";
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = "",
                    msg = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewbro, null),
                });


            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public async Task<ActionResult> clnOpenAddHTL(string paramkey, string oprfun)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                if ((paramkey ?? "") == "")
                {
                    modFilter = new cFilterContract();
                    HTL = new vmHTL();
                    modFilter.idcaption = HasKeyProtect.Encryption("HTLLIST");
                }
                else
                {
                    modFilter = TempData[tempTransksifilter] as cFilterContract;
                    HTL = TempData[tempTransksi] as vmHTL;
                }

                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HashNetFramework.HasKeyProtect.Decryption(modFilter.idcaption);

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);


                //if (int.Parse(UserTypes) == (int)UserType.Notaris)
                //{
                //    Common.ddlstatus = Common.ddlstatus.AsEnumerable().Where(x => x.Value == "6" || x.Value == "11" || x.Value == "7" || (int.Parse(x.Value) >= 20 && int.Parse(x.Value) < 30)).ToList();
                //}

                //if (int.Parse(UserTypes) == (int)UserType.HO || int.Parse(UserTypes) == (int)UserType.Branch)
                //{
                //    Common.ddlstatus = Common.ddlstatus.AsEnumerable().Where(x => x.Value == "0").ToList();
                //}

                //if (int.Parse(UserTypes) == (int)UserType.FDCM)
                //{
                //    Common.ddlstatus = Common.ddlstatus.AsEnumerable().Where(x => x.Value == "6" || x.Value == "10" || x.Value == "7" || (int.Parse(x.Value) >= 20 && int.Parse(x.Value) < 50)).ToList();
                //}

                //Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, UserID, GroupName);
                //if (int.Parse(UserTypes) == (int)UserType.Notaris)
                //{
                //    string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                //    Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                //}
                //ViewData["SelectHak"] = new MultiSelectList(Common.ddlhak, "Value", "Text");
                //ViewData["SelectNotaris"] = new MultiSelectList(Common.ddlnotaris, "Value", "Text");



                string Opr4view = "add";
                string keyup = paramkey;
                string keyup1 = "";
                string keyup2 = "";

                cHTL modeldata = new cHTL();
                if (HTL.DTAllTx is null)
                {
                    modeldata.IDHeaderTx = 0;
                    modeldata.TglOrder = "1984-04-29";
                    //modeldata.TgllahirDebitur = "1984-04-29";
                    modeldata.NoAppl = "";
                    modeldata.keylookupdataHTX = "";
                    modeldata.DataTNH = new DataTable();
                    modeldata.DataPSG = new DataTable();
                    modeldata.DataSRT = new DataTable();
                    modeldata.DataSRTPSG = new DataTable();
                    modeldata.Status = "0";
                }
                else
                {
                    DataRow dr = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                    if (dr != null)
                    {

                        Opr4view = "edit";
                        if (oprfun == "x4vw")
                        {
                            Opr4view = "view";
                        }


                        modeldata.IDHeaderTx = int.Parse(dr["Id"].ToString());
                        modeldata.TglOrder = dr["TglOrder"].ToString();
                        modeldata.NoBlanko = dr["NoBlanko"].ToString();
                        modeldata.NoSertifikat = dr["NoSertifikat"].ToString();
                        modeldata.NoAppl = dr["NoAppl"].ToString();
                        modeldata.JenisSertifikat = dr["JenisSertifikat"].ToString();
                        modeldata.NomorNIB = dr["NomorNIB"].ToString();
                        modeldata.NoSuratUkur = dr["NoSuratUkur"].ToString();
                        modeldata.LuasTanah = dr["LuasTanah"].ToString().Replace(".00", "");
                        modeldata.LokasiTanahDiProvinsi = dr["LokasiTanahDiProvinsi"].ToString();
                        modeldata.LokasiTanahDiKota = dr["LokasiTanahDiKota"].ToString();
                        modeldata.LokasiTanahDiKecamatan = dr["LokasiTanahDiKecamatan"].ToString();
                        modeldata.LokasiTanahDiDesaKelurahan = dr["LokasiTanahDiDesaKelurahan"].ToString();
                        modeldata.NamaDebitur = dr["Debitur"].ToString();
                        modeldata.WargaDebitur = dr["WargaDebitur"].ToString();
                        modeldata.JKelaminDebitur = dr["JKelaminDebitur"].ToString();
                        modeldata.TptLahirDebitur = dr["TptLahirDebitur"].ToString();
                        modeldata.TgllahirDebitur = dr["TgllahirDebitur"].ToString();
                        modeldata.PekerjaanDebitur = dr["PekerjaanDebitur"].ToString();
                        modeldata.NIKDebitur = dr["NIKDebitur"].ToString();
                        modeldata.JenisPengajuan = dr["JenisPengajuan"].ToString();
                        modeldata.JenisPengajuanDesc = dr["JenisPengajuanDesc"].ToString();
                        modeldata.AlamatDebitur = dr["AlamatDebitur"].ToString();
                        modeldata.ProvinsiDebitur = dr["ProvinsiDebitur"].ToString();
                        modeldata.KotaDebitur = dr["KotaDebitur"].ToString();
                        modeldata.RTDebitur = dr["RTDebitur"].ToString();
                        modeldata.RWDebitur = dr["RWDebitur"].ToString();
                        modeldata.StatusDebitur = dr["StatusDebitur"].ToString();
                        modeldata.KecamatanDebitur = dr["KecamatanDebitur"].ToString();
                        modeldata.DesaKelurahanDebitur = dr["DesaKelurahanDebitur"].ToString();
                        modeldata.PekerjaanPemilikSertifikat = dr["PekerjaanPemilikSertifikat"].ToString();
                        modeldata.NIKPemilikSertifikat = dr["NIKPemilikSertifikat"].ToString();
                        modeldata.JenisPengajuan = dr["JenisPengajuan"].ToString();
                        modeldata.JenisPengajuanDesc = dr["JenisPengajuanDesc"].ToString();
                        modeldata.NamaPemilikSertifikat = dr["NamaPemilikSertifikat"].ToString();

                        modeldata.JKelaminPemilikSertifikat = dr["JKelaminPemilikSertifikat"].ToString();
                        modeldata.TptlahirPemilikSertifikat = dr["TptlahirPemilikSertifikat"].ToString();
                        modeldata.TgllahirPemilikSertifikat = dr["TgllahirPemilikSertifikat"].ToString().Replace("00:00:00", "");
                        modeldata.WargaPemilikSertifikat = dr["WargaPemilikSertifikat"].ToString();

                        modeldata.AlamatPemilikSertifikat = dr["AlamatPemilikSertifikat"].ToString();
                        modeldata.ProvinsiPemilikSertifikat = dr["ProvinsiPemilikSertifikat"].ToString();
                        modeldata.KotaPemilikSertifikat = dr["KotaPemilikSertifikat"].ToString();
                        modeldata.KecamatanPemilikSertifikat = dr["KecamatanPemilikSertifikat"].ToString();
                        modeldata.DesaKelurahanPemilikSertifikat = dr["DesaKelurahanPemilikSertifikat"].ToString();

                        modeldata.OrderKeNotaris = dr["OrderKeNotaris"].ToString();
                        modeldata.NilaiHT = dr["NilaiHT"].ToString().Replace(".00", "");
                        modeldata.NilaiPinjamanDiterima = dr["NilaiTerimaNasabah"].ToString().Replace(".00", "");
                        modeldata.KodeAkta = dr["KodeAkta"].ToString();
                        modeldata.NoHT = dr["NoHT"].ToString();
                        modeldata.kodesht = dr["KodeSHT"].ToString();
                        modeldata.nosht = dr["NoSHT"].ToString();
                        modeldata.TglSertifikatCEK = dr["TglSertifikatCek"].ToString().Replace("00:00:00", "");
                        modeldata.TglSuratUkur = dr["TglSuratUkur"].ToString().Replace("00:00:00", "");

                        modeldata.JasaPengecekan = bool.Parse(dr["JasaPengecekan"].ToString());
                        modeldata.JasaValidasi = bool.Parse(dr["JasaValidasi"].ToString());
                        modeldata.SKMHT = bool.Parse(dr["SKMHT"].ToString());
                        modeldata.APHT_SHT = bool.Parse(dr["APHT_SHT"].ToString());
                        modeldata.ROYA = bool.Parse(dr["ROYA"].ToString());
                        modeldata.PENCORETAN_PTSL = bool.Parse(dr["PENCORETAN_PTSL"].ToString());
                        modeldata.KUASA_MENGAMBIL = bool.Parse(dr["KUASA_MENGAMBIL"].ToString());
                        modeldata.PNBP = bool.Parse(dr["PNBP"].ToString());
                        modeldata.ADM_HT = bool.Parse(dr["ADM_HT"].ToString());

                        modeldata.NoPerjanjian = dr["NoPerjanjian"].ToString();
                        modeldata.TglPerjanjian = dr["TglPerjanjian"].ToString();

                        modeldata.Keterangan = dr["Keterangan"].ToString();
                        modeldata.Status = dr["Status"].ToString();
                        modeldata.Statusdesc = dr["Statusdesc"].ToString();
                        modeldata.Statushakdesc = dr["Statushakdesc"].ToString();
                        //modeldata.StatusHT = dr["Statusdesc"].ToString();
                        modeldata.StatusHTdesc = dr["StatusHTDesc"].ToString();
                        modeldata.NamaCabang = dr["NamaCabang"].ToString();


                        modeldata.DeadlineSLA = dr["DeadlineSLA"].ToString();
                        modeldata.PosisiPenangan = dr["PosHandleIsue"].ToString();

                        //percobaan//
                        string perihal = dr["Perihal"].ToString();
                        string perihalPending = dr["AlasanPending"].ToString();
                        string perihalPendingAkd = dr["AlasanPendingAkd"].ToString();

                        string ShowPen = dr["ShowTabPend"].ToString();
                        string ShowPenAkd = dr["ShowTabPendAkd"].ToString();
                        string ShowTabTangan = dr["ShowTabTangan"].ToString();
                        string ShowTabFillSPA = dr["ShowTabFillSPA"].ToString();

                        modeldata.Case = perihal;
                        modeldata.CaseDesc = dr["PerihalDesc"].ToString();
                        modeldata.ShowTabCancel = dr["ShowTabCancel"].ToString();

                        modeldata.CaseCabPending = perihalPending;
                        modeldata.CaseCabPendingDesc = dr["AlasanPendingDesc"].ToString();

                        modeldata.CaseCabPendingAkd = perihalPendingAkd;
                        modeldata.CaseCabPendingAkdDesc = dr["AlasanPendingAkdDesc"].ToString();

                        modeldata.CaseCabDesc = dr["PerihalCabDesc"].ToString();

                        modeldata.noberkasht = dr["NoBerkasSHT"].ToString();
                        modeldata.noberkasceking = dr["NoBerkasCEK"].ToString();

                        modeldata.keylookupdataHTX = paramkey;

                        modeldata.JmlTerbitSPA = bool.Parse(dr["JmlTerbitSPA"].ToString());


                        //ambil data psangan debitur//
                        modeldata.DataPSG = await HTLddl.dbGetMultiData(modeldata.NoAppl, "0", caption, UserID, GroupName);
                        //ambil data tanah//
                        modeldata.DataTNH = await HTLddl.dbGetMultiData(modeldata.NoAppl, "1", caption, UserID, GroupName);
                        //ambil data SPA//
                        modeldata.DataTNHSPA = await HTLddl.dbGetMultiData(modeldata.NoAppl, "1", caption, UserID, GroupName);
                        //ambil data sertifikat//
                        modeldata.DataSRT = await HTLddl.dbGetMultiData(modeldata.NoAppl, "2", caption, UserID, GroupName);
                        //ambil data pasangan sertifikat//
                        modeldata.DataSRTPSG = await HTLddl.dbGetMultiData(modeldata.NoAppl, "3", caption, UserID, GroupName);

                        //ambil data upload document//
                        modeldata.DTDokumen = await Commonddl.dbdbGetJenisDokumen("0", modeldata.NoAppl, "4", caption, UserID, GroupName);

                        ViewBag.Nappl = modeldata.NoAppl;
                        ViewBag.Jndata1 = "1";
                        ViewBag.Jndata2 = "2";
                        ViewBag.Jndata3 = "0";
                        ViewBag.Jndata4 = "3";
                        ViewBag.ShowTabPend = ShowPen;
                        ViewBag.ShowTabPendAkad = ShowPenAkd;
                        ViewBag.ShowTabTangan = ShowTabTangan;
                        ViewBag.ShowTabFillSPA = ShowTabFillSPA;

                    }

                }

                if (Common.ddlDocument == null)
                {
                    Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenDll("-900", modeldata.NoAppl, "3", caption, UserID, GroupName);
                }
                ViewData["SelectDocumentReg"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                //if (Common.ddlstatus == null)
                //{
                Common.ddlstatus = await Commonddl.dbdbGetDdlEnumsListByEncryptNw("STATHDL", caption, UserID, GroupName, modeldata.Status);
                Common.ddlstatusmap = await Commonddl.dbdbGetDdlEnumsListByEncryptNwdt("MAPSTAT", caption, UserID, GroupName, modeldata.Status);
                //}
                ViewData["SelectStatus"] = new MultiSelectList(Common.ddlstatus, "Value", "Text");

                if (Common.ddlhak == null)
                {
                    Common.ddlhak = await Commonddl.dbdbGetDdlEnumsListByEncrypt("HAK", caption, UserID, GroupName, "99");
                }
                ViewData["SelectHak"] = new MultiSelectList(Common.ddlhak, "Value", "Text");

                if (Common.ddlJenPen == null)
                {
                    Common.ddlJenPen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JNAJU", caption, UserID, GroupName, "99");
                }
                ViewData["SelectJEN"] = new MultiSelectList(Common.ddlJenPen, "Value", "Text");

                if (Common.ddlJenKel == null)
                {
                    Common.ddlJenKel = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENKEL", caption, UserID, GroupName, "99");
                }
                ViewData["SelectJENKEL"] = new MultiSelectList(Common.ddlJenKel, "Value", "Text");

                if (Common.ddlStatKW == null)
                {
                    Common.ddlStatKW = await Commonddl.dbdbGetDdlEnumsListByEncrypt("WARGA", caption, UserID, GroupName, "99");
                }
                ViewData["SelectWarga"] = new MultiSelectList(Common.ddlStatKW, "Value", "Text");

                if (Common.ddlStatNKH == null)
                {
                    Common.ddlStatNKH = await Commonddl.dbdbGetDdlEnumsListByEncrypt("NIKAH", caption, UserID, GroupName, "99");
                }
                ViewData["SelectStatusNikah"] = new MultiSelectList(Common.ddlStatNKH, "Value", "Text");

                if (Common.ddlnotaris == null)
                {
                    Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, modeldata.NoAppl, UserID, GroupName);
                }
                if (int.Parse(UserTypes) == (int)UserType.Notaris)
                {
                    string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                    Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                }

                if (int.Parse(UserTypes) == (int)UserType.Branch && (int.Parse(modeldata.Status ?? "0") <= 0 || int.Parse(modeldata.Status ?? "0") == 6))
                {
                    Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, modeldata.NoAppl, UserID, GroupName);
                }

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);

                if (Common.ddlCase == null)
                {
                    Common.ddlCase = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CASE", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCase"] = new MultiSelectList(Common.ddlCase, "Value", "Text");

                if (Common.ddlCaseCab == null)
                {
                    Common.ddlCaseCab = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CASECAB", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCaseCab"] = new MultiSelectList(Common.ddlCaseCab, "Value", "Text");

                if (Common.ddlCasepen == null)
                {
                    Common.ddlCasepen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CASEPEN", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCasePending"] = new MultiSelectList(Common.ddlCasepen, "Value", "Text");

                if (Common.ddlCasepenAkd == null)
                {
                    Common.ddlCasepenAkd = await Commonddl.dbdbGetDdlEnumsListByEncrypt("PENAKD", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCasePendingAkd"] = new MultiSelectList(Common.ddlCasepenAkd, "Value", "Text");

                if (Common.ddlPosistionPenanganan == null)
                {
                    Common.ddlPosistionPenanganan = await Commonddl.dbdbGetDdlEnumsListByEncrypt("POSHDNLE", caption, UserID, GroupName, "99");
                }
                ViewData["SelectPosisiPenanganan"] = new MultiSelectList(Common.ddlPosistionPenanganan, "Value", "Text");


                ViewBag.showtabnotaris = "";
                string usrtp = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                if (int.Parse(usrtp) == (int)UserType.Notaris || int.Parse(usrtp) == (int)UserType.FDCM)
                {
                    ViewBag.showtabnotaris = "allow";
                }

                ViewBag.hidesave = "";
                if (int.Parse(usrtp) == (int)UserType.FDCM && Opr4view == "add")
                {
                    ViewBag.hidesave = "hidden";
                }

                ViewBag.btncaption = "Simpan";
                if ((int.Parse(usrtp) != (int)UserType.HO && int.Parse(usrtp) != (int)UserType.Branch) || (modeldata.Status == "21")) //pending
                {
                    ViewBag.btncaption = "Submit";
                }

                ViewBag.OprMenu = Opr4view == "add" ? "PENAMBAHAN DATA" : Opr4view == "edit" ? "PERUBAHAN DATA (" + modeldata.NoAppl + ")" : "";
                ViewBag.OprMenu = Opr4view == "view" ? "NO APLIKASI " + modeldata.NoAppl : ViewBag.OprMenu;
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;



                ViewBag.keyup = keyup;
                ViewBag.keyup1 = keyup1;
                ViewBag.keyup2 = keyup2;


                ////pengecekan on progress editable u nokontak //
                //if (((int.Parse(usrtp) == (int)UserType.Branch || int.Parse(usrtp) == (int)UserType.HO))
                //    && int.Parse((modeldata.Status ?? "0")) >= 10 && int.Parse((modeldata.Status ?? "0")) < 40)
                //{
                //    ViewBag.editkontrak = "allow";
                //    ViewBag.oprvalue = "view";
                //    ViewBag.hidesave = "show";
                //}

                string viewbro = "/Views/HTL/_uiHTLUpdNw.cshtml";
                if (int.Parse(usrtp) == (int)UserType.Branch)
                {
                    ViewBag.user = "cabang";
                }

                if (int.Parse(usrtp) == (int)UserType.Notaris)
                {
                    ViewBag.user = "notaris";
                    // viewbro = "/Views/HTL/_uiHTLUpd.cshtml"; //_uiHTLUpdNw.cshtml
                }

                if (int.Parse(usrtp) == (int)UserType.FDCM)
                {
                    ViewBag.user = "admin";
                }

                if (int.Parse(usrtp) == (int)UserType.FDCM && (modeldata.StatusHT == "47" || modeldata.StatusHT == "54"))
                {
                    ViewBag.allowht = "allow";
                }


                HTL.HeaderInfo = modeldata;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewbro, HTL.HeaderInfo),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }



        public async Task<ActionResult> clnOpenAddRoya(string paramkey, string oprfun)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                if ((paramkey ?? "") == "")
                {
                    modFilter = new cFilterContract();
                    HTL = new vmHTL();
                    modFilter.idcaption = HasKeyProtect.Encryption("HTLLIST");
                }
                else
                {
                    modFilter = TempData[tempTransksifilter] as cFilterContract;
                    HTL = TempData[tempTransksi] as vmHTL;
                }

                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HashNetFramework.HasKeyProtect.Decryption(modFilter.idcaption);

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);


                //if (int.Parse(UserTypes) == (int)UserType.Notaris)
                //{
                //    Common.ddlstatus = Common.ddlstatus.AsEnumerable().Where(x => x.Value == "6" || x.Value == "11" || x.Value == "7" || (int.Parse(x.Value) >= 20 && int.Parse(x.Value) < 30)).ToList();
                //}

                //if (int.Parse(UserTypes) == (int)UserType.HO || int.Parse(UserTypes) == (int)UserType.Branch)
                //{
                //    Common.ddlstatus = Common.ddlstatus.AsEnumerable().Where(x => x.Value == "0").ToList();
                //}

                //if (int.Parse(UserTypes) == (int)UserType.FDCM)
                //{
                //    Common.ddlstatus = Common.ddlstatus.AsEnumerable().Where(x => x.Value == "6" || x.Value == "10" || x.Value == "7" || (int.Parse(x.Value) >= 20 && int.Parse(x.Value) < 50)).ToList();
                //}

                //Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, UserID, GroupName);
                //if (int.Parse(UserTypes) == (int)UserType.Notaris)
                //{
                //    string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                //    Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                //}
                //ViewData["SelectHak"] = new MultiSelectList(Common.ddlhak, "Value", "Text");
                //ViewData["SelectNotaris"] = new MultiSelectList(Common.ddlnotaris, "Value", "Text");



                string Opr4view = "add";
                string keyup = paramkey;
                string keyup1 = "";
                string keyup2 = "";

                cHTL modeldata = new cHTL();
                if (HTL.DTAllTx is null)
                {
                    modeldata.IDHeaderTx = 0;
                    modeldata.TglOrder = "1984-04-29";
                    //modeldata.TgllahirDebitur = "1984-04-29";
                    modeldata.NoAppl = "";
                    modeldata.keylookupdataHTX = "";
                    modeldata.DataTNH = new DataTable();
                    modeldata.DataPSG = new DataTable();
                    modeldata.DataSRT = new DataTable();
                    modeldata.DataSRTPSG = new DataTable();
                    modeldata.Status = "0";
                }
                else
                {
                    DataRow dr = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                    if (dr != null)
                    {

                        Opr4view = "edit";
                        if (oprfun == "x4vw")
                        {
                            Opr4view = "view";
                        }


                        modeldata.IDHeaderTx = int.Parse(dr["Id"].ToString());
                        modeldata.TglOrder = dr["TglOrder"].ToString();
                        modeldata.NoBlanko = dr["NoBlanko"].ToString();
                        modeldata.NoSertifikat = dr["NoSertifikat"].ToString();
                        modeldata.NoAppl = dr["NoAppl"].ToString();
                        modeldata.JenisSertifikat = dr["JenisSertifikat"].ToString();
                        modeldata.NomorNIB = dr["NomorNIB"].ToString();
                        modeldata.NoSuratUkur = dr["NoSuratUkur"].ToString();
                        modeldata.LuasTanah = dr["LuasTanah"].ToString().Replace(".00", "");
                        modeldata.LokasiTanahDiProvinsi = dr["LokasiTanahDiProvinsi"].ToString();
                        modeldata.LokasiTanahDiKota = dr["LokasiTanahDiKota"].ToString();
                        modeldata.LokasiTanahDiKecamatan = dr["LokasiTanahDiKecamatan"].ToString();
                        modeldata.LokasiTanahDiDesaKelurahan = dr["LokasiTanahDiDesaKelurahan"].ToString();
                        modeldata.NamaDebitur = dr["Debitur"].ToString();
                        modeldata.WargaDebitur = dr["WargaDebitur"].ToString();
                        modeldata.JKelaminDebitur = dr["JKelaminDebitur"].ToString();
                        modeldata.TptLahirDebitur = dr["TptLahirDebitur"].ToString();
                        modeldata.TgllahirDebitur = dr["TgllahirDebitur"].ToString();
                        modeldata.PekerjaanDebitur = dr["PekerjaanDebitur"].ToString();
                        modeldata.NIKDebitur = dr["NIKDebitur"].ToString();
                        modeldata.JenisPengajuan = dr["JenisPengajuan"].ToString();
                        modeldata.JenisPengajuanDesc = dr["JenisPengajuanDesc"].ToString();
                        modeldata.AlamatDebitur = dr["AlamatDebitur"].ToString();
                        modeldata.ProvinsiDebitur = dr["ProvinsiDebitur"].ToString();
                        modeldata.KotaDebitur = dr["KotaDebitur"].ToString();
                        modeldata.RTDebitur = dr["RTDebitur"].ToString();
                        modeldata.RWDebitur = dr["RWDebitur"].ToString();
                        modeldata.StatusDebitur = dr["StatusDebitur"].ToString();
                        modeldata.KecamatanDebitur = dr["KecamatanDebitur"].ToString();
                        modeldata.DesaKelurahanDebitur = dr["DesaKelurahanDebitur"].ToString();
                        modeldata.PekerjaanPemilikSertifikat = dr["PekerjaanPemilikSertifikat"].ToString();
                        modeldata.NIKPemilikSertifikat = dr["NIKPemilikSertifikat"].ToString();
                        modeldata.JenisPengajuan = dr["JenisPengajuan"].ToString();
                        modeldata.JenisPengajuanDesc = dr["JenisPengajuanDesc"].ToString();
                        modeldata.NamaPemilikSertifikat = dr["NamaPemilikSertifikat"].ToString();

                        modeldata.JKelaminPemilikSertifikat = dr["JKelaminPemilikSertifikat"].ToString();
                        modeldata.TptlahirPemilikSertifikat = dr["TptlahirPemilikSertifikat"].ToString();
                        modeldata.TgllahirPemilikSertifikat = dr["TgllahirPemilikSertifikat"].ToString().Replace("00:00:00", "");
                        modeldata.WargaPemilikSertifikat = dr["WargaPemilikSertifikat"].ToString();

                        modeldata.AlamatPemilikSertifikat = dr["AlamatPemilikSertifikat"].ToString();
                        modeldata.ProvinsiPemilikSertifikat = dr["ProvinsiPemilikSertifikat"].ToString();
                        modeldata.KotaPemilikSertifikat = dr["KotaPemilikSertifikat"].ToString();
                        modeldata.KecamatanPemilikSertifikat = dr["KecamatanPemilikSertifikat"].ToString();
                        modeldata.DesaKelurahanPemilikSertifikat = dr["DesaKelurahanPemilikSertifikat"].ToString();

                        modeldata.OrderKeNotaris = dr["OrderKeNotaris"].ToString();
                        modeldata.NilaiHT = dr["NilaiHT"].ToString().Replace(".00", "");
                        modeldata.NilaiPinjamanDiterima = dr["NilaiTerimaNasabah"].ToString().Replace(".00", "");
                        modeldata.KodeAkta = dr["KodeAkta"].ToString();
                        modeldata.NoHT = dr["NoHT"].ToString();
                        modeldata.kodesht = dr["KodeSHT"].ToString();
                        modeldata.nosht = dr["NoSHT"].ToString();
                        modeldata.TglSertifikatCEK = dr["TglSertifikatCek"].ToString().Replace("00:00:00", "");
                        modeldata.TglSuratUkur = dr["TglSuratUkur"].ToString().Replace("00:00:00", "");

                        modeldata.JasaPengecekan = bool.Parse(dr["JasaPengecekan"].ToString());
                        modeldata.JasaValidasi = bool.Parse(dr["JasaValidasi"].ToString());
                        modeldata.SKMHT = bool.Parse(dr["SKMHT"].ToString());
                        modeldata.APHT_SHT = bool.Parse(dr["APHT_SHT"].ToString());
                        modeldata.ROYA = bool.Parse(dr["ROYA"].ToString());
                        modeldata.PENCORETAN_PTSL = bool.Parse(dr["PENCORETAN_PTSL"].ToString());
                        modeldata.KUASA_MENGAMBIL = bool.Parse(dr["KUASA_MENGAMBIL"].ToString());
                        modeldata.PNBP = bool.Parse(dr["PNBP"].ToString());
                        modeldata.ADM_HT = bool.Parse(dr["ADM_HT"].ToString());

                        modeldata.NoPerjanjian = dr["NoPerjanjian"].ToString();
                        modeldata.TglPerjanjian = dr["TglPerjanjian"].ToString();

                        modeldata.Keterangan = dr["Keterangan"].ToString();
                        modeldata.Status = dr["Status"].ToString();
                        modeldata.Statusdesc = dr["Statusdesc"].ToString();
                        modeldata.Statushakdesc = dr["Statushakdesc"].ToString();
                        //modeldata.StatusHT = dr["Statusdesc"].ToString();
                        modeldata.StatusHTdesc = dr["StatusHTDesc"].ToString();
                        modeldata.NamaCabang = dr["NamaCabang"].ToString();


                        modeldata.DeadlineSLA = dr["DeadlineSLA"].ToString();
                        modeldata.PosisiPenangan = dr["PosHandleIsue"].ToString();

                        //percobaan//
                        string perihal = dr["Perihal"].ToString();
                        string perihalPending = dr["AlasanPending"].ToString();
                        string perihalPendingAkd = dr["AlasanPendingAkd"].ToString();

                        string ShowPen = dr["ShowTabPend"].ToString();
                        string ShowPenAkd = dr["ShowTabPendAkd"].ToString();
                        string ShowTabTangan = dr["ShowTabTangan"].ToString();
                        string ShowTabFillSPA = dr["ShowTabFillSPA"].ToString();

                        modeldata.Case = perihal;
                        modeldata.CaseDesc = dr["PerihalDesc"].ToString();
                        modeldata.ShowTabCancel = dr["ShowTabCancel"].ToString();

                        modeldata.CaseCabPending = perihalPending;
                        modeldata.CaseCabPendingDesc = dr["AlasanPendingDesc"].ToString();

                        modeldata.CaseCabPendingAkd = perihalPendingAkd;
                        modeldata.CaseCabPendingAkdDesc = dr["AlasanPendingAkdDesc"].ToString();

                        modeldata.CaseCabDesc = dr["PerihalCabDesc"].ToString();

                        modeldata.noberkasht = dr["NoBerkasSHT"].ToString();
                        modeldata.noberkasceking = dr["NoBerkasCEK"].ToString();

                        modeldata.keylookupdataHTX = paramkey;

                        modeldata.JmlTerbitSPA = bool.Parse(dr["JmlTerbitSPA"].ToString());


                        //ambil data psangan debitur//
                        modeldata.DataPSG = await HTLddl.dbGetMultiData(modeldata.NoAppl, "0", caption, UserID, GroupName);
                        //ambil data tanah//
                        modeldata.DataTNH = await HTLddl.dbGetMultiData(modeldata.NoAppl, "1", caption, UserID, GroupName);
                        //ambil data SPA//
                        modeldata.DataTNHSPA = await HTLddl.dbGetMultiData(modeldata.NoAppl, "1", caption, UserID, GroupName);
                        //ambil data sertifikat//
                        modeldata.DataSRT = await HTLddl.dbGetMultiData(modeldata.NoAppl, "2", caption, UserID, GroupName);
                        //ambil data pasangan sertifikat//
                        modeldata.DataSRTPSG = await HTLddl.dbGetMultiData(modeldata.NoAppl, "3", caption, UserID, GroupName);

                        //ambil data upload document//
                        modeldata.DTDokumen = await Commonddl.dbdbGetJenisDokumen("0", modeldata.NoAppl, "4", caption, UserID, GroupName);

                        ViewBag.Nappl = modeldata.NoAppl;
                        ViewBag.Jndata1 = "1";
                        ViewBag.Jndata2 = "2";
                        ViewBag.Jndata3 = "0";
                        ViewBag.Jndata4 = "3";
                        ViewBag.ShowTabPend = ShowPen;
                        ViewBag.ShowTabPendAkad = ShowPenAkd;
                        ViewBag.ShowTabTangan = ShowTabTangan;
                        ViewBag.ShowTabFillSPA = ShowTabFillSPA;

                    }

                }

                if (Common.ddlDocument == null)
                {
                    Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenDll("-900", modeldata.NoAppl, "3", caption, UserID, GroupName);
                }
                ViewData["SelectDocumentReg"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                //if (Common.ddlstatus == null)
                //{
                Common.ddlstatus = await Commonddl.dbdbGetDdlEnumsListByEncryptNw("STATHDL", caption, UserID, GroupName, modeldata.Status);
                Common.ddlstatusmap = await Commonddl.dbdbGetDdlEnumsListByEncryptNwdt("MAPSTAT", caption, UserID, GroupName, modeldata.Status);
                //}
                ViewData["SelectStatus"] = new MultiSelectList(Common.ddlstatus, "Value", "Text");

                if (Common.ddlhak == null)
                {
                    Common.ddlhak = await Commonddl.dbdbGetDdlEnumsListByEncrypt("HAK", caption, UserID, GroupName, "99");
                }
                ViewData["SelectHak"] = new MultiSelectList(Common.ddlhak, "Value", "Text");

                if (Common.ddlJenPen == null)
                {
                    Common.ddlJenPen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JNAJU", caption, UserID, GroupName, "99");
                }
                ViewData["SelectJEN"] = new MultiSelectList(Common.ddlJenPen, "Value", "Text");

                if (Common.ddlJenKel == null)
                {
                    Common.ddlJenKel = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENKEL", caption, UserID, GroupName, "99");
                }
                ViewData["SelectJENKEL"] = new MultiSelectList(Common.ddlJenKel, "Value", "Text");

                if (Common.ddlStatKW == null)
                {
                    Common.ddlStatKW = await Commonddl.dbdbGetDdlEnumsListByEncrypt("WARGA", caption, UserID, GroupName, "99");
                }
                ViewData["SelectWarga"] = new MultiSelectList(Common.ddlStatKW, "Value", "Text");

                if (Common.ddlStatNKH == null)
                {
                    Common.ddlStatNKH = await Commonddl.dbdbGetDdlEnumsListByEncrypt("NIKAH", caption, UserID, GroupName, "99");
                }
                ViewData["SelectStatusNikah"] = new MultiSelectList(Common.ddlStatNKH, "Value", "Text");

                if (Common.ddlnotaris == null)
                {
                    Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, modeldata.NoAppl, UserID, GroupName);
                }
                if (int.Parse(UserTypes) == (int)UserType.Notaris)
                {
                    string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                    Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                }

                if (int.Parse(UserTypes) == (int)UserType.Branch && (int.Parse(modeldata.Status ?? "0") <= 0 || int.Parse(modeldata.Status ?? "0") == 6))
                {
                    Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, modeldata.NoAppl, UserID, GroupName);
                }

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);

                if (Common.ddlCase == null)
                {
                    Common.ddlCase = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CASE", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCase"] = new MultiSelectList(Common.ddlCase, "Value", "Text");

                if (Common.ddlCaseCab == null)
                {
                    Common.ddlCaseCab = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CASECAB", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCaseCab"] = new MultiSelectList(Common.ddlCaseCab, "Value", "Text");

                if (Common.ddlCasepen == null)
                {
                    Common.ddlCasepen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CASEPEN", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCasePending"] = new MultiSelectList(Common.ddlCasepen, "Value", "Text");

                if (Common.ddlCasepenAkd == null)
                {
                    Common.ddlCasepenAkd = await Commonddl.dbdbGetDdlEnumsListByEncrypt("PENAKD", caption, UserID, GroupName, "99");
                }
                ViewData["SelectCasePendingAkd"] = new MultiSelectList(Common.ddlCasepenAkd, "Value", "Text");

                if (Common.ddlPosistionPenanganan == null)
                {
                    Common.ddlPosistionPenanganan = await Commonddl.dbdbGetDdlEnumsListByEncrypt("POSHDNLE", caption, UserID, GroupName, "99");
                }
                ViewData["SelectPosisiPenanganan"] = new MultiSelectList(Common.ddlPosistionPenanganan, "Value", "Text");


                ViewBag.showtabnotaris = "";
                string usrtp = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                if (int.Parse(usrtp) == (int)UserType.Notaris || int.Parse(usrtp) == (int)UserType.FDCM)
                {
                    ViewBag.showtabnotaris = "allow";
                }

                ViewBag.hidesave = "";
                if (int.Parse(usrtp) == (int)UserType.FDCM && Opr4view == "add")
                {
                    ViewBag.hidesave = "hidden";
                }

                ViewBag.btncaption = "Simpan";
                if ((int.Parse(usrtp) != (int)UserType.HO && int.Parse(usrtp) != (int)UserType.Branch) || (modeldata.Status == "21")) //pending
                {
                    ViewBag.btncaption = "Submit";
                }

                ViewBag.OprMenu = Opr4view == "add" ? "PENAMBAHAN DATA" : Opr4view == "edit" ? "PERUBAHAN DATA (" + modeldata.NoAppl + ")" : "";
                ViewBag.OprMenu = Opr4view == "view" ? "NO APLIKASI " + modeldata.NoAppl : ViewBag.OprMenu;
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;



                ViewBag.keyup = keyup;
                ViewBag.keyup1 = keyup1;
                ViewBag.keyup2 = keyup2;


                ////pengecekan on progress editable u nokontak //
                //if (((int.Parse(usrtp) == (int)UserType.Branch || int.Parse(usrtp) == (int)UserType.HO))
                //    && int.Parse((modeldata.Status ?? "0")) >= 10 && int.Parse((modeldata.Status ?? "0")) < 40)
                //{
                //    ViewBag.editkontrak = "allow";
                //    ViewBag.oprvalue = "view";
                //    ViewBag.hidesave = "show";
                //}

                string viewbro = "/Views/HTL/_uiRoyaUpdNw.cshtml";
                if (int.Parse(usrtp) == (int)UserType.Branch)
                {
                    ViewBag.user = "cabang";
                }

                if (int.Parse(usrtp) == (int)UserType.Notaris)
                {
                    ViewBag.user = "notaris";
                    // viewbro = "/Views/HTL/_uiHTLUpd.cshtml"; //_uiHTLUpdNw.cshtml
                }

                if (int.Parse(usrtp) == (int)UserType.FDCM)
                {
                    ViewBag.user = "admin";
                }

                if (int.Parse(usrtp) == (int)UserType.FDCM && (modeldata.StatusHT == "47" || modeldata.StatusHT == "54"))
                {
                    ViewBag.allowht = "allow";
                }


                HTL.HeaderInfo = modeldata;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewbro, HTL.HeaderInfo),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }




        [HttpPost]
        public async Task<ActionResult> GroupPos(string NoAPP, string PosBerkas, string GroupPos, string ketPos, int nominal, string user)
        {
            var Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;

            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

                if (!string.IsNullOrEmpty(Account.AccountLogin.RouteName))
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                await HTLddl.dbSaveIsueGrp(NoAPP, PosBerkas, GroupPos, ketPos, nominal, user);

                // Kembalikan respon sukses
                return Json(new
                {
                    success = true,
                    message = "Data berhasil disimpan"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Jika ada kesalahan, tangani di sini
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<ActionResult> clnOpenAddHTLIPT(string paramkey, string idrel, string oprfun, string gdid, string idg, string scn)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HashNetFramework.HasKeyProtect.Decryption(modFilter.idcaption);

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);


                string Opr4view = "add";
                string keyup = paramkey;
                string keyup1 = "";
                string keyup2 = "";
                string NoAPPL = HashNetFramework.HasKeyProtect.Decryption(idrel);


                cHTLIPTData modeldata = new cHTLIPTData();


                if (gdid == "gdp")
                {
                    modeldata.JenisData = "99";
                }
                else if (gdid == "gdtnh")
                {
                    DataRow dr = HTL.HeaderInfo.DataTNH.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                    if (dr != null)
                    {

                        Opr4view = "edit";
                        if (oprfun == "x4vw")
                        {
                            Opr4view = "view";
                        }

                        modeldata.keylookupdataHTXIpt = dr["keylookupdata"].ToString();
                        modeldata.JenisData = dr["Jenisdata"].ToString();
                        modeldata.JenisSertifikat = dr["JenisHak"].ToString();
                        modeldata.NoSertifikat = dr["NoSertifikat"].ToString();
                        modeldata.JenisSertifikatDesc = dr["JenisHakDesc"].ToString();
                        modeldata.NomorNIB = dr["NoNIB"].ToString();
                        modeldata.NoBlanko = dr["NoBlanko"].ToString();
                        modeldata.NoSuratUkur = dr["NoSuratUkur"].ToString();
                        modeldata.TglSuratUkur = DateTime.Parse(dr["TglSuratUkur"].ToString()).ToString("dd-MMMM-yyyy");
                        modeldata.LuasTanah = dr["LuasTanah"].ToString().Replace(",", "").Replace(".00", "");
                        modeldata.LokasiTanahDiProvinsi = dr["ProvinsiTanah"].ToString();
                        modeldata.LokasiTanahDiKota = dr["KotaTanah"].ToString();
                        modeldata.LokasiTanahDiKecamatan = dr["KecamatanTanah"].ToString();
                        modeldata.LokasiTanahDiDesaKelurahan = dr["DesaKelurahanTanah"].ToString();
                        modeldata.NoApplIpt = dr["NoAPPL"].ToString();

                        modeldata.TglSKMHT = dr["TglSKMHT"].ToString();
                        modeldata.NoSKMHT = dr["NoSKMHT"].ToString();

                        modeldata.KodeAkta = dr["KodeAkta"].ToString();
                        modeldata.NoHT = dr["NoHT"].ToString();
                        modeldata.TglSertiCekCN = dr["TglHasilCek"].ToString();
                        modeldata.TglSPA = dr["TglSPA"].ToString();
                        modeldata.KodeSHT = dr["KodeSHT"].ToString();
                        modeldata.NoSHT = dr["NoSHT"].ToString();
                        modeldata.NoBerkasSHT = dr["NoBerkasSHT"].ToString();
                        modeldata.LinkBerkasSHT = dr["LinkBerkasSHT"].ToString();

                    }
                    else
                    {
                        modeldata.NoApplIpt = NoAPPL;
                        modeldata.JenisData = oprfun == "x0s" ? "1" : "NONE";
                    }

                    Common.ddlhak = await Commonddl.dbdbGetDdlEnumsListByEncrypt("HAK", caption, UserID, GroupName, "99");
                    ViewData["SelectHak"] = new MultiSelectList(Common.ddlhak, "Value", "Text");

                    Common.ddlLokprov = await Commonddl.dbdbGetDdlEnumsListByEncrypt("LOKTANAHPROV", caption, UserID, GroupName, "99");
                    ViewData["SelectLokasiTanahProv"] = new MultiSelectList(Common.ddlLokprov, "Value", "Text");

                    Common.ddlLokkota = await Commonddl.dbdbGetDdlEnumsListByEncrypt("LOKTANAHKOTA", caption, UserID, GroupName, modeldata.LokasiTanahDiProvinsi);
                    ViewData["SelectLokasiTanah"] = new MultiSelectList(Common.ddlLokkota, "Value", "Text");

                }

                else if (gdid == "gdtnhspa")
                {
                    DataRow dr = HTL.HeaderInfo.DataTNHSPA.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                    if (dr != null)
                    {

                        Opr4view = "edit";
                        if (oprfun == "x4vw")
                        {
                            Opr4view = "view";
                        }

                        modeldata.keylookupdataHTXIpt = dr["keylookupdata"].ToString();
                        modeldata.JenisData = dr["Jenisdata"].ToString();
                        modeldata.JenisSertifikat = dr["JenisHak"].ToString();
                        modeldata.NoSertifikat = dr["NoSertifikat"].ToString();
                        modeldata.JenisSertifikatDesc = dr["JenisHakDesc"].ToString();
                        modeldata.NomorNIB = dr["NoNIB"].ToString();
                        modeldata.NoBlanko = dr["NoBlanko"].ToString();
                        modeldata.NoSuratUkur = dr["NoSuratUkur"].ToString();
                        modeldata.TglSuratUkur = DateTime.Parse(dr["TglSuratUkur"].ToString()).ToString("dd-MMMM-yyyy");
                        modeldata.LuasTanah = dr["LuasTanah"].ToString().Replace(",", "").Replace(".00", "");
                        modeldata.LokasiTanahDiProvinsi = dr["ProvinsiTanah"].ToString();
                        modeldata.LokasiTanahDiKota = dr["KotaTanah"].ToString();
                        modeldata.LokasiTanahDiKecamatan = dr["KecamatanTanah"].ToString();
                        modeldata.LokasiTanahDiDesaKelurahan = dr["DesaKelurahanTanah"].ToString();
                        modeldata.NoApplIpt = dr["NoAPPL"].ToString();

                        modeldata.KodeAkta = dr["KodeAkta"].ToString();
                        modeldata.NoHT = dr["NoHT"].ToString();
                        modeldata.TglSertiCekCN = dr["TglHasilCek"].ToString();
                        modeldata.TglSPA = dr["TglSPA"].ToString();

                        modeldata.TglSKMHT = dr["TglSKMHT"].ToString();
                        modeldata.NoSKMHT = dr["NoSKMHT"].ToString();

                        modeldata.KodeSHT = dr["KodeSHT"].ToString();
                        modeldata.NoSHT = dr["NoSHT"].ToString();
                        modeldata.NoBerkasSHT = dr["NoBerkasSHT"].ToString();
                        modeldata.LinkBerkasSHT = dr["LinkBerkasSHT"].ToString();

                    }
                    else
                    {
                        modeldata.NoApplIpt = NoAPPL;
                        modeldata.JenisData = oprfun == "x0s" ? "1" : "NONE";
                    }

                    Common.ddlhak = await Commonddl.dbdbGetDdlEnumsListByEncrypt("HAK", caption, UserID, GroupName, "99");
                    ViewData["SelectHak"] = new MultiSelectList(Common.ddlhak, "Value", "Text");

                    Common.ddlLokprov = await Commonddl.dbdbGetDdlEnumsListByEncrypt("LOKTANAHPROV", caption, UserID, GroupName, "99");
                    ViewData["SelectLokasiTanahProv"] = new MultiSelectList(Common.ddlLokprov, "Value", "Text");

                    Common.ddlLokkota = await Commonddl.dbdbGetDdlEnumsListByEncrypt("LOKTANAHKOTA", caption, UserID, GroupName, modeldata.LokasiTanahDiProvinsi);
                    ViewData["SelectLokasiTanah"] = new MultiSelectList(Common.ddlLokkota, "Value", "Text");

                }


                if (gdid == "gdsrt")
                {
                    DataRow dr = HTL.HeaderInfo.DataSRT.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                    if (dr != null)
                    {

                        Opr4view = "edit";
                        if (oprfun == "x4vw")
                        {
                            Opr4view = "view";
                        }

                        modeldata.keylookupdataHTXIpt = dr["keylookupdata"].ToString();
                        modeldata.NoApplIpt = dr["NoAPPL"].ToString();
                        modeldata.NoSertifikat = dr["NoSertifikat"].ToString();
                        modeldata.REFNIK = dr["REFNIK"].ToString();
                        modeldata.NIK = dr["NIK"].ToString();
                        modeldata.JenisData = dr["JenisData"].ToString();
                        modeldata.Nama = dr["Nama"].ToString();
                        modeldata.JKelamin = dr["JKelamin"].ToString();
                        modeldata.Tptlahir = dr["Tptlahir"].ToString();

                        modeldata.Tgllahir = dr["Tgllahir"].ToString();
                        if (modeldata.Tgllahir != "")
                        {
                            modeldata.Tgllahir = DateTime.Parse(modeldata.Tgllahir).ToString("dd-MMMM-yyyy");
                        }

                        modeldata.Warga = dr["Warga"].ToString();
                        modeldata.StatusPernikahan = dr["StatusNikah"].ToString();
                        modeldata.Pekerjaan = dr["Pekerjaan"].ToString();
                        modeldata.Alamat = dr["Alamat"].ToString();
                        modeldata.RT = dr["RT"].ToString();
                        modeldata.RW = dr["RW"].ToString();

                        modeldata.Provinsi = dr["Provinsi"].ToString();
                        modeldata.Kota = dr["Kota"].ToString();
                        modeldata.Kecamatan = dr["Kecamatan"].ToString();
                        modeldata.DesaKelurahan = dr["DesaKelurahan"].ToString();
                    }
                    else
                    {
                        modeldata.NoApplIpt = NoAPPL;
                        modeldata.JenisData = oprfun == "x0s" ? "2" : "NONE";
                    }

                    Common.ddlNIB = await Commonddl.dbdbGetDdlEnumsListByEncrypt("NIB", caption, UserID, GroupName, modeldata.NoApplIpt);
                    ViewData["SelectNIB"] = new MultiSelectList(Common.ddlNIB, "Value", "Text");

                    Common.ddlJenKel = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENKEL", caption, UserID, GroupName, "99");
                    ViewData["SelectJENKEL"] = new MultiSelectList(Common.ddlJenKel, "Value", "Text");

                    Common.ddlStatKW = await Commonddl.dbdbGetDdlEnumsListByEncrypt("WARGA", caption, UserID, GroupName, "99");
                    ViewData["SelectWarga"] = new MultiSelectList(Common.ddlStatKW, "Value", "Text");

                    Common.ddlStatNKH = await Commonddl.dbdbGetDdlEnumsListByEncrypt("NIKAH", caption, UserID, GroupName, "99");
                    ViewData["SelectStatusNikah"] = new MultiSelectList(Common.ddlStatNKH, "Value", "Text");

                }

                if (gdid == "gdpsg")
                {
                    DataRow dr = HTL.HeaderInfo.DataPSG.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                    if (dr != null)
                    {

                        Opr4view = "edit";
                        if (oprfun == "x4vw")
                        {
                            Opr4view = "view";
                        }

                        modeldata.keylookupdataHTXIpt = dr["keylookupdata"].ToString();
                        modeldata.NoApplIpt = dr["NoAPPL"].ToString();
                        modeldata.NoSertifikat = dr["NoSertifikat"].ToString();
                        modeldata.JenisData = dr["JenisData"].ToString();

                        modeldata.REFNIK = dr["REFNIK"].ToString();
                        modeldata.NIK = dr["NIK"].ToString();
                        modeldata.Nama = dr["Nama"].ToString();
                        modeldata.JKelamin = dr["JKelamin"].ToString();
                        modeldata.Tptlahir = dr["Tptlahir"].ToString();

                        modeldata.Tgllahir = dr["Tgllahir"].ToString();
                        if (modeldata.Tgllahir != "")
                        {
                            modeldata.Tgllahir = DateTime.Parse(modeldata.Tgllahir).ToString("dd-MMMM-yyyy");
                        }

                        modeldata.Warga = dr["Warga"].ToString();
                        modeldata.StatusPernikahan = dr["StatusNikah"].ToString();

                        modeldata.Pekerjaan = dr["Pekerjaan"].ToString();
                        modeldata.Alamat = dr["Alamat"].ToString();
                        modeldata.RT = dr["RT"].ToString();
                        modeldata.RW = dr["RW"].ToString();
                        modeldata.Provinsi = dr["Provinsi"].ToString();
                        modeldata.Kota = dr["Kota"].ToString();
                        modeldata.Kecamatan = dr["Kecamatan"].ToString();
                        modeldata.DesaKelurahan = dr["DesaKelurahan"].ToString();

                    }
                    else
                    {
                        modeldata.NoApplIpt = NoAPPL;
                        modeldata.JenisData = oprfun == "x0s" ? "0" : "NONE";
                    }

                    Common.ddlJenKel = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENKEL", caption, UserID, GroupName, "99");
                    ViewData["SelectJENKEL"] = new MultiSelectList(Common.ddlJenKel, "Value", "Text");

                    Common.ddlStatKW = await Commonddl.dbdbGetDdlEnumsListByEncrypt("WARGA", caption, UserID, GroupName, "99");
                    ViewData["SelectWarga"] = new MultiSelectList(Common.ddlStatKW, "Value", "Text");

                    Common.ddlStatNKH = await Commonddl.dbdbGetDdlEnumsListByEncrypt("NIKAH", caption, UserID, GroupName, "99");
                    ViewData["SelectStatusNikah"] = new MultiSelectList(Common.ddlStatNKH, "Value", "Text");
                }

                if (gdid == "gdsrtpsg")
                {
                    DataRow dr = HTL.HeaderInfo.DataSRTPSG.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                    if (dr != null)
                    {

                        Opr4view = "edit";
                        if (oprfun == "x4vw")
                        {
                            Opr4view = "view";
                        }

                        modeldata.keylookupdataHTXIpt = dr["keylookupdata"].ToString();
                        modeldata.NoApplIpt = dr["NoAPPL"].ToString();
                        modeldata.NoSertifikat = dr["NoSertifikat"].ToString();
                        modeldata.JenisData = dr["JenisData"].ToString();

                        modeldata.REFNIK = dr["REFNIK"].ToString();
                        modeldata.NIK = dr["NIK"].ToString();
                        modeldata.Nama = dr["Nama"].ToString();
                        modeldata.JKelamin = dr["JKelamin"].ToString();
                        modeldata.Tptlahir = dr["Tptlahir"].ToString();

                        modeldata.Tgllahir = dr["Tgllahir"].ToString();
                        if (modeldata.Tgllahir != "")
                        {
                            modeldata.Tgllahir = DateTime.Parse(modeldata.Tgllahir).ToString("dd-MMMM-yyyy");
                        }

                        modeldata.Warga = dr["Warga"].ToString();
                        modeldata.StatusPernikahan = dr["StatusNikah"].ToString();
                        modeldata.Pekerjaan = dr["Pekerjaan"].ToString();
                        modeldata.Alamat = dr["Alamat"].ToString();
                        modeldata.RT = dr["RT"].ToString();
                        modeldata.RW = dr["RW"].ToString();
                        modeldata.Provinsi = dr["Provinsi"].ToString();
                        modeldata.Kota = dr["Kota"].ToString();
                        modeldata.Kecamatan = dr["Kecamatan"].ToString();
                        modeldata.DesaKelurahan = dr["DesaKelurahan"].ToString();

                    }
                    else
                    {
                        modeldata.NoApplIpt = NoAPPL;
                        modeldata.JenisData = oprfun == "x0s" ? "3" : "NONE";
                    }

                    Common.ddlNIK = await Commonddl.dbdbGetDdlEnumsListByEncrypt("NIK", caption, UserID, GroupName, modeldata.NoApplIpt);
                    ViewData["SelectNIK"] = new MultiSelectList(Common.ddlNIK, "Value", "Text");

                    Common.ddlJenKel = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENKEL", caption, UserID, GroupName, "99");
                    ViewData["SelectJENKEL"] = new MultiSelectList(Common.ddlJenKel, "Value", "Text");

                    Common.ddlStatKW = await Commonddl.dbdbGetDdlEnumsListByEncrypt("WARGA", caption, UserID, GroupName, "99");
                    ViewData["SelectWarga"] = new MultiSelectList(Common.ddlStatKW, "Value", "Text");

                    Common.ddlStatNKH = await Commonddl.dbdbGetDdlEnumsListByEncrypt("NIKAH", caption, UserID, GroupName, "99");
                    ViewData["SelectStatusNikah"] = new MultiSelectList(Common.ddlStatNKH, "Value", "Text");
                }


                ViewBag.showtabnotaris = "";
                string usrtp = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                if (int.Parse(usrtp) == (int)UserType.Notaris || int.Parse(usrtp) == (int)UserType.FDCM)
                {
                    ViewBag.showtabnotaris = "allow";
                }

                ViewBag.hidesave = "";
                if (int.Parse(usrtp) == (int)UserType.FDCM && Opr4view == "add")
                {
                    ViewBag.hidesave = "hidden";
                }

                ViewBag.btncaption = "Simpan";
                if ((int.Parse(usrtp) != (int)UserType.HO && int.Parse(usrtp) != (int)UserType.Branch) || (modeldata.Status == "21")) //pending
                {
                    ViewBag.btncaption = "Submit";
                }

                //ViewBag.OprMenu = Opr4view == "add" ? "PENAMBAHAN DATA" : Opr4view == "edit" ? "PERUBAHAN DATA (" + modeldata.NoAppl + ")" : "";
                //ViewBag.OprMenu = Opr4view == "view" ? "NO APLIKASI " + modeldata.NoAppl : ViewBag.OprMenu;
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;

                ViewBag.scan = scn;
                ViewBag.keyup = keyup;
                ViewBag.keyup1 = keyup1;
                ViewBag.keyup2 = keyup2;
                ViewBag.gdv = gdid;
                ViewBag.idgdv = idg;

                ////pengecekan on progress editable u nokontak //
                //if (((int.Parse(usrtp) == (int)UserType.Branch || int.Parse(usrtp) == (int)UserType.HO))
                //    && int.Parse((modeldata.Status ?? "0")) >= 10 && int.Parse((modeldata.Status ?? "0")) < 40)
                //{
                //    ViewBag.editkontrak = "allow";
                //    ViewBag.oprvalue = "view";
                //    ViewBag.hidesave = "show";
                //}

                string viewbro = "/Views/HTL/_uiHTLUpdMdf.cshtml";
                if (gdid == "gdtnhspa")
                {
                    viewbro = "/Views/HTL/_uiHTLUpdMdfspa.cshtml";
                    modeldata.JenisData = "1";
                }

                if (int.Parse(usrtp) == (int)UserType.Branch)
                {
                    ViewBag.user = "cabang";
                }

                if (int.Parse(usrtp) == (int)UserType.Notaris)
                {
                    ViewBag.user = "notaris";
                    //viewbro = "/Views/HTL/_uiHTLUpdMdf.cshtml";
                }

                //HTL.HeaderInfo = modeldata;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    msg = NoAPPL == "" ? "Silahkan disikan data aplikasi dan debitur terlebih dahulu, kemudian tekan tombol 'Simpan'" : "",
                    view = NoAPPL == "" ? "" : CustomEngineView.RenderRazorViewToString(ControllerContext, viewbro, modeldata),
                });


            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnUpdHTL(cHTL model, string ctex, string diadu, HttpPostedFileBase potofile, HttpPostedFileBase ttdformstr, HttpPostedFileBase ttdskstr, HttpPostedFileBase ttdabsstr)
        {

            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {

                HTL = TempData[tempTransksi] as vmHTL;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;


                //get value from aply filter //
                string keyword = modFilter.keywordfilter;
                string keylookupdata = model.keylookupdataHTX;

                //set field to output//
                string KeySearch = modFilter.RequestNo ?? "";
                string todate = modFilter.todate ?? "";
                string fromdate = modFilter.fromdate ?? "";
                string SelectArea = modFilter.SelectArea ?? "";
                string SelectBranch = modFilter.SelectBranch ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectNotaris = modFilter.SelectNotaris ?? "";
                string Status = modFilter.SelectContractStatus ?? "-1";

                //set default for paging //
                int PageNumber = 1;
                double TotalRecord = modFilter.TotalRecord;
                double TotalPage = 0;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double pagenumberclient = modFilter.pagenumberclient;
                double totalRecordclient = 0;
                double totalPageclient = 0;
                bool isModeFilter = modFilter.isModeFilter;

                //set filter//
                modFilter.keywordfilter = keyword;
                modFilter.keylookupfilter = keylookupdata;

                //set filter//
                modFilter.RequestNo = KeySearch;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectArea = SelectArea;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.todate = todate;
                modFilter.fromdate = fromdate;
                modFilter.SelectContractStatus = Status;
                modFilter.ModuleName = caption;
                modFilter.isModeFilter = true;
                //set filter//

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                // string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

                DataRow dr;
                int ID = 0;
                int statusOld = 0;
                int statusNew = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;

                }

                statusOld = int.Parse(HTL.HeaderInfo.Status ?? (model.Status ?? "0"));
                model.TglOrder = model.TglOrder ?? HTL.HeaderInfo.TglOrder;
                model.NoSertifikat = model.NoSertifikat ?? HTL.HeaderInfo.NoSertifikat;
                model.NoAppl = model.NoAppl ?? HTL.HeaderInfo.NoAppl;
                model.JenisSertifikat = model.JenisSertifikat ?? HTL.HeaderInfo.JenisSertifikat;
                model.NomorNIB = model.NomorNIB ?? HTL.HeaderInfo.NomorNIB;
                model.NoSuratUkur = model.NoSuratUkur ?? HTL.HeaderInfo.NoSuratUkur;
                model.LuasTanah = (model.LuasTanah ?? HTL.HeaderInfo.LuasTanah).Replace(",", "");
                model.LokasiTanahDiProvinsi = model.LokasiTanahDiProvinsi ?? HTL.HeaderInfo.LokasiTanahDiProvinsi;
                model.LokasiTanahDiKota = model.LokasiTanahDiKota ?? HTL.HeaderInfo.LokasiTanahDiKota;
                model.LokasiTanahDiKecamatan = model.LokasiTanahDiKecamatan ?? HTL.HeaderInfo.LokasiTanahDiKecamatan;
                model.LokasiTanahDiDesaKelurahan = model.LokasiTanahDiDesaKelurahan ?? HTL.HeaderInfo.LokasiTanahDiDesaKelurahan;
                model.NamaDebitur = model.NamaDebitur ?? HTL.HeaderInfo.NamaDebitur;

                model.WargaDebitur = model.WargaDebitur ?? HTL.HeaderInfo.WargaDebitur;
                model.JKelaminDebitur = model.JKelaminDebitur ?? HTL.HeaderInfo.JKelaminDebitur;
                model.TptLahirDebitur = model.TptLahirDebitur ?? HTL.HeaderInfo.TptLahirDebitur;
                model.TgllahirDebitur = model.TgllahirDebitur ?? HTL.HeaderInfo.TgllahirDebitur;

                model.AlamatDebitur = model.AlamatDebitur ?? HTL.HeaderInfo.AlamatDebitur;
                model.ProvinsiDebitur = model.ProvinsiDebitur ?? HTL.HeaderInfo.ProvinsiDebitur;
                model.KotaDebitur = model.KotaDebitur ?? HTL.HeaderInfo.KotaDebitur;
                model.KecamatanDebitur = model.KecamatanDebitur ?? HTL.HeaderInfo.KecamatanDebitur;
                model.DesaKelurahanDebitur = model.DesaKelurahanDebitur ?? HTL.HeaderInfo.DesaKelurahanDebitur;
                model.PekerjaanDebitur = model.PekerjaanDebitur ?? HTL.HeaderInfo.PekerjaanDebitur;
                model.NIKDebitur = model.NIKDebitur ?? HTL.HeaderInfo.NIKDebitur;

                model.NamaPemilikSertifikat = model.NamaPemilikSertifikat ?? HTL.HeaderInfo.NamaPemilikSertifikat;
                model.JKelaminPemilikSertifikat = model.JKelaminPemilikSertifikat ?? HTL.HeaderInfo.JKelaminPemilikSertifikat;
                model.TptlahirPemilikSertifikat = model.TptlahirPemilikSertifikat ?? HTL.HeaderInfo.TptlahirPemilikSertifikat;
                model.TgllahirPemilikSertifikat = model.TgllahirPemilikSertifikat ?? HTL.HeaderInfo.TgllahirPemilikSertifikat;
                model.WargaPemilikSertifikat = model.WargaPemilikSertifikat ?? HTL.HeaderInfo.WargaPemilikSertifikat;
                model.AlamatPemilikSertifikat = model.AlamatPemilikSertifikat ?? HTL.HeaderInfo.AlamatPemilikSertifikat;
                model.ProvinsiPemilikSertifikat = model.ProvinsiPemilikSertifikat ?? HTL.HeaderInfo.ProvinsiPemilikSertifikat;
                model.KotaPemilikSertifikat = model.KotaPemilikSertifikat ?? HTL.HeaderInfo.KotaPemilikSertifikat;
                model.KecamatanPemilikSertifikat = model.KecamatanPemilikSertifikat ?? HTL.HeaderInfo.KecamatanPemilikSertifikat;
                model.DesaKelurahanPemilikSertifikat = model.DesaKelurahanPemilikSertifikat ?? HTL.HeaderInfo.DesaKelurahanPemilikSertifikat;
                model.PekerjaanPemilikSertifikat = model.PekerjaanPemilikSertifikat ?? HTL.HeaderInfo.PekerjaanPemilikSertifikat;
                model.NIKPemilikSertifikat = model.NIKPemilikSertifikat ?? HTL.HeaderInfo.NIKPemilikSertifikat;

                model.OrderKeNotaris = (model.OrderKeNotaris ?? HTL.HeaderInfo.OrderKeNotaris);
                model.NilaiHT = (model.NilaiHT ?? HTL.HeaderInfo.NilaiHT).Replace(",", "");
                model.NilaiPinjamanDiterima = (model.NilaiPinjamanDiterima ?? HTL.HeaderInfo.NilaiPinjamanDiterima).Replace(",", "");


                model.KodeAkta = (model.KodeAkta ?? HTL.HeaderInfo.KodeAkta);
                model.NoHT = (model.NoHT ?? HTL.HeaderInfo.NoHT);
                model.TglSertifikatCEK = (model.TglSertifikatCEK ?? HTL.HeaderInfo.TglSertifikatCEK);
                model.TglSuratUkur = (model.TglSuratUkur ?? HTL.HeaderInfo.TglSuratUkur);

                model.JasaPengecekan = model.JasaPengecekan != HTL.HeaderInfo.JasaPengecekan ? model.JasaPengecekan : HTL.HeaderInfo.JasaPengecekan;
                model.JasaValidasi = model.JasaValidasi != HTL.HeaderInfo.JasaValidasi ? model.JasaValidasi : HTL.HeaderInfo.JasaValidasi;
                model.SKMHT = model.SKMHT != HTL.HeaderInfo.SKMHT ? model.SKMHT : HTL.HeaderInfo.SKMHT;
                model.APHT_SHT = model.APHT_SHT != HTL.HeaderInfo.APHT_SHT ? model.APHT_SHT : HTL.HeaderInfo.APHT_SHT;
                model.ROYA = model.ROYA != HTL.HeaderInfo.ROYA ? model.ROYA : HTL.HeaderInfo.ROYA;
                model.PENCORETAN_PTSL = model.PENCORETAN_PTSL != HTL.HeaderInfo.PENCORETAN_PTSL ? model.PENCORETAN_PTSL : HTL.HeaderInfo.PENCORETAN_PTSL;
                model.KUASA_MENGAMBIL = model.KUASA_MENGAMBIL != HTL.HeaderInfo.KUASA_MENGAMBIL ? model.KUASA_MENGAMBIL : HTL.HeaderInfo.KUASA_MENGAMBIL;
                model.PNBP = model.PNBP != HTL.HeaderInfo.PNBP ? model.PNBP : HTL.HeaderInfo.PNBP;
                model.ADM_HT = model.ADM_HT != HTL.HeaderInfo.ADM_HT ? model.ADM_HT : HTL.HeaderInfo.ADM_HT;

                model.NoPerjanjian = model.NoPerjanjian ?? HTL.HeaderInfo.NoPerjanjian;
                model.TglPerjanjian = model.TglPerjanjian ?? HTL.HeaderInfo.TglPerjanjian;
                model.Keterangan = model.Keterangan ?? "";
                model.Status = model.Status ?? "-1";   // HTL.HeaderInfo.Status;
                model.Statusdesc = model.Statusdesc ?? HTL.HeaderInfo.Statusdesc;
                model.Statushakdesc = model.Statushakdesc ?? HTL.HeaderInfo.Statushakdesc;
                model.NamaCabang = model.NamaCabang ?? HTL.HeaderInfo.NamaCabang;


                string UserTypes = HashNetFramework.HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                if (int.Parse(UserTypes) == (int)UserType.Branch || int.Parse(UserTypes) == (int)UserType.HO)
                {
                    model.OrderKeNotaris = (model.OrderKeNotaris ?? "") == "" ? "" : (model.OrderKeNotaris);
                }
                else
                {
                    model.OrderKeNotaris = (model.OrderKeNotaris ?? "") == "" ? "" : HasKeyProtect.Decryption(model.OrderKeNotaris);
                }

                ctex = (ctex ?? "") == "" ? "" : HasKeyProtect.Decryption(ctex);
                diadu = (diadu ?? "") == "" ? "" : HasKeyProtect.Decryption(diadu);
                model.NoAppl = diadu;
                statusNew = int.Parse(model.Status ?? "0");

                string EnumMessage = "";
                int result = 0;
                string notaris = (model.OrderKeNotaris == "-9999" ? "" : model.OrderKeNotaris);


                //pengecekan on progress editable u nokontrak cabang //
                if (((int.Parse(UserTypes) == (int)UserType.Branch || int.Parse(UserTypes) == (int)UserType.HO))
                    && int.Parse((model.Status ?? "0")) >= 10 && int.Parse((model.Status ?? "0")) < 40)
                {
                    model.Keterangan = HTL.HeaderInfo.Keterangan;
                }

                if (int.Parse(UserTypes) == (int)UserType.Branch && (ctex.ToLower() ?? "") == "submit")
                {
                    if (statusOld == statusNew && statusOld > 6)
                    {
                        EnumMessage = "Pilih status penanganan";
                    }
                    if (model.Keterangan == "")
                    {
                        EnumMessage = "Silahkan isikan catatan";
                    }
                }


                if (statusOld != statusNew && model.Keterangan == "" && (ctex.ToLower() ?? "") == "submit") // && statusOld > 6 tidak sama dengan revisi
                {
                    EnumMessage = "Silahkan isikan catatan";
                }

                if ((statusNew == 20 || statusNew == 30) && notaris == "") //proses pengecekan harus ada notaris
                {
                    EnumMessage = "Pilih Nama notaris";
                }
                if (int.Parse(UserTypes) == (int)UserType.Notaris && notaris == "")
                {
                    EnumMessage = "Pilih Nama notaris";
                }

                if (statusNew == -1 && statusOld != 0 && statusOld != 6 && (ctex.ToLower() ?? "") == "submit") //status penangan kosong atau tidak sama input berkas/perbaikan cabang
                {
                    EnumMessage = "Pilih Status Penanganan";
                }

                if (statusOld != int.Parse(model.Status) && int.Parse(model.Status) != -1 && (ctex.ToLower() ?? "") == "simpan")
                {
                    EnumMessage = "Silahkan 'Submit Pengajuan' jika anda merubah 'Status Penanganan'";
                }

                if ((model.NamaCabang) == "")
                {
                    EnumMessage = "Isikan Nama Cabang";
                }

                if (model.NoSertifikat.Length < 5)
                {
                    EnumMessage = "No Sertifikat minimal 5 digit";
                }


                if (model.NomorNIB.Length < 5)
                {
                    EnumMessage = "No NIB minimal 5 digit dan tidak boleh melebihi 13 digit";
                }

                if (model.NomorNIB.Length > 13)
                {
                    EnumMessage = "No NIB tidak boleh melebihi 13 digit";
                }

                if ((model.JenisSertifikat ?? "") == "N/A" || (model.JenisSertifikat ?? "") == "" || (model.JenisSertifikat ?? "") == "0")
                {
                    EnumMessage = "Pilih jenis hak";
                }

                if (((model.JenisPengajuan ?? "") == "N/A" || (model.JenisPengajuan ?? "") == "0" || (model.JenisPengajuan ?? "") == "") && int.Parse(UserTypes) == (int)UserType.Branch)
                {
                    EnumMessage = "Pilih jenis pengajuan";
                }

                if ((model.TglSuratUkur ?? "") == "" && int.Parse(UserTypes) == (int)UserType.Branch)
                {
                    EnumMessage = "Tgl Surat Ukur";
                }
                if (int.Parse(UserTypes) == (int)UserType.Branch && statusNew == 0 && (model.Keterangan ?? "") == "" && (ctex.ToLower() ?? "") == "submit")
                {
                    EnumMessage = "Silahkan isikan catatan";
                }

                if (((model.NilaiPinjamanDiterima ?? "0") == "0" || (model.NilaiPinjamanDiterima ?? "") == "") && int.Parse(UserTypes) == (int)UserType.Branch)
                {
                    EnumMessage = "Silahkan Isikan Nilai Pinjaman diterima Nasabah";
                }

                if (((model.NilaiHT ?? "0") == "0" || (model.NilaiHT ?? "") == "") && int.Parse(UserTypes) == (int)UserType.Branch)
                {
                    EnumMessage = "Silahkan Isikan Nilai Hak Tanggungan";
                }

                //penagihan invoice
                if (int.Parse(UserTypes) == (int)UserType.Notaris && statusNew == 50 &&
                    (model.NoPerjanjian.Length < 10 || (model.TglPerjanjian ?? "") == "" || (model.KodeAkta ?? "") == "" || (model.NoHT ?? "") == ""
                    || (model.NilaiHT ?? "0") == "0") && (model.JasaPengecekan == false || model.SKMHT == false
                    || model.APHT_SHT == false || model.PENCORETAN_PTSL == false || model.ROYA == false))
                {
                    EnumMessage = "Isikan No Perjanjian,Tgl Perjanjian, Nilai HT, kode akta , nomor APHT dan jenis penanganan dengan benar";
                }


                if (EnumMessage == "")
                {
                    DataTable dtx = await HTLddl.dbdbGetDdlOrderGetCek("11", "", "", "", UserID, GroupName);
                    int resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                    if (resultsuct > 0)
                    {
                        string poko = (ctex ?? "");
                        model.Status = (model.Status == "-1" || statusOld == 6 || statusOld == 0) ? statusOld.ToString() : model.Status;
                        result = await HTLddl.dbupdateHTL(ID, model, ctex, caption, UserID, GroupName);
                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "di" + ctex) : EnumMessage;
                    }
                    else
                    {
                        result = resultsuct;
                        EnumMessage = "Batas waktu submit hanya diperbolehkan hari senin-jumat dari jam 08:00 s/d/ 17:00 (dihari kerja)";
                    }
                }


                //if (result == 1 && (ctex.ToLower() ?? "") == "simpan")
                //{
                //    HTL.DTAllTx.Columns["Keterangan"].ReadOnly = false;
                //    DataRow dtr = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                //    dtr["NoAppl"] = model.NoAppl;
                //    dtr["NamaCabang"] = model.NamaCabang;
                //    dtr["JenisPengajuan"] = model.JenisPengajuan;
                //    dtr["NoSertifikat"] = model.NoSertifikat;
                //    dtr["JenisSertifikat"] = model.JenisSertifikat;
                //    dtr["NomorNIB"] = model.NomorNIB;
                //    dtr["NoBlanko"] = model.NoBlanko;

                //    dtr["NoSuratUkur"] = model.NoSuratUkur;
                //    dtr["TglSuratUkur"] = (model.TglSuratUkur ?? "") != "" ? DateTime.Parse(model.TglSuratUkur) : DateTime.MinValue;
                //    dtr["LuasTanah"] = model.LuasTanah;
                //    dtr["LokasiTanahDiProvinsi"] = model.LokasiTanahDiProvinsi;
                //    dtr["LokasiTanahDiKota"] = model.LokasiTanahDiKota;
                //    dtr["LokasiTanahDiKecamatan"] = model.LokasiTanahDiKecamatan;
                //    dtr["LokasiTanahDiDesaKelurahan"] = model.LokasiTanahDiDesaKelurahan;

                //    dtr["Debitur"] = model.NamaDebitur;
                //    dtr["WargaDebitur"] = model.WargaDebitur;
                //    dtr["JKelaminDebitur"] = model.JKelaminDebitur;
                //    dtr["TptLahirDebitur"] = model.TptLahirDebitur;
                //    dtr["TgllahirDebitur"] = model.TgllahirDebitur;

                //    dtr["NIKDebitur"] = model.NIKDebitur;
                //    dtr["PekerjaanDebitur"] = model.PekerjaanDebitur;
                //    dtr["AlamatDebitur"] = model.AlamatDebitur;
                //    dtr["ProvinsiDebitur"] = model.ProvinsiDebitur;
                //    dtr["KotaDebitur"] = model.KotaDebitur;
                //    dtr["KecamatanDebitur"] = model.KecamatanDebitur;
                //    dtr["DesaKelurahanDebitur"] = model.DesaKelurahanDebitur;

                //    dtr["NamaPemilikSertifikat"] = model.NamaPemilikSertifikat;
                //    dtr["NIKPemilikSertifikat"] = model.NIKPemilikSertifikat;
                //    dtr["PekerjaanPemilikSertifikat"] = model.PekerjaanPemilikSertifikat;
                //    dtr["JKelaminPemilikSertifikat"] = model.JKelaminPemilikSertifikat;
                //    dtr["TptlahirPemilikSertifikat"] = model.TptlahirPemilikSertifikat;
                //    dtr["TgllahirPemilikSertifikat"] = model.TgllahirPemilikSertifikat;
                //    dtr["WargaPemilikSertifikat"] = model.WargaPemilikSertifikat;

                //    dtr["AlamatPemilikSertifikat"] = model.AlamatPemilikSertifikat;
                //    dtr["ProvinsiPemilikSertifikat"] = model.ProvinsiPemilikSertifikat;
                //    dtr["KotaPemilikSertifikat"] = model.KotaPemilikSertifikat;
                //    dtr["KecamatanPemilikSertifikat"] = model.KecamatanPemilikSertifikat;
                //    dtr["DesaKelurahanPemilikSertifikat"] = model.DesaKelurahanPemilikSertifikat;

                //    dtr["JasaPengecekan"] = model.JasaPengecekan;
                //    dtr["JasaValidasi"] = model.JasaValidasi;
                //    dtr["SKMHT"] = model.SKMHT;
                //    dtr["APHT_SHT"] = model.APHT_SHT;
                //    dtr["ROYA"] = model.ROYA;
                //    dtr["PENCORETAN_PTSL"] = model.PENCORETAN_PTSL;
                //    dtr["KUASA_MENGAMBIL"] = model.KUASA_MENGAMBIL;
                //    dtr["PNBP"] = model.PNBP;
                //    dtr["ADM_HT"] = model.ADM_HT;

                //    dtr["OrderKeNotaris"] = model.OrderKeNotaris;
                //    dtr["Status"] = model.Status;
                //    dtr["NoPerjanjian"] = model.NoPerjanjian;
                //    dtr["NilaiHT"] = model.NilaiHT;
                //    dtr["NoHT"] = model.NoHT;
                //    dtr["KodeAkta"] = model.KodeAkta;

                //    dtr["Keterangan"] = model.Keterangan;
                //    HTL.DTAllTx.AcceptChanges();
                //}
                //&& (ctex.ToLower() ?? "") == "submit"
                if (result == 1)
                {

                    // try show filter data//
                    List<String> recordPage = await HTLddl.dbGetHeaderTxListCount("", "", "", "", "", "", 0, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await HTLddl.dbGetHeaderTxList(null, "", "", "", "", "", "", 0, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    HTL.DTAllTx = dtlist[0];
                    HTL.DTHeaderTx = dtlist[1];
                    HTL.FilterTransaksi = modFilter;

                    TempData[tempTransksi] = HTL;
                    TempData[tempTransksifilter] = modFilter;
                    TempData["common"] = Common;

                    ctex = "submit";
                }

                ViewBag.ShowNotaris = "";
                if (int.Parse(UserTypes) == (int)UserType.FDCM)
                {
                    ViewBag.ShowNotaris = "allow";
                }
                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;


                //ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLLstGrid.cshtml", HTL),
                    msg = EnumMessage,
                    mode = ctex.ToLower(),
                    resulted = result
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnUpdHTLNW(cHTL model, string ctex, string diadu, HttpPostedFileBase potofile, HttpPostedFileBase ttdformstr, HttpPostedFileBase ttdskstr, HttpPostedFileBase ttdabsstr)
        {

            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {


                // 'ModelState.AddModelError(nameof(ForgotPasswordMV.Email), 

                HTL = TempData[tempTransksi] as vmHTL;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;


                //get value from aply filter //
                string keyword = modFilter.keywordfilter;
                string keylookupdata = model.keylookupdataHTX;

                //set field to output//
                string KeySearch = modFilter.RequestNo ?? "";
                string todate = modFilter.todate ?? "";
                string fromdate = modFilter.fromdate ?? "";
                string SelectArea = modFilter.SelectArea ?? "";
                string SelectBranch = modFilter.SelectBranch ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectNotaris = modFilter.SelectNotaris ?? "";
                string Status = modFilter.SelectContractStatus ?? "-1";

                //set default for paging //
                int PageNumber = 1;
                double TotalRecord = modFilter.TotalRecord;
                double TotalPage = 0;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double pagenumberclient = modFilter.pagenumberclient;
                double totalRecordclient = 0;
                double totalPageclient = 0;
                bool isModeFilter = modFilter.isModeFilter;

                //set filter//
                modFilter.keywordfilter = keyword;
                modFilter.keylookupfilter = keylookupdata;

                //set filter//
                modFilter.RequestNo = KeySearch;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectArea = SelectArea;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.todate = todate;
                modFilter.fromdate = fromdate;
                modFilter.SelectContractStatus = Status;
                modFilter.ModuleName = caption;
                modFilter.isModeFilter = true;
                //set filter//

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                // string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

                DataRow dr;
                int ID = 0;
                int statusOld = 0;
                int statusNew = 0;

                if ((keylookupdata ?? "") != "")
                {
                    string checkID = HasKeyProtect.Decryption(keylookupdata);
                    if (checkID.All(char.IsDigit))
                    {
                        ID = int.Parse(checkID);
                    }
                    else
                    {
                        dr = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                        ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                    }
                }

                model.Status = model.Status ?? "0";
                model.Status = model.Status.All(char.IsNumber) ? model.Status : HasKeyProtect.Decryption(model.Status);

                statusOld = int.Parse(HTL.HeaderInfo.Status ?? (model.Status ?? "0"));
                model.TglOrder = model.TglOrder ?? HTL.HeaderInfo.TglOrder;
                model.NoSertifikat = model.NoSertifikat ?? HTL.HeaderInfo.NoSertifikat;
                model.NoAppl = model.NoAppl ?? HTL.HeaderInfo.NoAppl;

                model.NIKDebitur = model.NIKDebitur ?? HTL.HeaderInfo.NIKDebitur;
                model.NamaDebitur = model.NamaDebitur ?? HTL.HeaderInfo.NamaDebitur;
                model.WargaDebitur = model.WargaDebitur ?? HTL.HeaderInfo.WargaDebitur;
                model.JKelaminDebitur = model.JKelaminDebitur ?? HTL.HeaderInfo.JKelaminDebitur;
                model.TptLahirDebitur = model.TptLahirDebitur ?? HTL.HeaderInfo.TptLahirDebitur;
                model.TgllahirDebitur = model.TgllahirDebitur ?? HTL.HeaderInfo.TgllahirDebitur;
                model.AlamatDebitur = model.AlamatDebitur ?? HTL.HeaderInfo.AlamatDebitur;
                model.ProvinsiDebitur = model.ProvinsiDebitur ?? HTL.HeaderInfo.ProvinsiDebitur;
                model.KotaDebitur = model.KotaDebitur ?? HTL.HeaderInfo.KotaDebitur;
                model.KecamatanDebitur = model.KecamatanDebitur ?? HTL.HeaderInfo.KecamatanDebitur;
                model.DesaKelurahanDebitur = model.DesaKelurahanDebitur ?? HTL.HeaderInfo.DesaKelurahanDebitur;
                model.PekerjaanDebitur = model.PekerjaanDebitur ?? HTL.HeaderInfo.PekerjaanDebitur;
                model.ModifiedDate = model.ModifiedDate ?? HTL.HeaderInfo.ModifiedDate;

                string UserTypes = HashNetFramework.HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                if (int.Parse(UserTypes) == (int)UserType.Notaris)
                {
                    model.OrderKeNotaris = (model.OrderKeNotaris ?? HTL.HeaderInfo.OrderKeNotaris);
                }

                model.NilaiHT = (model.NilaiHT ?? HTL.HeaderInfo.NilaiHT ?? "0").Replace(",", "");
                model.NilaiPinjamanDiterima = (model.NilaiPinjamanDiterima ?? HTL.HeaderInfo.NilaiPinjamanDiterima ?? "0").Replace(",", "");

                if (int.Parse(UserTypes) == (int)UserType.Notaris)
                {
                    model.KodeAkta = (model.KodeAkta ?? HTL.HeaderInfo.KodeAkta);
                    model.NoHT = (model.NoHT ?? HTL.HeaderInfo.NoHT);
                    model.JasaPengecekan = model.JasaPengecekan != HTL.HeaderInfo.JasaPengecekan ? model.JasaPengecekan : HTL.HeaderInfo.JasaPengecekan;
                    model.JasaValidasi = model.JasaValidasi != HTL.HeaderInfo.JasaValidasi ? model.JasaValidasi : HTL.HeaderInfo.JasaValidasi;
                    model.SKMHT = model.SKMHT != HTL.HeaderInfo.SKMHT ? model.SKMHT : HTL.HeaderInfo.SKMHT;
                    model.APHT_SHT = model.APHT_SHT != HTL.HeaderInfo.APHT_SHT ? model.APHT_SHT : HTL.HeaderInfo.APHT_SHT;
                    model.ROYA = model.ROYA != HTL.HeaderInfo.ROYA ? model.ROYA : HTL.HeaderInfo.ROYA;
                    model.PENCORETAN_PTSL = model.PENCORETAN_PTSL != HTL.HeaderInfo.PENCORETAN_PTSL ? model.PENCORETAN_PTSL : HTL.HeaderInfo.PENCORETAN_PTSL;
                    model.KUASA_MENGAMBIL = model.KUASA_MENGAMBIL != HTL.HeaderInfo.KUASA_MENGAMBIL ? model.KUASA_MENGAMBIL : HTL.HeaderInfo.KUASA_MENGAMBIL;
                    model.PNBP = model.PNBP != HTL.HeaderInfo.PNBP ? model.PNBP : HTL.HeaderInfo.PNBP;
                    model.ADM_HT = model.ADM_HT != HTL.HeaderInfo.ADM_HT ? model.ADM_HT : HTL.HeaderInfo.ADM_HT;
                    model.NoPerjanjian = model.NoPerjanjian ?? HTL.HeaderInfo.NoPerjanjian;
                    model.TglPerjanjian = model.TglPerjanjian ?? HTL.HeaderInfo.TglPerjanjian;

                }

                model.Keterangan = model.Keterangan ?? "";
                model.Status = model.Status ?? "0";   // HTL.HeaderInfo.Status;
                model.Statusdesc = model.Statusdesc ?? HTL.HeaderInfo.Statusdesc;
                model.Statushakdesc = model.Statushakdesc ?? HTL.HeaderInfo.Statushakdesc;
                model.StatusHT = model.StatusHT ?? HTL.HeaderInfo.StatusHT;
                model.StatusHTdesc = model.StatusHTdesc ?? HTL.HeaderInfo.StatusHTdesc;
                model.NamaCabang = model.NamaCabang ?? HTL.HeaderInfo.NamaCabang;
                model.Case = model.Case ?? HTL.HeaderInfo.Case;
                model.CaseCabPending = model.CaseCabPending ?? HTL.HeaderInfo.CaseCabPending;
                model.CaseCabPendingAkd = model.CaseCabPendingAkd ?? HTL.HeaderInfo.CaseCabPendingAkd;
                model.nosht = model.nosht ?? HTL.HeaderInfo.nosht;
                model.kodesht = model.kodesht ?? HTL.HeaderInfo.kodesht;
                model.TglHasilSertifikat = model.TglHasilSertifikat ?? HTL.HeaderInfo.TglHasilSertifikat;
                model.noberkasceking = model.noberkasceking ?? HTL.HeaderInfo.noberkasceking;
                model.noberkasht = model.noberkasht ?? HTL.HeaderInfo.noberkasht;

                //if (model.JmlTerbitSPA != HTL.HeaderInfo.JmlTerbitSPA)
                //{
                //    model.JmlTerbitSPA = HTL.HeaderInfo.JmlTerbitSPA;
                //}

                if (int.Parse(UserTypes) == (int)UserType.Branch || int.Parse(UserTypes) == (int)UserType.HO)
                {
                    model.OrderKeNotaris = (model.OrderKeNotaris ?? "") == "" ? "" : (model.OrderKeNotaris);

                    if (model.OrderKeNotaris.Length > 15)
                    {
                        model.OrderKeNotaris = HasKeyProtect.Decryption(model.OrderKeNotaris);
                    }
                }
                else
                {
                    model.OrderKeNotaris = (model.OrderKeNotaris ?? "") == "" ? "" : HasKeyProtect.Decryption(model.OrderKeNotaris);
                }

                string bckpNoModl = model.NoAppl ?? "";
                ctex = (ctex ?? "") == "" ? "" : HasKeyProtect.Decryption(ctex);
                diadu = (diadu ?? "") == "" ? "" : HasKeyProtect.Decryption(diadu);

                model.NoAppl = (diadu ?? "") == "" ? model.NoAppl ?? "" : diadu;

                //no aplikasi boleh diedit jika status input dan belum ada detail
                if (int.Parse(Status) <= 0 && bckpNoModl != diadu && HTL.HeaderInfo.DataPSG.Rows.Count == 0 && HTL.HeaderInfo.DataTNH.Rows.Count == 0
                    && HTL.HeaderInfo.DataSRT.Rows.Count == 0 && HTL.HeaderInfo.DataSRTPSG.Rows.Count == 0)
                {
                    model.NoAppl = bckpNoModl;
                }

                statusNew = int.Parse(model.Status ?? "0");

                string EnumMessage = "";
                int result = 0;
                string notaris = (model.OrderKeNotaris == "-9999" ? "" : model.OrderKeNotaris);

                //if (int.Parse(UserTypes) == (int)UserType.Branch && (ctex.ToLower() ?? "") == "submit")
                //{
                //    if (statusOld == statusNew && statusOld > 6)
                //    {
                //        EnumMessage = "Pilih status penanganan";
                //    }
                //    if (model.Keterangan == "")
                //    {
                //        EnumMessage = "Silahkan isikan catatan";
                //    }
                //}

                //if (statusOld != statusNew && model.Keterangan == "" && (ctex.ToLower() ?? "") == "submit") // && statusOld > 6 tidak sama dengan revisi
                //{
                //    EnumMessage = "Silahkan isikan catatan";
                //}

                //if ((statusNew == 20 || statusNew == 30) && notaris == "") //proses pengecekan harus ada notaris
                //{
                //    EnumMessage = "Pilih Nama notaris";
                //}
                //if (int.Parse(UserTypes) == (int)UserType.Notaris && notaris == "")
                //{
                //    EnumMessage = "Pilih Nama notaris";
                //}

                //if (statusNew == -1 && statusOld != 0 && statusOld != 6 && (ctex.ToLower() ?? "") == "submit") //status penangan kosong atau tidak sama input berkas/perbaikan cabang
                //{
                //    EnumMessage = "Pilih Status Penanganan";
                //}

                //if (statusOld != int.Parse(model.Status) && int.Parse(model.Status) != -1 && (ctex.ToLower() ?? "") == "simpan")
                //{
                //    EnumMessage = "Silahkan 'Submit Pengajuan' jika anda merubah 'Status Penanganan'";
                //}
                //if (int.Parse(UserTypes) == (int)UserType.Branch && statusNew == 0 && (model.Keterangan ?? "") == "" && (ctex.ToLower() ?? "") == "submit")
                //{
                //    EnumMessage = "Silahkan isikan catatan";
                //}


                ////penagihan invoice
                //if (int.Parse(UserTypes) == (int)UserType.Notaris && statusNew == 50 &&
                //    (model.NoPerjanjian.Length < 10 || (model.TglPerjanjian ?? "") == "" || (model.KodeAkta ?? "") == "" || (model.NoHT ?? "") == ""
                //    || (model.NilaiHT ?? "0") == "0") && (model.JasaPengecekan == false || model.SKMHT == false
                //    || model.APHT_SHT == false || model.PENCORETAN_PTSL == false || model.ROYA == false))
                //{
                //    EnumMessage = "Isikan No Perjanjian,Tgl Perjanjian, Nilai HT, kode akta , nomor APHT dan jenis penanganan dengan benar";
                //}

                if ((model.Status == "27" || model.Status == "37" || model.Status == "36") && string.IsNullOrWhiteSpace(model.Keterangan))
                {
                    EnumMessage = "Silahkan isikan catatan";
                }
                else if ((model.Status == "37" || model.Status == "36") && model.Keterangan.Length < 50)
                {
                    EnumMessage = "Catatan harus lebih dari 50 karakter";
                }

                if ((model.NoAppl ?? "") == "")
                {
                    EnumMessage = "Isikan No Aplikasi";
                }
                string diaduasNoAppl = "";
                string IDIDENTI = "0";
                if (EnumMessage == "")
                {

                    //validation model
                    string EnumMessageModel = "";
                    string displayName = "";
                    foreach (var modelstat in ViewData.ModelState)
                    {
                        string strkey = modelstat.Key;

                        if (HasKeyProtect.Decryption(bckpNoModl) == "nonedtbale" && strkey.ToLower() == "noappl")
                        {
                            ModelState[strkey].Errors.Clear();
                        }

                        var error = modelstat.Value.Errors;
                        if (error.Count > 0)
                        {
                            MemberInfo property = typeof(cHTL).GetProperty(strkey);
                            if (property.CustomAttributes.Count() > 1)
                            {
                                try
                                {
                                    var attribute = property.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().Single();
                                    displayName = attribute.Name;
                                }
                                catch
                                {
                                    displayName = "";
                                }
                            }

                            displayName = displayName.Replace("_", "");
                            if (displayName != "")
                            {
                                foreach (var errortxt in error)
                                {
                                    EnumMessageModel = errortxt.ErrorMessage;
                                    if (EnumMessageModel != "")
                                    {
                                        EnumMessage = displayName + " " + EnumMessageModel;
                                        break;
                                    }
                                }

                                if (EnumMessageModel != "")
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (EnumMessage == "")
                    {
                        if ((ctex.ToLower() ?? "") == "submit")
                        {
                            //cek detail 
                            if (model.DataPSG == null)
                            {

                            }
                            else if (model.DataPSG.Rows.Count == 0)
                            {

                            }

                        }

                        DataTable dtx = await HTLddl.dbdbGetDdlOrderGetCek("11", "", "", "", UserID, GroupName);
                        int resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                        if (resultsuct > 0)
                        {
                            string poko = (ctex ?? "");
                            model.Status = (model.Status == "-1" || statusOld == 6 || statusOld == 0) ? statusOld.ToString() : model.Status;

                            string perihal = model.Case ?? "";
                            if (model.CaseMulti != null)
                            {
                                if (model.CaseMulti.Length > 0)
                                {
                                    perihal = string.Join(",", model.CaseMulti);
                                }
                            }


                            string perihalpending = model.CaseCabPending ?? "";
                            if (model.CaseCaPendingbMulti != null)
                            {
                                if (model.CaseCaPendingbMulti.Length > 0)
                                {
                                    perihalpending = string.Join(",", model.CaseCaPendingbMulti);
                                }
                            }


                            string perihalpendingakd = model.CaseCabPendingAkd ?? "";
                            if (model.CaseCaPendingAkdMulti != null)
                            {
                                if (model.CaseCaPendingAkdMulti.Length > 0)
                                {
                                    perihalpendingakd = string.Join(",", model.CaseCaPendingAkdMulti);
                                }
                            }

                            model.Case = perihal; //penanganan isue
                            model.CaseCabPending = perihalpending; //pending cabang
                            model.CaseCab = model.CaseCab ?? ""; //reject FIF
                            model.CaseCabPendingAkd = perihalpendingakd; //pending akad
                                                                         //tempat menentukan komen status//
                            DataTable dtresult = await HTLddl.dbupdateHTLNW(ID, model, ctex, caption, UserID, GroupName);
                            string HasilEnum = dtresult.Rows[0][0].ToString();
                            if (int.TryParse(HasilEnum, out int HasilEnumResult))
                            {
                                // Gunakan parsedResult untuk logika ketika hasil berupa angka
                                result = HasilEnumResult;
                                IDIDENTI = HasKeyProtect.Encryption(dtresult.Rows[0][1].ToString());
                                diaduasNoAppl = HasKeyProtect.Encryption(dtresult.Rows[0][2].ToString());
                                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "di" + ctex) : EnumMessage;

                                //result = int.Parse(dtresult.Rows[0][0].ToString());
                                //IDIDENTI = HasKeyProtect.Encryption(dtresult.Rows[0][1].ToString());
                                //diaduasNoAppl = HasKeyProtect.Encryption(dtresult.Rows[0][2].ToString());
                                //EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                                //EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "di" + ctex) : EnumMessage;


                                string orderPPAT = model.OrderKeNotaris;
                                if (result == 8890073) /* OD siap akad */
                                {
                                    DataTable dtresultod = await HTLddl.dbgetOrderOS("1", orderPPAT ?? "", caption, UserID, GroupName);
                                    EnumMessage = string.Format(EnumMessage, dtresultod.Rows[0][0].ToString(), dtresultod.Rows[0][1].ToString());
                                }

                                if (result == 8890074) /* OD sudah akad */
                                {
                                    DataTable dtresultod = await HTLddl.dbgetOrderOS("2", orderPPAT ?? "", caption, UserID, GroupName);
                                    EnumMessage = string.Format(EnumMessage, dtresultod.Rows[0][0].ToString(), dtresultod.Rows[0][1].ToString());
                                }

                                if (result == 8890085) /* OD expired */
                                {
                                    DataTable dtresultod = await HTLddl.dbgetOrderOS("3", orderPPAT ?? "", caption, UserID, GroupName);
                                    EnumMessage = string.Format(EnumMessage, dtresultod.Rows[0][0].ToString(), dtresultod.Rows[0][1].ToString());
                                }

                                if (result == 8890086) /* OD pending */
                                {
                                    DataTable dtresultod = await HTLddl.dbgetOrderOS("4", orderPPAT ?? "", caption, UserID, GroupName);
                                    EnumMessage = string.Format(EnumMessage, dtresultod.Rows[0][0].ToString(), dtresultod.Rows[0][1].ToString());
                                }

                                if (result == 8890087) /* OD perbaikan */
                                {
                                    DataTable dtresultod = await HTLddl.dbgetOrderOS("5", orderPPAT ?? "", caption, UserID, GroupName);
                                    EnumMessage = string.Format(EnumMessage, dtresultod.Rows[0][0].ToString(), dtresultod.Rows[0][1].ToString());
                                }
                            }
                            else
                            {
                                EnumMessage = HasilEnum;
                            }

                        }
                        else
                        {
                            result = resultsuct;
                            EnumMessage = "Batas waktu submit hanya diperbolehkan hari senin-jumat dari jam 08:00 s/d/ 17:00 (dihari kerja)";
                        }
                    }
                }

                ViewBag.ShowNotaris = "";
                if (int.Parse(UserTypes) == (int)UserType.FDCM)
                {
                    ViewBag.ShowNotaris = "allow";
                }
                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
                    mode = ctex.ToLower(),
                    diadu = diaduasNoAppl,
                    gtid = IDIDENTI,
                    resulted = result
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnUpdHTLIPT(cHTLIPTData model, string jntexipt, string auxipt, string ctexipt, HttpPostedFileBase filesipt, string euxipt)
        {

            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {

                HTL = TempData[tempTransksi] as vmHTL;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;


                //get value from aply filter //
                string keyword = modFilter.keywordfilter;
                string keylookupdata = model.keylookupdataHTXIpt;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                // string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

                DataRow dr;
                int ID = 0;
                int statusOld = 0;
                int statusNew = 0;


                model.NoApplIpt = HashNetFramework.HasKeyProtect.Decryption(auxipt);
                model.JenisData = HashNetFramework.HasKeyProtect.Decryption(jntexipt);
                string mode = HashNetFramework.HasKeyProtect.Decryption(ctexipt);
                string iptspa = HashNetFramework.HasKeyProtect.Decryption(euxipt);
                string nosertifikatlama = "";
                string niklama = "";
                string refniklama = "";

                string viewbro = "";
                string viewppat = "";

                string JENDOC = "";
                double MaxSIZEDOC = 0;
                if ((keylookupdata ?? "") != "")
                {
                    if (model.JenisData == "0") //pasangan debitur 
                    {
                        JENDOC = "KTP";
                        dr = HTL.HeaderInfo.DataPSG.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                        ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                        if (ID > 0)
                        {
                            nosertifikatlama = dr["NoSertifikat"].ToString();
                            niklama = dr["NIK"].ToString();
                            refniklama = dr["RefNIK"].ToString();
                        }
                    }

                    if (model.JenisData == "1") //bidang tanah
                    {
                        JENDOC = "SERTIFIKAT";
                        MaxSIZEDOC = 5000;

                        if (iptspa == "fillspaht")
                        {
                            dr = HTL.HeaderInfo.DataTNHSPA.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                        }
                        else
                        {
                            dr = HTL.HeaderInfo.DataTNH.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                        }

                        ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                        if (ID > 0)
                        {
                            nosertifikatlama = dr["NoSertifikat"].ToString();
                            niklama = dr["NIK"].ToString();
                            refniklama = dr["RefNIK"].ToString();
                        }
                    }

                    if (model.JenisData == "2") //pemilik sertifikat
                    {
                        JENDOC = "KTP";
                        dr = HTL.HeaderInfo.DataSRT.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                        ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                        if (ID > 0)
                        {
                            nosertifikatlama = dr["NoSertifikat"].ToString();
                            niklama = dr["NIK"].ToString();
                            refniklama = dr["RefNIK"].ToString();
                        }
                    }

                    if (model.JenisData == "3") //pasangan pemilik sertifikat
                    {
                        JENDOC = "KTP";
                        dr = HTL.HeaderInfo.DataSRTPSG.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                        ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                        if (ID > 0)
                        {
                            nosertifikatlama = dr["NoSertifikat"].ToString();
                            niklama = dr["NIK"].ToString();
                            refniklama = dr["RefNIK"].ToString();
                        }
                    }
                }


                string CEKMODEL = "";
                if (model.JenisData == "0" || model.JenisData == "2" || model.JenisData == "3")
                {
                    CEKMODEL = "DTKTP";
                }
                else if (model.JenisData == "1")
                {
                    CEKMODEL = "DTTNH";
                }

                //validation model
                string message = "";
                int result = -1;
                string EnumMessageModel = "";
                string displayName = "";
                string EnumMessage = "";
                foreach (var modelstat in ViewData.ModelState)
                {
                    string strkey = modelstat.Key;
                    var error = modelstat.Value.Errors;
                    if (error.Count > 0)
                    {
                        MemberInfo property = typeof(cHTLIPTData).GetProperty(strkey);
                        if (property.CustomAttributes.Count() > 1)
                        {
                            try
                            {
                                var attribute = property.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().Single();
                                displayName = attribute.Name;
                                if (!displayName.Contains(CEKMODEL))
                                {
                                    displayName = "";
                                }
                                else
                                {
                                    displayName = displayName.Replace(CEKMODEL, "");
                                }
                            }
                            catch
                            {
                                displayName = "";
                            }
                        }

                        displayName = displayName.Replace("_", "");
                        if (displayName != "")
                        {
                            foreach (var errortxt in error)
                            {
                                EnumMessageModel = errortxt.ErrorMessage;
                                if (EnumMessageModel != "")
                                {
                                    EnumMessage = displayName + " " + EnumMessageModel;
                                    break;
                                }
                            }

                            if (EnumMessageModel != "")
                            {
                                break;
                            }
                        }
                    }
                }

                //if (filesipt == null)
                //{
                //    EnumMessage = "Upload dokumen " + JENDOC;
                //}

                //if (filesipt != null)
                //{
                //    EnumMessage = await Commonddl.dbValidFileupload(filesipt, JENDOC, model.NoApplIpt, caption, UserID, GroupName, MaxSIZEDOC);
                //}

                if (iptspa == "fillspaht")
                {
                    EnumMessage = "";
                }

                if (EnumMessage == "")
                {
                    if (iptspa == "fillspaht")
                    {
                        model.JenisData = "fillspaht";
                        result = await HTLddl.dbupdateHTLIPTSPAHT(ID, model, mode, caption, UserID, GroupName);
                        model.JenisData = "1";
                    }
                    else
                    {
                        result = await HTLddl.dbupdateHTLIPT(ID, model, mode, caption, UserID, GroupName);
                    }


                    if (model.JenisData == "0")
                    {
                        viewbro = "/Views/HTL/uiHTLLstGridPSG.cshtml";
                    }
                    if (model.JenisData == "1")
                    {
                        if (iptspa == "fillspaht")
                        {
                            viewbro = "/Views/HTL/uiHTLLstGridTNHSPA.cshtml";
                        }
                        else
                        {
                            viewbro = "/Views/HTL/uiHTLLstGridTNH.cshtml";
                            IEnumerable<cListSelected> ppat = await HTLddl.dbdbGetDdlNotarisListByEncrypt(caption, model.NoApplIpt, UserID, GroupName);
                            viewppat = new JavaScriptSerializer().Serialize(ppat);
                        }

                    }
                    if (model.JenisData == "2")
                    {
                        viewbro = "/Views/HTL/uiHTLLstGridSRT.cshtml";
                    }
                    if (model.JenisData == "3")
                    {
                        viewbro = "/Views/HTL/uiHTLLstGridSRTPSG.cshtml";
                    }

                    if (result == 1)
                    {
                        if (model.JenisData == "0")
                        {
                            HTL.HeaderInfo.DataPSG = await HTLddl.dbGetMultiData(model.NoApplIpt, model.JenisData, caption, UserID, GroupName);
                        }
                        if (model.JenisData == "1")
                        {
                            if (iptspa == "fillspaht")
                            {
                                HTL.HeaderInfo.DataTNHSPA = await HTLddl.dbGetMultiData(model.NoApplIpt, model.JenisData, caption, UserID, GroupName);
                            }
                            else
                            {
                                HTL.HeaderInfo.DataTNH = await HTLddl.dbGetMultiData(model.NoApplIpt, model.JenisData, caption, UserID, GroupName);
                            }
                        }
                        if (model.JenisData == "2")
                        {
                            HTL.HeaderInfo.DataSRT = await HTLddl.dbGetMultiData(model.NoApplIpt, model.JenisData, caption, UserID, GroupName);
                        }
                        if (model.JenisData == "3")
                        {
                            HTL.HeaderInfo.DataSRTPSG = await HTLddl.dbGetMultiData(model.NoApplIpt, model.JenisData, caption, UserID, GroupName);
                        }
                        message = (mode == "Simpan" ? "Data Berhasil Disimpan" : mode == "Hapus" ? "Data Berhasil dihapus" : "");
                    }
                    else
                    {
                        message = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    }
                }
                else
                {
                    message = EnumMessage;
                    result = -1;
                }

                ViewBag.Nappl = model.NoApplIpt;
                ViewBag.Jndata1 = "1";
                ViewBag.Jndata2 = "2";
                ViewBag.Jndata3 = "0";
                ViewBag.Jndata4 = "3";

                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = (result == 1) ? CustomEngineView.RenderRazorViewToString(ControllerContext, viewbro, HTL.HeaderInfo) : "",
                    viewppatbro = viewppat,
                    msg = message,
                    mode = "",
                    resulted = result
                }); ;

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> clnOpenShowvalINV(string paramkey, string oprfun)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                HTL = HTL == null ? new vmHTL() : HTL;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                paramkey = HashNetFramework.HasKeyProtect.Decryption(paramkey);
                oprfun = HashNetFramework.HasKeyProtect.Decryption(oprfun);

                HTL.DTHistory = await HTLddl.dbShowHisHTLINV(paramkey, "HTLLIST", UserID, GroupName);
                ViewBag.NoAPP = "NO APLIKASI " + paramkey;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_CheckINV.cshtml", HTL),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> clnOpenShowvalEXP(string paramkey, string oprfun)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                HTL = HTL == null ? new vmHTL() : HTL;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                paramkey = HashNetFramework.HasKeyProtect.Decryption(paramkey);
                oprfun = HashNetFramework.HasKeyProtect.Decryption(oprfun);

                HTL.DTHistory = await HTLddl.dbShowHisHTLEXP(paramkey, "HTLLIST", UserID, GroupName);
                ViewBag.NoAPP = "NO APLIKASI " + paramkey;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_CheckEXP.cshtml", HTL),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> clnOpenShowvalRJT(string paramkey, string oprfun)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                HTL = HTL == null ? new vmHTL() : HTL;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                paramkey = HashNetFramework.HasKeyProtect.Decryption(paramkey);
                oprfun = HashNetFramework.HasKeyProtect.Decryption(oprfun);

                HTL.DTHistory = await HTLddl.dbShowHisHTLRJT(paramkey, "HTLLIST", UserID, GroupName);
                ViewBag.NoAPP = "NO APLIKASI " + paramkey;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_CheckRJT.cshtml", HTL),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public async Task<ActionResult> clnOpenShowHisTL(string paramkey, string oprfun)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                HTL = HTL == null ? new vmHTL() : HTL;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                paramkey = HashNetFramework.HasKeyProtect.Decryption(paramkey);
                oprfun = HashNetFramework.HasKeyProtect.Decryption(oprfun);

                HTL.DTHistory = await HTLddl.dbShowHisHTL(oprfun, paramkey, "HTLLIST", UserID, GroupName);
                HTL.DTGIsue = await HTLddl.dbShowHisGroupIsue(UserID);
                ViewBag.NoAPP = "NO APLIKASI " + paramkey;
                ViewBag.No_App = paramkey;
                ViewBag.User = UserID;
                //ViewBag.NoAPP = "NO APLIKASI " + paramkey;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_TimeLine.cshtml", HTL),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        #region upload Sertifikat
        public async Task<ActionResult> clnKoncePlodSrt(string paridno, string parkepo)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLODSERTICEK";
                string moduleID = HasKeyProtect.Decryption(caption);
                //if (Common.ddlDocument == null)
                {
                    Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenList("2", "", "3", caption, UserID, GroupName);
                }
                ViewData["SelectDocument"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Upload Sertifikat Pengecekan HT";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLUploadSRT.cshtml", HTL),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePlodsrtsve(HttpPostedFileBase files, string idx, string modepro, string tglpro, string noappl)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }


            string parcno = "";
            string keylokup = parcno;
            string Jenis_Doc = "SERTIFIKAT PENGECEKAN";

            byte[] bytefl = bytefl = Encoding.ASCII.GetBytes("");
            string filename = "";
            string filetype = "";
            string validmsg = "";
            int resultsuct = 0;

            HTL = TempData[tempTransksi] as vmHTL;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);

            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = (caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();

                if (AllowUpload == true)
                {

                    if (modepro == "chk")
                    {

                        var xmlString = "";
                        XmlDocument xml = new XmlDocument();
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\Template\\TemplateDoc_Check.xml";
                        xml.Load(xmlString);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                        nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
                        XmlElement root = xml.DocumentElement;

                        DataTable dtlist = await HTLddl.dbdbGetDdlOrderGetCek("8", "", DateTime.Parse(tglpro).ToString("yyyyMMdd"), moduleID, UserID, GroupName);
                        XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

                        foreach (DataRow rw in dtlist.Rows)
                        {
                            XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlAttrRow.Value = "0";
                            XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlRecordNo.Attributes.Append(xmlAttrRow);
                            foreach (DataColumn col in dtlist.Columns)
                            {
                                XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                                XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                                string attrval = col.ColumnName.Split('_')[1].ToString();
                                xmlAttrNumber.Value = attrval;
                                XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                                xmlRecordNoceldata.InnerText = rw[col].ToString();
                                xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                                xmlRecordNo.AppendChild(xmlRecordNocel);
                                xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                            }
                            nodesing.AppendChild(xmlRecordNo);
                        }

                        filename = "HasilPengecekan_Dokumen_" + DateTime.Parse(tglpro).ToString("yyyyMMdd") + ".xml";
                        filetype = "application/xml";
                        bytefl = Encoding.ASCII.GetBytes(xml.OuterXml);
                    }
                    else
                    {
                        validmsg = await Commonddl.dbValidFileupload(files, "SERTICEK", noappl, moduleID, UserID, GroupName);
                        if (validmsg == "")
                        {

                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(files.InputStream);
                            imagebyte = reader.ReadBytes((int)files.ContentLength);

                            //get mimne type
                            string mimeType = files.ContentType;
                            if (mimeType.Contains("image"))
                            {
                                imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                            }


                            var splitfilename = files.FileName.Split('_');
                            string noapplfile = splitfilename[0];
                            string NamaSertifikat = splitfilename[1];

                            //prepare to encrypt
                            string KECEP = noapplfile;
                            string KECEPDB = KECEP;
                            KECEP = HasKeyProtect.Encryption(KECEP);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                            string DocumentType = Jenis_Doc.ToUpper();
                            string FileName = files.FileName;

                            string ContentType = "Application/pdf";
                            string ContentLength = filebyteECP.Length.ToString();
                            string FileByte = base64String;

                            NamaSertifikat = splitfilename[1].Replace(".jpg", "").Replace(".jpeg", "").Replace(".JPG", "").Replace(".JPEG", "").Replace(".pdf", "").Replace(".PDF", "");
                            DataTable dtx = await HTLddl.dbSaveRegMitradoc("0", "", "", "", noapplfile, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                            resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                            if (resultsuct != 1)
                            {
                                validmsg = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct);
                            }
                        }
                    }
                }
                else
                {
                    validmsg = "user tidak memiliki akses";
                }

                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;

                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    filmsg = validmsg,
                    filidx = int.Parse(idx) + 1,
                    resultsuc = resultsuct,
                    flbt = bytefl,
                    flbtnm = filename,
                    flbtmtype = filetype,
                    mode = modepro,
                });

            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion upload Sertifikat

        #region upload Dokumen

        public async Task<ActionResult> clnCheckDocmandor(string paridno, string stated)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLOADDOC";
                string usertyp = Account.AccountLogin.UserType;
                string moduleID = HasKeyProtect.Decryption(caption);

                HTL = TempData[tempTransksi] as vmHTL;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData["common"] as vmCommon);

                stated = HasKeyProtect.Decryption(stated);
                paridno = HasKeyProtect.Decryption(paridno);
                await Commonddl.dbdbGetJenisDokumen(stated, paridno, "5", caption, UserID, GroupName);

                string opneiptspa = "";
                string vie = "";

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string vienmae1 = ".showapt";
                string vienmae2 = ".showcncl";
                // string vienmae3 = ".showpenakd";
                string vienmae = "";
                string valued = "";
                string iptspa = "";

                if (
                        ((stated == "46" || stated == "44") && (int.Parse(UserTypes) == (int)UserType.Notaris || int.Parse(UserTypes) == (int)UserType.FDCM))
                   )
                /*terbit spa */
                {
                    vienmae = ".showapt";
                    opneiptspa = "opn";
                    //vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_FillSPA.cshtml", HTL.HeaderInfo
                    //ambil data tanah//
                    //allow edit button SPA/HT
                    //HTL = new vmHTL();
                    //HTL.HeaderInfo = new cHTL();
                    HTL.HeaderInfo.DataTNHSPA = await HTLddl.dbGetMultiData(paridno, "1", caption, UserID, GroupName);
                    //HTL.HeaderInfo.NoAppl = paridno;

                    if (stated == "44")
                    {
                        valued = ".iptsht,.iptspa";
                    }

                    if (stated == "46")
                    {
                        valued = ".iptsht";
                    }


                    //
                    vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLLstGridTNHSPA.cshtml", HTL.HeaderInfo);
                }
                else if (stated == "24" || stated == "25" || stated == "7") //batal pengajuan
                {
                    opneiptspa = "opn";
                    vienmae = ".showcncl";

                    ViewData["SelectCaseCab"] = new MultiSelectList(Common.ddlCaseCab, "Value", "Text");
                    vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_FillCNCL.cshtml", HTL.HeaderInfo);
                }
                else if (stated == "37" || stated == "36" || stated == "27") //menyelesaikan masalah isue Sertifikat
                {
                    usertyp = HashNetFramework.HasKeyProtect.Decryption(usertyp);
                    if (usertyp == "3")
                    {
                        ViewBag.user = "notaris";
                    }
                    else if (usertyp == "0")
                    {
                        ViewBag.user = "admin";
                    }

                    opneiptspa = "opn";
                    vienmae = ".showtangan";
                    ViewData["SelectCase"] = new MultiSelectList(Common.ddlCase, "Value", "Text");
                    valued = HTL.HeaderInfo.Case;
                    vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_ListPenanganan.cshtml", HTL.HeaderInfo);
                }
                else if (stated == "21") //pending
                {
                    opneiptspa = "opn";
                    vienmae = ".showpending";
                    ViewData["SelectCasePending"] = new MultiSelectList(Common.ddlCasepen, "Value", "Text");

                    usertyp = HashNetFramework.HasKeyProtect.Decryption(usertyp);
                    if (usertyp == "3")
                    {
                        ViewBag.user = "notaris";
                    }
                    else if (usertyp == "0")
                    {
                        ViewBag.user = "admin";
                    }
                    ViewBag.oprvalue = "edit";
                    valued = HTL.HeaderInfo.CaseCabPending;
                    vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_ListPending.cshtml", HTL.HeaderInfo);
                }
                else if (stated == "32" || stated == "29" || stated == "30") //pending akad
                {
                    opneiptspa = "opn";
                    vienmae = ".showpenakd";
                    ViewData["SelectCasePendingAkd"] = new MultiSelectList(Common.ddlCasepenAkd, "Value", "Text");

                    usertyp = HashNetFramework.HasKeyProtect.Decryption(usertyp);
                    if (usertyp == "3")
                    {
                        ViewBag.user = "notaris";
                    }
                    else if (usertyp == "0")
                    {
                        ViewBag.user = "admin";
                    }
                    ViewBag.oprvalue = "edit";
                    valued = HTL.HeaderInfo.CaseCabPendingAkd;
                    vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_ListPendingAkad.cshtml", HTL.HeaderInfo);
                }
                else if (stated == "54" || stated == "47" || stated == "43" || GroupName == "SDB-REGHT") //pengajuan HT
                {
                    vienmae = ".showapt"; // ".showsht";
                    opneiptspa = "opn";
                    usertyp = HashNetFramework.HasKeyProtect.Decryption(usertyp);
                    if (usertyp == "3")
                    {
                        ViewBag.user = "notaris";
                    }
                    else if (usertyp == "0")
                    {
                        ViewBag.user = "admin";
                    }
                    ViewBag.oprvalue = "edit";
                    valued = "";


                    HTL.HeaderInfo.DataTNHSPA = await HTLddl.dbGetMultiData(paridno, "1", caption, UserID, GroupName);
                    //HTL.HeaderInfo.NoAppl = paridno;
                    vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLLstGridTNHSPA.cshtml", HTL.HeaderInfo);
                    //vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_FillSHT.cshtml", HTL.HeaderInfo);
                }
                else
                {
                    vienmae = ".showapt,.showsht,.showcncl,.showtangan,.showpending,.showpenakd";
                }

                bool AllowSaave = false;
                bool AllowEdit = false;
                bool AllowSubmit = false;

                DataRow[] drw = Common.ddlstatusmap.AsEnumerable().Where(x => x.Field<string>("EnumValue") == stated).ToArray();
                if (drw.Length > 0)
                {
                    AllowSaave = bool.Parse(drw[0]["AllowSave"].ToString());
                    AllowSubmit = bool.Parse(drw[0]["AllowSubmit"].ToString());
                    AllowEdit = bool.Parse(drw[0]["AllowEdit"].ToString());
                }
                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Upload Berkas Dokumen HT";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = opneiptspa,
                    vienm = vienmae,
                    vienm1 = vienmae1,
                    vienm2 = vienmae2,
                    view = vie,
                    valu = valued,
                    alsv = AllowSaave,
                    aled = AllowEdit,
                    alsb = AllowSubmit,
                    spaipt = iptspa
                }); ;

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> clnCheckDocmandorcncl(string paridno, string stated)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLOADDOC";
                string moduleID = HasKeyProtect.Decryption(caption);

                HTL = TempData[tempTransksi] as vmHTL;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData["common"] as vmCommon);

                stated = HasKeyProtect.Decryption(stated);
                paridno = HasKeyProtect.Decryption(paridno);
                await Commonddl.dbdbGetJenisDokumen(stated, paridno, "5", caption, UserID, GroupName);

                string opneiptspa = "";
                string vie = "";

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string vienmae = ".showcncl";
                if (stated == "24" || stated == "25" || stated == "7") //batal pengajuan
                {
                    opneiptspa = "opn";
                    ViewData["SelectCaseCab"] = new MultiSelectList(Common.ddlCaseCab, "Value", "Text");
                    vie = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_FillCNCL.cshtml", HTL.HeaderInfo);
                }

                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Upload Berkas Dokumen HT";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = opneiptspa,
                    vienm = vienmae,
                    view = vie
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public async Task<ActionResult> clnKoncePloddoc(string paridno, string parkepo)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLOADDOC";
                string moduleID = HasKeyProtect.Decryption(caption);

                HTL.DTDokumen = await Commonddl.dbdbGetJenisDokumen("0", paridno, "3", caption, UserID, GroupName);
                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Upload Berkas Dokumen HT";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLUploadDoc.cshtml", HTL),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> clnKoncePloddocnw(string paridno, string paridno1, string parkepo)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLOADDOC";
                string moduleID = HasKeyProtect.Decryption(caption);
                paridno = HasKeyProtect.Decryption(paridno);
                paridno1 = HasKeyProtect.Decryption(paridno1);
                paridno1 = (paridno1 == "-9999") ? "" : paridno1;
                parkepo = HasKeyProtect.Decryption(parkepo);
                string htocpar = (paridno == "") ? paridno1 : paridno;
                HTL.DTDokumen = await Commonddl.dbdbGetJenisDokumen("0", htocpar, "4", caption, UserID, GroupName);
                htocpar = HasKeyProtect.Encryption(htocpar);

                cHTL dtt = new cHTL();
                dtt.DTDokumen = HTL.DTDokumen;
                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Upload Dokumen";
                ViewBag.caption = caption;
                ViewBag.oprvalue = parkepo;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    htdoc = htocpar,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLUploadDocNw.cshtml", dtt),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> clnKoncePloddocnwvw(string paridno, string parkepo)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLOADDOC";
                string moduleID = HasKeyProtect.Decryption(caption);
                paridno = HasKeyProtect.Decryption(paridno);

                Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenDll("-900", paridno, "3", caption, UserID, GroupName);
                ViewData["SelectDocumentReg"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                cHTL dtt = new cHTL();
                dtt.DTDokumen = HTL.DTDokumen;
                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Lihat Dokumen";
                ViewBag.caption = caption;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLUploadDocVw.cshtml", dtt),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> clnKoncePloddocshtnw(string paridno, string paridno1, string parkepo)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLOADDOC";
                string moduleID = HasKeyProtect.Decryption(caption);
                paridno = HasKeyProtect.Decryption(paridno);
                paridno1 = HasKeyProtect.Decryption(paridno1);
                paridno1 = (paridno1 == "-9999") ? "" : paridno1;
                parkepo = HasKeyProtect.Decryption(parkepo);
                string htocpar = (paridno == "") ? paridno1 : paridno;
                // HTL.DTDokumen = await Commonddl.dbdbGetJenisDokumen("0", htocpar, "4", caption, UserID, GroupName);
                //htocpar = HasKeyProtect.Encryption(htocpar);

                cHTL dtt = new cHTL();
                //dtt.DTDokumen = HTL.DTDokumen;
                //modFilter.idcaption = caption;
                //TempData[tempTransksi] = HTL;
                //TempData[tempTransksifilter] = modFilter;
                //TempData["common"] = Common;

                ViewBag.captiondesc = "Upload Dokumen";
                ViewBag.caption = caption;
                ViewBag.oprvalue = parkepo;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    htdoc = htocpar,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLUploadDocNwSHT.cshtml", dtt),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePloddocsve(HttpPostedFileBase files, string idx, string modepro, string tglpro, string noappl)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            string parcno = "";
            string keylokup = parcno;
            string Jenis_Doc = "";

            byte[] bytefl = bytefl = Encoding.ASCII.GetBytes("");
            string filename = "";
            string filetype = "";
            string validmsg = "";
            int resultsuct = 0;

            HTL = TempData[tempTransksi] as vmHTL;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);

            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = (caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();

                if (AllowUpload == true)
                {

                    if (modepro == "chk")
                    {

                        var xmlString = "";
                        XmlDocument xml = new XmlDocument();
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\Template\\TemplateDoc_Check.xml";
                        xml.Load(xmlString);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                        nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
                        XmlElement root = xml.DocumentElement;

                        DataTable dtlist = await HTLddl.dbdbGetDdlOrderGetCek("5", "", DateTime.Parse(tglpro).ToString("yyyyMMdd"), moduleID, UserID, GroupName);
                        XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

                        foreach (DataRow rw in dtlist.Rows)
                        {
                            XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlAttrRow.Value = "0";
                            XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlRecordNo.Attributes.Append(xmlAttrRow);
                            foreach (DataColumn col in dtlist.Columns)
                            {
                                XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                                XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                                string attrval = col.ColumnName.Split('_')[1].ToString();
                                xmlAttrNumber.Value = attrval;
                                XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                                xmlRecordNoceldata.InnerText = rw[col].ToString();
                                xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                                xmlRecordNo.AppendChild(xmlRecordNocel);
                                xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                            }
                            nodesing.AppendChild(xmlRecordNo);
                        }

                        filename = "HasilPengecekan_Dokumen_" + DateTime.Parse(tglpro).ToString("yyyyMMdd") + ".xml";
                        filetype = "application/xml";
                        bytefl = Encoding.ASCII.GetBytes(xml.OuterXml);
                    }
                    else if (modepro == "sbal")
                    {
                        DataTable dtx = await HTLddl.dbdbGetDdlOrderGetCek("9", noappl, "", moduleID, UserID, GroupName);
                        resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                        if (resultsuct > 0)
                        {
                            validmsg = "Isikan semua File Dokumen untuk jenis dokumen yang bertanda *(bintang)";
                            resultsuct = -999;
                        }
                        else if (resultsuct == -8909)
                        {
                            validmsg = "Dokumen untuk No Applikasi '" + noappl + "' sudah diupload oleh user lain";
                            resultsuct = -999;
                        }
                        else
                        {
                            dtx = await HTLddl.dbdbGetDdlOrderGetCek("6", noappl, "", moduleID, UserID, GroupName);
                            resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                            if (resultsuct != 1)
                            {
                                if (resultsuct == -90)
                                {
                                    validmsg = "Upload tidak diijinkan, Applikasi sedang dalam proses pengecekan";
                                }
                                else
                                {
                                    validmsg = "Upload dokumen gagal, silahkan coba beberapa saat lagi";
                                }
                            }
                            else
                            {
                                validmsg = "Upload dokumen berhasil";
                            }
                        }
                    }
                    else
                    {
                        validmsg = await Commonddl.dbValidFileupload(files, "UPLOADDOC", noappl, moduleID, UserID, GroupName);
                        if (validmsg == "")
                        {

                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(files.InputStream);
                            imagebyte = reader.ReadBytes((int)files.ContentLength);

                            //get mimne type
                            string mimeType = files.ContentType;
                            if (mimeType.Contains("image"))
                            {
                                imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                            }


                            var splitfilename = files.FileName.Split('_');
                            string NoSertifikat = splitfilename[0];
                            string NamaSertifikat = splitfilename[1];

                            //prepare to encrypt
                            string KECEP = NoSertifikat;
                            string KECEPDB = KECEP;
                            KECEP = HasKeyProtect.Encryption(KECEP);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                            Jenis_Doc = splitfilename[1].ToLower().Replace(".jpg", "").Replace(".jpeg", "").Replace(".JPG", "").Replace(".JPEG", "").Replace(".pdf", "").Replace(".PDF", "");
                            string DocumentType = Jenis_Doc.ToUpper();
                            string FileName = files.FileName;

                            string ContentType = "Application/pdf";
                            string ContentLength = filebyteECP.Length.ToString();
                            string FileByte = base64String;

                            DataTable dtx = await HTLddl.dbSaveRegMitradoc("0", "", "", "", noappl, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                            resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                            if (resultsuct != 1)
                            {
                                validmsg = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct);
                            }
                        }
                    }
                }
                else
                {
                    validmsg = "user tidak memiliki akses";
                }

                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    filmsg = validmsg,
                    filidx = int.Parse(idx) + 1,
                    resultsuc = resultsuct,
                    flbt = bytefl,
                    flbtnm = filename,
                    flbtmtype = filetype,
                    mode = modepro,
                });

            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePloddoconesve(HttpPostedFileBase[] files, string[] documen, string cntproo, string noappl)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            string urlpath = "";
            if (IsErrorTimeout == true)
            {
                urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            string parcno = "";
            string keylokup = parcno;
            string EnumMessage = "";
            int result = 0;
            string viewtbl = "";

            HTL = TempData[tempTransksi] as vmHTL;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);


            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = (modFilter.idcaption ?? modFilter.ModuleID);

                string moduleID = caption != "UPLOADDOC" ? HasKeyProtect.Decryption(caption) : caption;
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();
                bool AllowSubmit = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowSubmit).SingleOrDefault();


                string FlagOpr = "";
                string notransaksi = "";
                string RegType = "";
                string Noktp = "";
                string NIK = "";
                string ID = "";
                string IDUpload = "";
                string IDresult = "0";
                //cntproo = HasKeyProtect.Decryption(cntproo);
                //string[] cntproodata = cntproo.Split('|');
                //if (cntproo != "templod")
                //{
                //    FlagOpr = HasKeyProtect.Decryption(cntproodata[0]);
                //    //notransaksi = (cntproodata[1]);
                //    //RegType = (cntproodata[2]);
                //    //Noktp = (cntproodata[3]);
                //    //NIK = (cntproodata[4]);
                //    //ID = (cntproodata[5]);
                //}
                //DataRow dr = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdataHTX") == noappl).SingleOrDefault();
                //noappl = dr["NoAPPL"].ToString();
                string kode = "";
                bool asxit = false;
                if (cntproo.Contains("#81x@") || cntproo.Contains("#82x@") || cntproo.Contains("#83x@") || cntproo.Contains("#84x@"))
                {
                    asxit = true;
                    kode = cntproo.Substring(cntproo.Length - 5);
                    cntproo = cntproo.Replace(kode, "");
                }


                cntproo = HashNetFramework.HasKeyProtect.Decryption(cntproo) == "-9999" ? cntproo : HashNetFramework.HasKeyProtect.Decryption(cntproo);
                if (HTL.HeaderInfo == null)
                {
                    HTL.HeaderInfo = new cHTL();
                }

                if (noappl == HTL.HeaderInfo.keylookupdataHTX && cntproo == HTL.HeaderInfo.NoAppl)
                {
                    string[] doc = null;

                    if (kode == "#81x@")
                    {
                        string[] doc1 = { HashNetFramework.HasKeyProtect.Encryption("SERTIFIKAT PENGECEKAN") };
                        doc = doc1;
                    }
                    else if (kode == "#82x@")
                    {
                        string[] doc2 = { HashNetFramework.HasKeyProtect.Encryption("TTD") };
                        doc = doc2;
                    }
                    else if (kode == "#83x@")
                    {
                        string[] doc3 = { HashNetFramework.HasKeyProtect.Encryption("PPK") };
                        doc = doc3;
                    }
                    else if (kode == "#84x@")
                    {
                        string[] doc4 = { HashNetFramework.HasKeyProtect.Encryption("SPA") };
                        doc = doc4;
                    }

                    documen = doc;
                    noappl = HTL.HeaderInfo.NoAppl;

                    //if (asxit == true)
                    //{
                    //    string[] doca = { HashNetFramework.HasKeyProtect.Encryption("TTD") };
                    //    documen = doca;
                    //}
                }

                if (AllowUpload == true || (AllowSubmit == true))
                {

                    if (files != null)
                    {
                        foreach (HttpPostedFileBase file in files)
                        {

                            decimal sizep = file.ContentLength / 1000;
                            if (file.FileName.Length > 50)
                            {
                                EnumMessage = "Nama File " + file.FileName + " tidak boleh lebih dari 50 karakter";
                            }
                            else
                            if (!file.ContentType.ToLower().Contains("jpg") && !file.ContentType.ToLower().Contains("jpeg") && !file.ContentType.ToLower().Contains("pdf"))
                            {
                                EnumMessage = "File " + file.FileName + " harus Extention jpg,jpeg,pdf";
                            }
                            else
                            if (sizep > 1000 && cntproo.ToString().ToLower() != "sertifikat")
                            {
                                EnumMessage = "File " + file.FileName + " Ukuran File tidak boleh lebih dari 1 MB";
                            }
                            else
                            if (sizep > 5000 && cntproo.ToString().ToLower() == "sertifikat")
                            {
                                EnumMessage = "File " + file.FileName + " Ukuran File tidak boleh lebih dari 5 MB";
                            }

                            if ((noappl ?? "").Length != 14)
                            {
                                EnumMessage = "No Applikasi harus 14 digit";
                            }
                        }

                        if (EnumMessage == "")
                        {
                            var idoc = 0;
                            foreach (HttpPostedFileBase file in files)
                            {

                                byte[] imagebyte = null;
                                BinaryReader reader = new BinaryReader(file.InputStream);
                                imagebyte = reader.ReadBytes((int)file.ContentLength);

                                //get mimne type
                                string mimeType = file.ContentType;
                                if (kode != "#82x@")
                                {
                                    if (mimeType.Contains("image"))
                                    {
                                        imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                                    }
                                }

                                //prepare to encrypt
                                string KECEP = noappl;
                                string KECEPDB = KECEP;
                                KECEP = HasKeyProtect.Encryption(KECEP);
                                byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                                //convert byte to base//
                                string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                                string DocumentType = HasKeyProtect.Decryption(documen[idoc]);
                                string FileName = "";

                                if ((HTL.HeaderInfo.NamaPemilikSertifikat ?? "") == "")
                                {
                                    FileName = DocumentType.ToUpper() + ".pdf";
                                }
                                else
                                {
                                    FileName = DocumentType.ToUpper() + "_" + (HTL.HeaderInfo.NoAppl ?? "") + "_" + (HTL.HeaderInfo.NamaPemilikSertifikat ?? "") + ".pdf";
                                }

                                string ContentType = "Application/pdf";
                                string ContentLength = filebyteECP.Length.ToString();
                                string FileByte = base64String;

                                DataTable dtx = new DataTable();
                                //if (cntproo != "templod")
                                //{
                                dtx = await HTLddl.dbSaveRegMitradoc("0", FlagOpr, "", Noktp, noappl, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                                //}
                                //else
                                //{
                                //    dtx = await HTLddl.dbSaveDocTemp("0", "", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                                //}

                                idoc = idoc + int.Parse(dtx.Rows[0][0].ToString());
                                IDresult = dtx.Rows[0][0].ToString();
                                IDUpload = HasKeyProtect.Encryption(dtx.Rows[0][1].ToString());
                                // disini ditambahkan jika gagal menyimpan do
                            }

                            if (files.Count() != int.Parse(idoc.ToString()))
                            {
                                EnumMessage = "Terdapat Dokumen yang tidak terupload silahkan dicek kembali";
                            }

                            result = int.Parse(IDresult);

                            if (result == -8909)
                            {
                                EnumMessage = "Dokumen untuk No Applikasi '" + noappl + "' sudah diupload oleh user lain";
                            }
                            if (result == 1)
                            {
                                EnumMessage = "Upload berhasil";
                            }
                        }
                    }
                    else
                    {
                        EnumMessage = "Pilih file yang akan diupload";
                    }
                }
                else
                {
                    EnumMessage = "User tidak memiliki akses";
                }
                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
                    resulted = result,
                    flag = "",
                    idhome = "",
                    modl = caption,
                    url = urlpath,
                    idrst = IDresult,
                    golpod = IDUpload
                });
            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePloddocsvenw(HttpPostedFileBase files, string idx, string modepro, string tglpro, string noappl)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            string parcno = "";
            string keylokup = parcno;
            string Jenis_Doc = "";

            byte[] bytefl = bytefl = Encoding.ASCII.GetBytes("");
            string filename = "";
            string filetype = "";
            string validmsg = "";
            int resultsuct = 0;

            HTL = TempData[tempTransksi] as vmHTL;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);

            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = (caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();

                if (AllowUpload == true)
                {

                    if (modepro == "chk")
                    {

                        var xmlString = "";
                        XmlDocument xml = new XmlDocument();
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\Template\\TemplateDoc_Check.xml";
                        xml.Load(xmlString);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                        nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
                        XmlElement root = xml.DocumentElement;

                        DataTable dtlist = await HTLddl.dbdbGetDdlOrderGetCek("5", "", DateTime.Parse(tglpro).ToString("yyyyMMdd"), moduleID, UserID, GroupName);
                        XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

                        foreach (DataRow rw in dtlist.Rows)
                        {
                            XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlAttrRow.Value = "0";
                            XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlRecordNo.Attributes.Append(xmlAttrRow);
                            foreach (DataColumn col in dtlist.Columns)
                            {
                                XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                                XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                                string attrval = col.ColumnName.Split('_')[1].ToString();
                                xmlAttrNumber.Value = attrval;
                                XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                                xmlRecordNoceldata.InnerText = rw[col].ToString();
                                xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                                xmlRecordNo.AppendChild(xmlRecordNocel);
                                xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                            }
                            nodesing.AppendChild(xmlRecordNo);
                        }

                        filename = "HasilPengecekan_Dokumen_" + DateTime.Parse(tglpro).ToString("yyyyMMdd") + ".xml";
                        filetype = "application/xml";
                        bytefl = Encoding.ASCII.GetBytes(xml.OuterXml);
                    }
                    else if (modepro == "sbal")
                    {
                        DataTable dtx = await HTLddl.dbdbGetDdlOrderGetCek("9", noappl, "", moduleID, UserID, GroupName);
                        resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                        if (resultsuct > 0)
                        {
                            validmsg = "Isikan semua File Dokumen untuk jenis dokumen yang bertanda *(bintang)";
                            resultsuct = -999;
                        }
                        else if (resultsuct == -8909)
                        {
                            validmsg = "Dokumen untuk No Applikasi '" + noappl + "' sudah diupload oleh user lain";
                            resultsuct = -999;
                        }
                        else
                        {
                            dtx = await HTLddl.dbdbGetDdlOrderGetCek("6", noappl, "", moduleID, UserID, GroupName);
                            resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                            if (resultsuct != 1)
                            {
                                if (resultsuct == -90)
                                {
                                    validmsg = "Upload tidak diijinkan, Applikasi sedang dalam proses pengecekan";
                                }
                                else
                                {
                                    validmsg = "Upload dokumen gagal, silahkan coba beberapa saat lagi";
                                }
                            }
                            else
                            {
                                validmsg = "Upload dokumen berhasil";
                            }
                        }
                    }
                    else
                    {
                        validmsg = await Commonddl.dbValidFileupload(files, "UPLOADDOC", noappl, moduleID, UserID, GroupName);
                        if (validmsg == "")
                        {

                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(files.InputStream);
                            imagebyte = reader.ReadBytes((int)files.ContentLength);

                            //get mimne type
                            string mimeType = files.ContentType;
                            if (mimeType.Contains("image"))
                            {
                                imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                            }



                            var splitfilename = files.FileName.Split('_');
                            string NoSertifikat = splitfilename[0];
                            string NamaSertifikat = splitfilename[1];

                            //prepare to encrypt
                            string KECEP = NoSertifikat;
                            string KECEPDB = KECEP;
                            KECEP = HasKeyProtect.Encryption(KECEP);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);


                            Jenis_Doc = splitfilename[1].ToLower().Replace(".jpg", "").Replace(".jpeg", "").Replace(".JPG", "").Replace(".JPEG", "").Replace(".pdf", "").Replace(".PDF", "");
                            string DocumentType = Jenis_Doc.ToUpper();
                            string FileName = files.FileName;

                            string ContentType = "Application/pdf";
                            string ContentLength = filebyteECP.Length.ToString();
                            string FileByte = base64String;


                            DataTable dtx = await HTLddl.dbSaveRegMitradoc("0", "", "", "", noappl, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                            resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                            if (resultsuct != 1)
                            {
                                validmsg = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct);
                            }
                        }
                    }
                }
                else
                {
                    validmsg = "user tidak memiliki akses";
                }

                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    filmsg = validmsg,
                    filidx = int.Parse(idx) + 1,
                    resultsuc = resultsuct,
                    flbt = bytefl,
                    flbtnm = filename,
                    flbtmtype = filetype,
                    mode = modepro,
                });

            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePloddoconesvenw(HttpPostedFileBase[] files, string[] documen, string cntproo, string noappl, string keypro)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            string urlpath = "";
            if (IsErrorTimeout == true)
            {
                urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            string parcno = "";
            string keylokup = parcno;
            string EnumMessage = "";
            int result = 0;
            string viewtbl = "";

            HTL = TempData[tempTransksi] as vmHTL;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);


            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = (modFilter.idcaption ?? modFilter.ModuleID);
                noappl = HasKeyProtect.Decryption(noappl);
                string moduleID = caption != "UPLOADDOC" ? HasKeyProtect.Decryption(caption) : caption;
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();
                bool AllowSubmit = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowSubmit).SingleOrDefault();

                string UserType = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                if (UserType == "0")
                {
                    AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == "HTLLIST").Select(x => x.AllowUpload).SingleOrDefault();
                }
                string FlagOpr = "";
                string notransaksi = "";
                string RegType = "";
                string Noktp = "";
                string NIK = "";
                string ID = "";
                string IDUpload = "";
                string IDresult = "0";
                //cntproo = HasKeyProtect.Decryption(cntproo);
                //string[] cntproodata = cntproo.Split('|');
                //if (cntproo != "templod")
                //{
                //    FlagOpr = HasKeyProtect.Decryption(cntproodata[0]);
                //    //notransaksi = (cntproodata[1]);
                //    //RegType = (cntproodata[2]);
                //    //Noktp = (cntproodata[3]);
                //    //NIK = (cntproodata[4]);
                //    //ID = (cntproodata[5]);
                //}
                //DataRow dr = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdataHTX") == noappl).SingleOrDefault();
                //noappl = dr["NoAPPL"].ToString();
                string kode = "";
                bool asxit = false;
                if (cntproo.Contains("#81x@") || cntproo.Contains("#82x@") || cntproo.Contains("#83x@") || cntproo.Contains("#84x@"))
                {
                    asxit = true;
                    kode = cntproo.Substring(cntproo.Length - 5);
                    cntproo = cntproo.Replace(kode, "");
                }


                cntproo = HashNetFramework.HasKeyProtect.Decryption(cntproo) == "-9999" ? cntproo : HashNetFramework.HasKeyProtect.Decryption(cntproo);
                if (HTL.HeaderInfo == null)
                {
                    HTL.HeaderInfo = new cHTL();
                }

                if (noappl == HTL.HeaderInfo.keylookupdataHTX && cntproo == HTL.HeaderInfo.NoAppl)
                {
                    string[] doc = null;

                    if (kode == "#81x@")
                    {
                        string[] doc1 = { HashNetFramework.HasKeyProtect.Encryption("SERTIFIKAT PENGECEKAN") };
                        doc = doc1;
                    }
                    else if (kode == "#82x@")
                    {
                        string[] doc2 = { HashNetFramework.HasKeyProtect.Encryption("TTD") };
                        doc = doc2;
                    }
                    else if (kode == "#83x@")
                    {
                        string[] doc3 = { HashNetFramework.HasKeyProtect.Encryption("PPK") };
                        doc = doc3;
                    }
                    else if (kode == "#84x@")
                    {
                        string[] doc4 = { HashNetFramework.HasKeyProtect.Encryption("SPA") };
                        doc = doc4;
                    }

                    documen = doc;
                    noappl = HTL.HeaderInfo.NoAppl;

                    //if (asxit == true)
                    //{
                    //    string[] doca = { HashNetFramework.HasKeyProtect.Encryption("TTD") };
                    //    documen = doca;
                    //}
                }

                if (AllowUpload == true || (AllowSubmit == true))
                {

                    if (files != null)
                    {
                        foreach (HttpPostedFileBase file in files)
                        {

                            decimal sizep = file.ContentLength / 1000;
                            if (file.FileName.Length > 50)
                            {
                                EnumMessage = "Nama File " + file.FileName + " tidak boleh lebih dari 50 karakter";
                            }
                            else
                            if (!file.ContentType.ToLower().Contains("jpg") && !file.ContentType.ToLower().Contains("jpeg") && !file.ContentType.ToLower().Contains("pdf"))
                            {
                                EnumMessage = "File " + file.FileName + " harus Extention jpg,jpeg,pdf";
                            }
                            else
                            if (sizep > 1000 && cntproo.ToString().ToLower() != "sertifikat")
                            {
                                EnumMessage = "File " + file.FileName + " Ukuran File tidak boleh lebih dari 1 MB";
                            }
                            else
                            if (sizep > 5000 && cntproo.ToString().ToLower() == "sertifikat")
                            {
                                EnumMessage = "File " + file.FileName + " Ukuran File tidak boleh lebih dari 5 MB";
                            }

                            if ((noappl ?? "").Length != 14)
                            {
                                if ((noappl ?? "") == "")
                                {
                                    EnumMessage = "Silahkan isikan data aplikasi dan debitur terlebih dahulu, kemudian tekan tombol 'simpan'";
                                }
                                else
                                {
                                    EnumMessage = "No Applikasi harus 14 digit";
                                }
                            }
                        }

                        if (EnumMessage == "")
                        {
                            var idoc = 0;
                            foreach (HttpPostedFileBase file in files)
                            {

                                byte[] imagebyte = null;
                                BinaryReader reader = new BinaryReader(file.InputStream);
                                imagebyte = reader.ReadBytes((int)file.ContentLength);

                                //get mimne type
                                string mimeType = file.ContentType;
                                if (kode != "#82x@")
                                {
                                    if (mimeType.Contains("image"))
                                    {
                                        imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                                    }
                                }

                                //PdfReader pdfReader = new PdfReader(imagebyte);
                                //List<Phrase> PhraseList = new List<Phrase>();
                                //List<int> pages = new List<int>();
                                //for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                                //{
                                string NoAPHT = "";
                                string KodeAkta = "";

                                //ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                                //string currentPageText = PdfTextExtractor.GetTextFromPage(pdfReader, 1, strategy);
                                //string[] words = currentPageText.Split('\n');
                                //foreach (var x in words)
                                //{
                                //    if (x.ToLower().Contains("tanggungan nomor"))
                                //    {
                                //        int strt = x.ToLower().IndexOf("tanggungan nomor") + ("tanggungan nomor").Length;
                                //        int end = x.ToLower().IndexOf("yang dibuat");
                                //        NoAPHT = x.Substring(strt, end - strt).Trim();
                                //    }

                                //    if (x.ToLower().Contains("kode akta"))
                                //    {
                                //        int strt1 = x.ToLower().IndexOf("kode akta") + ("kode akta").Length;
                                //        int end1 = x.ToLower().IndexOf("dan nilai");
                                //        KodeAkta = x.Substring(strt1, end1 - strt1).Trim();
                                //    }

                                //}
                                //if (currentPageText.Contains(searchText))
                                //{
                                //    pages.Add(page);
                                //    textBox1.AppendText(PdfTextExtractor.GetTextFromPage(pdfReader, page));
                                //    textBox1.Text += pages.ToString();
                                //}
                                //}
                                //pdfReader.Close();


                                //prepare to encrypt
                                string KECEP = noappl;
                                string KECEPDB = KECEP;
                                KECEP = HasKeyProtect.Encryption(KECEP);
                                byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                                //convert byte to base//
                                string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                                string DocumentType = HasKeyProtect.Decryption(documen[idoc]);
                                string FileName = "";

                                if ((HTL.HeaderInfo.NamaPemilikSertifikat ?? "") == "")
                                {
                                    FileName = DocumentType.ToUpper() + ".pdf";
                                }
                                else
                                {
                                    FileName = DocumentType.ToUpper() + "_" + (HTL.HeaderInfo.NoAppl ?? "") + "_" + (HTL.HeaderInfo.NamaPemilikSertifikat ?? "") + ".pdf";
                                }

                                string ContentType = "Application/pdf";
                                string ContentLength = filebyteECP.Length.ToString();
                                string FileByte = base64String;

                                DataTable dtx = new DataTable();
                                //if (cntproo != "templod")
                                //{
                                keypro = (keypro ?? "") == "" ? "0" : HasKeyProtect.Decryption(keypro);
                                string nik = "";
                                if (DocumentType == "SURAT PENGANTAR AKTA")
                                {
                                    Noktp = NoAPHT;
                                    nik = KodeAkta;
                                }
                                dtx = await HTLddl.dbSaveRegMitradoc(keypro, FlagOpr, nik, Noktp, noappl, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                                //}
                                //else
                                //{
                                //    dtx = await HTLddl.dbSaveDocTemp("0", "", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                                //}

                                idoc = idoc + int.Parse(dtx.Rows[0][0].ToString());
                                IDresult = dtx.Rows[0][0].ToString();
                                IDUpload = HasKeyProtect.Encryption(dtx.Rows[0][1].ToString());
                                // disini ditambahkan jika gagal menyimpan do
                            }

                            if (files.Count() != int.Parse(idoc.ToString()))
                            {
                                EnumMessage = "Terdapat Dokumen yang tidak terupload silahkan dicek kembali";
                            }

                            result = int.Parse(IDresult);

                            if (result == -8909)
                            {
                                EnumMessage = "Dokumen untuk No Applikasi '" + noappl + "' sudah diupload oleh user lain";
                            }
                            if (result == 1)
                            {
                                EnumMessage = "Upload berhasil";
                            }
                        }
                    }
                    else
                    {
                        EnumMessage = "Pilih file yang akan diupload";
                    }
                }
                else
                {
                    EnumMessage = "User tidak memiliki akses";
                }
                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
                    resulted = result,
                    flag = "",
                    idhome = "",
                    modl = caption,
                    url = urlpath,
                    idrst = IDresult,
                    golpod = IDUpload,
                });
            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);

            }
        }


        #endregion upload Dokumen


        public async Task<ActionResult> clnCheckDocmandornk(string paridno, string stated)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "HTLLIST";
                string moduleID = HasKeyProtect.Decryption(caption);

                HTL = TempData[tempTransksi] as vmHTL;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData["common"] as vmCommon);

                stated = stated ?? "";
                stated = stated.All(char.IsNumber) ? stated : HasKeyProtect.Decryption(stated);
                paridno = HasKeyProtect.Decryption(paridno);
                DataTable dt = await HTLddl.dbGetMultiData4NIK(paridno, stated, caption, UserID, GroupName);
                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);

                ViewBag.captiondesc = "Serach NIK";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    dtn = JSONresult,
                    view = ""
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> clnCheckProvchange(string paridno, string stated)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            try
            {

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "HTLLIST";
                string moduleID = HasKeyProtect.Decryption(caption);

                HTL = TempData[tempTransksi] as vmHTL;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData["common"] as vmCommon);

                Common.ddlLokprov = await Commonddl.dbdbGetDdlEnumsListByEncrypt("LOKTANAHKOTA", caption, UserID, GroupName, stated);
                ViewData["SelectLokasiTanahProv"] = new MultiSelectList(Common.ddlLokprov, "Value", "Text");


                modFilter.idcaption = caption;
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                string JSONresult = "";
                JSONresult = JsonConvert.SerializeObject(Common.ddlLokprov);

                ViewBag.captiondesc = "";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    dtn = JSONresult,
                    view = ""
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public async Task<ActionResult> clnchkfle(string secnocon, string coontpe, string clnfdc)
        {

            string EnumMessage = "";
            string filenamevar = "View Dokumen";
            byte[] res = null;
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {

                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                //sumber data pemberkasan //

                string caption = modFilter.idcaption;
                string moduleID = HasKeyProtect.Decryption(caption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string mode = moduleID;

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowDownload).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                if (HTL.CheckWithKey == "loadfile")
                {
                    TempData["dtfile"] = null;
                    HTL.CheckWithKey = "";
                }
                //cek pada sesion terlebih dulu
                List<cDocumentsGet> dttemp = (TempData["dtfile"] as List<cDocumentsGet>);

                string infokon = "";
                string infofisrst = "";


                if (clnfdc == "ckalleddxtx" || clnfdc == "ckalleddxtxp") /* upload pl pada grid yang sudah ttd*/
                {
                    secnocon = "MNL_" + HashNetFramework.HasKeyProtect.Decryption(secnocon).Replace("/", "").Replace("-", "");
                }
                else
                {
                    secnocon = HashNetFramework.HasKeyProtect.Decryption(secnocon);
                }


                int idicategetdb = 1;
                string ID = "0";
                string publicontype = coontpe;
                if ((dttemp == null))
                {
                    idicategetdb = 0;
                    if (coontpe == "chk")
                    {
                        ID = secnocon;
                        secnocon = "";
                        clnfdc = "";
                        coontpe = HasKeyProtect.Encryption("0");
                    }
                }
                else
                {
                    int dttempx = 0;
                    if (coontpe == "chk")
                    {
                        dttempx = dttemp.AsEnumerable().Where(x => x.ID_UPLOAD == secnocon).Count();
                        if (dttempx == 0)
                        {
                            idicategetdb = 0;
                            ID = secnocon;
                            secnocon = "";
                            clnfdc = "";
                            coontpe = HasKeyProtect.Encryption("0");
                        }
                    }
                    else
                    {
                        dttempx = dttemp.AsEnumerable().Where(x => x.ID_UPLOAD == secnocon).Count();
                        if (dttempx == 0)
                        {
                            idicategetdb = 0;
                        }
                    }
                }
                if (idicategetdb == 0)
                {

                    if (secnocon == "chkall")
                    {
                        secnocon = "-99";
                    }

                    //load ke DB jika disesion tidak ada//
                    modFilter.ID = ID;
                    modFilter.NoPerjanjian = secnocon;
                    modFilter.SelectJenisKontrak = HasKeyProtect.Decryption(coontpe);
                    modFilter.secIDFDC = clnfdc;
                    string tipe = coontpe == "pretemp" ? "5" : "2";
                    if (clnfdc == "ckalleddxtx" || clnfdc == "ckalleddxtxp")
                    {
                        dttemp = await Commonddl.Getdocview(0, secnocon.ToString(), "21", moduleID, UserID, GroupName);
                    }
                    else
                    {
                        dttemp = await Commonddl.Getdocview(int.Parse(secnocon.ToString()), "", tipe, moduleID, UserID, GroupName);
                    }
                    modFilter.NoPerjanjian = "";
                    modFilter.SelectJenisKontrak = "";
                    modFilter.secIDFDC = null;
                    modFilter.chalowses = false;
                }
                else
                {
                    infofisrst = "1";
                }

                //simpand di session//
                TempData["dtfile"] = dttemp;
                string viewhml = "";
                string passwd = "";
                if (dttemp.Count > 0)
                {
                    filenamevar = dttemp[0].ExternalName;
                    passwd = dttemp[0].password;
                    if (publicontype == "chk")
                    {
                        foreach (cDocumentsGet s in dttemp)
                        {
                            infokon = dttemp[0].cont_no.ToString();
                            byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                            string pooo = s.cont_no;
                            //pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                            string KECEP = HasKeyProtect.Encryption(pooo);
                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                            res = imageBytes;
                        }
                    }

                    if (infofisrst == "" && publicontype != "chk")
                    {
                        //iTextSharp.text.pdf.PdfReader finalPdf;
                        //iTextSharp.text.Document pdfContainer;
                        //PdfWriter pdfCopy;
                        //MemoryStream msFinalPdf = new MemoryStream();

                        //pdfContainer = new iTextSharp.text.Document();
                        //pdfCopy = new PdfSmartCopy(pdfContainer, msFinalPdf);
                        //pdfContainer.Open();

                        //int pageded = 0;
                        foreach (cDocumentsGet s in dttemp)
                        {
                            //pageded = pageded + 1;
                            byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                            string pooo = s.cont_no;
                            //pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                            string KECEP = HasKeyProtect.Encryption(pooo);
                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                            //byte[] pass = Encoding.ASCII.GetBytes(passwd);
                            res = imageBytes;
                        }


                        //    finalPdf = new iTextSharp.text.pdf.PdfReader(imageBytes, pass);
                        //    for (int i = 1; i < finalPdf.NumberOfPages + 1; i++)
                        //    {
                        //        ((PdfSmartCopy)pdfCopy).AddPage(pdfCopy.GetImportedPage(finalPdf, i));
                        //    }
                        //    pdfCopy.FreeReader(finalPdf);

                        //    if (pageded == dttemp.Count)
                        //    {
                        //        finalPdf.Close();
                        //    }
                        //}
                        //pdfCopy.Close();
                        //pdfContainer.Close();
                        //res = msFinalPdf.ToArray();
                        /*
                        MemoryStream msFinalPdfecp = new MemoryStream();
                        finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                        string password = passwd;
                        PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                        res = msFinalPdfecp.ToArray();
                        */
                    }
                }
                else
                {
                    EnumMessage = "Dokumen tidak ditemukan";
                }

                string nokonview = "";
                try
                {
                    nokonview = "Dokumen "; //+ dttemp[0].Document_Type.ToString()
                }
                catch
                {

                }

                if (secnocon == "chkall" || secnocon == "-99")
                {
                    EnumMessage = "";
                    nokonview = "Pengecekan Dokumen";
                    string pathfile = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\checkfile.pdf";
                    res = System.IO.File.ReadAllBytes(pathfile);
                }

                //download di password//
                if (clnfdc == "ckalledd" || clnfdc == "ckalleddx" || clnfdc == "ckalleddxtx" || clnfdc == "ckalleddxtxp")
                {
                    clnfdc = "1";
                    if (AllowGenerate == false)
                    {
                        EnumMessage = "User Tidak memiliki akses";
                    }
                    else
                    {
                        iTextSharp.text.pdf.PdfReader finalPdf;
                        MemoryStream msFinalPdfecp = new MemoryStream();
                        finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                        string password = passwd;
                        PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                        res = msFinalPdfecp.ToArray();
                    }
                }

                var contenttypeed = "application/pdf";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?paridfdc=" + secnocon + "&parmod=" + mode + "&parpowderdockd=wako&infokon=" + infokon + "&file=";
                //viewpathed = "data:application/pdf;base64," + base4;
                var jsonresult = Json(new { view = viewhml, moderror = IsErrorTimeout, dwn = clnfdc, infoselect = infofisrst, bytetyipe = res, msg = EnumMessage, cap = nokonview, contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
                jsonresult.MaxJsonLength = int.MaxValue;
                return jsonresult;

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnResend(string[] AktaSelectdwn, string prevedid, string namaidpool, string reooo)
        {

            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }


            try
            {

                //get filter data from session before//
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //set back filter data from session before//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;

                // get user group & akses //
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HashNetFramework.HasKeyProtect.Decryption(modFilter.idcaption);

                //extend//
                cAccountMetrik Metrik = Account.AccountMetrikList.Where(x => x.SecModuleID == modFilter.idcaption).SingleOrDefault();
                //bool AllowPrint = Metrik.AllowPrint;
                bool AllowDownload = Metrik.AllowDownload;

                //deript for db//
                string ClientID = HasKeyProtect.Decryption(modFilter.ClientLogin);
                string IDNotaris = HasKeyProtect.Decryption(modFilter.NotarisLogin);
                string IDCabang = HasKeyProtect.Decryption(modFilter.BranchLogin);
                string SecureModuleId = HasKeyProtect.Decryption(modFilter.idcaption);
                string Email = HasKeyProtect.Decryption(modFilter.MailerDaemoon);
                string UserGenCode = HasKeyProtect.Decryption(modFilter.GenDeamoon);



                ////set login key//
                string LoginAksesKey = UserID + Email + UserGenCode;

                DataTable dataupload = new DataTable();
                dataupload.Columns.Add("NOSERTIFIKAT", Type.GetType("System.String"));
                dataupload.Columns.Add("NAMA", Type.GetType("System.String"));
                dataupload.Columns.Add("NoAPPL", Type.GetType("System.String"));
                dataupload.Columns.Add("DOKUMENTYPE", Type.GetType("System.String"));

                List<string> ListIDgrd = new List<string>();
                var ij = 0;
                string keylookup = "";

                dbAccessHelper dbaccess = new dbAccessHelper();

                //looping unutk  nokontrak yang dipilih , dininputkan ke table temp//
                foreach (var aktasel in AktaSelectdwn)
                {
                    string[] valued = aktasel.Split('|');

                    keylookup = valued[0].ToString();
                    ListIDgrd.Add(keylookup);

                    DataRow resultquery;
                    ij = ij + 1;

                    resultquery = HTL.DTHeaderTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).SingleOrDefault();

                    if (resultquery != null)
                    {
                        //untuk sertifikat hanya boleh download yang status penagihan invoice
                        if (prevedid == "brk")
                        {
                            //if (int.Parse(resultquery["Status"].ToString()) == 10 || int.Parse(resultquery["Status"].ToString()) == 20 || int.Parse(resultquery["Status"].ToString()) == 21
                            //    || int.Parse(resultquery["Status"].ToString()) == 6)
                            {
                                dataupload.Rows.Add(new object[] { resultquery["NoSertifikat"], resultquery["NamaPemilikSertifikat"], resultquery["NoAppl"] });
                            }
                        }
                        else
                        {
                            //untuk sertifikat hanya boleh download yang status penagihan invoice
                            //if (int.Parse(resultquery["Status"].ToString()) == 30 || int.Parse(resultquery["Status"].ToString()) == 29)
                            {
                                dataupload.Rows.Add(new object[] { resultquery["NoSertifikat"], resultquery["NamaPemilikSertifikat"], resultquery["NoAppl"] });
                            }
                        }
                    }
                }

                DataTable DocumentByte = new DataTable();
                //var dbx = new DropboxClient(keybox);

                const int bufferSize = 104857600;
                var buffer = new byte[bufferSize];

                string statused = "";
                //var powderdockp = AllowPrint == true ? "1" : "0";
                var powderdockd = AllowDownload == true ? "1" : "0"; // untuk handele downloa pada pdfvier//

                if ((prevedid ?? "") == "" || (prevedid ?? "") == "brk")
                {
                    ZipEntry newZipEntry = new ZipEntry();
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var zip = new ZipFile())
                        {
                            zip.Password = UserID.ToLower();
                            //HasKeyProtect.Encryption(LoginAksesKey);
                            // string folderequest = "";
                            foreach (DataRow rowsdata in dataupload.Rows)
                            {
                                //folderequest = rowsdata["REQUEST_NO"].ToString();
                                string pathdwn = "";
                                string tipe = "";
                                if (prevedid == "brk")
                                {
                                    tipe = "2";
                                    pathdwn = "Berkas HT/" + rowsdata["NoAppl"] + " " + rowsdata["NAMA"] + "/";
                                }
                                else
                                {
                                    tipe = "1";
                                    pathdwn = "Sertifikat HT/";
                                }
                                try
                                {
                                    string pathsbe = "";
                                    List<cDocumentsGet> litdoc = await Commonddl.Getdocview(0, rowsdata["NoAppl"].ToString(), tipe, caption, UserID, GroupName);
                                    foreach (cDocumentsGet s in litdoc)
                                    {
                                        byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                                        string pooo = rowsdata["NoAppl"].ToString();
                                        //pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                                        string KECEP = HasKeyProtect.Encryption(pooo);
                                        imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                                        if (prevedid == "brk")
                                        {
                                            pathsbe = pathdwn + s.FILE_NAME;
                                        }
                                        else
                                        {
                                            pathsbe = s.FILE_NAME;
                                        }
                                        zip.AddEntry(pathsbe, imageBytes);
                                    }
                                }
                                catch
                                {
                                    statused = "not found";
                                }
                            }
                            zip.Save(memoryStream);
                        }
                        buffer = memoryStream.ToArray();
                    }
                }

                string minut = DateTime.Now.ToString("ddMMyyyymmss");
                string filenamepar = (prevedid == "brk" ? "BERKAS DOKUMEN HT_" + minut + ".zip" : "SERTIFIKAT PENGECEKAN HT_" + minut + ".zip");
                return File(buffer, "application/zip", filenamepar);
            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public static byte[] ReadAllBytes(Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<ActionResult> clnchkfledl(string secnocon, string geolo)
        {

            string EnumMessage = "";
            byte[] res = null;
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {

                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                //sumber data pemberkasan //

                string caption = modFilter.idcaption;
                string moduleID = caption;// HasKeyProtect.Decryption(caption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string mode = moduleID;

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowGenerate).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                secnocon = HashNetFramework.HasKeyProtect.Decryption(secnocon);
                DataTable dtx;
                if (geolo == "pretemp")
                {
                    dtx = await HTLddl.dbSaveDocTemp(secnocon, "88", "", "", "", "0", "", moduleID, UserID, GroupName);
                }
                else
                {
                    dtx = await HTLddl.dbSaveRegMitradoc(secnocon, "", "", "", "", "88", "", "", "", "0", "", moduleID, UserID, GroupName);
                }

                int resultsuct = int.Parse(dtx.Rows[0][0].ToString());

                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct);
                if (resultsuct == 1)
                {
                    EnumMessage = string.Format(EnumMessage, "Dokumen ", "dihapus");
                }
                var jsonresult = Json(new { view = "", moderror = IsErrorTimeout, msg = EnumMessage, rst = resultsuct, JsonRequestBehavior.AllowGet });
                jsonresult.MaxJsonLength = int.MaxValue;
                return jsonresult;

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnResendSHT(string[] AktaSelectdwn, string prevedid, string namaidpool, string reooo)
        {

            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }


            try
            {

                //get filter data from session before//
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //set back filter data from session before//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;

                // get user group & akses //
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HashNetFramework.HasKeyProtect.Decryption(modFilter.idcaption);
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                bool AllowDownload = false;
                if (!prevedid.Contains("=="))
                {
                    cAccountMetrik Metrik = Account.AccountMetrikList.Where(x => x.SecModuleID == modFilter.idcaption).SingleOrDefault();
                    AllowDownload = Metrik.AllowDownload;
                }



                //bool AllowPrint = Metrik.AllowPrint;

                //deript for db//
                string ClientID = HasKeyProtect.Decryption(modFilter.ClientLogin);
                string IDNotaris = HasKeyProtect.Decryption(modFilter.NotarisLogin);
                string IDCabang = HasKeyProtect.Decryption(modFilter.BranchLogin);
                string SecureModuleId = HasKeyProtect.Decryption(modFilter.idcaption);
                string Email = HasKeyProtect.Decryption(modFilter.MailerDaemoon);
                string UserGenCode = HasKeyProtect.Decryption(modFilter.GenDeamoon);



                ////set login key//
                string LoginAksesKey = UserID + Email + UserGenCode;
                string code = HasKeyProtect.Decryption(prevedid);
                DataTable dataupload = new DataTable();
                dataupload.Columns.Add("Key1", Type.GetType("System.String"));
                dataupload.Columns.Add("Key2", Type.GetType("System.String"));
                dataupload.Columns.Add("Key3", Type.GetType("System.String"));
                dataupload.Columns.Add("Key4", Type.GetType("System.String"));

                List<string> ListIDgrd = new List<string>();
                var ij = 0;
                string keylookup = "";
                bool isSKMHT = false;
                bool isAPHT = false;
                string NamaCabang = "";
                bool ismulticab = false;

                dbAccessHelper dbaccess = new dbAccessHelper();
                List<string> dtreport = new List<string>();
                dtreport.Add("No;No Perjanjian;Cabang;Debitur;No SHM;Nilai Pinjaman;Keterangan");

                if (reooo.Contains("=="))
                {
                    reooo = HasKeyProtect.Decryption(reooo);
                }

                if (AktaSelectdwn != null)
                {
                    //looping unutk  nokontrak yang dipilih , dininputkan ke table temp//
                    foreach (var aktasel in AktaSelectdwn)
                    {
                        string[] valued = aktasel.Split('|');

                        keylookup = valued[0].ToString();
                        ListIDgrd.Add(keylookup);
                        DataRow resultquery;

                        if (code != "" && code != "-9999" && reooo != "cretbast" && reooo != "cfrmbast" && reooo != "slabast" && reooo != "rolbast")
                        {
                            resultquery = HTL.DTHeaderTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).SingleOrDefault();
                        }
                        else
                        {
                            resultquery = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).SingleOrDefault();
                        }

                        if (resultquery != null)
                        {
                            if (resultquery["Keterangan"].ToString() == "SKMHT")
                            {
                                isSKMHT = true;
                            }
                            if (resultquery["Keterangan"].ToString() == "APHT")
                            {
                                isAPHT = true;
                            }
                            var nmcab = resultquery["NamaCabang"].ToString();
                            if (!NamaCabang.Contains(nmcab))
                            {
                                NamaCabang = NamaCabang + "|" + nmcab;
                            }

                            dataupload.Rows.Add(new object[] { resultquery["NoAppl"], resultquery["ID"], resultquery["Debitur"], resultquery["NoSHM"] });
                            ij = ij + 1;
                            dtreport.Add(ij.ToString() + ";" +
                                    resultquery["NoPerjanjian"] + ";" +
                                    resultquery["NamaCabang"] + ";" +
                                    resultquery["Debitur"] + ";" +
                                    resultquery["NoSHM"] + ";" +
                                    resultquery["PinjamanKonsumen"] + ";" +
                                    resultquery["Keterangan"]);
                        }
                    }
                }


                const int bufferSize = 104857600;
                var buffer = new byte[bufferSize];
                string minut = "";
                string filenamepar = "";
                string statused = "";
                string filtpe = "";
                string resulted = "";
                string nobast = "";

                if (reooo == "rekapulang")
                {
                    foreach (DataRow resultquery in HTL.DTAllTx.Rows)
                    {
                        resulted = "1";
                        nobast = resultquery["NoBAST"].ToString();

                        if (resultquery["Keterangan"].ToString() == "SKMHT")
                        {
                            isSKMHT = true;
                        }
                        if (resultquery["Keterangan"].ToString() == "APHT")
                        {
                            isAPHT = true;
                        }
                        var nmcab = resultquery["NamaCabang"].ToString();
                        if (!NamaCabang.Contains(nmcab))
                        {
                            NamaCabang = NamaCabang + "|" + nmcab;
                        }

                        dataupload.Rows.Add(new object[] { resultquery["NoAppl"], resultquery["ID"], resultquery["Debitur"], resultquery["NoSHM"] });
                        ij = ij + 1;
                        dtreport.Add(ij.ToString() + ";" +
                                resultquery["NoPerjanjian"] + ";" +
                                resultquery["NamaCabang"] + ";" +
                                resultquery["Debitur"] + ";" +
                                resultquery["NoSHM"] + ";" +
                                resultquery["PinjamanKonsumen"] + ";" +
                                resultquery["Keterangan"]);
                    }
                }
                DataTable DocumentByte = new DataTable();
                //var dbx = new DropboxClient(keybox);


                if (code == "trmppat" || reooo == "cretbast" || reooo == "rekapulang")
                {
                    string sourceFile = Server.MapPath(Request.ApplicationPath) + "External\\TemplateINV\\BUKTITERIMA.docx";
                    using (MemoryStream pdfDocumentstream = new MemoryStream())
                    {

                        Spire.Doc.Document doc = new Spire.Doc.Document();
                        doc.LoadFromFile(sourceFile);
                        /* update */

                        string colvalue = "";
                        foreach (DataColumn col in HTL.DTAllTx.Columns)
                        {
                            string colname = col.ColumnName;
                            colvalue = HTL.DTAllTx.Rows[0][colname].ToString();
                            if ((reooo != "cretbast"))
                            {
                                doc.Replace("{" + colname + "}", colvalue, false, true);
                            }
                            else if ((colname != "NoBAST" && colname != "TglBAST") && (reooo == "cretbast"))
                            {
                                doc.Replace("{" + colname + "}", colvalue, false, true);
                            }
                        }

                        filenamepar = "BAST GAGAL ULANGI LAGI.pdf";
                        List<string> hsilcabangspilt = new List<string>();
                        if (NamaCabang != "")
                        {
                            hsilcabangspilt = NamaCabang.Substring(1, NamaCabang.Length - 1).Split('|').ToList();
                            if (hsilcabangspilt.Count > 1)
                            {
                                filenamepar = "BAST GAGAL - SATU BAST HARUS SATU CABANG.pdf";
                            }
                        }
                        else
                        {
                            filenamepar = "BAST GAGAL - NAMA CABANG KOSONG.pdf";
                        }

                        string namacap = "";
                        if (isSKMHT == true && isAPHT == true)
                        {
                            namacap = "SKMHT,APHT,SHM DAN SERTIFIKAT HT";
                        }
                        else if (isSKMHT == true && isAPHT == false)
                        {
                            namacap = "SKMHT,SHM DAN SERTIFIKAT HT";
                        }
                        else if (isSKMHT == false && isAPHT == true)
                        {
                            namacap = "SKMHT,APHT,SHM DAN SERTIFIKAT HT";
                        }

                        if (reooo == "cretbast" && hsilcabangspilt.Count == 1)
                        {
                            DataTable dtresultod = await HTLddl.dbupdateBAST("1", dataupload, null, "", "", caption, UserID, GroupName);
                            resulted = dtresultod.Rows[0][0].ToString();
                            nobast = dtresultod.Rows[0][1].ToString();
                            string tglbast = dtresultod.Rows[0][2].ToString();
                            doc.Replace("{NoBAST}", nobast, false, true);
                            doc.Replace("{TglBAST}", tglbast, false, true);
                        }
                        doc.Replace("{NamaPenyerahan}", namacap, false, true);
                        doc.SaveToStream(pdfDocumentstream, Spire.Doc.FileFormat.PDF);

                        Spire.Pdf.PdfDocument docpdf = new Spire.Pdf.PdfDocument();
                        docpdf.LoadFromStream(pdfDocumentstream);
                        PdfSection sec = docpdf.Sections.Add();
                        sec.PageSettings.Width = PdfPageSize.A4.Width;
                        PdfPageBase page = sec.Pages.Add();

                        float y = 10;
                        PdfBrush brush1 = PdfBrushes.Black;
                        PdfTrueTypeFont font1 = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10f, FontStyle.Bold));
                        PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
                        page.Canvas.DrawString("LAMPIRAN  ", font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);
                        page.Canvas.DrawString(namacap, font1, brush1, page.Canvas.ClientSize.Width / 2, y + 15, format1);

                        y = y + font1.MeasureString("Country List", format1).Height;
                        y = y + 25;


                        //DataTable resultdtpdf = HTL.DTHeaderTx.DefaultView.ToTable(false, new string[]
                        //{ "No", "NoPerjanjian","Debitur", "NoSHM","PemilikSHM","NoAkta","KodeSHT","NoSHT","NilaiHT","PinjamanKonsumen" });

                        //string dataheader = "No;No Aplikasi;Nama Debitur;NoSertifikat;WilayahHak;Nilai HT";
                        string[] dataheader = { };

                        int Lc = 1;

                        string[] data = dtreport.ToArray();
                        String[][] dataSource = new String[data.Length][];
                        for (int i = 0; i < data.Length; i++)
                        {
                            dataSource[i] = data[i].Split(';');
                        }

                        PdfTable table = new PdfTable();
                        table.Style.CellPadding = 2;
                        table.Style.BorderPen = new PdfPen(brush1, 0.75f);
                        table.Style.HeaderStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                        table.Style.HeaderSource = PdfHeaderSource.Rows;
                        table.Style.HeaderRowCount = 1;
                        table.Style.ShowHeader = true;
                        table.Style.HeaderStyle.BackgroundBrush = PdfBrushes.CadetBlue;
                        table.DataSource = dataSource;

                        string[] listheader = dtreport[0].Split(';');
                        for (int i = 0; i < listheader.Length; i++)
                        {
                            table.Columns[i].ColumnName = listheader[i];
                            if (listheader[i] == "No")
                            {
                                table.Columns[i].Width = 5;
                                table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                            }
                            if (listheader[i] == "No Perjanjian")
                            {
                                table.Columns[i].Width = 15;
                                table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                            }
                            if (listheader[i] == "Debitur")
                            {
                                table.Columns[i].Width = 20;
                            }
                            if (listheader[i] == "NamaCabang")
                            {
                                table.Columns[i].Width = 20;
                            }
                            if (listheader[i] == "Nilai Pinjaman" || listheader[i] == "Nilai HT")
                            {
                                table.Columns[i].Width = 12;
                                table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                            }

                        }

                        PdfLayoutResult result = table.Draw(page, new PointF(0, y));
                        y = result.Bounds.Bottom + 5;

                        SizeF pageSized = result.Page.Size;
                        float tinggi = pageSized.Height;
                        docpdf.SaveToStream(pdfDocumentstream, FileFormat.PDF);

                        //footer//
                        Spire.Pdf.PdfDocument docfooter = new Spire.Pdf.PdfDocument();
                        docfooter.LoadFromStream(pdfDocumentstream);
                        SizeF pageSize = docfooter.Pages[0].Size;
                        float x = 90;
                        y = pageSize.Height - 40;
                        for (int i = 0; i < docfooter.Pages.Count; i++)
                        {
                            //draw line at bottom

                            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8f));
                            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
                            String footerText = "Catatan : Mohon untuk dicap oleh ppat dan cabang FIF pada setiap lembar bukti serah terima";
                            docfooter.Pages[i].Canvas.DrawString(footerText, font, PdfBrushes.Gray, x, y - 10, format);

                            PdfPen pen = new PdfPen(PdfBrushes.Gray, 0.5f);
                            docfooter.Pages[i].Canvas.DrawLine(pen, x, y, pageSize.Width - x, y);

                            //draw text at bottom
                            font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8f));
                            format = new PdfStringFormat(PdfTextAlignment.Left);
                            footerText = "Powered by PT. Sedayu Dana Banda @ " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            docfooter.Pages[i].Canvas.DrawString(footerText, font, PdfBrushes.Gray, x, y + 2, format);

                            //draw page number and page count at bottom
                            PdfPageNumberField number = new PdfPageNumberField();
                            PdfPageCountField count = new PdfPageCountField();
                            PdfCompositeField compositeField = new PdfCompositeField(font, PdfBrushes.Gray, "Page {0} of {1}", number, count);
                            compositeField.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Top);
                            SizeF size = font.MeasureString(compositeField.Text);
                            compositeField.Bounds = new RectangleF(pageSize.Width - (130), y + 2, size.Width, size.Height);
                            compositeField.Draw(docfooter.Pages[i].Canvas);

                        }

                        docfooter.SaveToStream(pdfDocumentstream);
                        filtpe = "application/pdf";
                        minut = DateTime.Now.ToString("ddMMyyyymmss");



                        if (resulted == "1")
                        {
                            filenamepar = "BUKTI SERAH TERIMA NO (" + nobast + ").pdf";
                            buffer = pdfDocumentstream.ToArray();
                        }
                        return File(buffer, filtpe, filenamepar);

                        //string viewpathed = "";
                        //if (resulted == "1")
                        //{
                        //    viewpathed = Convert.ToBase64String(buffer, 0, buffer.Length);
                        //}

                        //return Json(new
                        //{
                        //    moderror = IsErrorTimeout,
                        //    msg = nobast,
                        //    result = resulted,
                        //    bytetyipe = viewpathed,
                        //    typo = "",
                        //    invo = nobast,
                        //    jnfle = "",
                        //    namatitle = "BUKTI SERAH TERIMA_" + "(No." + nobast + ")",
                        //    view = "",
                        //});

                    }
                }
                else if (code == "trmcab")
                {

                    int result = await Commonddl.dbupdateflagsht("1", dataupload, caption, UserID, GroupName);
                    string errmessage = "";
                    errmessage = result.ToString() + " data Berhasil diproses, silahkan dicek kembali";
                    return Json(new
                    {
                        msg = errmessage,
                        moderror = IsErrorTimeout
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (reooo == "slabast" || reooo == "rolbast")
                {
                    DataTable dtrepoter = new DataTable();
                    string stat = "3";
                    string namafle = "Data_PembaharuanDeadline_BAST";
                    if (reooo == "rolbast")
                    {
                        stat = "4";
                        namafle = "Data_Rollback_BAST";
                    }

                    int result = await Commonddl.dbupdateflagsht(stat, dataupload, caption, UserID, GroupName);

                    if (result > 0)
                    {
                        var resultqueryer = from data in dataupload.AsEnumerable()
                                            select new
                                            {
                                                NoAplikasi = data.Field<string>("Key1"),
                                                Debitur = data.Field<string>("Key3"),
                                                NoSHM = data.Field<string>("Key4"),
                                            };

                        dtrepoter = resultqueryer.ToArray().ToDataTable();
                        dtrepoter.TableName = "DataReport";

                        using (MemoryStream pdfDocumentstream = new MemoryStream())
                        {
                            dtrepoter.WriteXml(pdfDocumentstream);
                            buffer = pdfDocumentstream.ToArray();
                        }
                        minut = DateTime.Now.ToString("ddMMyyyymmss");
                        filenamepar = namafle + minut + ".xml";
                        filtpe = "application/xml";
                    }
                    return File(buffer, filtpe, filenamepar);
                }
                else if (code == "excel")
                {
                    DataTable dtrepoter = new DataTable();
                    string namarpt = "";
                    if (reooo == "HTLLISTHT1")
                    {
                        namarpt = "(PENGAJUAN DITANGGUHKAN)";
                    }
                    else if (reooo == "HTLLISTHT2")
                    {
                        namarpt = "PENGAJUAN DITUTUP";
                    }
                    else if (reooo == "HTLLISTHT3")
                    {
                        namarpt = "MENUNGGU HASIL BPN";
                    }
                    else if (reooo == "HTLLISTHT4")
                    {
                        namarpt = "TERBIT HT";
                    }
                    else if (reooo == "HTLLISTHT5")
                    {
                        namarpt = "PENGAJUAN BARU";
                    }
                    else if (reooo == "HTLLISTHT")
                    {
                        namarpt = "DATAALL"; //all
                    }

                    if (reooo == "HTLLISTHT5")
                    {

                        var resultqueryer = from data in HTL.DTAllTx.AsEnumerable()
                                            select new
                                            {
                                                NamaPPAT = data.Field<string>("NamaPPAT"),
                                                NoAplikasi = data.Field<string>("NoAppl"),
                                                NoPerjanjian = data.Field<string>("NoPerjanjian"),
                                                Debitur = data.Field<string>("Debitur"),
                                                NoSHM = data.Field<string>("NoSHM"),
                                                KodeAkta = data.Field<string>("KodeAkta"),
                                                NoAkta = data.Field<string>("NoAkta"),
                                                TglSPA = data.Field<string>("TglSPA"),
                                                Keterangan = data.Field<string>("Keterangan"),
                                                NoBerkasSHT = data.Field<string>("NoBerkasSHT"),
                                                PICSDB = data.Field<string>("PICSDB_String"),
                                                DueDateSLA = data.Field<Int32>("DueDateSLA_String"),
                                            };

                        dtrepoter = resultqueryer.ToArray().ToDataTable();
                        dtrepoter.TableName = "DataReport";
                    }
                    else
                    {
                        var resultqueryer = from data in HTL.DTAllTx.AsEnumerable()
                                            select new
                                            {
                                                NamaPPAT = data.Field<string>("NamaPPAT"),
                                                NoAplikasi = data.Field<string>("NoAppl"),
                                                NoPerjanjian = data.Field<string>("NoPerjanjian"),
                                                Debitur = data.Field<string>("Debitur"),
                                                NoSHM = data.Field<string>("NoSHM"),
                                                WilayahHAK = data.Field<string>("WilayahHAK"),
                                                KodeAkta = data.Field<string>("KodeAkta"),
                                                NoAkta = data.Field<string>("NoAkta"),
                                                TglSPA = data.Field<string>("TglSPA"),
                                                NoBerkasSHT = UserTypes == "0" ? data.Field<string>("NoBerkasSHT") : "-",
                                                TglDaftarSHT = data.Field<string>("TglPendaftaranHT"),
                                                IssueSHM = data.Field<string>("ISUESHM"),
                                                Keterangan = data.Field<string>("Keterangan"),
                                                Posisi = data.Field<string>("Posisi"),
                                            };
                        dtrepoter = resultqueryer.ToArray().ToDataTable();
                        dtrepoter.TableName = "DataReport";
                    }
                    using (MemoryStream pdfDocumentstream = new MemoryStream())
                    {
                        dtrepoter.WriteXml(pdfDocumentstream);
                        buffer = pdfDocumentstream.ToArray();
                    }
                    minut = DateTime.Now.ToString("ddMMyyyymmss");
                    filenamepar = "Data_" + namarpt + "_" + minut + ".xml";
                    filtpe = "application/xml";
                    return File(buffer, filtpe, filenamepar);
                }
                else if (reooo == "cfrmbast")
                {
                    DataTable dtrepoter = new DataTable();
                    DataTable dtresultod = await HTLddl.dbupdateBAST("2", dataupload, null, "", "", caption, UserID, GroupName);
                    resulted = dtresultod.Rows[0][0].ToString();
                    nobast = dtresultod.Rows[0][1].ToString();

                    if (resulted == "1")
                    {
                        var resultqueryer = from data in HTL.DTAllTx.AsEnumerable()
                                            select new
                                            {
                                                NoAplikasi = data.Field<string>("NoAppl"),
                                                NoPerjanjian = data.Field<string>("NoPerjanjian"),
                                                Debitur = data.Field<string>("Debitur"),
                                                NoSHM = data.Field<string>("NoSHM"),
                                                KodeAkta = data.Field<string>("KodeAkta"),
                                                NoAkta = data.Field<string>("NoAkta"),
                                                Keterangan = data.Field<string>("Keterangan")
                                            };

                        dtrepoter = resultqueryer.ToArray().ToDataTable();
                        dtrepoter.TableName = "DataReport";

                        using (MemoryStream pdfDocumentstream = new MemoryStream())
                        {
                            dtrepoter.WriteXml(pdfDocumentstream);
                            buffer = pdfDocumentstream.ToArray();
                        }
                        minut = DateTime.Now.ToString("ddMMyyyymmss");
                        filenamepar = "Data_Konfirmasi_BAST" + minut + ".xml";
                        filtpe = "application/xml";
                    }
                    return File(buffer, filtpe, filenamepar);
                }
                else
                {
                    //var powderdockp = AllowPrint == true ? "1" : "0";
                    var powderdockd = AllowDownload == true ? "1" : "0"; // untuk handele downloa pada pdfvier//

                    if ((prevedid ?? "") == "" || (prevedid ?? "") == "brk")
                    {
                        ZipEntry newZipEntry = new ZipEntry();
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var zip = new ZipFile())
                            {
                                zip.Password = UserID.ToLower();
                                //HasKeyProtect.Encryption(LoginAksesKey);
                                // string folderequest = "";
                                foreach (DataRow rowsdata in dataupload.Rows)
                                {
                                    //folderequest = rowsdata["REQUEST_NO"].ToString();
                                    string pathdwn = "";
                                    string tipe = "";

                                    pathdwn = rowsdata["Key3"] + "/" + rowsdata["Key4"] + "/";

                                    tipe = "100";

                                    try
                                    {
                                        string pathsbe = "";
                                        List<cDocumentsGet> litdoc = await Commonddl.Getdocview(0, rowsdata["Key1"].ToString(), tipe, caption, UserID, GroupName);
                                        foreach (cDocumentsGet s in litdoc)
                                        {
                                            byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                                            string pooo = rowsdata["Key1"].ToString();
                                            //pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                                            string KECEP = HasKeyProtect.Encryption(pooo);
                                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                                            //if (prevedid == "brk")
                                            //{
                                            pathsbe = pathdwn + s.FILE_NAME;
                                            //}
                                            //else
                                            //{

                                            //pathsbe = s.FILE_NAME;
                                            //}
                                            zip.AddEntry(pathsbe, imageBytes);
                                        }
                                    }
                                    catch
                                    {
                                        statused = "not found";
                                    }
                                }
                                zip.Save(memoryStream);
                            }
                            buffer = memoryStream.ToArray();
                        }
                    }

                    minut = DateTime.Now.ToString("ddMMyyyymmss");
                    filenamepar = (prevedid == "brk" ? "BERKAS DOKUMEN HT_" + minut + ".zip" : "SERTIFIKAT DAN STICKER HT_" + minut + ".zip");
                    filtpe = "application/zip";

                    return File(buffer, filtpe, filenamepar);
                }


            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //filter data
        public async Task<ActionResult> clnOpenFilterpop(string opr, string cab, string reg)

        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                modFilter = modFilter == null ? new cFilterContract() : modFilter;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string moduleid = modFilter.ModuleID;

                if (opr != "load")
                {
                    IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + (reg ?? "")] as IEnumerable<cListSelected>);
                    //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                    bool loaddata = false;
                    //set field filter to varibale //
                    string SelectArea = modFilter.SelectArea ?? "";
                    string SelectBranch = modFilter.SelectBranch ?? "";
                    if (SelectArea != reg)
                    {
                        SelectArea = reg;
                        modFilter.SelectArea = SelectArea;
                        modFilter.SelectBranch = SelectBranch;
                        if (tempbrach == null)
                        {
                            loaddata = true;
                        }
                        else
                        {
                            int rec = tempbrach.Count();
                            loaddata = (rec > 0) ? false : true;
                        }

                        if (loaddata == true)
                        {
                            //decript for db//
                            string decSelectArea = SelectArea;  //HasKeyProtect.Decryption(SelectClient);
                            string decSelectBranch = SelectBranch; // HasKeyProtect.Decryption(SelectBranch);
                            Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", decSelectBranch, decSelectArea, "", UserID, GroupName);
                            tempbrach = Common.ddlBranch;
                        }
                    }
                    else
                    {
                        SelectBranch = "";
                    }
                    TempData["tempbrach" + (reg ?? "")] = tempbrach;

                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = HTL;
                    TempData[tempcommon] = Common;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        branchjson = new JavaScriptSerializer().Serialize(tempbrach),
                        brachselect = SelectBranch, //HasKeyProtect.Decryption(SelectBranch),
                    });

                }
                else
                {

                    // get value filter before//
                    string Keykode = modFilter.RequestNo;
                    string SelectArea = modFilter.SelectArea;
                    string SelectBranch = modFilter.SelectBranch;
                    string SelectDivisi = modFilter.SelectDivisi;
                    string SelectContractStatus = modFilter.SelectContractStatus ?? "1";
                    string fromdate = modFilter.fromdate ?? "";
                    string todate = modFilter.todate ?? "";

                    //decript for db//
                    //string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                    //string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    //if (Common.ddlStatusMitra == null)
                    //{
                    //    Common.ddlStatusMitra = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATHDL", moduleid, UserID, GroupName);
                    //}

                    string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                    UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                    moduleid = HasKeyProtect.Decryption(moduleid);

                    if (Common.ddlStatusMitra == null)
                    {
                        Common.ddlStatusMitra = await Commonddl.dbdbGetDdlEnumsListByEncryptNw("STATHDL", moduleid, UserID, GroupName, "99");
                    }

                    if (Common.ddlnotaris == null)
                    {
                        Common.ddlnotaris = await HTLddl.dbdbGetDdlNotarisListByEncrypt(moduleid, "", UserID, GroupName);
                    }

                    if (int.Parse(UserTypes) == (int)UserType.Notaris)
                    {
                        string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                        Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                    }

                    ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);
                    ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusMitra);

                    if (Common.ddlnotarisInv == null)
                    {
                        Common.ddlnotarisInv = await Commonddl.dbdbGetDdlNotarisListByEncryptINV(moduleid, UserID, GroupName);
                    }

                    ViewData["SelectNotarisInv"] = OwinLibrary.Get_SelectListItem(Common.ddlnotarisInv);

                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = HTL;
                    TempData[tempcommon] = Common;

                    string datakosong = HasKeyProtect.Encryption("");

                    // senback to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        opsi1 = Keykode,
                        opsi2 = SelectDivisi, //decSelectBranch,
                        opsi3 = SelectArea, //SelectNotaris,
                        opsi4 = SelectBranch,

                        opsi5 = SelectContractStatus,
                        opsi6 = fromdate,
                        opsi7 = todate,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/_uiFilterData.cshtml", modFilter),
                    });
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnHeaderTxFilter(cFilterContract model)
        {

            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                await Task.Delay(0);

                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;


                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string ClientID = Account.AccountLogin.ClientID;
                string IDCabang = Account.AccountLogin.IDCabang;
                string IDNotaris = Account.AccountLogin.IDNotaris;
                string GroupName = Account.AccountLogin.GroupName;
                string ClientName = Account.AccountLogin.ClientName;
                string CabangName = Account.AccountLogin.CabangName;
                string Mailed = Account.AccountLogin.Mailed;
                string GenMoon = Account.AccountLogin.GenMoon;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string idcaption = HasKeyProtect.Decryption(modFilter.ModuleID);
                string caption = idcaption;
                string menu = modFilter.Menu;


                // extend //
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                //set field to output//
                string KeySearch = model.RequestNo ?? "";
                string todate = model.todate ?? "";
                string fromdate = model.fromdate ?? "";
                string SelectArea = model.SelectArea ?? "";
                string SelectBranch = model.SelectBranch ?? "";
                string SelectDivisi = model.SelectDivisi ?? "";
                string SelectNotaris = model.SelectNotaris ?? "";
                string SelectNotarisInv = model.SelectNotarisDesc ?? "";
                string Status = model.SelectContractStatus ?? "-1";

                //set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                //set filter//
                Status = (Status.All(char.IsNumber) || Status == "-1") ? Status : HasKeyProtect.Decryption(Status);
                modFilter.RequestNo = KeySearch;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectArea = SelectArea;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.SelectNotarisDesc = SelectNotarisInv;
                modFilter.todate = todate;
                modFilter.fromdate = fromdate;
                modFilter.SelectContractStatus = Status;
                modFilter.ModuleName = caption;
                modFilter.isModeFilter = true;
                //set filter//

                // cek validation for filterisasi //
                //string validtxt = lgPendaftaran.CheckFilterisasiData(modFilter, download);
                string validtxt = "";
                if (validtxt == "")
                {

                    // try show filter data//
                    SelectNotaris = SelectNotaris != "" ? HasKeyProtect.Decryption(SelectNotaris) : SelectNotaris;
                    SelectNotarisInv = SelectNotarisInv != "" ? HasKeyProtect.Decryption(SelectNotarisInv) : SelectNotarisInv;

                    if (SelectNotarisInv != "")
                    {
                        SelectNotaris = SelectNotarisInv;
                        Status = "59";
                    }

                    List<String> recordPage = await HTLddl.dbGetHeaderTxListCount(KeySearch, SelectNotaris, SelectBranch, "", fromdate, todate, int.Parse(Status), PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await HTLddl.dbGetHeaderTxList(null, KeySearch, SelectNotaris, SelectBranch, "", fromdate, todate, int.Parse(Status), PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;
                    bool isModeFilter = modFilter.isModeFilter;

                    //set to object pendataran//
                    HTL.DTAllTx = dtlist[0];
                    HTL.DTHeaderTx = dtlist[1];
                    HTL.FilterTransaksi = modFilter;
                    HTL.Permission = PermisionModule;

                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = HTL;
                    TempData[tempcommon] = Common;


                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    ViewBag.ShowNotaris = "";
                    if (int.Parse(UserTypes) == (int)UserType.FDCM)
                    {
                        ViewBag.ShowNotaris = "allow";
                    }


                    ViewBag.menu = menu;
                    ViewBag.caption = caption;
                    ViewBag.captiondesc = menuitemdescription;
                    ViewBag.rute = "";
                    ViewBag.action = "";

                    //string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    //ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLLst.cshtml", HTL),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {
                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = HTL;
                    TempData[tempcommon] = Common;

                    //sendback to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = "",
                        download = "",
                        message = validtxt
                    });
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> clnRgridHeaderTx(int paged)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                // get value filter have been filter//
                string RegmitraNo = modFilter.RequestNo ?? "";
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
                string cabang = modFilter.SelectBranch ?? "";
                string SelectNotaris = modFilter.SelectNotaris ?? "";
                string status = modFilter.SelectContractStatus ?? "-1";

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);
                SelectNotaris = SelectNotaris != "" ? HasKeyProtect.Decryption(SelectNotaris) : SelectNotaris;
                // try show filter data//
                List<DataTable> dtlist = await HTLddl.dbGetHeaderTxList(null, RegmitraNo, SelectNotaris, cabang, "", fromdate, todate, int.Parse(status), PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                HTL.DTAllTx = dtlist[0];
                HTL.DTHeaderTx = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewBag.ShowNotaris = "";
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                if (int.Parse(UserTypes) == (int)UserType.FDCM)
                {
                    ViewBag.ShowNotaris = "allow";
                }

                //set session filterisasi //
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/HTL/uiHTLLstGrid.cshtml", HTL),
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public async Task<ActionResult> clnRgridIpt(string ap, string jn)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }
            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                ap = HasKeyProtect.Decryption(ap);
                jn = HasKeyProtect.Decryption(jn);

                string viewbro = "";

                if (jn == "0")
                {
                    viewbro = "/Views/HTL/uiHTLLstGridPSG.cshtml";
                    HTL.HeaderInfo.DataPSG = await HTLddl.dbGetMultiData(ap, jn, caption, UserID, GroupName);
                }


                if (jn == "1")
                {
                    viewbro = "/Views/HTL/uiHTLLstGridTNH.cshtml";
                    HTL.HeaderInfo.DataTNH = await HTLddl.dbGetMultiData(ap, jn, caption, UserID, GroupName);
                }

                if (jn == "2")
                {
                    viewbro = "/Views/HTL/uiHTLLstGridSRT.cshtml";
                    HTL.HeaderInfo.DataSRT = await HTLddl.dbGetMultiData(ap, jn, caption, UserID, GroupName);
                }

                if (jn == "3")
                {
                    viewbro = "/Views/HTL/uiHTLLstGridSRTPSG.cshtml";
                    HTL.HeaderInfo.DataSRTPSG = await HTLddl.dbGetMultiData(ap, jn, caption, UserID, GroupName);
                }


                ViewBag.Nappl = ap;
                ViewBag.Jndata1 = "1";
                ViewBag.Jndata2 = "2";
                ViewBag.Jndata3 = "0";
                ViewBag.Jndata4 = "3";

                //set session filterisasi //
                TempData[tempTransksi] = HTL;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewbro, HTL.HeaderInfo),
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

        private static void Table_BeginRowLayout(object sender, BeginRowLayoutEventArgs args)

        {

            args.MinimalHeight = 15f;

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> vwHT(string eux, string aux)
        {


            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }


            try
            {

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(modFilter.ModuleID);

                string data = HasKeyProtect.Decryption(eux);
                string tipe = data.Split(',')[1].ToString();
                string noappl = data.Split(',')[0].ToString();

                int result = await HTLddl.dbRequestDoc(tipe, noappl, caption, UserID, GroupName);
                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? "Request Dokumen Berhasil" : EnumMessage;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
                    mode = "",
                    diadu = "",
                    gtid = "",
                    resulted = result
                });

            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public static byte[] CreateDokumen4BPNHT(
                        string TemplateName,
                        string NoAppl,
                        bool IsDocSave,
                        string template,
                        string ttdnasabah,
                        string ttdppat,
                        string userid)
        {
            Byte[] res = null;
            byte[] imagetemplate = null;
            byte[] imageBytes = null;
            byte[] imageBytes1 = null;


            string filenamnew = TemplateName + ".docx";
            string filenamnewpdf = TemplateName + ".pdf";
            //string pathfilenew = path + string.Format(filenamnew, nomorhak);

            if (template != "")
            {
                imagetemplate = Convert.FromBase64String(ttdppat);
                string KECEP = HasKeyProtect.Encryption(userid);
                imagetemplate = HasKeyProtect.SetFileByteDecrypt(imagetemplate, KECEP);
                //System.IO.File.WriteAllBytes(path + "ttd.jpeg", imageBytes);
            }

            if (ttdppat != "")
            {
                imageBytes = Convert.FromBase64String(ttdppat);
                string KECEP = HasKeyProtect.Encryption(userid);
                imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                //System.IO.File.WriteAllBytes(path + "ttd.jpeg", imageBytes);
            }

            if (ttdnasabah != "")
            {
                imageBytes1 = Convert.FromBase64String(ttdnasabah);
                string KECEP1 = HasKeyProtect.Encryption(NoAppl);
                imageBytes1 = HasKeyProtect.SetFileByteDecrypt(imageBytes1, KECEP1);
                //System.IO.File.WriteAllBytes(path + "ttdnb.jpeg", imageBytes1);
            }

            //res = System.IO.File.ReadAllBytes(pathfilenew);
            //System.IO.File.Delete(pathfilenew);
            return res;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> vwmhn(string eux, string aux)
        {


            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }


            try
            {

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                HTL = TempData[tempTransksi] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = HTL;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string ProvinsiPHAK = "";
                string KotaPHAK = "";

                string ProvinsiBidangtanah = "";
                string KotaBidangtanah = "";
                string KecamatanBidangTanah = "";
                string KelDesaBidangTanah = "";
                string AlamatBidangTanah = "";

                string blankoSertifikat = "";
                string AtasnamaSertifikat = "";
                string nibSertifikat = "";
                string noktpSertifikat = "";
                string pekerjaanSertifikat = "";
                string alamatSertifikat = "";
                string alamatSertifikat2 = "";

                string TptlahirPemilikSertifikat = "";
                string TgllahirPemilikSertifikat = "";
                string ProvinsiPemilikSertifikat = "";
                string KotaPemilikSertifikat = "";
                string KecamatanPemilikSertifikat = "";
                string DesaKelurahanPemilikSertifikat = "";

                string SertifikathakMilikno = "";

                string JenisHak = "";
                string NilaiHakMilik = "";
                string NilaiHTDesc = "";


                string NamaPICPT = "";
                string AlamatPICPT = "";
                string NamaPTPIC = "";
                string NoSuratUkur = "";
                string TanggalSuratUkur = "";
                string LuasSuratUkur = "";

                string NamaPPAT = "";
                string KotkabPPAT = "";
                string AlamatPPAT = "";
                string NIKPPAT = "";
                string TptLahirPPAT = "";
                string TglLahirPPAT = "";
                string DomisiliPPAT = "";
                string IDBPN = "";
                string NoAppl = "";
                string WargaSertifikat = "";

                DataRow rw = HTL.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux).SingleOrDefault();

                ProvinsiPHAK = rw["ProvinsiPemilikSertifikat"].ToString();
                KotaPHAK = rw["KotaPemilikSertifikat"].ToString();
                ProvinsiBidangtanah = rw["LokasiTanahDiProvinsi"].ToString();
                KotaBidangtanah = rw["LokasiTanahDiKota"].ToString();
                KecamatanBidangTanah = rw["LokasiTanahDiKecamatan"].ToString();
                KelDesaBidangTanah = rw["LokasiTanahDiDesaKelurahan"].ToString();
                AlamatBidangTanah = rw["LokasiTanahDiAlamat"].ToString();


                nibSertifikat = rw["NomorNIB"].ToString();
                blankoSertifikat = rw["NoBlanko"].ToString();
                AtasnamaSertifikat = rw["NamaPemilikSertifikat"].ToString();
                noktpSertifikat = rw["NIKPemilikSertifikat"].ToString();
                pekerjaanSertifikat = rw["PekerjaanPemilikSertifikat"].ToString();
                SertifikathakMilikno = rw["NoSertifikat"].ToString();

                ProvinsiPemilikSertifikat = rw["ProvinsiPemilikSertifikat"].ToString();
                KotaPemilikSertifikat = rw["KotaPemilikSertifikat"].ToString();
                KecamatanPemilikSertifikat = rw["KecamatanPemilikSertifikat"].ToString();
                DesaKelurahanPemilikSertifikat = rw["DesaKelurahanPemilikSertifikat"].ToString();
                TptlahirPemilikSertifikat = rw["TptlahirPemilikSertifikat"].ToString();
                TgllahirPemilikSertifikat = DateTime.Parse(rw["TgllahirPemilikSertifikat"].ToString()).ToString("yyyy-MM-dd");

                WargaSertifikat = rw["WargaPemilikSertifikat"].ToString();
                WargaSertifikat = WargaSertifikat.ToLower() == "wni" ? "Indoensia" : "Asing";

                alamatSertifikat = rw["AlamatPemilikSertifikat"].ToString();
                alamatSertifikat2 = "Kel. " + DesaKelurahanPemilikSertifikat + " Kec. " + KecamatanPemilikSertifikat + " " + KotaPemilikSertifikat;

                JenisHak = rw["Statushakdesc"].ToString();

                NilaiHTDesc = rw["NilaiHTDesc"].ToString();

                NamaPPAT = rw["namanotaris"].ToString();
                KotkabPPAT = rw["kotkabnot"].ToString();
                AlamatPPAT = rw["alamatnot"].ToString();
                TglLahirPPAT = rw["tgllahirnot"].ToString();
                TptLahirPPAT = rw["tptlahirnot"].ToString();
                DomisiliPPAT = rw["kotkabnot"].ToString();
                NIKPPAT = rw["NIKNot"].ToString();
                IDBPN = rw["IDBPNNot"].ToString();

                NamaPICPT = rw["clntname"].ToString();
                AlamatPICPT = rw["clntarress"].ToString();
                NamaPTPIC = rw["clntpt"].ToString();
                NoSuratUkur = rw["NoSuratUkur"].ToString();
                TanggalSuratUkur = rw["TglSuratUkur"].ToString();
                LuasSuratUkur = rw["LuasTanah"].ToString().Replace(".00", "");

                NoAppl = rw["NoAppl"].ToString();

                NamaPPAT = rw["namanotaris"].ToString();
                AlamatPPAT = rw["alamatnot"].ToString();
                NIKPPAT = rw["NIKNot"].ToString();
                TptLahirPPAT = rw["tptlahirnot"].ToString();
                TglLahirPPAT = rw["tgllahirnot"].ToString();

                //tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                //NomorSPL = rw["ContractSPLNo"].ToString();
                //AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                //JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                //NamaMitra = rw["NamaMitra"].ToString();
                //KtpMitra = rw["NoKTP"].ToString();
                //NIKMitra = rw["NIKBaru"].ToString();
                //TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                //TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");


                caption = HasKeyProtect.Decryption(caption);
                // extend //
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();


                string ttdpat = "";
                string namanotaris = "";
                string filenamevar = "";

                KelDesaBidangTanah = initcapp.InitCap(KelDesaBidangTanah);
                KecamatanBidangTanah = initcapp.InitCap(KecamatanBidangTanah);
                KotaBidangtanah = initcapp.InitCap(KotaBidangtanah);
                ProvinsiBidangtanah = initcapp.InitCap(ProvinsiBidangtanah);

                string umurppat = Math.Abs(int.Parse(TglLahirPPAT.Substring(0, 4)) - DateTime.Now.Year).ToString() + " Tahun ";
                string umursertifikat = Math.Abs(int.Parse(TgllahirPemilikSertifikat.Substring(0, 4)) - DateTime.Now.Year).ToString() + " Tahun ";

                alamatSertifikat = alamatSertifikat + " Kel. " + initcapp.InitCap(KelDesaBidangTanah) + " Kec. " + initcapp.InitCap(KecamatanBidangTanah) + " " + initcapp.InitCap(KotaBidangtanah);
                DataTable dttd = await HTLddl.dbdbGetDdlOrderGetCek("10", NoAppl, "", caption, UserID, GroupName);
                string ttdnasabah = "";
                if (dttd.Rows.Count > 0)
                {
                    ttdnasabah = dttd.Rows[0][0].ToString();
                }

                Byte[] res = null;
                aux = HasKeyProtect.Decryption(aux);
                string kode = aux.Contains("skpt") ? "_SKPT" : "";
                string offic = ".docx"; //aux.Contains("2007") ? ".doc" : "docx";
                string contenttipe = "application/msword"; //"aux.Contains("2007") ? "application/msword" : "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                //if (aux == "mhn" || aux == "mhnskpt")
                //{
                //    ttdpat = Account.AccountLogin.ttdfform;
                //    namanotaris = Account.AccountLogin.NotarisName;
                //    if (namanotaris.ToLower().Contains("dewi mulyani"))
                //    {
                //        res = SuratmohonHTDOC_DEWI(NoAppl, kode, ProvinsiBidangtanah, KotaBidangtanah, KecamatanBidangTanah, KelDesaBidangTanah, AlamatBidangTanah, LuasSuratUkur,
                //            AtasnamaSertifikat, umursertifikat, TptlahirPemilikSertifikat, TgllahirPemilikSertifikat, noktpSertifikat, pekerjaanSertifikat, alamatSertifikat, DesaKelurahanPemilikSertifikat, KecamatanPemilikSertifikat, KotaPemilikSertifikat,
                //            JenisHak, SertifikathakMilikno, namanotaris.ToUpper(), AlamatPPAT, NIKPPAT, umurppat, ttdpat, Account.AccountLogin.UserID);
                //    }

                //    if (namanotaris.ToLower().Contains("adhisty"))
                //    {
                //        res = SuratmohonHTDOC_ADHIST(NoAppl, kode, ProvinsiBidangtanah, KotaBidangtanah, KecamatanBidangTanah, KelDesaBidangTanah, AlamatBidangTanah, LuasSuratUkur,
                //            AtasnamaSertifikat, umursertifikat, TptlahirPemilikSertifikat, TgllahirPemilikSertifikat, noktpSertifikat, pekerjaanSertifikat, alamatSertifikat, DesaKelurahanPemilikSertifikat, KecamatanPemilikSertifikat, KotaPemilikSertifikat,
                //            JenisHak, SertifikathakMilikno, namanotaris.ToUpper(), AlamatPPAT, NIKPPAT, umurppat, ttdpat, Account.AccountLogin.UserID);
                //    }

                //    filenamevar = "SURAT PERMOHONAN_" + SertifikathakMilikno.Substring(SertifikathakMilikno.Length - 5, 5) + offic;

                //}
                //else 

                if (aux == "suku" || aux == "sukuskpt")
                {
                    ttdpat = Account.AccountLogin.ttdsk;
                    namanotaris = Account.AccountLogin.NotarisName;

                    filenamevar = "SURAT KUASA_" + SertifikathakMilikno.Substring(SertifikathakMilikno.Length - 5, 5) + offic;
                }
                //else if (aux == "abs" || aux == "absskpt")
                //{
                //    System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("id-ID");
                //    DateTime tgllahir = DateTime.Parse(TglLahirPPAT, cultureinfo);
                //    TglLahirPPAT = TptLahirPPAT + "," + tgllahir.ToString("dd MMMM yyyy", cultureinfo);
                //    ttdpat = Account.AccountLogin.ttdabsah;
                //    namanotaris = Account.AccountLogin.NotarisName;

                //    if (namanotaris.ToLower().Contains("dewi mulyani"))
                //    {
                //        res = SuratAbs_DEWI(NoAppl, kode, ProvinsiBidangtanah, KotaBidangtanah, KecamatanBidangTanah, KelDesaBidangTanah, AlamatBidangTanah, LuasSuratUkur, nibSertifikat, blankoSertifikat, AtasnamaSertifikat,
                //                                                      umursertifikat, TptlahirPemilikSertifikat, TgllahirPemilikSertifikat, WargaSertifikat, noktpSertifikat, pekerjaanSertifikat, alamatSertifikat, DesaKelurahanPemilikSertifikat, KecamatanPemilikSertifikat, KotaPemilikSertifikat,
                //                                                      JenisHak, SertifikathakMilikno, namanotaris.ToUpper(), AlamatPPAT, NIKPPAT, ttdnasabah, ttdpat, Account.AccountLogin.UserID);

                //    }

                //    if (namanotaris.ToLower().Contains("adhisty"))
                //    {
                //        res = SuratAbs_ADHIST(NoAppl, kode, ProvinsiBidangtanah, KotaBidangtanah, KecamatanBidangTanah, KelDesaBidangTanah, AlamatBidangTanah, LuasSuratUkur, nibSertifikat, blankoSertifikat, AtasnamaSertifikat,
                //                                                      umursertifikat, TptlahirPemilikSertifikat, TgllahirPemilikSertifikat, WargaSertifikat, noktpSertifikat, pekerjaanSertifikat, alamatSertifikat, DesaKelurahanPemilikSertifikat, KecamatanPemilikSertifikat, KotaPemilikSertifikat,
                //                                                      JenisHak, SertifikathakMilikno, namanotaris.ToUpper(), AlamatPPAT, NIKPPAT, ttdnasabah, ttdpat, Account.AccountLogin.UserID);

                //    }


                //    filenamevar = "SURAT KEABSAHAN_" + SertifikathakMilikno.Substring(SertifikathakMilikno.Length - 5, 5) + offic;
                //}

                var contenttypeed = contenttipe; //"application /pdf";
                string powderdockp = PermisionModule.AllowPrint == true ? "1" : "0";
                string powderdockd = PermisionModule.AllowDownload == true ? "1" : "0";


                //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=&file=";
                var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = res, msg = "", contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
                jsonresult.MaxJsonLength = int.MaxValue;
                return jsonresult;
                //}
                //create area footer//
            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public static byte[] CreateDokumen4BPN(
                        string TemplateName,
                        string NoAppl,
                        bool IsDocSave,
                        string template,
                        string ttdnasabah,
                        string ttdppat,
                        string userid)
        {
            Byte[] res = null;
            byte[] imagetemplate = null;
            byte[] imageBytes = null;
            byte[] imageBytes1 = null;


            string filenamnew = TemplateName + ".docx";
            string filenamnewpdf = TemplateName + ".pdf";
            //string pathfilenew = path + string.Format(filenamnew, nomorhak);

            if (template != "")
            {
                imagetemplate = Convert.FromBase64String(ttdppat);
                string KECEP = HasKeyProtect.Encryption(userid);
                imagetemplate = HasKeyProtect.SetFileByteDecrypt(imagetemplate, KECEP);
                //System.IO.File.WriteAllBytes(path + "ttd.jpeg", imageBytes);
            }

            if (ttdppat != "")
            {
                imageBytes = Convert.FromBase64String(ttdppat);
                string KECEP = HasKeyProtect.Encryption(userid);
                imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                //System.IO.File.WriteAllBytes(path + "ttd.jpeg", imageBytes);
            }

            if (ttdnasabah != "")
            {
                imageBytes1 = Convert.FromBase64String(ttdnasabah);
                string KECEP1 = HasKeyProtect.Encryption(NoAppl);
                imageBytes1 = HasKeyProtect.SetFileByteDecrypt(imageBytes1, KECEP1);
                //System.IO.File.WriteAllBytes(path + "ttdnb.jpeg", imageBytes1);
            }

            //res = System.IO.File.ReadAllBytes(pathfilenew);
            //System.IO.File.Delete(pathfilenew);
            return res;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnOpenAddUploadRegisHTFlag(string JenisTransaksi, HttpPostedFileBase upflebyr)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //get session filterisasi //
                modFilter = TempData["UploadRegisHTListFilterTxt"] as cFilterContract;
                HTL = TempData["UploadRegisHTListTxt"] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = ""; //HasKeyProtect.Decryption(modFilter.idcaption);


                DataTable dt = new DataTable();

                string resulttxt = string.Empty;
                using (BinaryReader b = new BinaryReader(upflebyr.InputStream))
                {
                    byte[] binData = b.ReadBytes(upflebyr.ContentLength);
                    resulttxt = System.Text.Encoding.UTF8.GetString(binData);
                }
                dt = OwinLibrary.ConvertByteToDT(resulttxt);
                int result = await HTLddl.dbupdateUploadRegisHTFlag(JenisTransaksi, dt, caption, UserID, GroupName);
                string errmessage = "";
                errmessage = result.ToString() + " data Berhasil diproses, silahkan dicek kembali";

                TempData["UploadRegisHTListTxt"] = HTL;
                TempData["UploadRegisHTListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = errmessage,
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public async Task<ActionResult> clnOpenAddUploadRegis(string gontok)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //get session filterisasi //
                modFilter = TempData["UploadRegisHTListFilterTxt"] as cFilterContract;
                HTL = TempData["UploadRegisHTListTxt"] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                //string caption = HasKeyProtect.Decryption(modFilter.idcaption);

                string viewpo = "/Views/HTL/_uiUpdDT.cshtml";
                gontok = (gontok ?? "");
                if (gontok != "")
                {
                    gontok = HasKeyProtect.Decryption(gontok);
                }

                //if (gontok == "createinvoice")
                //{
                //} else


                Common.ddlJenisDokumen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("FLAGSPS", "", UserID, GroupName);
                ViewData["SelectTrans"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);

                TempData["UploadRegisHTListTxt"] = HTL;
                TempData["UploadRegisHTListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewpo, modFilter),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public async Task<ActionResult> clnOpenAddBASTSCH(string gontok)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //get session filterisasi //
                modFilter = TempData["UploadRegisHTListFilterTxt"] as cFilterContract;
                HTL = TempData["UploadRegisHTListTxt"] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                //string caption = HasKeyProtect.Decryption(modFilter.idcaption);

                string viewpo = "/Views/HTL/_uiUpdDTBAST.cshtml";
                gontok = (gontok ?? "");
                if (gontok != "")
                {
                    gontok = HasKeyProtect.Decryption(gontok);
                }

                //if (gontok == "createinvoice")
                //{
                //} else


                Common.ddlJenisDokumen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("FLAGBAST", "", UserID, GroupName);
                ViewData["SelectTrans"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);

                TempData["UploadRegisHTListTxt"] = HTL;
                TempData["UploadRegisHTListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewpo, modFilter),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnOpenAddBASTSCHFLAG(string JenisTransaksi, string NoBast, string Tglbast, HttpPostedFileBase upflebyr)
        {
            Account = (vmAccount)Session["Account"];
            bool IsErrorTimeout = false;
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {
                    IsErrorTimeout = true;
                }
            }
            else
            {
                IsErrorTimeout = true;
            }

            if (IsErrorTimeout == true)
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //get session filterisasi //
                modFilter = TempData["UploadRegisHTListFilterTxt"] as cFilterContract;
                HTL = TempData["UploadRegisHTListTxt"] as vmHTL;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = ""; //HasKeyProtect.Decryption(modFilter.idcaption);


                DataTable dt = new DataTable();

                string resulttxt = string.Empty;
                //using (BinaryReader b = new BinaryReader(upflebyr.InputStream))
                //{
                //    byte[] binData = b.ReadBytes(upflebyr.ContentLength);
                //    resulttxt = System.Text.Encoding.UTF8.GetString(binData);
                //}
                //dt = OwinLibrary.ConvertByteToDT(resulttxt);
                int result = await HTLddl.dbupdateUploadSCHBACTlag(JenisTransaksi, NoBast, Tglbast, caption, UserID, GroupName);
                string errmessage = "";
                errmessage = result.ToString() + " data Berhasil diproses, silahkan dicek kembali";

                TempData["UploadRegisHTListTxt"] = HTL;
                TempData["UploadRegisHTListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = errmessage,
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }

    }

}

/*
            float maxw = 0;
            using (var document = DocX.Load(path + filenam + ".docx"))
            {

                Xceed.Document.NET.Border ds = new Xceed.Document.NET.Border();
                ds.Tcbs = Xceed.Document.NET.BorderStyle.Tcbs_single;
                Xceed.Document.NET.Borders bd = new Xceed.Document.NET.Borders();
                bd.Left = ds;

                //document.MarginRight = 50;
                List<Xceed.Document.NET.Section> pop = document.Sections.ToList();


                string txt = "";
                foreach (Xceed.Document.NET.Section kop in pop)
                {
                    List<string> txtnp = new List<string>();
                    List<string> txtnpAll = new List<string>();
                    maxw = document.PageWidth - document.MarginRight; // - document.MarginLeft; //* 2.5F;
                    List<Xceed.Document.NET.Paragraph> po = kop.SectionParagraphs;
                    foreach (Xceed.Document.NET.Paragraph koip in po)
                    {
                        koip.Border(ds);
                        //txt = new String(koip.Text.ToCharArray().Distinct().ToArray());
                        {

                            float widhtparag = kop.PageWidth;
                            float maxllop = maxw;
                            string newstring = "";
                            string FontName = "Courier New";
                            int FontSize = 11;
                            float pjnkarperhruf = 0;
                            float jumlahhuruf = 0;
                            int startdefault = 85; //banyaknya karakter dalam satu baris di word file (hrus ditentukan dulu)

                            Font arialBold = new Font(FontName, FontSize);
                            Size textSize = TextRenderer.MeasureText("-", arialBold);
                            pjnkarperhruf = textSize.Width;
                            jumlahhuruf = maxllop / pjnkarperhruf;


                            foreach (var item in koip.Text.Split(new string[] { "\n" }
                                 , StringSplitOptions.RemoveEmptyEntries))
                            {
                                newstring = item;
                                arialBold = new Font(FontName, FontSize);
                                textSize = TextRenderer.MeasureText(newstring, arialBold);
                                float straloop = textSize.Width + koip.IndentationBefore;
                                float laswidth = maxllop;
                                int start = startdefault;
                                string cek = "";
                                double jmlbaris = Math.Ceiling(straloop / maxllop);
                                if (jmlbaris > 1)
                                {
                                    string txtgrap = "";  //newstring;
                                    string txtgrapnext = "";
                                    string graptxt = newstring;
                                    int leng = 0;
                                    //int lastspace = 0;
                                    int starbaris = 1;
                                    while (graptxt.Trim() != "")
                                    {
                                        if (leng > newstring.Length || leng < 0)
                                        {
                                            break;
                                        }

                                        // mencari kata dengan spasi//
                                        int postext = graptxt.IndexOf(" ") < 0 ? 1 : graptxt.IndexOf(" ");

                                        txtgrap = (txtgrap == "") ? graptxt.Substring(0, postext) : txtgrap + " " + graptxt.Substring(0, postext);
                                        graptxt = (graptxt.Length != 1) ? graptxt.Substring(postext + 1, graptxt.Length - (postext + 1)) : "";

                                        //combine with next word//
                                        txtgrapnext = (graptxt.IndexOf(" ") > 0) ? graptxt.Substring(0, graptxt.IndexOf(" ")) : "";

                                        int selisi = (startdefault - txtgrap.Length);
                                        if (selisi < 15 || graptxt == "" || postext == 0)
                                        {
                                            txtnp.Add(txtgrap + "\n");
                                            newstring = newstring.Replace(txtgrap, "");
                                            graptxt = newstring;
                                            txtgrap = "";
                                            starbaris = starbaris + 1;

                                            //selisi = (startdefault - txtgrap.Length);

                                            //if (starbaris == jmlbaris)
                                            //{
                                            //    txtnp.Add(txtgrap + "@");
                                            //    koip.ReplaceText(txtgrap, txtgrap + "\n");
                                            //}
                                        }
                                    }
                                }
                                else
                                {
                                    txtnp.Add(newstring + "\n");
                                }
                            }
                        }
                    }
                }

                document.SaveAs(path + string.Format(filenamnew, nomorhak));
                document.Dispose();

            }
*/
