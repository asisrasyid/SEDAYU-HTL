//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Helpers;
//using System.Web.Security;


//namespace DusColl
//{
//    public class blContractsRev
//    {


//        public async Task<int> ins_attachment(vmRoya model, HttpPostedFileBase FileUpload)
//        {
//            int resultt = 0;
//            CustomeVMModel CustomVM = new CustomeVMModel();
//            vmRoya vmRoya = new vmRoya();

//            string limitmaxwidthtoresize = ConfigurationManager.AppSettings["limitmaxwidthtoresize"].ToString();
//            string widthresizeto = ConfigurationManager.AppSettings["widthresizeto"].ToString();
//            string heightresizeto = ConfigurationManager.AppSettings["heightresizeto"].ToString();

//            string idcaption = CustomSecureData.Decryption(model.securemoduleID);

//            await Task.Delay(1);
//            //if (FileUpload.ContentType.ToLower().Contains("image"))
//            //{
//            //    WebImage ObjectUpload = new WebImage(FileUpload.InputStream);
//            //    ObjectUpload.FileName = FileUpload.FileName;
//            //    if (ObjectUpload.Width > int.Parse(limitmaxwidthtoresize))
//            //    {
//            //        ObjectUpload.Resize(int.Parse(widthresizeto), int.Parse(heightresizeto) == -1 ? ObjectUpload.Height : int.Parse(heightresizeto));
//            //    }

//            //    resultt = ValidationUpload.CheckValidationUpload(ObjectUpload.FileName, ObjectUpload.GetBytes().Length);
//            //    if (resultt > 0)
//            //    {
//            //        resultt = await CustomVM.ins_attachmentimage(model.AccountDetail.ClientID,model.DetailFilter.SelectClient, model.RoyaOneRow.secureContractNo, model.RoyaOneRow.secureSertifikate, model.varJenisDocument, model.AccountDetail.UserID, model.AccountGroupUser.GroupName, ObjectUpload, FileUpload.ContentType, idcaption);
//            //        if (resultt > 0)
//            //        {
//            //            resultt = await vmRoya.dbRoyaDoneUpload(model.AccountDetail.ClientID, model.DetailFilter.SelectClient,model.RoyaOneRow.secureContractNo, model.RoyaOneRow.secureSertifikate, model.AccountDetail.UserID, model.AccountGroupUser.GroupName, idcaption);
//            //        }
//            //    }

//            //}
//            //else
//            //{
//            //    resultt = ValidationUpload.CheckValidationUpload(FileUpload.FileName, FileUpload.ContentLength);
//            //    if (resultt > 0)
//            //    {
//            //        resultt = await CustomVM.ins_attachmentnonImage(model.AccountDetail.ClientID, model.DetailFilter.SelectClient,model.RoyaOneRow.secureContractNo, model.RoyaOneRow.secureSertifikate, model.varJenisDocument, model.AccountDetail.UserID, model.AccountGroupUser.GroupName, FileUpload, idcaption);
//            //        if (resultt > 0)
//            //        {
//            //            resultt = await vmRoya.dbRoyaDoneUpload(model.AccountDetail.ClientID, model.DetailFilter.SelectClient,model.RoyaOneRow.secureContractNo, model.RoyaOneRow.secureSertifikate, model.AccountDetail.UserID, model.AccountGroupUser.GroupName, idcaption);
//            //        }
//            //    }

//            //}



//            return resultt;
//        }

//        //public async Task<List<cRoya>> RefreshGridFiler(cFilter model, string ClientID, string UserID, string GroupName)
//        //{
//        //    vmRoya vmRoya = new vmRoya();

//        //    if (model != null)
//        //    {

//        //        vmRoya.RoyaList = await vmRoya.dbGetRoyaList(model, ClientID, UserID, GroupName);
//        //    }
//        //    else
//        //    {
//        //        vmRoya.RoyaList = await vmRoya.dbGetRoyaList(model, ClientID, UserID, GroupName);
//        //    }
//        //    return vmRoya.RoyaList;

//        //}

//        public string CheckRecordSearch(List<cContracts> datacontact)
//        {
//            string Message = "";
//            // trap fo record not found//
//            int FoundRecord = datacontact.Count;
//            Message = FoundRecord == 0 ? EnumsDesc.GetDescriptionEnums((ProccessOutput.RecordNotFound)) : "";
//            return Message;
//        }

//        public string CheckFilterisasiData(cFilter model,string isdownload="")
//        {

//            string validtxt = "";

//            if ((model.fromdate ?? "") != "" && (model.todate ?? "") != "")
//            {
//                DateTime dt = DateTime.Parse(model.fromdate);
//                DateTime dt1 = DateTime.Parse(model.todate);

//                double noOfDays = dt.Subtract(dt1).TotalDays;

//                if (Math.Abs(noOfDays) > 31)
//                {
//                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidRangeTanggalFilter);
//                }
//            }

//            if (isdownload == "1")
//            {
//                if ((model.fromdate ?? "") == "" || (model.todate ?? "") == "")
//                {
//                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
//                }

//                // untuk print report jika ho  harus isiskan cabang/ u performance//
//                if (((model.UserTypeApps == (int)UserType.HO) && model.UserType == model.UserTypeApps))
//                {
//                    string cabang = (model.SelectBranch ?? "") != "" ? CustomSecureData.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
//                    if (cabang == "")
//                    {
//                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
//                    }
//                }

//                if (((model.UserTypeApps == (int)UserType.FDCM) && model.UserType == model.UserTypeApps))
//                {

//                    string cabang = (model.SelectBranch ?? "") != "" ? CustomSecureData.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
//                    if (cabang == "")
//                    {
//                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
//                    }


//                    string client = (model.SelectClient ?? "") != "" ? CustomSecureData.Decryption(model.SelectClient) : model.SelectClient ?? "";
//                    if (client == "")
//                    {
//                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
//                    }


//                }

//                if (((model.UserTypeApps == (int)UserType.Notaris) && model.UserType == model.UserTypeApps))
//                {
//                    string client = (model.SelectClient ?? "") != "" ? CustomSecureData.Decryption(model.SelectClient) : model.SelectClient ?? "";
//                    if (client == "")
//                    {
//                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
//                    }

//                }

//            }
//            return validtxt;


//        }

//    }
//}
