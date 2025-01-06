var TableDatatablesEditable1 = function () {
    var initPickers = function () {
        //init date pickers
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            autoclose: true
        });
    }

    var handleTablePage = function (tabled, currentP, totalR, totalP, nextP, prevP, parurl, divgrid, ismodal, istotal = 'n') {
        var elemntupload = "";
        if (ismodal == 1) {
            elemntupload = ".modal-body";
        }

        if ((totalP == 0) && (totalR > 0)) {
            totalP = 1;
        }
        var table = $("#" + tabled);

        //if ($.fn.dataTable.isDataTable(tabled)) {
        //    table.dataTable().fnDestroy();
        //}

        if (!$.fn.dataTable.isDataTable(tabled)) {
            table.dataTable({
                "bInfo": true,
                "bLengthChange": false,
                "pageLength": 5,
                //"autoWidth": true,

                //altEditor: true,     // Enable altEditor
                language: {
                    "sSearch": "",  //Cari Data
                    searchPlaceholder: "Cari Data",  //Ketikan Kata Kunci"
                   "sEmptyTable": "Data Not Found"
                },
                //responsive: {
                //    breakpoints: [
                //        { name: 'desktop', width: 400 },
                //        //{ name: 'tablet', width: 1024 },
                //        //{ name: 'fablet', width: 768 },
                //        //{ name: 'phone', width: 480 }
                //    ]
                //}
                //,

                buttons: [
                    //{ extend: 'print', className: 'btn dark btn-outline'  },
                    //{ extend: 'pdf', className: 'btn green btn-outline' },
                    //{ extend: 'csv', className: 'btn purple btn-outline ' }
                ],

                "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable

                // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
                // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js).
                // So when dropdowns used the scrollable div should be removed.
                "dom": "<'row' <'col-md-12'T>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

                drawCallback: function () {
                    //var currentP =@Model.DetailFilter.PageNumber.ToString();
                    //var totalP =@Model.DetailFilter.TotalPage.ToString();
                    //var nextP =@Model.DetailFilter.PageNumber+1;
                    //var prevP =@Model.DetailFilter.PageNumber-1;

                    if (parseFloat(totalP) > 1) {
                        $("#" + tabled + "_paginate .pagination > li.active > a").text('Page ' + currentP + ' of ' + totalP + ' Pages');
                        $("#" + tabled + "_paginate .pagination > li.active > a").on('click', function (e) {
                            e.preventDefault();
                            return false;
                        });
                        $("#" + tabled + "_paginate .pagination > li.active > a").attr("href", "javascript:;");
                        $("#" + tabled + "_paginate .pagination > li.active").removeClass("active");
                        $("#" + tabled + "_info").html("");

                        if (totalR > 0) {
                            $("#" + tabled + "_info").append('<div class="">');
                            $("#" + tabled + "_info").append('<div class="form-group" style="margin-bottom: 0px">');
                            $("#" + tabled + "_info").append('<div class="col-md-4">');

                            $("#" + tabled + "_info").append("<select class='form-control select2' id='selectpage_" + tabled + "'></select>")
                            $("#" + tabled + "_info").append('</div >');
                            $("#" + tabled + "_info").append('</div >');
                            $("#" + tabled + "_info").append('</div >');

                            var options = $("#selectpage_" + tabled);
                            for (i = 1; i <= totalP; i++) {
                                var stroption = "<option />";
                                if (i == currentP) {
                                    stroption = "<option selected /> ";
                                }
                                options.append($(stroption).val(i).text('Page ' + i));
                            }

                            options.select2();

                            $("#selectpage_" + tabled).change(function () {
                                var id = $(this).val();
                                $.ajax({
                                    url: parurl,
                                    type: "POST",
                                    //contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: { paged: id },
                                    beforeSend: function () {
                                        App.blockUI({ target: elemntupload });
                                    },
                                    success: function (data) {
                                        if (data.moderror == false) {
                                            $("#" + divgrid).html(data.view);
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
                            });
                        }

                        if (parseFloat(currentP) > 1) {
                            $("#" + tabled + "_paginate > .pagination > li.prev.disabled").attr("href", "javascript:;");
                            $("#" + tabled + "_paginate > .pagination > li.prev.disabled").removeClass("disabled");
                            $("#" + tabled + "_paginate > .pagination > li.prev", this.api().table().container())
                                .on('click', function () {
                                    $.ajax({
                                        url: parurl,
                                        type: "POST",
                                        //contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        data: { paged: prevP },
                                        beforeSend: function () {
                                            App.blockUI({ target: elemntupload });
                                        },
                                        success: function (data) {
                                            if (data.moderror == false) {
                                                $("#" + divgrid).html(data.view);
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
                                });
                        }

                        if (parseFloat(currentP) < parseFloat(totalP)) {
                            $("#" + tabled + "_paginate .pagination > li.next.disabled").attr("href", "javascript:;");
                            $("#" + tabled + "_paginate .pagination > li.next.disabled").removeClass("disabled");
                            $("#" + tabled + "_paginate .pagination > li.next", this.api().table().container())
                                .on('click', function () {
                                    $.ajax({
                                        url: parurl,
                                        type: "POST",
                                        //contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        data: { paged: nextP },
                                        beforeSend: function () {
                                            App.blockUI({ target: elemntupload });
                                        },
                                        success: function (data) {
                                            if (data.moderror == false) {
                                                $("#" + divgrid).html(data.view);
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
                                });
                        }

                        if (parseFloat(currentP) >= parseFloat(totalP)) {
                            $("#" + tabled + "_paginate .pagination > li.next").unbind("click");
                            $("#" + tabled + "_paginate .pagination > li.next.disabled").addClass("disabled");
                        }

                        if (parseFloat(currentP) == 1) {
                            $("#" + tabled + "_paginate .pagination > li.prev").unbind("click");
                            $("#" + tabled + "_paginate .pagination > li.prev.disabled").addClass("disabled");
                        }
                    } else {
                        $("#" + tabled + "_info").remove();
                        $("#" + tabled + "_paginate").remove();
                    }
                }
                ,
                footerCallback: function (row, data, start, end, display) {
                    if (istotal == 'y') {
                        var api = this.api(), data;

                        // converting to interger to find total
                        var intVal = function (i) {
                            return typeof i === 'string' ?
                                i.replace(/[\$,]/g, '') * 1 :
                                typeof i === 'number' ?
                                    i : 0;
                        };

                        var monTotal = api
                            .column(5)
                            .data()
                            .reduce(function (a, b) {
                                b = b.replace(/\./g, '');
                                return intVal(a) + intVal(b);
                            }, 0);

                        monTotal = monTotal.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')

                        $(api.column(5).footer()).html(monTotal);
                    }
                }
            });
        }

        $("#" + tabled).on('search.dt', function () {
            var valued = $('.dataTables_filter input').val();
            //  if (valued.length);

            //var id = $(this).val();
            //$.ajax({
            //    url: parurl,
            //    type: "POST",
            //    //contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    data: { paged: id },
            //    beforeSend: function () {
            //        App.blockUI({ target: elemntupload });
            //    },
            //    success: function (data) {
            //        $("#" + divgrid).html(data.view);
            //        App.unblockUI(elemntupload);
            //    },
            //    error: function (x, y, z) {
            //        App.unblockUI();
            //        var jsoncoll = JSON.stringify(x);
            //        var jsonreposn = JSON.parse(jsoncoll);
            //        window.location.href = jsonreposn.responseJSON.url;
            //    }
            //});
        });
    }

    var handleTable = function (tabled) {
        var table = $(tabled);

        var oTable = table.dataTable({
            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js).
            // So when dropdowns used the scrollable div should be removed.
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "lengthMenu": [
                [15, 100, 150, -1],
                [15, 100, 150, "All"] // change per page values here
            ],

            // Or you can use remote translation file
            //"language": {
            //   url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
            //},

            // set the initial value
            "pageLength": 100,

            "language": {
                "lengthMenu": "",// _MENU_ Data",
                "sSearch": "",
                "searchPlaceholder": "Cari Data",  //Ketikan Kata Kunci"
                "emptyTable": "Tidak ada data",
                "info":"", //"Hal _START_ s/d _END_ ", //, Total data _TOTAL_ Data
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            }],
            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });
    }

    return {
        //main function to initiate the module
        init: function (partabled) {
            handleTable(partabled);
        },
        initPaging: function (partabled, currentP, totalR, totalP, nextP, prevP, parurl, divgrid, ismodal, istotal) {
            handleTablePage(partabled, currentP, totalR, totalP, nextP, prevP, parurl, divgrid, ismodal, istotal);
        }
    };
}();

jQuery(document).ready(function () {
    //TableDatatablesEditable1.init("#sample_editable_acctount");
});