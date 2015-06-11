window.AudioContext = window.AudioContext || window.webkitAudioContext;

var audioContext = null;
if (AudioContext) {
    audioContext = new AudioContext();
}
var audioRecorder = null;
var isRecording = false;
var audioSource = null;

function gotAudioStream(stream) {
    this.audioSource = stream;
    var inputPoint = this.audioContext.createGain();
    this.audioContext.createMediaStreamSource(this.audioSource).connect(inputPoint);
    this.audioRecorder = new Recorder(inputPoint);
    startWebSocketForMic();
    this.isRecording = true;
}

function toggleRecording() {
    if (this.isRecording) {
        stopRecording();
    } else {
        startRecording();
    }
}
function startRecording() {
    if (!this.isRecording) {
        this.isRecording = true;

    if (!navigator.getUserMedia) {
        navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
    }

    navigator.getUserMedia({
        "audio": true,
    }, gotAudioStream.bind(this), function (e) {
        window.alert('User rejected Microphone access.');
    });
}
}

function stopRecording() {
    if (this.isRecording) {
        this.isRecording = false;
        if (audioSource.stop) {
            audioSource.stop();
        }
        audioRecorder.stop();
        stopWebSocket();
        $("#SpokenText").text($('#messages').text());
        $("#SpokenTextForm").submit();
    }
}

function stopWebSocket() {
    if (websocket) {
        websocket.onmessage = function() {};
        websocket.onerror = function() {};
        websocket.onclose = function() {};
        websocket.close();
    }
}

function micOnClick() {
    if (this.isRecording) {
        this.stopRecording();
    }
    else {
        this.startRecording();
    }
}
