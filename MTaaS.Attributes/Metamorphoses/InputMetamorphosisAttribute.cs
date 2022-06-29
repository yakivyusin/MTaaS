using System;

namespace MTaaS.Attributes.Metamorphoses
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InputMetamorphosisAttribute : Attribute
    {
        public string RelationName { get; }

        public InputMetamorphosisAttribute(string relationName)
        {
            RelationName = relationName;
        }
    }
}
