using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DusColl
{
    public class blPendaftaran
    {


        public string CheckVlaidasiInput(cpendaftaranOder model, string SelectClient, string SelectBranch, int UserTypes)
        {

            string validtxt = "";

            if (model.CUST_TYPE_AHU == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) && model.GRTE_IDENTITY_TYPE != EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)))
            {
                validtxt = "Jenis identitas pada data DEBITUR/PELANGGAN harus NPWP";
            }
            else if (model.GRTE_IDENTITY_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.KTP)) && model.GRTE_IDENTITY_NO.Length != 16)
            {
                validtxt = "No identitas pada data DEBITUR/PELANGGAN harus 16 digit";
            }
            else if (model.GRTE_IDENTITY_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)) && model.GRTE_IDENTITY_NO.Length != 15)
            {
                validtxt = "No identitas pada data DEBITUR/PELANGGAN harus 15 digit";
            }
            else if (model.CUST_TYPE_AHU == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Personal)) && ((model.GRTE_GENDER ?? "") == "" || (model.GRTE_DATE_PLACE ?? "") == "" || (model.GRTE_PLACE_BIRTH ?? "") == "" || (model.GRTE_IDENTITY_TYPE ?? "") == ""))
            {
                validtxt = "Isikan Jenis Kelamin/Tgl lahir/Tempat lahir/Kewarganegaraan pada data DEBITUR/PELANGGAN";
            }
            else if (model.CUST_TYPE_AHU == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) && ((model.GRTE_GENDER ?? "") != "" || (model.GRTE_DATE_PLACE ?? "") != "" || (model.GRTE_PLACE_BIRTH ?? "") != "" || (model.GRTE_CITIZENSHIP ?? "") != ""))
            {
                validtxt = "Kosongkan Jenis Kelamin/Tgl lahir/Tempat lahir/Kewarganegaraan pada data DEBITUR/PELANGGAN";
            }

            else if (model.CUST_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) && model.CUST_IDENTITY_TYPE != EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)))
            {
                validtxt = "Jenis identitas pada data BPKB harus NPWP";
            }
            else if (model.CUST_IDENTITY_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.KTP)) && model.CUST_IDENTITY_NO.Length != 16)
            {
                validtxt = "No identitas pada data BPKB harus 16 digit";
            }
            else if (model.CUST_IDENTITY_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)) && model.CUST_IDENTITY_NO.Length != 15)
            {
                validtxt = "No identitas pada data BPKB harus 15 digit";
            }
            else if (model.CUST_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Personal)) && ((model.CUST_GENDER ?? "") == "" || (model.CUST_DATE_BIRTH ?? "") == "" || (model.CUST_PLACE_BIRTH ?? "") == "" || (model.CUST_IDENTITY_TYPE ?? "") == ""))
            {
                validtxt = "Isikan Jenis Kelamin/Tgl lahir/Tempat lahir/Kewarganegaraan pada data BPKB";
            }
            else if (model.CUST_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) && ((model.CUST_GENDER ?? "") != "" || (model.CUST_DATE_BIRTH ?? "") != "" || (model.CUST_PLACE_BIRTH ?? "") != "" || (model.CUST_CITIZENSHIP ?? "") != ""))
            {
                validtxt = "Kosongkan Jenis Kelamin/Tgl lahir/Tempat lahir/Kewarganegaraan pada data BPKB";
            }
            else if (model.OBJT_CONDITION == "U" && (model.OBJT_BPKB_NO ?? "") == "")
            {
                validtxt = "Isikan No BPKB Pada Data Object";
            }
            else if (model.OBJT_CONDITION == "N" && (model.OBJT_BPKB_NO ?? "") != "")
            {
                validtxt = "Kosongkan No BPKB Pada Data Object";
            }
            else if ((model.OBJT_WHEEL ?? "") == "" || (model.OBJT_WHEEL ?? "") == "0")
            {
                validtxt = "Isikan Jumlah Roda Pada Data Object";
            }
            else if (DateTime.Parse(model.CONT_FIRSTINSTALLMENT_DATE) > DateTime.Parse(model.CONT_ENDINSTALLMENT_DATE))
            {
                validtxt = "Tanggal awal angsuran harus lebih kecil dari Tanggal akhir angsuran";
            }
            else if (SelectBranch != model.CONT_NO.Substring(0, 3))
            {
                validtxt = "No Perjanjian tidak sama dengan cabang penginput data";
            }
            else if (((SelectClient ?? "") == "" || (SelectBranch ?? "") == "") && UserTypes != (int)HashNetFramework.UserType.FDCM)
            {
                validtxt = "Informasi Client dan cabang Kosong, Silahkan Login Kembali";
            }
            else
            if (model.CUST_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) &&
                (
                    (model.TGJW_NAME ?? "") == "" || (model.TGJW_GENDER ?? "") == "" || (model.TGJW_DATE_PLACE ?? "") == "" || (model.TGJW_PLACE_BIRTH ?? "") == "" || (model.TGJW_IDENTITY_TYPE ?? "") == "" ||
                    (model.TGJW_IDENTITY_NO ?? "") == "" || (model.TGJW_CITIZENSHIP ?? "") == "" || (model.TGJW_ADDRESS ?? "") == "" || (model.TGJW_NEIGHBOURHOOD_NO ?? "") == "" || (model.TGJW_HAMLET_NO ?? "") == "" ||
                    (model.TGJW_POSTCODE ?? "") == "" || (model.TGJW_PROVINCE ?? "") == "" || (model.TGJW_CITY ?? "") == "" || (model.TGJW_SUBDISTRICT ?? "") == "" || (model.TGJW_URBAN_VILLAGE ?? "") == ""

                )
               )
            {
                validtxt = "Isikan & Lengkapi PIC PENGHADAP BADAN USAHA";
            }
            else if (model.TGJW_IDENTITY_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.KTP)) && model.TGJW_IDENTITY_NO.Length != 16)
            {
                validtxt = "No identitas pada data PIC PENGHADAP BADAN USAHA harus 16 digit";
            }
            else if (model.TGJW_IDENTITY_TYPE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)) && model.TGJW_IDENTITY_NO.Length != 15)
            {
                validtxt = "No identitas pada data PIC PENGHADAP BADAN USAHA harus 15 digit";
            }


            if (model.CONT_PRINCIPAL_AMOUNT.Split('.').Count() > 1)
            {
                if (model.CONT_PRINCIPAL_AMOUNT.Split('.')[1].Length > 2)
                {
                    validtxt = "Pokok hutang pada Data Perjanjian & Object angka decimal tidak boleh lebih dari 2 decimal";
                }
            }
            if (model.CONT_COLLETERAL_AMOUNT.Split('.').Count() > 1)
            {
                if (model.CONT_COLLETERAL_AMOUNT.Split('.')[1].Length > 2)
                {
                    validtxt = "Nilai jaminan pada Data Perjanjian & Object angka decimal tidak boleh lebih dari 2 decimal";
                }
            }
            if (model.OBJT_AMOUNT.Split('.').Count() > 1)
            {
                if (model.OBJT_AMOUNT.Split('.')[1].Length > 2)
                {
                    validtxt = "Nilai object pada Data Perjanjian & Object angka decimal tidak boleh lebih dari 2 decimal";
                }
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