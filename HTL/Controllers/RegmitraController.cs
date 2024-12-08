using DocumentFormat.OpenXml.Packaging;
using HashNetFramework;
using Ionic.Zip;
using iTextSharp.text.pdf;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;
using System.Xml.Serialization;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DusColl.Controllers
{
    public class RegmitraController : Controller
    {
        //
        // GET: /Regmitra/

        vmAccount Account = new vmAccount();
        blAccount lgAccount = new blAccount();
        vmRegmitra Regmitra = new vmRegmitra();
        vmRegmitraddl Regmitraddl = new vmRegmitraddl();
        blRegmitraddl Regmitrabl = new blRegmitraddl();

        cFilterContract modFilter = new cFilterContract();
        vmCommon Common = new vmCommon();
        vmCommonddl Commonddl = new vmCommonddl();

        string tempTransksi = "Regmitradtlist";
        string tempTransksifilter = "Regmitralistfilter";
        string tempcommon = "common";
        string MainControllerNameHeaderTx = "Regmitra";
        string MainActionNameHeaderTx = "clnHeaderTx";


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnWFHeaderSve(cRegmitra model, HttpPostedFileBase[] files, string[] documen, string cntpro)
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

            int result = -1;
            string notransaksi = "";

            try
            {

                Regmitra = TempData[tempTransksi] as vmRegmitra;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(modFilter.ModuleID);

                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                //get value from aply filter //
                string email = "";
                string RequestNo = "";
                string fromdate = "";
                string todate = "";
                string keylookupdataDTX = HasKeyProtect.Decryption(model.keylookupdataDTX);
                string keylookupdataHTX = model.keylookupdataHTX;
                string FlagOpr = HasKeyProtect.Decryption(model.FlagOperation);
                FlagOpr = FlagOpr == "" ? "CRETHDR" : FlagOpr;

                //set default for paging //
                int PageNumber = 1;
                double TotalRecord = modFilter.TotalRecord;
                double TotalPage = 0;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double pagenumberclient = modFilter.pagenumberclient;
                double totalRecordclient = 0;
                double totalPageclient = 0;
                bool isModeFilter = modFilter.isModeFilter;

                //decript some model apply for DB//
                //caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                string view1 = "";
                bool IsEditItem = false;

                //saat edit barang//
                string captionsave = caption;
                if (FlagOpr == "REVHDR")
                {
                    captionsave = caption.Replace("WFTODO", "WFINIT");
                    IsEditItem = true;
                }


                //validasi file upload //
                //jumlah file tidak boleh lebih dari 15 file
                //ukuran kurang dari 800 KB
                //extention file jpeg
                //
                model.RegmitraType = FlagOpr == "CRETHDR" ? model.RegmitraType : Regmitra.HeaderInfo.RegmitraType;
                model.IDHeaderTx = Regmitra.HeaderInfo.IDHeaderTx;
                model.RegmitraNo = Regmitra.HeaderInfo.RegmitraNo;
                model.RegmitraCreatedBy = Regmitra.HeaderInfo.RegmitraCreatedBy;
                model.StatusFollow = model.StatusFollow ?? Regmitra.HeaderInfo.RegmitraStatus;
                model.RegmitraType = model.RegmitraType ?? Regmitra.HeaderInfo.RegmitraType;
                model.Area = model.Area ?? Regmitra.HeaderInfo.Area;
                model.Cabang = model.Cabang ?? Regmitra.HeaderInfo.Cabang;
                model.Divisi = model.Divisi ?? Regmitra.HeaderInfo.Divisi;
                model.DivisiName = model.DivisiName ?? Regmitra.HeaderInfo.DivisiName;
                model.tglmasuk = model.tglmasuk ?? Regmitra.HeaderInfo.tglmasuk;
                model.tglakhir = model.tglakhir ?? Regmitra.HeaderInfo.tglakhir;

                model.NamaMitra = model.NamaMitra ?? Regmitra.HeaderInfo.NamaMitra;
                model.NoKTP = model.NoKTP ?? Regmitra.HeaderInfo.NoKTP;
                model.Alamat = model.Alamat ?? Regmitra.HeaderInfo.Alamat;
                model.AlamatKorespodensi = model.AlamatKorespodensi ?? Regmitra.HeaderInfo.AlamatKorespodensi;
                model.NoNPWP = model.NoNPWP ?? Regmitra.HeaderInfo.NoNPWP;
                model.handleJob = model.handleJob ?? Regmitra.HeaderInfo.handleJob;
                model.JenisKelamin = model.JenisKelamin ?? Regmitra.HeaderInfo.JenisKelamin;
                model.Pendidikan = model.Pendidikan ?? Regmitra.HeaderInfo.Pendidikan;
                model.StatusKawin = model.StatusKawin ?? Regmitra.HeaderInfo.StatusKawin;
                model.Tempatlahir = model.Tempatlahir ?? Regmitra.HeaderInfo.Tempatlahir;
                model.Tgllahir = model.Tgllahir ?? Regmitra.HeaderInfo.Tgllahir;
                model.RegmitraStatus = model.RegmitraStatus ?? Regmitra.HeaderInfo.RegmitraStatus;
                model.NIKLama = model.NIKLama ?? Regmitra.HeaderInfo.NIKLama;
                model.NIKBaru = model.NIKBaru ?? Regmitra.HeaderInfo.NIKBaru;
                model.NoSPPI = model.NoSPPI ?? Regmitra.HeaderInfo.NoSPPI;
                model.StatusDate = model.StatusDate ?? Regmitra.HeaderInfo.StatusDate;

                model.NamaBank = model.NamaBank ?? Regmitra.HeaderInfo.NamaBank;
                model.CabangBank = model.CabangBank ?? Regmitra.HeaderInfo.CabangBank;
                model.Norekening = model.Norekening ?? Regmitra.HeaderInfo.Norekening;
                model.Pemilikkening = model.Pemilikkening ?? Regmitra.HeaderInfo.Pemilikkening;

                string valid = "";

                //get data mitra lama//
                string nm = HasKeyProtect.Decryption(model.keylookupdataDTX);
                DataTable dtu = await Commonddl.dbdbGetDdlMitraListByEncrypt(nm, "", "40", captionsave, UserID, GroupName);
                DataRow drx = dtu.AsEnumerable().SingleOrDefault();
                cRegmitra mitraold = new cRegmitra();
                if (drx != null)
                {
                    mitraold.tglmasuk = drx["tglmasuk"].ToString();
                    mitraold.tglakhir = drx["tglakhir"].ToString();
                    mitraold.contractnobefore = drx["ContractNo"].ToString();
                    mitraold.tglmasukbefore = drx["tglmasuk"].ToString();
                    mitraold.tglakhirbefore = drx["tglakhir"].ToString();

                    mitraold.divisinamebefore = drx["Divisi_Name"].ToString();
                    mitraold.cabangcodenamebefore = drx["brch_code"].ToString();
                    mitraold.cabangnamebefore = drx["brch_code"].ToString() + "-" + drx["brch_name"].ToString();
                    mitraold.regionnamebefore = drx["Region_Name"].ToString();
                    mitraold.handlejobnamebefore = drx["JobDesc"].ToString();

                    mitraold.NoKTP = drx["NoKTP"].ToString();
                    mitraold.NoNPWP = drx["NoNPWP"].ToString();
                    mitraold.Alamat = drx["Alamat"].ToString();
                    mitraold.AlamatKorespodensi = drx["AlamatKorespodensi"].ToString();

                    mitraold.Tempatlahir = drx["Tempatlahir"].ToString();
                    mitraold.Tgllahir = drx["Tgllahir"].ToString();
                    mitraold.JenisKelamin = drx["JenisKelamin"].ToString();

                    mitraold.NamaBank = drx["NamaBank"].ToString();
                    mitraold.CabangBank = drx["CabangBank"].ToString();
                    mitraold.Norekening = drx["Norekening"].ToString();
                    mitraold.Pemilikkening = drx["Pemilikkening"].ToString();
                    mitraold.RegmitraStatus = drx["StatusMitra"].ToString();

                    mitraold.NoSPPI = drx["NoSPPI"].ToString();
                    mitraold.NoFAX = drx["NoFAX"].ToString();
                    mitraold.NoHP = drx["NoHP"].ToString();
                    mitraold.NoHP1 = drx["NoHP1"].ToString();
                    mitraold.NoWA = drx["NoWA"].ToString();
                    mitraold.NamaFB = drx["NamaFB"].ToString();

                    mitraold.StatusDate = drx["StatusDate"].ToString();
                    mitraold.Email = drx["Email"].ToString();

                }
                else
                {
                    mitraold = Regmitra.HeaderInfo;
                }

                //jangan validasi jika tindakan 'cancel,rejectd'
                if (int.Parse(model.StatusFollow) != (int)HashNetFramework.StatusDocTrans.CANCELLED && int.Parse(Regmitra.HeaderInfo.StatusFollow) != (int)HashNetFramework.StatusDocTrans.REJECT)
                {
                    valid = await Regmitrabl.dbgetvalidate(model, mitraold, files, documen, Regmitra.DTDokumen, FlagOpr, Regmitra.HeaderInfo.tglakhirbefore, captionsave, UserID, GroupName);
                }
                string EnumMessage = "";
                string urlpath = "";
                if (valid == "")
                {

                    RequestNo = model.RegmitraNo;
                    model.FlagOperation = FlagOpr;
                    model.keylookupdataDTX = HasKeyProtect.Decryption(model.keylookupdataDTX);
                    string RegType = model.RegmitraType;

                    DataTable dt = await Regmitraddl.dbSaveRegMitra(model, captionsave, UserID, GroupName);
                    result = int.Parse(dt.Rows[0][0].ToString());
                    notransaksi = dt.Rows[0][1].ToString();
                    string Noktp = model.NoKTP;

                    //if (result == 1 && files != null)
                    //{
                    //    var idoc = 0;
                    //    foreach (HttpPostedFileBase file in files)
                    //    {

                    //        byte[] imagebyte = null;
                    //        BinaryReader reader = new BinaryReader(file.InputStream);
                    //        imagebyte = reader.ReadBytes((int)file.ContentLength);

                    //        //get mimne type
                    //        string mimeType = file.ContentType;
                    //        if (mimeType.Contains("image"))
                    //        {
                    //            imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                    //        }

                    //        //prepare to encrypt
                    //        string KECEP = "dodol";
                    //        string KECEPDB = KECEP;
                    //        KECEP = HasKeyProtect.Encryption(KECEP);
                    //        byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                    //        //convert byte to base//
                    //        string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                    //        string DocumentType = HasKeyProtect.Decryption(documen[idoc]);
                    //        string FileName = DocumentType + ".pdf";

                    //        string ContentType = "Application/pdf";
                    //        string ContentLength = filebyteECP.Length.ToString();
                    //        string FileByte = base64String;
                    //        DataTable dtx = await Regmitraddl.dbSaveRegMitradoc("0", FlagOpr, "", Noktp, notransaksi, RegType, DocumentType, FileName, ContentType, ContentLength, FileByte, captionsave, UserID, GroupName);

                    //        idoc = idoc + int.Parse(dtx.Rows[0][0].ToString());
                    //        // disini ditambahkan jika gagal menyimpan do
                    //    }

                    //    if (files.Count() != int.Parse(idoc.ToString()))
                    //    {
                    //        EnumMessage = "Terdapat Dokumen yang tidak terupload silahkan dicek kembali";
                    //    }

                    //}

                    string msgadd = "";
                    if (notransaksi.Contains("DARFT"))
                    {
                        msgadd = " dan pengajuan anda perlu dilengkapi ";
                    }

                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);

                    string msg1 = (FlagOpr ?? "") == "" ? "Penambahan barang" : (FlagOpr ?? "") == "CRETHDR" ? "Pengajuan " : "Pengajuan  No Workflow #" + notransaksi;
                    string msg2 = (FlagOpr ?? "") == "" ? "disimpan" : (FlagOpr ?? "") == "CRETHDR" ? "diproses dengan No Workflow #" + notransaksi : "diproses";



                    EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, msg1, msg2 + msgadd) : EnumMessage;

                    if (result == 2601 || result == 2627)
                    {
                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.RegMitraProsesDuplicate));
                    }

                    if (FlagOpr == "CRETHDR")
                    {
                        urlpath = "/";
                    }
                    else
                    {
                        vmHome Home = new vmHome();
                        Home.TodoUser = await Commonddl.dbGetApprovalTodo("1", caption, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                        view1 = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_HomeTodoUser.cshtml", Home);
                    }
                }
                else
                {
                    view1 = "";
                    EnumMessage = valid;
                }

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = view1,
                    msg = EnumMessage,
                    resulted = result,
                    flag = FlagOpr,
                    idhome = "",
                    modl = caption,
                    url = urlpath,
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });

                msg = result == 1 ? "No Pengajuan " + notransaksi + " Terjadi Masalah Pada saat menyimpan data dokumen, silahkan lengkapi dokumen kembali Pada Riwayat Pengajuan " : "";
                if (IsErrorTimeout == false)
                {
                    msg = result == 1 ? "No Pengajuan " + notransaksi + " Terjadi Masalah Pada saat menyimpan data dokumen, silahkan lengkapi data kembali Pada Riwayat Pengajuan " : "";
                    Response.StatusCode = 406;
                    Response.TrySkipIisCustomErrors = true;
                    urlpath = Url.Action("Index", "Error", new { area = "" });
                }

                return Json(new
                {
                    mesage = msg,
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> clnWFAttchSve(cRegmitra model, HttpPostedFileBase files)
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

                Regmitra = TempData[tempTransksi] as vmRegmitra;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(modFilter.ModuleID);

                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                //get value from aply filter //
                string email = "";
                string RequestNo = "";
                string fromdate = "";
                string todate = "";
                string keylookupdataDTX = model.keylookupdataDTX;
                string keylookupdataHTX = model.keylookupdataHTX;
                string FlagOpr = HasKeyProtect.Decryption(model.FlagOperation);
                FlagOpr = FlagOpr == "" ? "CRETHDR" : FlagOpr;

                //set default for paging //
                int PageNumber = 1;
                double TotalRecord = modFilter.TotalRecord;
                double TotalPage = 0;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double pagenumberclient = modFilter.pagenumberclient;
                double totalRecordclient = 0;
                double totalPageclient = 0;
                bool isModeFilter = modFilter.isModeFilter;

                //decript some model apply for DB//
                //caption = HasKeyProtect.Decryption(caption);

                DataRow dr;
                int ID = 0;
                string view1 = "";
                bool IsEditItem = false;

                //saat edit barang//
                string captionsave = caption;
                if (FlagOpr == "REVHDR")
                {
                    captionsave = caption.Replace("WFTODO", "WFINIT");
                    IsEditItem = true;
                }

                model.IDHeaderTx = Regmitra.HeaderInfo.IDHeaderTx;
                model.RegmitraNo = Regmitra.HeaderInfo.RegmitraNo;
                model.RegmitraCreatedBy = Regmitra.HeaderInfo.RegmitraCreatedBy;
                model.StatusFollow = model.StatusFollow ?? Regmitra.HeaderInfo.RegmitraStatus;

                RequestNo = model.RegmitraNo;
                model.FlagOperation = FlagOpr;

                DataTable dt = await Regmitraddl.dbSaveRegMitra(model, captionsave, UserID, GroupName);
                int result = int.Parse(dt.Rows[0][0].ToString());
                string notransaksi = dt.Rows[0][1].ToString();

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                string msg1 = (FlagOpr ?? "") == "" ? "Penambahan barang" : (FlagOpr ?? "") == "CRETHDR" ? "Pengajuan " : "Pengajuan  No.Transaksi #" + notransaksi;
                string msg2 = (FlagOpr ?? "") == "" ? "disimpan" : (FlagOpr ?? "") == "CRETHDR" ? "diproses dengan No.Transaksi #" + notransaksi : "diproses";
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, msg1, msg2) : EnumMessage;

                vmHome Home = new vmHome();
                Home.TodoUser = await Commonddl.dbGetApprovalTodo("1", caption, Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                view1 = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_HomeTodoUser.cshtml", Home);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = view1,
                    msg = EnumMessage,
                    resulted = result,
                    flag = FlagOpr,
                    idhome = "",
                    modl = caption
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
        public async Task<ActionResult> clnWFHeaderInitiateTx(String menu, String caption)
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
                string Area = Account.AccountLogin.Area;
                string AreaName = Account.AccountLogin.AreaName;
                string Divisi = Account.AccountLogin.Divisi;
                string DivisiName = Account.AccountLogin.DivisiName;
                string Mailed = Account.AccountLogin.Mailed;
                string GenMoon = Account.AccountLogin.GenMoon;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string idcaption = HasKeyProtect.Encryption(caption);

                // extend //
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                if (Common.ddlDevisi == null)
                {
                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", caption, UserID, GroupName);
                }

                if (Common.ddlRegion == null)
                {
                    Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("", "", caption, UserID, GroupName);
                }

                if (Common.ddlBranch == null)
                {
                    Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", "", "", caption, UserID, GroupName);
                }

                if (Common.ddlJenKel == null)
                {
                    Common.ddlJenKel = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENKEL", caption, UserID, GroupName);
                }

                if (Common.ddlStatKW == null)
                {
                    Common.ddlStatKW = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATKW", caption, UserID, GroupName);
                }

                if (Common.ddlJenPen == null)
                {
                    Common.ddlJenPen = await Commonddl.dbdbGetDdlpendidikanListByEncrypt("", UserID);
                }

                if (Common.ddlRegmitraType == null)
                {
                    Common.ddlRegmitraType = await Commonddl.dbdbGetDdlEnumsListByEncrypt("REGMTYPE", caption, UserID, GroupName);
                }

                if (Common.ddlBank == null)
                {
                    Common.ddlBank = await Commonddl.dbdbGetDdlbankNameListByEncrypt("", UserID);
                }

                if (Common.ddlJobs == null)
                {
                    Common.ddlJobs = await Commonddl.dbdbGetDdlhandleJobListByEncrypt("", "", "", caption, UserID, GroupName);
                }

                //if (Common.ddlBankBranch == null)
                //{
                //    Common.ddlBankBranch = await Commonddl.dbdbGetDdlBranchBranchListByEncrypt("", caption, UserID, GroupName);
                //}

                //if (Regmitra.DTDokumen == null)
                {
                    Regmitra.DTDokumen = await Commonddl.dbdbGetDokumenList("0", "INIT", "INIT", "1", "", "", caption, UserID, GroupName);
                    Common.ddlDocument = await Commonddl.dbdbGetDokumenListCek(Regmitra.DTDokumen);
                }

                if (Common.ddlStatusMitra == null)
                {
                    Common.ddlStatusMitra = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATMITRA", caption, UserID, GroupName);
                    Common.ddlStatusMitra = Common.ddlStatusMitra.AsEnumerable().Where(x => int.Parse(x.Value) < 10).ToList();
                }

                if (Common.ddlFollow == null)
                {
                    Common.ddlFollow = await Commonddl.dbgetDdlparamenumsList("FOLLOW");
                }
                Common.ddlFollow = Common.ddlFollow.Where(x => x.Text == "Submit" || x.Text == "Submit Draft");


                ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenKel);
                ViewData["SelectStatKawin"] = OwinLibrary.Get_SelectListItem(Common.ddlStatKW);
                ViewData["SelectPendidikan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenPen);
                ViewData["SelectNamaBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBank);
                //ViewData["SelectCabangBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBankBranch);
                ViewData["Selecthandlejob"] = OwinLibrary.Get_SelectListItem(Common.ddlJobs);
                ViewData["SelectFollow"] = OwinLibrary.Get_SelectListItem(Common.ddlFollow);
                ViewData["SelectType"] = OwinLibrary.Get_SelectListItem(Common.ddlRegmitraType);
                ViewData["SelectStatusMitra"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusMitra);
                ViewData["SelectDocumentReg"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                modFilter.ModuleID = idcaption;
                modFilter.UserTypes = UserTypes;

                //set session filterisasi //
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = MainControllerNameHeaderTx;
                ViewBag.action = MainActionNameHeaderTx;

                ViewBag.menuback = menu;
                ViewBag.captionback = caption.Replace("LST", "").Replace("WFINIT", "WFTODO");
                ViewBag.captiondescback = menuitemdescription;
                ViewBag.ruteback = "Home";
                ViewBag.actionback = "clnHomeTodo";

                Regmitra.HeaderInfo = new cRegmitra();
                Regmitra.HeaderInfo.StatusDoc = "0";
                Regmitra.HeaderInfo.Area = Area;
                Regmitra.HeaderInfo.Cabang = HasKeyProtect.Decryption(IDCabang);
                Regmitra.HeaderInfo.Divisi = Divisi;
                Regmitra.HeaderInfo.RegmitraCreatedBy = UserID;
                Regmitra.HeaderInfo.StatusDoc = "0";
                Regmitra.HeaderInfo.FlagOperation = HasKeyProtect.Encryption("CRETHDR");
                Regmitra.HeaderInfo.keylookupdataHTX = "0";
                Regmitra.HeaderInfo.keylookupdataDTX = HasKeyProtect.Encryption("0");
                Regmitra.HeaderInfo.StatusFollow = "0";
                Regmitra.HeaderInfo.IsPICApproval = false;
                Regmitra.HeaderInfo.Dokumen = Regmitra.DTDokumen;
                Regmitra.HeaderInfo.AllowEdit = "init";
                Regmitra.HeaderInfo.UserTypeInit = int.Parse(UserTypes);

                ViewBag.shodoc = "yes";
                ViewBag.listdoc = "no";
                ViewBag.disabled = "";
                ViewBag.CurModule = caption;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/WFRegmitview.cshtml", Regmitra),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                OwinLibrary.CreateLog("error", "LogErrorPUB.txt");

                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorPUB.txt");
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
        public async Task<ActionResult> clnWFHeaderTxView(string module, string curmodule, string paramkey, string mode = "")
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
                modFilter = (TempData[tempTransksifilter] as cFilterContract);
                modFilter = modFilter == null ? new cFilterContract() : modFilter;
                Regmitra = (TempData[tempTransksi] as vmRegmitra);
                Regmitra = Regmitra == null ? new vmRegmitra() : Regmitra;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;



                string returnview = "";

                string UserID = Account.AccountLogin.UserID;
                string UserName = Account.AccountLogin.UserName;
                string ClientID = Account.AccountLogin.ClientID;
                string IDCabang = Account.AccountLogin.IDCabang;
                string Area = Account.AccountLogin.Area;
                string Divisi = Account.AccountLogin.Divisi;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                string GroupName = Account.AccountLogin.GroupName;
                string caption = HasKeyProtect.Decryption(module);
                string curcaption = HasKeyProtect.Decryption(curmodule);
                string tempfilter = caption.Contains("TODODASH") ? "TODODASH" : "";
                caption = caption.Replace("TODODASH", "");

                mode = (mode ?? "");
                if (1 == 1)
                {
                    module = caption;
                    string RegquestNo = "";

                    // set default for paging//
                    int PageNumber = 1;
                    double TotalRecord = 0;
                    double TotalPage = 0;
                    double pagingsizeclient = 0;
                    double pagenumberclient = 0;
                    double totalRecordclient = 0;
                    double totalPageclient = 0;

                    //set header//
                    DataRow dr;
                    Regmitra = (Regmitra == null) ? new vmRegmitra() : Regmitra;
                    modFilter = (modFilter == null) ? new cFilterContract() : modFilter;
                    Regmitra.HeaderInfo = new cRegmitra();

                    // extend //
                    cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                    string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == curcaption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                    // extend //
                    bool istransaksi = false;
                    if (module.Contains("LST"))
                    {
                        ViewBag.menu = "";
                        dr = Regmitra.DTHeaderTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                        istransaksi = true;
                    }
                    else
                    {
                        //set caption view//
                        ViewBag.menu = "(Mitra)";
                        ViewBag.caption = caption;
                        ViewBag.captiondesc = menuitemdescription + ViewBag.menu;
                        ViewBag.rute = MainControllerNameHeaderTx;
                        ViewBag.action = MainActionNameHeaderTx;


                        List<DataTable> dtlistpop = await Regmitraddl.dbGetHeaderTxList(null, paramkey, "", "", "", "", "", 1, 1, 1, curcaption, UserID, GroupName);
                        dr = dtlistpop[0].AsEnumerable().Where(x => x.Field<string>("RegNo") == paramkey).SingleOrDefault();

                        //set caption view//
                        if (mode != "pre")
                        {
                            caption = caption.Replace("LST", "");
                            MainControllerNameHeaderTx = "Home";
                            MainActionNameHeaderTx = "clnHomeTodo";
                            Regmitra.DTHeaderTx = dtlistpop[0];
                        }
                    }

                    if (dr != null)
                    {

                        if (istransaksi == false)
                        {
                            RegquestNo = dr["RegNo"].ToString();
                            Regmitra.HeaderInfo.RegmitraNo = dr["RegNo"].ToString();
                            Regmitra.HeaderInfo.RegmitraDate = dr["RegDate"].ToString();
                            Regmitra.HeaderInfo.RegmitraType = dr["RegType"].ToString();
                            Regmitra.HeaderInfo.RegtypeDesc = dr["RegtypeDesc"].ToString();
                            Regmitra.HeaderInfo.RegmitraType = dr["Regtype"].ToString();
                            Regmitra.HeaderInfo.keylookupdataDTX = HasKeyProtect.Encryption(dr["RefID"].ToString());

                            //ambil ref u data lama sebelumnya
                            if (int.Parse(Regmitra.HeaderInfo.RegmitraType) != (int)HashNetFramework.RequetsTransMitra.BARU)
                            {
                                string paramkeyold = dr["RefID"].ToString();
                                DataTable dtlistpopold;// = await Commonddl.dbdbGetDdlMitraListByEncrypt(paramkeyold, "", Regmitra.HeaderInfo.RegmitraType, curcaption, UserID, GroupName);
                                DataRow drx = dtlistpopold.AsEnumerable().SingleOrDefault();
                                if (drx != null)
                                {
                                    Regmitra.HeaderInfo.contractnobefore = drx["ContractNo"].ToString();
                                    Regmitra.HeaderInfo.tglmasukbefore = drx["tglmasuk"].ToString();
                                    Regmitra.HeaderInfo.tglakhirbefore = drx["tglakhir"].ToString();

                                    Regmitra.HeaderInfo.divisinamebefore = drx["Divisi_Name"].ToString();
                                    Regmitra.HeaderInfo.cabangnamebefore = dr["brch_code"].ToString() + "-" + drx["brch_name"].ToString();
                                    Regmitra.HeaderInfo.cabangcodenamebefore = drx["brch_code"].ToString();
                                    Regmitra.HeaderInfo.regionnamebefore = drx["Region_Name"].ToString();
                                    Regmitra.HeaderInfo.handlejobnamebefore = drx["JobDesc"].ToString();

                                }
                                else
                                {
                                    Regmitra.HeaderInfo.contractnobefore = dr["ContractNo"].ToString();
                                    Regmitra.HeaderInfo.tglmasukbefore = dr["tglmasuk"].ToString();
                                    Regmitra.HeaderInfo.tglakhirbefore = dr["tglakhir"].ToString();

                                    Regmitra.HeaderInfo.divisinamebefore = dr["Divisi_Name"].ToString();
                                    Regmitra.HeaderInfo.cabangnamebefore = dr["brch_code"].ToString() + "-" + dr["brch_name"].ToString();
                                    Regmitra.HeaderInfo.cabangcodenamebefore = dr["brch_code"].ToString();
                                    Regmitra.HeaderInfo.regionnamebefore = dr["Region_Name"].ToString();
                                    Regmitra.HeaderInfo.handlejobnamebefore = dr["JobDesc"].ToString();
                                }
                            }
                        }
                        else
                        {
                            RegquestNo = dr["ContractNo"].ToString();
                            Regmitra.HeaderInfo.RegmitraNo = "";
                            Regmitra.HeaderInfo.RegmitraDate = "";
                            Regmitra.HeaderInfo.ContractNo = dr["ContractNo"].ToString();
                            Regmitra.HeaderInfo.ContractDate = dr["ContractDate"].ToString();
                        }

                        Regmitra.HeaderInfo.keylookupdataHTX = dr["keylookupdata"].ToString();
                        Regmitra.HeaderInfo.IDHeaderTx = int.Parse(dr["Id"].ToString());
                        Regmitra.HeaderInfo.IDDetailTx = int.Parse(dr["Id"].ToString());

                        Regmitra.HeaderInfo.CabangInit = dr["CabangInit"].ToString();
                        Regmitra.HeaderInfo.RegionInit = dr["RegionInit"].ToString();
                        Regmitra.HeaderInfo.UserTypeInit = int.Parse(dr["UserTypeInit"].ToString());

                        Regmitra.HeaderInfo.Divisi = dr["Divisi"].ToString();
                        Regmitra.HeaderInfo.DivisiName = dr["Divisi_Name"].ToString();
                        Regmitra.HeaderInfo.Area = dr["Area"].ToString();
                        Regmitra.HeaderInfo.AreaName = dr["Region_Name"].ToString();
                        Regmitra.HeaderInfo.Cabang = dr["Cabang"].ToString();
                        Regmitra.HeaderInfo.CabangDesc = dr["brch_code"].ToString() + "-" + dr["brch_name"].ToString();
                        Regmitra.HeaderInfo.handleJob = dr["handlejob"].ToString();
                        Regmitra.HeaderInfo.handleJobDesc = dr["JobDesc"].ToString();

                        Regmitra.HeaderInfo.tglmasuk = dr["tglmasuk"].ToString();
                        Regmitra.HeaderInfo.tglakhir = dr["tglakhir"].ToString();

                        Regmitra.HeaderInfo.NamaMitra = dr["NamaMitra"].ToString();
                        Regmitra.HeaderInfo.NoKTP = dr["NoKTP"].ToString();
                        Regmitra.HeaderInfo.NoNPWP = dr["NoNPWP"].ToString();
                        Regmitra.HeaderInfo.Alamat = dr["Alamat"].ToString();
                        Regmitra.HeaderInfo.AlamatKorespodensi = dr["AlamatKorespodensi"].ToString();
                        Regmitra.HeaderInfo.Tempatlahir = dr["Tempatlahir"].ToString();
                        Regmitra.HeaderInfo.Tgllahir = dr["Tgllahir"].ToString();
                        Regmitra.HeaderInfo.JenisKelamin = dr["JenisKelamin"].ToString();
                        Regmitra.HeaderInfo.JenisKelaminDesc = dr["JenisKelaminDesc"].ToString();
                        Regmitra.HeaderInfo.Pendidikan = dr["Pendidikan"].ToString();
                        Regmitra.HeaderInfo.PendidikanDesc = dr["PendidikanDesc"].ToString();
                        Regmitra.HeaderInfo.StatusKawin = dr["StatusKawin"].ToString();
                        Regmitra.HeaderInfo.StatusKawinDesc = dr["StatusKawinDesc"].ToString();
                        Regmitra.HeaderInfo.NamaBank = dr["NamaBank"].ToString();
                        Regmitra.HeaderInfo.NamaBankDesc = dr["NamaBankDesc"].ToString();
                        Regmitra.HeaderInfo.CabangBank = dr["CabangBank"].ToString();
                        Regmitra.HeaderInfo.CabangBankDesc = dr["CabangBankDesc"].ToString();

                        Regmitra.HeaderInfo.Norekening = dr["Norekening"].ToString();
                        Regmitra.HeaderInfo.Pemilikkening = dr["Pemilikkening"].ToString();
                        Regmitra.HeaderInfo.NoSPPI = dr["NoSPPI"].ToString();
                        Regmitra.HeaderInfo.NoFAX = dr["NoFAX"].ToString();
                        Regmitra.HeaderInfo.NoHP = dr["NoHP"].ToString();
                        Regmitra.HeaderInfo.NoHP1 = dr["NoHP1"].ToString();
                        Regmitra.HeaderInfo.NoWA = dr["NoWA"].ToString();
                        Regmitra.HeaderInfo.Email = dr["Email"].ToString();
                        Regmitra.HeaderInfo.NIKLama = dr["NIKLama"].ToString();
                        Regmitra.HeaderInfo.NIKBaru = dr["NIKBaru"].ToString();
                        Regmitra.HeaderInfo.NamaFB = dr["NamaFB"].ToString();
                        Regmitra.HeaderInfo.RegmitraStatus = dr["RegStatus"].ToString();
                        Regmitra.HeaderInfo.StatusDoc = dr["StatusDoc"].ToString();
                        Regmitra.HeaderInfo.StatusDocDesc = dr["StatusDocDesc"].ToString();
                        Regmitra.HeaderInfo.RegmitraStatus = dr["StatusMitra"].ToString();
                        Regmitra.HeaderInfo.RegmitraStatusDesc = dr["StatusMitraDesc"].ToString();
                        Regmitra.HeaderInfo.RegmitraCreatedBy = dr["CreatedBy"].ToString();
                        Regmitra.HeaderInfo.StatusFollow = dr["RegStatus"].ToString();
                        Regmitra.HeaderInfo.StatusDate = dr["StatusDate"].ToString();

                        Divisi = Regmitra.HeaderInfo.Divisi;
                        Area = Regmitra.HeaderInfo.Area;
                        IDCabang = HasKeyProtect.Encryption(Regmitra.HeaderInfo.Cabang);

                        string pendidikan = Regmitra.HeaderInfo.Pendidikan;
                        string jeniskelamin = Regmitra.HeaderInfo.JenisKelamin;
                        string handleJob = Regmitra.HeaderInfo.handleJob;

                        // try show filter log approval//
                        PageNumber = 1;
                        TotalRecord = 0;
                        TotalPage = 0;
                        pagingsizeclient = 0;
                        pagenumberclient = 0;
                        totalRecordclient = 0;
                        totalPageclient = 0;

                        ViewBag.CurModule = curcaption;
                        string captionwf = caption;
                        Regmitra.HeaderInfo.IsPICApproval = await Commonddl.dbGetApprovalCheck(RegquestNo, captionwf, UserID, GroupName);
                        List<String> recordPage = await Commonddl.dbGetApprovalLogListCount(RegquestNo, PageNumber, captionwf, UserID, GroupName);
                        TotalRecord = Convert.ToDouble(recordPage[0]);
                        TotalPage = Convert.ToDouble(recordPage[1]);
                        pagingsizeclient = Convert.ToDouble(recordPage[2]);
                        pagenumberclient = PageNumber;
                        List<DataTable> dtlist = await Commonddl.dbGetApprovalLogList(null, RegquestNo, PageNumber, pagenumberclient, pagingsizeclient, captionwf, UserID, GroupName);
                        totalRecordclient = dtlist[0].Rows.Count;
                        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                        Regmitra.DTLogTx = dtlist[1];
                        Regmitra.Permission = PermisionModule;

                        //set in filter for paging//
                        modFilter.TotalRecordLog = TotalRecord;
                        modFilter.TotalPageLog = TotalPage;
                        modFilter.pagingsizeclientLog = pagingsizeclient;
                        modFilter.totalRecordclientLog = totalRecordclient;
                        modFilter.totalPageclientLog = totalPageclient;
                        modFilter.pagenumberclientLog = pagenumberclient;

                        modFilter.ModuleID = HasKeyProtect.Encryption(caption);
                        modFilter.idcaption = modFilter.ModuleID;
                        Regmitra.FilterTransaksi = modFilter;


                        //set allow edit view //
                        string flagopr = "";
                        if (!module.Contains("LST"))
                        {
                            if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.WAITAPRV && Regmitra.HeaderInfo.IsPICApproval == true && curcaption.Contains("TODO"))
                            {
                                flagopr = "APRHDR";
                            }
                            else if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.REVISE)
                            {
                                flagopr = "REVHDR";
                            }
                            else if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.PENDTASK)
                            {
                                flagopr = "REVHDRDRAFT";
                            }

                            Common.ddlFollow = await Commonddl.dbgetDdlparamenumsList("FOLLOW");
                            if (flagopr == "REVHDR")
                            {
                                Common.ddlFollow = Common.ddlFollow.Where(x => x.Text == "Submit" || x.Text == "Cancelled" || x.Text == "Submit Draft");

                            }
                            else
                            {
                                if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.WAITAPRV)
                                {
                                    Common.ddlFollow = Common.ddlFollow.Where(x => x.Text != "Submit").Where(x => x.Text != "Cancelled" && x.Text != "Submit Draft").Where(x => x.Text != "Approve");
                                }
                                else if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.WAITEMPLID)
                                {
                                    Common.ddlFollow = Common.ddlFollow.Where(x => x.Text == "Approve");
                                }
                                else if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.PENDTASK)
                                {
                                    Common.ddlFollow = Common.ddlFollow.Where(x => x.Text == "Submit" || x.Text == "Cancelled" || x.Text == "Submit Draft");
                                }
                                else
                                {
                                    Common.ddlFollow = Common.ddlFollow.Where(x => x.Text != "Submit").Where(x => x.Text != "Cancelled" && x.Text != "Submit Draft");
                                }
                            }
                            Regmitra.HeaderInfo.FlagOperation = HasKeyProtect.Encryption(flagopr);

                            //untuk tipe dokumen yang baru dibuat akan muncul pd saat revisi & create & pendingtask//
                            string docinit = RegquestNo;
                            if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.REVISE || int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.PENDTASK)
                            {
                                docinit = "INIT";
                            }

                            Regmitra.DTDokumen = await Commonddl.dbdbGetDokumenList("0", RegquestNo, docinit, "1", Regmitra.HeaderInfo.Divisi, Regmitra.HeaderInfo.RegmitraType, caption, UserID, GroupName);
                            Regmitra.HeaderInfo.Dokumen = Regmitra.DTDokumen;


                            Common.ddlDocument = await Commonddl.dbdbGetDokumenListCek(Regmitra.DTDokumen);
                            ViewData["SelectDocumentReg"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);


                            Regmitra.HeaderInfo.AllowEdit = "no";
                            if ((Regmitra.HeaderInfo.IsPICApproval == true && curcaption.Contains("TODO"))  //&& flagopr == "REVHDR"
                                ||
                                (Regmitra.HeaderInfo.IsPICApproval == true && curcaption.Contains("TODO") && int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.PENDTASK)
                                )
                            {

                                if (flagopr == "REVHDR" || flagopr == "REVHDRDRAFT")
                                {
                                    Regmitra.HeaderInfo.AllowEdit = "yes";
                                }

                                if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0") == (int)HashNetFramework.RequetsTransMitra.ROTASI)
                                {
                                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("1", "", caption, UserID, GroupName);
                                }
                                else
                                {
                                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", caption, UserID, GroupName);
                                }

                                //if (Common.ddlRegion == null)
                                {
                                    Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("", "", caption, UserID, GroupName);
                                }

                                //if (Common.ddlBranch == null)
                                if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0") == (int)HashNetFramework.RequetsTransMitra.ROTASI)
                                {

                                    Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt((Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0"), "", Regmitra.HeaderInfo.Area.ToString(), caption, UserID, GroupName);
                                }
                                else
                                {
                                    Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", "", "", caption, UserID, GroupName);
                                }

                                //if (Common.ddlStatusMitra == null)
                                {
                                    Common.ddlStatusMitra = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATMITRA", caption, UserID, GroupName);
                                    Common.ddlStatusMitra = Common.ddlStatusMitra.AsEnumerable().Where(x => int.Parse(x.Value) < 10).ToList();
                                }

                                //if (Common.ddlJenKel == null)
                                {
                                    Common.ddlJenKel = await Commonddl.dbdbGetDdlEnumsListByEncrypt("JENKEL", caption, UserID, GroupName);
                                }

                                {
                                    Common.ddlStatKW = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATKW", caption, UserID, GroupName);
                                }

                                //if (Common.ddlJenPen == null)
                                {
                                    Common.ddlJenPen = await Commonddl.dbdbGetDdlpendidikanListByEncrypt("", UserID);
                                }

                                //if (Common.ddlBank == null)
                                {
                                    Common.ddlBank = await Commonddl.dbdbGetDdlbankNameListByEncrypt("", UserID);
                                }

                                //if (Common.ddlJobs == null)
                                {
                                    Common.ddlJobs = await Commonddl.dbdbGetDdlhandleJobListByEncrypt(Regmitra.HeaderInfo.RegmitraType ?? "", "", Regmitra.HeaderInfo.Divisi, caption, UserID, GroupName);
                                }

                                //if (Common.ddlBankBranch == null)
                                {
                                    Common.ddlBankBranch = await Commonddl.dbdbGetDdlBranchBranchListByEncrypt("", caption, UserID, GroupName);
                                }

                                //if (Common.ddlRegmitraType == null)
                                {
                                    Common.ddlRegmitraType = await Commonddl.dbdbGetDdlEnumsListByEncrypt("REGMTYPE", caption, UserID, GroupName);
                                }

                                Common.ddlMitra = await Commonddl.dbdbGetDdlEnumsListByEncrypt("MTRA", caption, UserID, GroupName);



                                ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                                ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                                ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                                ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenKel);
                                ViewData["SelectStatKawin"] = OwinLibrary.Get_SelectListItem(Common.ddlStatKW);
                                ViewData["SelectPendidikan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenPen);
                                ViewData["SelectNamaBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBank);
                                ViewData["SelectCabangBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBankBranch);
                                ViewData["Selecthandlejob"] = OwinLibrary.Get_SelectListItem(Common.ddlJobs);
                                ViewData["SelectMitra"] = OwinLibrary.Get_SelectListItem(Common.ddlMitra);
                                ViewData["SelectType"] = OwinLibrary.Get_SelectListItem(Common.ddlRegmitraType);
                                ViewData["SelectStatusMitra"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusMitra);

                            }

                            ViewData["SelectFollow"] = OwinLibrary.Get_SelectListItem(Common.ddlFollow);
                            //pengisian NIK akan muncul //
                            //APRV header doc bukan sebagai pengaju dan pendaftaran baru
                            ViewBag.shonik = "no";
                            if (flagopr == "APRHDR" && Regmitra.HeaderInfo.RegmitraCreatedBy != UserID
                                && (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString()) == (int)HashNetFramework.RequetsTransMitra.BARU)
                                && (int.Parse(Regmitra.HeaderInfo.StatusDoc.ToString()) == (int)HashNetFramework.StatusDocTrans.WAITAPRV))
                            {
                                ViewBag.shonik = "yes";
                            }

                            //memunculkan tab document
                            ViewBag.shodoc = "no";
                            if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString()) != (int)HashNetFramework.RequetsTransMitra.UBAH && int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString()) != (int)HashNetFramework.RequetsTransMitra.ROTASI
                                && int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString()) != (int)HashNetFramework.RequetsTransMitra.RANGKAPJOB)
                            {
                                ViewBag.shodoc = "yes";
                            }
                            ViewBag.listdoc = "no";
                        }
                        else
                        {
                            Regmitra.HeaderInfo.AllowEdit = "no";
                            ViewBag.shonik = "yes";
                            ViewBag.shodoc = "yes";
                            ViewBag.listdoc = "yes";

                            Regmitra.DTDokumen = await Commonddl.dbdbGetDokumenList("0", RegquestNo, RegquestNo, "1", "", "", caption, UserID, GroupName);
                            Regmitra.HeaderInfo.Dokumen = Regmitra.DTDokumen;

                        }

                        if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0") == (int)HashNetFramework.RequetsTransMitra.UBAH)
                        {
                            ViewBag.disabledtglmasuk = "disabled";
                            ViewBag.disabledtglakhir = "disabled";
                            ViewBag.disabledhandleJob = "disabled";
                            ViewBag.disablednamamitra = "disabled";
                            ViewBag.disablednoktp = "disabled";
                            ViewBag.disablednonpwp = "disabled";
                            ViewBag.disabledalamat = "disabled";
                            ViewBag.disabledtgllahir = "disabled";
                            ViewBag.disabledjenkel = "disabled";
                            ViewBag.disabledtptlahir = "disabled";
                            if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.REVISE)
                            {
                                ViewBag.showsearch = "yes";
                            }
                        }
                        if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0") == (int)HashNetFramework.RequetsTransMitra.PANJANG)
                        {
                            ViewBag.disabledtglmasuk = "";
                            ViewBag.disabledtglakhir = "";
                            ViewBag.disabledhandleJob = "disabled";
                            ViewBag.disablednamamitra = "disabled";
                            ViewBag.disablednoktp = "disabled";
                            ViewBag.disablednonpwp = "disabled";
                            ViewBag.disabledalamat = "disabled";
                            ViewBag.disabledtgllahir = "disabled";
                            ViewBag.disabledjenkel = "disabled";
                            ViewBag.disabledtptlahir = "disabled";
                            if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.REVISE)
                            {
                                ViewBag.showsearch = "yes";
                            }

                        }

                        if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0") == (int)HashNetFramework.RequetsTransMitra.ROTASI)
                        {
                            ViewBag.disabledtglmasuk = "disabled";
                            ViewBag.disabledtglakhir = "disabled";
                            ViewBag.disabledhandleJob = "disabled";
                            ViewBag.disablednamamitra = "disabled";
                            ViewBag.disablednoktp = "disabled";
                            ViewBag.disablednonpwp = "disabled";
                            ViewBag.disabledalamat = "disabled";
                            ViewBag.disabledtgllahir = "disabled";
                            ViewBag.disabledjenkel = "disabled";
                            ViewBag.disabledtptlahir = "disabled";
                            if (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.REVISE)
                            {
                                ViewBag.showsearch = "yes";
                            }

                        }

                        ViewBag.parmodule = module;
                        ViewBag.parcurmodule = curmodule;
                        ViewBag.paramkey = paramkey;

                        //Regmitra.HeaderInfo.RegmitraType = HasKeyProtect.Encryption(Regmitra.HeaderInfo.RegmitraType);
                        //set caption view//
                        ViewBag.menuback = ViewBag.menu.ToString().Replace(" (", "").Replace(")", "").Replace("(", "");
                        ViewBag.captionback = curcaption + tempfilter;
                        ViewBag.captiondesc = menuitemdescription + ViewBag.menu;
                        ViewBag.ruteback = MainControllerNameHeaderTx;
                        ViewBag.actionback = MainActionNameHeaderTx;

                        // senback to client browser//

                        returnview = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/WFRegmitView.cshtml", Regmitra);
                    }
                    else
                    {
                        returnview = "";
                    }
                }
                //else
                //{
                //    //memunculkan tab document
                //    ViewBag.shodoc = "no";
                //    if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString()) != (int)HashNetFramework.RequetsTransMitra.UBAH)
                //    {
                //        ViewBag.shodoc = "yes";
                //    }
                //    ViewBag.listdoc = "no";
                //    ViewBag.descap = "Pengajuan No : " + Regmitra.HeaderInfo.RegmitraNo;
                //    returnview = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitPreView.cshtml", Regmitra);
                //}

                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = "",
                    view = returnview
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

        public async Task<ActionResult> clnWFRgridHeaderTx(int paged)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                // get value filter have been filter//
                string RegmitraNo = modFilter.cfTransNo ?? "";
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);


                // try show filter data//
                List<DataTable> dtlist = await Regmitraddl.dbGetHeaderTxList(null, RegmitraNo, "", "", "", fromdate, todate, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                Regmitra.DTHeaderTx = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitLstGrid.cshtml", Regmitra),
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

        public async Task<ActionResult> clnWFRgridHeaderTxLog(int paged)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                // get value filter have been filter//
                string RegmitraNo = modFilter.cfTransNo ?? Regmitra.HeaderInfo.RegmitraNo ?? "";

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumberLog;
                double pagingsizeclient = modFilter.pagingsizeclientLog;
                double TotalRecord = modFilter.TotalRecordLog;
                double totalRecordclient = modFilter.totalRecordclientLog;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);


                // try show filter data//
                string captiondetail = "FORCAST";
                List<DataTable> dtlist = await Commonddl.dbGetApprovalLogList(null, RegmitraNo, PageNumber, pagenumberclient, pagingsizeclient, captiondetail, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclientLog = pagenumberclient;

                //set akta//
                Regmitra.DTLogTx = dtlist[1];

                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data";

                //set session filterisasi //
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/_uiGridRegmitraAprvLog.cshtml", Regmitra),
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
        public async Task<ActionResult> clnHeaderAndalTx(String menu, String caption)
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

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Regmitra";
                ViewBag.action = "clnHeaderAndalTx";

                //send back to client browser//
                return Json(new
                {
                    msgdt = "",
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/_uiAndalData.cshtml", Regmitra),
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorPUB.txt");
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
        public async Task<ActionResult> clnHeaderAndalSvTx(String menu, String caption, string mode, string tglpro, HttpPostedFileBase files)
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
                string msg = "";
                mode = mode ?? "";
                string vld = "";
                if (mode == "" || mode == "null")
                {
                    msg = "Silakan pilih proses yang akan anda lakukan";
                    vld = "x";
                }
                else if (mode == "2" && PermisionModule.AllowUpload == false)
                {
                    msg = "User tidak memiliki akses";
                    vld = "x";
                }
                else if (mode != "2" && PermisionModule.AllowDownload == false)
                {

                    msg = "User tidak memiliki akses";
                    vld = "x";
                }
                else if (mode == "2" && files == null)
                {
                    msg = "Silakan pilih file";
                    vld = "x";
                }
                else if (mode == "2A" && (tglpro ?? "") == "")
                {
                    msg = "Silakan isikan tanggal proses";
                    vld = "x";
                }


                byte[] bytefl = bytefl = Encoding.ASCII.GetBytes("");
                string filename = "";
                string filetype = "";
                tglpro = (tglpro ?? "");
                if (msg == "")
                {
                    var xmlString = "";
                    XmlDocument xml = new XmlDocument();
                    if (mode == "1A")
                    {
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\Template Employe.xml";
                        xml.Load(xmlString);
                    }
                    else if (mode == "2A")
                    {
                        tglpro = DateTime.Parse(tglpro).ToString("yyyyMMdd");
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\TemplateNIK_Check.xml";
                        xml.Load(xmlString);
                    }
                    else if (mode == "1B")
                    {
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\Template Bank.xml";
                        xml.Load(xmlString);
                    }
                    else if (mode == "1C")
                    {
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\Template Custom Field 2.xml";
                        xml.Load(xmlString);
                    }
                    else
                    {
                        xml.Load(files.InputStream);
                    }

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                    nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
                    XmlElement root = xml.DocumentElement;
                    List<string> str = new List<string>();



                    if (mode != "2")
                    {
                        // try show filter data//

                        DataTable dtlist = await Regmitraddl.dbGetTxdata4andal(mode, tglpro, caption, UserID, GroupName);
                        XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

                        foreach (DataRow rw in dtlist.Rows)
                        {
                            XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlAttrRow.Value = "0";
                            XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlRecordNo.Attributes.Append(xmlAttrRow);
                            foreach (DataColumn col in dtlist.Columns)
                            {
                                XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                                XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                                string attrval = col.ColumnName.Split('_')[1].ToString();
                                xmlAttrNumber.Value = attrval;
                                XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                                xmlRecordNoceldata.InnerText = rw[col].ToString();
                                xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                                xmlRecordNo.AppendChild(xmlRecordNocel);
                                xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                            }
                            nodesing.AppendChild(xmlRecordNo);
                        }
                        if (mode == "1A")
                        {
                            filename = "Data Mitra_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                            msg = "Download Data Mitra Berhasil";
                        }
                        if (mode == "2A")
                        {
                            filename = "Data Mitra Check_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                            msg = "Download Data Pengecekan NIK Mitra Berhasil";
                        }
                        if (mode == "1B")
                        {
                            filename = "Data Rekening Mitra_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                            msg = "Dowload Data Rekening Mitra";
                        }
                        if (mode == "1C")
                        {
                            filename = "Data MOU dan SPL Mitra_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                            msg = "Download Data Kontrak/PKS Mitra ";
                        }
                        filetype = "application/xml";
                        bytefl = Encoding.ASCII.GetBytes(xml.OuterXml);
                    }
                    else
                    {
                        int totalkolom = 0;
                        foreach (XmlNode node in root.SelectNodes("/*//ss:Row", nsmgr))
                        {
                            XmlNodeList batchNode = node.ChildNodes;
                            totalkolom = batchNode.Count;
                            var strcont = string.Join("|", batchNode.Cast<XmlNode>().Select(z => z.InnerText).ToList());
                            str.Add(strcont);
                        }

                        DataTable table = new DataTable();
                        var splitFileContents = (from f in str select f.Split('|')).ToArray();
                        int maxLength = (from s in splitFileContents select s.Count()).Max();
                        for (int i = 0; i < maxLength; i++)
                        {
                            table.Columns.Add(splitFileContents[0][i].Replace(" ", "").Replace(".", "").Replace(",", "").Replace("/", "").Trim().ToLower());
                        }
                        var ix = 0;
                        foreach (var line in splitFileContents)
                        {
                            if (ix > 0)
                            {
                                DataRow row = table.NewRow();
                                row.ItemArray = (object[])line;
                                table.Rows.Add(row);
                            }
                            ix = ix + 1;
                        }

                        //proses per 300 row//
                        string totget = "1000";
                        int totalrecord = 0;
                        int gagalrecord = 0;
                        int succesrecord = 0;

                        double totalpage = Math.Ceiling(double.Parse(table.Rows.Count.ToString()) / double.Parse(totget));
                        for (var i = 0; i < totalpage; i++)
                        {
                            DataTable dtx = table.AsEnumerable().Skip(i * int.Parse(totget)).Take(int.Parse(totget)).CopyToDataTable();
                            string col1 = dtx.Columns[0].ColumnName;
                            string col2 = dtx.Columns["idktpcanbeedit"].ColumnName;
                            foreach (DataColumn col in table.Columns)
                            {
                                if (col.ColumnName != col1 && col.ColumnName != col2)
                                {
                                    dtx.Columns.Remove(col.ColumnName);
                                }
                            }
                            dtx.Columns[0].ColumnName = "NOKTP";
                            dtx.Columns[1].ColumnName = "Valued";

                            DataTable dtlist = await Regmitraddl.dbGetTxdata4andalRute(dtx, caption, UserID, GroupName);
                            bool hasRows = dtlist.Rows.GetEnumerator().MoveNext();
                            if (hasRows == true)
                            {
                                totalrecord = int.Parse(dtlist.Rows[0][5].ToString());
                                gagalrecord = dtlist.AsEnumerable().Where(x => x.Field<Int32>("Status_String") == -1).Count();
                                succesrecord = dtlist.AsEnumerable().Where(x => x.Field<Int32>("Status_String") == 1).Count();
                                msg = "Upload NIK Mitra selesai : " + succesrecord.ToString() + " berhasil  dan " + gagalrecord.ToString() + " Gagal ";

                                xmlString = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\TemplateResultNIK_Upload.xml";
                                xml.Load(xmlString);

                                nsmgr = new XmlNamespaceManager(xml.NameTable);
                                nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
                                root = xml.DocumentElement;

                                XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

                                foreach (DataRow rw in dtlist.Rows)
                                {
                                    XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                                    xmlAttrRow.Value = "0";
                                    XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                                    xmlRecordNo.Attributes.Append(xmlAttrRow);
                                    foreach (DataColumn col in dtlist.Columns)
                                    {
                                        XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                                        XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                                        string attrval = col.ColumnName.Split('_')[1].ToString();
                                        xmlAttrNumber.Value = attrval;
                                        XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                                        xmlRecordNoceldata.InnerText = rw[col].ToString();
                                        xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                                        xmlRecordNo.AppendChild(xmlRecordNocel);
                                        xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                                    }
                                    nodesing.AppendChild(xmlRecordNo);
                                }
                                filename = "ResultUploadNIK_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                                filetype = "application/xml";
                                bytefl = Encoding.ASCII.GetBytes(xml.OuterXml);
                            }
                            else
                            {
                                msg = "Terjadi Kesalahan saat eksekusi script, silahkan cek hak akses script";
                            }
                        }

                    }


                }

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Regmitra";
                ViewBag.action = "clnHeaderAndalTx";

                //send back to client browser//
                return Json(new
                {
                    flbt = bytefl,
                    flbtnm = filename,
                    flbtmtype = filetype,
                    msgdt = msg,
                    moderror = IsErrorTimeout,
                    isdwn = vld,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/_uiAndalData.cshtml", Regmitra),
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorPUB.txt");
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
        public async Task<ActionResult> clnHeaderTx(String menu, String caption)
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
                string Divisi = "";
                string Cabang = "";
                string Area = "";
                string RequestNo = "";
                string FromDate = "";
                string Todate = "";
                int Status = (menu == "renew") ? 10 : -1; //default aktif


                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;


                // try show filter data//
                List<String> recordPage = await Regmitraddl.dbGetHeaderTxListdonCount(RequestNo, Divisi, Cabang, Area, FromDate, Todate, Status, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Regmitraddl.dbGetHeaderTxdonList(null, RequestNo, Divisi, Cabang, Area, FromDate, Todate, Status, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());


                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                modFilter.ModuleName = caption;
                modFilter.ModuleID = idcaption;
                modFilter.idcaption = idcaption;

                modFilter.UserTypes = UserTypes;

                //set to object pendataran//
                Regmitra.DTAllTx = dtlist[0];
                Regmitra.DTHeaderTx = dtlist[1];
                Regmitra.FilterTransaksi = modFilter;
                Regmitra.Permission = PermisionModule;

                //set session filterisasi //
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = "Regmitra";
                ViewBag.action = "clnHeaderTx";

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitLst.cshtml", Regmitra),
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorPUB.txt");
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

        public async Task<ActionResult> clnRgridHeaderTx(int paged)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                // get value filter have been filter//
                string KeySearch = modFilter.RequestNo ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectArea = modFilter.SelectArea ?? "";
                string SelectBranch = modFilter.SelectBranch ?? "";
                string fromdate = modFilter.fromdate ?? "";
                string todate = modFilter.todate ?? "";
                string Status = modFilter.SelectContractStatus ?? "-1";

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                // try show filter data//
                List<DataTable> dtlist = await Regmitraddl.dbGetHeaderTxdonList(null, KeySearch, SelectDivisi, SelectBranch, SelectArea, fromdate, todate, int.Parse(Status), PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                //set akta//
                Regmitra.DTHeaderTx = dtlist[1];
                
                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitLstGrid.cshtml", Regmitra),
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
        public async Task<ActionResult> clnHeaderTxChg(string nokeypop, string sangu, string sange, string togeub)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string Message = "";

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);
                DataRow dr = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == nokeypop).SingleOrDefault();
                sangu = sangu == "" ? "0" : sangu;
                DataTable dtx;
                int IDresult = 0;
                if (dr != null)
                {
                    if (sangu == "0")
                    {
                        Message = "Silahkan pilih status";
                    }
                    else
                    {
                        if (int.Parse(sangu) < (int)StatusMitra.ReNew)
                        {
                            string id = dr["Id"].ToString();
                            string stat = dr["StatusMitra"].ToString();
                            if (stat == sangu)
                            {
                                Message = "Tidak ada Perubahan Status Mitra, cek kembali status yang anda pilih ";
                            }
                            else
                            {
                                if ((sange ?? "") != "" || (togeub ?? "") != "")
                                {

                                    if ((togeub ?? "") != "" && DateTime.Parse(togeub) > DateTime.Now.Date)
                                    {
                                        Message = "Tgl Perubahan Status Mitra harus lebih kecil dari hari ini";
                                    }
                                    else
                                    {

                                        dtx = await Regmitraddl.dbSaveUpdtStatMitra(id, sangu, togeub, sange, caption, UserID, GroupName);
                                        IDresult = int.Parse(dtx.Rows[0][0].ToString());
                                        Message = EnumsDesc.GetDescriptionEnums((ProccessOutput)IDresult);

                                        if (IDresult == -21)
                                        {
                                            Message = "Terdapat pengajuan yang masih dalam proses untuk mitra, silahkan diproses terlebih dahulu";
                                        }
                                        else if (IDresult == 1)
                                        {

                                            Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == nokeypop).ToList<DataRow>().ForEach(x => { dr["StatusMitra"] = sangu; });
                                            Message = "Perubahan status mitra berhasil";
                                        }
                                    }
                                }
                                else
                                {
                                    Message = "Isikan Tgl Perubahan Status Mitra dan catatan ";
                                }
                            }
                        }
                        else
                        {
                            Message = "Perubahan Status Mitra tidak diijinkan";
                        }
                    }
                }
                else
                {
                    Message = "Data mitra tidak ditemukan, silahkan di refresh dahulu";
                }

                //set session filterisasi //
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = Message,
                    rslt = IDresult,
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


        public async Task<ActionResult> clnRecekExistmitra(string nokeypop)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string Message = "";

                //descript some value for db//
                caption = HasKeyProtect.Decryption(caption);

                DataTable dtu = await Commonddl.dbdbGetDdlMitraListByEncryptcek(nokeypop, "", "40", caption, UserID, GroupName);
                DataRow drx = dtu.AsEnumerable().SingleOrDefault();
                if (drx != null)
                {

                    Message = "Mitra dengan nomor KTP '" + nokeypop + "' sudah terdaftar dengan lokasi '" + drx["Region_Name"].ToString() + "/" + drx["brch_code"].ToString() + "-" + drx["brch_name"].ToString() +
                        "' menangani divisi '" + drx["Divisi_Name"].ToString() + "', Status Mitra Saat ini : " + drx["StatusMitraDesc"].ToString();
                }
                else
                {
                    Message = "Mitra dengan nomor KTP '" + nokeypop + "' belum terdaftar";
                }

                //set session filterisasi //
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = Message,
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

        public async Task<ActionResult> clnOpenvwmitr(string kyup, string kyup1, string kyup2)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                modFilter = modFilter == null ? new cFilterContract() : modFilter;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData[tempcommon] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string moduleid = HashNetFramework.HasKeyProtect.Decryption(modFilter.ModuleID);

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleid).Select(x => x.AllowGenerate).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                DataRow dr = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == kyup).SingleOrDefault();
                cRegmitra rgmit = new cRegmitra();
                rgmit.ContractNo = dr["ContractNo"].ToString();
                rgmit.ContractDate = dr["ContractDate"].ToString();
                rgmit.keylookupdataHTX = dr["keylookupdata"].ToString();
                rgmit.IDHeaderTx = int.Parse(dr["Id"].ToString());
                rgmit.IDDetailTx = int.Parse(dr["Id"].ToString());
                rgmit.Divisi = dr["Divisi"].ToString();
                rgmit.DivisiName = dr["Divisi_Name"].ToString();
                rgmit.Area = dr["Area"].ToString();
                rgmit.AreaName = dr["Region_Name"].ToString();
                rgmit.Cabang = dr["Cabang"].ToString();
                rgmit.CabangDesc = dr["brch_code"].ToString() + "-" + dr["brch_name"].ToString();
                rgmit.handleJob = dr["handlejob"].ToString();
                rgmit.handleJobDesc = dr["JobDesc"].ToString();
                rgmit.tglmasuk = dr["tglmasuk"].ToString();
                rgmit.tglakhir = dr["tglakhir"].ToString();
                rgmit.NamaMitra = dr["NamaMitra"].ToString();
                rgmit.NoKTP = dr["NoKTP"].ToString();
                rgmit.NoNPWP = dr["NoNPWP"].ToString();
                rgmit.Alamat = dr["Alamat"].ToString();
                rgmit.AlamatKorespodensi = dr["AlamatKorespodensi"].ToString();
                rgmit.Tempatlahir = dr["Tempatlahir"].ToString();
                rgmit.Tgllahir = dr["Tgllahir"].ToString();
                rgmit.JenisKelamin = dr["JenisKelamin"].ToString();
                rgmit.JenisKelaminDesc = dr["JenisKelaminDesc"].ToString();
                rgmit.Pendidikan = dr["Pendidikan"].ToString();
                rgmit.PendidikanDesc = dr["PendidikanDesc"].ToString();
                rgmit.StatusKawin = dr["StatusKawin"].ToString();
                rgmit.StatusKawinDesc = dr["StatusKawinDesc"].ToString();
                rgmit.NamaBank = dr["NamaBank"].ToString();
                rgmit.NamaBankDesc = dr["NamaBankDesc"].ToString();
                rgmit.CabangBank = dr["CabangBank"].ToString();
                rgmit.CabangBankDesc = dr["CabangBankDesc"].ToString();
                rgmit.Norekening = dr["Norekening"].ToString();
                rgmit.Pemilikkening = dr["Pemilikkening"].ToString();
                rgmit.NoSPPI = dr["NoSPPI"].ToString();
                rgmit.NoFAX = dr["NoFAX"].ToString();
                rgmit.NoHP = dr["NoHP"].ToString();
                rgmit.NoHP1 = dr["NoHP1"].ToString();
                rgmit.NoWA = dr["NoWA"].ToString();
                rgmit.Email = dr["Email"].ToString();
                rgmit.NamaFB = dr["NamaFB"].ToString();
                rgmit.NIKLama = dr["NIKLama"].ToString();
                rgmit.NIKBaru = dr["NIKBaru"].ToString();
                rgmit.StatusDoc = dr["StatusDoc"].ToString();
                rgmit.StatusDocDesc = dr["StatusDocDesc"].ToString();
                rgmit.RegmitraStatusDesc = dr["StatusMitraDesc"].ToString();

                moduleid = HasKeyProtect.Decryption(moduleid);
                DataTable DTDokumen = await Commonddl.dbdbGetDokumenList("0", rgmit.NoKTP, rgmit.NIKBaru, "3", "", "", moduleid, UserID, GroupName);
                rgmit.Dokumen = DTDokumen;

                DataTable DTApprovalDoc = await Commonddl.dbdbGetDokumenList("0", rgmit.NoKTP, rgmit.NIKBaru, "4", "", "", moduleid, UserID, GroupName);
                rgmit.ApprovalDOC = DTApprovalDoc;

                string usrtype = HashNetFramework.HasKeyProtect.Decryption(Account.AccountLogin.UserType);
                ViewBag.keyup = kyup;
                ViewBag.keyup1 = kyup1;
                ViewBag.keyup2 = kyup2;


                ViewBag.menu = "WFTODONEWREGLST";
                ViewBag.caption = "WFTODONEWREGLST";
                ViewBag.captiondesc = "Data Mitra";
                ViewBag.rute = "Regmitra";
                ViewBag.action = "clnHeaderTx";

                ViewBag.allowviewdoc = "true";
                ViewBag.allowviewdwn = AllowGenerate;
                if (int.Parse(usrtype) == (int)UserType.Branch || int.Parse(usrtype) == (int)UserType.Area)
                {
                    ViewBag.allowviewdoc = "false";
                }


                Common.ddlStatusMitra = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATMITRA", moduleid, UserID, GroupName);
                Common.ddlStatusMitra = Common.ddlStatusMitra.Where(x => int.Parse(x.Value) < 10);
                ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusMitra);

                ////if (Common.ddlDocument == null)
                //{
                //    Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenList("2", "", "", UserID, GroupName);
                //}
                //ViewData["SelectDocument"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);
                //ViewBag.captiondescpks = "Pengiriman PKS Mitra";


                string returnview = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitPreView.cshtml", rgmit);

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    loadcabang = 0,
                    keydata = "",
                    view = returnview
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

        public async Task<ActionResult> clnOpenFilterpopmitra(string cb, string tpe, string nm)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string Groupname = Account.AccountLogin.GroupName;
                string IDCabang = Account.AccountLogin.IDCabang;
                string CabangName = Account.AccountLogin.CabangName;
                string Area = Account.AccountLogin.Area;
                string AreaName = Account.AccountLogin.AreaName;
                string Divisi = Account.AccountLogin.Divisi;
                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                // get value filter before//
                string SelectBranch = modFilter.SelectBranch;
                string SelectClient = modFilter.SelectClient;
                string SelectNotaris = modFilter.SelectNotaris;
                string module = HasKeyProtect.Decryption(modFilter.ModuleID);

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData[tempcommon] = Common;

                string viewDataMitra = "";
                string viewDataRek = "";
                string viewDataCabang = "";
                string viewDatajob = "";
                string viewDataSearch = "";
                string viewDataMitralok = "";
                string viewdoc = "1";
                string viewjob = "1";
                string viewrek = "1";

                string found = "";
                string foundact = (int.Parse(Regmitra.HeaderInfo.StatusDoc) == (int)HashNetFramework.StatusDocTrans.REVISE) ? "rv" : "";
                string message = "";
                string hdjob = "";
                nm = nm ?? "";
                cb = cb ?? "";

                DataTable dt = await Commonddl.dbdbGetDdlMitraListByEncrypt(nm, cb, tpe, module, UserID, Groupname);
                DataRow dr = dt.AsEnumerable().SingleOrDefault();
                if (dr != null)
                {
                    found = "x";
                    Regmitra.HeaderInfo.keylookupdataDTX = HasKeyProtect.Encryption(dr["Id"].ToString());
                    Regmitra.HeaderInfo.keylookupdataHTX = dr["keylookupdata"].ToString();
                    Regmitra.HeaderInfo.Divisi = dr["Divisi"].ToString();
                    Regmitra.HeaderInfo.DivisiName = dr["Divisi_Name"].ToString();
                    Regmitra.HeaderInfo.Area = dr["Area"].ToString();
                    Regmitra.HeaderInfo.AreaName = dr["Region_Name"].ToString();
                    Regmitra.HeaderInfo.Cabang = dr["Cabang"].ToString();
                    Regmitra.HeaderInfo.CabangDesc = dr["brch_code"].ToString() + "-" + dr["brch_name"].ToString();
                    Regmitra.HeaderInfo.handleJob = dr["handlejob"].ToString();
                    Regmitra.HeaderInfo.handleJobDesc = dr["JobDesc"].ToString();

                    hdjob = dr["handlejob"].ToString();

                    Regmitra.HeaderInfo.tglmasuk = dr["tglmasuk"].ToString();
                    Regmitra.HeaderInfo.tglakhir = dr["tglakhir"].ToString();
                    Regmitra.HeaderInfo.contractnobefore = dr["ContractNo"].ToString();
                    Regmitra.HeaderInfo.tglmasukbefore = dr["tglmasuk"].ToString();
                    Regmitra.HeaderInfo.tglakhirbefore = dr["tglakhir"].ToString();

                    Regmitra.HeaderInfo.divisinamebefore = dr["Divisi_Name"].ToString();
                    Regmitra.HeaderInfo.cabangnamebefore = dr["brch_code"].ToString() + "-" + dr["brch_name"].ToString();
                    Regmitra.HeaderInfo.cabangcodenamebefore = dr["brch_code"].ToString();
                    Regmitra.HeaderInfo.regionnamebefore = dr["Region_Name"].ToString();
                    Regmitra.HeaderInfo.handlejobnamebefore = dr["JobDesc"].ToString();

                    if (int.Parse(tpe) == (int)HashNetFramework.RequetsTransMitra.PANJANG)
                    {
                        string date1 = Regmitra.HeaderInfo.tglakhirbefore;

                        if (Regmitra.HeaderInfo.divisinamebefore.ToLower().Contains("penjualan") || Regmitra.HeaderInfo.divisinamebefore.ToLower().Contains("sales"))
                        {
                            date1 = DateTime.Parse(date1).AddMonths(1).ToString("dd-MMMM-yyyy");
                        }
                        else
                        {
                            date1 = DateTime.Parse(date1).AddDays(1).ToString("dd-MMMM-yyyy");
                        }

                        vmCommonddl Commonddl = new vmCommonddl();
                        DataTable dtx = await Commonddl.dbdbGetDdlDevisiPeriodeListByEncrypt("1", Regmitra.HeaderInfo.Divisi, module, UserID, Groupname);
                        string periode = dtx.Rows[0][2].ToString();
                        CultureInfo provider = new CultureInfo("en-GB");
                        DateTime dt1 = DateTime.Parse(date1, provider, DateTimeStyles.NoCurrentDateDefault);
                        //salaes sampai akhir tahun
                        int minday = 0;
                        if (periode == "0")
                        {
                            periode = (12 - dt1.Month).ToString();
                        }
                        else
                        {
                            minday = -1;
                        }
                        DateTime dt2 = dt1.AddMonths(int.Parse(periode)).AddDays(minday);
                        string date2 = dt2.ToString("dd-MMMM-yyyy");
                        Regmitra.HeaderInfo.tglmasuk = date1;
                        Regmitra.HeaderInfo.tglakhir = date2;
                    }

                    Regmitra.HeaderInfo.NamaMitra = dr["NamaMitra"].ToString();
                    Regmitra.HeaderInfo.NoKTP = dr["NoKTP"].ToString();
                    Regmitra.HeaderInfo.NoNPWP = dr["NoNPWP"].ToString();
                    Regmitra.HeaderInfo.Alamat = dr["Alamat"].ToString();
                    Regmitra.HeaderInfo.AlamatKorespodensi = dr["AlamatKorespodensi"].ToString();
                    Regmitra.HeaderInfo.Tempatlahir = dr["Tempatlahir"].ToString();
                    Regmitra.HeaderInfo.Tgllahir = dr["Tgllahir"].ToString();
                    Regmitra.HeaderInfo.JenisKelamin = dr["JenisKelamin"].ToString();
                    Regmitra.HeaderInfo.JenisKelaminDesc = dr["JenisKelaminDesc"].ToString();
                    Regmitra.HeaderInfo.Pendidikan = dr["Pendidikan"].ToString();
                    Regmitra.HeaderInfo.PendidikanDesc = dr["PendidikanDesc"].ToString();
                    Regmitra.HeaderInfo.NamaBank = dr["NamaBank"].ToString();
                    Regmitra.HeaderInfo.NamaBankDesc = dr["NamaBankDesc"].ToString();
                    Regmitra.HeaderInfo.CabangBank = dr["CabangBank"].ToString();
                    Regmitra.HeaderInfo.CabangBankDesc = dr["CabangBankDesc"].ToString();
                    Regmitra.HeaderInfo.RegmitraStatus = dr["StatusMitra"].ToString();
                    Regmitra.HeaderInfo.RegmitraStatusDesc = dr["StatusMitraDesc"].ToString();
                    Regmitra.HeaderInfo.Norekening = dr["Norekening"].ToString();
                    Regmitra.HeaderInfo.Pemilikkening = dr["Pemilikkening"].ToString();
                    Regmitra.HeaderInfo.StatusKawin = dr["StatusKawin"].ToString();
                    Regmitra.HeaderInfo.NoSPPI = dr["NoSPPI"].ToString();
                    Regmitra.HeaderInfo.NoFAX = dr["NoFAX"].ToString();
                    Regmitra.HeaderInfo.NoHP = dr["NoHP"].ToString();
                    Regmitra.HeaderInfo.NoHP1 = dr["NoHP1"].ToString();
                    Regmitra.HeaderInfo.NoWA = dr["NoWA"].ToString();
                    Regmitra.HeaderInfo.NamaFB = dr["NamaFB"].ToString();
                    Regmitra.HeaderInfo.Email = dr["Email"].ToString();
                    Regmitra.HeaderInfo.NIKLama = dr["NIKLama"].ToString();
                    Regmitra.HeaderInfo.NIKBaru = dr["NIKBaru"].ToString();
                    Regmitra.HeaderInfo.AllowEdit = "yes";

                }
                else
                {
                    Regmitra.HeaderInfo = new cRegmitra();
                    Regmitra.HeaderInfo.StatusDoc = "0";
                    Regmitra.HeaderInfo.Area = Area;
                    Regmitra.HeaderInfo.AreaName = AreaName;
                    Regmitra.HeaderInfo.Cabang = HasKeyProtect.Decryption(IDCabang);
                    Regmitra.HeaderInfo.Divisi = Divisi;
                    Regmitra.HeaderInfo.RegmitraCreatedBy = UserID;
                    Regmitra.HeaderInfo.StatusDoc = "0";
                    Regmitra.HeaderInfo.FlagOperation = HasKeyProtect.Encryption("CRETHDR");
                    Regmitra.HeaderInfo.CabangDesc = CabangName;
                    Regmitra.HeaderInfo.keylookupdataHTX = "0";
                    Regmitra.HeaderInfo.keylookupdataDTX = HasKeyProtect.Encryption("0");
                    Regmitra.HeaderInfo.StatusFollow = "0";
                    Regmitra.HeaderInfo.IsPICApproval = false;
                    Regmitra.HeaderInfo.Dokumen = Regmitra.DTDokumen;
                    Regmitra.HeaderInfo.AllowEdit = (tpe ?? "") == "10" ? "yes" : "init";

                    if (nm != "")
                    {
                        if (int.Parse(tpe ?? "0") != (int)HashNetFramework.RequetsTransMitra.PANJANG)
                        {
                            message = "Data Mitra tidak ditemukan, Pastikan Mitra terdaftar dan berstatus aktif";
                        }
                        else
                        {
                            message = "Data Mitra tidak ditemukan, Pastikan Mitra terdaftar dan masa kontrak mitra sudah habis";
                        }
                    }
                    else
                    {
                        message = "Ketikan No.KTP/NIK ";
                    }
                    //message = (tpe ?? "") == "10" ? "" : (nm ?? "") == "" ? "Isikan NoKTP pada kolom 'Pencarian Mitra' " : "Data mitra tidak ditemukan, silahkan cek kembali";
                }

                tpe = (tpe ?? "0") == "" ? "0" : tpe;
                Regmitra.HeaderInfo.RegmitraType = tpe;
                Regmitra.HeaderInfo.UserTypeInit = int.Parse(UserTypes);
                if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0") == (int)HashNetFramework.RequetsTransMitra.ROTASI)
                {
                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("1", "", module, UserID, Groupname);
                }
                else
                {
                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", module, UserID, Groupname);
                }

                ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);

                if (int.Parse(Regmitra.HeaderInfo.RegmitraType.ToString() ?? "0") == (int)HashNetFramework.RequetsTransMitra.ROTASI)
                {
                    Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt(tpe, "", Regmitra.HeaderInfo.Area, module, UserID, Groupname);
                }
                else
                {
                    Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", "", "", module, UserID, Groupname);
                }

                ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenKel);
                ViewData["SelectStatKawin"] = OwinLibrary.Get_SelectListItem(Common.ddlStatKW);
                ViewData["SelectPendidikan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenPen);
                ViewData["SelectNamaBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBank);
                ViewData["SelectCabangBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBankBranch);
                ViewData["Selecthandlejob"] = OwinLibrary.Get_SelectListItem(Common.ddlJobs);
                ViewData["SelectType"] = OwinLibrary.Get_SelectListItem(Common.ddlRegmitraType);
                ViewData["SelectStatusMitra"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusMitra);

                ViewBag.CurModule = module;

                if (int.Parse(tpe) == (int)HashNetFramework.RequetsTransMitra.UBAH)
                {
                    ViewBag.disabledtglmasuk = "disabled";
                    ViewBag.disabledtglakhir = "disabled";
                    ViewBag.disabledhandleJob = "disabled";
                    ViewBag.disablednamamitra = "disabled";
                    ViewBag.disablednoktp = "disabled";
                    ViewBag.disablednonpwp = "disabled";
                    ViewBag.disabledalamat = "disabled";
                    ViewBag.disabledtgllahir = "disabled";
                    ViewBag.disabledjenkel = "disabled";
                    ViewBag.disabledtptlahir = "disabled";
                    viewjob = "";  //hidden panel job //

                    if ((int)UserType.Area == int.Parse(UserTypes))
                    {
                        ViewBag.disablednamamitra = "";
                    }


                }

                if (int.Parse(tpe) == (int)HashNetFramework.RequetsTransMitra.PANJANG)
                {
                    ViewBag.disabledtglmasuk = "";
                    ViewBag.disabledtglakhir = "";
                    ViewBag.disabledhandleJob = "disabled";
                    ViewBag.disablednamamitra = "disabled";
                    ViewBag.disablednoktp = "disabled";
                    ViewBag.disablednonpwp = "disabled";
                    ViewBag.disabledalamat = "disabled";
                    ViewBag.disabledtgllahir = "disabled";
                    ViewBag.disabledjenkel = "disabled";
                    ViewBag.disabledtptlahir = "disabled";
                }

                if (int.Parse(tpe) == (int)HashNetFramework.RequetsTransMitra.UBAHSTATUS)
                {
                    viewdoc = ""; //hidden panel documen
                    viewrek = "";
                    viewjob = "";
                }

                if (int.Parse(tpe) == (int)HashNetFramework.RequetsTransMitra.ROTASI)
                {
                    ViewBag.disabledtglmasuk = "disabled";
                    ViewBag.disabledtglakhir = "disabled";
                    viewdoc = ""; //hidden panel documen
                    viewrek = "";
                }

                if (int.Parse(tpe) == (int)HashNetFramework.RequetsTransMitra.RANGKAPJOB)
                {
                    ViewBag.disabledtglmasuk = "disabled";
                    ViewBag.disabledtglakhir = "disabled";
                    viewdoc = ""; //hidden panel documen
                    viewrek = "";
                }

                Regmitra.HeaderInfo.LoadView = "111";
                viewDataMitralok = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                Regmitra.HeaderInfo.LoadView = "1";
                viewDataMitra = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                Regmitra.HeaderInfo.LoadView = "2";
                viewDataRek = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                Regmitra.HeaderInfo.LoadView = "7";
                viewDatajob = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                Regmitra.HeaderInfo.LoadView = "10";
                viewDataSearch = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                Regmitra.HeaderInfo.LoadView = "0";
                viewDataCabang = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = viewDataMitra,
                    view1 = viewDataRek,
                    view0 = viewDataCabang,
                    view2 = viewDatajob,
                    view3 = viewDataSearch,
                    view4 = viewDataMitralok,
                    idx = Regmitra.HeaderInfo.keylookupdataDTX,
                    fnd = found,
                    fndop = foundact,
                    msg = message,
                    searctxt = nm,
                    isdoc = viewdoc,
                    isjob = viewjob,
                    isrk = viewrek,
                    hajob = hdjob
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

        public async Task<ActionResult> clnLoadChange(string kep, string kepreg, string tpe)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string Groupname = Account.AccountLogin.GroupName;
                string IDCabang = Account.AccountLogin.IDCabang;
                string CabangName = Account.AccountLogin.CabangName;
                string Area = Account.AccountLogin.Area;
                string Divisi = Account.AccountLogin.Divisi;


                // get value filter before//
                string SelectBranch = modFilter.SelectBranch;
                string SelectClient = modFilter.SelectClient;
                string SelectNotaris = modFilter.SelectNotaris;
                string module = HasKeyProtect.Decryption(modFilter.ModuleID);

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData[tempcommon] = Common;

                string viewload = "";
                string viewload1 = "";
                string found = "";
                string found1 = "";
                string found2 = "";
                string found3 = "";
                string found4 = "";
                string message = "";
                string date1 = "";
                string date2 = "";
                string div = "";
                string periode = "";


                //impact perubahan dvisi
                if (tpe == "dv")
                {
                    string[] kepo = kep.Split('|');

                    date1 = kepo[0].ToString();
                    div = kepo[1].ToString();

                    Regmitra.HeaderInfo.LoadView = "loadjobs";
                    string regytpe = (kepreg ?? Regmitra.HeaderInfo.RegmitraType);
                    Common.ddlJobs = await Commonddl.dbdbGetDdlhandleJobListByEncrypt(regytpe, "", div, "", UserID, Groupname);
                    ViewData["Selecthandlejob"] = OwinLibrary.Get_SelectListItem(Common.ddlJobs);
                    found = "ReloadJob";
                    viewload = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);
                    found1 = "tab_6_4";
                    found4 = "handleJob";
                    Regmitra.DTDokumen = await Commonddl.dbdbGetDokumenList("0", "INIT", "INIT", "1", div, kepreg, module, UserID, Groupname);
                    Regmitra.HeaderInfo.Dokumen = Regmitra.DTDokumen;
                    viewload1 = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitDoc.cshtml", Regmitra.HeaderInfo);

                    if (date1 != "" && int.Parse(regytpe) != (int)HashNetFramework.RequetsTransMitra.ROTASI && int.Parse(regytpe) != (int)HashNetFramework.RequetsTransMitra.RANGKAPJOB)
                    {
                        DataTable dt = await Commonddl.dbdbGetDdlDevisiPeriodeListByEncrypt("1", div, module, UserID, Groupname);
                        if (dt.Rows.Count > 0)
                        {
                            periode = dt.Rows[0][2].ToString();
                            CultureInfo provider = new CultureInfo("en-GB");
                            DateTime dt1 = DateTime.Parse(date1, provider, DateTimeStyles.NoCurrentDateDefault);

                            //salaes sampai akhir tahun
                            int minday = 0;
                            DateTime dt2 = dt1.AddMonths(int.Parse(periode));
                            if (periode == "0")
                            {
                                dt2 = dt2.AddMonths(12 - dt1.Month);
                                date2 = "31-" + dt2.ToString("MMMM-yyyy");
                            }
                            else
                            {
                                minday = -1;
                                date2 = dt2.AddDays(minday).ToString("dd-MMMM-yyyy");
                            }


                            found2 = "tglmasuk";
                            found3 = "tglakhir";
                        }
                    }

                }


                //impact perubahan tgl mulai kontrak
                if (tpe == "cnt")
                {
                    string[] kepo = kep.Split('|');

                    if (kepo.Length > 1)
                    {
                        if (kepo[0].ToString() != "" && kepo[1].ToString() != "")
                        {
                            date1 = kepo[0].ToString();
                            div = kepo[1].ToString();
                            DataTable dt = await Commonddl.dbdbGetDdlDevisiPeriodeListByEncrypt("1", div, module, UserID, Groupname);
                            if (dt.Rows.Count > 0)
                            {
                                periode = dt.Rows[0][2].ToString();
                                CultureInfo provider = new CultureInfo("en-GB");
                                DateTime dt1 = DateTime.Parse(date1, provider, DateTimeStyles.NoCurrentDateDefault);

                                //salaes sampai akhir tahun
                                int minday = 0;
                                DateTime dt2 = dt1.AddMonths(int.Parse(periode));
                                if (periode == "0")
                                {
                                    dt2 = dt2.AddMonths(12 - dt1.Month);
                                    date2 = "31-" + dt2.ToString("MMMM-yyyy");
                                }
                                else
                                {
                                    minday = -1;
                                    date2 = dt2.AddDays(minday).ToString("dd-MMMM-yyyy");
                                }

                                found2 = "tglmasuk";
                                found3 = "tglakhir";
                            }

                            if (int.Parse(kepreg ?? "0") == (int)HashNetFramework.RequetsTransMitra.UBAH)
                            {
                                Regmitra.DTDokumen = await Commonddl.dbdbGetDokumenList("0", "INIT", "INIT", "1", div, kepreg, module, UserID, Groupname);
                                Regmitra.HeaderInfo.Dokumen = Regmitra.DTDokumen;
                                found1 = "tab_6_4";
                                viewload1 = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitDoc.cshtml", Regmitra.HeaderInfo);
                            }

                        }
                    }
                }

                //Regmitra.HeaderInfo.RegmitraType = tpe;

                //ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                //ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                //ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                //ViewData["SelectJenisKelamin"] = OwinLibrary.Get_SelectListItem(Common.ddlJenKel);
                //ViewData["SelectPendidikan"] = OwinLibrary.Get_SelectListItem(Common.ddlJenPen);
                //ViewData["SelectNamaBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBank);
                //ViewData["SelectCabangBank"] = OwinLibrary.Get_SelectListItem(Common.ddlBankBranch);
                //ViewData["Selecthandlejob"] = OwinLibrary.Get_SelectListItem(Common.ddlJobs);
                //ViewData["SelectType"] = OwinLibrary.Get_SelectListItem(Common.ddlRegmitraType);

                //ViewBag.CurModule = module;


                //Regmitra.HeaderInfo.LoadView = "2";
                //viewDataRek = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                //Regmitra.HeaderInfo.LoadView = "0";
                //viewDataCabang = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitpopup.cshtml", Regmitra.HeaderInfo);

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = viewload,
                    view1 = viewload1,
                    idpop = found,
                    idpop1 = found1,
                    tipe = tpe,
                    idpop2 = found2,
                    idpop3 = found3,
                    idpop4 = found4,
                    dt2 = date1,
                    dt3 = date2,
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

        //filter data
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                modFilter = modFilter == null ? new cFilterContract() : modFilter;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData[tempcommon] as vmCommon);
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

                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = Regmitra;
                    TempData[tempcommon] = Common;

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
                    string SelectContractStatus = modFilter.SelectContractStatus ?? "1";
                    string fromdate = modFilter.fromdate ?? "";
                    string todate = modFilter.todate ?? "";


                    //decript for db//
                    //string decSelectClient = HasKeyProtect.Decryption(SelectClient);
                    //string decSelectBranch = HasKeyProtect.Decryption(SelectBranch);
                    if (Common.ddlStatusMitra == null)
                    {
                        Common.ddlStatusMitra = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATMITRA", moduleid, UserID, GroupName);
                    }

                    if (Common.ddlDevisi == null)
                    {
                        Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("", "", moduleid, UserID, GroupName);
                    }

                    if (Common.ddlRegion == null)
                    {
                        Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("", "", moduleid, UserID, GroupName);
                    }

                    if (Common.ddlBranch == null)
                    {
                        Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", "", "", moduleid, UserID, GroupName);
                    }

                    ViewData["SelectDivisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
                    ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
                    ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);
                    ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusMitra);

                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = Regmitra;
                    TempData[tempcommon] = Common;

                    string datakosong = HasKeyProtect.Encryption("");

                    // senback to client browser//
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        opsi1 = Keykode,
                        opsi2 = SelectDivisi, //decSelectBranch,
                        opsi3 = SelectArea, //SelectNotaris,
                        opsi4 = SelectBranch,

                        opsi5 = SelectContractStatus,
                        opsi6 = fromdate,
                        opsi7 = todate,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/_uiFilterData.cshtml", modFilter),
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
        public async Task<ActionResult> clnHeaderTxFilter(cFilterContract model)
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

                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData[tempcommon] as vmCommon);
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
                string idcaption = HasKeyProtect.Decryption(modFilter.ModuleID);
                string caption = idcaption;
                string menu = modFilter.Menu;


                // extend //
                cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
                string menuitemdescription = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();
                // extend //

                //set field to output//
                string KeySearch = model.RequestNo ?? "";
                string todate = model.todate ?? "";
                string fromdate = model.fromdate ?? "";
                string SelectArea = model.SelectArea ?? "";
                string SelectBranch = model.SelectBranch ?? "";
                string SelectDivisi = model.SelectDivisi ?? "";
                string Status = model.SelectContractStatus ?? "1";

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
                modFilter.SelectContractStatus = Status;
                modFilter.ModuleName = caption;
                modFilter.isModeFilter = true;
                //set filter//

                // cek validation for filterisasi //
                //string validtxt = lgPendaftaran.CheckFilterisasiData(modFilter, download);
                string validtxt = "";
                if (validtxt == "")
                {

                    // try show filter data//
                    List<String> recordPage = await Regmitraddl.dbGetHeaderTxListdonCount(KeySearch, SelectDivisi, SelectBranch, SelectArea, fromdate, todate, int.Parse(Status), PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Regmitraddl.dbGetHeaderTxdonList(null, KeySearch, SelectDivisi, SelectBranch, SelectArea, fromdate, todate, int.Parse(Status), PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                    totalRecordclient = dtlist[0].Rows.Count;
                    totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                    //back to set in filter//
                    modFilter.TotalRecord = TotalRecord;
                    modFilter.TotalPage = TotalPage;
                    modFilter.pagingsizeclient = pagingsizeclient;
                    modFilter.totalRecordclient = totalRecordclient;
                    modFilter.totalPageclient = totalPageclient;
                    modFilter.pagenumberclient = pagenumberclient;
                    bool isModeFilter = modFilter.isModeFilter;

                    //set to object pendataran//
                    Regmitra.DTAllTx = dtlist[0];
                    Regmitra.DTHeaderTx = dtlist[1];
                    Regmitra.FilterTransaksi = modFilter;
                    Regmitra.Permission = PermisionModule;

                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = Regmitra;
                    TempData[tempcommon] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    ViewBag.menu = menu;
                    ViewBag.caption = caption;
                    ViewBag.captiondesc = menuitemdescription;
                    ViewBag.rute = "";
                    ViewBag.action = "";

                    //string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    //ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + totalRecordclient.ToString() + " Kontrak" + filteron;
                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitLst.cshtml", Regmitra),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {
                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = Regmitra;
                    TempData[tempcommon] = Common;

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

        public async Task<ActionResult> clnchkfle(string secnocon, string coontpe, string clnfdc)
        {

            string EnumMessage = "";
            string filenamevar = "View Dokumen";
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                //sumber data pemberkasan //

                string caption = modFilter.idcaption;
                string moduleID = HasKeyProtect.Decryption(caption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string mode = moduleID;

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowDownload).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                if (Regmitra.CheckWithKey == "loadfile")
                {
                    TempData["dtfile"] = null;
                    Regmitra.CheckWithKey = "";
                }
                //cek pada sesion terlebih dulu
                List<cDocumentsGet> dttemp = (TempData["dtfile"] as List<cDocumentsGet>);

                string infokon = "";
                string infofisrst = "";


                if (clnfdc == "ckalleddxtx" || clnfdc == "ckalleddxtxp") /* upload pl pada grid yang sudah ttd*/
                {
                    secnocon = "MNL_" + HashNetFramework.HasKeyProtect.Decryption(secnocon).Replace("/", "").Replace("-", "");
                }
                else
                {
                    secnocon = HashNetFramework.HasKeyProtect.Decryption(secnocon);
                }


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

                    if (secnocon == "chkall")
                    {
                        secnocon = "-99";
                    }

                    //load ke DB jika disesion tidak ada//
                    modFilter.ID = ID;
                    modFilter.NoPerjanjian = secnocon;
                    modFilter.SelectJenisKontrak = HasKeyProtect.Decryption(coontpe);
                    modFilter.secIDFDC = clnfdc;
                    string tipe = coontpe == "pretemp" ? "5" : "2";
                    if (clnfdc == "ckalleddxtx" || clnfdc == "ckalleddxtxp")
                    {
                        dttemp = await Commonddl.Getdocview(0, secnocon.ToString(), "21", moduleID, UserID, GroupName);
                    }
                    else
                    {
                        dttemp = await Commonddl.Getdocview(int.Parse(secnocon.ToString()), "", tipe, moduleID, UserID, GroupName);
                    }
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
                string passwd = "";
                if (dttemp.Count > 0)
                {
                    filenamevar = dttemp[0].ExternalName;
                    passwd = dttemp[0].password;
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
                            string KECEP = HasKeyProtect.Encryption("dodol");
                            imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);

                            byte[] pass = Encoding.ASCII.GetBytes(passwd);
                            finalPdf = new iTextSharp.text.pdf.PdfReader(imageBytes, pass);
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
                        /*
                        MemoryStream msFinalPdfecp = new MemoryStream();
                        finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                        string password = passwd;
                        PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                        res = msFinalPdfecp.ToArray();
                        */
                    }
                }
                else
                {
                    EnumMessage = "Dokumen tidak ditemukan";
                }

                string nokonview = "";
                try
                {
                    nokonview = "Dokumen "; //+ dttemp[0].Document_Type.ToString()
                }
                catch
                {

                }

                if (secnocon == "chkall" || secnocon == "-99")
                {
                    EnumMessage = "";
                    nokonview = "Pengecekan Dokumen";
                    string pathfile = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\checkfile.pdf";
                    res = System.IO.File.ReadAllBytes(pathfile);
                }

                //download di password//
                if (clnfdc == "ckalledd" || clnfdc == "ckalleddx" || clnfdc == "ckalleddxtx" || clnfdc == "ckalleddxtxp")
                {
                    clnfdc = "1";
                    if (AllowGenerate == false)
                    {
                        EnumMessage = "User Tidak memiliki akses";
                    }
                    else
                    {
                        iTextSharp.text.pdf.PdfReader finalPdf;
                        MemoryStream msFinalPdfecp = new MemoryStream();
                        finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                        string password = passwd;
                        PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                        res = msFinalPdfecp.ToArray();
                    }
                }

                var contenttypeed = "application/pdf";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?paridfdc=" + secnocon + "&parmod=" + mode + "&parpowderdockd=wako&infokon=" + infokon + "&file=";
                //viewpathed = "data:application/pdf;base64," + base4;
                var jsonresult = Json(new { view = viewhml, moderror = IsErrorTimeout, dwn = clnfdc, infoselect = infofisrst, bytetyipe = res, msg = EnumMessage, cap = nokonview, contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
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

        public async Task<ActionResult> clnOpenclikp(string secnocon, string ty)
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                //sumber data pemberkasan //

                string caption = modFilter.ModuleID;
                string moduleID = HasKeyProtect.Decryption(caption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string mode = moduleID;

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowGenerate).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                string Message = "";

                int ID = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == secnocon).Select(x => x.Field<int>("Id")).SingleOrDefault();

                DataTable dt = await Regmitraddl.dbPublishPKS(ID.ToString(), ty, mode, UserID, GroupName);
                int result = int.Parse(dt.Rows[0][0].ToString());
                Message = result == 0 ? "Pastikan PKS dari Cabang/Area sudah diupload pada sistem" : EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                string mspks = "";
                if (result == 1)
                {
                    if (ty == "1")
                    {
                        Message = String.Format(Message, "PKS/SPL", "Dibagikan");
                        mspks = "PKS/SPL akan dapat didownload oleh Semua User";
                    }
                    else
                    {
                        Message = String.Format(Message, "Berbagi PKS/SPL", "Dihentikan");
                        mspks = "PKS/SPL Tidak dapat didownload oleh Semua User";
                    }
                }


                return Json(new
                {
                    moderror = IsErrorTimeout,
                    msg = Message,
                    swltitle = "Apakah anda yakin ?",
                    msgcfm = mspks,
                    swltype = "warning",
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

        public async Task<ActionResult> clnchkfledl(string secnocon, string geolo)
        {

            string EnumMessage = "";
            string filenamevar = "View Dokumen";
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
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                //sumber data pemberkasan //

                string caption = modFilter.idcaption;
                string moduleID = HasKeyProtect.Decryption(caption);
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;

                string mode = moduleID;

                bool AllowGenerate = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowGenerate).SingleOrDefault();
                modFilter.chalowses = AllowGenerate;

                secnocon = HashNetFramework.HasKeyProtect.Decryption(secnocon);
                DataTable dtx;
                if (geolo == "pretemp")
                {
                    dtx = await Regmitraddl.dbSaveDocTemp(secnocon, "88", "", "", "", "0", "", moduleID, UserID, GroupName);
                }
                else
                {
                    dtx = await Regmitraddl.dbSaveRegMitradoc(secnocon, "", "", "", "", "88", "", "", "", "0", "", moduleID, UserID, GroupName);
                }

                int resultsuct = int.Parse(dtx.Rows[0][0].ToString());

                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct);
                if (resultsuct == 1)
                {
                    EnumMessage = string.Format(EnumMessage, "Dokumen ", "dihapus");
                }
                var jsonresult = Json(new { view = "", moderror = IsErrorTimeout, msg = EnumMessage, rst = resultsuct, JsonRequestBehavior.AllowGet });
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


        #region upload template dokumen
        public async Task<ActionResult> clnKoncePloddoc(string paridno, string parkepo)
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
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLOADTEMPL";
                string moduleID = HasKeyProtect.Decryption(caption);
                //if (Common.ddlDocument == null)

                cRegmitra reg = new cRegmitra();
                reg.Dokumen = await Commonddl.dbdbGetJenisDokumen("3", "", "", UserID, GroupName);
                //ViewData["SelectDocument"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                modFilter.idcaption = HasKeyProtect.Encryption(caption);
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Pengkinian Template Dokumen";

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitDocTemplate.cshtml", reg),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePloddocsve(HttpPostedFileBase[] files, string[] documen, string cntproo)
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

            string urlpath = "";
            if (IsErrorTimeout == true)
            {
                urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            string parcno = "";
            string keylokup = parcno;
            string EnumMessage = "";
            int result = 0;
            string viewtbl = "";

            Regmitra = TempData[tempTransksi] as vmRegmitra;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);


            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = (modFilter.idcaption ?? modFilter.ModuleID);

                string moduleID = HasKeyProtect.Decryption(caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();
                bool AllowSubmit = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowSubmit).SingleOrDefault();


                string FlagOpr = "";
                string notransaksi = "";
                string RegType = "";
                string Noktp = "";
                string NIK = "";
                string ID = "";
                string IDUpload = "";
                string IDresult = "0";
                cntproo = HasKeyProtect.Decryption(cntproo);
                string[] cntproodata = cntproo.Split('|');

                if (cntproo != "templod")
                {
                    FlagOpr = HasKeyProtect.Decryption(cntproodata[0]);
                    notransaksi = (cntproodata[1]);
                    RegType = (cntproodata[2]);
                    Noktp = (cntproodata[3]);
                    NIK = (cntproodata[4]);
                    ID = (cntproodata[5]);
                }

                if (AllowUpload == true || (AllowSubmit == true))
                {

                    if (files != null)
                    {
                        foreach (HttpPostedFileBase file in files)
                        {

                            decimal sizep = file.ContentLength / 1024 / 102;
                            if (file.FileName.Length > 50)
                            {
                                EnumMessage = "Nama File " + file.FileName + " tidak boleh lebih dari 50 karakter";
                            }
                            else
                            if (!file.ContentType.ToLower().Contains("jpg") && !file.ContentType.ToLower().Contains("jpeg") && !file.ContentType.ToLower().Contains("pdf"))
                            {
                                EnumMessage = "File " + file.FileName + " harus Extention jpg,jpeg,pdf";
                            }
                            else
                            if (sizep > 350)
                            {
                                EnumMessage = "File " + file.FileName + " Ukuran File harus lebih kecil dari 350 KB";
                            }
                        }

                        if (EnumMessage == "")
                        {
                            var idoc = 0;
                            foreach (HttpPostedFileBase file in files)
                            {

                                byte[] imagebyte = null;
                                BinaryReader reader = new BinaryReader(file.InputStream);
                                imagebyte = reader.ReadBytes((int)file.ContentLength);

                                //get mimne type
                                string mimeType = file.ContentType;
                                if (mimeType.Contains("image"))
                                {
                                    imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                                }

                                //prepare to encrypt
                                string KECEP = "dodol";
                                string KECEPDB = KECEP;
                                KECEP = HasKeyProtect.Encryption(KECEP);
                                byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                                //convert byte to base//
                                string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                                string DocumentType = HasKeyProtect.Decryption(documen[idoc]);
                                string FileName = DocumentType + ".pdf";

                                string ContentType = "Application/pdf";
                                string ContentLength = filebyteECP.Length.ToString();
                                string FileByte = base64String;

                                DataTable dtx;

                                if (cntproo != "templod")
                                {
                                    dtx = await Regmitraddl.dbSaveRegMitradoc("0", FlagOpr, "", Noktp, notransaksi, RegType, DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                                }
                                else
                                {
                                    dtx = await Regmitraddl.dbSaveDocTemp("0", "", DocumentType, FileName, ContentType, ContentLength, FileByte, moduleID, UserID, GroupName);
                                }

                                idoc = idoc + int.Parse(dtx.Rows[0][0].ToString());
                                IDresult = dtx.Rows[0][1].ToString();
                                IDUpload = HasKeyProtect.Encryption(dtx.Rows[0][1].ToString());
                                // disini ditambahkan jika gagal menyimpan do
                            }

                            if (files.Count() != int.Parse(idoc.ToString()))
                            {
                                EnumMessage = "Terdapat Dokumen yang tidak terupload silahkan dicek kembali";
                            }

                            result = 1;
                        }
                    }
                    else
                    {
                        EnumMessage = "Tidak ada penambahan/perubahan pada template dokumen";
                    }
                }
                else
                {
                    EnumMessage = "User tidak memiliki akses";
                }
                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = EnumMessage,
                    resulted = result,
                    flag = "",
                    idhome = "",
                    modl = caption,
                    url = urlpath,
                    idrst = IDresult,
                    golpod = IDUpload
                });
            }

            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
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
        public async Task<ActionResult> clnKoncePloddocResend()
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

                // get user group & akses //
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;


                DataTable dataupload = new DataTable();
                dataupload = await Commonddl.dbdbGetJenisDokumen("3", "", "", UserID, GroupName);

                DataTable DocumentByte = new DataTable();
                //var dbx = new DropboxClient(keybox);

                const int bufferSize = 104857600;
                var buffer = new byte[bufferSize];

                string statused = "";

                ZipEntry newZipEntry = new ZipEntry();
                using (var memoryStream = new MemoryStream())
                {
                    using (var zip = new ZipFile())
                    {
                        //zip.Password = "simastra2022"; //HasKeyProtect.Encryption(LoginAksesKey);
                        foreach (DataRow rowsdata in dataupload.Rows)
                        {
                            string pathdwn = "Template Pendaftaran/" + rowsdata["DIVISI_NAME"] + "/" + rowsdata["DOCUMENT_TYPE"] + ".pdf";
                            try
                            {
                                cFilterContract models = new cFilterContract();
                                models.ID = "";

                                byte[] imageBytes = Convert.FromBase64String(rowsdata["FileByte"].ToString());
                                string pooo = "dodol";
                                string KECEP = HasKeyProtect.Encryption(pooo);
                                imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                                zip.AddEntry(pathdwn, imageBytes);
                            }
                            catch
                            {
                                statused = "not found";
                            }
                        }
                        zip.Save(memoryStream);
                    }
                    buffer = memoryStream.ToArray();
                }

                string minut = DateTime.Now.ToString("ddMMyyyymmss");
                string filenamepar = "Template Dokumen_" + minut + ".zip";
                return File(buffer, "application/zip", filenamepar);
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

        #endregion upload template dokumen


        #region upload SPPI
        public async Task<ActionResult> clnKoncePlod(string paridno, string parkepo)
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
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLODSPPIMITRA";
                string moduleID = HasKeyProtect.Decryption(caption);
                //if (Common.ddlDocument == null)
                {
                    Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenList("1", "", "", UserID, GroupName);
                }
                ViewData["SelectDocument"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                modFilter.idcaption = caption;
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Pengkinian SPPI Mitra";

                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitUpload.cshtml", Regmitra),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePlodsve(HttpPostedFileBase files, string idx, string modepro, string tglpro)
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

            string parcno = "";
            string keylokup = parcno;
            string Jenis_Doc = "Kartu SPPI";

            Regmitra = TempData[tempTransksi] as vmRegmitra;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);

            byte[] bytefl = bytefl = Encoding.ASCII.GetBytes("");
            string filename = "";
            string filetype = "";
            string validmsg = "";
            int resultsuct = 0;

            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = (caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();

                if (AllowUpload == true)
                {
                    if (modepro == "chk")
                    {

                        var xmlString = "";
                        XmlDocument xml = new XmlDocument();
                        xmlString = Server.MapPath(Request.ApplicationPath) + "External\\TemplateAndal\\TemplateSPPI_Check.xml";
                        xml.Load(xmlString);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                        nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
                        XmlElement root = xml.DocumentElement;

                        DataTable dtlist = await Regmitraddl.dbGetTxdata4andal("SP", DateTime.Parse(tglpro).ToString("yyyyMMdd"), caption, UserID, GroupName);
                        XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

                        foreach (DataRow rw in dtlist.Rows)
                        {
                            XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlAttrRow.Value = "0";
                            XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                            xmlRecordNo.Attributes.Append(xmlAttrRow);
                            foreach (DataColumn col in dtlist.Columns)
                            {
                                XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                                XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                                string attrval = col.ColumnName.Split('_')[1].ToString();
                                xmlAttrNumber.Value = attrval;
                                XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                                xmlRecordNoceldata.InnerText = rw[col].ToString();
                                xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                                xmlRecordNo.AppendChild(xmlRecordNocel);
                                xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                            }
                            nodesing.AppendChild(xmlRecordNo);
                        }

                        filename = "DataSPPIMitra_check.xml";
                        filetype = "application/xml";
                        bytefl = Encoding.ASCII.GetBytes(xml.OuterXml);
                    }
                    else
                    {
                        validmsg = await Commonddl.dbValidFileupload(files, "SPPI", moduleID, UserID, GroupName);
                        if (validmsg == "")
                        {

                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(files.InputStream);
                            imagebyte = reader.ReadBytes((int)files.ContentLength);

                            //get mimne type
                            string mimeType = files.ContentType;
                            if (mimeType.Contains("image"))
                            {
                                imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                            }

                            //prepare to encrypt
                            string KECEP = "dodol";
                            string KECEPDB = KECEP;
                            KECEP = HasKeyProtect.Encryption(KECEP);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                            string DocumentType = Jenis_Doc;
                            string FileName = DocumentType + ".pdf";

                            string ContentType = "Application/pdf";
                            string ContentLength = filebyteECP.Length.ToString();
                            string FileByte = base64String;

                            var splitfilename = files.FileName.Split('_');
                            string NoKTP_NIK = splitfilename[0];
                            string NOSPPI = splitfilename[1];
                            NOSPPI = splitfilename[1].Replace(".jpg", "").Replace(".jpeg", "").Replace(".JPG", "").Replace(".JPEG", "").Replace(".pdf", "").Replace(".PDF", "");

                            /*get nik or ktp*/
                            DataTable dt = await Commonddl.dbdbGetDdlMitraListByEncrypt(splitfilename[0], "", "40", caption, UserID, GroupName);
                            string NIK = dt.Rows[0]["NIKBaru"].ToString();
                            string NoKTP = dt.Rows[0]["NoKTP"].ToString();
                            DataTable dtx = await Regmitraddl.dbSaveRegMitradoc("0", "", NIK, NoKTP, NOSPPI, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, caption, UserID, GroupName);
                            resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                            validmsg = (resultsuct != 1) ? EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct) : "";

                        }
                    }
                }
                else
                {
                    validmsg = "User tidak memiliki akses";
                }
                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    filmsg = validmsg,
                    filidx = int.Parse(idx) + 1,
                    resultsuc = resultsuct,
                    flbt = bytefl,
                    flbtnm = filename,
                    flbtmtype = filetype,
                    mode = modepro,
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
        #endregion upload SPPI

        #region upload PKS Draft
        public async Task<ActionResult> clnKoncePlodpk(string paridno, string parkepo)
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
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLODPKSMITRA";
                string moduleID = HasKeyProtect.Decryption(caption);
                //if (Common.ddlDocument == null)
                {
                    Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenList("2", "", "", UserID, GroupName);
                }
                ViewData["SelectDocument"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                modFilter.idcaption = caption;
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Pengiriman PKS Mitra";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitUpload.cshtml", Regmitra),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePlodpksve(HttpPostedFileBase files, string idx)
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

            string parcno = "";
            string keylokup = parcno;
            string Jenis_Doc = "PKS Mitra";

            Regmitra = TempData[tempTransksi] as vmRegmitra;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);

            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = (caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();

                string validmsg = "";
                int resultsuct = 0;
                if (AllowUpload == true)
                {
                    validmsg = await Commonddl.dbValidFileupload(files, "PKS", moduleID, UserID, GroupName);
                    if (validmsg == "")
                    {

                        byte[] imagebyte = null;
                        BinaryReader reader = new BinaryReader(files.InputStream);
                        imagebyte = reader.ReadBytes((int)files.ContentLength);

                        //get mimne type
                        string mimeType = files.ContentType;
                        if (mimeType.Contains("image"))
                        {
                            imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                        }

                        //prepare to encrypt
                        string KECEP = "dodol";
                        string KECEPDB = KECEP;
                        KECEP = HasKeyProtect.Encryption(KECEP);
                        byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                        //convert byte to base//
                        string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                        string DocumentType = Jenis_Doc;
                        string FileName = DocumentType + ".pdf";

                        string ContentType = "Application/pdf";
                        string ContentLength = filebyteECP.Length.ToString();
                        string FileByte = base64String;

                        var splitfilename = files.FileName.Split('_');
                        string NoKTP_NIK = splitfilename[0];
                        string NOSPPI = splitfilename[1];
                        NOSPPI = splitfilename[1].Replace(".jpg", "").Replace(".jpeg", "").Replace(".JPG", "").Replace(".JPEG", "").Replace(".pdf", "").Replace(".PDF", "");

                        /*get nik or ktp*/
                        DataTable dt = await Commonddl.dbdbGetDdlMitraListByEncrypt(splitfilename[0], "", "40", caption, UserID, GroupName);
                        string NIK = dt.Rows[0]["NIKBaru"].ToString();
                        string NoKTP = dt.Rows[0]["NoKTP"].ToString();
                        DataTable dtx = await Regmitraddl.dbSaveRegMitradoc("0", "", NIK, NoKTP, NOSPPI, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, "", UserID, GroupName);
                        resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                        if (resultsuct != 1)
                        {
                            validmsg = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct);
                        }
                    }

                }
                else
                {
                    validmsg = "user tidak memiliki akses";
                }

                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    filmsg = validmsg,
                    filidx = int.Parse(idx) + 1,
                    resultsuc = resultsuct
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
        #endregion upload PKS Draft


        #region upload SPL Draft
        public async Task<ActionResult> clnKoncePlodspl(string paridno, string parkepo)
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
                string GroupName = Account.AccountLogin.GroupName;
                string caption = "UPLODSPLMITRA";
                string moduleID = HasKeyProtect.Decryption(caption);
                //if (Common.ddlDocument == null)
                {
                    Common.ddlDocument = await Commonddl.dbdbGetJenisDokumenList("21", "", "", UserID, GroupName);
                }
                ViewData["SelectDocument"] = OwinLibrary.Get_SelectListItem(Common.ddlDocument);

                modFilter.idcaption = caption;
                TempData[tempTransksi] = Regmitra;
                TempData[tempTransksifilter] = modFilter;
                TempData["common"] = Common;

                ViewBag.captiondesc = "Pengiriman SPL Mitra";
                ViewBag.caption = caption;
                // senback to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    ops1 = "",
                    ops2 = "",
                    ops3 = "",
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Regmitra/uiRegmitUpload.cshtml", Regmitra),
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                Response.StatusCode = 406;
                Response.TrySkipIisCustomErrors = true;
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKoncePlodsplsve(HttpPostedFileBase files, string idx)
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

            string parcno = "";
            string keylokup = parcno;
            string Jenis_Doc = "SPL Mitra";

            Regmitra = TempData[tempTransksi] as vmRegmitra;
            modFilter = TempData[tempTransksifilter] as cFilterContract;
            Common = (TempData["common"] as vmCommon);

            try
            {
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                string moduleID = (caption);
                bool AllowUpload = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == moduleID).Select(x => x.AllowUpload).SingleOrDefault();

                string validmsg = "";
                int resultsuct = 0;
                if (AllowUpload == true)
                {
                    validmsg = await Commonddl.dbValidFileupload(files, "SPL", moduleID, UserID, GroupName);
                    if (validmsg == "")
                    {

                        byte[] imagebyte = null;
                        BinaryReader reader = new BinaryReader(files.InputStream);
                        imagebyte = reader.ReadBytes((int)files.ContentLength);

                        //get mimne type
                        string mimeType = files.ContentType;
                        if (mimeType.Contains("image"))
                        {
                            imagebyte = OwinLibrary.ConvertImageByteToPDFByte(imagebyte);
                        }

                        //prepare to encrypt
                        string KECEP = "dodol";
                        string KECEPDB = KECEP;
                        KECEP = HasKeyProtect.Encryption(KECEP);
                        byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);

                        //convert byte to base//
                        string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);

                        string DocumentType = Jenis_Doc;
                        string FileName = DocumentType + ".pdf";

                        string ContentType = "Application/pdf";
                        string ContentLength = filebyteECP.Length.ToString();
                        string FileByte = base64String;

                        var splitfilename = files.FileName.Split('_');
                        string NoKTP_NIK = splitfilename[0];
                        string NOSPPI = splitfilename[1];
                        NOSPPI = splitfilename[1].Replace(".jpg", "").Replace(".jpeg", "").Replace(".JPG", "").Replace(".JPEG", "").Replace(".pdf", "").Replace(".PDF", "");

                        /*get nik or ktp*/
                        DataTable dt = await Commonddl.dbdbGetDdlMitraListByEncrypt(splitfilename[0], "", "40", caption, UserID, GroupName);
                        string NIK = dt.Rows[0]["NIKBaru"].ToString();
                        string NoKTP = dt.Rows[0]["NoKTP"].ToString();
                        DataTable dtx = await Regmitraddl.dbSaveRegMitradoc("0", "", NIK, NoKTP, NOSPPI, "99", DocumentType, FileName, ContentType, ContentLength, FileByte, "", UserID, GroupName);
                        resultsuct = int.Parse(dtx.Rows[0][0].ToString());
                        if (resultsuct != 1)
                        {
                            validmsg = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultsuct);
                        }
                    }

                }
                else
                {
                    validmsg = "user tidak memiliki akses";
                }

                ////send to session for filter data//
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;


                //string[] document = documen;
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    filmsg = validmsg,
                    filidx = int.Parse(idx) + 1,
                    resultsuc = resultsuct
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
        #endregion upload SPL Draft


        #region SPLMitra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> kumonprosplitervwcoll(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string NIKMitra = "";

                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractSPLNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                Byte[] res = SPLRECOCOL(NomorSPL, AtasNamaPKS, JabatanAtasNamaPKS, "", NamaMitra, KtpMitra, TglMasukMitra, TglAkhirMitra, tglhariini); ;

                var contenttypeed = "application/pdf";
                string powderdockp = "0";
                string powderdockd = "0";

                string filenamevar = (NIKMitra + "_" + NomorSPL.Replace("/", "").Replace("-", "") + "_" + NamaMitra) + ".pdf";

                //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=&file=";
                var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = res, msg = "", contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
                jsonresult.MaxJsonLength = int.MaxValue;
                return jsonresult;

                //create area footer//
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
        public async Task<ActionResult> kumonprosplitercoll(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string NIKMitra = "";

                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractSPLNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                Byte[] res = SPLRECOCOL(NomorSPL, AtasNamaPKS, JabatanAtasNamaPKS, "", NamaMitra, KtpMitra, TglMasukMitra, TglAkhirMitra, tglhariini); ;

                string minut = DateTime.Now.ToString("ddMMyyyymmss");
                string filenamepar = (NIKMitra + "_" + NomorSPL.Replace("/", "").Replace("-","") + "_" + NamaMitra) + ".pdf";


                iTextSharp.text.pdf.PdfReader finalPdf;
                MemoryStream msFinalPdfecp = new MemoryStream();
                finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                string password = NIKMitra;
                PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                res = msFinalPdfecp.ToArray();

                return File(res, "application/pdf", filenamepar);
                //create area footer//
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
        public async Task<ActionResult> kumonprosplitervwwopi(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string NIKMitra = "";
                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractSPLNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                Byte[] res = SPLWOPI(NomorSPL, AtasNamaPKS, JabatanAtasNamaPKS, "", NamaMitra, KtpMitra, TglMasukMitra, TglAkhirMitra, tglhariini);

                var contenttypeed = "application/pdf";
                string powderdockp = "0";
                string powderdockd = "0";

                string filenamevar = (NIKMitra + "_" + NomorSPL.Replace("/", "").Replace("-", "") + "_" + NamaMitra) + ".pdf";
                //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=&file=";
                var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = res, msg = "", contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
                jsonresult.MaxJsonLength = int.MaxValue;
                return jsonresult;
                //create area footer//
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
        public async Task<ActionResult> kumonprospliterwopi(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string NIKMitra = "";
                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractSPLNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                Byte[] res = SPLWOPI(NomorSPL, AtasNamaPKS, JabatanAtasNamaPKS, "", NamaMitra, KtpMitra, TglMasukMitra, TglAkhirMitra, tglhariini);

                string minut = DateTime.Now.ToString("ddMMyyyymmss");

                string filenamepar = (NIKMitra + "_" + NomorSPL.Replace("/", "") + "_" + NamaMitra) + ".pdf";

                iTextSharp.text.pdf.PdfReader finalPdf;
                MemoryStream msFinalPdfecp = new MemoryStream();
                finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                string password = NIKMitra;
                PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                res = msFinalPdfecp.ToArray();

                return File(res, "application/pdf", filenamepar);
                //create area footer//
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
        public async Task<ActionResult> kumonprosplitervwsale(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string NIKMitra = "";

                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractSPLNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                Byte[] res = SPLSales(NomorSPL, AtasNamaPKS, JabatanAtasNamaPKS, "", NamaMitra, KtpMitra, TglMasukMitra, TglAkhirMitra, tglhariini);

                var contenttypeed = "application/pdf";
                string powderdockp = "0";
                string powderdockd = "0";

                string filenamevar = (NIKMitra + "_" + NomorSPL.Replace("/", "").Replace("-", "") + "_" + NamaMitra) + ".pdf";

                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=&file=";
                var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = res, msg = "", contenttype = contenttypeed, filename = filenamevar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> kumonprosplitersale(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string NIKMitra = "";
                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractSPLNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));

                Byte[] res = SPLSales(NomorSPL, AtasNamaPKS, JabatanAtasNamaPKS, "", NamaMitra, KtpMitra, TglMasukMitra, TglAkhirMitra, tglhariini);

                string minut = DateTime.Now.ToString("ddMMyyyymmss");
                string filenamepar = (NIKMitra + "_" + NomorSPL.Replace("/", "").Replace("-", "") + "_" + NamaMitra) + ".pdf";

                iTextSharp.text.pdf.PdfReader finalPdf;
                MemoryStream msFinalPdfecp = new MemoryStream();
                finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                string password = NIKMitra;
                PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                res = msFinalPdfecp.ToArray();

                return File(res, "application/pdf", filenamepar);
                //create area footer//
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

        #endregion SPLMitra

        #region PKS
        //PKS MITRA coll-reco//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> kumonpropakslitercoll(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NamaPT = "";
                string AlamatPT = "";
                string AlamatPT1 = "";
                string TelpPT = "";
                string FaxPT = "";

                string NomorSPL = "";
                string NomorMOU = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string AlamatMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string handlejob = "";

                string hari = "";
                string tanggal = "";
                string bulan = "";
                string tahun = "";
                string tanggalangka = "";
                string periodekontrak = "";

                string handphone = "";
                string telpone = "";

                string bank = "";
                string cabangbank = "";
                string norek = "";
                string atasnamarek = "";
                string NIKMitra = "";

                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                NomorMOU = rw["ContractNo"].ToString();
                NamaPT = rw["PTNama"].ToString();
                AlamatPT = rw["PTAlamat"].ToString();
                AlamatPT1 = rw["PTAlamat1"].ToString();
                TelpPT = rw["PTTelp"].ToString();
                FaxPT = rw["PTFax"].ToString();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                AlamatMitra = rw["Alamat"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                telpone = rw["NoHPPKS"].ToString();
                handlejob = "";
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                hari = rw["Hari"].ToString().ToLower();
                tanggal = rw["Tanggal"].ToString().ToLower();
                bulan = rw["Bulan"].ToString().ToLower();
                tahun = rw["Tahun"].ToString().ToLower();
                tanggalangka = rw["tanggalangka"].ToString().ToLower();
                periodekontrak = rw["PeriodeKontrak"].ToString();

                bank = rw["NamaBankPKS"].ToString();
                cabangbank = rw["CabangBankPKS"].ToString();
                norek = rw["NorekeningPKS"].ToString();
                atasnamarek = rw["PemilikkeningPKS"].ToString();

                Byte[] res = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    var configurationOptions = new PdfGenerateConfig();

                    //Page is in Landscape mode, other option is Portrait
                    configurationOptions.PageOrientation = PdfSharp.PageOrientation.Portrait;

                    //Set page type as Letter. Other options are A4 …
                    configurationOptions.PageSize = PdfSharp.PageSize.A4;
                    //This is to fit Chrome Auto Margins when printing.Yours may be different
                    configurationOptions.MarginTop = 2;
                    configurationOptions.MarginLeft = 2;
                    configurationOptions.MarginRight = 2;
                    configurationOptions.MarginBottom = 2;

                    StringBuilder HTMLContent = new StringBuilder();
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    string locimg = Server.MapPath(Request.ApplicationPath) + "Images\\logosms1.png";

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<b><center>PERJANJIAN KERJASAMA MITRA PENAGIHAN <br /> No : " + NomorMOU + "</center></b>");
                    HTMLContent.Append("<p>Pada hari ini " + hari + ", tanggal " + tanggal + ", bulan " + bulan + " " + tahun + "(" + tanggalangka + "), telah dibuat dan ditandatangani Perjanjian Kerjasama Mitra Penagihan oleh dan antara: </p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li><b>" + NamaPT + "</b>, perseroan terbatas yang didirikan berdasarkan hukum  Negara Republik Indonesia, berkedudukan di Jakarta Pusat dan berkantor di Jalan Johar No. 22,<br/>" +
                                        "Kebon Sirih, Menteng, Jakarta Pusat 10340 dan dalam perbuatan hukum ini diwakili oleh <b>" + AtasNamaPKS + "</b>, bertindak dalam kedudukannya selaku " + JabatanAtasNamaPKS + ", dengan demikian <br/>" +
                                        "sah bertindak untuk dan atas nama " + NamaPT + ", (selanjutnya disebut sebagai <b>“PT SMS”</b>)" +
                                       "</li>");

                    HTMLContent.Append("<li><b>" + NamaMitra + "</b> pemegang Kartu Tanda Penduduk (KTP) nomor :<b>" + KtpMitra + "</b>, beralamat di " + AlamatMitra + " dalam hal " +
                                       "ini bertindak untuk dan atas nama diri sendiri (selanjutnya disebut <b>“Mitra”</b>).</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p>PT SMS dan Mitra dalam hal secara bersama-sama disebut “Para Pihak” dan masing-masing disebut “Pihak”, terlebih dahulu menerangkan hal-hal sebagai berikut :<br/>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Bahwa PT SMS adalah perseroan terbatas yang menjalankan usaha di bidang jasa konsultasi manajemen sumber daya manusia, termasuk untuk melakukan penagihan utang;<br/>");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Bahwa Mitra adalah perseorangan, yang mempunyai persyaratan dan kemampuan untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Bahwa PT SMS bermaksud membuka kesempatan menjadi mitra PT SMS (“Mitra”) bagi pihak-pihak yang bersedia untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("d.<span style='margin-left:5px'/>Bahwa Mitra dengan ini menyatakan kesediaannya untuk menjadi Mitra PT SMS dalam melakukan penagihan.</p>");

                    HTMLContent.Append("<p>Bahwa Para Pihak telah setuju dan sepakat untuk membuat, menetapkan, melaksanakan dan mematuhi Perjanjian Kerjasama Mitra Penagihan ini untuk selanjutnya disebut “Perjanjian” dengan syarat dan ketentuan sebagai berikut </p>");
                    HTMLContent.Append("<p><center><b>PASAL 1<br />DEFINISI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>”Penagihan” adalah kegiatan-kegiatan yang dilakukan oleh Mitra yang termasuk namun tidak terbatas dalam penagihan angsuran, penerimaan barang dan kegiatan lainnya yang tertera pada Surat Tugas dan tidak bertentangan dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>”Barang” adalah setiap barang-barang berwujud tahan lama seperti kendaraan bermotor mobil dan/atau motor dan/atau barang lainnya, Elektronik, Furniture, Komputer, Handphone dan barang Durable Goods yang merupakan objek pembiayaan dan/atau barang yang menjadi jaminan dalam suatu perjanjian tertulis antara Klien dengan debitur klien.</li>");
                    HTMLContent.Append("<li>”Debitur” adalah nasabah klien yang telah membuat dan menandatangani perjanjian tertulis dengan klien</li>");
                    HTMLContent.Append("<li>”Surat Tugas” adalah Surat Tugas yang diterbitkan klien kepada PT SMS yang diwakili oleh Mitra untuk melakukan penagihan kewajiban/utang dan penerimaan jaminan debitur klien.</li>");
                    HTMLContent.Append("<li>”Surat Penunjukan” adalah surat yang dikeluarkan oleh PT SMS untuk Mitra yang menunjukkan kepada klien,  bahwa pihak yang bersangkutan adalah Mitra PT SMS.</li>");
                    HTMLContent.Append("<li>”Dokumen Penagihan” adalah dokumen-dokumen yang diperlukan dalam proses Penagihan, termasuk namun tak terbatas pada Surat Penunjukan, Surat Tugas, Berita Acara Penerimaan Barang, Tanda Terima Angsuran, dan dokumen lain yang diperlukan pada saat Penagihan.</li>");
                    HTMLContent.Append("<li>”Klien” adalah pihak yang memberikan tugas kepada PT SMS untuk melakukan penagihan utang dan/atau penerimaan Barang dari Debitur.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 2<br />RUANG LINGKUP</b></center></p>");
                    HTMLContent.Append("<p>Mitra setuju untuk membantu PT SMS dalam melakukan Penagihan sesuai dengan Surat Penunjukan dan/atau Surat Tugas dari PT SMS atau Klien.<br />");
                    HTMLContent.Append("Bahwa dengan Perjanjian ini dan Surat Penunjukan dan/atau Surat Tugas dari PT SMS atau Klien kepada Mitra, tidak berarti bahwa Mitra menjadi karyawan dari PT SMS atau Klien..</p>");

                    HTMLContent.Append("<p><center><b>PASAL 3<br />HAK DAN KEWAJIBAN PT SMS</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS akan menerbitkan Surat Penunjukan kepada Mitra, khusus untuk dapat melaksanakan Penagihan.</li>");
                    HTMLContent.Append("<li>PT SMS wajib memberikan tembusan Surat Penunjukan yang diterbitkan kepada Klien paling lambat 1 (satu) hari kerja setelah Surat Penunjukan diterbitkan.</li>");
                    HTMLContent.Append("<li>PT SMS berhak membatalkan setiap saat Surat Penunjukan yang telah diterbitkan berdasarkan pertimbangan-pertimbangan dari PT SMS, hal mana tidak perlu dibuktikan pada Mitra, termasuk namun tak terbatas dalam hal tidak terpenuhinya ketentuan Pasal 4 ayat 1 Perjanjian ini.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 4<br />HAK DAN KEWAJIBAN MITRA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra wajib memiliki sertifikasi Profesi di bidang penagihan sesuai ketentuan perundang -undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban melaksanakan Penagihan berdasarkan Dokumen Penagihan serta tidak melanggar segala peraturan dan ketentuan perundang-undangan yang berlaku dan Mitra bertanggung jawab untuk segala tindakan yang dilakukan oleh Mitra.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban mengembalikan Dokumen Penagihan yang telah diterbitkan Klien kepada PT SMS, paling lambat 1 (satu) hari kerja setelah masa berlaku Dokumen Penagihan tersebut berakhir.</li>");
                    HTMLContent.Append("<li>Mitra  dilarang menyimpan Barang yang telah diterimakan dari Debitur dan/atau pihak lain dan wajib untuk segera menyerahkan Barang yang telah diterimakan dari Debitur dan/atau pihak lain kepada Klien, dalam waktu selambat-lambatnya 1 (satu) hari kerja sejak tanggal penerimaan kembali tersebut.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban membebaskan PT SMS dan/atau Klien dari segala tuntutan, gugatan atau kerugian yang timbul sebagai akibat dari tindakan Penagihan yang dilakukan Mitra.</li>");
                    HTMLContent.Append("<li>Untuk setiap penagihan yang berhasil dilakukan Mitra sesuai dengan tata cara sebagaimana tersebut dalam Perjanjian ini, maka Mitra berhak untuk mendapatkan <b><i>Success Fee</i></b> sesuai dengan ketentuan bagi hasil yang disepakati oleh Para Pihak.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 5<br />PEMBAYARAN <i>SUCCESS FEE</i></b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra akan mendapatkan <b><i>Success Fee</i></b>, setelah memenuhi ketentuan sebagai berikut :</li>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Mitra telah menerimakan titipan pembayaran angsuran dan/atau Barang titip jual, serta telah menyerahkan Barang yang dimaksud ke tempat yang telah ditentukan Klien.<br />");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Mitra telah membuat dan menandatangani dokumen tanda terima atas Barang yang telah diterimakan oleh Mitra.<br />");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Mitra menyerahkan  Dokumen Penagihan dimaksud kepada Klien.<br />");
                    HTMLContent.Append("<li>Pembayaran <b><i>Success Fee</i></b> akan dilakukan dengan cara transfer ke rekening Mitra yaitu sebagai berikut :");
                    HTMLContent.Append("<table width='50%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Nama Bank</td><td width='5px'>:</td><td align='left'>" + bank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Cabang Bank</td><td width='5px'>:</td><td align='left'>" + cabangbank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>No. Rekening</td><td width='5px'>:</td><td align='left'>" + norek + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Atas Nama</td><td width='5px'>:</td><td align='left'>" + atasnamarek + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("Nama yang tercatat pada rekening adalah nama pihak yang menandatangani Perjanjian ini. </li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 6<br />JANGKA WAKTU PERJANJIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Perjanjian ini berlaku selama " + periodekontrak + " mulai tanggal " + TglMasukMitra + " sampai dengan tanggal " + TglAkhirMitra + ";</li>");
                    HTMLContent.Append("<li>Para Pihak sepakat bahwa masing-masing pihak dapat mengakhiri Perjanjian ini secara sepihak dengan cara menyampaikan pemberitahuan kepada  pihak lainnya selambat-lambatnya 14 (empat belas) hari kalender sebelum tanggal pengakhiran Perjanjian yang dikehendaki.</li>");
                    HTMLContent.Append("<li>Dalam hal terjadi kondisi sebagaimana dimaksud pada ayat 2 Pasal ini, maka pengakhiran Perjanjian tersebut akan dicatat dalam sistem administrasi PT SMS, dan dengan demikian Surat Penunjukan yang pernah diterbitkan oleh PT SMS kepada Mitra menjadi tidak berlaku lagi.</li>");
                    HTMLContent.Append("<li>Berakhirnya Perjanjian ini tidak mengakhiri hubungan hukum dan kewajiban Para Pihak yang telah ada sebelum Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan adanya ketentuan pengakhiran dalam Perjanjian ini Para Pihak sepakat untuk melepaskan ketentuan pasal 1266 dan 1267 Kitab Undang - Undang Hukum Perdata yang berlaku di Indonesia.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:10px'>Halaman 1/2</p>");

                    HTMLContent.Append("</body></html>");

                    //page 2 //

                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<p><center><b>PASAL 7<br />SANKSI</b></center></p>");
                    HTMLContent.Append("<p>Apabila Mitra tidak melaksakan ketentuan sebagaimana dimaksud Pasal 4 Perjanjian, maka PT SMS dapat menahan pembayaran Success Fee Mitra sesuai kerugian yang diderita oleh PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 8<br />JAMINAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap dokumen jaminan yang diserahkan kepada PT SMS (jika ada) adalah dokumen milik Mitra atau diperoleh dari pihak lain sesuai hukum yang berlaku, apabila dokumen jaminan tersebut dimiliki atau diperoleh tidak sesuai hukum, maka Mitra melepaskan semua tuntutan dan/atau gugatan dari pihak lain kepada PT SMS dan/atau Klien dan mengganti seluruh biaya yang timbul akibat tuntutan dan/atau gugatan tersebut sehubungan dengan penyerahan dokumen jaminan kepada PT SMS.</li>");
                    HTMLContent.Append("<li>Mitra menjamin dalam menjalani kewajiban-kewajiban berdasarkan Perjanjian wajib mengikuti prosedur dari PT SMS, Klien dan sesuai dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa apa yang dinyatakan, baik pelaksanaan maupun dalam bentuk pernyataan dalam Perjanjian ini adalah benar dan tidak menyesatkan, apabila dikemudian hari hal-hal tersebut tidak sesuai, maka Mitra bertanggung jawab atas seluruh kerugian yang timbul kepada PT SMS dan/atau Klien akibat tidak sesuainya hal-hal yang disebutkan tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap kerugian yang timbul dalam bentuk apapun terhadap PT SMS akibat tidak dilaksanakan dan/atau tidak sesuai dengan Perjanjian ini maupun peraturan perundang-undangan yang berlaku, maka Mitra akan bertanggung jawab penuh atas kerugian tersebut.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 9<br />EVALUASI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS berhak untuk melaksanakan evaluasi berkala atas pelaksanaan Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Pelaksanaan evaluasi berkala sebagaimana dimaksud ayat 1 Pasal ini dilakukan paling sedikit sebanyak 2 (dua) kali selama masa Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 10<br />KORESPONDENSI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Untuk keperluan komunikasi dan surat menyurat serta pemberitahuan antara Para Pihak termasuk pemberitahuan tentang perubahan Penanggung Jawab sehubungan dengan pelaksanaan Perjanjian ini, maka disepakati alamat pemberitahuan dan wakil-wakil Para Pihak adalah sebagai berikut :");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaPT + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatPT + " " + AlamatPT1 + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + TelpPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + AtasNamaPKS + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding-top:15px'><td colspan='3'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaMitra + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatMitra + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + telpone + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + NamaMitra + "</td></tr>");
                    HTMLContent.Append("</table></li>");
                    HTMLContent.Append("<p style='margin:2px'></p>");

                    HTMLContent.Append("<li>Dalam hal salah satu pihak mengubah atau mengalami perubahan alamat, mengganti atau mengalami pergantian Penanggung Jawab, maka pihak yang mengubah atau mengalami perubahan alamat dan mengganti atau mengalami pergantian Penanggung Jawab tersebut harus segera memberitahukan alamat yang baru atau Penanggung Jawab yang baru selambat-lambatnya 7 (tujuh) hari kalender sejak terjadinya perubahan alamat tersebut.</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p><center><b>PASAL 11<br />PENYELESAIAN SENGKETA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Apabila dalam pelaksanaan Perjanjian ini terdapat sengketa atau perselisihan antara Para Pihak, maka Para Pihak sepakat dan setuju untuk menyelesaikannya dengan jalan musyawarah untuk mufakat;</li>");
                    HTMLContent.Append("<li>Apabila ketentuan yang dimaksud dalam ayat 1 Pasal ini tidak tercapai, maka Para Pihak setuju untuk menyelesaikan sengketa atau perselisihan tersebut melalui Pengadilan Negeri dimana PT SMS berdomisili, dengan tidak mengurangi hak PT SMS untuk mengajukan tuntutan atau gugatan hukum terhadap Mitra dihadapan pengadilan lain di wilayah Republik Indonesia sesuai dengan ketentuan hukum yang berlaku.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />LAIN-LAIN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Segala ketentuan dan syarat dalam Perjanjian ini berlaku serta mengikat bagi Para Pihak;</li>");
                    HTMLContent.Append("<li>Para Pihak menyatakan bahwa masing-masing Pihak secara hukum dan peraturan perundangan berhak untuk menandatangani Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Hal-hal yang tidak tercantum dan diatur dalam Perjanjian ini akan ditetapkan kemudian dalam Addendum secara tertulis berdasarkan kesepakatan Para Pihak dan merupakan bagian yang tidak terpisahkan dengan Perjanjian ini serta mempunyai kekuatan hukum yang sama dengan Perjanjian ini.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p>Demikian Perjanjian ini dibuat dalam rangkap 2 (dua),  masing-masing  bermaterai cukup dan mempunyai kekuatan hukum yang sama.</p>");

                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td>Jakarta, " + tglhariini + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px' height='230px'><td valign='top'><b>" + NamaPT + "</b></td><td valign='top' align='center'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='margin:40px'></p>");

                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td><b><u>" + AtasNamaPKS + "</u></b></td><td align='center' rowcolspan='2'><b><u>" + NamaMitra + "</u></b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td>" + JabatanAtasNamaPKS + "</td><td><b></b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:85px'>Halaman 2/2</p>");

                    HTMLContent.Append("</body></html>");


                    var OurPdfPage = PdfGenerator.GeneratePdf(HTMLContent.ToString(), configurationOptions);
                    OurPdfPage.Pages.RemoveAt(2);
                    OurPdfPage.Save(ms);
                    res = ms.ToArray();
                }

                string minut = DateTime.Now.ToString("ddMMyyyymmss");
                string filenamepar = (NIKMitra + "_" + NomorMOU.Replace("/", "") + "_" + NamaMitra);
                if (filenamepar.Length > 50)
                {
                    filenamepar = filenamepar.Substring(0, filenamepar.Length - 10);
                }
                filenamepar = filenamepar + ".pdf";

                iTextSharp.text.pdf.PdfReader finalPdf;
                MemoryStream msFinalPdfecp = new MemoryStream();
                finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                string password = NIKMitra;
                PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                res = msFinalPdfecp.ToArray();
                return File(res, "application/pdf", filenamepar);
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
        public async Task<ActionResult> kumonpropakslitervwcoll(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NamaPT = "";
                string AlamatPT = "";
                string AlamatPT1 = "";
                string TelpPT = "";
                string FaxPT = "";

                string NomorSPL = "";
                string NomorMOU = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string AlamatMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string handlejob = "";

                string hari = "";
                string tanggal = "";
                string bulan = "";
                string tahun = "";
                string tanggalangka = "";
                string periodekontrak = "";

                string handphone = "";
                string telpone = "";

                string bank = "";
                string cabangbank = "";
                string norek = "";
                string atasnamarek = "";
                string NIKMitra = "";
                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                NomorMOU = rw["ContractNo"].ToString();
                NamaPT = rw["PTNama"].ToString();
                AlamatPT = rw["PTAlamat"].ToString();
                AlamatPT1 = rw["PTAlamat1"].ToString();
                TelpPT = rw["PTTelp"].ToString();
                FaxPT = rw["PTFax"].ToString();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                AlamatMitra = rw["Alamat"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                telpone = rw["NoHPPKS"].ToString();
                handlejob = "";
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                hari = rw["Hari"].ToString().ToLower();
                tanggal = rw["Tanggal"].ToString().ToLower();
                bulan = rw["Bulan"].ToString().ToLower();
                tahun = rw["Tahun"].ToString().ToLower();
                tanggalangka = rw["tanggalangka"].ToString().ToLower();
                periodekontrak = rw["PeriodeKontrak"].ToString();

                bank = rw["NamaBankPKS"].ToString();
                cabangbank = rw["CabangBankPKS"].ToString();
                norek = rw["NorekeningPKS"].ToString();
                atasnamarek = rw["PemilikkeningPKS"].ToString();

                Byte[] res = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    var configurationOptions = new PdfGenerateConfig();

                    //Page is in Landscape mode, other option is Portrait
                    configurationOptions.PageOrientation = PdfSharp.PageOrientation.Portrait;

                    //Set page type as Letter. Other options are A4 …
                    configurationOptions.PageSize = PdfSharp.PageSize.A4;
                    //This is to fit Chrome Auto Margins when printing.Yours may be different
                    configurationOptions.MarginTop = 2;
                    configurationOptions.MarginLeft = 2;
                    configurationOptions.MarginRight = 2;
                    configurationOptions.MarginBottom = 2;

                    StringBuilder HTMLContent = new StringBuilder();
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    string locimg = Server.MapPath(Request.ApplicationPath) + "Images\\logosms1.png";

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<b><center>PERJANJIAN KERJASAMA MITRA PENAGIHAN <br /> No : " + NomorMOU + "</center></b>");
                    HTMLContent.Append("<p>Pada hari ini " + hari + ", tanggal " + tanggal + ", bulan " + bulan + " " + tahun + "(" + tanggalangka + "), telah dibuat dan ditandatangani Perjanjian Kerjasama Mitra Penagihan oleh dan antara: </p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li><b>" + NamaPT + "</b>, perseroan terbatas yang didirikan berdasarkan hukum  Negara Republik Indonesia, berkedudukan di Jakarta Pusat dan berkantor di Jalan Johar No. 22,<br/>" +
                                        "Kebon Sirih, Menteng, Jakarta Pusat 10340 dan dalam perbuatan hukum ini diwakili oleh <b>" + AtasNamaPKS + "</b>, bertindak dalam kedudukannya selaku " + JabatanAtasNamaPKS + ", dengan demikian <br/>" +
                                        "sah bertindak untuk dan atas nama " + NamaPT + ", (selanjutnya disebut sebagai <b>“PT SMS”</b>)" +
                                       "</li>");

                    HTMLContent.Append("<li><b>" + NamaMitra + "</b> pemegang Kartu Tanda Penduduk (KTP) nomor :<b>" + KtpMitra + "</b>, beralamat di " + AlamatMitra + " dalam hal " +
                                       "ini bertindak untuk dan atas nama diri sendiri (selanjutnya disebut <b>“Mitra”</b>).</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p>PT SMS dan Mitra dalam hal secara bersama-sama disebut “Para Pihak” dan masing-masing disebut “Pihak”, terlebih dahulu menerangkan hal-hal sebagai berikut :<br/>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Bahwa PT SMS adalah perseroan terbatas yang menjalankan usaha di bidang jasa konsultasi manajemen sumber daya manusia, termasuk untuk melakukan penagihan utang;<br/>");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Bahwa Mitra adalah perseorangan, yang mempunyai persyaratan dan kemampuan untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Bahwa PT SMS bermaksud membuka kesempatan menjadi mitra PT SMS (“Mitra”) bagi pihak-pihak yang bersedia untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("d.<span style='margin-left:5px'/>Bahwa Mitra dengan ini menyatakan kesediaannya untuk menjadi Mitra PT SMS dalam melakukan penagihan.</p>");

                    HTMLContent.Append("<p>Bahwa Para Pihak telah setuju dan sepakat untuk membuat, menetapkan, melaksanakan dan mematuhi Perjanjian Kerjasama Mitra Penagihan ini untuk selanjutnya disebut “Perjanjian” dengan syarat dan ketentuan sebagai berikut </p>");
                    HTMLContent.Append("<p><center><b>PASAL 1<br />DEFINISI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>”Penagihan” adalah kegiatan-kegiatan yang dilakukan oleh Mitra yang termasuk namun tidak terbatas dalam penagihan angsuran, penerimaan barang dan kegiatan lainnya yang tertera pada Surat Tugas dan tidak bertentangan dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>”Barang” adalah setiap barang-barang berwujud tahan lama seperti kendaraan bermotor mobil dan/atau motor dan/atau barang lainnya, Elektronik, Furniture, Komputer, Handphone dan barang Durable Goods yang merupakan objek pembiayaan dan/atau barang yang menjadi jaminan dalam suatu perjanjian tertulis antara Klien dengan debitur klien.</li>");
                    HTMLContent.Append("<li>”Debitur” adalah nasabah klien yang telah membuat dan menandatangani perjanjian tertulis dengan klien</li>");
                    HTMLContent.Append("<li>”Surat Tugas” adalah Surat Tugas yang diterbitkan klien kepada PT SMS yang diwakili oleh Mitra untuk melakukan penagihan kewajiban/utang dan penerimaan jaminan debitur klien.</li>");
                    HTMLContent.Append("<li>”Surat Penunjukan” adalah surat yang dikeluarkan oleh PT SMS untuk Mitra yang menunjukkan kepada klien,  bahwa pihak yang bersangkutan adalah Mitra PT SMS.</li>");
                    HTMLContent.Append("<li>”Dokumen Penagihan” adalah dokumen-dokumen yang diperlukan dalam proses Penagihan, termasuk namun tak terbatas pada Surat Penunjukan, Surat Tugas, Berita Acara Penerimaan Barang, Tanda Terima Angsuran, dan dokumen lain yang diperlukan pada saat Penagihan.</li>");
                    HTMLContent.Append("<li>”Klien” adalah pihak yang memberikan tugas kepada PT SMS untuk melakukan penagihan utang dan/atau penerimaan Barang dari Debitur.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 2<br />RUANG LINGKUP</b></center></p>");
                    HTMLContent.Append("<p>Mitra setuju untuk membantu PT SMS dalam melakukan Penagihan sesuai dengan Surat Penunjukan dan/atau Surat Tugas dari PT SMS atau Klien.<br />");
                    HTMLContent.Append("Bahwa dengan Perjanjian ini dan Surat Penunjukan dan/atau Surat Tugas dari PT SMS atau Klien kepada Mitra, tidak berarti bahwa Mitra menjadi karyawan dari PT SMS atau Klien..</p>");

                    HTMLContent.Append("<p><center><b>PASAL 3<br />HAK DAN KEWAJIBAN PT SMS</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS akan menerbitkan Surat Penunjukan kepada Mitra, khusus untuk dapat melaksanakan Penagihan.</li>");
                    HTMLContent.Append("<li>PT SMS wajib memberikan tembusan Surat Penunjukan yang diterbitkan kepada Klien paling lambat 1 (satu) hari kerja setelah Surat Penunjukan diterbitkan.</li>");
                    HTMLContent.Append("<li>PT SMS berhak membatalkan setiap saat Surat Penunjukan yang telah diterbitkan berdasarkan pertimbangan-pertimbangan dari PT SMS, hal mana tidak perlu dibuktikan pada Mitra, termasuk namun tak terbatas dalam hal tidak terpenuhinya ketentuan Pasal 4 ayat 1 Perjanjian ini.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 4<br />HAK DAN KEWAJIBAN MITRA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra wajib memiliki sertifikasi Profesi di bidang penagihan sesuai ketentuan perundang -undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban melaksanakan Penagihan berdasarkan Dokumen Penagihan serta tidak melanggar segala peraturan dan ketentuan perundang-undangan yang berlaku dan Mitra bertanggung jawab untuk segala tindakan yang dilakukan oleh Mitra.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban mengembalikan Dokumen Penagihan yang telah diterbitkan Klien kepada PT SMS, paling lambat 1 (satu) hari kerja setelah masa berlaku Dokumen Penagihan tersebut berakhir.</li>");
                    HTMLContent.Append("<li>Mitra  dilarang menyimpan Barang yang telah diterimakan dari Debitur dan/atau pihak lain dan wajib untuk segera menyerahkan Barang yang telah diterimakan dari Debitur dan/atau pihak lain kepada Klien, dalam waktu selambat-lambatnya 1 (satu) hari kerja sejak tanggal penerimaan kembali tersebut.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban membebaskan PT SMS dan/atau Klien dari segala tuntutan, gugatan atau kerugian yang timbul sebagai akibat dari tindakan Penagihan yang dilakukan Mitra.</li>");
                    HTMLContent.Append("<li>Untuk setiap penagihan yang berhasil dilakukan Mitra sesuai dengan tata cara sebagaimana tersebut dalam Perjanjian ini, maka Mitra berhak untuk mendapatkan <b><i>Success Fee</i></b> sesuai dengan ketentuan bagi hasil yang disepakati oleh Para Pihak.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 5<br />PEMBAYARAN <i>SUCCESS FEE</i></b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra akan mendapatkan <b><i>Success Fee</i></b>, setelah memenuhi ketentuan sebagai berikut :</li>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Mitra telah menerimakan titipan pembayaran angsuran dan/atau Barang titip jual, serta telah menyerahkan Barang yang dimaksud ke tempat yang telah ditentukan Klien.<br />");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Mitra telah membuat dan menandatangani dokumen tanda terima atas Barang yang telah diterimakan oleh Mitra.<br />");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Mitra menyerahkan  Dokumen Penagihan dimaksud kepada Klien.<br />");
                    HTMLContent.Append("<li>Pembayaran <b><i>Success Fee</i></b> akan dilakukan dengan cara transfer ke rekening Mitra yaitu sebagai berikut :");
                    HTMLContent.Append("<table width='50%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Nama Bank</td><td width='5px'>:</td><td align='left'>" + bank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Cabang Bank</td><td width='5px'>:</td><td align='left'>" + cabangbank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>No. Rekening</td><td width='5px'>:</td><td align='left'>" + norek + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Atas Nama</td><td width='5px'>:</td><td align='left'>" + atasnamarek + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("Nama yang tercatat pada rekening adalah nama pihak yang menandatangani Perjanjian ini. </li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 6<br />JANGKA WAKTU PERJANJIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Perjanjian ini berlaku selama " + periodekontrak + " mulai tanggal " + TglMasukMitra + " sampai dengan tanggal " + TglAkhirMitra + ";</li>");
                    HTMLContent.Append("<li>Para Pihak sepakat bahwa masing-masing pihak dapat mengakhiri Perjanjian ini secara sepihak dengan cara menyampaikan pemberitahuan kepada  pihak lainnya selambat-lambatnya 14 (empat belas) hari kalender sebelum tanggal pengakhiran Perjanjian yang dikehendaki.</li>");
                    HTMLContent.Append("<li>Dalam hal terjadi kondisi sebagaimana dimaksud pada ayat 2 Pasal ini, maka pengakhiran Perjanjian tersebut akan dicatat dalam sistem administrasi PT SMS, dan dengan demikian Surat Penunjukan yang pernah diterbitkan oleh PT SMS kepada Mitra menjadi tidak berlaku lagi.</li>");
                    HTMLContent.Append("<li>Berakhirnya Perjanjian ini tidak mengakhiri hubungan hukum dan kewajiban Para Pihak yang telah ada sebelum Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan adanya ketentuan pengakhiran dalam Perjanjian ini Para Pihak sepakat untuk melepaskan ketentuan pasal 1266 dan 1267 Kitab Undang - Undang Hukum Perdata yang berlaku di Indonesia.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:10px'>Halaman 1/2</p>");

                    HTMLContent.Append("</body></html>");

                    //page 2 //

                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<p><center><b>PASAL 7<br />SANKSI</b></center></p>");
                    HTMLContent.Append("<p>Apabila Mitra tidak melaksakan ketentuan sebagaimana dimaksud Pasal 4 Perjanjian, maka PT SMS dapat menahan pembayaran Success Fee Mitra sesuai kerugian yang diderita oleh PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 8<br />JAMINAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap dokumen jaminan yang diserahkan kepada PT SMS (jika ada) adalah dokumen milik Mitra atau diperoleh dari pihak lain sesuai hukum yang berlaku, apabila dokumen jaminan tersebut dimiliki atau diperoleh tidak sesuai hukum, maka Mitra melepaskan semua tuntutan dan/atau gugatan dari pihak lain kepada PT SMS dan/atau Klien dan mengganti seluruh biaya yang timbul akibat tuntutan dan/atau gugatan tersebut sehubungan dengan penyerahan dokumen jaminan kepada PT SMS.</li>");
                    HTMLContent.Append("<li>Mitra menjamin dalam menjalani kewajiban-kewajiban berdasarkan Perjanjian wajib mengikuti prosedur dari PT SMS, Klien dan sesuai dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa apa yang dinyatakan, baik pelaksanaan maupun dalam bentuk pernyataan dalam Perjanjian ini adalah benar dan tidak menyesatkan, apabila dikemudian hari hal-hal tersebut tidak sesuai, maka Mitra bertanggung jawab atas seluruh kerugian yang timbul kepada PT SMS dan/atau Klien akibat tidak sesuainya hal-hal yang disebutkan tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap kerugian yang timbul dalam bentuk apapun terhadap PT SMS akibat tidak dilaksanakan dan/atau tidak sesuai dengan Perjanjian ini maupun peraturan perundang-undangan yang berlaku, maka Mitra akan bertanggung jawab penuh atas kerugian tersebut.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 9<br />EVALUASI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS berhak untuk melaksanakan evaluasi berkala atas pelaksanaan Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Pelaksanaan evaluasi berkala sebagaimana dimaksud ayat 1 Pasal ini dilakukan paling sedikit sebanyak 2 (dua) kali selama masa Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 10<br />KORESPONDENSI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Untuk keperluan komunikasi dan surat menyurat serta pemberitahuan antara Para Pihak termasuk pemberitahuan tentang perubahan Penanggung Jawab sehubungan dengan pelaksanaan Perjanjian ini, maka disepakati alamat pemberitahuan dan wakil-wakil Para Pihak adalah sebagai berikut :");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaPT + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatPT + " " + AlamatPT1 + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + TelpPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + AtasNamaPKS + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding-top:15px'><td colspan='3'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaMitra + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatMitra + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + telpone + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + NamaMitra + "</td></tr>");
                    HTMLContent.Append("</table></li>");
                    HTMLContent.Append("<p style='margin:2px'></p>");

                    HTMLContent.Append("<li>Dalam hal salah satu pihak mengubah atau mengalami perubahan alamat, mengganti atau mengalami pergantian Penanggung Jawab, maka pihak yang mengubah atau mengalami perubahan alamat dan mengganti atau mengalami pergantian Penanggung Jawab tersebut harus segera memberitahukan alamat yang baru atau Penanggung Jawab yang baru selambat-lambatnya 7 (tujuh) hari kalender sejak terjadinya perubahan alamat tersebut.</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p><center><b>PASAL 11<br />PENYELESAIAN SENGKETA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Apabila dalam pelaksanaan Perjanjian ini terdapat sengketa atau perselisihan antara Para Pihak, maka Para Pihak sepakat dan setuju untuk menyelesaikannya dengan jalan musyawarah untuk mufakat;</li>");
                    HTMLContent.Append("<li>Apabila ketentuan yang dimaksud dalam ayat 1 Pasal ini tidak tercapai, maka Para Pihak setuju untuk menyelesaikan sengketa atau perselisihan tersebut melalui Pengadilan Negeri dimana PT SMS berdomisili, dengan tidak mengurangi hak PT SMS untuk mengajukan tuntutan atau gugatan hukum terhadap Mitra dihadapan pengadilan lain di wilayah Republik Indonesia sesuai dengan ketentuan hukum yang berlaku.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />LAIN-LAIN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Segala ketentuan dan syarat dalam Perjanjian ini berlaku serta mengikat bagi Para Pihak;</li>");
                    HTMLContent.Append("<li>Para Pihak menyatakan bahwa masing-masing Pihak secara hukum dan peraturan perundangan berhak untuk menandatangani Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Hal-hal yang tidak tercantum dan diatur dalam Perjanjian ini akan ditetapkan kemudian dalam Addendum secara tertulis berdasarkan kesepakatan Para Pihak dan merupakan bagian yang tidak terpisahkan dengan Perjanjian ini serta mempunyai kekuatan hukum yang sama dengan Perjanjian ini.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p>Demikian Perjanjian ini dibuat dalam rangkap 2 (dua),  masing-masing  bermaterai cukup dan mempunyai kekuatan hukum yang sama.</p>");

                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td>Jakarta, " + tglhariini + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px' height='230px'><td valign='top'><b>" + NamaPT + "</b></td><td valign='top' align='center'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='margin:40px'></p>");

                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td><b><u>" + AtasNamaPKS + "</u></b></td><td align='center' rowcolspan='2'><b><u>" + NamaMitra + "</u></b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td>" + JabatanAtasNamaPKS + "</td><td><b></b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:85px'>Halaman 2/2</p>");

                    HTMLContent.Append("</body></html>");


                    var OurPdfPage = PdfGenerator.GeneratePdf(HTMLContent.ToString(), configurationOptions);
                    OurPdfPage.Pages.RemoveAt(2);
                    OurPdfPage.Save(ms);
                    res = ms.ToArray();
                }

                var contenttypeed = "application/pdf";
                string powderdockp = "0";
                string powderdockd = "0";

                string filenamepar = (NIKMitra + "_" + NomorMOU.Replace("/", "") + "_" + NamaMitra);
                if (filenamepar.Length > 50)
                {
                    filenamepar = filenamepar.Substring(0, filenamepar.Length - 10);
                }
                filenamepar = filenamepar + ".pdf";

                //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=&file=";
                var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = res, msg = "", contenttype = contenttypeed, filename = filenamepar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
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


        //PKS MITRA wopi//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> kumonpropaksliterwopi(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NamaPT = "";
                string AlamatPT = "";
                string AlamatPT1 = "";
                string TelpPT = "";
                string FaxPT = "";

                string NomorMOU = "";
                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string AlamatMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string handlejob = "";

                string hari = "";
                string tanggal = "";
                string bulan = "";
                string tahun = "";
                string tanggalangka = "";

                string handphone = "";
                string telpone = "";
                string periodekontrak = "";
                string bank = "";
                string cabangbank = "";
                string norek = "";
                string atasnamarek = "";
                string NIKMitra = "";
                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                NomorMOU = rw["ContractNo"].ToString();
                NamaPT = rw["PTNama"].ToString();
                AlamatPT = rw["PTAlamat"].ToString();
                AlamatPT1 = rw["PTAlamat1"].ToString();
                TelpPT = rw["PTTelp"].ToString();
                FaxPT = rw["PTFax"].ToString();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                AlamatMitra = rw["Alamat"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                telpone = rw["NoHPPKS"].ToString();
                periodekontrak = rw["PeriodeKontrak"].ToString();

                handlejob = "";
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                hari = rw["Hari"].ToString().ToLower();
                tanggal = rw["Tanggal"].ToString().ToLower();
                bulan = rw["Bulan"].ToString().ToLower();
                tahun = rw["Tahun"].ToString().ToLower();
                tanggalangka = rw["tanggalangka"].ToString().ToLower();

                bank = rw["NamaBankPKS"].ToString();
                cabangbank = rw["CabangBankPKS"].ToString();
                norek = rw["NorekeningPKS"].ToString();
                atasnamarek = rw["PemilikkeningPKS"].ToString();

                Byte[] res = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    var configurationOptions = new PdfGenerateConfig();

                    //Page is in Landscape mode, other option is Portrait
                    configurationOptions.PageOrientation = PdfSharp.PageOrientation.Portrait;

                    //Set page type as Letter. Other options are A4 …
                    configurationOptions.PageSize = PdfSharp.PageSize.A4;
                    //This is to fit Chrome Auto Margins when printing.Yours may be different
                    configurationOptions.MarginTop = 2;
                    configurationOptions.MarginLeft = 2;
                    configurationOptions.MarginRight = 2;
                    configurationOptions.MarginBottom = 2;

                    StringBuilder HTMLContent = new StringBuilder();
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    string locimg = Server.MapPath(Request.ApplicationPath) + "Images\\logosms1.png";

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<b><center>PERJANJIAN KERJASAMA MITRA <br /> PERIHAL PENAWARAN KERINGANAN DENDA <br /> PENAGIHAN <br /> No : " + NomorMOU + "</center></b>");
                    HTMLContent.Append("<p>Pada hari ini " + hari + ", tanggal " + tanggal + ", bulan " + bulan + " " + tahun + "(" + tanggalangka + "), telah dibuat dan ditandatangani Perjanjian Kerjasama Mitra Perihal Penawaran Keringanan Denda, oleh dan antara :</p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li><b>" + NamaPT + "</b>, perseroan terbatas yang didirikan berdasarkan hukum  Negara Republik Indonesia, berkedudukan di Jakarta Pusat dan berkantor di Jalan Johar No. 22,<br/>" +
                                        "Kebon Sirih, Menteng, Jakarta Pusat 10340 dan dalam perbuatan hukum ini diwakili oleh <b>" + AtasNamaPKS + "</b>, bertindak dalam kedudukannya selaku " + JabatanAtasNamaPKS + ", dengan demikian <br/>" +
                                        "sah bertindak untuk dan atas nama " + NamaPT + ", (selanjutnya disebut sebagai <b>“PT SMS”</b>)" +
                                       "</li>");

                    HTMLContent.Append("<li><b>" + NamaMitra + "</b> pemegang Kartu Tanda Penduduk (KTP) nomor :<b>" + KtpMitra + "</b>, beralamat di " + AlamatMitra + " dalam hal " +
                                       "ini bertindak untuk dan atas nama diri sendiri (selanjutnya disebut <b>“Mitra”</b>).</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p>PT SMS dan Mitra dalam hal secara bersama-sama disebut “Para Pihak” dan masing-masing disebut “Pihak”, terlebih dahulu menerangkan hal-hal sebagai berikut :<br/>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Bahwa PT SMS adalah perseroan terbatas yang menjalankan usaha di bidang jasa konsultasi manajemen sumber daya manusia, termasuk untuk melakukan penawaran keringanan denda kepada debitur dari suatu perusahaan pembiayaan (kreditur);<br/>");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Bahwa Mitra adalah perseorangan, yang mempunyai persyaratan dan kemampuan untuk melakukan penawaran keringanan denda kepada debitur;<br/>");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Bahwa PT SMS bermaksud membuka kesempatan menjadi mitra PT SMS (“Mitra”) bagi pihak-pihak yang bersedia untuk melakukan penawaran keringanan denda kepada debitur;<br/>");
                    HTMLContent.Append("d.<span style='margin-left:5px'/>Bahwa Mitra dengan ini menyatakan kesediaannya untuk menjadi Mitra PT SMS dalam melakukan penawaran keringanan denda kepada debitur.</p>");

                    HTMLContent.Append("<p>Bahwa Para Pihak telah setuju dan sepakat untuk membuat, menetapkan, melaksanakan dan mematuhi Perjanjian Kerjasama Mitra Penagihan ini untuk selanjutnya disebut “Perjanjian” dengan syarat dan ketentuan sebagai berikut </p>");
                    HTMLContent.Append("<p><center><b>PASAL 1<br />DEFINISI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>”Penawaran Keringanan Denda” adalah kegiatan-kegiatan yang dilakukan oleh Mitra dalam rangka menyampaikan penawaran keringanan denda kepada Debitur yang tertera pada dokumen penawaran, dan tidak bertentangan dengan peraturan perundang-undangan yang berlaku. Mitra hanya melakukan negosiasi dengan Debitur untuk menawarkan keringanan denda Debitur sedangkan pembayaran denda dilakukan oleh Debitur di Kantor Cabang/RO/Unit Klien.</li>");
                    HTMLContent.Append("<li>”Debitur” adalah nasabah klien yang telah membuat dan menandatangani perjanjian tertulis dengan klien.</li>");
                    HTMLContent.Append("<li>“Denda” adalah kewajiban yang timbul akibat ketidaktepatan Debitur dalam pembayaran angsuran setiap bulannya sesuai dengan ketentuan Perjanjian Pembiayaan</li>");
                    HTMLContent.Append("<li>”Surat Penunjukan” adalah surat yang dikeluarkan oleh PT SMS untuk Mitra yang menunjukkan kepada klien,  bahwa pihak yang bersangkutan adalah Mitra PT SMS.</li>");
                    HTMLContent.Append("<li>”Dokumen Penawaran” adalah dokumen-dokumen yang diperlukan dalam proses Penawaran Keringanan Denda, termasuk namun tak terbatas pada Surat Penunjukan, Form Permohonan dan Persetujuan Keringanan (Diskon) denda, Daftar Kunjungan Harian (DKH) Mitra dan dokumen lain yang diperlukan pada saat Penawaran Keringanan Denda.</li>");
                    HTMLContent.Append("<li>”Klien” adalah pihak yang memberikan tugas kepada PT SMS untuk melakukan Penawaran Keringanan Denda kepada Debitur.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 2<br />RUANG LINGKUP</b></center></p>");
                    HTMLContent.Append("<p>Mitra setuju untuk membantu PT SMS dalam melakukan Penawaran Keringanan Denda sesuai dengan Dokumen Penawaran.<br />");
                    HTMLContent.Append("Bahwa dengan Perjanjian ini dan Surat Penunjukan dari PT SMS kepada Mitra, tidak berarti bahwa Mitra menjadi karyawan dari PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 3<br />HAK DAN KEWAJIBAN PT SMS</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS akan menerbitkan Surat Penunjukan kepada Mitra, khusus untuk dapat melaksanakan Penagihan.</li>");
                    HTMLContent.Append("<li>PT SMS wajib memberikan tembusan Surat Penunjukan yang diterbitkan kepada Klien paling lambat 1 (satu) hari kerja setelah Surat Penunjukan diterbitkan.</li>");
                    HTMLContent.Append("<li>PT SMS berhak membatalkan setiap saat Surat Penunjukan yang telah diterbitkan berdasarkan pertimbangan-pertimbangan dari PT SMS, hal mana tidak perlu dibuktikan pada Mitra, termasuk namun tak terbatas dalam hal tidak terpenuhinya ketentuan Pasal 4 ayat 1 Perjanjian ini.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 4<br />HAK DAN KEWAJIBAN MITRA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra berkewajiban melaksanakan Penawaran Keringanan Denda kepada Debitur berdasarkan Form Permohonan dan Persetujuan Keringanan (Diskon) Denda serta tidak melanggar segala peraturan dan ketentuan perundang-undangan yang berlaku dan Mitra bertanggung jawab untuk segala tindakan yang dilakukan oleh Mitra.</li>");
                    HTMLContent.Append("<li>Mitra  dilarang menerima uang denda dari Debitur dan/atau pihak lain dan wajib untuk segera menyerahkan Form Permohonan dan Persetujuan Keringanan (Diskon) denda serta Daftar Kunjungan Harian (DKH) Mitra kepada Klien, dalam waktu selambat-lambatnya 1 (satu) hari kerja sejak tanggal kunjungan ke Debitur.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban membebaskan PT SMS dan/atau Klien dari segala tuntutan, gugatan atau kerugian yang timbul sebagai akibat dari tindakan Penawaran Keringanan Denda kepada Debitur yang dilakukan Mitra.</li>");
                    HTMLContent.Append("<li>Untuk setiap Penawaran Keringanan Denda kepada Debitur yang berhasil dilakukan Mitra berdasarkan denda yang dibayarkan oleh debitur di Kantor Cabang/RO/Unit klien sesuai dengan tata cara sebagaimana tersebut dalam Perjanjian ini, maka Mitra berhak untuk mendapatkan Success Fee sesuai dengan ketentuan bagi hasil diantara Para Pihak.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 5<br />PEMBAYARAN <i>SUCCESS FEE</i></b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra akan mendapatkan <b><i>Success Fee</i></b>, setelah memenuhi ketentuan sebagai berikut :</li>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Mitra menyerahkan Form Permohonan dan Persetujuan Keringanan (Diskon) denda kepada Klien.<br />");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Debitur melakukan pembayaran denda sesuai kesepakatan yang tertera dalam Form Permohonan dan Persetujuan Keringanan (Diskon) denda di Kantor Cabang/RO/Unit Klien<br />");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Mitra menyerahkan  Daftar Kunjungan Harian (DKH) Mitra kepada Klien.<br />");
                    HTMLContent.Append("<li>Pembayaran <b><i>Success Fee</i></b> akan dilakukan dengan cara transfer ke rekening Mitra yaitu sebagai berikut :");
                    HTMLContent.Append("<table width='50%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Nama Bank</td><td width='5px'>:</td><td align='left'>" + bank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Cabang Bank</td><td width='5px'>:</td><td align='left'>" + cabangbank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>No. Rekening</td><td width='5px'>:</td><td align='left'>" + norek + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Atas Nama</td><td width='5px'>:</td><td align='left'>" + atasnamarek + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("Nama yang tercatat pada rekening adalah nama pihak yang menandatangani Perjanjian ini. </li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 6<br />JANGKA WAKTU PERJANJIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Perjanjian ini berlaku selama " + periodekontrak + " mulai tanggal " + TglMasukMitra + " sampai dengan tanggal " + TglAkhirMitra + ";</li>");
                    HTMLContent.Append("<li>Para Pihak sepakat bahwa masing-masing pihak dapat mengakhiri Perjanjian ini secara sepihak dengan cara menyampaikan pemberitahuan kepada  pihak lainnya selambat-lambatnya 14 (empat belas) hari kalender sebelum tanggal pengakhiran Perjanjian yang dikehendaki.</li>");
                    HTMLContent.Append("<li>Dalam hal terjadi kondisi sebagaimana dimaksud pada ayat 2 Pasal ini, maka pengakhiran Perjanjian tersebut akan dicatat dalam sistem administrasi PT SMS, dan dengan demikian Surat Penunjukan yang pernah diterbitkan oleh PT SMS kepada Mitra menjadi tidak berlaku lagi.</li>");
                    HTMLContent.Append("<li>Berakhirnya Perjanjian ini tidak mengakhiri hubungan hukum dan kewajiban Para Pihak yang telah ada sebelum Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan adanya ketentuan pengakhiran dalam Perjanjian ini Para Pihak sepakat untuk melepaskan ketentuan pasal 1266 dan 1267 Kitab Undang-Undang Hukum Perdata yang berlaku di Indonesia.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:10px'>Halaman 1/2</p>");

                    HTMLContent.Append("</body></html>");

                    //page 2 //

                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<p><center><b>PASAL 7<br />SANKSI</b></center></p>");
                    HTMLContent.Append("<p>Apabila Mitra tidak melaksakan ketentuan sebagaimana dimaksud Pasal 4 Perjanjian, maka PT SMS dapat menahan pembayaran Success Fee Mitra sesuai kerugian yang diderita oleh PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 8<br />JAMINAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap dokumen jaminan yang diserahkan kepada PT SMS (jika ada) adalah dokumen milik Mitra atau diperoleh dari pihak lain sesuai hukum yang berlaku, apabila dokumen jaminan tersebut dimiliki atau diperoleh tidak sesuai hukum, maka Mitra melepaskan semua tuntutan dan/atau gugatan dari pihak lain kepada PT SMS dan/atau Klien dan mengganti seluruh biaya yang timbul akibat tuntutan dan/atau gugatan tersebut sehubungan dengan penyerahan dokumen jaminan kepada PT SMS.</li>");
                    HTMLContent.Append("<li>Mitra menjamin dalam menjalani kewajiban-kewajiban berdasarkan Perjanjian wajib mengikuti prosedur dari PT SMS, Klien dan sesuai dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa apa yang dinyatakan, baik pelaksanaan maupun dalam bentuk pernyataan dalam Perjanjian ini adalah benar dan tidak menyesatkan, apabila dikemudian hari hal-hal tersebut tidak sesuai, maka Mitra bertanggung jawab atas seluruh kerugian yang timbul kepada PT SMS dan/atau Klien akibat tidak sesuainya hal-hal yang disebutkan tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap kerugian yang timbul dalam bentuk apapun terhadap PT SMS akibat tidak dilaksanakan dan/atau tidak sesuai dengan Perjanjian ini maupun peraturan perundang-undangan yang berlaku, maka Mitra akan bertanggung jawab penuh atas kerugian tersebut.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 9<br />EVALUASI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS berhak untuk melaksanakan evaluasi berkala atas pelaksanaan Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Pelaksanaan evaluasi berkala sebagaimana dimaksud ayat 1 Pasal ini dilakukan paling sedikit sebanyak 2 (dua) kali selama masa Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 10<br />KORESPONDENSI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Untuk keperluan komunikasi dan surat menyurat serta pemberitahuan antara Para Pihak termasuk pemberitahuan tentang perubahan Penanggung Jawab sehubungan dengan pelaksanaan Perjanjian ini, maka disepakati alamat pemberitahuan dan wakil-wakil Para Pihak adalah sebagai berikut :");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaPT + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatPT + " " + AlamatPT1 + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + TelpPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + AtasNamaPKS + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding-top:15px'><td colspan='3'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaMitra + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatMitra + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + telpone + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + NamaMitra + "</td></tr>");
                    HTMLContent.Append("</table></li>");
                    HTMLContent.Append("<p style='margin:2px'></p>");

                    HTMLContent.Append("<li>Dalam hal salah satu pihak mengubah atau mengalami perubahan alamat, mengganti atau mengalami pergantian Penanggung Jawab, maka pihak yang mengubah atau mengalami perubahan alamat dan mengganti atau mengalami pergantian Penanggung Jawab tersebut harus segera memberitahukan alamat yang baru atau Penanggung Jawab yang baru selambat-lambatnya 7 (tujuh) hari kalender sejak terjadinya perubahan alamat tersebut.</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p><center><b>PASAL 11<br />PENYELESAIAN SENGKETA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>1.	Perjanjian ini tunduk dan ditafsirkan menurut hukum  negara Republik Indonesia.</li>");
                    HTMLContent.Append("<li>Apabila dalam pelaksanaan Perjanjian ini terdapat sengketa atau perselisihan antara Para Pihak, maka Para Pihak sepakat dan setuju untuk menyelesaikannya dengan jalan musyawarah untuk mufakat;</li>");
                    HTMLContent.Append("<li>Apabila ketentuan yang dimaksud dalam ayat 1 Pasal ini tidak tercapai, maka Para Pihak setuju untuk menyelesaikan sengketa atau perselisihan tersebut melalui Pengadilan Negeri dimana PT SMS berdomisili, dengan tidak mengurangi hak PT SMS untuk mengajukan tuntutan atau gugatan hukum terhadap Mitra dihadapan pengadilan lain di wilayah Republik Indonesia sesuai dengan ketentuan hukum yang berlaku.</li>");

                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />KETENTUAN-KETENTUAN LAIN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Segala ketentuan dan syarat dalam Perjanjian ini berlaku serta mengikat bagi Para Pihak;</li>");
                    HTMLContent.Append("<li>Para Pihak menyatakan bahwa masing-masing Pihak secara hukum dan peraturan perundangan berhak untuk menandatangani Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Hal-hal yang tidak tercantum dan diatur dalam Perjanjian ini akan ditetapkan kemudian dalam Addendum secara tertulis berdasarkan kesepakatan Para Pihak dan merupakan bagian yang tidak terpisahkan dengan Perjanjian ini serta mempunyai kekuatan hukum yang sama dengan Perjanjian ini.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p>Demikian Perjanjian ini dibuat dalam rangkap 2 (dua),  masing-masing  bermaterai cukup dan mempunyai kekuatan hukum yang sama.</p>");

                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td>Jakarta, " + tglhariini + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px' height='230px'><td valign='top'><b>" + NamaPT + "</b></td><td valign='top' align='center'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='margin:40px'></p>");

                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td><b><u>" + AtasNamaPKS + "</u></b></td><td align='center' rowcolspan='2'><b><u>" + NamaMitra + "</u></b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td>" + JabatanAtasNamaPKS + "</td><td><b></b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:75px'>Halaman 2/2</p>");

                    HTMLContent.Append("</body></html>");


                    var OurPdfPage = PdfGenerator.GeneratePdf(HTMLContent.ToString(), configurationOptions);
                    OurPdfPage.Pages.RemoveAt(2);
                    OurPdfPage.Save(ms);
                    res = ms.ToArray();
                }

                string minut = DateTime.Now.ToString("ddMMyyyymmss");

                string filenamepar = (NIKMitra + "_" + NomorMOU.Replace("/", "") + "_" + NamaMitra);
                if (filenamepar.Length > 50)
                {
                    filenamepar = filenamepar.Substring(0, filenamepar.Length - 10);
                }
                filenamepar = filenamepar + ".pdf";


                iTextSharp.text.pdf.PdfReader finalPdf;
                MemoryStream msFinalPdfecp = new MemoryStream();
                finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                string password = NIKMitra;
                PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                res = msFinalPdfecp.ToArray();
                return File(res, "application/pdf", filenamepar);
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
        public async Task<ActionResult> kumonpropakslitervwwopi(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NamaPT = "";
                string AlamatPT = "";
                string AlamatPT1 = "";
                string TelpPT = "";
                string FaxPT = "";

                string NomorMOU = "";
                string NomorSPL = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string AlamatMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string handlejob = "";

                string hari = "";
                string tanggal = "";
                string bulan = "";
                string tahun = "";
                string tanggalangka = "";

                string handphone = "";
                string telpone = "";
                string periodekontrak = "";
                string bank = "";
                string cabangbank = "";
                string norek = "";
                string atasnamarek = "";
                string NIKMitra = "";
                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                NomorMOU = rw["ContractNo"].ToString();
                NamaPT = rw["PTNama"].ToString();
                AlamatPT = rw["PTAlamat"].ToString();
                AlamatPT1 = rw["PTAlamat1"].ToString();
                TelpPT = rw["PTTelp"].ToString();
                FaxPT = rw["PTFax"].ToString();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                AlamatMitra = rw["Alamat"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                telpone = rw["NoHPPKS"].ToString();
                periodekontrak = rw["PeriodeKontrak"].ToString();

                handlejob = "";
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                hari = rw["Hari"].ToString().ToLower();
                tanggal = rw["Tanggal"].ToString().ToLower();
                bulan = rw["Bulan"].ToString().ToLower();
                tahun = rw["Tahun"].ToString().ToLower();
                tanggalangka = rw["tanggalangka"].ToString().ToLower();

                bank = rw["NamaBankPKS"].ToString();
                cabangbank = rw["CabangBankPKS"].ToString();
                norek = rw["NorekeningPKS"].ToString();
                atasnamarek = rw["PemilikkeningPKS"].ToString();

                Byte[] res = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    var configurationOptions = new PdfGenerateConfig();

                    //Page is in Landscape mode, other option is Portrait
                    configurationOptions.PageOrientation = PdfSharp.PageOrientation.Portrait;

                    //Set page type as Letter. Other options are A4 …
                    configurationOptions.PageSize = PdfSharp.PageSize.A4;
                    //This is to fit Chrome Auto Margins when printing.Yours may be different
                    configurationOptions.MarginTop = 2;
                    configurationOptions.MarginLeft = 2;
                    configurationOptions.MarginRight = 2;
                    configurationOptions.MarginBottom = 2;

                    StringBuilder HTMLContent = new StringBuilder();
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    string locimg = Server.MapPath(Request.ApplicationPath) + "Images\\logosms1.png";

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<b><center>PERJANJIAN KERJASAMA MITRA <br /> PERIHAL PENAWARAN KERINGANAN DENDA <br /> PENAGIHAN <br /> No : " + NomorMOU + "</center></b>");
                    HTMLContent.Append("<p>Pada hari ini " + hari + ", tanggal " + tanggal + ", bulan " + bulan + " " + tahun + "(" + tanggalangka + "), telah dibuat dan ditandatangani Perjanjian Kerjasama Mitra Perihal Penawaran Keringanan Denda, oleh dan antara :</p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li><b>" + NamaPT + "</b>, perseroan terbatas yang didirikan berdasarkan hukum  Negara Republik Indonesia, berkedudukan di Jakarta Pusat dan berkantor di Jalan Johar No. 22,<br/>" +
                                        "Kebon Sirih, Menteng, Jakarta Pusat 10340 dan dalam perbuatan hukum ini diwakili oleh <b>" + AtasNamaPKS + "</b>, bertindak dalam kedudukannya selaku " + JabatanAtasNamaPKS + ", dengan demikian <br/>" +
                                        "sah bertindak untuk dan atas nama " + NamaPT + ", (selanjutnya disebut sebagai <b>“PT SMS”</b>)" +
                                       "</li>");

                    HTMLContent.Append("<li><b>" + NamaMitra + "</b> pemegang Kartu Tanda Penduduk (KTP) nomor :<b>" + KtpMitra + "</b>, beralamat di " + AlamatMitra + " dalam hal " +
                                       "ini bertindak untuk dan atas nama diri sendiri (selanjutnya disebut <b>“Mitra”</b>).</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p>PT SMS dan Mitra dalam hal secara bersama-sama disebut “Para Pihak” dan masing-masing disebut “Pihak”, terlebih dahulu menerangkan hal-hal sebagai berikut :<br/>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Bahwa PT SMS adalah perseroan terbatas yang menjalankan usaha di bidang jasa konsultasi manajemen sumber daya manusia, termasuk untuk melakukan penawaran keringanan denda kepada debitur dari suatu perusahaan pembiayaan (kreditur);<br/>");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Bahwa Mitra adalah perseorangan, yang mempunyai persyaratan dan kemampuan untuk melakukan penawaran keringanan denda kepada debitur;<br/>");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Bahwa PT SMS bermaksud membuka kesempatan menjadi mitra PT SMS (“Mitra”) bagi pihak-pihak yang bersedia untuk melakukan penawaran keringanan denda kepada debitur;<br/>");
                    HTMLContent.Append("d.<span style='margin-left:5px'/>Bahwa Mitra dengan ini menyatakan kesediaannya untuk menjadi Mitra PT SMS dalam melakukan penawaran keringanan denda kepada debitur.</p>");

                    HTMLContent.Append("<p>Bahwa Para Pihak telah setuju dan sepakat untuk membuat, menetapkan, melaksanakan dan mematuhi Perjanjian Kerjasama Mitra Penagihan ini untuk selanjutnya disebut “Perjanjian” dengan syarat dan ketentuan sebagai berikut </p>");
                    HTMLContent.Append("<p><center><b>PASAL 1<br />DEFINISI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>”Penawaran Keringanan Denda” adalah kegiatan-kegiatan yang dilakukan oleh Mitra dalam rangka menyampaikan penawaran keringanan denda kepada Debitur yang tertera pada dokumen penawaran, dan tidak bertentangan dengan peraturan perundang-undangan yang berlaku. Mitra hanya melakukan negosiasi dengan Debitur untuk menawarkan keringanan denda Debitur sedangkan pembayaran denda dilakukan oleh Debitur di Kantor Cabang/RO/Unit Klien.</li>");
                    HTMLContent.Append("<li>”Debitur” adalah nasabah klien yang telah membuat dan menandatangani perjanjian tertulis dengan klien.</li>");
                    HTMLContent.Append("<li>“Denda” adalah kewajiban yang timbul akibat ketidaktepatan Debitur dalam pembayaran angsuran setiap bulannya sesuai dengan ketentuan Perjanjian Pembiayaan</li>");
                    HTMLContent.Append("<li>”Surat Penunjukan” adalah surat yang dikeluarkan oleh PT SMS untuk Mitra yang menunjukkan kepada klien,  bahwa pihak yang bersangkutan adalah Mitra PT SMS.</li>");
                    HTMLContent.Append("<li>”Dokumen Penawaran” adalah dokumen-dokumen yang diperlukan dalam proses Penawaran Keringanan Denda, termasuk namun tak terbatas pada Surat Penunjukan, Form Permohonan dan Persetujuan Keringanan (Diskon) denda, Daftar Kunjungan Harian (DKH) Mitra dan dokumen lain yang diperlukan pada saat Penawaran Keringanan Denda.</li>");
                    HTMLContent.Append("<li>”Klien” adalah pihak yang memberikan tugas kepada PT SMS untuk melakukan Penawaran Keringanan Denda kepada Debitur.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 2<br />RUANG LINGKUP</b></center></p>");
                    HTMLContent.Append("<p>Mitra setuju untuk membantu PT SMS dalam melakukan Penawaran Keringanan Denda sesuai dengan Dokumen Penawaran.<br />");
                    HTMLContent.Append("Bahwa dengan Perjanjian ini dan Surat Penunjukan dari PT SMS kepada Mitra, tidak berarti bahwa Mitra menjadi karyawan dari PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 3<br />HAK DAN KEWAJIBAN PT SMS</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS akan menerbitkan Surat Penunjukan kepada Mitra, khusus untuk dapat melaksanakan Penagihan.</li>");
                    HTMLContent.Append("<li>PT SMS wajib memberikan tembusan Surat Penunjukan yang diterbitkan kepada Klien paling lambat 1 (satu) hari kerja setelah Surat Penunjukan diterbitkan.</li>");
                    HTMLContent.Append("<li>PT SMS berhak membatalkan setiap saat Surat Penunjukan yang telah diterbitkan berdasarkan pertimbangan-pertimbangan dari PT SMS, hal mana tidak perlu dibuktikan pada Mitra, termasuk namun tak terbatas dalam hal tidak terpenuhinya ketentuan Pasal 4 ayat 1 Perjanjian ini.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 4<br />HAK DAN KEWAJIBAN MITRA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra berkewajiban melaksanakan Penawaran Keringanan Denda kepada Debitur berdasarkan Form Permohonan dan Persetujuan Keringanan (Diskon) Denda serta tidak melanggar segala peraturan dan ketentuan perundang-undangan yang berlaku dan Mitra bertanggung jawab untuk segala tindakan yang dilakukan oleh Mitra.</li>");
                    HTMLContent.Append("<li>Mitra  dilarang menerima uang denda dari Debitur dan/atau pihak lain dan wajib untuk segera menyerahkan Form Permohonan dan Persetujuan Keringanan (Diskon) denda serta Daftar Kunjungan Harian (DKH) Mitra kepada Klien, dalam waktu selambat-lambatnya 1 (satu) hari kerja sejak tanggal kunjungan ke Debitur.</li>");
                    HTMLContent.Append("<li>Mitra berkewajiban membebaskan PT SMS dan/atau Klien dari segala tuntutan, gugatan atau kerugian yang timbul sebagai akibat dari tindakan Penawaran Keringanan Denda kepada Debitur yang dilakukan Mitra.</li>");
                    HTMLContent.Append("<li>Untuk setiap Penawaran Keringanan Denda kepada Debitur yang berhasil dilakukan Mitra berdasarkan denda yang dibayarkan oleh debitur di Kantor Cabang/RO/Unit klien sesuai dengan tata cara sebagaimana tersebut dalam Perjanjian ini, maka Mitra berhak untuk mendapatkan Success Fee sesuai dengan ketentuan bagi hasil diantara Para Pihak.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 5<br />PEMBAYARAN <i>SUCCESS FEE</i></b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra akan mendapatkan <b><i>Success Fee</i></b>, setelah memenuhi ketentuan sebagai berikut :</li>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Mitra menyerahkan Form Permohonan dan Persetujuan Keringanan (Diskon) denda kepada Klien.<br />");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Debitur melakukan pembayaran denda sesuai kesepakatan yang tertera dalam Form Permohonan dan Persetujuan Keringanan (Diskon) denda di Kantor Cabang/RO/Unit Klien<br />");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Mitra menyerahkan  Daftar Kunjungan Harian (DKH) Mitra kepada Klien.<br />");
                    HTMLContent.Append("<li>Pembayaran <b><i>Success Fee</i></b> akan dilakukan dengan cara transfer ke rekening Mitra yaitu sebagai berikut :");
                    HTMLContent.Append("<table width='50%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Nama Bank</td><td width='5px'>:</td><td align='left'>" + bank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Cabang Bank</td><td width='5px'>:</td><td align='left'>" + cabangbank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>No. Rekening</td><td width='5px'>:</td><td align='left'>" + norek + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Atas Nama</td><td width='5px'>:</td><td align='left'>" + atasnamarek + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("Nama yang tercatat pada rekening adalah nama pihak yang menandatangani Perjanjian ini. </li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 6<br />JANGKA WAKTU PERJANJIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Perjanjian ini berlaku selama " + periodekontrak + " mulai tanggal " + TglMasukMitra + " sampai dengan tanggal " + TglAkhirMitra + ";</li>");
                    HTMLContent.Append("<li>Para Pihak sepakat bahwa masing-masing pihak dapat mengakhiri Perjanjian ini secara sepihak dengan cara menyampaikan pemberitahuan kepada  pihak lainnya selambat-lambatnya 14 (empat belas) hari kalender sebelum tanggal pengakhiran Perjanjian yang dikehendaki.</li>");
                    HTMLContent.Append("<li>Dalam hal terjadi kondisi sebagaimana dimaksud pada ayat 2 Pasal ini, maka pengakhiran Perjanjian tersebut akan dicatat dalam sistem administrasi PT SMS, dan dengan demikian Surat Penunjukan yang pernah diterbitkan oleh PT SMS kepada Mitra menjadi tidak berlaku lagi.</li>");
                    HTMLContent.Append("<li>Berakhirnya Perjanjian ini tidak mengakhiri hubungan hukum dan kewajiban Para Pihak yang telah ada sebelum Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan adanya ketentuan pengakhiran dalam Perjanjian ini Para Pihak sepakat untuk melepaskan ketentuan pasal 1266 dan 1267 Kitab Undang-Undang Hukum Perdata yang berlaku di Indonesia.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:10px'>Halaman 1/2</p>");

                    HTMLContent.Append("</body></html>");

                    //page 2 //

                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<p><center><b>PASAL 7<br />SANKSI</b></center></p>");
                    HTMLContent.Append("<p>Apabila Mitra tidak melaksakan ketentuan sebagaimana dimaksud Pasal 4 Perjanjian, maka PT SMS dapat menahan pembayaran Success Fee Mitra sesuai kerugian yang diderita oleh PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 8<br />JAMINAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap dokumen jaminan yang diserahkan kepada PT SMS (jika ada) adalah dokumen milik Mitra atau diperoleh dari pihak lain sesuai hukum yang berlaku, apabila dokumen jaminan tersebut dimiliki atau diperoleh tidak sesuai hukum, maka Mitra melepaskan semua tuntutan dan/atau gugatan dari pihak lain kepada PT SMS dan/atau Klien dan mengganti seluruh biaya yang timbul akibat tuntutan dan/atau gugatan tersebut sehubungan dengan penyerahan dokumen jaminan kepada PT SMS.</li>");
                    HTMLContent.Append("<li>Mitra menjamin dalam menjalani kewajiban-kewajiban berdasarkan Perjanjian wajib mengikuti prosedur dari PT SMS, Klien dan sesuai dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa apa yang dinyatakan, baik pelaksanaan maupun dalam bentuk pernyataan dalam Perjanjian ini adalah benar dan tidak menyesatkan, apabila dikemudian hari hal-hal tersebut tidak sesuai, maka Mitra bertanggung jawab atas seluruh kerugian yang timbul kepada PT SMS dan/atau Klien akibat tidak sesuainya hal-hal yang disebutkan tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap kerugian yang timbul dalam bentuk apapun terhadap PT SMS akibat tidak dilaksanakan dan/atau tidak sesuai dengan Perjanjian ini maupun peraturan perundang-undangan yang berlaku, maka Mitra akan bertanggung jawab penuh atas kerugian tersebut.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 9<br />EVALUASI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS berhak untuk melaksanakan evaluasi berkala atas pelaksanaan Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Pelaksanaan evaluasi berkala sebagaimana dimaksud ayat 1 Pasal ini dilakukan paling sedikit sebanyak 2 (dua) kali selama masa Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 10<br />KORESPONDENSI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Untuk keperluan komunikasi dan surat menyurat serta pemberitahuan antara Para Pihak termasuk pemberitahuan tentang perubahan Penanggung Jawab sehubungan dengan pelaksanaan Perjanjian ini, maka disepakati alamat pemberitahuan dan wakil-wakil Para Pihak adalah sebagai berikut :");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaPT + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatPT + " " + AlamatPT1 + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + TelpPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + AtasNamaPKS + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding-top:15px'><td colspan='3'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaMitra + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatMitra + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + telpone + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Fax</td><td width='5px'>:</td><td align='left'>" + FaxPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + NamaMitra + "</td></tr>");
                    HTMLContent.Append("</table></li>");
                    HTMLContent.Append("<p style='margin:2px'></p>");

                    HTMLContent.Append("<li>Dalam hal salah satu pihak mengubah atau mengalami perubahan alamat, mengganti atau mengalami pergantian Penanggung Jawab, maka pihak yang mengubah atau mengalami perubahan alamat dan mengganti atau mengalami pergantian Penanggung Jawab tersebut harus segera memberitahukan alamat yang baru atau Penanggung Jawab yang baru selambat-lambatnya 7 (tujuh) hari kalender sejak terjadinya perubahan alamat tersebut.</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p><center><b>PASAL 11<br />PENYELESAIAN SENGKETA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>1.	Perjanjian ini tunduk dan ditafsirkan menurut hukum  negara Republik Indonesia.</li>");
                    HTMLContent.Append("<li>Apabila dalam pelaksanaan Perjanjian ini terdapat sengketa atau perselisihan antara Para Pihak, maka Para Pihak sepakat dan setuju untuk menyelesaikannya dengan jalan musyawarah untuk mufakat;</li>");
                    HTMLContent.Append("<li>Apabila ketentuan yang dimaksud dalam ayat 1 Pasal ini tidak tercapai, maka Para Pihak setuju untuk menyelesaikan sengketa atau perselisihan tersebut melalui Pengadilan Negeri dimana PT SMS berdomisili, dengan tidak mengurangi hak PT SMS untuk mengajukan tuntutan atau gugatan hukum terhadap Mitra dihadapan pengadilan lain di wilayah Republik Indonesia sesuai dengan ketentuan hukum yang berlaku.</li>");

                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />KETENTUAN-KETENTUAN LAIN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Segala ketentuan dan syarat dalam Perjanjian ini berlaku serta mengikat bagi Para Pihak;</li>");
                    HTMLContent.Append("<li>Para Pihak menyatakan bahwa masing-masing Pihak secara hukum dan peraturan perundangan berhak untuk menandatangani Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Hal-hal yang tidak tercantum dan diatur dalam Perjanjian ini akan ditetapkan kemudian dalam Addendum secara tertulis berdasarkan kesepakatan Para Pihak dan merupakan bagian yang tidak terpisahkan dengan Perjanjian ini serta mempunyai kekuatan hukum yang sama dengan Perjanjian ini.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p>Demikian Perjanjian ini dibuat dalam rangkap 2 (dua),  masing-masing  bermaterai cukup dan mempunyai kekuatan hukum yang sama.</p>");

                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td>Jakarta, " + tglhariini + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px' height='230px'><td valign='top'><b>" + NamaPT + "</b></td><td valign='top' align='center'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='margin:40px'></p>");

                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td><b><u>" + AtasNamaPKS + "</u></b></td><td align='center' rowcolspan='2'><b><u>" + NamaMitra + "</u></b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td>" + JabatanAtasNamaPKS + "</td><td><b></b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:75px'>Halaman 2/2</p>");

                    HTMLContent.Append("</body></html>");


                    var OurPdfPage = PdfGenerator.GeneratePdf(HTMLContent.ToString(), configurationOptions);
                    OurPdfPage.Pages.RemoveAt(2);
                    OurPdfPage.Save(ms);
                    res = ms.ToArray();
                }

                var contenttypeed = "application/pdf";
                string powderdockp = "0";
                string powderdockd = "0";


                string filenamepar = (NIKMitra + "_" + NomorMOU.Replace("/", "") + "_" + NamaMitra);
                if (filenamepar.Length > 50)
                {
                    filenamepar = filenamepar.Substring(0, filenamepar.Length - 10);
                }
                filenamepar = filenamepar + ".pdf";


                //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=&file=";
                var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = res, msg = "", contenttype = contenttypeed, filename = filenamepar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> kumonpropakslitersale(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NamaPT = "";
                string AlamatPT = "";
                string AlamatPT1 = "";
                string TelpPT = "";
                string FaxPT = "";

                string NomorSPL = "";
                string NomorMOU = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string AlamatMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string handlejob = "";

                string hari = "";
                string tanggal = "";
                string bulan = "";
                string tahun = "";
                string EmailMitra = "";
                string tanggalangka = "";
                string periodekontrak = "";

                string handphone = "";
                string telpone = "";

                string bank = "";
                string cabangbank = "";
                string norek = "";
                string atasnamarek = "";
                string WA = "";
                string NIKMitra = "";

                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                NomorMOU = rw["ContractNo"].ToString();
                NamaPT = rw["PTNama"].ToString();
                AlamatPT = rw["PTAlamat"].ToString();
                AlamatPT1 = rw["PTAlamat1"].ToString();
                TelpPT = rw["PTTelp"].ToString();
                FaxPT = rw["PTFax"].ToString();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                AlamatMitra = rw["Alamat"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                handlejob = "";
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                hari = rw["Hari"].ToString().ToLower();
                tanggal = rw["Tanggal"].ToString().ToLower();
                bulan = rw["Bulan"].ToString().ToLower();
                tahun = rw["Tahun"].ToString().ToLower();
                tanggalangka = rw["tanggalangka"].ToString().ToLower();
                periodekontrak = rw["PeriodeKontrak"].ToString();

                WA = rw["NoHPPKS"].ToString();
                EmailMitra = rw["EmailPKS"].ToString();

                bank = rw["NamaBankPKS"].ToString();
                cabangbank = rw["CabangBankPKS"].ToString();
                norek = rw["NorekeningPKS"].ToString();
                atasnamarek = rw["PemilikkeningPKS"].ToString();

                Byte[] res = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    var configurationOptions = new PdfGenerateConfig();

                    //Page is in Landscape mode, other option is Portrait
                    configurationOptions.PageOrientation = PdfSharp.PageOrientation.Portrait;

                    //Set page type as Letter. Other options are A4 …
                    configurationOptions.PageSize = PdfSharp.PageSize.A4;
                    //This is to fit Chrome Auto Margins when printing.Yours may be different
                    configurationOptions.MarginTop = 2;
                    configurationOptions.MarginLeft = 2;
                    configurationOptions.MarginRight = 2;
                    configurationOptions.MarginBottom = 2;

                    StringBuilder HTMLContent = new StringBuilder();
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    string locimg = Server.MapPath(Request.ApplicationPath) + "Images\\logosms1.png";

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<b><center>PERJANJIAN KERJASAMA MITRA PENJUALAN <br /> No : " + NomorMOU + "</center></b>");
                    HTMLContent.Append("<p>Pada hari ini " + hari + ", tanggal " + tanggal + ", bulan " + bulan + " " + tahun + "(" + tanggalangka + "), telah dibuat dan ditandatangani Perjanjian Kerjasama Mitra Penagihan oleh dan antara: </p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li><b>" + NamaPT + "</b>, perseroan terbatas yang didirikan berdasarkan hukum  Negara Republik Indonesia, berkedudukan di Jakarta Pusat dan berkantor di Jalan Johar No. 22,<br/>" +
                                        "Kebon Sirih, Menteng, Jakarta Pusat 10340 dan dalam perbuatan hukum ini diwakili oleh <b>" + AtasNamaPKS + "</b>, bertindak dalam kedudukannya selaku " + JabatanAtasNamaPKS + ", dengan demikian <br/>" +
                                        "sah bertindak untuk dan atas nama " + NamaPT + ", (selanjutnya disebut sebagai <b>“PT SMS”</b>)" +
                                       "</li>");

                    HTMLContent.Append("<li><b>" + NamaMitra + "</b> pemegang Kartu Tanda Penduduk (KTP) nomor :<b>" + KtpMitra + "</b>, beralamat di " + AlamatMitra + " dalam hal " +
                                       "ini bertindak untuk dan atas nama diri sendiri (selanjutnya disebut <b>“Mitra”</b>).</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p>PT SMS dan Mitra dalam hal secara bersama-sama disebut “Para Pihak” dan masing-masing disebut “Pihak”, terlebih dahulu menerangkan hal-hal sebagai berikut :<br/>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Bahwa PT SMS adalah perseroan terbatas yang menjalankan usaha di bidang jasa konsultasi manajemen sumber daya manusia, termasuk untuk melakukan penagihan utang;<br/>");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Bahwa Mitra adalah perseorangan, yang mempunyai persyaratan dan kemampuan untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Bahwa PT SMS bermaksud membuka kesempatan menjadi mitra PT SMS (“Mitra”) bagi pihak-pihak yang bersedia untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("d.<span style='margin-left:5px'/>Bahwa Mitra dengan ini menyatakan kesediaannya untuk menjadi Mitra PT SMS dalam melakukan pemasaran produk pembiayaan.</p>");

                    HTMLContent.Append("<p>Bahwa Para Pihak telah setuju dan sepakat untuk membuat, menetapkan, melaksanakan dan mematuhi Perjanjian Kerjasama Mitra Penagihan ini untuk selanjutnya disebut “Perjanjian” dengan syarat dan ketentuan sebagai berikut </p>");
                    HTMLContent.Append("<p><center><b>PASAL 1<br />DEFINISI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>”Pemasaran” adalah kegiatan-kegiatan yang dilakukan oleh Mitra termasuk namun tidak terbatas pada promosi, menjual, memberitahukan, menyebarluaskan maupun kegiatan lainnya dalam rangka untuk memasarkan Produk Pembiayaan sesuai dengan kebutuhan dan tidak bertentangan dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>”Produk Pembiayaan” adalah setiap barang/jasa yang dibiayai oleh suatu perusahaan pembiayaan yang memiliki hubungan hukum dengan PT SMS.</li>");
                    HTMLContent.Append("<li>”Calon Debitur/Konsumen” adalah adalah orang perorangan dan/atau badan usaha yang berminat dan setuju untuk menggunakan Produk Pembiayaan ditawarkan oleh Mitra.</li>");
                    HTMLContent.Append("<li>”Klien” adalah pihak yang memiliki Produk Pembiayaan dan telah bekerjasama dengan PT SMS.</li>");
                    HTMLContent.Append("<li>”Success Fee” adalah insentif untuk setiap keberhasilan Mitra dalam melakukan Pemasaran Produk Pembiayaan.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 2<br />RUANG LINGKUP</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra setuju untuk melakukan Penjualan Produk Pembiayaan sesuai dengan penunjukan PT SMS.</li>");
                    HTMLContent.Append("<li>Bahwa berdasarkan Perjanjian ini dan penunjukan dari PT SMS atau Klien, status Mitra bukan merupakan karyawan dari PT SMS atau Klien.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan ketentuan dalam ayat 2 Pasal ini, hubungan hukum yang timbul hanya sebatas dalam Perjanjian ini sehingga segala ketentuan dalam peraturan perundang-undangan tentang ketenagakerjaan maupun turunannya tidak berlaku bagi Mitra.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 3<br />HAK DAN KEWAJIBAN PT SMS</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS akan menerbitkan dokumen penunjukan kepada Mitra, khusus untuk dapat melaksanakan Penjualan Produk Pembiayaan.</li>");
                    HTMLContent.Append("<li>PT SMS wajib memberikan tembusan dokumen penunjukan yang diterbitkan kepada Klien paling lambat 1 (satu) hari kerja setelah dokumen tersebut diterbitkan.</li>");
                    HTMLContent.Append("<li>PT SMS berhak membatalkan setiap saat dokumen penunjukan yang telah diterbitkan berdasarkan pertimbangan-pertimbangan dari PT SMS, hal mana tidak perlu dibuktikan pada Mitra, termasuk namun tak terbatas dalam hal tidak terpenuhinya ketentuan Pasal 4 ayat 1 Perjanjian ini.</li>");
                    HTMLContent.Append("<li>PT SMS wajib untuk membayar Success Fee sesuai dengan sebagaimana yang dimaksud dalam Pasal 5 Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 4<br />HAK DAN KEWAJIBAN MITRA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra berkewajiban melaksanakan Penjualan serta tidak melanggar segala peraturan dan ketentuan perundang-undangan yang berlaku dan Mitra bertanggung jawab untuk segala tindakan yang dilakukan oleh Mitra.</li>");
                    HTMLContent.Append("<li>Mitra wajib melakukan tugas dan tanggung jawab sesuai yang sudah ditentukan oleh PT SMS baik yang sudah ditentukan dalam Perjanjian maupun terpisah dari Perjanjian namun tetap menjadi bagian yang tidak terpisah dari Perjanjian ini.</li>");
                    HTMLContent.Append("<li>Untuk setiap Penjualan Produk Pembiayaan yang berhasil dilakukan Mitra sesuai dengan tata cara sebagaimana tersebut dalam Perjanjian ini, maka Mitra berhak untuk mendapatkan Success Fee sesuai dengan ketentuan bagi hasil yang disepakati oleh Para Pihak sebagaimana dimaksud pada ketentuan yang berlaku.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 5<br />PEMBAYARAN <i>SUCCESS FEE</i></b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra akan mendapatkan Success Fee, apabila Mitra telah berhasil menjual Produk Pembiayaan, yaitu dengan telah disetujui dan dibiayainya Calon Debitur/Konsumen atas oleh Klien.</li>");
                    HTMLContent.Append("<li>Pembayaran <b><i>Success Fee</i></b> akan dilakukan dengan cara transfer ke rekening Mitra yaitu sebagai berikut :");
                    HTMLContent.Append("<table width='50%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Nama Bank</td><td width='5px'>:</td><td align='left'>" + bank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Cabang Bank</td><td width='5px'>:</td><td align='left'>" + cabangbank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>No. Rekening</td><td width='5px'>:</td><td align='left'>" + norek + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Atas Nama</td><td width='5px'>:</td><td align='left'>" + atasnamarek + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("Nama yang tercatat pada rekening adalah nama pihak yang menandatangani Perjanjian ini. </li>");
                    HTMLContent.Append("<li>Apabila rekening bank milik mitra sebagaimana yang tercantum dalam ayat 5 Pasal ini selain dari Bank Danamon, maka Mitra wajib menanggung biaya yang timbul akibat transfer antar bank yang ditentukan oleh bank.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 6<br />JANGKA WAKTU PERJANJIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Perjanjian ini berlaku mulai tanggal " + TglMasukMitra + " sampai dengan tanggal " + TglAkhirMitra + " dan dapat diperpanjang sesuai dengan kesepakatan secara tertulis oleh PT SMS dan Mitra sesuai dengan rekomendasi dari klien.;</li>");
                    HTMLContent.Append("<li>Para Pihak sepakat bahwa masing-masing pihak dapat mengakhiri Perjanjian ini secara sepihak dengan cara menyampaikan pemberitahuan kepada  pihak lainnya selambat-lambatnya 14 (empat belas) hari kalender sebelum tanggal pengakhiran Perjanjian yang dikehendaki.</li>");
                    HTMLContent.Append("<li>Dalam hal terjadi kondisi sebagaimana dimaksud pada ayat 2 Pasal ini, maka pengakhiran Perjanjian tersebut akan dicatat dalam sistem administrasi PT SMS, dan dengan demikian dokumen penunjukan yang pernah diterbitkan oleh PT SMS kepada Mitra menjadi tidak berlaku lagi.</li>");
                    HTMLContent.Append("<li>Berakhirnya Perjanjian ini tidak mengakhiri hubungan hukum dan kewajiban Para Pihak yang telah ada sebelum Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan adanya ketentuan pengakhiran dalam Perjanjian ini Para Pihak sepakat untuk melepaskan ketentuan pasal 1266 dan 1267 Kitab Undang - Undang Hukum Perdata yang berlaku di Indonesia.</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:50px'>Halaman 1/3</p>");

                    HTMLContent.Append("</body></html>");

                    //page 2 //

                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;");
                    HTMLContent.Append("</head>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<p><center><b>PASAL 7<br />SANKSI</b></center></p>");
                    HTMLContent.Append("<p>Apabila Mitra tidak melaksakan ketentuan sebagaimana dimaksud Pasal 4 Perjanjian, maka PT SMS dapat menahan pembayaran Success Fee Mitra sesuai kerugian yang diderita oleh PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 8<br />KERAHASIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Selama berlangsungnya, atau setelah berakhirnya jangka waktu Perjanjian ini atau diakhirinya Perjanjian, tanpa persetujuan tertulis terlebih dahulu dari pihak lainnya, tidak boleh mengungkapkan atau menyampaikan kepada setiap orang atau orang-orang atau kepada pihak manapun, informasi rahasia termasuk namun tidak terbatas pada dokumen-dokumen, informasi-informasi, surat-surat, maupun hal lainnya yang mungkin diterima atau diperoleh oleh Pihak Kedua atau yang mungkin diketemukan oleh klien selama berlangsungnya Perjanjian ini (selanjutnya disebut “Informasi Rahasia”).</li>");
                    HTMLContent.Append("<li>Mitra setuju untuk menyimpan Informasi Rahasia Pihak Pertama dan/atau klien, dan Mitra tidak akan menggunakan Informasi Rahasia untuk tujuan apapun selain untuk melaksanakan berdasarkan Perjanjian ini, dan setuju untuk mengembalikan kepada Pihak Pertama dan/atau klien segera atau menghancurkan, tanpa penundaan yang tidak semestinya, seluruh Informasi Rahasia dan lain-lain, yang berada dalam penguasaan Mitra berdasarkan permintaan tertulis dari Pihak Pertama dan/atau klien.</li>");
                    HTMLContent.Append("<li>Ketentuan dalam Pasal ini akan tetap berlaku dan mengikat bahkan setelah Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 9<br />PERNYATAAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS merupakan Pihak yang telah mendapatkan perizinan dan/atau izin dari instansi berwenang untuk menjalankan setiap usahanya sebagaimana yang dimaksud dalam Perjanjian.</li>");
                    HTMLContent.Append("<li>PT SMS merupakan Pihak yang telah mendapatkan perizinan dan/atau izin dari instansi berwenang untuk menjalankan setiap usahanya sebagaimana yang dimaksud dalam Perjanjian.</li>");
                    HTMLContent.Append("<li>Mitra menjamin dalam menjalani kewajiban-kewajiban berdasarkan Perjanjian wajib mengikuti prosedur dari Pihak Pertama dan/atau klien, dan sesuai dengan peraturan perundang-undangan yang berlaku dan apabila terdapat gugatan dan/atau tuntutan atas perbuatan dan/atau tindakan yang dilakukan Mitra yang merugikan Calon Konsumen/Konsumen dan/atau pihak lainnya termasuk Pihak Pertama dan/atau klien maka Mitra akan bertanggung jawab sepenuhnya serta membebaskan Pihak Pertama dan/atau klien dari segala gugatan dan/atau tuntutan yang timbul.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa Mitra tidak akan pernah menyalahgunakan setiap data, dokumen, informasi maupun hal lainnya yang diserahkan oleh Pihak Pertama dan/atau klien dan/atau Calon Konsumen dan/atau Konsumen selain untuk tujuan pelaksanaan Perjanjian ini dan akan menjaga kerahasiaan seluruh dokumen, surat, perjanjian, maupun hal lainnya yang menurut Pihak Pertama dan/atau klien dan/atau Calon Konsumen dan/atau Konsumen merupakan bagian yang tidak diperbolehkan untuk diketahui oleh umum dan tetap menjaga kerahasiannya walaupun Perjanjian ini telah berakhir.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa apa yang dinyatakan, baik pelaksanaan maupun dalam bentuk pernyataan dalam Perjanjian ini adalah benar dan tidak menyesatkan, apabila dikemudian hari hal-hal tersebut tidak sesuai, maka Mitra bertanggung jawab atas seluruh kerugian yang timbul kepada Pihak Pertama dan/atau klien akibat tidak sesuainya hal-hal yang disebutkan tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap kerugian yang timbul dalam bentuk apapun terhadap Pihak Pertama dan/atau klien akibat tidak dilaksanakan dan/atau tidak sesuai dengan Perjanjian ini maupun peraturan perundang-undangan yang berlaku, maka Mitra akan bertanggung jawab penuh atas kerugian tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin akan menjaga nama baik dari Pihak Pertama dan/atau klien dan tidak akan melakukan perbuatan dan/atau tindakan yang dapat merusak reputasi dan/atau nama baik dari Pihak Pertama dan/atau klien </li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 10<br />PENGALIHAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS dapat mengalihkan seluruh hak dan kewajiban dalam Perjanjian kepada pihak lainnya dengan pemberitahuan terlebih dahulu kepada Mitra.</li>");
                    HTMLContent.Append("<li>Mitra dilarang untuk mengalihkan baik sebagian maupun keseluruhan hak dan kewajiban berdasarkan Perjanjian ini kepada pihak lainnya tanpa ada persetujuan tertulis dari PT SMS</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 11<br />EVALUASI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS berhak untuk melaksanakan evaluasi berkala atas pelaksanaan Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Pelaksanaan evaluasi berkala sebagaimana dimaksud ayat 1 Pasal ini dilakukan paling sedikit sebanyak 2 (dua) kali selama masa Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />KORESPONDENSI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Untuk keperluan komunikasi dan surat menyurat serta pemberitahuan antara Para Pihak termasuk pemberitahuan tentang perubahan Penanggung Jawab sehubungan dengan pelaksanaan Perjanjian ini, maka disepakati alamat pemberitahuan dan wakil-wakil Para Pihak adalah sebagai berikut :");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaPT + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatPT + " " + AlamatPT1 + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + TelpPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + AtasNamaPKS + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding-top:15px'><td colspan='3'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaMitra + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatMitra + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon/WA</td><td width='5px'>:</td><td align='left'>" + WA + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Email</td><td width='5px'>:</td><td align='left'>" + EmailMitra + "</td></tr>");
                    HTMLContent.Append("</table></li>");
                    HTMLContent.Append("<p style='margin:2px'></p>");

                    HTMLContent.Append("<li>Dalam hal salah satu pihak mengubah atau mengalami perubahan alamat, mengganti atau mengalami pergantian Penanggung Jawab, maka pihak yang mengubah atau mengalami perubahan alamat dan mengganti atau mengalami pergantian Penanggung Jawab tersebut harus segera memberitahukan alamat yang baru atau Penanggung Jawab yang baru selambat-lambatnya 7 (tujuh) hari kalender sejak terjadinya perubahan alamat tersebut.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 13<br />KEADAAN MEMAKSA (FORCE MAJEURE)</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Keadaan Memaksa (force majeure) yaitu suatu peristiwa atau keadaan atau suatu kejadian diluar kekuasaan dan kemampuan salah satu Pihak yang mempengaruhi secara langsung sehingga kewajiban yang ditentukan dalam Perjanjian menjadi tidak dapat dipenuhi.</li>");
                    HTMLContent.Append("<li>Yang dapat digolongkan Keadaan Memaksa adalah perang, kerusuhan, revolusi, bencana alam (banjir, gempa bumi, badai, gunung meletus, tanah longsor, wabah penyakit, dan angin topan), kebakaran, perubahan peraturan perundang-undangan, dan keadaan Memaksa ini tidak termasuk hal-hal merugikan yang disebabkan oleh perbuatan atau kelalaian salah satu Pihak. </li>");
                    HTMLContent.Append("<li>Apabila terjadi  Keadaan Memaksa, dalam jangka waktu 7 (tujuh) hari kalender Pihak yang terkena Keadaan Memaksa wajib memberitahukan hal tersebut kepada pihak yang tidak terkena Keadaan Memaksa yang dibuktikan dengan keterangan dari instansi yang berwenang  dan atas kejadian Keadaan Memaksa tersebut akan dipertimbangkan lebih lanjut terhadap pelaksanaan Perjanjian selanjutnya berdasarkan kesepakatan tertulis Para Pihak. </li>");
                    HTMLContent.Append("<li>Tindakan yang diambil untuk mengatasi terjadinya Keadaan Memaksa, termasuk penambahan jangka waktu Perjanjian (jika ada), akan disepakati secara tertulis oleh Para Pihak.</li>");

                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:80px'>Halaman 2/3</p>");



                    HTMLContent.Append("</body></html>");


                    //page 3
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'background: white;width: 21cm;height: 29.7cm;display: block;margin: 0 auto;margin-bottom: 0.5cm;box-shadow: 0 0 0.5cm rgba(0,0,0,0.5)'><head>");
                    HTMLContent.Append("</head>");
                    HTMLContent.Append("<style>");
                    HTMLContent.Append("page[size = 'A4'] {'background: white;width: 21cm;height: 29.7cm;display: block;margin: 0 auto;margin-bottom: 0.5cm;box-shadow: 0 0 0.5cm rgba(0,0,0,0.5)}");
                    HTMLContent.Append("@media print {body, page[size = 'A4'] {margin: 0;box-shadow: 0'}}");
                    HTMLContent.Append("</style>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<img src='" + locimg + "' width='100px' height='50px'>");

                    HTMLContent.Append("<p><center><b>PASAL 14<br />NON GRATIFIKASI (FORCE MAJEURE)</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Para Pihak sepakat untuk tidak memberikan, membujuk untuk memberikan dan/atau mencoba untuk memberikan komisi, hadiah, jasa, pembayaran dan/atau pemberian apapun dalam bentuk apapun lainnya kepada staff, pegawai, pejabat, direktur dan/atau pihak terkait dari masing-masing Pihak;</li>");
                    HTMLContent.Append("<li>Ketentuan sebagaimana yang dimaksud dalam ayat 1 Pasal ini berlaku juga terhadap larangan untuk memberikan dan/atau mencoba memberikan komisi, hadiah, jasa, pembayaran, dan/atau pemberian dalam bentuk apapun sehingga dapat mempengaruhi pejabat publik (yang mencakup setiap pelaksanaan atau kelalaian untuk melaksanakan fungsi-fungsi bahkan jika hal tersebut berada di luar kewenangan resminya) maupun untuk mendapatkan atau mempertahankan keuntungan finansial atau kemudahan/keuntungan dalam bentuk apapun.</li>");
                    HTMLContent.Append("<li>Apabila salah satu Pihak melanggar ketentuan sebagaimana yang dimaksud dalam ayat 1 dan 2 Pasal ini maka Para Pihak sepakat bahwa Perjanjian dan/atau perjanjian apapun lainnya yang berhubungan dengan Para Pihak, dapat diakhiri oleh Pihak yang dirugikan tanpa diperlukannya pemberitahuan tertulis. Dalam hal dilakukannya pengakhiran Perjanjian dengan alasan tersebut diatas, maka Pihak yang melanggar  harus membayar denda kepada Pihak yang dirugikan sebesar nilai dari Perjanjian dan menggantikan segala kerugian yang dialami oleh Pihak yang dirugikan sehubungan dengan terjadinya pengakhiran Perjanjian.</li>");
                    HTMLContent.Append("<li>Dengan ini masing-masing Pihak menjamin, menyatakan, dan berjanji untuk membebaskan Pihak yang dirugikan apabila terbukti melanggar ketentuan sebagaimana yang dimaksud dalam ayat 1 dan 2 Perjanjian ini termasuk namun tidak terbatas pada tuntutan dan/atau gugatan dan menjadi tanggung jawab sepenuhnya dari Pihak yang melanggar.</li>");

                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 15<br />PENYELESAIAN SENGKETA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Apabila dalam pelaksanaan Perjanjian ini terdapat sengketa atau perselisihan antara Para Pihak, maka Para Pihak sepakat dan setuju untuk menyelesaikannya dengan jalan musyawarah untuk mufakat;</li>");
                    HTMLContent.Append("<li>Apabila ketentuan yang dimaksud dalam ayat 1 Pasal ini tidak tercapai, maka Para Pihak setuju untuk menyelesaikan sengketa atau perselisihan tersebut melalui Pengadilan Negeri dimana PT SMS berdomisili, dengan tidak mengurangi hak PT SMS untuk mengajukan tuntutan atau gugatan hukum terhadap Mitra dihadapan pengadilan lain di wilayah Republik Indonesia sesuai dengan ketentuan hukum yang berlaku.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />LAIN-LAIN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Segala ketentuan dan syarat dalam Perjanjian ini berlaku serta mengikat bagi Para Pihak;</li>");
                    HTMLContent.Append("<li>Para Pihak menyatakan bahwa masing-masing Pihak secara hukum dan peraturan perundangan berhak untuk menandatangani Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Hal-hal yang tidak tercantum dan diatur dalam Perjanjian ini akan ditetapkan kemudian dalam Addendum secara tertulis berdasarkan kesepakatan Para Pihak dan merupakan bagian yang tidak terpisahkan dengan Perjanjian ini serta mempunyai kekuatan hukum yang sama dengan Perjanjian ini.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p>Demikian Perjanjian ini dibuat dalam rangkap 2 (dua),  masing-masing  bermaterai cukup dan mempunyai kekuatan hukum yang sama.</p>");

                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td>Jakarta, " + tglhariini + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px' height='230px'><td valign='top'><b>" + NamaPT + "</b></td><td valign='top' align='center'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='margin:40px'></p>");

                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td><b><u>" + AtasNamaPKS + "</u></b></td><td align='center' rowcolspan='2'><b><u>" + NamaMitra + "</u></b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td>" + JabatanAtasNamaPKS + "</td><td><b></b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:325px'>Halaman 3/3</p>");


                    HTMLContent.Append("</body></html>");


                    var OurPdfPage = PdfGenerator.GeneratePdf(HTMLContent.ToString(), configurationOptions);
                    OurPdfPage.Pages.RemoveAt(3);
                    OurPdfPage.Save(ms);
                    res = ms.ToArray();
                }

                string minut = DateTime.Now.ToString("ddMMyyyymmss");

                string filenamepar = (NIKMitra + "_" + NomorMOU.Replace("/", "") + "_" + NamaMitra);
                if (filenamepar.Length > 50)
                {
                    filenamepar = filenamepar.Substring(0, filenamepar.Length - 10);
                }
                filenamepar = filenamepar + ".pdf";


                iTextSharp.text.pdf.PdfReader finalPdf;
                MemoryStream msFinalPdfecp = new MemoryStream();
                finalPdf = new iTextSharp.text.pdf.PdfReader(res);
                string password = NIKMitra;
                PdfEncryptor.Encrypt(finalPdf, msFinalPdfecp, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                res = msFinalPdfecp.ToArray();
                return File(res, "application/pdf", filenamepar);
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
        public async Task<ActionResult> kumonpropakslitervwsale(string eux, string aux)
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

                // get from session //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Regmitra = TempData[tempTransksi] as vmRegmitra;
                Common = (TempData["common"] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Regmitra;
                TempData["common"] = Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                string NamaPT = "";
                string AlamatPT = "";
                string AlamatPT1 = "";
                string TelpPT = "";
                string FaxPT = "";

                string NomorSPL = "";
                string NomorMOU = "";
                string AtasNamaPKS = "";
                string JabatanAtasNamaPKS = "";
                string NamaMitra = "";
                string AlamatMitra = "";
                string KtpMitra = "";
                string TglMasukMitra = "";
                string TglAkhirMitra = "";
                string tglhariini = "";
                string handlejob = "";

                string hari = "";
                string tanggal = "";
                string bulan = "";
                string tahun = "";
                string tanggalangka = "";
                string periodekontrak = "";

                string handphone = "";
                string telpone = "";
                string EmailMitra = "";
                string WA = "";

                string bank = "";
                string cabangbank = "";
                string norek = "";
                string atasnamarek = "";
                string NIKMitra = "";

                DataRow rw = Regmitra.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == eux && x.Field<int>("Id") == int.Parse(aux)).SingleOrDefault();

                NomorMOU = rw["ContractNo"].ToString();
                NamaPT = rw["PTNama"].ToString();
                AlamatPT = rw["PTAlamat"].ToString();
                AlamatPT1 = rw["PTAlamat1"].ToString();
                TelpPT = rw["PTTelp"].ToString();
                FaxPT = rw["PTFax"].ToString();

                tglhariini = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                NomorSPL = rw["ContractNo"].ToString();
                AtasNamaPKS = rw["CONT_ATASNAMA"].ToString();
                JabatanAtasNamaPKS = rw["CONT_JABATANATASNAMA"].ToString();
                NamaMitra = rw["NamaMitra"].ToString();
                AlamatMitra = rw["Alamat"].ToString();
                KtpMitra = rw["NoKTP"].ToString();
                NIKMitra = rw["NIKBaru"].ToString();
                handlejob = "";
                WA = rw["NoHPPKS"].ToString();
                TglMasukMitra = DateTime.Parse(rw["tglmasuk"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");
                TglAkhirMitra = DateTime.Parse(rw["tglakhir"].ToString(), new System.Globalization.CultureInfo("id-ID")).ToString("dd MMMM yyyy");

                hari = rw["Hari"].ToString().ToLower();
                tanggal = rw["Tanggal"].ToString().ToLower();
                bulan = rw["Bulan"].ToString().ToLower();
                tahun = rw["Tahun"].ToString().ToLower();
                tanggalangka = rw["tanggalangka"].ToString().ToLower();
                periodekontrak = rw["PeriodeKontrak"].ToString();

                bank = rw["NamaBankPKS"].ToString();
                cabangbank = rw["CabangBankPKS"].ToString();
                norek = rw["NorekeningPKS"].ToString();
                atasnamarek = rw["PemilikkeningPKS"].ToString();

                Byte[] res = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    var configurationOptions = new PdfGenerateConfig();

                    //Page is in Landscape mode, other option is Portrait
                    configurationOptions.PageOrientation = PdfSharp.PageOrientation.Portrait;

                    //Set page type as Letter. Other options are A4 …
                    configurationOptions.PageSize = PdfSharp.PageSize.A4;
                    //This is to fit Chrome Auto Margins when printing.Yours may be different
                    configurationOptions.MarginTop = 2;
                    configurationOptions.MarginLeft = 2;
                    configurationOptions.MarginRight = 2;
                    configurationOptions.MarginBottom = 2;

                    StringBuilder HTMLContent = new StringBuilder();
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'><head>");
                    HTMLContent.Append("</head>");

                    string locimg = Server.MapPath(Request.ApplicationPath) + "Images\\logosms1.png";

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<b><center>PERJANJIAN KERJASAMA MITRA PENJUALAN <br /> No : " + NomorMOU + "</center></b>");
                    HTMLContent.Append("<p>Pada hari ini " + hari + ", tanggal " + tanggal + ", bulan " + bulan + " " + tahun + "(" + tanggalangka + "), telah dibuat dan ditandatangani Perjanjian Kerjasama Mitra Penagihan oleh dan antara: </p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li><b>" + NamaPT + "</b>, perseroan terbatas yang didirikan berdasarkan hukum  Negara Republik Indonesia, berkedudukan di Jakarta Pusat dan berkantor di Jalan Johar No. 22,<br/>" +
                                        "Kebon Sirih, Menteng, Jakarta Pusat 10340 dan dalam perbuatan hukum ini diwakili oleh <b>" + AtasNamaPKS + "</b>, bertindak dalam kedudukannya selaku " + JabatanAtasNamaPKS + ", dengan demikian <br/>" +
                                        "sah bertindak untuk dan atas nama " + NamaPT + ", (selanjutnya disebut sebagai <b>“PT SMS”</b>)" +
                                       "</li>");

                    HTMLContent.Append("<li><b>" + NamaMitra + "</b> pemegang Kartu Tanda Penduduk (KTP) nomor :<b>" + KtpMitra + "</b>, beralamat di " + AlamatMitra + " dalam hal " +
                                       "ini bertindak untuk dan atas nama diri sendiri (selanjutnya disebut <b>“Mitra”</b>).</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p>PT SMS dan Mitra dalam hal secara bersama-sama disebut “Para Pihak” dan masing-masing disebut “Pihak”, terlebih dahulu menerangkan hal-hal sebagai berikut :<br/>");
                    HTMLContent.Append("a.<span style='margin-left:5px'/>Bahwa PT SMS adalah perseroan terbatas yang menjalankan usaha di bidang jasa konsultasi manajemen sumber daya manusia, termasuk untuk melakukan penagihan utang;<br/>");
                    HTMLContent.Append("b.<span style='margin-left:5px'/>Bahwa Mitra adalah perseorangan, yang mempunyai persyaratan dan kemampuan untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("c.<span style='margin-left:5px'/>Bahwa PT SMS bermaksud membuka kesempatan menjadi mitra PT SMS (“Mitra”) bagi pihak-pihak yang bersedia untuk melakukan penagihan;<br/>");
                    HTMLContent.Append("d.<span style='margin-left:5px'/>Bahwa Mitra dengan ini menyatakan kesediaannya untuk menjadi Mitra PT SMS dalam melakukan pemasaran produk pembiayaan.</p>");

                    HTMLContent.Append("<p>Bahwa Para Pihak telah setuju dan sepakat untuk membuat, menetapkan, melaksanakan dan mematuhi Perjanjian Kerjasama Mitra Penagihan ini untuk selanjutnya disebut “Perjanjian” dengan syarat dan ketentuan sebagai berikut </p>");
                    HTMLContent.Append("<p><center><b>PASAL 1<br />DEFINISI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>”Pemasaran” adalah kegiatan-kegiatan yang dilakukan oleh Mitra termasuk namun tidak terbatas pada promosi, menjual, memberitahukan, menyebarluaskan maupun kegiatan lainnya dalam rangka untuk memasarkan Produk Pembiayaan sesuai dengan kebutuhan dan tidak bertentangan dengan peraturan perundang-undangan yang berlaku.</li>");
                    HTMLContent.Append("<li>”Produk Pembiayaan” adalah setiap barang/jasa yang dibiayai oleh suatu perusahaan pembiayaan yang memiliki hubungan hukum dengan PT SMS.</li>");
                    HTMLContent.Append("<li>”Calon Debitur/Konsumen” adalah adalah orang perorangan dan/atau badan usaha yang berminat dan setuju untuk menggunakan Produk Pembiayaan ditawarkan oleh Mitra.</li>");
                    HTMLContent.Append("<li>”Klien” adalah pihak yang memiliki Produk Pembiayaan dan telah bekerjasama dengan PT SMS.</li>");
                    HTMLContent.Append("<li>”Success Fee” adalah insentif untuk setiap keberhasilan Mitra dalam melakukan Pemasaran Produk Pembiayaan.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 2<br />RUANG LINGKUP</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra setuju untuk melakukan Penjualan Produk Pembiayaan sesuai dengan penunjukan PT SMS.</li>");
                    HTMLContent.Append("<li>Bahwa berdasarkan Perjanjian ini dan penunjukan dari PT SMS atau Klien, status Mitra bukan merupakan karyawan dari PT SMS atau Klien.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan ketentuan dalam ayat 2 Pasal ini, hubungan hukum yang timbul hanya sebatas dalam Perjanjian ini sehingga segala ketentuan dalam peraturan perundang-undangan tentang ketenagakerjaan maupun turunannya tidak berlaku bagi Mitra.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 3<br />HAK DAN KEWAJIBAN PT SMS</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS akan menerbitkan dokumen penunjukan kepada Mitra, khusus untuk dapat melaksanakan Penjualan Produk Pembiayaan.</li>");
                    HTMLContent.Append("<li>PT SMS wajib memberikan tembusan dokumen penunjukan yang diterbitkan kepada Klien paling lambat 1 (satu) hari kerja setelah dokumen tersebut diterbitkan.</li>");
                    HTMLContent.Append("<li>PT SMS berhak membatalkan setiap saat dokumen penunjukan yang telah diterbitkan berdasarkan pertimbangan-pertimbangan dari PT SMS, hal mana tidak perlu dibuktikan pada Mitra, termasuk namun tak terbatas dalam hal tidak terpenuhinya ketentuan Pasal 4 ayat 1 Perjanjian ini.</li>");
                    HTMLContent.Append("<li>PT SMS wajib untuk membayar Success Fee sesuai dengan sebagaimana yang dimaksud dalam Pasal 5 Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 4<br />HAK DAN KEWAJIBAN MITRA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra berkewajiban melaksanakan Penjualan serta tidak melanggar segala peraturan dan ketentuan perundang-undangan yang berlaku dan Mitra bertanggung jawab untuk segala tindakan yang dilakukan oleh Mitra.</li>");
                    HTMLContent.Append("<li>Mitra wajib melakukan tugas dan tanggung jawab sesuai yang sudah ditentukan oleh PT SMS baik yang sudah ditentukan dalam Perjanjian maupun terpisah dari Perjanjian namun tetap menjadi bagian yang tidak terpisah dari Perjanjian ini.</li>");
                    HTMLContent.Append("<li>Untuk setiap Penjualan Produk Pembiayaan yang berhasil dilakukan Mitra sesuai dengan tata cara sebagaimana tersebut dalam Perjanjian ini, maka Mitra berhak untuk mendapatkan Success Fee sesuai dengan ketentuan bagi hasil yang disepakati oleh Para Pihak sebagaimana dimaksud pada ketentuan yang berlaku.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 5<br />PEMBAYARAN <i>SUCCESS FEE</i></b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Mitra akan mendapatkan Success Fee, apabila Mitra telah berhasil menjual Produk Pembiayaan, yaitu dengan telah disetujui dan dibiayainya Calon Debitur/Konsumen atas oleh Klien.</li>");
                    HTMLContent.Append("<li>Pembayaran <b><i>Success Fee</i></b> akan dilakukan dengan cara transfer ke rekening Mitra yaitu sebagai berikut :");
                    HTMLContent.Append("<table width='50%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Nama Bank</td><td width='5px'>:</td><td align='left'>" + bank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Cabang Bank</td><td width='5px'>:</td><td align='left'>" + cabangbank + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>No. Rekening</td><td width='5px'>:</td><td align='left'>" + norek + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Atas Nama</td><td width='5px'>:</td><td align='left'>" + atasnamarek + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("Nama yang tercatat pada rekening adalah nama pihak yang menandatangani Perjanjian ini. </li>");
                    HTMLContent.Append("<li>Apabila rekening bank milik mitra sebagaimana yang tercantum dalam ayat 5 Pasal ini selain dari Bank Danamon, maka Mitra wajib menanggung biaya yang timbul akibat transfer antar bank yang ditentukan oleh bank.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 6<br />JANGKA WAKTU PERJANJIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Perjanjian ini berlaku mulai tanggal " + TglMasukMitra + " sampai dengan tanggal " + TglAkhirMitra + " dan dapat diperpanjang sesuai dengan kesepakatan secara tertulis oleh PT SMS dan Mitra sesuai dengan rekomendasi dari klien.;</li>");
                    HTMLContent.Append("<li>Para Pihak sepakat bahwa masing-masing pihak dapat mengakhiri Perjanjian ini secara sepihak dengan cara menyampaikan pemberitahuan kepada  pihak lainnya selambat-lambatnya 14 (empat belas) hari kalender sebelum tanggal pengakhiran Perjanjian yang dikehendaki.</li>");
                    HTMLContent.Append("<li>Dalam hal terjadi kondisi sebagaimana dimaksud pada ayat 2 Pasal ini, maka pengakhiran Perjanjian tersebut akan dicatat dalam sistem administrasi PT SMS, dan dengan demikian dokumen penunjukan yang pernah diterbitkan oleh PT SMS kepada Mitra menjadi tidak berlaku lagi.</li>");
                    HTMLContent.Append("<li>Berakhirnya Perjanjian ini tidak mengakhiri hubungan hukum dan kewajiban Para Pihak yang telah ada sebelum Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("<li>Sehubungan dengan adanya ketentuan pengakhiran dalam Perjanjian ini Para Pihak sepakat untuk melepaskan ketentuan pasal 1266 dan 1267 Kitab Undang - Undang Hukum Perdata yang berlaku di Indonesia.</li>");
                    HTMLContent.Append("</ol>");


                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:50px'>Halaman 1/3</p>");

                    HTMLContent.Append("</body></html>");

                    //page 2 //

                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;");
                    HTMLContent.Append("</head>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<p style='position: fixed; top: 0; width: 100 %;'><img src='" + locimg + "' width='100px' height='50px'></p>");
                    HTMLContent.Append("<p><center><b>PASAL 7<br />SANKSI</b></center></p>");
                    HTMLContent.Append("<p>Apabila Mitra tidak melaksakan ketentuan sebagaimana dimaksud Pasal 4 Perjanjian, maka PT SMS dapat menahan pembayaran Success Fee Mitra sesuai kerugian yang diderita oleh PT SMS.</p>");

                    HTMLContent.Append("<p><center><b>PASAL 8<br />KERAHASIAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Selama berlangsungnya, atau setelah berakhirnya jangka waktu Perjanjian ini atau diakhirinya Perjanjian, tanpa persetujuan tertulis terlebih dahulu dari pihak lainnya, tidak boleh mengungkapkan atau menyampaikan kepada setiap orang atau orang-orang atau kepada pihak manapun, informasi rahasia termasuk namun tidak terbatas pada dokumen-dokumen, informasi-informasi, surat-surat, maupun hal lainnya yang mungkin diterima atau diperoleh oleh Pihak Kedua atau yang mungkin diketemukan oleh klien selama berlangsungnya Perjanjian ini (selanjutnya disebut “Informasi Rahasia”).</li>");
                    HTMLContent.Append("<li>Mitra setuju untuk menyimpan Informasi Rahasia Pihak Pertama dan/atau klien, dan Mitra tidak akan menggunakan Informasi Rahasia untuk tujuan apapun selain untuk melaksanakan berdasarkan Perjanjian ini, dan setuju untuk mengembalikan kepada Pihak Pertama dan/atau klien segera atau menghancurkan, tanpa penundaan yang tidak semestinya, seluruh Informasi Rahasia dan lain-lain, yang berada dalam penguasaan Mitra berdasarkan permintaan tertulis dari Pihak Pertama dan/atau klien.</li>");
                    HTMLContent.Append("<li>Ketentuan dalam Pasal ini akan tetap berlaku dan mengikat bahkan setelah Perjanjian ini berakhir.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 9<br />PERNYATAAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS merupakan Pihak yang telah mendapatkan perizinan dan/atau izin dari instansi berwenang untuk menjalankan setiap usahanya sebagaimana yang dimaksud dalam Perjanjian.</li>");
                    HTMLContent.Append("<li>PT SMS merupakan Pihak yang telah mendapatkan perizinan dan/atau izin dari instansi berwenang untuk menjalankan setiap usahanya sebagaimana yang dimaksud dalam Perjanjian.</li>");
                    HTMLContent.Append("<li>Mitra menjamin dalam menjalani kewajiban-kewajiban berdasarkan Perjanjian wajib mengikuti prosedur dari Pihak Pertama dan/atau klien, dan sesuai dengan peraturan perundang-undangan yang berlaku dan apabila terdapat gugatan dan/atau tuntutan atas perbuatan dan/atau tindakan yang dilakukan Mitra yang merugikan Calon Konsumen/Konsumen dan/atau pihak lainnya termasuk Pihak Pertama dan/atau klien maka Mitra akan bertanggung jawab sepenuhnya serta membebaskan Pihak Pertama dan/atau klien dari segala gugatan dan/atau tuntutan yang timbul.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa Mitra tidak akan pernah menyalahgunakan setiap data, dokumen, informasi maupun hal lainnya yang diserahkan oleh Pihak Pertama dan/atau klien dan/atau Calon Konsumen dan/atau Konsumen selain untuk tujuan pelaksanaan Perjanjian ini dan akan menjaga kerahasiaan seluruh dokumen, surat, perjanjian, maupun hal lainnya yang menurut Pihak Pertama dan/atau klien dan/atau Calon Konsumen dan/atau Konsumen merupakan bagian yang tidak diperbolehkan untuk diketahui oleh umum dan tetap menjaga kerahasiannya walaupun Perjanjian ini telah berakhir.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa apa yang dinyatakan, baik pelaksanaan maupun dalam bentuk pernyataan dalam Perjanjian ini adalah benar dan tidak menyesatkan, apabila dikemudian hari hal-hal tersebut tidak sesuai, maka Mitra bertanggung jawab atas seluruh kerugian yang timbul kepada Pihak Pertama dan/atau klien akibat tidak sesuainya hal-hal yang disebutkan tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin bahwa setiap kerugian yang timbul dalam bentuk apapun terhadap Pihak Pertama dan/atau klien akibat tidak dilaksanakan dan/atau tidak sesuai dengan Perjanjian ini maupun peraturan perundang-undangan yang berlaku, maka Mitra akan bertanggung jawab penuh atas kerugian tersebut.</li>");
                    HTMLContent.Append("<li>Mitra menjamin akan menjaga nama baik dari Pihak Pertama dan/atau klien dan tidak akan melakukan perbuatan dan/atau tindakan yang dapat merusak reputasi dan/atau nama baik dari Pihak Pertama dan/atau klien </li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 10<br />PENGALIHAN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS dapat mengalihkan seluruh hak dan kewajiban dalam Perjanjian kepada pihak lainnya dengan pemberitahuan terlebih dahulu kepada Mitra.</li>");
                    HTMLContent.Append("<li>Mitra dilarang untuk mengalihkan baik sebagian maupun keseluruhan hak dan kewajiban berdasarkan Perjanjian ini kepada pihak lainnya tanpa ada persetujuan tertulis dari PT SMS</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 11<br />EVALUASI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>PT SMS berhak untuk melaksanakan evaluasi berkala atas pelaksanaan Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Pelaksanaan evaluasi berkala sebagaimana dimaksud ayat 1 Pasal ini dilakukan paling sedikit sebanyak 2 (dua) kali selama masa Perjanjian.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />KORESPONDENSI</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Untuk keperluan komunikasi dan surat menyurat serta pemberitahuan antara Para Pihak termasuk pemberitahuan tentang perubahan Penanggung Jawab sehubungan dengan pelaksanaan Perjanjian ini, maka disepakati alamat pemberitahuan dan wakil-wakil Para Pihak adalah sebagai berikut :");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaPT + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatPT + " " + AlamatPT1 + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon</td><td width='5px'>:</td><td align='left'>" + TelpPT + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Up</td><td width='5px'>:</td><td align='left'>" + AtasNamaPKS + "</td></tr>");
                    HTMLContent.Append("</table>");
                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding-top:15px'><td colspan='3'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td colspan='3'><b>" + NamaMitra + "</b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Alamat</td><td width='5px'>:</td><td align='left'>" + AlamatMitra + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Telpon/WA</td><td width='5px'>:</td><td align='left'>" + WA + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td width='125px'>Email</td><td width='5px'>:</td><td align='left'>" + EmailMitra + "</td></tr>");
                    HTMLContent.Append("</table></li>");
                    HTMLContent.Append("<p style='margin:2px'></p>");

                    HTMLContent.Append("<li>Dalam hal salah satu pihak mengubah atau mengalami perubahan alamat, mengganti atau mengalami pergantian Penanggung Jawab, maka pihak yang mengubah atau mengalami perubahan alamat dan mengganti atau mengalami pergantian Penanggung Jawab tersebut harus segera memberitahukan alamat yang baru atau Penanggung Jawab yang baru selambat-lambatnya 7 (tujuh) hari kalender sejak terjadinya perubahan alamat tersebut.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 13<br />KEADAAN MEMAKSA (FORCE MAJEURE)</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Keadaan Memaksa (force majeure) yaitu suatu peristiwa atau keadaan atau suatu kejadian diluar kekuasaan dan kemampuan salah satu Pihak yang mempengaruhi secara langsung sehingga kewajiban yang ditentukan dalam Perjanjian menjadi tidak dapat dipenuhi.</li>");
                    HTMLContent.Append("<li>Yang dapat digolongkan Keadaan Memaksa adalah perang, kerusuhan, revolusi, bencana alam (banjir, gempa bumi, badai, gunung meletus, tanah longsor, wabah penyakit, dan angin topan), kebakaran, perubahan peraturan perundang-undangan, dan keadaan Memaksa ini tidak termasuk hal-hal merugikan yang disebabkan oleh perbuatan atau kelalaian salah satu Pihak. </li>");
                    HTMLContent.Append("<li>Apabila terjadi  Keadaan Memaksa, dalam jangka waktu 7 (tujuh) hari kalender Pihak yang terkena Keadaan Memaksa wajib memberitahukan hal tersebut kepada pihak yang tidak terkena Keadaan Memaksa yang dibuktikan dengan keterangan dari instansi yang berwenang  dan atas kejadian Keadaan Memaksa tersebut akan dipertimbangkan lebih lanjut terhadap pelaksanaan Perjanjian selanjutnya berdasarkan kesepakatan tertulis Para Pihak. </li>");
                    HTMLContent.Append("<li>Tindakan yang diambil untuk mengatasi terjadinya Keadaan Memaksa, termasuk penambahan jangka waktu Perjanjian (jika ada), akan disepakati secara tertulis oleh Para Pihak.</li>");

                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:80px'>Halaman 2/3</p>");



                    HTMLContent.Append("</body></html>");


                    //page 3
                    HTMLContent.Append("<!DOCTYPE html><html style='font-family:Times New Roman;font-size:9.2px;'background: white;width: 21cm;height: 29.7cm;display: block;margin: 0 auto;margin-bottom: 0.5cm;box-shadow: 0 0 0.5cm rgba(0,0,0,0.5)'><head>");
                    HTMLContent.Append("</head>");
                    HTMLContent.Append("<style>");
                    HTMLContent.Append("page[size = 'A4'] {'background: white;width: 21cm;height: 29.7cm;display: block;margin: 0 auto;margin-bottom: 0.5cm;box-shadow: 0 0 0.5cm rgba(0,0,0,0.5)}");
                    HTMLContent.Append("@media print {body, page[size = 'A4'] {margin: 0;box-shadow: 0'}}");
                    HTMLContent.Append("</style>");

                    HTMLContent.Append("<body style='margin:25px'>");
                    HTMLContent.Append("<img src='" + locimg + "' width='100px' height='50px'>");

                    HTMLContent.Append("<p><center><b>PASAL 14<br />NON GRATIFIKASI (FORCE MAJEURE)</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Para Pihak sepakat untuk tidak memberikan, membujuk untuk memberikan dan/atau mencoba untuk memberikan komisi, hadiah, jasa, pembayaran dan/atau pemberian apapun dalam bentuk apapun lainnya kepada staff, pegawai, pejabat, direktur dan/atau pihak terkait dari masing-masing Pihak;</li>");
                    HTMLContent.Append("<li>Ketentuan sebagaimana yang dimaksud dalam ayat 1 Pasal ini berlaku juga terhadap larangan untuk memberikan dan/atau mencoba memberikan komisi, hadiah, jasa, pembayaran, dan/atau pemberian dalam bentuk apapun sehingga dapat mempengaruhi pejabat publik (yang mencakup setiap pelaksanaan atau kelalaian untuk melaksanakan fungsi-fungsi bahkan jika hal tersebut berada di luar kewenangan resminya) maupun untuk mendapatkan atau mempertahankan keuntungan finansial atau kemudahan/keuntungan dalam bentuk apapun.</li>");
                    HTMLContent.Append("<li>Apabila salah satu Pihak melanggar ketentuan sebagaimana yang dimaksud dalam ayat 1 dan 2 Pasal ini maka Para Pihak sepakat bahwa Perjanjian dan/atau perjanjian apapun lainnya yang berhubungan dengan Para Pihak, dapat diakhiri oleh Pihak yang dirugikan tanpa diperlukannya pemberitahuan tertulis. Dalam hal dilakukannya pengakhiran Perjanjian dengan alasan tersebut diatas, maka Pihak yang melanggar  harus membayar denda kepada Pihak yang dirugikan sebesar nilai dari Perjanjian dan menggantikan segala kerugian yang dialami oleh Pihak yang dirugikan sehubungan dengan terjadinya pengakhiran Perjanjian.</li>");
                    HTMLContent.Append("<li>Dengan ini masing-masing Pihak menjamin, menyatakan, dan berjanji untuk membebaskan Pihak yang dirugikan apabila terbukti melanggar ketentuan sebagaimana yang dimaksud dalam ayat 1 dan 2 Perjanjian ini termasuk namun tidak terbatas pada tuntutan dan/atau gugatan dan menjadi tanggung jawab sepenuhnya dari Pihak yang melanggar.</li>");

                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 15<br />PENYELESAIAN SENGKETA</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Apabila dalam pelaksanaan Perjanjian ini terdapat sengketa atau perselisihan antara Para Pihak, maka Para Pihak sepakat dan setuju untuk menyelesaikannya dengan jalan musyawarah untuk mufakat;</li>");
                    HTMLContent.Append("<li>Apabila ketentuan yang dimaksud dalam ayat 1 Pasal ini tidak tercapai, maka Para Pihak setuju untuk menyelesaikan sengketa atau perselisihan tersebut melalui Pengadilan Negeri dimana PT SMS berdomisili, dengan tidak mengurangi hak PT SMS untuk mengajukan tuntutan atau gugatan hukum terhadap Mitra dihadapan pengadilan lain di wilayah Republik Indonesia sesuai dengan ketentuan hukum yang berlaku.</li>");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p><center><b>PASAL 12<br />LAIN-LAIN</b></center></p>");
                    HTMLContent.Append("<ol style='margin-left:10px;' >");
                    HTMLContent.Append("<li>Segala ketentuan dan syarat dalam Perjanjian ini berlaku serta mengikat bagi Para Pihak;</li>");
                    HTMLContent.Append("<li>Para Pihak menyatakan bahwa masing-masing Pihak secara hukum dan peraturan perundangan berhak untuk menandatangani Perjanjian ini;</li>");
                    HTMLContent.Append("<li>Hal-hal yang tidak tercantum dan diatur dalam Perjanjian ini akan ditetapkan kemudian dalam Addendum secara tertulis berdasarkan kesepakatan Para Pihak dan merupakan bagian yang tidak terpisahkan dengan Perjanjian ini serta mempunyai kekuatan hukum yang sama dengan Perjanjian ini.");
                    HTMLContent.Append("</ol>");

                    HTMLContent.Append("<p>Demikian Perjanjian ini dibuat dalam rangkap 2 (dua),  masing-masing  bermaterai cukup dan mempunyai kekuatan hukum yang sama.</p>");

                    HTMLContent.Append("<p style='margin:2px'></p>");
                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td>Jakarta, " + tglhariini + "</td></tr>");
                    HTMLContent.Append("<tr style='padding:0px' height='230px'><td valign='top'><b>" + NamaPT + "</b></td><td valign='top' align='center'><b>MITRA</b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='margin:40px'></p>");

                    HTMLContent.Append("<table width='70%' cellpadding='0' cellspacing='0'>");
                    HTMLContent.Append("<tr style='padding:0px'><td><b><u>" + AtasNamaPKS + "</u></b></td><td align='center' rowcolspan='2'><b><u>" + NamaMitra + "</u></b></td></tr>");
                    HTMLContent.Append("<tr style='padding:0px'><td>" + JabatanAtasNamaPKS + "</td><td><b></b></td></tr>");
                    HTMLContent.Append("</table>");

                    HTMLContent.Append("<p style='background:blue;width:100%;text-align:right;margin-bottom:0px;margin-top:325px'>Halaman 3/3</p>");


                    HTMLContent.Append("</body></html>");


                    var OurPdfPage = PdfGenerator.GeneratePdf(HTMLContent.ToString(), configurationOptions);
                    OurPdfPage.Pages.RemoveAt(3);
                    OurPdfPage.Save(ms);
                    res = ms.ToArray();
                }

                var contenttypeed = "application/pdf";
                string powderdockp = "0";
                string powderdockd = "0";


                string filenamepar = (NIKMitra + "_" + NomorMOU.Replace("/", "") + "_" + NamaMitra);
                if (filenamepar.Length > 50)
                {
                    filenamepar = filenamepar.Substring(0, filenamepar.Length - 10);
                }
                filenamepar = filenamepar + ".pdf";


                //"application /vnd.ms-excel";// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application /ms-excel";
                var viewpathed = "Content/assets/pages/pdfjs-dist/web/viewer.html?parpowderdockp=" + powderdockp + "&parpowderdockd=" + powderdockd + "&pardsecuredmoduleID=&file=";
                var jsonresult = Json(new { moderror = IsErrorTimeout, bytetyipe = res, msg = "", contenttype = contenttypeed, filename = filenamepar, viewpath = viewpathed, JsonRequestBehavior.AllowGet });
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

        #endregion PKS

        public static byte[] ReadAllBytes(Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }


        public static byte[] SPLSales(string NomorSPL, string AtasNamaPKS, string JabatanAtasNamaPKS, string AlamatPT, string NamaMitra, string KtpMitra, string TglMasukMitra, string TglAkhirMitra, string tglhariini)
        {
            double lebartable = 700;
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {

                XPen pen = new XPen(XColors.Black);

                PdfSharp.Pdf.PdfDocument documentpdfdetail1 = new PdfSharp.Pdf.PdfDocument();

                PdfSharp.Pdf.PdfPage pagedetail = new PdfSharp.Pdf.PdfPage();
                pagedetail.Size = PdfSharp.PageSize.A4;
                pagedetail.Orientation = PageOrientation.Portrait;
                documentpdfdetail1.AddPage(pagedetail);

                pagedetail.TrimMargins.Top = 25;
                pagedetail.TrimMargins.Left = 25;
                pagedetail.TrimMargins.Right = 25;
                pagedetail.TrimMargins.Bottom = 25;

                double titikawalcaption = 25;
                double titikawalcaptioninc = 15;

                XRect rectdetail = new XRect();
                //XFont fontFoth1 = new XFont("Arial", 10, XFontStyle.Regular);
                XFont fontFothb = new XFont("Arial", 12, XFontStyle.Bold);
                XFont fontFothcap = new XFont("Arial", 16, XFontStyle.Bold);


                XFont fontFothR10 = new XFont("Arial", 10, XFontStyle.Regular);
                XFont fontFothR12 = new XFont("Arial", 12, XFontStyle.Regular);

                XFont fontFothb10 = new XFont("Arial", 10, XFontStyle.Bold);
                XFont fontFothb12 = new XFont("Arial", 12, XFontStyle.Bold);

                XFont fontFothk = new XFont("Arial", 10, XFontStyle.Regular);
                XFont fontFothsk = new XFont("Arial", 7, XFontStyle.Regular);
                XFont fontFothskcolor = new XFont("Arial", 7, XFontStyle.Regular);

                //set grapic draw pdf//
                XGraphics gfxdetail = XGraphics.FromPdfPage(pagedetail);
                //prepare draw text in pdf//
                XTextFormatter tfdetail = new XTextFormatter(gfxdetail);
                tfdetail.Alignment = XParagraphAlignment.Left;
                //XSize size = gfxdetail.MeasureString("", fontFoth1);

                string pathserver = pathserver = System.Web.HttpContext.Current.Server.MapPath("~"); // System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");
                PdfSharp.Drawing.XImage image = PdfSharp.Drawing.XImage.FromFile(pathserver + "\\Images\\logosms1.png");
                int heighimage = image.PixelHeight;
                int widthimage = image.PixelWidth - 50;

                gfxdetail.DrawImage(image, -15, -15, widthimage - 40, heighimage - 50);

                //titikawalcaption = titikawalcaption + 25;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("PT SEJAHTERA MITRA SOLUSI", fontFothcap, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                //titikawalcaption = titikawalcaption + 20;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("Jl. Johar no. 22, RT 016 RW 06 Kel Kebon Sirih, Kec Menteng, Jakarta Pusat 10340", fontFothk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                //titikawalcaption = titikawalcaption + 15;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("No.Telp : (021) 3910972", fontFothk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                //titikawalcaption = titikawalcaption + 40;
                ////garis awal membuata table//
                //gfxdetail.DrawLine(pen, pagedetail.TrimMargins.Left, titikawalcaption, lebartable - 120, titikawalcaption);

                tfdetail.Alignment = XParagraphAlignment.Center;
                titikawalcaption = titikawalcaption + 70;
                rectdetail = new XRect(pagedetail.TrimMargins.Left - 80, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("SURAT PENUNJUKAN \n Nomor : " + NomorSPL, fontFothb, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                tfdetail.Alignment = XParagraphAlignment.Left;


                titikawalcaption = titikawalcaption + 50;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Yang bertandatangan di bawah ini : ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nama ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + AtasNamaPKS, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Jabatan ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + JabatanAtasNamaPKS, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Alamat ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : Jl. Johar no. 22, RT 016 RW 06 Kel Kebon Sirih, Kec Menteng, Jakarta Pusat 10340", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + 35;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Dengan ini menunjuk : ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nama ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + NamaMitra, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nomor KTP ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + KtpMitra, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                string textall = "\rSebagai salah satu Mitra penjualan dan/atau perwakilan PT SMS khususnya untuk melakukan Penawaran " +
                    "Produk Pembiayaan Konsumen yang tidak bertentangan dengan peraturan perundang-undangan yang berlaku untuk PT Adira Dinamika Multi Finance Tbk," +
                    "pada periode " + TglMasukMitra + " sampai dengan " + TglAkhirMitra + "." +
                    "\rDalam hal hubungan hukum antara PT SMS dengan Mitra Penjualan berakhir oleh sebab apapun sebelum berakhirnya " +
                    "periode penunjukan tersebut di atas, dan hal tersebut telah tercatat dalam sistem administrasi PT SMS," +
                    "maka Surat Penunjukan ini menjadi tidak berlaku lagi." +
                    "\r\r\rDemikian penunjukan ini kami buat, untuk dapat dipergunakan sebagaimana mestinya.\r\r";

                tfdetail.Alignment = XParagraphAlignment.Justify;
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable - 170, titikawalcaption);
                tfdetail.DrawString(textall, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                tfdetail.Alignment = XParagraphAlignment.Left;
                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 14);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Jakarta," + tglhariini, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                tfdetail.Alignment = XParagraphAlignment.Left;
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("PT Sejahtera Mitra Solusi", fontFothb, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 7);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(AtasNamaPKS, fontFothb, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                ////garis awal membuata table//
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                //gfxdetail.DrawLine(pen, pagedetail.TrimMargins.Left, titikawalcaption, 170, titikawalcaption);

                titikawalcaption = titikawalcaption + 20;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(JabatanAtasNamaPKS, fontFothb, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 5);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Tembusan:\rPT Adira Dinamika Multi Finance, Tbk", fontFothsk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);


                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 7);
                rectdetail = new XRect(5, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("PT Sejahtera Mitra Solusi\rJl. Johar No.22\rJakarta Pusat 10340\rTelp: 021-3910972", fontFothsk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                documentpdfdetail1.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        public static byte[] SPLWOPI(string NomorSPL, string AtasNamaPKS, string JabatanAtasNamaPKS, string AlamatPT, string NamaMitra, string KtpMitra, string TglMasukMitra, string TglAkhirMitra, string tglhariini)
        {
            double lebartable = 700;

            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {

                XPen pen = new XPen(XColors.Black);

                PdfSharp.Pdf.PdfDocument documentpdfdetail1 = new PdfSharp.Pdf.PdfDocument();

                PdfSharp.Pdf.PdfPage pagedetail = new PdfSharp.Pdf.PdfPage();
                pagedetail.Size = PdfSharp.PageSize.A4;
                pagedetail.Orientation = PageOrientation.Portrait;
                documentpdfdetail1.AddPage(pagedetail);

                pagedetail.TrimMargins.Top = 25;
                pagedetail.TrimMargins.Left = 25;
                pagedetail.TrimMargins.Right = 25;
                pagedetail.TrimMargins.Bottom = 25;

                double titikawalcaption = 25;
                double titikawalcaptioninc = 15;

                XRect rectdetail = new XRect();
                XFont fontFothb = new XFont("Arial", 12, XFontStyle.Bold);

                XFont fontFothR10 = new XFont("Arial", 10, XFontStyle.Regular);
                XFont fontFothR12 = new XFont("Arial", 12, XFontStyle.Regular);

                XFont fontFothb10 = new XFont("Arial", 10, XFontStyle.Bold);
                XFont fontFothb12 = new XFont("Arial", 12, XFontStyle.Bold);

                XFont fontFothcap = new XFont("Arial", 16, XFontStyle.Bold);
                XFont fontFothk = new XFont("Arial", 10, XFontStyle.Regular);
                XFont fontFothsk = new XFont("Arial", 7, XFontStyle.Regular);
                XFont fontFothskcolor = new XFont("Arial", 7, XFontStyle.Regular);

                //set grapic draw pdf//
                XGraphics gfxdetail = XGraphics.FromPdfPage(pagedetail);
                //prepare draw text in pdf//
                XTextFormatter tfdetail = new XTextFormatter(gfxdetail);
                tfdetail.Alignment = XParagraphAlignment.Left;
                //XSize size = gfxdetail.MeasureString("", fontFoth1);

                string pathserver = pathserver = System.Web.HttpContext.Current.Server.MapPath("~"); // System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");
                PdfSharp.Drawing.XImage image = PdfSharp.Drawing.XImage.FromFile(pathserver + "\\Images\\logosms1.png");
                int heighimage = image.PixelHeight;
                int widthimage = image.PixelWidth - 50;

                gfxdetail.DrawImage(image, -15, -15, widthimage - 40, heighimage - 50);

                //titikawalcaption = titikawalcaption + 25;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("PT SEJAHTERA MITRA SOLUSI", fontFothcap, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                //titikawalcaption = titikawalcaption + 20;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("Jl. Johar no. 22, RT 016 RW 06 Kel Kebon Sirih, Kec Menteng, Jakarta Pusat 10340", fontFothk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                //titikawalcaption = titikawalcaption + 15;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("No.Telp : (021) 3910972", fontFothk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                // titikawalcaption = titikawalcaption + 40;
                //garis awal membuata table//
                //gfxdetail.DrawLine(pen, pagedetail.TrimMargins.Left, titikawalcaption, lebartable - 120, titikawalcaption);

                tfdetail.Alignment = XParagraphAlignment.Center;
                titikawalcaption = titikawalcaption + 70;
                rectdetail = new XRect(pagedetail.TrimMargins.Left - 80, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("SURAT PENUNJUKAN \n Nomor : " + NomorSPL, fontFothb, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                tfdetail.Alignment = XParagraphAlignment.Left;


                titikawalcaption = titikawalcaption + 50;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Yang bertandatangan di bawah ini : ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nama ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + AtasNamaPKS, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Jabatan ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + JabatanAtasNamaPKS, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Alamat ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : Jl. Johar no. 22, RT 016 RW 06 Kel Kebon Sirih, Kec Menteng, Jakarta Pusat 10340", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + 35;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Dengan ini menunjuk : ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nama ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + NamaMitra, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nomor KTP ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + KtpMitra, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                string textall = "\rSebagai salah satu Mitra penagihan dan/atau perwakilan PT SMS khususnya untuk melakukan Penawaran " +
                    "Keringanan Denda Konsumen untuk PT Adira Dinamika Multi Finance Tbk, " +
                    "pada periode " + TglMasukMitra + " sampai dengan " + TglAkhirMitra + "." +
                    "\rDalam hal hubungan hukum antara PT SMS dengan Mitra berakhir oleh sebab apapun sebelum berakhirnya " +
                    "periode penunjukan tersebut di atas, dan hal tersebut telah tercatat dalam sistem administrasi PT SMS," +
                    "maka Surat Penunjukan ini menjadi tidak berlaku lagi." +
                    "\r\r\rDemikian penunjukan ini kami buat, untuk dapat dipergunakan sebagaimana mestinya.\r\r";

                tfdetail.Alignment = XParagraphAlignment.Justify;
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable - 170, titikawalcaption);
                tfdetail.DrawString(textall, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                tfdetail.Alignment = XParagraphAlignment.Left;
                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 14);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Jakarta," + tglhariini, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                tfdetail.Alignment = XParagraphAlignment.Left;
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("PT Sejahtera Mitra Solusi", fontFothb10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 7);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(AtasNamaPKS, fontFothb10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                //garis awal membuata table//
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                //gfxdetail.DrawLine(pen, pagedetail.TrimMargins.Left, titikawalcaption, 170, titikawalcaption);

                titikawalcaption = titikawalcaption + 20;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(JabatanAtasNamaPKS, fontFothb10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 5);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Tembusan:\rPT Adira Dinamika Multi Finance, Tbk", fontFothsk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 7);
                rectdetail = new XRect(5, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("PT Sejahtera Mitra Solusi\rJl. Johar No.22\rJakarta Pusat 10340\rTelp: 021-3910972", fontFothsk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);


                documentpdfdetail1.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        public static byte[] SPLRECOCOL(string NomorSPL, string AtasNamaPKS, string JabatanAtasNamaPKS, string AlamatPT, string NamaMitra, string KtpMitra, string TglMasukMitra, string TglAkhirMitra, string tglhariini)
        {
            double lebartable = 700;

            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {

                XPen pen = new XPen(XColors.Black);

                PdfSharp.Pdf.PdfDocument documentpdfdetail1 = new PdfSharp.Pdf.PdfDocument();

                PdfSharp.Pdf.PdfPage pagedetail = new PdfSharp.Pdf.PdfPage();
                pagedetail.Size = PdfSharp.PageSize.A4;
                pagedetail.Orientation = PageOrientation.Portrait;
                documentpdfdetail1.AddPage(pagedetail);

                pagedetail.TrimMargins.Top = 25;
                pagedetail.TrimMargins.Left = 25;
                pagedetail.TrimMargins.Right = 25;
                pagedetail.TrimMargins.Bottom = 25;

                double titikawalcaption = 25;
                double titikawalcaptioninc = 15;

                XRect rectdetail = new XRect();
                XFont fontFothb = new XFont("Arial", 12, XFontStyle.Bold);

                XFont fontFothR10 = new XFont("Arial", 10, XFontStyle.Regular);
                XFont fontFothR12 = new XFont("Arial", 12, XFontStyle.Regular);

                XFont fontFothb10 = new XFont("Arial", 10, XFontStyle.Bold);
                XFont fontFothb12 = new XFont("Arial", 12, XFontStyle.Bold);

                XFont fontFothcap = new XFont("Arial", 16, XFontStyle.Bold);
                XFont fontFothk = new XFont("Arial", 10, XFontStyle.Regular);
                XFont fontFothsk = new XFont("Arial", 7, XFontStyle.Regular);
                XFont fontFothskcolor = new XFont("Arial", 7, XFontStyle.Regular);

                //set grapic draw pdf//
                XGraphics gfxdetail = XGraphics.FromPdfPage(pagedetail);
                //prepare draw text in pdf//
                XTextFormatter tfdetail = new XTextFormatter(gfxdetail);
                tfdetail.Alignment = XParagraphAlignment.Left;

                string pathserver = pathserver = System.Web.HttpContext.Current.Server.MapPath("~"); // System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");
                PdfSharp.Drawing.XImage image = PdfSharp.Drawing.XImage.FromFile(pathserver + "\\Images\\logosms1.png");
                int heighimage = image.PixelHeight;
                int widthimage = image.PixelWidth - 50;

                gfxdetail.DrawImage(image, -15, -15, widthimage - 40, heighimage - 50);

                //titikawalcaption = titikawalcaption + 25;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("PT SEJAHTERA MITRA SOLUSI", fontFothcap, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                //titikawalcaption = titikawalcaption + 20;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("Jl. Johar no. 22, RT 016 RW 06 Kel Kebon Sirih, Kec Menteng, Jakarta Pusat 10340", fontFothk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                //titikawalcaption = titikawalcaption + 15;
                //rectdetail = new XRect(pagedetail.TrimMargins.Left + 150, titikawalcaption, lebartable, titikawalcaption);
                //tfdetail.DrawString("No.Telp : (021) 3910972", fontFothk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                //titikawalcaption = titikawalcaption + 40;
                ////garis awal membuata table//
                //gfxdetail.DrawLine(pen, pagedetail.TrimMargins.Left, titikawalcaption, lebartable - 120, titikawalcaption);

                tfdetail.Alignment = XParagraphAlignment.Center;
                titikawalcaption = titikawalcaption + 70;
                rectdetail = new XRect(pagedetail.TrimMargins.Left - 80, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("SURAT PENUNJUKAN \n Nomor : " + NomorSPL, fontFothb, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                tfdetail.Alignment = XParagraphAlignment.Left;


                titikawalcaption = titikawalcaption + 50;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Yang bertandatangan di bawah ini : ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nama ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + AtasNamaPKS, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Jabatan ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + JabatanAtasNamaPKS, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Alamat ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : Jl. Johar no. 22, RT 016 RW 06 Kel Kebon Sirih, Kec Menteng, Jakarta Pusat 10340", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + 35;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Dengan ini menunjuk : ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nama ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + NamaMitra, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Nomor KTP ", fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);
                rectdetail = new XRect(pagedetail.TrimMargins.Left + 120, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(" : " + KtpMitra, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                string textall = "\rSebagai salah satu Mitra penagihan dan/atau perwakilan PT SMS khususnya untuk melakukan Penagihan " +
                    "(kegiatan-kegiatan yang dilakukan oleh Mitra yang termasuk namun tidak terbatas dalam penagihan angsuran, " +
                    "penerimaan Barang dan kegiatan lainnya yang tertera pada Dokumen Penagihan dan tidak bertentangan " +
                    "dengan peraturan perundang-undangan yang berlaku) untuk PT Adira Dinamika Multi Finance Tbk," +
                    "pada periode " + TglMasukMitra + " sampai dengan " + TglAkhirMitra + "." +
                    "\rDalam hal hubungan hukum antara PT SMS dengan Mitra berakhir oleh sebab apapun sebelum berakhirnya " +
                    "periode penunjukan tersebut di atas, dan hal tersebut telah tercatat dalam sistem administrasi PT SMS," +
                    "maka Surat Penunjukan ini menjadi tidak berlaku lagi." +
                    "\r\r\rDemikian penunjukan ini kami buat, untuk dapat dipergunakan sebagaimana mestinya.\r\r";

                tfdetail.Alignment = XParagraphAlignment.Justify;
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable - 170, titikawalcaption);
                tfdetail.DrawString(textall, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                tfdetail.Alignment = XParagraphAlignment.Left;
                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 14);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Jakarta," + tglhariini, fontFothR10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                tfdetail.Alignment = XParagraphAlignment.Left;
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("PT Sejahtera Mitra Solusi", fontFothb10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 7);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(AtasNamaPKS, fontFothb10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                //garis awal membuata table//
                titikawalcaption = titikawalcaption + titikawalcaptioninc;
                //gfxdetail.DrawLine(pen, pagedetail.TrimMargins.Left, titikawalcaption, 170, titikawalcaption);

                titikawalcaption = titikawalcaption + 20;
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString(JabatanAtasNamaPKS, fontFothb10, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 5);
                rectdetail = new XRect(pagedetail.TrimMargins.Left, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("Tembusan:\rPT Adira Dinamika Multi Finance, Tbk", fontFothsk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                titikawalcaption = titikawalcaption + (titikawalcaptioninc * 7);
                rectdetail = new XRect(5, titikawalcaption, lebartable, titikawalcaption);
                tfdetail.DrawString("PT Sejahtera Mitra Solusi\rJl. Johar No.22\rJakarta Pusat 10340\rTelp: 021-3910972", fontFothsk, XBrushes.Black, rectdetail, XStringFormats.TopLeft);

                documentpdfdetail1.Save(ms);
                res = ms.ToArray();
            }


            return res;
        }

    }
}
