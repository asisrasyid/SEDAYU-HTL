using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class VerifiedOrderRegisController : Controller
    {

        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmVeryfiedOrderRegis VeryfiedOrderRegis = new vmVeryfiedOrderRegis();
        vmVeryfiedOrderRegisddl VeryfiedOrderRegisddl = new vmVeryfiedOrderRegisddl();
        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        blVeryfiedOrderRegis lgVeryfiedOrder = new blVeryfiedOrderRegis();


        #region Veryfide Order
        public async Task<ActionResult> clnContractReconRegis(string menu, string caption)
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
                string fromdate = DateTime.Now.ToString("dd-MMMM-yyyy");
                string todate = DateTime.Now.ToString("dd-MMMM-yyyy");
                string SelectJen = "1";

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
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;

                modFilter.SelectJenCek = SelectJen;

                modFilter.PageNumber = PageNumber;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);

                //List<String> recordPage = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedListCount(SelectClient, NoPerjanjian, fromdate, todate, SelectJen, PageNumber, caption, UserID, GroupName);
                //TotalRecord = Convert.ToDouble(recordPage[0]);
                //TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = 100;
                pagenumberclient = PageNumber;
                //List<DataTable> dtlist = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedList(null, SelectClient, NoPerjanjian, fromdate, todate, SelectJen, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                //totalRecordclient = dtlist[0].Rows.Count;
                //totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                List<DataTable> dtlist = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedListDash(null, SelectClient, NoPerjanjian, fromdate, todate, SelectJen, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                VeryfiedOrderRegis.DTOrdersFromDB = dtlist[0];
                VeryfiedOrderRegis.DTDetailForGrid = dtlist[1];
                VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                VeryfiedOrderRegis.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["VeryfiedOrderListTxt"] = VeryfiedOrderRegis;
                TempData["VeryfiedOrderListFilterTxt"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "VerifiedOrderRegis";
                ViewBag.action = "clnContractReconRegis";

                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/VeryfiedOrder/uiContractsTrackTxt.cshtml", VeryfiedOrderRegis),
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
                modFilter = TempData["VeryfiedOrderListFilterTxt"] as cFilterContract;
                VeryfiedOrderRegis = TempData["VeryfiedOrderListTxt"] as vmVeryfiedOrderRegis;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectClient = modFilter.SelectClient;
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";
                string SelectJenCek = modFilter.SelectJenCek ?? "1";
                string fromdate = modFilter.fromdate ?? DateTime.Now.ToString("dd-MMMM-yyyy");
                string todate = modFilter.todate ?? DateTime.Now.ToString("dd-MMMM-yyyy");

                //decript for db//
                string decSelectClient = HasKeyProtect.Decryption(SelectClient);

                // try make filter initial & set secure module name //
                if (Common.ddlClient == null)
                {
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(decSelectClient);
                }
                //if (Common.ddlJenCek == null)
                {
                    Common.ddlJenCek = await Commonddl.dbddlgetparamenumsList("TEMPCEK");
                }

                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectDocStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlDocStatus);
                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectJenCek"] = OwinLibrary.Get_SelectListItem(Common.ddlJenCek).OrderBy(x => int.Parse(x.Value));


                TempData["VeryfiedOrderListTxt"] = VeryfiedOrderRegis;
                TempData["VeryfiedOrderListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectClient,
                    opsi2 = SelectJenCek,
                    opsi3 = "",
                    opsi5 = NoPerjanjian,
                    opsi6 = fromdate,
                    opsi7 = todate,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/VeryfiedOrder/_uiFilterData.cshtml", VeryfiedOrderRegis.DetailFilter),
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
                modFilter = TempData["VeryfiedOrderListFilterTxt"] as cFilterContract;
                VeryfiedOrderRegis = TempData["VeryfiedOrderListTxt"] as vmVeryfiedOrderRegis;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = model.NoPerjanjian ?? "";
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string fromdate = model.fromdate;
                string todate = model.todate;
                string SelectJenCek = model.SelectJenCek ?? "1";


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

                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectJenCek = SelectJenCek;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = lgVeryfiedOrder.CheckFilterisasiDataTract(modFilter, download);
                if (validtxt == "")
                {

                    if (download != "1")
                    {
                        //descript some value for db//
                        SelectClient = HasKeyProtect.Decryption(SelectClient);
                        caption = HasKeyProtect.Decryption(caption);


                        // List<String> recordPage = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedListCount(SelectClient, NoPerjanjian, fromdate, todate, SelectJenCek, PageNumber, caption, UserID, GroupName);
                        //TotalRecord = Convert.ToDouble(recordPage[0]);
                        //TotalPage = Convert.ToDouble(recordPage[1]);
                        pagingsizeclient = 100;
                        pagenumberclient = PageNumber;
                        //List<DataTable> dtlist = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedList(null, SelectClient, NoPerjanjian, fromdate, todate, SelectJenCek, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                        //totalRecordclient = dtlist[0].Rows.Count;
                        //totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                        List<DataTable> dtlist = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedListDash(null, SelectClient, NoPerjanjian, fromdate, todate, SelectJenCek, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                        VeryfiedOrderRegis.DTOrdersFromDB = dtlist[0];
                        VeryfiedOrderRegis.DTDetailForGrid = dtlist[1];
                        VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                        VeryfiedOrderRegis.DetailFilter = modFilter;

                        //keep session filterisasi before//
                        TempData["VeryfiedOrderListTxt"] = VeryfiedOrderRegis;
                        TempData["VeryfiedOrderListFilterTxt"] = modFilter;
                        TempData["common"] = Common;

                        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                        var jendesc = Common.ddlJenCek.Where(x => x.Value == SelectJenCek).Select(x => x.Text).SingleOrDefault();
                        ViewBag.jeniscek = jendesc;

                    }

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/VeryfiedOrder/_uiGridContractTrackSmry.cshtml", VeryfiedOrderRegis),
                        download = "",
                        message = validtxt
                    });

                    //else
                    //{
                    //    Commonddl.dbddlgetparamenumsList()


                    //}
                }
                else
                {

                    TempData["VeryfiedOrderListTxt"] = VeryfiedOrderRegis;
                    TempData["VeryfiedOrderListFilterTxt"] = modFilter;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnVerifiedContractReconRegis(HttpPostedFileBase files, string ContNotValid, string Verified)
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

                //// get from session //
                modFilter = TempData["VeryfiedOrderListFilterTxt"] as cFilterContract;
                VeryfiedOrderRegis = TempData["VeryfiedOrderListTxt"] as vmVeryfiedOrderRegis;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string SelectJenCek = modFilter.SelectJenCek ?? "";


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

                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectJenCek = SelectJenCek;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//


                SelectClient = HasKeyProtect.Decryption(SelectClient);
                caption = HasKeyProtect.Decryption(caption);

                string resulted = "";
                string validtxt = "";
                DataTable dt = new DataTable();

                validtxt = "SIlahkan Tautakan File ";
                if (files != null)
                {
                    if (!files.ContentType.Contains("text/plain"))
                    {
                        validtxt = "Extention File harus .txt";
                    }
                    else
                    {
                        string resulttxt = string.Empty;
                        using (BinaryReader b = new BinaryReader(files.InputStream))
                        {
                            byte[] binData = b.ReadBytes(files.ContentLength);
                            resulttxt = System.Text.Encoding.UTF8.GetString(binData);
                        }
                        dt = OwinLibrary.ConvertByteToDT(resulttxt);

                    }
                }

                int result = 0;
                if (dt.Rows.Count > 0)
                {
                    result = await VeryfiedOrderRegisddl.dbupdateVerifikasi(dt, bool.Parse(Verified), bool.Parse(ContNotValid), caption, UserID, GroupName);
                }

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1) ? String.Format(EnumMessage, "Data berhasil ", "Diverifikasi") : EnumMessage;
                validtxt = (result == 1) ? "" : EnumMessage;

                if (validtxt == "")
                {

                    validtxt = EnumMessage;
                    resulted = result.ToString();

                    if (result == 1)
                    {


                        // List<String> recordPage = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedListCount(SelectClient, NoPerjanjian, fromdate, todate, SelectJenCek, PageNumber, caption, UserID, GroupName);
                        //TotalRecord = Convert.ToDouble(recordPage[0]);
                        //TotalPage = Convert.ToDouble(recordPage[1]);
                        pagingsizeclient = 100;
                        pagenumberclient = PageNumber;
                        //List<DataTable> dtlist = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedList(null, SelectClient, NoPerjanjian, fromdate, todate, SelectJenCek, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                        //totalRecordclient = dtlist[0].Rows.Count;
                        //totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                        List<DataTable> dtlist = await VeryfiedOrderRegisddl.dbGetTrackingOrderRegveryfiedListDash(null, SelectClient, NoPerjanjian, fromdate, todate, SelectJenCek, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                        VeryfiedOrderRegis.DTOrdersFromDB = dtlist[0];
                        VeryfiedOrderRegis.DTDetailForGrid = dtlist[1];
                        VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                        VeryfiedOrderRegis.DetailFilter = modFilter;
                    }

                }

                //keep session filterisasi before//
                TempData["VeryfiedOrderListTxt"] = VeryfiedOrderRegis;
                TempData["VeryfiedOrderListFilterTxt"] = modFilter;
                TempData["common"] = Common;


                //sendback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/VeryfiedOrder/_uiGridContractTrackSmry.cshtml", VeryfiedOrderRegis),
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


        public async Task<ActionResult> clnViewDetailVerifikasi(string jenisvery)
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
                modFilter = TempData["VeryfiedOrderListFilterTxt"] as cFilterContract;
                VeryfiedOrderRegis = TempData["VeryfiedOrderListTxt"] as vmVeryfiedOrderRegis;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string SelectJenCek = jenisvery;

                //set default for paging//
                int PageNumber = 1;
                bool isModeFilter = true;

                //set filter//
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.SelectClient = SelectClient;

                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectJenCek = SelectJenCek;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                SelectClient = HasKeyProtect.Decryption(SelectClient);
                caption = HasKeyProtect.Decryption(caption);


                VeryfiedOrderRegis.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["VeryfiedOrderListTxt"] = VeryfiedOrderRegis;
                TempData["VeryfiedOrderListFilterTxt"] = modFilter;

                //// set caption menut text //
                //ViewBag.menu = menu;
                //ViewBag.caption = caption;
                //ViewBag.captiondesc = menuitemdescription;
                //ViewBag.rute = "VerifiedOrderRegis";
                //ViewBag.action = "clnContractReconRegis";

                //ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/VeryfiedOrder/_uiPopupContractDetail.cshtml", VeryfiedOrderRegis),
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
        public ActionResult ExcelExportDetailVerifikasi(string tipemodule = "")
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
                modFilter = TempData["VeryfiedOrderListFilterTxt"] as cFilterContract;
                VeryfiedOrderRegis = TempData["VeryfiedOrderListTxt"] as vmVeryfiedOrderRegis;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;


                TempData["VeryfiedOrderListFilterTxt"] = modFilter;
                TempData["VeryfiedOrderListTxt"] = VeryfiedOrderRegis;
                TempData["common"] = Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string SelectJenCek = modFilter.SelectJenCek ?? "";


                //set default for paging//
                int PageNumber = 1;
                double pagenumberclient = 0;
                bool isModeFilter = true;


                //set filter//
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.SelectClient = SelectClient;

                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectJenCek = SelectJenCek;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                SelectClient = HasKeyProtect.Decryption(SelectClient);
                caption = HasKeyProtect.Decryption(caption);


                pagenumberclient = PageNumber;
                DataTable dt = VeryfiedOrderRegisddl.dbGetVerifikasiExport(SelectClient, NoPerjanjian, fromdate, todate, tipemodule, PageNumber, caption, UserID, GroupName);


                StringBuilder sb = new StringBuilder();

                sb.Append("<table border='1px' >");

                ////LINQ to get Column names

                sb.Append("<tr> ");
                //Looping through the column names
                foreach (var col in dt.Columns)
                    sb.Append("<td style='background-color:rgb(251, 202, 3)'>" + col + "</td>");
                sb.Append("</tr>");

                //Looping through the records
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr valign='middle' >");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName.Contains("NO_IDENTITAS"))
                        {
                            sb.Append("<td>'" + dr[dc].ToString() + "</td>");
                        }
                        else
                             if (dc.ColumnName.Contains("CLNT_ID"))
                        {
                            sb.Append("<td>'" + dr[dc].ToString() + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + dr[dc].ToString() + "</td>");
                        }
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</table>");



                string filename = "Data_Verifikasi_KodeVerifikasi " + SelectJenCek + ".xls";

                //Writing StringBuilder content to an excel file.
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                Response.Buffer = true;
                Response.ClearHeaders();
                //Response.ContentType = "application/vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8";
                //Response.ContentType = "text/xml";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Write(sb.ToString());
                Response.Flush();
                Response.Close();

                return View();

            }
            catch (Exception ex)
            {

                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });

                if (IsErrorTimeout == false)
                {
                    return RedirectToRoute("ErroPage");
                }else
                {
                    return RedirectToRoute("DefaultExpired");
                }
            }

        }

        #endregion Veryfide Order






    }
}
