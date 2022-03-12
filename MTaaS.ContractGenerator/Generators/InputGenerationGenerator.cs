using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MTaaS.ContractGenerator.Configuration;
using MTaaS.ContractGenerator.Models;
using System.Text;

namespace MTaaS.ContractGenerator.Generators
{
    [Generator]
    public class InputGenerationGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var relations = GeneratorConfigurationLoader.Load(context);

            foreach (var relation in relations)
            {
                var sourceText = SourceText.From(GetClassSourceText(relation), Encoding.UTF8);

                context.AddSource($"Generated.{relation.Name}InputGenerator.cs", sourceText);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private string GetClassSourceText(MetamorphicRelationConfiguration relation)
        {
            return $@"
using MTaaS.Contracts;

namespace MTaaS.InputGenerators
{{
    public partial class {relation.Name}InputGenerator : IInputGenerator<{relation.Models.Generator}, {relation.Models.Input}>
    {{
        public partial {relation.Models.Input} Generate({relation.Models.Generator} model);
    }}
}}";
        }
    }
}
