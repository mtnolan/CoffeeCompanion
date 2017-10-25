using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using ControllerLib;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CoffeeController
{
  public class Function
  {

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<LambdaProxyResponse> FunctionHandler(ApiGatewayProxyRequest request, ILambdaContext context)
    {
      if (!String.IsNullOrEmpty(request.Path)) {
        context.Logger.LogLine(request.Path);

      }



      return await Task.Run(() =>  new LambdaProxyResponse
      {
        body = "Pass",
        statusCode = HttpStatusCode.Accepted
      });
    }
  }
}
