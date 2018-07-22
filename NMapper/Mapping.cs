using System;

namespace NMapper
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class Mapping : Attribute
    {
        private string name;
        public Mapping(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

    }
}
