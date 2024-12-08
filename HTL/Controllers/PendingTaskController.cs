using HashNetFramework;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DusColl.Controllers
{

    public class PendingTaskController : Controller
    {

        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmPendingTask PendingTask = new vmPendingTask();
        vmPendingTaskddl PendingTaskddl = new vmPendingTaskddl();
        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();
        blPendingTask lgPendingTask = new blPendingTask();


        #region PendingTask Reguler
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
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;

                // get value filter before//
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan ?? "0";
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
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

                if (Common.ddlDocStatus == null)
                {
                    Common.ddlDocStatus = await Commonddl.dbddlgetparamenumsList("DocStatus");
                }

                if (Common.ddlJenisPelanggan == null)
                {
                    Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
                }

                if (Common.ddlJenisKontrak == null)
                {
                    Common.ddlJenisKontrak = await Commonddl.dbddlgetparamenumsList("CONTTYPE");
                }

                if (Common.ddlStatusCheck == null)
                {
                    Common.ddlStatusCheck = await Commonddl.dbddlgetparamenumsList("STATCHECK");
                }

                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectDocStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlDocStatus);
                ViewData["SelectClient"] = OwinLibrary.Get_SelectListItem(Common.ddlClient);
                ViewData["SelectBranch"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectJenisKontrak"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKontrak);
                ViewData["SelectStatusCheck"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusCheck);

                TempData["PendingTaskList"] = PendingTask;
                TempData["PendingTaskListFilter"] = modFilter;
                TempData["common"] = Common;

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    opsi1 = SelectClient,
                    opsi2 = decSelectBranch,
                    opsi3 = SelectJenisPelanggan,
                    opsi5 = NoPerjanjian,
                    opsi6 = fromdate,
                    opsi7 = todate,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiFilterData.cshtml", PendingTask.DetailFilter),
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
                //get session filterisasi //
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
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

                TempData["PendingTaskListFilter"] = modFilter;
                TempData["PendingTaskList"] = PendingTask;
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

        public async Task<ActionResult> clnPendingTask(string menu, string caption)
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


                // some field must be overide first for default filter//
                string SelectClient = ClientID;
                string SelectBranch = IDCabang;
                string SelectJenisPelanggan = "";
                string SelectJenisKontrak = "0";
                string SelectStatusCheck = "0";

                string fromdate = "";
                string todate = "";
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
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.SelectStatusCheck = SelectStatusCheck;


                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;

                modFilter.PageNumber = PageNumber;

                //descript some value for db//
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);

                List<String> recordPage = await PendingTaskddl.dbGetPendingTaskListCount(SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await PendingTaskddl.dbGetPendingTaskList(null, SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                //set to object pendingtask//
                PendingTask.DTOrdersFromDB = dtlist[0];
                PendingTask.DTDetailForGrid = dtlist[1];
                PendingTask.DetailFilter = modFilter;
                PendingTask.Permission = PermisionModule;

                //set session filterisasi //
                TempData["PendingTaskList"] = PendingTask;
                TempData["PendingTaskListFilter"] = modFilter;


                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "PendingTask";
                ViewBag.action = "clnPendingTask";

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";


                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/uiPendingTask.cshtml", PendingTask),
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

        public async Task<ActionResult> clnPendingTaskGet(string NoPerjanjian)
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

                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                if (Common.ddlJenisKelamin == null)
                {
                    Common.ddlJenisKelamin = await Commonddl.dbddlgetparamenumsList("JNSKLM");
                }
                if (Common.ddlKondisiObject == null)
                {
                    Common.ddlKondisiObject = await Commonddl.dbddlgetparamenumsList("KONDOBJ");
                }
                if (Common.ddlRoda == null)
                {
                    Common.ddlRoda = await Commonddl.dbddlgetparamenumsList("RODA");
                }
                if (Common.ddlJenisPelanggan == null)
                {
                    Common.ddlJenisPelanggan = await Commonddl.dbddlgetparamenumsList("GIVFDC");
                }
                if (Common.ddlDataGIVE == null)
                {
                    Common.ddlDataGIVE = await Commonddl.dbddlgetparamenumsList("DTAGVE");
                }

                ViewData["SelectKondisiObjt"] = OwinLibrary.Get_SelectListItem(Common.ddlKondisiObject);
                ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisKelamin);
                ViewData["SelectRoda"] = OwinLibrary.Get_SelectListItem(Common.ddlRoda);
                ViewData["SelectJenisPelanggan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenisPelanggan);
                ViewData["SelectDataGive"] = OwinLibrary.Get_SelectListItem(Common.ddlDataGIVE);

                DataTable resultquery1 = PendingTask.DTDetailForGrid.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == NoPerjanjian).CopyToDataTable();
                caption = HasKeyProtect.Decryption(caption);

                string fdcid = resultquery1.Rows[0]["IDFDC"].ToString();
                string clientid = resultquery1.Rows[0]["clientid"].ToString();
                string noperjan = resultquery1.Rows[0]["NoPerjanjian"].ToString();
                string StatusChecked = resultquery1.Rows[0]["StatusChecked"].ToString();
                int conttype = int.Parse(resultquery1.Rows[0]["CONT_TYPE"].ToString());


                DataTable resultquery = await PendingTaskddl.dbGetPendingTaskGet(fdcid, clientid, noperjan, conttype, StatusChecked, caption, UserID, GroupName);


                var SecNoPerjanjiand = HasKeyProtect.Encryption(resultquery.Rows[0]["NoPerjanjian"].ToString());
                var CabangIdd = HasKeyProtect.Encryption(resultquery.Rows[0]["CabangId"].ToString());
                var SecIDFDCd = HasKeyProtect.Encryption(resultquery.Rows[0]["IDFDC"].ToString());
                var clientidd = HasKeyProtect.Encryption(resultquery.Rows[0]["clientid"].ToString());

                TempData["PendingTaskList"] = PendingTask;
                TempData["PendingTaskListFilter"] = modFilter;
                TempData["common"] = Common;

                var datapendtask = new cPendingTask();

                datapendtask.SecNoPerjanjian = SecNoPerjanjiand;
                datapendtask.CabangId = CabangIdd;
                datapendtask.secIDFDC = SecIDFDCd;
                datapendtask.clientid = clientidd;

                datapendtask.JenisNasabah = resultquery.Rows[0]["JenisNasabah"].ToString();
                datapendtask.NamaNasabah = resultquery.Rows[0]["NamaNasabah"].ToString();
                datapendtask.JenisKelaminNasabah = resultquery.Rows[0]["JenisKelaminNasabah"].ToString();
                datapendtask.NoidentitasNasabah = resultquery.Rows[0]["NoidentitasNasabah"].ToString();
                datapendtask.NoContactnasabah = resultquery.Rows[0]["NoContactnasabah"].ToString();
                datapendtask.AlamatNasabah = resultquery.Rows[0]["AlamatNasabah"].ToString();
                datapendtask.KelurahanNasabah = resultquery.Rows[0]["KelurahanNasabah"].ToString();
                datapendtask.KecamatanNasabah = resultquery.Rows[0]["KecamatanNasabah"].ToString();
                datapendtask.KabupatenKotaNasabah = resultquery.Rows[0]["KabupatenKotaNasabah"].ToString();
                datapendtask.KabupatenKotaNasabahAHU = resultquery.Rows[0]["KabupatenKotaNasabahAHU"].ToString();
                datapendtask.ProvinsiNasabah = resultquery.Rows[0]["ProvinsiNasabah"].ToString();
                datapendtask.PoskodeNasabah = resultquery.Rows[0]["PoskodeNasabah"].ToString();
                datapendtask.RTNasabah = resultquery.Rows[0]["RTNasabah"].ToString();
                datapendtask.RWNasabah = resultquery.Rows[0]["RWNasabah"].ToString();

                datapendtask.NamaBPKB = resultquery.Rows[0]["NamaBPKB"].ToString();
                datapendtask.JenisKelaminBPKB = resultquery.Rows[0]["JenisKelaminBPKB"].ToString();
                datapendtask.NoidentitasBPKB = resultquery.Rows[0]["NoidentitasBPKB"].ToString();
                datapendtask.NoContactBPKB = resultquery.Rows[0]["NoContactBPKB"].ToString();
                datapendtask.AlamatBPKB = resultquery.Rows[0]["AlamatBPKB"].ToString();
                datapendtask.KelurahanBPKB = resultquery.Rows[0]["KelurahanBPKB"].ToString();
                datapendtask.KecamatanBPKB = resultquery.Rows[0]["KecamatanBPKB"].ToString();
                datapendtask.KabupatenKotaBPKB = resultquery.Rows[0]["KabupatenKotaBPKB"].ToString();
                datapendtask.KabupatenKotaBPKBAHU = resultquery.Rows[0]["KabupatenKotaBPKBAHU"].ToString();
                datapendtask.ProvinsiBPKB = resultquery.Rows[0]["ProvinsiBPKB"].ToString();
                datapendtask.PoskodeBPKB = resultquery.Rows[0]["PoskodeBPKB"].ToString();
                datapendtask.RTBPKB = resultquery.Rows[0]["RTBPKB"].ToString();
                datapendtask.RWBPKB = resultquery.Rows[0]["RWBPKB"].ToString();

                datapendtask.Notes = resultquery.Rows[0]["Notes"].ToString();

                datapendtask.cust_type_ahu = resultquery.Rows[0]["CustTypeAhu"].ToString();
                datapendtask.keylookupdata = NoPerjanjian;

                datapendtask.ContNotValid = bool.Parse(resultquery.Rows[0]["ContNotValid"].ToString());
                datapendtask.FillDebitur = bool.Parse(resultquery.Rows[0]["FillDebitur"].ToString());
                datapendtask.OverKontrak = bool.Parse(resultquery.Rows[0]["OverKontrak"].ToString());
                datapendtask.TakeOut = bool.Parse(resultquery.Rows[0]["TakeOut"].ToString());
                datapendtask.multiguna = bool.Parse(resultquery.Rows[0]["multiguna"].ToString());
                datapendtask.Verified = bool.Parse(resultquery.Rows[0]["Verified"].ToString());
                datapendtask.Regular = bool.Parse(resultquery.Rows[0]["Regular"].ToString());

                datapendtask.ContType = int.Parse(resultquery.Rows[0]["conttype"].ToString());
                datapendtask.AssetDesc = resultquery.Rows[0]["AssetDesc"].ToString();
                datapendtask.Roda = int.Parse(resultquery.Rows[0]["Roda"].ToString());

                datapendtask.MesinNumber = resultquery.Rows[0]["MesinNumber"].ToString();
                datapendtask.RangkaNumber = resultquery.Rows[0]["RangkaNumber"].ToString();

                datapendtask.KeteranganObject = resultquery.Rows[0]["KeteranganObject"].ToString();
                datapendtask.WarnaObject = resultquery.Rows[0]["WarnaObject"].ToString();
                datapendtask.NilaiObject = resultquery.Rows[0]["NilaiObject"].ToString();
                datapendtask.NilaiHutang = resultquery.Rows[0]["NilaiHutang"].ToString();
                datapendtask.NilaiJaminan = resultquery.Rows[0]["NilaiJaminan"].ToString();
                datapendtask.TahunObject = resultquery.Rows[0]["TahunObject"].ToString();
                datapendtask.BPKBobject = resultquery.Rows[0]["BPKBobject"].ToString();
                datapendtask.Kondisiobject = resultquery.Rows[0]["Kondisiobject"].ToString();
                datapendtask.MerkObject = resultquery.Rows[0]["MerkOBject"].ToString();
                datapendtask.TipeObject = resultquery.Rows[0]["TipeObject"].ToString();
                datapendtask.KategoriObject = resultquery.Rows[0]["KategoriObject"].ToString();

                datapendtask.NamaRegion = resultquery.Rows[0]["NamaRegion"].ToString();
                datapendtask.NamaCabang = resultquery.Rows[0]["NamaCabang"].ToString();
                datapendtask.NoPerjanjian = resultquery.Rows[0]["NoPerjanjian"].ToString();
                datapendtask.TglPerjanjian = DateTime.Parse(resultquery.Rows[0]["TglPerjanjian"].ToString());

                datapendtask.TglAwalAngsuran = resultquery.Rows[0]["TglAwalAngsur"].ToString();
                datapendtask.TglAkhirAngsuran = resultquery.Rows[0]["TglAkhirAngsur"].ToString();

                datapendtask.DataGIVE = resultquery.Rows[0]["DataGIVE"].ToString();
                datapendtask.StatusChecked = int.Parse(resultquery.Rows[0]["StatusChecked"].ToString());

                ViewBag.nokonview = datapendtask.JenisNasabah + " - " + resultquery.Rows[0]["NamaRegion"].ToString() + "-" + resultquery.Rows[0]["NamaCabang"].ToString() + "/" + resultquery.Rows[0]["NoPerjanjian"].ToString() + " - " + resultquery.Rows[0]["NamaNasabah"].ToString();
                ViewBag.noper = "Data Perjanjian (NO : " + datapendtask.NoPerjanjian + ")";

                ViewBag.contno = noperjan;
                ViewBag.conttype = HasKeyProtect.Encryption(conttype.ToString());
                ViewBag.IDFDCD = fdcid;

                if (datapendtask.StatusChecked != 0)
                {
                    ViewBag.AllowDis = "disabled";
                }

                TempData["dtfile"] = null;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiUpdatePendingTask.cshtml", datapendtask),
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


        public async Task<ActionResult> clnUpdatePendTask(cPendingTask model, string statusdata, string statusdatacheck)
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


                //await Task.Delay(1);
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["PendingTaskListFilter"] = modFilter;
                TempData["PendingTaskList"] = PendingTask;
                TempData["common"] = Common;


                //get value from aply filter //
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
                string SelectStatusCheck = modFilter.SelectStatusCheck;

                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string NoPerjanjian = modFilter.NoPerjanjian;

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
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.SelectStatusCheck = SelectStatusCheck;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;


                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                string NoPerjanjian1 = HasKeyProtect.Decryption(model.SecNoPerjanjian);
                string IDFDC = HasKeyProtect.Decryption(model.secIDFDC);
                string clientid = HasKeyProtect.Decryption(model.clientid);
                string CabangId = HasKeyProtect.Decryption(model.CabangId);

                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);

                //senback to model for proses db
                model.NoPerjanjian = NoPerjanjian1;
                model.IDFDC = IDFDC;
                model.clientid = clientid;
                model.CabangId = CabangId;

                //validasi PendingTask//
                string EnumMessage = "";
                int result = -9090;
                string opened = "show";
                statusdatacheck = statusdatacheck ?? "0";
                //15 NOTOK 20 OK

                if ((SelectStatusCheck ?? "0") == "0")
                {
                    EnumMessage = lgPendingTask.CheckVlaidasiInputPending(model, statusdatacheck);
                }
                else
                {
                    if (int.Parse(statusdatacheck) == (int)HashNetFramework.StatusChecked.CheckDraftFail && (model.Notes ?? "").Length < 15)
                    {
                        EnumMessage = "Silahkan Isikan Catatan";
                    }

                    if (int.Parse(statusdatacheck) == (int)HashNetFramework.StatusChecked.NA &&
                        int.Parse(SelectStatusCheck) == (int)HashNetFramework.StatusChecked.ReadyCheckDraft)
                    {
                        EnumMessage = "Silahkan Pilih Status Pengecekan";
                    }
                }


                if (EnumMessage == "")
                {
                    opened = "hide";

                    result = await PendingTaskddl.dbupdatePendTaskDoc(model, statusdata, statusdatacheck, caption, UserID, GroupName);

                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Kontrak Pending Task", "disimpan") : EnumMessage;

                    if (result == 1)
                    {
                        //refresh//
                        var totalrecord = PendingTask.DTOrdersFromDB.AsEnumerable().Where(x => x.Field<string>("keylookupdata") != model.keylookupdata);
                        PendingTask.DTOrdersFromDB = totalrecord.Count() == 0 ? null : totalrecord.CopyToDataTable();

                        if (PendingTask.DTOrdersFromDB == null)
                        {
                            ////get total data from server//
                            List<String> recordPage = await PendingTaskddl.dbGetPendingTaskListCount(SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, caption, UserID, GroupName);
                            TotalRecord = Convert.ToDouble(recordPage[0]);
                            TotalPage = Convert.ToDouble(recordPage[1]);
                            pagingsizeclient = Convert.ToDouble(recordPage[2]);
                            pagenumberclient = PageNumber;
                        }
                        else
                        {
                            double record = PendingTask.DTOrdersFromDB.Rows.Count;
                            if ((pagenumberclient * pagingsizeclient) >= record)
                            {
                                pagenumberclient = Math.Ceiling(record / pagingsizeclient);
                                TotalPage = Math.Ceiling(record / pagingsizeclient);
                            }
                        }

                        //set paging in grid client//
                        List<DataTable> dtlist = await PendingTaskddl.dbGetPendingTaskList(PendingTask.DTOrdersFromDB, SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                        totalRecordclient = dtlist[0].Rows.Count;
                        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                        //back to set in filter//
                        modFilter.TotalRecord = TotalRecord;
                        modFilter.TotalPage = TotalPage;
                        modFilter.pagingsizeclient = pagingsizeclient;
                        modFilter.totalRecordclient = totalRecordclient;
                        modFilter.totalPageclient = totalPageclient;
                        modFilter.pagenumberclient = pagenumberclient;

                        PendingTask.DTOrdersFromDB = dtlist[0];
                        PendingTask.DTDetailForGrid = dtlist[1];
                        PendingTask.DetailFilter = modFilter;

                        TempData["PendingTaskListFilter"] = modFilter;
                        TempData["PendingTaskList"] = PendingTask;

                    }

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                }


                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiGridPendingTaskList.cshtml", PendingTask),
                    msg = EnumMessage,
                    openshow = opened,
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


        public async Task<ActionResult> clnFollowPendTask(string statusdatacheck)
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


                //await Task.Delay(1);
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                TempData["PendingTaskListFilter"] = modFilter;
                TempData["PendingTaskList"] = PendingTask;
                TempData["common"] = Common;


                //get value from aply filter //
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
                string SelectStatusCheck = modFilter.SelectStatusCheck;

                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string NoPerjanjian = modFilter.NoPerjanjian;

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
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.SelectStatusCheck = SelectStatusCheck;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;


                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);

                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);


                //validasi PendingTask//
                string EnumMessage = "";
                int result = -9090;
                string opened = "show";
                string msgss = "";
                statusdatacheck = statusdatacheck ?? "090";
                //15 NOTOK 20 OK

                if (statusdatacheck == "9XXX$&^")
                {
                    statusdatacheck = "25";
                    msgss = "diajukan proses pembayaran";
                }
                if (statusdatacheck == "6XXX$&^")
                {
                    statusdatacheck = "0";
                    msgss = "diajukan proses Pendaftaran ulang silahkan cek kembali";
                }


                if (statusdatacheck == "25" || statusdatacheck == "0")
                {
                    opened = "hide";
                    result = await PendingTaskddl.dbGetPendingTaskFollowCheck("0", "", "", int.Parse(SelectJenisKontrak), SelectStatusCheck, caption, UserID, GroupName);

                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Kontrak Pending Task", msgss) : EnumMessage;

                    if (result == 1)
                    {

                        ////get total data from server//
                        List<String> recordPage = await PendingTaskddl.dbGetPendingTaskListCount(SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, caption, UserID, GroupName);
                        TotalRecord = Convert.ToDouble(recordPage[0]);
                        TotalPage = Convert.ToDouble(recordPage[1]);
                        pagingsizeclient = Convert.ToDouble(recordPage[2]);
                        pagenumberclient = PageNumber;


                        //set paging in grid client//
                        List<DataTable> dtlist = await PendingTaskddl.dbGetPendingTaskList(null, SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                        totalRecordclient = dtlist[0].Rows.Count;
                        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                        //back to set in filter//
                        modFilter.TotalRecord = TotalRecord;
                        modFilter.TotalPage = TotalPage;
                        modFilter.pagingsizeclient = pagingsizeclient;
                        modFilter.totalRecordclient = totalRecordclient;
                        modFilter.totalPageclient = totalPageclient;
                        modFilter.pagenumberclient = pagenumberclient;

                        PendingTask.DTOrdersFromDB = dtlist[0];
                        PendingTask.DTDetailForGrid = dtlist[1];
                        PendingTask.DetailFilter = modFilter;

                        TempData["PendingTaskListFilter"] = modFilter;
                        TempData["PendingTaskList"] = PendingTask;

                    }

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;

                }
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiGridPendingTaskList.cshtml", PendingTask),
                    msg = EnumMessage,
                    openshow = opened,
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnListFilterPendingTask(cFilterContract model, string download)
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
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string SelectClient = model.SelectClient ?? modFilter.ClientLogin;
                string SelectBranch = model.SelectBranch ?? modFilter.BranchLogin;
                SelectBranch = SelectBranch.Length <= 4 ? HasKeyProtect.Encryption(SelectBranch) : SelectBranch;
                string SelectJenisPelanggan = model.SelectJenisPelanggan ?? "";
                string SelectJenisKontrak = model.SelectJenisKontrak ?? "0";
                string SelectStatusCheck = model.SelectStatusCheck ?? "0";



                string fromdate = model.fromdate ?? "";
                string todate = model.todate ?? "";
                string NoPerjanjian = model.NoPerjanjian ?? "";


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
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.SelectStatusCheck = SelectStatusCheck;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//


                // cek validation for filterisasi //
                string validtxt = lgPendingTask.CheckFilterisasiDataPendTask(modFilter, download);
                if (validtxt == "")
                {

                    //descript some value for db//
                    SelectClient = HasKeyProtect.Decryption(SelectClient);
                    SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    caption = HasKeyProtect.Decryption(caption);

                    List<String> recordPage = await PendingTaskddl.dbGetPendingTaskListCount(SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await PendingTaskddl.dbGetPendingTaskList(null, SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;

                    //set to object pending task//
                    PendingTask.DTOrdersFromDB = dtlist[0];
                    PendingTask.DTDetailForGrid = dtlist[1];
                    PendingTask.DetailFilter = modFilter;

                    TempData["PendingTaskListFilter"] = modFilter;
                    TempData["PendingTaskList"] = PendingTask;
                    TempData["common"] = Common;

                    string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak" + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiGridPendingTaskList.cshtml", PendingTask),
                        download = "",
                        message = validtxt,
                    });
                    //modFilter.TotalRecord == 0 ? EnumsDesc.GetDescriptionEnums((ProccessOutput.RecordNotFound)) : ""

                }
                else
                {

                    TempData["PendingTaskListFilter"] = modFilter;
                    TempData["PendingTaskList"] = PendingTask;
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

        public async Task<ActionResult> clnPendingTaskRgrid(int paged)
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
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
                string SelectStatusCheck = modFilter.SelectStatusCheck;

                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string NoPerjanjian = modFilter.NoPerjanjian;


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

                List<DataTable> dtlist = await PendingTaskddl.dbGetPendingTaskList(PendingTask.DTOrdersFromDB, SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                // set order//
                PendingTask.DTDetailForGrid = dtlist[1];

                //set session filterisasi //
                TempData["PendingTaskList"] = PendingTask;
                TempData["PendingTaskListFilter"] = modFilter;
                TempData["common"] = Common;

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak";

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiGridPendingTaskList.cshtml", PendingTask),
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

        public async Task<ActionResult> clnCheckPemberkasanChkfle(string secnocon, string coontpe, string clnfdc)
        {

            string EnumMessage = "";
            string filenamevar = "View warkah";
            byte[] res = null;
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
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData["PendingTaskList"] = PendingTask;
                TempData["PendingTaskListFilter"] = modFilter;
                TempData["common"] = Common;

                //sumber data pemberkasan //

                string caption = modFilter.idcaption;
                string moduleID = HasKeyProtect.Decryption(caption);
                string mode = moduleID;

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowGenerate).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                if (PendingTask.CheckWithKey == "loadfile")
                {
                    TempData["dtfile"] = null;
                    PendingTask.CheckWithKey = "";
                }
                //cek pada sesion terlebih dulu
                List<cDocumentsGet> dttemp = (TempData["dtfile"] as List<cDocumentsGet>);

                string infokon = "";
                string infofisrst = "";

                int idicategetdb = 1;
                string ID = "0";
                string publicontype = coontpe;
                if ((dttemp == null))
                {
                    idicategetdb = 0;
                    if (coontpe == "chk")
                    {
                        ID = secnocon;
                        secnocon = "";
                        clnfdc = "";
                        coontpe = HasKeyProtect.Encryption("0");
                    }
                }
                else
                {
                    int dttempx = 0;
                    if (coontpe == "chk")
                    {
                        dttempx = dttemp.AsEnumerable().Where(x => x.ID_UPLOAD == secnocon).Count();
                        if (dttempx == 0)
                        {
                            idicategetdb = 0;
                            ID = secnocon;
                            secnocon = "";
                            clnfdc = "";
                            coontpe = HasKeyProtect.Encryption("0");
                        }
                    }
                    else
                    {
                        dttempx = dttemp.AsEnumerable().Where(x => x.cont_no == secnocon && x.cont_type == HasKeyProtect.Decryption(coontpe)).Count();
                        if (dttempx == 0)
                        {
                            idicategetdb = 0;
                        }
                    }
                }
                if (idicategetdb == 0)
                {
                    //load ke DB jika disesion tidak ada//
                    modFilter.ID = ID;
                    modFilter.NoPerjanjian = secnocon;
                    modFilter.SelectJenisKontrak = HasKeyProtect.Decryption(coontpe);
                    modFilter.secIDFDC = clnfdc;
                    modFilter.JenisDocument = "AHU";
                    dttemp = await Commonddl.dbGetViewDokumen(modFilter);
                    modFilter.NoPerjanjian = "";
                    modFilter.SelectJenisKontrak = "";
                    modFilter.secIDFDC = null;
                    modFilter.chalowses = false;
                }
                else
                {
                    infofisrst = "1";
                }

                //simpand di session//
                TempData["dtfile"] = dttemp;
                string viewhml = "";

                if (dttemp.Count > 0)
                {

                    if (publicontype == "chk")
                    {
                        foreach (cDocumentsGet s in dttemp)
                        {
                            infokon = dttemp[0].cont_no.ToString();
                            byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                            string pooo = s.CREATED_BY.ToString();
                            pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                            string KECEP = HasKeyProtect.Encryption(pooo);
                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                            res = imageBytes;
                        }
                    }

                    if (infofisrst == "" && publicontype != "chk")
                    {
                        iTextSharp.text.pdf.PdfReader finalPdf;
                        iTextSharp.text.Document pdfContainer;
                        PdfWriter pdfCopy;
                        MemoryStream msFinalPdf = new MemoryStream();

                        pdfContainer = new iTextSharp.text.Document();
                        pdfCopy = new PdfSmartCopy(pdfContainer, msFinalPdf);
                        pdfContainer.Open();

                        int pageded = 0;
                        foreach (cDocumentsGet s in dttemp)
                        {

                            pageded = pageded + 1;
                            byte[] imageBytes = Convert.FromBase64String(s.FILE_BYTE);
                            string pooo = s.CREATED_BY.ToString();
                            pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                            string KECEP = HasKeyProtect.Encryption(pooo);
                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);

                            finalPdf = new iTextSharp.text.pdf.PdfReader(imageBytes);
                            for (int i = 1; i < finalPdf.NumberOfPages + 1; i++)
                            {
                                ((PdfSmartCopy)pdfCopy).AddPage(pdfCopy.GetImportedPage(finalPdf, i));
                            }
                            pdfCopy.FreeReader(finalPdf);

                            if (pageded == dttemp.Count)
                            {
                                finalPdf.Close();
                            }
                        }
                        pdfCopy.Close();
                        pdfContainer.Close();
                        res = msFinalPdf.ToArray();
                    }
                }
                else
                {

                    EnumMessage = "Dokumen tidak ditemukan";
                }

                var contenttypeed = "application/pdf";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?paridfdc=" + secnocon + "&parmod=" + mode + "&parpowderdockd=wako&infokon=" + infokon + "&file=";
                //viewpathed = "data:application/pdf;base64," + base4;
                var jsonresult = Json(new { view = viewhml, moderror = IsErrorTimeout, infoselect = infofisrst, bytetyipe = res, msg = EnumMessage, contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
                jsonresult.MaxJsonLength = int.MaxValue;
                return jsonresult;

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
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
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


                PendingTask.DetailFilter = modFilter;

                //set session filterisasi //
                TempData["PendingTaskList"] = PendingTask;
                TempData["PendingTaskListFilter"] = modFilter;

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
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiPopupContractDetail.cshtml", PendingTask),
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
        public async Task<ActionResult> clnFollowInvContractRegis(HttpPostedFileBase files, string ContNotValid, string Verified)
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
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string SelectClient = modFilter.SelectClient ?? modFilter.ClientLogin;
                string SelectBranch = modFilter.SelectBranch ?? modFilter.BranchLogin;
                SelectBranch = SelectBranch.Length <= 4 ? HasKeyProtect.Encryption(SelectBranch) : SelectBranch;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan ?? "";
                string SelectJenisKontrak = modFilter.SelectJenisKontrak ?? "0";
                string SelectStatusCheck = modFilter.SelectStatusCheck ?? "0";



                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
                string NoPerjanjian = modFilter.NoPerjanjian ?? "";


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
                modFilter.SelectJenisPelanggan = SelectJenisPelanggan;
                modFilter.SelectJenisKontrak = SelectJenisKontrak;
                modFilter.SelectStatusCheck = SelectStatusCheck;
                modFilter.fromdate = fromdate;
                modFilter.todate = todate;
                modFilter.NoPerjanjian = NoPerjanjian;

                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

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
                    result = await PendingTaskddl.dbGetPendingTaskFollowInvoice(dt, bool.Parse(Verified), bool.Parse(ContNotValid), caption, UserID, GroupName);
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

                        //descript some value for db//
                        SelectClient = HasKeyProtect.Decryption(SelectClient);
                        SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                        caption = HasKeyProtect.Decryption(caption);

                        List<String> recordPage = await PendingTaskddl.dbGetPendingTaskListCount(SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, caption, UserID, GroupName);
                        TotalRecord = Convert.ToDouble(recordPage[0]);
                        TotalPage = Convert.ToDouble(recordPage[1]);
                        pagingsizeclient = Convert.ToDouble(recordPage[2]);
                        pagenumberclient = PageNumber;
                        List<DataTable> dtlist = await PendingTaskddl.dbGetPendingTaskList(null, SelectClient, SelectBranch, fromdate, todate, NoPerjanjian, SelectJenisPelanggan, SelectJenisKontrak, SelectStatusCheck, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                        totalRecordclient = dtlist[0].Rows.Count;
                        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                        //back to set in filter//
                        modFilter.TotalRecord = TotalRecord;
                        modFilter.TotalPage = TotalPage;
                        modFilter.pagingsizeclient = pagingsizeclient;
                        modFilter.totalRecordclient = totalRecordclient;
                        modFilter.totalPageclient = totalPageclient;
                        modFilter.pagenumberclient = pagenumberclient;

                        //set to object pending task//
                        PendingTask.DTOrdersFromDB = dtlist[0];
                        PendingTask.DTDetailForGrid = dtlist[1];
                        PendingTask.DetailFilter = modFilter;

                        TempData["PendingTaskListFilter"] = modFilter;
                        TempData["PendingTaskList"] = PendingTask;
                        TempData["common"] = Common;

                        string filteron = modFilter.isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                        ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak" + filteron;

                    }

                }

                //keep session filterisasi before//
                TempData["PendingTaskList"] = PendingTask;
                TempData["PendingTaskListFilter"] = modFilter;
                TempData["common"] = Common;


                //sendback to client browser//

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = validtxt,
                    result = resulted,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/PendingTask/_uiGridPendingTaskList.cshtml", PendingTask),
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

        public ActionResult ExcelExportPendingTask(string tipemodule = "")
        {

            Account = (vmAccount)Session["Account"];
            Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

            //bool IsErrorTimeout = false;
            if (Account != null)
            {
                if (Account.AccountLogin.RouteName != "")
                {
                    return RedirectToRoute(Account.AccountLogin.RouteName);
                    //IsErrorTimeout = true;
                }
            }
            else
            {
                return RedirectToRoute("DefaultExpired");
            }

            try
            {

                //get session filterisasi //
                modFilter = TempData["PendingTaskListFilter"] as cFilterContract;
                PendingTask = TempData["PendingTaskList"] as vmPendingTask;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;


                TempData["PendingTaskListFilter"] = modFilter;
                TempData["PendingTaskList"] = PendingTask;
                TempData["common"] = Common;

                string UserID = modFilter.UserID;
                string GroupName = modFilter.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string SelectClient = modFilter.SelectClient;
                string SelectBranch = modFilter.SelectBranch;
                string SelectJenisPelanggan = modFilter.SelectJenisPelanggan;
                string SelectJenisKontrak = modFilter.SelectJenisKontrak;
                string SelectStatusCheck = modFilter.SelectStatusCheck;

                string fromdate = modFilter.fromdate;
                string todate = modFilter.todate;
                string NoPerjanjian = modFilter.NoPerjanjian;

                //decript some model apply for DB//
                caption = HasKeyProtect.Decryption(caption);
                SelectClient = HasKeyProtect.Decryption(SelectClient);
                SelectBranch = HasKeyProtect.Decryption(SelectBranch);


                DataTable dt = PendingTaskddl.dbGetPendingTaskListExport(SelectClient, SelectBranch, "", NoPerjanjian, fromdate, todate, "", SelectJenisPelanggan, "", 1, caption, UserID, GroupName);

                StringBuilder sb = new StringBuilder();
                sb = OwinLibrary.ConvertDTToTxt(dt);
                string filename = "PendingTask_" + DateTime.Now.ToString("ddMMyyyssmm") + ".csv";

                //Writing StringBuilder content to an excel file.
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                Response.Buffer = true;
                Response.ClearHeaders();
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8";
                //Response.ContentType = "text/plain"; txt
                Response.ContentType = "application/text";
                //csv
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

                return RedirectToRoute("ErroPage");
            }

        }
        #endregion PendingTask Reguler

    }
}
