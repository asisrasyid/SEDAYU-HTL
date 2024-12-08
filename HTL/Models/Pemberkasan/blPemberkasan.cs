using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DusColl
{
    public class blPemberkasan
    {

        public string CheckVlaidasiInput(cDouemntsGroupType model, string SelectClient, string SelectBranch)
        {
            string validtxt = "";

            if (model.SelectDocStatus == "1" || model.SelectDocStatus == "4")
            {
                if (
                    (model.NamaGIVE ?? "") == "" || (model.JenisidentitasGIVE ?? "") == "" || (model.AlamatGIVE ?? "") == "" || (model.AlamatGIVE ?? "") == "" || (model.RTGIVE ?? "") == ""
                    || (model.RWGIVE ?? "") == "" || (model.PoskodeGIVE ?? "") == ""
                    || (model.ProvinsiGIVE ?? "") == "" || (model.KabupatenKotaGIVE ?? "") == "" || (model.KecamatanGIVE ?? "") == "" || (model.KelurahanGIVE ?? "") == ""
                   )
                {
                    validtxt = "Isikan Lengkapi data Pemberi Fidusia ";
                }
                else if (model.TIPEGIVE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) && model.JenisidentitasGIVE != EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)))
                {
                    validtxt = "Jenis identitas pada data Pemberi Fidusia harus NPWP";
                }
                else if (model.JenisidentitasGIVE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.KTP)) && model.NoidentitasGIVE.Length != 16)
                {
                    validtxt = "No identitas pada data Pemberi Fidusia harus 16 digit";
                }
                else if (model.JenisidentitasGIVE == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)) && model.NoidentitasGIVE.Length != 15)
                {
                    validtxt = "No identitas pada data Pemberi Fidusia harus 15 digit";
                }
                else if (model.TIPEGIVE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Personal)) && ((model.JenisKelaminGIVE ?? "") == "" || (model.TglLahirGIVE ?? "") == "" || (model.TempatLahirGIVE ?? "") == "" || (model.JenisidentitasGIVE ?? "") == ""))
                {
                    validtxt = "Isikan Jenis Kelamin/Tgl lahir/Tempat lahir/Kewarganegaraan pada data Pemberi Fidusia";
                }
                else if (model.TIPEGIVE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) && ((model.JenisKelaminGIVE ?? "") != "" || (model.TglLahirGIVE ?? "") != "" || (model.TempatLahirGIVE ?? "") != "" || (model.KewarganegaraanGIVE ?? "") != ""))
                {
                    validtxt = "Kosongkan Jenis Kelamin/Tgl lahir/Tempat lahir/Kewarganegaraan pada data Pemberi Fidusia";
                }

                else
                if (model.TIPEGIVE == EnumsDesc.GetDescriptionEnums((HashNetFramework.FidusiaType.Badan)) &&
                    (
                        (model.NamaPIC ?? "") == "" || (model.JenisKelaminPIC ?? "") == "" || (model.TglLahirPIC ?? "") == "" || (model.TempatLahirPIC ?? "") == "" || (model.JenisidentitasPIC ?? "") == "" ||
                        (model.NoidentitasPIC ?? "") == "" || (model.KewarganegaraanPIC ?? "") == "" || (model.AlamatPIC ?? "") == "" || (model.RTPIC ?? "") == "" || (model.RWPIC ?? "") == "" ||
                        (model.PoskodePIC ?? "") == "" || (model.ProvinsiPIC ?? "") == "" || (model.KabupatenKotaPIC ?? "") == "" || (model.KecamatanPIC ?? "") == "" || (model.KelurahanPIC ?? "") == ""

                    )
                   )
                {
                    validtxt = "Isikan & Lengkapi PIC BADAN USAHA";
                }
                else if (model.JenisidentitasPIC == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.KTP)) && model.NoidentitasPIC.Length != 16)
                {
                    validtxt = "No identitas pada data PIC BADAN USAHA harus 16 digit";
                }
                else if (model.JenisidentitasPIC == EnumsDesc.GetDescriptionEnums((HashNetFramework.jenisIdentitas.NPWP)) && model.NoidentitasPIC.Length != 15)
                {
                    validtxt = "No identitas pada data PIC BADAN USAHA harus 15 digit";
                }
            }
            return validtxt;
        }

        public string CheckFilterisasiDataUplod(cFilterContract model, string isdownload = "")
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
            if (((model.fromdate ?? "") == "" || (model.todate ?? "") == "") && ((model.NoPerjanjian ?? "") == ""))
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
            }

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

            string noperjanjian = model.NoPerjanjian ?? "";
            string region = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectRegion) : model.SelectRegion ?? "";
            string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
            string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";

            if ((client == "") && (model.UserTypeApps == (int)UserType.HO && noperjanjian == ""))
            {
                validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
            }

            //if ((cabang == "") && noperjanjian == "" && (model.UserTypeApps == (int)UserType.HO || model.UserTypeApps == (int)UserType.FDCM))
            //{
            //    validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
            //}



            return validtxt;


        }
    }


}