using MTaaS.ContractSample.Helpers;
using MTaaS.ContractSample.Models;

namespace MTaaS.InputGenerators
{
    public partial class AddRowInputGenerator
    {
        public partial TableItem[] Generate(GeneratorModel model)
        {
            return SharedGenerator.Generate(model.Seed, model.ItemsCount);
        }
    }
}