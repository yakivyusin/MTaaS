using MTaaS.Attributes.Metamorphoses;
using MTaaS.AttributiveSample.Models;
using System.Linq;

namespace MTaaS.AttributiveSample.Metamorphoses.Input
{
    [InputMetamorphosis("AddColumn")]
    public class AddColumnInputMetamorphosis
    {
        public object[] Transform(object[] input)
        {
            return input
                .OfType<TableItem>()
                .Select(x => new
                {
                    x.C1,
                    x.C2,
                    x.C3,
                    C4 = "C4"
                })
                .ToArray();
        }
    }
}