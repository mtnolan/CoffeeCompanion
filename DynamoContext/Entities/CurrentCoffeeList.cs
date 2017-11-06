using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoContext.Entities
{
  [DynamoDBTable("Coffee", LowerCamelCaseProperties = true)]
  public class CurrentCoffeeList
  {
    public CurrentCoffeeList()
    {
      Stock = new List<Coffee>();
    }

    [DynamoDBHashKey]
    public string Key { get; set; }
    
    [DynamoDBProperty(Converter = typeof(CoffeeConverter))]
    public List<Coffee> Stock { get; set; }
  }
}
