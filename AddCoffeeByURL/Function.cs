using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using ControllerLib;
using HtmlAgilityPack;
using DynamoContext;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AddCoffeeByURL
{
  public class Function :FunctionBase
  {
    protected override async Task<LambdaProxyResponse> ExecutionFunction(
      ApiGatewayProxyRequest request)
    {
      var requestBody = FunctionBody.GenerateFromRequest(request);
      var htmlWeb = new HtmlWeb();
      var url = requestBody.url;

      var document = htmlWeb.Load(url);

      var ogImage = GetMetaValue(document, "og:image");
      var title = GetTitle(document);
      var description = GetMetaValue(document, "description");

      var coffeeData = new CoffeeData {
        Title = title,
        Description = description,
        OGImage = ogImage,
      };

      var body = UtilityLibrary.Serialize(coffeeData);
      _context?.Logger.LogLine(body);

      var dbContext = new CoffeeContext();

      var id = GenerateIDFromURL(url);
      var success = await dbContext.AddCoffee(id, title, description, ogImage);

      return success
        ? new LambdaProxyResponse { statusCode = HttpStatusCode.Created }
        : new LambdaProxyResponse {
          statusCode = HttpStatusCode.InternalServerError
        };
    }

    private string GetTitle(HtmlDocument document)
    {
      var title = GetMetaValue(document, "og:title");
      if (title != null)
        return title;

      title = GetMetaValue(document, "title");
      if (title != null)
        return title;

      return document.DocumentNode.Descendants("title").FirstOrDefault()?.InnerText;
    }

    private string GetMetaValue(HtmlDocument document, string metaName)
    {
      try {
        var valueNode = document.DocumentNode.Descendants("meta").FirstOrDefault(x => x.GetAttributeValue("property", null) == metaName);

        if (valueNode != null) {
          return valueNode.GetAttributeValue("content", null);
        }

        valueNode = document.DocumentNode.Descendants("meta")
          .FirstOrDefault(x => x.GetAttributeValue("name", null) == metaName);

        return valueNode?.GetAttributeValue("content", null);

      } catch (Exception ex) {
        throw new Exception("Failed getting metadata tag: " + metaName, ex);
      }
    }

    private Guid GenerateIDFromURL(string url)
    {
      var uriBuilder = new UriBuilder(url);

      var uri = uriBuilder.Uri;
      var authority = Regex.Replace(
        uri.Authority,
        @"^[^.]*\.(?=\w+\.\w+$)",
        string.Empty);

      var urlString = authority + uri.AbsolutePath;

      var stringbytes = Encoding.UTF8.GetBytes(urlString);
      var hashedBytes = System.Security.Cryptography.SHA1.Create()
        .ComputeHash(stringbytes);
      Array.Resize(ref hashedBytes, 16);
      return new Guid(hashedBytes);
    }
  }

  [Serializable]
  internal class FunctionBody
  {
    public static FunctionBody GenerateFromRequest(
      ApiGatewayProxyRequest request
    )
    {
      return SerializerUtil.Deserialize<FunctionBody>(request.Body);
    }

    [DataMember]
    public string url { get; set; }
  }

  public class CoffeeData
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string OGImage { get; set; }
  }
}
