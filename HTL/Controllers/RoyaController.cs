//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;
//using Aspose;
//using Aspose.Cells;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using Newtonsoft.Json;
//using System.Web.Helpers;
//using CaptchaMvc.HtmlHelpers;
//using System.Web.Script.Serialization;
//using HashNetFramework;
//using ExcelDataReader;
//using System.Net;
//using System.Globalization;
//using System.Text;

//namespace DusColl.Controllers
//{
//    public class RoyaController : Controller
//    {

//        vmAccount vmAccount = new vmAccount();
//        blAccount blAccount = new blAccount();
//        CustomeModel CustomeModel = new CustomeModel();
//        CustomeVMModel CustomeVMModel = new CustomeVMModel();
//        vmContracts vmContracts = new vmContracts();
//        vmRoya vmRoya = new vmRoya();
//        blRoya blRoya = new blRoya();


//        //dbAccessHelper dbaccess = new dbAccessHelper();
//        //string strconnection = CustomSecureData.Decryption(ConfigurationManager.AppSettings["AppDB"].ToString());


//        //public async Task<ActionResult> clnGetBranch(string clientid)
//        //{
//        //    vmRoya.ddlBranch = await vmRoya.dbGetDdlBranchListByEncrypt("", clientid);

//        //    string branchjson = new JavaScriptSerializer().Serialize(vmRoya.ddlBranch);
//        //    return Json(branchjson, JsonRequestBehavior.AllowGet);
//        //}

//        //public async Task<ActionResult> clnRoyaRgrid(int paged)
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
//        //        //get session filterisasi //
//        //        cFilter modFilter = TempData["royalistfilter"] as cFilter;
//        //        vmRoya vmRoya = TempData["RoyaList"] as vmRoya;

//        //        // try show filter data//
//        //        modFilter.PageNumber = paged;
//        //        List<String> recordPage = await vmRoya.dbGetRoyaListCount(modFilter, vmAccount.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);
//        //        modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//        //        modFilter.TotalPage = Convert.ToDouble(recordPage[1]);
//        //        await vmRoya.dbGetRoyaList(modFilter, vmAccount.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);

//        //        //set session filterisasi //
//        //        TempData["RoyaList"] = vmRoya;
//        //        TempData["royalistfilter"] = modFilter;

//        //        return Json(new
//        //        {
//        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiGridRoyaList.cshtml", vmRoya),
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

//        public async Task<ActionResult> clncreateroya(string menu, string caption)
//        {
//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                Response.StatusCode = 406;
//                return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
//            }


//            try
//            {

//                //get menudesccriptio//
//                string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

//                // get metrik user akses by module id//
//                vmRoya.AccountDetail = vmAccount.AccountDetail;
//                vmRoya.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();


//                //// set data popup for filter //
//                //vmContracts.ddlBranch = await vmContracts.dbGetDdlBranchListByEncrypt("", vmAccount.AccountDetail.ClientID);
//                //ViewData["SelectBranch"] = CustomeModel.Get_SelectListItem(vmContracts.ddlBranch);

//                vmRoya.ddlClient = await vmRoya.dbGetClientListByEncrypt();
//                ViewData["SelectClient"] = CustomeModel.Get_SelectListItem(vmRoya.ddlClient);

//                vmRoya.UserTypeApps = vmAccount.AccountDetail.UserType;

//                //vmContracts.ddlContractStatus = await vmContracts.dbddlgetparamenumsList("ContStatus");
//                //ViewData["SelectContractStatus"] = CustomeModel.Get_SelectListItem(vmContracts.ddlContractStatus);

//                //// try make filter initial & set secure module name //

//                //string seccaption = CustomSecureData.Encryption(caption);
//                //cFilterContract modFilter = vmContracts.IntiFilter(seccaption, vmAccount.AccountDetail.ClientID, vmAccount.AccountDetail.IDCabang, vmAccount.AccountDetail.IDNotaris, vmAccount.AccountDetail.UserType);
//                //vmContracts.securemoduleID = seccaption;

//                ////set filter to variable filter in class contract for object view//
//                //vmContracts.DetailFilter = modFilter;
//                //// try show filter data//
//                //List<String> recordPage = await vmContracts.dbGetContractListCount(modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);
//                //modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//                //modFilter.TotalPage = Convert.ToDouble(recordPage[1]);
//                //await vmContracts.dbGetContractList(modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);

//                //set session filterisasi //
//                TempData["RoyaList"] = vmRoya;
//                //TempData["contractorderregislistfilter"] = modFilter;

//                // set caption menut text //
//                await Task.Delay(0);

//                ViewBag.InformationUpload = EnumsDesc.GetDescriptionEnums((ProccessOutput)ProccessOutput.InvalidSize);

//                ViewBag.menu = menu;
//                ViewBag.caption = caption;
//                ViewBag.captiondesc = menuitemdescription;

//                return Json(new
//                {
//                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/uiRoyaFormMasal.cshtml", vmRoya),
//                });
//            }
//            catch (Exception ex)
//            {
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                Response.StatusCode = 406;
//                Response.TrySkipIisCustomErrors = true;
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                });
//            }

//        }

//        public ActionResult clndownroyafile(string menu, string caption)
//        {


//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                Response.StatusCode = 406;
//                return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
//            }

//            try
//            {
//                caption = CustomSecureData.Decryption(caption);
//                //get menudesccriptio//
//                string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

//                // get metrik user akses by module id//
//                vmRoya.AccountDetail = vmAccount.AccountDetail;
//                vmRoya.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

//                if (vmRoya.AccountMetrik.AllowDownload == true)
//                {
//                    string filenametempltae = "RoyaFidusia.xlsx";
//                    string filedownload = Server.MapPath("~/External/TemplateFidusia/" + filenametempltae);
//                    byte[] fileBytes = System.IO.File.ReadAllBytes(filedownload);

//                    return Json(new { datafile = fileBytes, msg = "", contenttype = "application/vnd.ms-excel", filename = filenametempltae, JsonRequestBehavior.AllowGet });

