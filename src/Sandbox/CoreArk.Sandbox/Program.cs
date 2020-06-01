using System;
using System.Collections.Generic;
using System.Json;
using CoreArk.Packages.Extensions;
using Newtonsoft.Json;

namespace CoreArk.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var myClass = new MyClass
            {
                Id = Guid.NewGuid(),
                MySubClass = new MySubClass
                {
                    Name = "Sandbox Bot"
                },
                ManySubClasses = new List<MySubClass>
                {
                    new MySubClass
                    {
                        Name = "Sandbox SubBot",
                        Type = SomeType.Type2
                    }
                }
            };

            var myClassAsJson = JsonConvert.SerializeObject(myClass);

            var jsonValue = JsonValue.Parse(myClassAsJson);
            Console.WriteLine($"{jsonValue.GetPath("mySubClass.name")}");
            Console.WriteLine($"{jsonValue.GetPath("manySubClasses.name", "type", "1")}");
            
            Console.WriteLine($"{myClass.GetValue(nameof(myClass.Id)).Value} => {myClass.GetValue(nameof(myClass.MySubClass) + ".Name").Value}");
            Console.WriteLine("...finished.");
            Console.ReadKey();
        }
    }

    public class MyClass
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("mySubClass")]
        public MySubClass MySubClass { get; set; }
        [JsonProperty("manySubClasses")]
        public ICollection<MySubClass> ManySubClasses { get; set; }
    }

    public class MySubClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public SomeType Type { get; set; }
    }

    public enum SomeType
    {
        Type1,
        Type2
    }
        
}