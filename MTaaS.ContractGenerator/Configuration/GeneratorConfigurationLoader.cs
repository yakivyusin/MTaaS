using Microsoft.CodeAnalysis;
using MTaaS.ContractGenerator.Models;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MTaaS.ContractGenerator.Configuration
{
    internal static class GeneratorConfigurationLoader
    {
        private const string ConfigurationFileName = "mrelations.yaml";
        private static readonly DiagnosticDescriptor ConfigNotFoundError = new DiagnosticDescriptor(id: "MTAAS001",
                                                                                              title: "Couldn't find MT-as-a-Service config file",
                                                                                              messageFormat: "Couldn't find {0} file.",
                                                                                              category: "MT-as-a-Service",
                                                                                              DiagnosticSeverity.Error,
                                                                                              isEnabledByDefault: true);

        public static MetamorphicRelationConfiguration[] Load(GeneratorExecutionContext context)
        {
            var configFile = context.AdditionalFiles.FirstOrDefault(x => x.Path.EndsWith(ConfigurationFileName));

            if (configFile == null)
            {
                context.ReportDiagnostic(Diagnostic.Create(ConfigNotFoundError, Location.None, ConfigurationFileName));
                return new MetamorphicRelationConfiguration[0];
            }

            var configFileText = configFile.GetText(context.CancellationToken).ToString();
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<MetamorphicRelationConfiguration[]>(configFileText);
        }
    }
}
