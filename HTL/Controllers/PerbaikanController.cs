//using ExcelDataReader;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Globalization;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;

//namespace DusColl.Controllers
//{
//    public class PerbaikanController : Controller
//    {
//        //
//        // GET: /Perbaikan/

//        vmAccount vmAccount = new vmAccount();
//        blAccount blAccount = new blAccount();
//        vmContractsRev vmContractsRev = new vmContractsRev();
//        vmContracts vmContracts = new vmContracts();
//        blcontracts blcontracts = new blcontracts();
//        CustomeModel CustomeModel = new CustomeModel();
//        CustomeVMModel CustomeVMModel = new CustomeVMModel();


//        public async Task<ActionResult> clnregisrev(vmContractsRev model, string caption)
//        {
//            vmAccount = (vmAccount)Session["Account"];
//            vmAccount = blAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], vmAccount);

//            bool IsErrorTimeout = false;

//            if (vmAccount.UserLogin.RouteName != "")
//            {
//                Response.StatusCode = 406;
//                return Json(new { url = Url.Action(vmAccount.UserLogin.Action, vmAccount.UserLogin.Controller) });
//            }


//            try
//            {
//                await Task.Delay(0);
//                return Json(new
//                {
//                    view = ""
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

//        public async Task<ActionResult> clnContractGet(string caption, string NoPerjanjian, string idfdc)
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

//                // NoPerjanjian = CustomSecureData.Decryption(NoPerjanjian);
//                //idfdc = CustomSecureData.Decryption(idfdc);
//                //module kontrak//

//                caption = CustomSecureData.Decryption(caption);
//                vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

//                cFilterContract modFilter = vmContracts.IntiFilter(caption, vmAccount.AccountDetail.ClientID, vmAccount.AccountDetail.IDCabang, vmAccount.AccountDetail.IDNotaris, vmAccount.AccountDetail.UserType);

//                modFilter.SelectClient = idfdc;
//                modFilter.NoPerjanjian = NoPerjanjian.ToString();
//                modFilter.isModeFilter = true;
//                modFilter.idcaption = CustomSecureData.Encryption(caption);

//                modFilter.PageNumber = 1;
//                List<String> recordPage = await vmContracts.dbGetContractListCount(modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);
//                modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//                modFilter.TotalPage = Convert.ToDouble(recordPage[1]);

//                //set paging in grid client//
//                modFilter.pagingsizeclient = Convert.ToDouble(recordPage[2]);
//                modFilter.pagenumberclient = 1;
//                await vmContracts.dbGetContractList(null, modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);
//                modFilter.totalRecordclient = vmContracts.DTOrdersFromDB.Rows.Count;
//                modFilter.totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(modFilter.totalRecordclient.ToString()) / decimal.Parse(modFilter.pagingsizeclient.ToString())).ToString());

//                //set filter to variable filter in class contract for object view//
//                vmContracts.DetailFilter = modFilter;

//                await Task.Delay(0);
//                vmContracts.ContractDetailOne = vmContracts.ContractDetail.Where(x => x.NO_PERJANJIAN == NoPerjanjian).SingleOrDefault();

//                ViewBag.nokonview = vmContracts.ContractDetailOne.NO_PERJANJIAN.ToString() + "-" + vmContracts.ContractDetailOne.NAMA_DEBITUR.ToString();


//                vmContractsRev vmContractsRev = new vmContractsRev();
//                vmContractsRev.ContractDetailOne = vmContracts.ContractDetailOne;

//                TempData["ContractrevList"] = vmContractsRev;


//                return Json(new
//                {
//                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Perbaikan/_InputRevForm.cshtml", vmContractsRev),
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

//        public async Task<ActionResult> clnContractrev(string menu, string caption)
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
//                vmContractsRev.AccountDetail = vmAccount.AccountDetail;
//                vmContractsRev.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();


//                // set data popup for filter //
//                vmContractsRev.ddlBranch = await vmContracts.dbGetDdlBranchListByEncrypt(vmAccount.ddlBranchPopulate, "", vmAccount.AccountDetail.ClientID);
//                ViewData["SelectBranch"] = CustomeModel.Get_SelectListItem(vmContractsRev.ddlBranch);

//                vmContractsRev.ddlClient = await vmContracts.dbGetClientListByEncrypt();
//                ViewData["SelectClient"] = CustomeModel.Get_SelectListItem(vmContractsRev.ddlClient);

