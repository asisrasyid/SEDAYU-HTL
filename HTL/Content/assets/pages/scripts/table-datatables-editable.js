var TableDatatablesEditable = function () {
    var initPickers = function () {
        //init date pickers
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            autoclose: true
        });
    }

    var handleTable = function () {
        function restoreRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);

            for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                oTable.fnUpdate(aData[i], nRow, i, false);
            }

            oTable.fnDraw();
        }

        function editRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            jqTds[2].innerHTML = '<input type="text" class="form-control input-small" maxlength="50" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<div class="col-md-2">' +
                                '<div class="input-group input-small date date-picker" data-date-format="yyyy-mm-dd" data-date-start-date="+0d">' +
            '<input class="form-control" readonly="" type="text"  value="' + aData[3] + '">' +
            '<span class="input-group-btn">' +
            '<button class="btn default" type="button">' +
                                '<i class="fa fa-calendar"></i>' +
                                '</button></span></div> </div>'
            //jqTds[3].innerHTML = '<input type="text" class="form-control form-control-inline input-medium date-picker" value="' + aData[3] + '">';
            jqTds[4].innerHTML = '<input type="text" class="form-control input-small" maxlength="30" value="' + aData[4] + '">';
            jqTds[5].innerHTML = '<a class="edit" href="">Save</a> | <a class="cancel" href="">Cancel</a>';

            initPickers();
        }

        function saveRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 4, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a><span> | </span>' +
                    '<a href="javascript:;" onclick="vmContracts.OpenInfoGrd("' + aData[0] + '")">View</a>', nRow, 5, false);
            oTable.fnDraw();
            var aData = oTable.fnGetData(nRow);
            vmContracts.SaveGrd(aData[0], jqInputs[0].value, jqInputs[1].value, jqInputs[2].value)
        }

        function cancelEditRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 4, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 5, false);
            oTable.fnDraw();
        }

        var table = $('#sample_editable_1');

        var oTable = table.dataTable({
            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js).
            // So when dropdowns used the scrollable div should be removed.
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "lengthMenu": [
                [50, 100, 150, -1],
                [50, 100, 150, "All"] // change per page values here
            ],

            // Or you can use remote translation file
            //"language": {
            //   url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
            //},

            // set the initial value
            "pageLength": 50,

            "language": {
                "lengthMenu": " _MENU_ records"
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

        var tableWrapper = $("#sample_editable_1_wrapper");

        var nEditing = null;
        var nNew = false;

        $('#sample_editable_1_new').click(function (e) {
            e.preventDefault();

            if (nNew && nEditing) {
                if (confirm("Previose row not saved. Do you want to save it ?")) {
                    saveRow(oTable, nEditing); // save
                    $(nEditing).find("td:first").html("Untitled");
                    nEditing = null;
                    nNew = false;
                } else {
                    oTable.fnDeleteRow(nEditing); // cancel
                    nEditing = null;
                    nNew = false;

                    return;
                }
            }

            var aiNew = oTable.fnAddData(['', '', '', '', '', '']);
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            nEditing = nRow;
            nNew = true;
        });

        //table.on('click', '.delete', function (e) {
        //    e.preventDefault();

        //    if (confirm("Are you sure to delete this row ?") == false) {
        //        return;
        //    }

        //    var nRow = $(this).parents('tr')[0];
        //    oTable.fnDeleteRow(nRow);
        //    alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        //});

        table.on('click', '.cancel', function (e) {
            e.preventDefault();
            if (nNew) {
                oTable.fnDeleteRow(nEditing);
                nEditing = null;
                nNew = false;
            } else {
                restoreRow(oTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.edit', function (e) {
            e.preventDefault();
            nNew = false;
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTable, nEditing);
                editRow(oTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveRow(oTable, nEditing);
                nEditing = null;
            } else {
                /* No edit in progress - let's start one */
                editRow(oTable, nRow);
                nEditing = nRow;
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleTable();
        }
    };
}();

jQuery(document).ready(function () {
    TableDatatablesEditable.init();
});