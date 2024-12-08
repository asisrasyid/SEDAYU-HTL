using HashNetFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class HomeController : Controller
    {

        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmCommon Common = new vmCommon();
        vmHome Home = new vmHome();
        vmCommonddl Commonddl = new vmCommonddl();
        vmHomeddl Homeddl = new vmHomeddl();

        cFilterContract modFilter = new cFilterContract();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HomeFilter(cFilterContract model)
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

                modFilter = TempData["HomeFilter"] as cFilterContract;
                Home = TempData["HomeList"] as vmHome;
                Common = (TempData["common"] as vmCommon);
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
                string idcaption = HasKeyProtect.Decryption(modFilter.idcaption);
                string caption = idcaption;
                string menu = modFilter.Menu;


                // extend //
                caption = caption.Replace("TODODASH", "");
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                //set field to output//
                string KeySearch = model.RequestNo ?? "";
                string ModeTodo = modFilter.ModeTODO ?? "";
                string todate = model.todate ?? "";
                string fromdate = model.fromdate ?? "";
                string SelectArea = model.SelectArea ?? "";
                string SelectBranch = model.SelectBranch ?? "";
                string SelectDivisi = model.SelectDivisi ?? "";
                string Status = model.SelectRequestStatus ?? "";

                //set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                //set filter//
                modFilter.RequestNo = KeySearch;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectArea = SelectArea;
                modFilter.SelectBranch = SelectBranch;
                modFilter.todate = todate;
                modFilter.fromdate = fromdate;
                modFilter.SelectRequestStatus = Status;
                modFilter.ModeTODO = ModeTodo;
                modFilter.isModeFilter = true;
                //set filter//

                // cek validation for filterisasi //
                //string validtxt = lgPendaftaran.CheckFilterisasiData(modFilter, download);
                string validtxt = "";
                if (validtxt == "")
                {


                    //set paging in grid client//
                    List<String> recordPage = await Homeddl.dbGetHomeListCount(KeySearch, SelectDivisi, SelectArea, SelectBranch, Status, fromdate, todate, ModeTodo, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Homeddl.dbGetHomeList(null, KeySearch, SelectDivisi, SelectArea, SelectBranch, Status, fromdate, todate, ModeTodo, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    Home.TodoAll = dtlist[0];
                    Home.TodoUser = dtlist[1];
                    Home.DetailFilter = modFilter;

                    Home.DetailFilter = modFilter;

                    TempData["HomeFilter"] = modFilter;
                    TempData["HomeList"] = Home;
                    TempData["common"] = Common;


                    ViewBag.menu = menu;
                    ViewBag.caption = caption;
                    ViewBag.captiondesc = menuitemdescription;
                    ViewBag.rute = "Home";
                    ViewBag.action = "clnHomeTodo";


                    //string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    //ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_HomeTodo.cshtml", Home),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {
                    TempData["HomeFilter"] = modFilter;
                    TempData["HomeList"] = Home;
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
                modFilter = TempData["HomeFilter"] as cFilterContract;
                modFilter = modFilter == null ? new cFilterContract() : modFilter;
                Home = TempData["HomeList"] as vmHome;
                Common = (TempData["common"] as vmCommon);
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

                    TempData["HomeFilter"] = modFilter;
                    TempData["HomeList"] = Home;
                    TempData["common"] = Common;

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
                    string SelectRequestStatus = modFilter.SelectRequestStatus;
                    string fromdate = modFilter.fromdate ?? "";
                    string todate = modFilter.todate ?? "";


                    //decript for db//
                    //string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                    //string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);

                    //if (Common.ddlRegmitraType == null)
                    {
                        Common.ddlRegmitraType = await Commonddl.dbdbGetDdlEnumsListByEncrypt("REGMTYPE", moduleid, UserID, GroupName);
                    }

                    //if (Common.ddlDevisi == null)
                    {
                        Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", moduleid, UserID, GroupName);
                    }

                    //if (Common.ddlRegion == null)
                    {
                        Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("", "", moduleid, UserID, GroupName);
                    }

                    //if (Common.ddlBranch == null)
                    {
                        Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", "", "", moduleid, UserID, GroupName);
                    }

                    ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                    ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                    ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                    ViewData["SelectType"] = OwinLibrary.Get_SelectListItem(Common.ddlRegmitraType);

                    TempData["HomeFilter"] = modFilter;
                    TempData["HomeList"] = Home;
                    TempData["common"] = Common;

                    string datakosong = HasKeyProtect.Encryption("");

                    // senback to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        opsi1 = Keykode,
                        opsi2 = SelectDivisi, //decSelectBranch,
                        opsi3 = SelectArea, //SelectNotaris,
                        opsi4 = SelectBranch,
                        opsi5 = SelectRequestStatus,
                        opsi6 = fromdate,
                        opsi7 = todate,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_uiFilterData.cshtml", modFilter),
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


        public async Task<ActionResult> HomeGate(int id = 1, string Keycode = "")
        {


            string browser = HttpContext.Request.Browser.Browser;
            if (browser.ToLower() != "chrome")  //&& browser.ToLower() != "firefox"
            {
                return RedirectToRoute("UnsupportBrowser");
            }

            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.


            Keycode = Keycode ?? "";
            Account = (vmAccount)Session["Account"];
            if (Account != null)
            {
                Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);
                if (Account.AccountLogin.RouteName != "")
                {

                    return RedirectToRoute(Account.AccountLogin.RouteName);
                }
                Account.AccountTodo = await Commonddl.dbGetApprovalTodo("0", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);

                Session["Account"] = Account;
                // Home.activiryUser = await Homeddl.dbLogActivityUserWF("", Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                //Home.infouser = await Homeddl.dbLoginformasiUserWF("", Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            }
            else
            {
                return RedirectToRoute("DefaultExpired");
            }

            ViewBag.tipe = id;
            ViewBag.UserName = Account.AccountLogin.UserName;
            string GRP = Account.AccountLogin.GroupName;

            string menu = (GRP == "CABANG" || GRP == "AREA") ? "WFTRKNEWREG" : "WFTODONEWREG";
            //string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == menu).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

            string menuitemdescription = "";

            ViewBag.PendingVerifikasi = "0";
            ViewBag.PendingCek = "0";
            ViewBag.PendingINV = "0";

            ViewBag.dattime = DateTime.Now.ToString("dddd , dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            if (Account.AccountTodo.Rows.Count > 0)
            {
                ViewBag.PendingTask = Account.AccountTodo.Rows[0]["TotalPending"].ToString();
                ViewBag.PendingVerifikasi = Account.AccountTodo.Rows[0]["PendingVerifikasi"].ToString();
                ViewBag.PendingCek = Account.AccountTodo.Rows[0]["PendingCek"].ToString();
                ViewBag.PendingINV = Account.AccountTodo.Rows[0]["PendingINV"].ToString();
                ViewBag.PendingAkadCover = Account.AccountTodo.Rows[0]["PendingAkadCover"].ToString();
                ViewBag.PendingAkadNormal = Account.AccountTodo.Rows[0]["PendingAkadNormal"].ToString();
                ViewBag.PendingSiapAkad = Account.AccountTodo.Rows[0]["PendingSiapAkad"].ToString();
                ViewBag.PendingCheck = Account.AccountTodo.Rows[0]["PendingCheck"].ToString();
                ViewBag.SendBPN = Account.AccountTodo.Rows[0]["SendBPN"].ToString();
                ViewBag.TotalAkad = (int.Parse(ViewBag.PendingAkadCover) + int.Parse(ViewBag.PendingAkadNormal));
            }

            Keycode = "all";
            Home.TodoNOT = await Commonddl.dbGetApprovalTodo("20", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            if (Home.TodoNOT.Rows.Count > 0)
            {
                Home.TodoNOTCekSer = int.Parse(Home.TodoNOT.Compute("Sum([Pengecekan Sertifikat])", "").ToString());
                Home.TodoNOTSendBPN = int.Parse(Home.TodoNOT.Compute("Sum([Sending BPN])", "").ToString());
                Home.TodoNOTSiapAkad = int.Parse(Home.TodoNOT.Compute("Sum([Siap Akad])", "").ToString());
            }

            Keycode = "allod";
            Home.TodoNOTOD = await Commonddl.dbGetApprovalTodo("20", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            if (Home.TodoNOTOD.Rows.Count > 0)
            {
                Home.TodoNOTOD1 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP1)", "").ToString());
                Home.TodoNOTOD2 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP2)", "").ToString());
                Home.TodoNOTOD3 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP3)", "").ToString());
            }


            Keycode = "all";
            Home.TodoCAB = await Commonddl.dbGetApprovalTodo("30", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            if (Home.TodoCAB.Rows.Count > 0)
            {
                Home.TodoCABPerbaikan = int.Parse(Home.TodoCAB.Compute("Sum(Perbaikan)", "").ToString());
                Home.TodoCABPending = int.Parse(Home.TodoCAB.Compute("Sum(Pending)", "").ToString());
                Home.TodoCABSiapAkad = int.Parse(Home.TodoCAB.Compute("Sum([Siap Akad])", "").ToString());
            }

            Keycode = "all";
            Home.TodoCVERFY = await Commonddl.dbGetApprovalTodo("60", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            if (Home.TodoCVERFY.Rows.Count > 0)
            {
                Home.TodoVERFY1 = int.Parse(Home.TodoCVERFY.Compute("sum(Veryfy)", "").ToString());
            }

            Keycode = "allod";
            Home.TodoCVERFYOD = await Commonddl.dbGetApprovalTodo("60", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            if (Home.TodoCVERFYOD.Rows.Count > 0)
            {
                Home.TodoCVERFYOD1 = int.Parse(Home.TodoCVERFYOD.Compute("sum(GP1)", "").ToString());
                Home.TodoCVERFYOD2 = int.Parse(Home.TodoCVERFYOD.Compute("sum(GP2)", "").ToString());
            }

            Keycode = "all";
            Home.OrderAll = await Commonddl.dbGetApprovalTodo("70", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            if (Home.OrderAll.Rows.Count > 0)
            {
                Home.OrderAll1 = int.Parse(Home.OrderAll.Compute("Sum(GP1)", "").ToString());
                Home.OrderAll2 = int.Parse(Home.OrderAll.Compute("Sum(GP2)", "").ToString());
                Home.OrderAll3 = int.Parse(Home.OrderAll.Compute("Sum(GP3)", "").ToString());
                Home.OrderAll4 = int.Parse(Home.OrderAll.Compute("Sum(GP4)", "").ToString());
                Home.OrderAll5 = int.Parse(Home.OrderAll.Compute("Sum(GP5)", "").ToString());
                Home.OrderAll6 = int.Parse(Home.OrderAll.Compute("Sum(GP6)", "").ToString());
                Home.TotalYetFIF= int.Parse(Home.OrderAll.Compute("Sum(GP7)", "").ToString());
            }


            Keycode = "allod";
            Home.TodoReadyINV = await Commonddl.dbGetApprovalTodo("80", "", Keycode, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
            if (Home.TodoReadyINV.Rows.Count > 0)
            {
                Home.TotalInvCEK = int.Parse(Home.TodoReadyINV.Compute("sum(CEK)", "").ToString());
                Home.TotalInvSKMHTAPHT = int.Parse(Home.TodoReadyINV.Compute("sum(SPT)", "").ToString());
                Home.TotalInvCANCEL = int.Parse(Home.TodoReadyINV.Compute("sum(CNL)", "").ToString());
            }

            Home.TotalInv = Home.TotalInvCEK + Home.TotalInvSKMHTAPHT + Home.TotalInvCANCEL;
            ViewBag.UserTypes = HashNetFramework.HasKeyProtect.Decryption(Account.AccountLogin.UserType);


            ViewBag.menu = menu;
            ViewBag.caption = menu;
            ViewBag.captiondesc = menuitemdescription;
            ViewBag.rute = "Home";
            ViewBag.action = "clnHomeTodo";

            TempData["HomeFilter"] = new cFilterContract();
            TempData["HomeList"] = Home;
            TempData["common"] = new vmCommon();

            return View(Home);

        }

        [HttpPost]
        public async Task<ActionResult> clnHomeTodo(String menu, String caption)
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
                string GroupName = Account.AccountLogin.GroupName;
                string ClientName = Account.AccountLogin.ClientName;
                string CabangName = Account.AccountLogin.CabangName;
                string Mailed = Account.AccountLogin.Mailed;
                string GenMoon = Account.AccountLogin.GenMoon;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string idcaption = HasKeyProtect.Encryption(caption);

                // extend //
                string tempmodule = caption.Contains("TODODASH") ? "TODODASH" : "";
                caption = caption.Replace("TODODASH", "");
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                // some field must be overide first for default filter//
                string KeySearch = "";
                string ModeTodo = tempmodule;
                string SelectDivisi = "";
                string SelectArea = "";
                string SelectBranch = "";
                string fromdate = "";
                string todate = "";
                string Status = "";


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
                modFilter.GroupName = GroupName;
                modFilter.ClientName = ClientName;
                modFilter.CabangName = CabangName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;

                modFilter.RequestNo = KeySearch;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectArea = SelectArea;
                modFilter.SelectBranch = SelectBranch;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectRequestStatus = Status;
                modFilter.ModeTODO = ModeTodo;
                modFilter.PageNumber = PageNumber;


                //descript some value for db//
                //SelectClient = ""; // HasKeyProtect.Decryption(SelectClient);
                //SelectBranch = ""; //HasKeyProtect.Decryption(SelectBranch);

                //set paging in grid client//
                List<String> recordPage = await Homeddl.dbGetHomeListCount(KeySearch, SelectDivisi, SelectArea, SelectBranch, Status, fromdate, todate, ModeTodo, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Homeddl.dbGetHomeList(null, KeySearch, SelectDivisi, SelectArea, SelectBranch, Status, fromdate, todate, ModeTodo, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                modFilter.Menu = menu;

                //set to object pendataran//
                Home.TodoAll = dtlist[0];
                Home.TodoUser = dtlist[1];
                Home.DetailFilter = modFilter;
                Home.Permission = PermisionModule;

                //set session filterisasi //
                TempData["HomeList"] = Home;
                TempData["HomeFilter"] = modFilter;

                // set caption menut text //

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Home";
                ViewBag.action = "clnHomeTodo";
                ViewBag.dashaction = tempmodule.Contains("TODODASH") ? "TODODASH" : "";

                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";


                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_HomeTodo.cshtml", Home),
                    msg = "",
                    resulted = "",
                    flag = "",
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

        public async Task<ActionResult> clnHomeRgrid(int paged)
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
                modFilter = TempData["HomeFilter"] as cFilterContract;
                Home = TempData["HomeList"] as vmHome;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string KeySearch = modFilter.RequestNo;
                string ModeTodo = modFilter.ModeTODO;
                string SelectDivisi = modFilter.SelectDivisi;
                string SelectArea = modFilter.SelectArea;
                string SelectBranch = modFilter.SelectBranch;
                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string Status = modFilter.SelectRequestStatus;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                //SelectClient = HasKeyProtect.Decryption(SelectClient);
                //SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                caption = HasKeyProtect.Decryption(caption);
                caption = caption.Replace("TODODASH", "");
                //select page
                List<DataTable> dtlist = await Homeddl.dbGetHomeList(Home.TodoAll, KeySearch, SelectDivisi, SelectArea, SelectBranch, Status, fromdate, todate, ModeTodo, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                // set pendataran //
                Home.TodoUser = dtlist[1];

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                //set session filterisasi //
                TempData["HomeList"] = Home;
                TempData["HomeFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_HomeTodoGrid.cshtml", Home),
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


        public async Task<ActionResult> clnRgrTodo(string ap, string jn)
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
                modFilter = TempData["HomeFilter"] as cFilterContract;
                Home = TempData["HomeList"] as vmHome;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;


                string viewdt = "";
                string tbl = "";
                string dv = "";
                string vartot1 = "";
                string vartot2 = "";
                string vartot3 = "";
                string vartot4 = "";

                if (ap == "20")
                {
                    if (jn == "all" || jn == "day" || jn == "month")
                    {
                        Home.TodoNOT = await Commonddl.dbGetApprovalTodo(ap, "", jn, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                        if (Home.TodoNOT.Rows.Count > 0)
                        {
                            Home.TodoNOTCekSer = int.Parse(Home.TodoNOT.Compute("Sum([Pengecekan Sertifikat])", "").ToString());
                            Home.TodoNOTSendBPN = int.Parse(Home.TodoNOT.Compute("Sum([Sending BPN])", "").ToString());
                            Home.TodoNOTSiapAkad = int.Parse(Home.TodoNOT.Compute("Sum([Siap Akad])", "").ToString());

                            vartot1 = Home.TodoNOTCekSer.ToString("N0");
                            vartot2 = Home.TodoNOTSendBPN.ToString("N0");
                            vartot3 = Home.TodoNOTSiapAkad.ToString("N0");
                        }

                        viewdt = "~/Views/Home/_HomeTodoGridPPAT.cshtml";
                        tbl = "table_txppat";
                        dv = "todoppat";
                    }

                    if (jn == "allod" || jn == "dayod" || jn == "monthod")
                    {
                        Home.TodoNOTOD = await Commonddl.dbGetApprovalTodo(ap, "", jn, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                        if (Home.TodoNOTOD.Rows.Count > 0)
                        {
                            Home.TodoNOTOD1 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP1)", "").ToString());
                            Home.TodoNOTOD2 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP2)", "").ToString());
                            Home.TodoNOTOD2 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP3)", "").ToString());

                            vartot1 = Home.TodoNOTOD1.ToString("N0");
                            vartot2 = Home.TodoNOTOD2.ToString("N0");
                            vartot3 = Home.TodoNOTOD3.ToString("N0");
                        }
                        viewdt = "~/Views/Home/_HomeTodoGridODPPAT.cshtml";
                        tbl = "table_txodppat";
                        dv = "todoodppat";
                    }
                }


                if (ap == "30")
                {
                    if (jn == "all" || jn == "day" || jn == "month")
                    {
                        Home.TodoCAB = await Commonddl.dbGetApprovalTodo(ap, "", jn, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                        if (Home.TodoCAB.Rows.Count > 0)
                        {
                            Home.TodoCABPerbaikan = int.Parse(Home.TodoCAB.Compute("Sum([Perbaikan])", "").ToString());
                            Home.TodoCABPending = int.Parse(Home.TodoCAB.Compute("Sum([Pending])", "").ToString());
                            Home.TodoCABSiapAkad = int.Parse(Home.TodoCAB.Compute("Sum([Siap Akad])", "").ToString());

                            vartot1 = Home.TodoCABPerbaikan.ToString("N0");
                            vartot2 = Home.TodoCABPending.ToString("N0");
                            vartot3 = Home.TodoCABSiapAkad.ToString("N0");
                        }
                        viewdt = "~/Views/Home/_HomeTodoGridCAB.cshtml";
                        tbl = "table_txcab";
                        dv = "todocab";
                    }

                    //if (jn == "allod" || jn == "dayod" || jn == "monthod")
                    //{
                    //    Home.TodoNOTOD = await Commonddl.dbGetApprovalTodo(ap, "", jn, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                    //    Home.TodoNOTOD1 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP1)", "").ToString());
                    //    Home.TodoNOTOD2 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP2)", "").ToString());
                    //    Home.TodoNOTOD2 = int.Parse(Home.TodoNOTOD.Compute("Sum(GP3)", "").ToString());

                    //    vartot1 = Home.TodoNOTOD1.ToString("N0");
                    //    vartot2 = Home.TodoNOTOD2.ToString("N0");
                    //    vartot3 = Home.TodoNOTOD3.ToString("N0");

                    //    viewdt = "~/Views/Home/_HomeTodoGridODPPAT.cshtml";
                    //    tbl = "table_txodppat";
                    //    dv = "todoodppat";
                    //}
                }


                if (ap == "60")
                {
                    if (jn == "all" || jn == "day" || jn == "month")
                    {
                        Home.TodoCVERFY = await Commonddl.dbGetApprovalTodo(ap, "", jn, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                        if (Home.TodoCVERFY.Rows.Count > 0)
                        {
                            Home.TodoVERFY1 = int.Parse(Home.TodoCVERFY.Compute("Sum([Veryfy])", "").ToString());
                            vartot1 = Home.TodoVERFY1.ToString("N0");
                        }
                        viewdt = "~/Views/Home/_HomeTodoGridVRY.cshtml";
                        tbl = "table_txvry";
                        dv = "todovry";
                    }

                    if (jn == "allod" || jn == "dayod" || jn == "monthod")
                    {
                        Home.TodoCVERFYOD = await Commonddl.dbGetApprovalTodo(ap, "", jn, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                        if (Home.TodoCVERFYOD.Rows.Count > 0)
                        {
                            Home.TodoCVERFYOD1 = int.Parse(Home.TodoCVERFYOD.Compute("Sum(GP1)", "").ToString());
                            Home.TodoCVERFYOD2 = int.Parse(Home.TodoCVERFYOD.Compute("Sum(GP2)", "").ToString());
                            vartot1 = Home.TodoCVERFYOD1.ToString("N0");
                            vartot2 = Home.TodoCVERFYOD2.ToString("N0");
                        }

                        viewdt = "~/Views/Home/_HomeTodoGridODVRY.cshtml";
                        tbl = "table_txodvry";
                        dv = "todoododvry";
                    }
                }


                if (ap == "70")
                {
                    if (jn == "all" || jn == "day" || jn == "month")
                    {
                        Home.OrderAll = await Commonddl.dbGetApprovalTodo("70", "", jn, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                        if (Home.OrderAll.Rows.Count > 0)
                        {
                            Home.OrderAll1 = int.Parse(Home.OrderAll.Compute("Sum(GP1)", "").ToString());
                            Home.OrderAll2 = int.Parse(Home.OrderAll.Compute("Sum(GP2)", "").ToString());
                            Home.OrderAll3 = int.Parse(Home.OrderAll.Compute("Sum(GP3)", "").ToString());
                            Home.OrderAll4 = int.Parse(Home.OrderAll.Compute("Sum(GP4)", "").ToString());

                            vartot1 = Home.OrderAll1.ToString("N0");
                            vartot2 = Home.OrderAll2.ToString("N0");
                            vartot3 = Home.OrderAll3.ToString("N0");
                            vartot4 = Home.OrderAll4.ToString("N0");
                        }

                        viewdt = "";
                        tbl = "";
                        dv = "todoaju";
                    }
                }

                //set session filterisasi //
                TempData["HomeList"] = Home;
                TempData["HomeFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    tbled = tbl,
                    divdata = dv,
                    tot1 = vartot1,
                    tot2 = vartot2,
                    tot3 = vartot3,
                    tot4 = vartot4,
                    view = (viewdt == "") ? "" : CustomEngineView.RenderRazorViewToString(ControllerContext, viewdt, Home),
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
