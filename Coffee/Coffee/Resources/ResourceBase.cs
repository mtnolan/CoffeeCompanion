using Coffee.enums;
using Coffee.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Resources
{
    public abstract class ResourceBase
    {
        protected abstract IDictionary<HTTPVerb, Type> GetFunctionDictionary();

        public ICoffeeFunction GetFunctionFromRequest(HTTPVerb verb)
        {
            var functionDict = GetFunctionDictionary();
            return functionDict.ContainsKey(verb)
                ? (ICoffeeFunction) Activator.CreateInstance(functionDict[verb])
                : null;
        }
    }
}
