using MTaaS.Attributes;
using System.IO;
using YetAnotherConsoleTables;

namespace MTaaS.AttributiveSample.ArtifactEntryPoints
{
    [ArtifactEntryPoint("AddColumn")]
    [ArtifactEntryPoint("RemoveColumn")]
    public class UntypedArtifactEntryPoint
    {
        public StringWriter RunArtifact(object[] input)
        {
            var stringWriter = new StringWriter();
            var table = ConsoleTable.From(input);

            table.Write(stringWriter);

            return stringWriter;
        }
    }
}
