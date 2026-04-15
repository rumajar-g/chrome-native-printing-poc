window.addEventListener("message", (event) => {
  if (event.data?.type === "PRINT_LABEL") {
    chrome.runtime.sendMessage(event.data);
  }
});