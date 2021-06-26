using MTaaS.Sample.Models;
using System.Linq;

namespace MTaaS.Metamorphoses
{
    public partial class AddColumnInputMetamorphosis
    {
        public partial object[] Transform(object[] input)
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