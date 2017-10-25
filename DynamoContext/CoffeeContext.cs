using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoContext
{
  public class CoffeeContext
  {

    private const string CurrentCoffeeKey = "current";

    public async Task<bool> AddCoffee(
      Guid id,
      string title,
      string description,
      string imageUrl
    ) {
      var dbContext = GetDynamoDbContext();

      var idString = id.ToString("D");

      var currentCoffeeList =
        await dbContext.LoadAsync<CurrentCoffeeList>(CurrentCoffeeKey);

      if (currentCoffeeList == null) {
        currentCoffeeList = new CurrentCoffeeList {
          key = CurrentCoffeeKey
        };
      }

      if (currentCoffeeList.stock.Any(x => x["id"] == idString)) {
        // Already checked in
        return true;
      }

      currentCoffeeList.stock.Add(new Dictionary<string, string> {
        {"description", description },
        {"id", idString },
        {"image", imageUrl },
        {"title", title },
      });

      try {
        await dbContext.SaveAsync(currentCoffeeList);
      } catch (Exception e) {
        return false;
      }

      return true;
    }

    public async Task<bool> RemoveCoffee(string id)
    {
      var dbContext = GetDynamoDbContext();

      var currentCoffeeList =
        await dbContext.LoadAsync<CurrentCoffeeList>(CurrentCoffeeKey);

      if (currentCoffeeList == null) {
        // No current coffee list found
        return false;
      }

      var matchingCoffee =
        currentCoffeeList.stock.FirstOrDefault(x => x["id"] == id);

      if (matchingCoffee == null) {
        return true;
      }

      currentCoffeeList.stock.Remove(matchingCoffee);
      try {
        await dbContext.SaveAsync(currentCoffeeList);
      } catch (Exception e) {
        // Need to log here
        return false;
      }
      return true;
    }

    private static DynamoDBContext GetDynamoDbContext() {
      return new DynamoDBContext(
        new AmazonDynamoDBClient(Amazon.RegionEndpoint.USEast1));
    }
  }
}
