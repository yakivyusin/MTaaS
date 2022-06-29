using System;
using System.IO;
using System.Text;

namespace MTaaS.Metamorphoses
{
    public partial class ReverseOutputMetamorphosis
    {
        public partial StringWriter Transform(StringWriter output)
        {
            var outputLines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var stringBuilder = new StringBuilder();
            
            for (int i = 0; i < outputLines.Length; i++)
            {
                if (i <= 2)
                {
                    stringBuilder.AppendLine(outputLines[i]);
                }
                else
                {
                    stringBuilder.AppendLine(outputLines[outputLines.Length - i + 1]);
                }
            }
            
            return new StringWriter(stringBuilder);
        }
    }
}