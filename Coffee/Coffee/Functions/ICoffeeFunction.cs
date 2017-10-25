using Amazon.Lambda.Core;
using Amazon.S3;
using Coffee.enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Functions
{
  public interface ICoffeeFunction
  {
    Task<LambdaProxyResponse> ExecuteFunction(
      CoffeeAPIGatewayRequest request,
      ILambdaContext context
    );
  }
}
