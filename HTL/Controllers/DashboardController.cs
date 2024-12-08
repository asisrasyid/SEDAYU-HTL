using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        //vmAccount Account = new vmAccount();
        //blAccount lgAccount = new blAccount();
        //vmCommon Common = new vmCommon();
        //vmDash Dash = new vmDash();
        //vmDashddl Dashddl = new vmDashddl();
        //vmCommonddl Commonddl = new vmCommonddl();
        //cFilterContract modFilter = new cFilterContract();
        

        //public async Task<ActionResult> clnGetBranch(string clientid)
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

        //        modFilter = TempData["DashBoardFilter"] as cFilterContract;
        //        Dash = TempData["DashBoard"] as vmDash;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;


        //        IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid] as IEnumerable<cListSelected>);

        //        string UserID = modFilter.UserID;

        //        //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
        //        bool loaddata = false;

        //        //set field filter to varibale //
        //        string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
        //        string SelectBranch = modFilter.SelectBranch ?? modFilter.BranchLogin;

        //        if (SelectClient != clientid)
        //        {
        //            SelectClient = clientid;
        //            modFilter.SelectClient = SelectClient;
        //            modFilter.SelectBranch = SelectBranch;

        //            if (tempbrach == null)
        //            {
        //                loaddata = true;
        //            }
        //            else
        //            {
        //                int rec = tempbrach.Count();
        //                loaddata = (rec > 0) ? false : true;
        //            }

        //            if (loaddata == true)
        //            {
        //                //decript for db//
        //                string decSelectClient = HasKeyProtect.Decryption(SelectClient);
        //                string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);
        //                Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, "", UserID);
        //                tempbrach = Common.ddlBranch;
        //            }
        //        }

        //        TempData["tempbrach" + clientid] = tempbrach;

        //        TempData["DashBoardFilter"] = modFilter;
        //        TempData["DashBoard"] = Dash;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            branchjson = new JavaScriptSerializer().Serialize(tempbrach),
        //            brachselect = modFilter.SelectBranch,
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

        //public async Task<ActionResult> clnOpenFilterpop()
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

        //        //get session filterisasi //
        //        modFilter = TempData["DashBoardFilter"] as cFilterContract;
        //        Dash = TempData["DashBoard"] as vmDash;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string SelectBranch = modFilter.SelectBranch;
        //        string SelectClient = modFilter.SelectClient;
        //        string SelectNotaris = modFilter.SelectNotaris;

        //        //decript for db//
        //        string decSelectClient = HasKeyProtect.Decryption(SelectClient);
        //        string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);

        //        // try make filter initial & set secure module name //
        //        if (Common.ddlClient == null)
        //        {
        //            SelectClient = HasKeyProtect.Decryption(SelectClient);
        //            Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(decSelectClient);
        //        }

        //        if (Common.ddlClient.Count() == 1 && Common.ddlBranch == null)
        //        {
        //            SelectClient = Common.ddlClient.SingleOrDefault().Value;
        //            Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, "", UserID);
        //        }

        //        ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
        //        ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);

        //        TempData["DashBoardFilter"] = modFilter;
        //        TempData["DashBoard"] = Dash;
        //        TempData["common"] = Common;

        //        string datakosong = HasKeyProtect.Encryption("");

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            opsi1 = SelectClient,
        //            opsi2 = SelectBranch,
        //            opsi3 = SelectNotaris,
        //            opsi4 = "",
        //            opsi5 = "",
        //            opsi6 = "",
        //            opsi7 = "",
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/DashBoard/_uiFilterData.cshtml", modFilter),
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

        //[HttpPost]
        //public ActionResult clnDashMonitor(string menu, string caption)
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


        //        // some field must be overide first for default filter//
        //        string SelectClient = ClientID;
        //        string SelectBranch = IDCabang;
        //        string SelectNotaris = IDNotaris;
        //        string fromdate = "";
        //        int TipeDashboard = int.Parse(menu);

        //        modFilter.UserID = UserID;
        //        modFilter.UserName = UserName;
        //        modFilter.ClientLogin = ClientID;
        //        modFilter.BranchLogin = IDCabang;
        //        modFilter.NotarisLogin = IDNotaris;
        //        modFilter.RegionLogin = Region;
        //        modFilter.GroupName = GroupName;
        //        modFilter.ClientName = ClientName;
        //        modFilter.CabangName = CabangName;
        //        modFilter.MailerDaemoon = Mailed;
        //        modFilter.GenDeamoon = GenMoon;
        //        modFilter.UserType = int.Parse(UserTypes);
        //        modFilter.UserTypeApps = int.Parse(UserTypes);

        //        modFilter.SelectClient = SelectClient;
        //        modFilter.SelectBranch = SelectBranch;
        //        modFilter.SelectNotaris = SelectNotaris;
        //        modFilter.TipeDashboard = int.Parse(menu);
        //        modFilter.fromdate = fromdate;

        //        //decript for db//
        //        SelectClient = HasKeyProtect.Decryption(SelectClient);
        //        SelectBranch = HasKeyProtect.Decryption(SelectBranch);

        //        string viewhtml = "~";
        //        if (TipeDashboard == (int)MonitDesc.monitstatusReg)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_StatusKontrakFidusia(SelectClient, SelectBranch, fromdate, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorRegistration.cshtml";
        //        }
        //        else
        //        if (TipeDashboard == (int)MonitDesc.monitRegisAHU)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_PicRegAHU(SelectClient, SelectBranch, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorRegistrationPicAhu.cshtml";
        //        }
        //        else
        //        if (TipeDashboard == (int)MonitDesc.monitaktNTRY)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_NotarisNumbering(SelectClient, SelectBranch, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorPenomoranNotaris.cshtml";
        //        }
        //        else
        //        if (TipeDashboard == (int)MonitDesc.monitpiut)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_PiutangKlien(SelectClient, SelectBranch, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorOutstandingPiutang.cshtml";
        //        }
        //        //else
        //        //if (TipeDashboard == (int)MonitDesc.monitdocs)
        //        //{
        //        //    Dash.ListDataMonitoring = Dashddl.dbGetMonitor_DokumenPICAHU(SelectClient, SelectBranch, UserID, GroupName);
        //        //    viewhtml = "/Views/DashBoard/_monitorDocumentPicAhu.cshtml";
        //        //}
        //        else
        //        if (TipeDashboard == (int)MonitDesc.monitoutsRegis)
        //        {
        //            List<cDashMonitoring> monit = new List<cDashMonitoring>();
        //            cDashMonitoring monitdata = new cDashMonitoring();
        //            monitdata.TotalOrderIN = 0;
        //            monit.Add(monitdata);

        //            Dash.ListDataMonitoring = monit;
        //            viewhtml = "";

        //        }

        //        modFilter.ViewHtml = viewhtml;
        //        ////set filter data in session again//
        //        Dash.DetailFilter = modFilter;

        //        TempData["DashBoard"] = Dash;
        //        TempData["DashBoardFilter"] = modFilter;

        //        ViewBag.UserName = UserName;
        //        ViewBag.captiondesc = EnumsDesc.GetDescriptionEnums((MonitDesc)TipeDashboard);
        //        ViewBag.TipeDashboard = TipeDashboard;

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/DashBoard/_uiPageDashBoradMonitor.cshtml", Dash),
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


        ////[HttpGet]
        ////public ActionResult clnDashMonitorSignal(string menu, string caption)
        ////{

        ////    //Account = (vmAccount)Session["Account"];
        ////    //Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

        ////    //bool IsErrorTimeout = false;

        ////    //if (Account.AccountLogin.RouteName != "")
        ////    //{
        ////        ////return RedirectToRoute(vmAccount.UserLogin.RouteName);
        ////        //IsErrorTimeout = true;
        ////    //}

        ////    try
        ////    {

        ////        //string UserID = Account.AccountLogin.UserID;
        ////        //string UserName = Account.AccountLogin.UserName;
        ////        //string ClientID = Account.AccountLogin.ClientID;
        ////        //string IDCabang = Account.AccountLogin.IDCabang;
        ////        //string IDNotaris = Account.AccountLogin.IDNotaris;
        ////        //string Region = Account.AccountLogin.Region;
        ////        //string GroupName = Account.AccountLogin.GroupName;
        ////        //string ClientName = Account.AccountLogin.ClientName;
        ////        //string CabangName = Account.AccountLogin.CabangName;
        ////        //string Mailed = Account.AccountLogin.Mailed;
        ////        //string GenMoon = Account.AccountLogin.GenMoon;
        ////        //string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);


        ////        //// some field must be overide first for default filter//
        ////        //string SelectClient = ClientID;
        ////        //string SelectBranch = IDCabang;
        ////        //string SelectNotaris = IDNotaris;
        ////        //string fromdate = "";
        ////        int TipeDashboard = int.Parse(menu);

        ////        //modFilter.UserID = UserID;
        ////        //modFilter.UserName = UserName;
        ////        //modFilter.ClientLogin = ClientID;
        ////        //modFilter.BranchLogin = IDCabang;
        ////        //modFilter.NotarisLogin = IDNotaris;
        ////        //modFilter.RegionLogin = Region;
        ////        //modFilter.GroupName = GroupName;
        ////        //modFilter.ClientName = ClientName;
        ////        //modFilter.CabangName = CabangName;
        ////        //modFilter.MailerDaemoon = Mailed;
        ////        //modFilter.GenDeamoon = GenMoon;
        ////        //modFilter.UserType = int.Parse(UserTypes);
        ////        modFilter.UserTypeApps = 0;//int.Parse(UserTypes);

        ////        //modFilter.SelectClient = SelectClient;
        ////        //modFilter.SelectBranch = SelectBranch;
        ////        //modFilter.SelectNotaris = SelectNotaris;
        ////        //modFilter.TipeDashboard = int.Parse(menu);
        ////        //modFilter.fromdate = fromdate;

        ////        ////decript for db//
        ////        //SelectClient = HasKeyProtect.Decryption(SelectClient);
        ////        //SelectBranch = HasKeyProtect.Decryption(SelectBranch);

        ////        string viewhtml = "~";
        ////        if (TipeDashboard == (int)MonitDesc.monitoutsRegis)
        ////        {

        ////            string connectionString = HasKeyProtect.DecryptionPass(ConfigurationManager.ConnectionStrings["ConnSignalR"].ToString());
        ////            using (SqlConnection sqlcon = new SqlConnection(connectionString))
        ////            {

        ////                DataSet ds = new DataSet();
        ////                using (SqlCommand sqlcom = new SqlCommand("[db_singlr].udp_dsh_monitor_regis_outstanding", sqlcon))
        ////                {
        ////                    sqlcon.Open();
        ////                    sqlcom.CommandType = CommandType.Text;
        ////                    sqlcom.Notification = null;

        ////                    SqlDependency dependancy = new SqlDependency(sqlcom);
        ////                    dependancy.OnChange += new OnChangeEventHandler(dependancy_OnChange);

        ////                    if (sqlcon.State == ConnectionState.Closed)
        ////                    {
        ////                        sqlcon.Open();
        ////                    }

        ////                    SqlDataAdapter da = new SqlDataAdapter();
        ////                    da.SelectCommand = sqlcom;
        ////                    da.Fill(ds);

        ////                }

        ////                var monit = (from c in ds.Tables[0].AsEnumerable()
        ////                             select new cDashMonitoring()
        ////                             {
        ////                                 TotalOrderIN = c.Field<int>("OrderIN"),
        ////                                 TotalReadyToNotaris = c.Field<int>("ReadyToNotaris"),
        ////                                 TotalPendingTask = c.Field<int>("PendingTask"),
        ////                                 TotalInBoxNotaris = c.Field<int>("InBoxNotaris"),
        ////                                 TotalreadyToRegisAHU = c.Field<int>("readyToRegisAHU"),
        ////                                 TotalreadytoPaid = c.Field<int>("readytoPaid"),
        ////                                 Totalreadytoobjtserti = c.Field<int>("readytoobjtserti"),
        ////                                 Totalreadytogetserti = c.Field<int>("readytogetserti"),
        ////                                 Totalreadytoinvoice = c.Field<int>("readytoinvoice"),
        ////                                 Totalreadysendtxt = c.Field<int>("readysendtxt"),
        ////                                 TotalreadyVerifikasi = c.Field<int>("ReadyToVerifikasi"),
        ////                             }).ToList();

        ////                Dash.ListDataMonitoring = monit;
        ////                viewhtml = "/Views/DashBoard/_monitorRegistrationOutstanding.cshtml";
        ////            }
        ////        }

        ////        modFilter.ViewHtml = viewhtml;
        ////        ////set filter data in session again//
        ////        Dash.DetailFilter = modFilter;

        ////        //TempData["DashBoard"] = Dash;
        ////        //TempData["DashBoardFilter"] = modFilter;

        ////        ViewBag.UserName = "Signal Admin";
        ////        ViewBag.captiondesc = EnumsDesc.GetDescriptionEnums((MonitDesc)TipeDashboard);
        ////        ViewBag.TipeDashboard = TipeDashboard;

        ////        //send back to client browser//
        ////        return Json(new
        ////        {
        ////            view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewhtml, Dash.ListDataMonitoring),
        ////        }, JsonRequestBehavior.AllowGet);

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        var msg = ex.Message.ToString();
        ////        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        ////        Response.StatusCode = 406;
        ////        Response.TrySkipIisCustomErrors = true;
        ////        return Json(new
        ////        {
        ////            url = Url.Action("Index", "Error", new { area = "" }),
        ////            //moderror = IsErrorTimeout
        ////        }, JsonRequestBehavior.AllowGet);
        ////    }
        ////}


        //[HttpPost]
        //public async Task<ActionResult> DashBoardFilter(cFilterContract model)
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
        //        await Task.Delay(0);

        //        modFilter = TempData["DashBoardFilter"] as cFilterContract;
        //        Dash = TempData["DashBoard"] as vmDash;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;
        //        int TipeDashboard = modFilter.TipeDashboard;

        //        //get value from aply filter //
        //        string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
        //        string SelectBranch = model.SelectBranch ?? modFilter.BranchLogin;
        //        string SelectNotaris = model.SelectNotaris ?? modFilter.NotarisLogin;
        //        string fromdate = (model.fromdate ?? "") != "" ? DateTime.Parse("01-" + model.fromdate).ToString("yyyy-MM") : "";
        //        bool isModeFilter = true;

        //        //set filter//
        //        modFilter.SelectClient = SelectClient;
        //        modFilter.SelectBranch = SelectBranch;
        //        modFilter.SelectNotaris = SelectNotaris;
        //        modFilter.fromdate = fromdate;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        //descript some value for db//
        //        SelectClient = HasKeyProtect.Decryption(SelectClient);
        //        SelectBranch = HasKeyProtect.Decryption(SelectBranch);
        //        SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
        //        caption = HasKeyProtect.Decryption(caption);

        //        string viewhtml = "~";
        //        if (TipeDashboard == (int)MonitDesc.monitstatusReg)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_StatusKontrakFidusia(SelectClient, SelectBranch, fromdate, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorRegistration.cshtml";
        //        }
        //        else
        //        if (TipeDashboard == (int)MonitDesc.monitRegisAHU)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_PicRegAHU(SelectClient, SelectBranch, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorRegistrationPicAhu.cshtml";
        //        }
        //        else
        //        if (TipeDashboard == (int)MonitDesc.monitaktNTRY)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_NotarisNumbering(SelectClient, SelectBranch, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorPenomoranNotaris.cshtml";
        //        }
        //        else
        //        if (TipeDashboard == (int)MonitDesc.monitpiut)
        //        {
        //            Dash.ListDataMonitoring = Dashddl.dbGetMonitor_PiutangKlien(SelectClient, SelectBranch, UserID, GroupName);
        //            viewhtml = "/Views/DashBoard/_monitorOutstandingPiutang.cshtml";
        //        }
        //        //else
        //        //if (TipeDashboard == (int)MonitDesc.monitdocs)
        //        //{
        //        //    Dash.ListDataMonitoring = Dashddl.dbGetMonitor_DokumenPICAHU(SelectClient, SelectBranch, UserID, GroupName);
        //        //    viewhtml = "/Views/DashBoard/_monitorDocumentPicAhu.cshtml";
        //        //}
               

        //        ////set filter data in session again//
        //        modFilter.ViewHtml = viewhtml;
        //        Dash.DetailFilter = modFilter;


        //        //keep session filterisasi before//
        //        TempData["DashBoardFilter"] = modFilter;
        //        TempData["DashBoard"] = Dash;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewhtml, Dash.ListDataMonitoring),
        //            download = "",
        //            message = ""
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

        //public void dependancy_OnChange(object sender, SqlNotificationEventArgs e)

        //{
        //    if (e.Type == SqlNotificationType.Subscribe)
        //    {
        //        SignalRMon.NotifyChanges();
        //    }
        //}


    }
}
