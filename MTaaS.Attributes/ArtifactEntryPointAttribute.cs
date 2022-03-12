using System;

namespace MTaaS.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ArtifactEntryPointAttribute : Attribute
    {
        public string RelationName { get; }

        public ArtifactEntryPointAttribute(string relationName)
        {
            RelationName = relationName;
        }
    }
}
