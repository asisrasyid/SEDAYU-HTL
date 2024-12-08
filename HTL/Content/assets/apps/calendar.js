
var AppCalendar = function () {

    return {
        getdatarecord: function (modul) {
            App.blockUI();
            $.post("Home/clnHomeTodo", {
                menu: modul,
                caption: modul}, function () {
            }).done(function (response) {
                if (response.moderror == false) {
                    var data = response.view;
                    $("#pagecontent").html(data);
                    App.unblockUI();

                } else {
                    window.location.href = response.url;
                }

            }).fail(function (x) {
                App.unblockUI(elemntupload);
                App.unblockUI();
                var jsoncoll = JSON.stringify(x);
                var jsonreposn = JSON.parse(jsoncoll);
                if (jsonreposn.responseJSON.moderror == false) { window.location.href = jsonreposn.responseJSON.url; } else { location.reload(); }
            });
        }

    };

}();

$(document).ready(function () {
    //AppCalendar.getdatarecord("WFTODONEWREG");
});

