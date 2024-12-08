//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;

//namespace DusColl
//{

//    [Serializable]
//    public class cContractsRev : cContracts
//    {
//        public string secREFNO_PERJANJIAN { get; set; }
//        public DateTime? TGL_PENGAJUAN { get; set; }
//        public string CONT_HISTORICAL_NOTES { get; set; }
//        public string CONT_KRONOLOG_NOTES { get; set; }
//        public string NOREF_PERJANJIAN { get; set; }
        

//    }

//    [Serializable]
//    public class cContractsOrderRegisRevList
//    {
//        public string Tanggal_Perjanjian { get; set; }
//        public string NoPerjanjian { get; set; }

//        public string ref_NoPerjanjian { get; set; }

//        public string Jenis_Pelanggan { get; set; }
//        public string Jenis_Pembiayaan { get; set; }
//        public string Jenis_Penggunaan { get; set; }

//        public string Nama_Debitur { get; set; }
//        public string Jenis_Kelamin_Debitur { get; set; }
//        public string No_Telp_HP_Debitur { get; set; }
//        public string Jenis_Identitas_Debitur { get; set; }
//        public string No_KTP_NPWP_Debitur { get; set; }
//        public string Tempat_Lahir_Debitur { get; set; }
//        public string Tanggal_Lahir_Debitur { get; set; }
//        public string Pekerjaan_Debitur { get; set; }
//        public string Alamat_Debitur { get; set; }
//        public string RT_Debitur { get; set; }
//        public string RW_Debitur { get; set; }
//        public string Kelurahan_Debitur { get; set; }
//        public string Kecamatan_Debitur { get; set; }
//        public string Kota_Debitur { get; set; }
//        public string KodePos_Debitur { get; set; }
//        public string Provinsi_Debitur { get; set; }



//        public string Nama_BPKB { get; set; }
//        public string Jenis_Kelamin_BPKB { get; set; }
//        public string No_Telp_HP_BPKB { get; set; }
//        public string Jenis_Identitas_BPKB { get; set; }
//        public string No_KTP_NPWP_BPKB { get; set; }
//        public string Tempat_Lahir_BPKB { get; set; }
//        public string Tanggal_Lahir_BPKB { get; set; }
//        public string Pekerjaan_BPKB { get; set; }
//        public string Alamat_BPKB { get; set; }
//        public string RT_BPKB { get; set; }
//        public string RW_BPKB { get; set; }
//        public string Kelurahan_BPKB { get; set; }
//        public string Kecamatan_BPKB { get; set; }
//        public string Kota_BPKB { get; set; }
//        public string KodePos_BPKB { get; set; }
//        public string Provinsi_BPKB { get; set; }


//        public string Name_PIC_PT { get; set; }
//        public string Jenis_Kelamin_PIC_PT { get; set; }
//        public string NoHp_PIC_PT { get; set; }
//        public string KTP_PIC_PT { get; set; }
//        public string Jabatan_PIC_PT { get; set; }
//        public string Alamat_PIC_PT { get; set; }
//        public string RT_PIC_PT { get; set; }
//        public string RW_PIC_PT { get; set; }
//        public string Kelurahan_PIC_PT { get; set; }
//        public string Kecamatan_PIC_PT { get; set; }
//        public string Kota_PIC_PT { get; set; }
//        public string KodePos_PIC_PT { get; set; }
//        public string Provinsi_PIC_PT { get; set; }

//        public string Tanggal_akhir_angsuran { get; set; }
//        public string Tanggal_awal_angsuran { get; set; }
//        public string Jenis_Object { get; set; }
//        public string No_BPKB_Object_Bekas { get; set; }
//        public string Kondisi_Object { get; set; }
//        public Double? Jumlah_Roda { get; set; }

//        public Double? Nilai_PokokHutang { get; set; }
//        public Double? Nilai_Penjaminan { get; set; }
//        public Double? Nilai_Objek_Penjaminan { get; set; }
//        public string Merk { get; set; }
//        public string Tipe_Kendaraan { get; set; }
//        public string Warna { get; set; }
//        public string NoRangka { get; set; }
//        public string NoMesin { get; set; }
//        public Double? TahunPembuatan { get; set; }

//        public string Detail_Perbaikan_Perubahan { get; set; }
//        public string Histori_Perbaikan_Perubahan_Sertifikat { get; set; }

//    }



//    [Serializable]
//    public class cFilterContractRev
//    {
//        public string secIDFDC { get; set; }
//        public string idcaption { get; set; }
//        public string NoPerjanjian { get; set; }
//        public string NoSertifikat { get; set; }
//        public String fromdate { get; set; }
//        public String todate { get; set; }
//        public String SelectRoyaType { get; set; }
//        public String SelectRoyaStatus { get; set; }
//        public String SelectRoyaTypeDesc { get; set; }
//        public String SelectRoyaStatusDesc { get; set; }
//        public int UserType { get; set; }
//        public int UserTypeApps { get; set; }
//        public string pukulakta { get; set; }
//        public string SelectNotaris { get; set; }
//        public string SelectNotarisDesc { get; set; }
//        public string SelectClient { get; set; }
//        public string SelectClientDesc { get; set; }
//        public string SelectBranch { get; set; }
//        public string SelectBranchDesc { get; set; }
//        public string ClientLogin { get; set; }
//        public string NotaryLogin { get; set; }
//        public string CabangLogin { get; set; }
//        public string SelectCabang { get; set; }
//        public string SelectContractStatus { get; set; }
//        public string SelectContractStatusDesc { get; set; }
//        public int PageNumber { get; set; } = 1;
//        public int OutstandingOrder { get; set; }
//        public double PageSize { get; set; }
//        public double TotalPage { get; set; }
//        public double TotalRecord { get; set; }
//        public bool isdownload { get; set; } = false;
//        public bool isModeFilter { get; set; } = false;
//        public double pagingsizeclient { get; set; }
//        public double pagenumberclient { get; set; }
//        public double totalPageclient { get; set; }
//        public double totalRecordclient { get; set; }

//    }

//    [Serializable]
//    public class cDouemntsRev : cContractsRev
//    {

//        public int ID_UPLOAD { get; set; }
//        public String JENIS_DOCUMENT { get; set; }
//        public String FILE_NAME { get; set; }
//        public String CONTENT_TYPE { get; set; }
//        public decimal CONTENT_LENGTH { get; set; }
//        public Byte[] FILE_BYTE { get; set; }
//        public DateTime MODIFIED_DATE { get; set; }
//        public String USERID { get; set; }
//        public String KB { get; set; }
//        public String STATUSPERJANJIAN { get; set; }

//    }
//}
