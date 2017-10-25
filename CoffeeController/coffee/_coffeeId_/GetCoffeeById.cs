using System.Threading.Tasks;
using Amazon.Lambda.Core;
using ControllerLib;

namespace CoffeeController.coffee._coffeeId_
{
    public class GetCoffeeById : ICoffeeFunction
    {
      public Task<LambdaProxyResponse> ExecuteFunction(ApiGatewayProxyRequest request, ILambdaContext context) {
        throw new System.NotImplementedException();
      }
    }
}
