using System.Threading.Tasks;
using Amazon.Lambda.Core;
using ControllerLib;

namespace CoffeeController.coffee
{
    public class GetListOfCoffee : ICoffeeFunction
    {
      public Task<LambdaProxyResponse> ExecuteFunction(ApiGatewayProxyRequest request, ILambdaContext context) {
        throw new System.NotImplementedException();
      }
    }
}
