using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace DusColl
{
    public class blAccount
    {

        vmAccountddl Accountddl = new vmAccountddl();

        public async Task<vmAccount> AuthenticateUserGroupMatrik(cAccount model)
        {
            vmAccount Account = new vmAccount();

            Account.AccountLogin = model;
            Account.AccountGroupUserList = await Accountddl.dbGetAuthGroupUser(false, "", model.UserID);
            if (Account.AccountGroupUserList.Count > 0)
            {
                model.GroupName = Account.AccountGroupUserList.Where(x => x.MainGrup == true).Select(x => x.GroupName).SingleOrDefault().ToString();
                Account.AccountMetrikList = await Accountddl.dbaccountmatriklist(false, model.GroupName, "");
                if (Account.AccountMetrikList.Count == 0)
                {
                    model.MessageNotValid = ProccessOutput.NotAccess.GetDescriptionEnums().ToString();
                }
            }
            else
            {
                model.MessageNotValid = ProccessOutput.FilterValidUserNogroup.GetDescriptionEnums().ToString();
            }

            return Account;
        }
        public async Task<cAccount> AuthenticateUser(cAccount model, string email, string template)
        {

            string strMessage = "";
            model.Controller = "Home";
            model.email = email;
            model.template = template;
            model = await Accountddl.dbAuthenticateUser(model);
            strMessage = EnumsDesc.GetDescriptionEnums((ProccessOutput)model.AuthenProperty);

            if (model.AuthenProperty == (int)ProccessOutput.Success)
            {
                model.Controller = "Home";
                model.Action = "HomeGate";
                model.IDCabang = HasKeyProtect.Encryption(model.IDCabang ?? "");
                model.ClientID = HasKeyProtect.Encryption(model.ClientID ?? "");
                model.Region = HasKeyProtect.Encryption(model.Region ?? "");
                model.UserType = HasKeyProtect.Encryption(model.UserType);
                model.IDNotaris = HasKeyProtect.Encryption(model.IDNotaris ?? "");
                model.Mailed = HasKeyProtect.Encryption(model.Mailed ?? "");
                model.GenMoon = HasKeyProtect.Encryption(model.GenMoon ?? "");
                model.IDBPN = HasKeyProtect.Encryption(model.IDBPN ?? "");
                model.Phone = HasKeyProtect.Encryption(model.Phone ?? "");
                model.NoSK= HasKeyProtect.Encryption(model.NoSK?? "");

                strMessage = "";
            }
            else
            {
                model.Controller = "Account";
                model.Action = "LogUserIn";
            }


            model.MessageNotValid = strMessage;
            model.ShowMessage = "alert alert-danger display-block";
            model.PropAccess = model.AuthenProperty;
            return model;
        }
        public cAccount NotExistSesionID(HttpCookie authCookie, cAccount model)
        {

            if (model == null)
            {
                model = new cAccount();
                model.RouteName = "DefaultExpired";

            }
            else
            {
                if (model.UserID == null)
                {

                    model.SessionIDNotExist = false;
                    model.RouteName = "DefaultExpired";

                }
                else
                {

                    model.RouteName = "";
                    model.SessionIDNotExist = false;
                }

            }

            try
            {
                if (authCookie.Value != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    if ((ticket.Expired == true) || (model == null))
                    {
                        model.SessionIDNotExist = true;
                    }
                }
                else
                {
                    model.SessionIDNotExist = true;
                }

            }

            catch
            {
                model.SessionIDNotExist = true;
            }



            if (model.SessionIDNotExist == true)
            {
                model.RouteName = "DefaultExpired";
            }



            if (model.RouteName == "DefaultExpired")
            {
                model.Controller = "Account";
                model.Action = "AccountTimeOut";
            }

            ////cek selalu apakah user sedang login
            //vmCommonddl Commonddl = new vmCommonddl();
            //DataTable dt = Commonddl.dbSetHostHistoryCK(model.UserID, "", "", "", model.RouteName, "0", "", "chk");
            //int result = int.Parse(dt.Rows[0][0].ToString());
            //if (result == 0)
            //{
            //    model.RouteName = "DefaultExpired";
            //    model.Controller = "Account";
            //    model.Action = "AccountTimeOut";
            //}

            return model;
        }

    }
}