//                }
//                else
//                {
//                    return Json(new { msg = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidtemplateakses) });
//                }
//            }
//            catch (Exception ex)
//            {
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                Response.StatusCode = 406;
//                Response.TrySkipIisCustomErrors = true;
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                });
//            }


//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> clnUploadRoyaFile(string menu, string caption, string SelectClient, HttpPostedFileBase FileUpload)
//        {
//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            bool IsErrorTimeout = false;

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                //return RedirectToRoute(vmAccount.UserLogin.RouteName);
//                IsErrorTimeout = true;
//            }


//            try
//            {

//                //get menudesccriptio//
//                caption = CustomSecureData.Decryption(caption);
//                string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

//                // get metrik user akses by module id//
//                vmRoya.AccountDetail = vmAccount.AccountDetail;
//                vmRoya.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();



//                await Task.Delay(0);
//                string viewhtml = "";
//                string EnumMessage = "";
//                int resulted = 0;
//                string htmlkolomnotfound = "";
//                string temphtmlkolomnotfound = "";
//                DataTable Dt = new DataTable();
//                DataTable Dttmp = new DataTable();
//                string jsondata = "";
//                List<bulanvsmonth> namabulan = new List<bulanvsmonth>();

//                if (vmRoya.AccountMetrik.AllowUpload)
//                {
//                    if (FileUpload != null)
//                    {

//                        string fileName = FileUpload.FileName;
//                        string fileContentType = FileUpload.ContentType;
//                        string fileExtension = System.IO.Path.GetExtension(Request.Files[0].FileName);

//                        if ((fileExtension == ".csv") || (fileExtension == ".txt"))
//                        {

//                            for (int i = 0; i < 12; i++)
//                            {
//                                bulanvsmonth bln = new bulanvsmonth();
//                                bln.bulan = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i].ToString().Substring(0, 3);
//                                bln.idBulan = i + 1;
//                                namabulan.Add(bln);
//                            }

//                            string namafile = "Roya";
//                            string cekkolom = "";

//                            List<cContractsOrderRegis> orderRegis = await vmContracts.dbGetOrderRegisContractList(vmAccount.AccountDetail.ClientID, SelectClient, namafile);
//                            //cek nama template excel 
//                            if (orderRegis.Count > 0)
//                            {

//                                //get  total kolom template from db//
//                                int jumlahkolomdb = orderRegis.Where(x => x.JenisValidasi.Contains("KOLOM_")).Count();

//                                //IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(FileUpload.InputStream);

//                                //DataSet ds = excelReader.AsDataSet(new ExcelDataSetConfiguration()
//                                //{
//                                //    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
//                                //    {
//                                //        UseHeaderRow = true
//                                //    }
//                                //});
//                                using (System.IO.TextReader tr = new System.IO.StreamReader(FileUpload.InputStream, Encoding.UTF8))
//                                {
//                                    string line;
//                                    while ((line = tr.ReadLine()) != null)
//                                    {
//                                        string[] items = line.Trim().Split('|');
//                                        if (Dt.Columns.Count == 0)
//                                        {
//                                            // Create the data columns for the data table based on the number of items
//                                            // on the first line of the file
//                                            for (int i = 0; i < items.Length; i++)
//                                                Dt.Columns.Add(new DataColumn(items[i], typeof(string)));
//                                        }
//                                        else
//                                        {
//                                            Dt.Rows.Add(items);
//                                        }
//                                    }
//                                }


//                                //update fidusiaonline id jika dikosongkan//
//                                Dt.AsEnumerable().ToList().ForEach(p => p.SetField<string>("FIDUSIA_ONLINE_ID", p.Field<string>("NO_AGREEMENT")));

//                                //Dt = ds.Tables[0];
//                                Dttmp = Dt.Clone();
//                                // excelReader.Close();

//                                int jumlahkolomexcel = Dt.Columns.Count;

//                                if (jumlahkolomdb == jumlahkolomexcel)
//                                {

//                                    string namakolom = "";
//                                    string valuekkolom = "";
//                                    string message = "";
//                                    List<string> kolom = new List<string>();


//                                    foreach (DataColumn col in Dt.Columns)
//                                    {
//                                        namakolom = col.ColumnName;
//                                        //store kolom i array//
//                                        kolom.Add(namakolom);
//                                        int intfound = orderRegis.Where(x => x.valid_value == namakolom).Count();
//                                        if (intfound == 0)
//                                        {
//                                            htmlkolomnotfound = htmlkolomnotfound + "* Kolom " + namakolom + " tidak terdaftar\n";
//                                            cekkolom = "cekheader";
//                                        }

//                                    }

//                                    int i = 0;
//                                    int rowint = 0;
//                                    string strenter = "";
//                                    string strrowenter = "";
//                                    string NoKontrak = "";

//                                    if (htmlkolomnotfound == "")
//                                    {
//                                        foreach (DataRow rows in Dt.Rows)
//                                        {

//                                            //if (htmlkolomnotfound == "")
//                                            //{
//                                            Dttmp.Rows.Add(rows.ItemArray);
//                                            rowint = rowint + 1;
//                                            //cek content colom //
//                                            for (i = 0; i < kolom.Count; i++)
//                                            {
//                                                NoKontrak = rows[3].ToString();
//                                                namakolom = kolom[i].ToString();
//                                                valuekkolom = rows[i].ToString();
//                                                var tpdate = rows[i].GetType();


//                                                message = CustomeModel.validdata(namakolom, valuekkolom, orderRegis, Dttmp, tpdate.Name, namabulan, vmAccount.AccountDetail.IDCabang);


//                                                if (message != "")
//                                                {
//                                                    cekkolom = "cekformat";
//                                                    strrowenter = strrowenter == "" ? "\n\n<strong>* Baris Ke " + (rowint + 1).ToString() + " (NoPerjanjian-" + NoKontrak + ")  : \n</style></strong>" : strrowenter;
//                                                    temphtmlkolomnotfound = temphtmlkolomnotfound + (message != "" ? message + "" : "" + (message != "" ? strenter : ""));
//                                                    //break;
//                                                }

