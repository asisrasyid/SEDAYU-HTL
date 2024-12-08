using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DusColl
{

    [Serializable]
    public enum cCommandTextOrder
    {

        [Description("api/FDCMOrder/dbGetOrderList")]
        cmdGetOrderList = 1,


        [Description("api/FDCMOrder/dbGetDetailOrderList")]
        cmdGetDetailOrderList = 3,

        [Description("api/FDCMOrder/dbSearchOrderInfoCount")]
        cmdSearchOrderInfoCount = 4,
        
        [Description("api/FDCMOrder/dbSearchOrderListCreate")]
        cmdSearchOrderListCreate =6,

        [Description("api/FDCMOrder/dbSaveOrder")]
        cmdSaveOrder = 7,

        [Description("api/FDCMOrder/dbShareOrderNotaris")]
        cmdShareOrderNotaris = 8,

    }

}