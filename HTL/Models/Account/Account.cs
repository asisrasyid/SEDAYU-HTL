using Newtonsoft.Json;
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