using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DusColl
{


    [Serializable]
    public enum cCommandTextTrackingOrder
    {

        [Description("api/FDCMTrackingOrder/dbGetTrackingOrderRegList")]
        cmdGetTrackingOrderRegList = 1,

        [Description("api/FDCMTrackingOrder/dbupdatePICAHU")]
        cmdupdatePICAHU = 4,

    }

  
   

}
