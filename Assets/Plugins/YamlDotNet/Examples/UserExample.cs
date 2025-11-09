using System;
using System.IO;
using Xunit.Abstractions;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Samples.Helpers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static UnityEngine.EventSystems.EventTrigger;
using static YamlDotNet.Samples.DeserializeObjectGraph;
namespace YamlDotNet.Samples
{
    public class NodeInfo
    {
        public string descrip;
        public float price;
        public int quantity;
    }
    public class UserExample
    {
        private readonly ITestOutputHelper output;

        public UserExample(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Sample(
            DisplayName = "UserExample",
            Description = "Explains how to load YAML using the representation model."
        )]
        public void Main()
        {
            // Setup the input
            var input = new StringReader(Document);

            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);

            // Examine the stream
            var mapping =
                (YamlMappingNode)yaml.Documents[0].RootNode;

            var entry = ((YamlMappingNode)mapping.Children[2].Value).Children[0];
            output.WriteLine($"{((YamlScalarNode)entry.Key).Value}    {entry.Value}");

            var item1 = mapping.Children[new YamlScalarNode("item")];
            output.WriteLine($"key:{item1.ToString()}");
            //var item = (YamlScalarNode)item1;
            //using(var itemInput = new StringReader((item.Value)))
            //{
            //    var deserializer = new DeserializerBuilder()
            //        .WithNamingConvention(CamelCaseNamingConvention.Instance)
            //        .Build();

            //    var info = deserializer.Deserialize<NodeInfo>(input);
            //    output.WriteLine($"{info.descrip}, {info.quantity}, {info.price}");
            //}

            // List all the items
            //var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("items")];
            //foreach (YamlMappingNode item in items)
            //{
            //    output.WriteLine(
            //        "{0}\t{1}",
            //        item.Children[new YamlScalarNode("part_no")],
            //        item.Children[new YamlScalarNode("descrip")]
            //    );
            //}
        }

        private const string Document = @"---
            receipt:    Oz-Ware Purchase Invoice
            date:        2007-08-06
            customer:
                given:   Dorothy
                family:  Gale

            item:
                  part_no:   A4786
                  descrip:   Water Bucket (Filled)
                  price:     1.47
                  quantity:  4

            bill-to:  &id001
                street: |
                        123 Tornado Alley
                        Suite 16
                city:   East Westville
                state:  KS

            ship-to:  *id001

            specialDelivery:  >
                Follow the Yellow Brick
                Road to the Emerald City.
                Pay no attention to the
                man behind the curtain.
...";
    }
}
