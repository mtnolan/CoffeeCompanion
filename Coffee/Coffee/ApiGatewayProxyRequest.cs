using System;
using System.IO;

namespace Coffee
{
    [Serializable]
    public abstract class ApiGatewayProxyRequest
    {

        public string Resource { get; set; }

        public string Path { get; set; }

        public string HttpMethod { get; set; }

        public string Headers { get; set; }

        public string QueryStringParameters { get; set; }

        public string PathParameters { get; set; }

        public string StageVariables { get; set; }

        public MemoryStream Body { get; set; }

        public string IsBase64Encoded { get; set; }
    }
}
