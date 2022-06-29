using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MTaaS.ContractGenerator.Configuration;
using MTaaS.ContractGenerator.Models;
using System.Text;

namespace MTaaS.ContractGenerator.Generators
{
    [Generator]
    public class MetamorphicRelationGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var relations = GeneratorConfigurationLoader.Load(context);

            foreach (var relation in relations)
            {
                var sourceText = SourceText.From(GetClassSourceText(relation), Encoding.UTF8);

                context.AddSource($"Generated.{relation.Name}MetamorphicRelation.cs", sourceText);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private string GetClassSourceText(MetamorphicRelationConfiguration relation)
        {
            var equalityPart = string.IsNullOrEmpty(relation.Models.OutputEqualityComparer) ?
                "return output1.Equals(output2);" :
                $@"
                var equalityComparer = new {relation.Models.OutputEqualityComparer}();
                return equalityComparer.Equals(output1, output2);";

            return $@"
using MTaaS.Contracts;
using MTaaS.Metamorphoses;

namespace MTaaS.MetamorphicRelations
{{
    public partial class {relation.Name}MetamorphicRelation : IMetamorphicRelation<{relation.Models.Input},{relation.Models.Output}>
    {{
        public bool Validate({relation.Models.Input} input)
        {{
            var inputMetamorphosis = new {relation.Name}InputMetamorphosis();
            var outputMetamorphosis = new {relation.Name}OutputMetamorphosis();

            var output1 = outputMetamorphosis.Transform(RunArtifact(input));
            var output2 = RunArtifact(inputMetamorphosis.Transform(input));

            {equalityPart}
        }}

        private partial {relation.Models.Output} RunArtifact({relation.Models.Input} input);
    }}
}}";
        }
    }
}