//                                            }

//                                            htmlkolomnotfound = htmlkolomnotfound + strrowenter + temphtmlkolomnotfound;
//                                            strenter = "\n";
//                                            strrowenter = "";
//                                            temphtmlkolomnotfound = "";
//                                            Dttmp.Rows[0].Delete();

//                                            //}
//                                            //else
//                                            //{
//                                            //  break;
//                                            //}
//                                        }
//                                    }

//                                    if (htmlkolomnotfound != "")
//                                    {

//                                        if (cekkolom == "cekformat")
//                                        {

//                                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidExcelFormat));
//                                            resulted = (int)ProccessOutput.FilterValidExcelFormat;
//                                            htmlkolomnotfound = "<p style ='color:black;' align = 'justify'><strong>" + EnumMessage + " pada kolom berikut :</strong>" + htmlkolomnotfound + "</p>";
//                                        }
//                                        else
//                                        {
//                                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidNamaExcelFormat));
//                                            resulted = (int)ProccessOutput.FilterValidNamaExcelFormat;
//                                            htmlkolomnotfound = "<p style ='color:black;' align = 'justify'><strong>" + EnumMessage + " pada kolom berikut :</strong>" + htmlkolomnotfound + "</p>";
//                                        }

//                                    }
//                                    else
//                                    {
//                                        //cek double nokontrak//
//                                        var duplicates = Dt.AsEnumerable().GroupBy(r => r[2]).Where(gr => gr.Count() > 1).Select(g => g.Key);

//                                        //if (duplicates.Count() == 0)
//                                        //{
//                                        vmRoya.DetailRoyaListNotExistsDT = await vmRoya.dbRoyaFidusiaCheck(Dt, vmRoya.AccountDetail.ClientID, SelectClient, vmRoya.AccountDetail.IDCabang, vmRoya.AccountDetail.Email, vmRoya.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);

//                                        var differences = Dt.AsEnumerable().Except(vmRoya.DetailRoyaListNotExistsDT.AsEnumerable(), DataRowComparer.Default);
//                                        vmRoya.DetailRoyaListExistsDT = differences.Any() ? differences.CopyToDataTable() : new DataTable();

//                                        //semuda data pengajuan sudah pernah diajukan seblumnya
//                                        if (vmRoya.DetailRoyaListNotExistsDT.Rows.Count == 0)
//                                        {
//                                            EnumMessage = string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterNotValidDataPengajuan)), "ROYA");
//                                        }
//                                        else
//                                        {
//                                            if (vmRoya.DetailRoyaListNotExistsDT.Columns.Count > 1)
//                                            {

//                                                ViewBag.TotalAju = Dt.Rows.Count;
//                                                ViewBag.TotalDuplicate = duplicates.Count();
//                                                ViewBag.TotalExitsSend = vmRoya.DetailRoyaListExistsDT.Rows.Count;
//                                                ViewBag.TotalValidAju = vmRoya.DetailRoyaListNotExistsDT.Rows.Count;

//                                                vmRoya.DetailRoyaListDT = vmRoya.DetailRoyaListNotExistsDT;

//                                                viewhtml = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiGridRoyaCreateList.cshtml", vmRoya);
//                                                EnumMessage = string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.Success)), "Proses validasi data", ",Silahkan cek data sebelum data diajukan");
//                                                resulted = 1;
//                                            }
//                                            else
//                                            {
//                                                int resul = int.Parse(vmRoya.DetailRoyaListNotExistsDT.Rows[0][0].ToString());
//                                                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resul);

//                                            }
//                                        }

//                                        TempData["RoyadataList"] = vmRoya.DetailRoyaListDT;
//                                        TempData["Templatenameroya"] = fileName;

//                                        //}
//                                        //else
//                                        //{

//                                        //    string data = "";
//                                        //    foreach (var x in duplicates)
//                                        //    {
//                                        //        data = data + x.ToString() + '\n';

//                                        //    }
//                                        //    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidDuplicateDateexcel));
//                                        //    resulted = (int)ProccessOutput.FilterValidDuplicateDateexcel;
//                                        //    htmlkolomnotfound = EnumMessage + "\n" + data;

//                                        //}

//                                    }

//                                }
//                                else
//                                {
//                                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidTotalKolomExcelFormat));
//                                    EnumMessage = string.Format(EnumMessage, jumlahkolomdb.ToString());
//                                    resulted = (int)ProccessOutput.FilterValidTotalKolomExcelFormat;

//                                }

//                            }
//                            else
//                            {

//                                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidNamaExcelFormat));
//                                resulted = (int)ProccessOutput.FilterValidNamaExcelFormat;
//                            }

//                        }
//                        else
//                        {
//                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidExcelExtention));
//                            resulted = (int)ProccessOutput.FilterValidExcelExtention;
//                        }
//                        // string maxsizefile = vmAccount.AccountConfig.Where(x => x.Name == "MAXFILE").SingleOrDefault().Code;

//                    }
//                    else
//                    {
//                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidExcelNotUpload));
//                        resulted = (int)ProccessOutput.FilterValidExcelNotUpload;

//                    }
//                }
//                else
//                {
//                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.NotAccess));
//                    resulted = (int)ProccessOutput.NotAccess;
//                }

//                //send back to client browser//
//                return Json(new
//                {
//                    view = viewhtml,
//                    result = resulted.ToString(),
//                    msg = EnumMessage,
//                    msgerror = "",
//                    DataJson = jsondata,
//                    htmlmsg = htmlkolomnotfound
//                });

//            }
//            catch (Exception ex)
//            {
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                Response.StatusCode = 406;
//                Response.TrySkipIisCustomErrors = true;
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                    moderror = IsErrorTimeout
//                }, JsonRequestBehavior.AllowGet);
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> clnUploadRoyaSendFile(string menu, string caption, string verifiedcode, string parcln, string sended)
//        {
//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                Response.StatusCode = 406;
//                return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
//            }

//            try
//            {

//                //get menudesccriptio//
//                caption = CustomSecureData.Decryption(caption);
//                string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

