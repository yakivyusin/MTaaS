using System;

namespace MTaaS.Attributes.Metamorphoses
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class OutputMetamorphosisAttribute : Attribute
    {
        public string RelationName { get; }

        public OutputMetamorphosisAttribute(string relationName)
        {
            RelationName = relationName;
        }
    }
}
