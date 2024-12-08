using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class TrackingOrderController : Controller
    {
        //
        // GET: /TrackingOrder/

        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmTrackingOrder TrackingOrder = new vmTrackingOrder();
        cTrackingOrderSharePicAHU SharePICAHU = new cTrackingOrderSharePicAHU();
        vmTrackingOrderddl TrackingOrderddl = new vmTrackingOrderddl();
        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        blTrackingOrder lgTrackingOrder = new blTrackingOrder();


        #region Tracking Order
        public async Task<ActionResult> clnContractTrack(string menu, string caption)
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
                modFilter.NoPerjanjian = NoPerjanjian;

                modFilter.PageNumber = PageNumber;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);

                List<String> recordPage = await TrackingOrderddl.dbGetTrackingOrderRegListCount(SelectClient, NoPerjanjian, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await TrackingOrderddl.dbGetTrackingOrderRegList(null, SelectClient, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                TrackingOrder.DTOrdersFromDB = dtlist[0];
                TrackingOrder.DTDetailForGrid = dtlist[1];
                TrackingOrder.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["TrackingOrderRegList"] = TrackingOrder;
                TempData["TrackingOrderRegListFilter"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "TrackingOrder";
                ViewBag.action = "clnContractTrack";

                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/TrackingOrder/uiTrackingOrderReg.cshtml", TrackingOrder),
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
        public async Task<ActionResult> clnListFilterContractTrack(cFilterContract model, string download)
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
                modFilter = TempData["TrackingOrderRegListFilter"] as cFilterContract;
                TrackingOrder = TempData["TrackingOrderRegList"] as vmTrackingOrder;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = model.NoPerjanjian ?? "";
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;

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

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = lgTrackingOrder.CheckFilterisasiDataTract(modFilter, download);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    caption = HasKeyProtect.Decryption(caption);


                    List<String> recordPage = await TrackingOrderddl.dbGetTrackingOrderRegListCount(SelectClient, NoPerjanjian, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await TrackingOrderddl.dbGetTrackingOrderRegList(null, SelectClient, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //set in filter for paging//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    //set to object TrackingOrder//
                    TrackingOrder.DTOrdersFromDB = dtlist[0];
                    TrackingOrder.DTDetailForGrid = dtlist[1];
                    TrackingOrder.DetailFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["TrackingOrderRegList"] = TrackingOrder;
                    TempData["TrackingOrderRegListFilter"] = modFilter;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/TrackingOrder/_uiGridTrackingOrderRegList.cshtml", TrackingOrder),
                        download = "",
                        message = validtxt
                    });

                }
                else
                {

                    TempData["TrackingOrderRegList"] = TrackingOrder;
                    TempData["TrackingOrderRegListFilter"] = modFilter;
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

            try
            {
                //get session filterisasi //
                modFilter = TempData["TrackingOrderRegListFilter"] as cFilterContract;
                TrackingOrder = TempData["TrackingOrderRegList"] as vmTrackingOrder;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectClient = modFilter.SelectClient;
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";

                //decript for db//
                string decSelectClient = HasKeyProtect.Decryption(SelectClient);

                // try make filter initial & set secure module name //
                if (Common.ddlClient == null)
                {
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(decSelectClient);
                }



                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectDocStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlDocStatus);
                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);

                TempData["TrackingOrderRegList"] = TrackingOrder;
                TempData["TrackingOrderRegListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectClient,
                    opsi2 = "",
                    opsi3 = "",
                    opsi5 = NoPerjanjian,
                    opsi6 = "",
                    opsi7 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/TrackingOrder/_uiFilterData.cshtml", TrackingOrder.DetailFilter),
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
        public async Task<ActionResult> clnContractTrackGet(string NoPerjanjian, string idfdc)
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


                await Task.Delay(1);

                modFilter = TempData["TrackingOrderRegListFilter"] as cFilterContract;
                TrackingOrder = TempData["TrackingOrderRegList"] as vmTrackingOrder;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                idfdc = HasKeyProtect.Decryption(idfdc);

                DataTable selcdatatotable = TrackingOrder.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == NoPerjanjian).CopyToDataTable();
                TrackingOrder.DTDetailForGridRowSelected = selcdatatotable;

                ViewBag.nokonview = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString() + "-" + selcdatatotable.Rows[0]["NAMA_DEBITUR"].ToString();

                TempData["TrackingOrderRegList"] = TrackingOrder;
                TempData["TrackingOrderRegListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/TrackingOrder/_uiInfoContract.cshtml", TrackingOrder),
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
        public async Task<ActionResult> clnContractTrackRgrid(int paged)
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
                modFilter = TempData["TrackingOrderRegListFilter"] as cFilterContract;
                TrackingOrder = TempData["TrackingOrderRegList"] as vmTrackingOrder;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string NoPerjanjian = modFilter.NoPerjanjian;
                string SelectClient = modFilter.SelectClient;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                caption = HasKeyProtect.Decryption(caption);

                List<DataTable> dtlist = await TrackingOrderddl.dbGetTrackingOrderRegList(TrackingOrder.DTOrdersFromDB, SelectClient, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                TrackingOrder.DTDetailForGrid = dtlist[1];

                //set session filterisasi //
                TempData["TrackingOrderRegList"] = TrackingOrder;
                TempData["TrackingOrderRegListFilter"] = modFilter;
                TempData["common"] = Common;

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/TrackingOrder/_uiGridTrackingOrderRegList.cshtml", TrackingOrder),
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
        #endregion Tracking Order



        #region sharpic ahu
        public async Task<ActionResult> clnSharepicahu(string menu, string caption)
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
                string GroupName = Account.AccountLogin.GroupName;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string idcaption = HasKeyProtect.Encryption(caption);

                // extend /
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend /

                // try make filter initial & set secure module name //
                modFilter.UserID = UserID;
                modFilter.GroupName = GroupName;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;

                Common.ddlUser = await Commonddl.dbGetUserRegisAhuListByEncrypt();
                ViewData["SelectUC"] = OwinLibrary.Get_SelectListItem(Common.ddlUser);

                Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);


                SharePICAHU.FormUserPIC = UserID;

                //set filter to variable filter in class contract for object view//
                TempData["SharePicAHUFilter"] = modFilter;
                TempData["SharePICAHU"] = SharePICAHU;

                //set session filterisasi //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "TrackingOrder";
                ViewBag.action = "clnSharepicahu";

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/TrackingOrder/uiTrackingPicOrderAhu.cshtml", SharePICAHU),
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
        public async Task<ActionResult> clnSharepicahuupt(cTrackingOrderSharePicAHU model)
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

                modFilter = TempData["SharePicAHUFilter"] as cFilterContract;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;


                string ToUserPIC = model.ToUserPIC ?? "";
                string fromuser = model.FormUserPIC ?? "";
                string SelectJenisKontrak = model.SelectJenisKontrak ?? "";
                string NoKontrak = model.NoKontrak ?? "";
                int JumlahKontrak = model.JumlahKontrak;
                bool PicTidakada = model.PicTidakada;
                bool PicPendingTask = model.PicPendingTask;
                bool FullSharing = model.FullSharing;

                TempData["SharePicAHUFilter"] = modFilter;

                string validtext = "";
                if (ToUserPIC == "")
                {
                    validtext = "Isikan Field Transfer Ke User";
                }

                if (SelectJenisKontrak == "")
                {
                    validtext = "Isikan Jenis Registrasi";
                }
                else
                {

                    if ((int.Parse(SelectJenisKontrak) == 5 || int.Parse(SelectJenisKontrak) == 6) && (NoKontrak != "" || JumlahKontrak > 0))
                    {
                        validtext = "Kosongkan Field Nokontrak dan Jumlah Kontrak Harus Nol";
                    }

                    if ((int.Parse(SelectJenisKontrak) != 5 && int.Parse(SelectJenisKontrak) != 6) && (NoKontrak != "" && JumlahKontrak > 0))
                    {
                        validtext = "Isikan Salah satu Field No Perjanjian atau  Jumlah Kontrak ";
                    }

                }

                int result = 0;
                string EnumMessage = "";

                if (validtext == "")
                {
                    caption = HasKeyProtect.Decryption(caption);
                    fromuser = (fromuser ?? "") != "" ? HasKeyProtect.Decryption(fromuser) : fromuser;
                    ToUserPIC = (ToUserPIC ?? "") != "" ? HasKeyProtect.Decryption(ToUserPIC) : ToUserPIC;
                    result = await TrackingOrderddl.dbupdatePICAHU(NoKontrak, JumlahKontrak, int.Parse(SelectJenisKontrak), ToUserPIC, fromuser, FullSharing, PicTidakada, PicPendingTask, caption, UserID, GroupName);
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                }
                else
                {
                    EnumMessage = validtext;

                }
                EnumMessage = (result == 1) ? String.Format(EnumMessage, "Dokumen", " Ditransfer ") : EnumMessage;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
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
        #endregion share pic ahu



        //#region order fidusia klien
        //public ActionResult clndownloadorderregisfile(string menu, string caption)
        //{
        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;
        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {
        //        caption = HasKeyProtect.Decryption(caption);
        //        //get menudesccriptio//
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

        //        // get metrik user akses by module id//
        //        vmContracts.AccountDetail = vmAccount.AccountDetail;
        //        vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

        //        if (vmContracts.AccountMetrik.AllowDownload == true)
        //        {
        //            string filenametempltae = "PendaftaranFidusia.xls";
        //            string filedownload = Server.MapPath("~/External/TemplateFidusia/" + filenametempltae);
        //            byte[] fileBytes = System.IO.File.ReadAllBytes(filedownload);

        //            return Json(new { datafile = fileBytes, msg = "", contenttype = "application/vnd.ms-excel", filename = filenametempltae, JsonRequestBehavior.AllowGet });

        //        }
        //        else
        //        {
        //            return Json(new { msg = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidtemplateakses) });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public async Task<ActionResult> clncreateorderregis(string menu, string caption, string typos = "")
        //{
        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;
        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }


        //    try
        //    {

        //        string caption1 = HasKeyProtect.Decryption(caption);
        //        caption = caption1 == "-9999" ? caption : caption1;

        //        //get menudesccriptio//
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

        //        // get metrik user akses by module id//
        //        vmContracts.AccountDetail = vmAccount.AccountDetail;
        //        vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();


        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;

        //        // get pending count data //
        //        string kodecabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //        List<string> filepath = OwinLibrary.SetgetFilepath(kodecabang, vmAccount.AccountDetail.UserType, (int)UserType.FDCM, vmAccount.AccountDetail.UserID, System.Web.HttpContext.Current);
        //        int totaldatapending = OwinLibrary.GetTotalPend(filepath[0]);
        //        await Task.Delay(0);


        //        string viewhtml = "";
        //        if (typos == "")
        //        {
        //            vmContracts.DetailOrderRegis = new cContractsOrderRegisList();

        //            vmContracts.ddlJenisPelanggan = await vmContracts.dbddlgetparamenumsList("RCVFDC");
        //            ViewData["ddlJenisPelanggan"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlJenisPelanggan);

        //            vmContracts.ddlJenisPenggunaan = await vmContracts.dbddlgetparamenumsList("JNSUSED");
        //            ViewData["ddlJenisPenggunaan"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlJenisPenggunaan);

        //            vmContracts.ddlJenisPembiayaan = await vmContracts.dbddlgetparamenumsList("JNSLEASE");
        //            ViewData["ddlJenisPembiayaan"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlJenisPembiayaan);


        //            vmContracts.ddlJenisIdentitasDebitur = await vmContracts.dbddlgetparamenumsList("JNSIDEN");
        //            ViewData["ddlJenisIdentitasDebitur"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlJenisIdentitasDebitur);

        //            vmContracts.ddlJenisKelamin = await vmContracts.dbddlgetparamenumsList("JNSKLM");
        //            ViewData["ddlJenisKelamin"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlJenisKelamin);


        //            vmContracts.ddlJenisKewarganegaraan = await vmContracts.dbddlgetparamenumsList("JNSWRG");
        //            ViewData["ddlJenisKewarganegaraan"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlJenisKewarganegaraan);


        //            vmContracts.ddlJenisObject = await vmContracts.dbddlgetparamenumsList("JNSOBJ");
        //            ViewData["ddlJenisObject"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlJenisObject);


        //            vmContracts.ddlKondisiObject = await vmContracts.dbddlgetparamenumsList("KONDOBJ");
        //            ViewData["ddlKondisiObject"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlKondisiObject);


        //            viewhtml = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Contracts/uiContractsOrderRegis.cshtml", vmContracts);

        //        }
        //        else
        //        {
        //            viewhtml = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Contracts/uiContractsOrderRegisMasal.cshtml", vmContracts);

        //        }

        //        //set session filterisasi //
        //        TempData["ContractorderregisList"] = vmContracts;

        //        // set caption menut text //
        //        await Task.Delay(0);


        //        return Json(new
        //        {
        //            view = viewhtml,
        //            totaldata = totaldatapending
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnUploadOrderRegisFileMasal(string menu, string caption, HttpPostedFileBase FileUpload)
        //{
        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;
        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {

        //        //get menudesccriptio//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

        //        // get metrik user akses by module id//
        //        vmContracts.AccountDetail = vmAccount.AccountDetail;
        //        vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();



        //        await Task.Delay(0);
        //        //string viewhtml = "";
        //        string EnumMessage = "";
        //        int resulted = 0;
        //        string htmlkolomnotfound = "";
        //        string temphtmlkolomnotfound = "";
        //        DataTable Dt = new DataTable();
        //        DataTable Dttmp = new DataTable();
        //        //string jsondata = "";
        //        int lineCounter = 0;
        //        List<bulanvsmonth> namabulan = new List<bulanvsmonth>();
        //        List<cContractsOrderRegisList> listorder = new List<cContractsOrderRegisList>();

        //        if (vmContracts.AccountMetrik.AllowUpload)
        //        {
        //            if (FileUpload != null)
        //            {

        //                string fileName = FileUpload.FileName;
        //                string fileContentType = FileUpload.ContentType;
        //                string fileExtension = System.IO.Path.GetExtension(Request.Files[0].FileName);

        //                if (fileExtension == ".xls")
        //                {

        //                    for (int i = 0; i < 12; i++)
        //                    {
        //                        bulanvsmonth bln = new bulanvsmonth();
        //                        bln.bulan = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i].ToString().Substring(0, 3);
        //                        bln.idBulan = i + 1;
        //                        namabulan.Add(bln);
        //                    }


        //                    string namafile = "PendaftaranFidusia";  //sebagai nama template untuk pencarian di db//
        //                    string cekkolom = "";

        //                    IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(FileUpload.InputStream);

        //                    DataSet ds = excelReader.AsDataSet(new ExcelDataSetConfiguration()
        //                    {
        //                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                        {

        //                            UseHeaderRow = true
        //                        }
        //                    });

        //                    ds = OwinLibrary.ConvertToDataSetOfStrings(ds);

        //                    Dt = ds.Tables[0];
        //                    Dttmp = Dt.Clone();
        //                    excelReader.Close();

        //                    List<cContractsOrderRegis> orderRegis = await vmContracts.dbGetOrderRegisContractList(vmAccount.AccountDetail.ClientID, "", namafile);
        //                    //cek nama template excel 
        //                    if (orderRegis.Count > 0)
        //                    {
        //                        //get  total kolom template from db//
        //                        int jumlahkolomdb = orderRegis.Where(x => x.JenisValidasi.Contains("KOLOM_")).Count();

        //                        int jumlahkolomexcel = Dt.Columns.Count;

        //                        //bandingkan jml kolom di db dengan yang ada diexcel harus sama
        //                        if (jumlahkolomdb == jumlahkolomexcel)
        //                        {

        //                            string namakolom = "";
        //                            string valuekkolom = "";
        //                            string message = "";
        //                            List<string> kolom = new List<string>();


        //                            // cek nama kolom yang diexcel harus sama denga yang ada DB //
        //                            foreach (DataColumn col in Dt.Columns)
        //                            {
        //                                namakolom = col.ColumnName.Replace("/", "_");
        //                                //store kolom i array//
        //                                kolom.Add(namakolom);
        //                                int intfound = orderRegis.Where(x => x.valid_value == namakolom).Count();
        //                                if (intfound == 0)
        //                                {
        //                                    htmlkolomnotfound = htmlkolomnotfound + "* Kolom " + namakolom + " tidak sesuai dengan template\n";
        //                                    cekkolom = "cekheader";
        //                                }

        //                            }

        //                            int i = 0;
        //                            int rowint = 0;
        //                            string strenter = "";
        //                            string strrowenter = "";
        //                            string NoKontrak = "";

        //                            // jika nama2 kolom sudah sesuai denga template //
        //                            if (htmlkolomnotfound == "")
        //                            {
        //                                //cek format untuk masing2 baris //
        //                                foreach (DataRow rows in Dt.Rows)
        //                                {

        //                                    if (htmlkolomnotfound == "")
        //                                    {
        //                                        // tampung row data sebelum dicek//
        //                                        Dttmp.Rows.Add(rows.ItemArray);
        //                                        rowint = rowint + 1;

        //                                        //cek format untuk masing2 data pada kolom
        //                                        for (i = 0; i < kolom.Count; i++)
        //                                        {

        //                                            NoKontrak = rows[4].ToString(); //nokontrak sebagai key //

        //                                            namakolom = kolom[i].ToString();
        //                                            valuekkolom = rows[i].ToString();
        //                                            var tpdate = rows[i].GetType();

        //                                            //cek format untuk data yang diinputkan//
        //                                            message = OwinLibrary.validdata(namakolom, valuekkolom, orderRegis, Dttmp, tpdate.Name, namabulan);

        //                                            //jika format tidak sesuai akan ditampilkan//
        //                                            if (message != "")
        //                                            {
        //                                                cekkolom = "cekformat";
        //                                                strrowenter = strrowenter == "" ? "Baris Ke " + (rowint + 1).ToString() + " (NoPerjanjian-" + NoKontrak + ")  : \n" : strrowenter;
        //                                                temphtmlkolomnotfound = temphtmlkolomnotfound + (message != "" ? message + "" : "" + (message != "" ? strenter : ""));
        //                                                break;
        //                                            }
        //                                        }

        //                                        // collect to list order jika rows valid//
        //                                        if (message == "")
        //                                        {
        //                                            cContractsOrderRegisList DetailOrderRegis = new cContractsOrderRegisList();
        //                                            DetailOrderRegis = OwinLibrary.MapSaveDataRegOneResultCountDataMasal(Dttmp);

        //                                            //cek duplikasi berdasarkan nokontrak//
        //                                            int rowfound = listorder.AsEnumerable().Where(row => row.NoPerjanjian == NoKontrak).ToList().Count;

        //                                            if (rowfound > 0)
        //                                            {
        //                                                cekkolom = "cekformat";
        //                                                message = "ada duplikat data dalam penginputkan, silahkan cek kembali pada template";
        //                                                strrowenter = strrowenter == "" ? "Baris Ke " + (rowint + 1).ToString() + " (NoPerjanjian-" + NoKontrak + ")  : \n" : strrowenter;
        //                                                temphtmlkolomnotfound = temphtmlkolomnotfound + (message != "" ? message + "" : "" + (message != "" ? strenter : ""));
        //                                                //break;
        //                                            }
        //                                            else
        //                                            {
        //                                                listorder.Add(DetailOrderRegis);
        //                                            }

        //                                        }

        //                                        htmlkolomnotfound = htmlkolomnotfound + strrowenter + temphtmlkolomnotfound;
        //                                        strenter = "\n";
        //                                        strrowenter = "";
        //                                        temphtmlkolomnotfound = "";

        //                                        //hapus tampungan row data yang sudah dicek //
        //                                        Dttmp.Rows[0].Delete();

        //                                    }
        //                                    else //alert jika ditemukan ada format data yang tidak sesuai loopinggan akan diskip dan alert ditampilkan //
        //                                    {
        //                                        break;
        //                                    }
        //                                }
        //                            }

        //                            //jika ada yang tidak sesuai dengan format template data//
        //                            if (htmlkolomnotfound != "")
        //                            {
        //                                //alert karena format data pada kolom tidak sesuai//
        //                                if (cekkolom == "cekformat")
        //                                {

        //                                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidExcelFormat));
        //                                    resulted = (int)ProccessOutput.FilterValidExcelFormat;
        //                                    htmlkolomnotfound = EnumMessage + " pada kolom berikut :\n" + htmlkolomnotfound;
        //                                }
        //                                else //alert jika nama2 kolom tidak sesuai dengan template //
        //                                {
        //                                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidNamaExcelFormat));
        //                                    resulted = (int)ProccessOutput.FilterValidNamaExcelFormat;
        //                                    htmlkolomnotfound = EnumMessage + " pada kolom berikut :\n" + htmlkolomnotfound;
        //                                }

        //                            }
        //                            else //semua inputan seudah sesuai dengan template baik format maupun penamaannya //
        //                            {

        //                                ////// mencari row data yang double //
        //                                ////var duplicates = Dt.AsEnumerable().GroupBy(r => r[1]).Where(gr => gr.Count() > 1).Select(g => g.Key);

        //                                //////jika tidak ada yang double //
        //                                ////if (duplicates.Count() == 0)
        //                                ////{
        //                                if (Dt.Rows.Count > 0)
        //                                {
        //                                    string kodecabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //                                    //save data to txtfile //
        //                                    List<string> filepath = OwinLibrary.SetgetFilepath(kodecabang, vmAccount.AccountDetail.UserType, (int)UserType.FDCM, vmAccount.AccountDetail.UserID, System.Web.HttpContext.Current);
        //                                    resultsavetxt resulttext = OwinLibrary.SaveDataRegOneResultCountData(listorder, null, kodecabang, vmAccount.AccountDetail.UserType, filepath[0]);
        //                                    //save data to txtfile //

        //                                    EnumMessage = (resulttext.status == 1) ? string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.Success)), "Data", " disimpan") : resulttext.MessageError;
        //                                    resulted = resulttext.status;
        //                                    lineCounter = resulttext.countrecord;

        //                                }
        //                                else
        //                                {
        //                                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidNoRecord));
        //                                    resulted = (int)ProccessOutput.FilterValidNoRecord;
        //                                }
        //                                //}
        //                                //else //alert jika ada row data double//
        //                                //{

        //                                //    string data = "";
        //                                //    foreach (var x in duplicates)
        //                                //    {
        //                                //        data = data + x.ToString() + '\n';

        //                                //    }
        //                                //    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidDuplicateDateexcel));
        //                                //    resulted = (int)ProccessOutput.FilterValidDuplicateDateexcel;
        //                                //    htmlkolomnotfound = EnumMessage + "\n" + data;

        //                                //}

        //                            }

        //                        }
        //                        else //alert jika total kolom excel dan db tidak sesuai//
        //                        {
        //                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidTotalKolomExcelFormat));
        //                            EnumMessage = string.Format(EnumMessage, jumlahkolomdb.ToString());
        //                            resulted = (int)ProccessOutput.FilterValidTotalKolomExcelFormat;

        //                        }
        //                    }
        //                    else // alert jika nama template ditdak ada pada table template//
        //                    {

        //                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidNamaExcelFormat));
        //                        resulted = (int)ProccessOutput.FilterValidNamaExcelFormat;
        //                    }

        //                }
        //                else // alert jika nama file upload tidak sesuai , harus excel .xls//
        //                {
        //                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidextentionFormat));
        //                    resulted = (int)ProccessOutput.FilterValidextentionFormat;
        //                }
        //            }
        //            else //alert jika file upload kosong//
        //            {
        //                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidExcelNotUpload));
        //                resulted = (int)ProccessOutput.FilterValidExcelNotUpload;

        //            }
        //        }
        //        else // alert jika tidak ada akses //
        //        {
        //            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.NotAccess));
        //            resulted = (int)ProccessOutput.NotAccess;
        //        }

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            view = "",
        //            result = resulted.ToString(),
        //            msg = EnumMessage,
        //            msgerror = "",
        //            DataJson = "",
        //            htmlmsg = htmlkolomnotfound,
        //            datapersend = "",
        //            totaldata = lineCounter
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnOrderViewForEdit(string menu, string caption, string Selectdwn)
        //{


        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;
        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {

        //        string[] SelectdwnArray = Selectdwn.Split(',');

        //        //get menudesccriptio//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
        //        // get metrik user akses by module id//
        //        vmContracts.AccountDetail = vmAccount.AccountDetail;
        //        vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

        //        //int totaldatapending = 0;
        //        await Task.Delay(0);

        //        //get data to txtfile //
        //        string kodecabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //        List<string> filepath = OwinLibrary.SetgetFilepath(kodecabang, vmAccount.AccountDetail.UserType, (int)UserType.FDCM, vmAccount.AccountDetail.UserID, System.Web.HttpContext.Current);

        //        var item = TempData["confirmviewgrid"] as List<cContractsOrderRegisList>;
        //        var selectdata = item.Where(x => x.keylookupdata == SelectdwnArray[0]).ToList();

        //        //get data to txtfile //
        //        TempData["confirmviewgrid"] = vmContracts.DetailOrderRegisList;

        //        string branchjson = new JavaScriptSerializer().Serialize(selectdata);
        //        return Json(branchjson, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnCancelOrderView(string menu, string caption, string Selectdwn)
        //{


        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;
        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {

        //        string[] SelectdwnArray = Selectdwn.Split(',');

        //        //get menudesccriptio//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
        //        // get metrik user akses by module id//
        //        vmContracts.AccountDetail = vmAccount.AccountDetail;
        //        vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

        //        //int totaldatapending = 0;
        //        string viewhtml = "";
        //        await Task.Delay(0);

        //        //get data to txtfile //
        //        string kodecabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //        List<string> filepath = OwinLibrary.SetgetFilepath(kodecabang, vmAccount.AccountDetail.UserType, (int)UserType.FDCM, vmAccount.AccountDetail.UserID, System.Web.HttpContext.Current);

        //        var item = TempData["confirmviewgrid"] as List<cContractsOrderRegisList>;

        //        foreach (string items in SelectdwnArray)
        //        {
        //            var nokon = item.Where(x => x.keylookupdata == items).Select(x => x.NoPerjanjian).SingleOrDefault();
        //            var lines = System.IO.File.ReadAllLines(filepath[0]).Where(line => !line.Trim().Contains(nokon)).ToArray();
        //            System.IO.File.WriteAllLines(filepath[0], lines);

        //            vmContracts.DetailOrderRegisList = OwinLibrary.GetDataViewPending(filepath[0]);
        //            //get data to txtfile //
        //        }

        //        TempData["confirmviewgrid"] = vmContracts.DetailOrderRegisList;

        //        viewhtml = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Contracts/_uiGridContractOrderRegis.cshtml", vmContracts);

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            view = viewhtml,
        //            result = "",
        //            msg = "Data Pengajuan yang anda pilih berhasil dibatalkan,silahkan cek kembali",
        //            msgerror = "",
        //            DataJson = "",
        //            htmlmsg = "",
        //            totaldata = vmContracts.DetailOrderRegisList.Count
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnConfirmOrderView(string menu, string caption, string verifiedcode, string sended, string typeops = "")
        //{

        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;
        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {


        //        //get menudesccriptio//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
        //        // get metrik user akses by module id//
        //        vmContracts.AccountDetail = vmAccount.AccountDetail;
        //        vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

        //        int totaldatapending = 0;
        //        string viewhtml = "";
        //        await Task.Delay(0);

        //        //get data to txtfile //
        //        string kodecabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //        List<string> filepath = OwinLibrary.SetgetFilepath(kodecabang, vmAccount.AccountDetail.UserType, (int)UserType.FDCM, vmAccount.AccountDetail.UserID, System.Web.HttpContext.Current);
        //        vmContracts.DetailOrderRegisList = OwinLibrary.GetDataViewPending(filepath[0]);
        //        //get data to txtfile //

        //        if (typeops == "")
        //        {
        //            {
        //                viewhtml = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Contracts/_uiGridContractOrderRegis.cshtml", vmContracts);
        //                TempData["confirmviewgrid"] = vmContracts.DetailOrderRegisList;
        //            }
        //            return Json(new
        //            {
        //                view = viewhtml,
        //                totaldata = vmContracts.DetailOrderRegisList.Count
        //            });

        //        }
        //        else
        //        {
        //            string EnumMessage = "";
        //            string titleswl = "Informasi";
        //            string typeswl = "info";
        //            string txtbtnswl = "Tutup";
        //            bool swlcancel = true;
        //            string valid = "";
        //            string capresult = "";
        //            string capresultdetail = "";

        //            await Task.Delay(0);
        //            verifiedcode = verifiedcode ?? "";
        //            int resulted = 0;
        //            string templatename = "Order";
        //            bool checkkodeverifikasi = false;

        //            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        //            string ipAddress2 = Request.UserHostAddress;
        //            string HostPCName = Dns.GetHostName();

        //            //ambil flag untuk cek kode otp, kode flag harus 1/
        //            sended = HasKeyProtect.Decryption(sended);

        //            //jika kode otp=1 atau ada inputan kode veridikasi , maka harus ada pengecekan otp//
        //            checkkodeverifikasi = (sended == "1") ? true : false;

        //            vmAccount.AccountDetail.Email = "muhammad.hafid477@gmail.com";
        //            if ((vmContracts.DetailOrderRegisList.Count > 0) && vmContracts.DetailOrderRegisList != null)
        //            {
        //                var resultedexpired = 0;
        //                resulted = await OwinLibrary.dbOTPcheckvalid(vmAccount.AccountDetail.UserID, templatename, caption, vmAccount.AccountDetail.Email, HostPCName, ipAddress2, verifiedcode, checkkodeverifikasi);
        //                if (resulted != 1)
        //                {
        //                    resultedexpired = resulted;

        //                    //cek for the first time generate code//
        //                    checkkodeverifikasi = resultedexpired == (int)ProccessOutput.FilterNotValidKodeExpiredFirst ? false : checkkodeverifikasi;

        //                    //overide pengecekan : jika otpnya tidak expired maka checkkodeverifikasi jadikan false untuk dignerate ulang otpnya
        //                    checkkodeverifikasi = resultedexpired == (int)ProccessOutput.FilterNotValidKodeExpired ? false : checkkodeverifikasi;

        //                    //overide pengecekan : jika kode salah input jangan digenrate ulang otpnya //
        //                    checkkodeverifikasi = resultedexpired == (int)ProccessOutput.FilterValidoptgeneratecheck ? true : checkkodeverifikasi;

        //                    sended = HasKeyProtect.Encryption("1");

        //                    titleswl = "Masukan Kode Verifikasi";
        //                    typeswl = "input";
        //                    txtbtnswl = "Proses Konfirmasi";

        //                    if (checkkodeverifikasi == false)
        //                    {
        //                        verifiedcode = HashNetFramework.HasKeyProtect.GenerateOTP();

        //                        resulted = await OwinLibrary.dbOTPverifiedCode(vmAccount.AccountDetail.UserID, templatename, caption, vmAccount.AccountDetail.Email, HostPCName, ipAddress2, verifiedcode);
        //                        if (resulted == 1)
        //                        {
        //                            await vmAccount.dbaccounthostsave(vmAccount.UserLogin.UserID, HostPCName, ipAddress, "Request Code For " + menuitemdescription + " : " + verifiedcode).ConfigureAwait(false);
        //                            MessageEmail.sendEmail((int)EmailType.OtpOrderFidusia, vmAccount.AccountConfig, vmAccount.AccountDetail.Email, verifiedcode);
        //                        }

        //                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultedexpired);
        //                    }
        //                    else
        //                    {
        //                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
        //                    }

        //                    resulted = resultedexpired;
        //                }
        //                else
        //                {

        //                    //cek apakah sudah kirim data perhari ini  //
        //                    //if (!System.IO.File.Exists(filepath[1]))
        //                    //{

        //                    // resulted = await vmRoya.dbRoyaFidusiaSave(ListGrid, vmContracts.AccountDetail.ClientID, parcln, vmContracts.AccountDetail.IDCabang, vmContracts.AccountDetail.Email, templatename, caption, vmContracts.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);
        //                    EnumMessage = (resulted == 1) ? string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.Success)), menuitemdescription, " dikirim") : EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);

        //                    sended = "";

        //                    if (resulted == 1)
        //                    {
        //                        capresult = "Pengajuan Pendaftaran Fidusia Berhasil dikirim";
        //                        capresultdetail += "User Id  " + vmAccount.AccountDetail.UserID + " <br />";
        //                        capresultdetail += "Email " + vmAccount.AccountDetail.Email + " <br />";
        //                        capresultdetail += "Tanggal/pukul " + DateTime.Now.ToString("dd-MMMM-yyyy HH:mm:ss") + " <br /> ";
        //                        capresultdetail += "Telah melakukan proses pengajuan pendaftaran fidusia dengan jumlah kontrak sebanyak : " + vmContracts.DetailOrderRegisList.Count.ToString() + " kontrak";

        //                        swlcancel = false;

        //                        //replace file text done or push FTP or dropbox //
        //                        System.IO.File.Move(filepath[0], string.Format(filepath[1], DateTime.Now.ToString("HHmmss")));

        //                        // recon pending data //
        //                        totaldatapending = OwinLibrary.GetTotalPend(filepath[0]);
        //                        // recon pending data //

        //                    }

        //                    //}
        //                    //else
        //                    //{
        //                    //    // recon pending data //
        //                    //    resulted = 900;
        //                    //    EnumMessage = "Pengajuan Pendaftaran Fidusia sudah diajukan ";
        //                    //    totaldatapending = OwinLibrary.GetTotalPend(filepath[0]);
        //                    //    sended = "";
        //                    //    valid = "no";
        //                    //    // recon pending data //

        //                    //}
        //                }

        //            }
        //            else
        //            {
        //                resulted = (int)ProccessOutput.FilterValidDataUploadFound;
        //                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidDataUploadFound));
        //                valid = "no";
        //            }


        //            //send back to client browser//
        //            return Json(new
        //            {
        //                view = viewhtml,
        //                result = resulted.ToString(),
        //                msg = EnumMessage,
        //                msgerror = "",
        //                DataJson = "",
        //                htmlmsg = "",
        //                htmlvalid = valid,
        //                swltitle = titleswl,
        //                swltype = typeswl,
        //                swltxtbtn = txtbtnswl,
        //                swlcanceled = swlcancel,
        //                cmdput = sended,
        //                capresulted = capresult,
        //                capresultdetailed = capresultdetail,
        //                totaldata = totaldatapending
        //            });

        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnUploadOrderSendRegisFileone(string menu, string caption, cContractsOrderRegisList DetailOrderRegis)
        //{
        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;
        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {

        //        //get menudesccriptio//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
        //        // get metrik user akses by module id//
        //        vmContracts.AccountDetail = vmAccount.AccountDetail;
        //        vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

        //        string kodecabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //        //save data to txtfile //
        //        List<string> filepath = OwinLibrary.SetgetFilepath(kodecabang, vmAccount.AccountDetail.UserType, (int)UserType.FDCM, vmAccount.AccountDetail.UserID, System.Web.HttpContext.Current);
        //        resultsavetxt resulttext = OwinLibrary.SaveDataRegOneResultCountData(null, DetailOrderRegis, kodecabang, vmAccount.AccountDetail.UserType, filepath[0]);
        //        //save data to txtfile //

        //        await Task.Delay(0);

        //        //string verifiedcode = "";
        //        int resulted = 0;
        //        string templatename = TempData["Templatename"] as string;
        //        string EnumMessage = "";
        //        EnumMessage = (resulttext.status == 1) ? string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.Success)), "Data", " disimpan") : resulttext.MessageError;
        //        resulted = resulttext.status;
        //        int lineCounter = resulttext.countrecord;

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            view = "",
        //            result = resulted.ToString(),
        //            msg = EnumMessage,
        //            msgerror = "",
        //            DataJson = "",
        //            htmlmsg = "",
        //            datapersend = "",
        //            totaldata = lineCounter
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);

        //    }
        //}

        //#endregion order fidusia klien


        //#region Upload Document Cabang 
        //public async Task<ActionResult> clnContractUpload(string menu, string caption)
        //{
        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {
        //        //get menudesccriptio//
        //        string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

        //        // get information file upload//
        //        ViewBag.InformationUpload = EnumsDesc.GetDescriptionEnums((ProccessOutput)ProccessOutput.InvalidSize);



        //        //encription  module id //

        //        vmContracts.securemoduleID = HasKeyProtect.Encryption(caption);
        //        vmContracts.ClientLogin = vmAccount.AccountDetail.ClientID;

        //        // get document type for upload
        //        string doctype = HasKeyProtect.Decryption(vmContracts.ClientLogin) == "" ? "ALL" : "UPLOAD";
        //        vmContracts.ddlDocumentType = await CustomeVMModel.dbGetDocumentTypeListEncrypt(doctype, (int)ActionTypeDocument.Upload, vmAccount.AccountDetail.UserID);
        //        ViewData["SelectDocumentType"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlDocumentType);

        //        //populate clientid//
        //        vmContracts.ddlClient = await vmContracts.dbGetClientListByEncrypt();
        //        ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlClient);

        //        vmContracts.ddlkontrakFDC = await vmContracts.dbGetDdlContractByFDCListByEncrypt();
        //        ViewData["SelecCust"] = OwinLibrary.Get_SelectListItem(vmContracts.ddlkontrakFDC);


        //        //populate clientid//


        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;


        //        // senback to client browser//
        //        return Json(new
        //        {
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Contracts/uiContractUpload.cshtml", vmContracts),
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 406;
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //        });
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnContractUploadDelete(string codeidimg, string secureContractNo, string securemoduleID, string jenisdocument, string nosertifikat, string tautanid)
        //{
        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        Response.StatusCode = 406;
        //        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
        //    }

        //    try
        //    {
        //        // get user group & akses //
        //        string UserID = vmAccount.AccountDetail.UserID;
        //        string GroupName = vmAccount.AccountGroupUser.GroupName;

        //        //get session serach filter contract upload//
        //        cFilterContract modelfilter = TempData["contractlistfilter"] as cFilterContract;

        //        //try to delete file//
        //        modelfilter.idcaption = securemoduleID;
        //        modelfilter.ClientLogin = vmAccount.AccountDetail.ClientID;
        //        modelfilter.SelectClient = modelfilter.SelectClient;
        //        modelfilter.NoSertifikat = nosertifikat;
        //        modelfilter.NoPerjanjian = secureContractNo;
        //        modelfilter.JenisDocument = jenisdocument;


        //        //cek keyprotect login //
        //        string ClientID = HasKeyProtect.Decryption(vmAccount.AccountDetail.ClientID);
        //        string IDNotaris = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDNotaris);
        //        string IDCabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //        string email = (vmAccount.AccountDetail.Email ?? "");
        //        string usergencode = HasKeyProtect.Decryption(vmAccount.AccountDetail.UserGenCode);

        //        //set login key//
        //        string LoginAksesKey = UserID + email + ClientID + IDNotaris + IDCabang + usergencode;

        //        //key inputan user//
        //        tautanid = HasKeyProtect.Decryption(tautanid);

        //        // try to get document attacment //
        //        vmContracts.ContractDocumentOne = await vmContracts.dbGetDocumentByID(codeidimg, vmAccount.AccountDetail.ClientID, modelfilter.SelectClient, UserID, GroupName, securemoduleID);

        //        //document sertifikat ada //
        //        if (vmContracts.ContractDocumentOne != null)
        //        {

        //            //cek keyprotect dokumen //
        //            string ClientIDDoc = (vmContracts.ContractDocumentOne.client ?? "");
        //            string IDNotarisDoc = (vmAccount.AccountDetail.IDNotaris ?? "");
        //            string IDCabangDoc = (vmContracts.ContractDocumentOne.cabang ?? "");
        //            string keydocument = UserID + email + ClientID + IDNotaris + IDCabang + usergencode;


        //            string EnumMessage = "";
        //            string viewcontent = "";
        //            if ((tautanid == LoginAksesKey) && (LoginAksesKey == keydocument))
        //            {

        //                int result = await vmContracts.dbContractuploaddelete(codeidimg, modelfilter, UserID, GroupName);


        //                //refresh grid upload//

        //                string doctype = HasKeyProtect.Decryption(modelfilter.ClientLogin) == "" ? "ALL" : "UPLOAD";
        //                vmContracts.ContractDocument = await vmContracts.dbGetContractDocList(modelfilter, doctype, (int)ActionTypeDocument.View, UserID, GroupName);

        //                //set back securemodule//
        //                vmContracts.securemoduleID = securemoduleID;

        //                //set message result//
        //                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //                EnumMessage = result == 1 ? String.Format(EnumMessage, "Dokumen", "dihapus ") : EnumMessage;

        //                viewcontent = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Contracts/_uiGridupload.cshtml", vmContracts);
        //            }
        //            else
        //            {
        //                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKunciSandiFile));
        //                viewcontent = "";
        //            }

        //            //locate filter datakonrak to session
        //            TempData["contractlistfilter"] = modelfilter;


        //            // send back to client browser//
        //            return Json(new
        //            {
        //                view = viewcontent,
        //                result = "",
        //                msg = EnumMessage,
        //                msgerror = ""
        //            });
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterNotValidFileNotFound))
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 406;
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //        });
        //    }

        //}
        //#endregion Upload Document Cabang 


        //#region  Download FIle 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Attactmentdownload(string codeidimg, string securedmoduleID, string tautanid)
        //{

        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        return RedirectToRoute(vmAccount.UserLogin.RouteName);
        //    }

        //    try
        //    {

        //        //get filter data from session before//
        //        cFilterContract modFilter = TempData["contractlistfilter"] as cFilterContract;

        //        // get user group & akses //
        //        string UserID = vmAccount.AccountDetail.UserID;
        //        string GroupName = vmAccount.AccountGroupUser.GroupName;


        //        //set back filter data from session before//
        //        TempData["contractlistfilter"] = modFilter;


        //        //cek keyprotect login //
        //        string ClientID = HasKeyProtect.Decryption(vmAccount.AccountDetail.ClientID);
        //        string IDNotaris = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDNotaris);
        //        string IDCabang = HasKeyProtect.Decryption(vmAccount.AccountDetail.IDCabang);
        //        string email = (vmAccount.AccountDetail.Email ?? "");
        //        string usergencode = HasKeyProtect.Decryption(vmAccount.AccountDetail.UserGenCode);

        //        //set login key//
        //        string LoginAksesKey = UserID + email + ClientID + IDNotaris + IDCabang + usergencode;

        //        //key inputan user//
        //        tautanid = HasKeyProtect.Decryption(tautanid);

        //        // try to get document attacment //
        //        vmContracts.ContractDocumentOne = await vmContracts.dbGetDocumentByID(codeidimg, vmAccount.AccountDetail.ClientID, modFilter.SelectClient, UserID, GroupName, securedmoduleID);
        //        int result = vmContracts.ContractDocumentOne.Result;

        //        //cek keyprotect dokumen //
        //        string ClientIDDoc = (vmContracts.ContractDocumentOne.client ?? "");
        //        string IDNotarisDoc = (vmAccount.AccountDetail.IDNotaris ?? "");
        //        string IDCabangDoc = (vmContracts.ContractDocumentOne.cabang ?? "");
        //        string keydocument = UserID + email + ClientID + IDNotaris + IDCabang + usergencode;


        //        if ((tautanid == LoginAksesKey) && (LoginAksesKey == keydocument))
        //        {


        //            // get result proses//
        //            string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //            EnumMessage = result == 1 ? String.Format(EnumMessage, "Dokumen", " Diunduh ") : EnumMessage;

        //            //set back filter data from session before//
        //            TempData["contractlistfilter"] = modFilter;


        //            //check result 1: download file else user tidak ada akses//
        //            if (result == 1)
        //            {

        //                byte[] bytesToDecrypt = HasKeyProtect.GetFileByteDescript(vmContracts.ContractDocumentOne.FILE_BYTE, LoginAksesKey);

        //                return Json(new
        //                {
        //                    contenttype = vmContracts.ContractDocumentOne.CONTENT_TYPE,
        //                    bytetyipe = bytesToDecrypt,
        //                    filename = vmContracts.ContractDocumentOne.FILE_NAME,
        //                    msg = ""
        //                });
        //            }
        //            else
        //            {
        //                return Json(new
        //                {
        //                    msg = EnumMessage
        //                });
        //            }

        //        }
        //        else
        //        {

        //            return Json(new
        //            {
        //                msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKunciSandiFile))
        //            });

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 406;
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        //return RedirectToRoute("ErroPage");
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //        });
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> updatepaddock(string owneraction, string securenoper, string secureclen, string securedmoduleID, string secIdfdc, string actionopr)
        //{

        //    vmAccount = (vmAccount)Session["Account"];
        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

        //    bool IsErrorTimeout = false;

        //    if (vmAccount.UserLogin.RouteName != "")
        //    {
        //        //return RedirectToRoute(vmAccount.UserLogin.RouteName);
        //        IsErrorTimeout = true;
        //    }

        //    try
        //    {

        //        //get user identity host//
        //        string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        //        string hostname = Dns.GetHostName();

        //        string moduleid = (securedmoduleID ?? "") != "" ? HasKeyProtect.Decryption(securedmoduleID) : securedmoduleID ?? "";
        //        string idopener = (securenoper ?? "") != "" ? HasKeyProtect.Decryption(securenoper) : securenoper ?? "";
        //        string clientid = (secureclen ?? "") != "" ? HasKeyProtect.Decryption(secureclen) : secureclen ?? "";
        //        string id_fdc_client = (secIdfdc ?? "") != "" ? HasKeyProtect.Decryption(secIdfdc) : secIdfdc ?? "0";
        //        id_fdc_client = (id_fdc_client == "") ? "0" : id_fdc_client;

        //        actionopr = actionopr.ToLower().ToString() == "d" ? "Download" : "Print";

        //        await Task.Delay(0);

        //        int result = await vmContracts.dbGetSaveDocumentHistory(actionopr, clientid, idopener, int.Parse(id_fdc_client), ipAddress, hostname, moduleid, vmAccount.UserLogin.UserID, "");
        //        string msgtext = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);

        //        msgtext = result == 1 ? string.Format(msgtext, "Proses " + actionopr, "") : msgtext;

        //        return Json(new
        //        {
        //            msg = msgtext
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        Response.StatusCode = 406;
        //        Response.TrySkipIisCustomErrors = true;
        //        return Json(new
        //        {
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //            moderror = IsErrorTimeout
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion Download FIle 
    }
}