//                vmContractsRev.ddlContractStatus = await vmContracts.dbddlgetparamenumsList("ContStatus");
//                ViewData["SelectContractStatus"] = CustomeModel.Get_SelectListItem(vmContractsRev.ddlContractStatus);

//                // try make filter initial & set secure module name //

//                string seccaption = CustomSecureData.Encryption(caption);
//                cFilterContractRev modFilter = vmContractsRev.IntiFilter(seccaption, vmAccount.AccountDetail.ClientID, vmAccount.AccountDetail.IDCabang, vmAccount.AccountDetail.IDNotaris, vmAccount.AccountDetail.UserType);
//                vmContractsRev.securemoduleID = seccaption;

//                //set filter to variable filter in class contract for object view//
//                vmContractsRev.DetailFilter = modFilter;

//                // try show filter data//
//                List<String> recordPage = await vmContractsRev.dbGetContractRevListCount(modFilter, vmAccount.AccountDetail.UserID, vmContractsRev.AccountMetrik.GroupName);
//                modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//                modFilter.TotalPage = Convert.ToDouble(recordPage[1]);
//                await vmContractsRev.dbGetContractRevList(modFilter, vmAccount.AccountDetail.UserID, vmContractsRev.AccountMetrik.GroupName);

//                //set session filterisasi //
//                TempData["ContractRevList"] = vmContractsRev;
//                TempData["contractrevlistfilter"] = modFilter;

//                // set caption menut text //
//                ViewBag.menu = menu;
//                ViewBag.caption = caption;
//                ViewBag.captiondesc = menuitemdescription;
//                ViewBag.Total = "Total  : " + modFilter.TotalRecord.ToString() + " Berkas";

//                return Json(new
//                {
//                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Perbaikan/uiListRev.cshtml", vmContractsRev),
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

//        public async Task<ActionResult> clnContractRevGet(string caption, string NoRefPerjanjian, string idfdc, string tipos)
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


//                string viewhtml = "";
//                NoRefPerjanjian = CustomSecureData.Decryption(NoRefPerjanjian);
//                idfdc = CustomSecureData.Decryption(idfdc);

//                await Task.Delay(1);
//                vmContractsRev = TempData["ContractRevList"] as vmContractsRev;

//                if (tipos == "ref")
//                {
//                    string seccaption = CustomSecureData.Encryption("LISTCONTCT");
//                    cFilterContract modFilter = vmContracts.IntiFilter(seccaption, vmAccount.AccountDetail.ClientID, vmAccount.AccountDetail.IDCabang, vmAccount.AccountDetail.IDNotaris, vmAccount.AccountDetail.UserType);
//                    modFilter.NoPerjanjian = NoRefPerjanjian;
//                    modFilter.isModeFilter = true;
//                    await vmContracts.dbGetContractList(null, modFilter, vmAccount.AccountDetail.UserID, vmContractsRev.AccountMetrik.GroupName);

//                    vmContracts.ContractDetailOne = vmContracts.ContractDetail.Where(x => x.NO_PERJANJIAN == NoRefPerjanjian && x.ID_FDC == int.Parse(idfdc)).SingleOrDefault();

//                    viewhtml = "/Views/Perbaikan/_uiInfoContract.cshtml";

//                    TempData["ContractList"] = vmContracts;
//                    TempData["ContractRevList"] = vmContractsRev;

//                    return Json(new
//                    {
//                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewhtml, vmContracts),
//                    });

//                }
//                else
//                {
//                    viewhtml = "/Views/Perbaikan/_uiInfoRev.cshtml";

//                    vmContractsRev.ContractDetailOne = vmContractsRev.ContractDetail.Where(x => x.NO_PERJANJIAN == NoRefPerjanjian && x.ID_FDC == int.Parse(idfdc)).SingleOrDefault();
//                    TempData["ContractRevList"] = vmContractsRev;

//                    return Json(new
//                    {
//                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, viewhtml, vmContractsRev),
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
//                });

//            }
//        }

//        public async Task<ActionResult> clncreateorderregisRev(string menu, string caption)
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
//                vmContractsRev.AccountDetail = vmAccount.AccountDetail;
//                vmContractsRev.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();


