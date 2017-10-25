using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.S3Events;

using Amazon;
using Amazon.Lambda.Serialization.Json;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

using Coffee;
using CoffeeController;
using CoffeeController.models;
using ControllerLib;

namespace Coffee.Tests
{
  public class FunctionTest
  {
    [Fact]
    public async Task TestS3EventLambdaFunction()
    {

      // Invoke the lambda function and confirm the content type was returned.
      var function = new Function();
      
      var body = new Dictionary<string, string>();

      body.Add(
        "url", 
        "https://www.darkmattercoffee.com/products/giant-steps-1"
      );

      var bodyString = UtilityLibrary.Serialize(body);

      var contentType = await function.FunctionHandler(
          new CoffeeAPIGatewayRequest {
            Body = bodyString,
            HttpMethod = "POST",
            Path = "/GetCoffeeDetailsFromURL"
          },
          null
      );
      Assert.IsType<LambdaProxyResponse>(contentType);
      Console.WriteLine(contentType);
    }
  }
}
