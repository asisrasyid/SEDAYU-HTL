using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class OrderController : Controller
    {
        //        //
        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmOrders Order = new vmOrders();
        vmOrdersddl Orderddl = new vmOrdersddl();
        cFilterContract modFilter = new cFilterContract();
        cFilterContract modFilterDetail = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        blOrders lgOrder = new blOrders();


        #region Data Transaksi Order notaris
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
                modFilter = TempData["OrderListFilter"] as cFilterContract;
                Order = TempData["OrderList"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectClient = modFilter.SelectClient;
                string SelectNotaris = modFilter.SelectNotaris;
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";

                //decript for db//
                string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                string decSelectNotaris = HasKeyProtect.Decryption(SelectNotaris);

                if (Common.ddlClient == null)
                {
                    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(decSelectClient);
                }

                if (Common.ddlNotaris == null)
                {
                    Common.ddlNotaris = await Commonddl.dbGetNotarisListByEncrypt();
                }

                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

                TempData["OrderList"] = Order;
                TempData["OrderListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectClient,
                    opsi2 = "",
                    opsi3 = SelectNotaris,
                    opsi5 = "",
                    opsi6 = fromdate,
                    opsi7 = todate,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiFilterData.cshtml", Order.DetailFilter),
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
        public async Task<ActionResult> clnListFilterOrder(cFilterContract model, string download)
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
                modFilter = TempData["OrderListFilter"] as cFilterContract;
                Order = TempData["OrderList"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;


                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string SelectNotaris = model.SelectNotaris ?? modFilter.NotarisLogin;
                string SelectJenisKontrak = model.SelectJenisKontrak ?? modFilter.SelectJenisKontrak;
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
                modFilter.SelectClient = SelectClient;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = lgOrder.CheckFilterisasiData(modFilter, download);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    caption = HasKeyProtect.Decryption(caption);

                    List<String> recordPage = await Orderddl.dbGetOrderListCount(SelectClient, SelectNotaris, fromdate, todate, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Orderddl.dbGetOrderList(null, SelectClient, SelectNotaris, fromdate, todate, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    //set to object pendataran//
                    Order.DTOrdersFromDB = dtlist[0];
                    Order.DTDetailForGrid = dtlist[1];
                    Order.DetailFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["OrderListFilter"] = modFilter;
                    TempData["OrderList"] = Order;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiGridOrderList.cshtml", Order),
                        download = "",
                        message = validtxt
                    });

                }
                else
                {

                    TempData["OrderListFilter"] = modFilter;
                    TempData["OrderList"] = Order;
                    TempData["Common"] = Common;

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

        [HttpPost]
        public async Task<ActionResult> clnOrders(String menu, String caption)
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

                // some field must be overide first for default filter//
                string SelectClient = ClientID;
                string SelectNotaris = IDNotaris;
                string SelectJenisKontrak = "0";
                string fromdate = "";
                string todate = "";

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
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);

                List<String> recordPage = await Orderddl.dbGetOrderListCount(SelectClient, SelectNotaris, fromdate, todate, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Orderddl.dbGetOrderList(null, SelectClient, SelectNotaris, fromdate, todate, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                Order.DTOrdersFromDB = dtlist[0];
                Order.DTDetailForGrid = dtlist[1];
                Order.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["OrderList"] = Order;
                TempData["OrderListFilter"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Order";
                ViewBag.action = "clnOrders";

                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/uiOrder.cshtml", Order),
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

        public async Task<ActionResult> clnOrderRgrid(int paged)
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
                modFilter = TempData["OrderListFilter"] as cFilterContract;
                Order = TempData["OrderList"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string SelectClient = modFilter.SelectClient;
                string SelectNotaris = modFilter.SelectNotaris;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
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
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                caption = HasKeyProtect.Decryption(caption);

                List<DataTable> dtlist = await Orderddl.dbGetOrderList(Order.DTOrdersFromDB, SelectClient, SelectNotaris, fromdate, todate, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                // set order//
                Order.DTDetailForGrid = dtlist[1];

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";


                //set session filterisasi //
                TempData["OrderList"] = Order;
                TempData["OrderListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiGridOrderList.cshtml", Order),
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
        #endregion Data Transaksi Order notaris


        #region Data Transaksi detail Order notaris
        [HttpPost]
        public async Task<ActionResult> ViewDetailOrder(string tglOrder, string clnt, string ntry)
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

                // get curent session//
                modFilter = TempData["OrderListFilter"] as cFilterContract;
                Order = TempData["OrderList"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //set modfilter detail//
                modFilterDetail = new cFilterContract();


                string SelectClient = clnt;
                string SelectNotaris = ntry;
                string fromdate = tglOrder;


                // try show filter data//
                modFilterDetail.fromdate = tglOrder;
                modFilterDetail.SelectClient = clnt;
                modFilterDetail.SelectNotaris = ntry;
                modFilterDetail.idcaption = caption;

                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                //decript for db//
                caption = HasKeyProtect.Decryption(caption);

                List<String> recordPage = await Orderddl.dbGetDetailOrderCount(SelectClient, SelectNotaris, tglOrder, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Orderddl.dbGetDetailOrderList(null, SelectClient, SelectNotaris, tglOrder, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                //back to set in filter//
                modFilterDetail.TotalRecord = TotalRecord;
                modFilterDetail.TotalPage = TotalPage;
                modFilterDetail.pagingsizeclient = pagingsizeclient;
                modFilterDetail.totalRecordclient = totalRecordclient;
                modFilterDetail.totalPageclient = totalPageclient;
                modFilterDetail.pagenumberclient = pagenumberclient;

                //set to object detail order//
                Order.DTOrders1FromDB = dtlist[0];
                Order.DTDetail1ForGrid = dtlist[1];
                Order.DetailFilter1 = modFilterDetail;

                // set session filter //
                TempData["OrderDetailListFilter"] = modFilterDetail;
                TempData["OrderListFilter"] = modFilter;
                TempData["OrderList"] = Order;
                TempData["common"] = Common;


                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";


                //back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiPopUpDetailOrder.cshtml", Order)
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

        public async Task<ActionResult> clnOrderDetailRgrid(int paged)
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
                modFilter = TempData["OrderListFilter"] as cFilterContract;
                modFilterDetail = TempData["OrderDetailListFilter"] as cFilterContract;
                Order = TempData["OrderList"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string SelectClient = modFilterDetail.SelectClient;
                string SelectNotaris = modFilterDetail.SelectNotaris;
                string SelectJenisKontrak = modFilterDetail.SelectJenisKontrak;
                string fromdate = modFilterDetail.fromdate;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilterDetail.PageNumber;
                double pagingsizeclient = modFilterDetail.pagingsizeclient;
                double TotalRecord = modFilterDetail.TotalRecord;
                double totalRecordclient = modFilterDetail.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                List<DataTable> dtlist = await Orderddl.dbGetDetailOrderList(Order.DTOrders1FromDB, SelectClient, SelectNotaris, fromdate, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilterDetail.pagenumberclient = pagenumberclient;

                // set order detail//
                Order.DTDetail1ForGrid = dtlist[1];

                //set session filterisasi //
                TempData["OrderDetailListFilter"] = modFilterDetail;
                TempData["OrderListFilter"] = modFilter;
                TempData["OrderList"] = Order;
                TempData["common"] = Common;

                ViewBag.Total = "Total Data : " + modFilterDetail.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilterDetail.totalRecordclient.ToString() + " Kontrak";


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiPopUpDetailOrder.cshtml", Order),
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
        #endregion Data Transaksi detail Order notaris



        #region Transaksi Order Notaris
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
                modFilter = TempData["CreateOrderFilter"] as cFilterContract;
                Order = TempData["CreateOrder"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid + regionid] as IEnumerable<cListSelected>);

                string UserID = modFilter.UserID;

                //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                bool loaddata = false;

                //set field filter to varibale //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectBranch = modFilter.SelectBranch ?? modFilter.BranchLogin;
                string SelectRegion = modFilter.SelectRegion ?? modFilter.RegionLogin;

                if (((SelectClient != clientid) || (SelectRegion != regionid)) && (regionid != ""))
                {
                    SelectClient = clientid;
                    modFilter.SelectClient = SelectClient;
                    SelectRegion = regionid;
                    modFilter.SelectRegion = SelectRegion;
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
                        string decSelectRegion = HasKeyProtect.Decryption(SelectRegion);
                        Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, decSelectRegion, UserID);
                        tempbrach = Common.ddlBranch;
                    }
                }

                TempData["tempbrach" + clientid + regionid] = tempbrach;

                TempData["CreateOrderFilter"] = modFilter;
                TempData["CreateOrder"] = Order;
                TempData["common"] = Common;



                return Json(new
                {
                    moderror = IsErrorTimeout,
                    branchjson = new JavaScriptSerializer().Serialize(tempbrach),
                    brachselect = SelectBranch,
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

        public async Task<ActionResult> clnGetregion(string clientid)
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
                modFilter = TempData["CreateOrderFilter"] as cFilterContract;
                Order = TempData["CreateOrder"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                IEnumerable<cListSelected> tempregion = (TempData["tempregion" + clientid] as IEnumerable<cListSelected>);


                string UserID = modFilter.UserID;

                //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                bool loaddata = false;

                //set field filter to varibale //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectBranch = modFilter.SelectBranch ?? modFilter.BranchLogin;
                string SelectRegion = modFilter.SelectRegion ?? modFilter.RegionLogin;

                if (SelectClient != clientid)
                {
                    SelectClient = clientid;
                    modFilter.SelectClient = SelectClient;
                    modFilter.SelectBranch = SelectBranch;
                    modFilter.SelectRegion = SelectRegion;

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
                        string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                        Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt("", decSelectClient);
                        tempregion = Common.ddlRegion;
                    }
                }

                TempData["tempregion" + clientid] = tempregion;
                TempData["CreateOrder"] = Order;
                TempData["CreateOrderFilter"] = modFilter;
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

        [HttpPost]
        public async Task<ActionResult> CreateOrder(string menu, String caption)
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
                string SelectClient = ClientID;
                string SelectBranch = IDCabang;
                string SelectRegion = Region;
                string SelectNotaris = IDNotaris;

                int PageNumber = 1;

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
                modFilter.SelectRegion = SelectRegion;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                SelectRegion = HasKeyProtect.Decryption(SelectRegion);

                Order.DTSumaryOrderForGrid = await Orderddl.dbSearchOrderInfoCount(SelectClient, PageNumber, caption, UserID, GroupName);

                if (Common.ddlClient == null)
                {
                    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(SelectClient);
                }

                if (Common.ddlNotaris == null)
                {
                    Common.ddlNotaris = await Commonddl.dbGetNotarisListByEncrypt();
                }

                if (Common.ddlJenisKontrak == null)
                {
                    Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                }


                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
                ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlCabang);
                ViewData["JeniKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

                //set session filterisasi //
                TempData["CreateOrder"] = Order;
                TempData["CreateOrderFilter"] = modFilter;
                TempData["common"] = Common;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Order";
                ViewBag.action = "CreateOrder";


                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/uiCreateOrder.cshtml", Order),

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
        public async Task<ActionResult> SearchOrder(vmOrders model)
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
                //get session set before//
                modFilter = TempData["CreateOrderFilter"] as cFilterContract;
                Order = TempData["CreateOrder"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string SelectClient = model.ClientId ?? modFilter.ClientLogin;
                string SelectBranch = model.Cabang ?? modFilter.BranchLogin;
                string SelectRegion = model.Region ?? modFilter.RegionLogin;
                string SelectJenisKontrak = model.JenisKontrak ?? modFilter.SelectJenisKontrak;


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
                modFilter.SelectClient = SelectClient;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectRegion = SelectRegion;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = lgOrder.CheckSearchOrder(modFilter);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                    caption = HasKeyProtect.Decryption(caption);

                    List<String> recordPage = await Orderddl.dbSearchOrderListCreateCount(SelectClient, SelectRegion, SelectBranch, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Orderddl.dbSearchOrderListCreate(null, SelectClient, SelectRegion, SelectBranch, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    //set to object pendataran//
                    Order.DTOrdersCreateFromDB = dtlist[0];
                    Order.DTDetailCreateForGrid = dtlist[1];
                    Order.DetailFilter = modFilter;

                    //set back to session filter//
                    TempData["CreateOrder"] = Order;
                    TempData["CreateOrderFilter"] = modFilter;
                    TempData["common"] = Common;

                    //send back to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiGridCreateOrder.cshtml", Order),
                        //total = Order.Orders == null ? 0 : Order.Orders.Count,

                    });
                }
                else
                {
                    //set back to session filter//
                    TempData["CreateOrder"] = Order;
                    TempData["CreateOrderFilter"] = modFilter;
                    TempData["common"] = Common;

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
        public async Task<ActionResult> SubmitOrder(vmOrders model)
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

                modFilter = TempData["CreateOrderFilter"] as cFilterContract;
                Order = TempData["CreateOrder"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectRegion = modFilter.SelectRegion;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
                string SelectNotaris = model.NotarisId;

                decimal JumlahOrder = model.JumlahOrder;

                //set default for paging //
                int PageNumber = 1;
                double TotalRecord = modFilter.TotalRecord;
                double TotalPage = 0;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double pagenumberclient = modFilter.pagenumberclient;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                //set filter//
                modFilter.SelectClient = SelectClient;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;


                modFilter.JumlahOrder = JumlahOrder;

                string validtxt = lgOrder.CheckSubmitOrder(modFilter, model.Contracts, Order.DTDetailCreateForGrid.Rows.Count);
                if (validtxt == "")
                {

                    //decript some model apply for DB//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                    SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    caption = HasKeyProtect.Decryption(caption);

                    // collected selected data//
                    var resultcontracts = Order.DTOrdersCreateFromDB.AsEnumerable().Where(cx => model.Contracts.Contains(cx.Field<string>("Nomor"))).ToList();

                    //// create & insert data to temporari table//
                    Order.TableOrder = Orderddl.CreateTableOrder();
                    foreach (var contract in resultcontracts)
                    {
                        var row = Order.TableOrder.NewRow();
                        row["CONT_TYPE"] = contract.ItemArray[11];
                        row["CLIENT_FDC_ID"] = contract.ItemArray[10];
                        row["NoPerjanjian"] = contract.ItemArray[4];
                        row["TglPerjanjian"] = DateTime.Now;
                        row["KodeCabang"] = "";
                        row["NamaCabang"] = "";
                        row["JenisNasabah"] = "";
                        row["NamaNasabah"] = "";
                        row["ClientId"] = SelectClient;
                        row["NotarisId"] = SelectNotaris;
                        row["User"] = UserID;
                        row["SEND_CLIENT_DATE"] = contract.ItemArray[12]; ;
                        Order.TableOrder.Rows.Add(row);
                    }

                    // try to save data //
                    int result = await Orderddl.dbSaveOrder(Order.TableOrder, SelectClient, SelectRegion, SelectJenisKontrak, UserID, GroupName);
                    int resultemail = 0;
                    string msgemail = "";

                    if (result == 1)
                    {
                        //refresh//
                        var totalrecord = Order.DTOrdersCreateFromDB.AsEnumerable().Where(cx => !model.Contracts.Contains(cx.Field<string>("Nomor")));
                        Order.DTOrdersCreateFromDB = totalrecord.Count() == 0 ? null : totalrecord.CopyToDataTable();

                        if (Order.DTOrdersCreateFromDB == null)
                        {
                            //get total data from server//
                            List<String> recordPage = await Orderddl.dbSearchOrderListCreateCount(SelectClient, SelectRegion, SelectBranch, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
                            TotalRecord = Convert.ToDouble(recordPage[0]);
                            TotalPage = Convert.ToDouble(recordPage[1]);
                            pagingsizeclient = Convert.ToDouble(recordPage[2]);
                            pagenumberclient = PageNumber;
                        }
                        else
                        {
                            double record = Order.DTOrdersCreateFromDB.Rows.Count;
                            if ((pagenumberclient * pagingsizeclient) >= record)
                            {
                                pagenumberclient = Math.Ceiling(record / pagingsizeclient);
                                TotalPage = Math.Ceiling(record / pagingsizeclient);
                            }
                        }

                        int totalorder = model.Contracts.Count();
                        List<string> datanotaris = await Commonddl.dbGetNotarisList(SelectNotaris).ConfigureAwait(false);
                        string EmailNotaris = datanotaris[2];
                        string NamaNotaris = datanotaris[0];

                        //resultemail = MessageEmail.sendEmail((int)EmailType.OrderNotaris, EmailNotaris, totalorder.ToString(), NamaNotaris);
                        //msgemail = resultemail == 1 ? ProccessOutput.FilterValidEmailNotaris.GetDescriptionEnums().ToString() : ProccessOutput.FilterNotValidEmailNotaris.GetDescriptionEnums().ToString();

                        //set paging in grid client//
                        List<DataTable> dtlist = await Orderddl.dbSearchOrderListCreate(Order.DTOrdersCreateFromDB, SelectClient, SelectRegion, SelectBranch, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                        totalRecordclient = dtlist[0].Rows.Count;
                        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                        //back to set in filter//
                        modFilter.TotalRecord = TotalRecord;
                        modFilter.TotalPage = TotalPage;
                        modFilter.pagingsizeclient = pagingsizeclient;
                        modFilter.totalRecordclient = totalRecordclient;
                        modFilter.totalPageclient = totalPageclient;
                        modFilter.pagenumberclient = pagenumberclient;

                        Order.DTOrdersCreateFromDB = dtlist[0];
                        Order.DTDetailCreateForGrid = dtlist[1];
                        Order.DetailFilter = modFilter;

                        //get total order region//
                        Order.DTSumaryOrderForGrid = await Orderddl.dbSearchOrderInfoCount(SelectClient, PageNumber, caption, UserID, GroupName);

                    }

                    //proses output result save data//
                    string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = result == 1 ? String.Format(EnumMessage, "Pembuatan Order", "dibuat," + "\r" + msgemail) : EnumMessage;

                    //set back to session filter//
                    TempData["CreateOrder"] = Order;
                    TempData["CreateOrderFilter"] = modFilter;
                    TempData["Common"] = Common;

                    //send back to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiGridCreateOrder.cshtml", Order),
                        view1 = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiInfoRegion.cshtml", Order),
                        msg = EnumMessage,
                        resulted = result
                    });
                }
                else
                {

                    //set back to session filter//
                    TempData["CreateOrder"] = Order;
                    TempData["CreateOrderFilter"] = modFilter;
                    TempData["Common"] = Common;

                    //sendback to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiGridCreateOrder.cshtml", Order),
                        download = "",
                        resulted = "",
                        msg = validtxt,
                        notrd = SelectNotaris,
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

        public async Task<ActionResult> clnSearchOrderRgrid(int paged)
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
                modFilter = TempData["CreateOrderFilter"] as cFilterContract;
                Order = TempData["CreateOrder"] as vmOrders;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;


                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectRegion = modFilter.SelectRegion;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;


                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                caption = HasKeyProtect.Decryption(caption);

                List<DataTable> dtlist = await Orderddl.dbSearchOrderListCreate(Order.DTOrdersCreateFromDB, SelectClient, SelectRegion, SelectBranch, SelectJenisKontrak, pagenumberclient, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set order//
                Order.DTDetailCreateForGrid = dtlist[1];

                //set session filterisasi //
                TempData["CreateOrder"] = Order;
                TempData["CreateOrderFilter"] = modFilter;
                TempData["common"] = Common;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Order/_uiGridCreateOrder.cshtml", Order),
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

        #endregion Transaksi Order Notaris

        //        //[HttpPost]
        //        //public async Task<ActionResult> CancelOrder(string noOrder)
        //        //{
        //        //    vmAccount = (vmAccount)Session["Account"];
        //        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //        //    if (vmAccount.UserLogin.RouteName != "")
        //        //    {
        //        //        Response.StatusCode = 406;
        //        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //        //    }

        //        //    try
        //        //    {
        //        //        string userId = vmAccount.UserLogin.UserID;
        //        //        string message = await vmViewOrder.dbCancelOrder(noOrder, userId);

        //        //        message = message == "success" ? "No Order : <b>" + noOrder + "</b> has been cancelled." : "Cannot Cancel, " + message + ".";
        //        //        return Json(new
        //        //        {
        //        //            msg = message
        //        //        });
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        Response.StatusCode = 406;
        //        //        var msg = ex.Message.ToString();
        //        //        return Json(new
        //        //        {
        //        //            url = Url.Action("Index", "Error", new { area = "" }),
        //        //        });
        //        //    }
        //        //}

    }
}
