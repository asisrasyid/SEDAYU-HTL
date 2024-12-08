using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using HashNetFramework;
using Ionic.Zip;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace DusColl.Controllers
{
    public class AktaController : Controller
    {

        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmAkta akta = new vmAkta();
        vmAktaddl aktaddl = new vmAktaddl();
        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        blAkta lgakta = new blAkta();



        #region Filter data
        public async Task<ActionResult> clnGetBranch(string clientid)
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
                modFilter = TempData["AktaListFilter"] as cFilterContract;
                akta = TempData["AktaList"] as vmAkta;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid] as IEnumerable<cListSelected>);

                string UserID = modFilter.UserID;

                //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                bool loaddata = false;

                //set field filter to varibale //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectBranch = modFilter.SelectBranch ?? modFilter.BranchLogin;

                if (SelectClient != clientid)
                {
                    SelectClient = clientid;
                    modFilter.SelectClient = SelectClient;
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
                        string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                        string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);
                        Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, "", UserID);
                        tempbrach = Common.ddlBranch;
                    }
                }

                TempData["tempbrach" + clientid] = tempbrach;

                TempData["AktaListFilter"] = modFilter;
                TempData["AktaList"] = akta;
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
                modFilter = TempData["AktaListFilter"] as cFilterContract;
                akta = TempData["AktaList"] as vmAkta;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectBranch = modFilter.SelectBranch;
                string SelectClient = modFilter.SelectClient;
                string SelectNotaris = modFilter.SelectNotaris;
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
                string NoAkta = modFilter.NoAkta ?? "";
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";

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

                if (Common.ddlNotaris == null)
                {
                    Common.ddlNotaris = await Commonddl.dbGetNotarisListByEncrypt();
                }

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);

                TempData["AktaList"] = akta;
                TempData["AktaListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectClient,
                    opsi2 = decSelectBranch,
                    opsi3 = SelectNotaris,
                    opsi4 = NoAkta,
                    opsi5 = NoPerjanjian,
                    opsi6 = fromdate,
                    opsi7 = todate,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/_uiFilterData.cshtml", akta.DetailFilter),
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
        #endregion Filter data

        #region Data Transaksi Akta
        [HttpPost]
        public async Task<ActionResult> clnViewAkta(String menu, String caption)
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
                bool CrunchCiber = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.AllowDownload).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                // some field must be overide first for default filter//
                string SelectClient = ClientID;
                string SelectBranch = IDCabang;
                string SelectNotaris = IDNotaris;
                string fromdate = "";
                string todate = "";
                string NoPerjanjian = "";
                string NoAkta = "";

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
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.NoAkta = NoAkta;

                modFilter.PageNumber = PageNumber;
                modFilter.CrunchCiber = CrunchCiber;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);


                // try show filter data//
                List<String> recordPage = await aktaddl.dbGetAktaListCount(SelectClient, SelectBranch, SelectNotaris, fromdate, todate, NoAkta, NoPerjanjian, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await aktaddl.dbGetAktaList(null, SelectClient, SelectBranch, SelectNotaris, fromdate, todate, NoAkta, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                akta.DTAktaFromDB = dtlist[0];
                akta.DTDetailForGrid = dtlist[1];
                akta.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["AktaList"] = akta;
                TempData["AktaListFilter"] = modFilter;


                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Akta";
                ViewBag.action = "clnViewAkta";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";


                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/uiAkta.cshtml", akta),
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
        public async Task<ActionResult> clnListFilterAkta(cFilterContract model, string download)
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
                modFilter = TempData["AktaListFilter"] as cFilterContract;
                akta = TempData["AktaList"] as vmAkta;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = model.NoPerjanjian ?? "";
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string SelectBranch = model.SelectBranch ?? modFilter.BranchLogin;
                SelectBranch = SelectBranch.Length <= 4 ? HasKeyProtect.Encryption(SelectBranch) : SelectBranch;
                string SelectNotaris = model.SelectNotaris ?? modFilter.NotarisLogin;
                string NoAkta = model.NoAkta ?? "";
                string fromdate = model.fromdate ?? "";
                string todate = model.todate ?? "";

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
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.NoAkta = NoAkta;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = lgakta.CheckFilterisasiDataGen(modFilter);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    caption = HasKeyProtect.Decryption(caption);


                    // try show filter data//
                    List<String> recordPage = await aktaddl.dbGetAktaListCount(SelectClient, SelectBranch, SelectNotaris, fromdate, todate, NoAkta, NoPerjanjian, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await aktaddl.dbGetAktaList(null, SelectClient, SelectBranch, SelectNotaris, fromdate, todate, NoAkta, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    akta.DTAktaFromDB = dtlist[0];
                    akta.DTDetailForGrid = dtlist[1];
                    akta.DetailFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["AktaListFilter"] = modFilter;
                    TempData["AktaList"] = akta;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/_uiGridAktaList.cshtml", akta),
                        download = "",
                        message = validtxt
                    });

                }
                else
                {

                    TempData["AktaListFilter"] = modFilter;
                    TempData["AktaList"] = akta;
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

        public async Task<ActionResult> clnAktaRgridList(int paged)
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
                modFilter = TempData["AktaListFilter"] as cFilterContract;
                akta = TempData["AktaList"] as vmAkta;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string NoPerjanjian = modFilter.NoPerjanjian;
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectNotaris = modFilter.SelectNotaris;
                string NoAkta = modFilter.NoAkta;
                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                caption = HasKeyProtect.Decryption(caption);


                // try show filter data//
                List<DataTable> dtlist = await aktaddl.dbGetAktaList(akta.DTAktaFromDB, SelectClient, SelectBranch, SelectNotaris, fromdate, todate, NoAkta, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                akta.DTDetailForGrid = dtlist[1];

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                //set session filterisasi //
                TempData["AktaList"] = akta;
                TempData["AktaListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/_uiGridAktaList.cshtml", akta),
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
        public async Task<ActionResult> clnCetakAkta(string[] AktaSelectdwn, string prevedid, string namaidpool)
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


                //get filter data from session before//
                modFilter = TempData["AktaListFilter"] as cFilterContract;
                akta = TempData["AktaList"] as vmAkta;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData["AktaListFilter"] = modFilter;
                TempData["AktaList"] = akta;
                TempData["common"] = Common;

                // get user group & akses //
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //extend//
                cAccountMetrik Metrik = Account.AccountMetrikList.Where(x => x.SecModuleID == caption).SingleOrDefault();
                bool AllowPrint = Metrik.AllowPrint;
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
                dataupload.Columns.Add("CONT_TYPE", Type.GetType("System.Int32"));
                dataupload.Columns.Add("CLIENT_FDC_ID", Type.GetType("System.Int64"));
                dataupload.Columns.Add("CONT_NO", Type.GetType("System.String"));
                dataupload.Columns.Add("CLNT_ID", Type.GetType("System.String"));
                dataupload.Columns.Add("NTRY_ID", Type.GetType("System.String"));
                dataupload.Columns.Add("NO_DOCUMENT", Type.GetType("System.String"));

                List<string> ListIDgrd = new List<string>();
                var ij = 0;
                string keylookup = "";

                //looping unutk  nokontrak yang dipilih , dininputkan ke table temp//
                foreach (var aktasel in AktaSelectdwn)
                {

                    string[] valued = aktasel.Split('|');

                    keylookup = valued[0].ToString();
                    ListIDgrd.Add(keylookup);

                    ij = ij + 1;

                    DataTable resultquery = akta.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).CopyToDataTable();
                    if (resultquery != null)
                    {
                        dataupload.Rows.Add(new object[] { resultquery.Rows[0]["CONT_TYPE"], resultquery.Rows[0]["FDC_ID"], resultquery.Rows[0]["NoPerjanjian"], resultquery.Rows[0]["KodeKlien"], "", resultquery.Rows[0]["KodeAkta"] });
                    }
                }

                string Jenisdoc = "SALINAN AKTA";
                // try to get document attacment //
                DataTable DocumentByte = await Commonddl.dbGetDocumentByno(dataupload, Jenisdoc, ClientID, IDCabang, UserID, GroupName, SecureModuleId, LoginAksesKey, Email, UserGenCode);

                //document akta ada //
                if ((DocumentByte.Rows.Count > 0))
                {
                    //Harus ada gen code untuk passwordd file//
                    if ((UserGenCode != ""))
                    {

                        var powderdockp = AllowPrint == true ? "1" : "0";
                        var powderdockd = AllowDownload == true ? "1" : "0"; // untuk handele downloa pada pdfvier//

                        string filenamepar = "";
                        string EnumMessage = "";
                        string contenttyped = "";
                        byte[] bytesToDecrypt = null;
                        byte[] buffer = null;
                        var viewpathed = "";

                        //preveid untuk menandatang hanya liat atau download//
                        if ((prevedid ?? "") == "")
                        {
                            ZipEntry newZipEntry = new ZipEntry();
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var zip = new ZipFile())
                                {
                                    zip.Password = HasKeyProtect.Encryption(LoginAksesKey);
                                    foreach (DataRow doc in DocumentByte.Rows)
                                    {
                                        // bytesToDecrypt = HasKeyProtect.SetFileByteDecrypt(doc.FILE_BYTE, LoginAksesKey);
                                        bytesToDecrypt = doc.Field<Byte[]>("FILE_BYTE");
                                        filenamepar = doc.Field<string>("NO_PERJANJIAN") + "_" + doc.Field<string>("NO_DOCUMENT") + "_" + Jenisdoc + ".pdf";
                                        zip.AddEntry(filenamepar, bytesToDecrypt);

                                    }

                                    zip.Save(memoryStream);
                                }
                                buffer = memoryStream.ToArray();
                            }

                            //update sesuai dengan data yang didapat harus join//
                            dataupload.Rows.Clear();
                            foreach (DataRow aktasel in DocumentByte.Rows)
                            {
                                dataupload.Rows.Add(new object[] { aktasel.Field<Int64>("CLIENT_FDC_ID"), aktasel.Field<string>("NO_PERJANJIAN"), aktasel.Field<string>("CLNT_ID"), "", aktasel.Field<string>("NO_DOCUMENT") });
                            }


                            int resultflag = await Commonddl.dbSettDocumentFlagByno(dataupload, Jenisdoc, ClientID, IDCabang, UserID, GroupName, SecureModuleId, LoginAksesKey, Email, UserGenCode);
                            //berhasil flag//
                            if (resultflag == 1)
                            {
                                string minut = DateTime.Now.ToString("ddMMyyyymmss");
                                contenttyped = "application/zip";
                                filenamepar = "SALINAN_AKTA_" + minut + ".zip";
                            }
                            else
                            {
                                return Json(new
                                {
                                    moderror = IsErrorTimeout,
                                    msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidFlagDownload))
                                });
                            }

                        }
                        else
                        {

                            foreach (DataRow doc in DocumentByte.Rows)
                            {
                                //bytesToDecrypt = HasKeyProtect.SetFileByteDecrypt(doc.FILE_BYTE, LoginAksesKey);
                                bytesToDecrypt = doc.Field<Byte[]>("FILE_BYTE");
                                filenamepar = doc.Field<string>("NO_PERJANJIAN") + "_" + doc.Field<string>("NO_DOCUMENT") + "_" + Jenisdoc + ".pdf";
                                contenttyped = doc.Field<string>("CONTENT_TYPE");
                            }


                            buffer = bytesToDecrypt;

                            viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=" + caption + "&file=";
                        }

                        return Json(new
                        {
                            moderror = IsErrorTimeout,
                            contenttype = contenttyped,
                            bytetyipe = buffer,
                            filename = filenamepar,
                            viewpath = viewpathed,
                            msg = EnumMessage,
                            idprev = prevedid,
                            namapool = namaidpool,
                            dolpet = ListIDgrd
                        });
                    }
                    else
                    {

                        return Json(new
                        {
                            moderror = IsErrorTimeout,
                            msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKunciSandiFile))
                        });

                    }

                }
                else
                {
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterNotValidFileSalinAktaNotFound))
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
        #endregion Data Transaksi Akta



        #region Transaksi Akta
        [HttpPost]
        public async Task<ActionResult> clnAkta(string menu, string caption)
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


                await Task.Delay(0).ConfigureAwait(false);
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


                // some field must be overide first for default filter//
                string decIDNotaris = HasKeyProtect.Decryption(IDNotaris);
                cAkta info = await aktaddl.dbGetOrderOutstandingCount(decIDNotaris);
                string SelectNotaris = IDNotaris;
                string TglOrder = DateTime.Now.ToString("dd-MMMM-yyyy");
                string TglAkta = DateTime.Now.ToString("dd-MMMM-yyyy");
                string pukulakta = DateTime.Now.ToString("HH:mm");
                int JedaMenitAkta = info.JedaMenitAkta;
                string LastNumberAkta = info.LastNumberAkta;
                int OutstandingOrder = info.OutstandingOrder;

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

                modFilter.SelectNotaris = SelectNotaris;
                modFilter.TglOrder = TglOrder;
                modFilter.TglAkta = TglAkta;
                modFilter.pukulakta = pukulakta;
                modFilter.JedaMenitAkta = JedaMenitAkta;
                modFilter.LastNumberAkta = LastNumberAkta;
                modFilter.OutstandingOrder = OutstandingOrder;
                modFilter.NoPerjanjian = "";

                akta.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["AktaCreateList"] = akta;
                TempData["AktaCreateListFilter"] = modFilter;

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Akta";
                ViewBag.action = "clnAkta";

                //sent to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/uiCreateAkta.cshtml", akta),
                    result = "",
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

        public async Task<ActionResult> clnAktaRgrid(int paged)
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
                modFilter = TempData["AktaCreateListFilter"] as cFilterContract;
                akta = TempData["AktaCreateList"] as vmAkta;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string SelectNotaris = modFilter.SelectNotaris;
                string TglOrder = modFilter.TglOrder;
                string TglAkta = modFilter.TglAkta;
                string pukulakta = modFilter.pukulakta;
                string LastNumberAkta = modFilter.LastNumberAkta;
                int JedaMenitAkta = modFilter.JedaMenitAkta;
                int OutstandingOrder = modFilter.OutstandingOrder;
                string NoPerjanjian = modFilter.NoPerjanjian;
                bool IsFleet = modFilter.IsFleet;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                caption = HasKeyProtect.Decryption(caption);

                List<DataTable> dtlist = await aktaddl.dbGetAktaListCreate(akta.DTAktaCreateFromDB, SelectNotaris, TglAkta, LastNumberAkta, pukulakta, TglOrder, OutstandingOrder, JedaMenitAkta, NoPerjanjian, IsFleet, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                akta.DTDetailCreateForGrid = dtlist[1];

                //set session filterisasi //
                TempData["AktaCreateList"] = akta;
                TempData["AktaCreateListFilter"] = modFilter;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/_uiGridAktaCreate.cshtml", akta),
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
        public async Task<ActionResult> clnGenerateAkta(vmAkta model)
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
            await Task.Delay(0).ConfigureAwait(false);
            try
            {

                //get session set before//
                modFilter = TempData["AktaCreateListFilter"] as cFilterContract;
                akta = TempData["AktaCreateList"] as vmAkta;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string SelectNotaris = modFilter.NotarisLogin;
                string TglOrder = model.DetailFilter.TglOrder ?? "";
                string TglAkta = model.DetailFilter.TglAkta ?? "";
                string pukulakta = model.DetailFilter.pukulakta ?? "";
                string LastNumberAkta = model.DetailFilter.LastNumberAkta ?? "";
                int JedaMenitAkta = model.DetailFilter.JedaMenitAkta;
                int OutstandingOrder = model.DetailFilter.OutstandingOrder;
                string NoPerjanjian = model.DetailFilter.NoPerjanjian ?? "";
                bool Isfleet = model.DetailFilter.IsFleet;

                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                //set parameter akta//
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.TglOrder = TglOrder;
                modFilter.TglAkta = TglAkta;
                modFilter.pukulakta = pukulakta;
                modFilter.LastNumberAkta = LastNumberAkta;
                modFilter.JedaMenitAkta = JedaMenitAkta;
                modFilter.OutstandingOrder = OutstandingOrder;
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.IsFleet = Isfleet;

                string validtxt = "";  //lgakta.CheckFilterisasiDataGen(modFilter);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    caption = HasKeyProtect.Decryption(caption);

                    List<String> recordPage = await aktaddl.dbGetAktaListCreateCount(SelectNotaris, TglAkta, LastNumberAkta, pukulakta, TglOrder, OutstandingOrder, JedaMenitAkta, NoPerjanjian, Isfleet, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await aktaddl.dbGetAktaListCreate(null, SelectNotaris, TglAkta, LastNumberAkta, pukulakta, TglOrder, OutstandingOrder, JedaMenitAkta, NoPerjanjian, Isfleet, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    akta.DTAktaCreateFromDB = dtlist[0];
                    akta.DTDetailCreateForGrid = dtlist[1];

                    //try to generate number//
                    akta.DetailFilter = modFilter;

                    //set to session//
                    TempData["AktaCreateList"] = akta;
                    TempData["AktaCreateListFilter"] = modFilter;

                    //send back to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/_uiGridAktaCreate.cshtml", akta),
                        total = TotalRecord,
                        msg = ""
                    });
                }
                else
                {
                    //set back to session filter//
                    TempData["AktaCreateList"] = akta;
                    TempData["AktaCreateListFilter"] = modFilter;

                    //sendback to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = "",
                        download = "",
                        resulted = "",
                        msg = validtxt
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
        public async Task<ActionResult> clnAktaSave(vmAkta Model)
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

                akta = TempData["AktaCreateList"] as vmAkta;
                modFilter = TempData["AktaCreateListFilter"] as cFilterContract;

                string UserID = modFilter.UserID;
                string UserName = modFilter.UserName;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;
                bool IsFleet = modFilter.IsFleet;

                caption = HasKeyProtect.Decryption(caption);

                DataTable DTAktaSelected = await aktaddl.CreateTableAkta();
                foreach (var aktasel in Model.AktaSelect)
                {
                    DataTable contractselect = akta.DTDetailCreateForGrid.AsEnumerable().Where(x => x.Field<string>("keylookup") == aktasel).CopyToDataTable();
                    var row = DTAktaSelected.NewRow();

                    row["CONT_TYPE"] = contractselect.Rows[0]["CONT_TYPE"];
                    row["CLIENT_FDC_ID"] = contractselect.Rows[0]["FDC_ID"];
                    row["CONT_NO"] = contractselect.Rows[0]["NoPerjanjian"];
                    row["DEED_DATE"] = contractselect.Rows[0]["TglAkta"];
                    row["DEED_NO"] = contractselect.Rows[0]["AktaNo"];
                    row["DEED_CODE"] = contractselect.Rows[0]["AktaKode"];
                    row["DEED_TIME"] = contractselect.Rows[0]["PukulAkta"]; 
                    row["CLNT_ID"] = contractselect.Rows[0]["ClientID"];
                    row["NTRY_ID"] = contractselect.Rows[0]["NotarisID"];
                    row["SEND_CLIENT_DATE"] = contractselect.Rows[0]["Send_Client_Date"];
                    DTAktaSelected.Rows.Add(row);

                }


                int result = -1;
                int resultemail = 0;
                string msgemail = "";
                string EnumMessage = "";
                string validEnumMessage = await aktaddl.dbSaveAktaValid(DTAktaSelected, IsFleet);

                if (validEnumMessage == "")
                {
                    result = await aktaddl.dbSaveAkta(DTAktaSelected, UserID, GroupName).ConfigureAwait(false);
                    //if (result == 1)
                    //{
                    //    resultemail = MessageEmail.sendEmail((int)EmailType.AktaNotaris, "", DTAktaSelected.Rows.Count.ToString(), UserName);
                    //    msgemail = resultemail == 1 ? ProccessOutput.FilterValidEmailSDB.GetDescriptionEnums().ToString() : ProccessOutput.FilterNotValidEmailSDB.GetDescriptionEnums().ToString();
                    //}
                    //proses output result save data//
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = result == 1 ? String.Format(EnumMessage, "Pembuatan Akta", "dibuat," + "\r" + msgemail) : EnumMessage;
                }
                else
                {
                    EnumMessage = validEnumMessage;
                }

                //set to session//
                TempData["AktaCreateList"] = akta;
                TempData["AktaCreateListFilter"] = modFilter;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Akta/_uiGridAktaCreate.cshtml", akta),
                    msg = EnumMessage,
                    mntrans = "Transaksi",
                    mnid = caption,
                    rut = "Akta",
                    act = "clnAkta",
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
        #endregion Transaksi Akta

    }
}
