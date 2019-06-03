#load "basic_parser.csx"

using static System.Console;
using static CANAPE.Cli.ConsoleUtils;
var template = new FixedProxyTemplate();
// Local port of 4444, destination 127.0.0.1:12345
template.LocalPort = 4444;
template.Host = "127.0.0.1";
template.Port = 12345;
template.AddLayer<Parser>();
var service = template.Create();
// Add an event handler to log a packet. Just print to console.
service.LogPacketEvent += (s,e) => WritePacket(e.Packet);
// Print to console when a connection is created or closed.
service.NewConnectionEvent += (s,e) =>
WriteLine("New Connection: {0}", e.Description);
service.CloseConnectionEvent += (s,e) =>
WriteLine("Closed Connection: {0}", e.Description);
service.Start();
WriteLine("Created {0}", service);
WriteLine("Press Enter to exit...");
ReadLine();
service.Stop();