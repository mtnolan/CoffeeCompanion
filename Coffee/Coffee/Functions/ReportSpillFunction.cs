using System;
using System.Net;
using Amazon.Lambda.Core;
//using Slack.Webhooks.Core;
using System.Threading.Tasks;
using Coffee.enums;

namespace Coffee.Functions
{
  public class ReportSpillFunction :ICoffeeFunction
  {
    public Task<LambdaProxyResponse> ExecuteFunction(CoffeeAPIGatewayRequest request, ILambdaContext context)
    {
      //      var message = new SlackMessage {
      //        Text = "<!channel>: There has been a spill at the coffee machine!!",
      //        IconUrl = new Uri("https://static1.squarespace.com/static/50a96108e4b0a8a5e3e2c959/5673222c9cadb60e5542d97e/5673223705f8e24f350ec97c/1450385982313/spilledcoffee_insta.jpg?format=1000w"),
      //      };
      //
      //      var response = await new SlackClient(CoffeeConstants.WebhookURI).PostAsync(message);
      //
      //      //context.Logger.LogLine(response.ErrorException.ToString());
      //
      //      return new LambdaProxyResponse {
      //        statusCode = response.StatusCode,
      //        body = string.Empty,
      //      };

      return Task.Run(
        () => {
          return new LambdaProxyResponse {
            statusCode = HttpStatusCode.OK,
            body = string.Empty,
          };
        });
    }
  }
}
