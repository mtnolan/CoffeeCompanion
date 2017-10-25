using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using CoffeeController.models;
using ControllerLib;
using HtmlAgilityPack;

namespace CoffeeController.coffee.searches
{
  public class CoffeeSearch :ICoffeeFunction
  {
    public async Task<LambdaProxyResponse> ExecuteFunction(
      ApiGatewayProxyRequest request,
      ILambdaContext context)
    {
      var requestBody = FunctionBody.GenerateFromRepspnse(request);
      var ser = new JsonSerializer();
      var htmlWeb = new HtmlWeb();

      var document = htmlWeb.Load(requestBody.url);

      var ogImage = GetMetaValue(document, "og:image");
      var title = GetTitle(document);
      var description = GetMetaValue(document, "description");

      var coffeeData = new CoffeeData {
        Title = title,
        Description = description,
        OGImage = ogImage,
      };

      var memStream = new MemoryStream();
      ser.Serialize(coffeeData, memStream);
      var body = memStream.ToString();
      memStream.Flush();

      return await Task.FromResult<LambdaProxyResponse>(
        new LambdaProxyResponse {
          statusCode = HttpStatusCode.OK,
          body = body,
        });
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
  }
}
