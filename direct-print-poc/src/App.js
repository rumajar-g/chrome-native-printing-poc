import './App.css';
import { useState } from "react";

function App() {
  const [text, setText] = useState("");

  const sendPrint = () => {
    if (!text.trim()) {
      alert("Please enter text to print");
      return;
    }

    const message = {
      type: "PRINT_LABEL",
      data: text
    };

    window.postMessage(message, "*");

    alert("Print request sent: " + text);
    setText("");
  };

  return (
    <div style={{ padding: 20 }}>
      <h2>Direct Print PoC</h2>

      <input
        type="text"
        placeholder="Enter label text"
        value={text}
        onChange={(e) => setText(e.target.value)}
        style={{ padding: 8, width: 300 }}
      />

      <br /><br />

      <button onClick={sendPrint}>
        Print Label
      </button>
    </div>
  );
}

export default App;