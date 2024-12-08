using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Ionic.Zip;
using System.IO;
using HashNetFramework;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace DusColl.Controllers
{
    public class PendaftaranController : Controller
    {
        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmPendaftaran Pendaftaran = new vmPendaftaran();
        vmPendaftaranddl Pendaftaranddl = new vmPendaftaranddl();
        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        blPendaftaran lgPendaftaran = new blPendaftaran();


        public async Task<ActionResult> clnGetprovinAHU(string kd, string kdv, string kode)
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
                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;

                IEnumerable<cListSelected> dlist;

                dlist = await Commonddl.dbGetprovinAHU(kd, kdv);

                Common.ddlProvin = Common.ddlProvin;
                Common.ddlProvin1 = Common.ddlProvin1;
                Common.ddlProvin2 = Common.ddlProvin2;

                if (kd == "kota")
                {
                    if (kode.Contains("GRTE"))
                    {
                        Common.ddlKota = dlist;
                    }
                    else if (kode.Contains("CUST"))
                    {
                        Common.ddlKota1 = dlist;
                    }
                    else if (kode.Contains("TGJW"))
                    {
                        Common.ddlKota2 = dlist;
                    }
                }

                if (kd == "kecamatan")
                {
                    if (kode.Contains("GRTE"))
                    {
                        Common.ddlKecamatan = dlist;
                    }
                    else if (kode.Contains("CUST"))
                    {
                        Common.ddlKecamatan1 = dlist;
                    }
                    else if (kode.Contains("TGJW"))
                    {
                        Common.ddlKecamatan2 = dlist;
                    }
                }
                if (kd == "kelurahan")
                {
                    if (kode.Contains("GRTE"))
                    {
                        Common.ddlKelurahan = dlist;
                    }
                    else if (kode.Contains("CUST"))
                    {
                        Common.ddlKelurahan1 = dlist;
                    }
                    else if (kode.Contains("TGJW"))
                    {
                        Common.ddlKelurahan2 = dlist;
                    }
                }

                TempData["PendaftaranList"] = Pendaftaran;
                TempData["PendaftaranListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    dataprovin = new JavaScriptSerializer().Serialize(dlist),
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
                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
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
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["PendaftaranListFilter"] = modFilter;
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
                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectBranch = modFilter.SelectBranch;
                string SelectClient = modFilter.SelectClient;
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
                string SelectContractStatus = modFilter.SelectContractStatus ?? "";
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";

                //decript for db//
                string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);

                // try make filter initial & set secure module name //
                if (Common.ddlClient == null)
                {
                    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(decSelectClient);
                }

                if (Common.ddlClient.Count() == 1 && Common.ddlBranch == null)
                {
                    SelectClient = Common.ddlClient.SingleOrDefault().Value;
                    Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, "", UserID);
                }

                if (Common.ddlContractStatus == null)
                {
                    Common.ddlContractStatus = await Commonddl.dbddlgetparamenumsList("ContStatus");
                }

                if (Common.ddlJenisPelanggan == null)
                {
                    Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
                }


                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectContractStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlContractStatus);

                TempData["PendaftaranList"] = Pendaftaran;
                TempData["PendaftaranListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectClient,
                    opsi2 = decSelectBranch,
                    opsi3 = "",
                    opsi4 = SelectContractStatus,
                    opsi5 = NoPerjanjian,
                    opsi6 = fromdate,
                    opsi7 = todate,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/_uiFilterData.cshtml", Pendaftaran.DetailFilter),
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

        public async Task<ActionResult> clnPendaftaran(string menu, string caption)
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
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                // some field must be overide first for default filter//
                string SelectClient = ClientID;
                string SelectBranch = IDCabang;
                string fromdate = "";
                string todate = "";
                string NoPerjanjian = "";
                string SelectContractStatus = "";

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

                modFilter.SelectClient = SelectClient;
                modFilter.SelectBranch = SelectBranch;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;
                modFilter.SelectContractStatus = SelectContractStatus;

                modFilter.PageNumber = PageNumber;


                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);

                //set paging in grid client//
                List<String> recordPage = await Pendaftaranddl.dbGetPendaftaranListCount(SelectClient, SelectBranch, fromdate, todate, SelectContractStatus, NoPerjanjian, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Pendaftaranddl.dbGetPendaftaranList(null, SelectClient, SelectBranch, fromdate, todate, SelectContractStatus, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                Pendaftaran.DTFromDB = dtlist[0];
                Pendaftaran.DTDetailForGrid = dtlist[1];
                Pendaftaran.DetailFilter = modFilter;
                Pendaftaran.Permission = PermisionModule;

                //set session filterisasi //
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["PendaftaranListFilter"] = modFilter;

                // set caption menut text //

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Pendaftaran";
                ViewBag.action = "clnPendaftaran";


                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/uiPendaftaran.cshtml", Pendaftaran),
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
        public async Task<ActionResult> clnListFilterPendaftaran(cFilterContract model, string download)
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

                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
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
                string SelectContractStatus = model.SelectContractStatus ?? "";
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
                modFilter.SelectContractStatus = SelectContractStatus;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                // cek validation for filterisasi //
                string validtxt = lgPendaftaran.CheckFilterisasiData(modFilter, download);
                if (validtxt == "")
                {


                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    caption = HasKeyProtect.Decryption(caption);

                    //set paging in grid client//
                    List<String> recordPage = await Pendaftaranddl.dbGetPendaftaranListCount(SelectClient, SelectBranch, fromdate, todate, SelectContractStatus, NoPerjanjian, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Pendaftaranddl.dbGetPendaftaranList(null, SelectClient, SelectBranch, fromdate, todate, SelectContractStatus, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    Pendaftaran.DTFromDB = dtlist[0];
                    Pendaftaran.DTDetailForGrid = dtlist[1];
                    Pendaftaran.DetailFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["PendaftaranListFilter"] = modFilter;
                    TempData["PendaftaranList"] = Pendaftaran;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/_uiGridPendaftaranList.cshtml", Pendaftaran),
                        download = "",
                        message = validtxt
                    });

                }
                else
                {

                    TempData["PendaftaranListFilter"] = modFilter;
                    TempData["PendaftaranList"] = Pendaftaran;
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

        public async Task<ActionResult> clnPendaftaranRgrid(int paged)
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
                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string NoPerjanjian = modFilter.NoPerjanjian;
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectContractStatus = modFilter.SelectContractStatus;
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
                caption = HasKeyProtect.Decryption(caption);

                //select page
                List<DataTable> dtlist = await Pendaftaranddl.dbGetPendaftaranList(Pendaftaran.DTFromDB, SelectClient, SelectBranch, fromdate, todate, SelectContractStatus, NoPerjanjian, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                // set pendataran //
                Pendaftaran.DTDetailForGrid = dtlist[1];

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                //set session filterisasi //
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["PendaftaranListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/_uiGridPendaftaranList.cshtml", Pendaftaran),
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

        public async Task<ActionResult> clnPendaftaranGet(string NoPerjanjian, string idfdc)
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

                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                idfdc = HasKeyProtect.Decryption(idfdc);

                DataTable selcdatatotable = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == NoPerjanjian).CopyToDataTable();
                string SelectClient = selcdatatotable.Rows[0]["CLNT_ID"].ToString();
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                NoPerjanjian = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString();

                List<DataTable> dtlist = await Pendaftaranddl.dbGetPendaftaranInfoRegList(null, SelectClient, NoPerjanjian, 1, 1, 1, caption, UserID, GroupName);
                Pendaftaran.DTDetailForGridRowSelected = dtlist[0];

                ViewBag.nokonview = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString() + "-" + selcdatatotable.Rows[0]["NamaNasabah"].ToString();

                TempData["PendaftaranListFilter"] = modFilter;
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/_uiInfoContract.cshtml", Pendaftaran),
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

        public async Task<ActionResult> clnPendaftaranGet4Temp(string KeyLook)
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

                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;


                DataTable selcdatatotable = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == KeyLook).CopyToDataTable();
                string SelectClient = selcdatatotable.Rows[0]["CLNT_ID"].ToString();
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string NoPerjanjian = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString();

                List<DataTable> dtlist = await Pendaftaranddl.dbGetPendaftaranInfoRegList(null, SelectClient, NoPerjanjian, 1, 1, 1, caption, UserID, GroupName);
                Pendaftaran.DTDetailForGridRowSelected = dtlist[0];

                ViewBag.nokonview = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString() + "-" + selcdatatotable.Rows[0]["NamaNasabah"].ToString();

                TempData["PendaftaranListFilter"] = modFilter;
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["common"] = Common;

                Pendaftaran.DetailFilter.secIDFDC = KeyLook;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/_uiPendaftaranManual4Temp.cshtml", Pendaftaran.DetailFilter),
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

        public async Task<ActionResult> clnPendaftaranGet4TempSve(cFilterContract model)
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

                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string KeyLook = model.secIDFDC;
                DataTable selcdatatotable = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == KeyLook).CopyToDataTable();
                string SelectClient = selcdatatotable.Rows[0]["CLNT_ID"].ToString();
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string NoPerjanjian = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString();
                string cont_type = selcdatatotable.Rows[0]["CONT_TYPE"].ToString();
                string client_fdc_id = selcdatatotable.Rows[0]["CLIENT_FDC_ID"].ToString();
                string billid = model.BILL_ID_AHU ?? "";
                string voucher = model.VOUCHER_AHU ?? "";
                string pnbp = model.AmtPNBP ?? "0";

                string EnumMessage = "Silahkan Isikan Data terlebih dahulu";
                int result = 0;
                if (billid != "" && voucher != "" && pnbp != "0")
                {
                    result = await Pendaftaranddl.dbSetToTemp4Manual(NoPerjanjian, cont_type, client_fdc_id, pnbp, voucher, billid, caption, GroupName, UserID);
                }

                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = result == 1 ? "Data berhasil didaftarkan manual " : EnumMessage;

                TempData["PendaftaranListFilter"] = modFilter;
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
                    resulted = result.ToString(),
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

        public async Task<ActionResult> clnPendaftaranGet4TempPaid(string KeyLook)
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

                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;


                DataTable selcdatatotable = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == KeyLook).CopyToDataTable();
                string SelectClient = selcdatatotable.Rows[0]["CLNT_ID"].ToString();
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string NoPerjanjian = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString();

                List<DataTable> dtlist = await Pendaftaranddl.dbGetPendaftaranInfoRegList(null, SelectClient, NoPerjanjian, 1, 1, 1, caption, UserID, GroupName);
                Pendaftaran.DTDetailForGridRowSelected = dtlist[0];

                ViewBag.nokonview = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString() + "-" + selcdatatotable.Rows[0]["NamaNasabah"].ToString();

                TempData["PendaftaranListFilter"] = modFilter;
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["common"] = Common;

                Pendaftaran.DetailFilter.secIDFDC = KeyLook;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/_uiPendaftaranBlockPaid4Temp.cshtml", Pendaftaran.DetailFilter),
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

        public async Task<ActionResult> clnPendaftaranGet4TempPaidSve(cFilterContract model)
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

                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string KeyLook = model.secIDFDC;
                DataTable selcdatatotable = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == KeyLook).CopyToDataTable();
                string SelectClient = selcdatatotable.Rows[0]["CLNT_ID"].ToString();
                string caption = HasKeyProtect.Decryption(modFilter.idcaption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string NoPerjanjian = selcdatatotable.Rows[0]["NO_PERJANJIAN"].ToString();
                string cont_type = selcdatatotable.Rows[0]["CONT_TYPE"].ToString();
                string client_fdc_id = selcdatatotable.Rows[0]["CLIENT_FDC_ID"].ToString();
                string billid = model.BILL_ID_AHU ?? "";
                string voucher = model.VOUCHER_AHU ?? "";
                string notes = model.SelectContractPaidStatusDesc ?? "";

                string EnumMessage = "Silahkan Isikan Data terlebih dahulu";
                int result = 0;
                if (billid != "" && voucher != "" && notes != "")
                {
                    result = await Pendaftaranddl.dbSetToTemp4Paid(NoPerjanjian, cont_type, client_fdc_id, notes, voucher, billid, caption, GroupName, UserID);
                }

                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = result == 1 ? "Data berhasil disimpan " : EnumMessage;

                TempData["PendaftaranListFilter"] = modFilter;
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
                    resulted = result.ToString(),
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
                modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
                Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //set back filter data from session before//
                TempData["PendaftaranListFilter"] = modFilter;
                TempData["PendaftaranList"] = Pendaftaran;
                TempData["common"] = Common;

                // get user group & akses //
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //extend//
                cAccountMetrik Metrik = Pendaftaran.Permission;
                bool AllowPrint = Metrik.AllowPrint;
                bool AllowDownload = Metrik.AllowDownload;

                //deript for db//
                string ClientID = HasKeyProtect.Decryption(modFilter.ClientLogin);
                string IDNotaris = HasKeyProtect.Decryption(modFilter.NotarisLogin);
                string IDCabang = HasKeyProtect.Decryption(modFilter.BranchLogin);
                string SecureModuleId = HasKeyProtect.Decryption(modFilter.idcaption);


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

                    DataRow resultquery = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).SingleOrDefault();
                    if (resultquery != null)
                    {
                        dataupload.Rows.Add(new object[] { resultquery["CONT_TYPE"], resultquery["CLIENT_FDC_ID"], resultquery["NO_PERJANJIAN"], resultquery["CLNT_ID"], "", resultquery["SERTIFIKAT_NO"] });
                    }

                }

                int result = -1;
                string EnumMessage = "";


                bool cunki = false;
                if (reooo == "rder")
                {
                    cunki = true;
                }


                result = await Pendaftaranddl.dbSettFlagToresenserty(dataupload, cunki, GroupName, SecureModuleId, UserID);
                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = result == 1 ? "Data berhasil diajukan untuk pengiriman ulang sertifikat " : EnumMessage;

                return Json(new
                {
                    resulted = result.ToString(),
                    moderror = IsErrorTimeout,
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AttactmentdownloadDirect(string[] AktaSelectdwn, string prevedid, string namaidpool, string reooo)
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


        //    try
        //    {

        //        //get filter data from session before//
        //        modFilter = TempData["PendaftaranListFilter"] as cFilterContract;
        //        Pendaftaran = TempData["PendaftaranList"] as vmPendaftaran;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //set back filter data from session before//
        //        TempData["PendaftaranListFilter"] = modFilter;
        //        TempData["PendaftaranList"] = Pendaftaran;
        //        TempData["common"] = Common;

        //        // get user group & akses //
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //extend//
        //        cAccountMetrik Metrik = Account.AccountMetrikList.Where(x => x.SecModuleID == caption).SingleOrDefault();
        //        bool AllowPrint = Metrik.AllowPrint;
        //        bool AllowDownload = Metrik.AllowDownload;

        //        //deript for db//
        //        string ClientID = HasKeyProtect.Decryption(modFilter.ClientLogin);
        //        string IDNotaris = HasKeyProtect.Decryption(modFilter.NotarisLogin);
        //        string IDCabang = HasKeyProtect.Decryption(modFilter.BranchLogin);
        //        string SecureModuleId = HasKeyProtect.Decryption(modFilter.idcaption);
        //        string Email = HasKeyProtect.Decryption(modFilter.MailerDaemoon);
        //        string UserGenCode = HasKeyProtect.Decryption(modFilter.GenDeamoon);



        //        ////set login key//
        //        string LoginAksesKey = UserID + Email + UserGenCode;

        //        DataTable dataupload = new DataTable();
        //        dataupload.Columns.Add("CONT_TYPE", Type.GetType("System.Int32"));
        //        dataupload.Columns.Add("CLIENT_FDC_ID", Type.GetType("System.Int64"));
        //        dataupload.Columns.Add("CONT_NO", Type.GetType("System.String"));
        //        dataupload.Columns.Add("CLNT_ID", Type.GetType("System.String"));
        //        dataupload.Columns.Add("NTRY_ID", Type.GetType("System.String"));
        //        dataupload.Columns.Add("NO_DOCUMENT", Type.GetType("System.String"));


        //        List<string> ListIDgrd = new List<string>();
        //        var ij = 0;
        //        string keylookup = "";

        //        //looping unutk  nokontrak yang dipilih , dininputkan ke table temp//
        //        foreach (var aktasel in AktaSelectdwn)
        //        {
        //            string[] valued = aktasel.Split('|');

        //            keylookup = valued[0].ToString();
        //            ListIDgrd.Add(keylookup);


        //            ij = ij + 1;

        //            DataRow resultquery = Pendaftaran.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookup).SingleOrDefault();
        //            if (resultquery != null)
        //            {
        //                dataupload.Rows.Add(new object[] { resultquery["CONT_TYPE"], resultquery["CLIENT_FDC_ID"], resultquery["NO_PERJANJIAN"], resultquery["CLNT_ID"], "", resultquery["SERTIFIKAT_NO"] });
        //            }

        //        }

        //        if (reooo != "")
        //        {
        //            string Jenisdoc = "SERTIFIKAT FIDUSIA";
        //            // try to get document attacment //
        //            DataTable DocumentByte = await Commonddl.dbGetDocumentByno(dataupload, Jenisdoc, ClientID, IDCabang, UserID, GroupName, SecureModuleId, LoginAksesKey, Email, UserGenCode);

        //            //document sertifikat ada //
        //            if ((DocumentByte.Rows.Count > 0))
        //            {

        //                //Harus ada gen code untuk passwordd file//
        //                if ((UserGenCode != ""))
        //                {

        //                    var powderdockp = AllowPrint == true ? "1" : "0";
        //                    var powderdockd = AllowDownload == true ? "1" : "0"; // untuk handele downloa pada pdfvier//

        //                    string filenamepar = "";
        //                    string EnumMessage = "";
        //                    string contenttyped = "";
        //                    byte[] bytesToDecrypt = null;
        //                    byte[] buffer = null;
        //                    var viewpathed = "";



        //                    if ((prevedid ?? "") == "")
        //                    {
        //                        ZipEntry newZipEntry = new ZipEntry();
        //                        using (var memoryStream = new MemoryStream())
        //                        {
        //                            using (var zip = new ZipFile())
        //                            {
        //                                zip.Password = HasKeyProtect.Encryption(LoginAksesKey);
        //                                foreach (DataRow doc in DocumentByte.Rows)
        //                                {
        //                                    //bytesToDecrypt = HasKeyProtect.SetFileByteDecrypt(doc.FILE_BYTE, LoginAksesKey);
        //                                    bytesToDecrypt = doc.Field<Byte[]>("FILE_BYTE");
        //                                    filenamepar = doc.Field<string>("NO_PERJANJIAN") + "_" + doc.Field<string>("NO_DOCUMENT") + ".pdf";
        //                                    zip.AddEntry(filenamepar, bytesToDecrypt);

        //                                }

        //                                zip.Save(memoryStream);
        //                            }
        //                            buffer = memoryStream.ToArray();
        //                        }


        //                        //update sesuai dengan data yang didapat harus join//
        //                        dataupload.Rows.Clear();
        //                        foreach (DataRow aktasel in DocumentByte.Rows)
        //                        {
        //                            dataupload.Rows.Add(new object[] { aktasel["CONT_TYPE"], aktasel["CLIENT_FDC_ID"], aktasel["NO_PERJANJIAN"], aktasel["CLNT_ID"], "", aktasel["NO_DOCUMENT"] });
        //                        }

        //                        int resultflag = await Commonddl.dbSettDocumentFlagByno(dataupload, Jenisdoc, ClientID, IDCabang, UserID, GroupName, SecureModuleId, LoginAksesKey, Email, UserGenCode);
        //                        //berhasil flag//
        //                        if (resultflag == 1)
        //                        {
        //                            string minut = DateTime.Now.ToString("ddMMyyyymmss");
        //                            contenttyped = "application/zip";
        //                            filenamepar = "SERTIFIKAT_FIDUSIA_" + minut + ".zip";
        //                        }
        //                        else
        //                        {
        //                            return Json(new
        //                            {
        //                                moderror = IsErrorTimeout,
        //                                msg = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultflag)
        //                            });
        //                        }

        //                    }

        //                    return Json(new
        //                    {
        //                        moderror = IsErrorTimeout,
        //                        contenttype = contenttyped,
        //                        bytetyipe = buffer,
        //                        filename = filenamepar,
        //                        viewpath = viewpathed,
        //                        msg = EnumMessage,
        //                        idprev = prevedid,
        //                        namapool = namaidpool,
        //                        dolpet = ListIDgrd
        //                    });
        //                }
        //                else
        //                {

        //                    return Json(new
        //                    {
        //                        moderror = IsErrorTimeout,
        //                        msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKunciSandiFile))
        //                    });

        //                }

        //            }
        //            else
        //            {
        //                return Json(new
        //                {
        //                    moderror = IsErrorTimeout,
        //                    msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterNotValidFileNotFound))
        //                });
        //            }

        //        }
        //        else
        //        {
        //            int row = dataupload.Rows.Count;
        //            if (row <= 10)
        //            {
        //                DataTable dt = await Pendaftaranddl.dbGetReOrderAHU(dataupload, "", ClientID, IDCabang, UserID, GroupName, SecureModuleId, LoginAksesKey, Email, UserGenCode);
        //                int resultflag = int.Parse(dt.Rows[0][0].ToString());

        //                return Json(new
        //                {
        //                    moderror = IsErrorTimeout,
        //                    msg = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultflag)
        //                });
        //            }
        //            else
        //            {
        //                return Json(new
        //                {
        //                    moderror = IsErrorTimeout,
        //                    msg = "Daftarkan Ulang Ke AHU tidak boleh lebih dari 10 Kontrak",
        //                });
        //            }

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



        //#region input pendaftaran order
        //public async Task<ActionResult> clnGetBranchInputOrder(string clientid)
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


        //    try
        //    {

        //        //set session filterisasi //
        //        modFilter = TempData["PendaftaranOrderListFilter"] as cFilterContract;
        //        Pendaftaran = TempData["PendaftaranOrderList"] as vmPendaftaran;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        IEnumerable<cListSelected> tempbrach = (TempData["tempbrachorder" + clientid] as IEnumerable<cListSelected>);

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
        //                Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt("", decSelectClient, "", UserID);
        //                tempbrach = Common.ddlBranch;
        //            }
        //        }

        //        TempData["tempbrachorder" + clientid] = tempbrach;
        //        TempData["PendaftaranOrderList"] = Pendaftaran;
        //        TempData["PendaftaranOrderListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            branchjson = new JavaScriptSerializer().Serialize(tempbrach),
        //            brachselect = HasKeyProtect.Decryption(SelectBranch),
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
        //public async Task<ActionResult> clnPendaftaranOrder(string menu, string caption)
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



        //    try
        //    {

        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = Account.AccountLogin.UserID;
        //        string UserName = Account.AccountLogin.UserName;
        //        string ClientID = Account.AccountLogin.ClientID;
        //        string IDCabang = Account.AccountLogin.IDCabang;
        //        string IDNotaris = Account.AccountLogin.IDNotaris;
        //        string GroupName = Account.AccountLogin.GroupName;
        //        string ClientName = Account.AccountLogin.ClientName;
        //        string CabangName = Account.AccountLogin.CabangName;
        //        string Mailed = Account.AccountLogin.Mailed;
        //        string GenMoon = Account.AccountLogin.GenMoon;
        //        string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
        //        string idcaption = HasKeyProtect.Encryption(caption);

        //        // extend //
        //        string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
        //        // extend //

        //        // some field must be overide first for default filter//
        //        string SelectClient = ClientID;
        //        string SelectBranch = IDCabang;

        //        //descript some value for db//
        //        SelectClient = HasKeyProtect.Decryption(SelectClient);
        //        SelectBranch = HasKeyProtect.Decryption(SelectBranch);


        //        modFilter.UserID = UserID;
        //        modFilter.UserName = UserName;
        //        modFilter.ClientLogin = ClientID;
        //        modFilter.BranchLogin = IDCabang;
        //        modFilter.NotarisLogin = IDNotaris;
        //        modFilter.GroupName = GroupName;
        //        modFilter.ClientName = ClientName;
        //        modFilter.CabangName = CabangName;
        //        modFilter.MailerDaemoon = Mailed;
        //        modFilter.GenDeamoon = GenMoon;
        //        modFilter.UserType = int.Parse(UserTypes);
        //        modFilter.UserTypeApps = int.Parse(UserTypes);
        //        modFilter.idcaption = idcaption;

        //        modFilter.SelectClient = SelectClient;
        //        modFilter.SelectBranch = SelectBranch;

        //        Pendaftaran.Pendaftaranorderinput = new cpendaftaranOder();

        //        if (Common.ddlDocStatus == null)
        //        {
        //            Common.ddlDocStatus = await Commonddl.dbddlgetparamenumsList("DocStatus");
        //        }

        //        if (Common.ddlJenisPelanggan == null)
        //        {
        //            Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
        //        }

        //        if (Common.ddlJenisKelamin == null)
        //        {
        //            Common.ddlJenisKelamin = await Commonddl.dbddlgetparamenumsList("JNSKLM");
        //        }

        //        if (Common.ddlJenisIdentitas == null)
        //        {
        //            Common.ddlJenisIdentitas = await Commonddl.dbddlgetparamenumsList("JNSIDEN");
        //        }

        //        if (Common.ddlJenisKewarganegaraan == null)
        //        {
        //            Common.ddlJenisKewarganegaraan = await Commonddl.dbddlgetparamenumsList("JNSWRG");
        //        }

        //        //if (Common.ddlJenisKontrak == null)
        //        {
        //            Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("JNSLEASE");
        //        }

        //        if (Common.ddlJenisPenggunaan == null)
        //        {
        //            Common.ddlJenisPenggunaan = await Commonddl.dbddlgetparamenumsList("JNSUSED");
        //        }

        //        if (Common.ddlJenisObject == null)
        //        {
        //            Common.ddlJenisObject = await Commonddl.dbddlgetparamenumsList("JNSOBJ");
        //        }

        //        if (Common.ddlKondisiObject == null)
        //        {
        //            Common.ddlKondisiObject = await Commonddl.dbddlgetparamenumsList("KONDOBJ");
        //        }

        //        if (Common.ddlJenisKewarganegaraan == null)
        //        {
        //            Common.ddlJenisKewarganegaraan = await Commonddl.dbddlgetparamenumsList("JNSWRG");
        //        }

        //        if (Common.ddlRoda == null)
        //        {
        //            Common.ddlRoda = await Commonddl.dbddlgetparamenumsList("RODA");
        //        }

        //        //debitur//
        //        if (Common.ddlProvin == null)
        //        {
        //            Common.ddlProvin = await Commonddl.dbGetprovinAHU("Provin", "");
        //        }
        //        if (Common.ddlKota == null)
        //        {
        //            Common.ddlKota = await Commonddl.dbGetprovinAHU("kota", "");
        //        }
        //        if (Common.ddlKecamatan == null)
        //        {
        //            Common.ddlKecamatan = await Commonddl.dbGetprovinAHU("kecamatan", "");
        //        }
        //        if (Common.ddlKelurahan == null)
        //        {
        //            Common.ddlKelurahan = await Commonddl.dbGetprovinAHU("kelurahan", "");
        //        }

        //        if (Common.ddlProvin1 == null)
        //        {
        //            Common.ddlProvin1 = await Commonddl.dbGetprovinAHU("Provin", "");
        //        }
        //        if (Common.ddlKota1 == null)
        //        {
        //            Common.ddlKota1 = await Commonddl.dbGetprovinAHU("kota", "");
        //        }
        //        if (Common.ddlKecamatan1 == null)
        //        {
        //            Common.ddlKecamatan1 = await Commonddl.dbGetprovinAHU("kecamatan", "");
        //        }
        //        if (Common.ddlKelurahan1 == null)
        //        {
        //            Common.ddlKelurahan1 = await Commonddl.dbGetprovinAHU("kelurahan", "");
        //        }

        //        if (Common.ddlProvin2 == null)
        //        {
        //            Common.ddlProvin2 = await Commonddl.dbGetprovinAHU("Provin", "");
        //        }
        //        if (Common.ddlKota2 == null)
        //        {
        //            Common.ddlKota2 = await Commonddl.dbGetprovinAHU("kota", "");
        //        }
        //        if (Common.ddlKecamatan2 == null)
        //        {
        //            Common.ddlKecamatan2 = await Commonddl.dbGetprovinAHU("kecamatan", "");
        //        }
        //        if (Common.ddlKelurahan2 == null)
        //        {
        //            Common.ddlKelurahan2 = await Commonddl.dbGetprovinAHU("kelurahan", "");
        //        }


        //        if (Common.ddlClient == null)
        //        {
        //            SelectClient = HasKeyProtect.Decryption(SelectClient);
        //            Common.ddlClient = await Commonddl.dbGetClientListByEncrypt("");
        //        }

        //        if (Common.ddlBranch == null)
        //        {
        //            Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt("");
        //        }


        //        ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
        //        ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);


        //        ViewData["SelectJenisPerjanjian"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);
        //        ViewData["SelectJenisUsageType"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPenggunaan);
        //        ViewData["SelectJenisObjt"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisObject);
        //        ViewData["SelectKondisiObjt"] = OwinLibrary.Get_SelectListItem(Common.ddlKondisiObject);


        //        ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
        //        ViewData["SelectDocStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlDocStatus);
        //        ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKelamin);
        //        ViewData["SelectJenisIdentitas"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisIdentitas);
        //        ViewData["SelectJenisKewarganegaraan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKewarganegaraan);

        //        ViewData["SelectProvin"] = OwinLibrary.Get_SelectListItem(Common.ddlProvin);
        //        ViewData["SelectKota"] = OwinLibrary.Get_SelectListItem(Common.ddlKota);
        //        ViewData["SelectKecamatan"] = OwinLibrary.Get_SelectListItem(Common.ddlKecamatan);
        //        ViewData["SelectKelurahan"] = OwinLibrary.Get_SelectListItem(Common.ddlKelurahan);

        //        ViewData["SelectProvin1"] = OwinLibrary.Get_SelectListItem(Common.ddlProvin1);
        //        ViewData["SelectKota1"] = OwinLibrary.Get_SelectListItem(Common.ddlKota1);
        //        ViewData["SelectKecamatan1"] = OwinLibrary.Get_SelectListItem(Common.ddlKecamatan1);
        //        ViewData["SelectKelurahan1"] = OwinLibrary.Get_SelectListItem(Common.ddlKelurahan1);

        //        ViewData["SelectProvin2"] = OwinLibrary.Get_SelectListItem(Common.ddlProvin2);
        //        ViewData["SelectKota2"] = OwinLibrary.Get_SelectListItem(Common.ddlKota2);
        //        ViewData["SelectKecamatan2"] = OwinLibrary.Get_SelectListItem(Common.ddlKecamatan2);
        //        ViewData["SelectKelurahan2"] = OwinLibrary.Get_SelectListItem(Common.ddlKelurahan2);
        //        ViewData["SelectRoda"] = OwinLibrary.Get_SelectListItem(Common.ddlRoda);

        //        if (Session["ErroNoPerjanjian"] != null)
        //        {
        //            Pendaftaran.Pendaftaranorderinput = (cpendaftaranOder)Session["ErroNoPerjanjian"];
        //        }

        //        //set session filterisasi //
        //        TempData["PendaftaranOrderList"] = Pendaftaran;
        //        TempData["PendaftaranOrderListFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // set caption menut text //

        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "Pendaftaran";
        //        ViewBag.action = "clnPendaftaranOrder";
        //        ViewBag.UserTypeApps = modFilter.UserType;


        //        //ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";


        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Pendaftaran/uiPendaftaranOrder.cshtml", Pendaftaran.Pendaftaranorderinput),
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
        //public async Task<ActionResult> clnPendaftaranOrdersve(cpendaftaranOder model)
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

        //    try
        //    {

        //        //get session filterisasi //
        //        modFilter = TempData["PendaftaranOrderListFilter"] as cFilterContract;
        //        Pendaftaran = TempData["PendaftaranOrderList"] as vmPendaftaran;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //DataRow dtr = Pemberkasan.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == secnocon).SingleOrDefault();
        //        //string noperjanjian = dtr.Field<string>("NoPerjanjian");
        //        //string cont_type = dtr.Field<int>("CONT_TYPE").ToString();
        //        //double client_fdc_id = dtr.Field<Int64>("IDFDC");
        //        //string namanasabah = dtr.Field<string>("NamaNasabah");

        //        string UserID = Account.AccountLogin.UserID;
        //        string UserName = Account.AccountLogin.UserName;
        //        string ClientID = Account.AccountLogin.ClientID;
        //        string IDCabang = Account.AccountLogin.IDCabang;
        //        string IDNotaris = Account.AccountLogin.IDNotaris;
        //        string GroupName = Account.AccountLogin.GroupName;
        //        string ClientName = Account.AccountLogin.ClientName;
        //        string CabangName = Account.AccountLogin.CabangName;
        //        string Mailed = Account.AccountLogin.Mailed;
        //        string GenMoon = Account.AccountLogin.GenMoon;
        //        string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
        //        string caption = HasKeyProtect.Decryption(modFilter.idcaption);

        //        // extend //
        //        string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
        //        // extend //

        //        // some field must be overide first for default filter//
        //        string SelectClient = ClientID;
        //        string SelectBranch = IDCabang;

        //        //descript some value for db//
        //        SelectClient = HasKeyProtect.Decryption(SelectClient);
        //        SelectBranch = HasKeyProtect.Decryption(SelectBranch);

        //        string EnumMessage = "";

        //        List<cpendaftaranOder> listorder = new List<cpendaftaranOder>();
        //        listorder.Add(model);

        //        if (int.Parse(UserTypes) == (int)HashNetFramework.UserType.FDCM)
        //        {
        //            SelectClient = (HasKeyProtect.Decryption(model.CLNT_ID)) != "" ? HasKeyProtect.Decryption(model.CLNT_ID) : "";
        //            SelectBranch = (model.BRCH_ID ?? "");
        //            CabangName = "";
        //            //if (Common.ddlBranch == null)
        //            //{
        //            Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(SelectBranch, SelectClient, "","");
        //            //}
        //            CabangName = Common.ddlBranch.Where(x => x.Value == SelectBranch).SingleOrDefault().Text;
        //            //Common.ddlBranch = Common.ddlBranch;
        //        }

        //        EnumMessage = lgPendaftaran.CheckVlaidasiInput(model, SelectClient, SelectBranch, int.Parse(UserTypes));
        //        int result = 0;
        //        if (EnumMessage == "")
        //        {

        //            //get data provinsi DEBITUR//
        //            model.GRTE_PROVINCE = Common.ddlProvin.Where(x => x.Value == model.GRTE_PROVINCE).SingleOrDefault().Text;
        //            model.GRTE_CITY = Common.ddlKota.Where(x => x.Value == model.GRTE_CITY).SingleOrDefault().Text;
        //            //model.GRTE_SUBDISTRICT = Common.ddlKecamatan.Where(x => x.Value == model.GRTE_SUBDISTRICT).SingleOrDefault().Text;
        //            //model.GRTE_URBAN_VILLAGE = Common.ddlKelurahan.Where(x => x.Value == model.GRTE_URBAN_VILLAGE).SingleOrDefault().Text;

        //            ////get data provinsi BPKB//
        //            model.CUST_PROVINCE = Common.ddlProvin1.Where(x => x.Value == model.CUST_PROVINCE).SingleOrDefault().Text;
        //            model.CUST_CITY = Common.ddlKota1.Where(x => x.Value == model.CUST_CITY).SingleOrDefault().Text;
        //            //model.CUST_SUBDISTRICT = Common.ddlKecamatan1.Where(x => x.Value == model.CUST_SUBDISTRICT).SingleOrDefault().Text;
        //            //model.CUST_URBAN_VILLAGE = Common.ddlKelurahan1.Where(x => x.Value == model.CUST_URBAN_VILLAGE).SingleOrDefault().Text;

        //            //get data provinsi TGJW//
        //            if ((model.TGJW_PROVINCE ?? "") != "")
        //            {
        //                model.TGJW_PROVINCE = Common.ddlProvin2.Where(x => x.Value == model.TGJW_PROVINCE).SingleOrDefault().Text;
        //            }
        //            if ((model.TGJW_CITY ?? "") != "")
        //            {
        //                model.TGJW_CITY = Common.ddlKota2.Where(x => x.Value == model.TGJW_CITY).SingleOrDefault().Text;
        //            }
        //            //if ((model.TGJW_SUBDISTRICT ?? "") != "")
        //            //{
        //            //    model.TGJW_SUBDISTRICT = Common.ddlKecamatan2.Where(x => x.Value == model.TGJW_SUBDISTRICT).SingleOrDefault().Text;
        //            //}
        //            //if ((model.TGJW_URBAN_VILLAGE ?? "") != "")
        //            //{
        //            //    model.TGJW_URBAN_VILLAGE = Common.ddlKelurahan2.Where(x => x.Value == model.TGJW_URBAN_VILLAGE).SingleOrDefault().Text;
        //            //}


        //            //adjus some colom//
        //            model.CONT_DATE = model.CONT_DATE_SIGN;
        //            model.DATE_LIVE = model.CONT_DATE_SIGN;
        //            model.CONT_TENOR = "0";
        //            model.CONT_TYPE = "0";
        //            model.CLNT_ID = SelectClient;
        //            model.BRCH_ID = SelectBranch;
        //            model.BRCH_NAME = CabangName;
        //            model.CONT_STATUS = "0";
        //            model.CONT_PRINCIPAL_AMOUNT = model.CONT_PRINCIPAL_AMOUNT.Replace(",", "");
        //            model.CONT_COLLETERAL_AMOUNT = model.CONT_COLLETERAL_AMOUNT.Replace(",", "");
        //            model.OBJT_AMOUNT = model.OBJT_AMOUNT.Replace(",", "");
        //            model.OBJT_MANUFACTURE_YEAR = model.OBJT_ASSEMBLY_YEAR;
        //            model.SEND_CLIENT_DATE = DateTime.Now.ToString("yyyy-MM-dd");

        //            DataTable dt = OwinLibrary.ToDataTable(listorder);
        //            result = await Pendaftaranddl.dbsvePendaftaranOrder(dt, SelectClient, UserID, GroupName, caption);

        //            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //            EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Pengajuan Pendaftaran Fidusia ", "tersimpan") : EnumMessage;
        //            Session["ErroNoPerjanjian"] = null;
        //        }
        //        else
        //        {
        //            result = 0;
        //            Session["ErroNoPerjanjian"] = model;
        //        }

        //        TempData["PendaftaranOrderList"] = Pendaftaran;
        //        TempData["PendaftaranOrderListFilter"] = modFilter;
        //        TempData["common"] = Common;


        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            resulted = result,
        //            msg = EnumMessage,
        //            view = "",
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
        //        if (IsErrorTimeout == false)
        //        {
        //            Session["ErroNoPerjanjian"] = model;
        //            TempData["common"] = Common;
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
        //#endregion input pendaftaran order
    }
}
