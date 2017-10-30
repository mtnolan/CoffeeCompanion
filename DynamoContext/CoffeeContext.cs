using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Runtime.Internal.Util;

namespace DynamoContext
{
  public class CoffeeContext
  {
    private const string CurrentCoffeeKey = "current";

    private ILambdaContext _context;

    public CoffeeContext(ILambdaContext context) {
      _context = context;
    }

    public async Task<bool> AddCoffee(
      Guid id,
      string title,
      string description,
      string imageUrl
    ) {
      var dbContext = GetDynamoDbContext();

      var idString = id.ToString("D");

      var currentCoffeeList =
        dbContext.LoadAsync<CurrentCoffeeList>(CurrentCoffeeKey).Result;

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
        _context?.Logger.LogLine(e.ToString());
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
        // TODO: Add logging
        return false;
      }

      var matchingCoffee =
        currentCoffeeList.stock.FirstOrDefault(x => x["id"] == id);

      if (matchingCoffee == null) {
        // TODO: Add logging
        return true;
      }

      currentCoffeeList.stock.Remove(matchingCoffee);
      try {
        await dbContext.SaveAsync(currentCoffeeList);
      } catch (Exception e) {
        // TODO: Add logging
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
