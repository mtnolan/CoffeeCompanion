using System.IO;
using Amazon.Lambda.Serialization.Json;


namespace ControllerLib
{
  public static class SerializerUtil
  {
    public static string Serialize<T>(T value)
    {
      var ser = new JsonSerializer();

      var memStream = new MemoryStream();
      ser.Serialize(value, memStream);
      memStream.Position = 0;
      var sr = new StreamReader(memStream);
      var json = sr.ReadToEnd();
      memStream.Flush();
      return json;
    }

    public static T Deserialize<T>(string obj)
    {
      var ser = new JsonSerializer();
      var memStream = new MemoryStream();

      var sw = new StreamWriter(memStream);
      sw.Write(obj);
      sw.Flush();

      memStream.Position = 0;
      return ser.Deserialize<T>(memStream);
    }

    public static T Deserialize<T>(MemoryStream stream)
    {
      var ser = new JsonSerializer();

      stream.Position = 0;
      return ser.Deserialize<T>(stream);
    }
  }
}
