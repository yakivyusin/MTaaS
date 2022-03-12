using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MTaaS.Metamorphoses
{
    public partial class RemoveColumnOutputMetamorphosis
    {
        public partial StringWriter Transform(StringWriter output)
        {
            var outputLines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var trimIndex = outputLines[1].LastIndexOf('|', outputLines[1].Length - 4) + 1;
            var resultLines = outputLines
                .Select(x => x.Remove(trimIndex))
                .ToArray();

            var stringBuilder = new StringBuilder();
            
            foreach (var resultLine in resultLines)
            {
                stringBuilder.AppendLine(resultLine);
            }
            
            return new StringWriter(stringBuilder);
        }
    }
}