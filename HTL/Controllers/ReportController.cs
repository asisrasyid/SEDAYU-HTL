//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;
//using System.Web.Script.Serialization;
//using System.IO;
//using System.Security.Cryptography;
//using HashNetFramework;
//using System.Configuration;
//using System.Data;
using ExcelDataReader;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
using System.Text;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;
using System;
using System.Linq;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using HashNetFramework;

namespace DusColl.Controllers
{
    public class ReportController : Controller
    {
        // GET: /Report/
        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        vmReportData ReportData = new vmReportData();
        cFilterContract modFilter = new cFilterContract();
        blReportData lgReportData = new blReportData();
        vmRegmitraddl regmitraddl = new vmRegmitraddl();
        vmHTLddl HTddl = new vmHTLddl();


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
                modFilter = TempData["ReportListFilter"] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid] as IEnumerable<cListSelected>);

                //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                bool loaddata = false;
                //set field filter to varibale //
                string SelectBranch = modFilter.SelectBranch;
                string SelectClient = modFilter.SelectClient;
                string SelectRegion = modFilter.SelectRegion;
                string SelectNotaris = modFilter.SelectNotaris;
                string UserID = Account.AccountLogin.UserID;
                string IDCabang = Account.AccountLogin.IDCabang;
                string Region = Account.AccountLogin.Region;
                string GroupName = Account.AccountLogin.GroupName;



                //set field filter to varibale //
                string SelectArea = modFilter.SelectArea ?? "";
                SelectBranch = modFilter.SelectBranch ?? "";
                if (SelectArea != clientid)
                {
                    SelectArea = clientid;
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
                        Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", "", decSelectArea, "", UserID, GroupName);
                        tempbrach = Common.ddlBranch;
                    }
                }
                else
                {
                    SelectBranch = "";
                }

                TempData["tempbrach" + clientid] = tempbrach;
                TempData["ReportListFilter"] = modFilter;
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

        public async Task<ActionResult> clncreatereport(string menu, string caption)
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
                string seccaption = HasKeyProtect.Encryption(caption);

                // some field must be overide first for default filter//
                string SelectRegion = Region;
                string SelectClient = ClientID;
                string SelectBranch = IDCabang;
                string SelectNotaris = IDNotaris;
                // some field must be overide first for default filter//


                //set default filter //
                modFilter.idcaption = seccaption;
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
                modFilter.SelectClient = SelectClient;
                modFilter.SelectRegion = SelectRegion;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectNotaris = SelectNotaris;


                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectRegion = HasKeyProtect.Decryption(SelectRegion);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);


                //get menudesccriptio//
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                Common.ddlstatus = await Commonddl.dbdbGetDdlEnumsListByEncryptNw("STATHDL", caption, UserID, GroupName, "99");
                Common.ddlsrc = await Commonddl.dbdbGetDdlEnumsListByEncrypt("SRCSLA", caption, UserID, GroupName, "99");

                Common.ddlnotaris = await HTddl.dbdbGetDdlNotarisListByEncrypt(caption, "", UserID, GroupName);
                if (int.Parse(UserTypes) == (int)UserType.Notaris)
                {
                    string notrs = HashNetFramework.HasKeyProtect.Encryption(UserID);
                    Common.ddlnotaris = Common.ddlnotaris.AsEnumerable().Where(x => x.Value == notrs).ToList();
                }




                //if (Common.ddlDevisi == null)
                //{
                //    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", caption, UserID, GroupName);
                //}

                //if (Common.ddlBranch == null)
                //{
                //    string cabsel = "";
                //    if (int.Parse(UserTypes) == (int)UserType.Branch)
                //    {
                //        cabsel = SelectBranch;
                //    }
                //    Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", cabsel, "", caption, UserID, GroupName);
                //}

                //if (Common.ddlStatusMitra == null)
                //{
                //    Common.ddlStatusMitra = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATMITRA", caption, UserID, GroupName);
                //}

                //if (Common.ddlRegion == null)
                //{
                //    Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("", "", caption, UserID, GroupName);
                //}

                //ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                //ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                //ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlstatus);
                ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlnotaris);
                ViewData["SelectSRC"] = OwinLibrary.Get_SelectListItem(Common.ddlsrc);
                //// try make filter initial & set secure module name //
                //if (Common.ddlClient == null)
                //{
                //    Common.ddlClient = await Commonddl.dbGetClientListByEncrypt(SelectClient);
                //}

                //if (Common.ddlClient.Count() == 1 && Common.ddlBranch == null)
                //{
                //    Common.ddlBranch = await Commonddl.dbGetDdlBranchListByEncrypt(SelectBranch, SelectClient, SelectRegion, UserID);
                //    SelectClient = Common.ddlClient.SingleOrDefault().Value;
                //    TempData["tempbrach" + SelectClient] = Common.ddlBranch;
                //}

                //if (Common.ddlNotaris == null)
                //{
                //    Common.ddlNotaris = await Commonddl.dbGetNotarisListByEncrypt();
                //}

                //if (Common.ddlContractStatus == null)
                //{
                //    Common.ddlContractStatus = await Commonddl.dbddlgetparamenumsList("ContStatus");
                //}

                //if (Common.ddlStatusBilling == null)
                //{
                //    Common.ddlStatusBilling = await Commonddl.dbddlgetparamenumsList("ContPaid");
                //}

                //if (Common.ddlStatusClaimbase == null)
                //{
                //    Common.ddlStatusClaimbase = await Commonddl.dbddlgetparamenumsList("CLAIMBASE");
                //}

                //if (Common.ddlStatusWarkah == null)
                //{
                //    Common.ddlStatusWarkah = await Commonddl.dbddlgetparamenumsList("DocStatus");
                //}

                //if (Common.ddlJenisPelanggan == null)
                //{
                //    Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
                //}

                //if (Common.ddlJenisKontrak == null)
                //{
                //    Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                //}

                //if (Common.ddlRegion == null)
                //{
                //    Common.ddlRegion = await Commonddl.dbGetDdlRegionListByEncrypt();
                //}

                //ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                //ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                //ViewData["SelectNotaris"] = OwinLibrary.Get_SelectListItem(Common.ddlNotaris);
                //ViewData["SelectWarkahStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusWarkah);
                //ViewData["SelectContractStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlContractStatus);
                //ViewData["SelectContractPaidStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusBilling);
                //ViewData["SelectClaimbaseStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusClaimbase);
                //ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                //ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);
                //ViewData["SelectRegion"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                ViewBag.shownotaris = "";
                ViewBag.showsrc = "";
                if (int.Parse(UserTypes) == (int)UserType.FDCM)
                {
                    ViewBag.shownotaris = "yes";
                }

                if (caption == "RTRAJULOG")
                {
                    ViewBag.showsrc = "yes";
                }


                ////set filter to variable filter in class contract for object view//
                ReportData.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["ReportListFilter"] = modFilter;
                TempData["common"] = Common;

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Report";
                ViewBag.action = "clncreatereport";

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Report/uiReportForm.cshtml", ReportData),
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
        public async Task<ActionResult> clnListFilterCreateReport(cFilterContract model, string download, string jenisproses, string fromdatebln)
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
                modFilter = TempData["ReportListFilter"] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                Common.ddlBranch = TempData["tempbrach" + modFilter.SelectClient] as IEnumerable<cListSelected>;

                TempData["tempbrach" + modFilter.SelectClient] = Common.ddlBranch;
                TempData["ReportListFilter"] = modFilter;
                TempData["common"] = Common;


                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string idcaption = modFilter.idcaption;


                //set field to output//
                string fromdate = model.fromdate ?? "";
                string todate = model.todate ?? "";
                string SelectNotaris = model.SelectNotaris ?? "";
                //string NoPerjanjian = model.NoPerjanjian ?? "";
                //string NoAkta = model.NoAkta ?? "";

                //string IsOVerKontrak = model.IS_OVER_CONTRAK.ToString() ?? "false";
                //string SelectRegion = model.SelectRegion ?? modFilter.RegionLogin;
                //string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                //string SelectClientDesc = Common.ddlClient != null ? (Common.ddlClient.Where(x => x.Value == SelectClient).Select(y => y.Text).SingleOrDefault() ?? "") : "";
                string SelectBranch = model.SelectBranch ?? "";
                string SelectBranchDesc = Common.ddlBranch != null ? (Common.ddlBranch.Where(x => x.Value == SelectBranch).Select(y => y.Text).SingleOrDefault() ?? "") : "";

                string SelectDivisi = model.SelectDivisi ?? "";
                string SelectDivisiDesc = Common.ddlDevisi != null ? (Common.ddlDevisi.Where(x => x.Value == SelectDivisi).Select(y => y.Text).SingleOrDefault() ?? "") : "";

                string SelectContractStatus = model.SelectContractStatus ?? "-1";
                SelectContractStatus = (SelectContractStatus.All(char.IsNumber) || SelectContractStatus == "-1") ? SelectContractStatus : HasKeyProtect.Decryption(SelectContractStatus);
                string SelectContractStatusDesc = Common.ddlStatusMitra != null ? (Common.ddlStatusMitra.Where(x => x.Value == SelectContractStatus).Select(y => y.Text).SingleOrDefault() ?? "") : "";

                SelectNotaris = model.SelectNotaris ?? "";
                string SelectNotarisDesc = Common.ddlnotaris != null ? (Common.ddlnotaris.Where(x => x.Value == SelectNotaris).Select(y => y.Text).SingleOrDefault() ?? "") : "";


                //set to filter//
                modFilter.idcaption = idcaption;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.SelectContractStatus = SelectContractStatus;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectBranchDesc = SelectBranchDesc;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectNotaris = SelectNotaris;
                modFilter.SelectDivisiDesc = SelectDivisiDesc;

                // cek validation for filterisasi //
                idcaption = HasKeyProtect.Decryption(idcaption);
                string validtxt = "";  //lgReportData.CheckFilterisasiCreateReport(modFilter, download, idcaption, cretinvoice);
                if (validtxt == "")
                {

                    // sendback to client browser//
                    byte[] result = null;
                    string EnumMessage = "";
                    string filenamevar = "";


                    ////decript all value for pass in db//
                    //SelectClient = HasKeyProtect.Decryption(SelectClient);
                    //SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    //SelectNotaris = HasKeyProtect.Decryption(SelectNotaris);
                    //SelectRegion = HasKeyProtect.Decryption(SelectRegion);

                    //string ModuleName = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == idcaption).Select(y => y.MenuItem.ModuleName).SingleOrDefault();
                    //bool AllowDownload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == idcaption).Select(y => y.AllowDownload).SingleOrDefault();
                    //bool AllowPrint = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == idcaption).Select(y => y.AllowPrint).SingleOrDefault();
                    //string TitileReport = "LAPORAN " + ModuleName;
                    bool AllowDownload = false;
                    bool AllowPrint = false;

                    //if (idcaption == "RSLAPAJU")
                    //{
                    //    filenamevar = (download == "1") ? "Laporan_DataMitra{0}.xml" : "Laporan_DataMitra{0}.xml";
                    //    string TitileReport = "Laporan Data Mitra";
                    //    filenamevar = string.Format(filenamevar, DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    //    if (jenisproses == "2")
                    //    {
                    //        fromdate = "01-" + fromdatebln;
                    //        int dayju = DateTime.DaysInMonth(DateTime.Parse(fromdate).Year, DateTime.Parse(fromdate).Month) - 1;
                    //        todate = DateTime.Parse(fromdate).AddDays(dayju).ToString("dd-MMMM-yyyy");
                    //    }
                    //    DataTable dt = await HTddl.dbGetRptTxdonList("",model.NoPengajuanRequest ?? "", fromdate, todate, int.Parse(SelectContractStatus), idcaption, UserID, GroupName);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        result = await ReportData.dbDownloadLamporanDataHT(TitileReport, SelectBranchDesc, "", SelectDivisiDesc, fromdate, todate, SelectContractStatusDesc, dt, Server.MapPath(Request.ApplicationPath));
                    //    }
                    //    else
                    //    {
                    //        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.RecordNotFound));
                    //    }
                    //}

                    if (idcaption == "RTRAJU")
                    {
                        filenamevar = (download == "1") ? "Laporan_DataHTL_{0}.xml" : "Laporan_DataHTL_{0}.xml";
                        string TitileReport = "Laporan Data HTL";
                        filenamevar = string.Format(filenamevar, DateTime.Now.ToString("ddMMyyyyHHmmss"));

                        string textstat = "";
                        if (model.SelectContractStatusMulti != null)
                        {
                            foreach (string txt in model.SelectContractStatusMulti)
                            {
                                textstat = textstat + HasKeyProtect.Decryption(txt) + ",";
                            }
                            textstat = textstat.Substring(0, textstat.Length - 1);
                        }
                        else
                        {
                            textstat = "-1";
                        }

                        string textnot = "";
                        if (model.SelectNotarisMulti != null)
                        {
                            foreach (string txt in model.SelectNotarisMulti)
                            {
                                textnot = textnot + HasKeyProtect.Decryption(txt) + ",";
                            }
                            textnot = textnot.Substring(0, textnot.Length - 1);
                        }
                        else
                        {
                            textnot = "";
                        }


                        SelectNotaris = textnot;
                        SelectContractStatus = textstat;

                        //SelectNotaris = SelectNotaris != "" ? HasKeyProtect.Decryption(SelectNotaris) : SelectNotaris;
                        DataTable dt = await HTddl.dbGetRptTxdonList("1", model.NoPengajuanRequest ?? "", SelectNotaris, SelectBranch, fromdate, todate, SelectContractStatus, idcaption, UserID, GroupName);
                        if (dt.Rows.Count > 0)
                        {
                            string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                            string usetipedesc = "";
                            if (int.Parse(UserTypes) == (int)UserType.FDCM)
                            {
                                usetipedesc = "FDCM";
                            }
                            //else
                            //{
                            //    dt.Columns.Remove("Notaris_String");
                            //}
                            if (SelectContractStatus.Contains("43") || SelectContractStatus.Contains("42"))
                            {
                                result = await ReportData.dbDownloadLamporanDataHT("ReportDataBAST.xml", usetipedesc, TitileReport, SelectBranchDesc, "", SelectDivisiDesc, fromdate, todate, SelectContractStatusDesc, dt, Server.MapPath(Request.ApplicationPath));
                            }
                            else if (SelectContractStatus.Contains("41"))
                            {
                                result = await ReportData.dbDownloadLamporanDataHT("ReportDataBASTYET.xml", usetipedesc, TitileReport, SelectBranchDesc, "", SelectDivisiDesc, fromdate, todate, SelectContractStatusDesc, dt, Server.MapPath(Request.ApplicationPath));
                            }
                            else if (SelectContractStatus.Contains("47") || SelectContractStatus.Contains("48") || SelectContractStatus.Contains("49") || SelectContractStatus.Contains("50") || SelectContractStatus.Contains("54"))
                            {
                                result = await ReportData.dbDownloadLamporanDataHT("ReportDataSHT.xml", usetipedesc, TitileReport, SelectBranchDesc, "", SelectDivisiDesc, fromdate, todate, SelectContractStatusDesc, dt, Server.MapPath(Request.ApplicationPath));
                            }
                            else
                            {
                                result = await ReportData.dbDownloadLamporanDataHT("", usetipedesc, TitileReport, SelectBranchDesc, "", SelectDivisiDesc, fromdate, todate, SelectContractStatusDesc, dt, Server.MapPath(Request.ApplicationPath));
                            }
                        }
                        else
                        {
                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.RecordNotFound));
                        }
                    }

                    if (idcaption == "RTRAJULOG")
                    {
                        filenamevar = (download == "1") ? "Laporan_LogDataHTL_{0}.xml" : "Laporan_LogDataHTL_{0}.xml";
                        string TitileReport = "Laporan Log Data HTL";
                        filenamevar = string.Format(filenamevar, DateTime.Now.ToString("ddMMyyyyHHmmss"));

                        string textstat = "";
                        if (model.SelectContractStatusMulti != null)
                        {
                            foreach (string txt in model.SelectContractStatusMulti)
                            {
                                textstat = textstat + HasKeyProtect.Decryption(txt) + ",";
                            }
                            textstat = textstat.Substring(0, textstat.Length - 1);
                        }
                        else
                        {
                            textstat = "-1";
                        }

                        string textnot = "";
                        if (model.SelectNotarisMulti != null)
                        {
                            foreach (string txt in model.SelectNotarisMulti)
                            {
                                textnot = textnot + HasKeyProtect.Decryption(txt) + ",";
                            }
                            textnot = textnot.Substring(0, textnot.Length - 1);
                        }
                        else
                        {
                            textnot = "";
                        }


                        SelectNotaris = textnot;
                        SelectContractStatus = textstat;

                        //SelectNotaris != "" ? HasKeyProtect.Decryption(SelectNotaris) : SelectNotaris;
                        DataTable dt = await HTddl.dbGetRptTxdonList(model.TipeTransaksiA, model.NoPengajuanRequest ?? "", SelectNotaris, SelectBranch, fromdate, todate, SelectContractStatus, idcaption, UserID, GroupName);
                        if (dt.Rows.Count > 0)
                        {
                            string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                            string usetipedesc = "";
                            if (int.Parse(UserTypes) == (int)UserType.FDCM)
                            {
                                usetipedesc = "FDCM";
                            }
                            //else
                            //{
                            //    dt.Columns.Remove("Notaris_String");
                            //}
                            result = await ReportData.dbDownloadLamporanDataHT("ReportDataHTLOG.xml", usetipedesc, TitileReport, SelectBranchDesc, "", SelectDivisiDesc, fromdate, todate, SelectContractStatusDesc, dt, Server.MapPath(Request.ApplicationPath));
                        }
                        else
                        {
                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.RecordNotFound));
                        }
                    }

                    var powderdockp = AllowPrint == true ? "1" : "0";
                    var powderdockd = AllowDownload == true ? "1" : "0";

                    var contenttypeed = "application/xml";
                    //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
                    var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=" + model.idcaption + "&file=";
                    var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = result, msg = EnumMessage, contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
                    jsonresult.MaxJsonLength = int.MaxValue;
                    return jsonresult;

                }
                else
                {

                    //sendback to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = "",
                        download = "",
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


    }
}
