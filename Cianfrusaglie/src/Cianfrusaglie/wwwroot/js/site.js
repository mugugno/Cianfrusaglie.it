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