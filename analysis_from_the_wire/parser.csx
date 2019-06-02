using CANAPE.Net.Layers;
using System.IO;
class Parser : DataParserNetworkLayer
{
protected override bool NegotiateProtocol( Stream serverStream, Stream clientStream)
{
var client = new DataReader(clientStream);
        var server = new DataWriter(serverStream);
// Read magic from client and write it to server.
uint magic = client.ReadUInt32();
        Console.WriteLine("Magic: {0:X}", magic);
        server.WriteUInt32(magic);
        // Return true to signal negotiation was successful.
        return true;
    }
}