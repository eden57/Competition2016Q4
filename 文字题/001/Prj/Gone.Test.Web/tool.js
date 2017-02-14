
function AjaxPost(_url, _data, _callback, _async) {
    $.ajax({
        type: "Post",
        async: _async,
        contentType: "application/json",
        url: _url.indexOf("?") > 0 ? (_url + "&s=" + Math.random()) : _url + "?s=" + Math.random(),
        data: _data,
        dataType: 'json',
        success: function (rr) {
            _callback(rr);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
}