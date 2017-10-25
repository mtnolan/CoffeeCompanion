using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Coffee.Resources;
using Coffee.enums;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace Coffee
{
  public class Function
  {

    public Function() {

    }

    public async Task<LambdaProxyResponse> FunctionHandler(
      CoffeeAPIGatewayRequest request,
      ILambdaContext context
    ){
      var resource = ResourceFactory.GetResource(request);

      if (resource == null) {
        context?.Logger.LogLine("No resource fond for path");
        context?.Logger.LogLine(string.Format(
          "No resource fond for path[{0}]",
          request.Path
        ));
        return new LambdaProxyResponse {
          statusCode = HttpStatusCode.NotFound,
        };
      }

      context?.Logger.LogLine(string.Format(
        "Resource: {0}",
        resource.GetType().ToString()
      ));

      var verb = (HTTPVerb)Enum.Parse(typeof(HTTPVerb), request.HttpMethod);
      var function = resource.GetFunctionFromRequest(verb);

      if (function != null)
        return await function.ExecuteFunction(request, context);

      context?.Logger.LogLine("No function found for http verb");
      return new LambdaProxyResponse {
        statusCode = HttpStatusCode.NotFound,
      };
    }
  }
}
