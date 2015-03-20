namespace RimDev.Descriptor.Generic
{
    public class DescriptorContainer<T> : IDescriptorContainer
    {
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }

        public DescriptorContainer<T> SetDescription(string description)
        {
            Description = description;

            return this;
        }

        public DescriptorContainer<T> SetName(string name)
        {
            Name = name;

            return this;
        }

        public DescriptorContainer<T> SetType(string type)
        {
            Type = type;

            return this;
        }
    }
}
