using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DusColl.Controllers
{
    public class AccountController : Controller
    {
        private blAccount lgAccount = new blAccount();
        private vmAccount Account = new vmAccount();
        private vmAccountddl Accountddl = new vmAccountddl();
        private vmCommon Common = new vmCommon();
        private vmCommonddl Commonddl = new vmCommonddl();
        private cFilterContract modFilter = new cFilterContract();

        //// GET: /Account/
        public ActionResult LogUserIn()
        {
            string browser = HttpContext.Request.Browser.Browser;
            if (browser.ToLower() != "chrome" && browser.ToLower() != "firefox")
            {
                return RedirectToRoute("UnsupportBrowser");
            }

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

            if (IsErrorTimeout == false)
            {
                return RedirectToRoute("HomePages");
            }
            else
            {
                cAccount AccountSession = new cAccount();
                AccountSession.ShowMessage = "alert alert-danger display-hide";
                ViewBag.probituser = 0;
                ViewBag.tokenpub = "1";

                return View(AccountSession);
            }
        }

        public async Task<ActionResult> AccountTimeOut()
        {
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            string ipAddress2 = Request.UserHostAddress;
            string HostPCName = Dns.GetHostName();
            string domainname = Request.Url.Host;
            string browser = HttpContext.Request.Browser.Browser;

            Account = (vmAccount)Session["Account"];
            if (Account != null)
            {
                await Commonddl.dbSetHostHistory(Account.AccountLogin.UserID, HostPCName, ipAddress, ipAddress2, "", "0", browser, "lgt");
                Session["Account"] = null;
                TempData.Clear();
                Session.Contents.RemoveAll();
                FormsAuthentication.SignOut();
            }
            //else
            //{
            //    await Commonddl.dbSetHostHistory("", ipAddress, "", browser, "lgt");
            //    HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //    Session["Account"] = null;
            //    TempData.Clear();
            //    Session.Contents.RemoveAll();
            //    FormsAuthentication.SignOut();
            //}
            return View();
        }

        public async Task<ActionResult> LogOut()
        {
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            string ipAddress2 = Request.UserHostAddress;
            string HostPCName = Dns.GetHostName();
            string domainname = Request.Url.Host;
            string browser = HttpContext.Request.Browser.Browser;

            Account = (vmAccount)Session["Account"];
            if (Account != null)
            {
                await Commonddl.dbSetHostHistory(Account.AccountLogin.UserID, HostPCName, ipAddress, ipAddress2, "", "0", browser, "lgt");
                Session["Account"] = null;
                TempData.Clear();
                Session.Contents.RemoveAll();
                FormsAuthentication.SignOut();
                //TempData.Clear();
                //Session.Contents.RemoveAll();

                //Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                //Response.AddHeader("Pragma", "no-cache");
                //Response.AddHeader("Expires", "0");
                //FormsAuthentication.SignOut();
            }
            return View("");
        }

        public ActionResult clnAccountChucPropPrev(string fle)
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
                byte[] imageBytes = Convert.FromBase64String(fle);
                //pooo = pooo.Substring(pooo.Length - 5, 5).Replace("u", "o").ToLower();
                string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                imageBytes = HasKeyProtect.SetFileByteDecrypt(imageBytes, KECEP);
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = "",
                    msg = "",
                    bte = imageBytes,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 406;
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                return Json(new
                {
                    IsErrorTimeout = true,
                    url = Url.Action("Index", "Error", new { area = "" }),
                });
            }
        }

        public async Task<ActionResult> clnAccountChucProp()
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
                ViewBag.caption = "Profile";
                Session["Account"] = Account;

                cAccount mod = new cAccount();
                mod.UserName = Account.AccountLogin.UserName;
                mod.NotarisName = Account.AccountLogin.NotarisName;
                mod.UserID = Account.AccountLogin.UserID;
                mod.Photo = Account.AccountLogin.Photo;
                mod.ttdfform = Account.AccountLogin.ttdfform;
                mod.ttdsk = Account.AccountLogin.ttdsk;
                mod.ttdabsah = Account.AccountLogin.ttdabsah;

                mod.email = HasKeyProtect.Decryption(Account.AccountLogin.email);
                mod.Mailed = Account.AccountLogin.Mailed;

                mod.tgllahir = Account.AccountLogin.tgllahir;
                mod.tptlahir = Account.AccountLogin.tptlahir;
                mod.NIK = Account.AccountLogin.NIK;

                mod.Pekerjaan = Account.AccountLogin.Pekerjaan;
                mod.Jabatan = Account.AccountLogin.Jabatan;
                mod.Phone = HasKeyProtect.Decryption(Account.AccountLogin.Phone);

                mod.Alamat = Account.AccountLogin.Alamat;
                mod.Kota = Account.AccountLogin.Kota;
                mod.Domisili = Account.AccountLogin.Domisili;
                mod.Wilayah = Account.AccountLogin.Wilayah;
                mod.NoSK = HasKeyProtect.Decryption(Account.AccountLogin.NoSK);
                mod.TglSK = Account.AccountLogin.TglSK;

                mod.IDBPN = HasKeyProtect.Decryption(Account.AccountLogin.IDBPN);

                mod.topForm = Account.AccountLogin.ttdfform;
                mod.leftForm = Account.AccountLogin.ttdsk;

                mod.topSK = Account.AccountLogin.ttdfform;
                mod.leftSK = Account.AccountLogin.ttdsk;

                mod.topAB = Account.AccountLogin.ttdfform;
                mod.leftAB = Account.AccountLogin.ttdsk;

                mod.docForm = Account.AccountLogin.ttdfform;
                mod.docSK = Account.AccountLogin.docSK;
                mod.docAB = Account.AccountLogin.docAB;

                mod.JenisKelamin = Account.AccountLogin.JenisKelamin;

                mod.NotarisName = Account.AccountLogin.NotarisName;

                mod.handlemaplist = await Accountddl.HandleMap("0", "", Account.AccountLogin.UserID, Account.AccountLogin.GroupName);

                string UserTypes = HasKeyProtect.Decryption(Account.AccountLogin.UserType);

                var valspit = mod.NotarisName.Split(',');
                List<string> str = new List<string>();
                foreach (var x in valspit)
                {
                    str.Add(HasKeyProtect.Encryption(x));
                }
                ViewBag.namappat = string.Join(",", str);

                ViewBag.Nota = "";
                if (UserTypes == "3")
                {
                    ViewBag.Nota = "ntt";
                }

                if (UserTypes == "0")
                {
                    ViewBag.Nota = "adm";
                }

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/AccountProfile.cshtml", mod),
                    msg = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 406;
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                return Json(new
                {
                    IsErrorTimeout = true,
                    url = Url.Action("Index", "Error", new { area = "" }),
                });
            }
        }

        public ActionResult AccountChucgrp()
        {
            Account = (vmAccount)Session["Account"];
            Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

            if (Account.AccountLogin.RouteName != "")
            {
                return RedirectToRoute(Account.AccountLogin.RouteName);
            }

            Common.ddlGrupAkses = Commonddl.dbGetDdlgrupListByEncrypt(Account.AccountGroupUserList);
            ViewData["SelectGrupAkses"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupAkses);

            ViewBag.caption = "Pemilihan Grup Akses";
            //Account.ForcePass = true;
            Session["Account"] = Account;
            return View(Account);
        }

        public ActionResult AccountChucpas()
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
                return RedirectToRoute("HomePages");
            }
            else
            {
                //    AccountSession.ShowMessage = "alert alert-danger display-hide";
                ViewBag.probituser = 0;
                ViewBag.tokenpub = "1";

                return View(Account);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnAccountChucgongProfle(cAccount model, HttpPostedFileBase potofile, HttpPostedFileBase ttdform, HttpPostedFileBase ttdsk, HttpPostedFileBase ttdabs
            , HttpPostedFileBase docform, HttpPostedFileBase docSK, HttpPostedFileBase docAB)
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
                model.ttdfform = ((model.ttdfform ?? "") == "" || (model.ttdfform ?? "") == "undefined") ? "" : "";
                model.ttdsk = ((model.ttdsk ?? "") == "" || (model.ttdsk ?? "") == "undefined") ? "" : "";
                model.ttdabsah = ((model.ttdabsah ?? "") == "" || (model.ttdabsah ?? "") == "undefined") ? "" : "";

                model.docForm = ((model.docForm ?? "") == "" || (model.docForm ?? "") == "undefined") ? "" : "";
                model.docSK = ((model.docSK ?? "") == "" || (model.docSK ?? "") == "undefined") ? "" : "";
                model.docAB = ((model.docAB ?? "") == "" || (model.docAB ?? "") == "undefined") ? "" : "";

                model.Mailed = HasKeyProtect.Decryption(Account.AccountLogin.Mailed);
                string userIdpass = HasKeyProtect.Decryption(model.UserID);

                int result = 0;
                string EnumMessage = "";
                if (Account.AccountLogin.UserID == userIdpass)
                {
                    model.UserID = userIdpass;
                    model.GroupName = Account.AccountLogin.GroupName;
                    model.Photo = "";
                    if (potofile != null)
                    {
                        if (potofile.ContentLength / 1024 > 50)
                        {
                            EnumMessage = "Ukuran File Profile harus lebih kecil dari 50KB";
                        }
                        else
                        if (potofile.ContentType != "image/jpg" && potofile.ContentType != "image/jpeg")
                        {
                            EnumMessage = "Jenis File Profile harus JPEG/JPG";
                        }
                        else
                        if (
                            (potofile.FileName.Substring(potofile.FileName.Length - 4).ToLower() != ".jpg") &&
                            (potofile.FileName.Substring(potofile.FileName.Length - 5).ToLower() != ".jpeg")
                           )
                        {
                            EnumMessage = "Jenis File Profile harus JPEG/JPG";
                        }
                        else
                        {
                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(potofile.InputStream);
                            imagebyte = reader.ReadBytes((int)potofile.ContentLength);
                            //prepare to encrypt
                            string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);
                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                            model.Photo = base64String;
                        }
                    }

                    if (ttdform != null)
                    {
                        if (ttdform.ContentLength / 1024 > 300)
                        {
                            EnumMessage = "Ukuran File Formulir Permohonan harus lebih kecil dari 300KB";
                        }
                        else
                        if (ttdform.ContentType != "image/jpg" && ttdform.ContentType != "image/jpeg")
                        {
                            EnumMessage = "Jenis File Formulir Permohonan harus JPEG";
                        }
                        else
                        {
                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(ttdform.InputStream);
                            imagebyte = reader.ReadBytes((int)ttdform.ContentLength);
                            //prepare to encrypt
                            string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);
                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                            model.ttdfform = base64String;
                        }
                    }

                    if (ttdsk != null)
                    {
                        if (ttdsk.ContentLength / 1024 > 300)
                        {
                            EnumMessage = "Ukuran File Surat Kuasa harus lebih kecil dari 300KB";
                        }
                        else
                        if (ttdsk.ContentType != "image/jpg" && ttdsk.ContentType != "image/jpeg")
                        {
                            EnumMessage = "Jenis File Surat Kuasa harus JPEG";
                        }
                        else
                        {
                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(ttdsk.InputStream);
                            imagebyte = reader.ReadBytes((int)ttdsk.ContentLength);
                            //prepare to encrypt
                            string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);
                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                            model.ttdsk = base64String;
                        }
                    }

                    if (ttdabs != null)
                    {
                        if (ttdabs.ContentLength / 1024 > 30)
                        {
                            EnumMessage = "Ukuran File Surat Keabsahan harus lebih kecil dari 300KB";
                        }
                        else
                        if (ttdabs.ContentType != "image/jpg" && ttdabs.ContentType != "image/jpeg")
                        {
                            EnumMessage = "Jenis File Surat Keabsahan harus JPEG";
                        }
                        else
                        {
                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(ttdabs.InputStream);
                            imagebyte = reader.ReadBytes((int)ttdabs.ContentLength);
                            //prepare to encrypt
                            string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);
                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                            model.ttdabsah = base64String;
                        }
                    }

                    if (docform != null)
                    {
                        if (docform.ContentLength / 1024 > 30)
                        {
                            EnumMessage = "Ukuran File Formulir Permohonan harus lebih kecil dari 300KB";
                        }
                        else
                        if (docform.ContentType != "image/jpg" && docform.ContentType != "image/jpeg")
                        {
                            EnumMessage = "Jenis File Surat harus .docx";
                        }
                        else
                        {
                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(docform.InputStream);
                            imagebyte = reader.ReadBytes((int)docform.ContentLength);
                            //prepare to encrypt
                            string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);
                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                            model.docForm = base64String;
                        }
                    }

                    if (docSK != null)
                    {
                        if (docSK.ContentLength / 1024 > 30)
                        {
                            EnumMessage = "Ukuran File Surat Kuasa harus lebih kecil dari 300KB";
                        }
                        else
                        if (docSK.ContentType != "image/jpg" && docSK.ContentType != "image/jpeg")
                        {
                            EnumMessage = "Jenis File Surat Kuasa harus .docx";
                        }
                        else
                        {
                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(docSK.InputStream);
                            imagebyte = reader.ReadBytes((int)docSK.ContentLength);
                            //prepare to encrypt
                            string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);
                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                            model.docSK = base64String;
                        }
                    }

                    if (docAB != null)
                    {
                        if (docAB.ContentLength / 1024 > 30)
                        {
                            EnumMessage = "Ukuran File Surat Keabsahan harus lebih kecil dari 300KB";
                        }
                        else
                        if (docAB.ContentType != "image/jpg" && docAB.ContentType != "image/jpeg")
                        {
                            EnumMessage = "Jenis File Surat Keabsahan harus .docx";
                        }
                        else
                        {
                            byte[] imagebyte = null;
                            BinaryReader reader = new BinaryReader(docAB.InputStream);
                            imagebyte = reader.ReadBytes((int)docAB.ContentLength);
                            //prepare to encrypt
                            string KECEP = HasKeyProtect.Encryption(Account.AccountLogin.UserID);
                            byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(imagebyte, KECEP);
                            //convert byte to base//
                            string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                            model.docAB = base64String;
                        }
                    }

                    string perihal = model.NotarisName ?? "";
                    if (model.handlemap != null)
                    {
                        if (model.handlemap.Length > 0)
                        {
                            List<string> nama = new List<string>();
                            foreach (var str in model.handlemap)
                            {
                                nama.Add(HasKeyProtect.Decryption(str));
                            }
                            perihal = string.Join(",", nama);
                        }
                    }

                    if (EnumMessage == "")
                    {
                        model.NotarisName = perihal;
                        result = await Accountddl.dbprofileUserSve(model);
                    }
                }
                else
                {
                    EnumMessage = "Perubahan Profile Tidak Diijinkan";
                }

                EnumMessage = EnumMessage == "" ? EnumsDesc.GetDescriptionEnums((ProccessOutput)result) : EnumMessage;
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, "Perubahan Profile", "disimpan, silahkan untuk 'masuk kembali' untuk menggunakan aplikasi") : EnumMessage;
                // senback to client browser//

                //harus relogin
                if (result == 1)
                {
                    TempData.Clear();

                    Session.Contents.RemoveAll();

                    Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                    Response.AddHeader("Pragma", "no-cache");
                    Response.AddHeader("Expires", "0");

                    FormsAuthentication.SignOut();
                }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnAccountChucgongPro(string SelectGrupAkses)
        {
            Account = (vmAccount)Session["Account"];
            Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

            if (Account.AccountLogin.RouteName != "")
            {
                return RedirectToRoute(Account.AccountLogin.RouteName);
            }

            try
            {
                bool valid = false;
                string UserID = Account.AccountLogin.UserID;

                SelectGrupAkses = HasKeyProtect.Decryption(SelectGrupAkses);
                cAccountGroupUser grupuser = Account.AccountGroupUserList.Where(x => x.GroupName == SelectGrupAkses && x.UserGrup.ToLower() == UserID.ToLower()).SingleOrDefault();

                if (grupuser != null)
                {
                    valid = true;
                    string groupselected = grupuser.GroupName;
                    Account.AccountMetrikList = await Accountddl.dbaccountmatriklist(false, groupselected, "");
                    Account.AccountLogin.GroupName = groupselected;
                }

                Session["Account"] = Account;
                if (Account.AccountMetrikList.Count > 0 && valid == true)
                {
                    return RedirectToRoute("HomePages");
                }
                else
                {
                    return RedirectToRoute("AccountChucgrp");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                //Response.StatusCode = 500;
                //Response.TrySkipIisCustomErrors = true;
                return RedirectToRoute("ErroPage");
                //return Json(new
                //{
                //    url = Url.Action("Index", "Error", new { area = "" }),
                //}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> clnKipatkay(vmAccount model, string majutakgentar, string kursakukenji)
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
                string EnumMessage = "";

                //default for message popup//
                string titleswl = "Informasi";
                string typeswl = "info";
                string txtbtnswl = "Tutup";

                //get user identity host//
                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                string ipAddress2 = Request.UserHostAddress;
                string HostPCName = Dns.GetHostName();
                string templatename = Account.ForcePass == true ? "AccountForcechange" : "Accountchange";
                string UserID = Account.AccountLogin.UserID;
                string UserSec = HasKeyProtect.DecryptionPass(model.AccountLogin.secIDUser);
                string Mailed = HasKeyProtect.Decryption(Account.AccountLogin.Mailed);

                string verifiedcode = model.AccountLogin.Userkodeverified;
                string sended = kursakukenji;

                string verifiedcodeinputbyuser = verifiedcode ?? "";
                string valid = "";

                //decript user pass old from session//
                string pascodeencryp = HasKeyProtect.DecryptionPass(Account.AccountLogin.UserPass);

                //decript user pass old by input user//
                string pascodeencryp1 = (model.AccountLogin.UserPass);

                //try cek additional validation password //
                string pascodenew = model.AccountLogin.PasswordChange ?? "";
                string pascodenewretype = model.AccountLogin.RetypePassword;

                //cek format untuk pwd user//
                int resulted = OwinLibrary.CheckValidationPassWord(pascodenew);

                //cek kata sandi lama yang diinputkan dengan session harus sama//
                if (pascodeencryp != pascodeencryp1)
                {
                    resulted = (int)ProccessOutput.FilterValidKataSandi;
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKataSandi));
                    valid = "no";
                }

                //cek password baru tidak boleh sama dengan password lama
                if (pascodenew == pascodeencryp1)
                {
                    resulted = (int)ProccessOutput.FilterValidKataSandilamabaru;
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidKataSandilamabaru));
                    valid = "no";
                }

                //cek user login dengan user form harus sama//
                if (UserID != UserSec)
                {
                    resulted = (int)ProccessOutput.FilterValidAccountForChange;
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.FilterValidAccountForChange));
                    valid = "no";
                }

                //cek user login dengan user form harus sama//
                if (pascodenew != pascodenewretype)
                {
                    resulted = (int)ProccessOutput.KataSandiNotMathRetype;
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput.KataSandiNotMathRetype));
                    valid = "no";
                }

                // prepare for OTP code //
                cOTP ResultOTP = new cOTP();
                string inputOtpCode = "";
                string RedirectURL = "";
                bool ErrorOTP = false;
                bool isMustInputCode = false;
                bool txtcnlswl = false;
                string ShowMessage = "alert alert-danger";

                //jika valid//
                if ((resulted == 1))
                {
                    //cAccountRegis Account = new cAccountRegis();
                    DataTable dt = await Accountddl.dbSaveChangePass(pascodeencryp1, pascodenew, Mailed, "", UserID, "");
                    resulted = int.Parse(dt.Rows[0][0].ToString());
                    //ResultOTP = await Commonddl.VerifiedOTP(UserID, Mailed, templatename, HostPCName, ipAddress, verifiedcodeinputbyuser, "Request Code For Reset Change Pwd Login", EnumMessage, pascodeencryp, pascodenew);
                    //EnumMessage = ResultOTP.Message;
                    //resulted = ResultOTP.Result;
                    //ErrorOTP = ResultOTP.ErrorOTP;
                    //isMustInputCode = ResultOTP.isMustInputCode;

                    ////jika tidak berhasil simpan ke DB//
                    //if (ErrorOTP == true)
                    //{
                    //    titleswl = "Informasi";
                    //    typeswl = "warning";
                    //    valid = "no";
                    //}

                    // user must input otp //
                    //if (isMustInputCode == true && ErrorOTP == false)
                    //{
                    //    inputOtpCode = "1";
                    //    titleswl = "Kode Verifikasi";
                    //    typeswl = "input";
                    //    txtbtnswl = "Proses Konfirmasi";
                    //    txtcnlswl = true;
                    //}

                    //jika berhasil direcet url
                    if (resulted == (int)ProccessOutput.Success)
                    {
                        RedirectURL = Url.Action("LogOut", "Account", new { area = "" });

                        ShowMessage = "alert alert-success";

                        TempData.Clear();

                        Session.Contents.RemoveAll();

                        Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                        Response.AddHeader("Pragma", "no-cache");
                        Response.AddHeader("Expires", "0");

                        EnumMessage = "Perubahan password berhasil, silahkan login kembali";
                    }
                }
                else
                {
                    EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
                    valid = "no";
                }

                //send back to client browser //
                return Json(new
                {
                    view = "",
                    msg = EnumMessage,
                    url = RedirectURL,
                    htmlpool = valid,
                    swltitle = titleswl,
                    swltype = typeswl,
                    swltxtbtn = txtbtnswl,
                    swltxtcnl = txtcnlswl,
                    moderror = IsErrorTimeout,
                    ShowMessagex = ShowMessage,
                    resultedd = resulted,
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

        #region RegistrasiAccountNew

        private string tempTransksi = "Accounttlist";
        private string tempTransksifilter = "Accountlistfilter";
        private string tempcommon = "common";
        private string MainControllerNameHeaderTx = "Account";
        private string MainActionNameHeaderTx = "clnHeaderTx";

        public async Task<ActionResult> AccountRegis()
        {
            ViewBag.caption = "ID Pengguna Baru";
            //set session filterisasi //
            modFilter = TempData["RegisAccFilter"] as cFilterContract;
            Common = (TempData["commonRegis"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            cAccountRegisNw Account = new cAccountRegisNw();
            // if (Common.ddlDevisi == null)
            {
                Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("1", "", "", "", "");
            }
            //if (Common.ddlRegion == null)
            {
                Common.ddlRegion = await Commonddl.dbdbGetDdlRegionListByEncrypt("1", "", "", "", "");
            }
            // if (Common.ddlBranch == null)
            {
                Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", "", "", "", "usrrg", "");
            }

            Account.KodeRegisCabang = HasKeyProtect.Encryption("-1984321");

            ViewData["SelectDevisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);
            ViewData["SelectArea"] = OwinLibrary.Get_SelectListItem(Common.ddlRegion);
            ViewData["SelectCabang"] = OwinLibrary.Get_SelectListItem(Common.ddlBranch);

            TempData["RegisAccFilter"] = modFilter;
            TempData["commonRegis"] = Common;

            return View(Account);
        }

        public async Task<ActionResult> AccountRegisRT()
        {
            ViewBag.caption = "Reset Kata Sandi";
            //set session filterisasi //
            modFilter = TempData["RegisAccFilter"] as cFilterContract;
            Common = (TempData["commonRegis"] as vmCommon);
            Common = Common == null ? new vmCommon() : Common;

            cAccountRegis Account = new cAccountRegis();

            Account.KodeRegisCabang = HasKeyProtect.Encryption("-1984329");

            TempData["RegisAccFilter"] = modFilter;
            TempData["commonRegis"] = Common;

            return View(Account);
        }

        public ActionResult AccountAktivasi()
        {
            cAccountRegis AccountSession = new cAccountRegis();
            AccountSession.KodeRegisCabang = HasKeyProtect.Encryption("-1984321");
            return View(AccountSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AccountRegisAktivasi(cAccountRegis model)
        {
            model.KodeRegisCabang = HasKeyProtect.Decryption(model.KodeRegisCabang);

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

            if (IsErrorTimeout == true && model.KodeRegisCabang != "-1984321")
            {
                string urlpath = "/";
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                IsErrorTimeout = false;
            }

            try
            {
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Account = TempData[tempTransksi] as vmAccount;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //set session filterisasi //
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Account;
                TempData[tempcommon] = Common;

                int resulted = 0;
                string EnumMessage = "";
                string ShowMessage = "";
                string urldierect = "";
                string statusbf = "";
                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    string UserID = "";
                    string module = "";
                    string GroupName = "";
                    string domainname = "";

                    if (model.KodeRegisCabang != "-1984321")
                    {
                        //get result verifikasi user//
                        UserID = Account.AccountLogin.UserID ?? "";
                        module = model.moduleclass;
                        GroupName = Account.AccountLogin.GroupName;
                        domainname = Request.Url.Host;

                        module = HasKeyProtect.Decryption(module);

                        //default message//
                    }
                    else
                    {
                        model.RegAccountNo = HasKeyProtect.Decryption(model.Userkodeverified);
                    }

                    if ((model.jenisprosesfollowup ?? "") == "")
                    {
                        resulted = 4;
                        EnumMessage = "Silahkan Pilih Jenis Proses";
                    }
                    else if (model.DevisiSelect is null)
                    {
                        resulted = 4;
                        EnumMessage = "Silahkan Pilih Tipe Pengguna";
                    }
                    else
                    {
                        DataRow dr = Account.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == model.keylookupdataHTX).SingleOrDefault();
                        model.RegAccountNo = dr["RegAccountNo"].ToString();
                        model.email = dr["Email"].ToString();
                        statusbf = dr["RegAccountStatus"].ToString();

                        if ((model.jenisprosesfollowup ?? "") == "1" && statusbf != "0" && statusbf != "3" && statusbf != "5" && statusbf != "7")
                        {
                            EnumMessage = "Status Registrasi Pengguna harus 'Waiting'";
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "2" && statusbf != "0")
                        {
                            EnumMessage = "Status Registrasi Pengguna harus 'Waiting'";
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "3" && statusbf != "1")
                        {
                            EnumMessage = "Status Registrasi Pengguna harus 'Activated'";
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "4" && statusbf != "2")
                        {
                            EnumMessage = "Status Registrasi Pengguna harus 'Rejected'";
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "0")
                        {
                            EnumMessage = "Pilih jenis proses selaian 'Waiting''";
                        }

                        if (EnumMessage == "")
                        {
                            if (model.DevisiSelect.Length > 0)
                            {
                                model.Devisi = model.DevisiSelect[0].ToString();
                            }
                            else
                            {
                                model.Devisi = "";
                            }

                            DataTable dt = await Accountddl.dbSaveRegAccountAct(model, statusbf, module, UserID, GroupName);
                            resulted = int.Parse(dt.Rows[0][0].ToString());

                            if (model.KodeRegisCabang == "-1984321")
                            {
                                urldierect = "";
                                if (resulted == 1 || resulted == 88887)
                                {
                                    ShowMessage = "alert alert-success";
                                }
                                else
                                {
                                    ShowMessage = "alert alert-danger";
                                }
                            }
                            EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
                        }
                    }

                    if (resulted == 1 || resulted == 88887)
                    {
                        resulted = 1;
                        if ((model.jenisprosesfollowup ?? "") == "1")
                        {
                            if (statusbf == "3")
                            {
                                EnumMessage = "ID Pengguna Berhasil diaktifkan kembali";
                            }
                            else
                            {
                                EnumMessage = "ID Pengguna Berhasil diaktivasi";
                            }
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "2")
                        {
                            EnumMessage = "ID Pengguna Berhasil direjected";
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "3")
                        {
                            EnumMessage = "ID Pengguna Berhasil di Non-Aktifkan";
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "4")
                        {
                            EnumMessage = "ID Pengguna Berhasil diremove";
                        }
                        else if ((model.jenisprosesfollowup ?? "") == "8")
                        {
                            EnumMessage = "ID Pengguna Berhasil diLogOut";
                        }
                        else
                        {
                            EnumMessage = "ID Pengguna Berhasil direjected, ID Pengguna Sudah tidak dapat digunakan";
                        }
                    }
                }
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    url = urldierect,
                    probitusertxt = resulted,
                    tokenpubtxt = "",
                    msg = EnumMessage,
                    idval = model.keylookupdataHTX,
                    ShowMessagex = ShowMessage
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                string urlpath = "/";
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

        public async Task<ActionResult> AccountRegisFU(string module, string paramkey, string oprmn)
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
                string moduleid = modFilter.ModuleID;

                ViewBag.caption = "ID Pengguna Baru";

                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Account = TempData[tempTransksi] as vmAccount;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //set session filterisasi //
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Account;
                TempData[tempcommon] = Common;

                //if (Common.ddlStatusRegUsr == null)
                //{
                Common.ddlStatusRegUsr = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATRGUSR", moduleid, UserID, GroupName);
                //}
                if (Common.ddlDevisi == null)
                {
                    Common.ddlDevisi = await Commonddl.dbdbGetDdlDevisiListByEncrypt("1", "", "", "", "");
                }
                ViewData["SelectDevisi"] = OwinLibrary.Get_SelectListItem(Common.ddlDevisi);

                DataRow dr = Account.DTAllTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
                cAccountRegis FollowReg = new cAccountRegis();
                string msgnotfound = "";
                string statusbf = "0";
                string tipeee = "";
                if (dr != null)
                {
                    FollowReg.keylookupdataDTX = paramkey;
                    FollowReg.moduleclass = module;
                    FollowReg.IDHeaderTx = int.Parse(dr["Id"].ToString());
                    FollowReg.RegAccountNo = dr["RegAccountNo"].ToString();
                    FollowReg.RegAccountDate = dr["RegAccountDate"].ToString();
                    FollowReg.Devisi = dr["Divisi_id"].ToString();
                    FollowReg.Area = dr["Region_Name"].ToString();
                    FollowReg.Cabang = dr["brch_name"].ToString();
                    FollowReg.email = dr["Email"].ToString();
                    FollowReg.StatusAktif = dr["StatusAktif"].ToString();
                    FollowReg.NotesFollow = ""; //dr["notes"].ToString();
                    FollowReg.DevisiNama = dr["Divisi_Name"].ToString();
                    statusbf = dr["RegAccountStatus"].ToString();
                    tipeee = dr["usertpeee"].ToString();
                }

                /*waiting*/
                if (statusbf == "0")
                {
                    Common.ddlStatusRegUsr = Common.ddlStatusRegUsr.Where(x => x.Value == "1" || x.Value == "2");
                }

                if (statusbf == "1")
                {
                    Common.ddlStatusRegUsr = Common.ddlStatusRegUsr.Where(x => x.Value == "3" || x.Value == "8");
                }

                if (statusbf == "2")
                {
                    Common.ddlStatusRegUsr = Common.ddlStatusRegUsr.Where(x => x.Value == "4");
                }

                if (statusbf == "3")
                {
                    Common.ddlStatusRegUsr = Common.ddlStatusRegUsr.Where(x => x.Value == "1");
                }

                if (statusbf == "5")
                {
                    Common.ddlStatusRegUsr = Common.ddlStatusRegUsr.Where(x => x.Value == "1" || x.Value == "2");
                }

                if (statusbf == "7")
                {
                    Common.ddlStatusRegUsr = Common.ddlStatusRegUsr.Where(x => x.Value == "1");
                }

                ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusRegUsr);
                ViewBag.viewfrm = oprmn;
                ViewBag.usertpeee = tipeee;

                return Json(new
                {
                    msg = msgnotfound,
                    moderror = false,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/AccountRegisAktivasi.cshtml", FollowReg),
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
        public async Task<ActionResult> clnAccountChucgongProNew(cAccountRegisNw model)
        {
            await Task.Delay(0);
            string EnumMessage = "";
            string ShowMessage = "";
            int resulted = -9999;

            try
            {
                string kodereg = HashNetFramework.HasKeyProtect.Decryption(model.KodeRegisCabang);
                //reset password
                if (kodereg == "-1984329")
                {
                    ModelState["FullName"].Errors.Clear();
                    ModelState["PasswordChange"].Errors.Clear();
                    ModelState["RetypePassword"].Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    OwinLibrary.CreateLog("valid", "LogErrorFDCM.txt");

                    //get result verifikasi user//
                    // string UserID = model.UserID;
                    string Mailed = model.email;

                    //get user identity host//
                    string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                    string ipAddress2 = Request.UserHostAddress;
                    string HostPCName = Dns.GetHostName();
                    string domainname = Request.Url.Host;
                    //default message//

                    EnumMessage = "";
                    ShowMessage = "alert alert-danger";
                    string valid = "";

                    // prepare for OTP code //
                    cOTP ResultOTP = new cOTP();
                    //  string inputOtpCode = "";

                    //try cek additional validation password //
                    string pascodenew = model.PasswordChange ?? "";
                    string pascodenewretype = model.RetypePassword;

                    if (kodereg != "-1984329")
                    {
                        resulted = OwinLibrary.CheckValidationPassWord(pascodenew);

                        //cek user login dengan user form harus sama//
                        if (pascodenew != pascodenewretype)
                        {
                            resulted = (int)ProccessOutput.KataSandiNotMathRetype;
                            valid = "no";
                        }

                        int needverified = 0;
                        if (resulted == 1)
                        {
                            model.FlagOperation = "CRETHDR";
                            //model.email = "muhammad.hafid477@gmail.com";
                            model.KodeRegisCabang = HasKeyProtect.Decryption(model.KodeRegisCabang);

                            DataTable dt = await Accountddl.dbSaveRegAccountNw(model, "WFTODOREGACT", "", "");

                            if (dt.Rows.Count > 0)
                            {
                                resulted = int.Parse(dt.Rows[0][0].ToString());
                                //jika berhasil dan email sudah didaftarkan
                                if (resulted == 1 || resulted == (int)ProccessOutput.RegisKeyDuplicate)
                                {
                                    ShowMessage = "alert alert-success";

                                    string resultedNomor = (dt.Rows[0][1].ToString());
                                    resultedNomor = HasKeyProtect.Encryption(resultedNomor);
                                    string resultedmail = (dt.Rows[0][2].ToString());
                                    needverified = int.Parse(dt.Rows[0][3].ToString());
                                    ////jika account baru belom diverifikasi maka kirim email saja
                                    //if (needverified == 0)
                                    //{
                                    //    int resultsendemail = await MessageEmail.sendEmail((int)EmailType.useraktivasi, resultedmail, resultedNomor, "", "PT SMS").ConfigureAwait(false); //sendEmail()
                                    //}
                                }
                            }
                            else
                            {
                                resulted = -1;
                            }
                        }
                        EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);

                        // jika email duplicate tapi belom verifikasi dianggap berhasil
                        if ((resulted == (int)ProccessOutput.RegisKeyDuplicate) || (resulted == 1))
                        {
                            if (resulted == (int)ProccessOutput.RegisKeyDuplicate)
                            {
                                ShowMessage = "alert alert-danger";
                                EnumMessage = "ID Pengguna Sudah Terdaftar";
                            }
                            else
                            {
                                ShowMessage = "alert alert-success";
                                resulted = 1;
                                EnumMessage = "ID Pengguna Berhasil didaftarakan, admin kami akan mengkonfirmasi dan mengaktivasi user anda. " +
                                               "Untuk dapat menggunakan ID Pengguna Cek email anda setelah aktivasi oleh admin kami";
                            }
                        }
                    }
                    else //reset password
                    {
                        model.KodeRegisCabang = kodereg;
                        DataTable dt = await Accountddl.dbSaveRegRTAccount(model, "WFTODOREGACT", "", "");
                        if (dt.Rows.Count > 0)
                        {
                            resulted = int.Parse(dt.Rows[0][0].ToString());
                            //jika berhasil dan email sudah didaftarkan
                            if (resulted == 1)
                            {
                                ShowMessage = "alert alert-success";
                                EnumMessage = "Permohonan Reset Kata Sandi Berhasil , admin kami akan mengkonfirmasi anda. " +
                                                  "Untuk dapat menggunakan ID Pengguna Cek email anda setelah konfirmasi oleh admin ";
                            }
                            else
                            {
                                ShowMessage = "alert alert-danger";
                                EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)resulted);
                            }
                        }
                    }
                }
                else
                {
                    EnumMessage = string.Join(" , ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
                return Json(new
                {
                    url = "",
                    probitusertxt = resulted,
                    tokenpubtxt = "",
                    MessageNotValidx = EnumMessage,
                    ShowMessagex = ShowMessage
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 406;
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                });
            }
        }

        public async Task<ActionResult> clnGetBranchByRegion(string clientid, string regionid = "", string reg = "")
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
            if (IsErrorTimeout == true && reg == "")
            {
                string urlpath = Url.Action("AccountTimeOut", "Account", new { area = "" });
                return Json(new
                {
                    url = urlpath,
                    moderror = IsErrorTimeout
                }, JsonRequestBehavior.AllowGet);
            }

            if (reg != "")
            {
                IsErrorTimeout = false;
            }

            try
            {
                //set session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Account = TempData[tempTransksi] as vmAccount;
                Common = TempData[tempcommon] as vmCommon;
                Common = Common == null ? new vmCommon() : Common;

                IEnumerable<cListSelected> tempbrach = (TempData["tempbrach" + clientid + regionid] as IEnumerable<cListSelected>);

                //jika klien yang dipilih berbeda maka ambil cabang nya lagi//
                bool loaddata = false;

                string GroupName = "reg";
                string UserID = "";
                string SelectRegion = "";
                string SelectArea = "";
                string SelectBranch = "";

                if (reg == "")
                {
                    UserID = Account.AccountLogin.UserID ?? "";
                    //set field filter to varibale //
                    SelectRegion = modFilter.SelectRegion ?? modFilter.RegionLogin;
                    SelectArea = modFilter.SelectArea ?? modFilter.RegionLogin;
                    SelectBranch = modFilter.SelectBranch ?? modFilter.BranchLogin;
                }

                if ((SelectArea != regionid))
                {
                    SelectRegion = regionid;
                    SelectArea = regionid;

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
                        //string decSelectArea = (SelectArea ?? "") == "" ? "" : HasKeyProtect.Decryption(SelectArea);
                        //string decSelectRegion = (SelectRegion ?? "") == "" ? "" : HasKeyProtect.Decryption(SelectRegion);
                        string decSelectArea = SelectArea;
                        string decSelectBranch = "";
                        Common.ddlBranch = await Commonddl.dbdbGetDdlBranchListByEncrypt("", decSelectBranch, decSelectArea, "", "usrrg", GroupName);
                        tempbrach = Common.ddlBranch;
                    }
                }

                TempData["tempbrach" + clientid + regionid] = tempbrach;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempTransksi] = Account;
                TempData[tempcommon] = Common;

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

        #endregion RegistrasiAccountNew

        #region Transaksi ID Pengguna

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

                string NoRequest = modFilter.NoPengajuanRequest ?? "";
                string SelectBranch = modFilter.SelectBranch ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectArea = modFilter.SelectArea ?? "";
                string Email = modFilter.MailerDaemoon ?? "";
                string SelectContractStatus = modFilter.SelectContractStatus ?? "";

                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                // try show filter data//
                List<String> recordPage = await Accountddl.dbGetHeaderTxListCount(NoRequest, Email, SelectDivisi, SelectBranch, SelectArea, SelectContractStatus, PageNumber, caption, UserID, GroupName);
                TotalRecord = Convert.ToDouble(recordPage[0]);
                TotalPage = Convert.ToDouble(recordPage[1]);
                pagingsizeclient = Convert.ToDouble(recordPage[2]);
                pagenumberclient = PageNumber;
                List<DataTable> dtlist = await Accountddl.dbGetHeaderTxList(null, NoRequest, Email, SelectDivisi, SelectBranch, SelectArea, SelectContractStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                totalRecordclient = dtlist[0].Rows.Count;
                totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                //set in filter for paging//
                modFilter.TotalRecord = TotalRecord;
                modFilter.TotalPage = TotalPage;
                modFilter.pagingsizeclient = pagingsizeclient;
                modFilter.totalRecordclient = totalRecordclient;
                modFilter.totalPageclient = totalPageclient;
                modFilter.pagenumberclient = pagenumberclient;

                modFilter.ModuleID = idcaption;
                modFilter.idcaption = idcaption;
                modFilter.UserTypes = UserTypes;
                modFilter.ModuleName = caption;

                //set to object pendataran//
                Account.DTAllTx = dtlist[0];
                Account.DTHeaderTx = dtlist[1];
                Account.FilterTransaksi = modFilter;
                Account.Permission = PermisionModule;

                //set session filterisasi //
                TempData[tempTransksi] = Account;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = new vmCommon();

                //set caption view//
                ViewBag.menu = menu;
                ViewBag.caption = caption;
                ViewBag.captiondesc = menuitemdescription;
                ViewBag.rute = MainControllerNameHeaderTx;
                ViewBag.action = MainActionNameHeaderTx;

                ViewBag.menuipt = menu;
                ViewBag.captionipt = caption.Replace("LST", "");
                ViewBag.captiondescipt = menuitemdescription;
                //ViewBag.ruteipt = MainControllerNameDetailTx;
                //ViewBag.actionipt = MainActionNameDetailTx;

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/AccountRegisLst.cshtml", Account),
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
        public async Task<ActionResult> clnTeamVery(String menu, String caption)
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

                string NoRequest = modFilter.NoPengajuanRequest ?? "";
                string SelectBranch = modFilter.SelectBranch ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectArea = modFilter.SelectArea ?? "";
                string Email = modFilter.MailerDaemoon ?? "";
                string SelectContractStatus = modFilter.SelectContractStatus ?? "";

                // set default for paging//
                int PageNumber = 1;
                double TotalRecord = 0;
                double TotalPage = 0;
                double pagingsizeclient = 0;
                double pagenumberclient = 0;
                double totalRecordclient = 0;
                double totalPageclient = 0;

                DataTable dt = await Accountddl.dbGetteamvery(caption, UserID, GroupName);

                ////set in filter for paging//
                //modFilter.TotalRecord = TotalRecord;
                //modFilter.TotalPage = TotalPage;
                //modFilter.pagingsizeclient = pagingsizeclient;
                //modFilter.totalRecordclient = totalRecordclient;
                //modFilter.totalPageclient = totalPageclient;
                //modFilter.pagenumberclient = pagenumberclient;

                //modFilter.ModuleID = idcaption;
                //modFilter.idcaption = idcaption;
                //modFilter.UserTypes = UserTypes;
                //modFilter.ModuleName = caption;

                ////set to object pendataran//
                //Account.DTAllTx = dtlist[0];
                //Account.DTHeaderTx = dtlist[1];
                //Account.FilterTransaksi = modFilter;
                //Account.Permission = PermisionModule;

                ////set session filterisasi //
                //TempData[tempTransksi] = Account;
                //TempData[tempTransksifilter] = modFilter;
                //TempData[tempcommon] = new vmCommon();

                ////set caption view//
                //ViewBag.menu = menu;
                //ViewBag.caption = caption;
                //ViewBag.captiondesc = menuitemdescription;
                //ViewBag.rute = MainControllerNameHeaderTx;
                //ViewBag.action = MainActionNameHeaderTx;

                //ViewBag.menuipt = menu;
                //ViewBag.captionipt = caption.Replace("LST", "");
                //ViewBag.captiondescipt = menuitemdescription;
                //ViewBag.ruteipt = MainControllerNameDetailTx;
                //ViewBag.actionipt = MainActionNameDetailTx;

                ViewBag.Total = "Total Data : " + TotalRecord.ToString();

                //send back to client browser//
                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/TeamVery.cshtml", Account),
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
        public async Task<ActionResult> clnHeaderSve(cAccountRegis model)
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
                Account = TempData[tempTransksi] as vmAccount;
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.ModuleID;

                TempData[tempTransksi] = Account;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                //get value from aply filter //
                string NoRequest = modFilter.NoPengajuanRequest ?? "";
                string SelectBranch = modFilter.SelectBranch ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectArea = modFilter.SelectArea ?? "";
                string Email = modFilter.MailerDaemoon ?? "";
                string SelectContractStatus = modFilter.SelectContractStatus ?? "";

                string keylookupdataDTX = model.keylookupdataDTX;
                string keylookupdataHTX = model.keylookupdataHTX;
                string FlagOpr = model.FlagOperation ?? "";

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
                if (FlagOpr.IndexOf("_EDIT") > 0)
                {
                    model.FlagOperation = "";
                    FlagOpr = "";
                    IsEditItem = true;
                }

                FlagOpr = model.FlagOperation ?? "";
                model.IDHeaderTx = Account.HeaderInfo.IDHeaderTx;
                model.IDDetailTx = IsEditItem == false ? Account.HeaderInfo.IDDetailTx : int.Parse(model.keylookupdataDTX);
                model.RegAccountNo = Account.HeaderInfo.RegAccountNo;
                model.RegAccountCreateBy = Account.HeaderInfo.RegAccountCreateBy;
                model.StatusFollow = model.StatusFollow ?? Account.HeaderInfo.RegAccountStatus;
                NoRequest = model.RegAccountNo;

                model.KodeRegisCabang = "";
                DataTable dt = await Accountddl.dbSaveRegAccount(model, caption, UserID, GroupName);
                int result = int.Parse(dt.Rows[0][0].ToString());
                string notransaksi = dt.Rows[0][1].ToString();

                string EnumMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)result);
                string msg1 = (FlagOpr ?? "") == "" ? "Penambahan barang" : (FlagOpr ?? "") == "CRETHDR" ? "Pengajuan " : "Pengajuan  No.Transaksi #" + notransaksi;
                string msg2 = (FlagOpr ?? "") == "" ? "disimpan" : (FlagOpr ?? "") == "CRETHDR" ? "diproses dengan No.Transaksi #" + notransaksi : "diproses";
                EnumMessage = (result == 1 || result == 86) ? String.Format(EnumMessage, msg1, msg2) : EnumMessage;

                if (FlagOpr == "" || FlagOpr == "CRETHDR")
                {
                    if ((result == 1))
                    {
                        ////get total data from server//
                        List<String> recordPage = await Accountddl.dbGetHeaderTxListCount(NoRequest, Email, SelectDivisi, SelectBranch, SelectArea, SelectContractStatus, PageNumber, caption, UserID, GroupName);
                        TotalRecord = Convert.ToDouble(recordPage[0]);
                        TotalPage = Convert.ToDouble(recordPage[1]);
                        pagingsizeclient = Convert.ToDouble(recordPage[2]);
                        pagenumberclient = PageNumber;

                        //set paging in grid client//
                        List<DataTable> dtlist = await Accountddl.dbGetHeaderTxList(null, NoRequest, Email, SelectDivisi, SelectBranch, SelectArea, SelectContractStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                        totalRecordclient = dtlist[0].Rows.Count;
                        totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

                        //back to set in filter//
                        modFilter.TotalRecord = TotalRecord;
                        modFilter.TotalPage = TotalPage;
                        modFilter.pagingsizeclient = pagingsizeclient;
                        modFilter.totalRecordclient = totalRecordclient;
                        modFilter.totalPageclient = totalPageclient;
                        modFilter.pagenumberclient = pagenumberclient;

                        Account.DTHeaderTx = dtlist[0];
                        Account.DTDetailTx = dtlist[1];
                        Account.FilterTransaksi = modFilter;
                    }

                    TempData[tempTransksi] = Account;
                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempcommon] = Common;

                    string filteron = isModeFilter == false ? "" : "<br /> Pencarian Data :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + " Data <br /> Data on Pages : " + totalRecordclient.ToString() + " Data" + filteron;

                    view1 = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Forecast/_uiGridForecastCreate.cshtml", Account);
                }
                else
                {
                    vmHome Home = new vmHome();
                    Home.TodoUser = await Commonddl.dbGetApprovalTodo("1", caption, "", Account.AccountLogin.UserID, Account.AccountLogin.GroupName);
                    view1 = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Home/_HomeTodoUser.cshtml", Home);
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
                //get session filterisasi //
                modFilter = TempData[tempTransksifilter] as cFilterContract;
                Account = TempData[tempTransksi] as vmAccount;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                // get value filter have been filter//
                string NoRequest = modFilter.NoPengajuanRequest ?? "";
                string SelectBranch = modFilter.SelectBranch ?? "";
                string SelectDivisi = modFilter.SelectDivisi ?? "";
                string SelectArea = modFilter.SelectArea ?? "";
                string Email = modFilter.MailerDaemoon ?? "";
                string SelectContractStatus = modFilter.SelectContractStatus ?? "";

                // set & get for next paging //
                int pagenumberclient = paged;
                int PageNumber = modFilter.PageNumber;
                double pagingsizeclient = modFilter.pagingsizeclient;
                double TotalRecord = modFilter.TotalRecord;
                double totalRecordclient = modFilter.totalRecordclient;

                //descript some value for db//
                //SelectClient = HasKeyProtect.Decryption(SelectClient);
                //SelectBranch = HasKeyProtect.Decryption(SelectBranch);
                caption = HasKeyProtect.Decryption(caption);

                //select page
                List<DataTable> dtlist = await Accountddl.dbGetHeaderTxList(null, NoRequest, Email, SelectDivisi, SelectBranch, SelectArea, SelectContractStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
                // update active paging back to filter //
                modFilter.pagenumberclient = pagenumberclient;

                // set pendataran //
                Account.DTAllTx = dtlist[0];
                Account.DTHeaderTx = dtlist[1];

                bool isModeFilter = modFilter.isModeFilter;
                string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                //set session filterisasi //
                TempData[tempTransksi] = Account;
                TempData[tempTransksifilter] = modFilter;
                TempData[tempcommon] = Common;

                return Json(new
                {
                    moderror = IsErrorTimeout,
                    view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/AccountRegisLstGrid.cshtml", Account),
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
        public async Task<ActionResult> clnHeaderTxRegisFilter(cFilterContract model, string download)
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
                Account = TempData[tempTransksi] as vmAccount;
                Common = (TempData[tempcommon] as vmCommon);
                Common = Common == null ? new vmCommon() : Common;

                //get value from old define//
                string UserID = Account.AccountLogin.UserID;
                string GroupName = Account.AccountLogin.GroupName;
                string caption = modFilter.idcaption;

                //get value from aply filter //
                string NoRequest = model.RequestNo ?? "";
                string SelectBranch = model.SelectBranch ?? "";
                string SelectDivisi = model.SelectDivisi ?? "";
                string SelectArea = model.SelectArea ?? "";
                string Email = model.MailerDaemoon ?? "";
                string SelectContractStatus = model.SelectContractStatus ?? "";

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
                modFilter.NoPengajuanRequest = NoRequest;
                modFilter.SelectBranch = SelectBranch;
                modFilter.SelectDivisi = SelectDivisi;
                modFilter.SelectArea = SelectArea;
                modFilter.MailerDaemoon = Email;
                modFilter.SelectContractStatus = SelectContractStatus;
                modFilter.PageNumber = PageNumber;
                modFilter.isModeFilter = isModeFilter;
                //set filter//

                string validtxt = ""; //lgakta.CheckFilterisasiDataGen(modFilter);
                if (validtxt == "")
                {
                    //descript some value for db//
                    //SelectClient = SelectClient; //HasKeyProtect.Decryption(SelectClient);
                    //SelectBranch = SelectBranch; //HasKeyProtect.Decryption(SelectBranch);
                    //SelectDivisi = SelectDivisi; //HasKeyProtect.Decryption(SelectNotaris);
                    caption = HasKeyProtect.Decryption(caption);

                    // try show filter data//
                    List<String> recordPage = await Accountddl.dbGetHeaderTxListCount(NoRequest, Email, SelectDivisi, SelectBranch, SelectArea, SelectContractStatus, PageNumber, caption, UserID, GroupName);
                    TotalRecord = Convert.ToDouble(recordPage[0]);
                    TotalPage = Convert.ToDouble(recordPage[1]);
                    pagingsizeclient = Convert.ToDouble(recordPage[2]);
                    pagenumberclient = PageNumber;
                    List<DataTable> dtlist = await Accountddl.dbGetHeaderTxList(null, NoRequest, Email, SelectDivisi, SelectBranch, SelectArea, SelectContractStatus, PageNumber, pagenumberclient, pagingsizeclient, caption, UserID, GroupName);
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
                    Account.DTAllTx = dtlist[0];
                    Account.DTHeaderTx = dtlist[1];
                    Account.FilterTransaksi = modFilter;

                    //keep session filterisasi before//
                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = Account;
                    TempData[tempcommon] = Common;

                    string filteron = isModeFilter == false ? "" : ", Pencarian :  Aktif";
                    ViewBag.Total = "Total Data : " + TotalRecord.ToString() + filteron;

                    return Json(new
                    {
                        moderror = IsErrorTimeout,
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/AccountRegisLstGrid.cshtml", Account),
                        download = "",
                        message = validtxt
                    });
                }
                else
                {
                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = Account;
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
                Account = TempData[tempTransksi] as vmAccount;
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
                    TempData[tempTransksi] = Account;
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
                    if (Common.ddlStatusRegUsr == null)
                    {
                        Common.ddlStatusRegUsr = await Commonddl.dbdbGetDdlEnumsListByEncrypt("STATRGUSR", moduleid, UserID, GroupName);
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
                    ViewData["SelectStatus"] = OwinLibrary.Get_SelectListItem(Common.ddlStatusRegUsr);

                    TempData[tempTransksifilter] = modFilter;
                    TempData[tempTransksi] = Account;
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
                        view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/AccountRegisFilter.cshtml", modFilter),
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

        #endregion Transaksi ID Pengguna

        //[HttpPost]
        //public async Task<ActionResult> clnHeaderTxView(string module, string curmodule, string paramkey)
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

        //        modFilter = (TempData[tempTransksifilter] as cFilterContract) == null ? new cFilterContract() : modFilter;
        //        Account = (TempData[tempTransksi] as vmAccount) == null ? Account : Account;
        //        Common = (TempData[tempcommon] as vmCommon);
        //        Common = Common == null ? new vmCommon() : Common;

        //        if (Common.ddlFollow == null)
        //        {
        //            Common.ddlFollow = await Commonddl.dbgetDdlparamenumsList("FOLLOW");
        //        }
        //        Common.ddlFollow = Common.ddlFollow.Where(x => x.Text != "Revise");

        //        ViewData["SelectFollow"] = OwinLibrary.Get_SelectListItem(Common.ddlFollow);

        //        string returnview = "";
        //        string UserID = Account.AccountLogin.UserID;
        //        string GroupName = Account.AccountLogin.GroupName;
        //        string caption = HasKeyProtect.Decryption(module);
        //        if ((module ?? "") == "")
        //        {
        //            caption = HasKeyProtect.Decryption(modFilter.ModuleID);
        //        }

        //        module = caption;
        //        string RegquestNo = "";

        //        // set default for paging//
        //        int PageNumber = 1;
        //        double TotalRecord = 0;
        //        double TotalPage = 0;
        //        double pagingsizeclient = 0;
        //        double pagenumberclient = 0;
        //        double totalRecordclient = 0;
        //        double totalPageclient = 0;

        //        //set header//
        //        DataRow dr;
        //        Account = (Account == null) ? new vmAccount() : Account;
        //        modFilter = (modFilter == null) ? new cFilterContract() : modFilter;
        //        Account.HeaderInfo = new cAccountRegis();

        //        cAccountMetrik PermisionModule = Account.AccountMetrikList.Where(x => x.MenuItem.ModuleID == caption).SingleOrDefault();
        //        if (module.Contains("LST"))
        //        {
        //            dr = Account.DTHeaderTx.AsEnumerable().Where(x => x.Field<string>("keylookupdata") == paramkey).SingleOrDefault();
        //        }
        //        else
        //        {
        //            //set caption view//
        //            ViewBag.menu = "";
        //            ViewBag.caption = caption;
        //            ViewBag.captiondesc = "";
        //            ViewBag.rute = MainControllerNameHeaderTx;
        //            ViewBag.action = MainActionNameHeaderTx;

        //            List<DataTable> dtlistpop = await Accountddl.dbGetHeaderTxList(null, paramkey, "", "", "", 1, 1, 1, caption, UserID, GroupName);
        //            dr = dtlistpop[0].AsEnumerable().Where(x => x.Field<string>("RegAccountNo") == paramkey).SingleOrDefault();

        //            //set caption view//
        //            caption = caption.Replace("LST", "");
        //            MainControllerNameHeaderTx = "Home";
        //            MainActionNameHeaderTx = "clnHomeTodo";
        //            Account.DTHeaderTx = dtlistpop[0];
        //        }

        //        if (dr != null)
        //        {
        //            RegquestNo = dr["RegAccountNo"].ToString();
        //            Account.HeaderInfo.IDHeaderTx = int.Parse(dr["Id"].ToString());
        //            Account.HeaderInfo.IDDetailTx = 0;
        //            Account.HeaderInfo.keylookupdataHTX = dr["keylookupdata"].ToString();
        //            Account.HeaderInfo.RegAccountNo = dr["RegAccountNo"].ToString();
        //            Account.HeaderInfo.RegAccountDate = DateTime.Parse(dr["RegAccountDate"].ToString()).ToString("dd/MMM/yyyy");
        //            Account.HeaderInfo.Devisi = dr["Divisi_Name"].ToString();
        //            Account.HeaderInfo.Area = dr["Area"].ToString();
        //            Account.HeaderInfo.Cabang = dr["Region_Name"].ToString();
        //            Account.HeaderInfo.FullName = dr["FullName"].ToString();
        //            Account.HeaderInfo.email = dr["Email"].ToString();
        //            Account.HeaderInfo.RegAccountStatus = dr["RegAccountStatus"].ToString();
        //            Account.HeaderInfo.StatusDoc = dr["StatusDoc"].ToString();
        //            Account.HeaderInfo.StatusDocDesc = dr["StatusDocDesc"].ToString();
        //            Account.HeaderInfo.StatusAktif = dr["StatusAktif"].ToString();
        //            Account.HeaderInfo.RegAccountCreateBy = dr["CreatedBy"].ToString();
        //            Account.HeaderInfo.StatusFollow = dr["RegAccountStatus"].ToString();

        //            // try show filter log approval//
        //            PageNumber = 1;
        //            TotalRecord = 0;
        //            TotalPage = 0;
        //            pagingsizeclient = 0;
        //            pagenumberclient = 0;
        //            totalRecordclient = 0;
        //            totalPageclient = 0;

        //            ViewBag.CurModule = caption;
        //            string captionwf = "WFTODOREGACT";
        //            Account.HeaderInfo.IsPICApproval = await Commonddl.dbGetApprovalCheck(RegquestNo, captionwf, UserID, GroupName);
        //            List<String> recordPage = await Commonddl.dbGetApprovalLogListCount(RegquestNo, PageNumber, captionwf, UserID, GroupName);
        //            TotalRecord = Convert.ToDouble(recordPage[0]);
        //            TotalPage = Convert.ToDouble(recordPage[1]);
        //            pagingsizeclient = Convert.ToDouble(recordPage[2]);
        //            pagenumberclient = PageNumber;
        //            List<DataTable> dtlist = await Commonddl.dbGetApprovalLogList(null, RegquestNo, PageNumber, pagenumberclient, pagingsizeclient, captionwf, UserID, GroupName);
        //            totalRecordclient = dtlist[0].Rows.Count;
        //            totalPageclient = int.Parse(Math.Ceiling(decimal.Parse(totalRecordclient.ToString()) / decimal.Parse(pagingsizeclient.ToString())).ToString());

        //            Account.DTLogTx = dtlist[1];
        //            Account.Permission = PermisionModule;

        //            //set in filter for paging//
        //            modFilter.TotalRecordLog = TotalRecord;
        //            modFilter.TotalPageLog = TotalPage;
        //            modFilter.pagingsizeclientLog = pagingsizeclient;
        //            modFilter.totalRecordclientLog = totalRecordclient;
        //            modFilter.totalPageclientLog = totalPageclient;
        //            modFilter.pagenumberclientLog = pagenumberclient;

        //            modFilter.ModuleID = caption;
        //            Account.FilterTransaksi = modFilter;

        //            //set caption view//
        //            ViewBag.menu = "";
        //            ViewBag.caption = caption;
        //            ViewBag.captiondesc = "";
        //            ViewBag.rute = MainControllerNameHeaderTx;
        //            ViewBag.action = MainActionNameHeaderTx;

        //            // senback to client browser//
        //            returnview = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/WFAccountRegisView.cshtml", Account);

        //            TempData[tempTransksi] = Account;
        //            TempData[tempTransksifilter] = modFilter;
        //            TempData[tempcommon] = Common;

        //        }
        //        else
        //        {
        //            returnview = "";
        //        }

        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            loadcabang = 0,
        //            keydata = "",
        //            view = returnview
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

        //public ActionResult clnAccountChuc()
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
        //        ViewBag.caption = "Perubahan Kata Sandi";
        //        Session["Account"] = Account;
        //        return Json(new
        //        {
        //            moderror = IsErrorTimeout,
        //            view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Account/AccountChuc.cshtml", Account),
        //            msg = ""
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 406;
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        return Json(new
        //        {
        //            IsErrorTimeout = true,
        //            url = Url.Action("Index", "Error", new { area = "" }),
        //        });
        //    }

        //}

        ////public ActionResult AccountChangeForce()
        ////{
        ////    Account = (vmAccount)Session["Account"];
        ////    Account = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account);

        ////    if (Account.UserLogin.RouteName != "")
        ////    {
        ////        return RedirectToRoute(Account.UserLogin.RouteName);
        ////    }

        ////    ViewBag.caption = "Perubahan Kata Sandi";
        ////    Account.ForcePass = true;
        ////    Session["Account"] = Account;
        ////    return View(Account);

        ////}

        //public ActionResult AccountChucgrp()
        //{
        //    Account = (vmAccount)Session["Account"];
        //    Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

        //    if (Account.AccountLogin.RouteName != "")
        //    {
        //        return RedirectToRoute(Account.AccountLogin.RouteName);
        //    }

        //    Common.ddlGrupAkses = Commonddl.dbGetDdlgrupListByEncrypt(Account.AccountGroupUserList);
        //    ViewData["SelectGrupAkses"] = OwinLibrary.Get_SelectListItem(Common.ddlGrupAkses);

        //    ViewBag.caption = "Pemilihan Grup Akses";
        //    //Account.ForcePass = true;
        //    Session["Account"] = Account;
        //    return View(Account);

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> clnAccountChucgongPro(string SelectGrupAkses)
        //{
        //    Account = (vmAccount)Session["Account"];
        //    Account.AccountLogin = lgAccount.NotExistSesionID(Request.Cookies[FormsAuthentication.FormsCookieName], Account.AccountLogin);

        //    if (Account.AccountLogin.RouteName != "")
        //    {
        //        return RedirectToRoute(Account.AccountLogin.RouteName);
        //    }

        //    try
        //    {
        //        bool valid = false;
        //        string UserID = Account.AccountLogin.UserID;

        //        SelectGrupAkses = HasKeyProtect.Decryption(SelectGrupAkses);
        //        cAccountGroupUser grupuser = Account.AccountGroupUserList.Where(x => x.GroupName == SelectGrupAkses && x.UserGrup.ToLower() == UserID.ToLower()).SingleOrDefault();

        //        if (grupuser != null)
        //        {
        //            valid = true;
        //            string groupselected = grupuser.GroupName;
        //            Account.AccountMetrikList = await Accountddl.dbaccountmatriklist(false, groupselected, "");
        //            Account.AccountLogin.GroupName = groupselected;
        //        }

        //        Session["Account"] = Account;
        //        if (Account.AccountMetrikList.Count > 0 && valid == true)
        //        {
        //            return RedirectToRoute("HomePages");
        //        }
        //        else
        //        {
        //            return RedirectToRoute("AccountChucgrp");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message.ToString();
        //        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
        //        //Response.StatusCode = 500;
        //        //Response.TrySkipIisCustomErrors = true;
        //        return RedirectToRoute("ErroPage");
        //        //return Json(new
        //        //{
        //        //    url = Url.Action("Index", "Error", new { area = "" }),
        //        //}, JsonRequestBehavior.AllowGet);
        //    }
        //}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> Index(cAccount model)
        ////{
        ////    string pagesource = "";
        ////    try
        ////    {
        ////       ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ////        using (WebClient webClient = new WebClient())
        ////        {
        ////            webClient.DownloadFile("https://fidusia.ahu.go.id/app/form_cetak_sertifikat_pdf.php?id=2020013116100211","d:\\popp.pdf");

        ////            //System.IO.File.WriteAllBytes("d:\\dodol.pdf",data);

        ////            //using (MemoryStream mem = new MemoryStream(data))
        ////            //{
        ////            //    //using (var yourImage = Image.FromStream(mem))
        ////            //    //{
        ////            //    //    // If you want it as Png
        ////            //    //    yourImage.Save("path_to_your_file.png", ImageFormat.Png);

        ////            //    //    // If you want it as Jpeg
        ////            //    //    yourImage.Save("path_to_your_file.jpg", ImageFormat.Jpeg);
        ////            //    //}
        ////            //}

        ////        }

        ////        ////string URI = "https://fidusia.ahu.go.id/app/nextPageDaftar.php";

        ////        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        ////        //string remoteUri = "https://fidusia.ahu.go.id/app/form_cetak_sertifikat_pdf.php?id=2020013116100211";
        ////        //string fileName = "d:\\ms-banner.pdf", myStringWebResource = null;
        ////        //// Create a new WebClient instance.
        ////        //WebClient myWebClient = new WebClient();
        ////        //// Concatenate the domain with the Web resource filename.
        ////        //myStringWebResource = remoteUri ;
        ////        //// Download the Web resource and save it into the current filesystem folder.
        ////        //myWebClient.DownloadFile(myStringWebResource, fileName);
        ////        //Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
        ////        //Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Application.StartupPath);

        ////        //var serializeModel = JsonConvert.SerializeObject(model);// using Newtonsoft.Json;
        ////        //var response = await client.PostJsonWithModelAsync<ResultDTO>("http://www.website.com/api/create", serializeModel);
        ////        //return response;

        ////        //using (WebClient client = new WebClient())
        ////        //{
        ////        //    System.Collections.Specialized.NameValueCollection postData =
        ////        //       new System.Collections.Specialized.NameValueCollection()
        ////        //       {
        ////        //        { "lastID", "0" },
        ////        //        { "ygdicari", "1" },
        ////        //        { "cari", "2020013116100211"},
        ////        //        { "his", "1"},
        ////        //        { "date", ""}
        ////        //       };
        ////        //    pagesource = Encoding.UTF8.GetString(client.UploadValues(URI, postData));
        ////        //}
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        string po = ex.Message;
        ////    }

        ////    return Json(new
        ////    {
        ////        url = Url.Action(model.Action, model.Controller, new { area = "" }),
        ////        probitusertxt = ViewBag.probituser,
        ////        tokenpubtxt = ViewBag.tokenpub,
        ////        MessageNotValidx = pagesource,
        ////        ShowMessagex = ""
        ////    });
        /// <summary>
        //public ActionResult LogUserIn()
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ////}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogUserIn(cAccount model)
        {
            await Task.Delay(0);
            string EnumMessage = "";
            string ShowMessage = "";

            OwinLibrary.CreateLog("dsads", "LogErrorFDCM.txt");
            string browser = HttpContext.Request.Browser.Browser;

            /* var client = new HttpClient();
             var request = new HttpRequestMessage(HttpMethod.Post, "https://betaapi.mitsuilease.co.id:4200/oauth/v1/auth/accesstoken?GrantType=client_credentials");
             var collection = new List<KeyValuePair<string, string>>();
             collection.Add(new KeyValuePair<string, string>("ClientId", "W7iikLXW+sjIl9ut4Q96sjYHyYSl8viJIftUb+oc99564MDXJ2U="));
             collection.Add(new KeyValuePair<string, string>("ClientSecret", "bpFhvs4oXH6weDhgIdMYp/1ik+pAFI2lVjrGhjyD6Jo/smfCHN4="));
             ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
             var content = new FormUrlEncodedContent(collection);
             request.Content = content;
             var response = await client.SendAsync(request);
             response.EnsureSuccessStatusCode();
             Console.WriteLine(await response.Content.ReadAsStringAsync());*/

            try
            {
                //if (browser.ToLower() != "chrome")  //&& browser.ToLower() != "firefox"
                //{
                //    return RedirectToRoute("UnsupportBrowser");
                //}

                //// incremental delay to prevent brute force attacks
                //int incrementalDelay;
                //if (HttpContext.Application[Request.UserHostAddress] != null)
                //{
                //    // wait for delay if there is one
                //    incrementalDelay = (int)HttpContext.Application[Request.UserHostAddress];
                //    await Task.Delay(incrementalDelay * 1000);
                //}

                if (ModelState.IsValidField("UserPass") && ModelState.IsValidField("UserID"))
                {
                    OwinLibrary.CreateLog("valid", "LogErrorFDCM.txt");

                    model.codepass = model.UserPass;
                    // encrypt password user after input by user//
                    model.UserPass = HasKeyProtect.EncryptionPass(model.UserPass);
                    model.secIDUser = HasKeyProtect.EncryptionPass(model.UserID);

                    //coba verifikasi user id dan password pengguna//
                    model.Browsername = browser;
                    model = await lgAccount.AuthenticateUser(model, "", "");

                    //get result verifikasi user//
                    string UserID = model.UserID;
                    string Mailed = model.Mailed;
                    int PropAccess = model.PropAccess;

                    //get user identity host//
                    string ipAddress = "::1";   //Request.ServerVariables["REMOTE_ADDR"];
                    string ipAddress2 = "::1"; //Request.UserHostAddress;
                    string HostPCName = "SDB-024";  //Dns.GetHostName();
                    string verifiedcodeinputbyuser = model.Userkodeverified ?? "";
                    string templatename = "AccountWrongLogin";
                    string domainname = "localhost";   //Request.Url.Host;

                    //default message//
                    int resulted = -9999;
                    EnumMessage = model.MessageNotValid;
                    ShowMessage = model.ShowMessage;

                    string lmt = OwinLibrary.GetEchoMit();
                    EnumMessage = EnumMessage.Replace("{lmt}", lmt);

                    // prepare for OTP code //
                    cOTP ResultOTP = new cOTP();
                    string inputOtpCode = "";
                    bool ErrorOTP = false;
                    bool isMustInputCode = false;

                    //////pengecekan untuk user yang terkunci selama berkali2 inputkan password salah//
                    //if (PropAccess == (int)ProccessOutput.FilterNotValidWrongPassword)
                    //{
                    //    ResultOTP = await Commonddl.VerifiedOTP(UserID, Mailed, templatename, HostPCName, ipAddress, verifiedcodeinputbyuser, "Request Code For Reset Wrong Login", EnumMessage);
                    //    EnumMessage = ResultOTP.Message;
                    //    resulted = ResultOTP.Result;
                    //    ErrorOTP = ResultOTP.ErrorOTP;
                    //    isMustInputCode = ResultOTP.isMustInputCode;

                    //    // user must input otp //
                    //    if (isMustInputCode == true)
                    //    {
                    //        inputOtpCode = "1";
                    //    }
                    //}

                    //jika user dan kata sandi valid
                    if ((PropAccess == 1))
                    {
                        //get user matrik//
                        Account = await lgAccount.AuthenticateUserGroupMatrik(model);

                        // reset incremental delay on successful login
                        if (HttpContext.Application[Request.UserHostAddress] != null)
                        {
                            HttpContext.Application.Remove(Request.UserHostAddress);
                        }

                        var conf = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
                        var section = (System.Web.Configuration.SessionStateSection)conf.GetSection("system.web/sessionState");
                        string timeout = section.Timeout.TotalMinutes.ToString();

                        FormsAuthenticationTicket tiket = new FormsAuthenticationTicket(1,
                                                            model.UserID,
                                                            DateTime.Now,
                                                            DateTime.Now.AddMinutes(int.Parse(timeout)),
                                                            false,
                                                            string.Empty,
                                                            FormsAuthentication.FormsCookiePath);
                        string encryptedTicket = FormsAuthentication.Encrypt(tiket);
                        HttpCookie authcookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        authcookies.Secure = true;
                        authcookies.HttpOnly = true;
                        Response.Cookies.Add(authcookies);

                        await Commonddl.dbSetHostHistory(UserID, HostPCName, ipAddress, ipAddress2, "login to " + domainname, timeout, browser);
                        Account.AccountTodo = new DataTable();
                        Session["Account"] = Account;
                        //Session["ddlBranch"] = dt;

                        //get todo list //
                        string msg = "Login Success " + UserID + " Warning Text: " + EnumMessage;
                        OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                    }
                    //else
                    //{
                    //    // increment the delay on failed login attempts
                    //    if (HttpContext.Application[Request.UserHostAddress] == null)
                    //    {
                    //        incrementalDelay = 1;
                    //    }
                    //    else
                    //    {
                    //        incrementalDelay = (int)HttpContext.Application[Request.UserHostAddress] * 2;
                    //    }
                    //    HttpContext.Application[Request.UserHostAddress] = incrementalDelay;
                    //}
                    //flag untuk menampilkan textbox OTP Code//
                    ViewBag.probituser = inputOtpCode;
                }
                else
                {
                    ShowMessage = "alert alert-danger display-block";
                    //EnumMessage = string.Join(" , ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    EnumMessage = "Cek ID Pengguna dan Kata Sandi";
                    //// increment the delay on failed login attempts
                    //if (HttpContext.Application[Request.UserHostAddress] == null)
                    //{
                    //    incrementalDelay = 1;
                    //}
                    //else
                    //{
                    //    incrementalDelay = (int)HttpContext.Application[Request.UserHostAddress] * 2;
                    //}
                    //HttpContext.Application[Request.UserHostAddress] = incrementalDelay;
                }
                return Json(new
                {
                    url = Url.Action(model.Action, model.Controller, new { area = "" }),
                    probitusertxt = ViewBag.probituser,
                    tokenpubtxt = ViewBag.tokenpub,
                    MessageNotValidx = EnumMessage,
                    ShowMessagex = ShowMessage
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 406;
                var msg = ex.Message.ToString();
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
                return Json(new
                {
                    url = Url.Action("Index", "Error", new { area = "" }),
                });
            }
        }
    }
}