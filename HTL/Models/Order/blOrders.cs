using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DusColl
{
    public class blOrders
    {

        vmOrders vmContracts = new vmOrders();

        public string CheckSearchOrder(cFilterContract model)
        {

            string validtxt = "";
            string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
            if (client == "")
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
            }
            return validtxt;

        }

        public string CheckSubmitOrder(cFilterContract model, string[] TotalSelectdata, decimal maxdata)
        {

            string validtxt = "";

            string SelectClient = (model.SelectClient ?? "") == "" ? "" : HasKeyProtect.Decryption(model.SelectClient);
            string SelectNotaris = (model.SelectNotaris ?? "") == "" ? "" : HasKeyProtect.Decryption(model.SelectNotaris);


            if (TotalSelectdata == null)
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterDataPerjanjianOrderNotaris);
            }
            else if (TotalSelectdata.Length <= 0)
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterDataPerjanjianOrderNotaris);
            }

            if (model.JumlahOrder <= 0)
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterDataOrderNotaris);
            }

            if (model.JumlahOrder > maxdata)
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterDataMaxOrderNotaris);
            }

            if ((SelectNotaris ?? "") == "")
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidNotaris);
            }

            if ((SelectClient ?? "") == "")
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
            }


            return validtxt;
        }

        public string CheckFilterisasiData(cFilterContract model, string isdownload = "")
        {

            string validtxt = "";

            if ((model.fromdate ?? "") != "" || (model.todate ?? "") != "")
            {

                if ((model.fromdate ?? "") == "")
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
                }
                else if ((model.todate ?? "") == "")
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
                }
                else
                {

                    DateTime dt = DateTime.Parse(model.fromdate);
                    DateTime dt1 = DateTime.Parse(model.todate);

                    double noOfDays = dt.Subtract(dt1).TotalDays;

                    if (Math.Abs(noOfDays) > (int)LimitFilter.LimitTglFilter)
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidRangeTanggalFilter);
                    }
                }
            }

            if ((model.NoPerjanjian ?? "") != "" && (model.UserTypeApps != (int)UserType.HO && model.UserTypeApps != (int)UserType.Branch))
            {
                string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                if (client == "")
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                }
            }

           
            return validtxt;


        }
    }
}