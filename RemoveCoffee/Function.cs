using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using ControllerLib;
using DynamoContext;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace RemoveCoffee
{
  public class Function :FunctionBase
  {
    protected override async Task<LambdaProxyResponse> ExecutionFunction(ApiGatewayProxyRequest request)
    {
      var requestBody = FunctionBody.GenerateFromRequest(request);
      var id = requestBody.id;

      if (string.IsNullOrWhiteSpace(id)) {
        return new LambdaProxyResponse {
          statusCode = HttpStatusCode.BadRequest,
          body = "No Coffee ID defined",
        };
      }

      var dbContext = new CoffeeContext(_context);
      var success = await dbContext.RemoveCoffee(id);

      return !success
        ? new LambdaProxyResponse {
          statusCode = HttpStatusCode.InternalServerError,
          body = "There was a problem processing the request.",
        }
        : new LambdaProxyResponse {
          statusCode = HttpStatusCode.OK,
        };

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
      public string id { get; set; }
    }

  }
}
