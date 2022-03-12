using MTaaS.ContractSample.Models;
using System.Linq;

namespace MTaaS.Metamorphoses
{
    public partial class ReverseInputMetamorphosis
    {
        public partial TableItem[] Transform(TableItem[] input)
        {
            return input
                .Reverse()
                .ToArray();
        }
    }
}