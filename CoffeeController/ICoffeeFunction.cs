using System.Threading.Tasks;
using Amazon.Lambda.Core;
using ControllerLib;

namespace CoffeeController
{
  public interface ICoffeeFunction
  {
    Task<LambdaProxyResponse> ExecuteFunction(
      ApiGatewayProxyRequest request,
      ILambdaContext context
    );
  }
}
