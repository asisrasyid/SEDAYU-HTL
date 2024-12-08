using HashNetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DusColl
{
    public class blPendingTask
    {

        public string CheckVlaidasiInputPending(cPendingTask model, string statusdatacheck)
        {

            string validtxt = "";

            if (model.DataGIVE == "DEBITUR")
            {
                if ((model.cust_type_ahu ?? "").ToLower() == "personal" && (model.JenisKelaminNasabah ?? "") == "")
                {
                    validtxt = "Isikan Jenis Kelamin Pada Data Debitur";
                }
                if (model.NamaNasabah == "" || model.NoidentitasNasabah == "" || model.NoContactnasabah == "" || model.AlamatNasabah == "" ||
                    model.RTNasabah == "" || model.RWNasabah == "" || model.KelurahanNasabah == "" || model.KecamatanNasabah == "" ||
                    model.KabupatenKotaNasabah == "" || model.ProvinsiNasabah == "" || model.KabupatenKotaNasabahAHU == "" || model.PoskodeNasabah == "")
                {
                    validtxt = "Lengkapi Data Debitur";
                }
            }

            if (model.DataGIVE == "BPKB")
            {
                if ((model.cust_type_ahu ?? "").ToLower() == "personal" && (model.JenisKelaminBPKB ?? "") == "")
                {
                    validtxt = "Isikan Jenis Kelamin Pada Data BPKB";
                }
                if (model.NamaBPKB == "" || model.NoidentitasBPKB == "" || model.NoContactBPKB == "" || model.AlamatBPKB == "" ||
                    model.RTBPKB == "" || model.RWBPKB == "" || model.KelurahanBPKB == "" || model.KecamatanBPKB == "" ||
                    model.KabupatenKotaBPKB == "" || model.ProvinsiBPKB == "" || model.KabupatenKotaBPKBAHU == "" || model.PoskodeBPKB == "")
                {
                    validtxt = "Lengkapi Data BPKB";
                }
            }

            if ((model.TahunObject ?? "") == "")
            {
                validtxt = "Isikan Tahun Object";
            }

            if ((model.Kondisiobject ?? "") == "")
            {
                validtxt = "Isikan Kondisi Object";
            }

            if ((model.KategoriObject ?? "") == "")
            {
                validtxt = "Isikan Kategori Object";
            }

            if ((model.MerkObject ?? "") == "")
            {
                validtxt = "Isikan Merk Object";
            }

            if ((model.WarnaObject ?? "") == "")
            {
                validtxt = "Isikan Warna Object";
            }

            if (model.Roda.ToString() == "" || model.Roda.ToString() == "0")
            {
                validtxt = "Isikan Roda Object";
            }

            if (model.MesinNumber.ToString() == "")
            {
                validtxt = "Isikan No Mesin";
            }

            if (model.RangkaNumber.ToString() == "")
            {
                validtxt = "Isikan No Rangka";
            }

            if (model.NilaiHutang.ToString() == "" || model.NilaiHutang.ToString() == "0")
            {
                validtxt = "Isikan Nilai Hutang";
            }

            if (model.NilaiObject.ToString() == "" || model.NilaiObject.ToString() == "0")
            {
                validtxt = "Isikan Nilai Object";
            }

            if (model.NilaiJaminan.ToString() == "" || model.NilaiJaminan.ToString() == "0")
            {
                validtxt = "Isikan Nilai Jaminan";
            }

            if ((model.cust_type_ahu ?? "") == "")
            {
                validtxt = "Pilih Jenis Pemberi Fidusia AHU";
            }

            if ((model.DataGIVE ?? "") == "")
            {
                validtxt = "Pilih Sumber Data Pemberi Fidusia AHU";
            }

            return validtxt;
        }


        public string CheckFilterisasiDataPendTask(cFilterContract model, string isdownload = "")
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

            //if ((model.fromdate ?? "") != "" && (model.todate ?? "") != "")
            //{
            //    DateTime dt = DateTime.Parse(model.fromdate);
            //    DateTime dt1 = DateTime.Parse(model.todate);

            //    double noOfDays = dt.Subtract(dt1).TotalDays;

            //    if (Math.Abs(noOfDays) > (int)LimitFilter.LimitTglFilter)
            //    {
            //        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidRangeTanggalFilter);
            //    }
            //}


            //if (isdownload == "1")
            //{
            //    if ((model.fromdate ?? "") == "" || (model.todate ?? "") == "")
            //    {
            //        validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidTanggal);
            //    }

            //    // untuk print report jika ho dan admin sdb harus isiskan cabang/ u performance//
            //    if (((model.UserTypeApps == (int)UserType.HO)) && model.UserType == model.UserTypeApps)
            //    {
            //        string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
            //        if (cabang == "")
            //        {
            //            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
            //        }
            //    }


            //    if (((model.UserTypeApps == (int)UserType.FDCM) && model.UserType == model.UserTypeApps))
            //    {

            //        string cabang = (model.SelectBranch ?? "") != "" ? HasKeyProtect.Decryption(model.SelectBranch) : model.SelectBranch ?? "";
            //        if (cabang == "")
            //        {
            //            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidCabang);
            //        }

            //        string client = (model.SelectClient ?? "") != "" ? HasKeyProtect.Decryption(model.SelectClient) : model.SelectClient ?? "";
            //        if (client == "")
            //        {
            //            validtxt = EnumsDesc.GetDescriptionEnums(ProccessOutput.FilterValidClient);
            //        }

            //    }

            //}
            return validtxt;


        }
    }

}