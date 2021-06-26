﻿using System.IO;
using YetAnotherConsoleTables;

namespace MTaaS.MetamorphicRelations
{
    public partial class AddColumnMetamorphicRelation
    {
        private partial StringWriter RunArtifact(object[] input)
        {
            var stringWriter = new StringWriter();
            var table = ConsoleTable.From(input);

            table.Write(stringWriter);

            return stringWriter;
        }
    }
}