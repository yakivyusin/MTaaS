using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTaaS.AttributiveGenerator
{
    internal class ClassSyntaxReceiver : ISyntaxContextReceiver
    {
        private readonly Dictionary<string, List<(INamedTypeSymbol Type, string Relation)>> _types = new()
        {
            [Constants.InputMetamorphosisAttribute] = new(),
            [Constants.OutputMetamorphosisAttribute] = new(),
            [Constants.ArtifactEntryPointAttribute] = new(),
            [Constants.DataGeneratorAttribute] = new(),
            [Constants.OutputModelComparerAttribute] = new()
        };

        public IReadOnlyDictionary<string, List<(INamedTypeSymbol Type, string Relation)>> Types => _types;

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is not ClassDeclarationSyntax classDeclarationNode)
            {
                return;
            }

            var typeSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationNode) as INamedTypeSymbol;

            var patternAttributes = typeSymbol
                .GetAttributes()
                .Where(x => _types.Keys.Contains(x.AttributeClass.ToDisplayString()))
                .ToArray();

            foreach (var patternAttribute in patternAttributes)
            {
                _types[patternAttribute.AttributeClass.ToDisplayString()]
                    .Add((typeSymbol, patternAttribute.ConstructorArguments.First().Value.ToString()));
            }
        }
    }
}
