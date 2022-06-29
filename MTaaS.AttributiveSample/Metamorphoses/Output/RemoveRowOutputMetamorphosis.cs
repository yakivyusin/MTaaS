using MTaaS.Attributes.Metamorphoses;
using System;
using System.IO;
using System.Text;

namespace MTaaS.AttributiveSample.Metamorphoses.Output
{
    [OutputMetamorphosis("RemoveRow")]
    public class RemoveRowOutputMetamorphosis
    {
        public StringWriter Transform(StringWriter output)
        {
            var outputString = output.ToString();
            var stringBuilder = new StringBuilder(outputString);

            var lastIndex = outputString.LastIndexOf(Environment.NewLine, outputString.Length - 2);
            var prelastIndex = outputString.LastIndexOf(Environment.NewLine, lastIndex);

            stringBuilder.Remove(prelastIndex, outputString.Length - prelastIndex);
            stringBuilder.AppendLine();
            
            return new StringWriter(stringBuilder);
        }
    }
}