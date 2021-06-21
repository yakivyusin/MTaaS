using MTaaS.Sample.Models;
using System.Linq;

namespace MTaaS.Metamorphoses
{
    public partial class AddRowInputMetamorphosis
    {
        public partial TableItem[] Transform(TableItem[] input)
        {
            return input
                .Union(new[] { new TableItem() })
                .ToArray();
        }
    }
}