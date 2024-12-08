using HashNetFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DusColl
{

    [Serializable]
    public enum cCommandTextPendingTask
    {

        [Description("api/FDCMPendingTask/dbGetPendingTaskList")]
        cmdGetPendingTaskList = 1,
        
        [Description("api/FDCMPendingTask/dbupdatePendTaskDoc")]
        cmdupdatePendTaskDoc = 4,

        [Description("api/FDCMPendingTask/dbGetPendingTaskGet")]
        cmdGetPendingTaskGet = 7,

        [Description("api/FDCMPendingTask/dbGetPendingTaskFollowChecked")]
        cmdGetPendingTaskFollowChecked = 77,

        [Description("api/FDCMPendingTask/dbGetPendingTaskListExport")]
        cmdGetPendingTaskListExport = 8,

        [Description("api/FDCMPendingTask/dbGetPendingTaskFollowInvoice")]
        cmdGetPendingTaskFollowInvoice= 8,
        


    }

   
}