var websocket = null;
var textDisplay = "";

function startWebSocketForMic() {
    startDemo();

    var uri = 'wss://www.projectoxford.ai/ws/Speech?language=en-US';
    websocket = getWebSocket(uri);
    websocket.onopen = function () {
        audioRecorder.sendHeader(websocket);
        audioRecorder.record(websocket);
    };
}

function getWebSocket(uri){
    websocket = new WebSocket(uri);

    websocket.onerror = function (event) {
        stopRecording();
    };

    websocket.onmessage = function (event) {
        var data = event.data.toString();
        if (data == null || data.length <= 0) {
            return;
        }

        var ch = data.charAt(0);
        var message = data.substring(1);
        if (ch == 'e') {
            stopRecording();
        }
        else {
            var text = textDisplay + message;
            if (ch == 'f') {
                textDisplay = text + " ";
            }

            $('#messages').text(text);
        }
    };

    websocket.onclose = function (event) {
        stopRecording();
    };

    return websocket;
}