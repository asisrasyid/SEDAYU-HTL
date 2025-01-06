var elemtid = "body";
var elemntupload = ".modal-content";

var vmFinance = function () {
    var onOpenAdd = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplod",
            type: "POST",
            data: {},
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $("#filterdatadialogpaymentbilluplod").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onReqCnl = function (varkeylookup) {
        var jsoncoll = "";
        var jsonreposn = "";
        var kodepol = varkeylookup;
        swal({
            title: "Konfirmasi",
            text: "Yakin ingin Membatalkan Pengajuan, Jika anda sudah yakin silahkan tekan tombol 'Ya'",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: "Finance/clnPaymentRegisrejt",
                        type: "POST",
                        data: { kelookup: kodepol },
                        //contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.moderror == false) {
                                App.unblockUI();
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    if (data.result == "1") { $("#gridbillingpaymentlist").html(data.view); }
                                });
                            } else {
                                window.location.href = data.url;
                            }
                        },
                        error: function (x, y, z) {
                            App.unblockUI(elemntupload);
                            jsoncoll = JSON.stringify(x);
                            jsonreposn = JSON.parse(jsoncoll);
                            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                        }
                    });
                }
            });
    };
    var onOpenFilter = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenFilterpop",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();

                    $("#uidivfilter").html(data.view);

                    $('.date-picker').datepicker({
                        rtl: App.isRTL(),
                        orientation: "left",
                        autoclose: true
                    });
                    $('.select2').select2();
                    $("#SelectRequest").val(data.opsi1).trigger("change");
                    $("#SelectRequestStatus").val(data.opsi2).trigger("change");
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);

                    $("#filterdatadialog").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onApplyFilter = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var frm = $(parForm);
        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize() + "&download=" + pardownloadexcel,
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
            },
            success: function (data) {
                if (data.moderror == false) {
                    if (data.message == "") {
                        if (data.download == "") {
                            $("#gridbillingpaymentlist").html(data.view);
                            $("#table_List_billing_payment_upload").dataTable();
                            $("#filterdatadialog").modal("hide");
                        } else {
                            var download = document.getElementById('link');
                            download.href = 'data:application/octet-stream;base64,' + data.datafile;
                            download.click();
                        }
                    } else {
                        $("#filterdatadialog").modal("hide");
                        swal({
                            title: "Informasi",
                            text: data.message,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        }, function () {
                            $("#filterdatadialog").modal("show");
                        });
                    }
                    App.unblockUI(elemntupload);
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            },
        });
    };
    var onResetFilter = function (parForm) {
        var frm = $(parForm);
        $(frm)[0].reset();
        $('#filterdatadialog .select2').val('').trigger("change");
    };

    var onOpenAddInv = function (param) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplodINV",
            type: "POST",
            data: { gontok: param },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $(".jenser").html("PENGECEKAN SERTIPIKAT (NO.)");
                    $("#filterdatadialogpaymentbilluplodINV").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };

    var onOpenAddInvpaid = function (param) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplodINVPD",
            type: "POST",
            data: { gontok: param },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    vmFinance.init();
                    $("#filterdatadialogpaymentbilluplodINV").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };

    var onAddInvGEN = function (param2, param1) {
        var jsoncoll = "";
        var jsonreposn = "";

        var form = $('#billpayment_form');
        var token = $('input[name="__RequestVerificationToken"]', form).val();

        $.ajax({
            url: "Finance/clnPaymentRegisINV",
            type: "POST",
            data: { __RequestVerificationToken: token, JenisTransaksi: param1, gontok: param2 },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (x) {
                App.unblockUI();
                if (x.moderror == false) {
                    $("#filterdatadialogpaymentbilluplodINV").modal("hide");
                    swal({
                        title: "Informasi",
                        text: x.msg,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    }, function () {
                        if (x.result == 1) {
                            $("#gridbillingpaymentlist").html(x.view);
                            if (x.typo == "createinvoice") {
                                var a = document.createElement("a");
                                document.body.appendChild(a);
                                a.style = "display: block";
                                var bytes = new Uint8Array(x.bytetyipe);
                                var MIME_TYPE = "application/pdf";
                                var blob = new Blob([bytes], { type: MIME_TYPE });
                                var url = window.URL.createObjectURL(blob);
                                a.href = url;
                                a.download = x.jnfle; //returnedData.filename;
                                a.click();
                            }
                        }
                    });
                } else {
                    window.location.href = x.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };

    var onOpenAddInvPAT = function (param) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplodINVPAT",
            type: "POST",
            data: { gontok: param },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $(".jenser").html("PENGECEKAN SERTIPIKAT (NO.)");
                    $("#filterdatadialogpaymentbilluplodINVPAT").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onAddInvPATGEN = function (param2, param1) {
        var jsoncoll = "";
        var jsonreposn = "";

        var form = $('#billpayment_form');
        var token = $('input[name="__RequestVerificationToken"]', form).val();

        $.ajax({
            url: "Finance/clnPaymentRegisINVPAT",
            type: "POST",
            data: { __RequestVerificationToken: token, JenisTransaksi: param1, gontok: param2 },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (x) {
                App.unblockUI();
                if (x.moderror == false) {
                    $("#filterdatadialogpaymentbilluplodINVPAT").modal("hide");
                    swal({
                        title: "Informasi",
                        text: x.msg,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    }, function () {
                        if (x.result == 1) {
                            if (x.typo == "createinvoice") {
                                parfrm = '<embed src=data:application/pdf;base64,' + x.bytetyipe + '#toolbar=0 width="100% " height="600px" id="fame"></embed>';
                                //parfrm = '<object><embed id="preview_pdf" src="data:application/pdf;base64,' + x.bytetyipe + '></<object>';
                                $("#gridbillingpaymentlist").html(parfrm);
                                $(".jenser").html(x.namatitle);
                                //$(".pdprint").unbind("click");
                                //$(".pdprint").bind("click", function () {
                                //    w = window.open(window.location.href, "Invoice No. : " + x.invo);
                                //    w.document.open();
                                //    w.document.write(parfrm);
                                //    w.document.close();
                                //});
                            }
                        }
                    });
                } else {
                    window.location.href = x.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };

    var onReqCnlInv = function (varkeylookup, oprprm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var kodepol = varkeylookup;

        swal({
            title: "Konfirmasi",
            text: "Yakin ingin Membatalkan Pengajuan, Jika anda sudah yakin silahkan tekan tombol 'Ya'",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: "Finance/clnPaymentRegisrejtINV",
                        type: "POST",
                        data: { kelookup: kodepol, ogenta: oprprm },
                        //contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.moderror == false) {
                                App.unblockUI();
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    if (data.result == "1") { $("#gridbillingpaymentlist").html(data.view); }
                                });
                            } else {
                                window.location.href = data.url;
                            }
                        },
                        error: function (x, y, z) {
                            App.unblockUI(elemntupload);
                            jsoncoll = JSON.stringify(x);
                            jsonreposn = JSON.parse(jsoncoll);
                            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                        }
                    });
                }
            });
    };
    var onOpenFilterInv = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenFilterpopINV",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();

                    $("#uidivfilter").html(data.view);

                    $('.date-picker').datepicker({
                        rtl: App.isRTL(),
                        orientation: "left",
                        autoclose: true
                    });
                    $('.select2').select2();
                    $("#SelectRequest").val(data.opsi1).trigger("change");
                    $("#SelectRequestStatus").val(data.opsi2).trigger("change");
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);

                    $("#filterdatadialog").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onApplyFilterInv = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var frm = $(parForm);
        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize() + "&download=" + pardownloadexcel,
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
            },
            success: function (data) {
                if (data.moderror == false) {
                    if (data.message == "") {
                        if (data.download == "") {
                            $("#gridbillingpaymentlist").html(data.view);
                            $("#table_List_billing_payment_upload").dataTable();
                            $("#filterdatadialog").modal("hide");
                        } else {
                            var download = document.getElementById('link');
                            download.href = 'data:application/octet-stream;base64,' + data.datafile;
                            download.click();
                        }
                    } else {
                        $("#filterdatadialog").modal("hide");
                        swal({
                            title: "Informasi",
                            text: data.message,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        }, function () {
                            $("#filterdatadialog").modal("show");
                        });
                    }
                    App.unblockUI(elemntupload);
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            },
        });
    };
    var onResetFilterInv = function (parForm) {
        var frm = $(parForm);
        $(frm)[0].reset();
        $('#filterdatadialog .select2').val('').trigger("change");
    };

    var onOpenAddFP = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplodFakturRegis",
            type: "POST",
            data: {},
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $("#filterdatadialogpaymentbilluplodINV").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onReqCnlFP = function (varkeylookup) {
        var jsoncoll = "";
        var jsonreposn = "";
        var kodepol = varkeylookup;
        swal({
            title: "Konfirmasi",
            text: "Yakin ingin Membatalkan Pengajuan, Jika anda sudah yakin silahkan tekan tombol 'Ya'",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: "Finance/clnFakturRegisrejt",
                        type: "POST",
                        data: { kelookup: kodepol },
                        //contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.moderror == false) {
                                App.unblockUI();
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    if (data.result == "1") { $("#gridbillingpaymentlist").html(data.view); }
                                });
                            } else {
                                window.location.href = data.url;
                            }
                        },
                        error: function (x, y, z) {
                            App.unblockUI(elemntupload);
                            jsoncoll = JSON.stringify(x);
                            jsonreposn = JSON.parse(jsoncoll);
                            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                        }
                    });
                }
            });
    };
    var onOpenFilterFP = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenFilterpopFakturRegis",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();

                    $("#uidivfilter").html(data.view);

                    $('.date-picker').datepicker({
                        rtl: App.isRTL(),
                        orientation: "left",
                        autoclose: true
                    });
                    $('.select2').select2();
                    $("#SelectRequest").val(data.opsi1).trigger("change");
                    $("#SelectRequestStatus").val(data.opsi2).trigger("change");
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);

                    $("#filterdatadialog").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onApplyFilterFP = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var frm = $(parForm);
        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize() + "&download=" + pardownloadexcel,
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
            },
            success: function (data) {
                if (data.moderror == false) {
                    if (data.message == "") {
                        if (data.download == "") {
                            $("#gridbillingpaymentlist").html(data.view);
                            $("#table_List_billing_payment_upload").dataTable();
                            $("#filterdatadialog").modal("hide");
                        } else {
                            var download = document.getElementById('link');
                            download.href = 'data:application/octet-stream;base64,' + data.datafile;
                            download.click();
                        }
                    } else {
                        $("#filterdatadialog").modal("hide");
                        swal({
                            title: "Informasi",
                            text: data.message,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        }, function () {
                            $("#filterdatadialog").modal("show");
                        });
                    }
                    App.unblockUI(elemntupload);
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            },
        });
    };
    var onResetFilterFP = function (parForm) {
        var frm = $(parForm);
        $(frm)[0].reset();
        $('#filterdatadialog #NoPerjanjian').val('');
        $('#filterdatadialog .select2').val('').trigger("change");
    };

    var onOpenAddMNL = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplodMNL",
            type: "POST",
            data: {},
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $("#filterdatadialogpaymentbilluplodMNL").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onReqApplyMNL = function (varkeylookup) {
        var jsoncoll = "";
        var jsonreposn = "";
        var kodepol = varkeylookup;
        swal({
            title: "Konfirmasi",
            text: "Yakin ingin Buat Pengajuan xxxxxxxxxxxxxxxx, Jika anda sudah yakin silahkan tekan tombol 'Ya'",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: "Finance/clnPaymentRegisMNL",
                        type: "POST",
                        data: { model: kodepol },
                        //contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.moderror == false) {
                                App.unblockUI();
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    if (data.result == "1") { $("#gridbillingpaymentlist").html(data.view); }
                                });
                            } else {
                                window.location.href = data.url;
                            }
                        },
                        error: function (x, y, z) {
                            App.unblockUI(elemntupload);
                            jsoncoll = JSON.stringify(x);
                            jsonreposn = JSON.parse(jsoncoll);
                            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                        }
                    });
                }
            });
    };

    var onOpenAddJur = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplodJUR",
            type: "POST",
            data: {},
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $("#filterdatadialogpaymentbilluplodJUR").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onReqCnlJur = function (varkeylookup) {
        var jsoncoll = "";
        var jsonreposn = "";
        var kodepol = varkeylookup;
        swal({
            title: "Konfirmasi",
            text: "Yakin ingin Membatalkan Pengajuan, Jika anda sudah yakin silahkan tekan tombol 'Ya'",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: "Finance/clnPaymentRegisrejtJUR",
                        type: "POST",
                        data: { kelookup: kodepol },
                        //contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.moderror == false) {
                                App.unblockUI();
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    if (data.result == "1") { $("#gridbillingpaymentlist").html(data.view); }
                                });
                            } else {
                                window.location.href = data.url;
                            }
                        },
                        error: function (x, y, z) {
                            App.unblockUI(elemntupload);
                            jsoncoll = JSON.stringify(x);
                            jsonreposn = JSON.parse(jsoncoll);
                            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                        }
                    });
                }
            });
    };
    var onOpenFilterJur = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenFilterpopJUR",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();

                    $("#uidivfilter").html(data.view);

                    $('.date-picker').datepicker({
                        rtl: App.isRTL(),
                        orientation: "left",
                        autoclose: true
                    });
                    $('.select2').select2();
                    $("#SelectRequest").val(data.opsi1).trigger("change");
                    $("#SelectRequestStatus").val(data.opsi2).trigger("change");
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);

                    $("#filterdatadialog").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onApplyFilterJur = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var frm = $(parForm);
        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize() + "&download=" + pardownloadexcel,
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
            },
            success: function (data) {
                if (data.moderror == false) {
                    if (data.message == "") {
                        if (data.download == "") {
                            $("#gridbillingpaymentlist").html(data.view);
                            $("#table_List_billing_payment_upload").dataTable();
                            $("#filterdatadialog").modal("hide");
                        } else {
                            var download = document.getElementById('link');
                            download.href = 'data:application/octet-stream;base64,' + data.datafile;
                            download.click();
                        }
                    } else {
                        $("#filterdatadialog").modal("hide");
                        swal({
                            title: "Informasi",
                            text: data.message,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        }, function () {
                            $("#filterdatadialog").modal("show");
                        });
                    }
                    App.unblockUI(elemntupload);
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            },
        });
    };
    var onResetFilterJur = function (parForm) {
        var frm = $(parForm);
        $(frm)[0].reset();
        $('#filterdatadialog .select2').val('').trigger("change");
    };

    var onOpenAddInvBNI = function (parampo) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenAddUplodINV",
            type: "POST",
            data: { gontok: parampo },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $("#filterdatadialogpaymentbilluplodINV").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onReqCnlInvBNI = function (varkeylookup, varkd) {
        var kodepol = varkeylookup;
        var kddkod = varkd;
        var pesanTxt = "";

        if (kddkod == "apr") {
            pesanTxt = "Yakin ingin Approve Pengajuan, Jika anda sudah yakin silahkan tekan tombol 'Ya'"
        }
        if (kddkod == "cfm") {
            pesanTxt = "Yakin Data Sudah Dibayarkan Ke BNI, Jika anda sudah yakin silahkan tekan tombol 'Ya'"
        }
        if (kddkod == "cnl") {
            pesanTxt = "Yakin ingin Membatalkan Pengajuan, Jika anda sudah yakin silahkan tekan tombol 'Ya'"
        }

        swal({
            title: "Konfirmasi",
            text: pesanTxt,
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: "Finance/clnPaymentProsesBNI",
                        type: "POST",
                        data: { kelookup: kodepol, kodok: kddkod },
                        //contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.moderror == false) {
                                App.unblockUI();
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    if (data.result == "1") { $("#gridbillingpaymentlist").html(data.view); }
                                });
                            } else {
                                window.location.href = data.url;
                            }
                        },
                        error: function (x, y, z) {
                            App.unblockUI(elemntupload);
                            jsoncoll = JSON.stringify(x);
                            jsonreposn = JSON.parse(jsoncoll);
                            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                        }
                    });
                }
            });
    };
    var onOpenFilterInvBNI = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Finance/clnOpenFilterpopBNI",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();

                    $("#uidivfilter").html(data.view);

                    $('.date-picker').datepicker({
                        rtl: App.isRTL(),
                        orientation: "left",
                        autoclose: true
                    });
                    $('.select2').select2();
                    $("#SelectRequest").val(data.opsi1).trigger("change");
                    $("#SelectRequestStatus").val(data.opsi2).trigger("change");
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);

                    $("#filterdatadialog").modal("show");
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };
    var onApplyFilterInvBNI = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var frm = $(parForm);
        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize() + "&download=" + pardownloadexcel,
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
            },
            success: function (data) {
                if (data.moderror == false) {
                    if (data.message == "") {
                        if (data.download == "") {
                            $("#gridbillingpaymentlist").html(data.view);
                            $("#table_List_billing_payment_upload").dataTable();
                            $("#filterdatadialog").modal("hide");
                        } else {
                            var download = document.getElementById('link');
                            download.href = 'data:application/octet-stream;base64,' + data.datafile;
                            download.click();
                        }
                    } else {
                        $("#filterdatadialog").modal("hide");
                        swal({
                            title: "Informasi",
                            text: data.message,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        }, function () {
                            $("#filterdatadialog").modal("show");
                        });
                    }
                    App.unblockUI(elemntupload);
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemntupload);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            },
        });
    }; onCetakINV
    var onResetFilterInvBNI = function (parForm) {
        var frm = $(parForm);
        $(frm)[0].reset();
        $('#filterdatadialog .select2').val('').trigger("change");
    };

    var OnReCombine = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("rcom");

        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih Nama File Terlebih dahulu',
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
            return false;
        }

        btndownload.click();
    };

    var onChecklist = function (status, idname) {
        if (status == true) {
            $("input:checkbox[name='" + idname + "']:unchecked").each(function (i, o) {
                $("input:checkbox[name='" + idname + "']")[i].checked = true;
            });
        } else {
            $("input:checkbox[name='" + idname + "']:checked").each(function (i, o) {
                $("input:checkbox[name='" + idname + "']")[i].checked = false;
            });
        }
    };

    var onCetakINV = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("");
        $("#gontok").val(codeprev);

        //var form = $('#billpayment_form');
        //var token = $('input[name="__RequestVerificationToken"]', form).val();

        var table = $('#table_List_billing_payment_upload').DataTable();

        var length = table.page.info().recordsTotal;
        var noinv = $("#NoPengajuanRequest").val();

        var tit = (noinv == "") ? "Konfirmasi" : " Informasi";
        var txt = (noinv == "") ? "Apakah anda yakin untuk membuat invoice?" : "Apakah anda yakin untuk menampilkan rekap data tagihan No." + noinv + "?";
        var tpe = (noinv == "") ? "warning" : "info";

        if (length > 0) {
            swal({
                title: tit,
                text: txt,
                type: tpe,
                showCancelButton: true,
                confirmButtonText: 'Ya',
                cancelButtonText: "Tidak",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        btndownload.click();
                    } else {
                        return false;
                    }
                });
        } else {
            swal({
                title: "Informasi",
                type: 'info',
                text: 'Silahkan Rekap data tagihan dahulu',
                target: 'top'
            });
        }
    };

    return {
        rupiahbro: function (param, param2, ishtml) {
            var angka = param == "" ? "0" : param;
            var posisidecimal = -1;
            var angkadecimal = "";
            var angkaribuan = "";
            try {
                posisidecimal = angka.indexOf(".");
            } catch (error) {
                console.log("");
            }
            if (posisidecimal < 0) {
                angkaribuan = angka;
                angkadecimal = "";
            } else {
                angkadecimal = angka.substring(posisidecimal, angka.length);
                angkaribuan = angka.substring(0, posisidecimal);
            }

            if (typeof (angkaribuan) != "undefined") {
                var reverse = angkaribuan.toString().split('').reverse().join('');
                var ribuan = reverse.match(/\d{1,3}/g);
                ribuan = ribuan.join(',').split('').reverse().join('') + angkadecimal;
                if (ishtml == "1") {
                    $(param2).html("&nbsp;Rp." + ribuan);
                } else {
                    $(param2).val(ribuan);
                }
            }
        },

        init: function () {
            //currency value validation
            $('input.floatCurdisp').on('input', function () {
                this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');
            });
            $('input.floatCurdisp').on('focus', function () {
                $(this).select();
            });

            $('input.floatCurdisp').on('keyup', function () {
                var angka = $(this).val();
                var nm = this.id;
                vmFinance.rupiahbro(angka, "#id" + nm, "1");
            });
            $(".submtflag").unbind("click");
            $(".submtflag").bind("click", function (e) {
                e.preventDefault();
                $("#filterdatadialogpaymentbilluplodINV").modal("hide");
                swal({
                    title: "Konfirmasi",
                    text: "Yakin ingin memproses Data pembayaran ?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonText: 'Ya',
                    cancelButtonText: "Tidak",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            var formData = new FormData();
                            var form = $('#billpayment_form_flag');
                            var token = $('input[name="__RequestVerificationToken"]', form).val();
                            formData.append("JenisTransaksi", $(".cmb").val());
                            formData.append("upflebyr", $("#upflebyr")[0].files[0]);
                            formData.append("__RequestVerificationToken", token);
                            $.ajax({
                                url: 'Finance/clnOpenAddUplodINVFlag',
                                type: "POST",
                                data: formData,
                                beforeSend: function () {
                                    App.blockUI({});
                                },
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    App.unblockUI();
                                    swal({
                                        title: "Informasi",
                                        type: 'info',
                                        text: response.msg
                                    });
                                },
                                error: function (error) {
                                }
                            });
                        } else {
                            $("#filterdatadialogpaymentbilluplodINV").modal("show");
                        }
                    });
            });
        },

        initbutton: function () {
            $(".addinv").unbind("click");
            $(".addinv").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmFinance.OpenAddInv(par0);
            });

            $(".addinvgen").unbind("click");
            $(".addinvgen").bind("click", function () {
                //var pop = $(this).attr("data-value");
                //var par0 = pop.replace(/["]/g, "");
                //var add = $("#JenisTransaksi").val();
                //vmFinance.OpenInvGEN(par0, add);

                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmFinance.CetakINV(par0, par1);
            });

            $(".addinvgenpaid").unbind("click");
            $(".addinvgenpaid").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmFinance.OpenAddInvpaid(par0);
            });

            $(".addinvpat").unbind("click");
            $(".addinvpat").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmFinance.OpenAddInvPAT(par0);
            });

            $(".addinvpatgen").unbind("click");
            $(".addinvpatgen").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                var add = $("#JenisTransaksi").val();
                vmFinance.OpenInvPATGEN(par0, add);
            });

            $(".LoadMenu").unbind("click");
            $(".LoadMenu").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                var par3 = pop[3].replace(/["]/g, "");

                vmHomePages.LoadMenu(par0, par1, par2, par3);
            });
        },

        OpenAdd: function () {
            onOpenAdd();
        },
        ReqCnl: function (keylookup) {
            onReqCnl(keylookup);
        },
        OpenFilter() {
            onOpenFilter();
        },
        ApplyFilter: function (parForm) {
            onApplyFilter(parForm);
        },
        ResetFilter: function (parForm) {
            onResetFilter(parForm);
        },

        OpenAddMNL: function () {
            onOpenAddMNL();
        },
        ReqApplyMNL: function (paramtext) {
            onReqApplyMNL(paramtext);
        },

        OpenAddInv: function (param) {
            onOpenAddInv(param);
        },

        OpenAddInvpaid: function (param) {
            onOpenAddInvpaid(param);
        },

        OpenInvGEN: function (param, param1) {
            var form = $('#billpayment_form');
            var token = $('input[name="__RequestVerificationToken"]', form).val();
            var table = $('#table_List_billing_payment_upload').DataTable();
            //var table_length = table.data().count();
            var length = table.page.info().recordsTotal;
            var noinv = $("#NoPengajuanRequest").val();

            var tit = (noinv == "") ? "Konfirmasi" : " Informasi";
            var txt = (noinv == "") ? "Apakah anda yakin untuk membuat invoice?" : "Apakah anda yakin untuk menampilkan rekap data tagihan No." + noinv + "?";
            var tpe = (noinv == "") ? "warning" : "info";

            if (token != "" && (typeof (token) != "undefined" && length > 0)) {
                swal({
                    title: tit,
                    text: txt,
                    type: tpe,
                    showCancelButton: true,
                    confirmButtonText: 'Ya',
                    cancelButtonText: "Tidak",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            onAddInvGEN(param, param1);
                        }
                    });
            } else {
                swal({
                    title: "Informasi",
                    type: 'info',
                    text: 'Silahkan Rekap data tagihan dahulu',
                    target: 'top'
                });
            }
        },

        OpenAddInvPAT: function (param) {
            onOpenAddInvPAT(param);
        },

        OpenInvPATGEN: function (param, param1) {
            var form = $('#billpayment_form');
            var token = $('input[name="__RequestVerificationToken"]', form).val();
            var table = $('#table_List_billing_payment_upload').DataTable();
            //var table_length = table.data().count();
            var length = table.page.info().recordsTotal;
            var noinv = $("#NoPengajuanRequest").val();

            var tit = (noinv == "") ? "Konfirmasi" : " Informasi";
            var txt = (noinv == "") ? "Apakah anda yakin untuk membuat invoice?" : "Apakah anda yakin untuk menampilkan rekap data tagihan No." + noinv + "?";
            var tpe = (noinv == "") ? "warning" : "info";

            if (token != "" && (typeof (token) != "undefined" && length > 0)) {
                swal({
                    title: tit,
                    text: txt,
                    type: tpe,
                    showCancelButton: true,
                    confirmButtonText: 'Ya',
                    cancelButtonText: "Tidak",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            onAddInvPATGEN(param, param1);
                        }
                    });
            } else {
                swal({
                    title: "Informasi",
                    type: 'info',
                    text: 'Silahkan Rekap data tagihan dahulu',
                    target: 'top'
                });
            }
        },

        ReqCnlInv: function (keylookup, oprprm) {
            onReqCnlInv(keylookup, oprprm);
        },
        OpenFilterInv() {
            onOpenFilterInv();
        },
        ApplyFilterInv: function (parForm) {
            onApplyFilterInv(parForm);
        },
        ResetFilterInv: function (parForm) {
            onResetFilterInv(parForm);
        },

        OpenAddFP: function () {
            onOpenAddFP();
        },
        ReqCnlFP: function (keylookup) {
            onReqCnlFP(keylookup);
        },
        OpenFilterFP() {
            onOpenFilterFP();
        },
        ApplyFilterFP: function (parForm) {
            onApplyFilterFP(parForm);
        },
        ResetFilterFP: function (parForm) {
            onResetFilterFP(parForm);
        },

        OpenAddJur: function () {
            onOpenAddJur();
        },
        ReqCnlJur: function (keylookup) {
            onReqCnlJur(keylookup);
        },
        OpenFilterJur() {
            onOpenFilterJur();
        },
        ApplyFilterJur: function (parForm) {
            onApplyFilterJur(parForm);
        },
        ResetFilterJur: function (parForm) {
            onResetFilterJur(parForm);
        },

        OpenAddInvBNI: function () {
            onOpenAddInvBNI();
        },
        ReqCnlInvBNI: function (keylookup, kd) {
            onReqCnlInvBNI(keylookup, kd);
        },
        OpenFilterInvBNI() {
            onOpenFilterInvBNI();
        },
        ApplyFilterInvBNI: function (parForm) {
            onApplyFilterInvBNI(parForm);
        },
        ResetFilterInvBNI: function (parForm) {
            onResetFilterInvBNI(parForm);
        },

        Checklist: function (status, idname) {
            onChecklist(status, idname);
        },

        CetakINV: function (idnama, codeprev) {
            onCetakINV(idnama, codeprev);
        },

        ReCombine: function (idnama, codeprev) {
            OnReCombine(idnama, codeprev);
        },

        CooBeerBeginInvPPAT: function () {
            App.blockUI({
                target: '#billpayment_form'
            });
        },
        CooBeerSuccessInvPPAT: function (x) {
            App.unblockUI('#billpayment_form');
            if (x.moderror == false) {
                $("#filterdatadialogpaymentbilluplodINVPAT").modal("hide");
                swal({
                    title: "Informasi",
                    text: x.msg,
                    type: "info",
                    showConfirmButton: true,
                    closeOnCancel: true,
                    confirmButtonText: "Tutup",
                }, function () {
                    if (x.result == 1) {
                        $("#gridbillingpaymentlist").html(x.view);
                    }
                });
            } else {
                window.location.href = x.url;
            }
        },
        CooBeerFailureINVPPAT: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },

        CooBeerBeginInv: function () {
            App.blockUI({
                target: '#billpayment_form'
            });
        },
        CooBeerSuccessInv: function (x) {
            App.unblockUI('#billpayment_form');
            if (x.moderror == false) {
                $("#filterdatadialogpaymentbilluplodINV").modal("hide");
                swal({
                    title: "Informasi",
                    text: x.msg,
                    type: "info",
                    showConfirmButton: true,
                    closeOnCancel: true,
                    confirmButtonText: "Tutup",
                }, function () {
                    if (x.result == 1) {
                        $("#gridbillingpaymentlist").html(x.view);
                        $("#SelectNotaris").val(x.selnot);
                        $("#JenisTransaksi").val(x.jntr);
                        $("#RequestNo").val(x.noreq);
                        $("#NoPengajuanRequest").val(x.invo);

                        const linkSource = 'data:application/vnd.ms-excel;base64,' + x.bytetyipe;
                        const downloadLink = document.createElement("a");
                        const fileName = x.jnfle;
                        downloadLink.href = linkSource;
                        downloadLink.download = fileName;
                        downloadLink.click();
                    }
                });
            } else {
                window.location.href = x.url;
            }
        },
        CooBeerFailureInv: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },

        CooBeerBeginInvlag: function () {
            App.blockUI({
                target: '#billpayment_form'
            });
        },
        CooBeerSuccessInvFlag: function (x) {
            App.unblockUI('#billpayment_form');
            if (x.moderror == false) {
                $("#filterdatadialogpaymentbilluplodINV").modal("hide");
                swal({
                    title: "Informasi",
                    text: x.msg,
                    type: "info",
                    showConfirmButton: true,
                    closeOnCancel: true,
                    confirmButtonText: "Tutup",
                }, function () {
                    if (x.result == 1) {
                    }
                });
            } else {
                window.location.href = x.url;
            }
        },
        CooBeerFailureInvFlag: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },

        CooBeerBeginCetakInv: function () {
            App.blockUI();
        },
        CooBeerSuccessCetakInv: function (x) {
            App.unblockUI();
            if (x.moderror == false) {
                $("#filterdatadialogpaymentbilluplodINVPAT").modal("hide");
                swal({
                    title: "Informasi",
                    text: x.msg,
                    type: "info",
                    showConfirmButton: true,
                    confirmButtonText: "Tutup",
                }, function () {
                    if (x.result == 1) {
                        if (x.typo == "createinvoice") {
                            parfrm = '<embed src=data:application/pdf;base64,' + x.bytetyipe + '#toolbar=0 width="100% " height="600px" id="fame"></embed>';
                            //parfrm = '<object><embed id="preview_pdf" src="data:application/pdf;base64,' + x.bytetyipe + '></<object>';
                            $("#gridbillingpaymentlist").html(parfrm);
                            $(".jenser").html(x.namatitle);

                            //$(".pdprint").unbind("click");
                            //$(".pdprint").bind("click", function () {
                            //    w = window.open(window.location.href, "Invoice No. : " + x.invo);
                            //    w.document.open();
                            //    w.document.write(parfrm);
                            //    w.document.close();
                            //});
                        }
                    }
                });
            } else {
                window.location.href = x.url;
            }
        },
        CooBeerFailureCetakInv: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },

        CooBeerBegin: function () {
            App.blockUI();
        },
        CooBeerSuccess: function (x) {
            var stringpo;
            if (x.moderror == false) {
                if (x.msg != "") {
                    swal({
                        title: "Informasi",
                        text: x.msg,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    });
                } else {
                    if (x.resulted == "1") {
                        stringpo = x.dolpet;
                        stringpo.forEach(function (item) {
                            $(".DrillDownRow td input[value='" + item + "']").parent().parent().remove();
                        });
                    }
                }
                App.unblockUI();
            } else {
                window.location.href = x.url;
            }
        },
        CooBeerFailure: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },
    }
}();