using MTaaS.Attributes.Metamorphoses;
using MTaaS.AttributiveSample.Models;
using System.Linq;

namespace MTaaS.AttributiveSample.Metamorphoses.Input
{
    [InputMetamorphosis("RemoveColumn")]
    public class RemoveColumnInputMetamorphosis
    {
        public object[] Transform(object[] input)
        {
            return input
                .OfType<TableItem>()
                .Select(x => new
                {
                    x.C1,
                    x.C2
                })
                .ToArray();
        }
    }
}