﻿using MTaaS.Attributes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MTaaS.AttributiveSample.OutputModelComparers
{
    [OutputModelComparer("AddRow")]
    [OutputModelComparer("RemoveRow")]
    [OutputModelComparer("Reverse")]
    [OutputModelComparer("AddColumn")]
    [OutputModelComparer("RemoveColumn")]
    public class StringWriterComparer : IEqualityComparer<StringWriter>
    {
        public bool Equals([AllowNull] StringWriter x, [AllowNull] StringWriter y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            return x?.ToString().Equals(y?.ToString()) ?? false;
        }

        public int GetHashCode([DisallowNull] StringWriter obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
