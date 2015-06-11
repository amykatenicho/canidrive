$(document).ready(function () {
    $('.mic.demo_btn').click(function () {
        $("#micOff").hide();
        micOnClick();

    });
});

function startDemo() {
    resetSound();
    //stopRecording();
    textDisplay = "";
    $("#micOff").show();
    $("#micOn").hide();
}

function resetSound() {
    stopWebSocket();
}
