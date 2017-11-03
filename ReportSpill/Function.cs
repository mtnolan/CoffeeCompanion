using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using System.Net.Http;
using ControllerLib;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ReportSpill
{
    public class Function
    {
        public async Task<string> FunctionHandler(ApiGatewayProxyRequest input, ILambdaContext context)
        {
            var slackhookuri = "https://hooks.slack.com/services/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";
            var client = new SlackClient(new Uri(slackhookuri));

            var response = await client.SendMessageAsync("A spill has been reported!  If you were the brewer, please go help cleanup!");


            if (response.StatusCode == HttpStatusCode.OK)
            {
                return string.Format("Message successfully sent");
            }
            else
            {
                return string.Format("Failed to send message");
            }

            //  var message = new SlackMessage {
            //  Text = "<!channel>: There has been a spill at the coffee machine!!",
            //  IconUrl = new Uri("https://static1.squarespace.com/static/50a96108e4b0a8a5e3e2c959/5673222c9cadb60e5542d97e/5673223705f8e24f350ec97c/1450385982313/spilledcoffee_insta.jpg?format=1000w"),
            //};

            //var response = await new SlackClient("https://hooks.slack.com/services/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").PostAsync(message);

            //context.Logger.LogLine(response.ErrorException.ToString());

            //return (response.StatusCode == HttpStatusCode.OK).ToString();

        }
    }
}
