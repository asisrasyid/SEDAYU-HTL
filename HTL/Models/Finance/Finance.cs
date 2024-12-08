using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DusColl
{

    [Serializable]
    public enum cCommandTextFinance
    {

        [Description("api/FDCMFinance/dbGetPiutangPayList")]
        cmdGetPiutangPayList = 1,

        [Description("api/FDCMFinance/dbGetBillPaymentupd")]
        cmdGetBillPaymentupd = 8,

        [Description("api/FDCMFinance/dbGetBillPaymentupdtxt")]
        cmdGetBillPaymentupdtxt = 9,

        [Description("api/FDCMFinance/dbGetPiutangPayListINV")]
        cmdGetPiutangPayListINV = 16,
        
        [Description("api/FDCMFinance/dbGetBillPaymentupdINV")]
        cmdGetBillPaymentupdINV = 17,

        [Description("api/FDCMFinance/dbGetPayListBNI")]
        cmdGetPayListBNI = 1600,

        [Description("api/FDCMFinance/dbGetPayupdBNI")]
        cmdGetPayupdBNI = 1700,

        [Description("api/FDCMFinance/dbGetFakturRegisList")]
        cmdGetFakturRegisList = 160,

        [Description("api/FDCMFinance/dbGetFakturRegisupd")]
        cmddbGetFakturRegisupd = 170,


        [Description("api/FDCMFinance/dbGetAccountingList")]
        cmdGetAccountingList = 166,

        [Description("api/FDCMFinance/dbGetAccountingUpl")]
        cmdGetAccountingUpl = 177,

        [Description("api/FDCMFinance/dbGetAccountingJurnal")]
        cmdGetAccountingJurnal = 178,

        [Description("api/FDCMFinance/dbGetRptPiutangReg")]
        cmdGetRptPiutangReg = 2,

        [Description("api/FDCMFinance/dbGetRptClaimBaseReg")]
        cmdGetRptClaimBaseReg = 3,

        [Description("api/FDCMFinance/dbGetRptBillingCreateReg")]
        cmdGetRptBillingCreateReg = 5,

        [Description("api/FDCMFinance/dbGetRptBillingCreateRegDetail")]
        cmdGetRptBillingCreateRegDetail = 6,


        [Description("api/FDCMFinance/dbGetRptBillIDRegAHU")]
        cmdGetRptBillIDRegAHU = 99,

        //[Description("udp_app_finance_invoice_create")]
        //cmdGetRptBillCRET= 999,

        [Description("api/FDCMFinance/dbgetcombinetxtbni")]
        cmdgetcombinetxtbni = 999988,


        [Description("api/FDCMFinance/dbGetRptRlaba")]
        cmdGetRptRlaba = 9999,

        [Description("api/FDCMFinance/dbGetRptNeraca")]
        cmdGetRptNeraca = 88999,

    }
}