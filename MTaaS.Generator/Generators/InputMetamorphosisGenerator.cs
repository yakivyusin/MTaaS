using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MTaaS.Generator.Configuration;
using MTaaS.Generator.Models;
using System.Diagnostics;
using System.Text;

namespace MTaaS.Generator.Generators
{
    [Generator]
    public class InputMetamorphosisGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var relations = GeneratorConfigurationLoader.Load(context);

            foreach (var relation in relations)
            {
                var sourceText = SourceText.From(GetClassSourceText(relation), Encoding.UTF8);

                context.AddSource($"Generated.{relation.Name}InputMetamorphosis.cs", sourceText);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private string GetClassSourceText(MetamorphicRelationConfiguration relation)
        {
            return $@"
using MTaaS.Contracts.Metamorphoses;

namespace MTaaS.Metamorphoses
{{
    public partial class {relation.Name}InputMetamorphosis : IInputMetamorphosis<{relation.Models.Input}>
    {{
        public partial {relation.Models.Input} Transform({relation.Models.Input} input);
    }}
}}";
        }
    }
}
