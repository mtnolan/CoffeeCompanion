using System;
using System.Collections.Generic;
using System.Linq;

namespace ControllerLib
{
  [Serializable]
  public class ApiGatewayProxyRequest
  {
    private const string USER_AGENT = "user-agent";
    private const string SLACKBOT = "slackbot";

    public string Resource { get; set; }

    public string Path { get; set; }

    public string HttpMethod { get; set; }

    public IDictionary<string, string> Headers { get; set; }

    public IDictionary<string, string> QueryStringParameters { get; set; }

    public IDictionary<string, string> PathParameters { get; set; }

    public IDictionary<string, string> StageVariables { get; set; }

    public string Body { get; set; }

    public string IsBase64Encoded { get; set; }
    
    public bool IsSlackRequest()
    {
      var key = Headers.Keys.FirstOrDefault(x => x.ToLower() == USER_AGENT);
      return key != null && Headers[key].ToLower().Contains(SLACKBOT);
    }
  }
}