//                //// set data popup for filter //
//                //vmContracts.ddlBranch = await vmContracts.dbGetDdlBranchListByEncrypt("", vmAccount.AccountDetail.ClientID);
//                //ViewData["SelectBranch"] = CustomeModel.Get_SelectListItem(vmContracts.ddlBranch);

//                vmContracts.ddlClient = await vmContracts.dbGetClientListByEncrypt();
//                ViewData["SelectClient"] = CustomeModel.Get_SelectListItem(vmContracts.ddlClient);

//                //vmContracts.ddlContractStatus = await vmContracts.dbddlgetparamenumsList("ContStatus");
//                //ViewData["SelectContractStatus"] = CustomeModel.Get_SelectListItem(vmContracts.ddlContractStatus);

//                //// try make filter initial & set secure module name //

//                string seccaption = CustomSecureData.Encryption(caption);
//                cFilterContractRev modFilter = vmContractsRev.IntiFilter(seccaption, vmAccount.AccountDetail.ClientID, vmAccount.AccountDetail.IDCabang, vmAccount.AccountDetail.IDNotaris, vmAccount.AccountDetail.UserType);
//                vmContracts.securemoduleID = seccaption;

//                modFilter.UserTypeApps = vmAccount.AccountDetail.UserType;
//                modFilter.NoPerjanjian = "";
//                ////set filter to variable filter in class contract for object view//
//                //vmContracts.DetailFilter = modFilter;

//                //// try show filter data//
//                //List<String> recordPage = await vmContracts.dbGetContractListCount(modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);
//                //modFilter.TotalRecord = Convert.ToDouble(recordPage[0]);
//                //modFilter.TotalPage = Convert.ToDouble(recordPage[1]);
//                //await vmContracts.dbGetContractList(modFilter, vmAccount.AccountDetail.UserID, vmContracts.AccountMetrik.GroupName);

//                //set session filterisasi //
//                //TempData["ContractorderregisrevList"] = vmContractsRev;
//                TempData["contractorderregislistfilter"] = modFilter;

//                // set caption menut text //
//                await Task.Delay(0);

//                ViewBag.InformationUpload = EnumsDesc.GetDescriptionEnums((ProccessOutput)ProccessOutput.InvalidSize);

//                ViewBag.menu = menu;
//                ViewBag.caption = caption;
//                ViewBag.captiondesc = menuitemdescription;

//                return Json(new
//                {
//                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Perbaikan/uiRevForm.cshtml", modFilter),
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


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> clnUploadOrderRegisRevFile(string menu, string caption, HttpPostedFileBase FileUpload)
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
//                vmContractsRev.AccountDetail = vmAccount.AccountDetail;
//                vmContractsRev.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();



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

//                if (vmContractsRev.AccountMetrik.AllowUpload)
//                {
//                    if (FileUpload != null)
//                    {

//                        string fileName = FileUpload.FileName;
//                        string fileContentType = FileUpload.ContentType;
//                        string fileExtension = System.IO.Path.GetExtension(Request.Files[0].FileName);

//                        if (fileExtension == ".xls")
//                        {

//                            for (int i = 0; i < 12; i++)
//                            {
//                                bulanvsmonth bln = new bulanvsmonth();
//                                bln.bulan = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i].ToString().Substring(0, 3);
//                                bln.idBulan = i + 1;
//                                namabulan.Add(bln);
//                            }


//                            string namafile = fileName.Replace(".xls", "");
//                            string cekkolom = "";

//                            List<cContractsOrderRegis> orderRegis = await vmContracts.dbGetOrderRegisContractList(vmAccount.AccountDetail.ClientID, "", namafile);
//                            //cek nama template excel 
//                            if (orderRegis.Count > 0)
//                            {

//                                //get  total kolom template from db//
//                                int jumlahkolomdb = orderRegis.Where(x => x.JenisValidasi.Contains("KOLOM_")).Count();

//                                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(FileUpload.InputStream);

//                                DataSet ds = excelReader.AsDataSet(new ExcelDataSetConfiguration()
//                                {
//                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
//                                    {
//                                        UseHeaderRow = true
//                                    }
//                                });

//                                Dt = ds.Tables[0];
//                                Dttmp = Dt.Clone();
//                                excelReader.Close();

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

