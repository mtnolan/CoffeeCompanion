using System;
using System.Collections.Generic;
using System.Text;
using Coffee.enums;
using Coffee.Functions;

namespace Coffee.Resources
{
    public class CoffeeSpillResource : ResourceBase
    {
        protected override IDictionary<HTTPVerb, Type> GetFunctionDictionary()
        {
            return new Dictionary<HTTPVerb, Type>
            {
                { HTTPVerb.POST, typeof(ReportSpillFunction) },
            };
        }
    }
}
