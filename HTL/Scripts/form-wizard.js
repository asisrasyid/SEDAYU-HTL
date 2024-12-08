var FormWizard = function () {


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
    }

    var onDownOrderRegisFile = function (parmenu, parcaption) {
        $.ajax({
            url: "Contract/clndownloadorderregisfile",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { menu: parmenu, caption: parcaption },
            beforeSend: function () {
                App.blockUI({});
            },
            success: function (data) {
                if (data.msg != "") {
                    swal({
                        title: "Informasi",
                        text: data.msg,
                        type: "info",
                        showConfirmButton: true,
                        confirmButtonText: "Tutup",
                    });
                } else {

                    var byteArray = new Uint8Array(data.datafile);
                    var blob = new Blob([byteArray], { type: data.contenttype });
                    var link = document.createElement('a');
                    link.href = window.URL.createObjectURL(blob);
                    var fileName = data.filename;
                    link.download = fileName;
                    link.click();

                }
                App.unblockUI();
            },
            error: function (x, y, z) {
                App.unblockUI();
                var jsoncoll = JSON.stringify(x);
                var jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            },
            complete: function () {
                App.unblockUI();
            }
        });

    };

    var onUploadOrderRegisFileMasal = function () {

        try {
            document.getElementById("Order_form"), addEventListener("submit", function (e) {
                var form = e.target;
                if (form.getAttribute("enctype") == "multipart/form-data") {

                    var valid = $(form).valid();
                    if (valid == true) {
                        App.blockUI({ target: elemtid });
                        if (form.dataset.ajax) {
                            e.preventDefault();
                            e.stopImmediatePropagation();
                            var xhr = new XMLHttpRequest();
                            xhr.open(form.method, form.action);
                            xhr.onreadystatechange = function () {
                                var returnedData;
                                try {
                                    returnedData = JSON.parse(xhr.responseText);
                                } catch (e) {
                                    returnedData = xhr.responseText;
                                }

                                if (xhr.readyState == 4) {
                                    if (xhr.status = 200) {
                                        $("#FileUpload").val('');
                                        $(".fileinput-filename").text('')

                                        var pesan = returnedData.msgerror != "" ? returnedData.msgerror : returnedData.msg;
                                        swal({
                                            title: "Informasi",
                                            text: pesan,
                                            type: "info",
                                            showConfirmButton: true,
                                            confirmButtonText: "Tutup",
                                        }, function () {
                                            if (returnedData.msg != "") {
                                                swal({
                                                    title: "Informasi",
                                                    text: returnedData.msg,
                                                    type: "info",
                                                    showConfirmButton: true,
                                                    confirmButtonText: "Tutup",
                                                });
                                            }

                                            if (returnedData.result == "1") {
                                                $("#totaldata").html("Pending: " + returnedData.totaldata + " Data");
                                            } else {
                                                $("#errhtml").html(returnedData.htmlmsg);
                                                $("#errhtml").show();
                                            }

                                            //$("#senddataregis").show();
                                            //$("#gridCreateOrderregis").html(returnedData.view);
                                            //$("#table_List_orderRegis").dataTable({
                                            //    "language": {
                                            //        "info": "Total Pengajuan : _TOTAL_ Kontrak",
                                            //        "sSearch": "Cari data: ",
                                            //        "lengthMenu": "_MENU_",
                                            //    },
                                            //    "aaSorting": [[0, 'asc']],
                                            //    "length": 50,
                                            //});
                                        });

                                        if (returnedData.htmlmsg != "") {
                                            $("#errhtml").html(returnedData.htmlmsg);
                                            $("#errhtml").show();
                                        } else {
                                            $("#errhtml").html = "";
                                            $("#errhtml").hide();
                                        }

                                        App.unblockUI(elemtid);

                                    }

                                    if (xhr.status != 200) {

                                        App.unblockUI(elemtid);
                                        if (returnedData.moderror == false) { window.location.href = returnedData.url; } else { location.reload(); }
                                    }

                                    if (form.dataset.ajaxUpdate) {
                                        var updateTarget = document.querySelector(form.dataset.ajaxUpdate);
                                        if (updateTarget) {
                                            updateTarget.innerHTML = xhr.responseText;
                                        }
                                    }
                                }
                            };
                            xhr.send(new FormData(form));
                        }
                    }
                }
            }, true);
        }
        catch (ex) {

        }
    }



    return {
        //main function to initiate the module

        Checklist: function (status, idname) {
            onChecklist(status, idname);
        },

        DownOrderRegisFile: function (parmenu, parcaption) {
            onDownOrderRegisFile(parmenu, parcaption);
        },

        UploadOrderRegisFileMasal: function () {
            $("#errhtml").hide();
            onUploadOrderRegisFileMasal();
        },

        regmasal: function (tpos) {

            $(".modal-backdrop").remove();
            $("#messageboxakta").modal("hide");

            var parmenu = $("input[name=menu]").val();
            var parcaption = $("input[name=caption]").val()

            $.ajax({
                url: "Contract/clncreateorderregis",
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { menu: parmenu, caption: parcaption, typos: tpos },
                beforeSend: function () {
                    App.blockUI({});
                },
                success: function (data) {
                    $("#pagecontent").html(data.view);
                    $("#totaldata").html("Pending: " + data.totaldata + " Data");
                    App.unblockUI();
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    var jsoncoll = JSON.stringify(x);
                    var jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                },
                complete: function () {
                    App.unblockUI();
                }
            });


        },

        sendconfirmviewgrid: function () {

            var form = $("#Order_form");
            var token = $('input[name="__RequestVerificationToken"]', form).val();
            var parmenu = $("input[name=menu]").val();
            var parcaption = $("input[name=caption]").val();
            var parcode = $("input[name=verifiedcode]").val();
            var parsended = $("input[name=sended]").val();


            $.ajax({
                url: "Contract/clnConfirmOrderView",
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                data: { __RequestVerificationToken: token, menu: parmenu, caption: parcaption, verifiedcode: parcode, sended: parsended, typeops: "abc" },
                beforeSend: function () {
                    App.blockUI({});
                },
                success: function (x) {
                    var totaldata = $("#table_List_orderRegis >tbody >tr").length;
                    if (totaldata <= 0) {
                        swal({
                            title: "Informasi",
                            text: "Silahkan Isikan pengajuan pendaftaran fidusia",
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        });
                        App.unblockUI();
                        return;
                    }

                    swal({
                        title: x.swltitle,
                        text: x.msg,
                        type: x.swltype,
                        closeOnConfirm: false,
                        showCancelButton: x.swlcanceled,
                        confirmButtonText: x.swltxtbtn,
                        inputPlaceholder: "Kode Verifikasi"
                    }, function (inputValue) {
                        if (inputValue === false) return false;
                        if (inputValue === "") {
                            swal.showInputError("Kode Verifikasi harus diisi!");
                            return false
                        } else {
                            if (x.result == "1") {
                                $("#totaldata").html("Pending: " + x.totaldata + " Data");
                                $("#totaldata1").html("Pending: " + x.totaldata + " Data");
                                $("#table_List_orderRegis").empty();
                                $("#idrowconfirm").hide();
                                $("#idrowinput").hide();
                                $("#capresult").html(x.capresulted);
                                $("#capresultdetail").html(x.capresultdetailed);
                                $("#idrowsendconfirm").show();
                            } else {

                                if (x.htmlvalid == "") {
                                    $("input[name=verifiedcode]").val(inputValue);
                                    $("input[name=sended]").val(x.cmdput);
                                    FormWizard.sendconfirmviewgrid();
                                }
                            }
                            swal.close();
                        }
                    });

                    App.unblockUI();
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    var jsoncoll = JSON.stringify(x);
                    var jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            });

        },

        confirmviewgrid: function () {

            var form = $("#Order_form");
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var parmenu = $("input[name=menu]").val();
            var parcaption = $("input[name=caption]").val();

            $.ajax({
                url: "Contract/clnConfirmOrderView",
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                data: { __RequestVerificationToken: token, menu: parmenu, caption: parcaption },
                beforeSend: function () {
                    App.blockUI({});
                },
                success: function (data) {
                    if (data.view != "") {
                        $("#idrowconfirm").show();
                        $("#confirmviewgrid").html(data.view);
                        $("#table_List_orderRegis").dataTable();

                        var totaldata = $("#table_List_orderRegis >tbody >tr").length;
                        $("#totaldata1").html("Pending: " + data.totaldata + " Data");
                    }
                    $("#idrowinput").hide();
                    $("#idrowsendconfirm").hide();

                    App.unblockUI();
                },
                error: function (x, y, z) {
                    App.unblockUI();
                    var jsoncoll = JSON.stringify(x);
                    var jsonreposn = JSON.parse(jsoncoll);
                    if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                }
            });
        },

        cancelconfirmviewgrid: function (idnama) {

            var form = $("#Order_form");
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var parmenu = $("input[name=menu]").val();
            var parcaption = $("input[name=caption]").val();

            var seldown = [];
            $("input:checkbox[name='" + idnama + "']:checked").each(function (i, o) {
                var strp = $(o).val();
                seldown.push(strp);
            });


            if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
                swal({
                    title: "Informasi",
                    text: 'Pilih Data Pengajuan Terlebih dahulu',
                    type: "info",
                    showConfirmButton: true,
                    confirmButtonText: "Tutup",
                });
                return false;
            }

            swal({
                title: "Konfirmasi Pembatalan Pengajuan",
                text: "Apakah anda yakin ingin membatalkan pengajuan fidusia yang anda pilih? ",
                type: "warning",
                closeOnConfirm: false,
                showCancelButton: true,
                confirmButtonText: "Proses"
            }, function (inputValue) {
                if (inputValue) {

                    $.ajax({
                        url: "Contract/clnCancelOrderView",
                        type: "POST",
                        //contentType: "application/json; charset=utf-8",
                        data: { __RequestVerificationToken: token, menu: parmenu, caption: parcaption, Selectdwn: seldown.toString() },
                        beforeSend: function () {
                            App.blockUI({});
                        },
                        success: function (data) {
                            if (data.view != "") {
                                $("#idrowconfirm").show();
                                $("#confirmviewgrid").html(data.view);
                                $("#table_List_orderRegis").dataTable();

                                $("#totaldata").html("Pending: " + data.totaldata + " Data");
                                $("#totaldata1").html("Pending: " + data.totaldata + " Data");

                                swal({
                                    title: "Informasi",
                                    text: data.msg,
                                    type: "info",
                                    showConfirmButton: true,
                                    confirmButtonText: "Tutup",
                                });
                            }
                            $("#idrowinput").hide();
                            $("#idrowsendconfirm").hide();
                            swal.close();
                            App.unblockUI();
                        },
                        error: function (x, y, z) {
                            swal.close();
                            App.unblockUI();
                            var jsoncoll = JSON.stringify(x);
                            var jsonreposn = JSON.parse(jsoncoll);
                            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
                        }
                    });

                }
            });
        },

        Filterdataview: function (idnama) {

            var form = $("#Order_form");
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var parmenu = $("input[name=menu]").val();
            var parcaption = $("input[name=caption]").val();

            if ($("input:checkbox[name='" + idnama + "']:checked").length == 0) {
                swal({
                    title: "Informasi",
                    text: 'Pilih Data Pengajuan Terlebih dahulu',
                    type: "info",
                    showConfirmButton: true,
                    confirmButtonText: "Tutup",
                });
                return false;
            }

            var seldown = "";
            $("input:checkbox[name='" + idnama + "']:checked").each(function (i, o) {
                seldown = $(o).val();
            });

            $.ajax({
                url: "Contract/clnOrderViewForEdit",
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { __RequestVerificationToken: token, menu: parmenu, caption: parcaption, Selectdwn: seldown.toString() },
                beforeSend: function () {
                    App.blockUI({ target: elemntupload });
                },
                success: function (data) {
                    var datajson = JSON.parse(data);
                    $.each(datajson, function (idx, obj) {
                        options.append('<option value=' + obj.Value + '>' + obj.Text + '</option>');
                    });
                    App.unblockUI(elemntupload);
                },
                error: function (x, y, z) {
                    App.unblockUI(elemntupload);
                    var jsoncoll = JSON.stringify(x);
                    var jsonreposn = JSON.parse(jsoncoll);
                    window.location.href = jsonreposn.responseJSON.url;
                }
            });
        },

        regisorderoneBegin: function () {
            $(".alert-danger").hide();
            App.blockUI();
        },

        regisorderoneSuccess: function (x) {
            if (x.msg != "") {
                swal({
                    title: "Informasi",
                    text: x.msg,
                    type: "info",
                    showConfirmButton: true,
                    confirmButtonText: "Tutup",
                });
            }
            if (x.result == "1") {
                $("#totaldata").html("Pending: " + x.totaldata + " Data");
                FormWizard.refreshform();
            }

            App.unblockUI();
        },

        regidoderoneFailure: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },

        refreshform: function () {
            $('input[type != "hidden"]').val("");
            $('.select2').val("");
            $('#Nilai_PokokHutang,#Nilai_Penjaminan,#Nilai_Objek_Penjaminan').val("0");
            $("#idrowconfirm").hide();
            $("#table_List_orderRegis").dataTable().remove();
            $("#idrowinput").show();
            $("#idrowconfirm").hide();
            $("#idrowsendconfirm").hide();
            $("#idrowinputmasal").hide();
            $("input[name=verifiedcode]").val("");
            $("input[name=sended]").val("");
            FormWizard.init();
            $('#Order_form').find('.button-previous').hide();
            $('#Order_form').find('.button-submit').hide();
            $('#Order_form').find('.button-next').show();
            $('#Order_form').bootstrapWizard('first');
            jQuery('li', $('#Order_form')).removeClass("done");
        },

        refreshformmasal: function () {

            $('input[type != "hidden"]').val("");
            $("#idrowconfirm").hide();
            $("#table_List_orderRegis").dataTable().remove();
            $("#idrowinput").show();
            $("#idrowconfirm").hide();
            $("#idrowsendconfirm").hide();
            $("#idrowinputmasal").hide();

            FormWizard.init();

        },

        init: function () {
            if (!jQuery().bootstrapWizard) {
                return;
            }

            function format(state) {
                if (!state.id) return state.text; // optgroup
                return "<img class='flag' src='../../assets/global/img/flags/" + state.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + state.text;
            }

            $('.date-picker').datepicker({
                rtl: App.isRTL(),
                orientation: "left",
                autoclose: true
            });

            $("#Jenis_Pelanggan,#Jenis_Pembiayaan,#Jenis_Penggunaan,#Jenis_Kelamin_Debitur,#Jenis_Identitas_Debitur," +
                "#Jenis_Kelamin_BPKB,#Jenis_Identitas_BPKB,#Jenis_Object,#Kondisi_Object").select2({
                    placeholder: "Pilih..",
                    allowClear: true,
                    formatResult: format,
                    width: '120px',
                    formatSelection: format,
                    escapeMarkup: function (m) {
                        return m;
                    }
                });


            $.validator.addMethod("numbernilai", function (value, element) {
                value = value.replace(/\,/g, "").replace(/\./g, "");
                return this.optional(element) || /^\d{7,18}$/.test(value);
            }, "Isikan dengan angka (minimal 7 digit)");


            $.validator.addMethod("numberposkode", function (value, element) {
                return this.optional(element) || /^\d{5}$/.test(value);
            }, "Isikan dengan angka (5 digit)");

            $.validator.addMethod("numberhp", function (value, element) {
                return this.optional(element) || /^\d{1,13}$/.test(value);
            }, "Isikan dengan angka (maksimal 13 digit)");

            $.validator.addMethod("numberrtrw", function (value, element) {
                return this.optional(element) || /^\d{1,2}$/.test(value);
            }, "Isikan dengan angka  (maksimal 2 digit)");

            $.validator.addMethod("numberkontrak", function (value, element) {
                return this.optional(element) || /^\d{10}$/.test(value);
            }, "Isikan dengan angka (10 digit)");


            $.validator.addMethod("numberroda", function (value, element) {
                return this.optional(element) || /^\d{1,2}$/.test(value);
            }, "Isikan jumlah roda (maksimal 2 digit)");

            $.validator.addMethod("numbertahun", function (value, element) {
                return this.optional(element) || /^\d{4}$/.test(value);
            }, "Isikan tahun pembuatan (maksimal 4 digit)");

            var form = $('#Order_form');
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {

                    "DetailOrderRegis.Jenis_Pelanggan": {
                        required: true
                    },
                    "DetailOrderRegis.Jenis_Pembiayaan": {
                        required: true
                    },
                    "DetailOrderRegis.Jenis_Penggunaan": {
                        required: true
                    },
                    "DetailOrderRegis.Tanggal_Perjanjian": {
                        required: true
                    },
                    "DetailOrderRegis.NoPerjanjian": {
                        required: true,
                        numberkontrak: true
                    },
                    "DetailOrderRegis.Tanggal_awal_angsuran": {
                        required: true
                    },
                    "DetailOrderRegis.Tanggal_akhir_angsuran": {
                        required: true
                    },
                    "DetailOrderRegis.Nilai_PokokHutang": {
                        required: true,
                        numbernilai: true
                    },
                    "DetailOrderRegis.Nilai_Penjaminan": {
                        required: true,
                        numbernilai: true
                    },
                    "DetailOrderRegis.Nilai_Objek_Penjaminan": {
                        required: true,
                        numbernilai: true
                    },


                    "DetailOrderRegis.Nama_Debitur": {
                        required: true,
                        maxlength: 50
                    },
                    "DetailOrderRegis.Jenis_Kelamin_Debitur": {
                        required: true
                    },
                    "DetailOrderRegis.Tempat_Lahir_Debitur": {
                        required: true,
                        maxlength: 30
                    },
                    "DetailOrderRegis.Tanggal_Lahir_Debitur": {
                        required: true
                    },
                    "DetailOrderRegis.Jenis_Identitas_Debitur": {
                        required: true
                    },
                    "DetailOrderRegis.No_KTP_NPWP_Debitur": {
                        required: true,
                        maxlength: 18
                    },
                    "DetailOrderRegis.No_Telp_HP_Debitur": {
                        required: true,
                        numberhp: true
                    },
                    "DetailOrderRegis.Alamat_Debitur": {
                        required: true,
                        maxlength: 100
                    },
                    "DetailOrderRegis.RT_Debitur": {
                        required: true,
                        numberrtrw: true
                    },
                    "DetailOrderRegis.RW_Debitur": {
                        required: true,
                        numberrtrw: true
                    },
                    "DetailOrderRegis.Kelurahan_Debitur": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.Kecamatan_Debitur": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.Kota_Debitur": {
                        required: true,
                        maxlength: 35
                    },

                    "DetailOrderRegis.KodePos_Debitur": {
                        required: true,
                        numberposkode: true
                    },

                    "DetailOrderRegis.Provinsi_Debitur": {
                        required: true,
                        maxlength: 35
                    },


                    "DetailOrderRegis.Nama_Pemilik_BPKB": {
                        required: true,
                        maxlength: 50
                    },
                    "DetailOrderRegis.Jenis_Kelamin_Pemilik_BPKB": {
                        required: true
                    },
                    "DetailOrderRegis.Tempat_Lahir_Pemilik_BPKB": {
                        required: true,
                        maxlength: 30
                    },
                    "DetailOrderRegis.Tanggal_Lahir_Pemilik_BPKB": {
                        required: true
                    },
                    "DetailOrderRegis.Jenis_Identitas_Pemilik_BPKB": {
                        required: true
                    },
                    "DetailOrderRegis.No_KTP_NPWP_Pemilik_BPKB": {
                        required: true,
                        maxlength: 18
                    },
                    "DetailOrderRegis.No_Telp_HP_Pemilik_BPKB": {
                        required: true,
                        numberhp: true
                    },
                    "DetailOrderRegis.Alamat_Pemilik_BPKB": {
                        required: true,
                        maxlength: 100
                    },

                    "DetailOrderRegis.RT_Pemilik_BPKB": {
                        required: true,
                        numberrtrw: true
                    },
                    "DetailOrderRegis.RW_Pemilik_BPKB": {
                        required: true,
                        numberrtrw: true
                    },
                    "DetailOrderRegis.Kelurahan_Pemilik_BPKB": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.Kecamatan_Pemilik_BPKB": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.Kota_Pemilik_BPKB": {
                        required: true,
                        maxlength: 35
                    },

                    "DetailOrderRegis.KodePos_Pemilik_BPKB": {
                        required: true,
                        numberposkode: true
                    },

                    "DetailOrderRegis.Provinsi_Pemilik_BPKB": {
                        required: true,
                        maxlength: 35
                    },


                    "DetailOrderRegis.Jenis_Object": {
                        required: true,
                    },
                    "DetailOrderRegis.Kondisi_Object": {
                        required: true,
                    },
                    "DetailOrderRegis.Merk": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.Tipe_Kendaraan": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.Warna": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.NoRangka": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.NoMesin": {
                        required: true,
                        maxlength: 35
                    },
                    "DetailOrderRegis.TahunPembuatan": {
                        required: true,
                        numbertahun: true
                    },
                    "DetailOrderRegis.Jumlah_Roda": {
                        required: true,
                        numberroda: true
                    },
                    "DetailOrderRegis.No_BPKB_Object_Bekas": {
                        required: true,
                        maxlength: 35
                    }

                },


                messages: {

                    "DetailOrderRegis.Jenis_Pelanggan": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Jenis_Pembiayaan": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Jenis_Penggunaan": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Tanggal_Perjanjian": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.NoPerjanjian": {
                        required: "Field wajib diisi"

                    },
                    "DetailOrderRegis.Tanggal_awal_angsuran": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Tanggal_akhir_angsuran": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Nilai_PokokHutang": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Nilai_Penjaminan": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Nilai_Objek_Penjaminan": {
                        required: "Field wajib diisi"
                    },


                    "DetailOrderRegis.Nama_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Jenis_Kelamin_Debitur": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Tempat_Lahir_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Tanggal_Lahir_Debitur": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Jenis_Identitas_Debitur": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.No_KTP_NPWP_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.No_Telp_HP_Debitur": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Alamat_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.RT_Debitur": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.RW_Debitur": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Kelurahan_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Kecamatan_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Kota_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },

                    "DetailOrderRegis.KodePos_Debitur": {
                        required: "Field wajib diisi",
                    },

                    "DetailOrderRegis.Provinsi_Debitur": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },


                    "DetailOrderRegis.Nama_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Jenis_Kelamin_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Tempat_Lahir_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Tanggal_Lahir_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Jenis_Identitas_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.No_KTP_NPWP_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.No_Telp_HP_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Alamat_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.RT_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.RW_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Kelurahan_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Kecamatan_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Kota_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },

                    "DetailOrderRegis.KodePos_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                    },

                    "DetailOrderRegis.Provinsi_Pemilik_BPKB": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },


                    "DetailOrderRegis.Jenis_Object": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Kondisi_Object": {
                        required: "Field wajib diisi",
                    },
                    "DetailOrderRegis.Merk": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"

                    },
                    "DetailOrderRegis.Tipe_Kendaraan": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },
                    "DetailOrderRegis.Warna": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },

                    "DetailOrderRegis.NoMesin": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },

                    "DetailOrderRegis.NoRangka": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    },

                    "DetailOrderRegis.TahunPembuatan": {
                        required: "Field wajib diisi"
                    },
                    "DetailOrderRegis.Jumlah_Roda": {
                        required: "Field wajib diisi"
                    },

                    "DetailOrderRegis.No_BPKB_Object_Bekas": {
                        required: "Field wajib diisi",
                        maxlength: "Isikan (maksimal {0} karakkter)"
                    }

                },

                errorPlacement: function (error, element) { // render error placement for each input type
                    if (element.attr("name") == "gender") { // for uniform radio buttons, insert the after the given container
                        error.insertAfter("#form_gender_error");
                    } else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
                        error.insertAfter("#form_payment_error");
                    } else {
                        error.insertAfter(element); // for other inputs, just perform default behavior
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    success.hide();
                    error.show();
                    App.scrollTo(error, -200);
                },

                highlight: function (element) { // hightlight error inputs

                    $(element)
                        .closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    if (label.attr("for") == "gender" || label.attr("for") == "payment[]") { // for checkboxes and radio buttons, no need to show OK icon
                        label
                            .closest('.form-group').removeClass('has-error').addClass('has-success');
                        label.remove(); // remove error label here
                    } else { // display success icon for other inputs
                        label
                            .addClass('valid') // mark the current input as valid and display OK icon
                            .closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                    }
                }

            });

            var displayConfirm = function () {
                $('#tab4 .form-control-static', form).each(function () {
                    var input = $('[name="' + $(this).attr("data-display") + '"]', form);
                    if (input.is(":radio")) {
                        input = $('[name="' + $(this).attr("data-display") + '"]:checked', form);
                    }
                    if (input.is(":text") || input.is("textarea")) {
                        $(this).html(input.val());
                    } else if (input.is("select")) {
                        $(this).html(input.find('option:selected').text());
                    } else if (input.is(":radio") && input.is(":checked")) {
                        $(this).html(input.attr("data-title"));
                    } else if ($(this).attr("data-display") == 'payment[]') {
                        var payment = [];
                        $('[name="payment[]"]:checked', form).each(function () {
                            payment.push($(this).attr('data-title'));
                        });
                        $(this).html(payment.join("<br>"));
                    }
                });
            }

            var handleTitle = function (tab, navigation, index) {
                var total = navigation.find('li').length;
                var current = index + 1;
                // set wizard title
                $('.step-title', $('#Order_form')).text('Step ' + (index + 1) + ' of ' + total);
                // set done steps
                jQuery('li', $('#Order_form')).removeClass("done");
                var li_list = navigation.find('li');
                for (var i = 0; i < index; i++) {
                    jQuery(li_list[i]).addClass("done");
                }

                if (current == 1) {
                    $('#Order_form').find('.button-previous').hide();
                } else {
                    $('#Order_form').find('.button-previous').show();
                }

                if (current >= total) {
                    $('#Order_form').find('.button-next').hide();
                    $('#Order_form').find('.button-submit').show();
                    displayConfirm();
                } else {
                    $('#Order_form').find('.button-next').show();
                    $('#Order_form').find('.button-submit').hide();
                }
                App.scrollTo($('.page-title'));
            }

            // default form wizard
            $('#Order_form').bootstrapWizard({
                'nextSelector': '.button-next',
                'previousSelector': '.button-previous',
                onTabClick: function (tab, navigation, index, clickedIndex) {
                    return false;

                    success.hide();
                    error.hide();
                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, clickedIndex);
                },
                onNext: function (tab, navigation, index) {
                    success.hide();
                    error.hide();


                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, index);
                },
                onPrevious: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    handleTitle(tab, navigation, index);
                },
                onTabShow: function (tab, navigation, index) {
                    var total = navigation.find('li').length;
                    var current = index + 1;
                    var $percent = (current / total) * 100;
                    $('#Order_form').find('.progress-bar').css({
                        width: $percent + '%'
                    });
                }
            });

            $('#Order_form').find('.button-previous').hide();
            $('#Order_form').find('.button-submit').hide();
            //$('#Order_form .button-submit').click(function () { $(form).submit(); alert('ss'); });

            //apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
            $("#Jenis_Pelanggan,#Jenis_Pembiayaan,#Jenis_Penggunaan,#Jenis_Kelamin_Debitur, " +
                "#Jenis_Identitas_Debitur,#Tanggal_Perjanjian,#Tanggal_awal_angsuran,#Tanggal_akhir_angsuran,#Tanggal_Lahir_Debitur,#Tanggal_Lahir_Pemilik_BPKB," +
                "#Jenis_Kelamin_BPKB,#Jenis_Identitas_BPKB,#RT_Debitur,#RW_Debitur,#Jenis_Object,#Kondisi_Object", form).change(function () {
                    form.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
                });



        }

    };

}();



jQuery(document).ready(function () {
    FormWizard.init();
    $('.form-control').keyup(function () {
        this.value = this.value.toUpperCase();
    });
    $('#Nilai_PokokHutang,#Nilai_Penjaminan,#Nilai_Objek_Penjaminan').on('keyup', function () {
        $(this).val(function (index, value) {
            var count = (value.match(/\./g) || []).length;

            if (count <= 1) {
                var valueorg = value.replace(/\,/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                return valueorg;
            } else { return ""; }
        });
    });
});