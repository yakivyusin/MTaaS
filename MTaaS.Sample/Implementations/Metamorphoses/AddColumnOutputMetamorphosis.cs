using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MTaaS.Metamorphoses
{
    public partial class AddColumnOutputMetamorphosis
    {
        public partial StringWriter Transform(StringWriter output)
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