function sendToNativeApp(message) {
  chrome.runtime.sendNativeMessage(
    "com.direct.print.host",
    message,
    (response) => {
      console.log("Native response:", response);
    }
  );
}

chrome.runtime.onMessage.addListener((msg) => {
  if (msg.type === "PRINT_LABEL") {
    sendToNativeApp(msg);
  }
});