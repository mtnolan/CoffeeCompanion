using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Linq;

namespace DynamoContext.Entities
{
  public class Coffee
  {
    public Coffee(string id, string description, string image, string title)
    {
      Id = id;
      Description = description ?? string.Empty;
      Image = image ?? string.Empty;
      Title = title ?? string.Empty;
    }

    public string Id { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string Title { get; set; }
  }

  public class CoffeeConverter : IPropertyConverter
  {
    private const string ID = "id";
    private const string DESCRIPTION = "description";
    private const string IMAGE = "image";
    private const string TITLE = "title";

    public object FromEntry(DynamoDBEntry entry)
    {
      var list = new List<Coffee>();

      foreach (var coffeeMap in entry.AsListOfDocument())
      {
        list.Add(new Coffee(coffeeMap[ID], coffeeMap[DESCRIPTION], coffeeMap[IMAGE], coffeeMap[TITLE]));
      }
      return list;
    }

    public DynamoDBEntry ToEntry(object value)
    {
      var list = new DynamoDBList();
      var coffeeList = ((List<Coffee>)value);
      foreach (var coffee in coffeeList)
      {
        list.Add(
          new Document(new Dictionary<string, DynamoDBEntry> {
            { ID, coffee.Id },
            { DESCRIPTION, coffee.Description },
            { IMAGE, coffee.Image },
            { TITLE, coffee.Title },
          })
        );
      }

      return list;
    }
  }
}
