using MTaaS.Attributes.Metamorphoses;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MTaaS.AttributiveSample.Metamorphoses.Output
{
    [OutputMetamorphosis("AddRow")]
    public class AddRowOutputMetamorphosis
    {
        public StringWriter Transform(StringWriter output)
        {
            var outputString = output.GetStringBuilder().ToString();
            var stringBuilder = new StringBuilder(outputString);

            var outputRows = outputString
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            stringBuilder.AppendLine(Regex.Replace(outputRows[^2], @"[^\s|]", " "));
            stringBuilder.AppendLine(outputRows[^1]);
            
            return new StringWriter(stringBuilder);
        }
    }
}