using MTaaS.Attributes.Metamorphoses;
using MTaaS.AttributiveSample.Models;
using System.Linq;

namespace MTaaS.AttributiveSample.Metamorphoses.Input
{
    [InputMetamorphosis("AddRow")]
    public class AddRowInputMetamorphosis
    {
        public TableItem[] Transform(TableItem[] input)
        {
            return input
                .Union(new[] { new TableItem() })
                .ToArray();
        }
    }
}