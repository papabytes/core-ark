using System;
using System.Json;
using System.Linq;

namespace CoreArk.Packages.Extensions
{
    public static class JsonObjectExtensions
    {
        public static JsonValue GetPath(this JsonValue obj, string path, string clauseProperty = null, string clauseValue = null)
        {
            var splitPath = path.Split(".");

            var currentValue = obj;
            foreach (var pathpart in splitPath)
            {
                 currentValue = currentValue[pathpart];
                 
                 if (currentValue.JsonType == JsonType.Array)
                 {
                     var array = currentValue as JsonArray;
                     foreach (var jsonValue in array)
                     {
                         Console.WriteLine(jsonValue[clauseProperty].ToString());
                     }
                     currentValue = array.FirstOrDefault(a => a[clauseProperty].ToString() == clauseValue);
                 }
                 
            }
            return currentValue;
        }
    }
}