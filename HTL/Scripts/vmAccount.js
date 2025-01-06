var elemtid = "";
var elemntupload = "modal-content";

var vmAccount = function () {
    var onshowpowder = function (idhide) {
        $("#" + idhide).removeClass("powderhide");
        $("#" + idhide).addClass("powdershow");
        if ($('#' + idhide).attr("type") == "password") {
            $('#' + idhide).attr('type', 'text');
        }
    };
    var onhidepowder = function (idshow) {
        $("#" + idshow).removeClass("powdershow");
        $("#" + idshow).addClass("powderhide");
        if ($('#' + idshow).attr("type") == "text") {
            $('#' + idshow).attr('type', 'password');
        }
    };
    var OnChangeViewPass = function () {
        $("#show_hide_password a").on('click', function (event) {
            event.preventDefault();
            if ($('#show_hide_password input').attr("type") == "text") {
                $('#show_hide_password input').attr('type', 'password');
                $('#show_hide_password i').addClass("fa-eye-slash");
                $('#show_hide_password i').removeClass("fa-eye");
            } else if ($('#show_hide_password input').attr("type") == "password") {
                $('#show_hide_password input').attr('type', 'text');
                $('#show_hide_password i').removeClass("fa-eye-slash");
                $('#show_hide_password i').addClass("fa-eye");
            }
        });
    };

    var onChcoChuc = function () {
        var jsoncoll = "";
        var jsonreposn = "[]";
        $.ajax({
            url: "Account/clnAccountChuc",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                $("#pagecontent").html(data.view);
                App.unblockUI();
            },
            error: function (x, y, z) {
                App.unblockUI();
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                window.location.href = jsonreposn.responseJSON.url;
            }
        });
    };

    var onChcoChucNew = function () {
        var jsoncoll = "";
        var jsonreposn = "[]";
        $.ajax({
            url: "Account/clnAccountChucNew",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                $(".login").html(data.view);
                App.unblockUI();
            },
            error: function (x, y, z) {
                App.unblockUI();
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                window.location.href = jsonreposn.responseJSON.url;
            }
        });
    };

    var onApplySbmt = function (parForm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();

        if (valid == true) {
            var fieldata = new FormData();
            var fields = frm.serializeArray();
            $.each(fields, function (i, field) {
                fieldata.append(field.name, field.value);
            });
            fieldata.append('potofile', $('#potofile')[0].files[0]);
            // fieldata.append('ttdform', $('#ttdform')[0].files[0]);
            //fieldata.append('ttdsk', $('#ttdsk')[0].files[0]);
            //fieldata.append('ttdabs', $('#ttdabs')[0].files[0]);
            $.ajax({
                type: frm.attr('method'),
                url: frm.attr('action'),
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fieldata,
                beforeSend: function () {
                    App.blockUI();
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
                            if (data.resulted == "1") {
                                window.location.reload();
                            }
                        });
                    } else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                },
            });
        };
    };

    var onSearchCabangAjax = function (parm) {
        var ParReg = $("#SelectArea").val();
        var jsoncoll = "";
        var jsonreposn = "[]";
        var options = "undefined";
        var datajson = "[]";

        var parmurl = parm == "reg" ? "Account/clnGetBranchByRegion" : "Account/clnOpenFilterpop";

        if ($('#SelectBranch').length > 0) {
            $.ajax({
                url: parmurl,
                type: "POST",
                data: { opr: "unload", cab: "", regionid: ParReg, reg: parm },
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    App.blockUI({ target: elemntupload });
                },
                success: function (data) {
                    if (data.moderror == false) {
                        datajson = JSON.parse(data.branchjson);
                        options = $('#SelectBranch');
                        options.empty();
                        options.append('<option value="">Pilih..</option>');
                        $.each(datajson, function (idx, obj) {
                            options.append('<option value=' + obj.Value + '>' + obj.Text + '</option>');
                        });
                        $("#SelectBranch").val(data.brachselect).trigger("change");
                        var lengthbrn = ($('#SelectBranch > option').length) - 1;
                        if (lengthbrn == 1) {
                            $('#SelectBranch').select2("val", $('#SelectBranch option:eq(1)').val());
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
                }
            });
        }
    };

    var onOpenFURegis = function (modul, idkey, opr) {
        var jsoncoll = "";
        var jsonreposn = "";
        $.ajax({
            url: "Account/AccountRegisFU",
            type: "POST",
            data: { module: modul, paramkey: idkey, oprmn: opr },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivadd").html(data.view);
                    $("#keylookupdataHTX").val(idkey);
                    $("#filterdatadialogFU").modal("show");
                }
                else {
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

    var onOpenPrevProf = function (parame, pereme) {
        var jsoncoll = "";
        var jsonreposn = "";
        $.ajax({
            url: "Account/clnAccountChucPropPrev",
            type: "POST",
            data: { fle: parame },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    var byteArray = new Uint8Array(data.bte);
                    var link = window.URL.createObjectURL(new Blob([byteArray], { type: 'image/jpeg' }));
                    var parfrm = '<iframe src="' + link + '" width="100% " height="500px" id="frmbro"></iframe>';
                    $("#warkahdoc").html(parfrm);
                    $("#dialogwarkah").modal("show")
                }
                else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI();
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };

    var onOpenView = function (modul, idkey) {
        var jsoncoll = "";
        var jsonreposn = "";
        $.ajax({
            url: "Account/clnHeaderTxView",
            type: "POST",
            data: { module: modul, paramkey: idkey },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#pagecontent").html(data.view);
                    $("#table_tx").dataTable();
                    $("#table_tx_log").dataTable();
                    $("#keylookupdata").val(data.keydata);
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

    var onOpenFilter = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Account/clnOpenFilterpop",
            type: "POST",
            data: { opr: "load" },
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

                    $("#RequestNo").val(data.opsi1);
                    $("#SelectDivisi").val(data.opsi2).trigger("change");
                    $("#SelectArea").val(data.opsi3).trigger("change");
                    $("#SelectBranch").val(data.opsi4).trigger("change");
                    $('#SelectContractStatus').val(data.opsi5).change();
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);
                    $('#todate').datepicker().datepicker('setDate', data.opsi7);

                    var lengthbrn = ($('#SelectDivisi > option').length) - 1;
                    if (lengthbrn == 1) {
                        $('#SelectDivisi').select2("val", $('#SelectDivisi option:eq(1)').val());
                    }
                    var lengthbrn = ($('#SelectArea > option').length) - 1;
                    if (lengthbrn == 1) {
                        $('#SelectArea').select2("val", $('#SelectArea option:eq(1)').val());
                    }
                    var lengthbrn = ($('#SelectBranch > option').length) - 1;
                    if (lengthbrn == 1) {
                        $('#SelectBranch').select2("val", $('#SelectBranch option:eq(1)').val());
                    }
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

    var onResetFilter = function (parForm) {
        $('#filterdatadialog input[type="text"]').val('');
        $('#filterdatadialog .select2').val('').trigger("change");
    };

    var onApplyFilter = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var data1 = "undefined";
        var data2 = "undefined";
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
                    $(".modal-backdrop").hide();
                    $("#filterdatadialog").modal("hide");
                    $("#gridTx").html(data.view);
                    App.unblockUI(elemntupload);
                } else {
                    window.location.href = x.url;
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

    var onApplySbmtWF = function (parForm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();

        if (valid == true) {
            // $("#datadialogaddupdatemaster").modal("hide");
            $.ajax({
                type: frm.attr('method'),
                url: frm.attr('action'),
                data: frm.serialize(),
                beforeSend: function () {
                    App.blockUI();
                },
                success: function (data) {
                    if (data.moderror == false) {
                        App.unblockUI();
                        // $("#datadialogaddupdatemaster").modal("hide");
                        if (data.flag == "") {
                            if (data.resulted == "1") {
                                $("#gridTx").html(data.view);
                                $("#table_tx").dataTable();
                                onResetFormDtx("#clformAddTxDet");
                            } else {
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    $("#datadialogaddupdatemaster").modal("show");
                                });
                            }
                        } else {
                            swal({
                                title: "Informasi",
                                text: data.msg,
                                type: "info",
                                showConfirmButton: true,
                                confirmButtonText: "Tutup",
                            }, function () {
                                if (data.resulted == "1") {
                                    if (data.flag == "CRETHDR") {
                                        $("#gridTx").html(data.view);
                                        $("#table_tx").dataTable();
                                    } else {
                                        $("#datadialogaddupdatemaster").modal("hide");
                                        $("#header_notification_bar").remove();
                                        $(data.view).insertBefore(".dropdown-user");
                                        $("#backpage").click();
                                    }
                                }
                                //else {
                                // $("#datadialogaddupdatemaster").modal("show");
                                //}
                            });
                        }
                    } else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                },
            });
        }
    };

    var onApplySbmtActv = function (parForm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();

        if (valid == true) {
            $("#filterdatadialogFU").modal("hide");
            $.ajax({
                type: frm.attr('method'),
                url: frm.attr('action'),
                data: frm.serialize(),
                beforeSend: function () {
                    App.blockUI();
                },
                success: function (data) {
                    if (data.moderror == false) {
                        App.unblockUI();
                        if (data.ShowMessagex == "") {
                            swal({
                                title: "Informasi",
                                text: data.msg,
                                type: "info",
                                showConfirmButton: true,
                                confirmButtonText: "Tutup",
                            }, function () {
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                });
                                $("#" + data.idval).remove();
                                $("#relodregs").click();
                            });
                        } else {
                            $("#cssShowMessage").removeClass();
                            if ((data.probitusertxt == "1")) {
                                $("#cssShowMessage").addClass(data.ShowMessagex).show();
                                $("#idShowMessage").html(data.msg);
                                $("#form_regisfu,#footnoted").remove();
                            } else {
                                $("#cssShowMessage").addClass(data.ShowMessagex).show();
                                $("#idShowMessage").html(data.msg);
                            }
                        }
                    }
                    else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                },
            });
        }
    };

    var onApplySbmtChgp = function (parForm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();

        if (valid == true) {
            $("#filterdatadialog").modal("hide");
            $.ajax({
                type: frm.attr('method'),
                url: frm.attr('action'),
                data: frm.serialize(),
                beforeSend: function () {
                    App.blockUI();
                },
                success: function (data) {
                    if (data.moderror == false) {
                        App.unblockUI();
                        $("#cssShowMessage").removeClass();
                        if (data.swltype == 'input') {
                            $("#powderkep").removeClass("hideblock");
                            $("#powderkep").addClass("shanblock");
                            $("#cssShowMessage").addClass(data.ShowMessagex).show();
                            $("#idShowMessage").html(data.msg);
                        } else {
                            if (data.url == "") {
                                $("#cssShowMessage").addClass(data.ShowMessagex).show();
                                $("#idShowMessage").html(data.msg);
                            } else {
                                $("#frmmanabisa").remove();
                                $("#cssShowMessagex").addClass(data.ShowMessagex).show();
                                $("#idShowMessagex").html(data.msg);
                            }
                        }
                    }
                    else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                },
            });
        }
    };

    return {
        OpenFURegis: function (module, key, opr) {
            onOpenFURegis(module, key, opr);
        },

        OpenView: function (module, key) {
            onOpenView(module, key);
        },

        OpenFilter: function () {
            onOpenFilter();
        },

        ResetFilter: function (parForm) {
            onResetFilter(parForm);
        },

        ApplyFilter: function (parForm, pardownloadexcel) {
            onApplyFilter(parForm, pardownloadexcel);
        },

        SearchCabangAjax: function (parm) {
            onSearchCabangAjax(parm);
        },

        ChacctBeginNew: function () {
            App.blockUI();
        },
        ChacctSuccessNew: function (data) {
            App.unblockUI();
            $("#cssShowMessage").removeClass();
            if ((data.probitusertxt == "1")) {
                $("#cssShowMessage").addClass(data.ShowMessagex).show();
                $("#idShowMessage").html(data.MessageNotValidx);
                $("#btnMessage").show();
                $("#login_form").remove();
            } else {
                $("#cssShowMessage").addClass(data.ShowMessagex).show();
                $("#idShowMessage").html(data.MessageNotValidx);
                $("#btnMessage").hide();
            }
        },
        ChacctFailureNew: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },

        ChcoChuc: function () {
            onChcoChuc();
        },

        ChcoChucNew: function () {
            onChcoChucNew();
        },

        ApplySbmt: function (frm) {
            onApplySbmt(frm);
        },

        ApplySbmtWF: function (frm) {
            onApplySbmtWF(frm);
        },

        ApplySbmtActv: function (frm) {
            onApplySbmtActv(frm);
        },

        ApplySbmtChgp: function (frm) {
            onApplySbmtChgp(frm);
        },

        BeginSubmit: function () {
            App.blockUI();
        },
        SuccessSubmit: function (data) {
            $("#cssShowMessage").removeClass();
            if ((data.MessageNotValidx == "")) {
                App.blockUI();
                window.location = data.url;
            } else {
                $("#cssShowMessage").addClass(data.ShowMessagex).show();
                $("#idShowMessage").html(data.MessageNotValidx);
                // $.validator.unobtrusive.parse("#login_form");
                $("#powderkep").addClass("hideblock");
                $("#powderkep").removeClass("shanblock");
                if (data.probitusertxt == 1) {
                    $("#powderkep").removeClass("hideblock");
                    $("#powderkep").addClass("shanblock");
                }
                App.unblockUI();
            }
        },
        FailureSubmit: function (x) {
            App.unblockUI();
        },

        ChangeViewPass: function () {
            OnChangeViewPass();
        },
        showpowder: function (id) {
            onshowpowder(id);
        },
        hidepowder: function (id) {
            onhidepowder(id);
        },

        OpenPrevProf: function (param, pereme) {
            onOpenPrevProf(param, pereme);
        },
        initnumber: function () {
            $('input.Numberonly').on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');
            });
        },
        initbutton: function () {
            $(".OpenFilter").unbind("click");
            $(".OpenFilter").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmAccount.OpenFilter(par0);
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

            $(".actreg").unbind("click");
            $(".actreg").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmAccount.OpenFURegis(par0, par1, par2);
            });

            $(".applyreg").unbind("click");
            $(".applyreg").bind("click", function () {
                vmAccount.ApplySbmtActv('#form_regisfu', '');
            });

            $(".accregfil").unbind("click");
            $(".accregfil").bind("click", function () {
                vmAccount.ApplyFilter('#DashboardFilter_form', '');
            });

            $(".accprof").unbind("click");
            $(".accprof").bind("click", function () {
                vmAccount.ApplySbmt('#frmaccountchangegrup');
            });

            $(".accregfilref").unbind("click");
            $(".accregfilref").bind("click", function () {
                vmAccount.ResetFilter('#DashboardFilter_form');
            });

            $("#sbtchangepass").unbind("click");
            $("#sbtchangepass").bind("click", function () {
                vmAccount.ApplySbmtChgp('#frmmanabisa');
            })
        },

        initbackground: function () {
            $.backstretch([
                "../Content/assets/pages/media/bg/1.png",
                "../Content/assets/pages/media/bg/2.png"
            ], {
                fade: 1000,
                duration: 7000
            }
            );
        },
    };
}();