//                // get metrik user akses by module id//
//                vmRoya.AccountDetail = vmAccount.AccountDetail;
//                vmRoya.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();


//                //get data from session //
//                DataTable ListGrid = TempData["RoyadataList"] as DataTable;

//                var urld = "";
//                string EnumMessage = "";
//                string titleswl = "Informasi";
//                string typeswl = "info";
//                string txtbtnswl = "Tutup";
//                string valid = "";

//                await Task.Delay(0);
//                string viewhtml = "";
//                verifiedcode = verifiedcode ?? "";
//                int resulted = 0;
//                string templatename = "Roya";
//                bool checkkodeverifikasi = false;

//                //ListGrid = (ListGrid == null) ? new List<cRoyaList>() : ListGrid;


//                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
//                string ipAddress2 = Request.UserHostAddress;
//                string HostPCName = Dns.GetHostName();


//                //ambil flag untuk cek kode otp, kode flag harus 1/
//                sended = CustomSecureData.Decryption(sended);


//                //jika kode otp=1 atau ada inputan kode veridikasi , maka harus ada pengecekan otp//
//                checkkodeverifikasi = (sended == "1") ? true : false;


//                if (ListGrid.Rows.Count > 0)
//                {
//                    var resultedexpired = 0;
//                    resulted = await CustomeModel.dbOTPcheckvalid(vmAccount.AccountDetail.UserID, templatename, caption, vmAccount.AccountDetail.Email, HostPCName, ipAddress2, verifiedcode, checkkodeverifikasi);
//                    if (resulted != 1)
//                    {
//                        resultedexpired = resulted;

//                        //cek for the first time generate code//
//                        checkkodeverifikasi = resultedexpired == (int)ProccessOutput.FilterNotValidKodeExpiredFirst ? false : checkkodeverifikasi;

//                        //overide pengecekan : jika otpnya tidak expired maka checkkodeverifikasi jadikan false untuk dignerate ulang otpnya
//                        checkkodeverifikasi = resultedexpired == (int)ProccessOutput.FilterNotValidKodeExpired ? false : checkkodeverifikasi;

//                        //overide pengecekan : jika kode salah input jangan digenrate ulang otpnya //
//                        checkkodeverifikasi = resultedexpired == (int)ProccessOutput.FilterValidoptgeneratecheck ? true : checkkodeverifikasi;

//                        sended = CustomSecureData.Encryption("1");

//                        titleswl = "Masukan Kode Verifikasi";
//                        typeswl = "input";
//                        txtbtnswl = "Proses Konfirmasi";

//                        if (checkkodeverifikasi == false)
//                        {
//                            verifiedcode = HashNetFramework.HasKeyProtect.GenerateOTP();
//                            resulted = await CustomeModel.dbOTPverifiedCode(vmAccount.AccountDetail.UserID, templatename, caption, vmAccount.AccountDetail.Email, HostPCName, ipAddress2, verifiedcode);
//                            if (resulted == 1)
//                            {
//                                await vmAccount.dbaccounthostsave(vmAccount.UserLogin.UserID, HostPCName, ipAddress, "Request Code For Roya Fidusia " + menu + " : " + verifiedcode).ConfigureAwait(false);
//                                MessageEmail.sendEmail((int)EmailType.OtpOrderFidusia, vmAccount.AccountConfig, vmAccount.AccountDetail.Email, verifiedcode);
//                            }

//                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resultedexpired);
//                        }
//                        else
//                        {
//                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
//                        }

//                        resulted = resultedexpired;
//                    }
//                    else
//                    {

//                        resulted = await vmRoya.dbRoyaFidusiaSave(ListGrid, vmRoya.AccountDetail.ClientID, parcln, vmRoya.AccountDetail.IDCabang, vmRoya.AccountDetail.Email, templatename, caption, vmRoya.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);
//                        EnumMessage = (resulted == 1) ? string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.Success)), "Pengajuan Data Roya", "Berhasil dikirim") : EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);

//                        sended = "";
//                        //clear table//
//                        if (resulted == 1)
//                        {
//                            ListGrid.Clear();
//                        }

//                    }

//                }
//                else
//                {
//                    resulted = (int)ProccessOutput.FilterValidDataUploadFound;
//                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidDataUploadFound));
//                    valid = "no";
//                }

//                TempData["RoyadataList"] = ListGrid;
//                TempData["Templatenameroya"] = templatename;

//                //send back to client browser//
//                return Json(new
//                {
//                    view = viewhtml,
//                    result = resulted.ToString(),
//                    msg = EnumMessage,
//                    msgerror = "",
//                    DataJson = "",
//                    htmlmsg = "",
//                    htmlvalid = valid,
//                    swltitle = titleswl,
//                    swltype = typeswl,
//                    swltxtbtn = txtbtnswl,
//                    cmdput = sended
//                });

//            }
//            catch (Exception ex)
//            {
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                Response.StatusCode = 406;
//                Response.TrySkipIisCustomErrors = true;
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                });
//            }
//        }

//        public async Task<ActionResult> clnListRoya(string menu, string caption)
//        {
//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            bool IsErrorTimeout = false;

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                //return RedirectToRoute(vmAccount.UserLogin.RouteName);
//                IsErrorTimeout = true;
//            }


//            try
//            {

//                //get menudesccriptio//
//                string menuitemdescription = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).Select(y => y.MenuItem.ModuleName).SingleOrDefault().ToString();

//                // get metrik user akses by module id//
//                vmRoya.AccountDetail = vmAccount.AccountDetail;
//                vmRoya.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

//                // set data popup for filter //

//                vmRoya.ddlBranch = await vmRoya.dbGetDdlBranchListByEncrypt("", vmAccount.AccountDetail.ClientID);
//                ViewData["SelectBranch"] = CustomeModel.Get_SelectListItem(vmRoya.ddlBranch);

//                vmRoya.DDLRoyaType = await vmRoya.dbddlgetparamenumsList("RoyaType");
//                ViewData["SelectRoyaType"] = CustomeModel.Get_SelectListItem(vmRoya.DDLRoyaType);

