using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTaaS.AttributiveGenerator
{
    [Generator]
    public class MTaaSGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor NotAllRelationParts = new(
            id: "MTAAS002",
            title: "Can't find all required parts for relation",
            messageFormat: "Can't find all required parts for {0} relation",
            category: "MT-as-a-Service",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor InputMetamorphosisMismatch = new(
            id: "MTAAS003",
            title: "Can't find input metamorphosis method for relation",
            messageFormat: "Can't find input metamorphosis method for {0} relation",
            category: "MT-as-a-Service",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor OutputMetamorphosisMismatch = new(
            id: "MTAAS004",
            title: "Can't find output metamorphosis method for relation",
            messageFormat: "Can't find output metamorphosis method for {0} relation",
            category: "MT-as-a-Service",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor DataGeneratorMismatch = new(
            id: "MTAAS005",
            title: "Can't find data generator method for relation",
            messageFormat: "Can't find data generator method for {0} relation",
            category: "MT-as-a-Service",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor ArtifactEntryPointMismatch = new(
            id: "MTAAS006",
            title: "Can't find artifact entry point method for relation",
            messageFormat: "Can't find artifact entry point method for {0} relation",
            category: "MT-as-a-Service",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor OutputModelComparerNoInterface = new(
            id: "MTAAS007",
            title: "Output model comparer must implement IEqualityComparer interface",
            messageFormat: "Output model comparer for {0} relation must implement IEqualityComparer<{1}> interface",
            category: "MT-as-a-Service",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not ClassSyntaxReceiver receiver)
            {
                return;
            }

            var relationsToGenerate = new Dictionary<string, RelationToGenerate>();
            foreach (var type in receiver
                .Types
                .SelectMany(x => x.Value.Select(v => (x.Key, v.Type, v.Relation))))
            {
                if (!relationsToGenerate.TryGetValue(type.Relation, out var relation))
                {
                    relation = new RelationToGenerate
                    {
                        Name = type.Relation
                    };
                    relationsToGenerate.Add(relation.Name, relation);
                }

                relation.SetValue(type.Key, type.Type);
            }

            foreach (var relation in relationsToGenerate)
            {
                GenerateRelation(context, relation.Value);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassSyntaxReceiver());
        }

        private void GenerateRelation(GeneratorExecutionContext context, RelationToGenerate relation)
        {
            if (!relation.IsValid)
            {
                context.ReportDiagnostic(Diagnostic.Create(NotAllRelationParts, Location.None, relation.Name));
                return;
            }

            if (!relation.TryGetInputMetamorphosisMethod(out var inputMetamorphosisMethod))
            {
                context.ReportDiagnostic(Diagnostic.Create(InputMetamorphosisMismatch, Location.None, relation.Name));
                return;
            }

            if (!relation.TryGetOutputMetamorphosisMethod(inputMetamorphosisMethod.ReturnType, out var outputMetamorphosisMethod))
            {
                context.ReportDiagnostic(Diagnostic.Create(OutputMetamorphosisMismatch, Location.None, relation.Name));
                return;
            }

            if (!relation.TryGetDataGeneratorMethod(inputMetamorphosisMethod.Parameters[0].Type, out var dataGeneratorMethod))
            {
                context.ReportDiagnostic(Diagnostic.Create(DataGeneratorMismatch, Location.None, relation.Name));
                return;
            }

            if (!relation.TryGetArtifactEntryPointMethod(inputMetamorphosisMethod.ReturnType, outputMetamorphosisMethod.ReturnType, out var artifactEntryPointMethod))
            {
                context.ReportDiagnostic(Diagnostic.Create(ArtifactEntryPointMismatch, Location.None, relation.Name));
                return;
            }

            if (!relation.IsOutputModelComparerValid(outputMetamorphosisMethod.ReturnType))
            {
                context.ReportDiagnostic(Diagnostic.Create(OutputModelComparerNoInterface, Location.None, relation.Name, outputMetamorphosisMethod.ReturnType.Name));
                return;
            }

            AddRelationSourceCode(context, relation, inputMetamorphosisMethod, outputMetamorphosisMethod, artifactEntryPointMethod);
            AddFunctionSourceCode(context, relation, dataGeneratorMethod);
        }

        private void AddRelationSourceCode(
            GeneratorExecutionContext context,
            RelationToGenerate relation,
            IMethodSymbol inputMetamorphosisMethod,
            IMethodSymbol outputMetamorphosisMethod,
            IMethodSymbol artifactEntryPointMethod)
        {
            var isArrayInput = inputMetamorphosisMethod.ReturnType.TypeKind == TypeKind.Array;
            var inputType = isArrayInput ?
                (inputMetamorphosisMethod.ReturnType as IArrayTypeSymbol).ElementType :
                inputMetamorphosisMethod.ReturnType;

            var inputMetamorphosisCall = inputMetamorphosisMethod.IsStatic ?
                $"{relation.InputMetamorphosis.Name}.{inputMetamorphosisMethod.Name}" :
                $"inputMetamorphosis.{inputMetamorphosisMethod.Name}";

            var outputMetamorphosisCall = outputMetamorphosisMethod.IsStatic ?
                $"{relation.OutputMetamorphosis.Name}.{outputMetamorphosisMethod.Name}" :
                $"outputMetamorphosis.{outputMetamorphosisMethod.Name}";

            var artifactEntryPointCall = artifactEntryPointMethod.IsStatic ?
                $"{relation.ArtifactEntryPoint.Name}.{artifactEntryPointMethod.Name}" :
                $"artifactRunner.{artifactEntryPointMethod.Name}";

            var equalityComparison = relation.OutputModelComparer == null ?
                "return output1.Equals(output2);" :
                $@"
            var equalityComparer = new {relation.OutputModelComparer.Name}();
            return equalityComparer.Equals(output1, output2);";

            var sourceCode = $@"
#pragma warning disable CS0105

using {relation.InputMetamorphosis.ContainingNamespace.ToDisplayString()};
using {inputType.ContainingNamespace.ToDisplayString()};
using {relation.OutputMetamorphosis.ContainingNamespace.ToDisplayString()};
using {relation.ArtifactEntryPoint.ContainingNamespace.ToDisplayString()};
{(relation.OutputModelComparer != null ? $"using {relation.OutputModelComparer.ContainingNamespace.ToDisplayString()};" : string.Empty)}

namespace MTaaS.MetamorphicRelations
{{
    public sealed class {relation.FormattedName}MetamorphicRelation
    {{
        public bool Validate({inputType.Name}{(isArrayInput ? "[]" : string.Empty)} input)
        {{
            {(inputMetamorphosisMethod.IsStatic ? string.Empty : $"var inputMetamorphosis = new {relation.InputMetamorphosis.Name}();")}
            {(outputMetamorphosisMethod.IsStatic ? string.Empty : $"var outputMetamorphosis = new {relation.OutputMetamorphosis.Name}();")}
            {(artifactEntryPointMethod.IsStatic ? string.Empty : $"var artifactRunner = new {relation.ArtifactEntryPoint.Name}();")}

            var output1 = {outputMetamorphosisCall}({artifactEntryPointCall}(input));
            var output2 = {artifactEntryPointCall}({inputMetamorphosisCall}(input));

            {equalityComparison}
        }}
    }}
}}";

            var sourceText = SourceText.From(sourceCode, Encoding.UTF8);
            context.AddSource($"Generated.{relation.Name}MetamorphicRelation.cs", sourceText);
        }

        private void AddFunctionSourceCode(
            GeneratorExecutionContext context,
            RelationToGenerate relation,
            IMethodSymbol dataGeneratorMethod)
        {
            var generatorCall = dataGeneratorMethod.IsStatic ?
                $"{relation.DataGenerator.Name}.{dataGeneratorMethod.Name}" :
                $"dataGenerator.{dataGeneratorMethod.Name}";

            var sourceCode = $@"
#pragma warning disable CS0105

using MTaaS.MetamorphicRelations;
using {relation.DataGenerator.ContainingNamespace.ToDisplayString()};
using {dataGeneratorMethod.Parameters[0].Type.ContainingNamespace.ToDisplayString()};
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Text.Json;
using System.IO;

namespace MTaaS.MetamorphicFunctions
{{
    public static class {relation.FormattedName}MetamorphicFunction
    {{
        [FunctionName(""{relation.Name}"")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, ""post"", Route = null)] HttpRequest req)
        {{
            using var body = new StreamReader(req.Body);

            var json = body.ReadToEnd();
            {(dataGeneratorMethod.IsStatic ? string.Empty : $"var dataGenerator = new {relation.DataGenerator.Name}();")}
            var generationModel = JsonSerializer.Deserialize<{dataGeneratorMethod.Parameters[0].Type.Name}>(json);

            var input = {generatorCall}(generationModel);
            var relation = new {relation.FormattedName}MetamorphicRelation();

            return new JsonResult(relation.Validate(input));
        }}
    }}
}}";

            var sourceText = SourceText.From(sourceCode, Encoding.UTF8);
            context.AddSource($"Generated.{relation.Name}MetamorphicFunction.cs", sourceText);
        }
    }
}