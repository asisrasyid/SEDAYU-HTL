var elemtid = "body";
var elemntupload = ".modal-content";
var popupwindowwarkah = null;
var names = [];

var vmHTL = function () {
    var onResetFilter = function (parForm) {
        var frm = $(parForm);
        $('#filterdatadialog .form-control').val('');
        $('#filterdatadialog .select2').val('').trigger("change");

        lengthbrn = ($('#SelectContractStatus > option').length) - 1;
        if (lengthbrn == 1) {
            $('#SelectContractStatus').select2("val", $('#SelectContractStatus option:eq(1)').val());
        }
    };

    var onCetakSerti = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("");

        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih No Aplikasi/No Sertifikat Terlebih dahulu',
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
            return false;
        }
        btndownload.click();
    };

    var onCetakSertiht = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("");

        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih No Aplikasi/No Sertifikat Terlebih dahulu',
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
            return false;
        }
        btndownload.click();
    };

    var ondownexcel = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val("8gtd1xhpP3f577Qwdr55HA==");
        $("#reooo").val(codeprev);
        btndownload.click();
    };

    var onproBAST = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("cretbast");
        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih No Aplikasi/No Sertifikat Terlebih dahulu',
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
            return false;
        }
        swal({
            title: "Konfirmasi",
            text: "Jika anda sudah yakin data sudah benar silahkan tekan tombol 'Buat BAST' untuk Pembuatan BAST , tekan tombol 'Cek Data' untuk Pengecekan data",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Buat BAST',
            cancelButtonText: "Cek Data",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $(".cretbast").remove();
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                    setTimeout(function () {
                        $('.LoadMenu').click();
                    }, 2000);
                    btndownload.click();
                } else {
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                }
            });
    };

    var onprocfmBAST = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("cfrmbast");
        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih No Aplikasi/No Sertifikat Terlebih dahulu',
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
            return false;
        }
        swal({
            title: "Konfirmasi",
            text: "Jika anda sudah yakin data sudah benar silahkan tekan tombol 'Cofirm BAST' untuk Penerimaan BAST , tekan tombol 'Cek Data' untuk Pengecekan data ulang",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Cofirm BAST',
            cancelButtonText: "Cek Data",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $(".cfrmbast").remove();
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                    btndownload.click();
                } else {
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                }
            });
    };

    var onproslaBAST = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("slabast");
        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih No Aplikasi/No Sertifikat Terlebih dahulu',
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
            return false;
        }
        swal({
            title: "Konfirmasi",
            text: "Jika anda sudah yakin data sudah benar silahkan tekan tombol 'Set Deadline BAST' untuk Pembaharuan Deadline BAST , tekan tombol 'Cek Data' untuk Pengecekan data ulang",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Set Deadline BAST',
            cancelButtonText: "Cek Data",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $(".slabast").remove();
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                    btndownload.click();
                } else {
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                }
            });
    };

    var onprorolBAST = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("slabast");
        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih No Aplikasi/No Sertifikat Terlebih dahulu',
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
            return false;
        }
        swal({
            title: "Konfirmasi",
            text: "Jika anda sudah yakin data sudah benar silahkan tekan tombol 'Rollback BAST' untuk Pembaharuan Status, tekan tombol 'Cek Data' untuk Pengecekan data ulang",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Rollback BAST',
            cancelButtonText: "Cek Data",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $(".rolbast").remove();
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                    btndownload.click();
                } else {
                    var searchTerms = [];
                    var myTable = $('#table_tx').dataTable();
                    myTable.$("input:checkbox[name='AktaSelectdwn']:checked").each(function (i, o) {
                        searchTerms.push("^" + $(this).closest('td').siblings('td').html() + "$");
                    });
                    var myTable = $('#table_tx').DataTable();
                    myTable.column(1).search(searchTerms.join("|"), true, false, true).draw();
                }
            });
    };

    var onCetakSertima = function (idnama, codeprev) {
        $("#namaidpool").val(idnama);
        $("#prevedid").val(codeprev);
        $("#reooo").val("stigma");
        if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
            swal({
                title: "Informasi",
                text: 'Pilih No Aplikasi/No Sertifikat Terlebih dahulu',
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

    var onOpenAddDocvw = function () {
        var jsoncoll = "";
        var jsonreposn = "";
        var idkey = $("#aux").val();
        $.ajax({
            url: "HTL/clnKoncePloddocnwvw",
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
                    $('#dialogwarkah').draggable({
                        handle: ".modal-header"
                    });
                    $('.select2').select2();
                    $("#DokumenTyped").unbind("change");
                    $("#DokumenTyped").bind("change", function () {
                        vmHTL.chkfle(this, 'ckalled', 'ckalled');
                    });
                    // $(".msgupdoc").html("");
                    $(".modal-header").unbind("click");
                    $(".modal-header").bind("click", function () {
                        $(".togl").toggle();
                    });
                    $("#capt").html("DOKUMEN");
                    $("#dialogwarkah").modal("show");
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

    var onOpenAddDoc = function (opxr) {
        var jsoncoll = "";
        var jsonreposn = "";
        var idkey = document.getElementById("aux").value;
        var idct = document.getElementById("ctex").value;
        var grdoc = "4";
        $.ajax({
            url: "HTL/clnKoncePloddocnw",
            type: "POST",
            data: { paridno: idkey, paridno1: idct, parkepo: opxr, grdoctyp: grdoc },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivupdoc").html(data.view);
                    $('#dialogupdoc').draggable({
                        handle: ".modal-header"
                    });
                    $("#ctexdoc").val(data.htdoc);
                    $(".msgupdoc").html("");
                    $(".modal-header").unbind("click");
                    $(".modal-header").bind("click", function () {
                        $(".modal-bodyx").toggle();
                    });
                    $("#dialogupdoc").modal("show");
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

    var onOpenAddDocRoya = function (opxr) {
        var jsoncoll = "";
        var jsonreposn = "";
        var idkey = document.getElementById("aux").value;
        var idct = document.getElementById("ctex").value;
        var grdoc = "4";
        $.ajax({
            url: "HTL/clnKoncePloddocnwRoya",
            type: "POST",
            data: { paridno: idkey, paridno1: idct, parkepo: opxr, grdoctyp: grdoc },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#uidivupdoc").html(data.view);
                    $('#dialogupdoc').draggable({
                        handle: ".modal-header"
                    });
                    $("#ctexdoc").val(data.htdoc);
                    $(".msgupdoc").html("");
                    $(".modal-header").unbind("click");
                    $(".modal-header").bind("click", function () {
                        $(".modal-bodyx").toggle();
                    });
                    $("#dialogupdoc").modal("show");
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

    var onOpenAdd = function (modul, idkey, oprgt) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenAddHTL",
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
                    $("#uidivadd").html(data.view);
                    $("#keylookupdataHTX").val(data.keydata);
                    $("#grdl").hide();
                    $("#uidivadd").show();
                    vmHTL.init();
                    vmHTL.initbuton();
                    //$("#datadialogaupdhtl").modal("show");
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
    var onOpenAddRoya = function (modul, idkey, oprgt) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenAddRoyaDt",
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
                    $("#uidivadd").html(data.view);
                    $("#keylookupdataHTX").val(data.keydata);
                    $("#grdl").hide();
                    $("#uidivadd").show();
                    vmHTL.init();
                    vmHTL.initbuton();
                    //$("#datadialogaupdhtl").modal("show");
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

    var onOpenAddIpt = function (modul, idkey, idrelate, oprgt, gd, idgd) {
        var jsoncoll = "";
        var jsonreposn = "";
        idrelate = $("#diadu").val();
        $.ajax({
            url: "HTL/clnOpenAddHTLIPT",
            type: "POST",
            data: { paramkey: idkey, idrel: idrelate, oprfun: oprgt, gdid: gd, idg: idgd, scn: 'scn' },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    if (data.msg == "") {
                        $("#uidivaddipt").html(data.view);
                        $('#datadialogaupdhtlipt').draggable({
                            handle: ".modal-header"
                        });
                        $(".modal-header").unbind("click");
                        $(".modal-header").bind("click", function () {
                            $(".modal-bodyx").toggle();
                        });
                        $("#datadialogaupdhtlipt").modal("show");
                        vmHTL.init();
                        vmHTL.initbuton();
                        vmHTL.JcopSet();
                        $($("#modesht").val()).remove();
                    } else {
                        swal({
                            title: "Informasi",
                            text: data.msg,
                            type: "warning",
                            showCancelButton: true,
                            confirmButtonText: 'Ya',
                            cancelButtonText: "Tidak",
                            closeOnConfirm: true,
                            closeOnCancel: true
                        });
                    }
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

    var onOpenAddInv = function (idkey, oprgt, oprd) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenShowvalINV",
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

    var onOpenAddExp = function (idkey, oprgt, oprd) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenShowvalEXP",
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

    var onOpenAddRjt = function (idkey, oprgt, oprd) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenShowvalRJT",
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

    var onOpen_grpIsue = function (idkey, oprgt, oprd) {
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
                    $("#_vmGrpIsue").modal("show");
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

    var onCheckdocmandor = function (idkey) {
        var jsoncoll = "";
        var jsonreposn = "";
        var oprgt = $("#diadu").val()
        $.ajax({
            url: "HTL/clnCheckDocmandor",
            type: "POST",
            data: { stated: idkey, paridno: oprgt },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                App.unblockUI();
                if (data.moderror == false) {
                    $(".showtangan").html("");
                    $(".showpending").html("");
                    $(".showcncl").html("");
                    $(".showapt").html("");
                    $(".showpenakd").html("");
                    $(".showsht").html("");
                    $(".btn4submit").hide();
                    if (data.alsb == true) {
                        $(".btn4submit").show();
                    }
                    $(data.vienm).html(data.view);
                    $(".mlt").multiSelect();
                    $('.select2').select2();

                    $("#modesht").val(data.valu);

                    if ($("#table_txttnhspa").length > 0) {
                        $("#table_txttnhspa").DataTable().destroy();
                        $("#table_txttnhspa").DataTable({
                            responsive: true
                        });
                    }

                    if (data.vienm == ".showpending") {
                        var valustr = data.valu.split(",");
                        valustr.forEach(element => $("#CaseCaPendingbMulti").multiSelect('select', element));
                    } else if (data.vienm == ".showtangan") {
                        var valustr = data.valu.split(",");
                        valustr.forEach(element => $("#CaseMulti").multiSelect('select', element));
                    } else if (data.vienm == ".showpenakd") {
                        var valustr = data.valu.split(",");
                        valustr.forEach(element => $("#CaseCaPendingAkdMulti").multiSelect('select', element));
                    }
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

    var onCheckdocmandornk = function (idkey) {
        var jsoncoll = "";
        var jsonreposn = "";
        var oprgt = $("#diadu").val()
        $.ajax({
            url: "HTL/clnCheckDocmandornk",
            type: "POST",
            data: { stated: idkey, paridno: oprgt },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({ target: "#datadialogaupdhtlipt" });
            },
            success: function (data) {
                App.unblockUI("#datadialogaupdhtlipt");
                if (data.moderror == false) {
                    var json = $.parseJSON(data.dtn);
                    $(json).each(function (i, val) {
                        $.each(val, function (k, v) {
                            if (k.toLowerCase() != "nik") {
                                if (k.search("Tgl") < 0) {
                                    $("#" + k).val(v).change();
                                } else {
                                    $("#" + k).datepicker('setDate', v);
                                }
                            }
                        });
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
    };

    var onCheckProvchange = function (idkey) {
        var jsoncoll = "";
        var jsonreposn = "";
        var oprgt = $("#diadu").val()
        $.ajax({
            url: "HTL/clnCheckProvchange",
            type: "POST",
            data: { stated: idkey, paridno: oprgt },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({ target: "#datadialogaupdhtlipt" });
            },
            success: function (data) {
                App.unblockUI("#datadialogaupdhtlipt");
                if (data.moderror == false) {
                    //$("#LokasiTanahDiKota").select2('destroy').empty();
                    var jsp = JSON.parse(data.dtn);
                    $("#LokasiTanahDiKota").select2().empty();
                    $.map(jsp, function (o) {
                        $("#LokasiTanahDiKota").append("<option value=\"" + o.Value + "\">" + o.Text + "</option>")
                    }),
                        $("#LokasiTanahDiKota").select2();
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

    var onApplySbmt = function (parForm, prm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();
        //valid == true
        if (valid == true) {
            //$("#datadialogaupdhtl").modal("hide");
            swal({
                title: "Konfirmasi",
                text: "Yakin ingin menambah/mengubah data ? , Jika anda sudah yakin silahkan tekan tombol 'Ya'",
                type: "warning",
                showCancelButton: true,
                confirmButtonText: 'Ya',
                cancelButtonText: "Tidak",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $("#ctex").val(prm);
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
                                    //$("#datadialogaupdhtl").modal("hide");
                                    swal({
                                        title: "Informasi",
                                        text: data.msg,
                                        type: "info",
                                        showConfirmButton: true,
                                        closeOnConfirm: true,
                                        closeOnCancel: true,
                                        confirmButtonText: "Tutup",
                                    }, function () {
                                        if (data.resulted != "1") {
                                            //$("#datadialogaupdhtl").modal("show");
                                        } else {
                                            if (data.mode == "submit") {
                                                vmHomePages.LoadMenu('Data Transaksi', 'HTLLIST', 'HTL', 'clnHeaderTx');
                                            }
                                            if (data.mode == "simpan") {
                                                $("#ctex").val(data.diadu);
                                                $("#diadu").val(data.diadu);
                                                $("#keylookupdataHTX").val(data.gtid);
                                            }
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
                    }
                }
            )
        }
    };



    var onRoyaSbmt = function (parForm, prm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();
        //valid == true
        if (valid == true) {
            //$("#datadialogaupdhtl").modal("hide");
            swal({
                title: "Konfirmasi",
                text: "Yakin ingin menambah/mengubah data ? , Jika anda sudah yakin silahkan tekan tombol 'Ya'",
                type: "warning",
                showCancelButton: true,
                confirmButtonText: 'Ya',
                cancelButtonText: "Tidak",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $("#ctex").val(prm);
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
                                    //$("#datadialogaupdhtl").modal("hide");
                                    swal({
                                        title: "Informasi",
                                        text: data.msg,
                                        type: "info",
                                        showConfirmButton: true,
                                        closeOnConfirm: true,
                                        closeOnCancel: true,
                                        confirmButtonText: "Tutup",
                                    }, function () {
                                        if (data.resulted != "1") {
                                            //$("#datadialogaupdhtl").modal("show");
                                        } else {
                                            if (data.mode == "submit") {
                                                vmHomePages.LoadMenu('Data Transaksi', 'HTLLIST', 'HTL', 'clnHeaderTx');
                                            }
                                            if (data.mode == "simpan") {
                                                $("#ctex").val(data.diadu);
                                                $("#diadu").val(data.diadu);
                                                $("#keylookupdataHTX").val(data.gtid);
                                            }
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
                    }
                }
            )
        }
    };

    var onApplySbmtIpt = function (parForm, prm, gd, idgd) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();

        //valid == true
        if (valid == true) {
            $("#datadialogaupdhtlipt").modal("hide");
            swal({
                title: "Konfirmasi",
                text: "Yakin ingin memproses data ? , Jika anda sudah yakin silahkan tekan tombol 'Ya'",
                type: "warning",
                showCancelButton: true,
                confirmButtonText: 'Ya',
                cancelButtonText: "Tidak",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        // $("#ctex").val(prm);
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
                                    $("#datadialogaupdhtlipt").modal("hide");
                                    swal({
                                        title: "Informasi",
                                        text: data.msg,
                                        type: "info",
                                        showConfirmButton: true,
                                        closeOnConfirm: true,
                                        closeOnCancel: true,
                                        confirmButtonText: "Tutup",
                                    }, function () {
                                        if (data.resulted != "1") {
                                            $("#datadialogaupdhtlipt").modal("show");
                                        } else {
                                            var modesht = $("#modesht").val();
                                            $("#" + gd).html(data.view);
                                            $("#" + idgd).dataTable();
                                            $("#modesht").val(modesht);

                                            if (data.viewppatbro != "") {
                                                options = $('#OrderKeNotaris');
                                                options.empty();
                                                datapat = JSON.parse(data.viewppatbro);
                                                options.append('<option value="">Pilih..</option>');
                                                $.each(datapat, function (idx, obj) {
                                                    options.append('<option value=' + obj.Value + '>' + obj.Text + '</option>');
                                                });
                                            }
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
                    } else {
                        $("#datadialogaupdhtlipt").modal("show");
                    }
                }
            )
        }
    };

    var onApplyView = function (modul, curmodul, idkey, parmode) {
        var jsoncoll = "";
        var jsonreposn = "";
        $.ajax({
            url: "Regmitra/clnWFHeaderTxView",
            type: "POST",
            data: { module: modul, curmodule: curmodul, paramkey: idkey, mode: parmode },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                App.unblockUI();
                if (data.moderror == false) {
                    $("#filterdatadialogpredoc").html(data.view);
                    $("#tbluppaprvform").hide();
                    $("#backpage").remove();
                    $("#backpageprevbtn").show();

                    $("#backpageprev").unbind("click");
                    $("#backpageprev").bind("click", function () {
                        $("#filterdatadialogpredoc").html("");
                        $("#tbluppaprvform").show();
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
            }
        });
    };

    var onApplyViewChg = function (parm, prm1) {
        var jsoncoll = "";
        var jsonreposn = "";

        var token = $('input[name="__RequestVerificationToken"]', parm).val();
        var pusang = $("#pilihubahstatusmitra").val();
        var putet = $("#LastComment4Admin").val()
        var putet3 = $("#StatusDate").val()

        $.ajax({
            url: "Regmitra/clnHeaderTxChg",
            type: "POST",
            data: { __RequestVerificationToken: token, nokeypop: prm1, sangu: pusang, sange: putet, togeub: putet3 },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                App.unblockUI();
                if (data.moderror == false) {
                    $("#dialogubah").modal("hide");
                    swal({
                        title: "Informasi",
                        text: data.msg,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    }, function () {
                        if (data.rslt != "1") {
                            $("div").removeClass("modal-backdrop");
                            $("#dialogubah").modal("show");
                        } else {
                            var datax = $("#pilihubahstatusmitra option:selected").text();
                            $("#curstat").html(datax);
                            $("#StatusDate").val("")
                            $("#pilihubahstatusmitra").val("").change();
                            $("#LastComment4Admin").val("");
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
            }
        });
    };

    //var onApplySbmt = function (parForm) {
    //    var jsoncoll = "";
    //    var jsonreposn = "";
    //    var frm = $(parForm);
    //    var valid = $(parForm).valid();
    //    var sbt = $("#StatusFollow").val();

    //    if (valid == true || sbt == "5") {
    //        var formdata = new FormData();
    //        var datax = frm.serializeArray();
    //        $.each(datax, function (i, field) {
    //            formdata.append(field.name, field.value);
    //        });

    //        $("[type='file']").each(function () {
    //            var files = $(this).get(0).files;
    //            for (var i = 0; i < files.length; i++) {
    //                formdata.append("files", files[i]);
    //            }
    //        });

    //        $.ajax({
    //            type: frm.attr('method'),
    //            url: frm.attr('action'),
    //            processData: false,
    //            contentType: false,
    //            data: formdata,
    //            //data: frm.serialize(),
    //            beforeSend: function () {
    //                App.blockUI();
    //            },
    //            success: function (data) {
    //                if (data.moderror == false) {
    //                    App.unblockUI();
    //                    if (data.resulted == "1") {
    //                        swal({
    //                            title: "Informasi",
    //                            text: data.msg,
    //                            type: "info",
    //                            showConfirmButton: true,
    //                            confirmButtonText: "Tutup",
    //                        }, function () {
    //                            if (data.url == "/") {
    //                                window.location.href = data.url;
    //                            } else {
    //                                $("#header_notification_bar").remove();
    //                                $(data.view).insertBefore(".dropdown-user");
    //                                $("#backpage").click();
    //                            }
    //                        });
    //                    } else {
    //                        $("#Msg").html(data.msg);
    //                        $("#MsgPanel").show();
    //                    }
    //                } else {
    //                    window.location.href = data.url;
    //                }
    //            },
    //            error: function (x, y, z) {
    //                App.unblockUI();
    //                jsoncoll = JSON.stringify(x);
    //                jsonreposn = JSON.parse(jsoncoll);
    //                if (jsonreposn.responseJSON.mesage !== "") {
    //                    swal({
    //                        title: "Informasi",
    //                        text: jsonreposn.responseJSON.mesage,
    //                        type: "info",
    //                        showConfirmButton: true,
    //                        confirmButtonText: "Tutup",
    //                    }, function () {
    //                        if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
    //                    });
    //                } else {
    //                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
    //                }
    //            },
    //        });
    //    } else {
    //        $("#MsgPanel").show();
    //        $("#Msg").html("Silahkan Lengkapi Form Pengajuan dan Pilih tindakan");
    //    }
    //};

    var onApplySbmtdoc = function (parForm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();

        var formdata = new FormData();
        var datax = frm.serializeArray();
        $.each(datax, function (i, field) {
            formdata.append(field.name, field.value);
        });

        $("[type='file']").each(function () {
            var files = $(this).get(0).files;
            for (var i = 0; i < files.length; i++) {
                formdata.append("files", files[i]);
            }
        });

        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            processData: false,
            contentType: false,
            data: formdata,
            //data: frm.serialize(),
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
                            $("#refres").click();
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
                if (jsonreposn.responseJSON.mesage !== "") {
                    swal({
                        title: "Informasi",
                        text: jsonreposn.responseJSON.mesage,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    }, function () {
                        if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                    });
                } else {
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            },
        });
    };

    var onOpenFilterRoya = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenFilterpopRoya",
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

                    var lengthbrn = ($('#SelectBranch > option').length) - 1;
                    if (lengthbrn == 1) {
                        $('#SelectBranch').val($('#SelectBranch option:eq(1)').val()).change();
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

    var onOpenFilter = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnOpenFilterpop",
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

                    var lengthbrn = ($('#SelectBranch > option').length) - 1;
                    if (lengthbrn == 1) {
                        $('#SelectBranch').val($('#SelectBranch option:eq(1)').val()).change();
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

    var onLoadIpt = function (aped, jned, grd, idd) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "HTL/clnRgridIpt",
            type: "POST",
            data: { ap: aped, jn: jned },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#" + grd).html(data.view);
                    $("#" + idd).dataTable();
                    //$('.date-picker').datepicker({
                    //    rtl: App.isRTL(),
                    //    orientation: "left",
                    //    autoclose: true
                    //});
                    //$('.select2').select2();
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


    var onApplyFilterRoya = function (parForm, pardownloadexcel) {
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
                console.log(data)
                if (data.moderror == "1") {
                    $(".modal-backdrop").hide();
                    $("#filterdatadialog").modal("hide");
                    if (data.view !== "") {
                        $("#pagecontent").html(data.view);
                    }
                    App.unblockUI();
                    swal({
                        title: "Informasi",
                        text: data.message,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    });
                } else if (data.moderror == false) {
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

    var onProsesAndal = function (param1, param2, param3) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(param1);
        var formdata = new FormData();
        var datax = frm.serializeArray();
        var mode = $("#mode").val();
        formdata.append("caption", param2);
        formdata.append("menu", param3);
        formdata.append("mode", mode);
        //if (mode == "2") {
        $.each(datax, function (i, field) {
            formdata.append(field.name, field.value);
        });
        $("[type='file']").each(function () {
            var files = $(this).get(0).files;
            for (var i = 0; i < files.length; i++) {
                formdata.append("files", files[i]);
            }
        });
        //}

        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            processData: false,
            contentType: false,
            data: formdata,
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    swal({
                        title: "Informasi",
                        text: data.msgdt,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    }, function (dt) {
                        if (data.isdwn == "") {
                            var a = document.createElement("a");
                            document.body.appendChild(a);
                            a.style = "display: block";
                            var bytes = new Uint8Array(data.flbt);
                            var MIME_TYPE = data.flbtmtype;
                            var blob = new Blob([bytes], { type: MIME_TYPE });
                            var url = window.URL.createObjectURL(blob);
                            a.href = url;
                            a.download = data.flbtnm;
                            a.click();
                        }
                        $("#fileandal").val("");
                        $("#mode").val("").change();
                        $("#tglpro").val("");
                        $("#flinfo").html("");
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
    };

    var onOpenvwmitra = function (prmkey, prmkey1, prmkey2) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Regmitra/clnOpenvwmitr",
            type: "POST",
            data: { kyup: prmkey, kyup1: prmkey1, kyup2: prmkey2 },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#grdl").hide();
                    $("#uidivvw").html(data.view);
                    $("#uidivvw").show();
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

    var onChkwmitra = function (prmkey) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "Regmitra/clnRecekExistmitra",
            type: "POST",
            data: { nokeypop: prmkey },
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
    };

    var onvalidateMitraPop = function (prm) {
        var jsoncoll = "";
        var jsonreposn = "";

        //var parser=$("#Typereg").val();
        var parser = $("#RegmitraType").val();
        var parcb = $("#Cabang").val();
        var parnm = $("#searchmitra").val();

        $.ajax({
            url: "Regmitra/clnOpenFilterpopmitra",
            type: "POST",
            data: { cb: parcb, tpe: parser, nm: parnm },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    if (data.fndop == "") {
                        //$("#RegmitraType").val(parser).change();
                        //$("#frmregit .form-control").val("");
                        if (data.view !== "") {
                            $("#DataMitra").html(data.view);
                        }
                        if (data.view1 !== "") {
                            $("#DataRek").html(data.view1);
                        }
                        if (data.view0 !== "") {
                            $("#DataCabang").html(data.view0);
                        }
                        if (data.view2 !== "") {
                            $("#DataJob").html(data.view2);
                        }
                        if (data.view3 !== "") {
                            $("#DataSearch").html(data.view3);
                        }

                        if (data.view4 !== "") {
                            $("#DataMitralok").html(data.view4);
                        }

                        vmRegismitra.init();

                        /*
                        if (data.isdoc == "") {
                            $("#doc").remove();
                        }
                        */

                        if (data.isjob == "") {
                            $("#paneljob").hide();
                            $("#DataJob").html("");
                        } else {
                            $("#paneljob").show();
                        }

                        if (data.isrk == "") {
                            $("#panelrk").hide();
                        } else {
                            $("#panelrk").show();
                        }

                        $("#searchmitra").val(data.searctxt);

                        /*
                        $("#reboncat").hide();
                        $("#additinfo").show();
                        if (parser == "10" || parser == "20" || parser == "30") {
                            $("#reboncat").show();
                        }

                        if (parser == "20" || parser == "30") {
                            $("#additinfo").hide();
                        }
                        */

                        $("#keylookupdataDTX").val(data.idx);
                        $("#frmregit .disabled").attr("disabled", "disabled");

                        var lengthbrn = ($('#Area > option').length) - 1;
                        if (lengthbrn == 1) {
                            $('#Area').val($('#Area option:eq(1)').val()).change();
                        }
                        var lengthbrn = ($('#Divisi > option').length) - 1;
                        if (lengthbrn == 1) {
                            $('#Divisi').val($('#Divisi option:eq(1)').val()).change();
                        }
                    }

                    $("#MsgPanel").hide();
                    $("#Msg").html(data.msg);
                    if (data.msg !== "" && prm == "1") {
                        $("#MsgPanel").show();
                    }
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

    var onLoadChange = function (keypop) {
        var jsoncoll = "";
        var jsonreposn = "";
        var parserpm = keypop;
        var parcb = "";
        var parreg = $("#RegmitraType").val();
        var hdjb = $("#handleJob").val();

        if (keypop == "ar") {
            parcb = $("#Area").val();
        } else if (keypop == "dv") {
            parcb = $("#tglmasuk").val() + "|" + $("#Divisi").val();
        } else if (keypop == "cnt") {
            parcb = $("#tglmasuk").val() + "|" + $("#Divisi").val();;
        } else {
            return;
        }

        $.ajax({
            url: "Regmitra/clnLoadChange",
            type: "POST",
            data: { kep: parcb, kepreg: parreg, tpe: parserpm },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
                    App.unblockUI();
                    $("#" + data.idpop).html(data.view);
                    $("#" + data.idpop1).html(data.view1);
                    $("#" + data.idpop3).datepicker().datepicker("setDate", data.dt3);
                    $("#" + data.idpop4).val("").change();
                    $("#handleJob").val(hdjb).change();
                    if (hdjb == "") {
                        var lengthbrn = ($('#handleJob > option').length) - 1;
                        if (lengthbrn == 1) {
                            $('#handleJob').val($('#handleJob option:eq(1)').val()).change();
                        }
                    }
                    $('.select2').select2({});
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
                url: "Regmitra/clnOpenFilterpop",
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

    var _formatFileSize = function (bytes) {
        if (typeof bytes !== 'number') {
            return '';
        }
        if (bytes >= 1000000000) {
            return (bytes / 1000000000).toFixed(2) + ' GB';
        }
        if (bytes >= 1000000) {
            return (bytes / 1000000).toFixed(2) + ' MB';
        }
        return (bytes / 1000).toFixed(2) + ' KB';
    };

    var onsearchmitra = function (obj) {
        var serach = $(obj).val();
        if (serach == 10) {
            $("#searchmitra").val("");
        }
        //$("#doc").show();
        onvalidateMitraPop();
        $("#MsgPanel").hide();
    };

    var onchkfledl = function (parsecnocon, ent, obj, parmko) {
        var jsoncoll = "";
        var jsonreposn = "";

        if (ent == "0") {
            $(obj).closest('tr').find('td label').html("");
            $(obj).closest('tr').find("td input").prop("checked", false);
            // $(obj).closest('tr').find("td input").val("");
        } else {
            $.ajax({
                url: "Regmitra/clnchkfledl",
                type: "POST",
                dataType: "json",
                data: { secnocon: parsecnocon, geolo: parmko },
                beforeSend: function () {
                    App.blockUI({});
                },
                success: function (x) {
                    var parfrm = "";
                    if (x.moderror == false) {
                        if (x.msg != "") {
                            App.unblockUI();
                            swal({
                                title: "Informasi",
                                text: x.msg,
                                type: "info",
                                showConfirmButton: true,
                                confirmButtonText: "Tutup",
                            }, function () {
                                if (x.rst == "1") {
                                    $(obj).closest('tr').find('td label').html("File Dihapus");
                                    $(obj).closest('tr').find("td input").prop("checked", false);
                                    $("#MsgPanel").hide();
                                    //$(obj).closest('tr').find("td input").val("");
                                }
                            });
                        }
                        App.unblockUI();
                    } else {
                        window.location.href = x.url;
                    }
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            });
        }
    };

    var onchkfle = function (parsecnocon, parsecnoconmode, partryfil) {
        var jsoncoll = "";
        var jsonreposn = "";
        if (partryfil == "ckalled" || partryfil == "ckalledd") {
            if (parsecnocon == "[object HTMLSelectElement]") {
                parsecnocon = $("#DokumenTyped").val();
            }
        }

        $.ajax({
            url: "HTL/clnchkfle",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { secnocon: parsecnocon, coontpe: parsecnoconmode, clnfdc: partryfil },
            beforeSend: function () {
                App.blockUI({ target: "#dialogmodal" });
            },
            success: function (x) {
                alert('msuk');
                var parfrm = "";
                var selecttedhtml = "";
                var linked = "";
                if (x.moderror == false) {
                    if (x.msg != "") {
                        App.unblockUI("#dialogmodal");
                        $("#dialogwarkahpre").modal("hide");
                        $("#dialogwarkah").modal("hide");
                        swal({
                            title: "Informasi",
                            text: x.msg,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        }, function () {
                            if (x.bytetyipe != "" && x.bytetyipe != "null" && x.bytetyipe != null) {
                                if (parsecnoconmode == "pre") {
                                    $("#dialogwarkahpre").modal("show");
                                } else {
                                    $("#dialogwarkah").modal("show");
                                }
                            }
                        });
                        viewerUrl = "";
                    } else {
                        if (x.dwn == "1") {
                            var a = document.createElement("a");
                            document.body.appendChild(a);
                            a.style = "display: block";
                            var bytes = new Uint8Array(x.bytetyipe);
                            var MIME_TYPE = x.contenttype;
                            var blob = new Blob([bytes], { type: MIME_TYPE });
                            var url = window.URL.createObjectURL(blob);
                            a.href = url;
                            a.download = x.filename;
                            a.click();
                        } else {
                            byteArray = new Uint8Array(x.bytetyipe);
                            link = window.URL.createObjectURL(new Blob([byteArray], { type: x.contenttype }));
                            viewerUrl = x.viewpath + encodeURIComponent(link + "#" + x.filename);
                            //blob = new Blob([file], { type: "application/pdf;base64" }),
                            //objurl = window.URL.createObjectURL(blob);
                            parfrm = '<iframe src="' + link + '" width="100% " height="500px" id="frmbro"></iframe>';
                            if (x.infoselect == "") {
                                if (parsecnoconmode == "pre" || parsecnoconmode == "pretemp") {
                                    $("#warkahdocpre").html(parfrm);
                                    App.unblockUI("#dialogmodal");
                                } else {
                                    $("#warkahdoc").html(parfrm);
                                    App.unblockUI("#dialogmodal");
                                    $("#captdoc").html(x.filename);
                                }
                            }

                            if (parsecnoconmode == "pre" || parsecnoconmode == "pretemp") {
                                $("#captpre").html(x.cap);
                                App.unblockUI("#dialogmodal");
                                $("#dialogwarkahpre").modal("show");
                            } else {
                                $("#capt").html(x.cap);
                                App.unblockUI("#dialogmodal");
                                $("#dialogwarkah").modal("show");
                                $("div").removeClass("modal-backdrop");
                            }
                        }
                    }
                } else {
                    window.location.href = x.url;
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

    var onchkflevw = function (parmpat, param1, param2) {
        var jsoncoll = "";
        var jsonreposn = "";
        var token = $('input[name="__RequestVerificationToken"]', $("#clnHTL_form")).val();

        $.ajax({
            url: parmpat,
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { __RequestVerificationToken: token, eux: param1, aux: param2 },
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (x) {
                var parfrm = "";
                if (x.moderror == false) {
                    if (x.msg != "") {
                        App.unblockUI();
                        swal({
                            title: "Informasi",
                            text: x.msg,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        });
                        viewerUrl = "";
                    } else {
                        byteArray = new Uint8Array(x.bytetyipe);
                        //link = window.URL.createObjectURL(new Blob([byteArray], { type: x.contenttype }));
                        //viewerUrl = x.viewpath + encodeURIComponent(link + "#" + x.filename);
                        //window.open(viewerUrl, '', 'height=650,width=840');
                        var a = document.createElement("a");
                        document.body.appendChild(a);
                        a.style = "display: block";
                        var bytes = new Uint8Array(x.bytetyipe);
                        var blob = new Blob([bytes], { type: x.contenttype });
                        var url = window.URL.createObjectURL(blob);
                        a.href = url;
                        a.download = x.filename;
                        a.click();
                    }
                    App.unblockUI();
                } else {
                    window.location.href = x.url;
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

    var onSharePK = function (kyp, kyp1) {
        var jsoncoll = "";
        var jsonreposn = "";
        swal({
            title: "Konfirmasi",
            text: "Pastikan PKS Mitra sudah diupload oleh cabang/area, Jika anda yakin tekan  'Ya' untuk terbitkan SPL",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        }, function (confrim) {
            if (confrim) {
                $.ajax({
                    url: "Regmitra/clnOpenclikp",
                    type: "POST",
                    data: { secnocon: kyp, ty: kyp1 },
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

    var onOpenAddPemberkasan = function (vparcno, varkepo) {
        var jsoncoll = "";
        var jsonreposn = "";
        var tempkepo = varkepo;
        $.ajax({
            url: "Regmitra/clnKoncePlod",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { paridno: vparcno, parkepo: varkepo },
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.moderror == false) {
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

    var SetCoordinates = function (c) {
        $('#imgX1').val(c.x);
        $('#imgY1').val(c.y);
        $('#imgWidth').val(c.w);
        $('#imgHeight').val(c.h);
        $('#btnCrop').show();
    };

    return {
        OpenAdd: function (modul, idkey, oprgt) {
            onOpenAdd(modul, idkey, oprgt);
        },

        OpenAddRoya: function (modul, idkey, oprgt) {
            onOpenAddRoya(modul, idkey, oprgt);
        },

        OpenAddinv: function (modul, idkey, oprgt) {
            onOpenAddInv(modul, idkey, oprgt);
        },

        OpenAddExp: function (modul, idkey, oprgt) {
            onOpenAddExp(modul, idkey, oprgt);
        },

        OpenAddRJT: function (modul, idkey, oprgt) {
            onOpenAddRjt(modul, idkey, oprgt);
        },

        OpenAddIpt: function (modul, idkey, idrel, oprgt, op1, op2) {
            onOpenAddIpt(modul, idkey, idrel, oprgt, op1, op2);
        },

        LoadIpt: function (ap, jn, gd, id) {
            onLoadIpt(ap, jn, gd, id);
        },

        Openhst: function (idkey, oprgt, opr) {
            onOpenhst(idkey, oprgt, opr);
        },
        Open_grpIsue: function (idkey, oprgt, opr) {
            onOpen_grpIsue(idkey, oprgt, opr);
        },

        Checklist: function (status, idname) {
            onChecklist(status, idname);
        },

        CetakSerti: function (idnama, codeprev) {
            onCetakSerti(idnama, codeprev);
        },

        CetakSertiht: function (idnama, codeprev) {
            onCetakSertiht(idnama, codeprev);
        },

        proBAST: function (idnama, codeprev) {
            onproBAST(idnama, codeprev);
        },

        procfmBAST: function (idnama, codeprev) {
            onprocfmBAST(idnama, codeprev);
        },

        proslaBAST: function (idnama, codeprev) {
            onproslaBAST(idnama, codeprev);
        },

        prorolBAST: function (idnama, codeprev) {
            onprorolBAST(idnama, codeprev);
        },

        downexcel: function (idnama, codeprev) {
            ondownexcel(idnama, codeprev);
        },

        CetakSertima: function (idnama, codeprev) {
            onCetakSertima(idnama, codeprev);
        },

        OpenCatat: function (kt) {
            swal({
                title: "Catatan ",
                text: kt,
                type: "info",
                showConfirmButton: true,
                confirmButtonText: "Tutup",
            });
        },

        Checkdocmandor: function (diadu) {
            onCheckdocmandor(diadu);
        },

        Checkdocmandornk: function (diadu) {
            onCheckdocmandornk(diadu);
        },

        Checkchangeprov: function (diadu) {
            onCheckProvchange(diadu);
        },

        //SharePK: function (ex, eu) {
        //    onSharePK(ex, eu)
        //},
        kumonprosplit: function (ex, eu, ea) {
            $("#eux").val(ex);
            $("#aux").val(eu);
            ea = "/Regmitra/kumonprospliter" + ea;
            $("#frmkumonprospliter").attr("action", ea);
            $("#btnsupflow").click();
        },

        kumonprosplitvw: function (ex, eu, ea) {
            $("#eux").val(ex);
            $("#aux").val(eu);
            ea = "/HTL/vw" + ea;
            onchkflevw(ea, ex, eu);
        },

        JcopBegin: function () {
            $("#dialogIman").modal("hide");
            App.blockUI();
        },
        JcopSuccess: function (x) {
            if (x.moderror == false) {
                $('input[name="Nama"]').val(x.Nama);
                $('input[name="NIK"]').val(x.Nik);
                $('input[name="JKelamin"]').val(x.JenisKelaminDesc);
                $('input[name="Tptlahir"]').val(x.TempatLahir);
                $('input[name="Tgllahir"]').val(x.TanggalLahir);
                $('input[name="Warga"]').val(x.Kewarganegaraan);
                $('input[name="Pekerjaan"]').val(x.Pekerjaan);
                $('input[name="Alamat"]').val(x.Alamat);
                $('input[name="RT"]').val(x.Rt);
                $('input[name="RW"]').val(x.Rw);
                $('input[name="Provinsi"]').val(x.Provinsi);
                $('input[name="Kota"]').val(x.Kota);
                $('input[name="Kecamatan"]').val(x.Kec);
                $('input[name="DesaKelurahan"]').val(x.Kel);
                App.unblockUI();
                $("#dialogIman").modal("show");
            } else {
                window.location.href = x.url;
            }
        },

        JcopOpen(tpedata) {
            var jsoncoll = "";
            var jsonreposn = "";
            idrelate = $("#diadu").val();
            $.ajax({
                url: "HTL/clnOpenAddHTLIPTDBT",
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
                        $("#iman").html(data.view);
                        $('#dialogIman').draggable({
                            handle: ".modal-header"
                        });
                        $(".modal-header").unbind("click");
                        $(".modal-header").bind("click", function () {
                            $(".modal-bodyx").toggle();
                        });
                        $("#dialogIman").modal("show");
                        vmHTL.JcopSet();
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
        },

        JcopSet() {
            $('#FileUpload1').change(function () {
                if ($('#Image1').data('Jcrop')) {
                    $('#Image1').data('Jcrop').destroy();
                    $("#Image1").removeAttr("style");
                }
                $('#Image1').hide();
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#Image1').show();
                    $('#Image1').attr("src", e.target.result);
                    $('#Image1').Jcrop({
                        onChange: SetCoordinates,
                        onSelect: SetCoordinates,
                    });
                }
                reader.readAsDataURL($(this)[0].files[0]);
            });

            $("#btnresetCrop").click(function () {
                if ($('#Image1').data('Jcrop')) {
                    $('#Image1').data('Jcrop').destroy();
                    $("#Image1").removeAttr("style");
                    $('.btnUpload').hide();
                    var canvas = $("#canvas")[0];
                    canvas.height = 0;
                    canvas.width = 0;
                }
                $('#Image1').hide();
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#Image1').show();
                    $('#Image1').attr("src", e.target.result);
                    $('#Image1').Jcrop({
                        onChange: SetCoordinates,
                        onSelect: SetCoordinates,
                    });
                }
                reader.readAsDataURL($("#FileUpload1")[0].files[0]);
            });

            $('#btnCrop').click(function () {
                if (document.getElementById("FileUpload1").files.length == 0) {
                    $("#datadialogaupdhtlipt").modal("hide");
                    swal({
                        title: "Informasi",
                        text: "Silahkan Pilih file image (.jpg/.jpeg)",
                        type: "info",
                        showCancelButton: true,
                        confirmButtonText: 'Ya',
                        cancelButtonText: "Tidak",
                        closeOnConfirm: true,
                        closeOnCancel: true
                    }, function (confirm) {
                        if (confirm) {
                            $("#datadialogaupdhtlipt").modal("show");
                        }
                    });
                    return;
                }

                var x1 = $('#imgX1').val();
                var y1 = $('#imgY1').val();
                var width = $('#imgWidth').val();
                var height = $('#imgHeight').val();
                var canvas = $("#canvas")[0];
                var context = canvas.getContext('2d');
                var img = new Image();
                var imgbase;
                img.onload = function () {
                    canvas.height = height;
                    canvas.width = width;
                    context.drawImage(img, x1, y1, width, height, 0, 0, width, height);
                    $('#imgCropped').val(canvas.toDataURL());
                    imgbase = canvas.toDataURL();
                };
                img.src = $('#Image1').attr("src");

                var difname = "";
                if ($("#NIK").length == 0) {
                    difname = "dbt";
                }

                var formData = new FormData();
                formData.append('imgCropped', $('#Image1').attr("src"));
                formData.append('difname', difname);

                $.ajax({
                    url: "HTL/Save",
                    type: "POST",
                    //contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: formData,
                    contentType: false,
                    processData: false,
                    beforeSend: function () {
                        App.blockUI({ target: "#datadialogaupdhtlipt" });
                    },
                    success: function (data) {
                        App.unblockUI("#datadialogaupdhtlipt");
                        if (data.moderror == false) {
                            if (data.sm == "dbt") {
                                $('input[name="NamaDebitur"]').val(data.Nama);
                                $('input[name="NIKDebitur"]').val(data.Nik);
                                $('#JKelaminDebitur').val(data.JenisKelaminDesc).change();
                                $('input[name="TptLahirDebitur"]').val(data.TempatLahir);
                                $('input[name="TgllahirDebitur"]').val(data.TanggalLahir);
                                $('#WargaDebitur').val(data.Kewarganegaraan).change();
                                $('input[name="PekerjaanDebitur"]').val(data.Pekerjaan);
                                $('#StatusDebitur').val(data.StatusPerkawinan);
                                $('input[name="AlamatDebitur"]').val(data.Alamat);
                                $('input[name="RTDebitur"]').val(data.Rt);
                                $('input[name="RWDebitur"]').val(data.Rw);
                                $('input[name="ProvinsiDebitur"]').val(data.Provinsi);
                                $('input[name="KotaDebitur"]').val(data.Kota);
                                $('input[name="KecamatanDebitur"]').val(data.Kec);
                                $('input[name="DesaKelurahanDebitur"]').val(data.Kel);
                                $("#datadialogaupdhtlipt").modal("hide");
                            } else {
                                $('input[name="Nama"]').val(data.Nama);
                                $('input[name="NIK"]').val(data.Nik);
                                $('#JKelamin').val(data.JenisKelaminDesc).change();
                                $('input[name="Tptlahir"]').val(data.TempatLahir);
                                $('input[name="Tgllahir"]').val(data.TanggalLahir);
                                $('#Warga').val(data.Kewarganegaraan).change();
                                $('input[name="Pekerjaan"]').val(data.Pekerjaan);
                                $('#StatusPernikahan').val(data.StatusPerkawinan);
                                $('input[name="Alamat"]').val(data.Alamat);
                                $('input[name="RT"]').val(data.Rt);
                                $('input[name="RW"]').val(data.Rw);
                                $('input[name="Provinsi"]').val(data.Provinsi);
                                $('input[name="Kota"]').val(data.Kota);
                                $('input[name="Kecamatan"]').val(data.Kec);
                                $('input[name="DesaKelurahan"]').val(data.Kel);
                            }
                        } else {
                            window.location.href = data.url;
                        }
                    },
                    error: function (x, y, z) {
                        App.unblockUI("#datadialogaupdhtlipt");
                        jsoncoll = JSON.stringify(x);
                        jsonreposn = JSON.parse(jsoncoll);
                        if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                    }
                });
            });

            //$("#btnUpload").click(function () {
            //    alert("sdsdsd");
            //    $("#formocr").submit();
            //});
        },
        openAddDoc(opx) {
            onOpenAddDoc(opx);
        },

        openAddDocRoya(opx) {
            onOpenAddDocRoya(opx);
        },

        chkfle(parsecnocon, parsecnoconmode, parcontype) {
            onchkfle(parsecnocon, parsecnoconmode, parcontype);
        },

        Ochkfle() {
            onOpenAddDocvw();
        },

        ApplySbmt: function (frm, prm) {
            onApplySbmt(frm, prm);
        },
        RoyaSbmt: function (frm, prm) {
            onRoyaSbmt(frm, prm);
        },

        ApplySbmtIpt: function (frm, prm, gd, idgd) {
            onApplySbmtIpt(frm, prm, gd, idgd);
        },

        OpenFilter() {
            onOpenFilter();
        },
        OpenFilterRoya() {
            onOpenFilterRoya();
        },
        ApplyFilter: function (parForm, pardownloadexcel) {
            onApplyFilter(parForm, pardownloadexcel);
        },
        ApplyFilterRoya: function (parForm, pardownloadexcel) {
            onApplyFilterRoya(parForm, pardownloadexcel);
        },

        ResetFilter: function () {
            onResetFilter();
        },

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

        BackForm: function (parm, parm1) {
            if ($('#grdl').length > 0)         // use this if you are using id to check
            {
                $("#grdl").show();
                $("#uidivadd").html("");
                $("#uidivadd").hide();
            } else {
                vmHomePages.LoadMenu('Data Transaksi', 'HTLLIST', 'HTL', 'clnHeaderTx')
            }
        },

        setvalhandle(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16) {
            $('.select2').select2({
            });

            $.fn.datepicker.defaults.format = "dd-MM-yyyy";
            $('.date-picker').datepicker({
                rtl: App.isRTL(),
                orientation: "bottom",
                autoclose: true
            });
            var lengthbrn = ($('#OrderKeNotaris > option').length) - 1;
            if (lengthbrn == 1) {
                $('#OrderKeNotaris').val($('#OrderKeNotaris option:eq(1)').val()).change();
            } else {
                $('#OrderKeNotaris').val(x1).change();
            }

            $("#Keterangan").val("");

            $("#JKelaminDebitur").val(x2).change();
            $("#WargaDebitur").val(x3).change();
            $("#JKelaminPemilikSertifikat").val(x4).change();
            $("#WargaPemilikSertifikat").val(x5).change();

            $("#TglSuratUkur").datepicker('setDate', x6);
            $("#TgllahirDebitur").datepicker('setDate', x7);
            $("#TgllahirPemilikSertifikat").datepicker('setDate', x8);
            $("#TglPerjanjian").datepicker('setDate', x9);

            $("#StatusDebitur").val(x13).change();

            $.validator.setDefaults({
                ignore: []
            });

            $("#CaseMulti").multiSelect();
            var valustr = x14.split(",");
            valustr.forEach(element => $("#CaseMulti").multiSelect('select', element));

            if ($("#CaseCaPendingbMulti").length > 0) {
                $("#CaseCaPendingbMulti").multiSelect();
                var valustr = x15.split(",");
                valustr.forEach(element => $("#CaseCaPendingbMulti").multiSelect('select', element));
            }

            if ($("#CaseCaPendingAkdMulti").length > 0) {
                $("#CaseCaPendingAkdMulti").multiSelect();
                var valustr = x16.split(",");
                valustr.forEach(element => $("#CaseCaPendingAkdMulti").multiSelect('select', element));
            }

            //vmHTL.rupiahbro(x10, "#idNilaiHT", "1");
            //vmHTL.rupiahbro(x11, "#idNilaiPinjamanDiterima", "1");
            //vmHTL.rupiahbro(x12, "#idLuasTanah", "0");
        },

        initbuton: function () {
            $(".btnshtvw").unbind("click");
            $(".btnshtvw").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                ea = "/HTL/vw" + par2;
                onchkflevw(ea, par1, par2);
            });

            $(".OpenFilter").unbind("click");
            $(".OpenFilter").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmHTL.OpenFilter(par0);
            });

            $(".OpenFilterRoya").unbind("click");
            $(".OpenFilterRoya").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmHTL.OpenFilterRoya(par0);
            });

            $(".LoadMenuIpt").unbind("click");
            $(".LoadMenuIpt").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                var par3 = pop[3].replace(/["]/g, "");
                vmHTL.LoadIpt(par0, par1, par2, par3);
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

            $(".tutup,.tangguh,.terbit,.allht,.menuggu,.baruaju,.allht").unbind("click");
            $(".tutup,.tangguh,.terbit,.allht,.menuggu,.baruaju,.allht").bind("click", function () {
                var ModuleName = "xx";
                var ckk = $("#koreksi:checked").val();
                if (ckk != "on") {
                    ckk = "";
                }
                if (this.className == "terbit") {
                    ModuleName = "HTLLISTHT4" + ckk;
                } else if (this.className == "tangguh") {
                    ModuleName = "HTLLISTHT1" + ckk;
                } else if (this.className == "tutup") {
                    ModuleName = "HTLLISTHT2" + ckk;
                }
                else if (this.className == "menuggu") {
                    ModuleName = "HTLLISTHT3" + ckk;
                } else if (this.className == "allht") {
                    ModuleName = "HTLLISTHT" + ckk;
                } else if (this.className == "serachsht") {
                    ModuleName = "HTLLISTHT" + ckk;
                } else if (this.className == "baruaju") {
                    ModuleName = "HTLLISTHT5" + ckk;
                }

                var par0 = "Transaksi Data";
                var par1 = ModuleName;
                var par2 = "HTL"
                var par3 = "clnHeaderTxHT";
                var par4 = $(".serachsht").val();

                vmHomePages.LoadMenu(par0, par1, par2, par3, par4);
            });
            $(".royadone,.prosroya,.allroya").unbind("click");
            $(".royadone,.prosroya,.allroya").bind("click", function () {
                var ModuleName = "xx";
                var ckk = $("#koreksi:checked").val();
                if (ckk != "on") {
                    ckk = "";
                }
                if (this.className == "royadone") {
                    ModuleName = "ROYADONE" + ckk;
                } else if (this.className == "prosroya") {
                    ModuleName = "ROYAPROS" + ckk;
                } else if (this.className == "allroya") {
                    ModuleName = "ROYAALL" + ckk;
                }
                
                var par0 = "Transaksi Data";
                var par1 = ModuleName;
                var par2 = "HTL"
                var par3 = "clnHeaderTxRoyaPros";
                var par4 = $(".serachsht").val();

                vmHomePages.LoadMenu(par0, par1, par2, par3, par4);
            });

            $(".cetserti").unbind("click");
            $(".cetserti").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.CetakSerti(par0, par1);
            });

            $(".cetsertiht").unbind("click");
            $(".cetsertiht").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.CetakSertiht(par0, par1);
            });

            $(".downexel").unbind("click");
            $(".downexel").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");

                vmHTL.downexcel(par0, par1);
            });

            $(".cretbast").unbind("click");
            $(".cretbast").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var popp = $("#reooo").val();
                if (popp == "EASIClLM2c3pncvbrI64qA==") {
                    $("#namaidpool").val(par0);
                    $("#prevedid").val(par1);
                    btndownload.click();
                } else {
                    vmHTL.proBAST(par0, par1);
                }
            });

            $(".cfrmbast").unbind("click");
            $(".cfrmbast").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.procfmBAST(par0, par1);
            });

            $(".schcfrmbast").unbind("click");
            $(".schcfrmbast").bind("click", function () {
                var jsoncoll = "";
                var jsonreposn = "";

                $.ajax({
                    url: "HTL/clnOpenAddBASTSCH",
                    type: "POST",
                    data: { gontok: '' },
                    //contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        App.blockUI({});
                    },
                    success: function (data) {
                        if (data.moderror == false) {
                            App.unblockUI();
                            $("#uidivadd").html(data.view);
                            vmHTL.updatinitbast();
                            $("#OpenAddUploadRegisHTFlag").modal("show");
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

            $(".slabast").unbind("click");
            $(".slabast").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.proslaBAST(par0, par1);
            });

            $(".rolbast").unbind("click");
            $(".rolbast").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.prorolBAST(par0, par1);
            });

            $(".advfilerbast").unbind("click");
            $(".advfilerbast").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var parmenu = pop[0].replace(/["]/g, "");
                var parcaption = pop[1].replace(/["]/g, "");
                var ruteed = pop[2].replace(/["]/g, "");
                var actioned = pop[3].replace(/["]/g, "");

                var tblfilter = pop[4].replace(/["]/g, "");
                var str = $("#" + tblfilter + "> label > input").val();

                if (str == "") {
                    swal({
                        title: "Informasi",
                        text: "Isikan No BAST / No Perjanjian pada field \"Cari Data\"",
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    });
                    return false;
                }

                var jsoncoll = "";
                var jsonreposn = "[]";
                $(".modal-backdrop").remove();
                $.ajax({
                    url: ruteed + "/" + actioned,
                    type: "POST",
                    //contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: { menu: parmenu, caption: parcaption, tipemodule: "", kd: str },
                    beforeSend: function () {
                        App.blockUI({});
                    },
                    success: function (data) {
                        if (data.moderror == false) {
                            $("#pagecontent").html(data.view);
                            App.unblockUI();
                            $("#reooo").val("EASIClLM2c3pncvbrI64qA==");
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
            });

            $(".upgdht").unbind("click");
            $(".upgdht").bind("click", function () {
                var jsoncoll = "";
                var jsonreposn = "";

                $.ajax({
                    url: "HTL/clnOpenAddUploadRegis",
                    type: "POST",
                    data: { gontok: '' },
                    //contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        App.blockUI({});
                    },
                    success: function (data) {
                        if (data.moderror == false) {
                            App.unblockUI();
                            $("#uidivadd").html(data.view);
                            vmHTL.updatinit();
                            $("#OpenAddUploadRegisHTFlag").modal("show");
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

            $(".cetbukser").unbind("click");
            $(".cetbukser").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.CetakSertima(par0, par1);
            });

            $(".cetterima").unbind("click");
            $(".cetterima").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.CetakSertima(par0, par1);
            });

            $(".cekedbox").unbind("click");
            $(".cekedbox").bind("click", function () {
                vmHTL.Checklist(this.checked, 'AktaSelectdwn');
            });

            $(".cetbrks").unbind("click");
            $(".cetbrks").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.CetakSerti(par0, par1);
            });

            $(".openadd").unbind("click");
            $(".openadd").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.OpenAdd(par0, par1);
            });

            $(".openaddipt").unbind("click");
            $(".openaddipt").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                var par3 = pop[3].replace(/["]/g, "");
                var par4 = pop[4].replace(/["]/g, "");
                var par5 = pop[5].replace(/["]/g, "");
                vmHTL.OpenAddIpt(par0, par1, par2, par3, par4, par5);
            });

            $(".open4view").unbind("click");
            $(".open4view").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.OpenAdd(par0, par1, par2);
            });

            $(".openRoyaview").unbind("click");
            $(".openRoyaview").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.OpenAddRoya(par0, par1, par2);
            });

            $(".open4edit").unbind("click");
            $(".open4edit").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.OpenAdd(par0, par1, par2);
            });

            $(".open4editInv").unbind("click");
            $(".open4editInv").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.OpenAddinv(par0, par1, par2);
            });

            $(".open4editExp").unbind("click");
            $(".open4editExp").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.OpenAddExp(par0, par1, par2);
            });

            $(".open4editRjt").unbind("click");
            $(".open4editRjt").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.OpenAddRJT(par0, par1, par2);
            });

            $(".open4editipt").unbind("click");
            $(".open4editipt").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                var par3 = pop[3].replace(/["]/g, "");
                var par4 = pop[4].replace(/["]/g, "");
                var par5 = pop[5].replace(/["]/g, "");
                vmHTL.OpenAddIpt(par0, par1, par2, par3, par4, par5);
            });

            $(".open4viewtipt").unbind("click");
            $(".open4viewtipt").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                var par3 = pop[3].replace(/["]/g, "");
                var par4 = pop[4].replace(/["]/g, "");
                var par5 = pop[5].replace(/["]/g, "");
                vmHTL.OpenAddIpt(par0, par1, par2, par3, par4, par5);
            });

            $(".open4riwayat").unbind("click");
            $(".open4riwayat").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.Openhst(par0, par1, par2);
            });

            $(".openedit_grouping").unbind("click");
            $(".openedit_grouping").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                vmHTL.Open_grpIsue(par0, par1, par2);
            });

            $(".filtehtlref").unbind("click");
            $(".filtehtlref").bind("click", function () {
                vmHTL.ResetFilter('#DashboardFilter_form');
            });

            $(".filtehtl").unbind("click");
            $(".filtehtl").bind("click", function () {
                vmHTL.ApplyFilter('#DashboardFilter_form', '');
            });

            $(".filteRoya").unbind("click");
            $(".filteRoya").bind("click", function () {
                vmHTL.ApplyFilterRoya('#DashboardFilter_form', '');
            });

            $("#DokumenTyped").unbind("change");
            $("#DokumenTyped").bind("change", function () {
                vmHTL.chkfle(this, 'ckalled', 'ckalled');
            });

            $(".uplodsrt").unbind("click");
            $(".uplodsrt").bind("click", function () {
                FormFileUpload.subsvelodded();
            });

            $(".uplodspa").unbind("click");
            $(".uplodspa").bind("click", function () {
                FormFileUpload.subsveloddedspa();
            });

            $(".uplodppk").unbind("click");
            $(".uplodppk").bind("click", function () {
                FormFileUpload.subsveloddedppk();
            });

            $(".uppttdnsb").unbind("click");
            $(".uppttdnsb").bind("click", function () {
                FormFileUpload.subsveloddedttd();
            });

            $(".btn4save").unbind("click");
            $(".btn4save").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.ApplySbmt(par0, par1);
            });


            $(".btnRoyasave").unbind("click");
            $(".btnRoyasave").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.RoyaSbmt(par0, par1);
            });

            $(".btn4saveipt").unbind("click");
            $(".btn4saveipt").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                var par2 = pop[2].replace(/["]/g, "");
                var par3 = pop[3].replace(/["]/g, "");
                $("#ctexipt").val(par1);
                vmHTL.ApplySbmtIpt(par0, par1, par2, par3);
            });

            $(".btn4saveiptpsg").unbind("click");
            $(".btn4saveiptpsg").bind("click", function () {
                $(".PSG").val("");
                $(".PSGCMB").val("").change();
            });

            $(".btn4submit").unbind("click");
            $(".btn4submit").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                var par1 = pop[1].replace(/["]/g, "");
                vmHTL.ApplySbmt(par0, par1);
            });

            $(".btn4chfle").unbind("click");
            $(".btn4chfle").bind("click", function () {
                vmHTL.Ochkfle();
            });

            $(".btn4close").unbind("click");
            $(".btn4close").bind("click", function () {
                vmHTL.BackForm('#clnHTL_form', '');
            });

            $(".btn4closeipt").unbind("click");
            $(".btn4closeipt").bind("click", function () {
                $('#datadialogaupdhtlipt').modal("hide");
            });

            $(".btnIMan").unbind("click");
            $(".btnIMan").bind("click", function () {
                vmHTL.JcopOpen();
                vmHTL.JcopSet();
            });

            $(".btnUpDocced").unbind("click");
            $(".btnUpDocced").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                vmHTL.openAddDoc(par0);
            });

            $(".btnUpDoccedRoya").unbind("click");
            $(".btnUpDoccedRoya").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                vmHTL.openAddDocRoya(par0);
            });

            var x10 = $("#NilaiHT").val();
            var x11 = $("#NilaiPinjamanDiterima").val();
            var x12 = $("#NilaiHT").val();

            vmHTL.rupiahbro(x10, "#idNilaiHT", "1");
            vmHTL.rupiahbro(x11, "#idNilaiPinjamanDiterima", "1");
            vmHTL.rupiahbro(x12, "#idLuasTanah", "0");
        },

        updatinitbast: function () {
            $(".submtflagbast").unbind("click");

            var datax = $('#JenisTransaksi').select2('data');
            var msgcfm = datax[0].text;
            $(".submtflagbast").bind("click", function (e) {
                e.preventDefault();
                $("#OpenAddUploadRegisHTFlag").modal("hide");
                swal({
                    title: "Konfirmasi",
                    text: msgcfm + " ?",
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
                            formData.append("NoBast", $("#NoBast").val());
                            formData.append("Tglbast", $("#Tglbast").val());
                            formData.append("upflebyr", $("#upflebyr")[0].files[0]);
                            formData.append("__RequestVerificationToken", token);
                            $.ajax({
                                url: 'HTL/clnOpenAddBASTSCHFLAG',
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
                            $("#OpenAddUploadRegisHTFlag").modal("show");
                        }
                    });
            });
        },

        updatinit: function () {
            $(".submtflag").unbind("click");
            $(".submtflag").bind("click", function (e) {
                e.preventDefault();
                $("#OpenAddUploadRegisHTFlag").modal("hide");
                swal({
                    title: "Konfirmasi",
                    text: "Yakin ingin memproses Data Pendaftaran SHT ?",
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
                                url: 'HTL/clnOpenAddUploadRegisHTFlag',
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
                            $("#OpenAddUploadRegisHTFlag").modal("show");
                        }
                    });
            });
        },
        init: function () {
            $('input.NonEdittable').css("border", "0px");
            $('.NonEdittableSpanTxtArea').css("border", "0px");
            $('.NonEdittableSpanTxtArea').css("height", "50px");
            $('.tgl,.dtt').css("z-index", "0");

            $('.NonEdittableSpanTxt').css("border", "0px");

            $('input.Numberonly').on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');
            });

            $('input.NumberonlyDesc').on('input', function () {
                this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');
            });

            $('input.Textonly').on('input', function () {
                this.value = this.value.replace(/[0-9]/g, '');
            });

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
                vmHTL.rupiahbro(angka, "#id" + nm, "1");
            });

            //numeric value validation
            $('input.floatNumberdisp').on('input', function () {
                this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');
            });
            $('input.floatNumberdisp').on('focus', function () {
                $(this).select();
            });

            $('input.floatNumberdisp').on('keyup', function () {
                var angka = $(this).val();
                var nm = this.id;
                vmHTL.rupiahbro(angka, "#id" + nm, "0");
            });

            //integer value validation
            $('input.floatNumber').on('input', function () {
                this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');
            });
            $('input.floatNumber').on('focus', function () {
                $(this).select();
            });
            $('input.floatNumber').on('blur', function () {
                var angka = $(this).val();
                vmHTL.rupiahbro(angka, this, "0");
            });

            $('input.floatNumber').on('keyup', function () {
                var angka = $(this).val();
                vmHTL.rupiahbro(angka, "#idrup", "1");
            });

            $('.select2').select2({}).on("change", function (e) {
                $(this).valid();
                if (this.id.toLowerCase() == "status") {
                    vmHTL.Checkdocmandor($(this).val());
                }
            });

            $.validator.setDefaults({
                ignore: []
            });
            $.getScript("Scripts/jquery.validate.unobtrusive.min.js", function () {
                $("#frmregit").data("unobtrusiveValidation", null);
                $("#frmregit").data("validator", null);
                $.validator.unobtrusive.parse($("#frmregit"));
            });

            $(".nkSrc").on('click', function () {
                var nik = $(".nkSrccmb").val();
                vmHTL.Checkdocmandornk(nik);
            });

            $("#LokasiTanahDiProvinsi").on('change', function () {
                var nik = $(this).val();
                vmHTL.Checkchangeprov(nik);
            });

            //$("#Tgllahir,#tglmasuk,#tglakhir").datepicker({
            //    defaultDate: null
            //});

            $(".dtt").datepicker().on('change', function () {
                $(this).valid();
            });

            //$("#Alamat,#AlamatKorespodensi").on("keypress", function (e) {
            //    var key = e.keyCode;
            //    if (key == 13) {
            //        return false;
            //    }
            //    else {
            //        return true;
            //    }
            //});
        },
    }
}();