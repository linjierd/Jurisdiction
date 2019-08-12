
/*Null取值*/
var isNull = function (v) { return !(typeof v !== "undefined" && v != null); };
var ifNull = function (val, replace) {
    if (isNull(val))
        return replace;
    return val;
};
var getDict = function (dict, key) {
    if (dict[key]) { return dict[key]; }
};

/*查询条件序列化*/
$.filterSerialize = function(filterContainer) {
    return $(filterContainer).find("input,select").filter(function() {
        return ($(this).val() && $(this).val().length > 0 && $(this).val() != "支持用*模糊查询");
    }).serialize();
};


$.filterSerializeArray = function (filterContainer) {
    return $(filterContainer).find("input,select").filter(function () {
        return ($(this).val() && $(this).val().length > 0 && $(this).val() != "支持用*模糊查询");
    }).serializeArray();
};
var LzhAjax = {
    ajaxRequest: function (controller, action, params, fnOnAjaxEnd) { _privateAjaxRequest(controller, action, params, null, null, null, fnOnAjaxEnd); },
    ajaxRequest_Simple: function (controller, action, params, sender, fnOnAjaxEnd) { _privateAjaxRequest(controller, action, params, sender, null, null, fnOnAjaxEnd); },
    ajaxRequest_NonReturn: function (controller, action, params, sender, fnOnAjaxEnd) { _privateAjaxRequest(controller, action, params, sender, "NonReturn", null, fnOnAjaxEnd); },
    ajaxRequest_GetHtml: function (controller, action, params, sender, holder, fnOnAjaxEnd, type) { _privateAjaxRequest(controller, action, params, sender, "GetHtml", holder, fnOnAjaxEnd, type); },
    closeWindowEx: function () { if ($('#windowExDiv').length > 0) { $('#windowExDiv').overlay().close(); } },
    SyncRqeuest: function (controller, action, params, fnOnAjaxEnd, type) { _privateAjaxRequest(controller, action, params, null, null, null, fnOnAjaxEnd, type, false); }
};
function _privateAjaxRequest(controller, action, params, sender, agrs, holder, fnOnAjaxEnd, type, async) {
    var url = "/" + controller + "/" + action + "";
    var ret;
    ajax_onStart(sender, agrs, holder);

    if (typeof type === "undefined") {
        type = "POST";
    }
    if (typeof async === "undefined") {
        async = true;
    }

    $.ajax({
        type: type,
        url: url,
        async: async,
        data: params,
        success: function (data) {
            ret = data;
            if (data.toString().indexOf("[error]") == 0) {
                alert(data);
            } else {
                if (fnOnAjaxEnd) { fnOnAjaxEnd(data); }
            }
            ajax_onEnd(sender, agrs, holder, data);
        }
    });

}

//请求开始前事件
function ajax_onStart(sender, agrs, holder) {
    loadingProcess_onStart();
    if ($(sender).length > 0) {
        $(sender).attr('disabled', 'disabled');
        $(sender).css('cursor', 'wait');

        window.setTimeout(function () { ajax_onEnd(sender); }, 60000);
    }

    if (agrs == "NonReturn") {
    }
    else if (agrs == "GetHtml") {
        if ($(holder).length > 0) {
            $(holder).html("<img src='/Content/Base/Images/loading-0.gif' alt='数据加载中...'/>");
        }
    }
}

//请求结束事件(无论是否成功)
function ajax_onEnd(sender, agrs, holder, data) {
    loadingProcess_onEnd();

    if ($(sender).length > 0) {
        $(sender).removeAttr('disabled');
        $(sender).css('cursor', 'pointer');
    }

    if (agrs == "NonReturn") {
        alert("操作成功！");
    } else if (agrs == "GetHtml") {
        if ($(holder).length > 0) {
            $(holder).html(data);
        }
    }
}
function loadingProcess_onStart() {
}

function loadingProcess_onEnd() {
}

$.filterSerializeJson = function (filterContainer) {
    var serializeObj = {};
    var array = $.filterSerializeArray(filterContainer);
    $(array).each(function () {
        if (serializeObj[this.name]) {
            if ($.isArray(serializeObj[this.name])) {
                serializeObj[this.name].push(this.value);
            } else {
                serializeObj[this.name] = [serializeObj[this.name], this.value];
            }
        } else {
            serializeObj[this.name] = this.value;
        }
    });
    return serializeObj;
};

// 打开窗体,标题,宽度,高度,回调函数
var openWindow = function(url, title, width, hight, bankFun) {
    //iframe窗
    layer.open({
        type: 2,
        title: title,
        closeBtn: 1, //不显示关闭按钮
        maxmin: true,
        area: [width, hight],
        shift: 2,
        content: [url, 'yes'],//iframe的url，no代表不显示滚动条
        end: function() {
            if (bankFun) bankFun();
        }
    });
};




