using System;
using System.ComponentModel;

namespace DusColl
{
    [Serializable]
    public enum cCommandTextCommon
    {
        //[Description("udp_com_app_docupload_flag_getbyno")]
        //cmdSettDocumentFlagBynot = 0,

        //[Description("udp_com_app_docupload_getbyno")]
        //cmdGetDocumentByno = 1,

        [Description("api/FDCMCommon/dbddlgetparamenumsList")]
        cmdddlgetparamenumsList = 2,

        [Description("api/FDCMCommon/dbGetDdlRegionListByEncrypt")]
        cmdGetDdlRegionListByEncrypt = 3,

        [Description("api/FDCMCommon/dbGetClientListByEncrypt")]
        cmdGetClientListByEncrypt = 4,

        [Description("api/FDCMCommon/dbGetUserRegisAhuListByEncrypt")]
        cmdGetUserRegisAhuListByEncrypt = 5,

        [Description("api/FDCMCommon/dbGetNotarisListByEncrypt")]
        cmdGetNotarisListByEncrypt = 6,

        [Description("api/FDCMCommon/dbGetNotarisList")]
        cmdGetNotarisList = 7,

        [Description("api/FDCMCommon/dbdbGetDdlBranchListByEncrypt")]
        cmddbGetDdlBranchListByEncrypt = 8,

        [Description("api/FDCMCommon/dbdbGetDdlBranchListByEncryptDT")]
        cmddbGetDdlBranchListByEncryptDT = 88,

        [Description("api/FDCMCommon/dbGetConfig")]
        cmdGetConfig = 9,

        [Description("api/FDCMCommon/dbSetHostHistory")]
        SetHostHistory = 10,

        [Description("api/FDCMCommon/dbSetOTPCode")]
        cmdSetOTPCode = 11,

        [Description("api/FDCMCommon/dbCheckOTPCode")]
        cmdCheckOTPCode = 12,

        [Description("api/FDCMCommon/dbGetProvinsList")]
        cmdGetProvinsList = 13,

        //[Description("udp_com_app_document_get")]
        //cmdGeDocument = 14,

        [Description("api/FDCMCommon/dbGeJenisDocument")]
        cmdGeJenisDocument = 15,

        [Description("api/FDCMCommon/dbGeAHUprovin")]
        cmdGeAHUprovin = 16,

        [Description("api/FDCMCommon/dbGetCoa")]
        cmdGetCoa = 17,

        [Description("api/FDCMCommon/dbGeAHUPengadilanNegeri")]
        cmdGeAHUPengadilanNegeri = 160,
    }
}