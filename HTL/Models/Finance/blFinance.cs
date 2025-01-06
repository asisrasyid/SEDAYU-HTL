using HashNetFramework;
using System;

namespace DusColl
{
    public class blFinance
    {
        public string CheckFilterisasiBillingCreate(cFilterContract model, string isdownload = "")
        {
            string validtxt = "";

            if (((model.fromdate ?? "") != "" && (model.todate ?? "") != "") && (model.NoPerjanjian ?? "") == "")
            {
                DateTime dt = DateTime.Parse(model.fromdate);
                DateTime dt1 = DateTime.Parse(model.todate);

                double noOfDays = dt.Subtract(dt1).TotalDays;

                if (Math.Abs(noOfDays) > (int)LimitFilter.LimitTglFilter)
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidRangeTanggalFilterBilling);
                }
            }

            if ((model.duedate ?? "") == "")
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTempoTanggal);
            }
            else
            {
                DateTime dt = DateTime.Parse(model.fromdate);
                DateTime dt1 = DateTime.Parse(model.duedate);

                if (dt > dt1)
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTempoMoreTanggal);
                }
            }
            if (isdownload == "1")
            {
                if (((model.fromdate ?? "") == "" || (model.todate ?? "") == "") && (model.NoPerjanjian ?? "") == "")
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
                }

                if (((model.UserTypeApps == (int)UserType.FDCM) && model.UserType == model.UserTypeApps))
                {
                    string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                    if (client == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                    }
                }
            }
            return validtxt;
        }

        public string CheckFilterisasiBilling(cFilterContract model, string isdownload = "")
        {
            string validtxt = "";

            if ((model.fromdate ?? "") != "" && (model.todate ?? "") != "")
            {
                DateTime dt = DateTime.Parse(model.fromdate);
                DateTime dt1 = DateTime.Parse(model.todate);

                double noOfDays = dt.Subtract(dt1).TotalDays;

                if (Math.Abs(noOfDays) > (int)LimitFilter.LimitTglFilter)
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidRangeTanggalFilter);
                }
            }

            if (isdownload == "1")
            {
                if ((model.fromdate ?? "") == "" || (model.todate ?? "") == "")
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
                }

                // untuk print report jika ho dan admin sdb harus isiskan cabang/ u performance//
                if (((model.UserTypeApps == (int)UserType.HO)) && model.UserType == model.UserTypeApps)
                {
                    string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
                    if (cabang == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
                    }
                }

                if (((model.UserTypeApps == (int)UserType.FDCM) && model.UserType == model.UserTypeApps))
                {
                    string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
                    if (cabang == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
                    }

                    string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                    if (client == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                    }
                }
            }
            return validtxt;
        }
    }
}