//                                            if (htmlkolomnotfound == "")
//                                            {
//                                                Dttmp.Rows.Add(rows.ItemArray);
//                                                rowint = rowint + 1;
//                                                //cek content colom //
//                                                for (i = 0; i < kolom.Count; i++)
//                                                {
//                                                    NoKontrak = rows[1].ToString();
//                                                    namakolom = kolom[i].ToString();
//                                                    valuekkolom = rows[i].ToString();
//                                                    var tpdate = rows[i].GetType();

//                                                    message = CustomeModel.validdata(namakolom, valuekkolom, orderRegis, Dttmp, tpdate.Name, namabulan);


//                                                    if (message != "")
//                                                    {
//                                                        cekkolom = "cekformat";
//                                                        strrowenter = strrowenter == "" ? "Baris Ke " + (rowint + 1).ToString() + " (NoPerjanjian-" + NoKontrak + ")  : \n" : strrowenter;
//                                                        temphtmlkolomnotfound = temphtmlkolomnotfound + (message != "" ? message + "" : "" + (message != "" ? strenter : ""));
//                                                        break;
//                                                    }




//                                                }

//                                                htmlkolomnotfound = htmlkolomnotfound + strrowenter + temphtmlkolomnotfound;
//                                                strenter = "\n";
//                                                strrowenter = "";
//                                                temphtmlkolomnotfound = "";
//                                                Dttmp.Rows[0].Delete();

//                                            }
//                                            else
//                                            {
//                                                break;
//                                            }
//                                        }
//                                    }

//                                    if (htmlkolomnotfound != "")
//                                    {
//                                        if (cekkolom == "cekformat")
//                                        {

//                                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidExcelFormat));
//                                            resulted = (int)ProccessOutput.FilterValidExcelFormat;
//                                            htmlkolomnotfound = EnumMessage + " pada kolom berikut :\n" + htmlkolomnotfound;
//                                        }
//                                        else
//                                        {
//                                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidNamaExcelFormat));
//                                            resulted = (int)ProccessOutput.FilterValidNamaExcelFormat;
//                                            htmlkolomnotfound = EnumMessage + " pada kolom berikut :\n" + htmlkolomnotfound;
//                                        }

//                                    }
//                                    else
//                                    {

//                                        var duplicates = Dt.AsEnumerable().GroupBy(r => r[1]).Where(gr => gr.Count() > 1).Select(g => g.Key);

//                                        if (duplicates.Count() == 0)
//                                        {
//                                            List<cContractsOrderRegisRevList> ListGrid = await vmContractsRev.dbGetListOrderRegisRev(Dt);
//                                            vmContractsRev.DetailOrderRegisList = ListGrid;

//                                            if (ListGrid.Count > 0)
//                                            {
//                                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(ListGrid, Formatting.Indented);
//                                                DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);

//                                                string resultedcheck = await vmContractsRev.dbOrderRegisRevFidusiaCheck(pDt, vmContractsRev.AccountDetail.ClientID, vmContractsRev.AccountDetail.IDCabang, vmContractsRev.AccountDetail.UserID, vmContractsRev.AccountMetrik.GroupName);
//                                                if (CustomeModel.IsNumber(resultedcheck) && (resultedcheck == "1"))
//                                                {
//                                                    viewhtml = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Perbaikan/_uiGridRevList.cshtml", vmContractsRev);
//                                                    EnumMessage = string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.Success)), "Proses validasi data", ",Silahkan cek data sebelum data diajukan");
//                                                    resulted = 1;

//                                                    TempData["ContractorderregisrevdataList"] = vmContractsRev.DetailOrderRegisList;
//                                                    TempData["Templatenamerev"] = fileName;
//                                                }
//                                                else
//                                                {
//                                                    resulted = resultedcheck.Length > 5 ? int.Parse(resultedcheck.Substring(0, 3)) : int.Parse(resultedcheck);
//                                                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidDataExcelFormat));
//                                                    string EnumMessageExt = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
//                                                    htmlkolomnotfound = resultedcheck.Length > 5 ? EnumMessage + " : \n" + string.Format(EnumMessageExt, resultedcheck.Remove(0, 3)) : "";

//                                                }
//                                            }
//                                            else
//                                            {
//                                                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidNoRecord));
//                                                resulted = (int)ProccessOutput.FilterValidNoRecord;

//                                            }
//                                        }
//                                        else
//                                        {

//                                            string data = "";
//                                            foreach (var x in duplicates)
//                                            {
//                                                data = data + x.ToString() + '\n';

