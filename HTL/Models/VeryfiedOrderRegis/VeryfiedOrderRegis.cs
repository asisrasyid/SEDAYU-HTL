using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DusColl
{


    [Serializable]
    public enum cCommandTextVeryfiedOrderRegis
    {
        
        
        [Description("api/FDCMVeryfiedOrderRegis/dbGetVeryfiedOrderRegList")]
        cmdGetVeryfiedOrderRegList = 1,

        [Description("api/FDCMVeryfiedOrderRegis/dbGetVeryfiedOrderRegListdsh")]
        cmdGetVeryfiedOrderRegListdsh = 2,

        [Description("api/FDCMVeryfiedOrderRegis/dbGetVeryfiedExport")]
        cmdGetVeryfiedExport = 3,

        [Description("api/FDCMVeryfiedOrderRegis/dbGetVeryfiedupd")]
        cmdGetVeryfiedupd = 4,

    }


}
