using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace MTaaS.AttributiveGenerator
{
    internal class RelationToGenerate
    {
        private readonly Dictionary<string, INamedTypeSymbol> _values = new()
        {
            [Constants.InputMetamorphosisAttribute] = null,
            [Constants.OutputMetamorphosisAttribute] = null,
            [Constants.ArtifactEntryPointAttribute] = null,
            [Constants.DataGeneratorAttribute] = null,
            [Constants.OutputModelComparerAttribute] = null
        };

        public string Name { get; set; }

        public string FormattedName => Name.Replace(' ', '_');

        public INamedTypeSymbol InputMetamorphosis => _values[Constants.InputMetamorphosisAttribute];

        public INamedTypeSymbol OutputMetamorphosis => _values[Constants.OutputMetamorphosisAttribute];

        public INamedTypeSymbol ArtifactEntryPoint => _values[Constants.ArtifactEntryPointAttribute];

        public INamedTypeSymbol DataGenerator => _values[Constants.DataGeneratorAttribute];

        public INamedTypeSymbol OutputModelComparer => _values[Constants.OutputModelComparerAttribute];

        public bool IsValid =>
            InputMetamorphosis != null &&
            OutputMetamorphosis != null &&
            ArtifactEntryPoint != null &&
            DataGenerator != null;

        public void SetValue(string attributeName, INamedTypeSymbol value)
        {
            _values[attributeName] = value;
        }

        public bool TryGetInputMetamorphosisMethod(out IMethodSymbol method)
        {
            var candidates = InputMetamorphosis
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(x => x.DeclaredAccessibility == Accessibility.Public &&
                            x.Parameters.Length == 1 &&
                            SymbolEqualityComparer.Default.Equals(x.ReturnType, x.Parameters[0].Type) &&
                            x.MethodKind == MethodKind.Ordinary)
                .ToList();

            if (candidates.Count == 1)
            {
                method = candidates[0];
                return true;
            }

            method = null;
            return false;
        }

        public bool TryGetOutputMetamorphosisMethod(ITypeSymbol inputType, out IMethodSymbol method)
        {
            var candidates = OutputMetamorphosis
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(x => x.DeclaredAccessibility == Accessibility.Public &&
                            x.Parameters.Length == 1 &&
                            SymbolEqualityComparer.Default.Equals(x.ReturnType, x.Parameters[0].Type) &&
                            !SymbolEqualityComparer.Default.Equals(x.ReturnType, inputType) &&
                            x.MethodKind == MethodKind.Ordinary)
                .ToList();

            if (candidates.Count == 1)
            {
                method = candidates[0];
                return true;
            }

            method = null;
            return false;
        }

        public bool TryGetDataGeneratorMethod(ITypeSymbol inputType, out IMethodSymbol method)
        {
            var candidates = DataGenerator
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(x => x.DeclaredAccessibility == Accessibility.Public &&
                            x.Parameters.Length == 1 &&
                            SymbolEqualityComparer.Default.Equals(x.ReturnType, inputType) &&
                            !SymbolEqualityComparer.Default.Equals(x.Parameters[0].Type, inputType) &&
                            x.MethodKind == MethodKind.Ordinary)
                .ToList();

            if (candidates.Count == 1)
            {
                method = candidates[0];
                return true;
            }

            method = null;
            return false;
        }

        public bool TryGetArtifactEntryPointMethod(ITypeSymbol inputType, ITypeSymbol outputType, out IMethodSymbol method)
        {
            var candidates = ArtifactEntryPoint
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(x => x.DeclaredAccessibility == Accessibility.Public &&
                            x.Parameters.Length == 1 &&
                            SymbolEqualityComparer.Default.Equals(x.Parameters[0].Type, inputType) &&
                            SymbolEqualityComparer.Default.Equals(x.ReturnType, outputType) &&
                            x.MethodKind == MethodKind.Ordinary)
                .ToList();

            if (candidates.Count == 1)
            {
                method = candidates[0];
                return true;
            }

            method = null;
            return false;
        }

        public bool IsOutputModelComparerValid(ITypeSymbol outputType)
        {
            if (OutputModelComparer == null)
            {
                return true;
            }

            var equalityComparerInterface = OutputModelComparer
                .AllInterfaces
                .FirstOrDefault(x => x.ContainingNamespace.ToDisplayString() == "System.Collections.Generic" &&
                            x.Name == "IEqualityComparer");

            if (equalityComparerInterface == null)
            {
                return false;
            }

            if (!SymbolEqualityComparer.Default.Equals(equalityComparerInterface.TypeArguments[0], outputType))
            {
                return false;
            }

            return true;
        }
    }
}
