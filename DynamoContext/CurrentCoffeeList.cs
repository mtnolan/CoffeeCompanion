using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoContext
{
  [DynamoDBTable("Coffee")]
  public class CurrentCoffeeList
  {
    public CurrentCoffeeList()
    {
      stock = new List<Dictionary<string, string>>();
    }

    public string key { get; set; }

    public List<Dictionary<string, string>> stock { get; set; }
  }
}
