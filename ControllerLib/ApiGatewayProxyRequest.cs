using System;
using System.Collections.Generic;

namespace ControllerLib
{
    [Serializable]
    public class ApiGatewayProxyRequest
    {
        public string Resource { get; set; }

        public string Path { get; set; }

        public string HttpMethod { get; set; }
    
        public IDictionary<string, string> Headers { get; set; }

        public IDictionary<string, string> QueryStringParameters { get; set; }

        public IDictionary<string, string> PathParameters { get; set; }

        public IDictionary<string, string> StageVariables { get; set; }

        public string Body { get; set; }

        public string IsBase64Encoded { get; set; }
    }
}
