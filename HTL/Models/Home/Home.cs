using System;
using System.ComponentModel;

namespace DusColl
{
    [Serializable]
    public enum cCommandTextHome
    {
        [Description("api/FDCMHome/dbGetkontrakcalender")]
        cmdGetkontrakcalender = 0,
    }
}