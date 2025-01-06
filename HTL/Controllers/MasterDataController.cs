using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class MasterDataController : Controller
    {
        private vmAccount Account = new vmAccount();
        private blAccount lgAccount = new blAccount();
        private vmMasterData master = new vmMasterData();
        private cFilterMasterData modFilter = new cFilterMasterData();
        private vmMasterDataddl MasterDataddl = new vmMasterDataddl();
        private vmCommon Common = new vmCommon();
        private vmCommonddl Commonddl = new vmCommonddl();

        //#region Master Provinsi
        //[HttpPost]
        //public async Task<ActionResult> clnMTDPROVIN(String menu, String caption)
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

        //        List<String> recordPage = await MasterDataProvinsiddl.dbGeProvinListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;

        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        totalRecordclient = dtlist[0].Rows.Count;
        //        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //        if (Common.ddlProvin == null)
        //        {
        //            Common.ddlProvin = await Commonddl.dbGetProvinsi();
        //        }

        //        ViewData["SelectProvinsi"] = OwinLibrary.Get_SelectListItem(Common.ddlProvin);

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

        //        //set session filterisasi //
        //        TempData["ProvinsiList"] = master;
        //        TempData["ProvinsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDPROVIN";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Provinsi/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDPROVIN(int paged)
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
        //        modFilter = TempData["ProvinsiFilter"] as cFilterMasterData;
        //        master = TempData["ProvinsiList"] as vmMasterData;
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
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["ProvinsiList"] = master;
        //        TempData["ProvinsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Provinsi/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDPROVIN(string paramkey)
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
        //        modFilter = TempData["ProvinsiFilter"] as cFilterMasterData;
        //        master = TempData["ProvinsiList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        ViewData["SelectProvinsi"] = OwinLibrary.Get_SelectListItem(Common.ddlProvin);

        //        cMasterDataProvinsi modeldata = new cMasterDataProvinsi();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.Provinsi = dr["Provinsi"].ToString();
        //            modeldata.Kota = dr["Kota"].ToString();
        //            modeldata.Kecamatan = dr["Kecamatan"].ToString();
        //            modeldata.Kelurahan = dr["Kelurahan"].ToString();
        //            modeldata.ZipCode = dr["ZipCode"].ToString();
        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["ProvinsiList"] = master;
        //        TempData["ProvinsiFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Provinsi/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clnUpdMasterMTDPROVIN(cMasterDataProvinsi model)
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
        //        master = TempData["ProvinsiList"] as vmMasterData;
        //        modFilter = TempData["ProvinsiFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["ProvinsiList"] = master;
        //        TempData["ProvinsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateProvin(ID, model.Provinsi, model.Kota, model.Kecamatan, model.Kelurahan, model.ZipCode, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Provinsi ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            //refresh//
        //            var totalrecord = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") != keylookupdata);
        //            master.DTFromDB = totalrecord.Count() == 0 ? null : totalrecord.CopyToDataTable();

        //            if (master.DTFromDB == null)
        //            {
        //                ////get total data from server//
        //                List<String> recordPage = await MasterDataProvinsiddl.dbGeProvinListCount(keyword, PageNumber, caption, UserID, GroupName);
        //                TotalRecord = Convert.ToDouble(recordPage[0]);
        //                TotalPage = Convert.ToDouble(recordPage[1]);
        //                pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //                pagenumberclient = PageNumber;
        //            }
        //            else
        //            {
        //                double record = master.DTFromDB.Rows.Count;
        //                if ((pagenumberclient * pagingsizeclient) >= record)
        //                {
        //                    pagenumberclient = Math.Ceiling(record / pagingsizeclient);
        //                    TotalPage = Math.Ceiling(record / pagingsizeclient);
        //                }
        //            }

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["ProvinsiList"] = master;
        //            TempData["ProvinsiFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectProvinsi"] = OwinLibrary.Get_SelectListItem(Common.ddlProvin);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Provinsi/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDPROVIN()
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
        //        modFilter = TempData["ProvinsiFilter"] as cFilterMasterData;
        //        master = TempData["ProvinsiList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword;

        //        TempData["ProvinsiList"] = master;
        //        TempData["ProvinsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Provinsi/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDPROVIN(cFilterMasterData model)
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
        //        modFilter = TempData["ProvinsiFilter"] as cFilterMasterData;
        //        master = TempData["ProvinsiList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";

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
        //        modFilter.keyword = Keyword;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGeProvinListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["ProvinsiFilter"] = modFilter;
        //            TempData["ProvinsiList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Provinsi/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["ProvinsiFilter"] = modFilter;
        //            TempData["ProvinsiList"] = master;
        //            TempData["common"] = Common;
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
        //#endregion Master Provinsi

        //#region Master Provinsi AHU
        //[HttpPost]
        //public async Task<ActionResult> clnMTDPROVINAHU(String menu, String caption)
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
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGeProvinAhuListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinAhuList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        totalRecordclient = dtlist[0].Rows.Count;
        //        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //        //if (Common.ddlPROVINAHU == null)
        //        //{
        //        //    Common.ddlPROVINAHU = await Commonddl.dbGetPROVINAHUsi();
        //        //}

        //        //ViewData["SelectPROVINAHUsi"] = OwinLibrary.Get_SelectListItem(Common.ddlPROVINAHU);

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

        //        //set session filterisasi //
        //        TempData["PROVINAHUsiList"] = master;
        //        TempData["PROVINAHUsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDPROVINAHU";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ProvinsiAHU/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDPROVINAHU(int paged)
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
        //        modFilter = TempData["PROVINAHUsiFilter"] as cFilterMasterData;
        //        master = TempData["PROVINAHUsiList"] as vmMasterData;
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
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinAhuList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["PROVINAHUsiList"] = master;
        //        TempData["PROVINAHUsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ProvinsiAHU/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDPROVINAHU(string paramkey)
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
        //        modFilter = TempData["PROVINAHUsiFilter"] as cFilterMasterData;
        //        master = TempData["PROVINAHUsiList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //ViewData["SelectPROVINAHUsi"] = OwinLibrary.Get_SelectListItem(Common.ddlPROVINAHU);

        //        cMasterDataProvinsiAHU modeldata = new cMasterDataProvinsiAHU();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.Provinsi = dr["NamaProvinsi"].ToString();
        //            modeldata.Kota = dr["NamaKabKot"].ToString();
        //            modeldata.AliasKota = dr["NamaKabKotAlias"].ToString();
        //            modeldata.Kecamatan = dr["NamaKecamatan"].ToString();
        //            modeldata.AliasKecamatan = dr["NamaKecamatanAlias"].ToString();
        //            modeldata.PengadilanNegeri = dr["PengadilanNegeri"].ToString();
        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["PROVINAHUsiList"] = master;
        //        TempData["PROVINAHUsiFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ProvinsiAHU/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clnUpdMasterMTDPROVINAHU(cMasterDataProvinsiAHU model)
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
        //        master = TempData["PROVINAHUsiList"] as vmMasterData;
        //        modFilter = TempData["PROVINAHUsiFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["PROVINAHUsiList"] = master;
        //        TempData["PROVINAHUsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateProvinAhu(ID, model.Provinsi, model.Kota, model.AliasKota, model.Kecamatan, model.AliasKecamatan, model.PengadilanNegeri, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Provinsi AHU/Pengadilan Negeri ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            ////get total data from server//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGeProvinAhuListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinAhuList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["PROVINAHUsiList"] = master;
        //            TempData["PROVINAHUsiFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        //ViewData["SelectPROVINAHUsi"] = OwinLibrary.Get_SelectListItem(Common.ddlPROVINAHU);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ProvinsiAHU/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDPROVINAHU()
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
        //        modFilter = TempData["PROVINAHUsiFilter"] as cFilterMasterData;
        //        master = TempData["PROVINAHUsiList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword;

        //        TempData["PROVINAHUsiList"] = master;
        //        TempData["PROVINAHUsiFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ProvinsiAHU/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDPROVINAHU(cFilterMasterData model)
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
        //        modFilter = TempData["PROVINAHUsiFilter"] as cFilterMasterData;
        //        master = TempData["PROVINAHUsiList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";

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
        //        modFilter.keyword = Keyword;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGeProvinListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetProvinAhuList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["PROVINAHUsiFilter"] = modFilter;
        //            TempData["PROVINAHUsiList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ProvinsiAHU/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["PROVINAHUsiFilter"] = modFilter;
        //            TempData["PROVINAHUsiList"] = master;
        //            TempData["common"] = Common;
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
        //#endregion Master Provinsi AHU

        //#region Master Notaris
        //[HttpPost]
        //public async Task<ActionResult> clnMTDNTRY(String menu, String caption)
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
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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

        //        //set session filterisasi //
        //        TempData["NTRYMList"] = master;
        //        TempData["NTRYMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDNTRY";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Notaris/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDNTRY(int paged)
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
        //        modFilter = TempData["NTRYMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYMList"] as vmMasterData;
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
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["NTRYMList"] = master;
        //        TempData["NTRYMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Notaris/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDNTRY(string paramkey)
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
        //        modFilter = TempData["NTRYMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //ViewData["SelectNTRYsi"] = OwinLibrary.Get_SelectListItem(Common.ddlNTRY);

        //        cMasterDataNotaris modeldata = new cMasterDataNotaris();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.NotarisID = dr["NTRY_ID"].ToString();
        //            modeldata.NotarisNama = dr["NTRY_NAME"].ToString();
        //            modeldata.NTRY_SK_NO = dr["NTRY_SK_NO"].ToString();
        //            modeldata.NTRY_SK_DATE = DateTime.Parse(dr["NTRY_SK_DATE"].ToString()).ToString("dd-MMMM-yyyy");
        //            modeldata.NTRY_ADDRESS = dr["NTRY_ADDRESS"].ToString();
        //            modeldata.NTRY_FEE = decimal.Parse(dr["NTRY_FEE"].ToString()).ToString("N2");
        //            modeldata.NTRY_MAX_DEED = decimal.Parse(dr["NTRY_MAX_DEED"].ToString()).ToString("N2");
        //            modeldata.RegionID = dr["NTRY_REGION"].ToString();
        //            modeldata.NTRY_DOMICILE = dr["NTRY_DOMICILE"].ToString();
        //            modeldata.NTRY_EMAIL = dr["NTRY_EMAIL"].ToString();
        //            modeldata.NTRY_NAME_REG_AHU = dr["NTRY_NAME_REG_AHU"].ToString();
        //            modeldata.NTRY_ID_AHU = dr["NTRY_ID_AHU"].ToString();
        //            modeldata.NTRY_ID_WILAYAH_AHU = dr["NTRY_ID_WILAYAH_AHU"].ToString();
        //            modeldata.NTRY_WILAYAH_AHU = dr["NTRY_WILAYAH_AHU"].ToString();
        //            modeldata.actived = bool.Parse(dr["Actived"].ToString());

        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["NTRYMList"] = master;
        //        TempData["NTRYMListFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Notaris/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clnUpdMasterMTDNTRY(cMasterDataNotaris model)
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
        //        master = TempData["NTRYMList"] as vmMasterData;
        //        modFilter = TempData["NTRYMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["NTRYMList"] = master;
        //        TempData["NTRYMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateNotaris(ID, model.NotarisID, model.NotarisNama, model.RegionID, model.NTRY_SK_NO, model.NTRY_SK_DATE,
        //                                                                 model.NTRY_ADDRESS, model.NTRY_EMAIL, model.NTRY_DOMICILE, model.NTRY_NAME_REG_AHU, model.NTRY_ID_AHU,
        //                                                                 model.NTRY_ID_WILAYAH_AHU, model.NTRY_WILAYAH_AHU, model.actived,
        //                                                                 model.NTRY_FEE.Replace(",", ""), model.NTRY_MAX_DEED.Replace(",", ""), caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Notaris ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            ////get total data from server//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["NTRYMList"] = master;
        //            TempData["NTRYMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        //ViewData["SelectNTRYsi"] = OwinLibrary.Get_SelectListItem(Common.ddlNTRY);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Notaris/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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

        //public async Task<ActionResult> clndelAddMTDNTRY(string paramkey)
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
        //        master = TempData["NTRYMList"] as vmMasterData;
        //        modFilter = TempData["NTRYMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["NTRYMList"] = master;
        //        TempData["NTRYMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = paramkey;

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
        //        modFilter.keyword = keyword;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbdeleteNotaris(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Notaris ", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["NTRYMList"] = master;
        //            TempData["NTRYMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Notaris/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDNTRY()
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
        //        modFilter = TempData["NTRYMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword;

        //        TempData["NTRYMList"] = master;
        //        TempData["NTRYMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Notaris/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDNTRY(cFilterMasterData model)
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
        //        modFilter = TempData["NTRYMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";

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
        //        modFilter.keyword = Keyword;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["NTRYMListFilter"] = modFilter;
        //            TempData["NTRYMList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Notaris/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["NTRYsiFilter"] = modFilter;
        //            TempData["NTRYsiList"] = master;
        //            TempData["common"] = Common;
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

        //#endregion Master notaris

        //#region Master Notaris Staff
        //[HttpPost]
        //public async Task<ActionResult> clnMTDNTRYSTAFF(String menu, String caption)
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
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisStaffListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisStaffList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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

        //        if (Common.ddlNotaris == null)
        //        {
        //            Common.ddlNotaris = await Commonddl.dbGetNotarisListByEncrypt();
        //        }

        //        ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

        //        //set session filterisasi //
        //        TempData["NTRYSTAFFMList"] = master;
        //        TempData["NTRYSTAFFMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDNTRYSTAFF";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotarisStaff/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDNTRYSTAFF(int paged)
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
        //        modFilter = TempData["NTRYSTAFFMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYSTAFFMList"] as vmMasterData;
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
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisStaffList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["NTRYSTAFFMList"] = master;
        //        TempData["NTRYSTAFFMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotarisStaff/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDNTRYSTAFF(string paramkey)
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
        //        modFilter = TempData["NTRYSTAFFMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYSTAFFMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

        //        cMasterDataNotarisstaff modeldata = new cMasterDataNotarisstaff();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.NotarisID = HasKeyProtect.Encryption(dr["idnotaris"].ToString());
        //            modeldata.StaffName = dr["StaffName"].ToString();
        //            modeldata.BirthPlace = dr["BirthPlace"].ToString();
        //            modeldata.BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd-MMMM-yyyy");
        //            modeldata.City = dr["City"].ToString();
        //            modeldata.Address = dr["Address"].ToString();

        //            modeldata.NeighborHood_No = dr["NeighborHood_No"].ToString();
        //            modeldata.Hamlet_No = dr["Hamlet_No"].ToString();
        //            modeldata.UrbanVillage = dr["UrbanVillage"].ToString();
        //            modeldata.SubDistrict = dr["SubDistrict"].ToString();
        //            modeldata.Identity_No = dr["Identity_No"].ToString();
        //            modeldata.StartDate = dr["StartDate"].ToString() != "" ? DateTime.Parse(dr["StartDate"].ToString()).ToString("dd-MMMM-yyyy") : null;
        //            modeldata.EndDate = dr["EndDate"].ToString() != "" ? DateTime.Parse(dr["EndDate"].ToString()).ToString("dd-MMMM-yyyy") : null;

        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["NTRYSTAFFMList"] = master;
        //        TempData["NTRYSTAFFMListFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotarisStaff/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clnUpdMasterMTDNTRYSTAFF(cMasterDataNotarisstaff model)
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
        //        master = TempData["NTRYSTAFFMList"] as vmMasterData;
        //        modFilter = TempData["NTRYSTAFFMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["NTRYSTAFFMList"] = master;
        //        TempData["NTRYSTAFFMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateNotarisStaff(ID, NotarisID, model.StaffName, model.BirthPlace, model.BirthDate, model.City,
        //                                                                 model.Address, model.NeighborHood_No, model.Hamlet_No, model.UrbanVillage, model.SubDistrict,
        //                                                                 model.Identity_No, model.StartDate, model.EndDate, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Notaris Staff ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            ////get total data from server//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisStaffListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisStaffList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["NTRYSTAFFMList"] = master;
        //            TempData["NTRYSTAFFMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotarisStaff/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clndelAddMTDNTRYSTAFF(string paramkey)
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
        //        master = TempData["NTRYSTAFFMList"] as vmMasterData;
        //        modFilter = TempData["NTRYSTAFFMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["NTRYSTAFFMList"] = master;
        //        TempData["NTRYSTAFFMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = paramkey;

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
        //        modFilter.keyword = keyword;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbdeleteNotarisStaff(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Notaris Staff", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisStaffListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisStaffList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["NTRYSTAFFMList"] = master;
        //            TempData["NTRYSTAFFMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotarisStaff/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDNTRYSTAFF()
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
        //        modFilter = TempData["NTRYSTAFFMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYSTAFFMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword;

        //        TempData["NTRYSTAFFMList"] = master;
        //        TempData["NTRYSTAFFMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotarisStaff/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDNTRYSTAFF(cFilterMasterData model)
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
        //        modFilter = TempData["NTRYSTAFFMListFilter"] as cFilterMasterData;
        //        master = TempData["NTRYSTAFFMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";

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
        //        modFilter.keyword = Keyword;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetNotarisStaffListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetNotarisStaffList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["NTRYSTAFFMListFilter"] = modFilter;
        //            TempData["NTRYSTAFFMList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotarisStaff/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["NTRYSTAFFsiFilter"] = modFilter;
        //            TempData["NTRYSTAFFsiList"] = master;
        //            TempData["common"] = Common;
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
        //#endregion Master notaris staff

        #region Master DIVISI

        [HttpPost]
        public async Task<ActionResult> clnMTDDVISI(String menu, String caption)
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
                string Keyword = "";

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetDivisiListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetDivisiList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlPic == null)
                {
                    Common.ddlPic = await Commonddl.dbdbGetDdlPICListByEncrypt("1", "", caption, UserID, GroupName);
                }

                if (Common.ddlPeriode == null)
                {
                    Common.ddlPeriode = await Commonddl.dbdbGetDdlEnumsListByEncrypt("CONTPERIOD", caption, UserID, GroupName);
                }

                if (Common.ddlTeamHO == null)
                {
                    Common.ddlTeamHO = await Commonddl.dbdbGetDdlEnumsListByEncrypt("GRUPTEAM", caption, UserID, GroupName);
                }

                ViewData["SelectTeamHO"] = OwinLibrary.Get_SelectListItem(Common.ddlTeamHO);
                ViewData["SelectPIC"] = OwinLibrary.Get_SelectListItem(Common.ddlPic);
                ViewData["SelectPeriode"] = OwinLibrary.Get_SelectListItem(Common.ddlPeriode);

                //set session filterisasi //
                TempData["DVISIMList"] = master;
                TempData["DVISIMListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDDVISI";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDDVISI(int paged)
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
                modFilter = TempData["DVISIMListFilter"] as cFilterMasterData;
                master = TempData["DVISIMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetDivisiList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];
                bool isModeFilter = modFilter.isModeFilter;

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["DVISIMList"] = master;
                TempData["DVISIMListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDDVISI(string paramkey, string oprfun)
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
                modFilter = TempData["DVISIMListFilter"] as cFilterMasterData;
                master = TempData["DVISIMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectTeamHO"] = new MultiSelectList(Common.ddlTeamHO, "Value", "Text");
                ViewData["SelectPIC"] = new MultiSelectList(Common.ddlPic, "Value", "Text");
                ViewData["SelectPeriode"] = new MultiSelectList(Common.ddlPeriode, "Value", "Text");

                string Opr4view = "add";
                cMasterDataDivisi modeldata = new cMasterDataDivisi();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.DIVISI_ID = (dr["ID"].ToString());
                    modeldata.DIVISI_NAME = (dr["DIVISI_NAME"].ToString());
                    modeldata.CONT_PERIODE = (dr["CONT_PERIODE"].ToString());
                    modeldata.CONT_ATASNAMA = (dr["CONT_UP"].ToString());
                    modeldata.CONT_JABATANATASNAMA = (dr["CONT_UPJBT"].ToString());
                    modeldata.GROUP_ID = (dr["GROUP_ID"].ToString());
                    modeldata.GROUP_NAME = (dr["GROUP_NAME"].ToString());
                    modeldata.IsActive = bool.Parse(dr["IsActive"].ToString());
                    modeldata.CONT_PERIODE_DESC = (dr["CONT_PERIODE_DESC"].ToString());
                    modeldata.DIV_CODE_WF = (dr["DIVISI_CODE"].ToString());
                    modeldata.PIC = dr["PICSelect"].ToString().Split('|');
                    modeldata.PicSelect = dr["PICSelect"].ToString().Replace("|", ",");
                    modeldata.hdPicSelect = modeldata.PicSelect;

                    //modeldata.PIC= dr["PicSelect"].ToString().Split('|');
                    //modeldata.PicSelect = dr["PicSelect"].ToString().Replace("|", ",");

                    //List<SelectListItem> listsel = new List<SelectListItem>();
                    //foreach ( var x in modeldata.PIC)
                    //{
                    //    SelectListItem po = new SelectListItem();
                    //    po.Text = x;
                    //    po.Value = x;
                    //    listsel.Add(po);
                    //}
                    //IEnumerable<SelectListItem> myCollectionpic = listsel.AsEnumerable();
                    //ViewData["SelectPic"] = new MultiSelectList(myCollectionpic, "Value", "Text");

                    //modeldata.DivisiSelect = dr["PicSelect"].ToString().Replace("|", ",");
                    //modeldata.hdDivisiSelect = modeldata.DivisiSelect;
                    //if (modeldata.DivisiSelect != "")
                    //{
                    //    modeldata.DivisiSelect = modeldata.DivisiSelect.Remove(modeldata.DivisiSelect.Length - 1, 1);
                    //}
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["DVISIMList"] = master;
                TempData["DVISIMListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDDVISI(cMasterDataDivisi model)
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
                master = TempData["DVISIMList"] as vmMasterData;
                modFilter = TempData["DVISIMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["DVISIMList"] = master;
                TempData["DVISIMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                // string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string picselect = (model.hdPicSelect ?? "").Replace(",", "|");
                int result = await MasterDataddl.dbupdateDivisi(ID, ID.ToString(), model.DIVISI_NAME, model.CONT_PERIODE, model.CONT_ATASNAMA, model.CONT_JABATANATASNAMA, model.GROUP_ID, picselect, model.IsActive, model.DIV_CODE_WF, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Divisi", "disimpan") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetDivisiListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetDivisiList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["DVISIMList"] = master;
                    TempData["DVISIMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDDVISI(string paramkey)
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
                master = TempData["DVISIMList"] as vmMasterData;
                modFilter = TempData["DVISIMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["DVISIMList"] = master;
                TempData["DVISIMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteDivisi(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Divisi", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetDivisiListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetDivisiList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["DVISIMList"] = master;
                    TempData["DVISIMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDDVISI()
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
                modFilter = TempData["DVISIMListFilter"] as cFilterMasterData;
                master = TempData["DVISIMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";

                TempData["DVISIMList"] = master;
                TempData["DVISIMListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = Keyword,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDDVISI(cFilterMasterData model)
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
                modFilter = TempData["DVISIMListFilter"] as cFilterMasterData;
                master = TempData["DVISIMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";

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
                modFilter.keyword = Keyword;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetDivisiListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetDivisiList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["DVISIMListFilter"] = modFilter;
                    TempData["DVISIMList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Divisi/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["DVISIMListFilter"] = modFilter;
                    TempData["DVISIMList"] = master;
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

        #endregion Master DIVISI

        #region Master DIVISI GROUP

        [HttpPost]
        public async Task<ActionResult> clnMTDDVISIGRP(String menu, String caption)
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
                string Keyword = "";

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetDivisiGrpListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetDivisiGrpList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlPic == null)
                {
                    Common.ddlPic = await Commonddl.dbdbGetDdlPICListByEncrypt("", "1", caption, UserID, GroupName);
                }

                ViewData["SelectPic"] = OwinLibrary.Get_SelectListItem(Common.ddlPic);

                //set session filterisasi //
                TempData["DVISIMGRPList"] = master;
                TempData["DVISIMGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDDVISIGRP";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DivisiGroup/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDDVISIGRP(int paged)
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
                modFilter = TempData["DVISIMGRPListFilter"] as cFilterMasterData;
                master = TempData["DVISIMGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetDivisiGrpList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];
                bool isModeFilter = modFilter.isModeFilter;

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["DVISIMGRPList"] = master;
                TempData["DVISIMGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DivisiGroup/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDDVISIGRP(string paramkey, string oprfun)
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
                modFilter = TempData["DVISIMGRPListFilter"] as cFilterMasterData;
                master = TempData["DVISIMGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectPic"] = new MultiSelectList(Common.ddlPic, "Value", "Text");

                string Opr4view = "add";
                cMasterDataDivisi modeldata = new cMasterDataDivisi();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.GROUP_ID = (dr["ID"].ToString());
                    modeldata.GROUP_NAME = (dr["GROUP_NAME"].ToString());
                    modeldata.PIC = dr["PicSelect"].ToString().Split('|');
                    modeldata.PicSelect = dr["PicSelect"].ToString().Replace("|", ",");
                    modeldata.hdPicSelect = modeldata.PicSelect;
                    if (modeldata.PicSelect != "")
                    {
                        modeldata.PicSelect = modeldata.PicSelect.Remove(modeldata.PicSelect.Length - 1, 1);
                    }
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["DVISIMGRPList"] = master;
                TempData["DVISIMGRPListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DivisiGroup/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDDVISIGRP(cMasterDataDivisi model)
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
                master = TempData["DVISIMGRPList"] as vmMasterData;
                modFilter = TempData["DVISIMGRPListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["DVISIMGRPList"] = master;
                TempData["DVISIMGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                // string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string picselect = (model.hdPicSelect ?? "").Replace(",", "|");
                int result = await MasterDataddl.dbupdateDivisiGrp(ID, model.GROUP_NAME, picselect, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Admin HO", "disimpan") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetDivisiGrpListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetDivisiGrpList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["DVISIMGRPList"] = master;
                    TempData["DVISIMGRPListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DivisiGroup/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDDVISIGRP(string paramkey)
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
                master = TempData["DVISIMGRPList"] as vmMasterData;
                modFilter = TempData["DVISIMGRPListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["DVISIMGRPList"] = master;
                TempData["DVISIMGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteDivisiGrp(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Admin HO", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetDivisiGrpListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetDivisiGrpList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["DVISIMGRPList"] = master;
                    TempData["DVISIMGRPListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DivisiGroup/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDDVISIGRP()
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
                modFilter = TempData["DVISIMGRPListFilter"] as cFilterMasterData;
                master = TempData["DVISIMGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";

                TempData["DVISIMGRPList"] = master;
                TempData["DVISIMGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DivisiGroup/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDDVISIGRP(cFilterMasterData model)
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
                modFilter = TempData["DVISIMGRPListFilter"] as cFilterMasterData;
                master = TempData["DVISIMGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";

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
                modFilter.keyword = Keyword;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetDivisiGrpListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetDivisiGrpList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["DVISIMGRPListFilter"] = modFilter;
                    TempData["DVISIMGRPList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DivisiGroup/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["DVISIMGRPListFilter"] = modFilter;
                    TempData["DVISIMGRPList"] = master;
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

        #endregion Master DIVISI GROUP

        #region Master Handle Jobs

        [HttpPost]
        public async Task<ActionResult> clnMTDJOBDESC(String menu, String caption)
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
                string Keyword = "";
                string SelectDivisi = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGethandlejobsListCount(Keyword, SelectDivisi, IsActive, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGethandlejobsList(null, Keyword, SelectDivisi, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlDevisi == null)
                {
                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", caption, UserID, GroupName);
                }

                ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);

                //set session filterisasi //
                TempData["JOBDESCMList"] = master;
                TempData["JOBDESCMListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDJOBDESC";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/HandleJob/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDJOBDESC(int paged)
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
                modFilter = TempData["JOBDESCMListFilter"] as cFilterMasterData;
                master = TempData["JOBDESCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGethandlejobsList(null, keyword, SelectDivisi, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];
                bool isModeFilter = modFilter.isModeFilter;

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["JOBDESCMList"] = master;
                TempData["JOBDESCMListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/HandleJob/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDJOBDESC(string paramkey, string oprfun)
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
                modFilter = TempData["JOBDESCMListFilter"] as cFilterMasterData;
                master = TempData["JOBDESCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectDivisi"] = new MultiSelectList(Common.ddlDevisi, "Value", "Text");
                string Opr4view = "add";
                cMasterDataHandleJobs modeldata = new cMasterDataHandleJobs();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.JOBID = (dr["Id"].ToString());
                    modeldata.JOBDESC = (dr["JOBDESC"].ToString());
                    modeldata.IsActive = bool.Parse(dr["IsActive"].ToString());
                    modeldata.DivisiSelectOne = dr["DivisiSelectOne"].ToString();
                    modeldata.DivisiSelect = dr["DivisiSelect"].ToString();

                    //modeldata.DIVISI = dr["DivisiSelect"].ToString().Split('|');
                    //modeldata.DivisiSelect = dr["DivisiSelect"].ToString().Replace("|", ",");
                    //modeldata.hdDivisiSelect = modeldata.DivisiSelect;
                    //if (modeldata.DivisiSelect != "")
                    //{
                    //    modeldata.DivisiSelect = modeldata.DivisiSelect.Remove(modeldata.DivisiSelect.Length - 1, 1);
                    //}
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.oprvalue = Opr4view;
                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.Menu = modFilter.Menu;
                TempData["JOBDESCMList"] = master;
                TempData["JOBDESCMListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/HandleJob/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDJOBDESC(cMasterDataHandleJobs model)
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
                master = TempData["JOBDESCMList"] as vmMasterData;
                modFilter = TempData["JOBDESCMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                TempData["JOBDESCMList"] = master;
                TempData["JOBDESCMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                // string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

                DataRow dr;
                int ID = 0;
                int IDDET = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                    IDDET = (dr != null) ? int.Parse(dr["IdDet"].ToString()) : IDDET;
                }

                string divisiselect = String.Join(", ", model.DIVISI.ToArray()); //(model.hdDivisiSelect ?? "").Replace(",", "|");
                int result = await MasterDataddl.dbupdatehandlejobs(ID, IDDET, ID.ToString(), model.JOBDESC, divisiselect, model.DivisiSelectOne ?? "", model.IsActive, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data HandleJob", "disimpan") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGethandlejobsListCount(keyword, SelectDivisi, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGethandlejobsList(null, keyword, SelectDivisi, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["JOBDESCMList"] = master;
                    TempData["JOBDESCMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/HandleJob/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDJOBDESC(string paramkey)
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
                master = TempData["JOBDESCMList"] as vmMasterData;
                modFilter = TempData["JOBDESCMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                TempData["JOBDESCMList"] = master;
                TempData["JOBDESCMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                int IDDET = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                    IDDET = (dr != null) ? int.Parse(dr["IdDet"].ToString()) : IDDET;
                }

                int result = await MasterDataddl.dbdeletehandlejobs(ID, IDDET, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data HandleJob", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGethandlejobsListCount(keyword, SelectDivisi, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGethandlejobsList(null, keyword, SelectDivisi, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["JOBDESCMList"] = master;
                    TempData["JOBDESCMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/HandleJob/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDJOBDESC()
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
                modFilter = TempData["JOBDESCMListFilter"] as cFilterMasterData;
                master = TempData["JOBDESCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;

                ViewData["SelectDivisi"] = new MultiSelectList(Common.ddlDevisi, "Value", "Text");

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["JOBDESCMList"] = master;
                TempData["JOBDESCMListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = Keyword,
                    opsi2 = SelectDivisi,
                    opsi13 = IsActive,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/HandleJob/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDJOBDESC(cFilterMasterData model)
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
                modFilter = TempData["JOBDESCMListFilter"] as cFilterMasterData;
                master = TempData["JOBDESCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                string SelectDivisi = model.SelectDivisi ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGethandlejobsListCount(Keyword, SelectDivisi, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGethandlejobsList(null, Keyword, SelectDivisi, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    // jika ditemukan update session

                    //set in filter for paging//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    //set to object pendataran//
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["JOBDESCMListFilter"] = modFilter;
                    TempData["JOBDESCMList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/HandleJob/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["JOBDESCMListFilter"] = modFilter;
                    TempData["JOBDESCMList"] = master;
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

        #endregion Master Handle Jobs

        #region Master DocType

        [HttpPost]
        public async Task<ActionResult> clnMTDDOCTPE(String menu, String caption)
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
                string Keyword = "";
                string SelectDivivi = "";
                string SelectPengajuan = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.SelectDivisi = SelectDivivi;
                modFilter.SelectPengajuan = SelectPengajuan;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetDocTypeListCount(Keyword, SelectDivivi, SelectPengajuan, IsActive, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetDocTypeList(null, Keyword, SelectDivivi, SelectPengajuan, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlDevisi == null)
                {
                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", caption, UserID, GroupName);
                }
                if (Common.ddlRegmitraType == null)
                {
                    Common.ddlRegmitraType = await Commonddl.dbdbGetDdlEnumsListByEncrypt("REGMTYPE", caption, UserID, GroupName);
                }
                if (Common.ddlJenisDokumen == null)
                {
                    Common.ddlJenisDokumen = await Commonddl.dbdbGetJenisDokumenList("0", "", "3", caption, UserID, GroupName);
                }

                ViewData["SelectRegType"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlRegmitraType);
                ViewData["SelectDokumen"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);

                //set session filterisasi //
                TempData["DOCTPEMList"] = master;
                TempData["DOCTPEMListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDDOCTPE";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DocType/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDDOCTPE(int paged)
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
                modFilter = TempData["DOCTPEMListFilter"] as cFilterMasterData;
                master = TempData["DOCTPEMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectPengajuan = modFilter.SelectPengajuan ?? "";
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetDocTypeList(null, keyword, SelectDivisi, SelectPengajuan, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];
                bool isModeFilter = modFilter.isModeFilter;

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["DOCTPEMList"] = master;
                TempData["DOCTPEMListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DocType/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDDOCTPE(string paramkey, string oprfun)
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
                modFilter = TempData["DOCTPEMListFilter"] as cFilterMasterData;
                master = TempData["DOCTPEMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectDivisi"] = new MultiSelectList(Common.ddlDevisi, "Value", "Text");
                ViewData["SelectRegType"] = new MultiSelectList(Common.ddlRegmitraType, "Value", "Text");
                ViewData["SelectDokumen"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisDokumen);

                string Opr4view = "add";
                cMasterDataDocType modeldata = new cMasterDataDocType();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.DOCUMENT_ID = (dr["Id"].ToString());
                    modeldata.IDDET = (dr["IdDet"].ToString());
                    modeldata.DOCUMENT_TYPE = (dr["DOCUMENT_TYPE"].ToString());
                    modeldata.DOCUMENT_ALIAS = (dr["DOCUMENT_ALIAS"].ToString());
                    modeldata.REGTYPE = (dr["REGTYPE"].ToString());
                    modeldata.REGTYPE_DESC = (dr["REGTYPE_DESC"].ToString());
                    modeldata.IsNeedTemplate = bool.Parse(dr["IsNeedTemplate"].ToString());
                    modeldata.IsNeedTemplate_Desc = (dr["IsNeedTemplate_Desc"].ToString());
                    modeldata.IsMandatory = bool.Parse(dr["IsMandatory"].ToString());
                    modeldata.IsMandatory_Desc = (dr["IsMandatory_Desc"].ToString());
                    modeldata.IsActive = bool.Parse(dr["IsActive"].ToString());
                    modeldata.DivisiSelectOne = dr["DivisiSelectOne"].ToString();
                    //modeldata.DIVISI = dr["DivisiSelect"].ToString().Split(',');
                    modeldata.DivisiSelect = dr["DivisiSelect"].ToString();
                    //modeldata.hdDivisiSelect = modeldata.DivisiSelect;
                    //if (modeldata.DivisiSelect != "")
                    //{
                    //    modeldata.DivisiSelect = modeldata.DivisiSelect.Remove(modeldata.DivisiSelect.Length - 1, 1);
                    //}
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["DOCTPEMList"] = master;
                TempData["DOCTPEMListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DocType/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDDOCTPE(cMasterDataDocType model, HttpPostedFileBase files)
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
                master = TempData["DOCTPEMList"] as vmMasterData;
                modFilter = TempData["DOCTPEMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                TempData["DOCTPEMList"] = master;
                TempData["DOCTPEMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword ?? "";
                string keylookupdata = model.keylookup;
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectPengajuan = modFilter.SelectPengajuan ?? "";
                bool IsActive = modFilter.IsActiveData;

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
                modFilter.keyword = keyword;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectPengajuan = SelectPengajuan;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                // string NotarisID = HasKeyProtect.Decryption(model.NotarisID);

                DataRow dr;
                int ID = 0;
                int IDDET = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                    IDDET = (dr != null) ? int.Parse(dr["IdDet"].ToString()) : IDDET;
                }

                string divisiselect = String.Join(", ", model.DIVISI.ToArray()); //(model.hdDivisiSelect ?? "").Replace(",", "|");
                int result = await MasterDataddl.dbupdateDocType(ID, IDDET, model.DOCUMENT_TYPE, model.DOCUMENT_ALIAS ?? "", model.REGTYPE, model.IsMandatory, model.IsActive, divisiselect, model.DivisiSelectOne ?? "", model.IsNeedTemplate, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Dokumen ", "disimpan") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetDocTypeListCount(keyword, SelectDivisi, SelectPengajuan, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetDocTypeList(null, keyword, SelectDivisi, SelectPengajuan, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["DOCTPEMList"] = master;
                    TempData["DOCTPEMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //ViewData["SelectNamaNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DocType/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDDOCTPE(string paramkey)
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
                master = TempData["DOCTPEMList"] as vmMasterData;
                modFilter = TempData["DOCTPEMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                TempData["DOCTPEMList"] = master;
                TempData["DOCTPEMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectPengajuan = modFilter.SelectPengajuan ?? "";
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectPengajuan = SelectPengajuan;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                int IDDET = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                    IDDET = (dr != null) ? int.Parse(dr["IdDet"].ToString()) : IDDET;
                }

                int result = await MasterDataddl.dbdeleteDocType(ID, IDDET, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Dokumen ", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetDocTypeListCount(keyword, SelectDivisi, SelectPengajuan, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetDocTypeList(null, keyword, SelectDivisi, SelectPengajuan, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["DOCTPEMList"] = master;
                    TempData["DOCTPEMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DocType/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDDOCTPE()
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
                modFilter = TempData["DOCTPEMListFilter"] as cFilterMasterData;
                master = TempData["DOCTPEMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;

                ViewData["SelectDivisi"] = new MultiSelectList(Common.ddlDevisi, "Value", "Text");
                ViewData["SelectRegType"] = new MultiSelectList(Common.ddlRegmitraType, "Value", "Text");

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                string SelectDevisi = modFilter.SelectDivisi ?? "";
                string SelectPengajuan = modFilter.SelectPengajuan ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["DOCTPEMList"] = master;
                TempData["DOCTPEMListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = Keyword,
                    opsi2 = SelectDevisi, //decSelectBranch,
                    opsi3 = SelectPengajuan,
                    opsi13 = IsActive,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DocType/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDDOCTPE(cFilterMasterData model)
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
                modFilter = TempData["DOCTPEMListFilter"] as cFilterMasterData;
                master = TempData["DOCTPEMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                string SelectDivisi = model.SelectDivisi ?? "";
                string SelectPengajuan = model.SelectPengajuan ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectPengajuan = SelectPengajuan;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetDocTypeListCount(Keyword, SelectDivisi, SelectPengajuan, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetDocTypeList(null, Keyword, SelectDivisi, SelectPengajuan, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["DOCTPEMListFilter"] = modFilter;
                    TempData["DOCTPEMList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian Data : Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/DocType/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["DOCTPEMListFilter"] = modFilter;
                    TempData["DOCTPEMList"] = master;
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

        #endregion Master DocType

        #region Master Branch

        [HttpPost]
        public async Task<ActionResult> clnMTDBRCH(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;
                bool IsPusat = false;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetCabangListCount(Keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetCabangList(null, Keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlRegion == null)
                {
                    Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("1", "", caption, UserID, GroupName);
                }

                if (Common.ddlPic == null)
                {
                    Common.ddlPic = await Commonddl.dbdbGetDdlPICListByEncrypt("1", "2", caption, UserID, GroupName);
                }

                ViewData["SelectPic"] = OwinLibrary.Get_SelectListItem(Common.ddlPic);
                ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                //set session filterisasi //
                TempData["BRCHMList"] = master;
                TempData["BRCHMListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDBRCH";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Cabang/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDBRCH(int paged)
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
                modFilter = TempData["BRCHMListFilter"] as cFilterMasterData;
                master = TempData["BRCHMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetCabangList(master.DTFromDB, keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["BRCHMList"] = master;
                TempData["BRCHMListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Cabang/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDBRCH(string paramkey, string oprfun)
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
                modFilter = TempData["BRCHMListFilter"] as cFilterMasterData;
                master = TempData["BRCHMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                ViewData["SelectPic"] = new MultiSelectList(Common.ddlPic, "Value", "Text");

                string Opr4view = "add";
                cMasterDataBranch modeldata = new cMasterDataBranch();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.BRCH_ID = dr["Id"].ToString();
                    modeldata.REGION = dr["REGION_ID"].ToString();
                    modeldata.BRCH_NAME = dr["BRCH_NAME"].ToString();
                    modeldata.BRCH_CODE = dr["BRCH_CODE"].ToString();
                    modeldata.AREA_NAME = dr["REGION_NAME"].ToString();
                    modeldata.PIC = dr["PicSelect"].ToString().Split(',');
                    modeldata.IsPusat = bool.Parse(dr["IsPusat"].ToString());
                    modeldata.IsActive = bool.Parse(dr["IsActive"].ToString());
                    modeldata.PicSelect = dr["PicSelect"].ToString();
                    //modeldata.hdPicSelect = modeldata.PicSelect;
                    //if (modeldata.PicSelect != "")
                    //{
                    //  modeldata.PicSelect = modeldata.PicSelect.Remove(modeldata.PicSelect.Length - 1, 1);
                    //}
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["BRCHMList"] = master;
                TempData["BRCHMListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Cabang/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDBRCH(cMasterDataBranch model)
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
                master = TempData["BRCHMList"] as vmMasterData;
                modFilter = TempData["BRCHMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["BRCHMList"] = master;
                TempData["BRCHMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                string Klien = HasKeyProtect.Decryption(model.CLNT_ID);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BRCH_ID).All(char.IsNumber))
                //{
                string picselect = (model.hdPicSelect ?? "").Replace(",", "|");
                result = await MasterDataddl.dbupdateCabang(ID, model.REGION, model.BRCH_ID, model.BRCH_CODE, model.BRCH_NAME, picselect, model.IsPusat, model.IsActive, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Cabang ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetCabangListCount(keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetCabangList(null, keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["BRCHMList"] = master;
                    TempData["BRCHMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Cabang/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDBRCH(string paramkey)
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
                master = TempData["BRCHMList"] as vmMasterData;
                modFilter = TempData["BRCHMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["BRCHMList"] = master;
                TempData["BRCHMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteCabang(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Cabang", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetCabangListCount(keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetCabangList(null, keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["BRCHMList"] = master;
                    TempData["BRCHMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Cabang/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDBRCH()
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
                modFilter = TempData["BRCHMListFilter"] as cFilterMasterData;
                master = TempData["BRCHMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["BRCHMList"] = master;
                TempData["BRCHMListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Cabang/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDBRCH(cFilterMasterData model)
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
                modFilter = TempData["BRCHMListFilter"] as cFilterMasterData;
                master = TempData["BRCHMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;
                bool IsPusat = model.IsPusatData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetCabangListCount(Keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetCabangList(null, Keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["BRCHMListFilter"] = modFilter;
                    TempData["BRCHMList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Cabang/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["BRCHMListFilter"] = modFilter;
                    TempData["BRCHMList"] = master;
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

        #endregion Master Branch

        #region Master Region/Area

        [HttpPost]
        public async Task<ActionResult> clnMTDRGN(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;
                bool IsPusat = false;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetRegionListCount(Keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetRegionList(null, Keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                //if (Common.ddlRegion == null)
                //{
                //    Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("1", "", caption, UserID, GroupName);
                //}
                //ViewData["SelectKlien"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                //set session filterisasi //
                TempData["RGNMList"] = master;
                TempData["RGNMListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDRGN";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Region/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDRGN(int paged)
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
                modFilter = TempData["RGNMListFilter"] as cFilterMasterData;
                master = TempData["RGNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetRegionList(master.DTFromDB, keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["RGNMList"] = master;
                TempData["RGNMListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Region/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDRGN(string paramkey, string oprfun)
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
                modFilter = TempData["RGNMListFilter"] as cFilterMasterData;
                master = TempData["RGNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                string Opr4view = "add";
                cMasterDataArea modeldata = new cMasterDataArea();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.REGION = dr["Id"].ToString();
                    modeldata.IsActive = bool.Parse(dr["IsActive"].ToString());
                    modeldata.IsPusat = bool.Parse(dr["IsPusat"].ToString());
                    modeldata.REGION_NAME = dr["REGION_NAME"].ToString();
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["RGNMList"] = master;
                TempData["RGNMListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Region/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDRGN(cMasterDataArea model)
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
                master = TempData["RGNMList"] as vmMasterData;
                modFilter = TempData["RGNMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["RGNMList"] = master;
                TempData["RGNMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                //string Klien = HasKeyProtect.Decryption(model.CLNT_ID);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.REGION).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateRegion(ID, ID.ToString(), model.REGION_NAME, model.IsPusat, model.IsActive, caption, UserID, GroupName);
                // }
                //else
                //{
                //  EnumMessage = "Isikan Kode Area dengan Angka";
                //}
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Area ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetRegionListCount(keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetRegionList(null, keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["RGNMList"] = master;
                    TempData["RGNMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Region/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDRGN(string paramkey)
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
                master = TempData["RGNMList"] as vmMasterData;
                modFilter = TempData["RGNMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["RGNMList"] = master;
                TempData["RGNMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteRegion(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Area", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetRegionListCount(keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetRegionList(null, keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["RGNMList"] = master;
                    TempData["RGNMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Region/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDRGN()
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
                modFilter = TempData["RGNMListFilter"] as cFilterMasterData;
                master = TempData["RGNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                TempData["RGNMList"] = master;
                TempData["RGNMListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    opsi13 = IsActive,
                    opsi14 = IsPusat,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Region/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDRGN(cFilterMasterData model)
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
                modFilter = TempData["RGNMListFilter"] as cFilterMasterData;
                master = TempData["RGNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;
                bool IsPusat = model.IsPusatData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetRegionListCount(Keyword, IsActive, IsPusat, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetRegionList(null, Keyword, IsActive, IsPusat, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["RGNMListFilter"] = modFilter;
                    TempData["RGNMList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Region/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["RGNMListFilter"] = modFilter;
                    TempData["RGNMList"] = master;
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

        #endregion Master Region/Area

        #region Master Bank PPAT

        [HttpPost]
        public async Task<ActionResult> clnMTDBANKNOT(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetBankNotListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetBankNotList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlnotaris == null)
                {
                    Common.ddlnotaris = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "NOTARIS", caption, UserID, GroupName);
                }

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);

                //set session filterisasi //
                TempData["BANKMNOTList"] = master;
                TempData["BANKMNOTListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDBANKNOT";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/BankNot/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDBANKNOT(int paged)
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
                modFilter = TempData["BANKMNOTListFilter"] as cFilterMasterData;
                master = TempData["BANKMNOTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetBankNotList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["BANKMNOTList"] = master;
                TempData["BANKMNOTListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/BankNot/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDBANKNOT(string paramkey, string oprfun)
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
                modFilter = TempData["BANKMNOTListFilter"] as cFilterMasterData;
                master = TempData["BANKMNOTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectNotaris"] = new MultiSelectList(Common.ddlnotaris, "Value", "Text");

                string Opr4view = "add";
                cMasterDataBank modeldata = new cMasterDataBank();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.ID = dr["id"].ToString();
                    modeldata.SelectNotaris = dr["UserID"].ToString();
                    modeldata.UserName = dr["UserName"].ToString();
                    modeldata.PemilikRekening = dr["PemilikRekening"].ToString();
                    modeldata.NoRekening = dr["NoRekening"].ToString();
                    modeldata.NamaBank = dr["NamaBank"].ToString();
                    modeldata.CabangBank = dr["CabangBank"].ToString();
                    modeldata.KodeInvoice = dr["InitCodeINV"].ToString();
                    modeldata.PRC_CHECK = Opr4view == "view" ? double.Parse(dr["PRC_CHECK"].ToString()).ToString("N0") : dr["PRC_CHECK"].ToString();
                    modeldata.PRC_SKMHT = Opr4view == "view" ? double.Parse(dr["PRC_SKMHT"].ToString()).ToString("N0") : dr["PRC_SKMHT"].ToString();
                    modeldata.PRC_APHT = Opr4view == "view" ? double.Parse(dr["PRC_APHT"].ToString()).ToString("N0") : dr["PRC_APHT"].ToString();
                    modeldata.PRC_CNLBAKD = Opr4view == "view" ? double.Parse(dr["PRC_CNLBAKD"].ToString()).ToString("N0") : dr["PRC_CNLBAKD"].ToString();
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["BANKMNOTList"] = master;
                TempData["BANKMNOTListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/BankNot/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDBANKNOT(cMasterDataBank model)
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
                master = TempData["BANKMNOTList"] as vmMasterData;
                modFilter = TempData["BANKMNOTListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["BANKMNOTList"] = master;
                TempData["BANKMNOTListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BANK_ID).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateBankNOT(ID, model.SelectNotaris, model.PemilikRekening, model.NoRekening, model.NamaBank, model.CabangBank, model.KodeInvoice, model.PRC_CHECK, model.PRC_SKMHT,
                                                            model.PRC_APHT, model.PRC_CNLBAKD, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Bank ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetBankNotListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetBankNotList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["BANKMNOTList"] = master;
                    TempData["BANKMNOTListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/BankNot/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDBANKNOT(string paramkey)
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
                master = TempData["BANKMNOTList"] as vmMasterData;
                modFilter = TempData["BANKMNOTListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["BANKMNOTList"] = master;
                TempData["BANKMNOTListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteBankNot(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Bank", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetBankNotListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetBankNotList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["BANKMNOTList"] = master;
                    TempData["BANKMNOTListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/BankNot/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDBANKNOT()
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
                modFilter = TempData["BANKMNOTListFilter"] as cFilterMasterData;
                master = TempData["BANKMNOTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["BANKMNOTList"] = master;
                TempData["BANKMNOTListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/BankNot/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDBANKNOT(cFilterMasterData model)
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
                modFilter = TempData["BANKMNOTListFilter"] as cFilterMasterData;
                master = TempData["BANKMNOTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetBankNotListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetBankNotList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["BANKMNOTListFilter"] = modFilter;
                    TempData["BANKMNOTList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/BankNot/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["BANKMNOTListFilter"] = modFilter;
                    TempData["BANKMNOTList"] = master;
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

        #endregion Master Bank PPAT

        #region Master Jadwal INV PPAT

        [HttpPost]
        public async Task<ActionResult> clnMTDSCHINV(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetSchInvNotListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetSchInvNotList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlnotaris == null)
                {
                    Common.ddlnotaris = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "NOTARIS", caption, UserID, GroupName);
                }

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);

                //set session filterisasi //
                TempData["MTDSCHINVList"] = master;
                TempData["MTDSCHINVListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDSchInvNot";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SchInvNot/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDSCHINV(int paged)
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
                modFilter = TempData["MTDSCHINVListFilter"] as cFilterMasterData;
                master = TempData["MTDSCHINVList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetSchInvNotList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["MTDSCHINVList"] = master;
                TempData["MTDSCHINVListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SchInvNot/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDSCHINV(string paramkey, string oprfun)
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
                modFilter = TempData["MTDSCHINVListFilter"] as cFilterMasterData;
                master = TempData["MTDSCHINVList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectNotaris"] = new MultiSelectList(Common.ddlnotaris, "Value", "Text");

                string Opr4view = "add";
                cMasterSchINVPPAT modeldata = new cMasterSchINVPPAT();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.ID = dr["id"].ToString();
                    modeldata.PPATID = dr["PPATid"].ToString();
                    modeldata.TglINV = dr["tglinv"].ToString();
                    modeldata.UserName = dr["UserName"].ToString();
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["MTDSCHINVList"] = master;
                TempData["MTDSCHINVListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SchInvNot/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDSCHINV(cMasterSchINVPPAT model)
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
                master = TempData["MTDSCHINVList"] as vmMasterData;
                modFilter = TempData["MTDSCHINVListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["MTDSCHINVList"] = master;
                TempData["MTDSCHINVListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BANK_ID).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateSchInvNot(ID, model.PPATID, model.TglINV, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetSchInvNotListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetSchInvNotList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["MTDSCHINVList"] = master;
                    TempData["MTDSCHINVListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SchInvNot/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDSCHINV(string paramkey)
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
                master = TempData["MTDSCHINVList"] as vmMasterData;
                modFilter = TempData["MTDSCHINVListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["MTDSCHINVList"] = master;
                TempData["MTDSCHINVListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteSchInvNot(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetSchInvNotListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetSchInvNotList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["MTDSCHINVList"] = master;
                    TempData["MTDSCHINVListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SchInvNot/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDSCHINV()
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
                modFilter = TempData["MTDSCHINVListFilter"] as cFilterMasterData;
                master = TempData["MTDSCHINVList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["MTDSCHINVList"] = master;
                TempData["MTDSCHINVListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SchInvNot/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDSCHINV(cFilterMasterData model)
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
                modFilter = TempData["MTDSCHINVListFilter"] as cFilterMasterData;
                master = TempData["MTDSCHINVList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetSchInvNotListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetSchInvNotList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["MTDSCHINVListFilter"] = modFilter;
                    TempData["MTDSCHINVList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SchInvNot/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["MTDSCHINVListFilter"] = modFilter;
                    TempData["MTDSCHINVList"] = master;
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

        #endregion Master Jadwal INV PPAT

        #region Master Wilayah PPAT

        [HttpPost]
        public async Task<ActionResult> clnMTDRGNNOT(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetNotRgnListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetNotRgnList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlnotaris == null)
                {
                    Common.ddlnotaris = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "NOTARIS", caption, UserID, GroupName);
                }

                if (Common.ddlnotarisRgn == null)
                {
                    Common.ddlnotarisRgn = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "NOTARISRGN", caption, UserID, GroupName);
                }

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);
                ViewData["SelectNotarisRgn"] = OwinLibrary.Get_SelectListItem(Common.ddlnotarisRgn);

                //set session filterisasi //
                TempData["NOTRGNList"] = master;
                TempData["NOTRGNListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDNOTRGN";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotRegion/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDRGNNOT(int paged)
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
                modFilter = TempData["NOTRGNListFilter"] as cFilterMasterData;
                master = TempData["NOTRGNList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetNotRgnList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["NOTRGNList"] = master;
                TempData["NOTRGNListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotRegion/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDRGNNOT(string paramkey, string oprfun)
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
                modFilter = TempData["NOTRGNListFilter"] as cFilterMasterData;
                master = TempData["NOTRGNList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectNotaris"] = new MultiSelectList(Common.ddlnotaris, "Value", "Text");
                ViewData["SelectNotarisRgn"] = new MultiSelectList(Common.ddlnotarisRgn, "Value", "Text");

                string Opr4view = "add";
                cMasterDataWilayahPPAT modeldata = new cMasterDataWilayahPPAT();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.ID = dr["id"].ToString();
                    modeldata.SelectNotaris = dr["UserID"].ToString();
                    modeldata.UserName = dr["UserName"].ToString();
                    modeldata.Wilayah = dr["wilayah"].ToString();
                    modeldata.IsActiveData = bool.Parse(dr["IsActiveData"].ToString());
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["NOTRGNList"] = master;
                TempData["NOTRGNListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotRegion/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDRGNNOT(cMasterDataWilayahPPAT model)
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
                master = TempData["NOTRGNList"] as vmMasterData;
                modFilter = TempData["NOTRGNListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["NOTRGNList"] = master;
                TempData["NOTRGNListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BANK_ID).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateNOTRgn(ID, model.SelectNotaris, model.SelectNotarisRgn, model.IsActiveData, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetNotRgnListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetNotRgnList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["NOTRGNList"] = master;
                    TempData["NOTRGNListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);
                ViewData["SelectNotarisRgn"] = OwinLibrary.Get_SelectListItem(Common.ddlnotarisRgn);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotRegion/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDRGNNOT(string paramkey)
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
                master = TempData["NOTRGNList"] as vmMasterData;
                modFilter = TempData["NOTRGNListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["NOTRGNList"] = master;
                TempData["NOTRGNListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteNotRgn(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetNotRgnListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetNotRgnList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["NOTRGNList"] = master;
                    TempData["NOTRGNListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotRegion/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDRGNNOT()
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
                modFilter = TempData["NOTRGNListFilter"] as cFilterMasterData;
                master = TempData["NOTRGNList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["NOTRGNList"] = master;
                TempData["NOTRGNListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotRegion/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDRGNNOT(cFilterMasterData model)
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
                modFilter = TempData["NOTRGNListFilter"] as cFilterMasterData;
                master = TempData["NOTRGNList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetNotRgnListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetNotRgnList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["NOTRGNListFilter"] = modFilter;
                    TempData["NOTRGNList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/NotRegion/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["NOTRGNListFilter"] = modFilter;
                    TempData["NOTRGNList"] = master;
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

        #endregion Master Wilayah PPAT

        #region Master Comment History

        [HttpPost]
        public async Task<ActionResult> clnMTDLOGCOMMENT(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetLogCommentListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetLogCommentList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                //set session filterisasi //
                TempData["LOGCOMMENTList"] = master;
                TempData["LOGCOMMENTListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDLOGCOMMENT";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/History/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDLOGCOMMENT(int paged)
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
                modFilter = TempData["LOGCOMMENTListFilter"] as cFilterMasterData;
                master = TempData["LOGCOMMENTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetLogCommentList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["LOGCOMMENTList"] = master;
                TempData["LOGCOMMENTListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/History/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDLOGCOMMENT(string paramkey, string oprfun)
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
                modFilter = TempData["LOGCOMMENTListFilter"] as cFilterMasterData;
                master = TempData["LOGCOMMENTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string captionnoc = HasKeyProtect.Decryption(modFilter.idcaption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string Opr4view = "add";
                cMasterLogHIstory modeldata = new cMasterLogHIstory();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }
                    modeldata.ID = dr["ID"].ToString();
                    modFilter.keylookupdata = paramkey;

                    Common.ddlstatus = await Commonddl.dbdbGetDdlEnumsListByEncryptNw("STATHDL", captionnoc, UserID, GroupName, "9999");
                    ViewData["SelectStatus"] = new MultiSelectList(Common.ddlstatus, "Value", "Text");
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["LOGCOMMENTList"] = master;
                TempData["LOGCOMMENTListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/History/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDLOGCOMMENT(cMasterLogHIstory model)
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
                master = TempData["LOGCOMMENTList"] as vmMasterData;
                modFilter = TempData["LOGCOMMENTListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["LOGCOMMENTList"] = master;
                TempData["LOGCOMMENTListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BANK_ID).All(char.IsNumber))
                //{
                string statused = HasKeyProtect.Decryption(model.SelectDivisi);
                result = await MasterDataddl.dbupdateLogComment(ID, statused, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetLogCommentListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetLogCommentList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["LOGCOMMENTList"] = master;
                    TempData["LOGCOMMENTListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);
                ViewData["SelectNotarisRgn"] = OwinLibrary.Get_SelectListItem(Common.ddlnotarisRgn);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/History/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDLOGCOMMENT(string paramkey)
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
                master = TempData["LOGCOMMENTList"] as vmMasterData;
                modFilter = TempData["LOGCOMMENTListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["LOGCOMMENTList"] = master;
                TempData["LOGCOMMENTListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteLogComment(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetLogCommentListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetLogCommentList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["LOGCOMMENTList"] = master;
                    TempData["LOGCOMMENTListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/History/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDLOGCOMMENT()
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
                modFilter = TempData["LOGCOMMENTListFilter"] as cFilterMasterData;
                master = TempData["LOGCOMMENTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["LOGCOMMENTList"] = master;
                TempData["LOGCOMMENTListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/History/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDLOGCOMMENT(cFilterMasterData model)
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
                modFilter = TempData["LOGCOMMENTListFilter"] as cFilterMasterData;
                master = TempData["LOGCOMMENTList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetLogCommentListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetLogCommentList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["LOGCOMMENTListFilter"] = modFilter;
                    TempData["LOGCOMMENTList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/History/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["LOGCOMMENTListFilter"] = modFilter;
                    TempData["LOGCOMMENTList"] = master;
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

        #endregion Master Comment History

        #region Master Account grouop

        [HttpPost]
        public async Task<ActionResult> clnMTDACCTGRP(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetAcctGrpListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetAcctGrpList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlUserID == null)
                {
                    Common.ddlUserID = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "USR", caption, UserID, GroupName);
                }

                if (Common.ddlGrupUser == null)
                {
                    Common.ddlGrupUser = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "GRP", caption, UserID, GroupName);
                }

                ViewData["SelectUsr"] = OwinLibrary.Get_SelectListItem(Common.ddlUserID);
                ViewData["SelectGrp"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupUser);

                //set session filterisasi //
                TempData["ACCTGRPList"] = master;
                TempData["ACCTGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDACCTGRP";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/AcctGrp/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDACCTGRP(int paged)
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
                modFilter = TempData["ACCTGRPListFilter"] as cFilterMasterData;
                master = TempData["ACCTGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetAcctGrpList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["ACCTGRPList"] = master;
                TempData["ACCTGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/AcctGrp/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDACCTGRP(string paramkey, string oprfun)
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
                modFilter = TempData["ACCTGRPListFilter"] as cFilterMasterData;
                master = TempData["ACCTGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectUsr"] = new MultiSelectList(Common.ddlUserID, "Value", "Text");
                ViewData["SelectGrp"] = new MultiSelectList(Common.ddlGrupUser, "Value", "Text");

                string Opr4view = "add";
                cMasterDataAcctGroup modeldata = new cMasterDataAcctGroup();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.ID = dr["id"].ToString();
                    modeldata.UserGroup = dr["UserID"].ToString();
                    modeldata.UserName = dr["UserName"].ToString();
                    modeldata.NamaGroup = dr["GroupName"].ToString();
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["ACCTGRPList"] = master;
                TempData["ACCTGRPListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/AcctGrp/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDACCTGRP(cMasterDataAcctGroup model)
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
                master = TempData["ACCTGRPList"] as vmMasterData;
                modFilter = TempData["ACCTGRPListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["ACCTGRPList"] = master;
                TempData["ACCTGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BANK_ID).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateAcctGrp(ID, model.UserGroup, model.NamaGroup, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetAcctGrpListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetAcctGrpList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["ACCTGRPList"] = master;
                    TempData["ACCTGRPListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectUsr"] = OwinLibrary.Get_SelectListItem(Common.ddlUserID);
                ViewData["SelectGrp"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupUser);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/AcctGrp/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDACCTGRP(string paramkey)
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
                master = TempData["ACCTGRPList"] as vmMasterData;
                modFilter = TempData["ACCTGRPListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["ACCTGRPList"] = master;
                TempData["ACCTGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteAcctGrp(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetAcctGrpListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetAcctGrpList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["ACCTGRPList"] = master;
                    TempData["ACCTGRPListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/AcctGrp/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDACCTGRP()
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
                modFilter = TempData["ACCTGRPListFilter"] as cFilterMasterData;
                master = TempData["ACCTGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["ACCTGRPList"] = master;
                TempData["ACCTGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/AcctGrp/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDACCTGRP(cFilterMasterData model)
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
                modFilter = TempData["ACCTGRPListFilter"] as cFilterMasterData;
                master = TempData["ACCTGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetAcctGrpListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetAcctGrpList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["ACCTGRPListFilter"] = modFilter;
                    TempData["ACCTGRPList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/AcctGrp/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["ACCTGRPListFilter"] = modFilter;
                    TempData["ACCTGRPList"] = master;
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

        #endregion Master Account grouop

        #region Master user cabang

        [HttpPost]
        public async Task<ActionResult> clnMTDCABGRP(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetAccCabangListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetAccCabangList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                if (Common.ddlUserID == null)
                {
                    Common.ddlUserID = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "USRCAB", caption, UserID, GroupName);
                }

                if (Common.ddlGrupUser == null)
                {
                    Common.ddlGrupUser = await Commonddl.dbdbGetDdlEnumListByEncrypt("", "CAB", caption, UserID, GroupName);
                }

                ViewData["SelectUsr"] = OwinLibrary.Get_SelectListItem(Common.ddlUserID);
                ViewData["SelectGrp"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupUser);

                //set session filterisasi //
                TempData["CABGRPList"] = master;
                TempData["CABGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDCABGRP";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CabangUser/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDCABGRP(int paged)
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
                modFilter = TempData["CABGRPListFilter"] as cFilterMasterData;
                master = TempData["CABGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetAcctGrpList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["CABGRPList"] = master;
                TempData["CABGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CabangUser/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDCABGRP(string paramkey, string oprfun)
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
                modFilter = TempData["CABGRPListFilter"] as cFilterMasterData;
                master = TempData["CABGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                ViewData["SelectUsr"] = new MultiSelectList(Common.ddlUserID, "Value", "Text");
                ViewData["SelectGrp"] = new MultiSelectList(Common.ddlGrupUser, "Value", "Text");

                string Opr4view = "add";
                cMasterCabangUser modeldata = new cMasterCabangUser();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.ID = dr["id"].ToString();
                    modeldata.mainUserid = dr["MainUserID"].ToString();
                    modeldata.branchid = dr["Brch_ID"].ToString();
                    modeldata.SelectBranch = dr["Wilayah"].ToString();
                    modeldata.IsActiveData = bool.Parse(dr["IsActiveData"].ToString());

                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["CABGRPList"] = master;
                TempData["CABGRPListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CabangUser/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDCABGRP(cMasterCabangUser model)
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
                master = TempData["CABGRPList"] as vmMasterData;
                modFilter = TempData["CABGRPListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["CABGRPList"] = master;
                TempData["CABGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BANK_ID).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateAccCabang(ID, model.mainUserid, model.branchid, model.IsActiveData, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetAccCabangListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetAccCabangList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["CABGRPList"] = master;
                    TempData["CABGRPListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectUsr"] = OwinLibrary.Get_SelectListItem(Common.ddlUserID);
                ViewData["SelectGrp"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupUser);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CabangUser/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDCABGRP(string paramkey)
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
                master = TempData["CABGRPList"] as vmMasterData;
                modFilter = TempData["CABGRPListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["CABGRPList"] = master;
                TempData["CABGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteAccCabang(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data ", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetAccCabangListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetAccCabangList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["CABGRPList"] = master;
                    TempData["CABGRPListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CabangUser/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDCABGRP()
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
                modFilter = TempData["CABGRPListFilter"] as cFilterMasterData;
                master = TempData["CABGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["CABGRPList"] = master;
                TempData["CABGRPListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CabangUser/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDCABGRP(cFilterMasterData model)
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
                modFilter = TempData["CABGRPListFilter"] as cFilterMasterData;
                master = TempData["CABGRPList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetAccCabangListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetAccCabangList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["CABGRPListFilter"] = modFilter;
                    TempData["CABGRPList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CabangUser/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["CABGRPListFilter"] = modFilter;
                    TempData["CABGRPList"] = master;
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

        #endregion Master user cabang

        //#region Master Bank

        //[HttpPost]
        //public async Task<ActionResult> clnMTDBANK(String menu, String caption)
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
        //        bool IsActive = true;

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
        //        modFilter.IsActiveData = IsActive;
        //        modFilter.PageNumber = PageNumber;

        //        // try show filter data//
        //        List<String> recordPage = await MasterDataddl.dbGetBankListCount(Keyword, IsActive, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataddl.dbGetBankList(null, Keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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

        //        //set session filterisasi //
        //        TempData["BANKMList"] = master;
        //        TempData["BANKMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDBANK";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString();

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Bank/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDBANK(int paged)
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
        //        modFilter = TempData["BANKMListFilter"] as cFilterMasterData;
        //        master = TempData["BANKMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        // get value filter have been filter//
        //        string keyword = modFilter.keyword;
        //        bool IsActive = modFilter.IsActiveData;

        //        // set & get for next paging //
        //        int pagenumberclient = paged;
        //        int PageNumber = modFilter.PageNumber;
        //        double pagingsizeclient = modFilter.pagingsizeclient;
        //        double TotalRecord = modFilter.TotalRecord;
        //        double totalRecordclient = modFilter.totalRecordclient;

        //        //descript some value for db//
        //        caption = HasKeyProtect.Decryption(caption);

        //        // try show filter data//
        //        List<DataTable> dtlist = await MasterDataddl.dbGetBankList(master.DTFromDB, keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        bool isModeFilter = modFilter.isModeFilter;
        //        string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

        //        //set session filterisasi //
        //        TempData["BANKMList"] = master;
        //        TempData["BANKMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Bank/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDBANK(string paramkey, string oprfun)
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
        //        modFilter = TempData["BANKMListFilter"] as cFilterMasterData;
        //        master = TempData["BANKMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string Opr4view = "add";
        //        cMasterDataBank modeldata = new cMasterDataBank();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            Opr4view = "edit";
        //            if (oprfun == "x4vw")
        //            {
        //                Opr4view = "view";
        //            }

        //            modeldata.ID = dr["id"].ToString();
        //            modeldata.BANK_ID = dr["BANK_ID"].ToString();
        //            modeldata.BANK_NAME = dr["BANK_NAME"].ToString();
        //            modeldata.BI_CODE = "";
        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
        //        ViewBag.oprvalue = Opr4view;
        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["BANKMList"] = master;
        //        TempData["BANKMListFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Bank/_uiUpdateMasterData.cshtml", modeldata),
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

        //public async Task<ActionResult> clnUpdMasterMTDBANK(cMasterDataBank model)
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
        //        master = TempData["BANKMList"] as vmMasterData;
        //        modFilter = TempData["BANKMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["BANKMList"] = master;
        //        TempData["BANKMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        bool IsActive = modFilter.IsActiveData;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.IsActiveData = IsActive;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        string EnumMessage = "";
        //        int result = -1;
        //        //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
        //        //{
        //        //    EnumMessage = "Region Harus diawali kata 'REG'";
        //        //}
        //        //else
        //        //{
        //        //if ((model.BANK_ID).All(char.IsNumber))
        //        //{
        //        result = await MasterDataddl.dbupdateBank(ID, model.BANK_ID, model.BANK_NAME, model.BI_CODE, model.IsActive, caption, UserID, GroupName);
        //        //}
        //        //else
        //        //{
        //        //  EnumMessage = "Isikan Kode Cabang dengan Angka";
        //        // }
        //        //}

        //        if (EnumMessage == "")
        //        {
        //            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //            EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Bank ", "disimpan") : EnumMessage;
        //        }

        //        if (result == 1)
        //        {
        //            ////get total data from server//
        //            List<String> recordPage = await MasterDataddl.dbGetBankListCount(keyword, IsActive, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataddl.dbGetBankList(null, keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["BANKMList"] = master;
        //            TempData["BANKMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

        //        ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Bank/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clndelAddMTDBANK(string paramkey)
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
        //        master = TempData["BANKMList"] as vmMasterData;
        //        modFilter = TempData["BANKMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["BANKMList"] = master;
        //        TempData["BANKMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        bool IsActive = modFilter.IsActiveData;

        //        string keylookupdata = paramkey;

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
        //        modFilter.keyword = keyword;
        //        modFilter.IsActiveData = IsActive;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataddl.dbdeleteBank(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Bank", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            // try show filter data//
        //            List<String> recordPage = await MasterDataddl.dbGetBankListCount(keyword, IsActive, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataddl.dbGetBankList(null, keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["BANKMList"] = master;
        //            TempData["BANKMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Bank/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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

        //public async Task<ActionResult> clnOpenFilterMTDBANK()
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
        //        modFilter = TempData["BANKMListFilter"] as cFilterMasterData;
        //        master = TempData["BANKMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword ?? "";
        //        bool IsActive = modFilter.IsActiveData;

        //        TempData["BANKMList"] = master;
        //        TempData["BANKMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            opsi1 = Keyword,
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Bank/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDBANK(cFilterMasterData model)
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
        //        modFilter = TempData["BANKMListFilter"] as cFilterMasterData;
        //        master = TempData["BANKMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";
        //        bool IsActive = model.IsActiveData;

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
        //        modFilter.keyword = Keyword;
        //        modFilter.IsActiveData = IsActive;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataddl.dbGetBankListCount(Keyword, IsActive, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataddl.dbGetBankList(null, Keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["BANKMListFilter"] = modFilter;
        //            TempData["BANKMList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Bank/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["BANKMListFilter"] = modFilter;
        //            TempData["BANKMList"] = master;
        //            TempData["common"] = Common;
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
        //#endregion Master bank

        #region Master Pendidikan

        [HttpPost]
        public async Task<ActionResult> clnMTDEDUC(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGeteducListCount(Keyword, IsActive, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGeteducList(null, Keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                //set session filterisasi //
                TempData["EDUCMList"] = master;
                TempData["EDUCMListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDEDUC";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Pendidikan/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDEDUC(int paged)
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
                modFilter = TempData["EDUCMListFilter"] as cFilterMasterData;
                master = TempData["EDUCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGeteducList(master.DTFromDB, keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["EDUCMList"] = master;
                TempData["EDUCMListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Pendidikan/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDEDUC(string paramkey, string oprfun)
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
                modFilter = TempData["EDUCMListFilter"] as cFilterMasterData;
                master = TempData["EDUCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string Opr4view = "add";
                cMasterDataPendidikan modeldata = new cMasterDataPendidikan();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.ID = dr["id"].ToString();
                    modeldata.educ_name = dr["Pendidikan"].ToString();
                    modeldata.IsActive = bool.Parse(dr["IsActive"].ToString());
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["EDUCMList"] = master;
                TempData["EDUCMListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Pendidikan/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDEDUC(cMasterDataPendidikan model)
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
                master = TempData["EDUCMList"] as vmMasterData;
                modFilter = TempData["EDUCMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["EDUCMList"] = master;
                TempData["EDUCMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.BANK_ID).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateeduc(ID, model.educ_name, model.IsActive, caption, UserID, GroupName);
                //}
                //else
                //{
                //  EnumMessage = "Isikan Kode Cabang dengan Angka";
                // }
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Bank ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGeteducListCount(keyword, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGeteducList(null, keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["EDUCMList"] = master;
                    TempData["EDUCMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Pendidikan/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDEDUC(string paramkey)
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
                master = TempData["EDUCMList"] as vmMasterData;
                modFilter = TempData["EDUCMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["EDUCMList"] = master;
                TempData["EDUCMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteeduc(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Bank", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGeteducListCount(keyword, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGeteducList(null, keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["EDUCMList"] = master;
                    TempData["EDUCMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Pendidikan/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDEDUC()
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
                modFilter = TempData["EDUCMListFilter"] as cFilterMasterData;
                master = TempData["EDUCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;

                TempData["EDUCMList"] = master;
                TempData["EDUCMListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Pendidikan/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDEDUC(cFilterMasterData model)
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
                modFilter = TempData["EDUCMListFilter"] as cFilterMasterData;
                master = TempData["EDUCMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGeteducListCount(Keyword, IsActive, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGeteducList(null, Keyword, IsActive, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["EDUCMListFilter"] = modFilter;
                    TempData["EDUCMList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Pendidikan/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["EDUCMListFilter"] = modFilter;
                    TempData["EDUCMList"] = master;
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

        #endregion Master Pendidikan

        #region InfoText

        [HttpPost]
        public async Task<ActionResult> clnMTDINFO(String menu, String caption)
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
                string Keyword = "";
                bool IsActive = true;
                bool IsPusat = false;

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
                modFilter.GroupName = GroupName;
                modFilter.MailerDaemoon = Mailed;
                modFilter.GenDeamoon = GenMoon;
                modFilter.UserType = int.Parse(UserTypes);
                modFilter.UserTypeApps = int.Parse(UserTypes);
                modFilter.idcaption = idcaption;
                modFilter.Menu = menuitemdescription;
                modFilter.ModuleName = HasKeyProtect.Decryption(idcaption);
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;
                modFilter.PageNumber = PageNumber;

                // try show filter data//
                List<String> recordPage = await MasterDataddl.dbGetInfoTextListCount(Keyword, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await MasterDataddl.dbGetInfoTextList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                master.DTFromDB = dtlist[0];
                master.DTDetailForGrid = dtlist[1];
                master.MasterFilter = modFilter;

                //if (Common.ddlRegion == null)
                //{
                //    Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("1", "", caption, UserID, GroupName);
                //}
                //ViewData["SelectKlien"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                //set session filterisasi //
                TempData["INNMList"] = master;
                TempData["INNMListFilter"] = modFilter;
                TempData["common"] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "MasterData";
                ViewBag.action = "clnMTDINFO";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/InfoText/uiMasterData.cshtml", master),
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

        public async Task<ActionResult> clnRgridListMTDINFO(int paged)
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
                modFilter = TempData["INNMListFilter"] as cFilterMasterData;
                master = TempData["INNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await MasterDataddl.dbGetInfoTextList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                master.DTDetailForGrid = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData["INNMList"] = master;
                TempData["INNMListFilter"] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/InfoText/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenAddMTDINFO(string paramkey, string oprfun)
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
                modFilter = TempData["INNMListFilter"] as cFilterMasterData;
                master = TempData["INNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                string Opr4view = "add";
                cMasterDataInfoText modeldata = new cMasterDataInfoText();
                DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                if (dr != null)
                {
                    Opr4view = "edit";
                    if (oprfun == "x4vw")
                    {
                        Opr4view = "view";
                    }

                    modeldata.ID = dr["Id"].ToString();
                    modeldata.EndDate = dr["EndDate"].ToString();
                    modeldata.InfoText = dr["InfoText"].ToString();
                    modFilter.keylookupdata = paramkey;
                }

                ViewBag.OprMenu = (Opr4view == "add" ? "Penambahan Data" : Opr4view == "edit" ? " Perubahan Data" : "");
                ViewBag.oprvalue = Opr4view;
                ViewBag.Menu = modFilter.Menu;
                TempData["INNMList"] = master;
                TempData["INNMListFilter"] = modFilter;
                TempData["common"] = Common;
                // senback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = paramkey,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/InfoText/_uiUpdateMasterData.cshtml", modeldata),
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

        public async Task<ActionResult> clnUpdMasterMTDINFO(cMasterDataInfoText model)
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
                master = TempData["INNMList"] as vmMasterData;
                modFilter = TempData["INNMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["INNMList"] = master;
                TempData["INNMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                string keylookupdata = model.keylookup;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                modFilter.keylookupdata = keylookupdata;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                //string Klien = HasKeyProtect.Decryption(model.CLNT_ID);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                string EnumMessage = "";
                int result = -1;
                //if ((model.REGION ?? "") != "" && (model.REGION.Substring(0, 3) != "REG"))
                //{
                //    EnumMessage = "Region Harus diawali kata 'REG'";
                //}
                //else
                //{
                //if ((model.REGION).All(char.IsNumber))
                //{
                result = await MasterDataddl.dbupdateInfoText(ID, model.InfoText, model.EndDate, caption, UserID, GroupName);
                // }
                //else
                //{
                //  EnumMessage = "Isikan Kode Area dengan Angka";
                //}
                //}

                if (EnumMessage == "")
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Informasi  ", "disimpan") : EnumMessage;
                }

                if (result == 1)
                {
                    ////get total data from server//
                    List<String> recordPage = await MasterDataddl.dbGetInfoTextListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;

                    //set paging in grid client//
                    List<DataTable> dtlist = await MasterDataddl.dbGetInfoTextList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["INNMList"] = master;
                    TempData["INNMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/InfoText/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clndelAddMTDINFO(string paramkey)
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
                master = TempData["INNMList"] as vmMasterData;
                modFilter = TempData["INNMListFilter"] as cFilterMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["INNMList"] = master;
                TempData["INNMListFilter"] = modFilter;
                TempData["common"] = Common;

                //get value from aply filter //
                string keyword = modFilter.keyword;
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                string keylookupdata = paramkey;

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
                modFilter.keyword = keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                if ((keylookupdata ?? "") != "")
                {
                    dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
                    ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
                }

                int result = await MasterDataddl.dbdeleteRegion(ID, caption, UserID, GroupName);

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Area", "dihapus") : EnumMessage;

                if (result == 1)
                {
                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetInfoTextListCount(keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetInfoTextList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    TempData["INNMList"] = master;
                    TempData["INNMListFilter"] = modFilter;
                    TempData["common"] = Common;
                }

                string filteron = isModeFilter == false ? "" : ", Pencarian : Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/InfoText/_uiGridMasterDataList.cshtml", master),
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

        public async Task<ActionResult> clnOpenFilterMTDINFO()
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
                modFilter = TempData["INNMListFilter"] as cFilterMasterData;
                master = TempData["INNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string Keyword = modFilter.keyword ?? "";
                bool IsActive = modFilter.IsActiveData;
                bool IsPusat = modFilter.IsPusatData;

                TempData["INNMList"] = master;
                TempData["INNMListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    opsi1 = Keyword,
                    opsi13 = IsActive,
                    opsi14 = IsPusat,
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/InfoText/_uiFilterData.cshtml", master.MasterFilter),
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
        public async Task<ActionResult> clnListFilterMTDINFO(cFilterMasterData model)
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
                modFilter = TempData["INNMListFilter"] as cFilterMasterData;
                master = TempData["INNMList"] as vmMasterData;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string Keyword = model.keyword ?? "";
                bool IsActive = model.IsActiveData;
                bool IsPusat = model.IsPusatData;

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
                modFilter.keyword = Keyword;
                modFilter.IsActiveData = IsActive;
                modFilter.IsPusatData = IsPusat;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = "";
                if (validtxt == "")
                {
                    //descript some value for db//
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await MasterDataddl.dbGetInfoTextListCount(Keyword, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await MasterDataddl.dbGetInfoTextList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    master.DTFromDB = dtlist[0];
                    master.DTDetailForGrid = dtlist[1];
                    master.MasterFilter = modFilter;

                    //keep session filterisasi before//
                    TempData["INNMListFilter"] = modFilter;
                    TempData["INNMList"] = master;
                    TempData["common"] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/InfoText/_uiGridMasterDataList.cshtml", master),
                        download = "",
                        message = validtxt,
                    });
                }
                else
                {
                    TempData["INNMListFilter"] = modFilter;
                    TempData["INNMList"] = master;
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

        #endregion InfoText

        //#region Master Klien
        //[HttpPost]
        //public async Task<ActionResult> clnMTDCLNT(String menu, String caption)
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
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGetKlienListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetKlienList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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

        //        if (Common.ddlJenisPelanggan == null)
        //        {
        //            Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
        //        }

        //        ViewData["SelectTipeKlien"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);

        //        //set session filterisasi //
        //        TempData["CLNTMList"] = master;
        //        TempData["CLNTMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDCLNT";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Klien/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDCLNT(int paged)
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
        //        modFilter = TempData["CLNTMListFilter"] as cFilterMasterData;
        //        master = TempData["CLNTMList"] as vmMasterData;
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
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetKlienList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["CLNTMList"] = master;
        //        TempData["CLNTMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Klien/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDCLNT(string paramkey)
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
        //        modFilter = TempData["CLNTMListFilter"] as cFilterMasterData;
        //        master = TempData["CLNTMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        ViewData["SelectTipeKlien"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);

        //        cMasterDataKlien modeldata = new cMasterDataKlien();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.CLNT_ID = (dr["CLNT_ID"].ToString());
        //            modeldata.CLNT_NAME = dr["CLNT_NAME"].ToString();
        //            modeldata.CLNT_ADDRESS = dr["CLNT_ADDRESS"].ToString();
        //            modeldata.CLNT_TYPE = dr["CLNT_TYPE"].ToString();
        //            modeldata.CLNT_CONTACT_NO = dr["CLNT_CONTACT_NO"].ToString();

        //            modeldata.CLNT_NPWP_NIK_SK = dr["CLNT_NPWP_NIK_SK"].ToString();
        //            modeldata.CLNT_POSTCODE = dr["CLNT_POSTCODE"].ToString();
        //            modeldata.CLNT_PROVINCE = dr["CLNT_PROVINCE"].ToString();
        //            modeldata.CLNT_CITY = dr["CLNT_CITY"].ToString();

        //            modeldata.CLNT_SUBDISTRICT = dr["CLNT_SUBDISTRICT"].ToString();
        //            modeldata.CLNT_URBAN_VILLAGE = dr["CLNT_URBAN_VILLAGE"].ToString();
        //            modeldata.CLNT_HAMLET_NO = dr["CLNT_HAMLET_NO"].ToString();
        //            modeldata.CLNT_NEIGHBOORHOOD_NO = dr["CLNT_NEIGHBOORHOOD_NO"].ToString();

        //            modeldata.CLNT_PROVINCE_ID_AHU = dr["CLNT_PROVINCE_ID_AHU"].ToString();
        //            modeldata.CLNT_CITY_ID_AHU = dr["CLNT_CITY_ID_AHU"].ToString();

        //            modeldata.actived = bool.Parse(dr["Actived"].ToString());

        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["CLNTMList"] = master;
        //        TempData["CLNTMListFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Klien/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clnUpdMasterMTDCLNT(cMasterDataKlien model)
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
        //        master = TempData["CLNTMList"] as vmMasterData;
        //        modFilter = TempData["CLNTMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["CLNTMList"] = master;
        //        TempData["CLNTMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string Klien = HasKeyProtect.Decryption(model.CLNT_ID);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateKlien(ID, model.CLNT_ID, model.CLNT_NAME, model.CLNT_ADDRESS, model.CLNT_TYPE, model.CLNT_CONTACT_NO,
        //                                                                 model.CLNT_NPWP_NIK_SK, model.CLNT_POSTCODE, model.CLNT_PROVINCE, model.CLNT_CITY, model.CLNT_SUBDISTRICT,
        //                                                                 model.CLNT_URBAN_VILLAGE, model.CLNT_HAMLET_NO, model.CLNT_NEIGHBOORHOOD_NO, model.CLNT_PROVINCE_ID_AHU, model.CLNT_CITY_ID_AHU,
        //                                                                 model.actived, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Klien ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            ////get total data from server//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetKlienListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetKlienList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["CLNTMList"] = master;
        //            TempData["CLNTMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectTipeKlien"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Klien/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clndelAddMTDCLNT(string paramkey)
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
        //        master = TempData["CLNTMList"] as vmMasterData;
        //        modFilter = TempData["CLNTMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["CLNTMList"] = master;
        //        TempData["CLNTMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = paramkey;

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
        //        modFilter.keyword = keyword;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbdeleteKlien(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Klien", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetKlienListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetKlienList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["CLNTMList"] = master;
        //            TempData["CLNTMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Klien/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDCLNT()
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
        //        modFilter = TempData["CLNTMListFilter"] as cFilterMasterData;
        //        master = TempData["CLNTMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword;

        //        TempData["CLNTMList"] = master;
        //        TempData["CLNTMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Klien/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDCLNT(cFilterMasterData model)
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
        //        modFilter = TempData["CLNTMListFilter"] as cFilterMasterData;
        //        master = TempData["CLNTMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";

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
        //        modFilter.keyword = Keyword;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetKlienListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetKlienList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["CLNTMListFilter"] = modFilter;
        //            TempData["CLNTMList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Klien/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["CLNTsiFilter"] = modFilter;
        //            TempData["CLNTsiList"] = master;
        //            TempData["common"] = Common;
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
        //#endregion Master Klien

        //#region Master COA
        //[HttpPost]
        //public async Task<ActionResult> clnMTDCOA(String menu, String caption)
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
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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

        //        if (Common.ddlGrupCoa == null)
        //        {
        //            Common.ddlGrupCoa = await Commonddl.dbddlgetparamenumsList("COG");
        //        }
        //        if (Common.ddlGrupNeraca == null)
        //        {
        //            Common.ddlGrupNeraca = await Commonddl.dbddlgetparamenumsList("NRC");
        //        }
        //        if (Common.ddlNatureCoa == null)
        //        {
        //            Common.ddlNatureCoa = await Commonddl.dbddlgetparamenumsList("NTRACCT");
        //        }

        //        ViewData["SelectGrupCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupCoa);
        //        ViewData["SelecNatureCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlNatureCoa);
        //        ViewData["SelectGrupNeraca"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupNeraca);

        //        //set session filterisasi //
        //        TempData["COAMList"] = master;
        //        TempData["COAMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDCOA";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Coa/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDCOA(int paged)
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
        //        modFilter = TempData["COAMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMList"] as vmMasterData;
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
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["COAMList"] = master;
        //        TempData["COAMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Coa/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDCOA(string paramkey)
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
        //        modFilter = TempData["COAMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        ViewData["SelectGrupCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupCoa);
        //        ViewData["SelecNatureCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlNatureCoa);
        //        ViewData["SelectGrupNeraca"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupNeraca);

        //        cMasterDataCoa modeldata = new cMasterDataCoa();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.CoaCode = dr["CoaCode"].ToString();
        //            modeldata.Description = dr["Description"].ToString();
        //            modeldata.TipeCoa = bool.Parse(dr["TipeCoa"].ToString());
        //            modeldata.OprMinus = bool.Parse(dr["OprMinus"].ToString());
        //            modeldata.NatureAcct = dr["NatureAcct"].ToString();
        //            modeldata.GrupOrderRptbalance = dr["GrupOrderRptbalance"].ToString();
        //            modeldata.GrupAcct = dr["GrupAcct"].ToString();
        //            modeldata.IsNotViewInLR = bool.Parse(dr["IsNotViewInLR"].ToString());
        //            modeldata.IsNotViewInNR = bool.Parse(dr["IsNotViewInNR"].ToString());
        //            modeldata.InitialCode = dr["InitialCode"].ToString();
        //            modeldata.IsBlock = bool.Parse(dr["IsBlock"].ToString());

        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["COAMList"] = master;
        //        TempData["COAMListFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Coa/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clnUpdMasterMTDCOA(cMasterDataCoa model)
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
        //        master = TempData["COAMList"] as vmMasterData;
        //        modFilter = TempData["COAMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["COAMList"] = master;
        //        TempData["COAMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        //string Klien = HasKeyProtect.Decryption(model.CLNT_ID);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateCoa(ID, model.CoaCode, model.Description, model.TipeCoa, model.OprMinus, model.NatureAcct,
        //                                                                 model.GrupOrderRptbalance, model.GrupAcct, model.IsNotViewInLR, model.IsNotViewInNR,
        //                                                                 model.InitialCode, model.IsBlock, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Chart Of Account ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            ////get total data from server//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["COAMList"] = master;
        //            TempData["COAMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectGrupCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupCoa);
        //        ViewData["SelecNatureCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlNatureCoa);
        //        ViewData["SelectGrupNeraca"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupNeraca);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Coa/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clndelAddMTDCOA(string paramkey)
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
        //        master = TempData["COAMList"] as vmMasterData;
        //        modFilter = TempData["COAMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["COAMList"] = master;
        //        TempData["COAMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = paramkey;

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
        //        modFilter.keyword = keyword;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbdeleteCoa(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Chart Of Account", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["COAMList"] = master;
        //            TempData["COAMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Coa/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDCOA()
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
        //        modFilter = TempData["COAMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword;

        //        TempData["COAMList"] = master;
        //        TempData["COAMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Coa/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDCOA(cFilterMasterData model)
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
        //        modFilter = TempData["COAMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";

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
        //        modFilter.keyword = Keyword;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["COAMListFilter"] = modFilter;
        //            TempData["COAMList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/Coa/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["COAsiFilter"] = modFilter;
        //            TempData["COAsiList"] = master;
        //            TempData["common"] = Common;
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
        //#endregion Master COA

        //#region Master COA MAP
        //[HttpPost]
        //public async Task<ActionResult> clnMTDCOAMAP(String menu, String caption)
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
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaMAPListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaMAPList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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

        //        if (Common.ddlCoa == null)
        //        {
        //            Common.ddlCoa = await Commonddl.dbGetCoaListByEncrypt();
        //        }

        //        if (Common.ddlCoaPost == null)
        //        {
        //            Common.ddlCoaPost = await Commonddl.dbddlgetparamenumsList("COGPOST");
        //        }

        //        ViewData["SelectCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlCoa);
        //        ViewData["SelectCoaPost"] = OwinLibrary.Get_SelectListItem(Common.ddlCoaPost);

        //        //set session filterisasi //
        //        TempData["COAMAPMList"] = master;
        //        TempData["COAMAPMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDCOAMAP";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CoaMap/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDCOAMAP(int paged)
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
        //        modFilter = TempData["COAMAPMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMAPMList"] as vmMasterData;
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
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaMAPList(master.DTFromDB, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["COAMAPMList"] = master;
        //        TempData["COAMAPMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CoaMap/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDCOAMAP(string paramkey)
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
        //        modFilter = TempData["COAMAPMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMAPMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        ViewData["SelectCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlCoa);
        //        ViewData["SelectCoaPost"] = OwinLibrary.Get_SelectListItem(Common.ddlCoaPost);

        //        cMasterDataCoaMap modeldata = new cMasterDataCoaMap();
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.KodeGroup = dr["KodeGroup"].ToString();
        //            modeldata.Transaksi = dr["Transaksi"].ToString();
        //            modeldata.LawanTransaksi = (dr["LawanTransaksi"].ToString());
        //            modeldata.D = (dr["D"].ToString());
        //            modeldata.K = dr["K"].ToString();
        //            modeldata.StartDate = dr["StartDate"].ToString() != "" ? DateTime.Parse(dr["StartDate"].ToString()).ToString("dd-MMMM-yyyy") : null;
        //            modeldata.EndDate = dr["EndDate"].ToString() != "" ? DateTime.Parse(dr["EndDate"].ToString()).ToString("dd-MMMM-yyyy") : null;

        //            modFilter.keylookupdata = paramkey;
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["COAMAPMList"] = master;
        //        TempData["COAMAPMListFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CoaMap/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clnUpdMasterMTDCOAMAP(cMasterDataCoaMap model)
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
        //        master = TempData["COAMAPMList"] as vmMasterData;
        //        modFilter = TempData["COAMAPMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["COAMAPMList"] = master;
        //        TempData["COAMAPMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.keyword = keyword;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        //string Klien = HasKeyProtect.Decryption(model.CLNT_ID);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateCoaMAP(ID, model.KodeGroup, model.Transaksi, model.LawanTransaksi, model.D, model.K,
        //                                                                 model.StartDate, model.EndDate, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Chart Of Mapping ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            ////get total data from server//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaMAPListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;

        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaMAPList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["COAMAPMList"] = master;
        //            TempData["COAMAPMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectCoa"] = OwinLibrary.Get_SelectListItem(Common.ddlCoa);
        //        ViewData["SelectCoaPost"] = OwinLibrary.Get_SelectListItem(Common.ddlCoaPost);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CoaMap/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clndelAddMTDCOAMAP(string paramkey)
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
        //        master = TempData["COAMAPMList"] as vmMasterData;
        //        modFilter = TempData["COAMAPMListFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["COAMAPMList"] = master;
        //        TempData["COAMAPMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string keyword = modFilter.keyword;
        //        string keylookupdata = paramkey;

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
        //        modFilter.keyword = keyword;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbdeleteCoaMAP(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Chart Of Mapping", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaMAPListCount(keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaMAPList(null, keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["COAMAPMList"] = master;
        //            TempData["COAMAPMListFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CoaMap/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDCOAMAP()
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
        //        modFilter = TempData["COAMAPMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMAPMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string Keyword = modFilter.keyword;

        //        TempData["COAMAPMList"] = master;
        //        TempData["COAMAPMListFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CoaMap/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDCOAMAP(cFilterMasterData model)
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
        //        modFilter = TempData["COAMAPMListFilter"] as cFilterMasterData;
        //        master = TempData["COAMAPMList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string Keyword = model.keyword ?? "";

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
        //        modFilter.keyword = Keyword;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGetCoaMAPListCount(Keyword, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetCoaMAPList(null, Keyword, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["COAMAPMListFilter"] = modFilter;
        //            TempData["COAMAPMList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/CoaMap/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["COAMAPsiFilter"] = modFilter;
        //            TempData["COAMAPsiList"] = master;
        //            TempData["common"] = Common;
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
        //#endregion Master COA MAP

        //#region Master Pembagian Order PIC Persentage
        //[HttpPost]
        //public async Task<ActionResult> clnMTDSHRPIC(String menu, String caption)
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
        //        string SelectUserID = HasKeyProtect.Encryption("");
        //        string SelectRegion = HasKeyProtect.Encryption("");
        //        string SelectJenisKontrak = null;

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

        //        modFilter.SelectUserID = SelectUserID;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;

        //        modFilter.PageNumber = PageNumber;

        //        //descript some value for db//
        //        SelectUserID = HasKeyProtect.Decryption(SelectUserID);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        // try show filter data//
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGeGeSharePICListCount(SelectUserID, SelectRegion, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeSharePICList(null, SelectUserID, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        totalRecordclient = dtlist[0].Rows.Count;
        //        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //        if (Common.ddlUser == null)
        //        {
        //            Common.ddlUser = await Commonddl.dbGetUserRegisAhuListByEncrypt();
        //        }

        //        if (Common.ddlRegion == null)
        //        {
        //            Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt();
        //        }

        //        if (Common.ddlJenisKontrak == null)
        //        {
        //            Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
        //        }

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

        //        //set session filterisasi //
        //        TempData["ShareOrderPICList"] = master;
        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDSHRPIC";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SharePIC/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDSHRPIC(int paged)
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
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        // get value filter have been filter//
        //        string SelectUserID = modFilter.SelectUserID;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak;

        //        // set & get for next paging //
        //        int pagenumberclient = paged;
        //        int PageNumber = modFilter.PageNumber;
        //        double pagingsizeclient = modFilter.pagingsizeclient;
        //        double TotalRecord = modFilter.TotalRecord;
        //        double totalRecordclient = modFilter.totalRecordclient;

        //        //descript some value for db//
        //        caption = HasKeyProtect.Decryption(caption);
        //        SelectUserID = HasKeyProtect.Decryption(SelectUserID);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        // try show filter data//
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeSharePICList(master.DTFromDB, SelectUserID, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["ShareOrderPICList"] = master;
        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SharePIC/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDSHRPIC(string paramkey)
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
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        ViewData["SelectUserID"] = OwinLibrary.Get_SelectListItem(Common.ddlUser);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);
        //        ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlCabang);

        //        cMasterDataSharePIC modeldata = new cMasterDataSharePIC();
        //        modeldata.oprkey = "add";
        //        string selectbranch = "";
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.SelectUserID = HasKeyProtect.Encryption(dr["UserID"].ToString());
        //            modeldata.RegionID = HasKeyProtect.Encryption(dr["RegionID"].ToString());
        //            modeldata.Cabang = dr["BRCH_ID"].ToString();
        //            selectbranch = dr["BRCH_ID"].ToString();
        //            modeldata.JenisKontrak = dr["CONT_TYPE"].ToString();
        //            modeldata.persentase = dr["Persentage"].ToString();
        //            modeldata.actived = bool.Parse(dr["active"].ToString());

        //            modFilter.keylookupdata = paramkey;
        //            modeldata.oprkey = "edit";
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["ShareOrderPICList"] = master;
        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            cabang = selectbranch,
        //            loadcabang = 1,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SharePIC/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clndelAddMTDSHRPIC(string paramkey)
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
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["ShareOrderPICList"] = master;
        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string SelectUserID = modFilter.SelectUserID;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak;
        //        string keylookupdata = paramkey;

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
        //        modFilter.SelectUserID = SelectUserID;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        SelectUserID = HasKeyProtect.Decryption(SelectUserID);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbdeleteSharePIC(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Share PIC PendingTask", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeSharePICList(null, SelectUserID, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["ShareOrderPICList"] = master;
        //            TempData["ShareOrderPICFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectUserID"] = OwinLibrary.Get_SelectListItem(Common.ddlUser);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SharePIC/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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

        //public async Task<ActionResult> clnUpdMasterMTDSHRPIC(cMasterDataSharePIC model)
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
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["ShareOrderPICList"] = master;
        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string SelectUserID = modFilter.SelectUserID;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.SelectUserID = SelectUserID;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        //string UserIDID = HasKeyProtect.Decryption(model.UserID);
        //        string RegionID = model.RegionID ?? "";
        //        RegionID = (RegionID!="") ? HasKeyProtect.Decryption(model.RegionID) : RegionID;
        //        string CabangID = model.Cabang ?? "";

        //        SelectUserID = HasKeyProtect.Decryption(model.SelectUserID);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateSharePIC(ID, SelectUserID, RegionID, CabangID, model.JenisKontrak, model.persentase, model.actived, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Share PIC PendingTask", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeSharePICList(null, "", SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["ShareOrderPICList"] = master;
        //            TempData["ShareOrderPICFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectUserID"] = OwinLibrary.Get_SelectListItem(Common.ddlUser);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SharePIC/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDSHRPIC()
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
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string SelectUserID = modFilter.SelectUserID;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak ?? "0";

        //        ViewData["SelectUserID"] = OwinLibrary.Get_SelectListItem(Common.ddlUser);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

        //        TempData["ShareOrderPICList"] = master;
        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SharePIC/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDSHRPIC(cFilterMasterData model)
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
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string SelectUserID = (model.SelectUserID ?? "");
        //        SelectUserID = SelectUserID == "" ? HasKeyProtect.Encryption(SelectUserID) : SelectUserID;
        //        string SelectRegion = (model.SelectRegion ?? "");
        //        SelectRegion = SelectRegion == "" ? HasKeyProtect.Encryption(SelectRegion) : SelectRegion;
        //        string SelectJenisKontrak = model.SelectJenisKontrak ?? null;

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
        //        modFilter.SelectUserID = SelectUserID;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);
        //            SelectUserID = HasKeyProtect.Decryption(SelectUserID);
        //            SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGeGeSharePICListCount(SelectUserID, SelectRegion, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeSharePICList(null, SelectUserID, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["ShareOrderPICFilter"] = modFilter;
        //            TempData["ShareOrderPICList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/SharePIC/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["ShareOrderPICFilter"] = modFilter;
        //            TempData["ShareOrderPICList"] = master;
        //            TempData["common"] = Common;
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
        //public async Task<ActionResult> clnGetBranchPIC(string clientid, string regionid = "")
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
        //        //set session filterisasi //
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid + regionid] as IEnumerable<cListSelected>);

        //        string UserID = modFilter.UserID;

        //        //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
        //        bool loaddata = false;

        //        //set field filter to varibale //

        //        //string SelectNotaris = (model.SelectNotaris ?? "");
        //        //SelectNotaris = SelectNotaris == "" ? HasKeyProtect.Encryption(SelectNotaris) : SelectNotaris;
        //        //string SelectRegion = (model.SelectRegion ?? "");
        //        //SelectRegion = SelectRegion == "" ? HasKeyProtect.Encryption(SelectRegion) : SelectRegion;
        //        //string SelectJenisKontrak = model.SelectJenisKontrak ?? null;

        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectBranch = modFilter.SelectBranch;

        //        if (((SelectRegion != regionid)) && (regionid != ""))
        //        {
        //            SelectRegion = regionid;

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
        //                string decSelectClient = "";
        //                string decSelectBranch = (SelectBranch ?? "") == "" ? "" : HasKeyProtect.Decryption(SelectBranch);
        //                string decSelectRegion = HasKeyProtect.Decryption(SelectRegion);
        //                Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, decSelectRegion, UserID);
        //                tempbrach = Common.ddlBranch;
        //            }
        //        }

        //        TempData["tempbrach" + clientid + regionid] = tempbrach;

        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["ShareOrderPICList"] = master;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            branchjson = new JavaScriptSerializer().Serialize(tempbrach),
        //            brachselect = SelectBranch,
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
        //public ActionResult clnListExcelMTDSHRPIC(string tipemodule = "")
        //{
        //    Account = (vmAccount)Session["Account"];
        //    Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

        //    //bool IsErrorTimeout = false;
        //    if (Account != null)
        //    {
        //        if (Account.AccountLogin.RouteName != "")
        //        {
        //            return RedirectToRoute(Account.AccountLogin.RouteName);
        //            //IsErrorTimeout = true;
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToRoute("DefaultExpired");
        //    }

        //    try
        //    {
        //        //get session filterisasi //
        //        modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderPICList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        TempData["ShareOrderPICFilter"] = modFilter;
        //        TempData["ShareOrderPICList"] = master;
        //        TempData["common"] = Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        // get value filter before//
        //        string SelectUserID = modFilter.SelectUserID ?? "";
        //        SelectUserID = SelectUserID == "" ? HasKeyProtect.Encryption(SelectUserID) : SelectUserID;
        //        string SelectRegion = modFilter.SelectRegion ?? "";
        //        SelectRegion = SelectRegion == "" ? HasKeyProtect.Encryption(SelectRegion) : SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak ?? null;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        SelectUserID = HasKeyProtect.Decryption(SelectUserID);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);
        //        DataTable dt = MasterDataProvinsiddl.dbGetSharePICListExport(SelectUserID, SelectRegion, SelectJenisKontrak, 1, caption, UserID, GroupName);

        //        StringBuilder sb = new StringBuilder();
        //        sb = OwinLibrary.ConvertDTToTxt(dt);
        //        string filename = "SharingOrderPIC_" + DateTime.Now.ToString("ddMMyyyssmm") + ".csv";

        //        //Writing StringBuilder content to an excel file.
        //        Response.Clear();
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.Charset = "";
        //        Response.Buffer = true;
        //        Response.ClearHeaders();
        //        //Response.ContentType = "application/vnd.ms-excel";
        //        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8";
        //        //Response.ContentType = "text/plain"; txt
        //        Response.ContentType = "application/text";
        //        //csv
        //        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //        Response.Write(sb.ToString());
        //        Response.Flush();
        //        Response.Close();

        //        return View();

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");

        //        return RedirectToRoute("ErroPage");
        //    }

        //}
        //#endregion Master Pembagian Order PIC Persentage

        //#region Master Pembagian Order Notaris Persentage
        //[HttpPost]
        //public async Task<ActionResult> clnMTDSHRNTRS(String menu, String caption)
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
        //        string SelectNotaris = HasKeyProtect.Encryption("");
        //        string SelectRegion = HasKeyProtect.Encryption("");
        //        string SelectJenisKontrak = null;

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

        //        modFilter.SelectNotaris = SelectNotaris;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;

        //        modFilter.PageNumber = PageNumber;

        //        //descript some value for db//
        //        SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        // try show filter data//
        //        List<String> recordPage = await MasterDataProvinsiddl.dbGeGeShareNotarisListCount(SelectNotaris, SelectRegion, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
        //        TotalRecord = Convert.ToDouble(recordPage[0]);
        //        TotalPage = Convert.ToDouble(recordPage[1]);
        //        pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //        pagenumberclient = PageNumber;
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeShareNotarisList(null, SelectNotaris, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        totalRecordclient = dtlist[0].Rows.Count;
        //        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //        if (Common.ddlNotaris == null)
        //        {
        //            Common.ddlNotaris = await Commonddl.dbGetNotarisListByEncrypt();
        //        }

        //        if (Common.ddlRegion == null)
        //        {
        //            Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt();
        //        }

        //        if (Common.ddlJenisKontrak == null)
        //        {
        //            Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
        //        }

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

        //        //set session filterisasi //
        //        TempData["ShareOrderNotarisList"] = master;
        //        TempData["ShareOrderNotarisFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //set caption view//
        //        ViewBag.menu = menu;
        //        ViewBag.caption = caption;
        //        ViewBag.captiondesc = menuitemdescription;
        //        ViewBag.rute = "MasterData";
        //        ViewBag.action = "clnMTDSHRNTRS";

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //send back to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ShareNotaris/uiMasterData.cshtml", master),
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
        //public async Task<ActionResult> clnRgridListMTDSHRNTRS(int paged)
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
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        // get value filter have been filter//
        //        string SelectNotaris = modFilter.SelectNotaris;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak;

        //        // set & get for next paging //
        //        int pagenumberclient = paged;
        //        int PageNumber = modFilter.PageNumber;
        //        double pagingsizeclient = modFilter.pagingsizeclient;
        //        double TotalRecord = modFilter.TotalRecord;
        //        double totalRecordclient = modFilter.totalRecordclient;

        //        //descript some value for db//
        //        caption = HasKeyProtect.Decryption(caption);
        //        SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        // try show filter data//
        //        List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeShareNotarisList(master.DTFromDB, SelectNotaris, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //        // update active paging back to filter //
        //        modFilter.pagenumberclient = pagenumberclient;

        //        //set akta//
        //        master.DTDetailForGrid = dtlist[1];

        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

        //        //set session filterisasi //
        //        TempData["ShareOrderNotarisList"] = master;
        //        TempData["ShareOrderNotarisFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ShareNotaris/_uiGridMasterDataList.cshtml", master),
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
        //public async Task<ActionResult> clnOpenAddMTDSHRNTRS(string paramkey)
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
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);
        //        ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlCabang);

        //        cMasterDataShareNotaris modeldata = new cMasterDataShareNotaris();
        //        modeldata.oprkey = "add";
        //        string selectbranch = "";
        //        DataRow dr = master.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        if (dr != null)
        //        {
        //            modeldata.NotarisID = HasKeyProtect.Encryption(dr["NTRY_ID"].ToString());
        //            modeldata.NotarisIDBCKP = HasKeyProtect.Encryption(dr["NTRY_ID_BCKP"].ToString());
        //            modeldata.RegionID = HasKeyProtect.Encryption(dr["RegionID"].ToString());
        //            modeldata.Cabang = dr["BRCH_ID"].ToString();
        //            selectbranch = dr["BRCH_ID"].ToString();
        //            modeldata.JenisKontrak = dr["CONT_TYPE"].ToString();
        //            modeldata.persentase = dr["Persentage"].ToString();
        //            modeldata.actived = bool.Parse(dr["active"].ToString());

        //            modFilter.keylookupdata = paramkey;
        //            modeldata.oprkey = "edit";
        //        }

        //        ViewBag.Menu = modFilter.Menu;
        //        TempData["ShareOrderNotarisList"] = master;
        //        TempData["ShareOrderNotarisFilter"] = modFilter;
        //        TempData["common"] = Common;
        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            cabang = selectbranch,
        //            loadcabang = 1,
        //            keydata = paramkey,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ShareNotaris/_uiUpdateMasterData.cshtml", modeldata),
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
        //public async Task<ActionResult> clndelAddMTDSHRNTRS(string paramkey)
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
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["ShareOrderNotarisList"] = master;
        //        TempData["ShareOrderNotarisFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string SelectNotaris = modFilter.SelectNotaris;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak;
        //        string keylookupdata = paramkey;

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
        //        modFilter.SelectNotaris = SelectNotaris;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbdeleteShareNotaris(ID, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Share Order Notaris ", "dihapus") : EnumMessage;

        //        if (result == 1)
        //        {
        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeShareNotarisList(null, SelectNotaris, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["ShareOrderNotarisList"] = master;
        //            TempData["ShareOrderNotarisFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ShareNotaris/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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

        //public async Task<ActionResult> clnUpdMasterMTDSHRNTRS(cMasterDataShareNotaris model)
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
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        TempData["ShareOrderNotarisList"] = master;
        //        TempData["ShareOrderNotarisFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        //get value from aply filter //
        //        string SelectNotaris = modFilter.SelectNotaris;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak;
        //        string keylookupdata = model.keylookup;

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
        //        modFilter.SelectNotaris = SelectNotaris;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;
        //        modFilter.keylookupdata = keylookupdata;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        string NotarisID = HasKeyProtect.Decryption(model.NotarisID);
        //        string NotarisIDBCKP = HasKeyProtect.Decryption(model.NotarisIDBCKP);
        //        string RegionID = HasKeyProtect.Decryption(model.RegionID);
        //        string CabangID = model.Cabang ?? "";

        //        SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //        DataRow dr;
        //        int ID = 0;
        //        if ((keylookupdata ?? "") != "")
        //        {
        //            dr = master.DTFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == keylookupdata).SingleOrDefault();
        //            ID = (dr != null) ? int.Parse(dr["Id"].ToString()) : ID;
        //        }

        //        int result = await MasterDataProvinsiddl.dbupdateShareNotaris(ID, NotarisID, NotarisIDBCKP, RegionID, CabangID, model.JenisKontrak, model.persentase, model.actived, caption, UserID, GroupName);

        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
        //        EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Data Share Order Notaris ", "disimpan") : EnumMessage;

        //        if (result == 1)
        //        {
        //            //set paging in grid client//
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeShareNotarisList(null, SelectNotaris, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //back to set in filter//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            TempData["ShareOrderNotarisList"] = master;
        //            TempData["ShareOrderNotarisFilter"] = modFilter;
        //            TempData["common"] = Common;
        //        }

        //        string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //        ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //        ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ShareNotaris/_uiGridMasterDataList.cshtml", master),
        //            msg = EnumMessage,
        //            resulted = result
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
        //public async Task<ActionResult> clnOpenFilterMTDSHRNTRS()
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
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        string UserID = modFilter.UserID;

        //        // get value filter before//
        //        string SelectNotaris = modFilter.SelectNotaris;
        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak ?? "0";

        //        ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
        //        ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
        //        ViewData["SelectjenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);

        //        TempData["ShareOrderNotarisList"] = master;
        //        TempData["ShareOrderNotarisFilter"] = modFilter;
        //        TempData["common"] = Common;

        //        // senback to client browser//
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ShareNotaris/_uiFilterData.cshtml", master.MasterFilter),
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
        //public async Task<ActionResult> clnListFilterMTDSHRNTRS(cFilterMasterData model)
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
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        //get value from old define//
        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        //get value from aply filter //
        //        string SelectNotaris = (model.SelectNotaris ?? "");
        //        SelectNotaris = SelectNotaris == "" ? HasKeyProtect.Encryption(SelectNotaris) : SelectNotaris;
        //        string SelectRegion = (model.SelectRegion ?? "");
        //        SelectRegion = SelectRegion == "" ? HasKeyProtect.Encryption(SelectRegion) : SelectRegion;
        //        string SelectJenisKontrak = model.SelectJenisKontrak ?? null;

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
        //        modFilter.SelectNotaris = SelectNotaris;
        //        modFilter.SelectRegion = SelectRegion;
        //        modFilter.SelectJenisKontrak = SelectJenisKontrak;

        //        modFilter.PageNumber = PageNumber;
        //        modFilter.isModeFilter = isModeFilter;
        //        //set filter//

        //        string validtxt = "";
        //        if (validtxt == "")
        //        {
        //            //descript some value for db//
        //            caption = HasKeyProtect.Decryption(caption);
        //            SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
        //            SelectRegion = HasKeyProtect.Decryption(SelectRegion);

        //            // try show filter data//
        //            List<String> recordPage = await MasterDataProvinsiddl.dbGeGeShareNotarisListCount(SelectNotaris, SelectRegion, SelectJenisKontrak, PageNumber, caption, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await MasterDataProvinsiddl.dbGetGeShareNotarisList(null, SelectNotaris, SelectRegion, SelectJenisKontrak, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            //set in filter for paging//
        //            modFilter.TotalRecord = TotalRecord;
        //            modFilter.TotalPage = TotalPage;
        //            modFilter.pagingsizeclient = pagingsizeclient;
        //            modFilter.totalRecordclient = totalRecordclient;
        //            modFilter.totalPageclient = totalPageclient;
        //            modFilter.pagenumberclient = pagenumberclient;

        //            //set to object pendataran//
        //            master.DTFromDB = dtlist[0];
        //            master.DTDetailForGrid = dtlist[1];
        //            master.MasterFilter = modFilter;

        //            //keep session filterisasi before//
        //            TempData["ShareOrderNotarisFilter"] = modFilter;
        //            TempData["ShareOrderNotarisList"] = master;
        //            TempData["common"] = Common;

        //            string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
        //            ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

        //            return Json(new
        //            {
        //                moderror = IsErrorTimeout,
        //                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Master/ShareNotaris/_uiGridMasterDataList.cshtml", master),
        //                download = "",
        //                message = TotalRecord == 0 ? "Data tidak ditemukan " : validtxt,
        //            });

        //        }
        //        else
        //        {
        //            TempData["ShareOrderNotarisFilter"] = modFilter;
        //            TempData["ShareOrderNotarisList"] = master;
        //            TempData["common"] = Common;
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
        //public async Task<ActionResult> clnGetBranch(string clientid, string regionid = "")
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
        //        //set session filterisasi //
        //        bool actPIC = false;
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;

        //        if (modFilter == null)
        //        {
        //            modFilter = TempData["ShareOrderPICFilter"] as cFilterMasterData;
        //            master = TempData["ShareOrderPICList"] as vmMasterData;
        //            actPIC = true;
        //        }

        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid + regionid] as IEnumerable<cListSelected>);

        //        string UserID = modFilter.UserID;

        //        //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
        //        bool loaddata = false;

        //        //set field filter to varibale //

        //        //string SelectNotaris = (model.SelectNotaris ?? "");
        //        //SelectNotaris = SelectNotaris == "" ? HasKeyProtect.Encryption(SelectNotaris) : SelectNotaris;
        //        //string SelectRegion = (model.SelectRegion ?? "");
        //        //SelectRegion = SelectRegion == "" ? HasKeyProtect.Encryption(SelectRegion) : SelectRegion;
        //        //string SelectJenisKontrak = model.SelectJenisKontrak ?? null;

        //        string SelectRegion = modFilter.SelectRegion;
        //        string SelectBranch = modFilter.SelectBranch;

        //        if (((SelectRegion != regionid)) && (regionid != ""))
        //        {
        //            SelectRegion = regionid;

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
        //                string decSelectClient = "";
        //                string decSelectBranch = (SelectBranch ?? "") == "" ? "" : HasKeyProtect.Decryption(SelectBranch);
        //                string decSelectRegion = HasKeyProtect.Decryption(SelectRegion);
        //                Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(decSelectBranch, decSelectClient, decSelectRegion, UserID);
        //                tempbrach = Common.ddlBranch;
        //            }
        //        }

        //        TempData["tempbrach" + clientid + regionid] = tempbrach;

        //        if (actPIC == true)
        //        {
        //            TempData["ShareOrderPICFilter"] = modFilter;
        //            TempData["ShareOrderPICList"] = master;
        //        }
        //        else
        //        {
        //            TempData["ShareOrderNotarisFilter"] = modFilter;
        //            TempData["ShareOrderNotarisList"] = master;
        //        }
        //        TempData["common"] = Common;

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            branchjson = new JavaScriptSerializer().Serialize(tempbrach),
        //            brachselect = SelectBranch,
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
        //public ActionResult clnListExcelMTDSHRNTRS(string tipemodule = "")
        //{
        //    Account = (vmAccount)Session["Account"];
        //    Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

        //    //bool IsErrorTimeout = false;
        //    if (Account != null)
        //    {
        //        if (Account.AccountLogin.RouteName != "")
        //        {
        //            return RedirectToRoute(Account.AccountLogin.RouteName);
        //            //IsErrorTimeout = true;
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToRoute("DefaultExpired");
        //    }

        //    try
        //    {
        //        //get session filterisasi //
        //        modFilter = TempData["ShareOrderNotarisFilter"] as cFilterMasterData;
        //        master = TempData["ShareOrderNotarisList"] as vmMasterData;
        //        Common = (TempData["common"] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        TempData["ShareOrderNotarisFilter"] = modFilter;
        //        TempData["ShareOrderNotarisList"] = master;
        //        TempData["common"] = Common;

        //        string UserID = modFilter.UserID;
        //        string GroupName = modFilter.GroupName;
        //        string caption = modFilter.idcaption;

        //        // get value filter before//
        //        string SelectNotaris = modFilter.SelectNotaris ?? "";
        //        SelectNotaris = SelectNotaris == "" ? HasKeyProtect.Encryption(SelectNotaris) : SelectNotaris;
        //        string SelectRegion = modFilter.SelectRegion ?? "";
        //        SelectRegion = SelectRegion == "" ? HasKeyProtect.Encryption(SelectRegion) : SelectRegion;
        //        string SelectJenisKontrak = modFilter.SelectJenisKontrak ?? null;

        //        //decript some model apply for DB//
        //        caption = HasKeyProtect.Decryption(caption);
        //        SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
        //        SelectRegion = HasKeyProtect.Decryption(SelectRegion);
        //        DataTable dt = MasterDataProvinsiddl.dbGetShareNotarisListExport(SelectNotaris, SelectRegion, SelectJenisKontrak, 1, caption, UserID, GroupName);

        //        StringBuilder sb = new StringBuilder();
        //        sb = OwinLibrary.ConvertDTToTxt(dt);
        //        string filename = "SharingOrderNotaris_" + DateTime.Now.ToString("ddMMyyyssmm") + ".csv";

        //        //Writing StringBuilder content to an excel file.
        //        Response.Clear();
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.Charset = "";
        //        Response.Buffer = true;
        //        Response.ClearHeaders();
        //        //Response.ContentType = "application/vnd.ms-excel";
        //        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8";
        //        //Response.ContentType = "text/plain"; txt
        //        Response.ContentType = "application/text";
        //        //csv
        //        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //        Response.Write(sb.ToString());
        //        Response.Flush();
        //        Response.Close();

        //        return View();

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");

        //        return RedirectToRoute("ErroPage");
        //    }

        //}
        //#endregion Master Pembagian Order Notaris Persentage
    }
}