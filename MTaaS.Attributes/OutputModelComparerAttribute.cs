using System;

namespace MTaaS.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class OutputModelComparerAttribute : Attribute
    {
        public string RelationName { get; }

        public OutputModelComparerAttribute(string relationName)
        {
            RelationName = relationName;
        }
    }
}