//                vmRoya.DDLRoyaStatus = await vmRoya.dbddlgetparamenumsList("RoyaStatus");
//                ViewData["SelectRoyaStatus"] = CustomeModel.Get_SelectListItem(vmRoya.DDLRoyaStatus);

//                vmRoya.DDLRoyaClient = await vmRoya.dbGetClientListByEncrypt();
//                ViewData["SelectClient"] = CustomeModel.Get_SelectListItem(vmRoya.DDLRoyaClient);

//                // try make filter initial & set secure module name //

//                string seccaption = CustomSecureData.Encryption(caption);
//                cFilter modFilter = vmRoya.IntiFilter(seccaption, vmAccount.AccountDetail.ClientID, vmAccount.AccountDetail.IDCabang, vmAccount.AccountDetail.IDNotaris, vmAccount.AccountDetail.UserType);
//                vmRoya.securemoduleID = seccaption;



//                // try show filter data//
//                //List<String> recordPage = await vmRoya.dbGetRoyaListCount(modFilter, vmAccount.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);
//                //modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//                //modFilter.TotalPage = Convert.ToDouble(recordPage[1]);
//                //await vmRoya.dbGetRoyaList(modFilter, vmAccount.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);


//                modFilter.PageNumber = 1;
//                List<String> recordPage = await vmRoya.dbGetRoyaListCount(modFilter, vmAccount.AccountDetail.UserID, vmAccount.AccountGroupUser.GroupName);
//                modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//                modFilter.TotalPage = Convert.ToDouble(recordPage[1]);

//                //set paging in grid client//
//                modFilter.pagingsizeclient = Convert.ToDouble(recordPage[2]);
//                modFilter.pagenumberclient = 1;
//                await vmRoya.dbGetRoyaList(null, modFilter, vmAccount.AccountDetail.UserID, vmAccount.AccountGroupUser.GroupName);
//                modFilter.totalRecordclient = vmRoya.DTOrdersFromDB.Rows.Count;
//                modFilter.totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(modFilter.totalRecordclient.ToString()) / decimal.Parse(modFilter.pagingsizeclient.ToString())).ToString());


//                //set filter to variable filter in class roya for object view//
//                vmRoya.DetailFilter = modFilter;

//                //set session filterisasi //
//                TempData["royalistfilter"] = modFilter;
//                TempData["RoyaList"] = vmRoya;

//                // set caption menut text //
//                ViewBag.menu = menu;
//                ViewBag.caption = caption;
//                ViewBag.captiondesc = menuitemdescription;

//                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";


//                List<cAccountConfig> config = vmAccount.AccountConfig.Where(x => x.Name == "KEYPOWDER").ToList();
//                string keypowder = (config[0].Code.ToString());
//                vmContracts.CheckWithKey = keypowder;

//                // sendback to client browser //
//                return Json(new
//                {
//                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/uiListRoya.cshtml", vmRoya)
//                });
//            }
//            catch (Exception ex)
//            {
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                Response.StatusCode = 406;
//                Response.TrySkipIisCustomErrors = true;
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                    moderror = IsErrorTimeout
//                }, JsonRequestBehavior.AllowGet);
//            }

//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> clnListFilterRoya(cFilter model, string download)
//        {
//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            bool IsErrorTimeout = false;

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                //return RedirectToRoute(vmAccount.UserLogin.RouteName);
//                IsErrorTimeout = true;
//            }


//            try
//            {

//                // get decritiin for module id encryption//
//                string idcaption = CustomSecureData.Decryption(model.idcaption);

//                cFilter modelfilter = TempData["royalistfilter"] as cFilter;
//                vmRoya = TempData["RoyaList"] as vmRoya;

//                // get auth & user For access//
//                vmRoya.AccountDetail = vmAccount.AccountDetail;
//                vmRoya.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == idcaption).SingleOrDefault();

//                //set client login id//
//                model.ClientLogin = modelfilter.ClientLogin;
//                model.CabangLogin = modelfilter.CabangLogin;
//                model.NotaryLogin = modelfilter.NotaryLogin;
//                model.UserType = modelfilter.UserType;
//                model.UserTypeApps = defineusertypeAPPs.GetDefineUserTypeAPPvsDB(modelfilter.ClientLogin, modelfilter.CabangLogin, modelfilter.NotaryLogin, modelfilter.UserType);

//                string validtxt = blRoya.CheckFilterisasiData(model, download);
//                if (validtxt == "")
//                {


//                    //try to get list by filter//
//                    model.isdownload = download == "" ? false : true;
//                    model.isModeFilter = true;

//                    model.PageNumber = 1;
//                    List<String> recordPage = await vmRoya.dbGetRoyaListCount(model, vmAccount.AccountDetail.UserID, vmAccount.AccountGroupUser.GroupName);
//                    model.TotalRecord = Convert.ToDouble(recordPage[0]);
//                    model.TotalPage = Convert.ToDouble(recordPage[1]);

//                    //set paging in grid client//
//                    model.pagingsizeclient = Convert.ToDouble(recordPage[2]);
//                    model.pagenumberclient = 1;
//                    await vmRoya.dbGetRoyaList(null, model, vmAccount.AccountDetail.UserID, vmAccount.AccountGroupUser.GroupName);
//                    model.totalRecordclient = vmRoya.DTOrdersFromDB.Rows.Count;
//                    model.totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(model.totalRecordclient.ToString()) / decimal.Parse(model.pagingsizeclient.ToString())).ToString());

//                    model.isModeFilter = false;

//                    // check user click dowbload or not

//                    // set secure module name to vmroya //
//                    vmRoya.securemoduleID = model.idcaption;

//                    //set filter to detailfilter
//                    vmRoya.DetailFilter = model;

//                    //keep session filterisasi before//
//                    TempData["royalistfilter"] = model;
//                    TempData["RoyaList"] = vmRoya;

//                    ViewBag.Total = "Total Data : " + model.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + model.totalRecordclient.ToString() + " Kontrak";


