using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ControllerLib
{
  public abstract class FunctionBase {
    protected ILambdaContext _context;

    public async Task<LambdaProxyResponse> FunctionHandler(ApiGatewayProxyRequest request, ILambdaContext context)
    {
      _context = context;

      LambdaProxyResponse response;
      try {
        response = await ExecutionFunction(request);
      } catch (Exception e) {
        _context?.Logger.LogLine(UtilityLibrary.Serialize(e));
        return new LambdaProxyResponse {
          statusCode = HttpStatusCode.InternalServerError,
          body = "Server Error",
        };
      }
      return response;
    }

    protected abstract Task<LambdaProxyResponse> ExecutionFunction(
      ApiGatewayProxyRequest request);
  }
}
