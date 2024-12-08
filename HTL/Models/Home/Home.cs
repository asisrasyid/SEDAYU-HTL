using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DusColl
{

    [Serializable]
    public enum cCommandTextHome
    {
        [Description("api/FDCMHome/dbGetkontrakcalender")]
        cmdGetkontrakcalender = 0,
    }
}