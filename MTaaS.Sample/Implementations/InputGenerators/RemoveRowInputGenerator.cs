using MTaaS.Sample.Helpers;
using MTaaS.Sample.Models;

namespace MTaaS.InputGenerators
{
    public partial class RemoveRowInputGenerator
    {
        public partial TableItem[] Generate(GeneratorModel model)
        {
            return SharedGenerator.Generate(model.Seed, model.ItemsCount);
        }
    }
}