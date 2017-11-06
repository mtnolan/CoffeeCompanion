using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using ControllerLib;
using DynamoContext;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace GetCurrentCoffee
{
  public class Function : FunctionBase
  {
    protected override async Task<LambdaProxyResponse> ExecutionFunction(ApiGatewayProxyRequest request)
    {
      var dbContext = new CoffeeContext(_context);
      var coffee = await dbContext.GetCurrentCoffee();

      return new LambdaProxyResponse
      {
        statusCode = HttpStatusCode.OK,
        body = SerializerUtil.Serialize(coffee),
      };
    }
  }
}
