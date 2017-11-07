using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using ControllerLib;
using DynamoContext;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace RemoveCoffee
{
  public class Function : FunctionBase
  {
    protected override async Task<LambdaProxyResponse> ExecutionFunction(ApiGatewayProxyRequest request)
    {
      if (!request.PathParameters.ContainsKey("id"))
      {
        return new LambdaProxyResponse
        {
          statusCode = HttpStatusCode.BadRequest,
          body = "No coffee ID defined in path parameter.",
        };
      }

      var id = request.PathParameters["id"];

      var dbContext = new CoffeeContext(_context);
      var success = await dbContext.RemoveCoffee(id);

      return !success
        ? new LambdaProxyResponse
        {
          statusCode = HttpStatusCode.InternalServerError,
          body = "There was a problem processing the request.",
        }
        : new LambdaProxyResponse
        {
          statusCode = HttpStatusCode.OK,
        };
    }
  }
}
