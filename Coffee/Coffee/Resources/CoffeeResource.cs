using System;
using System.Collections.Generic;
using Coffee.Functions;
using Coffee.enums;

namespace Coffee.Resources
{
    public class  CoffeeResource : ResourceBase
    {        
        protected override IDictionary<HTTPVerb, Type> GetFunctionDictionary()
        {
            return new Dictionary<HTTPVerb, Type>
            {
                { HTTPVerb.POST, typeof(AddCoffeeByURLFunction) },
            };
        }
    }
}
