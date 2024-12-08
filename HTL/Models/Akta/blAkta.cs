using HashNetFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DusColl
{
    public class blAkta
    {

        public string CheckFilterisasiDataGen(cFilterContract model)
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

        public string CheckFilterisasiDataSubmit(cFilterContract model)
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
