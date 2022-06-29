using MTaaS.AttributiveSample.Models;
using System;
using MTaaS.Attributes;

namespace MTaaS.AttributiveSample.DataGenerators
{
    [DataGenerator("AddRow")]
    [DataGenerator("RemoveRow")]
    [DataGenerator("Reverse")]
    public class TypedSharedGenerator
    {
        public TableItem[] Generate(GeneratorModel model)
        {
            var random = new Random(model.Seed.GetHashCode());
            var result = new TableItem[model.ItemsCount];

            for (int i = 0; i < model.ItemsCount; i++)
            {
                result[i] = new TableItem
                {
                    C1 = random.Next(1000, 9999),
                    C2 = random.NextDouble().ToString("0.0000"),
                    C3 = "Default"
                };
            };

            return result;
        }
    }
}
