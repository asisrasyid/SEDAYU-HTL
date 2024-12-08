using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DusColl
{
    public class blTrackingOrder
    {


        public string CheckFilterisasiDataTract(cFilterContract model, string isdownload = "")
        {

            string validtxt = "";


            if ((model.NoPerjanjian ?? "") != "" && (model.UserTypeApps != (int)UserType.HO && model.UserTypeApps != (int)UserType.Branch))
            {
                string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                if (client == "")
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                }
            }

            if ((model.NoPerjanjian ?? "") == "")
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidNoPerjanjian);
            }
            //if ((model.fromdate ?? "") == "" || (model.todate ?? "") == "")
            //{
            //    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
            //}

            // untuk print report jika ho dan admin sdb harus isiskan cabang/ u performance//
            //if (((model.UserTypeApps == (int)UserType.HO)) && model.UserType == model.UserTypeApps)
            //{
            //    string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
            //    if (cabang == "")
            //    {
            //        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
            //    }
            //}


            //if (((model.UserTypeApps == (int)UserType.FDCM) && model.UserType == model.UserTypeApps))
            //{

            //    string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
            //    if (cabang == "")
            //    {
            //        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
            //    }

            //    string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
            //    if (client == "")
            //    {
            //        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
            //    }

            //}


            return validtxt;

        }
    }

}