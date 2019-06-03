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
int CalcChecksum(byte[] data) {
int chksum = 0;
       foreach(byte b in data) {
           chksum += b;
}
       return chksum;
   }


DataFrame ReadData(DataReader reader) {
int length = reader.ReadInt32();
int chksum = reader.ReadInt32();
return reader.ReadBytes(length).ToDataFrame();
}
void WriteData(DataFrame frame, DataWriter writer) {
byte[] data = frame.ToArray();
writer.WriteInt32(data.Length);
writer.WriteInt32(CalcChecksum(data));
writer.WriteBytes(data);
}

protected override DataFrame ReadInbound(DataReader reader) {
return ReadData(reader);
}
protected override void WriteOutbound(DataFrame frame, DataWriter writer) {
       WriteData(frame, writer);
}
protected override DataFrame ReadOutbound(DataReader reader) {
       return ReadData(reader);
}

protected override void WriteInbound(DataFrame frame, DataWriter writer) {
       WriteData(frame, writer);
       }
}