//                                            }
//                                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidDuplicateDateexcel));
//                                            resulted = (int)ProccessOutput.FilterValidDuplicateDateexcel;
//                                            htmlkolomnotfound = EnumMessage + "\n" + data;

//                                        }

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
//                });
//            }
//        }

//        public ActionResult clndownloadorderregisrevfile(string menu, string caption)
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
//                vmContracts.AccountDetail = vmAccount.AccountDetail;
//                vmContracts.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();

//                if (vmContracts.AccountMetrik.AllowDownload == true)
//                {
//                    string filenametempltae = "PerbaikanFidusia.xls";
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
//        public async Task<ActionResult> clnUploadOrderSendRegisRevFile(string menu, string caption, string otpcode, string sended)
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
//                vmContractsRev.AccountDetail = vmAccount.AccountDetail;
//                vmContractsRev.AccountMetrik = vmAccount.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();


//                //get data from session //
//                List<cContractsOrderRegisRevList> ListGrid = TempData["ContractorderregisrevdataList"] as List<cContractsOrderRegisRevList>;

//                await Task.Delay(0);
//                string viewhtml = "";
//                string EnumMessage = "";
//                string verifiedcode = "";
//                int resulted = 0;
//                string templatename = TempData["Templatenamerev"] as string;

//                ListGrid = (ListGrid == null) ? new List<cContractsOrderRegisRevList>() : ListGrid;


//                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
//                string ipAddress2 = Request.UserHostAddress;
//                string HostPCName = Dns.GetHostName();

//                if (ListGrid.Count > 0)
//                {
//                    if (sended == "")
//                    {
//                        //get user identity host//js
//                        resulted = await CustomeModel.dbOTPcheckvalid(vmAccount.AccountDetail.UserID, templatename, caption, vmAccount.AccountDetail.Email, HostPCName, ipAddress2, "");
//                        if (resulted != 1)
//                        {
//                            verifiedcode = HashNetFramework.HasKeyProtect.GenerateOTP();
//                            resulted = await CustomeModel.dbOTPverifiedCode(vmAccount.AccountDetail.UserID, templatename, caption, vmAccount.AccountDetail.Email, HostPCName, ipAddress2, verifiedcode);

//                            if (resulted == 1)
//                            {
//                                await vmAccount.dbaccounthostsave(vmAccount.UserLogin.UserID, HostPCName, ipAddress, "Request Code For Perbaikan Fidusia " + menu + " : " + verifiedcode);
//                                //MessageEmail.sendEmail((int)EmailType.OtpOrderFidusia, vmAccount.AccountConfig, vmAccount.AccountDetail.Email, verifiedcode);
//                            }
//                            else
//                            {
//                                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidoptgenerate));
//                            }
//                        }
//                    }
//                    else
//                    {

//                        resulted = await CustomeModel.dbOTPcheckvalid(vmAccount.AccountDetail.UserID, templatename, caption, vmAccount.AccountDetail.Email, HostPCName, ipAddress2, otpcode, true);
//                        if (resulted == 1)
//                        {
//                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(ListGrid, Formatting.Indented);
//                            DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);


//                            resulted = await vmContractsRev.dbOrderRegisRevFidusiaSave(pDt, vmContractsRev.AccountDetail.ClientID, vmContractsRev.AccountDetail.IDCabang, vmContractsRev.AccountDetail.UserID, vmContractsRev.AccountMetrik.GroupName);
//                            EnumMessage = (resulted == 1) ? string.Format(EnumsDesc.GetDescriptionEnums((ProccessOutput.Success)), "Pengajuan Data Perbaikan Fiduisa", "Berhasil dikirim") : EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);

//                            //clear table//
//                            if (resulted == 1)
//                            {
//                                ListGrid.Clear();
//                            }

//                        }
//                        else
//                        {
//                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidoptgeneratecheck));

//                        }

//                    }

//                }
//                else
//                {
//                    resulted = (int)ProccessOutput.FilterValidDataUploadFound;
//                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidDataUploadFound));
//                }
//                TempData["ContractorderregisrevdataList"] = ListGrid;
//                TempData["Templatenamerev"] = templatename;

//                //send back to client browser//
//                return Json(new
//                {
//                    view = viewhtml,
//                    result = resulted.ToString(),
//                    msg = EnumMessage,
//                    msgerror = "",
//                    DataJson = "",
//                    htmlmsg = "",
//                    datapersend = sended
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


//    }
//}
