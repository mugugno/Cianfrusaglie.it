function parseFloatIgnoreCommas(number) {
    var numberNoCommas = number.replace(/,/g, '.');
    return parseFloat(numberNoCommas);
}
