using System.Net;
using System.Net.Sockets;
using System.Text;
using WindowsInput;
using WindowsInput.Native;

namespace Aluminium;

class Program
{
    static void Main(string[] args)
    {
        TcpListener server = null;
        try
        {
            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("0.0.0.0");

            server = new TcpListener(localAddr, port);
            server.Start();

            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected!");

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {data}");

                    SimulateKeyPress(data);
                }

                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"SocketException: {e}");
        }
        finally
        {
            server?.Stop();
        }
    }

static void SimulateKeyPress(string command)
{
    var simulator = new InputSimulator();

    Console.WriteLine($"Received command: {command}");

    switch (command.ToLower())
    {
        // Media Controls
        case "fullscreen":
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_F);
            break;
        case "playpause":
            simulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            break;
        case "forward":
            simulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
            break;
        case "backward":
            simulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            break;
        case "volumeup":
            simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
            break;
        case "volumedown":
            simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
            break;
        case "mute":
            simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_MUTE);
            break;
        case "nexttrack":
            simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
            break;
        case "previoustrack":
            simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
            break;
        case "ctrlshiftesc":
            simulator.Keyboard.ModifiedKeyStroke(
                new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT },
                VirtualKeyCode.ESCAPE
            );
            break;
        case "esc":
            simulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
            break;

        default:
            Console.WriteLine($"Unknown command: {command}");
            break;
    }
}
}