
$(document).ready(function () {
    initFlash();
});

function GetWidth()
{
    return document.getElementById('hvGame').clientWidth;
}

function GetHeigth() {
    return $(window).height() - 120;
}

function initFlash() {
    var flashvars = {};
    var params = {};
    var attributes = {};
    var nWitdh = GetWidth();
    var nHeight = GetHeigth();
    var gamePath = $('#hfGamePath').attr('data-game-path');
    swfobject.embedSWF(gamePath,
                       "hvGame",
                       nWitdh.toString(),
                       nHeight.toString(),
                       "10",
                       "expressInstall.swf",
                       flashvars,
                       params,
                       attributes);
}