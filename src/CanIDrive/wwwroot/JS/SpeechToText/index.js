$(document).ready(function () {
    $('.mic.demo_btn').click(function () {
        micOnClick();
    });
});

function startDemo() {
    resetSound();
    //stopRecording();
    textDisplay = "";
}

function resetSound() {
    stopWebSocket();
}
