using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DusColl
{

    [Serializable]
    public enum cCommandTextPemberkasan
    {


        [Description("api/FDCMPemberkasan/dbGetPemberkasanlist")]
        cmdGetPemberkasanlist = 2,

        [Description("api/FDCMPemberkasan/dbGetPemberkasanget")]
        cmdGetPemberkasanget = 4,
        
        [Description("api/FDCMPemberkasan/dbdGetPemberkasanstatusupd")]
        cmdGetPemberkasanstatusupd = 5,

        [Description("api/FDCMPemberkasan/dbGetPemberkasanget4map")]
        cmdGetPemberkasanget4map = 41,

        [Description("api/FDCMPemberkasan/dbGetAktaRegisList")]
        cmdGetAktaRegisList = 2000,

        [Description("api/FDCMPemberkasan/dbGetRptPemberkasan")]
        cmdGetRptPemberkasan = 2001,


        [Description("api/DIS/GetPemberkasanUplodSve")]
        cmdGetPemberkasansveuplod = 6,

        [Description("api/DIS/GetPemberkasanMap")]
        cmdGetPemberkasanmap= 7,

        [Description("api/DIS/GetPemberkasandocview")]
        cmdGetPemberkasandocview = 8,

        [Description("api/DIS/GetPemberkasanMapSve")]
        cmdGetSveDocMapping= 9,


        [Description("api/DIS/GetPemberkasanMapDel")]
        cmdGetDelDocMapping = 91,

        [Description("api/DIS/GetPemberkasandoc4ZipNotaris")]
        cmdGetPemberkasandoc4ZipNotaris = 80,

        //[Description("udp_app_pemberkasanakta_send_req")]
        //cmdGetpemberkasanAktagUpl = 177,


    }

   
}