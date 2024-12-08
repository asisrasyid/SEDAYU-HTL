var elemtid = "body";
var elemntupload = ".modal-body";


var vmHomePages = function () {

    var onInit = function () {
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            orientation: "bottom",
            autoclose: true
        });
    };

    var onResetFilter = function (parForm) {
        var frm = $(parForm);
        $('#filterdatadialog .form-control').val('');
        $('#filterdatadialog .select2').val('').trigger("change");

        var lengthbrn = ($('#SelectArea > option').length) - 1;
        if (lengthbrn == 1) {
            $('#SelectArea').select2("val", $('#SelectArea option:eq(1)').val());
        }

        lengthbrn = ($('#SelectBranch > option').length) - 1;
        if (lengthbrn == 1) {
            $('#SelectBranch').select2("val", $('#SelectBranch option:eq(1)').val());
        }

        lengthbrn = ($('#SelectDivisi > option').length) - 1;
        if (lengthbrn == 1) {
            $('#SelectDivisi').select2("val", $('#SelectDivisi option:eq(1)').val());
        }
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
                    $(".modal-backdrop").hide();
                    $("#filterdatadialog").modal("hide");
                    if (data.view !== "") {
                        $("#pagecontent").html(data.view);
                    }
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

    var onOpenFilter = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Home/clnOpenFilterpop",
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
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);
                    $('#todate').datepicker().datepicker('setDate', data.opsi7);

                    var lengthbrn = ($('#SelectArea > option').length) - 1;
                    if (lengthbrn == 1) {
                        $('#SelectArea').select2("val", $('#SelectArea option:eq(1)').val());
                    }

                    lengthbrn = ($('#SelectDivisi > option').length) - 1;
                    if (lengthbrn == 1) {
                        $('#SelectDivisi').select2("val", $('#SelectDivisi option:eq(1)').val());
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

    var onFilterBranchByClient = function () {
        var ParReg = $("#SelectArea").val();
        var jsoncoll = "";
        var jsonreposn = "[]";
        var options = "undefined";
        var datajson = "[]";

        if ($('#SelectBranch').length > 0) {
            $.ajax({
                url: "Home/clnOpenFilterpop",
                type: "POST",
                data: { opr: "unload", cab: "", reg: ParReg },
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


                        lengthbrn = ($('#SelectBranch > option').length) - 1;
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

    var onChcoChucprop = function () {
        var jsoncoll = "";
        var jsonreposn = "[]";
        $.ajax({
            url: "Account/clnAccountChucProp",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    $(".page-quick-sidebar-toggler").click();
                    $("#pagecontent").html(data.view);
                    App.unblockUI();
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


    var onChToDo = function (aped, jned) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        $.ajax({
            url: "Home/clnRgrTodo",
            type: "POST",
            data: { ap: aped, jn: jned },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    if (data.tbled != "") {
                        $("#" + data.divdata).html(data.view);
                    }

                    $("." + data.divdata + "1").html(data.tot1);
                    $("." + data.divdata + "2").html(data.tot2);
                    $("." + data.divdata + "3").html(data.tot3);
                    $("." + data.divdata + "4").html(data.tot4);

                    if (data.tbled != "") {
                        $("#" + data.tbled).DataTable().destroy();
                        TableDatatablesEditable1.init("#" + data.tbled);
                    }

                    App.unblockUI();
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

    var onLoadMenu = function (parmenu, parcaption, ruteed, actioned, partipemodule) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        $(".modal-backdrop").remove();
        $.ajax({
            url: ruteed + "/" + actioned,
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { menu: parmenu, caption: parcaption, tipemodule: partipemodule },
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    $("#pagecontent").html(data.view);
                    // onHideSidebar();
                    App.unblockUI();

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
            complete: function () {
                App.unblockUI();
            }
        });
    };

    var onHideSidebar = function () {
        $("body").addClass("page-sidebar-closed");
        $("ul").addClass("page-sidebar-menu-closed");
        $("#hideside").click();
        //page - sidebar navbar - collapse out collapse in
    };

    var onOpenView = function (moduled, modulecur, modul, idkey) {

        var jsoncoll = "";
        var jsonreposn = "";
        $.ajax({
            url: modul,
            type: "POST",
            data: { module: moduled, curmodule: modulecur, paramkey: idkey },
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

    var onOpenhst = function (idkey, oprgt, oprd) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenShowHisTL",
            type: "POST",
            data: { paramkey: idkey, oprfun: oprgt },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivhis").html(data.view);
                    if (oprd !== "gd") {
                        $("#grdl").hide();
                    }
                    $("#filterdatadialoghist").modal("show");
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

    //open menu transaksi //
    return {


        initbackground: function () {
            $.backstretch([
                "../Content/assets/pages/media/bg/1.jpg",
                "../Content/assets/pages/media/bg/2.jpg"
            ], {
                fade: 1000,
                duration: 7000
            }
            );
        },

        Openhst: function (idkey, oprgt, opr) {
            onOpenhst(idkey, oprgt, opr);
        },


        OpenView: function (moduled, curmodule, modul, idkey) {
            onOpenView(moduled, curmodule, modul, idkey);
        },

        ChToDo: function (ab, jn) {
            onChToDo(ab, jn);
        },

        ChcoChuc: function () {
            onChcoChuc();
        },

        ChcoChucprop: function () {
            onChcoChucprop();
        },

        ApplyFilter: function (parForm, pardownloadexcel) {
            onApplyFilter(parForm, pardownloadexcel);
        },
        LoadMenu: function (parmenu, parcaption, ruted, actioned, tipemodule) {
            onLoadMenu(parmenu, parcaption, ruted, actioned, tipemodule);
        },
        OpenFilter: function () {
            onOpenFilter();
        },

        ResetFilter: function (parForm) {
            onResetFilter(parForm);
        },

        FilterBranchByClient: function () {
            onFilterBranchByClient();
        },
        initbuton: function () {
            $(".OpenFilter").unbind("click");
            $(".OpenFilter").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmHomePages.OpenFilter(par0);
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

            $(".OpenView").unbind("click");
            $(".OpenView").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/[']/g, "");
                var par1 = pop[1].replace(/[']/g, "");
                var par2 = pop[2].replace(/[']/g, "");
                var par3 = pop[3].replace(/[']/g, "");
                vmHomePages.OpenView(par0, par1, par2, par3);
            });

            $(".open4riwayat").unbind("click");
            $(".open4riwayat").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHomePages.Openhst(par0, par1);
            });

            /*
            $("#SelectArea").unbind("change");
            $("#SelectArea").bind("change", function () {
                vmRegismitra.FilterBranchByClient();
            });

            $(".masterfilterref").unbind("click");
            $(".masterfilterref").bind("click", function () {
                vmHomePages.ResetFilter('#DashboardFilter_form')
            })

            $(".masterfilter").unbind("click");
            $(".masterfilter").bind("click", function () {
                vmHomePages.ApplyFilter('#DashboardFilter_form', '')
            })

            $(".todoactclik,#todoreq").unbind("click");
            $(".todoactclik,#todoreq").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/[']/g, "");
                var par1 = pop[1].replace(/[']/g, "");
                var par2 = pop[2].replace(/[']/g, "");
                var par3 = pop[3].replace(/[']/g, "");
                vmHomePages.LoadMenu(par0, par1, par2, par3);
            });
            */
        },

        initbackground: function () {
            //$.backstretch([
            //    "../Content/assets/pages/media/bg/1.jpg",
            //    "../Content/assets/pages/media/bg/2.jpg"
            //], {
            //        fade: 1000,
            //        duration: 7000
            //    }
            //);
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

    vmHomePages.initbuton();
    vmHomePages.initbackground();

    $(".shtstatuscl").unbind("click");
    $(".shtstatuscl").bind("click", function () {
        var jsoncoll = "";
        var jsonreposn = "";
        var idkey = $("#aux").val();
        $.ajax({
            url: "HTL/clnKoncePloddocshtnw",
            type: "POST",
            data: { paridno: idkey, parkepo: '' },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivupdocvw").html(data.view);
                    $('#dialogupdocsht').draggable({
                        handle: ".modal-header"
                    });
                    //$('.select2').select2();
                    //$("#DokumenTyped").unbind("change");
                    //$("#DokumenTyped").bind("change", function () {
                    //    vmHTL.chkfle(this, 'ckalled', 'ckalled');
                    //});
                    //// $(".msgupdoc").html("");
                    //$(".modal-header").unbind("click");
                    //$(".modal-header").bind("click", function () {
                    //    $(".togl").toggle();
                    //});
                    //$("#capt").html("DOKUMEN");
                    $("#dialogupdocsht").modal("show");
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

    });

    $(".shtstatuscl").unbind("mouseout");
    $(".shtstatuscl").bind("mouseout", function () {
        $(".note-success").css("background-color", "#c0edf1");
        $(".note-success").css("cursor", "default");
    });

    $(".shtstatuscl").unbind("mouseover");
    $(".shtstatuscl").bind("mouseover", function () {
        $(".note-success").css("background-color", "#d9ffbd");
        $(".note-success").css("cursor", "pointer");
    });


    tabled = 'table_txppat';
    if ($("#" + tabled).length > 0) {
        $("#" + tabled).DataTable().destroy();
        TableDatatablesEditable1.init("#" + tabled);
    }

    tabled = 'table_txodppat';
    if ($("#" + tabled).length > 0) {
        $("#" + tabled).DataTable().destroy();
        TableDatatablesEditable1.init("#" + tabled);
    }

    tabled = 'table_txcab';
    if ($("#" + tabled).length > 0) {
        $("#" + tabled).DataTable().destroy();
        TableDatatablesEditable1.init("#" + tabled);
    }


    var tabled = 'table_txodcab';
    if ($("#" + tabled).length > 0) {
        $("#" + tabled).DataTable().destroy();
        TableDatatablesEditable1.init("#" + tabled);
    }

    tabled = 'table_txvry';
    if ($("#" + tabled).length > 0) {
        $("#" + tabled).DataTable().destroy();
        TableDatatablesEditable1.init("#" + tabled);
    }

    var tabled = 'table_txodvry';
    if ($("#" + tabled).length > 0) {
        $("#" + tabled).DataTable().destroy();
        TableDatatablesEditable1.init("#" + tabled);
    }

    $(".alldtnot").unbind("click");
    $(".alldtnot").bind("click", function () {
        vmHomePages.ChToDo("20", "all");
    });
    $(".daydtnot").unbind("click");
    $(".daydtnot").bind("click", function () {
        vmHomePages.ChToDo("20", "day");
    });
    $(".mondtnot").unbind("click");
    $(".mondtnot").bind("click", function () {
        vmHomePages.ChToDo("20", "month");
    });


    $(".alldtnotod").unbind("click");
    $(".alldtnotod").bind("click", function () {
        vmHomePages.ChToDo("20", "allod");
    });
    $(".daydtnotod").unbind("click");
    $(".daydtnotod").bind("click", function () {
        vmHomePages.ChToDo("20", "dayod");
    });
    $(".mondtnotod").unbind("click");
    $(".mondtnotod").bind("click", function () {
        vmHomePages.ChToDo("20", "monthod");
    });


    $(".alldtcab").unbind("click");
    $(".alldtcab").bind("click", function () {
        vmHomePages.ChToDo("30", "all");
    });
    $(".daydtcab").unbind("click");
    $(".daydtcab").bind("click", function () {
        vmHomePages.ChToDo("30", "day");
    });
    $(".mondtcab").unbind("click");
    $(".mondtcab").bind("click", function () {
        vmHomePages.ChToDo("30", "month");
    });

    $(".alldtverfy").unbind("click");
    $(".alldtverfy").bind("click", function () {
        vmHomePages.ChToDo("60", "all");
    });
    $(".daydtverfy").unbind("click");
    $(".daydtverfy").bind("click", function () {
        vmHomePages.ChToDo("60", "day");
    });
    $(".mondtverfy").unbind("click");
    $(".mondtverfy").bind("click", function () {
        vmHomePages.ChToDo("60", "month");
    });


    $(".alldtverfyod").unbind("click");
    $(".alldtverfyod").bind("click", function () {
        vmHomePages.ChToDo("60", "allod");
    });
    $(".daydtverfyod").unbind("click");
    $(".daydtverfyod").bind("click", function () {
        vmHomePages.ChToDo("60", "dayod");
    });
    $(".mondtverfyod").unbind("click");
    $(".mondtverfyod").bind("click", function () {
        vmHomePages.ChToDo("60", "monthod");
    });


    $(".alldtaju").unbind("click");
    $(".alldtaju").bind("click", function () {
        vmHomePages.ChToDo("70", "all");
    });
    $(".daydtaju").unbind("click");
    $(".daydtaju").bind("click", function () {
        vmHomePages.ChToDo("70", "day");
    });
    $(".mondtaju").unbind("click");
    $(".mondtaju").bind("click", function () {
        vmHomePages.ChToDo("70", "month");
    });


    $(".profus").unbind("click");
    $(".profus").bind("click", function () {
        vmHomePages.ChcoChucprop()
    });


    $(".homerefresh").unbind("click");
    $(".homerefresh").bind("click", function () {
        location.reload();
    });

    $(".actdrop").unbind("click");
    $(".actdrop").bind("click", function () {
        var pop = $(this).attr("data-value");
        pop = pop.replace("vmHomePages.LoadMenu", "");
        pop = pop.replace("(", "").replace(")", "");
        pop = pop.split(",");
        var par0 = pop[0].replace(/[']/g, "");
        var par1 = pop[1].replace(/[']/g, "");
        var par2 = pop[2].replace(/[']/g, "");
        var par3 = pop[3].replace(/[']/g, "");
        vmHomePages.LoadMenu(par0, par1, par2, par3);

    });

    $(".searchtxt").unbind("change");
    $(".searchtxt").bind("change", function () {
        var pop = $(this).attr("data-value");
        pop = pop.split(",");
        var divdta = pop[0].replace(/[']/g, "");
        var divbckp = pop[1].replace(/[']/g, "");

        var text = $(this).val();
        var htmlcek = document.getElementById(divdta).innerHTML;
        var elem;
        if (htmlcek == "") {
            document.getElementById(divdta).innerHTML = document.getElementById(divbckp).innerHTML;
        }
        elem = $("#" + divdta + ">div:contains('" + text + "')");
        $("#" + divdta).html(elem);
        if (text == "") {
            $("#" + divdta).html($("#" + divbckp).html());
        }

    });


    try {

        if ($("#todonot").length > 0) {
            var copyhtml = document.getElementById("todonot").innerHTML;
            document.getElementById("todonotbckp").innerHTML = copyhtml;
            copyhtml = document.getElementById("todocab").innerHTML;
            document.getElementById("todocabbckp").innerHTML = copyhtml;
            copyhtml = document.getElementById("todoadm").innerHTML;
            document.getElementById("todoadmbckp").innerHTML = copyhtml;
            copyhtml = document.getElementById("todooder").innerHTML;
            document.getElementById("todooderbckp").innerHTML = copyhtml;
        }
    } catch (error) {
        console.log(error);
    }

})

