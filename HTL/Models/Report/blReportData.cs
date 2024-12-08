using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DusColl
{
    public class blReportData
    {

        public string CheckFilterisasiCreateReport(cFilterContract model, string isdownload = "", string idcaption = "", string cretinvoice = "")
        {

            string validtxt = "";

            if ((model.fromdate ?? "") != "" && (model.todate ?? "") != "")
            {
                DateTime dt = DateTime.Parse(model.fromdate);
                DateTime dt1 = DateTime.Parse(model.todate);

                double noOfDays = dt.Subtract(dt1).TotalDays;

                if ((Math.Abs(noOfDays) > (int)LimitFilter.LimitTglFilter) && idcaption != EnumsDesc.GetDescriptionEnums((ReportType.PiutangPendaftaran)))
                {
                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidRangeTanggalFilterBilling);
                }
            }


            if (isdownload == "1" || isdownload == "2")
            {

                if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.CLAIMBASE)))
                {

                    string statusclaim = (model.SelectClaimBaseStatus ?? "");
                    if (statusclaim == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidStatus);
                    }

                    if (model.UserTypeApps != (int)UserType.HO && model.UserTypeApps != (int)UserType.Branch)
                    {
                        string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                        if (client == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                        }
                    }

                }

                else if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.AktBLN)))
                {

                    if ((model.fromdate ?? "") == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidPeriodeAkta);
                    }

                    if (model.UserTypeApps != (int)UserType.Notaris)
                    {
                        string notaris = (model.SelectNotaris ?? "") != "" ? HasKeyProtect.Decryption(model.SelectNotaris) : model.SelectNotaris ?? "";
                        if (notaris == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidNotaris);
                        }
                    }
                }

                else if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.AktaUsed)))
                {

                    if ((model.fromdate ?? "") == "" || (model.todate ?? "") == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
                    }

                    if (model.UserTypeApps != (int)UserType.Notaris)
                    {

                        string notaris = (model.SelectNotaris ?? "") != "" ? HasKeyProtect.Decryption(model.SelectNotaris) : model.SelectNotaris ?? "";
                        if (notaris == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidNotaris);
                        }
                    }
                }

                else if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.TAXaktaNotaris)))
                {
                    if ((model.fromdate ?? "") == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTahunPajak);
                    }

                    if (model.UserTypeApps != (int)UserType.Notaris)
                    {
                        string notaris = (model.SelectNotaris ?? "") != "" ? HasKeyProtect.Decryption(model.SelectNotaris) : model.SelectNotaris ?? "";
                        if (notaris == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidNotaris);
                        }
                    }
                }

                else if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.CRETBILLREG)))
                {

                    if ((model.duedate ?? "") == "" && cretinvoice == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTempoTanggal);
                    }
                    else
                    {

                        if (((model.fromdate ?? "") == "" || (model.todate ?? "") == "") && cretinvoice == "")
                        {

                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
                        }
                        else
                        {
                            if (cretinvoice == "")
                            {
                                DateTime dt = DateTime.Parse(model.fromdate);
                                DateTime dt1 = DateTime.Parse(model.duedate);

                                if ((dt > dt1) && cretinvoice == "")
                                {
                                    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTempoMoreTanggal);
                                }
                            }
                        }
                    }

                    string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
                    if (cabang == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
                    }
                    if (model.UserTypeApps != (int)UserType.HO && model.UserTypeApps != (int)UserType.Branch)
                    {
                        string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                        if (client == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                        }
                    }
                }
                else if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.PiutangPendaftaran)))
                {

                    string statusclaim = (model.SelectContractPaidStatus ?? "");
                    if (statusclaim == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidStatus);
                    }

                    if (model.UserTypeApps != (int)UserType.HO && model.UserTypeApps != (int)UserType.Branch)
                    {
                        string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                        if (client == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                        }
                    }

                }
                else if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.PendaftaranFidusia)))
                {

                    string statusclaim = (model.SelectContractStatus ?? "");
                    if (statusclaim == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidStatus);
                    }

                    if (model.UserTypeApps != (int)UserType.HO && model.UserTypeApps != (int)UserType.Branch)
                    {
                        string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                        if (client == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                        }
                    }
                }

                else if (idcaption == EnumsDesc.GetDescriptionEnums((ReportType.PengirimanBerkasPendaftaranMonthTodate)))
                {

                    if ((model.fromdate ?? "") == "" || (model.todate ?? "") == "")
                    {
                        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
                    }
                    if (model.UserTypeApps != (int)UserType.HO && model.UserTypeApps != (int)UserType.Branch)
                    {
                        string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
                        if (client == "")
                        {
                            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
                        }
                    }
                }

            }
            return validtxt;

        }

    }
}