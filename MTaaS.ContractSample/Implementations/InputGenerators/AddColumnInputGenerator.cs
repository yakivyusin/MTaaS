using MTaaS.ContractSample.Helpers;
using MTaaS.ContractSample.Models;

namespace MTaaS.InputGenerators
{
    public partial class AddColumnInputGenerator
    {
        public partial object[] Generate(GeneratorModel model)
        {
            return SharedGenerator.Generate(model.Seed, model.ItemsCount);
        }
    }
}