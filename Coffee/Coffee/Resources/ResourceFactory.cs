using System;
using System.Collections.Generic;

namespace Coffee.Resources
{
  public static class ResourceFactory {
    private static readonly IDictionary<string, Type> Resources =
      new Dictionary<string, Type> {
        {"/GetCoffeeDetailsFromURL", typeof(CoffeeResource)},
        {"/spills/report", typeof(CoffeeSpillResource)},
      };

    public static ResourceBase GetResource(CoffeeAPIGatewayRequest request)
    {
      return Resources.ContainsKey(request.Path)
        ? (ResourceBase) Activator.CreateInstance(Resources[request.Path])
        : null;
    }
  }
}
