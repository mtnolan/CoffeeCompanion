using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Tests
{
  /*
   *       new Core.Models.Coffee {
          Title = "",
          Image = "",
          Description = "",
        },
   */
    public static class TestData
    {
      public static List<Core.Models.Coffee> Coffee = new List<Core.Models.Coffee> {
        new Core.Models.Coffee {
          Title = "Giant Steps (Dark Roast)",
          Image = "https://cdn.shopify.com/s/files/1/0294/6861/products/Giant_Steps_large.png?v=1448379276",
          Description = "Our Premiere dark roast is the sinister step-brother to A Love Supreme, and highlights caramelized sugars within the coffee beans.",
        },
        new Core.Models.Coffee {
          Title = "Streetlevel",
          Image = "http://cdn.shopify.com/s/files/1/0035/9372/products/Streetlevel_Best_Sellers_grande.jpg?v=1490113524",
          Description = "The Streetlevel blend is special not only because of its reliable flavor profile, but also, by virtue of the fact that it shares a name with one of Verve’s guid",
        },
        new Core.Models.Coffee {
          Title = "",
          Image = "",
          Description = "The dawn of something great - Costa Rica meets Kenya for a brand new day in coffee. Layers of dark, silken berry meet bright, clean citrus sweetness.",
        },
        new Core.Models.Coffee {
          Title = "Costa Rica Aurora | Peet\'s Coffee",
          Image = "",
          Description = "The dawn of something great - Costa Rica meets Kenya for a brand new day in coffee. Layers of dark, silken berry meet bright, clean citrus sweetness.",
        },
      };
    }
}
