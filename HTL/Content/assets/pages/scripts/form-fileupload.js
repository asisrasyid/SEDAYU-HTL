var FormFileUpload = function () {

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

    var onchkfledl = function (parsecnocon, ent, obj, parmko) {
        var jsoncoll = "";
        var jsonreposn = "";

        if (ent == "0") {
            $(obj).closest('tr').find('td label').html("");
            $(obj).closest('tr').find("td input").prop("checked", false);
            // $(obj).closest('tr').find("td input").val("");
        } else {
            $.ajax({
                url: "HTL/clnchkfledl",
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
                            $("#dialogupdoc").modal("hide");
                            swal({
                                title: "Informasi",
                                text: x.msg,
                                type: "info",
                                showConfirmButton: true,
                                confirmButtonText: "Tutup",
                            }, function () {
                                if (x.rst == "1") {
                                    $(obj).closest('tr').find('td label').html("");
                                    $(obj).closest('tr').find("input[type='file']").val("");
                                    $(obj).closest('tr').find('td .btnflednw').unbind("change");
                                    $(obj).closest('tr').find('td .btnflednw').bind("change", function () {
                                        FormFileUpload.Gtflienw(this, "0", "0");
                                    });
                                }
                                $("#dialogupdoc").modal("show");
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
                parsecnocon = $("#documentselect").val();
            }
        }

        $.ajax({
            url: "Regmitra/clnchkfle",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { secnocon: parsecnocon, coontpe: parsecnoconmode, clnfdc: partryfil },
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (x) {
                var parfrm = "";
                var selecttedhtml = "";
                var linked = "";
                if (x.moderror == false) {
                    if (x.msg != "") {
                        App.unblockUI();
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
                            parfrm = '<iframe src="' + viewerUrl + '" width="100% " height="500px" id="frmbro"></iframe>';
                            if (x.infoselect == "") {
                                if (parsecnoconmode == "pre" || parsecnoconmode == "pretemp") {
                                    $("#warkahdocpre").html(parfrm);
                                } else {
                                    $("#warkahdoc").html(parfrm);
                                }
                            }
                            if (parsecnoconmode == "pre" || parsecnoconmode == "pretemp") {
                                $("#captpre").html(x.cap);
                                $("#dialogwarkahpre").modal("show");
                            } else {
                                $("#capt").html(x.cap);
                                $("#dialogwarkah").modal("show");
                                $("div").removeClass("modal-backdrop");
                            }

                        }
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


    return {
        //main function to initiate the module
        Cekfileup: function (param) {
            var nopel = $("#noappl").val();
            if (nopel.length != 14) {
                txt = "No Aplikasi harus 14 digit";
                swal({
                    title: "Informasi",
                    text: txt,
                    type: "info",
                    showConfirmButton: true,
                    confirmButtonText: "Tutup",
                });
            } else {
                nopel = param == "rf" ? "" : nopel;
                $.ajax({
                    url: "HTL/clnKoncePloddoc",
                    type: "POST",
                    //contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: { paridno: nopel },
                    beforeSend: function () {
                        App.blockUI({});
                    },
                    success: function (x) {
                        $("#pagecontent").html("");
                        $("#pagecontent").html(x.view);
                        $("#noappl").val(nopel);
                        if (nopel != "") {
                            $("#noappl").attr("readonly", "readonly");
                            $("#ipt").hide();
                            $("#uplod").show();
                        } else {
                            $("#ipt").show();
                            $("#uplod").hide();
                        }
                        App.unblockUI();
                    },
                });
            }
        },

        Gtflie: function (obj, cntproo, paramt) {
            var datafile = $(obj).get(0).files;
            var htmled = datafile[0].name;
            var txt = "";
            var noappl = $("#noappl").val();

            if (htmled.length > 100) {
                txt = "Nama file maximal 100 karakter";
            }
            else if ((datafile[0].size / 1000).toFixed(2) > 1000 && cntproo != "SERTIFIKAT") {
                txt = "Ukuran file maximal 1 MB";
            }
            else if ((datafile[0].size / 1000).toFixed(2) > 5000 && (cntproo == "SERTIFIKAT" || cntproo == "MEMO PPYM")) {
                txt = "Ukuran file maximal 5 MB";
            }
            else if (datafile[0].type != "image/jpg" && datafile[0].type != "image/jpeg" && datafile[0].type != "application/pdf") {
                txt = "Tipe file yang boleh diupload .jpg/.jpeg/.pdf";
            }
            else if (noappl.length != 14) {
                txt = "No Aplikasi harus 14 digit dfdfd";
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
            } else {
                var doc = $(obj).closest('tr').find("td input").val();
                var token = $('input[name="__RequestVerificationToken"]').val();
                var formdata = new FormData();
                formdata.append("files", datafile[0]);
                formdata.append("cntproo", cntproo);
                formdata.append("__RequestVerificationToken", token);
                formdata.append("documen", doc);
                formdata.append("noappl", noappl);

                $.ajax({
                    url: "HTL/clnKoncePloddoconesve",
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: formdata,
                    dataType: "json",
                    beforeSend: function () {
                        App.blockUI({});
                    },
                    success: function (data) {
                        if (data.moderror == false) {
                            App.unblockUI();
                            if (parseInt(data.idrst) > 0) {
                                var sz = _formatFileSize(datafile[0].size);
                                $(obj).closest('tr').find('td label').html("File : " + htmled + " <br /> Size : " + sz);
                                var pop = $(obj).closest('tr').find("td input").prop("checked", true);

                                $(obj).closest('tr').find('td #btnviewbtn,#btndelbtn').removeAttr("onclick");
                                $(obj).closest('tr').find('td #btnviewbtn').unbind("click");
                                $(obj).closest('tr').find('td #btnviewbtn').bind("click", function () {
                                    if (paramt == 'pretemp') {
                                        vmRegismitra.chkfle(data.golpod, paramt);
                                    } else {
                                        vmRegismitra.chkfleone(data.golpod);
                                    }
                                });
                                $(obj).closest('tr').find('td #btndelbtn').unbind("click");
                                $(obj).closest('tr').find('td #btndelbtn').bind("click", function () {
                                    vmRegismitra.chkfledl(data.golpod, data.golpod, this, paramt);
                                });
                                $("#noappl").attr("readonly", "readonly");
                                $("#MsgPanel").hide();
                            } else {
                                $(obj).closest('tr').find('td label').html(data.msg);
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
            }
        },


        Gtflienw: function (obj, cntproo, paramt, kyp) {
            var datafile = $(obj).get(0).files;
            var htmled = datafile[0].name;
            var txt = "";

            var noappl = $("#ctexdoc").val();

            if (htmled.length > 100) {
                txt = "Nama file maximal 100 karakter";
            }
            else if ((datafile[0].size / 1000).toFixed(2) > 1000 && cntproo != "SERTIFIKAT") {
                txt = "Ukuran file maximal 1 MB";
            }
            else if ((datafile[0].size / 1000).toFixed(2) > 5000 && (cntproo == "SERTIFIKAT" || cntproo == "MEMO PPYM")) {
                txt = "Ukuran file maximal 5 MB";
            }
            else if (datafile[0].type != "image/jpg" && datafile[0].type != "image/jpeg" && datafile[0].type != "application/pdf") {
                txt = "Tipe file yang boleh diupload .jpg/.jpeg/.pdf";
            }

            if (txt != "") {
                $("#dialogupdoc").modal("hide");
                $(obj).val("");
                swal({
                    title: "Informasi",
                    text: txt,
                    type: "info",
                    showConfirmButton: true,
                    closeOnConfirm: true,
                    closeOnCancel: true,
                    confirmButtonText: "Tutup",
                }, function () {
                    $("#dialogupdoc").modal("show");
                });
            } else {
                var doc = $(obj).closest('tr').find("td input").val();
                var token = $('input[name="__RequestVerificationToken"]').val();
                var formdata = new FormData();
                formdata.append("files", datafile[0]);
                formdata.append("cntproo", cntproo);
                formdata.append("__RequestVerificationToken", token);
                formdata.append("documen", doc);
                formdata.append("noappl", noappl);
                formdata.append("keypro", paramt);

                $.ajax({
                    url: "HTL/clnKoncePloddoconesvenw",
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: formdata,
                    dataType: "json",
                    beforeSend: function () {
                        App.blockUI
                            ({ target: '.modal-content' });
                    },
                    success: function (data) {
                        if (data.moderror == false) {
                            App.unblockUI('.modal-content');
                            $("#dialogupdoc").modal("hide");
                            swal({
                                title: "Informasi",
                                text: data.msg,
                                type: "info",
                                showConfirmButton: true,
                                closeOnConfirm: true,
                                closeOnCancel: true,
                                confirmButtonText: "Tutup",
                            }, function () {
                                if (data.resulted == "1") {
                                    var sz = _formatFileSize(datafile[0].size);
                                    $(obj).closest('tr').find('td label').html("File : " + htmled + " <br /> Size : " + sz);
                                    $(obj).closest('tr').find('td .btndelbtn').unbind("click");
                                    $(obj).closest('tr').find('td .btndelbtn').bind("click", function () {
                                        FormFileUpload.chkfledl(data.golpod, data.golpod, this, paramt);
                                    });
                                } else {
                                    $(obj).val("");
                                }
                                $("#dialogupdoc").modal("show");
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
        },

        chkfle(parsecnocon, parsecnoconmode, parcontype) {
            onchkfle(parsecnocon, parsecnoconmode, parcontype);
        },

        chkfleone(parsecnocon) {
            if (parsecnocon == "") {
                parsecnocon = $('#documentselect option:eq(1)').val();
            }
            var inp = $("#documentselect").find("option[value='" + parsecnocon + "']").length;
            if (inp > 0) {
                $("#pilihdoc").show();
                $("#documentselect").val(parsecnocon).change();
            } else {
                $("#pilihdoc").hide();
                onchkfle(parsecnocon, "ckalled", "ckalled");
            }
        },

        chkfledl: function (pamr, pamr1, obj, parm2) {
            onchkfledl(pamr, pamr1, obj, parm2);
        },

        subsvelodded: function (mode) {
            var token = $('input[name="__RequestVerificationToken"]', $("#clnHTL_form")).val();
            var files = $("#fileupload").get(0).files;
            var noappl = $("#keylookupdataHTX").val();
            var aux = $("#aux").val();
            var formdatanw = new FormData();
            formdatanw.append("__RequestVerificationToken", token);
            formdatanw.append("files", files[0]);
            formdatanw.append("noappl", noappl);
            formdatanw.append("cntproo", aux + '#81x@');
            $.ajax({
                type: "POST",
                url: 'HTL/clnKoncePloddoconesve',
                processData: false,
                contentType: false,
                data: formdatanw,
                beforeSend: function () {
                    App.blockUI();
                },
                success: function (data) {
                    if (data.moderror == false) {
                        App.unblockUI();
                        $("#infoname").html("<br />" + data.msg);
                        $("#fileupload").val("");
                    } else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, yz, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            });
        },

        subsveloddedttd: function (mode) {
            var token = $('input[name="__RequestVerificationToken"]', $("#clnHTL_form")).val();
            var files = $("#fileuploadttd").get(0).files;
            var noappl = $("#keylookupdataHTX").val();
            var aux = $("#aux").val();
            var formdatanw = new FormData();
            formdatanw.append("__RequestVerificationToken", token);
            formdatanw.append("files", files[0]);
            formdatanw.append("noappl", noappl);
            formdatanw.append("cntproo", aux + '#82x@');
            $.ajax({
                type: "POST",
                url: 'HTL/clnKoncePloddoconesve',
                processData: false,
                contentType: false,
                data: formdatanw,
                beforeSend: function () {
                    App.blockUI();
                },
                success: function (data) {
                    if (data.moderror == false) {
                        App.unblockUI();
                        $("#infonamettd").html("<br />" + data.msg);
                        $("#fileuploadttd").val("");
                    } else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, yz, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            });
        },

        subsveloddedppk: function (mode) {
            var token = $('input[name="__RequestVerificationToken"]', $("#clnHTL_form")).val();
            var files = $("#fileuploadppk").get(0).files;
            var noappl = $("#keylookupdataHTX").val();
            var aux = $("#aux").val();
            var formdatanw = new FormData();
            formdatanw.append("__RequestVerificationToken", token);
            formdatanw.append("files", files[0]);
            formdatanw.append("noappl", noappl);
            formdatanw.append("cntproo", aux + '#83x@');
            $.ajax({
                type: "POST",
                url: 'HTL/clnKoncePloddoconesve',
                processData: false,
                contentType: false,
                data: formdatanw,
                beforeSend: function () {
                    App.blockUI();
                },
                success: function (data) {
                    if (data.moderror == false) {
                        App.unblockUI();
                        $("#infonameppk").html("<br />" + data.msg);
                        $("#fileuploadppk").val("");
                    } else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, yz, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            });
        },

        subsveloddedspa: function (mode) {
            var token = $('input[name="__RequestVerificationToken"]', $("#clnHTL_form")).val();
            var files = $("#fileuploadspa").get(0).files;
            var noappl = $("#keylookupdataHTX").val();
            var aux = $("#aux").val();
            var formdatanw = new FormData();
            formdatanw.append("__RequestVerificationToken", token);
            formdatanw.append("files", files[0]);
            formdatanw.append("noappl", noappl);
            formdatanw.append("cntproo", aux + '#84x@');
            $.ajax({
                type: "POST",
                url: 'HTL/clnKoncePloddoconesve',
                processData: false,
                contentType: false,
                data: formdatanw,
                beforeSend: function () {
                    App.blockUI();
                },
                success: function (data) {
                    if (data.moderror == false) {
                        App.unblockUI();
                        $("#infonamespa").html("<br />" + data.msg);
                        $("#fileuploadspa").val("");
                    } else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, yz, z) {
                    App.unblockUI();
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            });
        },

        subsvelod: function (mode) {

            var frm = $("#fileuploadform");
            var formdata = new FormData();
            var datax = frm.serializeArray();
            $.each(datax, function (i, field) {
                formdata.append(field.name, field.value);
            });
            var formdatanw = new FormData();
            var dta = formdata.getAll('documen');
            var dtkn = formdata.get('__RequestVerificationToken');
            var noappl = formdata.get('noappl');
            if (mode != "sbal") {
                var files = $("#fileupload").get(0).files;
                if (files.length > 0 || mode == "chk") {
                    if (mode == "chk") {
                        var tglp = formdata.get('tglpro');
                        if (tglp !== "") {
                            formdatanw.append("__RequestVerificationToken", dtkn);
                            formdatanw.append("idx", "0");
                            formdatanw.append("modepro", mode);
                            formdatanw.append("tglpro", tglp);

                            $.ajax({
                                type: frm.attr('method'),
                                url: frm.attr('action'),
                                processData: false,
                                contentType: false,
                                data: formdatanw,
                                beforeSend: function () {
                                    App.blockUI();
                                },
                                success: function (data) {
                                    if (data.moderror == false) {
                                        App.unblockUI();
                                        if (data.mode == "chk") {
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
                                    } else {
                                        window.location.href = data.url;
                                    }
                                },
                                error: function (x, yz, z) {
                                    App.unblockUI();
                                    jsoncoll = JSON.stringify(x);
                                    jsonreposn = JSON.parse(jsoncoll);
                                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                                }
                            })
                        } else {
                            swal({
                                title: "Informasi",
                                text: "Isikan Tanggal Upload untuk pengecekan data yang sudah terupload",
                                type: "info",
                                showConfirmButton: true,
                                confirmButtonText: "Tutup",
                            });
                        }
                    } else {
                        $(".start").hide();
                        $(dta).each(function (x, y) {
                            var formdatanw = new FormData();
                            formdatanw.append("__RequestVerificationToken", dtkn);
                            formdatanw.append("files", files[y]);
                            formdatanw.append("idx", y);
                            formdatanw.append("modepro", mode);
                            formdatanw.append("noappl", noappl);
                            setTimeout(
                                $.ajax({
                                    type: frm.attr('method'),
                                    url: frm.attr('action'),
                                    processData: false,
                                    contentType: false,
                                    data: formdatanw,
                                    beforeSend: function () {
                                        App.blockUI();
                                    },
                                    success: function (data) {
                                        if (data.moderror == false) {
                                            $("#fl" + data.filidx).html(data.filmsg);
                                            if (data.resultsuc == "1") {
                                                $("#flbtn" + data.filidx).unbind("click");
                                                $("#flbtn" + data.filidx).addClass("btn-success");
                                                $("#flbtn" + data.filidx).removeClass("green");
                                                $("#flbtn" + data.filidx + " > i").html("berhasil")
                                                $("#flbtn" + data.filidx + " > i").addClass("fa-check");
                                                $("#flbtn" + data.filidx + " > i").removeClass("fa-remove");
                                                var jmls = $("#info").html();
                                                $("#info").html(parseInt(jmls) + 1);
                                                $("#flbtn" + data.filidx).parents("tr").remove();
                                                App.unblockUI();
                                            } else {
                                                var jmls = $("#infog").html();
                                                $("#infog").html(parseInt(jmls) + 1);
                                                App.unblockUI();
                                            }
                                        } else {
                                            window.location.href = data.url;
                                        }
                                    },
                                    error: function (x, yz, z) {
                                        App.unblockUI();
                                        jsoncoll = JSON.stringify(x);
                                        jsonreposn = JSON.parse(jsoncoll);
                                        if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                                    }
                                }), 5000);
                        });
                    }
                }
            } else {
                formdatanw.append("__RequestVerificationToken", dtkn);
                formdatanw.append("idx", "0");
                formdatanw.append("modepro", mode);
                formdatanw.append("tglpro", tglp);
                formdatanw.append("noappl", noappl);
                $.ajax({
                    type: frm.attr('method'),
                    url: frm.attr('action'),
                    processData: false,
                    contentType: false,
                    data: formdatanw,
                    beforeSend: function () {
                        App.blockUI();
                    },
                    success: function (data) {
                        if (data.moderror == false) {
                            App.unblockUI();
                            swal({
                                title: "Informasi",
                                text: data.filmsg,
                                type: "info",
                                showConfirmButton: true,
                                confirmButtonText: "Tutup",
                            }, function () {
                                if (data.resultsuc == "1") {
                                    FormFileUpload.Cekfileup('rf');
                                }
                            });

                        } else {
                            window.location.href = data.url;
                        }
                    },
                    error: function (x, yz, z) {
                        App.unblockUI();
                        jsoncoll = JSON.stringify(x);
                        jsonreposn = JSON.parse(jsoncoll);
                        if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                    }
                });
            }
        },


        init: function () {
            //hafid
            // Initialize the jQuery File Upload widget:
            ///(\.|\/)(gif|jpe?g|png)$/i

            $("#uplod").hide();
            $("#ipt").show();

            $('#fileupload,#fileuploadttd,#fileuploadppk,#fileuploadspa').on("change", function () {
                for (var i = 0; i < this.files.length; ++i) {
                    var idx = ($("#tblupp tr").length + 1) - 1;
                    var sz = _formatFileSize(this.files[i].size);
                    var szval = (this.files[i].size / 1000).toFixed(2);
                    var nmval = this.files[i].name.length;
                    if ((szval <= 1000) && (nmval <= 50)) {
                        var html = '<tr class="template-upload">' +
                            '<td>' + idx + '</td>' +
                            '<td>' + this.files[i].name + '</td>' +
                            '<td> ' + sz + '</td>' +
                            '<td><span id=fl' + idx + ' class="error-block"> </span></td >' +
                            '<td>' +
                            '<a href="javascript:;" id=flbtn' + idx + ' class="btn green btn-outline sbold opp" value = "batalkan" style="padding:0px 8px"><i class="fa fa-remove "> batalkan</i></a>' +
                            '<input type="checkbox" name="documen" value="' + (idx - 1) + '" checked style="visibility:hidden;font-size:5px" />' +
                            '</td > ' +
                            '</tr>';
                        $("#tblupp").find('tbody').append(html);

                        if (this.id == "fileupload") {
                            $("#infoname").html("<br />" + this.files[i].name);
                        }
                        if (this.id == "fileuploadspa") {
                            $("#infonamespa").html("<br />" + this.files[i].name);
                        }
                        if (this.id == "fileuploadppk") {
                            $("#infonameppk").html("<br />" + this.files[i].name);
                        }
                        if (this.id == "infonamettd") {
                            $("#infonamettd").html("<br />" + this.files[i].name);
                        }

                    } else {
                        $("#fileupload").val("").change();
                        swal({
                            title: "Informasi",
                            text: "Upload File sesuai dengan catatan diatas, Silahkan ulangi kembali",
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        }, function () {
                            $("#tblupp > tbody").empty();
                            $(".cancel").click();
                        });
                        return;
                    }
                }
                $('.opp').unbind("click");
                $('.opp').bind("click", function () {
                    var idx = $(this).closest('tr').index();
                    $(this).closest('tr').remove();
                });
            });

            $(".cancel").unbind("click");
            $(".cancel").bind("click", function () {
                $("#tblupp > tbody").empty();
                $("#fileupload").val("").change();
                $(".fileinput-button").show();
                $(".start").show();
                $("#info").html("0");
                $("#infog").html("0");
                $("#tglpro").val("");
            });

            $.fn.datepicker.defaults.format = "dd-MM-yyyy";
            $('.date-picker').datepicker({
                rtl: App.isRTL(),
                orientation: "top",
                autoclose: true
            });
        },
    };

}();

jQuery(document).ready(function () {
    FormFileUpload.init();

    $(".upploddsrt").unbind("click");
    $(".upploddsrt").bind("click", function () {
        FormFileUpload.subsvelod();
    });

    $("#uplod").unbind("click");
    $("#uplod").bind("click", function () {
        FormFileUpload.subsvelod('sbal');
    });

    $("#ipt").unbind("click");
    $("#ipt").bind("click", function () {
        FormFileUpload.Cekfileup();
    });

    $("#ref").unbind("click");
    $("#ref").bind("click", function () {
        FormFileUpload.Cekfileup('rf');
    });

    $(".btnfled").unbind("change");
    $(".btnfled").bind("change", function () {
        var pop = $(this).attr("data-value");
        pop = pop.split(",");
        var par0 = pop[0].replace(/[']/g, "");
        FormFileUpload.Gtflie(this, par0);
    });

    $(".btnflednw").unbind("change");
    $(".btnflednw").bind("change", function () {
        var pop = $(this).attr("data-value");
        pop = pop.split(",");
        var par0 = pop[0].replace(/[']/g, "");
        var par1 = pop[1].replace(/[']/g, "");
        FormFileUpload.Gtflienw(this, par0, par1);
    });

    $("#btnviewbtn").unbind("click");
    $("#btnviewbtn").bind("click", function () {
        var pop = $(this).attr("data-value");
        pop = pop.split(",");
        var par0 = pop[0].replace(/[']/g, "");
        FormFileUpload.chkfleone(par0);
    });

    $(".btndelbtn").unbind("click");
    $(".btndelbtn").bind("click", function () {
        var pop = $(this).attr("data-value");
        var tbb = $("#tblupp");
        pop = pop.split(",");
        var par0 = pop[0].replace(/[']/g, "");
        var par1 = pop[1].replace(/[']/g, "");
        var par2 = pop[2].replace(/[']/g, "");
        FormFileUpload.chkfledl(par0, par1, this, par2);
    });

});
