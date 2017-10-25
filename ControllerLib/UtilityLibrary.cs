using System.IO;
using Amazon.Lambda.Serialization.Json;

namespace ControllerLib
{
  public static class UtilityLibrary
  {
    public static string Serialize(object obj)
    {
      var ser = new JsonSerializer();
      var memStream = new MemoryStream();
      ser.Serialize(obj, memStream);
      memStream.Position = 0;
      var streamReader = new StreamReader(memStream);
      var str = streamReader.ReadToEnd();
      memStream.Flush();
      return str;
    }
  }
}