//                    //sendback to client browser//
//                    return Json(new
//                    {
//                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiGridRoyaList.cshtml", vmRoya),
//                        download = "",
//                        message = validtxt
//                    });


//                }
//                else
//                {

//                    TempData["royalistfilter"] = modelfilter;
//                    TempData["RoyaList"] = vmRoya;

//                    //sendback to client browser//
//                    return Json(new
//                    {
//                        view = "",
//                        download = "",
//                        message = validtxt
//                    });

//                }
//            }
//            catch (Exception ex)
//            {
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                Response.StatusCode = 406;
//                Response.TrySkipIisCustomErrors = true;
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                    moderror = IsErrorTimeout
//                }, JsonRequestBehavior.AllowGet);
//            }

//        }

//        public async Task<ActionResult> clnRoyaRgrid(int paged)
//        {
//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            bool IsErrorTimeout = false;

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                //return RedirectToRoute(vmAccount.UserLogin.RouteName);
//                IsErrorTimeout = true;
//            }

//            try
//            {
//                //get session filterisasi //
//                cFilter modFilter = TempData["royalistfilter"] as cFilter;
//                vmRoya = TempData["RoyaList"] as vmRoya;

//                //// try show filter data//
//                //modFilter.PageNumber = paged;
//                //List<String> recordPage = await vmContracts.dbGetContractListCount(modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);
//                //modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//                //modFilter.TotalPage = Convert.ToDouble(recordPage[1]);
//                //await vmContracts.dbGetContractList(modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);

//                //select page
//                modFilter.pagenumberclient = paged;
//                await vmRoya.dbGetRoyaList(vmRoya.DTOrdersFromDB, modFilter, vmAccount.AccountDetail.UserID, vmAccount.AccountGroupUser.GroupName);


//                ViewBag.Total = "Total Data : " + modFilter.TotalRecord.ToString() + " Kontrak <br /> Data on Pages : " + modFilter.totalRecordclient.ToString() + " Kontrak";

//                //set session filterisasi //
//                TempData["RoyaList"] = vmRoya;
//                TempData["royalistfilter"] = modFilter;

//                return Json(new
//                {
//                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiGridRoyaList.cshtml", vmRoya),
//                });
//            }
//            catch (Exception ex)
//            {
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                Response.StatusCode = 406;
//                Response.TrySkipIisCustomErrors = true;
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                    moderror = IsErrorTimeout
//                }, JsonRequestBehavior.AllowGet);
//            }

//        }

//        //public async Task<ActionResult> clnListRoyaInfo(string secureRoyaID)
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

//        //        //get akses//
//        //        vmRoya.AccountDetail = vmAccount.AccountDetail;
//        //        // get user group & akses //
//        //        string UserID = vmAccount.AccountDetail.UserID;
//        //        string GroupName = vmAccount.AccountGroupUser.GroupName;

//        //        // get filter before & sendback to session for newly filter//
//        //        cFilter modelfilter = TempData["royalistfilter"] as cFilter;
//        //        TempData["royalistfilter"] = modelfilter;

//        //        // try to get info detail roya//
//        //        List<cRoya> ListRoya = await vmRoya.dbGetRoyaListbyID(secureRoyaID, vmAccount.AccountDetail.ClientID, modelfilter.SelectClient);

//        //        // set singlerow for result//
//        //        vmRoya.RoyaOneRow = new cRoya();
//        //        vmRoya.RoyaOneRow = ListRoya.SingleOrDefault();

//        //        // sendback to client browser//
//        //        return Json(new
//        //        {
//        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiInfoRoya.cshtml", vmRoya)
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

//        //public async Task<ActionResult> clnListRoyaUpload()
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
//        //        await Task.Delay(1);
//        //        vmRoya.RoyaOneRow = new cRoya();

//        //        int result = -4;
//        //        //get information size upload for dispalyed in form upload //
//        //        ViewBag.InformationUpload = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);

//        //        //get jenis document untuk document roya//
//        //        vmRoya.ddlDocumentType = await CustomeVMModel.dbGetDocumentTypeList("ROYA");
//        //        ViewData["SelectDocumentType"] = CustomeModel.Get_SelectListItem(vmRoya.ddlDocumentType);

//        //        //sendback to client browser//
//        //        return Json(new
//        //        {
//        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiInputUpload.cshtml", vmRoya)
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

//        ////[HttpPost]
//        ////[ValidateAntiForgeryToken]
//        ////public async Task<ActionResult> clnRoyaUploadipt(vmRoya model, HttpPostedFileBase FileUpload)
//        ////{
//        ////    vmAccount = (vmAccount)Session["Account"];
//        ////    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//        ////    if (vmAccount.UserLogin.RouteName != "")
//        ////    {
//        ////        Response.StatusCode = 406;
//        ////        return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
//        ////    }

//        ////    try
//        ////    {

//        ////        //decript caption foe get akses//
//        ////        string idcaption = CustomSecureData.Decryption(model.securemoduleID);

//        ////        // get user authenticate & access//
//        ////        model.AccountDetail = vmAccount.AccountDetail;
//        ////        model.AccountGroupUser = vmAccount.AccountGroupUser;

//        ////        //set securemoduleid back again//
//        ////        vmRoya.securemoduleID = model.securemoduleID;


//        ////        // get filter before & sendback to session for newly filter//
//        ////        cFilter modelfilter = TempData["royalistfilter"] as cFilter;
//        ////        model.DetailFilter = modelfilter;
//        ////        TempData["royalistfilter"] = model.DetailFilter;

//        ////        // try to insert //
//        ////        int result = await blRoya.ins_attachment(model, FileUpload);


//        ////        // get result after insert//
//        ////        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
//        ////        EnumMessage = result == 1 ? String.Format(EnumMessage, "File", "ditautkan ") : EnumMessage;

