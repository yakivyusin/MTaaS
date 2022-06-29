namespace MTaaS.AttributiveGenerator
{
    internal static class Constants
    {
        private const string AttributesRootNamespace = $"{nameof(MTaaS)}.{nameof(MTaaS.Attributes)}.";
        private const string AttributesMetamorphosesNamespace = $"{AttributesRootNamespace}{nameof(MTaaS.Attributes.Metamorphoses)}.";

        public const string InputMetamorphosisAttribute = $"{AttributesMetamorphosesNamespace}{nameof(InputMetamorphosisAttribute)}";
        public const string OutputMetamorphosisAttribute = $"{AttributesMetamorphosesNamespace}{nameof(OutputMetamorphosisAttribute)}";
        public const string ArtifactEntryPointAttribute = $"{AttributesRootNamespace}{nameof(ArtifactEntryPointAttribute)}";
        public const string DataGeneratorAttribute = $"{AttributesRootNamespace}{nameof(DataGeneratorAttribute)}";
        public const string OutputModelComparerAttribute = $"{AttributesRootNamespace}{nameof(OutputModelComparerAttribute)}";
    }
}