$(document).bind("contextmenu", function (e) {
    e.preventDefault();
});

$(document).keydown(function (e) {
    if (e.which === 123) {
        return false;
    }
});

$(document).ready(function () {
    try {
        vmAccount.initnumber();
        vmAccount.initbutton();

        vmAccount.initbackground();

        vmAccount.hidepowder('UserPass');
        vmAccount.hidepowder('PasswordChange');
        vmAccount.hidepowder('RetypePassword');

        $("label.rememberme,.psdefeusr").mouseout(function () {
            vmAccount.hidepowder('UserPass');
        }).mouseover(function () {
            vmAccount.showpowder('UserPass');
        });

        $(".psdefe").mouseout(function () {
            vmAccount.hidepowder('PasswordChange');
        }).mouseover(function () {
            vmAccount.showpowder('PasswordChange');
        });

        $(".psdefetype").mouseout(function () {
            vmAccount.hidepowder('RetypePassword');
        }).mouseover(function () {
            vmAccount.showpowder('RetypePassword');
        });

        $(".select2-selection").css("border", "1px solid #00417c");

        if ($("#SelectArea").length > 0) {
            $("#SelectArea").select2({
                placeholder: "Pilih Area",
                allowClear: true
            });
        }
        if ($("#SelectBranch").length > 0) {
            $("#SelectBranch").select2({
                placeholder: "Pilih Cabang",
                allowClear: true
            });
        }
    } catch (error) {
        console.log(error);
    }
});