using System.Text;
using System.Text.Json;

class Program
{
    static void Main()
    {
        try
        {
            File.AppendAllText("debug.log", "STARTED\n");

            using var stdin = Console.OpenStandardInput();
            using var stdout = Console.OpenStandardOutput();

            byte[] lengthBytes = new byte[4];

            int read = stdin.Read(lengthBytes, 0, 4);
            File.AppendAllText("debug.log", $"Read length bytes: {read}\n");

            int length = BitConverter.ToInt32(lengthBytes, 0);

            byte[] buffer = new byte[length];
            stdin.Read(buffer, 0, length);

            string input = Encoding.UTF8.GetString(buffer);

            File.AppendAllText("debug.log", $"INPUT: {input}\n");

            var data = JsonSerializer.Deserialize<PrintMessage>(input);

            File.WriteAllText("print_output.txt", data?.data ?? "EMPTY");

            SendResponse(stdout, new { success = true });
        }
        catch (Exception ex)
        {
            File.AppendAllText("debug.log", "ERROR: " + ex.ToString() + "\n");
        }
    }

    static void SendResponse(Stream stdout, object response)
    {
        string json = JsonSerializer.Serialize(response);
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        stdout.Write(BitConverter.GetBytes(bytes.Length));
        stdout.Write(bytes);
        stdout.Flush();
    }

    class PrintMessage
    {
        public string type { get; set; }
        public string data { get; set; }
    }
}