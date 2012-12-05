var _temText = "";
var _id = "";

function _SetText(obj) {
    _temText = obj.value;
}

function _StopDrop(evt, obj) {
    _temText = obj.value;
    _id = obj.id;
    var theEvent = evt || window.event;
    theEvent.returnValue = false;
    setTimeout("_ChangeText()", 100);
    return false;
}

function _False(evt) {
    var theEvent = evt || window.event;
    theEvent.returnValue = false;
}

function _ChangeText(obj) {
    document.getElementById(_id).value = _temText;
}

function _CheckText(obj, precission) {
    var isValid = true;
    var text = obj.value;
    if (text.indexOf(".") < 0) {
        if (text.length > precission) {
            obj.value = _temText;
        }
    }
}

function _Validate(evt, type, scale, precission, obj) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    var text = obj.value;

    if (window.event == null && navigator.appName == "Netscape") {
        if (key == 37 || key == 39 || key == 9) return true;
    }

    if (key == 8)
        return true;

    if (type == "numeric") {
        if (key < 48 || key > 57) {
            if (key == 46) {
                if (String.fromCharCode(key) == '.')
                    return false;
            }
            else {
                return false;
            }
        }
    } else if (type == "alphabet") {
        if ((key >= 97 && key <= 122) || (key >= 65 && key <= 90)) {
            return true;
        } else {
            return false;
        }
    } else if (type == "decimal") {
        if (key < 48 || key > 57) {
            if (key != 46) {
                return false;
            } else {
                // check the point is already exists or not
                var selectedText = GetSelection(obj);
                if (text.indexOf(".") >= 0 && selectedText.indexOf(".") < 0)
                    return false;
            }
        }

        var cursorPos;
        if (!document.selection)
            cursorPos = obj.selectionStart;
        else
            cursorPos = _GetCursorPosition();

        var isValid = _ValidateDecimal(text, key, cursorPos, scale, precission, obj);
        return isValid;

    }

    function _ValidateDecimal(text, key, cursorPos, scale, precission, obj) {
        var selectedText = GetSelection(obj);
        if (selectedText.length > 0) {
            if (selectedText.indexOf(".") < 0) {
                return true;
            }
            if (selectedText == text) {
                return true;
            }
            if (selectedText.indexOf(".") >= 0 && String.fromCharCode(key) == '.') {
                return true;
            }
        }
        var isValid = true;
        var currScale, currPrec;
        if (key == 46) {
            currScale = text.length - cursorPos;
            if (currScale > scale)
                isValid = false;
        } else {
            // no decimal present
            if (text.indexOf(".") == -1) {
                if (text.length == precission)
                    isValid = false;
            } else {
                var arr = text.split(".");
                if (cursorPos <= text.indexOf(".")) {
                    if (arr[0].length == precission)
                        isValid = false;
                }
                else {
                    if (arr[1].length == scale)
                        isValid = false;
                }
            }

        }

        return isValid;
    }

    function GetSelection(obj) {
        var textComponent = obj;
        var selectedText;
        // IE version
        if (document.selection != undefined) {
            textComponent.focus();
            var sel = document.selection.createRange();
            selectedText = sel.text;
        }
        // Mozilla version
        else if (textComponent.selectionStart != undefined) {
            var startPos = textComponent.selectionStart;
            var endPos = textComponent.selectionEnd;
            selectedText = textComponent.value.substring(startPos, endPos)
        }
        return selectedText;
    }

    function _GetCursorPosition() {
        var obj = document.activeElement;
        var cur = document.selection.createRange();
        var pos = 0;
        if (obj && cur) {
            var tr = obj.createTextRange();
            if (tr) {
                while (cur.compareEndPoints("StartToStart", tr) > 0) {
                    tr.moveStart("character", 1);
                    pos++;
                }
                return pos;
            }
        }
        return -1;
    }
}

function _FormatDecimal(obj, type, scale, precission) {
    var cVal = "";
    dcVal = obj.value;

    if (type == "decimal") {
        if (dcVal.length > 0) {
            if (dcVal.indexOf(".") == -1) { //No Decimal Point Present
                dcVal += ".";
                for (var i = 0; i < scale; i++) {
                    dcVal += "0";
                }
            }
            else {
                if (dcVal.indexOf(".") == 0) { //First Position of the number
                    cVal = "0" + dcVal;
                    // ::commented by amit::
                    /*
                    for (var j = 0; j < precission; j++) {
                        cVal += "0";
                    }
                    cVal = cVal + dcVal;
                    */
                    if (dcVal.indexOf(".") == (dcVal.length - 1)) {
                        for (var k = 0; k < scale; k++) {
                            cVal += "0";
                        }
                    }
                    dcVal = cVal;
                }
                else {
                    if (dcVal.indexOf(".") > 0 && dcVal.indexOf(".") == (dcVal.length - 1)) {
                        for (var l = 0; l < scale; l++) {
                            dcVal += "0";
                        }
                    }
                }
            }
        }
    }
    obj.value = dcVal;
    return true;
}

function _ValidateAlphabet(evt, obj) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;

    if (String.fromCharCode(key) == '>' || String.fromCharCode(key) == '<')
        return false;
}