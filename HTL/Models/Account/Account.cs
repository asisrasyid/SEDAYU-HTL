using System;
using System.ComponentModel;

namespace DusColl
{
    [Serializable]
    public enum cCommandTextAuthAcct
    {
        [Description("api/FDCMAuth/dbAuthenticateUser")]
        cmdAuthenticateUser = 0,

        [Description("api/FDCMAuth/dbAuthenticateGrop")]
        cmdAuthenticateGrop = 1,

        [Description("api/FDCMAuth/dbAuthenticateMetrik")]
        cmdAuthenticateMetrik = 4,
    }
}