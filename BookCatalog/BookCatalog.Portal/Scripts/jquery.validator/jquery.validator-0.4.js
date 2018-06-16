/*
    jQuery Form Validators v0.4
    Website: http://validator.codeplex.com/
    License: http://validator.codeplex.com/license
    Examples:
    <input type='text' data-validator-group="groupName" data-validator-required="please enter a value" />
    <input type='text' data-validator-group="groupName" data-validator-email="invalid email" />
    <input type='text' data-validator-group="groupName" data-validator-regular="must be less than 6 chars long" data-validator-validexpress=".{6,}" />
    <input type='text' data-validator-group="groupName" data-validator-regular="must be less than 6 chars long" data-validator-invalidexpress=".{,6}" />
    <input type='text' data-validator-group="groupName" data-validator-compare="password mismatch" compareTo="button1" />
    <input type='text' data-validator-group="groupName" data-validator-custom="must be less than 6 chars long" data-validator-customfn="this.length < 6" />
    <input type='text' data-validator-group="groupName" data-validator-invalid="username cannot be used" data-validator-invalidVal="username" />
*/

var validate;

(function ($) {

    validate = function (group) {
        var marker = true;
        $("*[data-validator-group=" + group + "]").each(function (i, elm) {
            if (check(elm)) {
                $(elm).highlight();
                if (marker)
                    $(elm).find(":input").andSelf().focus();
                marker = false;
            }
            else
                $(elm).unhighlight();
        });
        return marker;
    }

    function revalidate() {
        if (!check(this))
            $(this).unhighlight();
        else
            $(this).highlight();
    }

    function check(elm) {
        var jelm = $(elm);

        var listsize = jelm.find("input:radio, input:checkbox").size();
        if (jelm.attr("disabled") || listsize > 0 && listsize == jelm.find("input:radio:disabled, input:checkbox:disabled").size())
            return "";

        //if empty value only perform required validation
        if ((jelm.val() == "" || jelm.val() == null || jelm.is("input:checkbox:not(':checked')")) && jelm.find("input:radio:checked, input:checkbox:checked").size() == 0)
            return jelm.data("validator-required") ? "validator-required" : "";

        if (jelm.data("validator-regular") && jelm.data("validator-validexpress") && !new RegExp(jelm.data("validator-validexpress"), "m").test(jelm.val()))
            return "validator-regular";

        if (jelm.data("validator-regular") && jelm.data("validator-invalidexpress") && new RegExp(jelm.data("validator-invalidexpress"), "m").test(jelm.val()))
            return "validator-regular";

        if (jelm.data("validator-compare") && $("#" + jelm.data("validator-compareto")).val() != jelm.val())
            return "validator-compare";

        if (jelm.data("validator-custom") && !new Function(jelm.data("validator-customfn")).call(elm))
            return "validator-custom";

        if (jelm.data("validator-invalid") && jelm.val() == jelm.data("validator-invalidval"))
            return "validator-invalid";

        if (validators != undefined) {
            for (var name in validators)
                if (jelm.data("validator-" + name) && !validators[name].call(elm))
                    return "validator-" + name;
        }
    }

    function showAlert() {
        var ctrl = $(this);
        var top = ctrl.offset().top + ctrl.height() + 4;
        var left = ctrl.offset().left + Math.max(ctrl.width() - 260, 0);
        ctrl.parents().each(function () {
            if ($(this).css("position") != "static" && (!$.browser.mozilla || $(this).css("display") != "table")) {
                var offset = $(this).offset();
                top -= offset.top;
                left -= offset.left;
                return false;
            }
        });
        ctrl.parent().children(".alertbox").remove();
        ctrl.parent().append("<div class='alertbox' style='top:" + top + "px; left:" + left + "px;'><div>" + ctrl.data(check(this)) + "</div></div>");
    }

    function hideAlert() {
        $(this).parent().children(".alertbox").remove();
    }

    $.fn.highlight = function () { this.addClass("highlight").focus(showAlert).blur(hideAlert).change(revalidate); return this; }
    $.fn.unhighlight = function () { this.removeClass("highlight").unbind("focus", showAlert).unbind("blur", hideAlert).parent().children(".alertbox").remove(); return this; }

})(jQuery);

//create your own custom validators below
//validator name is mapped to attribute name on form field (must be all lower case can use '-' to separate words) e.g. "email" => <input data-validator-email="error message" />
//validator function should return true if valid and false if not
var validators = {
    "email": function () { return new RegExp("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$", "mi").test(this.value); } //http://www.regular-expressions.info/email.html
};
