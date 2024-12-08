using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DusColl
{



    [Serializable]
    public class cRoyaList
    {

        public string KodeCabang { get; set; }

        public string NoPerjanjian { get; set; }
        public string Jenis_Roya { get; set; }

        public string Nama_PemberiFidusia { get; set; }
        public string Identitas_PemberiFidusia { get; set; }
        public string ContactNo_PemberiFidusia { get; set; }
        public string Alamat_PemberiFidusia { get; set; }
        public string KodePos_PemberiFidusia { get; set; }
        public string Propinsi_PemberiFidusia { get; set; }
        public string Kota_PemberiFidusia { get; set; }
        public string Kecamatan_PemberiFidusia { get; set; }
        public string Kelurahan_PemberiFidusia { get; set; }

        public string RT_PemberiFidusia { get; set; }
        public string RW_PemberiFidusia { get; set; }

        public string Amount_PokokHutang { get; set; }
        public string Amount_OTR { get; set; }
        public string Amount_Penjamin { get; set; }

        public string Tanggal_Roya { get; set; }
        public string Nomor_Sertifikat { get; set; }
        public string Tanggal_Sertifikat { get; set; }

        public string Nomor_Akta { get; set; }
        public string Tanggal_Akta { get; set; }

        public string Nama_Notaris { get; set; }
        public string Fidusia_Online_ID { get; set; }

    }


    [Serializable]
    public class cRoya
    {

        public string KeyLookupdata{ get; set; }

        public string SecureRoyaID { get; set; }
        public string secID_FDC { get; set; }

        public string secureContractNo { get; set; }
        public string secureSertifikate { get; set; }
        public string varJenisDocument { get; set; }

        public int RoyaID { get; set; }
        public string RoyaNumber { get; set; }

        public int Status { get; set; }

        public string StatusDesc { get; set; }


        [Required(ErrorMessage = "Data harus terisi")]
        public DateTime? TanggalPenghapusan { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string RoyaCause { get; set; }


        [Required(ErrorMessage = "Data harus terisi")]
        public string RoyaType { get; set; }


        [Required(ErrorMessage = "Data harus terisi")]
        public string NoPerjanjian { get; set; }

        public DateTime TglPerjanjian { get; set; }

        //pemberi fiducia//

        [Required(ErrorMessage = "Data harus terisi")]
        public string JenisPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string NamaPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string NPWP_SK_NIKPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "No Kontak tidak benar.")]
        [StringLength(12, ErrorMessage = "No Kontak panjang harus 11 - 12 digit.", MinimumLength = 11)]
        public string ContactNumberPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string AlamatPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string KodePosPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KotaPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string Kabupaten_KotaPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KabupatenPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KecamatanPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KelurahanPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string RTPemberi { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string RWPemberi { get; set; }

        //pemberi fiducia//


        //penerima fiducia //
        [Required(ErrorMessage = "Data harus terisi")]
        public string JenisPenerima { get; set; }


        [Required(ErrorMessage = "Data harus terisi")]
        public string NamaPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string NPWP_SK_NIKPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "No Kontak tidak benar.")]
        [StringLength(12, ErrorMessage = "No Kontak panjang harus 11 - 12 digit.", MinimumLength = 11)]
        public string ContactNumberPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string AlamatPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string KodePosPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KotaPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string Kabupaten_KotaPenerima { get; set; }


        [Required(ErrorMessage = "Data harus terisi")]
        public string KabupatenPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KecamatanPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KelurahanPenerima { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string RTPenerima { get; set; }


        [Required(ErrorMessage = "The Address field is required")]
        [RegularExpression(@"^^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Data harus terisi angka")]
        public string RWPenerima { get; set; }

        //penerima Fiducis//


        // Nilai Jaminan Fiducia //

        //[Required(ErrorMessage = "Data harus terisi")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Must be money.")]
        public decimal NilaiHutang { get; set; }
        //[Required(ErrorMessage = "Data harus terisi")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Must be money.")]
        public decimal NilaiPenjaminan { get; set; }

        // Nilai Jaminan Fiducia //


        // Akta Notaris Fiducia //
        [Required(ErrorMessage = "Data harus terisi")]
        public string NoAkta { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string NamaAkta { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public DateTime TanggalAkta { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string NamaNotarisakta { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KedudukanNotarisakta { get; set; }
        // Akta Notaris Fiducia //



        // Data Centralisasi//
        [Required(ErrorMessage = "Data harus terisi")]
        public string IDNotaris { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string NamaNotaris { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KedudukanNotaris { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string NoSertifikat { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public DateTime TanggalSertifikat { get; set; }

        // [Required(ErrorMessage = "Data harus terisi")]
        public string WaktuSertifikat { get; set; }
        // Data Centralisasi//


        public DateTime? TanggalRequestRoya { get; set; }


        public string NoRoyaSertifikat { get; set; }

        public DateTime? TanggalRoyaSertifikat { get; set; }

        // Data DeCentralisasi//
        [Required(ErrorMessage = "Data harus terisi")]
        public string IDNotarisLama { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string NamaNotarisLama { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string KedudukanNotarisLama { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public string NoSertifikatLama { get; set; }

        [Required(ErrorMessage = "Data harus terisi")]
        public DateTime TanggalSertifikatLama { get; set; }

        public string WaktuSertifikatLama { get; set; }
        // Data DeCentralisasi//


    }
    [Serializable]
    public class cFilter
    {

        public string idcaption { get; set; }
        public string NoPerjanjian { get; set; }
        public string NoSertifikat { get; set; }
        public String fromdate { get; set; }
        public String todate { get; set; }
        public String SelectRoyaType { get; set; }
        public String SelectRoyaStatus { get; set; }
        public String SelectRoyaTypeDesc { get; set; }
        public String SelectRoyaStatusDesc { get; set; }
        public int UserType { get; set; }
        public int UserTypeApps { get; set; }
        public string pukulakta { get; set; }
        public string SelectNotaris { get; set; }
        public string SelectNotarisDesc { get; set; }
        public string SelectClient { get; set; }
        public string SelectClientDesc { get; set; }
        public string SelectBranch { get; set; }
        public string SelectBranchDesc { get; set; }
        public string ClientLogin { get; set; }
        public string NotaryLogin { get; set; }
        public string CabangLogin { get; set; }
        public string SelectCabang { get; set; }
        public string SelectContractStatus { get; set; }
        public string SelectContractStatusDesc { get; set; }
        public int PageNumber { get; set; } = 1;
        public int OutstandingOrder { get; set; }
        public double PageSize { get; set; }
        public double TotalPage { get; set; }
        public double TotalRecord { get; set; }
        public bool isdownload { get; set; } = false;
        public bool isModeFilter { get; set; } = false;
        public double pagingsizeclient { get; set; }
        public double pagenumberclient { get; set; }
        public double totalPageclient { get; set; }
        public double totalRecordclient { get; set; }
    }

    [Serializable]
    public class cDouemntsroya : cRoya
    {

        public int ID_UPLOAD { get; set; }
        public String JENIS_DOCUMENT { get; set; }
        public String FILE_NAME { get; set; }
        public String CONTENT_TYPE { get; set; }
        public decimal CONTENT_LENGTH { get; set; }
        public Byte[] FILE_BYTE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public String USERID { get; set; }
        public String KB { get; set; }
        public String STATUSPERJANJIAN { get; set; }

    }
}