//        ////        //Refreseh update upload by Id Roya//
//        ////        if (result > 0)
//        ////        {
//        ////            vmRoya.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == idcaption).SingleOrDefault();
//        ////            //vmRoya.RoyaList = await blRoya.RefreshGridFiler(modelfilter, model.AccountDetail.ClientID, model.AccountDetail.UserID, model.AccountGroupUser.GroupName);
//        ////            TempData["royalistfilter"] = modelfilter;
//        ////        }


//        ////        //return output Json to Browser//
//        ////        return Json(new
//        ////        {
//        ////            view = result > 0 ? CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiGridRoyaList.cshtml", vmRoya) : "",
//        ////            result = result.ToString(),
//        ////            msg = EnumMessage,
//        ////            msgerror = ""
//        ////        });


//        ////    }

//        ////    catch (Exception ex)
//        ////    {
//        ////        Response.StatusCode = 406;
//        ////        var msg = ex.Message.ToString();
//        ////        return Json(new
//        ////        {
//        ////            url = Url.Action("Index", "Error", new { area = "" }),
//        ////        });
//        ////    }
//        ////}

//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<ActionResult> Attactmentdownload(string codeidimg, string securedmoduleID)
//        //{

//        //    vmAccount = (vmAccount)Session["Account"];
//        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//        //    if (vmAccount.UserLogin.RouteName != "")
//        //    {

//        //        return RedirectToRoute(vmAccount.UserLogin.RouteName);
//        //    }
//        //    try
//        //    {

//        //        // get user group & akses //
//        //        string UserID = vmAccount.AccountDetail.UserID;
//        //        string GroupName = vmAccount.AccountGroupUser.GroupName;

//        //        // get filter before & sendback to session for newly filter//
//        //        cFilter modelfilter = TempData["royalistfilter"] as cFilter;
//        //        TempData["royalistfilter"] = modelfilter;

//        //        //get file //
//        //        vmRoya.RoyaDocumentOne = await vmRoya.dbGetDocumentByID(vmAccount.AccountDetail.ClientID, modelfilter.SelectClient, codeidimg, UserID, GroupName, securedmoduleID);
//        //        int result = vmRoya.RoyaDocumentOne.Result;

//        //        //get result from get file//
//        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
//        //        EnumMessage = result == 1 ? String.Format(EnumMessage, "Dokumen", " Diunduh ") : EnumMessage;

//        //        //check result 
//        //        if (result == 1)
//        //        {
//        //            //result OK file will be downloded//
//        //            return File(vmRoya.RoyaDocumentOne.FILE_BYTE, vmRoya.RoyaDocumentOne.CONTENT_TYPE, vmRoya.RoyaDocumentOne.FILE_NAME);
//        //        }
//        //        else
//        //        {
//        //            // result not ok sendback to client browser//
//        //            return Content("<script>alert('" + EnumMessage + "');return false;</script>");
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Response.StatusCode = 500;
//        //        var msg = ex.Message.ToString();
//        //        return RedirectToRoute("ErroPage");
//        //    }
//        //}

//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<ActionResult> clnCancelRoya(string IdSecure)
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

//        //        vmRoya vmRoya = TempData["RoyaList"] as vmRoya;

//        //        //get user akses//
//        //        vmRoya.AccountDetail = vmAccount.AccountDetail;
//        //        vmRoya.AccountGroupUser = vmAccount.AccountGroupUser;

//        //        // get filter before & sendback to session for newly filter//
//        //        cFilter modelfilter1 = TempData["royalistfilter"] as cFilter;
//        //        TempData["royalistfilter"] = modelfilter1;


//        //        //set securemoduleid back again//
//        //        vmRoya.securemoduleID = modelfilter1.idcaption;

//        //        int result = await vmRoya.dbRoyaCancel(IdSecure, vmAccount.UserLogin.UserID, vmAccount.AccountGroupUser.GroupName, vmAccount.AccountDetail.ClientID, modelfilter1.SelectClient);
//        //        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
//        //        EnumMessage = result == 1 ? String.Format(EnumMessage, "Permohonan roya", "dibatalkan") : EnumMessage;

//        //        //Refreseh update cancel by Id Roya when success//
//        //        var viewmodif = "";
//        //        if (result == 1)
//        //        {
//        //            // try show filter data//
//        //            // modelfilter1.PageNumber = 1;
//        //            List<String> recordPage = await vmRoya.dbGetRoyaListCount(modelfilter1, vmAccount.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);
//        //            modelfilter1.TotalRecord = Convert.ToDouble(recordPage[0]);
//        //            modelfilter1.TotalPage = Convert.ToDouble(recordPage[1]);
//        //            await vmRoya.dbGetRoyaList(modelfilter1, vmAccount.AccountDetail.UserID, vmRoya.AccountMetrik.GroupName);

//        //            viewmodif = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Roya/_uiGridRoyaList.cshtml", vmRoya);
//        //        }
//        //        //Refreseh update cancel by Id Roya when success//


//        //        TempData["royalistfilter"] = modelfilter1;
//        //        TempData["RoyaList"] = vmRoya;

//        //        return Json(new
//        //        {
//        //            view = viewmodif,
//        //            msg = EnumMessage
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

//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<ActionResult> clnSubmitRoya(vmRoya model)
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

//        //        // Code for validating the CAPTCHA  
//        //        int result = -99;
//        //        string EnumMessage = "Isikan Captcha dengan benar";
//        //        if (this.IsCaptchaValid("Captcha is not valid"))
//        //        {
//        //            model.AccountDetail = vmAccount.AccountDetail;
//        //            model.AccountGroupUser = vmAccount.AccountGroupUser;

//        //            result = await vmRoya.dbSaveRoya(model);
//        //            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
//        //            EnumMessage = result == 1 ? String.Format(EnumMessage, "Permohonan roya", "dibuat") : EnumMessage;
//        //        }

//        //        return Json(new
//        //        {
//        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Shared/_PartialCaptcha.cshtml", ""),
//        //            msg = EnumMessage,
//        //            resulted = result
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

//        //[HttpPost]
//        //public async Task<ActionResult> clnGetCertificateInfo(string noperjanjian, string securedmoduleID)
//        //{

