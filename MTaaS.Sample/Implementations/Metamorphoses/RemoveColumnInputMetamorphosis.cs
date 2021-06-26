using MTaaS.Sample.Models;
using System.Linq;

namespace MTaaS.Metamorphoses
{
    public partial class RemoveColumnInputMetamorphosis
    {
        public partial object[] Transform(object[] input)
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