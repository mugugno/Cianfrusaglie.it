function parseFloatIgnoreCommas(number) {
    var numberNoCommas = number.replace(/,/g, '.');
    return parseFloat(numberNoCommas);
}

function displayTextMaxLen(maxLen, input, feedback) {
    feedback.html(maxLen +"/"+ maxLen);

    input.keyup(function () {
        var text_remaining = maxLen - input.val().length;
        feedback.html(text_remaining + "/"+ maxLen);
    });
}

function openFbPopUp(fburl, fbimgurl, fbtitle, fbsummary) {
    var sharerURL= "http://www.facebook.com/sharer/sharer.php?s=100&p[url]=" + encodeURI(fburl) + "&p[images][0]=" + encodeURI(fbimgurl) + "&p[title]=" + encodeURI(fbtitle) + "&p[summary]=" + encodeURI(fbsummary);
    window.open(
      sharerURL,
      "facebook-share-dialog",
      "width=626,height=436");
}