//        //    vmAccount = (vmAccount)Session["Account"];
//        //    vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//        //    if (vmAccount.UserLogin.RouteName != "")
//        //    {

//        //        return RedirectToRoute(vmAccount.UserLogin.RouteName);
//        //    }
//        //    try
//        //    {

//        //        // get user group & akses //
//        //        string UserID = vmAccount.AccountDetail.UserID;
//        //        string GroupName = vmAccount.AccountGroupUser.GroupName;

//        //        string clientID = vmAccount.AccountDetail.ClientID;
//        //        string cabang = vmAccount.AccountDetail.IDCabang;


//        //        List<cContracts> listcontract = await vmRoya.dbGetContractCertificateInfo(noperjanjian, clientID, cabang, UserID, GroupName, securedmoduleID);
//        //        cContracts contract = new cContracts();
//        //        contract = listcontract.SingleOrDefault();

//        //        string contractJson = JsonConvert.SerializeObject(contract);

//        //        return Json(new
//        //        {
//        //            dataJson = contractJson,
//        //            msg = blRoya.CheckRecordSearch(listcontract)
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

//        //[HttpPost]
//        //public ActionResult clnGetClientInfo()
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
//        //        string contractJson = JsonConvert.SerializeObject(vmAccount.AccountDetail.ClientDetail);
//        //        return Json(new
//        //        {
//        //            dataJson = contractJson
//        //        });


//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        var msg = ex.Message.ToString();
//        //        return Json(new
//        //        {
//        //            url = Url.Action("Index", "Error", new { area = "" }),
//        //        });
//        //    }
//        //}


//        //// Download FIle //
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> AttactmentdownloadDirect(string secureperid, string secureserid, string securedmoduleID, string secIdfdc, string tautanid)
//        {

//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                return RedirectToRoute(vmAccount.UserLogin.RouteName);
//            }

//            try
//            {

//                //get filter data from session before//
//                cFilterContract modFilter = TempData["contractlistfilter"] as cFilterContract;

//                // get user group & akses //
//                string UserID = vmAccount.AccountDetail.UserID;
//                string GroupName = vmAccount.AccountGroupUser.GroupName;


//                //set back filter data from session before//
//                TempData["contractlistfilter"] = modFilter;


//                //cek keyprotect login //
//                string ClientID = CustomSecureData.Decryption(vmAccount.AccountDetail.ClientID);
//                string IDNotaris = CustomSecureData.Decryption(vmAccount.AccountDetail.IDNotaris);
//                string IDCabang = CustomSecureData.Decryption(vmAccount.AccountDetail.IDCabang);
//                string email = (vmAccount.AccountDetail.Email ?? "");
//                string usergencode = CustomSecureData.Decryption(vmAccount.AccountDetail.UserGenCode);

//                List<cAccountConfig> config = vmAccount.AccountConfig.Where(x => x.Name == "KEYPOWDER").ToList();
//                string keypowder = (config[0].Code.ToString());
//                vmRoya.CheckWithKey = keypowder;

//                //set login key//
//                string LoginAksesKey = UserID + email + ClientID + IDNotaris + IDCabang + usergencode;

//                //key inputan user//
//                tautanid = CustomSecureData.Decryption(tautanid);

//                //ambil functiondoc dari vm kontrak //
//                vmContracts vmContracts = new vmContracts();

//                // try to get document attacment //
//                DataTable dt = new DataTable();
//                vmContracts.ContractDocumentOne = null; // await vmContracts.dbGetDocumentByno(dt, secureserid, secIdfdc, "SERTIFIKAT ROYA", vmAccount.AccountDetail.ClientID, modFilter.SelectClient, UserID, GroupName, securedmoduleID, LoginAksesKey);

//                //document sertifikat ada //
//                if (vmContracts.ContractDocumentOne != null)
//                {
//                    int result = vmContracts.ContractDocumentOne.Result;

//                    //cek keyprotect dokumen //
//                    string ClientIDDoc = (vmContracts.ContractDocumentOne.client ?? "");
//                    string IDNotarisDoc = (vmAccount.AccountDetail.IDNotaris ?? "");
//                    string IDCabangDoc = (vmContracts.ContractDocumentOne.cabang ?? "");
//                    string keydocument = UserID + email + ClientID + IDNotaris + IDCabang + usergencode;


//                    if (((tautanid == LoginAksesKey) && (LoginAksesKey == keydocument)) || keypowder == "0")
//                    {


//                        // get result proses//
//                        string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
//                        EnumMessage = result == 1 ? String.Format(EnumMessage, "Dokumen", " Diunduh ") : EnumMessage;

//                        //set back filter data from session before//
//                        TempData["contractlistfilter"] = modFilter;


//                        //check result 1: download file else user tidak ada akses//
//                        if (result == 1)
//                        {

//                            byte[] bytesToDecrypt = HasKeyProtect.SetFileByteDecrypt(vmContracts.ContractDocumentOne.FILE_BYTE, LoginAksesKey);

//                            return Json(new
//                            {
//                                contenttype = vmContracts.ContractDocumentOne.CONTENT_TYPE,
//                                bytetyipe = bytesToDecrypt,
//                                filename = vmContracts.ContractDocumentOne.FILE_NAME,
//                                msg = ""
//                            });
//                        }
//                        else
//                        {
//                            return Json(new
//                            {
//                                msg = EnumMessage
//                            });
//                        }

//                    }
//                    else
//                    {

//                        return Json(new
//                        {
//                            msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKunciSandiFile))
//                        });

//                    }

//                }
//                else
//                {
//                    return Json(new
//                    {
//                        msg = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterNotValidFileRoyaNotFound))
//                    });
//                }
//            }

//            catch (Exception ex)
//            {
//                Response.StatusCode = 406;
//                var msg = ex.Message.ToString();
//                ErrorLogApps.Logger(msg);
//                //return RedirectToRoute("ErroPage");
//                return Json(new
//                {
//                    url = Url.Action("Index", "Error", new { area = "" }),
//                });
//            }
//        }
//        //// END Download FIle Direct //


//    }
//}
