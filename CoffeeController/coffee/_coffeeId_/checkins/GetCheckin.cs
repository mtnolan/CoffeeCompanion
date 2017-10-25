using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using ControllerLib;

namespace CoffeeController.coffee._coffeeId_.checkins
{
    public class GetCheckin : ICoffeeFunction
    {
      public Task<LambdaProxyResponse> ExecuteFunction(ApiGatewayProxyRequest request, ILambdaContext context) {
        throw new NotImplementedException();
      }
    }
}
