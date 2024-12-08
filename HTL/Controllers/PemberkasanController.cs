using HashNetFramework;
using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class PemberkasanController : Controller
    {

        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmPemberkasan Pemberkasan = new vmPemberkasan();
        vmPemberkasanddl Pemberkasanddl = new vmPemberkasanddl();
        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        blPemberkasan lgPemberkasan = new blPemberkasan();

        #region Filter Data
        public async Task<ActionResult> clnOpenFilterpop()
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
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectBranch = modFilter.SelectBranch;
                string SelectRegion = modFilter.SelectRegion;
                string SelectClient = modFilter.SelectClient;
                string SelectNotaris = modFilter.SelectNotaris;
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";
                string SelectDocStatus = modFilter.SelectDocStatus ?? "";
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan ?? "";
                string Fill_Debitur_str = modFilter.Fill_Debitur_str ?? "";
                string StatusAkta = modFilter.StatusAkta ?? "";
                bool Fill_Debitur = modFilter.Fill_Debitur;
                string NamaBPKBDebitur = modFilter.NamaBPKBDebitur ?? "";
                string JenisKontrak = modFilter.SelectJenisKontrak;

                //decript for db//
                string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);

                // try make filter initial & set secure module name //
                if (Common.ddlClient == null)
                {
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(decSelectClient);
                }

                if (Common.ddlNotaris == null)
                {
                    SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    Common.ddlNotaris = await Commonddl.dbGetNotarisListByEncrypt();
                }

                if (Common.ddlClient.Count() == 1 && Common.ddlBranch == null)
                {
                    SelectClient = Common.ddlClient.SingleOrDefault().Value;
                    Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, "", UserID);
                }

                if (Common.ddlDocStatus == null)
                {
                    Common.ddlDocStatus = await Commonddl.dbddlgetparamenumsList("DocStatus");
                }

                if (Common.ddlJenisPelanggan == null)
                {
                    Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
                }

                if (Common.ddlRegion == null)
                {
                    Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt();
                }

                if (Common.ddlRegion == null)
                {
                    Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt();
                }

                if (Common.ddlJenisKontrak == null)
                {
                    Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                }

                if (Common.ddlAktaStat == null)
                {
                    Common.ddlAktaStat = await Commonddl.dbddlgetparamenumsList("AKTASTAT");
                }

                if (Common.ddlFillDebitur == null)
                {
                    Common.ddlFillDebitur = await Commonddl.dbddlgetparamenumsList("FILDEBITUR");
                }


                ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);
                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectDocStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlDocStatus);
                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
                ViewData["AktaStat"] = OwinLibrary.Get_SelectListItem(Common.ddlAktaStat);
                ViewData["FilDebitur"] = OwinLibrary.Get_SelectListItem(Common.ddlFillDebitur);



                TempData["PemberkasanList"] = Pemberkasan;
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["common"] = Common;

                string datakosong = HasKeyProtect.Encryption("");

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = modFilter.SelectClient,
                    cabang = decSelectBranch,
                    opsi3 = modFilter.SelectJenisPelanggan,
                    opsi4 = modFilter.SelectDocStatus,
                    opsi5 = modFilter.NoPerjanjian == datakosong ? "" : modFilter.NoPerjanjian,
                    opsi6 = modFilter.fromdate,
                    opsi7 = modFilter.todate,
                    region = modFilter.SelectRegion,
                    opsi8 = modFilter.Fill_Debitur,
                    opsi9 = modFilter.NamaBPKBDebitur,
                    opsi10 = modFilter.SelectNotaris,
                    opsi11 = modFilter.Fill_Debitur_str,
                    opsi12= modFilter.StatusAkta,
                    loadcabang = 0,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiFilterData.cshtml", Pemberkasan.DetailFilter),
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
        public async Task<ActionResult> clnOpenFilterpopMap()
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
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectBranch = modFilter.SelectBranch;
                string SelectRegion = modFilter.SelectRegion;
                string SelectClient = modFilter.SelectClient;
                string SelectNotaris = modFilter.SelectNotaris;
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";
                string SelectDocStatus = modFilter.SelectDocStatus ?? "";
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan ?? "";


                //decript for db//
                string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);

                // try make filter initial & set secure module name //
                if (Common.ddlClient == null)
                {
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(decSelectClient);
                }

                if (Common.ddlClient.Count() == 1 && Common.ddlBranch == null)
                {
                    SelectClient = Common.ddlClient.SingleOrDefault().Value;
                    Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, "", UserID);
                }


                if (Common.ddlRegion == null)
                {
                    Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt();
                }

                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                TempData["PemberkasanList"] = Pemberkasan;
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["common"] = Common;

                string datakosong = HasKeyProtect.Encryption("");

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = modFilter.SelectClient,
                    cabang = decSelectBranch,
                    opsi3 = modFilter.SelectJenisPelanggan,
                    opsi4 = modFilter.SelectDocStatus,
                    opsi5 = modFilter.NoPerjanjian == datakosong ? "" : modFilter.NoPerjanjian,
                    opsi6 = modFilter.fromdate,
                    opsi7 = modFilter.todate,
                    region = modFilter.SelectRegion,
                    loadcabang = 0,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiFilterDataMap.cshtml", Pemberkasan.DetailFilter),
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
        public async Task<ActionResult> clnGetRegion(string clientid)
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
                //set session filterisasi //
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                IEnumerable<cListSelected> tempregion = (TempData["tempregion" + clientid] as IEnumerable<cListSelected>);

                string UserID = modFilter.UserID;

                //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                bool loaddata = false;

                //set field filter to varibale //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectRegion = modFilter.SelectRegion ?? modFilter.RegionLogin;

                if ((SelectClient != clientid))
                {
                    SelectClient = clientid;

                    if (tempregion == null)
                    {
                        loaddata = true;
                    }
                    else
                    {
                        int rec = tempregion.Count();
                        loaddata = (rec > 0) ? false : true;
                    }

                    if (loaddata == true)
                    {
                        //decript for db//
                        string decSelectClient = (SelectClient ?? "") == "" ? "" : HasKeyProtect.Decryption(SelectClient);
                        string decSelectRegion = "";
                        Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt(decSelectRegion, decSelectClient);
                        tempregion = Common.ddlRegion;
                    }
                }

                TempData["tempregion" + clientid] = tempregion;

                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    regionjson = new JavaScriptSerializer().Serialize(tempregion),
                    regionselect = SelectRegion,
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
        public async Task<ActionResult> clnGetBranch(string clientid, string regionid = "")
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
                //set session filterisasi //
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid + regionid] as IEnumerable<cListSelected>);

                string UserID = modFilter.UserID;

                //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                bool loaddata = false;

                //set field filter to varibale //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectRegion = modFilter.SelectRegion ?? modFilter.RegionLogin;
                string SelectBranch = modFilter.SelectBranch ?? modFilter.BranchLogin;

                if ((SelectClient != clientid) && (SelectRegion != regionid))
                {
                    SelectClient = clientid;
                    SelectRegion = regionid;

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
                        string decSelectClient = (SelectClient ?? "") == "" ? "" : HasKeyProtect.Decryption(SelectClient);
                        string decSelectRegion = (SelectRegion ?? "") == "" ? "" : HasKeyProtect.Decryption(SelectRegion);
                        string decSelectBranch = "";
                        Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, decSelectRegion, UserID);
                        tempbrach = Common.ddlBranch;
                    }
                }

                TempData["tempbrach" + clientid + regionid] = tempbrach;

                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    branchjson = new JavaScriptSerializer().Serialize(tempbrach),
                    brachselect = HasKeyProtect.Decryption(SelectBranch),
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
        public async Task<ActionResult> clnListFilterPemberkasaan(cFilterContract model, string download)
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
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = model.NoPerjanjian ?? "";
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string SelectRegion = model.SelectRegion ?? modFilter.RegionLogin;
                string SelectBranch = model.SelectBranch ?? modFilter.BranchLogin;
                SelectBranch = SelectBranch.Length <= 4 ? HasKeyProtect.Encryption(SelectBranch) : SelectBranch;
                string SelectNotaris = model.SelectNotaris ?? modFilter.NotarisLogin;
                string fromdate = model.fromdate ?? "";
                string todate = model.todate ?? "";
                string SelectDocStatus = model.SelectDocStatus ?? "";
                string SelectJenisPelanggan = model.SelectJenisPelanggan ?? "";
                string SelectJenisKontrak = model.SelectJenisKontrak ?? "";
                bool Fill_Debitur = model.Fill_Debitur;
                string Fill_Debitur_str = model.Fill_Debitur_str ?? "";
                string StatusAkta = model.StatusAkta ?? "";
                string namabpkbdebitur = model.NamaBPKBDebitur ?? "";


                //set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;
                bool isModeFilter = true;

                //set filter//
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.SelectClient = SelectClient;
                modFilter.SelectRegion = SelectRegion;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectDocStatus = SelectDocStatus;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.Fill_Debitur = Fill_Debitur;
                modFilter.StatusAkta = StatusAkta;
                modFilter.Fill_Debitur_str = Fill_Debitur_str;
                modFilter.NamaBPKBDebitur = namabpkbdebitur;
                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = lgPemberkasan.CheckFilterisasiDataUplod(modFilter, download);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                    caption = HasKeyProtect.Decryption(caption);

                    List<String> recordPage = await Pemberkasanddl.dbGetPemberkasanListCount(SelectClient, SelectRegion, SelectBranch, SelectNotaris, NoPerjanjian, SelectJenisKontrak, SelectDocStatus, SelectJenisPelanggan, fromdate, todate, "", Fill_Debitur_str, namabpkbdebitur, StatusAkta, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<System.Data.DataTable> dtlist = await Pemberkasanddl.dbGetPemberkasanList(null, SelectClient, SelectRegion, SelectBranch, SelectNotaris, NoPerjanjian, SelectJenisKontrak, SelectDocStatus, SelectJenisPelanggan, fromdate, todate, "", Fill_Debitur_str, namabpkbdebitur, StatusAkta, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //set in filter for paging//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    //set to object pemberkasan//
                    Pemberkasan.DTOrdersFromDB = dtlist[0];
                    Pemberkasan.DTDetailForGrid = dtlist[1];
                    Pemberkasan.DetailFilter = modFilter;


                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["PemberkasanList"] = Pemberkasan;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiGridPemberkasanList.cshtml", Pemberkasan),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {

                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["PemberkasanList"] = Pemberkasan;
                    TempData["common"] = Common;
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
        #endregion Filter Data


        #region Data transaksi
        public async Task<ActionResult> clnPemberkasan(string menu, string caption)
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
                string SelectClient = ClientID;
                string SelectRegion = Region;
                string SelectBranch = IDCabang;
                string SelectNotaris = IDNotaris;
                string SelectDocStatus = "";
                string SelectJenisPelanggan = "";
                string SelectJenisDoc = "";
                string SelectJenisKontrak = "";
                string fromdate = "";
                string todate = "";
                string NoPerjanjian = "";
                bool Fill_Debitur = true;
                string Fill_Debitur_str = "";
                string StatusAkta = "";
                string namabpkbdebitur = "";

                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                modFilter.UserID = UserID;
                modFilter.UserName = UserName;
                modFilter.ClientLogin = ClientID;
                modFilter.BranchLogin = IDCabang;
                modFilter.NotarisLogin = IDNotaris;
                modFilter.RegionLogin = Region;
                modFilter.GroupName = GroupName;
                modFilter.ClientName = ClientName;
                modFilter.CabangName = CabangName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;

                modFilter.SelectClient = SelectClient;
                modFilter.SelectRegion = SelectRegion;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.SelectDocStatus = SelectDocStatus;
                modFilter.JenisDocument = SelectJenisDoc;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.Fill_Debitur = Fill_Debitur;
                modFilter.Fill_Debitur_str = Fill_Debitur_str;
                modFilter.StatusAkta = StatusAkta;
                modFilter.NamaBPKBDebitur = namabpkbdebitur;
                modFilter.PageNumber = PageNumber;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                SelectRegion = HasKeyProtect.Decryption(SelectRegion);

                List<String> recordPage = await Pemberkasanddl.dbGetPemberkasanListCount(SelectClient, SelectRegion, SelectBranch, SelectNotaris, NoPerjanjian, SelectJenisKontrak, SelectDocStatus, SelectJenisPelanggan, fromdate, todate, "", Fill_Debitur_str, namabpkbdebitur, StatusAkta, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<System.Data.DataTable> dtlist = await Pemberkasanddl.dbGetPemberkasanList(null, SelectClient, SelectRegion, SelectBranch, SelectNotaris, NoPerjanjian, SelectJenisKontrak, SelectDocStatus, SelectJenisPelanggan, fromdate, todate, "", Fill_Debitur_str, namabpkbdebitur, StatusAkta, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                //set to object pendataran//
                Pemberkasan.DTOrdersFromDB = dtlist[0];
                Pemberkasan.DTDetailForGrid = dtlist[1];
                Pemberkasan.DetailFilter = modFilter;
                Pemberkasan.Permission = PermisionModule;

                //set session filterisasi //
                Pemberkasan.DocumentGroupType = null;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["dtfile"] = null;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Pemberkasan";
                ViewBag.action = "clnPemberkasan";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/uiPemberkasan.cshtml", Pemberkasan),
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
        public async Task<ActionResult> clnPemberkasanRgrid(int paged)
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
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string NoPerjanjian = modFilter.NoPerjanjian;
                string SelectClient = modFilter.SelectClient;
                string SelectRegion = modFilter.SelectRegion;
                string SelectBranch = modFilter.SelectBranch;
                string SelectNotaris = modFilter.SelectNotaris;
                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string SelectDocStatus = modFilter.SelectDocStatus;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
                bool Fill_Debitur = modFilter.Fill_Debitur;
                string Fill_Debitur_str = modFilter.Fill_Debitur_str;
                string StatusAkta = modFilter.StatusAkta;
                string NamaBPKBDebitur = modFilter.NamaBPKBDebitur;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                caption = HasKeyProtect.Decryption(caption);

                List<System.Data.DataTable> dtlist = await Pemberkasanddl.dbGetPemberkasanList(null, SelectClient, SelectRegion, SelectBranch, SelectNotaris, NoPerjanjian, SelectJenisKontrak, SelectDocStatus, SelectJenisPelanggan, fromdate, todate, "", Fill_Debitur_str, NamaBPKBDebitur, StatusAkta, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                Pemberkasan.DTDetailForGrid = dtlist[1];

                //string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                //set session filterisasi //
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiGridPemberkasanList.cshtml", Pemberkasan),
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
        public async Task<ActionResult> clnPemberkasanStatusGet(string idfdc, string parmode)
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

                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                if (Common.ddlDocStatus == null)
                {
                    Common.ddlDocStatus = await Commonddl.dbddlgetparamenumsList("DocStatus");
                }

                if (Common.ddlJenisPelanggan == null)
                {
                    Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
                }

                if (Common.ddlJenisKelamin == null)
                {
                    Common.ddlJenisKelamin = await Commonddl.dbddlgetparamenumsList("JNSKLM");
                }

                if (Common.ddlJenisIdentitas == null)
                {
                    Common.ddlJenisIdentitas = await Commonddl.dbddlgetparamenumsList("JNSIDEN");
                }

                if (Common.ddlJenisKewarganegaraan == null)
                {
                    Common.ddlJenisKewarganegaraan = await Commonddl.dbddlgetparamenumsList("JNSWRG");
                }

                if (Common.ddlPengadilanNegeri == null)
                {
                    Common.ddlPengadilanNegeri = await Commonddl.dbGetPengadilanAHU("kota", "");
                }



                //if (Common.ddlProvinsi == null)
                //{
                //    Common.ddlProvinsi = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                //}
                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectDocStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlDocStatus);
                ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKelamin);
                ViewData["SelectJenisIdentitas"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisIdentitas);
                ViewData["SelectJenisKewarganegaraan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKewarganegaraan);
                ViewData["SelectPengadilanKota"] = OwinLibrary.Get_SelectListItem(Common.ddlPengadilanNegeri);


                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;


                System.Data.DataTable resultquery1 = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == idfdc).CopyToDataTable();
                string noperjanjian = resultquery1.Rows[0]["NoPerjanjian"].ToString();
                string IDFDCD = resultquery1.Rows[0]["IDFDC"].ToString();
                string ClientID = resultquery1.Rows[0]["ClientID"].ToString();
                string moduleID = HasKeyProtect.Decryption(caption);
                int docstatus = int.Parse(resultquery1.Rows[0]["StatusDoc"].ToString());
                int cont_type = int.Parse(resultquery1.Rows[0]["CONT_TYPE"].ToString());

                string IDFDCDTmpe = "";
                string noperjanjianTmpe = "";
                if (Pemberkasan.DocumentGroupType != null)
                {
                    IDFDCDTmpe = Pemberkasan.DocumentGroupType[0].IDFDC;
                    noperjanjianTmpe = Pemberkasan.DocumentGroupType[0].NoPerjanjian;

                }

                string Removeform = "";
                if ((Pemberkasan.DocumentGroupType == null) || (IDFDCD != IDFDCDTmpe && noperjanjian != noperjanjianTmpe && IDFDCDTmpe != "" && noperjanjianTmpe != ""))
                {
                    System.Data.DataTable resultquery = await Pemberkasanddl.dbGetDocumentPemberkasanJenis(IDFDCD, noperjanjian, ClientID, cont_type, moduleID, UserID, GroupName);
                    Removeform = "ok";
                    List<dynamic> dynamicListReturned = OwinLibrary.GetListFromDT(typeof(cDouemntsGroupType), resultquery);
                    Pemberkasan.DocumentGroupType = dynamicListReturned.Cast<cDouemntsGroupType>().ToList();
                }
                TempData["DocumentGroupType"] = Pemberkasan.DocumentGroupType;

                ViewBag.nokonview = resultquery1.Rows[0]["NoPerjanjian"].ToString() + "-" + IDFDCD + "-" + resultquery1.Rows[0]["NamaNasabah"].ToString();
                ViewBag.conttype = HasKeyProtect.Encryption(cont_type.ToString());

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    Removeformed = Removeform,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiPemberkasanCheckStatus.cshtml", Pemberkasan.DocumentGroupType),
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
        public async Task<ActionResult> clnPemberkasanUpdateStatus(List<HashNetFramework.cDouemntsGroupType> model, string keylokup, string parmode, string mustang, string resultextend = "", bool uploadberkas = false)
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

                //await Task.Delay(1);
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;
                Pemberkasan.DocumentGroupType = TempData["DocumentGroupType"] as List<cDouemntsGroupType>;


                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;


                //get value from aply filter //
                string SelectClient = modFilter.SelectClient;
                string SelectRegion = modFilter.SelectRegion;
                string SelectBranch = modFilter.SelectBranch;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan;
                string SelectDocStatus = modFilter.SelectDocStatus;
                string SelectNotaris = modFilter.SelectNotaris;
                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string NoPerjanjian = modFilter.NoPerjanjian;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
                bool Fill_Debitur = modFilter.Fill_Debitur;
                string Fill_Debitur_str = modFilter.Fill_Debitur_str;
                string StatusAkta = modFilter.StatusAkta;
                string NamaBPKBDebitur = modFilter.NamaBPKBDebitur;

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
                modFilter.SelectClient = SelectClient;
                modFilter.SelectRegion = SelectRegion;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectDocStatus = SelectDocStatus;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.Fill_Debitur = Fill_Debitur;
                modFilter.Fill_Debitur_str = Fill_Debitur_str;
                modFilter.StatusAkta = StatusAkta;
                modFilter.NamaBPKBDebitur = NamaBPKBDebitur;


                caption = HasKeyProtect.Decryption(caption);
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);

                int result = (int)ProccessOutput.needfile;
                string EnumMessage = "";

                int jmlfileuload = 0;

                DataRow selectrow = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylokup).SingleOrDefault();
                string noperjanjian = selectrow["NoPerjanjian"].ToString();
                string idfdc = selectrow["IDFDC"].ToString();
                string clientid = selectrow["clientid"].ToString();
                string JenisCust = selectrow["JenisNasabah"].ToString();
                string conttype = selectrow["cont_type"].ToString();
                if ((resultextend == "") && (uploadberkas == false))//handle refresh data setelah input upload file jika kosong proses update y
                {

                    EnumMessage = lgPemberkasan.CheckVlaidasiInput(model[0], SelectClient, SelectBranch);

                    if (EnumMessage == "")
                    {
                        int statusbefore = int.Parse(selectrow["StatusDoc"].ToString());
                        result = await Pemberkasanddl.dbupdatePemberkasanStatusDoc(model, Pemberkasan.DocumentGroupType, noperjanjian, idfdc, clientid, JenisCust, conttype, UserID, GroupName, caption, statusbefore);
                        if (result == 1 || result == 86)
                        {
                            Pemberkasan.DocumentGroupType = null;
                        }
                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Update Dokumen warkah", "tersimpan") : EnumMessage;
                    }
                    TempData["DocumentGroupType"] = Pemberkasan.DocumentGroupType;
                }
                else

                if (uploadberkas == true)
                {
                    //u transaksi saat add//
                    if (Pemberkasan.uploadSuccessFile != null)
                    {
                        jmlfileuload = Pemberkasan.uploadSuccessFile.Where(x => x.keylook == keylokup).Count();
                    }
                    int jmlfileharusupload = Pemberkasan.DocumentGroupType.Count;

                    //ambil jml file yang sempat diupload tapi belum di click proses warkah//
                    jmlfileuload = jmlfileuload + Pemberkasan.DocumentGroupType.Where(x => x.CheckValue == true).Count();

                    //jika jmlfileuload

                    if (jmlfileuload >= jmlfileharusupload)
                    {
                        result = 1;
                        Pemberkasan.uploadSuccessFile.RemoveAll(x => x.keylook == keylokup);
                    }

                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Dokumen warkah berhasil", "terkirim") : EnumMessage;
                }

                if (result == 1 || result == 86)
                {

                    //refresh//
                    var totalrecord = Pemberkasan.DTOrdersFromDB.AsEnumerable().Where(x => x.Field<string>("NoPerjanjian") != noperjanjian && x.Field<Int64>("IDFDC") != Int64.Parse(idfdc.ToString()) && x.Field<string>("clientid") == clientid);
                    Pemberkasan.DTOrdersFromDB = totalrecord.Count() == 0 ? null : totalrecord.CopyToDataTable();

                    if (Pemberkasan.DTOrdersFromDB == null)
                    {

                        ////get total data from server//
                        List<String> recordPage = await Pemberkasanddl.dbGetPemberkasanListCount(SelectClient, SelectRegion, SelectBranch, SelectNotaris, NoPerjanjian, SelectJenisKontrak, SelectDocStatus, SelectJenisPelanggan, fromdate, todate, "", Fill_Debitur_str, NamaBPKBDebitur, StatusAkta, PageNumber, caption, UserID, GroupName);
                        TotalRecord = Convert.ToDouble(recordPage[0]);
                        TotalPage = Convert.ToDouble(recordPage[1]);
                        pagingsizeclient = Convert.ToDouble(recordPage[2]);
                        pagenumberclient = PageNumber;


                    }
                    else
                    {
                        double record = Pemberkasan.DTOrdersFromDB.Rows.Count;
                        if ((pagenumberclient * pagingsizeclient) >= record)
                        {
                            pagenumberclient = Math.Ceiling(record / pagingsizeclient);
                            TotalPage = Math.Ceiling(record / pagingsizeclient);
                        }
                    }

                    List<System.Data.DataTable> dtlist = await Pemberkasanddl.dbGetPemberkasanList(null, SelectClient, SelectRegion, SelectBranch, SelectNotaris, NoPerjanjian, SelectJenisKontrak, SelectDocStatus, SelectJenisPelanggan, fromdate, todate, "", Fill_Debitur_str, NamaBPKBDebitur, StatusAkta, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    Pemberkasan.DTOrdersFromDB = dtlist[0];
                    Pemberkasan.DTDetailForGrid = dtlist[1];
                    Pemberkasan.DetailFilter = modFilter;

                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["PemberkasanList"] = Pemberkasan;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;



                }


                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiGridPemberkasanList.cshtml", Pemberkasan),
                    msg = EnumMessage,
                    resulted = result,
                    ketloop = keylokup
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
        #endregion Data transaksi

        public async Task<ActionResult> clnOpenAddPemberkasan(string paridno, string parkepo)
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

                //get session filterisasi //
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = HasKeyProtect.Decryption(caption);
                string IDFDCD = "0";
                string noperjanjian = "";
                string ClientID = "";
                string namanasabah = "";
                string jeniscontrak = "0";

                if (paridno != "add")
                {
                    DataRow resultquery = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paridno).SingleOrDefault();
                    IDFDCD = resultquery.Field<int>("IDFDC").ToString();
                    noperjanjian = resultquery.Field<string>("NoPerjanjian");
                    ClientID = resultquery.Field<string>("ClientID");
                    namanasabah = resultquery.Field<string>("NamaNasabah");


                    System.Data.DataTable dt = await Pemberkasanddl.dbGetDocumentPemberkasanJenis(IDFDCD, noperjanjian, ClientID, 0, moduleID, UserID, GroupName);
                    List<dynamic> dynamicListReturned = OwinLibrary.GetListFromDT(typeof(cDouemntsGroupType), dt);
                    Pemberkasan.DocumentGroupType = dynamicListReturned.Cast<cDouemntsGroupType>().ToList();

                    TempData["PemberkasanList"] = Pemberkasan;
                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["common"] = Common;

                    string datakosong = HasKeyProtect.Encryption("");
                    @ViewBag.titiled = "(NoPerjanjian :" + noperjanjian + " -" + namanasabah + ")";
                    @ViewBag.opr = "edt";
                }
                else
                {

                    Pemberkasan.DocumentGroupType = new List<cDouemntsGroupType>();

                    if ((parkepo ?? "") != "")
                    {
                        DataRow resultquery = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == parkepo).SingleOrDefault();
                        IDFDCD = resultquery.Field<Int64>("IDFDC").ToString();
                        noperjanjian = resultquery.Field<string>("NoPerjanjian");
                        ClientID = resultquery.Field<string>("ClientID");
                        namanasabah = resultquery.Field<string>("NamaNasabah");
                        jeniscontrak = resultquery.Field<Int32>("CONT_TYPE").ToString();
                    }
                    else
                    {
                        cDouemntsGroupType docadd = new cDouemntsGroupType();
                        docadd.NoPerjanjian = "";
                        docadd.JenisDocument = "";
                        docadd.SelectJenisKontrak = "";
                        Pemberkasan.DocumentGroupType.Add(docadd);
                    }

                    if (Common.ddlJenisDokumen == null)
                    {
                        Common.ddlJenisDokumen = await Commonddl.dbddlgetJenisDokumenList("");
                    }
                    if (Common.ddlJenisKontrak == null)
                    {
                        Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                    }

                    ViewData["SelectDocument"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);
                    ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

                    Pemberkasan.ddlJenisDokumen = Common.ddlJenisDokumen;

                    TempData["PemberkasanList"] = Pemberkasan;
                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["common"] = Common;
                    @ViewBag.opr = "add";
                    @ViewBag.parno = parkepo;
                }


                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = noperjanjian,
                    ops2 = namanasabah,
                    ops3 = jeniscontrak,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiPemberkasanUplod.cshtml", Pemberkasan.DocumentGroupType),
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


        #region API DIS
        public async Task<ActionResult> clnPemberkasanMap(string menu, string caption)
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
                //var list = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).ToList();
                bool AllowBrowse = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(x => x.AllowBrowse).SingleOrDefault();
                // extend //

                // some field must be overide first for default filter//
                string SelectClient = ClientID;
                string SelectBranch = IDCabang;
                string SelectNotaris = IDNotaris;
                string SelectDocStatus = "";
                string SelectJenisPelanggan = "";
                string SelectJenisDoc = "";
                string SelectJenisKontrak = "";
                string fromdate = "";
                string todate = "";
                string NoPerjanjian = "";

                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                modFilter.UserID = UserID;
                modFilter.UserName = UserName;
                modFilter.ClientLogin = ClientID;
                modFilter.BranchLogin = IDCabang;
                modFilter.NotarisLogin = IDNotaris;
                modFilter.RegionLogin = Region;
                modFilter.GroupName = GroupName;
                modFilter.ClientName = ClientName;
                modFilter.CabangName = CabangName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;

                modFilter.SelectClient = SelectClient;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.SelectDocStatus = SelectDocStatus;
                modFilter.JenisDocument = SelectJenisDoc;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.AllowAccess = AllowBrowse;

                modFilter.PageNumber = PageNumber;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);

                modFilter.CrunchCiber = true;
                List<String> recordPage = await Pemberkasanddl.dbGetPemberkasanMappingListCount(modFilter);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;

                //set in filter for paging//
                modFilter.CrunchCiber = false;

                List<System.Data.DataTable> dtlist = await Pemberkasanddl.dbGetPemberkasanMappingList(null, modFilter, pagenumberclient, pagingsizeclient);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                //set to object pendataran//
                Pemberkasan.DTOrdersFromDB = dtlist[0];
                Pemberkasan.DTDetailForGrid = dtlist[1];
                Pemberkasan.DetailFilter = modFilter;
                Pemberkasan.Permission = PermisionModule;

                //set session filterisasi //
                Pemberkasan.DocumentGroupType = null;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["PemberkasanListFilter"] = modFilter;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription + " map";
                ViewBag.rute = "Pemberkasan";
                ViewBag.action = "clnPemberkasanMap";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Dokumen";

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/uiPemberkasanMap.cshtml", Pemberkasan),
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
        public async Task<ActionResult> clnPemberkasaniptsv(HttpPostedFileBase[] files, List<HashNetFramework.cDouemntsGroupType> JENISDOC)
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
            Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
            modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
            Common = (TempData["common"] as vmCommon);
            Common.ddlJenisDokumen = Pemberkasan.ddlJenisDokumen;

            try
            {
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = HasKeyProtect.Decryption(caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();
                string MaxSizeFile = HasKeyProtect.DecryptionPass(OwinLibrary.GetMaxSzFile());
                int result = ValidationUpload.CheckValidationUpload(MaxSizeFile, files[0].FileName, (int)files[0].ContentLength);

                string namapelanggan = "";
                string IDFDCD = "0";
                string OverKontrak = "";
                string noperjanjian = "";
                string jeniskontrak = "0";

                parcno = JENISDOC[0].JENIS_DOCUMENT.Split('|')[0].ToString();
                noperjanjian = JENISDOC[0].JENIS_DOCUMENT.Split('|')[2].ToString();
                OverKontrak = JENISDOC[0].JENIS_DOCUMENT.Split('|')[1].ToString();
                jeniskontrak = JENISDOC[0].JENIS_DOCUMENT.Split('|')[3].ToString();

                //if ((parcno ?? "") != "")
                //{
                //    DataRow resultquery = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == parcno).SingleOrDefault();
                //    IDFDCD = resultquery.Field<Int64>("IDFDC").ToString();
                //    noperjanjian = resultquery.Field<string>("NoPerjanjian");
                //    string ClientID = resultquery.Field<string>("ClientID");
                //    namapelanggan = resultquery.Field<string>("NamaNasabah");
                //    jeniskontrak = resultquery.Field<int>("CONT_TYPE").ToString();
                //}
                //|| (namapelanggan ?? "") == ""

                string validmsg = "";
                if ((noperjanjian ?? "") == "" || (jeniskontrak ?? "") == "")
                {
                    validmsg = "Isikan no perjanjian, Jenis Transaksi, silahkan upload kembali ";
                    result = -999999;
                }

                if (validmsg == "")
                {
                    //cek file harus ada pada JENISDOC
                    string data = JENISDOC.Where(x => x.JENIS_DOCUMENT.Contains(files[0].FileName)).SingleOrDefault().JENIS_DOCUMENT;
                    if ((data ?? "") != "")
                    {
                        string key = data.Split('|')[0];
                        Jenis_Doc = Common.ddlJenisDokumen.Where(x => x.Value == key).SingleOrDefault().Text;
                        //get byte file//
                        byte[] imagebyte = null;
                        BinaryReader reader = new BinaryReader(files[0].InputStream);
                        imagebyte = reader.ReadBytes((int)files[0].ContentLength);

                        //get mimne type
                        string mimeType = files[0].ContentType;
                        if (mimeType.Contains("image"))
                        {
                            imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                        }

                        //prepare to encrypt
                        string KECEP = "dodol";
                        string KECEPDB = KECEP;
                        KECEP = HasKeyProtect.Encryption(KECEP);
                        byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                        //convert byte to base//
                        string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                        //set variable for db
                        string FILE_NAME_PRM = files[0].FileName;
                        string CONTENT_TYPE_PRM = mimeType;
                        decimal CONTENT_LENGTH_PRM = imagebyte.Length;
                        string FILE_BYTE_PRM = base64String;
                        string SOURCE_PATH_PRM = files[0].FileName;
                        string USERLOG_PRM = "AppFDCMDudul";
                        DateTime LASTWRITE_FILE_PRM = DateTime.Now;

                        jeniskontrak = (jeniskontrak ?? "") == "" ? "0" : jeniskontrak;
                        string cabang = (noperjanjian ?? "") != "" ? noperjanjian.Substring(0, 3) : "";
                        noperjanjian = noperjanjian ?? "";

                        cDocuments listDoc = new cDocuments();
                        listDoc.ID = 0;
                        listDoc.CLIENT_FDC_ID = double.Parse(IDFDCD);
                        listDoc.CONT_NO = noperjanjian;
                        listDoc.CONT_TYPE = int.Parse(jeniskontrak);
                        listDoc.BRCH_ID = cabang;
                        listDoc.REGION_ID = "";
                        listDoc.GRTE_NAME = namapelanggan;
                        listDoc.CLNT_ID = "001";
                        listDoc.DOCUMENT_TYPE = Jenis_Doc;
                        listDoc.FILE_NAME = FILE_NAME_PRM;
                        listDoc.CONTENT_TYPE = CONTENT_TYPE_PRM;
                        listDoc.CONTENT_LENGTH = CONTENT_LENGTH_PRM;
                        listDoc.FILE_BYTE = FILE_BYTE_PRM;
                        listDoc.SOURCE_PATH = SOURCE_PATH_PRM;
                        listDoc.USERLOG = USERLOG_PRM;
                        listDoc.LASTWRITE_FILE = LASTWRITE_FILE_PRM;
                        listDoc.ISECP = true;
                        listDoc.chanmod = HasKeyProtect.Encryption(moduleID);
                        listDoc.chalowses = AllowUpload;
                        listDoc.gruples = GroupName;
                        listDoc.USERLOGIN = UserID;

                        result = await Commonddl.dbUploadDokumen(listDoc);
                    }
                    else
                    {
                        result = (int)ProccessOutput.filenotvaliadhack;
                    }
                }
                //var selctrow = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylokup).SingleOrDefault();
                //string noperjanjian = selctrow.Field<string>("NoPerjanjian");

                string EnumMessage = (validmsg ?? "") == "" ? EnumsDesc.GetDescriptionEnums((ProccessOutput)result) : validmsg;
                EnumMessage = result == 1 ? "OK" : EnumMessage;


                if (result == 1)
                {
                    cUploadFileSuccess filds = new cUploadFileSuccess();
                    //filds.keylook = keylokup;
                    //filds.noperjanjian = noperjanjian;
                    //filds.jmlfileupload = 1;

                    //if (Pemberkasan.uploadSuccessFile == null)
                    //{
                    //    Pemberkasan.uploadSuccessFile = new List<cUploadFileSuccess>();
                    //}
                    //Pemberkasan.uploadSuccessFile.Add(filds);
                }

                ////send to session for filter data//
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;

                //send back to client browser//
                List<cupload> lstUploadReults = new List<cupload>();
                cupload addfile = new cupload()
                {
                    name = files[0].FileName + "            ( JENIS DOKUMEN : " + Jenis_Doc + " )",
                    size = files[0].ContentLength,
                    thumbnailUrl = "Content/assets/pages/img/fileicon.png",
                    type = files[0].ContentType,
                    error = EnumMessage
                };

                lstUploadReults.Add(addfile);

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    files = lstUploadReults,
                });

            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                string EnumMessage = "Waktu Penggunaan Applikasi Habis, Silahkan Login ulang dan upload kembali";
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                if (IsErrorTimeout == false)
                {
                    ////send to session for filter data//
                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["PemberkasanList"] = Pemberkasan;
                    TempData["common"] = Common;

                    EnumMessage = "File yang anda upload mengalami kendala, silahkan batalkan dan coba kembali";
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }

                //send back to client browser//
                List<cupload> lstUploadReults = new List<cupload>();
                cupload addfile = new cupload()
                {
                    name = files[0].FileName + "            ( JENIS DOKUMEN : " + Jenis_Doc + " )",
                    size = files[0].ContentLength,
                    thumbnailUrl = "Content/assets/pages/img/fileicon.png",
                    type = files[0].ContentType,
                    error = EnumMessage
                };

                lstUploadReults.Add(addfile);

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    files = lstUploadReults,
                });


            }
        }
        public async Task<ActionResult> clnCheckPemberkasan(string secnocon, string mode, string tryfil = "")
        {

            Byte[] res = null;
            string EnumMessage = "";
            string filenamevar = "View warkah";

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
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;
                Pemberkasan.CheckWithKey = "loadfile";

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                System.Data.DataTable dtr = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == secnocon).CopyToDataTable();
                string noperjanjian = dtr.Rows[0]["NoPerjanjian"].ToString();
                string cont_type = dtr.Rows[0]["CONT_TYPE"].ToString();
                double client_fdc_id = double.Parse(dtr.Rows[0]["IDFDC"].ToString());
                string namanasabah = dtr.Rows[0]["NamaNasabah"].ToString();
                string OverKontrak = dtr.Rows[0]["OverKontrak"].ToString();

                string IDFDCD = dtr.Rows[0]["IDFDC"].ToString();
                string ClientID = dtr.Rows[0]["ClientID"].ToString();
                string moduleID = HasKeyProtect.Decryption(caption);
                int docstatus = int.Parse(dtr.Rows[0]["StatusDoc"].ToString());

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowGenerate).SingleOrDefault();

                //Pemberkasan.DocumentGroupType = null;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["common"] = Common;

                if (Common.ddlDocStatus == null)
                {
                    Common.ddlDocStatus = await Commonddl.dbddlgetparamenumsList("DocStatus");
                }

                if (Common.ddlJenisPelanggan == null)
                {
                    Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
                }

                if (Common.ddlJenisKelamin == null)
                {
                    Common.ddlJenisKelamin = await Commonddl.dbddlgetparamenumsList("JNSKLM");
                }

                if (Common.ddlJenisIdentitas == null)
                {
                    Common.ddlJenisIdentitas = await Commonddl.dbddlgetparamenumsList("JNSIDEN");
                }

                if (Common.ddlJenisKewarganegaraan == null)
                {
                    Common.ddlJenisKewarganegaraan = await Commonddl.dbddlgetparamenumsList("JNSWRG");
                }

                if (Common.ddlPengadilanNegeri == null)
                {
                    Common.ddlPengadilanNegeri = await Commonddl.dbGetPengadilanAHU("kota", "");
                }


                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectDocStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlDocStatus);
                ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKelamin);
                ViewData["SelectJenisIdentitas"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisIdentitas);
                ViewData["SelectJenisKewarganegaraan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKewarganegaraan);
                ViewData["SelectPengadilanKota"] = OwinLibrary.Get_SelectListItem(Common.ddlPengadilanNegeri);


                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;

                string IDFDCDTmpe = "";
                string noperjanjianTmpe = "";
                string ContypeTmpe = "";
                if (Pemberkasan.DocumentGroupType != null)
                {
                    IDFDCDTmpe = Pemberkasan.DocumentGroupType[0].IDFDC;
                    noperjanjianTmpe = Pemberkasan.DocumentGroupType[0].NoPerjanjian;
                    ContypeTmpe = Pemberkasan.DocumentGroupType[0].SelectJenisKontrak;
                }

                string Removeform = "";
                //IDFDCD != IDFDCDTmpe &&
                if (Pemberkasan.DocumentGroupType == null || noperjanjian != (noperjanjianTmpe ?? "") || IDFDCD.ToString() != (IDFDCDTmpe ?? ""))
                {
                    System.Data.DataTable resultquery = await Pemberkasanddl.dbGetDocumentPemberkasanJenis(IDFDCD, noperjanjian, ClientID, int.Parse(cont_type), moduleID, UserID, GroupName);
                    Removeform = "ok";
                    List<dynamic> dynamicListReturned = OwinLibrary.GetListFromDT(typeof(cDouemntsGroupType), resultquery);
                    Pemberkasan.DocumentGroupType = dynamicListReturned.Cast<cDouemntsGroupType>().ToList();
                }
                TempData["DocumentGroupType"] = Pemberkasan.DocumentGroupType;

                ViewBag.nokonview = "No Perjanjian : " + dtr.Rows[0]["NoPerjanjian"].ToString() + " - " + IDFDCD + " - " + dtr.Rows[0]["NamaNasabah"].ToString() + (OverKontrak.ToLower() == "true" ? "(OverKontrak)" : "");

                string infokon = ViewBag.nokonview;
                ViewBag.conttype = HasKeyProtect.Encryption(cont_type.ToString());
                ViewBag.IDFDCD = IDFDCD;
                ViewBag.contno = noperjanjian;
                ViewBag.Lookup = secnocon;

                string viewhml = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_FileSelectedBerkas.cshtml", Pemberkasan.DocumentGroupType);

                var contenttypeed = "application/pdf";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?paridfdc=" + secnocon + "&parmod=" + mode + "&parpowderdockd=wako&infokon=" + infokon + "&file=";
                var jsonresult = Json(new { view = viewhml, moderror = IsErrorTimeout, infoselect = "1", bytetyipe = res, msg = EnumMessage, contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
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
        public async Task<ActionResult> clnCheckPemberkasanChkfle(string secnocon, string coontpe, string clnfdc)
        {

            string EnumMessage = "";
            string filenamevar = "View warkah";
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
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData["PemberkasanList"] = Pemberkasan;
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["common"] = Common;

                //sumber data pemberkasan //

                string caption = modFilter.idcaption;
                string moduleID = HasKeyProtect.Decryption(caption);
                string mode = moduleID;

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowGenerate).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                if (Pemberkasan.CheckWithKey == "loadfile")
                {
                    TempData["dtfile"] = null;
                    Pemberkasan.CheckWithKey = "";
                }
                //cek pada sesion terlebih dulu
                List<cDocumentsGet> dttemp = (TempData["dtfile"] as List<cDocumentsGet>);

                string infokon = "";
                string infofisrst = "";

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
                        dttempx = dttemp.AsEnumerable().Where(x => x.cont_no == secnocon && x.cont_type == HasKeyProtect.Decryption(coontpe)).Count();
                        if (dttempx == 0)
                        {
                            idicategetdb = 0;
                        }
                    }
                }
                if (idicategetdb == 0)
                {
                    //load ke DB jika disesion tidak ada//
                    modFilter.ID = ID;
                    modFilter.NoPerjanjian = secnocon;
                    modFilter.SelectJenisKontrak = HasKeyProtect.Decryption(coontpe);
                    modFilter.secIDFDC = clnfdc;
                    dttemp = await Commonddl.dbGetViewDokumen(modFilter);
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

                if (dttemp.Count > 0)
                {

                    if (publicontype == "chk")
                    {
                        foreach (cDocumentsGet s in dttemp)
                        {
                            infokon = dttemp[0].cont_no.ToString();
                            byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                            string pooo = s.CREATED_BY.ToString();
                            pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                            string KECEP = HasKeyProtect.Encryption(pooo);
                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                            res = imageBytes;
                        }
                    }

                    if (infofisrst == "" && publicontype != "chk")
                    {
                        iTextSharp.text.pdf.PdfReader finalPdf;
                        iTextSharp.text.Document pdfContainer;
                        PdfWriter pdfCopy;
                        MemoryStream msFinalPdf = new MemoryStream();

                        pdfContainer = new iTextSharp.text.Document();
                        pdfCopy = new PdfSmartCopy(pdfContainer, msFinalPdf);
                        pdfContainer.Open();

                        int pageded = 0;
                        foreach (cDocumentsGet s in dttemp)
                        {

                            pageded = pageded + 1;
                            byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                            string pooo = s.CREATED_BY.ToString();
                            pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                            string KECEP = HasKeyProtect.Encryption(pooo);
                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);

                            finalPdf = new iTextSharp.text.pdf.PdfReader(imageBytes);
                            for (int i = 1; i < finalPdf.NumberOfPages + 1; i++)
                            {
                                ((PdfSmartCopy)pdfCopy).AddPage(pdfCopy.GetImportedPage(finalPdf, i));
                            }
                            pdfCopy.FreeReader(finalPdf);

                            if (pageded == dttemp.Count)
                            {
                                finalPdf.Close();
                            }
                        }
                        pdfCopy.Close();
                        pdfContainer.Close();
                        res = msFinalPdf.ToArray();
                    }
                }
                else
                {

                    EnumMessage = "Dokumen tidak ditemukan";
                }



                var contenttypeed = "application/pdf";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?paridfdc=" + secnocon + "&parmod=" + mode + "&parpowderdockd=wako&infokon=" + infokon + "&file=";
                //viewpathed = "data:application/pdf;base64," + base4;
                var jsonresult = Json(new { view = viewhml, moderror = IsErrorTimeout, infoselect = infofisrst, bytetyipe = res, msg = EnumMessage, contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
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


        public async Task<ActionResult> clnSveDocMapping(string parcno, string contenap, string idup, string tpe, string overcnt)
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

                string keylokup = parcno;
                string Jenis_Doc = "";
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common.ddlJenisDokumen = Pemberkasan.ddlJenisDokumen;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = HasKeyProtect.Decryption(caption);
                bool AllowBrowse = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowBrowse).SingleOrDefault();

                int result = 0;
                string validmsg = "";
                string noperjanjian = "";
                string namapelanggan = "";
                string cabangName = ""; ;
                string region = "";


                if ((tpe ?? "") == "")
                {
                    //cek data pada FDCM dulu//
                    System.Data.DataTable dt = await Pemberkasanddl.dbGetDocumentPemberkasan4map(contenap, parcno, "", overcnt, moduleID, UserID, GroupName);
                    if (dt.Rows.Count > 1)
                    {
                        foreach (DataRow rw in dt.Rows)
                        {

                            noperjanjian = rw.Field<string>("cont_no");
                            string ClientID = rw.Field<string>("CLNT_ID");
                            namapelanggan = rw.Field<string>("GRTE_NAME");
                            string jeniskontrak = rw.Field<int>("cont_type").ToString();
                            string cabang = rw.Field<string>("BRCH_ID").ToString();
                            cabangName = rw.Field<string>("BRCH_NAME").ToString();
                            region = rw.Field<string>("REGION").ToString();

                            validmsg = validmsg + noperjanjian + "|" + namapelanggan + "|" + cabangName + "\n";
                        }
                    }

                    if (dt.Rows.Count == 0)
                    {
                        validmsg = "No Kontrak tidak ditemukan , silahkan cek data";
                    }


                    if (validmsg == "")
                    {
                        string IDFDCD = dt.Rows[0]["client_fdc_id"].ToString();
                        noperjanjian = dt.Rows[0]["cont_no"].ToString();
                        string ClientID = dt.Rows[0]["CLNT_ID"].ToString();
                        namapelanggan = dt.Rows[0]["GRTE_NAME"].ToString();
                        string jeniskontrak = dt.Rows[0]["cont_type"].ToString();
                        string cabang = dt.Rows[0]["BRCH_ID"].ToString();
                        cabangName = dt.Rows[0]["BRCH_NAME"].ToString();
                        region = dt.Rows[0]["REGION"].ToString();

                        cDocuments listDoc = new cDocuments();
                        listDoc.ID = int.Parse(idup);
                        listDoc.CLIENT_FDC_ID = double.Parse(IDFDCD);
                        listDoc.CONT_NO = noperjanjian;
                        listDoc.CONT_TYPE = int.Parse(jeniskontrak);
                        listDoc.BRCH_ID = cabang;
                        listDoc.BRCH_NAME = cabangName;
                        listDoc.REGION_ID = region;
                        listDoc.GRTE_NAME = namapelanggan;
                        listDoc.CLNT_ID = ClientID;
                        listDoc.DOCUMENT_TYPE = Jenis_Doc;
                        listDoc.chanmod = HasKeyProtect.Encryption(moduleID);
                        listDoc.USERLOGIN = UserID;
                        listDoc.chalowses = AllowBrowse;
                        listDoc.gruples = GroupName;

                        result = await Commonddl.dbsvemapDokumen(listDoc, "");
                    }
                }

                string msg = "Mapping Dokumen";
                if ((tpe ?? "") == "1")
                {
                    cDocuments listDoc = new cDocuments();
                    listDoc.ID = int.Parse(idup);
                    listDoc.USERLOGIN = UserID;
                    listDoc.chalowses = AllowBrowse;
                    listDoc.chanmod = HasKeyProtect.Encryption(moduleID);
                    listDoc.gruples = GroupName;
                    result = await Commonddl.dbsvemapDokumen(listDoc, "1");
                    msg = "Hapus Dokumen";
                }

                string EnumMessage = (validmsg ?? "") == "" ? EnumsDesc.GetDescriptionEnums((ProccessOutput)result) : validmsg;
                EnumMessage = result == 1 ? msg + " Berhasil" : EnumMessage;

                ////send to session for filter data//
                TempData["PemberkasanListFilter"] = modFilter;
                TempData["PemberkasanList"] = Pemberkasan;
                TempData["common"] = Common;


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    resulted = result,
                    nocont = noperjanjian,
                    nopel = namapelanggan,
                    reg = region,
                    cab = cabangName,
                    msg = EnumMessage
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
        public async Task<ActionResult> clnListFilterPemberkasaanMap(cFilterContract model, string download)
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
                modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
                Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = model.NoPerjanjian ?? "";
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string SelectRegion = model.SelectRegion ?? modFilter.RegionLogin;
                string SelectBranch = model.SelectBranch ?? modFilter.BranchLogin;
                SelectBranch = SelectBranch.Length <= 4 ? HasKeyProtect.Encryption(SelectBranch) : SelectBranch;
                string SelectNotaris = model.SelectNotaris ?? modFilter.NotarisLogin;
                string fromdate = model.fromdate ?? "";
                string todate = model.todate ?? "";
                string SelectDocStatus = model.SelectDocStatus ?? "";
                string SelectJenisPelanggan = model.SelectJenisPelanggan ?? "";
                string SelectJenisKontrak = model.SelectJenisKontrak ?? "";

                //set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;
                bool isModeFilter = true;

                //set filter//
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.SelectClient = SelectClient;
                modFilter.SelectRegion = SelectRegion;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectDocStatus = SelectDocStatus;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = lgPemberkasan.CheckFilterisasiDataUplod(modFilter, download);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                    caption = HasKeyProtect.Decryption(caption);

                    modFilter.CrunchCiber = true;
                    List<String> recordPage = await Pemberkasanddl.dbGetPemberkasanMappingListCount(modFilter);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set in filter for paging//
                    modFilter.CrunchCiber = false;

                    List<System.Data.DataTable> dtlist = await Pemberkasanddl.dbGetPemberkasanMappingList(null, modFilter, pagenumberclient, pagingsizeclient);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                    //set in filter for paging//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    //set to object pemberkasan//
                    Pemberkasan.DTOrdersFromDB = dtlist[0];
                    Pemberkasan.DTDetailForGrid = dtlist[1];
                    Pemberkasan.DetailFilter = modFilter;

                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["PemberkasanList"] = Pemberkasan;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/_uiGridPemberkasanListMap.cshtml", Pemberkasan),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {

                    TempData["PemberkasanListFilter"] = modFilter;
                    TempData["PemberkasanList"] = Pemberkasan;
                    TempData["common"] = Common;
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

        #endregion API DIS

        void SplitePDF(string filepath)
        {
            iTextSharp.text.pdf.PdfReader reader = null;
            int currentPage = 1;
            int pageCount = 0;
            //string filepath_New = filepath + "\\PDFDestination\\";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            //byte[] arrayofPassword = encoding.GetBytes(ExistingFilePassword);
            reader = new iTextSharp.text.pdf.PdfReader(filepath);
            reader.RemoveUnusedObjects();
            pageCount = reader.NumberOfPages;
            string ext = System.IO.Path.GetExtension(filepath);
            for (int i = 1; i <= pageCount; i++)
            {
                iTextSharp.text.pdf.PdfReader reader1 = new iTextSharp.text.pdf.PdfReader(filepath);
                string outfile = filepath.Replace((System.IO.Path.GetFileName(filepath)), (System.IO.Path.GetFileName(filepath).Replace(".pdf", "") + "_" + i.ToString()) + ext);
                reader1.RemoveUnusedObjects();
                iTextSharp.text.Document doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentPage));
                iTextSharp.text.pdf.PdfCopy pdfCpy = new iTextSharp.text.pdf.PdfCopy(doc, new System.IO.FileStream(outfile, System.IO.FileMode.Create));
                doc.Open();
                for (int j = 1; j <= 1; j++)
                {
                    iTextSharp.text.pdf.PdfImportedPage page = pdfCpy.GetImportedPage(reader1, currentPage);
                    //pdfCpy.SetFullCompression();
                    pdfCpy.AddPage(page);
                    currentPage += 1;
                }
                doc.Close();
                pdfCpy.Close();
                reader1.Close();
                reader.Close();

            }
        }

        #region Request Cetak akta
        public async Task<ActionResult> clnPemberkasanAkta(string menu, string caption)
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
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                //// some field must be overide first for default filter//
                string SelectClient = ClientID;
                string SelectRequest = "";
                string SelectRequestStatus = "0";
                string fromdate = "";

                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                modFilter.UserID = UserID;
                modFilter.UserName = UserName;
                modFilter.ClientLogin = ClientID;
                modFilter.BranchLogin = IDCabang;
                modFilter.NotarisLogin = IDNotaris;
                modFilter.RegionLogin = Region;
                modFilter.GroupName = GroupName;
                modFilter.ClientName = ClientName;
                modFilter.CabangName = CabangName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;

                modFilter.SelectClient = SelectClient;
                modFilter.fromdate = fromdate;
                modFilter.SelectRequest = SelectRequest;
                modFilter.SelectRequestStatus = SelectRequestStatus;

                modFilter.PageNumber = PageNumber;

                ////descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);

                List<String> recordPage = await Pemberkasanddl.dbGetPemberkasanAktaListCount(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<System.Data.DataTable> dtlist = await Pemberkasanddl.dbGetPemberkasanAktaList(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                ////set to object pendataran//
                Pemberkasan.DTOrdersFromDB = dtlist[0];
                Pemberkasan.DTDetailForGrid = dtlist[1];
                //VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                Pemberkasan.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["PemberkasanAktaListTxt"] = Pemberkasan;
                TempData["PemberkasanAktaListFilterTxt"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Pemberkasan";
                ViewBag.action = "clnPemberkasanAkta";



                string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;


                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pemberkasan/uiPemberkasanAkta.cshtml", Pemberkasan),
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
        #endregion Request Cetak akta



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AttactmentdownloadDirectSvr(string idfdc)
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



            //get filter data from session before//
            modFilter = TempData["PemberkasanListFilter"] as cFilterContract;
            Pemberkasan = TempData["PemberkasanList"] as vmPemberkasan;
            Common = (TempData["common"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            //set back filter data from session before//
            TempData["PendaftaranListFilter"] = modFilter;
            TempData["PendaftaranList"] = Pemberkasan;
            TempData["common"] = Common;

            // get user group & akses //
            string UserID = modFilter.UserID;
            string GroupName = modFilter.GroupName;
            string caption = modFilter.idcaption;

            //extend//
            cAccountMetrik Metrik = Account.AccountMetrikList.Where(x => x.SecModuleID == caption).SingleOrDefault();
            bool AllowPrint = Metrik.AllowPrint;
            bool AllowDownload = Metrik.AllowDownload;

            ////deript for db//
            //string ClientID = HasKeyProtect.Decryption(modFilter.ClientLogin);
            //string IDNotaris = HasKeyProtect.Decryption(modFilter.NotarisLogin);
            //string IDCabang = HasKeyProtect.Decryption(modFilter.BranchLogin);
            //string SecureModuleId = HasKeyProtect.Decryption(modFilter.idcaption);
            //string Email = HasKeyProtect.Decryption(modFilter.MailerDaemoon);
            //string UserGenCode = HasKeyProtect.Decryption(modFilter.GenDeamoon);
            System.Data.DataTable resultquery1 = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == idfdc).CopyToDataTable();
            string noperjanjian = resultquery1.Rows[0]["NoPerjanjian"].ToString();
            string IDFDCD = resultquery1.Rows[0]["IDFDC"].ToString();
            string ClientID = resultquery1.Rows[0]["ClientID"].ToString();
            string moduleID = HasKeyProtect.Decryption(caption);
            int cont_type = int.Parse(resultquery1.Rows[0]["CONT_TYPE"].ToString());


            byte[] buffer = await Pemberkasanddl.dbGetPemberkasanDwnwrkhnotaris(noperjanjian, IDFDCD, cont_type.ToString(), "DOCNOTARIS", moduleID, UserID, GroupName);

            //////set login key//
            //string LoginAksesKey = UserID + Email + UserGenCode;

            //DataTable dataupload = new DataTable();
            //dataupload.Columns.Add("CONT_TYPE", Type.GetType("System.Int32"));
            //dataupload.Columns.Add("CLIENT_FDC_ID", Type.GetType("System.Int64"));
            //dataupload.Columns.Add("CONT_NO", Type.GetType("System.String"));
            //dataupload.Columns.Add("GRTE_NAME", Type.GetType("System.String"));
            //dataupload.Columns.Add("CLNT_ID", Type.GetType("System.String"));
            //dataupload.Columns.Add("NTRY_ID", Type.GetType("System.String"));
            //dataupload.Columns.Add("REQUEST_NO", Type.GetType("System.String"));
            //dataupload.Columns.Add("NO_DOCUMENT", Type.GetType("System.String"));

            //List<string> ListIDgrd = new List<string>();
            //var ij = 0;
            //string keylookup = "";

            //dbAccessHelper dbaccess = new dbAccessHelper();
            //string strconnectionCFG = HasKeyProtect.DecryptionPass(OwinLibrary.GetDBCFG());
            ////get location upload FILE//
            //SqlParameter[] sqlParamx =
            //{
            //        new SqlParameter ("@key","DX"+IDCabang),
            //    };

            //DataTable dtx = await dbaccess.ExecuteDataTable(strconnectionCFG, "[udp_app_account_authconfigsempi]", sqlParamx);
            //string keybox = dtx.Rows[0][0].ToString();

            ////looping unutk  nokontrak yang dipilih , dininputkan ke table temp//
            //foreach (var aktasel in AktaSelectdwn)
            //{
            //    string[] valued = aktasel.Split('|');

            //    keylookup = valued[0].ToString();
            //    ListIDgrd.Add(keylookup);


            //    ij = ij + 1;

            //    DataRow resultquery = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).SingleOrDefault();
            //    if (resultquery != null)
            //    {
            //        dataupload.Rows.Add(new object[] { resultquery["CONT_TYPE"], resultquery["CLIENT_FDC_ID"], resultquery["NO_PERJANJIAN"], resultquery["NamaNasabah"], resultquery["CLNT_ID"], "", resultquery["NO_REQUEST"], resultquery["SERTIFIKAT_NO"] });
            //    }

            //}

            //System.Data.DataTable dt = new System.Data.DataTable();
            //List<cDocumentsGet> DDL = new List<cDocumentsGet>();
            //byte[] bytesToDecrypt = null;
            //byte[] buffer = null;

            // cFilterContract models = JsonConvert.DeserializeObject<cFilterContract>(model.ToString());
            //string contenttyped = "application/zip";
            //string filenamepar = "WARKAH_" + (models.NoPerjanjian ?? "NO_NUMBER") + ".zip";


            //dbAccessHelper dbaccess = new dbAccessHelper();
            //string strconnection = HasKeyProtect.DecryptionPass(OwinLibrary.GetDBDIS());

            string minut = DateTime.Now.ToString("ddMMyyyymmss");
            string filenamepar = "WARKAH_" + minut + ".zip";
            return File(buffer, "application/zip", filenamepar);


        }

    }
}
