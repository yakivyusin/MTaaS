using MTaaS.Attributes.Metamorphoses;
using MTaaS.AttributiveSample.Models;
using System.Linq;

namespace MTaaS.AttributiveSample.Metamorphoses.Input
{
    [InputMetamorphosis("Reverse")]
    public class ReverseInputMetamorphosis
    {
        public TableItem[] Transform(TableItem[] input)
        {
            return input
                .Reverse()
                .ToArray();
        }
    }
}