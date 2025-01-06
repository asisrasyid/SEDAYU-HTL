using HashNetFramework;
using Spire.Pdf;
using Spire.Pdf.AutomaticFields;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class FinanceController : Controller
    {
        private vmAccount Account = new vmAccount();
        private blAccount lgAccount = new blAccount();
        private vmFinance Finance = new vmFinance();
        private vmFinanceddl Financeddl = new vmFinanceddl();
        private cFilterContract modFilter = new cFilterContract();
        private vmCommon Common = new vmCommon();
        private vmCommonddl Commonddl = new vmCommonddl();
        private blFinance lgFinance = new blFinance();
        private vmHTLddl HTLddl = new vmHTLddl();

        /*

        #region invoice tax

        public async Task<ActionResult> clnFakturRegis(string menu, string caption)
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
                string NomorFaktur = "";

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
                modFilter.NoPerjanjian = NomorFaktur;

                modFilter.PageNumber = PageNumber;

                ////descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);

                List<String> recordPage = await Financeddl.dbGetListCountFakturRegis(SelectClient, fromdate, SelectRequest, NomorFaktur, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Financeddl.dbGetListFakturRegis(null, SelectClient, fromdate, SelectRequest, NomorFaktur, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                Finance.DTOrdersFromDB = dtlist[0];
                Finance.DTDetailForGrid = dtlist[1];
                //VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                Finance.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["FinanceTaxListTxt"] = Finance;
                TempData["FinanceTaxListFilterTxt"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Finance";
                ViewBag.action = "clnFakturRegis";

                string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/uiFakturRegis.cshtml", Finance),
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

        public async Task<ActionResult> clnOpenAddUplodFakturRegis()
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
                modFilter = TempData["FinanceTaxListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceTaxListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                // try make filter initial & set secure module name //
                if (Common.ddlJenisKontrak == null)
                {
                    Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                }
                ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

                TempData["FinanceTaxListTxt"] = Finance;
                TempData["FinanceTaxListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiFakturRegisUpload.cshtml", Finance.DetailFilter),
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

        public async Task<ActionResult> clnOpenFilterpopFakturRegis()
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
                modFilter = TempData["FinanceTaxListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceTaxListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectRequest = modFilter.SelectRequest;
                string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
                string fromdate = modFilter.fromdate ?? "";

                // try make filter initial & set secure module name //
                if (Common.ddlJenisRequest == null)
                {
                    Common.ddlJenisRequest = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                }

                if (Common.ddlJenisRequestStatus == null)
                {
                    Common.ddlJenisRequestStatus = await Commonddl.dbddlgetparamenumsList("REQ_TYPE_S");
                }

                ViewData["SelectRequest"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequest);
                ViewData["SelectRequestStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequestStatus);

                //set session filterisasi //
                TempData["FinanceTaxListTxt"] = Finance;
                TempData["FinanceTaxListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectRequest,
                    opsi2 = SelectRequestStatus,
                    opsi3 = "",
                    opsi5 = "",
                    opsi6 = fromdate,
                    opsi7 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiFilterDataFakturRegis.cshtml", Finance.DetailFilter),
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
        public async Task<ActionResult> clnListFilterFakturRegis(cFilterContract model, string download)
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
                modFilter = TempData["FinanceTaxListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceTaxListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter before//
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string SelectRequest = model.SelectRequest ?? "";
                string SelectRequestStatus = model.SelectRequestStatus ?? "0";
                string fromdate = modFilter.fromdate ?? "";
                string NoFaktur = model.NoPerjanjian ?? "";

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
                modFilter.SelectRequest = SelectRequest;
                modFilter.SelectRequestStatus = SelectRequestStatus;
                modFilter.fromdate = fromdate;
                modFilter.NoPerjanjian = NoFaktur;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = ""; // lgOrder.CheckFilterisasiData(modFilter, download);
                if (validtxt == "")
                {
                    ////descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    caption = HasKeyProtect.Decryption(caption);

                    List<String> recordPage = await Financeddl.dbGetListCountFakturRegis(SelectClient, fromdate, SelectRequest, NoFaktur, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Financeddl.dbGetListFakturRegis(null, SelectClient, fromdate, SelectRequest, NoFaktur, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    Finance.DTOrdersFromDB = dtlist[0];
                    Finance.DTDetailForGrid = dtlist[1];
                    Finance.DetailFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["FinanceTaxListFilterTxt"] = modFilter;
                    TempData["FinanceTaxListTxt"] = Finance;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridFakturRegis.cshtml", Finance),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {
                    TempData["FinanceTaxListFilterTxt"] = modFilter;
                    TempData["FinanceTaxListTxt"] = Finance;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnFakturRegisup(HttpPostedFileBase files, cFilterContract model)
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
                modFilter = TempData["FinanceTaxListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceTaxListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                ////get value from aply filter //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectRequest = modFilter.SelectRequest ?? "";
                string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
                string fromdate = modFilter.fromdate ?? "";
                string NoFaktur = modFilter.NoPerjanjian ?? "";

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
                modFilter.SelectRequest = SelectRequest;
                modFilter.SelectRequestStatus = SelectRequestStatus;
                modFilter.fromdate = fromdate;
                modFilter.NoPerjanjian = NoFaktur;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                byte[] filbyte = null;
                string resulted = "";
                string validtxt = "";
                DataTable dt = new DataTable();

                validtxt = "";

                if ((model.fromdate ?? "") == "")
                {
                    validtxt = "Isikan periode invoice";
                }

                if ((model.SelectJenisKontrak ?? "") == "")
                {
                    validtxt = "Pilih jenis pendaftaran";
                }

                if ((model.NoPerjanjian ?? "") == "")
                {
                    validtxt = "Isikan no faktur";
                }

                if ((model.fromdate ?? "") != "")
                {
                    if (DateTime.Parse(model.fromdate) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        validtxt = "Periode Invoice harus lebih besar/sama dengan tanggal hari ini";
                    }
                }

                int result = 0;
                DataTable resultdt = new DataTable();
                if (validtxt == "")
                {
                    resultdt = await Financeddl.dbGetFakturRegisupd(model.NoPerjanjian, model.SelectJenisKontrak, 0, model.fromdate, "0", 0, filbyte, "", caption, UserID, GroupName);
                    result = int.Parse(resultdt.Rows[0][0].ToString());
                }

                string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", "Diproses Silahkan Cek Kembali") : EnumMessage;
                validtxt = (result == 1) ? "" : EnumMessage;

                if (validtxt == "")
                {
                    validtxt = EnumMessage;
                    resulted = result.ToString();
                }

                if (result == 1)
                {
                    //set paging in grid client//
                    List<DataTable> dtlist = await Financeddl.dbGetListFakturRegis(null, "", fromdate, SelectRequest, NoFaktur, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    Finance.DTOrdersFromDB = dtlist[0];
                    Finance.DTDetailForGrid = dtlist[1];
                }

                string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                //keep session filterisasi before//
                TempData["FinanceTaxListTxt"] = Finance;
                TempData["FinanceTaxListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                //sendback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridFakturRegis.cshtml", Finance),
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
        public async Task<ActionResult> clnFakturRegisrejt(string kelookup)
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
                modFilter = TempData["FinanceTaxListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceTaxListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                ////get value from aply filter //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectRequest = modFilter.SelectRequest ?? "";
                string SelectRequestStatus = modFilter.SelectRequestStatus ?? "";
                string fromdate = modFilter.fromdate ?? "";
                string NoFaktur = modFilter.NoPerjanjian ?? "";

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
                modFilter.SelectRequest = SelectRequest;
                modFilter.SelectRequestStatus = SelectRequestStatus;
                modFilter.fromdate = fromdate;
                modFilter.NoPerjanjian = NoFaktur;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                string resulted = "";
                string validtxt = "";
                DataTable dt = new DataTable();

                validtxt = "";

                int result = 0;
                DataTable resultdt = new DataTable();

                //string reqid = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).Select(x => x.Field<int>("ID")).SingleOrDefault().ToString();

                DataTable dtx = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).CopyToDataTable();
                string reqid = dtx.Rows[0]["ID"].ToString();

                resultdt = await Financeddl.dbGetFakturRegisupd("", "", dt.Rows.Count, null, reqid, 2, null, "", caption, UserID, GroupName);
                result = int.Parse(resultdt.Rows[0][0].ToString());

                string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan ", "Dibatalkan, Silahkan Cek Kembali") : EnumMessage;
                validtxt = (result == 1) ? "" : EnumMessage;

                if (validtxt == "")
                {
                    validtxt = EnumMessage;
                    resulted = result.ToString();
                }

                if (result == 1)
                {
                    //set paging in grid client//
                    List<DataTable> dtlist = await Financeddl.dbGetListFakturRegis(null, "", fromdate, SelectRequest, NoFaktur, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    Finance.DTOrdersFromDB = dtlist[0];
                    Finance.DTDetailForGrid = dtlist[1];
                }

                string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                //keep session filterisasi before//
                TempData["FinanceTaxListTxt"] = Finance;
                TempData["FinanceTaxListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                //sendback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridFakturRegis.cshtml", Finance),
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

        #endregion invoice tax

    */

        #region billing invoice

        public async Task<ActionResult> clnBillPaymentRegisINVPAT(string menu, string caption)
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

                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                modFilter.ModuleName = caption;

                ////set to object pendataran//
                Finance.DTOrdersFromDB = new DataTable();
                Finance.DTDetailForGrid = new DataTable();
                Finance.Permission = PermisionModule;
                //VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                Finance.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Finance";
                ViewBag.action = "clnBillPaymentRegisINVPAT";

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/uiBillingPaymentINVPAT.cshtml", Finance),
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

        public async Task<ActionResult> clnOpenAddUplodINVPAT(string gontok)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);

                string viewpo = "/Views/Finance/_uiBillingPaymentUploadINVPAT.cshtml";
                gontok = (gontok ?? "");
                if (gontok != "")
                {
                    gontok = HasKeyProtect.Decryption(gontok);
                }

                //if (gontok == "createinvoice")
                //{
                //} else
                //// try make filter initial & set secure module name //
                Common.ddlJenisDokumen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENINV", caption, UserID, GroupName);
                ViewData["JENINV"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);

                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewpo, Finance.DetailFilter),
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
        public async Task<ActionResult> clnPaymentRegisINVPAT(string JenisTransaksi, string gontok, string NoPengajuanRequest)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //////get value from aply filter //
                //string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                //string SelectRequest = modFilter.SelectRequest ?? "";
                //string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
                //string fromdate = modFilter.fromdate ?? "";
                JenisTransaksi = HasKeyProtect.Decryption(JenisTransaksi ?? "");
                gontok = (gontok ?? "") == "" ? "" : HasKeyProtect.Decryption(gontok ?? "");
                ////set default for paging //
                //int PageNumber = 1;
                //double TotalRecord = modFilter.TotalRecord;
                //double TotalPage = 0;
                //double pagingsizeclient = modFilter.pagingsizeclient;
                //double pagenumberclient = modFilter.pagenumberclient;
                //double totalRecordclient = 0;
                //double totalPageclient = 0;
                //bool isModeFilter = modFilter.isModeFilter;

                ////set filter//
                //modFilter.SelectClient = SelectClient;
                //modFilter.SelectRequest = SelectRequest;
                //modFilter.SelectRequestStatus = SelectRequestStatus;
                //modFilter.fromdate = fromdate;

                //modFilter.PageNumber = PageNumber;
                //modFilter.isModeFilter = isModeFilter;
                ////set filter//

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                string resulted = "";
                string validtxt = "";
                string viewpathed = "";
                validtxt = "";
                int result = 0;
                string namatemplte = "";
                DataTable resultdt = new DataTable();
                DataTable resultdt4inv = new DataTable();
                NoPengajuanRequest = NoPengajuanRequest ?? "";
                JenisTransaksi = JenisTransaksi == "-9999" ? "recap" : JenisTransaksi;
                JenisTransaksi = JenisTransaksi + gontok ?? "";

                //cek validasi invoice recap//
                DataTable dtvalid = await HTLddl.dbgetInvPPATvalid("9", UserID, caption, UserID, GroupName);
                validtxt = dtvalid.Rows[0][0].ToString();
                if ((NoPengajuanRequest ?? "") != "" || JenisTransaksi == "recapcreateinvoice")
                {
                    validtxt = "";
                }

                if (gontok == "createinvoice")
                {
                    DataTable dtresultod = await HTLddl.dbgetOrderOS("9", UserID, caption, UserID, GroupName);
                    int totalpendht = int.Parse(dtresultod.Rows[0][0].ToString());
                    int limithriht = int.Parse(dtresultod.Rows[0][1].ToString());
                    int resultpendht = int.Parse(dtresultod.Rows[0][2].ToString());
                    string EnumMessagedht = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultpendht);

                    dtresultod = await HTLddl.dbgetOrderOS("10", UserID, caption, UserID, GroupName);
                    int totalpendbast = int.Parse(dtresultod.Rows[0][0].ToString());
                    int limithribast = int.Parse(dtresultod.Rows[0][1].ToString());
                    int resultpendbast = int.Parse(dtresultod.Rows[0][2].ToString());
                    string EnumMessagedbast = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultpendbast);

                    resultdt4inv = Finance.DTREKAP;
                    if (resultdt4inv.Rows.Count <= 0)
                    {
                        validtxt = "Tidak ada data tagihan yang akan dibuat invoice";
                    }
                    else if (totalpendht > 0)
                    {
                        validtxt = string.Format(EnumMessagedht, totalpendht, limithriht);
                    }
                    else if (totalpendbast > 0)
                    {
                        validtxt = string.Format(EnumMessagedbast, totalpendbast, limithribast);
                    }
                    else
                    {
                        resultdt = Finance.DTREKAP.DefaultView.ToTable(false, new string[] { "No Aplikasi", "NoInv", "No Perjanjian", "NoSertifikat" }); ;
                        resultdt.Columns[0].ColumnName = "Key1";
                        resultdt.Columns[1].ColumnName = "Key2";
                        resultdt.Columns[2].ColumnName = "Key3";
                        resultdt.Columns[3].ColumnName = "Key4";
                        NoPengajuanRequest = resultdt.Rows[0][1].ToString();
                    }
                }
                else
                {
                    if (resultdt.Columns.Count <= 0)
                    {
                        resultdt.Columns.Add("Key1");
                        resultdt.Columns.Add("Key2");
                        resultdt.Columns.Add("Key3");
                        resultdt.Columns.Add("Key4");
                    }
                }

                //if (JenisTransaksi == "" || JenisTransaksi == "N/A")
                //{
                //    validtxt = "Silahkan Pilih Jenis Tagihan";
                //    resulted = "99";

                //    if (Finance.DTREKAP == null)
                //    {
                //        Finance.DTREKAP = resultdt;
                //    }
                //    else
                //    {
                //        resultdt = Finance.DTREKAP;
                //    }
                //}

                string jenisfile = "";
                byte[] datarekp = null;
                string infotext = "Rekap Data Tagihan " + (NoPengajuanRequest != "" ? " No. " + NoPengajuanRequest : "");
                if (validtxt == "")
                {
                    //if (gontok == "createinvoice")
                    //{
                    //    resultdt4inv.Columns.Remove("NoInv");
                    //    resultdt4inv.Columns.Remove("NoRekening");
                    //    resultdt4inv.Columns.Remove("PemilikRekening");
                    //    resultdt4inv.Columns.Remove("NamaBank");
                    //    datarekp = DttpPdf.exportpdf(resultdt4inv, "Lampiran Invoice Pengecekan Sertifikat");
                    //}
                    resultdt = await Financeddl.dbupdatepaymentINVGENPPAT(JenisTransaksi, resultdt, datarekp, NoPengajuanRequest, "", caption, UserID, GroupName);
                    if (resultdt.Rows.Count > 0)
                    {
                        result = 1;
                        if (gontok == "createinvoice")
                        {
                            infotext = NoPengajuanRequest == "" ? "Pembuatan Invoice No. " : " Data Tagihan No. ";
                            NoPengajuanRequest = resultdt.Rows[0][0].ToString();
                            infotext = infotext + NoPengajuanRequest;

                            namatemplte = "PPAT"; // JenisTransaksi.Replace("createinvoice", "").Replace("/", "").ToUpper();
                            string sourceFile = Server.MapPath(Request.ApplicationPath) + "External\\TemplateINV\\" + namatemplte + ".docx";
                            using (MemoryStream pdfDocumentstream = new MemoryStream())
                            {
                                jenisfile = namatemplte.Replace("PENGCEKAN", "PENGECEKAN") + "_" + ((NoPengajuanRequest ?? "DRAFT") == "" ? "DRAFT" : NoPengajuanRequest) + "_" + DateTime.Now.ToString("yyyyMMdd");

                                Spire.Doc.Document doc = new Spire.Doc.Document();
                                doc.LoadFromFile(sourceFile);

                                string colvalue = "";
                                foreach (DataColumn col in resultdt4inv.Columns)
                                {
                                    string colname = col.ColumnName;
                                    if (colname.ToLower() == "noinv")
                                    {
                                        colvalue = resultdt4inv.Rows[0][colname].ToString() == "" ? NoPengajuanRequest : resultdt4inv.Rows[0][colname].ToString();
                                    }
                                    else
                                    {
                                        colvalue = resultdt4inv.Rows[0][colname].ToString();
                                    }
                                    doc.Replace("{" + colname + "}", colvalue, false, true);
                                }
                                doc.SaveToStream(pdfDocumentstream, Spire.Doc.FileFormat.PDF);

                                //prepare to genrate table 4 lampiran//
                                Spire.Pdf.PdfDocument docpdf = new Spire.Pdf.PdfDocument();
                                docpdf.LoadFromStream(pdfDocumentstream);

                                PdfSection sec = docpdf.Sections.Add();
                                sec.PageSettings.Width = PdfPageSize.A4.Width;
                                //sec.PageSettings.Orientation = PdfPageOrientation.Landscape;
                                PdfPageBase page = sec.Pages.Add();

                                float y = 10;

                                PdfBrush brush1 = PdfBrushes.Black;
                                PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 14f, FontStyle.Bold));
                                PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
                                page.Canvas.DrawString("LAMPIRAN INVOICE NO." + ((NoPengajuanRequest ?? "DRAFT-01") == "" ? "DRAFT-01" : NoPengajuanRequest), font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);

                                y = y + font1.MeasureString("Country List", format1).Height;
                                y = y + 5;

                                DataTable resultdtpdf = Finance.DTREKAP.DefaultView.ToTable(false, new string[]
                                { "No", "No Aplikasi","No Perjanjian","Tgl Perjanjian", "Nama Debitur","NoSertifikat","Nilai HT","Pinjaman_Konsumen","No HT","Kode Akta","StatusKontrack" });

                                //string dataheader = "No;No Aplikasi;Nama Debitur;NoSertifikat;WilayahHak;Nilai HT";
                                string[] dataheader = { "No;No Aplikasi;No Perjanjian;Tgl Perjanjian;Nama Debitur;No Sertipikat;Hak Tanggungan;Pinjaman Konsumen;No APHT;Kode Akta;Status" };

                                string[] datadetail = resultdtpdf.AsEnumerable().Select(item =>
                                     string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                                     item["No"],
                                     item["No Aplikasi"],
                                     item["No Perjanjian"],
                                     item["Tgl Perjanjian"],
                                     item["Nama Debitur"],
                                     item["NoSertifikat"],
                                     double.Parse(item["Nilai HT"].ToString()).ToString("N0"),
                                     double.Parse(item["Pinjaman_Konsumen"].ToString()).ToString("N0"),
                                     item["No HT"],
                                     item["Kode AKta"],
                                     item["StatusKontrack"])
                                 ).ToArray();

                                string[] data = dataheader.Union(datadetail).ToArray();

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

                                string[] listheader = dataheader[0].Split(';');
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
                                        table.Columns[i].Width = 10;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                    }
                                    if (listheader[i] == "Tgl Perjanjian")
                                    {
                                        table.Columns[i].Width = 8;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                    }
                                    if (listheader[i] == "Nama Debitur")
                                    {
                                        table.Columns[i].Width = 30;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                    }
                                    if (listheader[i] == "No Sertifikat")
                                    {
                                        table.Columns[i].Width = 30;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                    }
                                    if (listheader[i] == "Hak Tanggungan" || listheader[i] == "Pinjaman Konsumen")
                                    {
                                        table.Columns[i].Width = 12;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                                    }
                                    if (listheader[i] == "No APHT")
                                    {
                                        table.Columns[i].Width = 8;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                    }
                                    if (listheader[i] == "Kode Akta")
                                    {
                                        table.Columns[i].Width = 8;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                    }
                                    if (listheader[i] == "StatusKontrack")
                                    {
                                        table.Columns[i].Width = 10;
                                        table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                    }
                                }
                                table.Draw(page, new PointF(0, y));
                                docpdf.SaveToStream(pdfDocumentstream, FileFormat.PDF);

                                //footer//
                                Spire.Pdf.PdfDocument docfooter = new Spire.Pdf.PdfDocument();
                                docfooter.LoadFromStream(pdfDocumentstream);
                                SizeF pageSize = docfooter.Pages[0].Size;
                                float x = 90;
                                y = pageSize.Height - 72;
                                for (int i = 0; i < docfooter.Pages.Count; i++)
                                {
                                    //draw line at bottom
                                    PdfPen pen = new PdfPen(PdfBrushes.Gray, 0.5f);
                                    docfooter.Pages[i].Canvas.DrawLine(pen, x, y, pageSize.Width - x, y);

                                    //draw text at bottom
                                    PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("Arial", 8f));
                                    PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
                                    String footerText = "Powered by PT. Sedayu Dana Banda @ " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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

                                /*
                                //protect with digital signature
                                Spire.Pdf.PdfDocument docsing = new Spire.Pdf.PdfDocument();
                                docsing.LoadFromStream(pdfDocumentstream);
                                PdfCertificate cert = new PdfCertificate(@"d:\hafid.pfx", "SedayuDanaBanda");
                                var signature = new Spire.Pdf.Security.PdfSignature(docsing, docsing.Pages[0], cert, "Invoice");
                                signature.Bounds = new RectangleF(new PointF(280, 600), new SizeF(260, 90));
                                signature.DistinguishedName = "DN:";
                                signature.LocationInfoLabel = "Location:";
                                signature.LocationInfo = "London";
                                signature.ReasonLabel = "Reason: ";
                                signature.Reason = "Le document est certifie";
                                signature.DateLabel = "Date: ";
                                signature.Date = DateTime.Now;
                                signature.ContactInfoLabel = "Contact: ";
                                signature.ContactInfo = "123456789";
                                signature.Certificated = false;
                                signature.DocumentPermissions = PdfCertificationFlags.ForbidChanges;
                                */

                                datarekp = pdfDocumentstream.ToArray();
                            }
                            resultdt = new DataTable();
                            viewpathed = Convert.ToBase64String(datarekp, 0, datarekp.Length); //"Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockd=wako&file=";
                        }
                    }
                    else
                    {
                        resultdt = Finance.DTREKAP;
                    }
                }

                Finance.DTREKAP = resultdt;
                string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1) ? String.Format(EnumMessage, infotext, " Diproses, Silahkan Cek Kembali") : EnumMessage;
                validtxt = (result == 1) ? "" : EnumMessage;

                if (validtxt == "")
                {
                    validtxt = EnumMessage;
                    resulted = result.ToString();
                }

                if (result == 1)
                {
                    //back to set in filter//
                    modFilter.TotalRecord = Finance.DTREKAP.Rows.Count;
                    modFilter.TotalPage = 1;
                    modFilter.pagingsizeclient = 1500;
                    modFilter.totalRecordclient = 1500;
                    modFilter.totalPageclient = 1;
                    modFilter.pagenumberclient = 1;

                    Finance.DTOrdersFromDB = resultdt;
                    Finance.DTDetailForGrid = resultdt;

                    ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Data";
                }

                ViewBag.JenisTransaksi = JenisTransaksi;
                ViewBag.NoPengajuanRequest = NoPengajuanRequest;

                //keep session filterisasi before//
                modFilter.idcaption = HasKeyProtect.Encryption(caption);
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                //sendback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    bytetyipe = viewpathed,
                    typo = gontok,
                    invo = NoPengajuanRequest,
                    jnfle = jenisfile,
                    namatitle = namatemplte.ToLower() == "pengcekan" ? "PENGECEKAN SERTIPIKAT(No." + NoPengajuanRequest + ")" : "SKMHT/APHT(No." + NoPengajuanRequest + ")",
                    view = (resulted == "99") ? "" : CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadINVPAT.cshtml", Finance),
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

        public async Task<ActionResult> clnBillPaymentRegisINV(string menu, string caption)
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

                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                modFilter.ModuleName = caption;

                ////set to object pendataran//
                Finance.DTOrdersFromDB = new DataTable();
                Finance.DTDetailForGrid = new DataTable();
                Finance.Permission = PermisionModule;
                //VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                Finance.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Finance";
                ViewBag.action = "clnBillPaymentRegisINVPAT";

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/uiBillingPaymentINV.cshtml", Finance),
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

        public async Task<ActionResult> clnOpenAddUplodINVPD(string gontok)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);

                string viewpo = "/Views/Finance/_uiBillingPaymentGenINV.cshtml";
                gontok = (gontok ?? "");
                if (gontok != "")
                {
                    gontok = HasKeyProtect.Decryption(gontok);
                }

                //if (gontok == "createinvoice")
                //{
                //} else
                //// try make filter initial & set secure module name //
                Common.ddlJenisDokumen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("PAIDINV", caption, UserID, GroupName);
                ViewData["SelectTrans"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);

                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewpo, Finance.DetailFilter),
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
        public async Task<ActionResult> clnPaymentRegisINVPD(string JenisTransaksi, string gontok, string NoPengajuanRequest, string SelectNotaris, string RequestNo, string[] AktaSelectdwn)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                string JenisTransaksiencp = JenisTransaksi;
                string SelectNotarisecp = SelectNotaris;
                JenisTransaksi = HasKeyProtect.Decryption(JenisTransaksi ?? "");
                gontok = (gontok ?? "") == "" ? "" : HasKeyProtect.Decryption(gontok ?? "");

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                string resulted = "";
                string validtxt = "";
                validtxt = "";
                int result = 0;
                bool isdelete = false;
                string namatemplte = "";
                DataTable resultdt = new DataTable();
                DataTable resultdt4inv = new DataTable();
                NoPengajuanRequest = NoPengajuanRequest ?? "";
                JenisTransaksi = JenisTransaksi + gontok ?? "";

                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                SelectNotaris = (SelectNotaris == "-9999") ? "" : SelectNotaris;

                //if (gontok == "createinvoice")
                //{
                //    if (AktaSelectdwn != null)
                //    {
                //        foreach (var x in AktaSelectdwn)
                //        {
                //            DataRow[] dtr = Finance.DTREKAP.Select("keylookupdata='" + x + "'");
                //            foreach (var drow in dtr)
                //            {
                //                drow.Delete();
                //                isdelete = true;
                //            }
                //            Finance.DTREKAP.AcceptChanges();

                //            //load agian
                //            //Finance.DTREKAP = await Financeddl.dbupdatepaymentINVGEN(JenisTransaksi, resultdt, null, NoPengajuanRequest, "", SelectNotaris, RequestNo, caption, UserID, GroupName);

                //        }
                //        resultdt4inv = Finance.DTREKAP;
                //    }
                //    else
                //    {
                //        resultdt4inv = Finance.DTREKAP;
                //    }

                //    if (resultdt4inv.Rows.Count <= 0)
                //    {
                //        validtxt = "Tidak ada data tagihan yang akan dibuat invoice";
                //    }
                //    else
                //    {
                //        if (isdelete == true) //jika ada perubaha reload kembali
                //        {
                //            resultdt = Finance.DTREKAP.DefaultView.ToTable(false, new string[] { "No Aplikasi", "NoInv", "No Perjanjian", "NoSertifikat" }); ;
                //            resultdt.Columns[0].ColumnName = "Key1";
                //            resultdt.Columns[1].ColumnName = "Key2";
                //            resultdt.Columns[2].ColumnName = "Key3";
                //            resultdt.Columns[3].ColumnName = "Key4";
                //            Finance.DTREKAP = await Financeddl.dbupdatepaymentINVGEN(JenisTransaksi.Replace("createinvoice", ""), resultdt, null, "reload", "", SelectNotaris, RequestNo, caption, UserID, GroupName);
                //            resultdt4inv = Finance.DTREKAP;
                //        }
                //        else
                //        {
                //            resultdt = Finance.DTREKAP.DefaultView.ToTable(false, new string[] { "No Aplikasi", "NoInv", "No Perjanjian", "NoSertifikat" }); ;
                //            resultdt.Columns[0].ColumnName = "Key1";
                //            resultdt.Columns[1].ColumnName = "Key2";
                //            resultdt.Columns[2].ColumnName = "Key3";
                //            resultdt.Columns[3].ColumnName = "Key4";
                //        }

                //        NoPengajuanRequest = resultdt.Rows[0][1].ToString();
                //    }
                //}

                //if (JenisTransaksi == "" || JenisTransaksi == "N/A")
                //{
                //    validtxt = "Silahkan Pilih Jenis Tagihan";
                //    resulted = "99";
                //}

                string jenisfile = "";
                string viewpathed = "";
                byte[] datarekp = null;
                string infotext = "Rekap Data Tagihan " + (NoPengajuanRequest != "" ? " No. " + NoPengajuanRequest : "");
                namatemplte = JenisTransaksi.Replace("createinvoice", "").Replace("/", "").ToUpper();

                if (validtxt == "")
                {
                    resultdt = await Financeddl.dbupdatepaymentINVGEN(JenisTransaksi, resultdt, datarekp, NoPengajuanRequest, "", SelectNotaris, RequestNo, caption, UserID, GroupName);
                }
                string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1) ? String.Format(EnumMessage, infotext, " Diproses, Silahkan Cek Kembali") : EnumMessage;
                validtxt = (result == 1) ? "" : EnumMessage;

                if (validtxt == "")
                {
                    validtxt = EnumMessage;
                    resulted = result.ToString();
                }

                ViewBag.JenisTransaksi = JenisTransaksi;
                ViewBag.NoPengajuanRequest = NoPengajuanRequest;

                //keep session filterisasi before//
                modFilter.idcaption = HasKeyProtect.Encryption(caption);
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                //sendback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    bytetyipe = viewpathed,
                    typo = gontok,
                    invo = NoPengajuanRequest,
                    jnfle = jenisfile,
                    jntr = JenisTransaksiencp ?? "",
                    selnot = SelectNotarisecp ?? "",
                    noreq = RequestNo ?? "",
                    namatitle = "",
                    view = "",
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
        public async Task<ActionResult> clnOpenAddUplodINVFlag(string JenisTransaksi, HttpPostedFileBase upflebyr)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);

                DataTable dt = new DataTable();

                string resulttxt = string.Empty;
                using (BinaryReader b = new BinaryReader(upflebyr.InputStream))
                {
                    byte[] binData = b.ReadBytes(upflebyr.ContentLength);
                    resulttxt = System.Text.Encoding.UTF8.GetString(binData);
                }
                dt = OwinLibrary.ConvertByteToDT(resulttxt);
                int result = await Financeddl.dbupdatepaymentINVGENFlag(JenisTransaksi, dt, caption, UserID, GroupName);
                string errmessage = "";
                errmessage = result.ToString() + " data Berhasil diproses, silahkan dicek kembali";

                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
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

        public async Task<ActionResult> clnOpenAddUplodINV(string gontok)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);

                string viewpo = "/Views/Finance/_uiBillingPaymentUploadINV.cshtml";
                gontok = (gontok ?? "");
                if (gontok != "")
                {
                    gontok = HasKeyProtect.Decryption(gontok);
                }

                //if (gontok == "createinvoice")
                //{
                //} else
                //// try make filter initial & set secure module name //
                Common.ddlJenisDokumen = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENINV", caption, UserID, GroupName);
                ViewData["JENINV"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);

                Common.ddlnotaris = await Commonddl.dbdbGetDdlNotarisListByEncryptINV("xt", UserID, GroupName);
                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);

                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewpo, Finance.DetailFilter),
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
        public async Task<ActionResult> clnPaymentRegisINV(string JenisTransaksi, string gontok, string NoPengajuanRequest, string SelectNotaris, string RequestNo, string[] AktaSelectdwn)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //////get value from aply filter //
                //string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                //string SelectRequest = modFilter.SelectRequest ?? "";
                //string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
                //string fromdate = modFilter.fromdate ?? "";
                string JenisTransaksiencp = JenisTransaksi;
                string SelectNotarisecp = SelectNotaris;
                JenisTransaksi = HasKeyProtect.Decryption(JenisTransaksi ?? "");
                gontok = (gontok ?? "") == "" ? "" : HasKeyProtect.Decryption(gontok ?? "");
                ////set default for paging //
                //int PageNumber = 1;
                //double TotalRecord = modFilter.TotalRecord;
                //double TotalPage = 0;
                //double pagingsizeclient = modFilter.pagingsizeclient;
                //double pagenumberclient = modFilter.pagenumberclient;
                //double totalRecordclient = 0;
                //double totalPageclient = 0;
                //bool isModeFilter = modFilter.isModeFilter;

                ////set filter//
                //modFilter.SelectClient = SelectClient;
                //modFilter.SelectRequest = SelectRequest;
                //modFilter.SelectRequestStatus = SelectRequestStatus;
                //modFilter.fromdate = fromdate;

                //modFilter.PageNumber = PageNumber;
                //modFilter.isModeFilter = isModeFilter;
                ////set filter//

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                string resulted = "";
                string validtxt = "";
                validtxt = "";
                int result = 0;
                bool isdelete = false;
                string namatemplte = "";
                DataTable resultdt = new DataTable();
                DataTable resultdt4inv = new DataTable();
                NoPengajuanRequest = NoPengajuanRequest ?? "";
                JenisTransaksi = JenisTransaksi + gontok ?? "";

                SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                SelectNotaris = (SelectNotaris == "-9999") ? "" : SelectNotaris;

                if (gontok == "createinvoice")
                {
                    if (AktaSelectdwn != null)
                    {
                        foreach (var x in AktaSelectdwn)
                        {
                            DataRow[] dtr = Finance.DTREKAP.Select("keylookupdata='" + x + "'");
                            foreach (var drow in dtr)
                            {
                                drow.Delete();
                                isdelete = true;
                            }
                            Finance.DTREKAP.AcceptChanges();

                            //load agian
                            //Finance.DTREKAP = await Financeddl.dbupdatepaymentINVGEN(JenisTransaksi, resultdt, null, NoPengajuanRequest, "", SelectNotaris, RequestNo, caption, UserID, GroupName);
                        }
                        resultdt4inv = Finance.DTREKAP;
                    }
                    else
                    {
                        resultdt4inv = Finance.DTREKAP;
                    }

                    if (resultdt4inv.Rows.Count <= 0)
                    {
                        validtxt = "Tidak ada data tagihan yang akan dibuat invoice";
                    }
                    else
                    {
                        if (isdelete == true) //jika ada perubaha reload kembali
                        {
                            resultdt = Finance.DTREKAP.DefaultView.ToTable(false, new string[] { "No Aplikasi", "NoInv", "No Perjanjian", "NoSertifikat" }); ;
                            resultdt.Columns[0].ColumnName = "Key1";
                            resultdt.Columns[1].ColumnName = "Key2";
                            resultdt.Columns[2].ColumnName = "Key3";
                            resultdt.Columns[3].ColumnName = "Key4";
                            Finance.DTREKAP = await Financeddl.dbupdatepaymentINVGEN(JenisTransaksi.Replace("createinvoice", ""), resultdt, null, "reload", "", SelectNotaris, RequestNo, caption, UserID, GroupName);
                            resultdt4inv = Finance.DTREKAP;
                        }
                        else
                        {
                            resultdt = Finance.DTREKAP.DefaultView.ToTable(false, new string[] { "No Aplikasi", "NoInv", "No Perjanjian", "NoSertifikat" }); ;
                            resultdt.Columns[0].ColumnName = "Key1";
                            resultdt.Columns[1].ColumnName = "Key2";
                            resultdt.Columns[2].ColumnName = "Key3";
                            resultdt.Columns[3].ColumnName = "Key4";
                        }

                        NoPengajuanRequest = resultdt.Rows[0][1].ToString();
                    }
                }
                else
                {
                    if (resultdt.Columns.Count <= 0)
                    {
                        resultdt.Columns.Add("Key1");
                        resultdt.Columns.Add("Key2");
                        resultdt.Columns.Add("Key3");
                        resultdt.Columns.Add("Key4");
                    }
                    //Finance.DTREKAP = resultdt;
                }

                if (JenisTransaksi == "" || JenisTransaksi == "N/A")
                {
                    validtxt = "Silahkan Pilih Jenis Tagihan";
                    resulted = "99";

                    if (Finance.DTREKAP == null)
                    {
                        Finance.DTREKAP = resultdt;
                    }
                    else
                    {
                        resultdt = Finance.DTREKAP;
                    }
                }

                string jenisfile = "";
                string viewpathed = "";
                byte[] datarekp = null;
                string infotext = "Rekap Data Tagihan " + (NoPengajuanRequest != "" ? " No. " + NoPengajuanRequest : "");
                namatemplte = JenisTransaksi.Replace("createinvoice", "").Replace("/", "").ToUpper();

                if (validtxt == "")
                {
                    //if (gontok == "createinvoice")
                    //{
                    //    resultdt4inv.Columns.Remove("NoInv");
                    //    resultdt4inv.Columns.Remove("NoRekening");
                    //    resultdt4inv.Columns.Remove("PemilikRekening");
                    //    resultdt4inv.Columns.Remove("NamaBank");
                    //    datarekp = DttpPdf.exportpdf(resultdt4inv, "Lampiran Invoice Pengecekan Sertifikat");
                    //}

                    resultdt = await Financeddl.dbupdatepaymentINVGEN(JenisTransaksi, resultdt, datarekp, NoPengajuanRequest, "", SelectNotaris, RequestNo, caption, UserID, GroupName);
                    if (resultdt.Rows.Count > 0)
                    {
                        result = 1;
                        resulted = "1";
                        if (gontok == "createinvoice")
                        {
                            infotext = NoPengajuanRequest == "" ? "Pembuatan Invoice No. " : " Data Tagihan No. ";
                            NoPengajuanRequest = resultdt.Rows[0][0].ToString();
                            infotext = infotext + NoPengajuanRequest;

                            resultdt = new DataTable();
                        }
                        else
                        {
                            Finance.DTREKAP = resultdt;
                        }

                        string sourceFile = Server.MapPath(Request.ApplicationPath) + "External\\TemplateINV\\" + namatemplte + "ADM.docx";
                        using (MemoryStream pdfDocumentstream = new MemoryStream())
                        {
                            jenisfile = namatemplte.Replace("PENGCEKAN", "PENGECEKAN") + "_" + ((NoPengajuanRequest ?? "DRAFT") == "" ? "DRAFT" : NoPengajuanRequest) + "_" + DateTime.Now.ToString("yyyyMMdd");
                            if (gontok == "createinvoice")
                            {
                                Spire.Doc.Document doc = new Spire.Doc.Document();

                                doc.LoadFromFile(sourceFile);
                                string colvalue = "";
                                foreach (DataColumn col in resultdt4inv.Columns)
                                {
                                    string colname = col.ColumnName;
                                    if (colname.ToLower() == "noinv")
                                    {
                                        colvalue = resultdt4inv.Rows[0][colname].ToString() == "" ? NoPengajuanRequest : resultdt4inv.Rows[0][colname].ToString();
                                    }
                                    else
                                    {
                                        colvalue = resultdt4inv.Rows[0][colname].ToString();
                                    }
                                    doc.Replace("{" + colname + "}", colvalue, false, true);
                                }
                                doc.SaveToStream(pdfDocumentstream, Spire.Doc.FileFormat.PDF);
                            }

                            Spire.Pdf.PdfDocument docpdf = new Spire.Pdf.PdfDocument();

                            if (gontok == "createinvoice")
                            {
                                docpdf.LoadFromStream(pdfDocumentstream);
                            }

                            PdfSection sec = docpdf.Sections.Add();
                            sec.PageSettings.Width = PdfPageSize.A4.Width;
                            PdfPageBase page = sec.Pages.Add();

                            float y = 10;

                            PdfBrush brush1 = PdfBrushes.Black;
                            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 14f, FontStyle.Bold));
                            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
                            page.Canvas.DrawString("LAMPIRAN INVOICE " + namatemplte.Replace("PENGCEKAN", "PENGECEKAN") + " No." + ((NoPengajuanRequest ?? "DRAFT-01") == "" ? "DRAFT-01" : NoPengajuanRequest), font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);

                            y = y + font1.MeasureString("Country List", format1).Height;
                            y = y + 5;

                            DataTable resultdtpdf = Finance.DTREKAP.DefaultView.ToTable(false, new string[]
                            { "No", "No Perjanjian","Tgl Perjanjian", "Nama Debitur","NoSertifikat","NamaPPAT","InvoicePPAT","tglInvoicePPAT","Nilai HT","Pinjaman_Konsumen","No HT","Kode Akta" });

                            //string dataheader = "No;No Aplikasi;Nama Debitur;NoSertifikat;WilayahHak;Nilai HT";
                            string[] dataheader = { "No;No Perjanjian;Tgl Perjanjian;Nama Debitur;No Sertipikat;Nama PPAT;No Invoice;Tgl Invoice;Hak Tanggungan;Pinjaman Konsumen;No APHT;Kode Akta" };

                            string[] datadetail = resultdtpdf.AsEnumerable().Select(item =>
                                 string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}{11};",
                                 item["No"],
                                 item["No Perjanjian"],
                                 item["Tgl Perjanjian"],
                                 item["Nama Debitur"],
                                 item["NoSertifikat"],
                                 item["NamaPPAT"],
                                 item["InvoicePPAT"],
                                 DateTime.Parse(item["tglInvoicePPAT"].ToString()).ToString("dd/MM/yyy"),
                                 double.Parse(item["Nilai HT"].ToString()).ToString("N0"),
                                 double.Parse(item["Pinjaman_Konsumen"].ToString()).ToString("N0"),
                                 item["No HT"],
                                 item["Kode AKta"])
                             ).ToArray();

                            string[] data = dataheader.Union(datadetail).ToArray();

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

                            string[] listheader = dataheader[0].Split(';');
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
                                    table.Columns[i].Width = 10;
                                    table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                }
                                if (listheader[i] == "Tgl Perjanjian")
                                {
                                    table.Columns[i].Width = 8;
                                    table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                                }
                                if (listheader[i] == "Nama Debitur")
                                {
                                    table.Columns[i].Width = 12;
                                }
                                if (listheader[i] == "No Sertifikat")
                                {
                                    table.Columns[i].Width = 14;
                                }
                                if (listheader[i] == "Hak Tanggungan" || listheader[i] == "Pinjaman Konsumen")
                                {
                                    table.Columns[i].Width = 12;
                                    table.Columns[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                                }
                                if (listheader[i] == "No APHT")
                                {
                                    table.Columns[i].Width = 8;
                                }
                                if (listheader[i] == "Kode Akta")
                                {
                                    table.Columns[i].Width = 8;
                                }
                            }
                            table.Draw(page, new PointF(0, y));
                            if (gontok == "createinvoice")
                            {
                                docpdf.SaveToStream(pdfDocumentstream, FileFormat.PDF);
                                //footer//
                                Spire.Pdf.PdfDocument docfooter = new Spire.Pdf.PdfDocument();
                                docfooter.LoadFromStream(pdfDocumentstream);
                                SizeF pageSize = docfooter.Pages[0].Size;
                                float x = 90;
                                y = pageSize.Height - 72;
                                for (int i = 0; i < docfooter.Pages.Count; i++)
                                {
                                    //draw line at bottom
                                    PdfPen pen = new PdfPen(PdfBrushes.Gray, 0.5f);
                                    docfooter.Pages[i].Canvas.DrawLine(pen, x, y, pageSize.Width - x, y);

                                    //draw text at bottom
                                    PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("Arial", 8f));
                                    PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
                                    String footerText = "Powered by PT. Sedayu Dana Banda @ " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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

                                jenisfile = jenisfile + ".pdf";
                                docfooter.SaveToStream(pdfDocumentstream);
                            }
                            else
                            {
                                jenisfile = jenisfile + ".xlsx";
                                docpdf.SaveToStream(pdfDocumentstream, FileFormat.XLSX);
                            }

                            /*
                            //protect with digital signature
                            Spire.Pdf.PdfDocument docsing = new Spire.Pdf.PdfDocument();
                            docsing.LoadFromStream(pdfDocumentstream);
                            PdfCertificate cert = new PdfCertificate(@"d:\hafid.pfx", "SedayuDanaBanda");
                            var signature = new Spire.Pdf.Security.PdfSignature(docsing, docsing.Pages[0], cert, "Invoice");
                            signature.Bounds = new RectangleF(new PointF(280, 600), new SizeF(260, 90));
                            signature.DistinguishedName = "DN:";
                            signature.LocationInfoLabel = "Location:";
                            signature.LocationInfo = "London";
                            signature.ReasonLabel = "Reason: ";
                            signature.Reason = "Le document est certifie";
                            signature.DateLabel = "Date: ";
                            signature.Date = DateTime.Now;
                            signature.ContactInfoLabel = "Contact: ";
                            signature.ContactInfo = "123456789";
                            signature.Certificated = false;
                            signature.DocumentPermissions = PdfCertificationFlags.ForbidChanges;
                            */

                            datarekp = pdfDocumentstream.ToArray();
                            viewpathed = Convert.ToBase64String(datarekp, 0, datarekp.Length);
                        }
                    }
                    else
                    {
                        resultdt = Finance.DTREKAP;
                    }
                }

                Finance.DTREKAP = resultdt;
                string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1) ? String.Format(EnumMessage, infotext, " Diproses, Silahkan Cek Kembali") : EnumMessage;
                validtxt = (result == 1) ? "" : EnumMessage;

                if (validtxt == "")
                {
                    validtxt = EnumMessage;
                    resulted = result.ToString();
                }

                //if (result == 1)
                //{
                //back to set in filter//
                if (Finance.DTREKAP == null)
                {
                    modFilter.TotalRecord = 0;
                    resultdt = new DataTable();
                }
                else
                {
                    modFilter.TotalRecord = Finance.DTREKAP.Rows.Count;
                }
                modFilter.TotalPage = 1;
                modFilter.pagingsizeclient = 1500;
                modFilter.totalRecordclient = 1500;
                modFilter.totalPageclient = 1;
                modFilter.pagenumberclient = 1;

                Finance.DTOrdersFromDB = resultdt;
                Finance.DTDetailForGrid = resultdt;

                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Data";
                //}

                ViewBag.JenisTransaksi = JenisTransaksi;
                ViewBag.NoPengajuanRequest = NoPengajuanRequest;

                //keep session filterisasi before//
                modFilter.idcaption = HasKeyProtect.Encryption(caption);
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                //sendback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    bytetyipe = viewpathed,
                    typo = gontok,
                    invo = NoPengajuanRequest,
                    jnfle = jenisfile,
                    jntr = JenisTransaksiencp ?? "",
                    selnot = SelectNotarisecp ?? "",
                    noreq = RequestNo ?? "",
                    namatitle = namatemplte.ToLower() == "pengcekan" ? "PENGECEKAN SERTIPIKAT(No." + NoPengajuanRequest + ")" : "SKMHT/APHT(No." + NoPengajuanRequest + ")",
                    view = (resulted == "99") ? "" : CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadINV.cshtml", Finance),
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnListFilterBillingINV(cFilterContract model, string download)
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
        //        modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
        //        Finance = TempData["FinanceListTxt"] as vmFinance;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        // get value filter before//
        //        string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
        //        string SelectRequest = model.SelectRequest ?? "";
        //        string SelectRequestStatus = model.SelectRequestStatus ?? "0";
        //        string fromdate = modFilter.fromdate ?? "";

        //        //set default for paging//
        //        int PageNumber = 1;
        //        double TotalRecord = 0;
        //        double TotalPage = 0;
        //        double pagingsizeclient = 0;
        //        double pagenumberclient = 0;
        //        double totalRecordclient = 0;
        //        double totalPageclient = 0;
        //        bool isModeFilter = true;

        //        //set filter//
        //        modFilter.SelectRequest = SelectRequest;
        //        modFilter.SelectRequestStatus = SelectRequestStatus;
        //        modFilter.fromdate = fromdate;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        // cek validation for filterisasi //
        //        string validtxt = ""; // lgOrder.CheckFilterisasiData(modFilter, download);
        //        if (validtxt == "")
        //        {
        //            ////descript some value for db//
        //            SelectClient = HasKeyProtect.Decryption(SelectClient);
        //            caption = HasKeyProtect.Decryption(caption);

        //            List<String> recordPage = await Financeddl.dbGetPayListCountINV(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await Financeddl.dbGetPayListINV(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            Finance.DTOrdersFromDB = dtlist[0];
        //            Finance.DTDetailForGrid = dtlist[1];
        //            Finance.DetailFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["FinanceListFilterTxt"] = modFilter;
        //            TempData["FinanceListTxt"] = Finance;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadINV.cshtml", Finance),
        //                download = "",
        //                message = validtxt
        //            });

        //        }
        //        else
        //        {
        //            TempData["FinanceListFilterTxt"] = modFilter;
        //            TempData["FinanceListTxt"] = Finance;
        //            TempData["Common"] = Common;

        //            //sendback to client browser//
        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = "",
        //                download = "",
        //                message = validtxt
        //            });

        //        }
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
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnPaymentRegisINV(HttpPostedFileBase[] files, cFilterContract model)
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
        //        //// get from session //
        //        modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
        //        Finance = TempData["FinanceListTxt"] as vmFinance;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //////get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        ////get value from aply filter //
        //        string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
        //        string SelectRequest = modFilter.SelectRequest ?? "";
        //        string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
        //        string fromdate = modFilter.fromdate ?? "";

        //        //set default for paging //
        //        int PageNumber = 1;
        //        double TotalRecord = modFilter.TotalRecord;
        //        double TotalPage = 0;
        //        double pagingsizeclient = modFilter.pagingsizeclient;
        //        double pagenumberclient = modFilter.pagenumberclient;
        //        double totalRecordclient = 0;
        //        double totalPageclient = 0;
        //        bool isModeFilter = modFilter.isModeFilter;

        //        //set filter//
        //        modFilter.SelectClient = SelectClient;
        //        modFilter.SelectRequest = SelectRequest;
        //        modFilter.SelectRequestStatus = SelectRequestStatus;
        //        modFilter.fromdate = fromdate;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        byte[] filbyte = null;
        //        string resulted = "";
        //        string validtxt = "";
        //        DataTable dt = new DataTable();

        //        validtxt = "";
        //        if (files != null)
        //        {
        //            if (files.Count() != 2)
        //            {
        //                validtxt = "Hanya boleh 2 file yang diupload";
        //            }
        //            else
        //            {
        //                var lmpir = files.Where(x => x.ContentType.Contains("text/xml")).Count();
        //                var lmpir1 = files.Where(x => x.ContentType.Contains("application/pdf")).Count();

        //                if (lmpir == 0 || lmpir1 == 0)
        //                {
        //                    validtxt = "Upload 2 File , 1 File untuk lampiran berformat(.xml) dan 1 File untuk invoice berformat (.pdf)";
        //                }
        //            }
        //            if (validtxt == "")
        //            {
        //                foreach (var fl in files)
        //                {
        //                    string resulttxt = string.Empty;
        //                    using (BinaryReader b = new BinaryReader(fl.InputStream))
        //                    {
        //                        byte[] binData = b.ReadBytes(fl.ContentLength);
        //                        resulttxt = System.Text.Encoding.UTF8.GetString(binData);
        //                        filbyte = binData;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            validtxt = "Silahkan Tautakan File ";
        //        }

        //        if ((model.fromdate ?? "") == "")
        //        {
        //            validtxt = "Isikan Periode Invoice";
        //        }

        //        int result = 0;
        //        DataTable resultdt = new DataTable();
        //        if (validtxt == "")
        //        {
        //            foreach (var fl in files)
        //            {
        //                string contenttype = fl.ContentType;
        //                string filename = "";
        //                if (contenttype.Contains("text/xml"))
        //                {
        //                    filename = "Lampiran Invoice Periode_" + DateTime.Parse(model.fromdate).ToString("dd_MMM_yyyy");
        //                }
        //                if (contenttype.Contains("application/pdf"))
        //                {
        //                    filename = "Invoice Periode_" + DateTime.Parse(model.fromdate).ToString("dd_MMM_yyyy");
        //                }
        //                resultdt = await Financeddl.dbupdatepaymentINV(filename, (model.RequestNo ?? ""), model.SelectJenisKontrak, dt.Rows.Count, model.fromdate, "0", 0, filbyte, contenttype, caption, UserID, GroupName);
        //                result = int.Parse(resultdt.Rows[0][0].ToString());
        //            }
        //        }

        //        string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", "Diproses Silahkan Cek Kembali") : EnumMessage;
        //        validtxt = (result == 1) ? "" : EnumMessage;

        //        if (validtxt == "")
        //        {
        //            validtxt = EnumMessage;
        //            resulted = result.ToString();
        //        }

        //        if (result == 1)
        //        {
        //            //set paging in grid client//
        //            List<DataTable> dtlist = await Financeddl.dbGetPayListINV(null, "", fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            Finance.DTOrdersFromDB = dtlist[0];
        //            Finance.DTDetailForGrid = dtlist[1];

        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        //keep session filterisasi before//
        //        TempData["FinanceListTxt"] = Finance;
        //        TempData["FinanceListFilterTxt"] = modFilter;
        //        TempData["common"] = Common;

        //        //sendback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            msg = validtxt,
        //            result = resulted,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadINV.cshtml", Finance),
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
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnPaymentRegisINVGEN(cFilterContract model)
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
        //        //// get from session //
        //        modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
        //        Finance = TempData["FinanceListTxt"] as vmFinance;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //////get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        ////get value from aply filter //
        //        string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
        //        string SelectRequest = modFilter.SelectRequest ?? "";
        //        string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
        //        string fromdate = modFilter.fromdate ?? "";

        //        //set default for paging //
        //        int PageNumber = 1;
        //        double TotalRecord = modFilter.TotalRecord;
        //        double pagingsizeclient = modFilter.pagingsizeclient;
        //        double pagenumberclient = modFilter.pagenumberclient;
        //        double totalRecordclient = 0;
        //        bool isModeFilter = modFilter.isModeFilter;

        //        //set filter//
        //        modFilter.SelectClient = SelectClient;
        //        modFilter.SelectRequest = SelectRequest;
        //        modFilter.SelectRequestStatus = SelectRequestStatus;
        //        modFilter.fromdate = fromdate;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        string resulted = "";
        //        string validtxt = "";
        //        string Periodeiv = "";
        //        DataTable dt = new DataTable();

        //        byte[] resultbyte = new byte[0];
        //        int resultdud = 0;
        //        DataTable resultdt = new DataTable();

        //        if ((model.fromdate ?? "") == "")
        //        {
        //            validtxt = "Isikan Periode Invoice";
        //        }

        //        if ((model.fromdate ?? "") != "")
        //        {
        //            DateTime dt0 = DateTime.Parse(model.fromdate);
        //            DateTime dt1 = DateTime.Parse(DateTime.Now.ToString("ddMMMyyyy"));
        //            if (dt0 > dt1)
        //            {
        //                validtxt = "Periode Invoice tidak boleh melebihi tanggal hari ini";
        //            }
        //            Periodeiv = dt0.ToString("dd-MMM-yyyy");
        //        }

        //        if (validtxt == "")
        //        {
        //            resultdt = await Financeddl.dbupdatepaymentINVGEN(model.fromdate ?? "", caption, UserID, GroupName);

        //            vmReportData rpt = new vmReportData();
        //            resultbyte = await rpt.dbDownloadLampiranINV(resultdt, Server.MapPath(Request.ApplicationPath));
        //            if (resultdt.Rows.Count > 0)
        //            {
        //                validtxt = "Generate Invoice Berhasil";
        //                resultdud = 22;
        //            }
        //            else
        //            {
        //                validtxt = "Tidak ada data untuk periode yang anda pilih";
        //                resultdud = 23;
        //            }
        //        }

        //        //keep session filterisasi before//
        //        TempData["FinanceListTxt"] = Finance;
        //        TempData["FinanceListFilterTxt"] = modFilter;
        //        TempData["common"] = Common;

        //        var contenttypeed = "application/xml";
        //        //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
        //        var viewpathed = "";
        //        string filenamevar = "Lampiran_Inovice_" + Periodeiv;
        //        var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = resultbyte, result = resultdud, msg = validtxt, contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
        //        jsonresult.MaxJsonLength = int.MaxValue;
        //        return jsonresult;

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
        //public async Task<ActionResult> clnPaymentRegisrejtINV(string kelookup, string ogenta)
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
        //        //// get from session //
        //        modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
        //        Finance = TempData["FinanceListTxt"] as vmFinance;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //////get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        ////get value from aply filter //
        //        string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
        //        string SelectRequest = modFilter.SelectRequest ?? "";
        //        string SelectRequestStatus = modFilter.SelectRequestStatus ?? "";
        //        string fromdate = modFilter.fromdate ?? "";

        //        //set default for paging //
        //        int PageNumber = 1;
        //        double TotalRecord = modFilter.TotalRecord;
        //        double TotalPage = 0;
        //        double pagingsizeclient = modFilter.pagingsizeclient;
        //        double pagenumberclient = modFilter.pagenumberclient;
        //        double totalRecordclient = 0;
        //        double totalPageclient = 0;
        //        bool isModeFilter = modFilter.isModeFilter;

        //        //set filter//
        //        modFilter.SelectClient = SelectClient;
        //        modFilter.SelectRequest = SelectRequest;
        //        modFilter.SelectRequestStatus = SelectRequestStatus;
        //        modFilter.fromdate = fromdate;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        string resulted = "";
        //        string validtxt = "";
        //        DataTable dt = new DataTable();

        //        validtxt = "";

        //        int result = 0;
        //        DataTable resultdt = new DataTable();

        //        //string reqid = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).Select(x => x.Field<int>("ID")).SingleOrDefault().ToString();

        //        DataTable dtx = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).CopyToDataTable();
        //        string reqid = dtx.Rows[0]["ID"].ToString();

        //        ogenta = ogenta ?? "";
        //        string msg = "";
        //        ogenta = ogenta != "" ? HasKeyProtect.Decryption(ogenta) : ogenta;
        //        int status = 0;

        //        if (ogenta == "approved")
        //        {
        //            msg = "Diapproved";
        //            status = 1;
        //        }
        //        if (ogenta == "paid")
        //        {
        //            status = 2;
        //        }
        //        if (ogenta == "")
        //        {
        //            status = 22;
        //            msg = "Dibatalkan";
        //        }

        //        resultdt = await Financeddl.dbupdatepaymentINV("", "", "", dt.Rows.Count, null, reqid, status, null, "", caption, UserID, GroupName);
        //        result = int.Parse(resultdt.Rows[0][0].ToString());

        //        string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", msg + ", Silahkan Cek Kembali") : EnumMessage;
        //        validtxt = (result == 1) ? "" : EnumMessage;

        //        if (validtxt == "")
        //        {
        //            validtxt = EnumMessage;
        //            resulted = result.ToString();
        //        }

        //        if (result == 1)
        //        {
        //            //set paging in grid client//
        //            List<DataTable> dtlist = await Financeddl.dbGetPayListINV(null, "", fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            Finance.DTOrdersFromDB = dtlist[0];
        //            Finance.DTDetailForGrid = dtlist[1];

        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        //keep session filterisasi before//
        //        TempData["FinanceListTxt"] = Finance;
        //        TempData["FinanceListFilterTxt"] = modFilter;
        //        TempData["common"] = Common;

        //        //sendback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            msg = validtxt,
        //            result = resulted,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadINV.cshtml", Finance),
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

        #endregion billing invoice

        /*

        #region billing BNI

        public async Task<ActionResult> clnBillPaymentRegisBNI(string menu, string caption)
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

                List<String> recordPage = await Financeddl.dbGetPayListCountBNI(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Financeddl.dbGetPayListBNI(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                Finance.DTOrdersFromDB = dtlist[0];
                Finance.DTDetailForGrid = dtlist[1];
                //VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
                Finance.DetailFilter = modFilter;
                Finance.Permission = PermisionModule;

                //set session filterisasi //
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;

                // set caption menut text //
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Finance";
                ViewBag.action = "clnBillPaymentRegisBNI";

                string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                // send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/uiBillingPaymentBNI.cshtml", Finance),
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

        public async Task<ActionResult> clnOpenFilterpopBNI()
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectRequest = modFilter.SelectRequest;
                string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
                string fromdate = modFilter.fromdate ?? "";

                // try make filter initial & set secure module name //
                if (Common.ddlJenisRequest == null)
                {
                    Common.ddlJenisRequest = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                }

                //if (Common.ddlJenisRequestStatus == null)
                {
                    Common.ddlJenisRequestStatus = await Commonddl.dbddlgetparamenumsList("REQ_TYPE_B");
                }

                ViewData["SelectRequest"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequest);
                ViewData["SelectRequestStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequestStatus);
                Common.ddlJenisRequestStatus = null;

                //set session filterisasi //
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectRequest,
                    opsi2 = SelectRequestStatus,
                    opsi3 = "",
                    opsi5 = "",
                    opsi6 = fromdate,
                    opsi7 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiFilterDataBNI.cshtml", Finance.DetailFilter),
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
        public async Task<ActionResult> clnListFilterBillingBNI(cFilterContract model, string download)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter before//
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string SelectRequest = model.SelectRequest ?? "";
                string SelectRequestStatus = model.SelectRequestStatus ?? "0";
                string fromdate = model.fromdate ?? "";

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
                modFilter.SelectRequest = SelectRequest;
                modFilter.SelectRequestStatus = SelectRequestStatus;
                modFilter.fromdate = fromdate;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = ""; // lgOrder.CheckFilterisasiData(modFilter, download);
                if (validtxt == "")
                {
                    ////descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    caption = HasKeyProtect.Decryption(caption);

                    List<String> recordPage = await Financeddl.dbGetPayListCountBNI(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Financeddl.dbGetPayListBNI(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    Finance.DTOrdersFromDB = dtlist[0];
                    Finance.DTDetailForGrid = dtlist[1];
                    Finance.DetailFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["FinanceListFilterTxt"] = modFilter;
                    TempData["FinanceListTxt"] = Finance;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadBNI.cshtml", Finance),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {
                    TempData["FinanceListFilterTxt"] = modFilter;
                    TempData["FinanceListTxt"] = Finance;
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

        public async Task<ActionResult> clnBillPaymentRgridINVBNI(int paged)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                // get value filter before//

                string SelectClient = "";
                string SelectRequest = modFilter.SelectRequest ?? "";
                string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
                string fromdate = modFilter.fromdate ?? "";

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                modFilter.SelectClient = SelectClient;
                modFilter.SelectRequest = SelectRequest;
                modFilter.SelectRequestStatus = SelectRequestStatus;
                modFilter.fromdate = fromdate;

                caption = HasKeyProtect.Decryption(caption);
                List<DataTable> dtlist = await Financeddl.dbGetPayListBNI(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                Finance.DTDetailForGrid = dtlist[1];

                //string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                //set session filterisasi //
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["FinanceListTxt"] = Finance;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadBNI.cshtml", Finance),
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
        public async Task<ActionResult> clnPaymentProsesBNI(string kelookup, string kodok)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                ////get value from aply filter //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectRequest = modFilter.SelectRequest ?? "";
                string SelectRequestStatus = modFilter.SelectRequestStatus ?? "";
                string fromdate = modFilter.fromdate ?? "";

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
                modFilter.SelectRequest = SelectRequest;
                modFilter.SelectRequestStatus = SelectRequestStatus;
                modFilter.fromdate = fromdate;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                string resulted = "";
                string validtxt = "";
                DataTable dt = new DataTable();

                validtxt = "";

                int result = 0;
                DataTable resultdt = new DataTable();

                //string reqid = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).Select(x => x.Field<int>("ID")).SingleOrDefault().ToString();
                DataTable dtx = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).CopyToDataTable();

                string reqid = dtx.Rows[0]["ID"].ToString();

                int status = 0;
                string msgtext = "";
                if (kodok == "apr")
                {
                    status = 1;
                    msgtext = "DiApproved";
                }
                if (kodok == "cnl")
                {
                    status = 3;
                    msgtext = "Dibatalkan";
                }
                if (kodok == "cfm")
                {
                    status = 4;
                    msgtext = "Dibayarkan Ke BNI";
                }

                result = await Financeddl.dbupdatepaymentBNI("", "", dt.Rows.Count, null, reqid, status, null, "", caption, UserID, GroupName);

                string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", msgtext + ", Silahkan Cek Kembali") : EnumMessage;
                validtxt = (result == 1) ? "" : EnumMessage;

                if (validtxt == "")
                {
                    validtxt = EnumMessage;
                    resulted = result.ToString();
                }

                if (result == 1)
                {
                    //set paging in grid client//
                    List<DataTable> dtlist = await Financeddl.dbGetPayListBNI(null, "", fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    Finance.DTOrdersFromDB = dtlist[0];
                    Finance.DTDetailForGrid = dtlist[1];
                }

                string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                //keep session filterisasi before//
                TempData["FinanceListTxt"] = Finance;
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["common"] = Common;

                //sendback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUploadBNI.cshtml", Finance),
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
        public async Task<ActionResult> clnReCombine(string[] AktaSelectdwn, string prevedid, string namaidpool, string reooo)
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
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //set back filter data from session before//
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["FinanceListTxt"] = Finance;
                TempData["common"] = Common;

                // get user group & akses //
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //extend//
                cAccountMetrik Metrik = Finance.Permission;
                bool AllowPrint = Metrik.AllowPrint;
                bool AllowDownload = Metrik.AllowDownload;

                ////deript for db//
                //string ClientID = HasKeyProtect.Decryption(modFilter.ClientLogin);
                //string IDNotaris = HasKeyProtect.Decryption(modFilter.NotarisLogin);
                //string IDCabang = HasKeyProtect.Decryption(modFilter.BranchLogin);
                string SecureModuleId = HasKeyProtect.Decryption(modFilter.idcaption);

                //DataTable dataupload = new DataTable();
                //dataupload.Columns.Add("CONT_TYPE", Type.GetType("System.Int32"));
                //dataupload.Columns.Add("CLIENT_FDC_ID", Type.GetType("System.Int64"));
                //dataupload.Columns.Add("CONT_NO", Type.GetType("System.String"));
                //dataupload.Columns.Add("CLNT_ID", Type.GetType("System.String"));
                //dataupload.Columns.Add("NTRY_ID", Type.GetType("System.String"));
                //dataupload.Columns.Add("NO_DOCUMENT", Type.GetType("System.String"));

                List<string> ListIDgrd = new List<string>();
                var ij = 0;
                string keylookup = "";

                //looping unutk  nokontrak yang dipilih , dininputkan ke table temp//
                string combinename = "";
                string combinenameid = "";

                foreach (var aktasel in AktaSelectdwn)
                {
                    string[] valued = aktasel.Split('|');

                    keylookup = valued[0].ToString();
                    ListIDgrd.Add(keylookup);

                    ij = ij + 1;

                    DataRow resultquery = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).SingleOrDefault();
                    if (resultquery != null)
                    {
                        if (!combinename.Contains(resultquery["REQUEST_TYPE"].ToString() + resultquery["CREATED_BY"].ToString()))
                        {
                            combinename = combinename + resultquery["REQUEST_TYPE"].ToString() + resultquery["CREATED_BY"].ToString() + "|";
                        }
                        combinenameid = combinenameid + resultquery["ID"].ToString() + "|";
                    }
                }

                int result = -1;
                string EnumMessage = "";

                bool cunki = false;
                if (reooo == "rder")
                {
                    cunki = true;
                }

                string[] councombine = combinename.Split('|').Where(x => x != "").ToArray();
                if (councombine.Length > 1)
                {
                    EnumMessage = "Hanya tipe pengajuan dan user pendaftaran yang sama yang dapat digabungkan";
                }
                else
                {
                    result = await Financeddl.dbgetcombinetxtbni(combinenameid, cunki, GroupName, SecureModuleId, UserID);
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                }

                EnumMessage = result == 1 ? " File txt berhasil digabungkan" : EnumMessage;

                return Json(new
                {
                    resulted = result.ToString(),
                    moderror = IsErrorTimeout,
                    msg = EnumMessage,
                    dolpet = ListIDgrd
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

        #endregion billing BNI

        */

        /*

    #region billing payment

    public async Task<ActionResult> clnBillPaymentRegis(string menu, string caption)
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

            List<String> recordPage = await Financeddl.dbGetPayListCount(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
            TotalRecord = Convert.ToDouble(recordPage[0]);
            TotalPage = Convert.ToDouble(recordPage[1]);
            pagingsizeclient = Convert.ToDouble(recordPage[2]);
            pagenumberclient = PageNumber;
            List<DataTable> dtlist = await Financeddl.dbGetPayList(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
            Finance.DTOrdersFromDB = dtlist[0];
            Finance.DTDetailForGrid = dtlist[1];
            //VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
            Finance.DetailFilter = modFilter;

            //set session filterisasi //
            TempData["FinanceListTxt"] = Finance;
            TempData["FinanceListFilterTxt"] = modFilter;

            // set caption menut text //
            ViewBag.menu = menu;
            ViewBag.caption = caption;
            ViewBag.captiondesc = menuitemdescription;
            ViewBag.rute = "Finance";
            ViewBag.action = "clnBillPaymentRegis";

            string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

            // send back to client browser//
            return Json(new
            {
                moderror = IsErrorTimeout,
                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/uiBillingPayment.cshtml", Finance),
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

    public async Task<ActionResult> clnOpenAddUplod()
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
            modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
            Finance = TempData["FinanceListTxt"] as vmFinance;
            Common = (TempData["common"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            // try make filter initial & set secure module name //
            if (Common.ddlJenisKontrak == null)
            {
                Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
            }
            ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

            TempData["FinanceListTxt"] = Finance;
            TempData["FinanceListFilterTxt"] = modFilter;
            TempData["common"] = Common;
            // senback to client browser//
            return Json(new
            {
                moderror = IsErrorTimeout,
                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiBillingPaymentUpload.cshtml", Finance.DetailFilter),
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
            modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
            Finance = TempData["FinanceListTxt"] as vmFinance;
            Common = (TempData["common"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            string UserID = modFilter.UserID;

            // get value filter before//
            string SelectRequest = modFilter.SelectRequest;
            string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
            string fromdate = modFilter.fromdate ?? "";

            // try make filter initial & set secure module name //
            if (Common.ddlJenisRequest == null)
            {
                Common.ddlJenisRequest = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
            }

            if (Common.ddlJenisRequestStatus == null)
            {
                Common.ddlJenisRequestStatus = await Commonddl.dbddlgetparamenumsList("REQ_TYPE_S");
            }

            ViewData["SelectRequest"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequest);
            ViewData["SelectRequestStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequestStatus);

            //set session filterisasi //
            TempData["FinanceListTxt"] = Finance;
            TempData["FinanceListFilterTxt"] = modFilter;
            TempData["common"] = Common;

            // senback to client browser//
            return Json(new
            {
                moderror = IsErrorTimeout,
                opsi1 = SelectRequest,
                opsi2 = SelectRequestStatus,
                opsi3 = "",
                opsi5 = "",
                opsi6 = fromdate,
                opsi7 = "",
                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiFilterData.cshtml", Finance.DetailFilter),
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
    public async Task<ActionResult> clnListFilterBilling(cFilterContract model, string download)
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
            modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
            Finance = TempData["FinanceListTxt"] as vmFinance;
            Common = (TempData["common"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            //get value from old define//
            string UserID = modFilter.UserID;
            string GroupName = modFilter.GroupName;
            string caption = modFilter.idcaption;

            // get value filter before//
            string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
            string SelectRequest = model.SelectRequest ?? "";
            string SelectRequestStatus = model.SelectRequestStatus ?? "0";
            string fromdate = modFilter.fromdate ?? "";

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
            modFilter.SelectRequest = SelectRequest;
            modFilter.SelectRequestStatus = SelectRequestStatus;
            modFilter.fromdate = fromdate;

            modFilter.PageNumber = PageNumber;
            modFilter.isModeFilter = isModeFilter;
            //set filter//

            // cek validation for filterisasi //
            string validtxt = ""; // lgOrder.CheckFilterisasiData(modFilter, download);
            if (validtxt == "")
            {
                ////descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                caption = HasKeyProtect.Decryption(caption);

                List<String> recordPage = await Financeddl.dbGetPayListCount(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Financeddl.dbGetPayList(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                Finance.DTOrdersFromDB = dtlist[0];
                Finance.DTDetailForGrid = dtlist[1];
                Finance.DetailFilter = modFilter;

                //keep session filterisasi before//
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["FinanceListTxt"] = Finance;
                TempData["common"] = Common;

                string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUpload.cshtml", Finance),
                    download = "",
                    message = validtxt
                });
            }
            else
            {
                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["FinanceListTxt"] = Finance;
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
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> clnPaymentRegis(HttpPostedFileBase files, cFilterContract model)
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
            modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
            Finance = TempData["FinanceListTxt"] as vmFinance;
            Common = (TempData["common"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            //////get value from old define//
            string UserID = modFilter.UserID;
            string GroupName = modFilter.GroupName;
            string caption = modFilter.idcaption;

            ////get value from aply filter //
            string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
            string SelectRequest = modFilter.SelectRequest ?? "";
            string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
            string fromdate = modFilter.fromdate ?? "";

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
            modFilter.SelectRequest = SelectRequest;
            modFilter.SelectRequestStatus = SelectRequestStatus;
            modFilter.fromdate = fromdate;

            modFilter.PageNumber = PageNumber;
            modFilter.isModeFilter = isModeFilter;
            //set filter//

            //decript some model apply for DB//
            caption = HasKeyProtect.Decryption(caption);

            byte[] filbyte = null;
            string resulted = "";
            string validtxt = "";
            DataTable dt = new DataTable();

            validtxt = "";
            if (files != null)
            {
                if (!files.ContentType.Contains("text/plain"))
                {
                    validtxt = "Extention File harus .txt";
                }
                else if ((files.FileName != "Paid.txt") && (files.FileName != "ClaimBase.txt") && (files.FileName != "ClaimBaseSend.txt"))
                {
                    validtxt = "Nama File harus Paid.txt atau ClaimBase.txt atau ClaimBaseSend.txt";
                }
                else if ((model.fromdate ?? "") == "" && (files.FileName != "ClaimBaseSend.txt"))
                {
                    validtxt = "Isikan Tgl Pembayaran";
                }
                else if ((model.fromdate ?? "") != "" && (files.FileName == "ClaimBaseSend.txt"))
                {
                    validtxt = "Kosongkan Tgl Pembayaran";
                }
                else
                {
                    string resulttxt = string.Empty;
                    using (BinaryReader b = new BinaryReader(files.InputStream))
                    {
                        byte[] binData = b.ReadBytes(files.ContentLength);
                        resulttxt = System.Text.Encoding.UTF8.GetString(binData);
                        filbyte = binData;
                    }

                    dt = OwinLibrary.ConvertByteToDT(resulttxt);
                    string[] cont_type = dt.AsEnumerable().Select(x => x.Field<string>("CONT_TYPE")).Distinct().ToArray();
                    if (cont_type.Length > 1)
                    {
                        validtxt = "Terdapat Tipe Dokumen lebih dari satu pada kolom CONT_TYPE, silahkan cek kembali";
                    }
                    else
                    {
                        if (model.SelectJenisKontrak != cont_type[0])
                        {
                            validtxt = "Jenis Pembayaran tidak sesuai dengan data pada template pada kolom CONT_TYPE, silahkan cek kembali";
                        }
                    }
                }
            }
            else
            {
                validtxt = "Silahkan Tautakan File ";
            }

            int result = 0;
            DataTable resultdt = new DataTable();
            if ((dt.Rows.Count > 0) && validtxt == "")
            {
                string contenttype = files.ContentType;
                resultdt = await Financeddl.dbupdatepayment(files.FileName, model.SelectJenisKontrak, dt.Rows.Count, model.fromdate, "0", 0, filbyte, contenttype, caption, UserID, GroupName);
                result = int.Parse(resultdt.Rows[0][0].ToString());
            }

            string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
            EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", "Diproses Silahkan Cek Kembali") : EnumMessage;
            validtxt = (result == 1) ? "" : EnumMessage;

            if (validtxt == "")
            {
                validtxt = EnumMessage;
                resulted = result.ToString();
            }

            if (result == 1)
            {
                //set paging in grid client//
                List<DataTable> dtlist = await Financeddl.dbGetPayList(null, "", fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                //back to set in filter//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                Finance.DTOrdersFromDB = dtlist[0];
                Finance.DTDetailForGrid = dtlist[1];
            }

            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

            //keep session filterisasi before//
            TempData["FinanceListTxt"] = Finance;
            TempData["FinanceListFilterTxt"] = modFilter;
            TempData["common"] = Common;

            //sendback to client browser//

            return Json(new
            {
                moderror = IsErrorTimeout,
                msg = validtxt,
                result = resulted,
                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUpload.cshtml", Finance),
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
    public async Task<ActionResult> clnPaymentRegisrejt(string kelookup)
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
            modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
            Finance = TempData["FinanceListTxt"] as vmFinance;
            Common = (TempData["common"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            //////get value from old define//
            string UserID = modFilter.UserID;
            string GroupName = modFilter.GroupName;
            string caption = modFilter.idcaption;

            ////get value from aply filter //
            string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
            string SelectRequest = modFilter.SelectRequest ?? "";
            string SelectRequestStatus = modFilter.SelectRequestStatus ?? "";
            string fromdate = modFilter.fromdate ?? "";

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
            modFilter.SelectRequest = SelectRequest;
            modFilter.SelectRequestStatus = SelectRequestStatus;
            modFilter.fromdate = fromdate;

            modFilter.PageNumber = PageNumber;
            modFilter.isModeFilter = isModeFilter;
            //set filter//

            //decript some model apply for DB//
            caption = HasKeyProtect.Decryption(caption);

            string resulted = "";
            string validtxt = "";
            DataTable dt = new DataTable();

            validtxt = "";

            int result = 0;
            DataTable resultdt = new DataTable();

            DataTable dtx = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).CopyToDataTable();
            string reqid = dtx.Rows[0]["ID"].ToString();

            resultdt = await Financeddl.dbupdatepayment("", "", dt.Rows.Count, null, reqid, 2, null, "", caption, UserID, GroupName);
            result = int.Parse(resultdt.Rows[0][0].ToString());

            string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
            EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", "Dibatalkan, Silahkan Cek Kembali") : EnumMessage;
            validtxt = (result == 1) ? "" : EnumMessage;

            if (validtxt == "")
            {
                validtxt = EnumMessage;
                resulted = result.ToString();
            }

            if (result == 1)
            {
                //set paging in grid client//
                List<DataTable> dtlist = await Financeddl.dbGetPayList(null, "", fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                //back to set in filter//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                Finance.DTOrdersFromDB = dtlist[0];
                Finance.DTDetailForGrid = dtlist[1];
            }

            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

            //keep session filterisasi before//
            TempData["FinanceListTxt"] = Finance;
            TempData["FinanceListFilterTxt"] = modFilter;
            TempData["common"] = Common;

            //sendback to client browser//

            return Json(new
            {
                moderror = IsErrorTimeout,
                msg = validtxt,
                result = resulted,
                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUpload.cshtml", Finance),
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

    #endregion billing payment

    */

        /*

      #region billing payment manual

      public async Task<ActionResult> clnBillPaymentRegisMNL(string menu, string caption)
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

              List<String> recordPage = await Financeddl.dbGetPayListCount(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
              TotalRecord = Convert.ToDouble(recordPage[0]);
              TotalPage = Convert.ToDouble(recordPage[1]);
              pagingsizeclient = Convert.ToDouble(recordPage[2]);
              pagenumberclient = PageNumber;
              List<DataTable> dtlist = await Financeddl.dbGetPayList(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
              Finance.DTOrdersFromDB = dtlist[0];
              Finance.DTDetailForGrid = dtlist[1];
              //VeryfiedOrderRegis.DTOrdersFromDBSMRY = dtlist[0];
              Finance.DetailFilter = modFilter;

              //set session filterisasi //
              TempData["FinanceMNLListTxt"] = Finance;
              TempData["FinanceMNLListFilterTxt"] = modFilter;

              // set caption menut text //
              ViewBag.menu = menu;
              ViewBag.caption = caption;
              ViewBag.captiondesc = menuitemdescription;
              ViewBag.rute = "Finance";
              ViewBag.action = "clnBillPaymentRegisMNL";

              string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
              ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

              List<HashNetFramework.cpendaftaranOder> order = new List<cpendaftaranOder>();
              // send back to client browser//
              return Json(new
              {
                  moderror = IsErrorTimeout,
                  view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/uiBillingPaymentMNL.cshtml", order),
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

      public async Task<ActionResult> clnOpenAddUplodMNL()
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
              modFilter = TempData["FinanceMNLListFilterTxt"] as cFilterContract;
              Finance = TempData["FinanceMNLListTxt"] as vmFinance;
              Common = (TempData["common"] as vmCommon);
              Common = Common == null ? new vmCommon() : Common;

              // try make filter initial & set secure module name //
              if (Common.ddlJenisKontrak == null)
              {
                  Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
              }
              ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

              TempData["FinanceMNLListTxt"] = Finance;
              TempData["FinanceMNLListFilterTxt"] = modFilter;
              TempData["common"] = Common;
              // senback to client browser//

              List<cpendaftaranOder> Oder = new List<cpendaftaranOder>();
              return Json(new
              {
                  moderror = IsErrorTimeout,
                  view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiBillingPaymentUploadMNL.cshtml", Oder),
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

      public async Task<ActionResult> clnOpenFilterpopMnl()
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
              modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
              Finance = TempData["FinanceListTxt"] as vmFinance;
              Common = (TempData["common"] as vmCommon);
              Common = Common == null ? new vmCommon() : Common;

              string UserID = modFilter.UserID;

              // get value filter before//
              string SelectRequest = modFilter.SelectRequest;
              string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
              string fromdate = modFilter.fromdate ?? "";

              // try make filter initial & set secure module name //
              if (Common.ddlJenisRequest == null)
              {
                  Common.ddlJenisRequest = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
              }

              if (Common.ddlJenisRequestStatus == null)
              {
                  Common.ddlJenisRequestStatus = await Commonddl.dbddlgetparamenumsList("REQ_TYPE_S");
              }

              ViewData["SelectRequest"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequest);
              ViewData["SelectRequestStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisRequestStatus);

              //set session filterisasi //
              TempData["FinanceListTxt"] = Finance;
              TempData["FinanceListFilterTxt"] = modFilter;
              TempData["common"] = Common;

              // senback to client browser//
              return Json(new
              {
                  moderror = IsErrorTimeout,
                  opsi1 = SelectRequest,
                  opsi2 = SelectRequestStatus,
                  opsi3 = "",
                  opsi5 = "",
                  opsi6 = fromdate,
                  opsi7 = "",
                  view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiFilterData.cshtml", Finance.DetailFilter),
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
      public async Task<ActionResult> clnListFilterBillingMnl(cFilterContract model, string download)
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
              modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
              Finance = TempData["FinanceListTxt"] as vmFinance;
              Common = (TempData["common"] as vmCommon);
              Common = Common == null ? new vmCommon() : Common;

              //get value from old define//
              string UserID = modFilter.UserID;
              string GroupName = modFilter.GroupName;
              string caption = modFilter.idcaption;

              // get value filter before//
              string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
              string SelectRequest = model.SelectRequest ?? "";
              string SelectRequestStatus = model.SelectRequestStatus ?? "0";
              string fromdate = modFilter.fromdate ?? "";

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
              modFilter.SelectRequest = SelectRequest;
              modFilter.SelectRequestStatus = SelectRequestStatus;
              modFilter.fromdate = fromdate;

              modFilter.PageNumber = PageNumber;
              modFilter.isModeFilter = isModeFilter;
              //set filter//

              // cek validation for filterisasi //
              string validtxt = ""; // lgOrder.CheckFilterisasiData(modFilter, download);
              if (validtxt == "")
              {
                  ////descript some value for db//
                  SelectClient = HasKeyProtect.Decryption(SelectClient);
                  caption = HasKeyProtect.Decryption(caption);

                  List<String> recordPage = await Financeddl.dbGetPayListCount(SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, caption, UserID, GroupName);
                  TotalRecord = Convert.ToDouble(recordPage[0]);
                  TotalPage = Convert.ToDouble(recordPage[1]);
                  pagingsizeclient = Convert.ToDouble(recordPage[2]);
                  pagenumberclient = PageNumber;
                  List<DataTable> dtlist = await Financeddl.dbGetPayList(null, SelectClient, fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                  Finance.DTOrdersFromDB = dtlist[0];
                  Finance.DTDetailForGrid = dtlist[1];
                  Finance.DetailFilter = modFilter;

                  //keep session filterisasi before//
                  TempData["FinanceListFilterTxt"] = modFilter;
                  TempData["FinanceListTxt"] = Finance;
                  TempData["common"] = Common;

                  string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                  ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                  return Json(new
                  {
                      moderror = IsErrorTimeout,
                      view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUpload.cshtml", Finance),
                      download = "",
                      message = validtxt
                  });
              }
              else
              {
                  TempData["FinanceListFilterTxt"] = modFilter;
                  TempData["FinanceListTxt"] = Finance;
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
      public async Task<ActionResult> clnPaymentRegisMNL(string model)
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
              //// get from session //
              modFilter = TempData["FinanceMNLListFilterTxt"] as cFilterContract;
              Finance = TempData["FinanceMNLListTxt"] as vmFinance;
              Common = (TempData["common"] as vmCommon);
              Common = Common == null ? new vmCommon() : Common;

              //////get value from old define//
              string UserID = modFilter.UserID;
              string GroupName = modFilter.GroupName;
              string caption = modFilter.idcaption;

              ////get value from aply filter //
              string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
              string SelectRequest = modFilter.SelectRequest ?? "";
              string SelectRequestStatus = modFilter.SelectRequestStatus ?? "0";
              string fromdate = modFilter.fromdate ?? "";

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
              modFilter.SelectRequest = SelectRequest;
              modFilter.SelectRequestStatus = SelectRequestStatus;
              modFilter.fromdate = fromdate;

              modFilter.PageNumber = PageNumber;
              modFilter.isModeFilter = isModeFilter;
              //set filter//

              //decript some model apply for DB//
              caption = HasKeyProtect.Decryption(caption);

              byte[] filbyte = null;
              string resulted = "";
              string validtxt = "";
              DataTable dt = new DataTable();

              validtxt = "";

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

                      var modeles = new Dictionary<string, string>
                          {
                             {"NoFkt", "2sssss"},
                             {"fromdate", "20210902"},
                             {"TableVariable", model},
                             {"SelectJenisKontrak","0" },
                             {"SelectClient","001" },
                             {"idcaption", caption},
                             {"UserID", UserID},
                             {"GroupName", GroupName},
                          };
                      var stringPayload = JsonConvert.SerializeObject(modeles);
                      var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                      string cmdtextapi = "api/FDCM/dbGetBillPaymentupdINVMNL";
                      var responsed = client.PostAsync(cmdtextapi, content).Result;
                      if (responsed.IsSuccessStatusCode)
                      {
                          resultJSON = responsed.Content.ReadAsStringAsync().Result;
                          dt = (DataTable)JsonConvert.DeserializeObject(resultJSON, (typeof(DataTable)));
                      }
                  }
              }

              //if (files != null)
              //{
              //if (!files.ContentType.Contains("text/plain"))
              //{
              //    validtxt = "Extention File harus .txt";
              //}
              //else if ((files.FileName != "Paid.txt") && (files.FileName != "ClaimBase.txt") && (files.FileName != "ClaimBaseSend.txt"))
              //{
              //    validtxt = "Nama File harus Paid.txt atau ClaimBase.txt atau ClaimBaseSend.txt";
              //}
              //else if ((model.fromdate ?? "") == "" && (files.FileName != "ClaimBaseSend.txt"))
              //{
              //    validtxt = "Isikan Tgl Pembayaran";
              //}
              //else if ((model.fromdate ?? "") != "" && (files.FileName == "ClaimBaseSend.txt"))
              //{
              //    validtxt = "Kosongkan Tgl Pembayaran";
              //}
              //else
              //{
              //    string resulttxt = string.Empty;
              //    using (BinaryReader b = new BinaryReader(files.InputStream))
              //    {
              //        byte[] binData = b.ReadBytes(files.ContentLength);
              //        resulttxt = System.Text.Encoding.UTF8.GetString(binData);
              //        filbyte = binData;
              //    }

              //    dt = OwinLibrary.ConvertByteToDT(resulttxt);
              //    string[] cont_type = dt.AsEnumerable().Select(x => x.Field<string>("CONT_TYPE")).Distinct().ToArray();
              //    if (cont_type.Length > 1)
              //    {
              //        validtxt = "Terdapat Tipe Dokumen lebih dari satu pada kolom CONT_TYPE, silahkan cek kembali";
              //    }
              //    else
              //    {
              //        if (model.SelectJenisKontrak != cont_type[0])
              //        {
              //            validtxt = "Jenis Pembayaran tidak sesuai dengan data pada template pada kolom CONT_TYPE, silahkan cek kembali";
              //        }
              //    }
              //}
              //}
              //else
              //{
              //    validtxt = "Silahkan Tautakan File ";
              //}

              //int result = 0;
              //DataTable resultdt = new DataTable();
              //if ((dt.Rows.Count > 0) && validtxt == "")
              //{
              //    string contenttype = files.ContentType;
              //    resultdt = await Financeddl.dbupdatepayment(files.FileName, model.SelectJenisKontrak, dt.Rows.Count, model.fromdate, "0", 0, filbyte, contenttype, caption, UserID, GroupName);
              //    result = int.Parse(resultdt.Rows[0][0].ToString());
              //}

              //string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
              //EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", "Diproses Silahkan Cek Kembali") : EnumMessage;
              //validtxt = (result == 1) ? "" : EnumMessage;

              //if (validtxt == "")
              //{
              //    validtxt = EnumMessage;
              //    resulted = result.ToString();
              //}

              //if (result == 1)
              //{
              //    //set paging in grid client//
              //    List<DataTable> dtlist = await Financeddl.dbGetPayList(null, "", fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
              //    totalRecordclient = dtlist[0].Rows.Count;
              //    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

              //    //back to set in filter//
              //    modFilter.TotalRecord = TotalRecord;
              //    modFilter.TotalPage = TotalPage;
              //    modFilter.pagingsizeclient = pagingsizeclient;
              //    modFilter.totalRecordclient = totalRecordclient;
              //    modFilter.totalPageclient = totalPageclient;
              //    modFilter.pagenumberclient = pagenumberclient;

              //    Finance.DTOrdersFromDB = dtlist[0];
              //    Finance.DTDetailForGrid = dtlist[1];

              //}

              //string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
              //ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

              //keep session filterisasi before//
              TempData["FinanceMNLListTxt"] = Finance;
              TempData["FinanceMNLListFilterTxt"] = modFilter;
              TempData["common"] = Common;

              //sendback to client browser//

              return Json(new
              {
                  moderror = IsErrorTimeout,
                  msg = validtxt,
                  result = resulted,
                  view = ""//CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUpload.cshtml", Finance),
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
      public async Task<ActionResult> clnPaymentRegisrejtmnl(string kelookup)
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
              //// get from session //
              modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
              Finance = TempData["FinanceListTxt"] as vmFinance;
              Common = (TempData["common"] as vmCommon);
              Common = Common == null ? new vmCommon() : Common;

              //////get value from old define//
              string UserID = modFilter.UserID;
              string GroupName = modFilter.GroupName;
              string caption = modFilter.idcaption;

              ////get value from aply filter //
              string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
              string SelectRequest = modFilter.SelectRequest ?? "";
              string SelectRequestStatus = modFilter.SelectRequestStatus ?? "";
              string fromdate = modFilter.fromdate ?? "";

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
              modFilter.SelectRequest = SelectRequest;
              modFilter.SelectRequestStatus = SelectRequestStatus;
              modFilter.fromdate = fromdate;

              modFilter.PageNumber = PageNumber;
              modFilter.isModeFilter = isModeFilter;
              //set filter//

              //decript some model apply for DB//
              caption = HasKeyProtect.Decryption(caption);

              string resulted = "";
              string validtxt = "";
              DataTable dt = new DataTable();

              validtxt = "";

              int result = 0;
              DataTable resultdt = new DataTable();

              DataTable dtx = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kelookup).CopyToDataTable();
              string reqid = dtx.Rows[0]["ID"].ToString();

              resultdt = await Financeddl.dbupdatepayment("", "", dt.Rows.Count, null, reqid, 2, null, "", caption, UserID, GroupName);
              result = int.Parse(resultdt.Rows[0][0].ToString());

              string EnumMessage = (validtxt ?? "") != "" ? validtxt : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
              EnumMessage = (result == 1) ? String.Format(EnumMessage, "Pengajuan", "Dibatalkan, Silahkan Cek Kembali") : EnumMessage;
              validtxt = (result == 1) ? "" : EnumMessage;

              if (validtxt == "")
              {
                  validtxt = EnumMessage;
                  resulted = result.ToString();
              }

              if (result == 1)
              {
                  //set paging in grid client//
                  List<DataTable> dtlist = await Financeddl.dbGetPayList(null, "", fromdate, SelectRequest, SelectRequestStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                  totalRecordclient = dtlist[0].Rows.Count;
                  totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                  //back to set in filter//
                  modFilter.TotalRecord = TotalRecord;
                  modFilter.TotalPage = TotalPage;
                  modFilter.pagingsizeclient = pagingsizeclient;
                  modFilter.totalRecordclient = totalRecordclient;
                  modFilter.totalPageclient = totalPageclient;
                  modFilter.pagenumberclient = pagenumberclient;

                  Finance.DTOrdersFromDB = dtlist[0];
                  Finance.DTDetailForGrid = dtlist[1];
              }

              string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
              ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

              //keep session filterisasi before//
              TempData["FinanceListTxt"] = Finance;
              TempData["FinanceListFilterTxt"] = modFilter;
              TempData["common"] = Common;

              //sendback to client browser//

              return Json(new
              {
                  moderror = IsErrorTimeout,
                  msg = validtxt,
                  result = resulted,
                  view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Finance/_uiGridBillingPaymentUpload.cshtml", Finance),
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

      #endregion billing payment manual

      */

        public async Task<ActionResult> DwnFiletxt(string lookup)
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
                return RedirectToRoute("DefaultExpired");
            }

            try
            {
                //// get from session //
                modFilter = TempData["FinanceListFilterTxt"] as cFilterContract;
                Finance = TempData["FinanceListTxt"] as vmFinance;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //////get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                ////get value from aply filter //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectRequest = modFilter.SelectRequest ?? "";
                string SelectRequestStatus = modFilter.SelectRequestStatus ?? "";
                string fromdate = modFilter.fromdate ?? "";

                TempData["FinanceListFilterTxt"] = modFilter;
                TempData["FinanceListTxt"] = Finance;
                TempData["common"] = Common;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                SelectClient = HasKeyProtect.Decryption(SelectClient);

                DataTable dtx = Finance.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == lookup).CopyToDataTable();
                string reqid = dtx.Rows[0]["ID"].ToString();

                cFinance dt = await Financeddl.dbGetPayListTxt(SelectClient, reqid, caption, UserID, GroupName);
                byte[] bytefile = dt.bytefile;
                string contenttype = dt.content_type;
                string filename = dt.filename;
                //Writing StringBuilder content to an excel file.
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                Response.Buffer = true;
                Response.ClearHeaders();
                Response.ContentType = contenttype;
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.OutputStream.Write(bytefile, 0, bytefile.Length);
                Response.Flush();
                Response.Close();
                if (filename.ToLower().Contains("paymentbni"))
                {
                    int result = await Financeddl.dbupdatepaymentBNI("", "", 0, null, reqid, 2, null, "", caption, UserID, GroupName);
                    if (result != 1)
                    {
                        var msg = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                    }
                }
                return File(bytefile, contenttype);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });

                if (IsErrorTimeout == false)
                {
                    return RedirectToRoute("ErroPage");
                }
                else
                {
                    return RedirectToRoute("DefaultExpired");
                }
            }
        }
    }
}