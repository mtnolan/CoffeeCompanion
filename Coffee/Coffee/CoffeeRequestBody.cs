using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Coffee
{

  [Serializable]
  class CoffeeRequestBody
  {
    [DataMember]
    public string Url { get; set; }

  }
}
