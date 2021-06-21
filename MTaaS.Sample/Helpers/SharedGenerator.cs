using MTaaS.Sample.Models;
using System;

namespace MTaaS.Sample.Helpers
{
    public static class SharedGenerator
    {
        public static TableItem[] Generate(Guid seed, int count)
        {
            var random = new Random(seed.GetHashCode());
            var result = new TableItem[count];

            for (int i = 0; i < count; i++)
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
