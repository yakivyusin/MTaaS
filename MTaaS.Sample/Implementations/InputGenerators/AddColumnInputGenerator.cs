using MTaaS.Sample.Helpers;
using MTaaS.Sample.Models;

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