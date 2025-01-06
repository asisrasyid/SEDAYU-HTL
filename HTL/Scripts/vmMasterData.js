var elemtid = "body";
var elemntupload = ".modal-content";
var popupwindowwarkah = null;

var vmMasterData = function () {
    var onOpenAdd = function (modul, idkey, oprgt) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "MasterData/clnOpenAdd" + modul,
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
                    $("#keylookup").val(data.keydata);
                    if (data.loadcabang == "1") {
                        onSearchCabangAjax(data.cabang);
                    }
                    $("#datadialogaddupdatemaster").modal("show");
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

    var onDelAdd = function (modul, idkey) {
        var jsoncoll = "";
        var jsonreposn = "";
        var idkeyset = idkey;
        swal({
            title: "Konfirmasi",
            text: "Yakin ingin menghapus data ? , Jika anda sudah yakin silahkan tekan tombol 'Ya'",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: "Tidak"
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: "MasterData/clndelAdd" + modul,
                        type: "POST",
                        data: { paramkey: idkeyset },
                        //contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.moderror == false) {
                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                }, function () {
                                    if (data.resulted == "1") {
                                        $("#gridMasterData").html(data.view);
                                        $("#table_List_master").dataTable();
                                    }
                                });
                            } else {
                                window.location.href = data.url;
                            }
                            App.unblockUI()
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

    var onApplySbmt = function (parForm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);
        var valid = $(parForm).valid();
        if (valid == true) {
            $("#datadialogaddupdatemaster").modal("hide");
            swal({
                title: "Konfirmasi",
                text: "Yakin ingin menabah/mengubah data ? , Jika anda sudah yakin silahkan tekan tombol 'Ya'",
                type: "warning",
                showCancelButton: true,
                confirmButtonText: 'Ya',
                cancelButtonText: "Tidak",
                closeOnConfirm: true,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
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
                                    $("#datadialogaddupdatemaster").modal("hide");
                                    swal({
                                        title: "Informasi",
                                        text: data.msg,
                                        type: "info",
                                        showConfirmButton: true,
                                        confirmButtonText: "Tutup",
                                    }, function () {
                                        if (data.resulted != "1") {
                                            $("#datadialogaddupdatemaster").modal("show");
                                        } else {
                                            $("#gridMasterData").html(data.view);
                                            $("#table_List_master").dataTable();
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
                        $("#datadialogaddupdatemaster").modal("show");
                    }
                }
            )
        }
    };

    var onOpenFilter = function (modul) {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "MasterData/clnOpenFilter" + modul,
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
                    $("#keyword").val(data.opsi1);
                    $("#SelectDivisi").val(data.opsi2).change();
                    $("#SelectPengajuan").val(data.opsi3).change();
                    $("#IsActiveData").prop("checked", data.opsi13);
                    $("#IsPusatData").prop("checked", data.opsi14);
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
    var onApplyFilter = function (parForm) {
        var jsoncoll = "";
        var jsonreposn = "";
        var frm = $(parForm);

        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize(),
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
            },
            success: function (data) {
                if (data.moderror == false) {
                    if (data.message == "") {
                        $("#gridMasterData").html(data.view);
                        $("#table_List_master").dataTable();
                        $("#filterdatadialog").modal("hide");
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

    var onSearchCabangAjax = function (selectcab) {
        var Parclientid = $("#ClientId").val();
        var datajson = "[]";
        var options = "undefined";
        var jsoncoll = "";
        var jsonreposn = "[]";
        var Parreg = $("#RegionID").val();
        var selectcab1 = selectcab;

        $.ajax({
            url: "MasterData/clnGetBranch",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { clientid: Parclientid, regionid: Parreg },
            beforeSend: function () {
                App.blockUI({ target: elemtid });
            },
            success: function (data) {
                if (data.moderror == false) {
                    datajson = JSON.parse(data.branchjson);
                    options = $("#Cabang");
                    options.empty();

                    options.append('<option value="">Pilih..</option>');
                    $.each(datajson, function (idx, obj) {
                        options.append('<option value=' + obj.Value + '>' + obj.Text + '</option>');
                    });

                    if (typeof selectcab1 !== "undefined") {
                        $("#Cabang").val(selectcab1).trigger("change");
                    } else {
                        $("#Cabang").val(data.brachselect).trigger("change");
                    }

                    options.select2();
                    App.unblockUI(elemtid);
                } else {
                    window.location.href = data.url;
                }
            },
            error: function (x, y, z) {
                App.unblockUI(elemtid);
                jsoncoll = JSON.stringify(x);
                jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            }
        });
    };

    return {
        OpenAdd: function (modul, idkey, oprgt) {
            onOpenAdd(modul, idkey, oprgt);
        },
        DelAdd: function (modul, idkey) {
            onDelAdd(modul, idkey);
        },
        ApplySbmt: function (frm) {
            onApplySbmt(frm);
        },
        OpenFilter(modul) {
            onOpenFilter(modul);
        },
        ApplyFilter: function (parForm) {
            onApplyFilter(parForm);
        },
        ResetFilter: function (parForm) {
            onResetFilter(parForm);
        },
        SearchCabangAjax: function () {
            onSearchCabangAjax();
        },
        changeTxtdoc: function (obj, objt) {
            var txt = $(obj).find(":selected").text();
            $(objt).val(txt);
        },
        Gtflie: function (obj) {
            var datafile = $(obj).get(0).files;
            var htmled = datafile[0].name;
            var txt = "";
            if (htmled.length > 50) {
                txt = "Nama file maximal 50 karakter";
            }
            else if ((datafile[0].size / 1000).toFixed(2) > 350) {
                txt = "Ukuran file maximal 350 KB";
            }
            if (datafile[0].type != "image/jpg" && datafile[0].type != "image/jpeg" && datafile[0].type != "application/pdf") {
                txt = "Tipe file yang boleh diupload .jpg/.jpeg/.pdf";
            }
            if (txt != "") {
                swal({
                    title: "Informasi",
                    text: txt,
                    type: "info",
                    showConfirmButton: true,
                    confirmButtonText: "Tutup",
                }, function () {
                    $(obj).val("");
                });
            }
        },
        init: function () {
            $('input.NonEdittable').css("border", "0px");
            $('input.Numberonly').on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');
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
                var posisidecimal = angka.indexOf(".");
                var angkadecimal = angka.substring(posisidecimal, angka.length);
                var angkaribuan = angka.substring(0, posisidecimal);
                if (posisidecimal < 0) {
                    angkaribuan = angka;
                    angkadecimal = "";
                }
                var reverse = angkaribuan.toString().split('').reverse().join('');
                var ribuan = reverse.match(/\d{1,3}/g);
                ribuan = ribuan.join(',').split('').reverse().join('') + angkadecimal;
                $(this).val(ribuan);
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
                vmMasterData.rupiahbro(angka, "#id" + nm, "1");
            });
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

        initbuton: function () {
            $(".OpenFilter").unbind("click");
            $(".OpenFilter").bind("click", function () {
                var pop = $(this).attr("data-value");
                var par0 = pop.replace(/["]/g, "");
                vmMasterData.OpenFilter(par0);
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

            $(".OpenAdd").unbind("click");
            $(".OpenAdd").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/["]/g, "");
                vmMasterData.OpenAdd(par0, "");
            });

            $(".masteradd").unbind("click");
            $(".masteradd").bind("click", function () {
                vmMasterData.ApplySbmt('#clnMaster_form');
            });

            $(".masteraddmodif,.masteraddlihat").unbind("click");
            $(".masteraddmodif,.masteraddlihat").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/[']/g, "");
                var par1 = pop[1].replace(/[']/g, "");
                var par2 = pop[2].replace(/[']/g, "");
                vmMasterData.OpenAdd(par0, par1, par2);
            });

            $(".masteraddhpus").unbind("click");
            $(".masteraddhpus").bind("click", function () {
                var pop = $(this).attr("data-value");
                pop = pop.split(",");
                var par0 = pop[0].replace(/[']/g, "");
                var par1 = pop[1].replace(/[']/g, "");
                vmMasterData.DelAdd(par0, par1);
            });

            $(".masterfilterref").unbind("click");
            $(".masterfilterref").bind("click", function () {
                vmMasterData.ResetFilter('#MasterFilterForm_form');
            });

            $(".masterfilter").unbind("click");
            $(".masterfilter").bind("click", function () {
                vmMasterData.ApplyFilter('#MasterFilterForm_form', '')
            });

            //vmMasterData.rupiahbro(x10, "#idNilaiHT", "1");
            //vmMasterData.rupiahbro(x11, "#idNilaiPinjamanDiterima", "1");
            //vmMasterData.rupiahbro(x12, "#idLuasTanah", "0");
        },

        initcur: function (x10, x11, x12, x13, e10, e11, e12, e13) {
            vmMasterData.rupiahbro(x10, "#id" + e10, "1");
            vmMasterData.rupiahbro(x11, "#id" + e11, "1");
            vmMasterData.rupiahbro(x12, "#id" + e12, "1");
            vmMasterData.rupiahbro(x13, "#id" + e13, "1");
        },

        validobjt: function (obj, parm) {
            $(obj).valid();
        },
    }
}();