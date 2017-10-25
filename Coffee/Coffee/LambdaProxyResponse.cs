using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Coffee
{
  public class LambdaProxyResponse
  {
    public LambdaProxyResponse()
    {
      headers = new Dictionary<string, string>();
    }

    public Dictionary<string, string> headers { get; set; }

    public HttpStatusCode statusCode { get; set; }

    public string body { get; set; }
  }
}
