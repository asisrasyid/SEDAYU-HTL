using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DusColl
{


    [Serializable]
    public enum cCommandTextMaster
    {


        [Description("api/FDCMMaster/dbSaveProvin")]
        cmdSaveProvin = 2,
        [Description("api/FDCMMaster/dbGetProvinList")]
        cmdGetProvinList = 1,
      

        [Description("api/FDCMMaster/dbSaveProvinAhu")]
        cmdSaveProvinAhu = 777,
        [Description("api/FDCMMaster/dbGetProvinAhuList")]
        cmdGetProvinAhuList= 999,

        [Description("api/FDCMMaster/dbSaveSharePIC")]
        cmdSaveSharePIC = 1212,
        [Description("api/FDCMMaster/dbGetSharePICList")]
        cmdGetSharePICList = 1111,
        [Description("api/FDCMMaster/dbDelSharePIC")]
        cmdDelSharePIC = 1313,

        [Description("api/FDCMMaster/dbSaveShareNotaris")]
        cmdSaveShareNotaris = 12,
        [Description("api/FDCMMaster/dbGetShareNotarisList")]
        cmdGetShareNotarisList = 11,
        [Description("api/FDCMMaster/dbDelShareNotaris")]
        cmdDelShareNotaris = 13,


        [Description("api/FDCMMaster/dbSaveNotaris")]
        cmdSaveNotaris = 120,
        [Description("api/FDCMMaster/dbGetNotarisList")]
        cmdGetNotarisList = 110,
        [Description("api/FDCMMaster/dbDelNotaris")]
        cmdDelNotaris = 130,

        [Description("api/FDCMMaster/dbSaveNotarisStaff")]
        cmdSaveNotarisStaff = 122,
        [Description("api/FDCMMaster/dbGetNotarisStaffList")]
        cmdGetNotarisStaffList = 101,
        [Description("api/FDCMMaster/dbDelNotarisStaff")]
        cmdDelNotarisStaff = 132,
        
        [Description("api/FDCMMaster/dbSavePenghadapPT")]
        cmdSavePenghadapPT = 1221,
        [Description("api/FDCMMaster/dbGetPenghadapPTList")]
        cmdGetPenghadapPTList = 1121,
        [Description("api/FDCMMaster/dbDelPenghadapPT")]
        cmdDelPenghadapPT= 1321,

        [Description("api/FDCMMaster/dbSaveklien")]
        cmdSaveklien = 82222,
        [Description("api/FDCMMaster/dbGetklienList")]
        cmdGetklienList = 812211,
        [Description("api/FDCMMaster/dbDelklien")]
        cmdDelklien = 83232,

        [Description("api/FDCMMaster/dbSavebranch")]
        cmdSavebranch = 12222,
        [Description("api/FDCMMaster/dbGetbranchList")]
        cmdGetbranchList = 112211,
        [Description("api/FDCMMaster/dbDelbranch")]
        cmdDelbranch = 13232,

        [Description("api/FDCMMaster/dbGetjurnalcoaList")]
        cmdGetjurnalcoaList = 212211,
        [Description("api/FDCMMaster/dbSavejurnalcoa")]
        cmdSavejurnalcoa = 22222,
        [Description("api/FDCMMaster/dbDeljurnalcoa")]
        cmdDeljurnalcoa = 23232,

        [Description("api/FDCMMaster/dbGetjurnalmapList")]
        cmdGetjurnalmapList = 212212,
        [Description("api/FDCMMaster/dbSavejurnalmap")]
        cmdSavejurnalmap = 22223,
        [Description("api/FDCMMaster/dbDeljurnalmap")]
        cmdDeljurnalmap = 23233,

    }

   

  

}