
$(document).ready(function () {
    var isHTML5 = $('#hfHtml5').attr('data-ishtml5');
    if (isHTML5)
        initHTML5();
    else
        initFlash();
});

function GetWidth()
{
    return document.getElementById('hvGame').clientWidth;
}

function GetHeigth() {
    return $(window).height() - 120;
}
function initHTML5() {
    var nWitdh = GetWidth();
    var nHeight = GetHeigth();
    var gamePath = $('#hfGamePath').attr('data-game-path');
    document.getElementById('hvGame').innerHTML='<iframe src="' + gamePath + '" style="border:0px; width:'+ nWitdh + 'px; height:' + nHeight + 'px;"></iframe>';
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