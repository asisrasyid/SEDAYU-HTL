using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DusColl
{

    [Serializable]
    public enum cCommandTextAkta
    {

        [Description("api/FDCMAkta/dbGetAktaList")]
        cmdGetAktaList = 1,

        [Description("api/FDCMAkta/dbGetOrderOutstandingCount")]
        cmdGetOrderOutstandingCount = 4,

        [Description("api/FDCMAkta/dbGetAktaListCreate")]
        cmdGetAktaListCreate = 6,

        [Description("api/FDCMAkta/dbSaveAkta")]
        cmdSaveAkta = 7,

        [Description("api/FDCMAkta/dbSaveAktavalid")]
        cmdSaveAktavalid = 11,

        [Description("api/FDCMAkta/dbGetRptAktaUsedSummary")]
        cmdGetRptAktaUsedSummary = 8,

        [Description("api/FDCMAkta/dbGetRptAktaBLN")]
        cmdGetRptAktaBLN = 9,

        [Description("api/FDCMAkta/dbGetRptAktaTaxNtry")]
        cmdGetRptAktaTaxNtry = 10,


        [Description("api/FDCMAkta/dbGetRptAktaDetailBLN")]
        cmdGetRptAktaDetailBLN = 12,

    }

    //[Serializable]
    //public class cAkta
    //{
    //    public string No_Perjanjian { get; set; }

    //    [Required(ErrorMessage = "Pilih Tanggal Order")]
    //    public string Tgl_order { get; set; }

    //    //public string stringNoAkta { get; set; }
    //    public int OutstandingOrder { get; set; }
    //    public int JedaMenitAkta { get; set; }
    //    public string LastNumberAkta { get; set; }
    //    //public string Kode_Akta { get; set; }
    //    //public string No_Akta { get; set; }
    //    public string TglAkta { get; set; }
    //    [DataType(DataType.Time, ErrorMessage = "date is not a corret format")]
    //    [Required(ErrorMessage = "Isikan Pukul Akta")]
    //    public string Pukul_Akta { get; set; }

    //    public string NoPerjanjian { get; set; }
    //    //public string ClientID { get; set; }
    //    //public string NotarisID { get; set; }
    //    //public DateTime CreatedDate { get; set; }
    //    //public string CreatedBy { get; set; }
    //    //public DateTime ModifiedDate { get; set; }
    //    //public string ModifiedBy { get; set; }
    //    //public DateTime DeletedDate { get; set; }
    //    //public string DeletedBy { get; set; }
    //    //public bool Deleted { get; set; }
    //    //public cOrder DetailOrder { get; set; }
    //}

    //[Serializable]
    //public class cOrder
    //{
    //    public string NoPerjanjian { get; set; }
    //    public string NotarisID { get; set; }
    //    public string NotarisName { get; set; }
    //    public string ClientID { get; set; }
    //    public string secClientID { get; set; }
    //    public string id_FDC { get; set; }
    //    public string NamaClient { get; set; }
    //    public string NoOrder { get; set; }
    //    public int JumlahOrder { get; set; }
    //    public DateTime TglOrder { get; set; }

    //}

    //[Serializable]
    //public class cListOrderAkta : cOrder
    //{
    //    public DateTime TglAkta { get; set; }
    //    public string AktaNo { get; set; }
    //    public string AktaKode { get; set; }
    //    public TimeSpan PukulAkta { get; set; }
    //    public string CreatedBy { get; set; }
    //    public int SeqID { get; set; }
    //}

    //[Serializable]
    //public class cAktaSave
    //{
    //    public string NO_PERJANJIAN { get; set; }
    //    public DateTime TGL_AKTA { get; set; }
    //    public string NO_AKTA { get; set; }
    //    public string KODE_AKTA { get; set; }
    //    public string PUKUL_AKTA { get; set; }
    //    public string CreatedBy { get; set; }
    //    public string Order_Date { get; set; }
    //    public string NotarisName { get; set; }
    //    public string NotarisID { get; set; }
    //    public int SeqID { get; set; }
    //}

    //[Serializable]
    //public class cViewAkta
    //{
    //    public string keylookupdata { get; set; }
    //    public string Regional { get; set; }
    //    public string secidakta { get; set; }
    //    public string NoPerjanjian { get; set; }
    //    public DateTime TglPerjanjian { get; set; }
    //    public string KodeCabang { get; set; }
    //    public string secID_FDC { get; set; }
    //    public string NamaCabang { get; set; }
    //    public string JenisNasabah { get; set; }
    //    public string NamaNasabah { get; set; }
    //    public string NamaBPKB { get; set; }
    //    public string NamaClient { get; set; }
    //    public string NamaNotaris { get; set; }
    //    public string KodeAkta { get; set; }
    //    public string NoAkta { get; set; }
    //    public DateTime TglAkta { get; set; }
    //    public TimeSpan PukulAkta { get; set; }
    //    public string SifatAkta { get; set; }
    //    public string KodeNotaris { get; set; }
    //    public string KodeClient { get; set; }
    //    public string namaNotaris { get; set; }
    //    public string namaClient { get; set; }
    //    public int TotalAkta { get; set; }
    //    public int JumlahNoAkta { get; set; }
    //    public bool ISDOWNLOAD_SALINAKTA { get; set; }
    //    public bool ISDOWNLOAD_MINUTA { get; set; }
    //    public bool IsGenerateAkta { get; set; }
    //    public string PICAkta { get; set; }
    //    //public string CLIENT_FDC_ID { get; set; }
    //}



    //[Serializable]
    //public class caktaTaxNtry
    //{

    //    public Int64 Rang { get; set; }
    //    public string IdNotaris { get; set; }
    //    public string NamaNotaris { get; set; }
    //    public string Periode { get; set; }
    //    public int JmlAkta { get; set; }
    //    public decimal GrossIncome { get; set; }
    //    public decimal PKP50Persen { get; set; }
    //    public decimal PKPKomulatif { get; set; }
    //    public decimal PPHTarifPersen { get; set; }
    //    public decimal PPHTerhutang { get; set; }
    //}



    //[Serializable]
    //public class cSummaryAkta
    //{
    //    public string NotarisID { get; set; }
    //    public string ClientID { get; set; }
    //    public int bln1 { get; set; }
    //    public int bln2 { get; set; }
    //    public int bln3 { get; set; }
    //    public int bln4 { get; set; }
    //    public int bln5 { get; set; }
    //    public int bln6 { get; set; }
    //    public int bln7 { get; set; }
    //    public int bln8 { get; set; }
    //    public int bln9 { get; set; }
    //    public int bln10 { get; set; }
    //    public int bln11 { get; set; }
    //    public int bln12 { get; set; }
    //}

    //[Serializable]
    //public class cSertifikatAkta
    //{
    //    public string MERK_KENDARAN { get; set; }
    //    public string TYPE_KENDARAN { get; set; }
    //    public string JENIS_KENDARAAN { get; set; }
    //    public string WARNA_KENDARAN { get; set; }
    //    public string NOMOR_RANGKA { get; set; }
    //    public string NOMOR_MESIN { get; set; }
    //    public string KONDISI_KENDARAAN { get; set; }
    //    public string NILAI_POKOK_HUTANG { get; set; }
    //    public string NILAI_PENJAMINAN { get; set; }
    //    public decimal NILAI_OBJECT { get; set; }
    //    public string TAHUN_PEMBUATAN { get; set; }

    //    public DateTime? TGL_AKTA { get; set; }
    //    public string NO_AKTA { get; set; }
    //    public string DAY_AKTA { get; set; }
    //    public string PUKUL_AKTA { get; set; }
    //    public string NOTARIS_ID { get; set; }

    //    public string CUST_NAME { get; set; }
    //    public string CUST_PLCBIRTH { get; set; }
    //    public DateTime? CUST_BIRTHDATE { get; set; }
    //    public string CUST_JOB { get; set; }
    //    public string CUST_CITIZIEN { get; set; }
    //    public string CUST_CITY { get; set; }
    //    public string CUST_ADDRESS { get; set; }
    //    public string CUST_NBGNO { get; set; }
    //    public string CUST_HMLNO { get; set; }
    //    public string CUST_URBNVILLAGE { get; set; }
    //    public string CUST_SUBDSC { get; set; }
    //    public string CUST_DSCT { get; set; }
    //    public string CUST_IDTTYPE { get; set; }
    //    public string CUST_IDTNO { get; set; }

    //    public string DEBT_NAME { get; set; }
    //    public string DEBT_PLCBIRTH { get; set; }
    //    public DateTime? DEBT_BIRTHDATE { get; set; }
    //    public string DEBT_JOB { get; set; }
    //    public string DEBT_CITIZIEN { get; set; }
    //    public string DEBT_CITY { get; set; }
    //    public string DEBT_ADDRESS { get; set; }
    //    public string DEBT_NBGNO { get; set; }
    //    public string DEBT_HMLNO { get; set; }
    //    public string DEBT_URBANVILLAGE { get; set; }
    //    public string DEBT_SUBDSC { get; set; }
    //    public string DEBT_DSCT { get; set; }
    //    public string DEBT_IDTTYPE { get; set; }
    //    public string DEBT_IDTNO { get; set; }

    //    public string NTRY_STAFF_NAME { get; set; }
    //    public string NTRY_STAFF_TITLE { get; set; }
    //    public string NTRY_STAFF_BIRTHPLACE { get; set; }
    //    public string NTRY_STAFF_BIRTHDATE { get; set; }
    //    public string NTRY_STAFF_JOBTITLE { get; set; }
    //    public string NTRY_STAFF_CITY { get; set; }
    //    public string NTRY_STAFF_CITIZIEN { get; set; }
    //    public string NTRY_STAFF_ADDRESS { get; set; }
    //    public string NTRY_STAFF_NBGNO { get; set; }
    //    public string NTRY_STAFF_HMLNO { get; set; }
    //    public string NTRY_STAFF_URBVILLAGE { get; set; }
    //    public string NTRY_STAFF_SUBDSC { get; set; }
    //    public string NTRY_STAFF_IDTNO { get; set; }
    //    public string NTRY_STAFF_PLCASS { get; set; }

    //    public string CONT_NO { get; set; }
    //    public DateTime? CONT_DATE { get; set; }

    //    public string FILE_NAME { get; set; }
    //    public string NTRY_ID { get; set; }

    //    public string DESC_DEED_DATE { get; set; }
    //    public string DESC_CUST_BIRTHDATE { get; set; }
    //    public string DESC_CONT_DATE { get; set; }
    //    public string DESC_GRTE_BIRTHDATE { get; set; }
    //    public string DESC_NILAIPEMBIAYAAN { get; set; }
    //    public string DESC_NILAIPENJAMINAN { get; set; }

    //}

}
