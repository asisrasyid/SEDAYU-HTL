using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DusColl
{

    [Serializable]
    public enum cCommandTextPendaftaran
    {
        
        [Description("api/FDCMPendaftaran/dbGetPendaftaranList")]
        cmdGetPendaftaranList = 1,

        [Description("api/FDCMPendaftaran/dbGetRptPendaftaran")]
        cmdGetRptPendaftaran = 2,

        [Description("api/FDCMPendaftaran/dbGetRptPendaftaranPNBPOut")]
        cmdGetRptPendaftaranPNBPOut = 3,

        [Description("api/FDCMPendaftaran/dbGetRptPendaftaranSLA")]
        cmdGetRptPendaftaranSLA = 4,

        [Description("api/FDCMPendaftaran/dbGetRptPengirimanBerkasPendaftaranMonthTodate")]
        cmdGetRptPengirimanBerkasPendaftaranMonthTodate = 5,

        //[Description("udp_app_pendaftaran_resend")]
        [Description("api/FDCMPendaftaran/dbGetResendPendaftaran")]
        cmdGetResendPendaftaran = 9,

        [Description("api/FDCMPendaftaran/dbSetPendaftaranMnl4Temp")]
        cmdSetPendaftaranMnl4Temp = 91,

        [Description("api/FDCMPendaftaran/dbSetPendaftaranMnl4TempPaid")]
        cmdSetPendaftaranMnl4TempPaid = 911,

        //[Description("udp_app_pendaftaran_import_iptsve")]
        //cmdGetPendaftaranOrdersve = 7,
        //[Description("udp_app_pendaftaran_reorder_ahu")]
        //cmdGetPendaftaranReOrderAHU = 9,

    }
}
