using System;

namespace MTaaS.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class DataGeneratorAttribute : Attribute
    {
        public string RelationName { get; }

        public DataGeneratorAttribute(string relationName)
        {
            RelationName = relationName;
        }
    }
}
