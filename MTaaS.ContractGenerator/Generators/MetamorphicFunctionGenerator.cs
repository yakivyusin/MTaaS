using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MTaaS.ContractGenerator.Configuration;
using MTaaS.ContractGenerator.Models;
using System.Text;

namespace MTaaS.ContractGenerator.Generators
{
    [Generator]
    public class MetamorphicFunctionGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var relations = GeneratorConfigurationLoader.Load(context);

            foreach (var relation in relations)
            {
                var sourceText = SourceText.From(GetClassSourceText(relation), Encoding.UTF8);

                context.AddSource($"Generated.{relation.Name}MetamorphicFunction.cs", sourceText);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private string GetClassSourceText(MetamorphicRelationConfiguration relation)
        {
            return $@"
using MTaaS.MetamorphicRelations;
using MTaaS.InputGenerators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Text.Json;
using System.IO;

namespace MTaaS.MetamorphicFunctions
{{
    public static class {relation.Name}MetamorphicFunction
    {{
        [FunctionName(""{relation.Name}"")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, ""post"", Route = null)] HttpRequest req)
        {{
            using var body = new StreamReader(req.Body);

            var json = body.ReadToEnd();
            var generator = new {relation.Name}InputGenerator();
            var generationModel = JsonSerializer.Deserialize<{relation.Models.Generator}>(json);

            var input = generator.Generate(generationModel);
            var relation = new {relation.Name}MetamorphicRelation();

            return new JsonResult(relation.Validate(input));
        }}
    }}
}}";
        }
    }
}
