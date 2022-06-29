using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MTaaS.ContractGenerator.Configuration;
using MTaaS.ContractGenerator.Models;
using System.Text;

namespace MTaaS.ContractGenerator.Generators
{
    [Generator]
    public class OutputMetamorphosisGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var relations = GeneratorConfigurationLoader.Load(context);

            foreach (var relation in relations)
            {
                var sourceText = SourceText.From(GetClassSourceText(relation), Encoding.UTF8);

                context.AddSource($"Generated.{relation.Name}OutputMetamorphosis.cs", sourceText);
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
    public partial class {relation.Name}OutputMetamorphosis : IOutputMetamorphosis<{relation.Models.Output}>
    {{
        public partial {relation.Models.Output} Transform({relation.Models.Output} output);
    }}
}}";
        }
    }
}
