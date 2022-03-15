using MTaaS.Attributes.Metamorphoses;
using System;
using System.IO;
using System.Text;

namespace MTaaS.AttributiveSample.Metamorphoses.Output
{
    [OutputMetamorphosis("AddColumn")]
    public class AddColumnOutputMetamorphosis
    {
        public StringWriter Transform(StringWriter output)
        {
            var outputLines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var contentLine = " C4 |";
            var separatorLine = new string('-', contentLine.Length);

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < outputLines.Length; i++)
            {
                stringBuilder.Append(outputLines[i]);
                stringBuilder.AppendLine(i % 2 == 0 ? separatorLine : contentLine);
            }
            
            return new StringWriter(stringBuilder);
        }
    }
}