var elemntupload = ".modal-body";

var vmReportData = function () {
    var onInit = function () {
        if ($("#todate").length) {
            $('.date-picker').datepicker({
                rtl: App.isRTL(),
                orientation: "right bottom",
                autoclose: true
            });
        } else
            if ($("#fromdatebln").length) {
                $('.date-picker').datepicker({
                    rtl: App.isRTL(),
                    format: "MM-yyyy",
                    orientation: "right bottom",
                    autoclose: true
                });
            }
            else {
                $('.date-picker').datepicker({
                    rtl: App.isRTL(),
                    format: "yyyy",
                    viewMode: "years",
                    minViewMode: "years",
                    orientation: "right bottom",
                    autoclose: true
                });
            }

        ComponentsBootstrapSelect.init();

        var lengthbrn = ($('#SelectArea > option').length) - 1;
        if (lengthbrn == 1) {
            $('#SelectArea').val($('#SelectArea option:eq(1)').val()).change();
        }

        lengthbrn = ($('#SelectBranch > option').length) - 1;
        if (lengthbrn == 1) {
            $('#SelectBranch').select2("val", $('#SelectBranch option:eq(1)').val());
        }

        $('.select2').select2();

        $(".dreport").unbind("click");
        $(".dreport").bind("click", function () {
            vmReportData.ApplyFilter('#ReportFilter_form', '2');
        });

        $(".dreportref").unbind("click");
        $(".dreportref").bind("click", function () {
            vmReportData.ResetFilter('#ReportFilter_form');
        })
    };

    var onResetFilter = function (parForm) {
        var frm = $(parForm);
        $(frm)[0].reset();
        $('.select2').val('').trigger("change");
        var lengthbrn = ($('#SelectBranch > option').length) - 1;
        if (lengthbrn == 1) {
            $('#SelectBranch').select2("val", $('#SelectBranch option:eq(1)').val());
        }
    };

    var onOpenFilter = function () {
        $("#filterdatadialog").modal("show");
    };

    var onFilterBranchByClient = function (parm) {
        var Parclientid = $("#SelectArea").val();
        var jsoncoll = "";
        var jsonreposn = "[]";
        var datajson = "[]";
        var options = "undefined";

        if ($('#SelectBranch').length > 0) {
            $.ajax({
                url: "Report/clnGetBranch",
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { clientid: Parclientid },
                beforeSend: function () {
                    App.blockUI({ target: elemntupload });
                },
                success: function (data) {
                    if (data.moderror == false) {
                        datajson = JSON.parse(data.branchjson);
                        options = $("#SelectBranch");
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
                    }
                    else {
                        window.location.href = data.url;
                    }
                },
                error: function (x, y, z) {
                    App.unblockUI(elemntupload);
                    jsoncoll = JSON.stringify(x);
                    jsonreposn = JSON.parse(jsoncoll);
                    window.location.href = jsonreposn.responseJSON.url;
                }
            });
        }
    };
    var onApplyFilter = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var byteArray = null;
        var blob = null;
        var link = null;
        var fileName = null;
        var t = null;
        var stringpo = null;
        var viewerUrl = null;
        var downloadmode = pardownloadexcel;

        $("#CreateInvoice").val("");

        var frm = $(parForm);
        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize() + "&download=" + pardownloadexcel,
            beforeSend: function () {
                App.blockUI();
            },
            success: function (x) {
                if (x.moderror == false) {
                    App.unblockUI();
                    if (x.msg != "") {
                        swal({
                            title: "Informasi",
                            text: x.msg,
                            type: "info",
                            showConfirmButton: true,
                            confirmButtonText: "Tutup",
                        });
                    } else {
                        if (downloadmode < 2) {
                            byteArray = new Uint8Array(x.bytetyipe);
                            link = window.URL.createObjectURL(new Blob([byteArray], { type: x.contenttype }));
                            viewerUrl = x.viewpath + encodeURIComponent(link + "#" + x.filename);
                            window.open(viewerUrl, '', 'height=650,width=840');
                        } else {
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
                        }
                    }
                }
                else {
                    window.location.href = x.url;
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

    var onCretBILL = function (parForm, pardownloadexcel) {
        var jsoncoll = "";
        var jsonreposn = "[]";
        var byteArray = null;
        var blob = null;
        var link = null;
        var fileName = null;
        var t = null;
        var stringpo = null;
        var viewerUrl = null;
        var downloadmode = pardownloadexcel;

        $("#CreateInvoice").val("1");

        var frm = $(parForm);
        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize() + "&download=" + pardownloadexcel,
            beforeSend: function () {
                App.blockUI({ target: elemntupload });
            },
            success: function (x) {
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
                        if (downloadmode < 2) {
                            byteArray = new Uint8Array(x.bytetyipe);
                            link = window.URL.createObjectURL(new Blob([byteArray], { type: x.contenttype }));
                            viewerUrl = x.viewpath + encodeURIComponent(link + "#" + x.filename);
                            window.open(viewerUrl, '', 'height=650,width=840');
                        } else {
                            byteArray = new Uint8Array(x.bytetyipe);
                            blob = new Blob([byteArray], { type: x.contenttype });
                            link = document.createElement('a');
                            link.href = window.URL.createObjectURL(blob);
                            fileName = x.filename;
                            link.download = fileName;
                            link.click();
                        }
                    }
                    App.unblockUI(elemntupload);
                }
                else {
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

    return {
        init: function () {
            onInit();
        },
        OpenFilter: function () {
            onOpenFilter();
        },
        ApplyFilter: function (parForm, pardownloadexcel) {
            onApplyFilter(parForm, pardownloadexcel);
        },

        CretBILL: function (parForm, pardownloadexcel) {
            onCretBILL(parForm, pardownloadexcel);
        },

        ResetFilter: function (parForm) {
            onResetFilter(parForm);
        },
        DownloadBegin: function () {
            App.blockUI();
        },
        FilterBranchByClient: function (parm) {
            onFilterBranchByClient(parm);
        },
        UploadConvertFile: function () {
            $("#errhtml").hide();
            onUploadConvertFile();
        }
    };
}();

$(document).ready(function () {
    vmReportData.init();
});