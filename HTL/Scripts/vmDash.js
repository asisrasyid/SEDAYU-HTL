var elemtid = "body";
var elemntupload = ".modal-content";

var vmDash = function () {
    var onFilterBranchByClient = function () {
        var Parclientid = $("#SelectClient").val();
        var jsoncoll = "";
        var jsonreposn = "[]";
        var options = "undefined";
        var datajson = "[]";

        if ($('#SelectBranch').length > 0) {
            $.ajax({
                url: "DashBoard/clnGetBranch",
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { clientid: Parclientid },
                beforeSend: function () {
                    App.blockUI({ target: elemntupload });
                },
                success: function (data) {
                    if (data.moderror == false) {
                        options = $('#filterdatadialog #SelectBranch');
                        options.empty();
                        datajson = JSON.parse(data.branchjson);
                        options.append('<option value="">Pilih..</option>');
                        $.each(datajson, function (idx, obj) {
                            options.append('<option value=' + obj.Value + '>' + obj.Text + '</option>');
                        });
                        $("#SelectBranch").val(data.brachselect).trigger("change");
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

    var onOpenFilter = function () {
        var jsoncoll = "";
        var jsonreposn = "";

        $.ajax({
            url: "DashBoard/clnOpenFilterpop",
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
                    $("#SelectClient").val(data.opsi1).trigger("change");
                    $("#SelectBranch").val(data.opsi2).trigger("change");
                    $("#SelectNotaris").val(data.opsi3).trigger("change");
                    $("#NoAkta").val(data.opsi4);
                    $("#NoPerjanjian").val(data.opsi5);
                    $('#fromdate').datepicker().datepicker('setDate', data.opsi6);
                    $('#todate').datepicker().datepicker('setDate', data.opsi7);

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
                        $("#loppid").html(data.view);
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

    return {
        OpenFilter: function () {
            onOpenFilter();
        },
        ApplyFilter: function (parForm, pardownloadexcel) {
            onApplyFilter(parForm, pardownloadexcel);
        },
        ResetFilter: function (parForm) {
            onResetFilter(parForm);
        },
        FilterBranchByClient: function () {
            onFilterBranchByClient();
        },
    };
}();