var elemtid = "";

var vmAccountReg = function () {

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
	
    var onSearchCabangAjax = function (selectcab) {

        var Parclientid = $("#ClientId").val();
        var datajson = "[]";
        var options = "undefined";
        var jsoncoll = "";
        var jsonreposn = "[]";
        var Parreg = $("#RegionID").val();
        
        $.ajax({
            url: "Pemberkasan/clnGetBranch",
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { clientid: Parclientid, regionid: Parreg },
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
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

                    $("#Cabang").val(data.brachselect).trigger("change");

                    options.select2();
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
    };

    return {

        SearchCabangAjax: function () {
            onSearchCabangAjax();
        },

        ChacctBegin: function () {
            App.blockUI();
        },
        ChacctSuccess: function (x) {
            swal({
                title: x.swltitle,
                text: x.msg,
                type: x.swltype,
                closeOnConfirm: false,
                confirmButtonText: x.swltxtbtn,
                inputPlaceholder: "Kode Verifikasi"
            }, function (inputValue) {
                if (inputValue === false) return false;
                if (inputValue === "") {
                    swal.showInputError("Kode Verifikasi harus diisi!");
                    return false
                } else {
                    if (x.url != "") {
                        window.location.href = x.url;
                    } else {
                        if (x.htmlvalid == "") {
                            $("#verifiedcode").val(inputValue);
                            $("#sended").val(x.cmdput);
                            $("#frmaccountchange").submit();
                        }
                        swal.close();
                    }
                }
            });
            App.unblockUI();
        },
        ChacctFailure: function (x) {
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

        BeginSubmit: function () {
            App.blockUI();
        },
        SuccessSubmit: function (data) {
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

        ChacctBegin: function () {
            App.blockUI();
        },
        ChacctSuccess: function (x) {

            swal({
                title: x.swltitle,
                text: x.msg,
                type: x.swltype,
                closeOnConfirm: false,
                confirmButtonText: x.swltxtbtn,
                showCancelButton: x.swltxtcnl,
                cancelButtonText: "Tutup",
                inputPlaceholder: "Kode Verifikasi"
            }, function (inputValue) {
                var poki = inputValue;
                if (poki === false) { $("#majutakgentar").val(""); return false };
                if (poki === "") {
                    swal.showInputError("Kode Verifikasi harus diisi!");
                    return false
                } else {
                    if (x.url != "" && (typeof x.url !== "undefined")) {
                        window.location.href = x.url;
                    } else {
                        if ((x.htmlpool == "") || (typeof x.htmlpool == "undefined")) {

                            $("#majutakgentar").val(poki);
                            $("#kursakukenji").val(x.cmdput);
                            $("#frmmanabisa").submit();
                        }

                        swal.close();
                    }
                }
            });

            App.unblockUI();

        },
        ChacctFailure: function (x) {
            App.unblockUI();
            var jsoncoll = JSON.stringify(x);
            var jsonreposn = JSON.parse(jsoncoll);
            if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
        },

        initnumber: function () {
            $('input.Numberonly').on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');
            });
        },

    };

}();

