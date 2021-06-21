using MTaaS.Sample.Helpers;
using MTaaS.Sample.Models;

namespace MTaaS.InputGenerators
{
    public partial class AddRowInputGenerator
    {
        public partial TableItem[] Generate(MTaaS.Sample.Models.GeneratorModel model)
        {
            return SharedGenerator.Generate(model.Seed, model.ItemsCount);
        }
    }
}