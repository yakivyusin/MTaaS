using MTaaS.ContractSample.Models;
using System.IO;
using YetAnotherConsoleTables;

namespace MTaaS.MetamorphicRelations
{
    public partial class ReverseMetamorphicRelation
    {
        private partial StringWriter RunArtifact(TableItem[] input)
        {
            var stringWriter = new StringWriter();
            var table = ConsoleTable.From(input);

            table.Write(stringWriter);

            return stringWriter;
        }
    }
}