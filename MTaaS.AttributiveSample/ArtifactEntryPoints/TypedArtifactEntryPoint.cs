using MTaaS.Attributes;
using MTaaS.AttributiveSample.Models;
using System.IO;
using YetAnotherConsoleTables;

namespace MTaaS.AttributiveSample.ArtifactEntryPoints
{
    [ArtifactEntryPoint("AddRow")]
    [ArtifactEntryPoint("RemoveRow")]
    [ArtifactEntryPoint("Reverse")]
    public class TypedArtifactEntryPoint
    {
        public StringWriter RunArtifact(TableItem[] input)
        {
            var stringWriter = new StringWriter();
            var table = ConsoleTable.From(input);

            table.Write(stringWriter);

            return stringWriter;
        }
    }
}
