using System;
using System.Collections.Generic;
using Slack.Webhooks.Core;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ReportSpill
{
  public class Function
  {

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(string input, ILambdaContext context)
    {
      var message = new SlackMessage {
        Text = "<!channel>: There has been a spill at the coffee machine!!",
        IconUrl = new Uri("https://static1.squarespace.com/static/50a96108e4b0a8a5e3e2c959/5673222c9cadb60e5542d97e/5673223705f8e24f350ec97c/1450385982313/spilledcoffee_insta.jpg?format=1000w"),
      };

      var response = await new SlackClient("https://hooks.slack.com/services/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").PostAsync(message);

      context.Logger.LogLine(response.ErrorException.ToString());

      return (response.StatusCode == HttpStatusCode.OK).ToString();

    }
  }
}
