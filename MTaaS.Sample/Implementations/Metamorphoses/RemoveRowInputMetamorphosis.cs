using MTaaS.Sample.Models;
using System.Linq;

namespace MTaaS.Metamorphoses
{
    public partial class RemoveRowInputMetamorphosis
    {
        public partial TableItem[] Transform(TableItem[] input)
        {
            return input
                .Take(input.Length - 1)
                .ToArray();
        }
    }
}