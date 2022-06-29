using MTaaS.Attributes.Metamorphoses;
using MTaaS.AttributiveSample.Models;
using System.Linq;

namespace MTaaS.AttributiveSample.Metamorphoses.Input
{
    [InputMetamorphosis("RemoveRow")]
    public class RemoveRowInputMetamorphosis
    {
        public TableItem[] Transform(TableItem[] input)
        {
            return input
                .Take(input.Length - 1)
                .ToArray();
        }
    }
}